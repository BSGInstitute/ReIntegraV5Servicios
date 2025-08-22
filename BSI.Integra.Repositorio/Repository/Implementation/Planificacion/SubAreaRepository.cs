using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SubAreaCapacitacionRepository
    /// Autor: Gretel Canasa.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_SubAreaCapacitacion
    /// </summary>
    public class SubAreaRepository : GenericRepository<TSubArea>, ISubAreaRepository
    {
        private Mapper _mapper;

        public SubAreaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubArea, SubArea>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: GreteL Canasa
        /// Fecha: 02/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de SubArea para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public async Task<IEnumerable<SubAreaDTO>> ObtenerSubAreaAsync()
        {
            try
            {
                var query = @"SELECT Id, IdArea, Nombre 
                FROM pla.T_SubArea WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<SubAreaDTO>>(resultado)!;
                return new List<SubAreaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroAsync(): {ex.Message}");
            }
        }
        /// Autor: GreteL Canasa
        /// Fecha: 02/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de SubArea para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id, Nombre FROM pla.T_SubArea WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroAsync(): {ex.Message}");
            }
        }
        
        /// Autor: GreteL Canasa
        /// Fecha: 02/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de SubArea para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerSubAreaPorIdAreaAsync(int idArea)
        {
            try
            {
                var query = @"SELECT Id, Nombre FROM pla.T_SubArea WHERE Estado=1 AND IdArea=@idArea";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new{idArea});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroAsync(): {ex.Message}");
            }
        }
    }
}
