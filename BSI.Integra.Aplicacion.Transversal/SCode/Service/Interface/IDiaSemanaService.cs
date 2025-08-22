using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDiaSemanaService
    {
        #region Metodos Base
        DiaSemana Add(DiaSemana entidad);
        DiaSemana Update(DiaSemana entidad);
        bool Delete(int id, string usuario);

        List<DiaSemana> Add(List<DiaSemana> listadoEntidad);
        List<DiaSemana> Update(List<DiaSemana> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        public IEnumerable<ComboDTO> ObtenerCombo();
    }
}
