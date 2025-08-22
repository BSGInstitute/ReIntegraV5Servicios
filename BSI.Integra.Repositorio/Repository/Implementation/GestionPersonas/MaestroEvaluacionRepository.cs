using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class MaestroEvaluacionRepository : GenericRepository<TPuestoTrabajoRemuneracion>, IMaestroEvaluacionRepository
    {
        private Mapper _mapper;
        public MaestroEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoRemuneracion, GestionRemuneracionPuestoTrabajoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoRemuneracion, TPuestoTrabajoRemuneracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor:  Sergio Yepez Pillco.
        /// Fecha: 02/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public List<ExamenTestDTO> ObtenerExamenTest()
        {
            try
            {
                List<ExamenTestDTO> EvaluacionPersona = new List<ExamenTestDTO>();
                var _query = "SELECT * FROM  gp.T_ExamenTest WHERE Estado = 1 ORDER BY id DESC";
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionPersona = JsonConvert.DeserializeObject<List<ExamenTestDTO>>(dataDB);
                }
                return EvaluacionPersona;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor:  Sergio Yepez Pillco.
        /// Fecha: 07/01/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de V_ObtenerEvaluacionAgrupadaExamen relacionados a un idEvaluacion.
        /// </summary>
        /// <returns> List<EvaluacionAgrupadaComponenteDTO> </returns>
        public List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int IdEvaluacion)
        {
            try
            {
                List<EvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<EvaluacionAgrupadaComponenteDTO>();
                var campos = "IdAsignacionPreguntaExamen,IdComponente,NombreComponente,IdGrupoComponenteEvaluacion,NombreGrupoComponenteEvaluacion,IdEvaluacion,NombreEvaluacion,IdPregunta,EnunciadoPregunta,NroOrden ";

                var _query = "SELECT " + campos + " FROM  gp.V_ObtenerEvaluacionAgrupadaExamen where IdEvaluacion=" + IdEvaluacion + " order by IdEvaluacion, IdGrupoComponenteEvaluacion,IdComponente,NroOrden";
                var dataDB = _dapperRepository.QueryDapper(_query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<EvaluacionAgrupadaComponenteDTO>>(dataDB);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
