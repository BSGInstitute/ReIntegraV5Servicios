using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ParametroEvaluacionRepository
    /// Autor: Gilmer Qm
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_ParametroEvaluacion
    /// </summary>
    public class ParametroEvaluacionRepository : GenericRepository<TParametroEvaluacion>, IParametroEvaluacionRepository
    {
        private Mapper _mapper;
        public ParametroEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TParametroEvaluacion, ParametroEvaluacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros por el (FK) IdCriterioEvaluacion
        /// </summary>
        /// <param name="idCriterioEvaluacion"> PK de T_CriterioEvaluacion </param> 
        /// <returns> List<ParametroEvaluacionDTO> </returns>  
        public List<ParametroEvaluacion> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdCriterioEvaluacion,
                                       IdEscalaCalificacion,
                                       Nombre,
                                       Ponderacion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                            FROM pla.T_ParametroEvaluacion
                            WHERE Estado = 1
                                  AND IdCriterioEvaluacion = @IdCriterioEvaluacion ORDER BY Id desc;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCriterioEvaluacion = idCriterioEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ParametroEvaluacion>>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
