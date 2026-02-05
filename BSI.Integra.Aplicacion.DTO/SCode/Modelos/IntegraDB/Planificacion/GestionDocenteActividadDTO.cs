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
}
