using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionDatoRemarketingCategoriaOrigenService
    {
        #region Metodos Base
        ConfiguracionDatoRemarketingCategoriaOrigen Add(ConfiguracionDatoRemarketingCategoriaOrigen entidad);
        ConfiguracionDatoRemarketingCategoriaOrigen Update(ConfiguracionDatoRemarketingCategoriaOrigen entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionDatoRemarketingCategoriaOrigen> Add(List<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad);
        List<ConfiguracionDatoRemarketingCategoriaOrigen> Update(List<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();

        public bool ActualizarListaConfiguracionDatoRemarketingCategoriaOrigen(int idConfiguracionDatoRemarketing, List<int> listaIdCategoriaOrigen, string usuario);
        public bool EliminarListaConfiguracionDatoRemarketingCategoriaOrigen(int idConfiguracionDatoRemarketing, string usuarioResponsable);


    }
}
