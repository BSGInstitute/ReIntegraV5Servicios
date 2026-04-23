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
    /// Repositorio para gestión de T_GestionPagoCronograma (cuotas de pago)
    /// </summary>
    public class GestionPagoCronogramaRepository : GenericRepository<TGestionPagoCronograma>, IGestionPagoCronogramaRepository
    {
        private const string UsuarioSistema = "Sistema";
        private Mapper _mapper;

        public GestionPagoCronogramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGestionPagoCronograma, GestionPagoCronograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGestionPagoCronograma MapeoEntidad(GestionPagoCronograma entidad)
        {
            try
            {
                TGestionPagoCronograma modelo = _mapper.Map<TGestionPagoCronograma>(entidad);
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

        private static void AsignarCamposAuditoriaInsercion(TGestionPagoCronograma modelo)
        {
            var fechaActual = DateTime.Now;
            var usuarioAuditoria = ObtenerUsuarioAuditoria(modelo.UsuarioCreacion, modelo.UsuarioModificacion);

            modelo.FechaCreacion = fechaActual;
            modelo.FechaModificacion = fechaActual;
            modelo.UsuarioCreacion = usuarioAuditoria;
            modelo.UsuarioModificacion = usuarioAuditoria;
            modelo.Estado = true;
        }

        private static void AsignarCamposAuditoriaActualizacion(TGestionPagoCronograma modelo, TGestionPagoCronograma entidadExistente)
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

        public TGestionPagoCronograma Add(GestionPagoCronograma entidad)
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

        public TGestionPagoCronograma Update(GestionPagoCronograma entidad)
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

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, ObtenerUsuarioAuditoria(usuario));
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TGestionPagoCronograma> Add(IEnumerable<GestionPagoCronograma> listadoEntidad)
        {
            try
            {
                List<TGestionPagoCronograma> listado = new List<TGestionPagoCronograma>();
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

        public IEnumerable<TGestionPagoCronograma> Update(IEnumerable<GestionPagoCronograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGestionPagoCronograma> listado = new List<TGestionPagoCronograma>();
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

        public bool ActualizarFechaPago(int idCronograma, DateTime fechaRealPago, string usuario)
        {
            try
            {
                var existente = base.FirstBy(w => w.Id == idCronograma && w.Estado == true);
                if (existente == null)
                    throw new Exception($"No se encontró la cuota con Id {idCronograma}");

                existente.FechaRealPago = fechaRealPago;
                existente.UsuarioModificacion = ObtenerUsuarioAuditoria(usuario, existente.UsuarioCreacion);
                existente.FechaModificacion = DateTime.Now;
                base.Update(existente);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionPagoCronogramaDTO> ObtenerCronogramaPorGestionPago(int idGestionPago)
        {
            try
            {
                // Query 1: Obtener cuotas
                var _queryCuotas = @"
                    SELECT 
                        Id, IdGestionPago, NumeroCuota, MontoCuota,
                        FechaVencimiento, FechaProbablePago, FechaRealPago
                    FROM fin.T_GestionPagoCronograma
                    WHERE IdGestionPago = @IdGestionPago AND Estado = 1
                    ORDER BY NumeroCuota";

                List<GestionPagoCronogramaDTO> cronogramas = new List<GestionPagoCronogramaDTO>();
                var resultadoCuotas = _dapperRepository.QueryDapper(_queryCuotas, new { IdGestionPago = idGestionPago });

                if (!string.IsNullOrEmpty(resultadoCuotas) && !resultadoCuotas.Contains("[]"))
                {
                    cronogramas = JsonConvert.DeserializeObject<List<GestionPagoCronogramaDTO>>(resultadoCuotas);
                }

                if (cronogramas.Count == 0) return cronogramas;

                // Query 2: Obtener todos los archivos de todas las cuotas en una sola consulta (evita N+1)
                var _queryArchivos = @"
                    SELECT 
                        Id, IdGestionPago, IdGestionPagoCronograma,
                        NombreArchivo, ContentTypeArchivo
                    FROM fin.T_GestionPagoArchivo
                    WHERE IdGestionPagoCronograma IN (
                        SELECT Id FROM fin.T_GestionPagoCronograma
                        WHERE IdGestionPago = @IdGestionPago AND Estado = 1
                    ) AND Estado = 1";

                List<GestionPagoArchivoDTO> todosArchivos = new List<GestionPagoArchivoDTO>();
                var resultadoArchivos = _dapperRepository.QueryDapper(_queryArchivos, new { IdGestionPago = idGestionPago });

                if (!string.IsNullOrEmpty(resultadoArchivos) && !resultadoArchivos.Contains("[]"))
                {
                    todosArchivos = JsonConvert.DeserializeObject<List<GestionPagoArchivoDTO>>(resultadoArchivos);
                }

                // Agrupar en memoria
                foreach (var cuota in cronogramas)
                {
                    cuota.Archivos = todosArchivos.Where(a => a.IdGestionPagoCronograma == cuota.Id).ToList();
                }

                return cronogramas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
