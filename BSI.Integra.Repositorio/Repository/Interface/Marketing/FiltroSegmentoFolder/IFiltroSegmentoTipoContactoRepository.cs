
using BSI.Integra.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.FiltroSegmentoTipoContacto
{
    public interface IFiltroSegmentoTipoContactoRepository
    {
        List<ComboDTO> ObtenerTodoFiltro();
    }
}
