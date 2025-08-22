using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class PreguntaEncuestaRespuestaDTO 
    {
        public int Id { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public string? Respuesta { get; set; }
        public int? Orden { get; set; }
        public decimal? Puntaje { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
    public class PreguntaEncuestaRespuestaEntradaDTO
    {
        public int? Id { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public string? Respuesta { get; set; }
        public int? Orden { get; set; }
        public decimal? Puntaje { get; set; }
        public string Usuario { get; set; } = null!;
    }

    public class PreguntaRespuestaDTO
    {
        public int IdPreguntaEncuestaRespuesta { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public string? Respuesta { get; set; }
        public int? Orden { get; set; }
        public decimal? Puntaje { get; set; }
    }

}
