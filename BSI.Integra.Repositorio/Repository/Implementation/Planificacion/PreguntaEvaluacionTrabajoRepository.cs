using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PreguntaEvaluacionTrabajoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 17/07/2023
    /// <summary>
    /// Gestión general de T_PreguntaEvaluacionTrabajo
    /// </summary>
    public class PreguntaEvaluacionTrabajoRepository : GenericRepository<TPreguntaEvaluacionTrabajo>, IPreguntaEvaluacionTrabajoRepository
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaEvaluacionTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaEvaluacionTrabajo, PreguntaEvaluacionTrabajo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 17/07/2023
        /// <param name="idConfigurarEvaluacionTrabajo"> (PK) de T_ConfigurarEvaluacionTrabajo </param>
        /// <summary>
        /// Obtiene los registros relacionados al IdConfigurarEvaluacionTrabajo
        /// </summary>
        /// <return> IEnumerable<PreguntaEvaluacionTrabajo> </return>
        public IEnumerable<PreguntaEvaluacionTrabajo> ObtenerPorIdConfigurarEvaluacionTrabajo(int idConfigurarEvaluacionTrabajo)
        {
            try
            {
                var _queryfiltrocapitulo = @"SELECT Id,
                                                   IdConfigurarEvaluacionTrabajo,
                                                   IdPregunta,
                                                   Estado,
                                                   FechaCreacion,
                                                   FechaModificacion,
                                                   UsuarioCreacion,
                                                   UsuarioModificacion,
                                                   RowVersion,
                                                   IdMigracion
                                            FROM pla.T_PreguntaEvaluacionTrabajo
                                            WHERE Estado = 1
                                                  AND IdConfigurarEvaluacionTrabajo = @IdConfigurarEvaluacionTrabajo;";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdConfigurarEvaluacionTrabajo = idConfigurarEvaluacionTrabajo });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaEvaluacionTrabajo>>(SubfiltroCapitulo);
                return new List<PreguntaEvaluacionTrabajo>();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
    }
}
