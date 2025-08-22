using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: BloqueHorarioRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/10/2022
    /// <summary>
    /// Gestión general de TBloqueHorario
    /// </summary>
    public class BloqueHorarioRepository : GenericRepository<TBloqueHorario>, IBloqueHorarioRepository
    {
        private Mapper _mapper;
        public BloqueHorarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TBloqueHorario, BloqueHorario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de TBloqueHorario.
        /// </summary>
        /// <returns> List<BloqueHorarioProcesarBicDTO> </returns>
        public IEnumerable<BloqueHorarioProcesarBicDTO> ObtenerCombo()
        {
            try
            {
                var lista = new List<BloqueHorarioProcesarBicDTO>();
                string _query = @"SELECT Id,Nombre,Descripcion,HoraInicio,HoraFin,IdConfiguracionBIC 
                                    FROM mkt.T_BloqueHorario 
                                    WHERE Estado=1";
                var queryRespuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<BloqueHorarioProcesarBicDTO>>(queryRespuesta);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 02/22/22
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuracion de un bloque horario, filtrado por dia
        /// </summary>
        /// <param name="dia">Cadena con el nombre del dia</param>
        /// <returns>Objeto de clase BloqueHorarioProcesaOportunidadBO</returns>
        public BloqueHorarioProcesaOportunidad ObtenerConfiguracion(string dia)
        {
            BloqueHorarioProcesaOportunidad bloqueHorarioProcesarOportunidad = new BloqueHorarioProcesaOportunidad();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene el bloque horario de la configuracion bic
        /// </summary>
        /// <returns></returns>
        public List<BloqueHorarioProcesarBicDTO> ObtenerPorIdConfiguracionBic(int idConfiguracionBic)
        {
            try
            {
                List<BloqueHorarioProcesarBicDTO> rpta = new List<BloqueHorarioProcesarBicDTO>();
                string query = "SELECT Nombre, HoraInicio, HoraFin FROM mkt.V_ObtenerBloqueHorario WHERE IdConfiguracionBIC=@idConfiguracionBic";
                var resultado = _dapperRepository.QueryDapper(query, new { idConfiguracionBic });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BloqueHorarioProcesarBicDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
