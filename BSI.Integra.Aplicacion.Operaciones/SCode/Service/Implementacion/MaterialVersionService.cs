using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: MaterialVersionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_MaterialVersions
    /// </summary>
    public class MaterialVersionService : IMaterialVersionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MaterialVersionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialVersion, MaterialVersion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMaterialVersion, MaterialVersionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MaterialVersion, ComboDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MaterialVersion, MaterialVersionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public MaterialVersionDTO Insertar(MaterialVersionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    MaterialVersion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        Descripcion = dto.Descripcion,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var resultado = _unitOfWork.MaterialVersionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<MaterialVersionDTO>(resultado);
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MaterialVersionDTO Actualizar(MaterialVersionDTO dto, string usuario)
        {
            try
            {
                MaterialVersion entidad = new MaterialVersion();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.MaterialVersionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.Descripcion = dto.Descripcion;
                            entidad.FechaModificacion = DateTime.Now;
                            entidad.UsuarioModificacion = usuario;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
                _unitOfWork.MaterialVersionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MaterialVersionDTO>(entidad);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var materialVersion = _unitOfWork.MaterialVersionRepository.ObtenerPorId(id);
                if (materialVersion != null && materialVersion.Id != 0)
                {
                    var respuesta = _unitOfWork.MaterialVersionRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MaterialVersion> InsertarLista(List<MaterialVersion> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<MaterialVersion> entidades = new();
                    foreach (var item in dtos)
                    {
                        MaterialVersion entidad = new()
                        {
                            Nombre = item.Nombre,
                            Descripcion = item.Descripcion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        entidades.Add(entidad);
                    }
                    var respuesta = _unitOfWork.MaterialVersionRepository.Add(entidades);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<MaterialVersion>>(respuesta);
                }
                return new List<MaterialVersion>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<MaterialVersion> ActualizarLista(List<MaterialVersion> dtos, string usuario)
        {
            try
            {
                if (dtos != null && dtos.Count() > 0)
                {
                    List<MaterialVersion> entidades = new();
                    var ids = dtos.Select(x => x.Id).ToList();
                    var lista = _unitOfWork.MaterialVersionRepository.ObtenerPorIds(ids);

                    if (lista != null && lista.Count() > 0)
                    {
                        foreach (var item in dtos)
                        {
                            MaterialVersion entidad = lista.FirstOrDefault(x => x.Id == item.Id);
                            if (entidad != null && entidad.Id != 0)
                            {
                                entidad.Nombre = item.Nombre;
                                entidad.Descripcion = item.Descripcion;
                                entidad.FechaModificacion = DateTime.Now;
                                entidad.UsuarioModificacion = usuario;
                                entidades.Add(entidad);
                            }
                        }
                        var respuesta = _unitOfWork.MaterialVersionRepository.Update(entidades);
                        _unitOfWork.Commit();
                        return _mapper.Map<List<MaterialVersion>>(respuesta);
                    }
                }
                return new List<MaterialVersion>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EliminarLista(List<int> listadoIds, string usuario)
        {
            try
            {
                if (listadoIds.Count() > 0)
                {
                    var materialesAccion = _unitOfWork.MaterialVersionRepository.ObtenerPorIds(listadoIds);
                    if (materialesAccion != null && materialesAccion.Count() > 0)
                    {
                        var respuesta = _unitOfWork.MaterialVersionRepository.Delete(materialesAccion.Select(x => x.Id).ToList(), usuario);
                        _unitOfWork.Commit();
                        return respuesta;
                    }
                    else
                    {
                        throw new BadRequestException("No se encontro las entidades");
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MaterialVersions para mostrarse en combo.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<MaterialVersionDTO> Obtener()
        {
            try
            {
                return _unitOfWork.MaterialVersionRepository.ObtenerMaterialVersion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 13/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GrabacionCelularCorporativo para mostrarse.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<GrabacionCelularCorporativoDTO> ObtenerGrabacionesCelularCorporativo()
        {
            try
            {
                return _unitOfWork.MaterialVersionRepository.ObtenerGrabacionesCelularCorporativo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 13/07/2025
        /// Version: 1.0
        /// <summary>
        /// Inserta registros de T_GrabacionCelularCorporativo.
        /// </summary>
        /// <returns> bool </returns>
        public bool InsertarGrabacionesCelularCorporativo(GrabacionCelularCorporativoDTO item, string usuario)
        {
            try
            {

                ///subida auido a blobstorage
                IGestionArchivoLlamadaService gestionArchivoLlamadaService = new GestionArchivoLlamadaService(_unitOfWork);
                string url = string.Empty;
                const string rutaBlob = "bsgcelulares/Grabaciones/";
                const string rutaCompleta = $"https://repositorioaudiollamada.blob.core.windows.net/{rutaBlob}";

                url = gestionArchivoLlamadaService.SubirArchivoAudioLlamada(item.File.ConvertToByte(), item.File.ContentType, item.NombreArchivo, rutaCompleta, rutaBlob);
                
                //Url
                item.Url = url;
                //NumeroDestino//FechaGrabacion
                var temporal = item.NombreArchivo.Split(' ');
                var temporal2 = temporal[temporal.Length-1];
                var temporal3 = temporal2.Split('_');
                var numero = temporal3[0];
                var fecha = temporal3[1];
                var hora = temporal3[2].Replace(".wav","");
                var fechagenerada = new DateTime(Int32.Parse(String.Concat("20" + fecha.Substring(0, 2))), Int32.Parse(fecha.Substring(2, 2)), Int32.Parse(fecha.Substring(4, 2)), Int32.Parse(hora.Substring(0, 2)), Int32.Parse(hora.Substring(2, 2)), Int32.Parse(hora.Substring(4, 2)));

                item.NumeroDestino = numero;
                item.FechaGrabacion = fechagenerada;

                //guardado
                return _unitOfWork.MaterialVersionRepository.InsertarGrabacionesCelularCorporativo(item, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Carlos Crispin Riquelme.
        /// Fecha: 06/04/2026
        /// Version: 1.0
        /// <summary>
        /// Generar una llamada en la actividad trabajada
        /// </summary>
        /// <returns> bool </returns>
        public bool GenerarNuevaLlamadaActividad(GrabacionCelularLlamadasPorDiaDTO obj, string usuario)
        {
            try
            {

                NuevaLlamadaActividadDTO nuevoObjeto = new NuevaLlamadaActividadDTO();
                nuevoObjeto.IdActividadDetalle = obj.IdActividadDetalle.Value;
                nuevoObjeto.DuracionContesto = obj.DuracionContesto.Value;
                nuevoObjeto.NombreArchivo = obj.NombreArchivo;
                nuevoObjeto.NroBytes = obj.NroBytes.Value;
                nuevoObjeto.File = obj.File;
                nuevoObjeto.GrabacionContrato = obj.GrabacionContrato.Value;
                nuevoObjeto.IdLlamada=obj.IdLlamada;

                //aqui obtengo el idpersonal y el anexo por el idoportunidad


                var oportunidadService = new OportunidadService(_unitOfWork);
                var personalService = new PersonalService(_unitOfWork);
                var alumnoService = new AlumnoService(_unitOfWork);
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);



                var oportunidadSeleccionada = oportunidadService.ObtenerPorId(obj.IdOportunidad.Value);
                var personalSeleccionado = personalService.ObtenerPorId(oportunidadSeleccionada.IdPersonalAsignado.Value);
                var alumnoSeleccionado = alumnoService.ObtenerPorId(oportunidadSeleccionada.IdAlumno.Value);
                var actividadDetalleSeleccionada = actividadDetalleService.ObtenerPorId(obj.IdActividadDetalle.Value);

                nuevoObjeto.IdPersonalAsignado = personalSeleccionado.Id;
                nuevoObjeto.Anexo3CX = personalSeleccionado.Anexo3Cx;
                nuevoObjeto.TelefonoDestino = LimpiarCelular(alumnoSeleccionado.Celular, alumnoSeleccionado.IdCodigoPais.Value);
                nuevoObjeto.FechaInicio = actividadDetalleSeleccionada.FechaReal.Value;


                IGestionArchivoLlamadaService gestionArchivoBo = new GestionArchivoLlamadaService(_unitOfWork);
                string url = string.Empty;
                const string rutaBlob = "asterisk/2023/Regularizacion/";
                const string rutaCompleta = $"https://repositorioaudiollamada.blob.core.windows.net/{rutaBlob}";

                url = gestionArchivoBo.SubirArchivoAudioLlamada(nuevoObjeto.File.ConvertToByte(), nuevoObjeto.File.ContentType, nuevoObjeto.NombreArchivo, rutaCompleta, rutaBlob);

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        ILlamadaWebphoneAsteriskService llamadaWebphoneAsteriskService = new LlamadaWebphoneAsteriskService(_unitOfWork);
                        ILlamadaWebphoneCruceCentralService llamadaWebphoneCruceCentralService = new LlamadaWebphoneCruceCentralService(_unitOfWork);
                        LlamadaWebphoneAsterisk nuevaLlamadaWebphoneAsterisk = llamadaWebphoneAsteriskService.Insertar(nuevoObjeto, url, usuario);
                        LlamadaWebphoneCruceCentral nuevaLlamadaWebphoneCruceCentral = llamadaWebphoneCruceCentralService.Insertar(nuevoObjeto, nuevaLlamadaWebphoneAsterisk, usuario);

                        scope.Complete();
                        return true;
                    }
                    catch (Exception)
                    {
                        scope.Dispose();
                        throw;
                    }
                }

            }
            catch(Exception) {
                return false;
            }
            
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MaterialVersions para mostrarse en combo.
        /// </summary>
        /// <returns> List<MaterialVersionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.MaterialVersionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] ConvertToByte(IFormFile file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivoEntrada"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirDocumentosOportunidadRepositorio(IFormFile archivoEntrada, string tipo, string nombreArchivo)
        {
            try
            {
                var archivo = ConvertToByte(archivoEntrada);
                string _nombreLink = string.Empty;

                //Elimina los caracteres con tilde
                var nombreValidado = NormalizarCadena(nombreArchivo);

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/Ventas/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreValidado);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreValidado;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreValidado.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            catch (Exception e)
            {
                //throw new Exception(e.Message);
                return "";
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/04/2023
        /// Version: 1.0
        /// <summary>
        /// Normaliza la Cadena reemplazando los Acentos y la letra ñ
        /// </summary>
        /// <param name="input">Cadena a ser Normalizada</param>
        /// <returns> string </returns>
        public string NormalizarCadena(string input)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            Regex replace_A_Mayuscula = new Regex("[Á]", RegexOptions.Compiled);
            Regex replace_E_Mayuscula = new Regex("[É]", RegexOptions.Compiled);
            Regex replace_I_Mayuscula = new Regex("[Í]", RegexOptions.Compiled);
            Regex replace_O_Mayuscula = new Regex("[Ó]", RegexOptions.Compiled);
            Regex replace_U_Mayuscula = new Regex("[Ú]", RegexOptions.Compiled);
            Regex replace_N_Mayuscula = new Regex("[Ñ]", RegexOptions.Compiled);
            Regex replace_n_Accents = new Regex("[ñ]", RegexOptions.Compiled);

            Regex replace_caracteresEspeciales = new Regex("[]|[|@|~|#|$|%|&|{|}|,|;|°|¿|!|¡|'|^|=|+]", RegexOptions.Compiled);

            input = replace_caracteresEspeciales.Replace(input, "");
            input = replace_a_Accents.Replace(input, "a");
            input = replace_e_Accents.Replace(input, "e");
            input = replace_i_Accents.Replace(input, "i");
            input = replace_o_Accents.Replace(input, "o");
            input = replace_u_Accents.Replace(input, "u");
            input = replace_n_Accents.Replace(input, "n");
            input = replace_A_Mayuscula.Replace(input, "A");
            input = replace_E_Mayuscula.Replace(input, "E");
            input = replace_I_Mayuscula.Replace(input, "I");
            input = replace_O_Mayuscula.Replace(input, "O");
            input = replace_U_Mayuscula.Replace(input, "U");
            input = replace_N_Mayuscula.Replace(input, "N");

            return input;
        }
        public string LimpiarCelular(string numeroCelular, int IdCodigoPais)
        {

            switch (IdCodigoPais)
            {
                case 57:
                    if (
                    !numeroCelular.StartsWith("0057") &&
                    !numeroCelular.StartsWith("57") &&
                    !numeroCelular.StartsWith("+57") &&
                    !numeroCelular.StartsWith("057") &&
                    !numeroCelular.StartsWith("+057") &&
                    !numeroCelular.StartsWith("+0057") &&
                    numeroCelular != ""
                    )
                    {
                        numeroCelular = "0057" + numeroCelular;
                    }
                    break;
                case 591:
                    if (
                     !numeroCelular.StartsWith("00591") &&
                     !numeroCelular.StartsWith("591") &&
                     !numeroCelular.StartsWith("+591") &&
                     !numeroCelular.StartsWith("0591") &&
                     !numeroCelular.StartsWith("+0591") &&
                     !numeroCelular.StartsWith("+00591") &&
                     numeroCelular != ""
                   )
                    {
                        numeroCelular = "00591" + numeroCelular;
                    }
                    break;
                case 52: // Multiple cases sharing logic
                    if (
                      !numeroCelular.StartsWith("0052") &&
                      !numeroCelular.StartsWith("52") &&
                      !numeroCelular.StartsWith("+52") &&
                      !numeroCelular.StartsWith("052") &&
                      !numeroCelular.StartsWith("+052") &&
                      !numeroCelular.StartsWith("+0052") &&
                      numeroCelular != ""
                    )
                    {
                        numeroCelular = "0052" + numeroCelular;
                    }
                    break;
                case 51: // Multiple cases sharing logic
                    if (numeroCelular.StartsWith("0051"))
                    {
                        numeroCelular = numeroCelular.Substring(4);
                    }
                    if (numeroCelular.StartsWith("51"))
                    {
                        numeroCelular = numeroCelular.Substring(2);
                    }
                    if (numeroCelular.StartsWith("+51"))
                    {
                        numeroCelular = numeroCelular.Substring(3);
                    }
                    if (numeroCelular.StartsWith("051"))
                    {
                        numeroCelular = numeroCelular.Substring(3);
                    }
                    if (numeroCelular.StartsWith("+051"))
                    {
                        numeroCelular = numeroCelular.Substring(4);
                    }
                    if (numeroCelular.StartsWith("+0051"))
                    {
                        numeroCelular = numeroCelular.Substring(5);
                    }
                    break;
                default:
                    break;
            }

            if (IdCodigoPais == 591 || IdCodigoPais == 57 || IdCodigoPais == 52)
            {
                numeroCelular = numeroCelular
                  .Replace("+", "")
                  .Replace("-", "")
                  .Replace("_", "")
                  .Replace(" ", "")
                  .Replace("/", "");

                if (numeroCelular.Substring(0, 1) == "0")
                {
                    for (int i = 0; i < numeroCelular.Length; i++)
                    {
                        string caracter = numeroCelular.Substring(0, 1);
                        if (caracter == "0")
                        {
                            numeroCelular = numeroCelular.Substring(1);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return numeroCelular.Trim();

        }
    }
}
