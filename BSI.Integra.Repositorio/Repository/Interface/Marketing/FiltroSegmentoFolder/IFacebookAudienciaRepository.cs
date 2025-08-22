
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookAudiencia
{
    public interface IFacebookAudienciaRepository
    {
        List<ComboDTO> ObtenerCombo();
        List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento);
         List<FacebookAudienciaComboDTO> ObtenerComboFacebookAudiencia();
        public List<FacebookAudienciaComboDTO> ObtenerComboListaPublico();


    }
}
