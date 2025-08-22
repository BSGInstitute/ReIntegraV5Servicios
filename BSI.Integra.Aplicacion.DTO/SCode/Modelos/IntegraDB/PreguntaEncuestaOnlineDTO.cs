using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class PreguntaEncuestaOnlineDTO
    {
        public int Id { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public int IdEncuestaOnline { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class PreguntaEncuestaOnlineEntradaDTO
    {
        public int? Id { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public int IdEncuestaOnline { get; set; }
        public string Usuario { get; set; }
    }

    public class PreguntaAsociadaEncuestaOnlineDTO
    {
        public int IdPreguntaEncuesta { get; set; }
        public int? IdEncuestaOnline { get; set; }
        public int? IdPreguntaEncuestaOnline { get; set; }
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

   

}
