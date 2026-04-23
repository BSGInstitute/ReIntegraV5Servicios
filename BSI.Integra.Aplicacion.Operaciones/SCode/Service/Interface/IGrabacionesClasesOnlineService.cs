using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IGrabacionesClasesOnlineService
    {
        List<GrabacionesClasesOnlineDTO> GenerarVistaProgramasOnline(GrabacionesClasesOnlineFiltroDTO filtro);
        List<SesionesClasesOnlineResumenDTO> ObtenerSesiones(SesionesFiltroDTO filtro);
        List<SesionesClasesOnlineDetalleResumenDTO> ObtenerDetalleResumenGrabacionSesion(SesionesFiltroDTO filtro);
        bool ActualizarSesiones(SesionesClasesOnlineModificarFiltroDTO filtro);
        bool ModificarDisponibilidadProgramaDefecto(DataDisponibilidadProgramaDefectoDTO filtro);
        List<DataDisponibilidadProgramaDefectoDTO> ObtenerDisponibilidadPrograma();
        PEspecificoUltimaSesionDTO ObtenerUltimaSesionPorIdPEspecifico(int idPEspecifico);
    }

}
