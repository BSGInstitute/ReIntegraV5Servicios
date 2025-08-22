using BSI.Integra.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface ISexoService
    {
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
