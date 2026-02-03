using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class GestionDocenteFlujoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
    }

    public class GestionDocenteActividadCabeceraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
        public string Usuario { get; set; }
    }

    public class GestionDocenteActividadCabeceraFlujoDTO
    {
        public int Id { get; set; }
        public int IdGestionDocenteFlujo { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public bool Estado { get; set; }
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

    public class GestionDocenteDisparadorReglaTiempoRelativoDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class GestionDocenteDisparadorReglaTiempoFijoDTO
    {
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int Cantidad { get; set; }
        public int IdGestionDocenteUnidadTiempo { get; set; }

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

    public class MaestroGestionDocenteActividadDTO
    {
        public GestionDocenteActividadCabeceraDTO Cabecera { get; set; }
        public List<GestionDocenteActividadDetalleDTO> Detalles { get; set; }
        public List<GestionDocenteOcurrenciaDTO> Ocurrencias { get; set; }
    }
}
