
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IFlujoService
    {
        IEnumerable<FlujoDetalleDTO> Obtener();
        FlujoCombosDTO ObtenerCombos();
        IEnumerable<FlujoFaseDTO> ObtenerFlujoFasePorIdFlujo(int idFlujo);
        IEnumerable<FlujoActividadDTO> ObtenerFlujoActividadPorIdFlujoFase(int idFlujoFase);
        IEnumerable<FlujoOcurrenciaDetalleDTO> ObtenerFlujoOcurrenciaPorIdFlujoActividad(int idFlujoActividad);
        bool Insertar(FlujoDTO dto, string usuario);
        bool Actualizar(FlujoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        bool InsertarFase(FlujoFaseDTO dto, string usuario);
        bool ActualizarFase(FlujoFaseDTO dto, string usuario);
        bool EliminarFase(int id, string usuario);
        bool InsertarActividad(FlujoActividadDTO dto, string usuario);
        bool ActualizarActividad(FlujoActividadDTO dto, string usuario);
        bool EliminarActividad(int id, string usuario);
        bool InsertarOcurrencia(FlujoOcurrenciaDTO dto, string usuario);
        bool ActualizarOcurrencia(FlujoOcurrenciaDTO dto, string usuario);
        bool EliminarOcurrencia(int id, string usuario);
    }
}
