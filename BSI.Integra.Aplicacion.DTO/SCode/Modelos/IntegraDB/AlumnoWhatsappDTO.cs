using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AlumnoWhatsappDTO
    {
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
    }
    public class DatosAlumnoWhatsappDTO
    {
        public int? Id { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Email1 { get; set; }
        public string? Email2 { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdAFormacion { get; set; }
        public int? IdATrabajo { get; set; }
        public int? IdCargo { get; set; }
        //public bool? Desuscrito { get; set; }
        //public bool? Archivado { get; set; }
    }

 

}