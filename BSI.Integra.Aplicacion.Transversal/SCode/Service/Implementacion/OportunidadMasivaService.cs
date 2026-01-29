using AutoMapper;
using Azure.Storage.Blobs;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using Twilio.Jwt.AccessToken;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoDatoService
    /// Autor: Margiory Ramirez Neyra
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class OportunidadMasivaService : IOportunidadMasivaService
    {
        private IUnitOfWork _unitOfWork;
        private string token = string.Empty;
        private Mapper _mapper;
        public OportunidadMasivaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var objToken = _unitOfWork.AutenticacionServicioExternoRepository.ObtenerTokenpoId(1);//ValorEstatico.IdAutenticacionFacebookLeadsReportes
            token = objToken != null ? objToken.Valor : string.Empty;


        }

        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";
        private readonly string containerName = "oportunidades-masivas";



        #region Metodos Base

        #endregion



        public async Task<string> SubirArchivoAsync(IFormFile archivo)
        {
            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                string fileName = Path.GetFileName(archivo.FileName);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                using (var stream = archivo.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                return blobClient.Uri.ToString(); // Devuelve la URL del archivo en Azure
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al subir archivo: {ex.Message}");
                return null;
            }
        }

        public string InsertarArchivo(string nombreArchivo, string usuario)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.InsertarArchivo(nombreArchivo, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ProcesarOportunidadMasiva(IFormFile file, string usuario)
        {

            if (file == null || file.Length <= 0)
            {
                throw new BadRequestException("No se ha proporcionado un archivo o el archivo está vacío.");
            }

            try
            {
                List<InformacionBaseOportunidadMasiva> listaDatos = new List<InformacionBaseOportunidadMasiva>();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        InformacionBaseOportunidadMasiva datos = new InformacionBaseOportunidadMasiva();

                        datos.FilaExcel = row;
                        datos.Nombres = worksheet.Cells[row, 1].Value?.ToString();
                        datos.Apellidos = worksheet.Cells[row, 2].Value?.ToString();
                        datos.Correo = worksheet.Cells[row, 3].Value?.ToString();
                        datos.Celular = worksheet.Cells[row, 4].Value?.ToString();
                        datos.Pais = worksheet.Cells[row, 5].Value?.ToString();
                        datos.Ciudad = worksheet.Cells[row, 6].Value?.ToString();
                        datos.Cargo = worksheet.Cells[row, 7].Value?.ToString();
                        datos.AreaFormacion = worksheet.Cells[row, 8].Value?.ToString();
                        datos.AreaTrabajo = worksheet.Cells[row, 9].Value?.ToString();
                        datos.Industria = worksheet.Cells[row, 10].Value?.ToString();
                        datos.CentroCosto = worksheet.Cells[row, 11].Value?.ToString();
                        datos.Origen = worksheet.Cells[row, 12].Value?.ToString();
                        datos.Asesor = worksheet.Cells[row, 13].Value?.ToString();
                        datos.TipoDato = worksheet.Cells[row, 14].Value?.ToString();
                        datos.FaseOportunidad = worksheet.Cells[row, 15].Value?.ToString();

                        listaDatos.Add(datos);
                    }
                }
                var resultado = ProcesarInformacionOportunidades(listaDatos, usuario);

                //Construccion del mensaje de respuesta
                var sb = new StringBuilder();
                sb.AppendLine("El proceso de creación de oportunidades masivas ha finalizado.");
                sb.AppendLine();
                if (resultado == null || !resultado.Any())
                {
                    sb.AppendLine("✅ El proceso finalizó sin errores.");
                }
                else
                {
                    sb.AppendLine($"⚠️ Se identificaron {resultado.Count} registros con error.");
                    sb.AppendLine();
                    sb.AppendLine("Filas del archivo Excel con error:");
                    foreach (var fila in resultado.Select(x => x.FilaExcel).Distinct())
                    {
                        sb.AppendLine($"- Fila {fila}");
                    }
                }
                sb.AppendLine();
                sb.AppendLine("Este correo es unicamente informativo. No responder.");
                string mensaje = sb.ToString();
                mensaje = mensaje.Replace(Environment.NewLine, "<br/>").Replace("\n", "<br/>");

                //Envio del correo proceso finalizado
                List<string> correosAlerta = new List<string>();
                correosAlerta.Add("mkilimajer@bsginstitute.com");
                var mailServiceAlerta = new TMK_MailService();
                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                mailDataAlerta.Sender = "loscataf@bsginstitute.com";
                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                mailDataAlerta.Subject = "Proceso Creacion Oportunidades Masivas - Finalizado";
                mailDataAlerta.Message = mensaje;
                mailDataAlerta.Bcc = string.Empty;
                mailDataAlerta.AttachedFiles = null;
                mailServiceAlerta.SetData(mailDataAlerta);
                mailServiceAlerta.SendMessageTask();

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<InformacionBaseOportunidadMasiva> ProcesarInformacionOportunidades(List<InformacionBaseOportunidadMasiva> datos, string usuario)
        {
            var servicio = new OportunidadService(_unitOfWork);
            try
            {
                var datosCorrectos = new List<InformacionBaseOportunidadMasiva>();
                var datosIncorrectos = new List<InformacionBaseOportunidadMasiva>();

                var listaCargos = _unitOfWork.CargoRepository.ObtenerCombo();
                var listaAreaFormacion = _unitOfWork.AreaFormacionRepository.ObtenerCombo();
                var listaAreaTrabajo = _unitOfWork.AreaTrabajoRepository.ObtenerCombo();
                var listaIndustria = _unitOfWork.IndustriaRepository.ObtenerCombo();
                var listaPaises = _unitOfWork.PaisRepository.ObtenerCombo();
                var listaCiudad = _unitOfWork.CiudadRepository.ObtenerCombo();
                datos.ForEach(opo =>
                {
                    var idAlumno = 0;
                    var idPais = 0;
                    var idCiudad = 0;
                    //validar alumno
                    var alumno = _unitOfWork.AlumnoRepository.FirstBy(x => !string.IsNullOrEmpty(x.Email1) && x.Email1.ToLower() == opo.Correo.Trim().ToLower());
                    if (alumno == null)
                    {
                        alumno = _unitOfWork.AlumnoRepository.FirstBy(x => !string.IsNullOrEmpty(x.Email2) && x.Email2.ToLower() == opo.Correo.Trim().ToLower());
                        if (alumno != null)
                        {
                            idAlumno = alumno.Id;
                        }
                        else
                        {
                            alumno = new();
                        }
                    }
                    else
                    {
                        idAlumno = alumno.Id;
                    }
                    IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                    var nombres = opo.Nombres.Trim().Split(" ");
                    var nombre1 = string.Empty;
                    var nombre2 = string.Empty;

                    if (nombres.Count() > 0)
                    {
                        if (nombres.Count() == 1)
                        {
                            nombre1 = nombres.ElementAt(0);
                        }
                        else
                        {
                            nombre1 = nombres.ElementAt(0);
                            nombre2 = string.Join(" ", nombres.Skip(1).ToList());
                        }
                    }

                    var apellidos = opo.Apellidos.Trim().Split(" ");
                    var apellidoPaterno = string.Empty;
                    var apellidoMaterno = string.Empty;

                    if (apellidos.Count() > 0)
                    {
                        if (apellidos.Count() == 1)
                        {
                            apellidoPaterno = apellidos.ElementAt(0);
                        }
                        else
                        {
                            apellidoPaterno = apellidos.ElementAt(0);
                            apellidoMaterno = string.Join(" ", apellidos.Skip(1).ToList());
                        }
                    }

                    var cargo = listaCargos.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Cargo).ToLower());
                    var idCargo = 24;
                    if (cargo != null)
                    {
                        idCargo = cargo.Id;
                    }

                    var aFormacion = listaAreaFormacion.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.AreaFormacion).ToLower());
                    var idAFormacion = 153;
                    if (aFormacion != null)
                    {
                        idAFormacion = aFormacion.Id;
                    }

                    var aTrabajo = listaAreaTrabajo.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.AreaTrabajo).ToLower());
                    var idATrabajo = 27;
                    if (aTrabajo != null)
                    {
                        idATrabajo = aTrabajo.Id;
                    }

                    var industria = listaIndustria.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Industria).ToLower());
                    var idIndustria = 24;
                    if (industria != null)
                    {
                        idIndustria = industria.Id;
                    }

                    var pais = listaPaises.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Pais).ToLower());
                    if (pais != null)
                    {
                        idPais = pais.Id;
                    }

                    var ciudad = listaCiudad.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Ciudad).ToLower());
                    if (ciudad != null)
                    {
                        idCiudad = ciudad.Id;
                    }

                    var centroCosto = _unitOfWork.CentroCostoRepository.FirstBy(x => x.Nombre == opo.CentroCosto);
                    var idCentroCosto = 0;
                    if (centroCosto != null)
                    {
                        idCentroCosto = centroCosto.Id;
                    }
                    else
                    {
                        throw new BadRequestException("No se encontro el centro costo");
                    }

                    var origen = _unitOfWork.OrigenRepository.FirstBy(x => x.Nombre == opo.Origen);
                    var idOrigen = 0;
                    if (origen != null)
                    {
                        idOrigen = origen.Id;
                    }
                    else
                    {
                        throw new BadRequestException("No se encontro el origen");
                    }

                    string celular = new string(opo.Celular.Where(char.IsDigit).ToArray());

                    var dtoOportunidad = new OportunidadFormularioDTO();
                    dtoOportunidad.Id = 0;
                    dtoOportunidad.IdCentroCosto = idCentroCosto;
                    dtoOportunidad.IdFaseOportunidad = ValorEstatico.IdFaseOportunidadBNC;
                    dtoOportunidad.IdOrigen = idOrigen;
                    dtoOportunidad.IdPersonal_Asignado = ValorEstatico.IdPersonalAsignacionAutomatica;
                    dtoOportunidad.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                    dtoOportunidad.UltimoComentario = string.Empty;
                    dtoOportunidad.fk_id_tipointeraccion = 18;

                    if (idAlumno == 0)
                    {
                        dtoOportunidad.IdAlumno = 0;

                        var alumnoDTO = new AlumnoFormularioOportunidadDTO();
                        alumnoDTO.Id = 0;
                        alumnoDTO.Nombre1 = nombre1;
                        alumnoDTO.Nombre2 = nombre2;
                        alumnoDTO.ApellidoPaterno = apellidoPaterno;
                        alumnoDTO.ApellidoMaterno = apellidoMaterno;
                        alumnoDTO.DNI = string.Empty;
                        alumnoDTO.Direccion = string.Empty;
                        alumnoDTO.Telefono = string.Empty;
                        alumnoDTO.Celular = celular;
                        alumnoDTO.Email1 = opo.Correo.Trim();
                        alumnoDTO.Email2 = string.Empty;
                        alumnoDTO.IdCargo = idCargo;
                        alumnoDTO.IdAFormacion = idAFormacion;
                        alumnoDTO.IdATrabajo = idATrabajo;
                        alumnoDTO.IdIndustria = idIndustria;
                        alumnoDTO.IdReferido = null;
                        alumnoDTO.IdCodigoPais = idPais;
                        alumnoDTO.IdCodigoCiudad = idCiudad;
                        alumnoDTO.HoraContacto = null;
                        alumnoDTO.HoraPeru = null;
                        alumnoDTO.Telefono2 = string.Empty;
                        alumnoDTO.Celular2 = string.Empty;
                        alumnoDTO.IdEmpresa = null;
                        alumnoDTO.Comentario = string.Empty;

                        var dto = new RegistroOportunidadAlumnoDTO()
                        {
                            Alumno = alumnoDTO,
                            Oportunidad = dtoOportunidad,
                            //FechaRegistroCampania = opo.FechaRegistroCampania,
                            Usuario = usuario
                        };
                        try
                        {
                            CrearOportunidadCrearAlumnoVentas(dto);
                            datosCorrectos.Add(opo);
                        }
                        catch
                        {
                            datosIncorrectos.Add(opo);
                        }

                    }
                    else
                    {
                        //Buscar ultima oportunidad por alumno
                        OportunidadFaseDTO datosUltimaOportunidad = _unitOfWork.OportunidadRepository.ObtenerFaseUltimaOportunidadPorIdAlumno(idAlumno);

                        //Fases cerradas: NI,BIC,BIC1,BIC2,RN3,RN1,RN4,RN5,BRM1,NS,E,RN
                        int[] idsFasesCerradas = { 1, 3, 4, 6, 7, 9, 11, 14, 26, 27, 29, 36 };
                        //Fases respuesta negativa temporal: RN2-A, RN2-B, RN2-C
                        int[] idsFasesRespuestaNegativaTemporal = { 10, 41, 42 };

                        //Cambiar asesor automatico a su asesor anterior si corresponde
                        if (idsFasesRespuestaNegativaTemporal.Contains(datosUltimaOportunidad.IdFaseOportunidad))
                        {
                            dtoOportunidad.IdPersonal_Asignado = datosUltimaOportunidad.IdPersonal_Asignado;
                        }

                        //Procesar oportunidades como BNC
                        if (idsFasesCerradas.Contains(datosUltimaOportunidad.IdFaseOportunidad) || idsFasesRespuestaNegativaTemporal.Contains(datosUltimaOportunidad.IdFaseOportunidad))
                        {
                            dtoOportunidad.IdAlumno = alumno.Id;
                            var alumnoDTO = new AlumnoFormularioOportunidadDTO();
                            alumnoDTO.Id = alumno.Id;
                            alumnoDTO.Nombre1 = nombre1;
                            alumnoDTO.Nombre2 = nombre2;
                            alumnoDTO.ApellidoPaterno = apellidoPaterno;
                            alumnoDTO.ApellidoMaterno = apellidoMaterno;
                            alumnoDTO.DNI = alumno.Dni;
                            alumnoDTO.Direccion = alumno.Direccion;
                            alumnoDTO.Telefono = alumno.Telefono;
                            alumnoDTO.Celular = celular;
                            alumnoDTO.Email1 = alumno.Email1;
                            alumnoDTO.Email2 = alumno.Email2;
                            alumnoDTO.IdCargo = idCargo;
                            alumnoDTO.IdAFormacion = idAFormacion;
                            alumnoDTO.IdATrabajo = idATrabajo;
                            alumnoDTO.IdIndustria = idIndustria;
                            alumnoDTO.IdReferido = alumno.IdReferido;
                            alumnoDTO.IdCodigoPais = alumno.IdPais ?? idPais;
                            alumnoDTO.IdCodigoCiudad = idCiudad;
                            alumnoDTO.HoraContacto = alumno.HoraContacto;
                            alumnoDTO.HoraPeru = alumno.HoraPeru;
                            alumnoDTO.Telefono2 = alumno.Telefono2;
                            alumnoDTO.Celular2 = alumno.Celular2;
                            alumnoDTO.IdEmpresa = alumno.IdEmpresa;
                            alumnoDTO.Comentario = alumno.Comentario;

                            var dto = new RegistroOportunidadAlumnoDTO()
                            {
                                Alumno = alumnoDTO,
                                Oportunidad = dtoOportunidad,
                                Usuario = usuario
                            };
                            try
                            {
                                ActualizarAlumnoCrearOportunidadVentas(dto);
                                datosCorrectos.Add(opo);
                            }
                            catch
                            {
                                datosIncorrectos.Add(opo);
                            }
                        }
                    }
                });

                return datosIncorrectos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string LimpiarCadena(string valor)
        {
            string valorSinTildes = Regex.Replace(valor.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            string cadenaLimpia = Regex.Replace(valorSinTildes, @"\s+", " ");
            return cadenaLimpia.Trim();
        }

        public bool CrearOportunidadCrearAlumnoVentas(RegistroOportunidadAlumnoDTO formulario)
        {
            var asignacionDatosAutomatico = new AsignacionRegularAutomaticaService(_unitOfWork);
            var servicio = new OportunidadService(_unitOfWork);
            try
            {
                var email1 = Regex.Replace(formulario.Alumno.Email1, @"\s", "");
                var email2 = Regex.Replace(formulario.Alumno.Email2, @"\s", "");

                Alumno alumno = new Alumno
                {
                    Nombre1 = formulario.Alumno.Nombre1,
                    Nombre2 = formulario.Alumno.Nombre2,
                    ApellidoPaterno = formulario.Alumno.ApellidoPaterno,
                    ApellidoMaterno = formulario.Alumno.ApellidoMaterno,
                    Direccion = formulario.Alumno.Direccion,
                    Telefono = formulario.Alumno.Telefono,
                    Celular = formulario.Alumno.Celular,
                    Email1 = email1,
                    Email2 = email2,
                    IdCargo = formulario.Alumno.IdCargo,
                    IdAformacion = formulario.Alumno.IdAFormacion,
                    IdAtrabajo = formulario.Alumno.IdATrabajo,
                    IdIndustria = formulario.Alumno.IdIndustria,
                    IdReferido = formulario.Alumno.IdReferido,
                    IdCodigoPais = formulario.Alumno.IdCodigoPais,
                    IdCiudad = formulario.Alumno.IdCodigoCiudad,
                    HoraContacto = formulario.Alumno.HoraContacto,
                    HoraPeru = formulario.Alumno.HoraPeru,
                    IdEmpresa = (formulario.Alumno.IdEmpresa == 0 || formulario.Alumno.IdEmpresa == -1) ? null : formulario.Alumno.IdEmpresa,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = formulario.Usuario,
                    UsuarioModificacion = formulario.Usuario,
                    Comentario = formulario.Alumno.Comentario
                };

                OportunidadBoDTO oportunidad = new OportunidadBoDTO
                {
                    IdCentroCosto = formulario.Oportunidad.IdCentroCosto,
                    IdPersonalAsignado = formulario.Oportunidad.IdPersonal_Asignado,
                    IdTipoDato = formulario.Oportunidad.IdTipoDato,
                    IdFaseOportunidad = formulario.Oportunidad.IdFaseOportunidad,
                    IdOrigen = formulario.Oportunidad.IdOrigen,
                    UltimoComentario = formulario.Oportunidad.UltimoComentario,
                    IdTipoInteraccion = formulario.Oportunidad.fk_id_tipointeraccion,
                    FechaRegistroCampania = formulario.FechaRegistroCampania ?? DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = formulario.Usuario,
                    UsuarioModificacion = formulario.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Alumno = alumno
                };

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;

                servicio.CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Alumno);

                IAgendaService agendaService = new AgendaService(_unitOfWork);
                // 827 Correo Informacion del Curso Completo
                agendaService.EnviarCorreoOportunidadAutomatico(oportunidad.Id, 1967, "Automatico1967");

                try
                {
                    var nuevaProbabilidad = servicio.ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {

                }

                try
                {
                    servicio.MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // Mailing
                try
                {
                    servicio.EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception)
                {
                }
                try
                {

                    _unitOfWork.OportunidadRepository.InsertarHistorialOportunidad(oportunidad.Id, formulario.Usuario);
                }
                catch (Exception ex)
                {
                    // Si falla la inserción en historial, solo lo logueamos y seguimos el flujo normal
                    Console.WriteLine($"❌ Error al insertar en historial: {ex.Message}");
                }


                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1457}" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1457}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarAlumnoCrearOportunidadVentas(RegistroOportunidadAlumnoDTO formulario)
        {
            var servicio = new OportunidadService(_unitOfWork);
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(formulario.Alumno.Id);
                if (alumno == null)
                {
                    throw new BadRequestException("El alumno no existe");
                }
                alumno.Nombre1 = formulario.Alumno.Nombre1;
                alumno.Nombre2 = formulario.Alumno.Nombre2;
                alumno.ApellidoPaterno = formulario.Alumno.ApellidoPaterno;
                alumno.ApellidoMaterno = formulario.Alumno.ApellidoMaterno;
                alumno.Direccion = formulario.Alumno.Direccion;
                alumno.Telefono = formulario.Alumno.Telefono;
                alumno.Celular = formulario.Alumno.Celular;
                //alumno.Email1 = Formulario.Alumno.Email1;
                alumno.Email2 = formulario.Alumno.Email2;
                alumno.IdCargo = formulario.Alumno.IdCargo;
                alumno.IdAformacion = formulario.Alumno.IdAFormacion;
                alumno.IdAtrabajo = formulario.Alumno.IdATrabajo;
                alumno.IdIndustria = formulario.Alumno.IdIndustria;
                alumno.IdReferido = formulario.Alumno.IdReferido;
                alumno.IdCodigoPais = formulario.Alumno.IdCodigoPais;
                alumno.IdCiudad = formulario.Alumno.IdCodigoCiudad;
                alumno.HoraContacto = formulario.Alumno.HoraContacto;
                alumno.HoraPeru = formulario.Alumno.HoraPeru;
                var empresaAlumno = formulario.Alumno.IdEmpresa;
                alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                alumno.IdEmpresa = formulario.Alumno.IdEmpresa;
                alumno.FechaModificacion = DateTime.Now;
                alumno.UsuarioModificacion = formulario.Usuario;

                OportunidadBoDTO oportunidad = new OportunidadBoDTO();
                oportunidad.IdCentroCosto = formulario.Oportunidad.IdCentroCosto;
                oportunidad.IdPersonalAsignado = formulario.Oportunidad.IdPersonal_Asignado;
                oportunidad.IdTipoDato = formulario.Oportunidad.IdTipoDato;
                oportunidad.IdFaseOportunidad = formulario.Oportunidad.IdFaseOportunidad;
                oportunidad.IdOrigen = formulario.Oportunidad.IdOrigen;
                oportunidad.UltimoComentario = formulario.Oportunidad.UltimoComentario;
                oportunidad.IdTipoInteraccion = formulario.Oportunidad.fk_id_tipointeraccion;
                oportunidad.Estado = true;
                oportunidad.FechaRegistroCampania = formulario.FechaRegistroCampania ?? DateTime.Now;
                oportunidad.UsuarioCreacion = formulario.Usuario;
                oportunidad.UsuarioModificacion = formulario.Usuario;
                oportunidad.FechaCreacion = DateTime.Now;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.Alumno = alumno;
                oportunidad.IdAlumno = alumno.Id;

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;

                servicio.CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Alumno);

                IAgendaService agendaService = new AgendaService(_unitOfWork);
                // 827 Correo Informacion del Curso Completo
                agendaService.EnviarCorreoOportunidadAutomatico(oportunidad.Id, 1967, "Automatico1967");

                ///01/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = servicio.ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                }

                try
                {
                    servicio.MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                    //solo si no funciona MetodoODyOM
                }

                try
                {
                    servicio.EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }
                try
                {

                    _unitOfWork.OportunidadRepository.InsertarHistorialOportunidad(oportunidad.Id, formulario.Usuario);
                }
                catch (Exception ex)
                {
                    // Si falla la inserción en historial, solo lo logueamos y seguimos el flujo normal
                    Console.WriteLine($"❌ Error al insertar en historial: {ex.Message}");
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1456}" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertarHistorialOportunidad(int idOportunidad, string usuario)
        {
            try
            {
                _unitOfWork.OportunidadRepository.InsertarHistorialOportunidad(idOportunidad, usuario);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OportunidadMasivaDTO> ObtenerOportunidadesMasivas()
        {
            try
            {
                var resultado = _unitOfWork.OportunidadRepository.ObtenerOportunidadesMasivas();
                var alumnoService = new AlumnoService(_unitOfWork);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}

