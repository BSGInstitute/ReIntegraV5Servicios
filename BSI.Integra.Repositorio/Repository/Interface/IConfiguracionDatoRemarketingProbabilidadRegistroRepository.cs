using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionDatoRemarketingProbabilidadRegistroRepository : IGenericRepository<TConfiguracionDatoRemarketingProbabilidadRegistro>
    {
        #region Metodos Base
        TConfiguracionDatoRemarketingProbabilidadRegistro Add(ConfiguracionDatoRemarketingProbabilidadRegistro entidad);
        TConfiguracionDatoRemarketingProbabilidadRegistro Update(ConfiguracionDatoRemarketingProbabilidadRegistro entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionDatoRemarketingProbabilidadRegistro> Add(IEnumerable<ConfiguracionDatoRemarketingProbabilidadRegistro> listadoEntidad);
        IEnumerable<TConfiguracionDatoRemarketingProbabilidadRegistro> Update(IEnumerable<ConfiguracionDatoRemarketingProbabilidadRegistro> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        


    }
}
