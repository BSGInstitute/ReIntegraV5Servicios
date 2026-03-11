using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppMensajeEnviadoService
    {
        #region Metodos Base
        WhatsAppMensajeEnviado Add(WhatsAppMensajeEnviado entidad);
        WhatsAppMensajeEnviado Update(WhatsAppMensajeEnviado entidad);
        bool Delete(int id, string usuario);

        List<WhatsAppMensajeEnviado> Add(List<WhatsAppMensajeEnviado> listadoEntidad);
        List<WhatsAppMensajeEnviado> Update(List<WhatsAppMensajeEnviado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<WhatsAppMensajeEnviadoDTO> ObtenerWhatsAppMensajeEnviado();
        IEnumerable<WhatsAppMensajeEnviadoComboDTO> ObtenerCombo();
        List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area);
        List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibido(int idPersonal);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsEnviado(int idPersonal);
        List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area);
        bool ObtenerRespuestaValidarNumeroLibreCompleto(string numero, int idPais, int idCentroCosto, int idPersonal);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChats(int idPersonal);
        List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area, int idTipoAgenda);
        string ObtenerMensajeMultimedia(string waId);
        string ObtenerMensajeMultimediaPla(string waId);
        PersonalAlumnoDTO ObtenerConversacionNumero(string numero);
        PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChat();
        bool ValidarPlantillasEnviadas(string plantilla, string numero);
        bool ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero);
        bool ValidarMesajesEnviadosEn24Horas(string numero);
        bool ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero);
        bool ValidarMesajesEnviadosEn24HorasComercial(string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado);
        MensajeChatDTO ValidarUltimoMensajeRecibido(string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibidoControlMensaje(int idPersonal);

        List<InfoApiWhatsappDTO> ListaInformacionApiWhatsapp(int idPais);
        List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketing(int Tab, int Dia);
        List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketingV2(FiltroChatWhatsappDTO filtro);
        List<ChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingPorCelular(string Celular);
        List<ChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingBusquedaPorCelular(string Celular);
        bool ArchivarChat(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion);
        bool ArchivarChatMasivo(List<WhatsAppChatArchivadoDTO> lista, int IdPersonal, string UsuarioModificacion);
        bool SuscribirAlumno(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion);
        bool Desuscribir(string Celular, int IdAlumno, string UsuarioModificacion);
        bool DesuscribirChatMasivo(List<WhatsAppChatArchivadoDTO> lista, string usuario);
        bool DesArchivarChat(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion);
        ComboAtributoAlumnoDTO ObtenerCombosAtributosAlumno();
        AtributosAlumnoDTO ObtenerDatosAlumnoWhatsApp(int IdAlumno);
        ObtenerAtributosAlumnoOriginalDTO ActualizarDatosAlumno(ObtenerAtributosAlumnoDTO AlumnoActualizar, string Usuario);
        public int CrearOportunidadWhatsapp(OportunidadWhatsappDTO dto, string usuario);
       // public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppFacebookMarketing(int Tab, int Dia);
        List<WhatsAppHistorialMensajesOperacionesDTO> ListaHistorialMensajeChatOperaciones(int idPersonal, string numero, string area);
        List<ProbabilidaWhatsAppDTO> ObtenerProbabilidadPorOportunidad(int idOportunidad);
        List<ProgramaPorOportunidadDTO> ObtenerProgramaPorOportunidadWhatsapp(int idOportunidad);
        public IEnumerable<ComboDTO> ObtenerPersonalOportunidad();
        public AsesorActualDTO ObtenerIdAsesorActual(int idOportunidad);
        public ModeloPredictivoDTO ObtenerModeloPredictivoPorAlumnoYPrograma(int idAlumno, int idPGeneral);
        public ProbabilidadResultadoDTO ValidarProbabilidadOportunidadesRecalculo(int idOportunidad, int idAlumno, int idArea, int idPGeneral);
        public void EliminarRegistroModeloPredictivoPorAlumno(int idAlumno);
        public List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppFacebookMarketing(int Tab, int Dia, int IdAsesor);
        public bool EsAsesorVentasValido(int idAsesor);
        Task<DatosExtraccionRegistrosResponseDTO> CapturarRegistrosModeloIA(DatosExtraccionRegistrosDTO datosExtraccionRegistros);
        Task<DesactivarInteraccionResponseDTO> DesactivarInteraccionAutomaticaWhatsapp(string celularAlumno, string idCampania);
        Task<DatosInteraccionAutomaticaResponseDTO> ObtenerDatosExtraidosInteraccionAutomatica(string celularAlumno);
        Task<DesactivarInteraccionResponseDTO> ValidarGuardadoDatosInteraccionAutomatica(string celularAlumno);
    }
}