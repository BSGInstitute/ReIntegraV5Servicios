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
    /// Repositorio: OtroMovimientoCajaRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class OtroMovimientoCajaRepository : GenericRepository<TOtroMovimientoCaja>, IOtroMovimientoCajaRepository
    {
        private Mapper _mapper;

        public OtroMovimientoCajaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOtroMovimientoCaja, OtroMovimientoCaja>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TOtroMovimientoCaja MapeoEntidad(OtroMovimientoCaja entidad)
        {
            try
            {
                //crea la entidad padre
                TOtroMovimientoCaja modelo = _mapper.Map<TOtroMovimientoCaja>(entidad);

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

        public TOtroMovimientoCaja Add(OtroMovimientoCaja entidad)
        {
            try
            {
                var OtroMovimientoCaja = MapeoEntidad(entidad);
                base.Insert(OtroMovimientoCaja);
                return OtroMovimientoCaja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOtroMovimientoCaja Update(OtroMovimientoCaja entidad)
        {
            try
            {
                var OtroMovimientoCaja = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OtroMovimientoCaja.RowVersion = entidadExistente.RowVersion;

                base.Update(OtroMovimientoCaja);
                return OtroMovimientoCaja;
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


        public IEnumerable<TOtroMovimientoCaja> Add(IEnumerable<OtroMovimientoCaja> listadoEntidad)
        {
            try
            {
                List<TOtroMovimientoCaja> listado = new List<TOtroMovimientoCaja>();
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

        public IEnumerable<TOtroMovimientoCaja> Update(IEnumerable<OtroMovimientoCaja> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOtroMovimientoCaja> listado = new List<TOtroMovimientoCaja>();
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
        /// Autor:Margiory Ramirez.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene La lista de registros con Estado=1  (usado para llenar grilla en CRUD)
        /// </summary>
        /// <returns></returns>
        public List<OtroMovimientoCajaDTO> ObtenerListaOtroMovimientoCaja()
        {
            try
            {
                List<OtroMovimientoCajaDTO> OtroMovimientoCajaFinanzas = new List<OtroMovimientoCajaDTO>();
                var _query = "SELECT * FROM fin.V_OtroMovimientoCajaObtenerDatos  where Estado=1 order by Id desc";
                var OtroMovimientoCajaFinanzasDB = _dapperRepository.QueryDapper(_query, null);
                if (!OtroMovimientoCajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(OtroMovimientoCajaFinanzasDB))
                {
                    OtroMovimientoCajaFinanzas = JsonConvert.DeserializeObject<List<OtroMovimientoCajaDTO>>(OtroMovimientoCajaFinanzasDB);
                }
                return OtroMovimientoCajaFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// <summary>
        /// Obtiene un Registro con Estado=1  de OtroMovimientoCaja por su Id
        /// </summary>
        /// <returns></returns>
        public List<OtroMovimientoCajaDTO> ObtenerOtroMovimientoCajaPorID(int Id)
        {
            try
            {
                List<OtroMovimientoCajaDTO> OtroMovimientoCajaFinanzas = new List<OtroMovimientoCajaDTO>();
                var _query = "SELECT * FROM fin.V_OtroMovimientoCajaObtenerDatos  where Estado=1 AND Id=" + Id;
                var OtroMovimientoCajaFinanzasDB = _dapperRepository.QueryDapper(_query, null);
                if (!OtroMovimientoCajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(OtroMovimientoCajaFinanzasDB))
                {
                    OtroMovimientoCajaFinanzas = JsonConvert.DeserializeObject<List<OtroMovimientoCajaDTO>>(OtroMovimientoCajaFinanzasDB);
                }
                return OtroMovimientoCajaFinanzas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }





    }
}
