using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EncuestaSesionPrograma : BaseIntegraEntity
    {

        public int? IdPgeneral { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdPespecificoSesion { get; set; }
        public int? IdEncuestaOnline { get; set; }
        public bool? EncuestaObligatoria { get; set; }
        public bool? EncuestaActiva { get; set; }
        public bool? AsignadoPara { get; set; }
    }
}
