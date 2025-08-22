using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IControlDocAlumnoRepository : IGenericRepository<TControlDocAlumno>
    {
        #region Metodos Base
        TControlDocAlumno Add(ControlDocAlumno entidad);
        TControlDocAlumno Update(ControlDocAlumno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TControlDocAlumno> Add(IEnumerable<ControlDocAlumno> listadoEntidad);
        IEnumerable<TControlDocAlumno> Update(IEnumerable<ControlDocAlumno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        ControlDocAlumno ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera);
    }
}
