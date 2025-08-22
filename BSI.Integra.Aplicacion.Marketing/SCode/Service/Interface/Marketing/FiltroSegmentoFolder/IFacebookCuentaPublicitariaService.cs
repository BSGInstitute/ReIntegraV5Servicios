
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FacebookCuentaPublicitaria
{
    public interface IFacebookCuentaPublicitariaService
    {
        List<DTO.ComboDTO> ObtenerCombo();
         List<FacebookCuentaPublicitariaDTO> ObtenerComboFacebookCuentaPublicitaria();

    }
}
