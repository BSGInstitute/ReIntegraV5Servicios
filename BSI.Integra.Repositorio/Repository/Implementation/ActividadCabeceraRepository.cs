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
    /// Repositorio: ActividadCabeceraRepository
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ActividadCabecera
    /// </summary>
    public class ActividadCabeceraRepository : GenericRepository<TActividadCabecera>, IActividadCabeceraRepository
    {
        private Mapper _mapper;

        public ActividadCabeceraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadCabecera, ActividadCabecera>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TActividadCabecera MapeoEntidad(ActividadCabecera entidad)
        {
            try
            {
                //crea la entidad padre
                TActividadCabecera modelo = _mapper.Map<TActividadCabecera>(entidad);

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

        public TActividadCabecera Add(ActividadCabecera entidad)
        {
            try
            {
                var ActividadCabecera = MapeoEntidad(entidad);
                base.Insert(ActividadCabecera);
                return ActividadCabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TActividadCabecera Update(ActividadCabecera entidad)
        {
            try
            {
                var ActividadCabecera = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ActividadCabecera.RowVersion = entidadExistente.RowVersion;

                base.Update(ActividadCabecera);
                return ActividadCabecera;
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


        public IEnumerable<TActividadCabecera> Add(IEnumerable<ActividadCabecera> listadoEntidad)
        {
            try
            {
                List<TActividadCabecera> listado = new List<TActividadCabecera>();
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

        public IEnumerable<TActividadCabecera> Update(IEnumerable<ActividadCabecera> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TActividadCabecera> listado = new List<TActividadCabecera>();
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


        public List<ComboDTO> ObtenerFiltro()
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = "SELECT id, nombre FROM com.T_ActividadCabecera WHERE estado = 1";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ActividadCabeceraDTO> ObtenerTodoActividadAutomatica()
        {
            try
            {
                List<ActividadCabeceraDTO> ACabecera = new List<ActividadCabeceraDTO>();
                var _query = "SELECT Id, Nombre, Descripcion, FechaCreacion2, DuracionEstimada, ReproManual, ReproAutomatica, IdPlantilla, IdActividadBase, FechaModificacion2, ValidaLlamada, IdPlantillaSpeech, NumeroMaximoLlamadas" +
                    " ,IdConjuntoLista,IdFrecuencia,FechaInicioActividad,DiaFrecuenciaMensual,EsRepetitivo,HoraInicio,HoraFin,CantidadIntevaloTiempo,IdTiempoIntervalo,Activo,FechaFinActividad, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdFacebookCuentaPublicitaria, NombreActividadBase, NombreConjuntoLista FROM [com].[V_ObtenerActividadesCabecera_Agenda] WHERE Estado = 1 AND EsEnvioMasivo=1 Order by FechaCreacion desc";
                var ACabeceraDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(ACabeceraDB) && !ACabeceraDB.Contains("[]"))
                {
                    ACabecera = JsonConvert.DeserializeObject<List<ActividadCabeceraDTO>>(ACabeceraDB);
                }
                return ACabecera;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ComboDTO> ObtenerActividadesBaseMasivo()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();
                string query = "SELECT Id, Nombre FROM mkt.V_TActividadBase_ObtenerParaCombo WHERE Id!=1";
                var responseQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(responseQuery) && !responseQuery.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(responseQuery);
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ActividadCabeceraDTO> ObtenerActividadPorId(int IdActividadCabecera)
        {
            try
            {
                List<ActividadCabeceraDTO> ReprogramacionCab = new List<ActividadCabeceraDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre, Descripcion, DuracionEstimada, ReproManual, ReproAutomatica, Idplantilla, IdActividadBase, ValidaLlamada, IdPlantilla_Speech, NumeroMaximo_Llamadas, Estado, IdConjuntoLista, IdFrecuencia, DiaFrecuenciaMensual, EsRepetitivo, HoraInicio, HoraFin, CantidadIntevaloTiempo, IdTiempoIntervalo, Activo, FechaInicioActividad, FechaFinActividad, IdPersonalAreaTrabajo, EsEnvioMasivo, IdTipoDato FROM com.T_ActividadCabecera where id =" + IdActividadCabecera;
                var ReprogramacionCabDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(ReprogramacionCabDB) && !ReprogramacionCabDB.Contains("[]"))
                {
                    ReprogramacionCab = JsonConvert.DeserializeObject<List<ActividadCabeceraDTO>>(ReprogramacionCabDB);
                }
                return ReprogramacionCab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
