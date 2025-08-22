using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    public class ReporteEgresoPorRubroService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReporteEgresoPorRubroService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<ReporteEgresoPorRubroDTO> VizualizarReporteEgresoPorRubro(ReporteEgresoPorRubroSedesAnioDTO Filtro)
        {
            try
            {

                ComprobantePagoPorFurService _repoComprobantePagoPorFur = new ComprobantePagoPorFurService(unitOfWork);
                var ListaDesdeDB = _repoComprobantePagoPorFur.ObtenerDatosReporteEgresosPorRubro(Filtro.IdEmpresa, Filtro.FechaInicio, Filtro.FechaFin).OrderByDescending(x => x.IdRubro).ToList();
                return ListaDesdeDB;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<DesgloseReporteEgresoPorRubroDTO> VizualizarDesgloseReporteEgresoPorRubro(FiltroDesgloseReporteEgresoPorRubroDTO Filtro)
        {
            try
            {
                ComprobantePagoPorFurService _repoComprobantePagoPorFur = new ComprobantePagoPorFurService(unitOfWork);
                var ListaDesdeDB = _repoComprobantePagoPorFur.ObtenerDesgloceReporteEgresosPorRubro(Filtro.IdEmpresa, Filtro.FechaInicio, Filtro.FechaFin,Filtro.IdRubro);
                return ListaDesdeDB;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void CopiarValores(ReporteEgresoPorRubroDTO origen, ReporteEgresoPorRubroDTO destino)
        {
            destino.Enero = origen.Enero;
            destino.Febrero = origen.Febrero;
            destino.Marzo = origen.Marzo;
            destino.Abril = origen.Abril;
            destino.Mayo = origen.Mayo;
            destino.Junio = origen.Junio;
            destino.Julio = origen.Julio;
            destino.Agosto = origen.Agosto;
            destino.Septiembre = origen.Septiembre;
            destino.Octubre = origen.Octubre;
            destino.Noviembre = origen.Noviembre;
            destino.Diciembre = origen.Diciembre;
            destino.IdRubro = origen.IdRubro;
            destino.IdSede = origen.IdSede;
            destino.EmpresaSede = origen.EmpresaSede;
            destino.Total = origen.Total;
        }
    }
}
