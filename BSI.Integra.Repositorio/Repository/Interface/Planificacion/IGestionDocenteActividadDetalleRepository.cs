using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteActividadDetalleRepository : IGenericRepository<TGestionDocenteActividadDetalle>
    {
        TGestionDocenteActividadDetalle Add(GestionDocenteActividadDetalle entidad);
        TGestionDocenteActividadDetalle Update(GestionDocenteActividadDetalle entidad);
        IEnumerable<GestionDocenteSesionDTO> ObtenerSesiones();
        IEnumerable<GestionDocenteConfianzaUmbralNivelDTO> ObtenerConfianzaUmbralNiveles();
        IEnumerable<GestionDocenteOcurrenciaTipoDTO> ObtenerOcurrenciaTipos();
        IEnumerable<GestionDocenteReferenciaTiempoDTO> ObtenerReferenciasTiempo();
        IEnumerable<GestionDocenteActividadDetalleTipoDTO> ObtenerActividadDetalleTipos();
        IEnumerable<GestionDocenteDisparadorFlujoTipoDTO> ObtenerDisparadorFlujoTipos();
        IEnumerable<GestionDocenteUnidadTiempoDTO> ObtenerUnidadesTiempo();
        IEnumerable<OcurrenciaReferenciaDTO> ObtenerOcurrenciasReferencia();
        IEnumerable<GestionDocenteDisparadorDetalleOutputDTO> ObtenerDisparadorReglaTiempo();
        IEnumerable<GestionDocenteModoMarcadoDTO> ObtenerModosMarcado();
        IEnumerable<GestionDocenteMedioComunicacionDTO> ObtenerMediosComunicacion();
        IEnumerable<GestionDocentePlantillaMedioComunicacionDTO> ObtenerPlantillasMedioComunicacion();
        IEnumerable<GestionDocenteActividadDetalleOutputDTO> ObtenerDetallesPorCabecera(int idCabecera);
        IEnumerable<GestionDocenteDisparadorDetalleOutputDTO> ObtenerDisparadoresPorIds(string ids);
        IEnumerable<GestionDocenteDisparadorReglaTiempoFijoOutputDTO> ObtenerReglasTiempoFijoPorDisparadores(string ids);
        IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoOutputDTO> ObtenerReglasTiempoRelativoPorDisparadores(string ids);
        IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO> ObtenerReferenciasRelativasPorReglas(string ids);
        IEnumerable<GestionDocenteDisparadorOcurrenciaDetalleOutputDTO> ObtenerDisparadoresOcurrenciaPorIds(string ids);
        IEnumerable<GestionContactoActividadDetalleSesionDTO> ObtenerSesionesPorDetalles(string ids);
        IEnumerable<GestionDocenteOcurrenciaOutputDTO> ObtenerOcurrenciasPorDetalles(string ids);
        IEnumerable<OcurrenciaIaConfiguracionCompletaDTO> ObtenerIaConfiguracionesPorOcurrencias(string ids);
        IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO> ObtenerEjemplosEntrenamientoPorConfiguraciones(string ids);

        Task<IEnumerable<GestionDocenteActividadDetalleOutputDTO>> ObtenerDetallesPorCabeceraAsync(int idCabecera);
        Task<IEnumerable<GestionDocenteDisparadorDetalleOutputDTO>> ObtenerDisparadoresPorIdsAsync(string ids);
        Task<IEnumerable<GestionDocenteDisparadorReglaTiempoFijoOutputDTO>> ObtenerReglasTiempoFijoPorDisparadoresAsync(string ids);
        Task<IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoOutputDTO>> ObtenerReglasTiempoRelativoPorDisparadoresAsync(string ids);
        Task<IEnumerable<GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO>> ObtenerReferenciasRelativasPorReglasAsync(string ids);
        Task<IEnumerable<GestionDocenteDisparadorOcurrenciaDetalleOutputDTO>> ObtenerDisparadoresOcurrenciaPorIdsAsync(string ids);
        Task<IEnumerable<GestionContactoActividadDetalleSesionDTO>> ObtenerSesionesPorDetallesAsync(string ids);
        Task<IEnumerable<GestionDocenteOcurrenciaOutputDTO>> ObtenerOcurrenciasPorDetallesAsync(string ids);
        Task<IEnumerable<OcurrenciaIaConfiguracionCompletaDTO>> ObtenerIaConfiguracionesPorOcurrenciasAsync(string ids);
        Task<IEnumerable<GestionDocenteIaEntrenamientoEjemploOutputDTO>> ObtenerEjemplosEntrenamientoPorConfiguracionesAsync(string ids);
    }
}
