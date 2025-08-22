using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelRepLegItemRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión general de T_SentinelRepLegItem
    /// </summary>
    public class SentinelRepLegItemRepository : GenericRepository<TSentinelRepLegItem>, ISentinelRepLegItemRepository
    {
        private Mapper _mapper;

        public SentinelRepLegItemRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelRepLegItem, SentinelRepLegItem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelRepLegItem MapeoEntidad(SentinelRepLegItem entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelRepLegItem modelo = _mapper.Map<TSentinelRepLegItem>(entidad);

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

        public TSentinelRepLegItem Add(SentinelRepLegItem entidad)
        {
            try
            {
                var SentinelRepLegItem = MapeoEntidad(entidad);
                base.Insert(SentinelRepLegItem);
                return SentinelRepLegItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelRepLegItem Update(SentinelRepLegItem entidad)
        {
            try
            {
                var SentinelRepLegItem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelRepLegItem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelRepLegItem);
                return SentinelRepLegItem;
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


        public IEnumerable<TSentinelRepLegItem> Add(IEnumerable<SentinelRepLegItem> listadoEntidad)
        {
            try
            {
                List<TSentinelRepLegItem> listado = new List<TSentinelRepLegItem>();
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

        public IEnumerable<TSentinelRepLegItem> Update(IEnumerable<SentinelRepLegItem> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelRepLegItem> listado = new List<TSentinelRepLegItem>();
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
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelRepLegItem.
        /// </summary>
        /// <returns> List<SentinelRepLegItemDTO> </returns>
        public IEnumerable<SentinelRepLegItemDTO> ObtenerSentinelRepLegItem()
        {
            try
            {
                List<SentinelRepLegItemDTO> rpta = new List<SentinelRepLegItemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDocumento,
	                    NumeroDocumento,
	                    Nombres,
	                    ApellidoPaterno,
	                    ApellidoMaterno,
	                    RazonSocial,
	                    Cargo,
	                    SemaforoActual,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelRepLegItem WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelRepLegItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelRepLegItem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelRepLegItemComboDTO> </returns>
        public IEnumerable<SentinelRepLegItemComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelRepLegItemComboDTO> rpta = new List<SentinelRepLegItemComboDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    NumeroDocumento,
	                    RazonSocial
                    FROM com.T_SentinelRepLegItem
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelRepLegItemComboDTO>>(resultado);
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
        /// Obtiene los registros de T_SentinelRepLegItem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelRepLegItemDTO> </returns>
        public IEnumerable<SentinelRepLegItemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelRepLegItemDTO> rpta = new List<SentinelRepLegItemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDocumento,
	                    NumeroDocumento,
	                    Nombres,
	                    ApellidoPaterno,
	                    ApellidoMaterno,
	                    RazonSocial,
	                    Cargo,
	                    SemaforoActual,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelRepLegItem
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelRepLegItemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
