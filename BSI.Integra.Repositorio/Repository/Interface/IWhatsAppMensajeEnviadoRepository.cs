using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppMensajeEnviadoRepository : IGenericRepository<TWhatsAppMensajeEnviado>
    {
        #region Metodos Base
        TWhatsAppMensajeEnviado Add(WhatsAppMensajeEnviado entidad);
        TWhatsAppMensajeEnviado Update(WhatsAppMensajeEnviado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppMensajeEnviado> Add(IEnumerable<WhatsAppMensajeEnviado> listadoEntidad);
        IEnumerable<TWhatsAppMensajeEnviado> Update(IEnumerable<WhatsAppMensajeEnviado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<WhatsAppMensajeEnviadoDTO> ObtenerWhatsAppMensajeEnviado();
        IEnumerable<WhatsAppMensajeEnviadoComboDTO> ObtenerCombo();
        List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area);
        List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area);
        List<WhatsAppHistorialMensajesOperacionesDTO> ListaHistorialMensajeChatOperaciones(int idPersonal, string numero, string area);
        List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatAtc(int idPersonal, string numero, string area, string idPais);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibido(int idPersonal);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsEnviado(int idPersonal);
        List<WhatsAppHistorialMensajesDTO> ListaHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area);
        bool ValidarPlantillasEnviadas(string plantilla, string numero);
        bool ValidarPlantillasEnviadasApiComercial(string plantilla, string numero);
        bool ValidarPlantillasEnviadasApiComercialPersonal(string plantilla, string numero, int idPersonal);
        bool ValidarPlantillasEnviadasApiComercial(string plantilla, string numero, DateTime fechaUltimoMensajeRecibido);
        bool ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero);
        bool ValidarMesajeRecibidosApiComercial(string numero);
        bool ValidarMesajesEnviadosEn24Horas(string numero);
        bool ValidarMesajesEnviadosEn24HorasComercial(string Numero, string CuentaIdentificadorWA);
        MensajeChatDTO UltimoMensajeRecibido(string Numero, string CuentaIdentificadorWA);
        bool ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero);
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChats(int idPersonal);
        List<WhatsAppMensajesDTO> HistorialChatsRecibido(int idPersonal, string numero, string area, int idTipoAgenda);
        string ObtenerMensajeMultimedia(string waId);
        PersonalAlumnoDTO ObtenerConversacionNumero(string numero);
        PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChat();
        List<WhatsAppMensajesDTO> ListaUltimoMensajeChatsRecibidoControlMensaje(int idPersonal);
        List<InfoApiWhatsappDTO> ListaInformacionApiWhatsapp(int idPersonal);
        List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketing(int Tab, int Dia);
        List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppMarketingv2(int tab, DateTime fechaInicio, DateTime fechaFin);
        List<ChatWhatsAppMarketingDTO> ObtenerChatWhatsAppFacebookMarketing(int Tab, int Dia,int IdAsesor);
        List<ObtenerChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingPorCelular(string Celular);
        List<ObtenerChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingMasivoPorCelular(string Celular);
        List<ObtenerChatWhatsAppMarketingPorCelularDTO> ObtenerChatWhatsAppMarketingBusquedaPorCelular(string Celular);
        bool ArchivarChat(string Celular, int IdAlumno, int IdPersonal, string UsuarioModificacion);
        bool DesArchivarChat(string Celular, string UsuarioModificacion);
        bool SuscribirAlumno(string Celular, int IdAlumno, string UsuarioModificacion);
        bool Desuscribir(string Celular, int IdAlumno, string UsuarioModificacion);

        List<ComboDTO> ObtenerComboIndustria();
        List<ComboDTO> ObtenerComboAreaFormacion();
        List<ComboDTO> ObtenerComboAreaTrabajo();
        List<ComboDTO> ObtenerComboCargo();
        List<HistorialAlumnoDTO> ObtenerHistorialAlumnoWhatsApp(int IdAlumno);
        ObtenerAtributosAlumnoDTO ObtenerDatosAlumnoWhatsApp(int IdAlumno);
        ObtenerAtributosAlumnoOriginalDTO ObtenerDatosOriginalesAlumnoWhatsApp(int? IdAlumno);
        bool ActualizarDatosAlumno(int? IdAlumno, string CampoActualizado, string? ValorAnterior, string? ValorNuevo, string Usuario);
        bool InsertarMensajesLogJsonEnvios(int? IdAlumno, string Numero, string Mensaje);
        List<ProbabilidaWhatsAppDTO> ObtenerProbabilidadPorOportunidad(int idOportunidad);
        List<ProgramaPorOportunidadDTO> ObtenerProgramaPorOportunidadWhatsapp(int idOportunidad);
        public IEnumerable<OportunidadVentaCruzadaWhatsappDTO> ObtenerVentaCruzadaPorIdAlumnoWhatsapp(int idAlumno, int idArea, int idPGeneral);
        public IEnumerable<ComboDTO> ObtenerPersonalOportunidad();
        public AsesorActualDTO ObtenerIdAsesorActual(int idOportunidad);
        public ModeloPredictivoDTO ObtenerModeloPredictivoPorAlumnoYPrograma(int idAlumno, int idPGeneral);
        public void EliminarRegistroModeloPredictivoPorAlumno(int idAlumno);
        bool VerificarActualizarAlumno(int idAlumno);
        public void RegistrarActualizacionAlumno(int idAlumno, string usuario);
        public bool EsAsesorVentasValido(int idAsesor);
        public string ObtenerRangoProbabilidadAlumno(int idAlumno);
        public List<MensajeExtraccionRegistroDTO> ObtenerChatWhatsAppMarketingPorCelularRangoFecha(string celularAlumno, DateTime fechaInicio, DateTime fechaFin);


    }

}