using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionContactoService
    {
        Task<int> ProcesarInsercionGestionAsync(CrearGestionContactoDTO dto);
        IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string valor);
        IEnumerable<ComboDTO> ObtenerPEspecificoPorCentroCosto(int idCentroCosto);
        IEnumerable<PEspecificoSesionProveedorDTO> ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico);
        IEnumerable<ComboDTO> ObtenerGestionDocenteFlujos();
        Task<int> InsertarOportunidadDocenteAsync(CrearOportunidadDocenteDTO dto);
        Task<int> ActualizarOportunidadDocenteAsync(ActualizarOportunidadDocenteDTO dto);
        IEnumerable<EstadoGestionContactoDTO> ObtenerEstadosGestionContacto();
        Task<int> InsertarGestionContactoDocenteFlujoAsync(InsertarGestionContactoDocenteFlujoDTO dto);
        Task<int> CongelarFlujoDocenteAsync(int idGestionContactoDocenteFlujo, DateTime? fechaInicioFlujoCongelado = null);
        Task<int> CongelarActividadPorSesionesAsync(CongelarActividadSesionesDTO dto, string usuarioCreacion);
        Task<int> AgregarActividadExtraCongeladaAsync(AgregarActividadExtraCongeladaDTO dto, string usuarioCreacion);
        OportunidadDocenteListResponseDTO ObtenerOportunidadesDocente(string busqueda, int pagina, int porPagina);
        IEnumerable<DocenteComboDTO> ObtenerDocentes();
        Task<ActividadesFlujoPorCategoriaResponseDTO> ObtenerActividadesFlujoPorCategoriaAsync(int idGestionContactoDocenteFlujo);

        // Métodos para Hangfire
        Task<List<ActividadPendienteDTO>> ObtenerActividadesPendientesAsync();
        Task<ResultadoEjecucionDTO> ActualizarEstadoActividadAsync(ActualizarEstadoRequestDTO request);
        Task<ResultadoEjecucionDTO> MarcarOcurrenciaAsync(MarcarOcurrenciaRequestDTO request);

        // Métodos para ejecución manual de actividades
        Task<ResultadoEjecucionDTO> EjecutarActividadManualmenteAsync(EjecutarActividadManualDTO request);
        Task<ResultadoEjecucionDTO> AdelantarFechaActividadAsync(AdelantarActividadDTO request);
        Task<List<ActividadDependienteDTO>> ObtenerActividadesDependientesAsync(int idActividadDetalleCongelada);
    }
}
