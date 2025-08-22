using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: QuejaSugerenciasRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 20/07/2023
    /// <summary>
    /// Gestión general de Reportes Queja Sugerencias
    /// </summary>
    public class QuejaSugerenciaRepository : IQuejaSugerenciaRepository
    {
        private IDapperRepository _dapperRepository;
        public QuejaSugerenciaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Tipo Función: POST
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de quejas y sugerencias segun el filtro ingresado
        /// </summary>
        /// <param name="fechainicio"> Fecha creacion de [integraDB_PortalWeb].[dbo].[T_PortalQuejaSugerencia] </param>
        /// <param name="fechafin"> Fecha creacion de [integraDB_PortalWeb].[dbo].[T_PortalQuejaSugerencia] </param>
        /// <param name="areas"> (PK's) de T_AreaCapacitacion  </param>
        /// <param name="subareas"> (PK's) de  T_SubAreaCapacitacion   </param>
        /// <param name="programageneral"> (PK's) de T_PGeneral </param>
        /// <param name="tipo"> (PK's) de [integraDB_PortalWeb].[dbo].[T_TipoQuejaSugerencia] </param>
        /// <returns>Lista del reporte quejas y sugerencias en un List<QuejaSugerenciaDTO></returns>
        public IEnumerable<QuejaSugerenciaDTO> GenerarReporteQuejaSugerencia(DateTime fechainicio, DateTime fechafin, string areas, string subareas, string programageneral, string tipo)
        {
            try
            {
                var query = _dapperRepository.QuerySPDapper("[mkt].[SP_ReporteQuejaSugerencia]", new { fechainicio, fechafin, areas, subareas, programageneral, tipo });
                if (!string.IsNullOrEmpty(query) && !query.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<QuejaSugerenciaDTO>>(query);
                return new List<QuejaSugerenciaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en GenerarReporteQuejaSugerencia: {ex.Message}", ex);
            }
        }
    }
}
