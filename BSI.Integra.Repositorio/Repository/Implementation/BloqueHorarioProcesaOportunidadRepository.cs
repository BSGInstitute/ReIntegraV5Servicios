using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: BloqueHorarioProcesaOportunidadRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_BloqueHorarioProcesaOportunidad
    /// </summary>
    public class BloqueHorarioProcesaOportunidadRepository : GenericRepository<TBloqueHorarioProcesaOportunidad>, IBloqueHorarioProcesaOportunidadRepository
    {
        private Mapper _mapper;

        public BloqueHorarioProcesaOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBloqueHorarioProcesaOportunidad, BloqueHorarioProcesaOportunidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TBloqueHorarioProcesaOportunidad MapeoEntidad(BloqueHorarioProcesaOportunidad entidad)
        {
            try
            {
                //crea la entidad padre
                TBloqueHorarioProcesaOportunidad modelo = _mapper.Map<TBloqueHorarioProcesaOportunidad>(entidad);

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

        public TBloqueHorarioProcesaOportunidad Add(BloqueHorarioProcesaOportunidad entidad)
        {
            try
            {
                var BloqueHorarioProcesaOportunidad = MapeoEntidad(entidad);
                base.Insert(BloqueHorarioProcesaOportunidad);
                return BloqueHorarioProcesaOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TBloqueHorarioProcesaOportunidad Update(BloqueHorarioProcesaOportunidad entidad)
        {
            try
            {
                var BloqueHorarioProcesaOportunidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                BloqueHorarioProcesaOportunidad.RowVersion = entidadExistente.RowVersion;

                base.Update(BloqueHorarioProcesaOportunidad);
                return BloqueHorarioProcesaOportunidad;
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


        public IEnumerable<TBloqueHorarioProcesaOportunidad> Add(IEnumerable<BloqueHorarioProcesaOportunidad> listadoEntidad)
        {
            try
            {
                List<TBloqueHorarioProcesaOportunidad> listado = new List<TBloqueHorarioProcesaOportunidad>();
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

        public IEnumerable<TBloqueHorarioProcesaOportunidad> Update(IEnumerable<BloqueHorarioProcesaOportunidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TBloqueHorarioProcesaOportunidad> listado = new List<TBloqueHorarioProcesaOportunidad>();
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

        /// Autor:Margiory Ramirez Neyra
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// Obtiene la configuracion de un bloque horario, filtrado por dia
        /// </summary>
        /// <param name="dia">Cadena con el nombre del dia</param>
        /// <returns>Objeto de clase BloqueHorarioProcesaOportunidadBO</returns>
        public BloqueHorarioProcesaOportunidad ObtenerConfiguracionPorDia(string dia)
        {
            var bloqueHorarioProcesarOportunidad = new BloqueHorarioProcesaOportunidad();
            try
            {
                var query = @"
                            SELECT
	                            Id,
	                            Activo,
	                            Prelanzamiento,
	                            Descripcion,
	                            Sede,
	                            Dia,
	                            TurnoM,
	                            HoraInicioM,
	                            HoraFinM,
	                            TurnoT,
	                            HoraInicioT,
	                            HoraFinT,
	                            ProbabilidadOportunidad
                            FROM mkt.V_ObtenerTodoBloqueHorarioProcesaOportunidad
                            WHERE Estado = 1 AND Dia = @dia;";
                var bloqueHorarioProcesarOportunidadDb = _dapperRepository.FirstOrDefault(query, new { dia });
                if (!string.IsNullOrEmpty(bloqueHorarioProcesarOportunidadDb) && bloqueHorarioProcesarOportunidadDb != "null")
                {
                    bloqueHorarioProcesarOportunidad = JsonConvert.DeserializeObject<BloqueHorarioProcesaOportunidad>(bloqueHorarioProcesarOportunidadDb)!;
                }
                return bloqueHorarioProcesarOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene la configuracion de un bloque horario, filtrado por dia
        /// </summary>
        /// <param name="dia">Cadena con el nombre del dia</param>
        /// <returns>Objeto de clase BloqueHorarioProcesaOportunidadBO</returns>
        public BloqueHorarioProcesaOportunidad ObtenerConfiguracion(string dia)
        {
            var bloqueHorarioProcesarOportunidad = new BloqueHorarioProcesaOportunidad();
            try
            {
                var query = "SELECT Id, Activo, Prelanzamiento, Descripcion, Sede, Dia, TurnoM, HoraInicioM, HoraFinM, TurnoT, HoraInicioT, HoraFinT, ProbabilidadOportunidad FROM mkt.V_ObtenerTodoBloqueHorarioProcesaOportunidad WHERE Estado = 1 AND Dia = @dia";
                var bloqueHorarioProcesarOportunidadDb = _dapperRepository.FirstOrDefault(query, new { dia });
                bloqueHorarioProcesarOportunidad = JsonConvert.DeserializeObject<BloqueHorarioProcesaOportunidad>(bloqueHorarioProcesarOportunidadDb);
                return bloqueHorarioProcesarOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
