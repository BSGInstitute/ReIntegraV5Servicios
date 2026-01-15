using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: TipoDescuentoSolicitudService
    /// Autor: Lolo Zaa
    /// Fecha: 12/01/2026
    /// <summary>
    /// Gestión de solicitudes de aprobación para tipos de descuento con subida de archivos
    /// </summary>
    public class TipoDescuentoSolicitudService : ITipoDescuentoSolicitudService
    {
        private IUnitOfWork _unitOfWork;

        // IDs de personal con rol Coordinador
        private static readonly int[] IdsCoordinadores = { 135, 259, 4094 };
        // IDs de personal con rol Gerencia
        private static readonly int[] IdsGerencia = { 213 };

        public TipoDescuentoSolicitudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.3
        /// <summary>
        /// Inserta una nueva solicitud de aprobación para tipo de descuento
        /// Si el solicitante es Coordinador o Gerencia, auto-aprueba según corresponda
        /// Si el tipo de descuento tiene NivelAprobacion = 3, se auto-aprueba Coordinador (estado 6)
        /// </summary>
        public void InsertarSolicitud(TipoDescuentoSolicitudEntradaDTO solicitud)
        {
            string? nombreArchivo = null;
            string? contentType = null;

            // Procesar archivo si existe
            if (solicitud.Files != null && solicitud.Files.Count > 0)
            {
                var file = solicitud.Files.First();
                contentType = file.ContentType;
                nombreArchivo = file.FileName;
                nombreArchivo = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", SlugNombreArchivo(nombreArchivo));
                var urlArchivoRepositorio = SubirArchivoSolicitudTipoDescuentoRepositorio(file, file.ContentType, nombreArchivo);

                if (string.IsNullOrEmpty(urlArchivoRepositorio))
                {
                    throw new Exception("Error al subir el archivo");
                }
            }

            // Obtener el nivel de aprobación del tipo de descuento
            var tipoDescuento = _unitOfWork.TipoDescuentoRepository.ObtenerPorId(solicitud.IdTipoDescuento);
            var nivelAprobacion = tipoDescuento?.IdTipoDescuentoNivelAprobacion;

            // Crear la solicitud
            _unitOfWork.TipoDescuentoSolicitudRepository.InsertarSolicitud(
                solicitud.IdTipoDescuento,
                solicitud.IdOportunidad,
                solicitud.IdPersonalSolicitante,
                solicitud.ComentarioSolicitud,
                nombreArchivo,
                contentType,
                solicitud.Usuario
            );

            // Determinar si requiere auto-aprobación
            var esCoordinador = IdsCoordinadores.Contains(solicitud.IdPersonalSolicitante);
            var esGerencia = IdsGerencia.Contains(solicitud.IdPersonalSolicitante);
            var requiereAutoAprobacionCoordinador = nivelAprobacion == 3 || esCoordinador || esGerencia;

            if (requiereAutoAprobacionCoordinador)
            {
                // Obtener el ID de la solicitud recién creada
                var idSolicitud = _unitOfWork.TipoDescuentoSolicitudRepository
                    .ObtenerIdSolicitudPendiente(solicitud.IdTipoDescuento, solicitud.IdOportunidad);

                if (idSolicitud.HasValue)
                {
                    // Auto-aprobar como Coordinador (estado pasa a 6 - Pendiente aprobación Gerencia)
                    var comentarioAutoAprobacion = nivelAprobacion == 3
                        ? "Auto-aprobación por nivel de aprobación del descuento"
                        : "Auto-aprobación por rol de solicitante";

                    _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitudCoordinador(
                        idSolicitud.Value,
                        comentarioAutoAprobacion,
                        null,
                        null,
                        solicitud.Usuario
                    );

                    // Si es Gerencia, también auto-aprobar como Gerencia
                    if (esGerencia)
                    {
                        _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitudGerencia(
                            idSolicitud.Value,
                            "Auto-aprobación por rol de solicitante",
                            null,
                            null,
                            solicitud.Usuario
                        );
                    }
                }
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Convierte IFormFile a byte array
        /// </summary>
        public byte[] ConvertToByte(IFormFile file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.OpenReadStream());
            imageByte = rdr.ReadBytes((int)file.Length);
            return imageByte;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Sube el archivo al blob storage de Azure
        /// </summary>
        /// <param name="archivoEntrada">Archivo a subir</param>
        /// <param name="tipo">Content type del archivo</param>
        /// <param name="nombreArchivo">Nombre del archivo</param>
        /// <returns>URL del archivo en blob storage</returns>
        public string SubirArchivoSolicitudTipoDescuentoRepositorio(IFormFile archivoEntrada, string tipo, string nombreArchivo)
        {
            try
            {
                var archivo = ConvertToByte(archivoEntrada);
                string _nombreLink = string.Empty;

                // Eliminar caracteres con tilde
                nombreArchivo = nombreArchivo.Replace("á", "a");
                nombreArchivo = nombreArchivo.Replace("é", "e");
                nombreArchivo = nombreArchivo.Replace("í", "i");
                nombreArchivo = nombreArchivo.Replace("ó", "o");
                nombreArchivo = nombreArchivo.Replace("ú", "u");

                nombreArchivo = nombreArchivo.Replace("Á", "A");
                nombreArchivo = nombreArchivo.Replace("É", "E");
                nombreArchivo = nombreArchivo.Replace("Í", "I");
                nombreArchivo = nombreArchivo.Replace("Ó", "O");
                nombreArchivo = nombreArchivo.Replace("Ú", "U");

                // Eliminar las Ñ
                nombreArchivo = nombreArchivo.Replace("ñ", "n");
                nombreArchivo = nombreArchivo.Replace("Ñ", "N");

                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"repositorioweb/AprobacionDescuentos/";

                    // Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = tipo;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = tipo;
                    Stream stream = new MemoryStream(archivo);

                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);
                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
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
                return "";
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Sanitiza el nombre del archivo eliminando caracteres especiales
        /// </summary>
        public string SlugNombreArchivo(string textoOriginal)
        {
            string extension = textoOriginal.Substring(textoOriginal.LastIndexOf("."));
            string texto = textoOriginal;

            // Caracteres inválidos
            texto = Regex.Replace(texto, @"[^a-zA-Z0-9\s-]", "");
            texto = texto.Replace("+", " ");
            texto = texto.Replace("-", " ");

            // Convierte múltiples espacios
            texto = Regex.Replace(texto, @"\s+", " ").Trim();
            texto = texto.Trim();
            texto = texto + extension;

            return texto;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las solicitudes de aprobación de tipos de descuento
        /// </summary>
        public IEnumerable<TipoDescuentoSolicitudListadoDTO> ObtenerTodasSolicitudes()
        {
            return _unitOfWork.TipoDescuentoSolicitudRepository.ObtenerTodasSolicitudes();
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Procesa el archivo de respuesta y retorna nombre y contentType
        /// </summary>
        private (string? nombreArchivo, string? contentType) ProcesarArchivoRespuesta(IList<IFormFile>? files)
        {
            if (files == null || files.Count == 0)
                return (null, null);

            var file = files.First();
            var contentType = file.ContentType;
            var nombreArchivo = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-", SlugNombreArchivo(file.FileName));
            var urlArchivoRepositorio = SubirArchivoSolicitudTipoDescuentoRepositorio(file, file.ContentType, nombreArchivo);

            if (string.IsNullOrEmpty(urlArchivoRepositorio))
            {
                throw new Exception("Error al subir el archivo de respuesta");
            }

            return (nombreArchivo, contentType);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Coordinador
        /// </summary>
        public void AprobarSolicitudCoordinador(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitudCoordinador(
                dto.IdSolicitud,
                dto.ComentarioRespuesta,
                nombreArchivo,
                contentType,
                dto.Usuario
            );
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Coordinador
        /// </summary>
        public void RechazarSolicitudCoordinador(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.RechazarSolicitudCoordinador(
                dto.IdSolicitud,
                dto.ComentarioRespuesta,
                nombreArchivo,
                contentType,
                dto.Usuario
            );
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Gerencia
        /// </summary>
        public void AprobarSolicitudGerencia(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitudGerencia(
                dto.IdSolicitud,
                dto.ComentarioRespuesta,
                nombreArchivo,
                contentType,
                dto.Usuario
            );
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Gerencia
        /// </summary>
        public void RechazarSolicitudGerencia(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.RechazarSolicitudGerencia(
                dto.IdSolicitud,
                dto.ComentarioRespuesta,
                nombreArchivo,
                contentType,
                dto.Usuario
            );
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Lista solicitudes de descuento con filtros y paginación
        /// </summary>
        public TipoDescuentoSolicitudPaginadoDTO ListarSolicitudes(TipoDescuentoSolicitudFiltroDTO filtro)
        {
            return _unitOfWork.TipoDescuentoSolicitudRepository.ListarSolicitudes(filtro);
        }

        /// Autor: Joseph Llanque
        /// Fecha: 15/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los estados de solicitud de descuento activos
        /// </summary>
        public IEnumerable<TipoDescuentoSolicitudEstadoDTO> ObtenerEstadosSolicitud()
        {
            return _unitOfWork.TipoDescuentoSolicitudRepository.ObtenerEstadosSolicitud();
        }
    }
}
