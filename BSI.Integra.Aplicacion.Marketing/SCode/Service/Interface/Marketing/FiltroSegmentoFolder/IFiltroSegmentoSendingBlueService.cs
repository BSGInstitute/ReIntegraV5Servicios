using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FiltroSegmentoFolder
{
    public interface IFiltroSegmentoSendingBlueService { 
        public List<FiltroSegmentoPanelDTO> ObtenerFiltroSegmentoPanel();
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultadoFiltroSegmento(int id, int idFiltroSegmentoTipoContacto);


    }
}
