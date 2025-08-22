using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICodigoCiiuIndustriaService
    {
        CodigoCiiuIndustria ObtenerPorId(int id);
        List<ComboDTO> ObtenerPorNombre(string filtro);
    }
}
