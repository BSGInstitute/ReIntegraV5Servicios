using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteActividadService
    {
        Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto);
        Task<int> InsertarCabeceraAsync(GestionDocenteActividadCabeceraDTO dto);
        Task<int> InsertarDetalleAsync(InsertarActividadDetalleRequestDTO request);
        Task<int> InsertarOcurrenciaAsync(InsertarOcurrenciaRequestDTO request);
        Task<int> AsociarActividadAFlujoAsync(GestionDocenteActividadCabeceraFlujoDTO dto);
        Task<bool> DesasociarActividadDeFlujoAsync(int id, string usuario);
        Task<List<GestionDocenteActividadCabeceraFlujoDTO>> ObtenerActividadesPorFlujoAsync(int idFlujo);
        IEnumerable<GestionDocenteSesionDTO> ObtenerSesiones();
        IEnumerable<GestionDocenteOcurrenciaDTO> ObtenerOcurrencias();
        IEnumerable<GestionDocenteConfianzaUmbralNivelDTO> ObtenerConfianzaUmbralNiveles();
        IEnumerable<GestionDocenteOcurrenciaTipoDTO> ObtenerOcurrenciaTipos();
        IEnumerable<GestionDocenteReferenciaTiempoDTO> ObtenerReferenciasTiempo();
        IEnumerable<GestionDocenteActividadDetalleTipoDTO> ObtenerActividadDetalleTipos();
        IEnumerable<GestionDocenteDisparadorFlujoTipoDTO> ObtenerDisparadorFlujoTipos();
        IEnumerable<GestionDocenteUnidadTiempoDTO> ObtenerUnidadesTiempo();
        List<object> ObtenerDisparadorFlujoTiposConfiguracion();
        IEnumerable<GestionDocenteModoMarcadoDTO> ObtenerModosMarcado();
        IEnumerable<GestionDocenteMedioComunicacionDTO> ObtenerMediosComunicacion();
        IEnumerable<GestionDocentePlantillaMedioComunicacionDTO> ObtenerPlantillasMedioComunicacion();
        IEnumerable<GestionDocentePlantillaMedioComunicacionDTO> ObtenerPlantillasMedioComunicacionPorMedioComunicacion(int idMedioComunicacion);
        Task<ActividadCabeceraCompletaDTO> ObtenerActividadCabeceraCompletaAsync(int id);
        List<object> ObtenerDisparadorReglaTiempo();
    }
}
