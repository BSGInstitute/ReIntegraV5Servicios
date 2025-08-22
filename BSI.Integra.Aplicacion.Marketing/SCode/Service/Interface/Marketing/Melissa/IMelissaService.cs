using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Melissa.MelissaDTO;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Melissa
{
    public interface IMelissaService
    {
        Task<MelissaVerificacionDTO> ValidarNumero(string numero, int? idCodigoPais);
        //bool InsertarLogMelissa(string numero, int? idCodigoPais, MelissaVerificacionDTO resultado);
    }
}
