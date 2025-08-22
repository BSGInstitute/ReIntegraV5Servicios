using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface IWhatsAppMensajePublicidadService
    {
        #region Metodos Base
        WhatsAppMensajePublicidad Add(WhatsAppMensajePublicidadDTO entidad);
        WhatsAppMensajePublicidad Update(WhatsAppMensajePublicidadDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<WhatsAppMensajePublicidad> Add(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad);
        IEnumerable<WhatsAppMensajePublicidad> Update(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public bool InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(List<WhatsAppMensajePublicidadDTO> listaNuevoWhatsAppMensajePublicidad);
        public int InsertarWhatsAppMensajePublicidad(WhatsAppMensajePublicidadDTO filtro);
        public bool ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO preprocesamientoWhatsAppCampaniaGeneral);
        public List<WhatsAppResultadoCampaniaGeneralDTO> ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(int idCampaniaGeneralDetalle);
        DatosALumnoWhatsappDTO ObtenerDatosAlumnoIntegra(string WaFrom, string NumeroEmpresa);
    }
}
