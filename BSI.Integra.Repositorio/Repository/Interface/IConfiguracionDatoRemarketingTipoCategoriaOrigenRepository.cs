using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionDatoRemarketingTipoCategoriaOrigenRepository : IGenericRepository<TConfiguracionDatoRemarketingTipoCategoriaOrigen>
    {
        #region Metodos Base
        TConfiguracionDatoRemarketingTipoCategoriaOrigen Add(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad);
        TConfiguracionDatoRemarketingTipoCategoriaOrigen Update(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionDatoRemarketingTipoCategoriaOrigen> Add(IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad);
        IEnumerable<TConfiguracionDatoRemarketingTipoCategoriaOrigen> Update(IEnumerable<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        
    }
}
