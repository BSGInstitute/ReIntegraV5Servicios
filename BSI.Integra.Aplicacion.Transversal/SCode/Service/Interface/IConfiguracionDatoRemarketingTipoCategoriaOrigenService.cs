using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionDatoRemarketingTipoCategoriaOrigenService
    {
        #region Metodos Base
        ConfiguracionDatoRemarketingTipoCategoriaOrigen Add(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad);
        ConfiguracionDatoRemarketingTipoCategoriaOrigen Update(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> Add(List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad);
        List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> Update(List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
       


    }
}
