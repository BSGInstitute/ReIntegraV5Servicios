using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class FinalizarProgramarGestionPlaDTO
    {
        public ActividadGestionAntiguaDTO ActividadAntigua { get; set; }
        public DatosGestionDocenteDTO DatosGestion { get; set; }
        public DatosFiltroFinalizarActividadDTO? Filtro { get; set; }
    }

    public class ActividadGestionAntiguaDTO
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public int IdOcurrencia { get; set; }
        public int IdOcurrenciaActividad { get; set; }
        public int IdProveedor { get; set; }
        public int IdGestionContacto { get; set; }
    }

    public class DatosGestionDocenteDTO
    {
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdFaseGestionContacto { get; set; }
        public int? IdOrigen { get; set; }
        public string? UltimoComentario { get; set; }
        public int? IdEstadoGestionContacto { get; set; }
        public int? IdSubEstadoGestionContacto { get; set; }
        public bool? EstadoSeguimientoWhatsApp { get; set; }
        public string? UltimaFechaProgramada { get; set; }
    }
    public class CrearGestionContactoDTO
    {
        public int? IdCentroCosto { get; set; }
        public int IdPersonal_Asignado { get; set; }    // Asesor
        public int IdClasificacionPersona { get; set; } // Docente
        public int IdFaseGestionContacto { get; set; }
        public int IdOrigen { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Comentario { get; set; }
        public int IdEstadoGestionContacto { get; set; }
    }

    public class PEspecificoSesionProveedorDTO
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
    }

    public class CrearOportunidadDocenteDTO
    {
        public int? IdCentroCosto { get; set; }
        public int IdProveedor { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class ProveedorClasificacionDTO
    {
        public int IdClasificacionPersona { get; set; }
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
    }

    public class EstadoGestionContactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class InsertarGestionContactoDocenteFlujoDTO
    {
        public int IdGestionContacto { get; set; }
        public int IdGestionDocenteFlujo { get; set; }
        public string UsuarioCreacion { get; set; }
    }

    public class OportunidadDocenteListItemDTO
    {
        public int Id { get; set; }
        public int? DocenteId { get; set; }
        public string DocenteNombre { get; set; }
        public string TipoOportunidad { get; set; }
        public string Curso { get; set; }
        public string FlujoAsignado { get; set; }
    }

    public class OportunidadDocenteListResponseDTO
    {
        public IEnumerable<OportunidadDocenteListItemDTO> Oportunidades { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int PorPagina { get; set; }
    }

    // ===== DTOs para Actividades de Flujo por Categoría =====

    public class ActividadesFlujoPorCategoriaResponseDTO
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public IEnumerable<SesionConActividadesDTO> Sesiones { get; set; }
        public IEnumerable<ActividadCabeceraDTO> Actividades { get; set; }
    }

    public class SesionConActividadesDTO
    {
        public int IdSesion { get; set; }
        public int NumeroSesion { get; set; }
        public DateTime? FechaInicioSesion { get; set; }
        public int? IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public IEnumerable<ActividadCabeceraDTO> Actividades { get; set; }
    }

    public class ActividadCabeceraDTO
    {
        public int IdGestionDocenteActividadCabeceraCongelada { get; set; }
        public string NombreCabecera { get; set; }
        public string DescripcionCabecera { get; set; }
        public IEnumerable<ActividadDetalleDTO> Detalles { get; set; }
    }

    public class ActividadDetalleDTO
    {
        public int IdGestionDocenteActividadDetalleCongelada { get; set; }
        public string NombreDetalle { get; set; }
        public string NombrePlantilla { get; set; }
        public string MedioComunicacion { get; set; }
        public string EstadoEjecucionDetalle { get; set; }
        public IEnumerable<DisparadorDTO> Disparadores { get; set; }
    }

    public class DisparadorDTO
    {
        public int IdDisparadorCongelado { get; set; }
        public string TipoDisparador { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaFija { get; set; }
        public int? CantidadTiempoRelativo { get; set; }
        public string UnidadTiempo { get; set; }
        public string CodigoReferenciaTiempo { get; set; }
        public string NombreReferenciaTiempo { get; set; }
        public string NombreEvento { get; set; }
        public string OcurrenciaPrevia { get; set; }
        public string EstadoEjecucionDisparador { get; set; }
        public bool TieneFechaFija { get; set; }
        public bool TieneTiempoRelativo { get; set; }
        public bool TieneEvento { get; set; }
        public bool TieneOcurrenciaPrevia { get; set; }
    }
}
