using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionDatoRemarketingTipoDatoService
    {
        #region Metodos Base
        ConfiguracionDatoRemarketingTipoDato Add(ConfiguracionDatoRemarketingTipoDato entidad);
        ConfiguracionDatoRemarketingTipoDato Update(ConfiguracionDatoRemarketingTipoDato entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionDatoRemarketingTipoDato> Add(List<ConfiguracionDatoRemarketingTipoDato> listadoEntidad);
        List<ConfiguracionDatoRemarketingTipoDato> Update(List<ConfiguracionDatoRemarketingTipoDato> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        public bool ActualizarListaConfiguracionDatoRemarketingTipoDato(int idConfiguracionDatoRemarketing, List<int> listaIdTipoDato, string usuario);
        public bool EliminarListaConfiguracionDatoRemarketingTipoDato(int idConfiguracionDatoRemarketing, string usuarioResponsable);
       


    }
}
