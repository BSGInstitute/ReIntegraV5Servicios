using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstadoSolicitudBeneficioRepository
    /// Autor: Jorge Gamero
    /// Fecha: 02/09/2024
    /// <summary>
    /// Gestión general de T_EstadoSolicitudBeneficio
    /// </summary>
    public class EstadoSolicitudBeneficioRepository : GenericRepository<TEstadoSolicitudBeneficio>, IEstadoSolicitudBeneficioRepository
    {
        private Mapper _mapper;

        public EstadoSolicitudBeneficioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoSolicitudBeneficio, EstadoSolicitudBeneficio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 02/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM pla.T_EstadoSolicitudBeneficio WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
