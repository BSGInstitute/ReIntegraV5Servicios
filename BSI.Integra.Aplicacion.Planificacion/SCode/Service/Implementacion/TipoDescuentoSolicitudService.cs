using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private static readonly int[] IdsSupervisores = { 135, 259 };
        private static readonly int[] IdsCoordinadores = { 4094, 5112, 4765, 6584, 6632 };
        private static readonly int[] IdsGerencia = { 213, 6589, 5276, 5564 };

        private const string TIPO_APROBACION_COORDINADOR = "COORDINADOR";
        private const string TIPO_APROBACION_SUPERVISOR = "SUPERVISOR";
        private const string TIPO_APROBACION_GERENCIA = "GERENCIA";

        private const int NIVEL_SUPERVISOR = 2;
        private const int NIVEL_GERENCIA = 3;
        private const int NIVEL_COORDINADOR = 4;

        public TipoDescuentoSolicitudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Autor Modificacion: Jose Vega
        /// Fecha Modificacion: 24/04/2026
        /// Version: 1.4
        /// <summary>
        /// Inserta una nueva solicitud de aprobación para tipo de descuento.
        /// Si el solicitante es Coordinador, Supervisor o Gerencia, dispara la
        /// auto-aprobación en cascada según el nivel del descuento.
        /// </summary>
        public void InsertarSolicitud(TipoDescuentoSolicitudEntradaDTO solicitud)
        {
            string? nombreArchivo = null;
            string? contentType = null;

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

            var tipoDescuento = _unitOfWork.TipoDescuentoRepository.ObtenerPorId(solicitud.IdTipoDescuento);
            var nivelAprobacion = tipoDescuento?.IdTipoDescuentoNivelAprobacion;

            _unitOfWork.TipoDescuentoSolicitudRepository.InsertarSolicitud(
                solicitud.IdTipoDescuento,
                solicitud.IdOportunidad,
                solicitud.IdPersonalSolicitante,
                solicitud.ComentarioSolicitud,
                nombreArchivo,
                contentType,
                solicitud.Usuario
            );

            AutoAprobarCascada(
                solicitud.IdTipoDescuento,
                solicitud.IdOportunidad,
                solicitud.IdPersonalSolicitante,
                nivelAprobacion,
                solicitud.Usuario
            );
        }

        /// Autor: Jose Vega
        /// Fecha: 24/04/2026
        /// Version: 1.0
        /// <summary>
        /// Aplica las firmas automáticas que le corresponden al solicitante según su rol
        /// y avanza la solicitud en el flujo jerárquico Coordinador -> Supervisor -> Gerencia.
        /// </summary>
        private void AutoAprobarCascada(
            int idTipoDescuento,
            int idOportunidad,
            int idPersonalSolicitante,
            int? nivelAprobacion,
            string usuario)
        {
            var esSupervisor = IdsSupervisores.Contains(idPersonalSolicitante);
            var esCoordinador = IdsCoordinadores.Contains(idPersonalSolicitante);
            var esGerencia = IdsGerencia.Contains(idPersonalSolicitante);

            if (!esSupervisor && !esCoordinador && !esGerencia)
            {
                return;
            }

            var idSolicitud = _unitOfWork.TipoDescuentoSolicitudRepository
                .ObtenerIdSolicitudPendiente(idTipoDescuento, idOportunidad);

            if (!idSolicitud.HasValue)
            {
                return;
            }

            const string motivo = "Auto-aprobación por rol de solicitante";

            _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitud(
                idSolicitud.Value,
                TIPO_APROBACION_COORDINADOR,
                motivo,
                null,
                null,
                usuario
            );

            if (nivelAprobacion == NIVEL_COORDINADOR)
            {
                return;
            }

            if (esSupervisor || esGerencia)
            {
                _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitud(
                    idSolicitud.Value,
                    TIPO_APROBACION_SUPERVISOR,
                    motivo,
                    null,
                    null,
                    usuario
                );
            }

            if (nivelAprobacion == NIVEL_GERENCIA && esGerencia)
            {
                _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitud(
                    idSolicitud.Value,
                    TIPO_APROBACION_GERENCIA,
                    motivo,
                    null,
                    null,
                    usuario
                );
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

        /// Autor: Jose Vega
        /// Fecha: 24/04/2026
        /// Version: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Supervisor.
        /// Transiciones (segun nivel del descuento): estado 7 -> 8 (nivel 2) o 7 -> 6 (nivel 3).
        /// </summary>
        public void AprobarSolicitudSupervisor(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitud(
                dto.IdSolicitud,
                TIPO_APROBACION_SUPERVISOR,
                dto.ComentarioRespuesta,
                nombreArchivo,
                contentType,
                dto.Usuario
            );
        }

        /// Autor: Jose Vega
        /// Fecha: 24/04/2026
        /// Version: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Supervisor (estado 7 -> 9).
        /// </summary>
        public void RechazarSolicitudSupervisor(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.RechazarSolicitudSupervisor(
                dto.IdSolicitud,
                dto.ComentarioRespuesta,
                nombreArchivo,
                contentType,
                dto.Usuario
            );
        }

        /// Autor: Lolo Zaa
        /// Fecha: 16/01/2026
        /// Autor Modificacion: Jose Vega
        /// Fecha Modificacion: 24/04/2026
        /// Version: 1.2
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Coordinador.
        /// Transiciones (segun nivel del descuento): estado 1 -> 2 (nivel 4) o 1 -> 7 (niveles 2 y 3).
        /// </summary>
        public void AprobarSolicitudCoordinador(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitud(
                dto.IdSolicitud,
                TIPO_APROBACION_COORDINADOR,
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
        /// Rechaza una solicitud de tipo de descuento a nivel Coordinador (estado 1 -> 3).
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
        /// Fecha: 16/01/2026
        /// Version: 1.1
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Gerencia
        /// </summary>
        public void AprobarSolicitudGerencia(TipoDescuentoSolicitudRespuestaEntradaDTO dto)
        {
            var (nombreArchivo, contentType) = ProcesarArchivoRespuesta(dto.Files);

            _unitOfWork.TipoDescuentoSolicitudRepository.AprobarSolicitud(
                dto.IdSolicitud,
                TIPO_APROBACION_GERENCIA,
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
        /// Autor Modificacion: Jose Vega
        /// Fecha Modificacion: 24/04/2026
        /// Version: 1.1
        /// <summary>
        /// Lista solicitudes de descuento con filtros y paginación. El universo de
        /// oportunidades visibles depende del rol del usuario consultante:
        /// Gerencia ve todas; Supervisor / Coordinador ven sólo las propias y las
        /// de sus subordinados directos (com.SP_TPersonal_GetSubordinadosVentas).
        /// </summary>
        public TipoDescuentoSolicitudPaginadoDTO ListarSolicitudes(TipoDescuentoSolicitudFiltroDTO filtro, int idPersonalUsuario)
        {
            filtro.IdsAsesoresFiltro = ResolverIdsAsesoresPermitidos(idPersonalUsuario);
            return _unitOfWork.TipoDescuentoSolicitudRepository.ListarSolicitudes(filtro);
        }

        /// Autor: Jose Vega
        /// Fecha: 24/04/2026
        /// Version: 1.0
        /// <summary>
        /// Devuelve la lista de IDs de asesores que el usuario tiene autorizado consultar.
        /// Retorna null cuando el usuario es Gerencia (ve todo, sin filtro).
        /// Para los demás roles incluye al propio usuario porque
        /// com.SP_TPersonal_GetSubordinadosVentas no se incluye a sí mismo.
        /// </summary>
        private List<int>? ResolverIdsAsesoresPermitidos(int idPersonalUsuario)
        {
            if (IdsGerencia.Contains(idPersonalUsuario))
            {
                return null;
            }

            var ids = new HashSet<int> { idPersonalUsuario };

            var subordinados = _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoVentas(idPersonalUsuario);
            if (subordinados != null)
            {
                foreach (var s in subordinados)
                {
                    ids.Add(s.Id);
                }
            }

            return ids.ToList();
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
