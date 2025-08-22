using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoPersonaRepository
    /// Autor: Gilmer Qm
    /// Fecha: 26/05/2023
    /// <summary>
    /// Gestión general de T_TipoPersona
    /// </summary>
    /// </summary>
    public class TipoPersonaRepository : GenericRepository<TTipoPersona>, ITipoPersonaRepository
    {
        public TipoPersonaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoPersona para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id AS Id,
                               Nombre AS Nombre 
                        FROM [conf].[T_TipoPersona]
                        WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoPersona para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public async Task<IEnumerable<ComboDTO>> ObtenerComboPorIdsAsync(IEnumerable<int> ids)
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id AS Id,
                               Nombre AS Nombre 
                        FROM [conf].[T_TipoPersona]
                        WHERE Estado = 1 AND Id IN @ids";
                var resultado = await _dapperRepository.QueryDapperAsync(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
    }
}
