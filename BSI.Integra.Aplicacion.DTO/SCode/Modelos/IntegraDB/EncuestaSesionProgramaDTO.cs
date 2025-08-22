using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class EncuestaSesionProgramaDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPespecificoSesion { get; set; }
        public int? IdEncuestaOnline { get; set; }
        public bool? EncuestaObligatoria { get; set; }
        public bool? EncuestaActiva { get; set; }
        public bool? AsignadoPara { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class EncuestaProgramaDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int NumeroEncuestas { get; set; }
    }
    public class EncuestaSesionProgramaEntradaDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public int IdPespecificoSesion { get; set; }
        public int IdEncuestaOnline { get; set; }
        public bool EncuestaObligatoria { get; set; }
        public bool EncuestaActiva { get; set; }
        public bool AsignadoPara { get; set; }
        public string Usuario { get; set; }
    }

    public class EncuestaSesionAsignadaDTO
    {
        public int? IdEncuestaSesionPrograma { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPespecificoSesion { get; set; }
        public int? IdEncuestaOnline { get; set; }
        public string? NombreEncuesta { get; set; }
        public bool? EncuestaObligatoria { get; set; }
        public bool? EncuestaActiva { get; set; }
        public bool? AsignadoPara { get; set; }
    }

}
