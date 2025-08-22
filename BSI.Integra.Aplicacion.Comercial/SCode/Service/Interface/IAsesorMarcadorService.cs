using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAsesorMarcadorService
    {

        IEnumerable<AsesorMarcadorDTO> Obtener();
        AsesorMarcadorDTO Actualizar(AsesorMarcadorDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        List<ReporteFinalMarcadorDTO> ObtenerReporteAsesorMarcadorAutomatico(FiltroReporteAsesorMarcadorDTO filtro);
        List<ReporteFinalPromedioDTO> ObtenerReporteAsesorTiempoPromedio(FiltroReporteAsesorMarcadorDTO filtro);
    }
}
