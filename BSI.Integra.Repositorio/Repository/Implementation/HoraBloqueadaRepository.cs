using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: HoraBloqueadaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_HoraBloqueada
    /// </summary>
    public class HoraBloqueadaRepository : GenericRepository<THoraBloqueadum>, IHoraBloqueadaRepository
    {
        private Mapper _mapper;

        public HoraBloqueadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<THoraBloqueadum, HoraBloqueada>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private THoraBloqueadum MapeoEntidad(HoraBloqueada entidad)
        {
            try
            {
                //crea la entidad padre
                THoraBloqueadum modelo = _mapper.Map<THoraBloqueadum>(entidad);

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

        public THoraBloqueadum Add(HoraBloqueada entidad)
        {
            try
            {
                var HoraBloqueada = MapeoEntidad(entidad);
                base.Insert(HoraBloqueada);
                return HoraBloqueada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public THoraBloqueadum AddAsync(HoraBloqueada entidad)
        {
            try
            {
                var HoraBloqueada = MapeoEntidad(entidad);
                base.InsertAsync(HoraBloqueada);
                return HoraBloqueada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public THoraBloqueadum Update(HoraBloqueada entidad)
        {
            try
            {
                var HoraBloqueada = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                HoraBloqueada.RowVersion = entidadExistente.RowVersion;

                base.Update(HoraBloqueada);
                return HoraBloqueada;
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


        public IEnumerable<THoraBloqueadum> Add(IEnumerable<HoraBloqueada> listadoEntidad)
        {
            try
            {
                List<THoraBloqueadum> listado = new List<THoraBloqueadum>();
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

        public IEnumerable<THoraBloqueadum> Update(IEnumerable<HoraBloqueada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<THoraBloqueadum> listado = new List<THoraBloqueadum>();
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_HoraBloqueada.
        /// </summary>
        /// <returns> List<HoraBloqueadaDTO> </returns>
        public IEnumerable<HoraBloqueadaDTO> ObtenerHoraBloqueada()
        {
            try
            {
                List<HoraBloqueadaDTO> rpta = new List<HoraBloqueadaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPersonal,
	                    Fecha,
	                    Hora,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_HoraBloqueada
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<HoraBloqueadaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas la horas bloquedas que el asesor puede tener durante el dia para la programaciond e actividades
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="fecha"></param>
        /// <returns> List<HoraBloqueadaDTO> </returns>
        public List<HoraBloqueadaRADTO> ObtenerHorasBloquedasReprogramacionPorAsesor(int idPersonal, DateTime fecha)
        {
            try
            {
                DateTime fechatemp = new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
                List<HoraBloqueadaRADTO> rpta = new List<HoraBloqueadaRADTO>();
                var query = @"
                    SELECT Fecha,Hora
                    FROM com.V_THoraBloqueada_FechaProgramacionAutomatica
                    WHERE Fecha = @fechatemp AND IdPersonal = @idPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { fechatemp, idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<HoraBloqueadaRADTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ReprogramarAlumnoClasesOnline(int idAlumno)
        {
            try
            {
                var query = @"UPDATE ope.T_CalculoSesionesOnline SET Estado=0 WHERE IdAlumno=@idAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { idAlumno });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
