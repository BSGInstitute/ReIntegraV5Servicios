using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionDatoRemarketingTipoDatoRepository : IGenericRepository<TConfiguracionDatoRemarketingTipoDato>
    {
        #region Metodos Base
        TConfiguracionDatoRemarketingTipoDato Add(ConfiguracionDatoRemarketingTipoDato entidad);
        TConfiguracionDatoRemarketingTipoDato Update(ConfiguracionDatoRemarketingTipoDato entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionDatoRemarketingTipoDato> Add(IEnumerable<ConfiguracionDatoRemarketingTipoDato> listadoEntidad);
        IEnumerable<TConfiguracionDatoRemarketingTipoDato> Update(IEnumerable<ConfiguracionDatoRemarketingTipoDato> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();

        





    }
}
