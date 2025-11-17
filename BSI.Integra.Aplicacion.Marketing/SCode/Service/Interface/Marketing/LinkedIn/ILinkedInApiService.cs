using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.LinkedIn
{
    public interface ILinkedInApiService
    {
        Task<bool> ObtenerDatos();
        Task<bool> ObtenerForms();
        Task<bool> ObtenerGrupoCampañas();
        Task<bool> ObtenerCampañas();
        object GenerarOportunindadesLead();
        IEnumerable<ReporteLeadsDTO> ObtenerReporteLeads();
        IEnumerable<ReporteLeadsDTO> ObtenerReporteLeadsByFecha(FiltroLandingPagePortaLinkedInDTO filtro);
        IEnumerable<ReporteLeadsPendientesDTO> ObtenerReportePendientes(int cuentaAsociada);
        bool Actualizar(LinkedInActualizarDTO dto, string usuario);
        bool SubirOportunidadesPendientes(string usuario);
        bool SubirOportunidadesPendientesSeleccionadas(List<string> guidLinkedinLead, string usuario);
        BoolDTO ValidarCreacionOportunidadLinkedinEstado();
        BoolDTO ValidarEstadoParaControlLinkedin();
    }
}
