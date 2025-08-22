using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoMovimientoCajaRepository
    /// Autor: Margiory Ramirez.
    /// Fecha: 20/12/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class TipoMovimientoCajaRepository : GenericRepository<TTipoMovimientoCaja>, ITipoMovimientoCajaRepository
    {
        private Mapper _mapper;

        public TipoMovimientoCajaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoMovimientoCaja, TipoMovimientoCaja>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TTipoMovimientoCaja MapeoEntidad(TipoMovimientoCaja entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoMovimientoCaja modelo = _mapper.Map<TTipoMovimientoCaja>(entidad);

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

        public TTipoMovimientoCaja Add(TipoMovimientoCaja entidad)
        {
            try
            {
                var TipoMovimientoCaja = MapeoEntidad(entidad);
                base.Insert(TipoMovimientoCaja);
                return TipoMovimientoCaja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoMovimientoCaja Update(TipoMovimientoCaja entidad)
        {
            try
            {
                var TipoMovimientoCaja = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoMovimientoCaja.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoMovimientoCaja);
                return TipoMovimientoCaja;
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


        public IEnumerable<TTipoMovimientoCaja> Add(IEnumerable<TipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                List<TTipoMovimientoCaja> listado = new List<TTipoMovimientoCaja>();
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

        public IEnumerable<TTipoMovimientoCaja> Update(IEnumerable<TipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoMovimientoCaja> listado = new List<TTipoMovimientoCaja>();
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
        /// Autor: Margiory Ramirez.
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los TipoMovimientoCaja (para ser usada en Combobox)
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
 
      
        public List<TipoMovimientoCajaDTO> ObtenerListaTipoMovimientoCaja()
        {
            try
            {
                List<TipoMovimientoCajaDTO> Lista = new List<TipoMovimientoCajaDTO>();
                var _query = "SELECT Id, Nombre FROM fin.T_TipoMovimientoCaja WHERE Estado=1";
                var listaDB = _dapperRepository.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    Lista = JsonConvert.DeserializeObject<List<TipoMovimientoCajaDTO>>(listaDB);
                }
                return Lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
