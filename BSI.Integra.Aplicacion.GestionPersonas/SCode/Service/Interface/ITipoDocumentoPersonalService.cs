using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface ITipoDocumentoPersonalService
    {

        IEnumerable<TipoDocumentoPersonalComboDTO> ObtenerComboDocumentos();

    }
}
