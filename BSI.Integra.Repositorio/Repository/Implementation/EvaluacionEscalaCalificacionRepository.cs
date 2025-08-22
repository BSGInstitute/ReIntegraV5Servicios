using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EvaluacionEscalaCalificacionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_EvaluacionEscalaCalificacion
    /// </summary>
    public class EvaluacionEscalaCalificacionRepository : GenericRepository<TEvaluacionEscalaCalificacion>, IEvaluacionEscalaCalificacionRepository
    {
        private Mapper _mapper;

        public EvaluacionEscalaCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEvaluacionEscalaCalificacion, EvaluacionEscalaCalificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de evaluacion escala calificacion filtrado por el IdModalidadCurso
        /// </summary>
        /// <param name="idModalidadCurso"> Id de Modalidad curso </param>
        /// <returns> List<EvaluacionEscalaCalificacion> </returns>
        public List<EvaluacionEscalaCalificacion> ObtenerPorModalidadCurso(int idModalidadCurso)
        {
            try
            {
                var query = @"SELECT Id,IdModalidadCurso,CodigoCiudad,EscalaCalificacion,NotaAprobatoria,RedondeoDecimales,EscalaTexto,NotaAprobatoriaTexto,
		                             Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,RowVersion,IdMigracion 
	                          FROM ope.T_EvaluacionEscalaCalificacion WHERE Estado = 1 AND IdModalidadCurso= @IdModalidadCurso";
                var res = _dapperRepository.QueryDapper(query, new { IdModalidadCurso = idModalidadCurso });
                return JsonConvert.DeserializeObject<List<EvaluacionEscalaCalificacion>>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
