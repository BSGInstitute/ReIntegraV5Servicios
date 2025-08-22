using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModoPersonalFurRepository : IGenericRepository<TModoPersonalFur>
    {
        #region Metodos Base
        TModoPersonalFur Add(ModoPersonalFur entidad);
        TModoPersonalFur Update(ModoPersonalFur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModoPersonalFur> Add(IEnumerable<ModoPersonalFur> listadoEntidad);
        IEnumerable<TModoPersonalFur> Update(IEnumerable<ModoPersonalFur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        ModoPersonalFurDTO ObtenerPermisosFurByIdPersonal(int IdPersonal);


    }
}
