using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class RespuestaPreguntaProgramaCapacitacionDTO
    {
        public int Id { get; set; } 
        public int? IdPreguntaProgramaCapacitacion { get; set; } 
        public bool? RespuestaCorrecta { get; set; } 
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
