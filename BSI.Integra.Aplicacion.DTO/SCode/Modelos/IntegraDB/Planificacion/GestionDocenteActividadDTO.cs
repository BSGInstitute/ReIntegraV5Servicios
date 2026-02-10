using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class GestionDocenteActividadCabeceraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
        public string Usuario { get; set; }
    }

    public class GestionDocenteActividadDetalleDTO
    {
        public int Id { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public int IdGestionDocenteActividadDetalleTipo { get; set; }
        public int IdPlantillaMedioComunicacion { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
    }

    public class GestionDocenteDisparadorDetalleDTO
    {
        public int Id { get; set; }
        public int IdGestionDocenteDisparadorFlujoTipo { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoFijoDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoRelativoDTO
    {
        public int Id { get; set; }
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int Cantidad { get; set; }
        public int IdGestionDocenteUnidadTiempo { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoRelativoReferenciaDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        public int IdGestionDocenteReferenciaTiempo { get; set; }
    }

    public class GestionDocenteDisparadorOcurrenciaDetalleDTO
    {
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int IdGestionDocenteOcurrenciaPrevia { get; set; }
    }

    public class GestionDocenteOcurrenciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteOcurrenciaTipo { get; set; }
        public int IdGestionDocenteActividadDetalle { get; set; }
        public int IdGestionDocenteModoMarcado { get; set; }
        public bool RequiereComentario { get; set; }
        public bool RequiereFechaHora { get; set; }
        public string Usuario { get; set; }
    }

    public class InsertarActividadDetalleRequestDTO
    {
        public GestionDocenteActividadDetalleDTO Detalle { get; set; }
        public GestionDocenteDisparadorDetalleDTO Disparador { get; set; }
        public GestionDocenteDisparadorReglaTiempoFijoDTO? ReglaTiempoFijo { get; set; }
        public GestionDocenteDisparadorReglaTiempoRelativoDTO? ReglaTiempoRelativo { get; set; }
        public GestionDocenteDisparadorOcurrenciaDetalleDTO? OcurrenciaDetalle { get; set; }
        public GestionDocenteDisparadorReglaTiempoRelativoReferenciaDTO? ReferenciaRelativa { get; set; }
        public int? IdGestionDocenteSesion { get; set; }
    }

    public class GestionDocenteOcurrenciaIaConfiguracionDTO
    {
        public string Prompt { get; set; }
        public int IdGestionDocenteConfianzaUmbralNivel { get; set; }
    }

    public class GestionDocenteIaEntrenamientoEjemploDTO
    {
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        public int IdGestionDocenteIaEntrenamientoClasificacionTipo { get; set; }
        public string TextoEjemplo { get; set; }
        public bool EsPositivo { get; set; }
    }

    public class InsertarOcurrenciaRequestDTO
    {
        public GestionDocenteOcurrenciaDTO Ocurrencia { get; set; }
        public GestionDocenteOcurrenciaIaConfiguracionDTO? IaConfiguracion { get; set; }
        public List<GestionDocenteIaEntrenamientoEjemploDTO>? EjemplosEntrenamiento { get; set; }
    }

    public class MaestroGestionDocenteActividadDTO
    {
        public GestionDocenteActividadCabeceraDTO Cabecera { get; set; }
        public List<InsertarActividadDetalleRequestDTO> Detalles { get; set; }
        public List<InsertarOcurrenciaRequestDTO> Ocurrencias { get; set; }
    }

    public class GestionDocenteConfianzaUmbralNivelDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class GestionDocenteOcurrenciaTipoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class GestionDocenteReferenciaTiempoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }

    public class GestionDocenteActividadDetalleTipoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class GestionDocenteModoMarcadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class GestionDocenteMedioComunicacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class GestionDocentePlantillaMedioComunicacionDTO
    {
        public int IdPlantillaMedioComunicacion { get; set; }
        public int IdPlantilla { get; set; }
        public string NombrePlantilla { get; set; }
        public int IdMedioComunicacion { get; set; }
        public string NombreMedioComunicacion { get; set; }
    }

    public class GestionContactoActividadDetalleSesionDTO
    {
        public int IdGestionContactoActividadDetalleSesion { get; set; }
        public int IdGestionDocenteActividadDetalle { get; set; }
        public int IdGestionDocenteSesion { get; set; }
        public string NombreSesion { get; set; }
    }

    public class GestionDocenteActividadCabeceraOutputDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public string NombreEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
        public string NombreCategoria { get; set; }
    }

    public class GestionDocenteActividadDetalleOutputDTO
    {
        public int IdGestionDocenteActividadDetalle { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public int IdGestionDocenteActividadDetalleTipo { get; set; }
        public string NombreActividadDetalleTipo { get; set; }
        public int IdPlantillaMedioComunicacion { get; set; }
        public string NombrePlantilla { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public string NombreActividadDetalle { get; set; }
    }

    public class GestionDocenteOcurrenciaOutputDTO
    {
        public int IdGestionDocenteOcurrencia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteOcurrenciaTipo { get; set; }
        public string NombreOcurrenciaTipo { get; set; }
        public int IdGestionDocenteActividadDetalle { get; set; }
        public int IdGestionDocenteModoMarcado { get; set; }
        public string NombreModoMarcado { get; set; }
        public bool RequiereComentario { get; set; }
        public bool RequiereFechaHora { get; set; }
    }

    public class OcurrenciaIaConfiguracionCompletaDTO
    {
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        public string Prompt { get; set; }
        public int IdGestionDocenteConfianzaUmbralNivel { get; set; }
        public string NombreConfianzaUmbralNivel { get; set; }
        public int IdGestionDocenteOcurrencia { get; set; }
        public List<GestionDocenteIaEntrenamientoEjemploOutputDTO> EjemplosEntrenamiento { get; set; }
    }

    public class GestionDocenteDisparadorDetalleOutputDTO
    {
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int IdGestionDocenteDisparadorFlujoTipo { get; set; }
        public string NombreDisparadorFlujoTipo { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoFijoOutputDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoRegla { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoRelativoOutputDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int Cantidad { get; set; }
        public int IdGestionDocenteUnidadTiempo { get; set; }
        public string TipoRegla { get; set; }
        public string NombreUnidadTiempo { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempoRelativo { get; set; }
        public int IdGestionDocenteReferenciaTiempo { get; set; }
        public string NombreReferenciaTiempo { get; set; }
    }

    public class GestionDocenteDisparadorOcurrenciaDetalleOutputDTO
    {
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int IdGestionDocenteOcurrenciaPrevia { get; set; }
        public string NombreOcurrenciaPrevia { get; set; }
    }

    public class GestionDocenteIaEntrenamientoEjemploOutputDTO
    {
        public int IdGestionDocenteOcurrenciaIaConfiguracion { get; set; }
        public int IdGestionDocenteIaEntrenamientoClasificacionTipo { get; set; }
        public string TextoEjemplo { get; set; }
        public bool EsPositivo { get; set; }
        public string NombreClasificacionTipo { get; set; }
    }

    public class OcurrenciaCompletaDTO
    {
        public GestionDocenteOcurrenciaOutputDTO Ocurrencia { get; set; }
        public OcurrenciaIaConfiguracionCompletaDTO IaConfiguracion { get; set; }
    }

    public class DisparadorDetalleCompletoDTO
    {
        public GestionDocenteDisparadorDetalleOutputDTO DisparadorDetalle { get; set; }
        public GestionDocenteDisparadorReglaTiempoFijoOutputDTO ReglaTiempoFijo { get; set; }
        public GestionDocenteDisparadorReglaTiempoRelativoOutputDTO ReglaTiempoRelativo { get; set; }
        public GestionDocenteDisparadorReglaTiempoRelativoReferenciaOutputDTO ReferenciaRelativa { get; set; }
        public GestionDocenteDisparadorOcurrenciaDetalleOutputDTO OcurrenciaDetalle { get; set; }
    }

    public class ActividadDetalleCompletoDTO
    {
        public GestionDocenteActividadDetalleOutputDTO Detalle { get; set; }
        public DisparadorDetalleCompletoDTO Disparador { get; set; }
        public GestionContactoActividadDetalleSesionDTO Sesion { get; set; }
        public List<OcurrenciaCompletaDTO> Ocurrencias { get; set; }
    }

    public class ActividadCabeceraCompletaDTO
    {
        public GestionDocenteActividadCabeceraOutputDTO Cabecera { get; set; }
        public List<ActividadDetalleCompletoDTO> Detalles { get; set; }
    }
}
