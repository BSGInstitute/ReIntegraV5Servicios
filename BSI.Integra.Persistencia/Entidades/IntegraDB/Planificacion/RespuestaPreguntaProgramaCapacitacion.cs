using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class RespuestaPreguntaProgramaCapacitacion : BaseIntegraEntity
    {
        public int? IdPreguntaProgramaCapacitacion { get; set; } 
        public bool? RespuestaCorrecta { get; set; }
        [Required]
        public int NroOrden { get; set; } 
        public string? EnunciadoRespuesta { get; set; } 
        public int? NroOrdenRespuesta { get; set; } 
        public int? Puntaje { get; set; } 
        public string? FeedbackPositivo { get; set; } 
        public string? FeedbackNegativo { get; set; } 
        public bool? MostrarFeedBack { get; set; } 
        public int? PuntajeTipoRespuesta { get; set; }
    }
}
