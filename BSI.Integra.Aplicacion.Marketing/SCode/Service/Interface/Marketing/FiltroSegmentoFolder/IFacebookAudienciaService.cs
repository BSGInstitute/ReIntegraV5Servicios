
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FacebookAudiencia
{
    public interface IFacebookAudienciaService
    {
        List<DTO.ComboDTO> ObtenerCombo();
        List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento);
         List<FacebookAudienciaComboDTO> ObtenerComboFacebookAudiencia();
        public List<FacebookAudienciaComboDTO> ObtenerComboListaPublico();

    }
}
