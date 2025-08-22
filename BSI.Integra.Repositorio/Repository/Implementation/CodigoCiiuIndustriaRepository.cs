using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CodigoCiiuIndustriaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión general de T_CodigoCiiuIndustria
    /// </summary>
    public class CodigoCiiuIndustriaRepository : GenericRepository<TCodigoCiiuIndustrium>, ICodigoCiiuIndustriaRepository
    {
        private Mapper _mapper;

        public CodigoCiiuIndustriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCodigoCiiuIndustrium, CodigoCiiuIndustria>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CodigoCiiuIndustria con el estado=1.
        /// </summary> 
        /// <returns> CodigoCiiuIndustria </returns>
        public CodigoCiiuIndustria ObtenerPorId(int id)
        {
            try
            {
                var rpta = new CodigoCiiuIndustria();
                var query = @" SELECT Id,
                                       CIIU,
                                       Nombre,
                                       IdIndustria,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion 
                                FROM pla.T_CodigoCiiuIndustria WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<CodigoCiiuIndustria>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CodigoCiiuIndustria con el estado=1 y filtro de Nombre
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerPorNombre(string filtro)
        {
            try
            {
                var rpta = new List<ComboDTO>();
                var query = @" SELECT Id, Nombre  
                                FROM pla.T_CodigoCiiuIndustria 
                                WHERE Estado =1 AND Nombre LIKE '%" + filtro + "%'";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
