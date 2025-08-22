using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionDatoRemarketingRepository : IGenericRepository<TConfiguracionDatoRemarketing>
    {
        #region Metodos Base
        TConfiguracionDatoRemarketing Add(ConfiguracionDatoRemarketing entidad);
        TConfiguracionDatoRemarketing Update(ConfiguracionDatoRemarketing entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionDatoRemarketing> Add(IEnumerable<ConfiguracionDatoRemarketing> listadoEntidad);
        IEnumerable<TConfiguracionDatoRemarketing> Update(IEnumerable<ConfiguracionDatoRemarketing> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


        IEnumerable<ComboDTO> ObtenerCombo();

        public List<ConfiguracionDatoRemarketingGrillaDTO> ObtenerConfiguracionesDatoRemarketing();
        public List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> ObtenerAgendaTabVentasParaConfiguracion();
        public int ObtenerTabRedireccionRemarketing(int idTipoDato, int idSubCategoriaDato, int idProbabilidadRegistroPw);


    }
}
