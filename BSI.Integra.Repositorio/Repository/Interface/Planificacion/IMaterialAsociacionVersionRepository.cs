using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAsociacionVersionRepository
    {
        #region Metodos Base
        TMaterialAsociacionVersion Add(MaterialAsociacionVersion entidad);
        TMaterialAsociacionVersion Update(MaterialAsociacionVersion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAsociacionVersion> Add(IEnumerable<MaterialAsociacionVersion> listadoEntidad);
        IEnumerable<TMaterialAsociacionVersion> Update(IEnumerable<MaterialAsociacionVersion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialAsociacionVersion> ObtenerPorIdMaterialTipo(int idMaterialTipo);
    }
}
