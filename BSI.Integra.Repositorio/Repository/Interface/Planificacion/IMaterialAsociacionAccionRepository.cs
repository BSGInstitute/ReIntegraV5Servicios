using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAsociacionAccionRepository
    {
        #region Metodos Base
        TMaterialAsociacionAccion Add(MaterialAsociacionAccion entidad);
        TMaterialAsociacionAccion Update(MaterialAsociacionAccion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAsociacionAccion> Add(IEnumerable<MaterialAsociacionAccion> listadoEntidad);
        IEnumerable<TMaterialAsociacionAccion> Update(IEnumerable<MaterialAsociacionAccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialAsociacionAccion> ObtenerPorIdMaterialTipo(int idTipoDocumentoAlumno);
    }
}
