using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ConfirmacionWebinarAutomaticaDTO
    {
        public int IdPEspecificoSesion { get; set; }
    }
    public class RptaConfirmacionWebinarAutomaticaDTO
    { 
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
    }
    public class WebinarAlumnoAsistenciaDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool EstadoAsistencia { get; set; }
    }

    public class AsistenciaConfirmacionWebinarDTO
    {
        public int? Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public bool Confirmo { get; set; }
        public bool Asistio { get; set; }
        public bool? EnvioCorreoWebinar { get; set; }
        public bool? EnvioWhatsAppWebinar { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion {get; set;}
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
