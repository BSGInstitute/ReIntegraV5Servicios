using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProgramaGeneralPuntoCorteDetalleService
    {
        #region Metodos Base
        ProgramaGeneralPuntoCorteDetalle Add(ProgramaGeneralPuntoCorteDetalle entidad);
        ProgramaGeneralPuntoCorteDetalle Update(ProgramaGeneralPuntoCorteDetalle entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralPuntoCorteDetalle> Add(List<ProgramaGeneralPuntoCorteDetalle> listadoEntidad);
        List<ProgramaGeneralPuntoCorteDetalle> Update(List<ProgramaGeneralPuntoCorteDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
