using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ExpositorService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_Expositor
    /// </summary>
    public class ExpositorService : IExpositorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ExpositorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExpositor, Expositor>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExpositorDTO, Expositor>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExpositorDTO, TExpositor>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Expositor
        /// </summary>
        /// <param name="dto">Expositor</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>ExpositorDTO</returns>
        public ExpositorDTO Insertar(ExpositorDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    if(dto.HojaVidaResumidaPerfil==null || dto.HojaVidaResumidaPerfil=="")
                    {
                        throw new BadRequestException("Campo Hoja de Vida Resumida no puede ser null");
                    }
                    
                    Expositor entidad = new();
                    byte[] dataHojaVida = Convert.FromBase64String(dto.HojaVidaResumidaPerfil); 
                    
                    var decodedHojaVida = Encoding.UTF8.GetString(dataHojaVida);

                    var IdExpositorRepetido = _unitOfWork.ExpositorRepository.ObtenerExpositorEliminadoEmailRepetido(dto.Email1);

                    if (IdExpositorRepetido == null || IdExpositorRepetido == 0)
                    {
                        entidad.IdTipoDocumento = dto.IdTipoDocumento;
                        entidad.NroDocumento = dto.NroDocumento;
                        entidad.PrimerNombre = dto.PrimerNombre;
                        entidad.SegundoNombre = dto.SegundoNombre;
                        entidad.ApellidoPaterno = dto.ApellidoPaterno;
                        entidad.ApellidoMaterno = dto.ApellidoMaterno;
                        entidad.FechaNacimiento = dto.FechaNacimiento;
                        entidad.IdPaisProcedencia = dto.IdPaisProcedencia;
                        entidad.IdCiudadProcedencia = dto.IdCiudadProcedencia;
                        entidad.IdReferidoPor = dto.IdReferidoPor;
                        entidad.TelfCelular1 = dto.TelfCelular1;
                        entidad.TelfCelular2 = dto.TelfCelular2;
                        entidad.TelfCelular3 = dto.TelfCelular3;
                        entidad.Email1 = dto.Email1;
                        entidad.Email2 = dto.Email2;
                        entidad.Email3 = dto.Email3;
                        entidad.Domicilio = dto.Domicilio;
                        entidad.IdPaisDomicilio = dto.IdPaisDomicilio;
                        entidad.IdCiudadDomicilio = dto.IdCiudadDomicilio;
                        entidad.LugarTrabajo = dto.LugarTrabajo;
                        entidad.IdPaisLugarTrabajo = dto.IdPaisLugarTrabajo;
                        entidad.IdCiudadLugarTrabajo = dto.IdCiudadLugarTrabajo;
                        entidad.AsistenteNombre = dto.AsistenteNombre;
                        entidad.AsistenteTelefono = dto.AsistenteTelefono;
                        entidad.AsistenteCelular = dto.AsistenteCelular;
                        entidad.HojaVidaResumidaPerfil = decodedHojaVida;
                        entidad.HojaVidaResumidaSpeech = dto.HojaVidaResumidaSpeech;
                        entidad.FormacionAcademica = dto.FormacionAcademica;
                        entidad.ExperienciaProfesional = dto.ExperienciaProfesional;
                        entidad.Publicaciones = dto.Publicaciones;
                        entidad.PremiosDistinciones = dto.PremiosDistinciones;
                        entidad.OtraInformacion = dto.OtraInformacion;
                        entidad.Estado = true;
                        entidad.UsuarioCreacion = usuario;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.FechaModificacion = DateTime.Now;
                        entidad.IdPersonalAsignado = dto.IdPersonalAsignado;

                        if (!string.IsNullOrEmpty(dto.FotoDocente))
                        {
                            entidad.FotoDocente = dto.FotoDocente;
                            entidad.UrlFotoDocente = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/docentes/" + dto.UrlFotoDocente.Replace(" ", "%20");
                        }

                        var resultadoExpositor = _unitOfWork.ExpositorRepository.Add(entidad);
                        _unitOfWork.Commit();
                        entidad.Id = resultadoExpositor.Id;
                        IPersonaService personaService = new PersonaService(_unitOfWork);
                        var resultadoPersona = personaService.InsertarPersona(entidad.Id, TipoPersona.Docente, usuario);

                        if (resultadoPersona != null)
                        {
                            return _mapper.Map<ExpositorDTO>(entidad);
                        }
                        else
                        {
                            var nombreTablaV3 = "tPLA_expositor";
                            var nombreTablaV4 = "pla.T_Expositor";
                            var resultado = _unitOfWork.ExpositorRepository.EliminarFisicaExpositor(nombreTablaV3, nombreTablaV4, entidad.Id, null, null);
                            if (resultado == true)
                            {
                                throw new BadRequestException("Se elimino el docente");
                            }
                            else
                            {
                                throw new BadRequestException("No se elimino docente");
                            }
                        }
                    }
                    else
                    {
                        throw new BadRequestException($"El correo {dto.Email1} es repetido");
                    }
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un Expositor
        /// </summary>
        /// <param name="dto">Expositor</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>ExpositorDTO</returns>
        public ExpositorDTO Actualizar(ExpositorDTO dto, string usuario)
        {
            try
            {
                Expositor? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ExpositorRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            byte[] dataHojaVida = Convert.FromBase64String(dto.HojaVidaResumidaPerfil);
                            var decodedHojaVida = Encoding.UTF8.GetString(dataHojaVida);
                            entidad.IdTipoDocumento = dto.IdTipoDocumento;
                            entidad.NroDocumento = dto.NroDocumento;
                            entidad.PrimerNombre = dto.PrimerNombre;
                            entidad.SegundoNombre = dto.SegundoNombre;
                            entidad.ApellidoPaterno = dto.ApellidoPaterno;
                            entidad.ApellidoMaterno = dto.ApellidoMaterno;
                            entidad.FechaNacimiento = dto.FechaNacimiento;
                            entidad.IdPaisProcedencia = dto.IdPaisProcedencia;
                            entidad.IdCiudadProcedencia = dto.IdCiudadProcedencia;
                            entidad.IdReferidoPor = dto.IdReferidoPor;
                            entidad.TelfCelular1 = dto.TelfCelular1;
                            entidad.TelfCelular2 = dto.TelfCelular2;
                            entidad.TelfCelular3 = dto.TelfCelular3;
                            entidad.Email1 = dto.Email1;
                            entidad.Email2 = dto.Email2;
                            entidad.Email3 = dto.Email3;
                            entidad.Domicilio = dto.Domicilio;
                            entidad.IdPaisDomicilio = dto.IdPaisDomicilio;
                            entidad.IdCiudadDomicilio = dto.IdCiudadDomicilio;
                            entidad.LugarTrabajo = dto.LugarTrabajo;
                            entidad.IdPaisLugarTrabajo = dto.IdPaisLugarTrabajo;
                            entidad.IdCiudadLugarTrabajo = dto.IdCiudadLugarTrabajo;
                            entidad.AsistenteNombre = dto.AsistenteNombre;
                            entidad.AsistenteTelefono = dto.AsistenteTelefono;
                            entidad.AsistenteCelular = dto.AsistenteCelular;
                            entidad.HojaVidaResumidaPerfil = decodedHojaVida;
                            entidad.HojaVidaResumidaSpeech = dto.HojaVidaResumidaSpeech;
                            entidad.FormacionAcademica = dto.FormacionAcademica;
                            entidad.ExperienciaProfesional = dto.ExperienciaProfesional;
                            entidad.Publicaciones = dto.Publicaciones;
                            entidad.PremiosDistinciones = dto.PremiosDistinciones;
                            entidad.OtraInformacion = dto.OtraInformacion;
                            entidad.Estado = true;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.IdPersonalAsignado = dto.IdPersonalAsignado;

                            if (!string.IsNullOrEmpty(dto.FotoDocente))
                            {
                                entidad.FotoDocente = dto.FotoDocente;
                                entidad.UrlFotoDocente = "https://repositorioweb.blob.core.windows.net/repositorioweb/img/docentes/" + dto.UrlFotoDocente.Replace(" ", "%20");
                            }
                            var respuesta = _unitOfWork.ExpositorRepository.Update(entidad);
                            _unitOfWork.Commit();
                            return _mapper.Map<ExpositorDTO>(respuesta);
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Seccion Especifico asociada a un ProgramaGeneral con un Titulo especifico.
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                if (_unitOfWork.ExpositorRepository.Exist(id))
                {
                    var respuesta = _unitOfWork.ExpositorRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo de Expositor
        /// </summary>
        /// <returns> PGeneralDocumentoSeccionDTO </returns>
        public async Task<ComboModuloExpositorDTO> ObtenerCombosModulo()
        {
            try
            {
                var taskTipoDocumento = _unitOfWork.TipoDocumentoRepository.ObtenerAsync();
                var taskCoordinador = _unitOfWork.CoordinadoraRepository.ObtenerCoordinadoresDocentesAsync();
                var taskPais = _unitOfWork.PaisRepository.ObtenerComboAsync();
                var taskCiudad = _unitOfWork.CiudadRepository.ObtenerAsync();
                var taskExpositor = _unitOfWork.ExpositorRepository.ObtenerComboAsync();
                var combos = new ComboModuloExpositorDTO();
                combos.TipoDocumentos = await taskTipoDocumento;
                combos.Coordinadores = await taskCoordinador;
                combos.Paises = await taskPais;
                combos.Ciudades = await taskCiudad;
                combos.Expositores = await taskExpositor;
                return combos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// <summary>
        /// Registra la foto del expositor
        /// </summary>
        /// <param name="files"></param>
        /// <returns> Url del archivo y nombre del archivo </returns>
        public (string? UrlArchivo, string? NombreArchivo) RegistrarArchivoFotoExpositor(IFormFile files)
        {
            try
            {
                string respuesta = string.Empty;
                using (var ms = new MemoryStream())
                {
                    files.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = GuardarArchivoExpositor(fileBytes, files.ContentType, files.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return (null, null);
                }
                else
                {
                    return (respuesta, files.FileName);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// <summary>
        /// Guarda el archivo en el blob storage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns> url del archivo del blob storage </returns>
        private string GuardarArchivoExpositor(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string nombreLink = string.Empty;
                try
                {
                    const string azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    const string direccionBlob = @"repositorioweb/img/docentes/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                        nombreLink = "https://repositorioweb.blob.core.windows.net/" + direccionBlob + nombreArchivo.Replace(" ", "%20");
                    else
                        nombreLink = "";
                    return nombreLink;

                }
                catch (Exception)
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/08/2023
        /// <summary>
        /// Obtiene todos los registro de expositor
        /// </summary>
        /// <returns> Lista ExpositorDTO</returns>
        public IEnumerable<ExpositorDTO> Obtener()
        {
            return _unitOfWork.ExpositorRepository.Obtener();
        }


        /// Autor: Jeremy Pacheco Garcia
        /// Fecha: 26/06/2025
        /// <summary>
        /// Obtiene el Combo de expositor
        /// </summary>
        /// <returns> Lista ComboDTO</returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            return _unitOfWork.ExpositorRepository.ObtenerCombo();
        }
    }
}
