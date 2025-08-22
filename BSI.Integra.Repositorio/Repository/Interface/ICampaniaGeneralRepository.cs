using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampaniaGeneralRepository : IGenericRepository<TCampaniaGeneral>
    {
        #region Metodos Base
        TCampaniaGeneral Add(CampaniaGeneral entidad);
        TCampaniaGeneral Update(CampaniaGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaGeneral> Add(IEnumerable<CampaniaGeneral> listadoEntidad);
        IEnumerable<TCampaniaGeneral> Update(IEnumerable<CampaniaGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CampaniaGeneralEnvio> ObtenerCampaniaGeneral();
        IEnumerable<CampaniaGeneralEnvio> ObtenerCampaniaGeneralWhatsApp();
        IEnumerable<ConfiguracionDeEnvioParaWhatsAppMasPlantilla> ObtenerConfiguracionDeEnvioParaWhatsAppMasPlantilla();
        CampaniaGeneral AddSqlInsert(CampaniaGeneral entidad);
        void ActualizarEstadoEnvioWhatsApp(int idCampaniaGeneral);
        List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO> ObtenerActividadCampaniaGeneralParaEjecutar();
        Object EjecutarReplicado(int IdCampaniaGeneral, string usuario);
        List<IdCampaniaGeneral> ObtenerCampaniaGeneralDetalle(int IdCampaniaGeneral);
        List<InformacionPreprocesamientoWhatsAppCampaniaGeneralDTO> ObtenerCantidadCampaniaGeneralDetalle(int IdCampaniaGeneralDetalle);
        public List<ObtenerPrioridadesEnvioWhatsAppDTO> ObtenerPrioridadesEnvioWhatsApp();

        public ResultadoEjecucionCampaniaDTO ObtenerPrioridadesEnvioWhatsApp2();

    }
}
