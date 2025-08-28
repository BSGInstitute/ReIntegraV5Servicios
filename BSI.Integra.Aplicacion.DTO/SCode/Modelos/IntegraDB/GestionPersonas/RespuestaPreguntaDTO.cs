using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class RespuestaPreguntaDTO
    {
        public int? id { get; set; }
        public int? IdPregunta { get; set; }
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

    public class RespuestaPreguntaFactorDesaprovatorioComboDTO
    {
        public int IdRespuestaDesaprovatoria { get; set; }
        public string Nombre { get; set; }
    }

    public class RespuestaPreguntaEntradaDTO
    {
        public int? Id { get; set; }
        public int IdPregunta { get; set; }
        public string? EnunciadoRespuesta { get; set; }
        public int NroOrden { get; set; }
        public int? Puntaje { get; set; }
        public string Usuario { get; set; } = null!;
    }

    public class PreguntaRespuestaAsincronicaDTO
    {
        public int IdRespuestaPregunta { get; set; }
        public int IdPregunta { get; set; }
        public string? EnunciadoRespuesta { get; set; }
        public int? NroOrden { get; set; }
        public decimal? Puntaje { get; set; }
    }

    public class PreguntaRespuesta {
        public int IdRespuestaPregunta { get; set; }
        public int IdPregunta { get; set; }
        public string? EnunciadoRespuesta { get; set; }
        public int? NroOrden { get; set; }
        public decimal? Puntaje { get; set; }
    }

    public class RespuestaPreguntaImportadaDTO {
        public IFormFile File { get; set; }
        public int IdPregunta { get; set; }
    }

}
