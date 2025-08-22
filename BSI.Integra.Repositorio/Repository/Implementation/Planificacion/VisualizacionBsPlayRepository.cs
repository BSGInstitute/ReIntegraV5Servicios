using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: VisualizacionBsPlayRepository
    /// Autor: Gilmer Qm
    /// Fecha: 07/06/2023
    /// <summary>
    /// Gestión general de T_VisualizacionBsPlay
    /// </summary>
    public class VisualizacionBsPlayRepository : GenericRepository<TVisualizacionBsPlay>, IVisualizacionBsPlayRepository
    {
        private Mapper _mapper;
        public VisualizacionBsPlayRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TVisualizacionBsPlay, VisualizacionBsPlay>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de T_VisualizacionBsPlay
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>  
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre
                            FROM [pla].[T_VisualizacionBsPlay]
                            WHERE Estado = 1;";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
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
