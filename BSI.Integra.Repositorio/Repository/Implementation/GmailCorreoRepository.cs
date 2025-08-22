using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: GmailCorreoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_GmailCorreo
    /// </summary>
    public class GmailCorreoRepository : GenericRepository<TGmailCorreo>, IGmailCorreoRepository
    {
        private Mapper _mapper;

        public GmailCorreoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGmailCorreo, GmailCorreo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGmailCorreoArchivoAdjunto, GmailCorreoArchivoAdjunto>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGmailCorreo MapeoEntidad(GmailCorreo entidad)
        {
            try
            {
                //crea la entidad padre
                TGmailCorreo modelo = _mapper.Map<TGmailCorreo>(entidad);

                //mapea los hijos
                if (entidad.GmailCorreoArchivoAdjuntos != null && entidad.GmailCorreoArchivoAdjuntos.Count > 0)
                {
                    var gmailCorreoArchivoAdjuntos = _mapper.Map<List<TGmailCorreoArchivoAdjunto>>(entidad.GmailCorreoArchivoAdjuntos);
                    foreach (var hijoNivel1 in gmailCorreoArchivoAdjuntos)
                    {
                        modelo.TGmailCorreoArchivoAdjuntos.Add(hijoNivel1);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGmailCorreo Add(GmailCorreo entidad)
        {
            try
            {
                var GmailCorreo = MapeoEntidad(entidad);
                base.Insert(GmailCorreo);
                return GmailCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGmailCorreo AddSync(GmailCorreo entidad)
        {
            try
            {
                var GmailCorreo = MapeoEntidad(entidad);
                base.InsertAsync(GmailCorreo);
                return GmailCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGmailCorreo Update(GmailCorreo entidad)
        {
            try
            {
                var GmailCorreo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                GmailCorreo.RowVersion = entidadExistente.RowVersion;

                base.Update(GmailCorreo);
                return GmailCorreo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TGmailCorreo> Add(IEnumerable<GmailCorreo> listadoEntidad)
        {
            try
            {
                List<TGmailCorreo> listado = new List<TGmailCorreo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TGmailCorreo> Update(IEnumerable<GmailCorreo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGmailCorreo> listado = new List<TGmailCorreo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_GmailCorreo.
        /// </summary>
        /// <returns> List<GmailCorreoDTO> </returns>
        public IEnumerable<GmailCorreoDTO> ObtenerGmailCorreo()
        {
            try
            {
                List<GmailCorreoDTO> rpta = new List<GmailCorreoDTO>();
                var query = @"
                    SELECT Id,IdEtiqueta,IdGMailCliente,IdCorreoGmailFormat,Asunto,Fecha,EmailBody,Seen,Remitente,Destinatarios,IdPersonal,filas,
	                    IdInteraccion,Cc,ResumenMensaje,Bcc,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,IdClasificacionPersona
                    FROM mkt.T_GmailCorreo WITH(NOLOCK) WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GmailCorreoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GmailCorreo para mostrarse en combo.
        /// </summary>
        /// <returns> List<GmailCorreoComboDTO> </returns>
        public IEnumerable<GmailCorreoComboDTO> ObtenerCombo()
        {
            try
            {
                List<GmailCorreoComboDTO> rpta = new List<GmailCorreoComboDTO>();
                var query = @"SELECT Id,Asunto FROM mkt.T_GmailCorreo WITH(NOLOCK) WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GmailCorreoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los correos enviados por el asesor segun ciertos filtros
        /// </summary>
        /// <param name="filtroBandejaCorreo">Filtros para Bandeja Correo</param>
        /// <returns> List<GmailCorreoComboDTO> </returns>
        public IEnumerable<CorreoEnviadoPorPersonalDTO> ObtenerCorreosEnviadosPorFiltroBandeja(FiltroBandejaCorreoParaRepositorioDTO filtroBandejaCorreo)
        {
            try
            {
                List<CorreoEnviadoPorPersonalDTO> correosEnviados = new List<CorreoEnviadoPorPersonalDTO>();
                var resultadoStoreProcedure = _dapperRepository.QuerySPDapper("com.SP_ObtenerCorreosEnviadosPorPersonal"
                    , new
                    {
                        filtroBandejaCorreo.IdPersonal,
                        filtroBandejaCorreo.Skip,
                        filtroBandejaCorreo.Take,
                        filtroBandejaCorreo.Destinatarios,
                        filtroBandejaCorreo.Asunto,
                        filtroBandejaCorreo.Remitente
                    });
                bool resultadoValido = !string.IsNullOrEmpty(resultadoStoreProcedure) && !resultadoStoreProcedure.Contains("[]") && !resultadoStoreProcedure.Contains("{}");
                if (resultadoValido)
                {
                    correosEnviados = JsonConvert.DeserializeObject<List<CorreoEnviadoPorPersonalDTO>>(resultadoStoreProcedure);
                }
                return correosEnviados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 25/08/2022
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public string SubirArchivo(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {

                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/comprobantes/";
                    //string _direccionBlob = @"correos/individuales/";

                    string nombreValidado = NormalizarCadena(nombreArchivo);

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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blobstorage
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="tipo"></param>
        /// <param name="nombreArchivo"></param>
        /// <returns></returns>
        public async Task<string> SubirArchivoAsync(byte[] archivo, string tipo, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;

                try
                {

                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"operaciones/comprobantes/";
                    //string _direccionBlob = @"correos/individuales/";

                    string nombreValidado = NormalizarCadena(nombreArchivo);

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
                    await objRegistrado;
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/08/2022
        /// Autor Modificación: Jonathan Caipo
        /// Fecha Modificación: 10/04/2023
        /// Version: 1.1
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el correo por el Id
        /// </summary>
        /// <param name="idCorreo"></param>
        /// <returns>GmailCorreo</returns>
        public GmailCorreo ObtenerCorreoPorId(int idCorreo)
        {
            try
            {
                GmailCorreo rpta = new GmailCorreo();
                var query = @"SELECT 
                                Id,
                                IdEtiqueta,
                                IdGMailCliente AS IdGmailCliente,
                                IdCorreoGmailFormat,
                                Asunto,
                                Fecha,
                                EmailBody,
                                Seen,
                                Remitente,
                                Destinatarios,
                                IdPersonal,
                                filas AS Filas,
                                IdInteraccion,
                                Cc,
                                ResumenMensaje	,
                                Bcc,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                IdClasificacionPersona   
                            FROM mkt.T_GmailCorreo WITH(NOLOCK) WHERE Estado=1 AND Id=@idCorreo";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCorreo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<GmailCorreo>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los correos enviados de ventas a los alumnos
        /// </summary>
        /// <param name="emailAlumno"></param>
        public List<CorreoAlumnoVentasDTO> ObtenerCorreosAlumnosSoloVentas(string emailAlumno)
        {
            try
            {
                string query = @"SELECT 
                                    Remitente, Destinatarios, Asunto, EmailBody, Fecha
                                FROM 
                                    mkt.T_GmailCorreo AS CG WITH(NOLOCK) INNER JOIN gp.T_Personal AS P WITH(NOLOCK) ON P.Id = CG.IdPersonal
                                WHERE 
                                    CG.Destinatarios = @EmailAlumno AND P.AreaAbrev ='VE'";
                string correos = _dapperRepository.QueryDapper(query, new { EmailAlumno = emailAlumno });
                if (!string.IsNullOrEmpty(correos) && !correos.Contains("{}"))
                {
                    List<CorreoAlumnoVentasDTO> correoBodyDTO = JsonConvert.DeserializeObject<List<CorreoAlumnoVentasDTO>>(correos)!;
                    return correoBodyDTO;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<StringDTO> listaDestinatariosExitosoFacturama()
        {
            var _query = string.Empty;
            List<StringDTO> lista = new List<StringDTO>();

            _query = "SELECT Correo AS Valor FROM fin.T_FacturamaCorreo WHERE Estado = 1 AND EnvioExitoso=1";
            var repuesta = _dapperRepository.QueryDapper(_query, null);

            if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
            {
                lista = JsonConvert.DeserializeObject<List<StringDTO>>(repuesta);
            }

            return lista;
        }
        public List<StringDTO> listaDestinatariosErroneoFacturama()
        {
            var _query = string.Empty;
            List<StringDTO> lista = new List<StringDTO>();

            _query = "SELECT Correo AS Valor FROM fin.T_FacturamaCorreo WHERE Estado = 1 AND EnvioErroneo=1";
            var repuesta = _dapperRepository.QueryDapper(_query, null);

            if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
            {
                lista = JsonConvert.DeserializeObject<List<StringDTO>>(repuesta);
            }

            return lista;
        }
    }
}
