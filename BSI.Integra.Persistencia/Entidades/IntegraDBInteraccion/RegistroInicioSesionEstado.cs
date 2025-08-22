using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion
{
    public class RegistroInicioSesionEstado : BaseIntegraEntity
    {
        public int IdRegistroInicioSesion { get; set; }
        public string TokenGenerada { get; set; }
        public bool InicioSesionCorrecta { get; set; }
        public string Descripcion { get; set; }
    }
}
