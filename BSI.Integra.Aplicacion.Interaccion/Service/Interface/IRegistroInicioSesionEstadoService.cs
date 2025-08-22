using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Interface
{
    public interface IRegistroInicioSesionEstadoService
    {
        public List<RegistroInicioSesionEstadoDTO> Obtener();
        public bool RegistrarInicioSesionEstado(RegistroInicioSesionEstadoLogueoDTO Model);
    }
}
