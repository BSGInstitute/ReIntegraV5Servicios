using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPeriodoLectivoRepository : IGenericRepository<TPeriodoLectivo>
    {
        #region Metodos Base
        TPeriodoLectivo Add(PeriodoLectivo entidad);
        TPeriodoLectivo Update(PeriodoLectivo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPeriodoLectivo> Add(IEnumerable<PeriodoLectivo> listadoEntidad);
        IEnumerable<TPeriodoLectivo> Update(IEnumerable<PeriodoLectivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
 
    }
}

