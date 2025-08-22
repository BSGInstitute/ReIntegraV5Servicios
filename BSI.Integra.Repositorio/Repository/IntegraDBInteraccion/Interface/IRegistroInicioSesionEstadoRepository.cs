using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface
{
    public interface IRegistroInicioSesionEstadoRepository
    {
        public List<RegistroInicioSesionEstadoDTO> Obtener();
        public bool RegistrarInicioSesionEstado(RegistroInicioSesionEstadoLogueoDTO Model);
    }
}
