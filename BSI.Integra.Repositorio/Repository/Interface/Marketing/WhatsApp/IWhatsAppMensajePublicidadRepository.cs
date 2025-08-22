using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppMensajePublicidadRepository : IGenericRepository<TWhatsAppMensajePublicidad>
    {

        #region Metodos Base
        TWhatsAppMensajePublicidad Add(WhatsAppMensajePublicidadDTO entidad);
        TWhatsAppMensajePublicidad Update(WhatsAppMensajePublicidadDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppMensajePublicidad> Add(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad);
        IEnumerable<TWhatsAppMensajePublicidad> Update(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public bool InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(List<WhatsAppMensajePublicidadDTO> listaNuevoWhatsAppMensajePublicidad);
        public int InsertarWhatsAppMensajePublicidad(WhatsAppMensajePublicidadDTO filtro);
        public bool ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO preprocesamientoWhatsAppCampaniaGeneral);
        public List<WhatsAppResultadoCampaniaGeneralDTO> ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(int idCampaniaGeneralDetalle);
        public List<WhatsAppMensajePublicidadDTO> ObtenerTodosLosmenajesPorIdCampaniaGeneral(int IdCampaniaGeneral);
        DatosALumnoWhatsappDTO ObtenerDatosAlumnoIntegra(string WaFrom, string NumeroEmpresa);
    }
}