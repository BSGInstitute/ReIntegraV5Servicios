using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Melissa.MelissaDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Melissa
{
    public interface IMelissaRepository
    {
        List<CodigoPaisIsoDTO> ObtenerIsoPais(int? idCodigoPais);
        bool InsertarMelissaLog(string numero, int? idCodigoPais, string resultado);
        Task<MelissaVerificacionDTO> ValidarNumero(string numero, int? idCodigoPais);
    }
}
