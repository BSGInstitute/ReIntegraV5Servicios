using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFacebookLeadService
    {
        #region Metodos Base

        #endregion

        public int ProcesarFacebookLead(LeadgenInformacionDTO LeadgenInformacionDTO);

    }
}
