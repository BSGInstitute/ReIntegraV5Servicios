using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelSdtResVenItemRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtResVenItem
    /// </summary>
    public class SentinelSdtResVenItemRepository : GenericRepository<TSentinelSdtResVenItem>, ISentinelSdtResVenItemRepository
    {
        private Mapper _mapper;

        public SentinelSdtResVenItemRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSdtResVenItem, SentinelSdtResVenItem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSdtResVenItem MapeoEntidad(SentinelSdtResVenItem entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtResVenItem modelo = _mapper.Map<TSentinelSdtResVenItem>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtResVenItem Add(SentinelSdtResVenItem entidad)
        {
            try
            {
                var SentinelSdtResVenItem = MapeoEntidad(entidad);
                base.Insert(SentinelSdtResVenItem);
                return SentinelSdtResVenItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtResVenItem Update(SentinelSdtResVenItem entidad)
        {
            try
            {
                var SentinelSdtResVenItem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtResVenItem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtResVenItem);
                return SentinelSdtResVenItem;
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


        public IEnumerable<TSentinelSdtResVenItem> Add(IEnumerable<SentinelSdtResVenItem> listadoEntidad)
        {
            try
            {
                List<TSentinelSdtResVenItem> listado = new List<TSentinelSdtResVenItem>();
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

        public IEnumerable<TSentinelSdtResVenItem> Update(IEnumerable<SentinelSdtResVenItem> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSdtResVenItem> listado = new List<TSentinelSdtResVenItem>();
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
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelSdtResVenItem.
        /// </summary>
        /// <returns> List<SentinelSdtResVenItemDTO> </returns>
        public IEnumerable<SentinelSdtResVenItemDTO> ObtenerSentinelSdtResVenItem()
        {
            try
            {
                List<SentinelSdtResVenItemDTO> rpta = new List<SentinelSdtResVenItemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDocumento,
	                    NroDocumento,
	                    CantidadDocs,
	                    Fuente,
	                    Entidad,
	                    Monto,
	                    Cantidad,
	                    DiasVencidos,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtResVenItem
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtResVenItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtResVenItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtResVenItemComboDTO> </returns>
        public IEnumerable<SentinelSdtResVenItemComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSdtResVenItemComboDTO> rpta = new List<SentinelSdtResVenItemComboDTO>();
                var query = @"SELECT Id,NroDocumento,Fuente,Entidad FROM com.T_SentinelSdtResVenItem WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtResVenItemComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SentinelSdtResVenItem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtResVenItemDTO> </returns>
        public IEnumerable<SentinelSdtResVenItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelSdtResVenItemDTO> rpta = new List<SentinelSdtResVenItemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDocumento,
	                    NroDocumento,
	                    CantidadDocs,
	                    Fuente,
	                    Entidad,
	                    Monto,
	                    Cantidad,
	                    DiasVencidos,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtResVenItem
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtResVenItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/11/2022
        /// <summary>
        /// Obtiene Los Datos Vencidos Para El detalle de Sentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtResVenItemDatosVencidosDTO> ObtenerDatosVencidos(int idSentinel)
        {
            try
            {
                string _queryDatosVencido = "Select TipoDocumento,NroDocumento,CantidadDocs,Fuente,Monto,Cantidad,DiasVencidos,Entidad from com.V_TSentinelSdtResVenItem_ObtenerDatosVencidos Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryDatosVencido = _dapperRepository.QueryDapper(_queryDatosVencido, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtResVenItemDatosVencidosDTO>>(queryDatosVencido);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
