using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGrabacionesClasesOnlineRepository
    {
        List<GrabacionesClasesOnlineDTO> GenerarVistaProgramasOnline(GrabacionesClasesOnlineFiltroDTO filtro);
        List<SesionesClasesOnlineResumenDTO> ObtenerSesiones(SesionesFiltroDTO filtro);
        List<SesionesClasesOnlineDetalleResumenDTO> ObtenerDetalleResumenGrabacionSesion(SesionesFiltroDTO filtro);
        List<DataDisponibilidadProgramaDefectoDTO> ObtenerDisponibilidadPrograma();
        bool ActualizarSesiones(SesionesClasesOnlineModificarFiltroDTO filtro);
        bool ModificarDisponibilidadProgramaDefecto(DataDisponibilidadProgramaDefectoDTO filtro);
        PEspecificoUltimaSesionDTO ObtenerUltimaSesionPorIdPEspecifico(int idPEspecifico);
    }
}
