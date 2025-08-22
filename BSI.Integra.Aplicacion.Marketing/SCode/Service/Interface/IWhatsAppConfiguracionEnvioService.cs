using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppResultadoConjuntoListaDTO = BSI.Integra.Aplicacion.DTO.Modelos.WhatsAppResultadoConjuntoListaDTO;
using WhatsAppMensajeEnviadoAutomaticoDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.WhatsAppMensajeEnviadoAutomaticoDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppConfiguracionEnvioService
    {
        #region Metodos Base
        TWhatsAppConfiguracionEnvio Add(WhatsAppConfiguracionEnvioDTO entidad, string usuario);
        TWhatsAppConfiguracionEnvio Update(WhatsAppConfiguracionEnvioDTO entidad, string usuario);
        bool Delete(int id, string usuario);
        IEnumerable<TWhatsAppConfiguracionEnvio> Add(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad, string usuario);
        IEnumerable<TWhatsAppConfiguracionEnvio> Update(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad, string usuario);
        TWhatsAppConfiguracionEnvio Update(TWhatsAppConfiguracionEnvio entidad, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ConjuntoListaDetalleWhatsAppDTO> ObtenerConfiguracionPorIdConjuntoLista(int idConjuntoLista);
        int EliminarEnviosProcesados(int idConjuntoLista);
        bool ActualizarEstadoWhatsAppRecuperacion(string tipo, string usuarioResponsable, bool estadoHabilitado, int IdModuloSistemaWhatsAppMailing);
        void EliminarWhatsAppConfiguracionMailingGeneral(int idCampaniaGeneralDetalle);
        WhatsAppConfiguracionEnvioDTO InsertarWhatsAppConfiguracionGeneralMailing(int idCampaniaGeneralDetalle);
        bool InsertarRegistroCaidaServidor(string servidor);
        List<ConjuntoListaDetalleWhatsAppDTO> ConsultaWhatsAppYConfiguracionEnvio(int IdConjuntoLista);
        TWhatsAppConfiguracionEnvio FirstById(int id);
        //void RemplazarEtiquetas(List<ReemplazoEtiquetaPlantillaDTO> NumeroAlumno, int IdPersonal, int IdPlantilla, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal, List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario);
    }
}
