using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public  class PersonalArchivoDTO
    {
        public int? Id { get; set; } 
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string MimeType { get; set; }
        public bool? EsImagen { get; set; }
    }
}
