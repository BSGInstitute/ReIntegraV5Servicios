using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class EstructuraEspecificaTareaRepository : GenericRepository<TEstructuraEspecificaTarea>, IEstructuraEspecificaTareaRepository
    {
        private Mapper _mapper;
        public EstructuraEspecificaTareaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstructuraEspecificaTarea, EstructuraEspecificaTarea>(MemberList.None).ReverseMap();
                cfg.CreateMap<EstructuraEspecificaTareaDTO, EstructuraEspecificaTarea>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Repositorio: TareaEvaluacionTareaRepositorioAula
        /// Autor: MiguelMora
        /// Fecha: 05/09/2022
        /// <summary>
        /// Obtiene el estado del envio las preguntas reflexivas de por matricula
        /// </summary>
        /// <returns>  </returns>  
        public bool EstadoEnvioAprendizajeReflexivo(int IdMatriculaCabecera, int IdPEspecifico, int IdTareaEvaluacionTarea)
        {
            try
            {
                var _query = string.Empty;
                Aplicacion.DTO.BoolDTO lista = new Aplicacion.DTO.BoolDTO();

                _query = "[pw].[SP_PW_EstadoAprendizajeReflexivo]";
                var repuesta = _dapperRepository.QuerySPFirstOrDefault(_query, new { IdMatriculaCabecera = IdMatriculaCabecera, IdPEspecifico = IdPEspecifico, IdTareaEvaluacionTarea = IdTareaEvaluacionTarea });

                if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]") && repuesta != "null")
                {
                    lista = JsonConvert.DeserializeObject<Aplicacion.DTO.BoolDTO>(repuesta);
                    return lista.Valor.Value;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

    }
}
