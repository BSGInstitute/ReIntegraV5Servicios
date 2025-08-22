using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DepartamentoPaiRepository
    /// Autor: Jorge Gamero
    /// Fecha: 19/09/2024
    /// <summary>
    /// Gestión general de T_DepartamentoPais
    /// </summary>
    public class DepartamentoPaiRepository : GenericRepository<TDepartamentoPai>, IDepartamentoPaiRepository
    {
        private Mapper _mapper;

        public DepartamentoPaiRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDepartamentoPai, DepartamentoPai>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 19/07/2024
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
                var query = @"SELECT Id, Nombre FROM conf.T_DepartamentoPais WHERE Estado = 1 ORDER BY Nombre";
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

        /// Autor: Jorge Gamero
        /// Fecha: 19/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene codigo de departamento por Id
        /// </summary> 
        /// <returns> CodigoDepartamentoDTO </returns>
        public CodigoDepartamentoDTO ObtenerCodigoPorId(int id)
        {
            try
            {
                var codigo = new CodigoDepartamentoDTO();
                var query = @"SELECT Codigo FROM conf.T_DepartamentoPais WHERE ID = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    codigo = JsonConvert.DeserializeObject<CodigoDepartamentoDTO>(resultado);
                }
                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
