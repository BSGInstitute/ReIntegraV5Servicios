using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICongelamientoProyeccionFurRepository : IGenericRepository<TCongelamientoProyeccionFur>
    {
        #region Metodos Base
        TCongelamientoProyeccionFur Add(CongelamientoProyeccionFur entidad);
        TCongelamientoProyeccionFur Update(CongelamientoProyeccionFur entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCongelamientoProyeccionFur> Add(IEnumerable<CongelamientoProyeccionFur> listadoEntidad);
        IEnumerable<TCongelamientoProyeccionFur> Update(IEnumerable<CongelamientoProyeccionFur> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public CongelamientoProyeccionFurStringDTO ObtenerCongelamientoProyeccionFur(int idCabecera);
    }
}
