using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AsignacionAutomaticaConfiguracionRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomaticaConfiguracion
    /// </summary>
    public class AsignacionAutomaticaConfiguracionRepository : GenericRepository<TAsignacionAutomaticaConfiguracion>, IAsignacionAutomaticaConfiguracionRepository
    {
        private Mapper _mapper;

        public AsignacionAutomaticaConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionAutomaticaConfiguracion, AsignacionAutomaticaConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAsignacionAutomaticaConfiguracion MapeoEntidad(AsignacionAutomaticaConfiguracion entidad)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaConfiguracion modelo = _mapper.Map<TAsignacionAutomaticaConfiguracion>(entidad);

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

        public TAsignacionAutomaticaConfiguracion Add(AsignacionAutomaticaConfiguracion entidad)
        {
            try
            {
                var AsignacionAutomaticaConfiguracion = MapeoEntidad(entidad);
                base.Insert(AsignacionAutomaticaConfiguracion);
                return AsignacionAutomaticaConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionAutomaticaConfiguracion Update(AsignacionAutomaticaConfiguracion entidad)
        {
            try
            {
                var AsignacionAutomaticaConfiguracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionAutomaticaConfiguracion.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionAutomaticaConfiguracion);
                return AsignacionAutomaticaConfiguracion;
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


        public IEnumerable<TAsignacionAutomaticaConfiguracion> Add(IEnumerable<AsignacionAutomaticaConfiguracion> listadoEntidad)
        {
            try
            {
                List<TAsignacionAutomaticaConfiguracion> listado = new List<TAsignacionAutomaticaConfiguracion>();
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

        public IEnumerable<TAsignacionAutomaticaConfiguracion> Update(IEnumerable<AsignacionAutomaticaConfiguracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionAutomaticaConfiguracion> listado = new List<TAsignacionAutomaticaConfiguracion>();
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

        /// Autor:Margiory Ramirez
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AsignacionAutomaticaConfiguracion .
        /// </summary>
        /// <returns> List<AsignacionAutomaticaConfiguracionDTO> </returns>
        public List<AsignacionAutomaticaConfiguracionDTO> ObtenerConfiguracionAsignacionAutomatica()
        {
            try
            {
                List<AsignacionAutomaticaConfiguracionDTO> configuraciones = new List<AsignacionAutomaticaConfiguracionDTO>();
                var _query = string.Empty;
                _query = "SELECT C.Id,(SELECT Iif(codigo IS NULL, 0, id) FROM pla.T_FaseOportunidad WHERE  Id = C.IdFaseOportunidad) AS FaseOportunidad, IIF(IdTipoDato IS NULL,0,IdTipoDato) AS IdTipoDato,IIF(IdOrigen Is Null, 0, IdOrigen) AS IdOrigen ,Inclusivo,Habilitado FROM   mkt.T_AsignacionAutomaticaConfiguracion AS C WHERE  c.Habilitado = 1 AND c.Estado = 1 ";
                var ConfiguracionesDB = _dapperRepository.QueryDapper(_query, null);
                configuraciones = JsonConvert.DeserializeObject<List<AsignacionAutomaticaConfiguracionDTO>>(ConfiguracionesDB);
                return configuraciones;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
