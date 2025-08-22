using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalArchivo : BaseIntegraEntity
    {
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string MimeType { get; set; }
        public bool? EsImagen { get; set; }
    }
}
