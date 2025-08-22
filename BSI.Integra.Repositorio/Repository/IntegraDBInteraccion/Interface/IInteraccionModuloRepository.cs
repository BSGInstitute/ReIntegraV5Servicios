using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface
{
    public interface IInteraccionModuloRepository
    {
        public bool RegistrarInteraccionModulo(RegistroInteraccionModuloDTO Model);
    }
}
