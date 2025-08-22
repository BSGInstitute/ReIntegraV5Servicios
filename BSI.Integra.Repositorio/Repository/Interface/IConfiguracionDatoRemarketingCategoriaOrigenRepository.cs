using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionDatoRemarketingCategoriaOrigenRepository : IGenericRepository<TConfiguracionDatoRemarketingCategoriaOrigen>
    {
        #region Metodos Base
        TConfiguracionDatoRemarketingCategoriaOrigen Add(ConfiguracionDatoRemarketingCategoriaOrigen entidad);
        TConfiguracionDatoRemarketingCategoriaOrigen Update(ConfiguracionDatoRemarketingCategoriaOrigen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionDatoRemarketingCategoriaOrigen> Add(IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad);
        IEnumerable<TConfiguracionDatoRemarketingCategoriaOrigen> Update(IEnumerable<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
      


    }
}
