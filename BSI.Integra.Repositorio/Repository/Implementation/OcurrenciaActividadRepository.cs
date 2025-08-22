using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OcurrenciaActividadRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 15/12/2022
    /// <summary>
    /// Gestión general de T_OcurrenciaActividad
    /// </summary>
    public class OcurrenciaActividadRepository : GenericRepository<TOcurrenciaActividad>, IOcurrenciaActividadRepository
    {
        private Mapper _mapper;

        public OcurrenciaActividadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOcurrenciaActividad, OcurrenciaActividad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de Ocurrencias de acuerdo al IdActividadCabecera y IdOcurrenciaPadre de la actividad 
        /// abierta por el Asesor
        /// </summary>
        /// <param name="ocurrenciaActividad"></param>
        public List<ArbolOcurrenciaDTO> ObtenerArbolOcurrencia(int idActividadCabecera, int idOcurrenciaActividadPadre)
        {
            try
            {
                string query = @"
                                SELECT 
                                    IdOcurrenciaActividad, IdOcurrenciaReporte, RequiereLlamada, EstadoOcurrencia, NombreOcurrencia, Color, Roles, Nivel, TieneOcurrencias, 
                                    TieneActividades, IdFaseOportunidad, IdOcurrenciaActividad_Padre, FechaCreacion, IdPlantilla_Speech, NombreEstadoOcurrencia, 
                                    CrearOportunidad, FaseSiguiente, IdPlantillaWP, IdPlantillaCE 
                                FROM 
                                    com.V_HojaGetArbolDeOcurrencias 
                                WHERE 
                                    IdActividadCabecera = @IdActividadCabecera AND IdOcurrenciaActividad_Padre = @IdOcurrenciaPadre AND EstadoOa = 1 AND EstadoOc = 1";
                string queryArbolOcurrencia = _dapperRepository.QueryDapper(query, new { @IdActividadCabecera = idActividadCabecera, IdOcurrenciaPadre = idOcurrenciaActividadPadre });
                List<ArbolOcurrenciaDTO> listaArbolOcurrencia = JsonConvert.DeserializeObject<List<ArbolOcurrenciaDTO>>(queryArbolOcurrencia)!;
                return listaArbolOcurrencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
