using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoDescuentoSolicitudRepository
    /// Autor: Lolo Zaa
    /// Fecha: 12/01/2026
    /// <summary>
    /// Gestión de solicitudes de aprobación para tipos de descuento
    /// </summary>
    public class TipoDescuentoSolicitudRepository : ITipoDescuentoSolicitudRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public TipoDescuentoSolicitudRepository(IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.1
        /// <summary>
        /// Inserta una nueva solicitud de aprobación para tipo de descuento
        /// </summary>
        public void InsertarSolicitud(
            int idTipoDescuento,
            int idOportunidad,
            int idPersonalSolicitante,
            string? comentarioSolicitud,
            string? nombreArchivoSolicitud,
            string? contentTypeSolicitud,
            string usuario)
        {
            var parametros = new
            {
                IdTipoDescuento = idTipoDescuento,
                IdOportunidad = idOportunidad,
                IdPersonal_Solicitante = idPersonalSolicitante,
                ComentarioSolicitud = comentarioSolicitud,
                NombreArchivoSolicitud = nombreArchivoSolicitud,
                ContentTypeSolicitud = contentTypeSolicitud,
                Usuario = usuario
            };

            _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoInsertarSolicitud", parametros);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las solicitudes de aprobación de tipos de descuento
        /// </summary>
        public IEnumerable<TipoDescuentoSolicitudListadoDTO> ObtenerTodasSolicitudes()
        {
            var resultado = _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoSolicitudObtener", null);

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                var solicitudes = JsonConvert.DeserializeObject<List<TipoDescuentoSolicitudListadoDTO>>(resultado);
                return solicitudes ?? new List<TipoDescuentoSolicitudListadoDTO>();
            }

            return new List<TipoDescuentoSolicitudListadoDTO>();
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Coordinador
        /// </summary>
        public void AprobarSolicitudCoordinador(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario)
        {
            var parametros = new
            {
                IdTipoDescuentoSolicitud = idSolicitud,
                ComentarioRespuesta = comentarioRespuesta,
                NombreArchivoRespuesta = nombreArchivoRespuesta,
                ContentTypeRespuesta = contentTypeRespuesta,
                Usuario = usuario
            };

            _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoAprobarSolicitudCoordinador", parametros);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Coordinador
        /// </summary>
        public void RechazarSolicitudCoordinador(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario)
        {
            var parametros = new
            {
                IdTipoDescuentoSolicitud = idSolicitud,
                ComentarioRespuesta = comentarioRespuesta,
                NombreArchivoRespuesta = nombreArchivoRespuesta,
                ContentTypeRespuesta = contentTypeRespuesta,
                Usuario = usuario
            };

            _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoRechazarSolicitudCoordinador", parametros);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Aprueba una solicitud de tipo de descuento a nivel Gerencia
        /// </summary>
        public void AprobarSolicitudGerencia(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario)
        {
            var parametros = new
            {
                IdTipoDescuentoSolicitud = idSolicitud,
                ComentarioRespuesta = comentarioRespuesta,
                NombreArchivoRespuesta = nombreArchivoRespuesta,
                ContentTypeRespuesta = contentTypeRespuesta,
                Usuario = usuario
            };

            _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoAprobarSolicitudGerencia", parametros);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Rechaza una solicitud de tipo de descuento a nivel Gerencia
        /// </summary>
        public void RechazarSolicitudGerencia(
            int idSolicitud,
            string? comentarioRespuesta,
            string? nombreArchivoRespuesta,
            string? contentTypeRespuesta,
            string usuario)
        {
            var parametros = new
            {
                IdTipoDescuentoSolicitud = idSolicitud,
                ComentarioRespuesta = comentarioRespuesta,
                NombreArchivoRespuesta = nombreArchivoRespuesta,
                ContentTypeRespuesta = contentTypeRespuesta,
                Usuario = usuario
            };

            _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoRechazarSolicitudGerencia", parametros);
        }

        /// Autor: Lolo Zaa
        /// Fecha: 14/01/2026
        /// Version: 1.0
        /// <summary>
        /// Lista solicitudes de descuento con filtros y paginación
        /// </summary>
        public TipoDescuentoSolicitudPaginadoDTO ListarSolicitudes(TipoDescuentoSolicitudFiltroDTO filtro)
        {
            var parametros = new
            {
                IdTipoDescuentoSolicitudEstado = filtro.IdTipoDescuentoSolicitudEstado,
                IdPersonal_Asignado = filtro.IdPersonal_Asignado,
                FechaInicio = filtro.FechaInicio,
                FechaFin = filtro.FechaFin,
                NumeroPagina = filtro.NumeroPagina,
                RegistrosPorPagina = filtro.RegistrosPorPagina
            };

            var resultado = _dapperRepository.QuerySPDapper("pla.SP_TipoDescuentoListarSolicitudes", parametros);

            var respuesta = new TipoDescuentoSolicitudPaginadoDTO
            {
                NumeroPagina = filtro.NumeroPagina,
                RegistrosPorPagina = filtro.RegistrosPorPagina
            };

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
            {
                var items = JsonConvert.DeserializeObject<List<TipoDescuentoSolicitudItemDTO>>(resultado);
                respuesta.Items = items ?? new List<TipoDescuentoSolicitudItemDTO>();

                // Obtener TotalRegistros del primer item si existe
                if (respuesta.Items.Count > 0)
                {
                    respuesta.TotalRegistros = respuesta.Items[0].TotalRegistros;
                }
            }

            return respuesta;
        }

        /// Autor: Lolo Zaa
        /// Fecha: 15/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el ID de una solicitud pendiente por tipo de descuento y oportunidad
        /// </summary>
        public int? ObtenerIdSolicitudPendiente(int idTipoDescuento, int idOportunidad)
        {
            var sql = @"SELECT TOP 1 Id
                        FROM [pla].[T_TipoDescuentoSolicitud]
                        WHERE IdTipoDescuento = @IdTipoDescuento
                          AND IdOportunidad = @IdOportunidad
                          AND IdTipoDescuentoSolicitudEstado = 1
                          AND Estado = 1
                        ORDER BY FechaCreacion DESC";

            var parametros = new
            {
                IdTipoDescuento = idTipoDescuento,
                IdOportunidad = idOportunidad
            };

            var resultado = _dapperRepository.FirstOrDefault(sql, parametros);

            if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
            {
                var data = JsonConvert.DeserializeObject<dynamic>(resultado);
                if (data != null && data.Id != null)
                {
                    return (int)data.Id;
                }
            }

            return null;
        }
    }
}
