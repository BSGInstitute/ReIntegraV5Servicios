using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPgeneralProyectoAplicacionAnexoRepository : IGenericRepository<TPgeneralProyectoAplicacionAnexo>
    {
        #region Metodos Base
        TPgeneralProyectoAplicacionAnexo Add(PgeneralProyectoAplicacionAnexo entidad);
        TPgeneralProyectoAplicacionAnexo Update(PgeneralProyectoAplicacionAnexo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralProyectoAplicacionAnexo> Add(IEnumerable<PgeneralProyectoAplicacionAnexo> listadoEntidad);
        IEnumerable<TPgeneralProyectoAplicacionAnexo> Update(IEnumerable<PgeneralProyectoAplicacionAnexo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PgeneralProyectoAplicacionAnexoDTO> ObtenerListaPgeneralProyectoAplicacionAnexoPorIdPGeneral(int idPgeneral);
        PgeneralProyectoAplicacionAnexo? ObtenerPorId(int id);
    }
}
