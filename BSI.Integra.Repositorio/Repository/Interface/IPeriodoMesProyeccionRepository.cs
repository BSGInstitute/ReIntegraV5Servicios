using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPeriodoMesProyeccionRepository : IGenericRepository<TPeriodoMesProyeccion>
    {
        #region Metodos Base
        TPeriodoMesProyeccion Add(PeriodoMesProyeccion entidad);
        TPeriodoMesProyeccion Update(PeriodoMesProyeccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPeriodoMesProyeccion> Add(IEnumerable<PeriodoMesProyeccion> listadoEntidad);
        IEnumerable<TPeriodoMesProyeccion> Update(IEnumerable<PeriodoMesProyeccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<PeriodoMesProyeccionDTO> ObtenerPeriodoMesProyeccion();
        public IEnumerable<PeriodoMesProyeccionCombo> ObtenerPeriodoMesProyeccionCombo();

        public PeriodoMesProyeccion ObtenerPeriodoMesProyeccionById(int id);
    }
}
