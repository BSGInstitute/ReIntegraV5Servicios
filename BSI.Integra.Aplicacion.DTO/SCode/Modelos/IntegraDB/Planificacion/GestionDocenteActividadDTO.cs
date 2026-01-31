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
        public int IdGestionDocenteTipoActividadDetalle { get; set; }
        public int IdPlantillaMediaComunicacion { get; set; }
        public int IdGestionDocenteDetalleDisparador { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public GestionDocenteDetalleDisparadorDTO Disparador { get; set; }
    }

    public class GestionDocenteDetalleDisparadorDTO
    {
        public int Id { get; set; }
        public int IdGestionDocenteTipoDisparadorFlujo { get; set; }
        public List<int> IdsOcurrenciasPrevias { get; set; }
        public DateTime? Fecha { get; set; }
        public TimeSpan? Hora { get; set; }
        public int? CantidadTiempo { get; set; }
        public int? IdGestionDocenteUnidadTiempo { get; set; }
        public int? IdOcurrenciaActividadAnterior { get; set; }
    }

    public class GestionDocenteOcurrenciaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteTipoOcurrencia { get; set; }
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
