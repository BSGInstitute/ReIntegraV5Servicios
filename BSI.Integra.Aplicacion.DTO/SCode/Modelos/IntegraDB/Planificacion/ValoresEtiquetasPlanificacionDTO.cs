using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ValoresEtiquetasPlanificacionDTO
    {
        public string NombreDocente { get; set; }
        public string NombreCurso { get; set; }
        public string NombreCursoProgramaHijo { get; set; }
        public string PlazoOtorgado { get; set; }
        public string FechaPrimeraSesion { get; set; }
        public string NombrePais { get; set; }
        public string Tarifa { get; set; }
        public string Moneda { get; set; }
        public string PlazoPago { get; set; }
    }
}