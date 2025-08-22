using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPeriodoMesProyeccionService
    {
        #region Metodos Base
        PeriodoMesProyeccion Add(PeriodoMesProyeccion entidad);
        PeriodoMesProyeccion Update(PeriodoMesProyeccion entidad);
        bool Delete(int id, string usuario);

        List<PeriodoMesProyeccion> Add(List<PeriodoMesProyeccion> listadoEntidad);
        List<PeriodoMesProyeccion> Update(List<PeriodoMesProyeccion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<PeriodoMesProyeccion> InsertarPeriodoMesProyeccion(List<PeriodoMesProyeccionDTO> entidad, string Usuario);
        PeriodoMesProyeccion ActulizarPeriodoMesProyeccion(PeriodoMesProyeccionDTO entidad, string Usuario);


    }
}
