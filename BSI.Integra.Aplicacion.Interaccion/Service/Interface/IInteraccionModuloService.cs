using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Interface
{
    public interface IInteraccionModuloService
    {
        public bool RegistrarInteraccionModulo(RegistroInteraccionModuloDTO Model);
    }
}
