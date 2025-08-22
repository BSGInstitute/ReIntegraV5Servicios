using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IAreaParametroSeoPwService
    {
        IEnumerable<AreaParametrosSeoPorIdAreaDTO> ObtenerAreaParametrosSeoPorIdArea(int IdTag);
        List<AreaParametroSeoPw> ObtenerPorId(int Id);
        AreaParametroSeoPw ObtenertodoId(int Id);
        void EliminarPorIdAreaCapacitacion(string usuario, int idAreaCapacitacion, List<AreaParametrosSeoPorIdAreaDTO> nuevos);
    }
}
