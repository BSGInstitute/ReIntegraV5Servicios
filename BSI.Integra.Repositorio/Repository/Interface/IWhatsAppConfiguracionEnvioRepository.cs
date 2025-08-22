using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppConfiguracionEnvioRepository
    {
        #region Metodos Base
        TWhatsAppConfiguracionEnvio Add(WhatsAppConfiguracionEnvioDTO entidad);
        TWhatsAppConfiguracionEnvio Update(WhatsAppConfiguracionEnvioDTO entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TWhatsAppConfiguracionEnvio> Add(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad);
        IEnumerable<TWhatsAppConfiguracionEnvio> Update(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ConjuntoListaDetalleWhatsAppDTO> ObtenerConfiguracionPorIdConjuntoLista(int idConjuntoLista);
        int EliminarEnviosProcesados(int idConjuntoLista);
        void EliminarWhatsAppConfiguracionMailingGeneral(int idCampaniaGeneralDetalle);
        WhatsAppConfiguracionEnvioDTO InsertarWhatsAppConfiguracionGeneralMailing(int idCampaniaGeneralDetalle);
        bool ActualizarEstadoWhatsAppRecuperacion(string tipo, string usuarioResponsable, bool estadoHabilitado, int IdModuloSistemaWhatsAppMailing);

        bool InsertarRegistroCaidaServidor(string servidor);
        TWhatsAppConfiguracionEnvio FirstById(int id);
        List<ConjuntoListaDetalleWhatsAppDTO> ConsultaWhatsAppYConfiguracionEnvio(int IdConjuntoLista);
    }
}
