using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class PreguntaEncuestaDTO
    {
        public int Id { get; set; }
        public int IdPreguntaEncuestaCategoria { get; set; }
        public int IdPreguntaEncuestaTipo { get; set; }
        public string? Pregunta { get; set; }
        public bool? ActivarDescripcion { get; set; }
        public string? Descripcion { get; set; } = null!;
        public bool? PreguntaObligatoria { get; set; }
        public bool? PreguntaActiva { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class PreguntaEncuestaEntradaDTO
    {
        public int? Id { get; set; }
        public int IdPreguntaEncuestaCategoria { get; set; }
        public int IdPreguntaEncuestaTipo { get; set; }
        public string? Pregunta { get; set; }
        public bool? ActivarDescripcion { get; set; }
        public string? Descripcion { get; set; } = null!;
        public bool? PreguntaObligatoria { get; set; }
        public bool? PreguntaActiva { get; set; }
        public string Usuario { get; set; }
    }

    public class BancoPreguntaEncuestaDTO
    {
        public int IdPreguntaEncuesta { get; set; }
        public int IdPreguntaEncuestaCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdPreguntaEncuestaTipo { get; set; }
        public string Tipo { get; set; }
        public string Pregunta { get; set; }
        public string? Descripcion { get; set; } = null!;
        public bool ActivarDescripcion { get; set; }
        public bool PreguntaObligatoria { get; set; }
        public bool PreguntaActiva { get; set; }
    }

    public class PreguntaEncuestaAsincronicaDTO
    {
        public int IdPregunta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int IdTipoRespuesta { get; set; }
        public string TipoRespuesta { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string PreguntaTipo { get; set; }
    }

    public class BancoPreguntaEncuestaAsincronicaDTO
    {
        public int IdAsignacionPreguntaExamen { get; set; }
        public string NombreEncuesta { get; set; }
        public int IdPregunta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int IdTipoRespuesta { get; set; }
        public string TipoRespuesta { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string PreguntaTipo { get; set; }
    }

}

