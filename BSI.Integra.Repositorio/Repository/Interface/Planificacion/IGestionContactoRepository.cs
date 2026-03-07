using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionContactoRepository : IGenericRepository<TGestionContacto>
    {
        TGestionContacto AddAsync(GestionContacto entidad);
        Task<bool> ExisteCentroCostoAsync(int id);
        Task<bool> ExisteClasificacionPersonaAsync(int id);
        Task<bool> ExisteFaseGestionAsync(int id);
        Task<bool> ExisteGestionActivaAsync(int idDocente, int idCentroCosto);
        Task<bool> ExisteOrigenAsync(int id);
        Task<bool> ExistePersonalAsync(int id);
        Task<GestionContactoSimpleDTO> ObtenerPorIdAsync(int id);
        TGestionContacto Update(GestionContacto entidad);
        IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string nombreParcial);
        IEnumerable<ComboDTO> ObtenerPEspecificoPorCentroCosto(int idCentroCosto);
        IEnumerable<PEspecificoSesionProveedorDTO> ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico);
        IEnumerable<ComboDTO> ObtenerGestionDocenteFlujos();
        ProveedorClasificacionDTO ObtenerClasificacionPorProveedor(int idProveedor);
        IEnumerable<EstadoGestionContactoDTO> ObtenerEstadosGestionContacto();
        TGestionContactoDocenteFlujo InsertarGestionContactoDocenteFlujo(InsertarGestionContactoDocenteFlujoDTO dto);
        Task<int> CongelarFlujoDocenteAsync(int idGestionContactoDocenteFlujo, DateTime? fechaInicioFlujoCongelado = null);
        OportunidadDocenteListResponseDTO ObtenerOportunidadesDocente(string busqueda, int pagina, int porPagina);
        IEnumerable<DocenteComboDTO> ObtenerDocentes();
        Task<ActividadesFlujoPorCategoriaResponseDTO> ObtenerActividadesFlujoPorCategoriaAsync(int idGestionContactoDocenteFlujo);

        // Métodos para Hangfire
        Task<List<ActividadPendienteDTO>> ObtenerActividadesPendientesAsync();
        Task<ResultadoEjecucionDTO> ActualizarEstadoActividadAsync(ActualizarEstadoRequestDTO request);
        Task<ResultadoEjecucionDTO> MarcarOcurrenciaAsync(MarcarOcurrenciaRequestDTO request);

        // Métodos para ejecución manual
        Task<ActividadPendienteDTO> ObtenerActividadParaEjecucionManualAsync(int idActividadDetalleCongelada, int idDisparadorCongelado);
        Task<ResultadoEjecucionDTO> EjecutarActividadManualmenteAsync(EjecutarActividadManualDTO request);
        Task<ResultadoEjecucionDTO> AdelantarFechaActividadAsync(AdelantarActividadDTO request);
        Task<List<ActividadDependienteDTO>> ObtenerActividadesDependientesAsync(int idActividadDetalleCongelada);
    }
}
