using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISeguimientoAlumnoCategoriaRepository : IGenericRepository<TSeguimientoAlumnoCategorium>
    {

        #region
        TSeguimientoAlumnoCategorium Add(SeguimientoAlumnoCategoria entidad);
        TSeguimientoAlumnoCategorium Update(SeguimientoAlumnoCategoria entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSeguimientoAlumnoCategorium> Add(IEnumerable<SeguimientoAlumnoCategoria> listadoEntidad);
        IEnumerable<TSeguimientoAlumnoCategorium> Update(IEnumerable<SeguimientoAlumnoCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<FiltroSeguimientoAlumnoCategoriaDTO> ObtenerSeguimientoAlumnoCategoria();
        List<ComentarioConfiguracionDTO> ObtenerConfiguracion();
        SeguimientoAlumnoCategoria ObtenerPorId(int id);
    }
}
