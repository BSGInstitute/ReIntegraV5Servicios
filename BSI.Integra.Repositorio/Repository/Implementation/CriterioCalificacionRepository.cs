using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CriterioCalificacionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 05/10/2022
    /// <summary>
    /// Gestión general de T_CriterioCalificacion
    /// </summary>
    public class CriterioCalificacionRepository : GenericRepository<TCriterioCalificacion>, ICriterioCalificacionRepository
    {
        private Mapper _mapper;

        public CriterioCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioCalificacion, CriterioCalificacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 05/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros Id y Sigla de T_CriterioCalificacion donde el Estado está activo.
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> combo = new List<ComboDTO>();
                var query = "SELECT Id, Sigla AS Nombre FROM fin.T_CriterioCalificacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
                {
                    combo = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return combo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
