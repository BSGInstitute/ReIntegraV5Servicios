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

                var _query = @"SELECT * FROM fin.V_GestionPagoCabecera WHERE 1 = 1";

                if (filtro.IdComprobantePago.HasValue) _query += " AND IdComprobantePago = @IdComprobantePago";
                if (filtro.IdProveedor.HasValue) _query += " AND IdProveedor = @IdProveedor";
                if (filtro.IdPagoEstado.HasValue) _query += " AND IdPagoEstado = @IdPagoEstado";
                if (filtro.IdModalidadPago.HasValue) _query += " AND IdModalidadPago = @IdModalidadPago";
                if (filtro.IdEmpresa.HasValue) _query += " AND IdEmpresa = @IdEmpresa";
                if (filtro.FechaDesde.HasValue) _query += " AND FechaSolicitud >= @FechaDesde";
                if (filtro.FechaHasta.HasValue) _query += " AND FechaSolicitud <= @FechaHasta";

                var queryResultado = _dapperRepository.QueryDapper(_query, new
                {
                    filtro.IdComprobantePago,
                    filtro.IdProveedor,
                    filtro.IdPagoEstado,
                    filtro.IdModalidadPago,
                    filtro.IdEmpresa,
                    filtro.FechaDesde,
                    filtro.FechaHasta
                });

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaGestiones = JsonConvert.DeserializeObject<List<GestionPagoDTO>>(queryResultado);
                }
                return listaGestiones;
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
