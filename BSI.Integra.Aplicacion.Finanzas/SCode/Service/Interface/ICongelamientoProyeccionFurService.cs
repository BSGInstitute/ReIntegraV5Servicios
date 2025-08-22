using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICongelamientoProyeccionFurService
    {
        #region Metodos Base
        bool Delete(int id, string usuario);

        List<CongelamientoProyeccionFur> Add(List<CongelamientoProyeccionFur> listadoEntidad);
        List<CongelamientoProyeccionFur> Update(List<CongelamientoProyeccionFur> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
