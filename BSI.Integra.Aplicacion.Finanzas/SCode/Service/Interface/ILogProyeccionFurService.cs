using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ILogProyeccionFurService
    {
        #region Metodos Base
        LogProyeccionFur Add(LogProyectadosDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<LogProyeccionFur> Add(List<LogProyeccionFur> listadoEntidad);
        List<LogProyeccionFur> Update(List<LogProyeccionFur> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
