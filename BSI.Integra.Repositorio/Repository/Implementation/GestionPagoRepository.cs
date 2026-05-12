using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para gestión de T_GestionPago (cabecera del módulo de pagos)
    /// </summary>
    public class GestionPagoRepository : GenericRepository<TGestionPago>, IGestionPagoRepository
    {
        private const string UsuarioSistema = "Sistema";
        private Mapper _mapper;

        public GestionPagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGestionPago, GestionPago>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGestionPago MapeoEntidad(GestionPago entidad)
        {
            try
            {
                TGestionPago modelo = _mapper.Map<TGestionPago>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string ObtenerUsuarioAuditoria(params string?[] usuarios)
        {
            return usuarios.FirstOrDefault(usuario => !string.IsNullOrWhiteSpace(usuario)) ?? UsuarioSistema;
        }

        private static void AsignarCamposAuditoriaInsercion(TGestionPago modelo)
        {
            var fechaActual = DateTime.Now;
            var usuarioAuditoria = ObtenerUsuarioAuditoria(modelo.UsuarioCreacion, modelo.UsuarioModificacion);

            modelo.FechaCreacion = fechaActual;
            modelo.FechaModificacion = fechaActual;
            modelo.UsuarioCreacion = usuarioAuditoria;
            modelo.UsuarioModificacion = usuarioAuditoria;
            modelo.Estado = true;
        }

        private static void AsignarCamposAuditoriaActualizacion(TGestionPago modelo, TGestionPago entidadExistente)
        {
            var fechaActual = DateTime.Now;
            var usuarioModificacion = ObtenerUsuarioAuditoria(modelo.UsuarioModificacion, modelo.UsuarioCreacion, entidadExistente.UsuarioCreacion);

            modelo.RowVersion = entidadExistente.RowVersion;
            modelo.FechaCreacion = entidadExistente.FechaCreacion;
            modelo.UsuarioCreacion = ObtenerUsuarioAuditoria(entidadExistente.UsuarioCreacion, modelo.UsuarioCreacion);
            modelo.FechaModificacion = fechaActual;
            modelo.UsuarioModificacion = usuarioModificacion;
            modelo.Estado = entidadExistente.Estado;
        }

        public TGestionPago Add(GestionPago entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                AsignarCamposAuditoriaInsercion(modelo);
                base.Insert(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TGestionPago Update(GestionPago entidad)
        {
            try
            {
                var modelo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id);
                if (entidadExistente == null)
                    throw new Exception($"No se encontró el registro con Id {entidad.Id}");
                AsignarCamposAuditoriaActualizacion(modelo, entidadExistente);
                base.Update(modelo);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int idGestionPago, string usuario)
        {
            try
            {
                base.Delete(idGestionPago, ObtenerUsuarioAuditoria(usuario));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TGestionPago> Add(IEnumerable<GestionPago> listadoEntidad)
        {
            try
            {
                List<TGestionPago> listado = new List<TGestionPago>();
                foreach (var entidad in listadoEntidad)
                {
                    var modelo = MapeoEntidad(entidad);
                    AsignarCamposAuditoriaInsercion(modelo);
                    listado.Add(modelo);
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TGestionPago> Update(IEnumerable<GestionPago> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGestionPago> listado = new List<TGestionPago>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id));
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    if (entidadExistente == null)
                        throw new Exception($"No se encontró el registro con Id {item.Id}");
                    AsignarCamposAuditoriaActualizacion(item, entidadExistente);
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
                base.Delete(listadoIds, ObtenerUsuarioAuditoria(usuario));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public IEnumerable<GestionPagoDTO> ObtenerGestionesPago(FiltroGestionPagoDTO filtro)
        {
            try
            {
                List<GestionPagoDTO> listaGestiones = new List<GestionPagoDTO>();

                var _query = @"
                    SELECT
                        V.*,
                        (SELECT STRING_AGG(CC.Nombre, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         INNER JOIN pla.T_CentroCosto CC WITH (NOLOCK) ON F.IdCentroCosto = CC.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1 AND CC.Estado = 1) AS NombreCentroCosto,
                        (SELECT STRING_AGG(P.Nombre, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         INNER JOIN fin.T_Producto P WITH (NOLOCK) ON F.IdProducto = P.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1 AND P.Estado = 1) AS NombreProducto,
                        (SELECT STRING_AGG(PG.Nombre, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         INNER JOIN pla.T_CentroCosto CC WITH (NOLOCK) ON F.IdCentroCosto = CC.Id
                         INNER JOIN pla.T_PGeneral PG WITH (NOLOCK) ON CC.IdPgeneral = CAST(PG.Id AS VARCHAR)
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1 AND CC.Estado = 1 AND PG.Estado = 1) AS NombreProgramaGeneral,
                        (SELECT STRING_AGG(F.Codigo, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1) AS Fur,
                        (SELECT STRING_AGG(CAST(F.Cantidad AS VARCHAR), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1) AS Cantidad,
                        (SELECT STRING_AGG(CAST(F.PrecioUnitarioMonedaOrigen AS VARCHAR), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1) AS PrecioUnitario,
                        (SELECT STRING_AGG(FORMAT(F.FechaLimite, 'MMMM', 'es-ES'), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1) AS MesEjecucion,
                        (SELECT STRING_AGG(FORMAT(F.FechaLimite, 'dd/MM/yyyy'), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = V.IdComprobantePago AND CPF.Estado = 1 AND F.Estado = 1) AS FechaEjecucion
                    FROM fin.V_GestionPagoCabecera V
                    WHERE 1 = 1";

                if (filtro.IdComprobantePago.HasValue) _query += " AND V.IdComprobantePago = @IdComprobantePago";
                if (filtro.IdProveedor.HasValue) _query += " AND V.IdProveedor = @IdProveedor";
                if (filtro.IdPagoEstado.HasValue) _query += " AND V.IdPagoEstado = @IdPagoEstado";
                if (filtro.IdModalidadPago.HasValue) _query += " AND V.IdModalidadPago = @IdModalidadPago";
                if (filtro.IdEmpresa.HasValue) _query += " AND V.IdEmpresa = @IdEmpresa";
                if (filtro.FechaDesde.HasValue) _query += " AND V.FechaSolicitud >= @FechaDesde";
                if (filtro.FechaHasta.HasValue) _query += " AND V.FechaSolicitud <= @FechaHasta";

                // Filtro por FUR
                if (filtro.IdFur.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        WHERE CPF.IdComprobantePago = V.IdComprobantePago
                        AND CPF.IdFur = @IdFur
                        AND CPF.Estado = 1)";
                }

                // Filtro por Producto (a través de FUR)
                if (filtro.IdProducto.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                        WHERE CPF.IdComprobantePago = V.IdComprobantePago
                        AND F.IdProducto = @IdProducto
                        AND CPF.Estado = 1 AND F.Estado = 1)";
                }

                // Filtro por Centro de Costo (a través de FUR)
                if (filtro.IdCentroCosto.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                        WHERE CPF.IdComprobantePago = V.IdComprobantePago
                        AND F.IdCentroCosto = @IdCentroCosto
                        AND CPF.Estado = 1 AND F.Estado = 1)";
                }

                // Filtro por Programa General (a través de FUR → CentroCosto)
                if (filtro.IdProgramaGeneral.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                        INNER JOIN pla.T_CentroCosto CC WITH (NOLOCK) ON F.IdCentroCosto = CC.Id
                        WHERE CPF.IdComprobantePago = V.IdComprobantePago
                        AND CC.IdPgeneral = CAST(@IdProgramaGeneral AS VARCHAR)
                        AND CPF.Estado = 1 AND F.Estado = 1 AND CC.Estado = 1)";
                }

                var queryResultado = _dapperRepository.QueryDapper(_query, new
                {
                    filtro.IdComprobantePago,
                    filtro.IdProveedor,
                    filtro.IdPagoEstado,
                    filtro.IdModalidadPago,
                    filtro.IdEmpresa,
                    filtro.FechaDesde,
                    filtro.FechaHasta,
                    filtro.IdFur,
                    filtro.IdProducto,
                    filtro.IdCentroCosto,
                    filtro.IdProgramaGeneral
                });

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaGestiones = JsonConvert.DeserializeObject<List<GestionPagoDTO>>(queryResultado);

                    if (listaGestiones.Any())
                    {
                        var idsGestionPago = string.Join(",", listaGestiones.Select(x => x.IdGestionPago));

                        var queryCronogramas = $"SELECT Id, IdGestionPago, NumeroCuota, MontoCuota, FechaVencimiento, FechaProbablePago, FechaRealPago FROM fin.T_GestionPagoCronograma WHERE IdGestionPago IN ({idsGestionPago}) AND Estado = 1";
                        var resCronogramas = _dapperRepository.QueryDapper(queryCronogramas, null);
                        var listaCronogramas = new List<GestionPagoCronogramaDTO>();
                        if (!string.IsNullOrEmpty(resCronogramas) && !resCronogramas.Contains("[]")) {
                            listaCronogramas = JsonConvert.DeserializeObject<List<GestionPagoCronogramaDTO>>(resCronogramas);
                        }

                        var queryArchivos = $"SELECT Id, IdGestionPago, IdGestionPagoCronograma, NombreArchivo, ContentTypeArchivo FROM fin.T_GestionPagoArchivo WHERE IdGestionPago IN ({idsGestionPago}) AND Estado = 1";
                        var resArchivos = _dapperRepository.QueryDapper(queryArchivos, null);
                        var listaArchivos = new List<GestionPagoArchivoDTO>();
                        if (!string.IsNullOrEmpty(resArchivos) && !resArchivos.Contains("[]")) {
                            listaArchivos = JsonConvert.DeserializeObject<List<GestionPagoArchivoDTO>>(resArchivos);
                        }

                        foreach (var gestion in listaGestiones)
                        {
                            gestion.Cronograma = listaCronogramas.Where(x => x.IdGestionPago == gestion.IdGestionPago).ToList();
                            var archivosGestion = listaArchivos.Where(x => x.IdGestionPago == gestion.IdGestionPago).ToList();

                            // Archivos de cabecera
                            gestion.Archivos = archivosGestion.Where(x => x.IdGestionPagoCronograma == null).ToList();

                            // Archivos por cuota
                            foreach(var cuota in gestion.Cronograma)
                            {
                                cuota.Archivos = archivosGestion.Where(x => x.IdGestionPagoCronograma == cuota.Id).ToList();
                            }
                        }
                    }
                }
                return listaGestiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de comprobantes con gestión de pago aplicando los filtros recibidos.
        /// </summary>
        public IEnumerable<ReporteComprobanteGestionPagoDTO> ObtenerReporteComprobantesYPagos(FiltroGestionPagoDTO filtro)
        {
            try
            {
                List<ReporteComprobanteGestionPagoDTO> listaReporte = new List<ReporteComprobanteGestionPagoDTO>();

                var _query = @"
                    SELECT
                        CP.Id AS IdComprobantePago,
                        CP.SerieComprobante,
                        CP.NumeroComprobante,
                        CP.FechaEmision,
                        CP.MontoBruto,
                        CP.MontoNeto,
                        CP.IdProveedor,
                        COALESCE(
                            NULLIF(LTRIM(RTRIM(CONCAT_WS(' ',
                                NULLIF(LTRIM(RTRIM(P.Nombre1)), ''),
                                NULLIF(LTRIM(RTRIM(P.Nombre2)), ''),
                                NULLIF(LTRIM(RTRIM(P.ApePaterno)), ''),
                                NULLIF(LTRIM(RTRIM(P.ApeMaterno)), '')
                            ))), ''),
                            NULLIF(LTRIM(RTRIM(P.RazonSocial)), '')
                        ) AS RazonSocial,
                        CP.IdMoneda,
                        M.Nombre AS NombreMoneda,
                        CP.IdEmpresa,
                        E.Nombre AS NombreEmpresa,
                        GP.Id AS IdGestionPago,
                        GP.ServicioValidado,
                        GP.FechaSolicitud,
                        GP.ObservacionDocumentacion,
                        GP.LevantamientoObservacion,
                        GP.ConformidadFinanzas,
                        GP.ObservacionProgramacionPago,
                        GP.IdModalidadPago,
                        MP.Nombre AS NombreModalidadPago,
                        GP.IdPagoEstado,
                        PE.Nombre AS NombrePagoEstado,
                        (SELECT STRING_AGG(CC.Nombre, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         INNER JOIN pla.T_CentroCosto CC WITH (NOLOCK) ON F.IdCentroCosto = CC.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1 AND CC.Estado = 1) AS NombreCentroCosto,
                        (SELECT STRING_AGG(PR.Nombre, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         INNER JOIN fin.T_Producto PR WITH (NOLOCK) ON F.IdProducto = PR.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1 AND PR.Estado = 1) AS NombreProducto,
                        (SELECT STRING_AGG(PG.Nombre, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         INNER JOIN pla.T_CentroCosto CC WITH (NOLOCK) ON F.IdCentroCosto = CC.Id
                         INNER JOIN pla.T_PGeneral PG WITH (NOLOCK) ON CC.IdPgeneral = CAST(PG.Id AS VARCHAR)
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1 AND CC.Estado = 1 AND PG.Estado = 1) AS NombreProgramaGeneral,
                        (SELECT STRING_AGG(F.Codigo, ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1) AS Fur,
                        (SELECT STRING_AGG(CAST(F.Cantidad AS VARCHAR), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1) AS Cantidad,
                        (SELECT STRING_AGG(CAST(F.PrecioUnitarioMonedaOrigen AS VARCHAR), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1) AS PrecioUnitario,
                        (SELECT STRING_AGG(FORMAT(F.FechaLimite, 'MMMM', 'es-ES'), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1) AS MesEjecucion,
                        (SELECT STRING_AGG(FORMAT(F.FechaLimite, 'dd/MM/yyyy'), ', ')
                         FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                         INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                         WHERE CPF.IdComprobantePago = CP.Id AND CPF.Estado = 1 AND F.Estado = 1) AS FechaEjecucion
                    FROM fin.T_ComprobantePago AS CP
                    LEFT JOIN fin.T_GestionPago AS GP ON CP.Id = GP.IdComprobantePago AND GP.Estado = 1
                    LEFT JOIN fin.T_ModalidadPago AS MP ON GP.IdModalidadPago = MP.Id
                    LEFT JOIN fin.T_PagoEstado AS PE ON GP.IdPagoEstado = PE.Id
                    LEFT JOIN fin.T_Proveedor AS P ON CP.IdProveedor = P.Id
                    LEFT JOIN pla.T_Moneda AS M ON CP.IdMoneda = M.Id
                    LEFT JOIN pla.T_Empresa AS E ON CP.IdEmpresa = E.Id
                    WHERE CP.Estado = 1
                      AND EXISTS (
                          SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                          INNER JOIN fin.T_FurPago FP WITH (NOLOCK) ON FP.IdFur = CPF.IdFur
                          WHERE CPF.IdComprobantePago = CP.Id
                            AND CPF.Estado = 1
                            AND FP.Estado = 1
                            AND FP.PrecioTotalMonedaDolares <> 0
                      )";

                if (filtro.IdComprobantePago.HasValue) _query += " AND CP.Id = @IdComprobantePago";
                if (filtro.IdProveedor.HasValue) _query += " AND CP.IdProveedor = @IdProveedor";
                if (filtro.IdPagoEstado.HasValue) _query += " AND GP.IdPagoEstado = @IdPagoEstado";
                if (filtro.IdModalidadPago.HasValue) _query += " AND GP.IdModalidadPago = @IdModalidadPago";
                if (filtro.IdEmpresa.HasValue) _query += " AND CP.IdEmpresa = @IdEmpresa";
                if (filtro.FechaDesde.HasValue) _query += " AND CP.FechaEmision >= @FechaDesde";
                if (filtro.FechaHasta.HasValue) _query += " AND CP.FechaEmision <= @FechaHasta";

                // Filtro por FUR
                if (filtro.IdFur.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        WHERE CPF.IdComprobantePago = CP.Id
                        AND CPF.IdFur = @IdFur
                        AND CPF.Estado = 1)";
                }

                // Filtro por Producto (a través de FUR)
                if (filtro.IdProducto.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                        WHERE CPF.IdComprobantePago = CP.Id
                        AND F.IdProducto = @IdProducto
                        AND CPF.Estado = 1 AND F.Estado = 1)";
                }

                // Filtro por Centro de Costo (a través de FUR)
                if (filtro.IdCentroCosto.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                        WHERE CPF.IdComprobantePago = CP.Id
                        AND F.IdCentroCosto = @IdCentroCosto
                        AND CPF.Estado = 1 AND F.Estado = 1)";
                }

                // Filtro por Programa General (a través de FUR → CentroCosto)
                if (filtro.IdProgramaGeneral.HasValue)
                {
                    _query += @" AND EXISTS (
                        SELECT 1 FROM fin.T_ComprobantePagoPorFur CPF WITH (NOLOCK)
                        INNER JOIN fin.T_Fur F WITH (NOLOCK) ON CPF.IdFur = F.Id
                        INNER JOIN pla.T_CentroCosto CC WITH (NOLOCK) ON F.IdCentroCosto = CC.Id
                        WHERE CPF.IdComprobantePago = CP.Id
                        AND CC.IdPgeneral = CAST(@IdProgramaGeneral AS VARCHAR)
                        AND CPF.Estado = 1 AND F.Estado = 1 AND CC.Estado = 1)";
                }

                var queryResultado = _dapperRepository.QueryDapper(_query, new
                {
                    filtro.IdComprobantePago,
                    filtro.IdProveedor,
                    filtro.IdPagoEstado,
                    filtro.IdModalidadPago,
                    filtro.IdEmpresa,
                    filtro.FechaDesde,
                    filtro.FechaHasta,
                    filtro.IdFur,
                    filtro.IdProducto,
                    filtro.IdCentroCosto,
                    filtro.IdProgramaGeneral
                });

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaReporte = JsonConvert.DeserializeObject<List<ReporteComprobanteGestionPagoDTO>>(queryResultado);

                    if (listaReporte.Any())
                    {
                        var gestionesIniciadas = listaReporte.Where(x => x.IdGestionPago.HasValue).Select(x => x.IdGestionPago.Value).ToList();

                        if (gestionesIniciadas.Any())
                        {
                            var idsGestionPago = string.Join(",", gestionesIniciadas);

                            var queryCronogramas = $"SELECT Id, IdGestionPago, NumeroCuota, MontoCuota, FechaVencimiento, FechaProbablePago, FechaRealPago FROM fin.T_GestionPagoCronograma WHERE IdGestionPago IN ({idsGestionPago}) AND Estado = 1";
                            var resCronogramas = _dapperRepository.QueryDapper(queryCronogramas, null);
                            var listaCronogramas = new List<GestionPagoCronogramaDTO>();
                            if (!string.IsNullOrEmpty(resCronogramas) && !resCronogramas.Contains("[]")) {
                                listaCronogramas = JsonConvert.DeserializeObject<List<GestionPagoCronogramaDTO>>(resCronogramas);
                            }

                            var queryArchivos = $"SELECT Id, IdGestionPago, IdGestionPagoCronograma, NombreArchivo, ContentTypeArchivo FROM fin.T_GestionPagoArchivo WHERE IdGestionPago IN ({idsGestionPago}) AND Estado = 1";
                            var resArchivos = _dapperRepository.QueryDapper(queryArchivos, null);
                            var listaArchivos = new List<GestionPagoArchivoDTO>();
                            if (!string.IsNullOrEmpty(resArchivos) && !resArchivos.Contains("[]")) {
                                listaArchivos = JsonConvert.DeserializeObject<List<GestionPagoArchivoDTO>>(resArchivos);
                            }

                            foreach (var reporte in listaReporte.Where(x => x.IdGestionPago.HasValue))
                            {
                                reporte.Cronograma = listaCronogramas.Where(x => x.IdGestionPago == reporte.IdGestionPago).ToList();
                                var archivosGestion = listaArchivos.Where(x => x.IdGestionPago == reporte.IdGestionPago).ToList();

                                // Archivos de cabecera
                                reporte.Archivos = archivosGestion.Where(x => x.IdGestionPagoCronograma == null).ToList();

                                // Archivos por cuota
                                foreach(var cuota in reporte.Cronograma)
                                {
                                    cuota.Archivos = archivosGestion.Where(x => x.IdGestionPagoCronograma == cuota.Id).ToList();
                                }
                            }
                        }
                    }
                }
                return listaReporte;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public GestionPagoDTO? ObtenerGestionPagoPorId(int idGestionPago)
        {
            try
            {
                GestionPagoDTO? gestionPago = null;

                var queryResultado = _dapperRepository.QuerySPFirstOrDefault(
                    "fin.SP_GestionPagoComprobanteDetalle",
                    new { IdGestionPago = idGestionPago });

                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "null")
                {
                    gestionPago = JsonConvert.DeserializeObject<GestionPagoDTO>(queryResultado);
                }
                return gestionPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GestionPagoDTO? ObtenerGestionPagoPorComprobante(int idComprobantePago)
        {
            try
            {
                GestionPagoDTO? gestionPago = null;

                var queryResultado = _dapperRepository.QuerySPFirstOrDefault(
                    "fin.SP_GestionPagoComprobanteDetalle",
                    new { IdComprobantePago = idComprobantePago });

                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "null")
                {
                    gestionPago = JsonConvert.DeserializeObject<GestionPagoDTO>(queryResultado);
                }
                return gestionPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
