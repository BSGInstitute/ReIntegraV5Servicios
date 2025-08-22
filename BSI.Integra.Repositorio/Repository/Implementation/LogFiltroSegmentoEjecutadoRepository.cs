using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: LogFiltroSegmentoEjecutadoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_LogFiltroSegmentoEjecutado
    /// </summary>
    public class LogFiltroSegmentoEjecutadoRepository : GenericRepository<TLogFiltroSegmentoEjecutado>, ILogFiltroSegmentoEjecutadoRepository
    {
        private Mapper _mapper;

        public LogFiltroSegmentoEjecutadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLogFiltroSegmentoEjecutado, LogFiltroSegmentoEjecutado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TLogFiltroSegmentoEjecutado MapeoEntidad(LogFiltroSegmentoEjecutado entidad)
        {
            try
            {
                //crea la entidad padre
                TLogFiltroSegmentoEjecutado modelo = _mapper.Map<TLogFiltroSegmentoEjecutado>(entidad);

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


        public TLogFiltroSegmentoEjecutado Add(LogFiltroSegmentoEjecutado entidad)
        {
            try
            {
                var LogFiltroSegmentoEjecutado = MapeoEntidad(entidad);
                base.Insert(LogFiltroSegmentoEjecutado);
                return LogFiltroSegmentoEjecutado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
  
        #endregion
        public List<LogFiltroSegmentoEjecutadoDTO> ObtenerPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<LogFiltroSegmentoEjecutadoDTO> listadoFiltroSegmentoEjecutado = new List<LogFiltroSegmentoEjecutadoDTO>();
                var query = "SELECT Id, NombreCentroCosto, NombreOrigen, NombreTipoDato, NombreFaseOportunidad, TotalOportunidadesCreadas, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion FROM mkt.V_ObtenerLogFiltroSegmentoEjecutado WHERE IdFiltroSegmento = @idFiltroSegmento AND EstadoLogFiltroSegmentoEjecutado = 1;";
                var listadoFiltroSegmentoEjecutadoDB = _dapperRepository.QueryDapper(query, new { idFiltroSegmento });
                if (!string.IsNullOrEmpty(listadoFiltroSegmentoEjecutadoDB) && !listadoFiltroSegmentoEjecutadoDB.Contains("[]"))
                {
                    listadoFiltroSegmentoEjecutado = JsonConvert.DeserializeObject<List<LogFiltroSegmentoEjecutadoDTO>>(listadoFiltroSegmentoEjecutadoDB);
                }
                return listadoFiltroSegmentoEjecutado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
