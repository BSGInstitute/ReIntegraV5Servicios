using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ReporteProblemasAulaVirtualRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 24/04/2023
    /// <summary>
    /// Gestión general de Reporte Problemas del Aula Virtual
    /// </summary>
    public class ReporteProblemasAulaVirtualRepository : IReporteProblemasAulaVirtualRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteProblemasAulaVirtualRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reporte completo de Problemas Aula Virtual
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns> Lista DTO - List<ReporteProblemasAulaVirtualResultadoDTO> - reporte </returns>
        public IEnumerable<ReporteProblemasAulaVirtualResultadoDTO> ObtenerReporteProblemasAulaVirtual(ReporteProblemasAulaVirtualFiltroDTO filtro)
        {
            try
            {
                IEnumerable<ReporteProblemasAulaVirtualResultadoDTO> reporte = new List<ReporteProblemasAulaVirtualResultadoDTO>();

                string coordinadores = null;
                string centroCosto = null;
                string tipoCategoriaError = null;
                string matriculaCabecera = null;

                if (filtro.IdsCoordinadores != null && filtro.IdsCoordinadores.Count() > 0)
                    coordinadores = String.Join(",", filtro.IdsCoordinadores);

                if (filtro.IdsCentrosCosto != null && filtro.IdsCentrosCosto.Count() > 0)
                    centroCosto = String.Join(",", filtro.IdsCentrosCosto);

                if (filtro.IdsTiposCategoriaError != null && filtro.IdsTiposCategoriaError.Count() > 0)
                    tipoCategoriaError = String.Join(",", filtro.IdsTiposCategoriaError);

                if (filtro.IdsMatriculasCabecera != null && filtro.IdsMatriculasCabecera.Count() > 0)
                    matriculaCabecera = String.Join(",", filtro.IdsMatriculasCabecera);

                var query = _dapperRepository.QuerySPDapper("pla.SP_ReporteProblemasAulaVirtual", new
                {
                    Coordinador = coordinadores,
                    CentroCosto = centroCosto,
                    MatriculaCabecera = matriculaCabecera,
                    TipoCategoriaError = tipoCategoriaError,
                    filtro.FechaInicio,
                    filtro.FechaFin
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporte = JsonConvert.DeserializeObject<IEnumerable<ReporteProblemasAulaVirtualResultadoDTO>>(query)!;
                }
                return reporte;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerReporteProblemasAulaVirtual()", ex);
            }
        }
    }
}
