using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppDesuscritoService
    {
        bool ExistePorNumeroTelefono(string numero);
    }
}
