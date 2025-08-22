using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionDatoRemarketingProbabilidadRegistroService
    {
        #region Metodos Base
        ConfiguracionDatoRemarketingProbabilidadRegistro Add(ConfiguracionDatoRemarketingProbabilidadRegistro entidad);
        ConfiguracionDatoRemarketingProbabilidadRegistro Update(ConfiguracionDatoRemarketingProbabilidadRegistro entidad);
        bool Delete(int id, string usuario);

        List<ConfiguracionDatoRemarketingProbabilidadRegistro> Add(List<ConfiguracionDatoRemarketingProbabilidadRegistro> listadoEntidad);
        List<ConfiguracionDatoRemarketingProbabilidadRegistro> Update(List<ConfiguracionDatoRemarketingProbabilidadRegistro> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        public bool ActualizarListaConfiguracionDatoRemarketingProbabilidadRegistro(int idConfiguracionDatoRemarketing, List<int> listaIdProbabilidadRegistro, string usuario);


    }
}
