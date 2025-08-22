using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface INuevoAlumnoCongeladoRepository : IGenericRepository<TNuevoAlumnoCongelado>
    {
        #region Metodos Base
        TNuevoAlumnoCongelado Add(NuevoAlumnoCongelado entidad);
        TNuevoAlumnoCongelado Update(NuevoAlumnoCongelado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TNuevoAlumnoCongelado> Add(IEnumerable<NuevoAlumnoCongelado> listadoEntidad);
        IEnumerable<TNuevoAlumnoCongelado> Update(IEnumerable<NuevoAlumnoCongelado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<NuevoAlumnoCongeladoDTO> ObtenerListaNuevoAlumnoCongelado();
        bool InsertarExcelNuevoAlumnoCongelado(List<NuevoAlumnoCongeladoExcelDTO> datos, DateTime FechaCongelamiento, int IdPeriodo, string User);
    }
}
