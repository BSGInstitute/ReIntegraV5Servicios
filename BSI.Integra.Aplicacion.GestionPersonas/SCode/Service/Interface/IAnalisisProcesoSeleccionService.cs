using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IAnalisisProcesoSeleccionService
    {
        ReportePrincipalAnalisisProcesoSeleccionDTO GenerarReporte(FiltroAnalisisProcesoSeleccionDTO Filtro);
        ReportePrincipalAnalisisProcesoSeleccionDTO GenerarReporte_V2(FiltroAnalisisProcesoSeleccionDTO Filtro);
        ReporteFiltroAnalisisDTO ObtenerComboFiltro();
    }
}
