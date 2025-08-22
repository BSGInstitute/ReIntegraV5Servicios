using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion
{
    public class InteraccionModulo
    {
        public int IdUsuario { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public int FechaInteraccionEntera { get; set; }
        public int HoraInteraccionEntera { get; set; }
        public string UrlAnterior { get; set; }
        public string UrlActual { get; set; }
        public string IpPublica { get; set; }
        public string IpLocal { get; set; }
        public string DireccionMac { get; set; }
        public string ControlTipo { get; set; }
        public string ControlNombre { get; set; }
        public string Contenido { get; set; }
        public string NombreModulo { get; set; }
        public string InteraccionJson { get; set; }
    }
}
