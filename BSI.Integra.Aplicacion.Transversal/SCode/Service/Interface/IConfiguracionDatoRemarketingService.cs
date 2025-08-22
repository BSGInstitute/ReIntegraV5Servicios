using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionDatoRemarketingService
    {
        #region Metodos Base
        ConfiguracionDatoRemarketing Add(ConfiguracionDatoRemarketing entidad);
        ConfiguracionDatoRemarketing Update(ConfiguracionDatoRemarketing entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionDatoRemarketing> Add(List<ConfiguracionDatoRemarketing> listadoEntidad);
        List<ConfiguracionDatoRemarketing> Update(List<ConfiguracionDatoRemarketing> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DTO.ComboDTO> ObtenerCombo();

        public List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO> ObtenerConfiguracionesDatoRemarketing();
        public List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> ObtenerAgendaTabVentasParaConfiguracion();
        //public int ActualizarListaConfiguracionDatoRemarketing(ConfiguracionDatoRemarketingDTO configuracionDatoRemarketingAActualizar);

        //public bool ActualizarListaConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingDTO configuracionDatoRemarketingAActualizar);


    }
}
