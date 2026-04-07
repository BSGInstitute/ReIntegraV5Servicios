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
    /// Repositorio para gestión de T_GestionPagoArchivo (archivos adjuntos)
    /// </summary>
    public class GestionPagoArchivoRepository : GenericRepository<TGestionPagoArchivo>, IGestionPagoArchivoRepository
    {
        private const string UsuarioSistema = "Sistema";
        private Mapper _mapper;

        public GestionPagoArchivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGestionPagoArchivo, GestionPagoArchivo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TGestionPagoArchivo MapeoEntidad(GestionPagoArchivo entidad)
        {
            try
            {
                TGestionPagoArchivo modelo = _mapper.Map<TGestionPagoArchivo>(entidad);
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

        private static void AsignarCamposAuditoriaInsercion(TGestionPagoArchivo modelo)
        {
            var fechaActual = DateTime.Now;
            var usuarioAuditoria = ObtenerUsuarioAuditoria(modelo.UsuarioCreacion, modelo.UsuarioModificacion);

            modelo.FechaCreacion = fechaActual;
            modelo.FechaModificacion = fechaActual;
            modelo.UsuarioCreacion = usuarioAuditoria;
            modelo.UsuarioModificacion = usuarioAuditoria;
            modelo.Estado = true;
        }

        private static void AsignarCamposAuditoriaActualizacion(TGestionPagoArchivo modelo, TGestionPagoArchivo entidadExistente)
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

        public TGestionPagoArchivo Add(GestionPagoArchivo entidad)
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

        public TGestionPagoArchivo Update(GestionPagoArchivo entidad)
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

        public IEnumerable<TGestionPagoArchivo> Add(IEnumerable<GestionPagoArchivo> listadoEntidad)
        {
            try
            {
                List<TGestionPagoArchivo> listado = new List<TGestionPagoArchivo>();
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

        public IEnumerable<TGestionPagoArchivo> Update(IEnumerable<GestionPagoArchivo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TGestionPagoArchivo> listado = new List<TGestionPagoArchivo>();
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

        public IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosPorGestionPago(int idGestionPago)
        {
            try
            {
                List<GestionPagoArchivoDTO> listaArchivos = new List<GestionPagoArchivoDTO>();

                var _query = @"
                    SELECT 
                        Id, IdGestionPago, IdGestionPagoCronograma,
                        NombreArchivo, ContentTypeArchivo
                    FROM fin.T_GestionPagoArchivo
                    WHERE IdGestionPago = @IdGestionPago 
                        AND IdGestionPagoCronograma IS NULL 
                        AND Estado = 1";

                var queryResultado = _dapperRepository.QueryDapper(_query, new { IdGestionPago = idGestionPago });

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaArchivos = JsonConvert.DeserializeObject<List<GestionPagoArchivoDTO>>(queryResultado);
                }
                return listaArchivos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosPorCronograma(int idGestionPagoCronograma)
        {
            try
            {
                List<GestionPagoArchivoDTO> listaArchivos = new List<GestionPagoArchivoDTO>();

                var _query = @"
                    SELECT 
                        Id, IdGestionPago, IdGestionPagoCronograma,
                        NombreArchivo, ContentTypeArchivo
                    FROM fin.T_GestionPagoArchivo
                    WHERE IdGestionPagoCronograma = @IdGestionPagoCronograma AND Estado = 1";

                var queryResultado = _dapperRepository.QueryDapper(_query, new { IdGestionPagoCronograma = idGestionPagoCronograma });

                if (!string.IsNullOrEmpty(queryResultado) && !queryResultado.Contains("[]"))
                {
                    listaArchivos = JsonConvert.DeserializeObject<List<GestionPagoArchivoDTO>>(queryResultado);
                }
                return listaArchivos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
