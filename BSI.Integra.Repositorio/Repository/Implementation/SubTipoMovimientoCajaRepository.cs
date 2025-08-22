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
    /// Repositorio: SubTipoMovimientoCajaRepository
    /// Autor: Margiory Ramirez.
    /// Fecha: 20/12/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class SubTipoMovimientoCajaRepository : GenericRepository<TSubTipoMovimientoCaja>, ISubTipoMovimientoCajaRepository
    {
        private Mapper _mapper;

        public SubTipoMovimientoCajaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubTipoMovimientoCaja, SubTipoMovimientoCaja>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TSubTipoMovimientoCaja MapeoEntidad(SubTipoMovimientoCaja entidad)
        {
            try
            {
                //crea la entidad padre
                TSubTipoMovimientoCaja modelo = _mapper.Map<TSubTipoMovimientoCaja>(entidad);

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

        public TSubTipoMovimientoCaja Add(SubTipoMovimientoCaja entidad)
        {
            try
            {
                var SubTipoMovimientoCaja = MapeoEntidad(entidad);
                base.Insert(SubTipoMovimientoCaja);
                return SubTipoMovimientoCaja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubTipoMovimientoCaja Update(SubTipoMovimientoCaja entidad)
        {
            try
            {
                var SubTipoMovimientoCaja = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SubTipoMovimientoCaja.RowVersion = entidadExistente.RowVersion;

                base.Update(SubTipoMovimientoCaja);
                return SubTipoMovimientoCaja;
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


        public IEnumerable<TSubTipoMovimientoCaja> Add(IEnumerable<SubTipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                List<TSubTipoMovimientoCaja> listado = new List<TSubTipoMovimientoCaja>();
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

        public IEnumerable<TSubTipoMovimientoCaja> Update(IEnumerable<SubTipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSubTipoMovimientoCaja> listado = new List<TSubTipoMovimientoCaja>();
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
        /// Obtiene los SubTipoMovimientoCaja (para ser usada en Combobox)
        /// </summary>
        /// <returns> List<SubTipoMovimientoCajaDTO> </returns>

        public List<SubTipoMovimientoCajaDTO> ObtenerListaSubTipoMovimientoCaja()
        {
            try
            {
                List<SubTipoMovimientoCajaDTO> Lista = new List<SubTipoMovimientoCajaDTO>();
                var _query = "SELECT Id, IdTipoMovimientoCaja, Nombre FROM fin.T_SubTipoMovimientoCaja WHERE Estado=1";
                var listaDB = _dapperRepository.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    Lista = JsonConvert.DeserializeObject<List<SubTipoMovimientoCajaDTO>>(listaDB);
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
