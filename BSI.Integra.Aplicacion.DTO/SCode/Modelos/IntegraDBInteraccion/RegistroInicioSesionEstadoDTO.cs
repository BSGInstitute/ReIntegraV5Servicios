using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion
{
    public class RegistroInicioSesionEstadoDTO
    {
    }
    public class RegistroInicioSesionEstadoLogueoDTO
    {
        public int IdRegistroInicioSesion { get; set; }
        public string TokenGenerada { get; set; }
        public bool InicioSesionCorrecta { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    }
}
