using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadClasificacionOperacionesRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_OportunidadClasificacionOperaciones
    /// </summary>
    public class OportunidadClasificacionOperacionesRepository : GenericRepository<TOportunidadClasificacionOperacione>, IOportunidadClasificacionOperacionesRepository
    {
        private Mapper _mapper;

        public OportunidadClasificacionOperacionesRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadClasificacionOperacione, OportunidadClasificacionOperaciones>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_OportunidadClasificacionOperaciones asociados a un IdOportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public OportunidadClasificacionOperaciones? ObtenerPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var query = @"
                            SELECT  
                                Id, IdOportunidad, IdMatriculaCabecera, DiasAtrasoCuotaPago, CuotasAtrasoCuotaPago, MontoAtrasoCuotaPago, MonedaCuotaPago, IdAgendaTab, Estado, UsuarioCreacion,
                                UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion, DiasSeguimiento, DiasActividadesEjecutadas, UltimoContacto, ReproduccionVideoProgramado,
                                DiasSeguimiento7dias, DiasActividadesEjecutadas7dias, DiasSeguimiento14dias, DiasActividadesEjecutadas14dias, DiasSeguimiento21dias, DiasActividadesEjecutadas21dias,
                                DiasSeguimiento3014dias, DiasActividadesEjecutadas3014dias, DiasSeguimiento3021dias, DiasActividadesEjecutadas3021dias, DiasSeguimiento60dias, IdEstadoCompromiso, 
                                DiasSeguimiento90dias, DiasActividadesEjecutadas90dias, DiasAtrasoAvanceAcademico, EstadoAutoevaluaciones, FechaProximaCuota, DiasAtrasoAvanceAcademicoSinProyecto,
                                IdTarifario, PorcentajeAvanceAcademicosinProyecto, NotaPromedio, TieneProyectoFinal, PorcentajeAvanceAcademico, DiasDesdeUltimoAvance, AvanceReal, ValorAvanceReal,
                                AvanceProgramado, ValorAvanceProgramado, AvanceRealSesion, AvanceRealAutoevaluacion, ReproduccionVideoReal, AvanceProgramadoSesion, AvanceProgramadoAutoevaluacion,
                                FechaUltimaActividadEjecutada, DiasActividadesEjecutadas60dias   
                            FROM 
                                ope.T_OportunidadClasificacionOperaciones
                            WHERE 
                                Estado = 1 AND IdOportunidad = @IdOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<OportunidadClasificacionOperaciones>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta la oportunidad generada en la tabla ope.T_OportunidadClasificacionOperaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        public void CalcularPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var query = "ope.SP_CalcularOportunidadClasificacionOperaciones_PorIdOportunidad";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { idOportunidad = idOportunidad.ToString() });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
