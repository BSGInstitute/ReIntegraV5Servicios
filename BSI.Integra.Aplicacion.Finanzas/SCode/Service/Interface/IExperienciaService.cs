using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IExperienciaService
    {
        #region Metodos Base
        Experiencia Add(ExperienciaRecibidoDTO data, string Usuario);
        Experiencia Update(ExperienciaRecibidoDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<Experiencia> Add(List<Experiencia> listadoEntidad);
        List<Experiencia> Update(List<Experiencia> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ExperienciaComboDTO> ObtenerCombo();
    }
}
