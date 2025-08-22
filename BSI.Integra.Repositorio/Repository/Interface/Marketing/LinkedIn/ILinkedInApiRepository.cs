using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.LinkedIn
{
    public interface ILinkedInApiRepository
    {
        IntDTO? ObtenerPorIdForm(int? IdLinkedInForm);
        IntDTO? ObtenerPorIdGrupoCampaign(long? idLinkedInGroupCampaign);
        IntDTO? ObtenerPorIdCampaign(long? idLinkedInCampaign);
        StringDTO? ObtenerPorIdLead(string? idLinkedInLead);
        void InsertarObjetoSerializadoForm(FormLinkedinDTO entidad);
        void InsertarObjetoSerializadoQuestionForm(QuestionFormDTO entidad);
        void InsertarObjetoSerializadoGrupoCampaign(GroupCampaignLinkedInDTO entidad);
        void InsertarObjetoSerializadoCampaign(CampaignLinkedInDTO entidad);
        void InsertarObjetoSerializadoQuestionLead(QuestionLeadDTO entidad);
        void InsertarObjetoSerializadoLeadLinkedIn(LinkedInLeadApiDTO entidad);
        List<IntDTO> BusquedaDeGruposCampaign();
        List<IntDTO> BusquedaDeForms();
        List<StringDTO> BuscarLeadsSinOportunindades();
        public StringDTO ObtenerToken();
        IntDTO? ObtenerLeadFormStart(int? idLinkedInForm);
        void ActualizarStartLeadsForm(FormStart entidad);
        List<InformacionBaseOportunidad> BuscarDatoLead();
        List<InformacionBaseOportunidad> BuscarDatoLeadAprobados();
        List<InformacionBaseOportunidad> BuscarDatoLeadARevisar();
        void ActualizarOportunidadLead(StringDTO Id);
        bool InsertaOportunidadesparaRevisar();
        void ValidarOportunindadesCreadas(ValidacionOportunidadCreadaLeadDTO entidad);
        void RegistrarOportunidadesNoCreadads(DateTime fecha);
        void ActualizarEstadoEnviado(int Id);
        void ActualizarEstadoEnviadoError(int Id);
        BoolDTO ObtenerEstadoEnvio(int id);
        IEnumerable<ReporteLeadsDTO> ObtenerReporteLeads();
        IEnumerable<ReporteLeadsDTO> ObtenerReporteLeadsByFecha(FiltroLandingPagePortaLinkedInDTO filtro);
        IEnumerable<ReporteLeadsPendientesDTO> ObtenerReportePendientes();

        bool CrearFormularioRegularizado(LinkedInActualizarDTO dto, string usuario);
        bool ActualizarFormularioRegularizado(LinkedInActualizarDTO dto, string usuario);
        IntDTO? ObtenerIdPorGuidLinkedInLead(string GuidLinkedInLead);

        void VerificarOportunidadEnLeads(VerificarOportunidadLead dto);
        List<InformacionBaseOportunidad> ObtenerReportePendientesRevisados();
        BoolDTO ValidarCreacionOportunidadLinkedinEstado();
        BoolDTO ValidarObtencionLeadLinkedinEstado();
    }
}
