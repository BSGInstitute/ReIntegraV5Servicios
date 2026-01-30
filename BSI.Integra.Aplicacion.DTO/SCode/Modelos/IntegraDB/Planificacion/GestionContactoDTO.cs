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
}
