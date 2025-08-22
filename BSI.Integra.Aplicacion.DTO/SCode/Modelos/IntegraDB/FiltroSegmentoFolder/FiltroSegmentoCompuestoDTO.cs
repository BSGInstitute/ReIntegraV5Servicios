using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder
{
    public class FiltroSegmentoCompuestoDTO
    {
        public int IdAlumno { get; set; }
        public string Email1 { get; set; }
        public string? Email1Encriptado { get; set; }
        public string Email2 { get; set; }
        public string NombreAlumno { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string? CelularEncriptado { get; set; }
        public string NombrePais { get; set; }
        public string NombreCiudad { get; set; }
        public string NombreAreaFormacion { get; set; }
        public string NombreAreaTrabajo { get; set; }
        public string NombreIndustria { get; set; }
        public string NombreCargo { get; set; }
        public bool? EsVentaCruzada { get; set; }

        public string NombreCentroCosto { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdOportunidad { get; set; }
    }


    public class AreaSubAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdArea { get; set; }
    }
}
