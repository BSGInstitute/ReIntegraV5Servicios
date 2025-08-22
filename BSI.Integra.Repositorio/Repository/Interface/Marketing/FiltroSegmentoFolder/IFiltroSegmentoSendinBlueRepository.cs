using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.FiltroSegmentoFolder
{
    public interface IFiltroSegmentoSendinBlueRepository
    {
        List<FiltroSegmentoPanelDTO> ObtenerFiltroSegmentoPanel();
        List<FiltroSegmentoCompuestoDTO> ObtenerResultadoFiltroSegmento(int id, int idFiltroSegmentoTipoContacto);

    }
}
