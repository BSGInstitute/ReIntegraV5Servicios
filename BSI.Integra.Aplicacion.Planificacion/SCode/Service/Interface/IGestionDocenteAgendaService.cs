using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteAgendaService
    {
        List<DocenteConCursoDTO> ObtenerDocentesConCursos();
        DetalleDocenteAgendaDTO ObtenerDetalleDocente(int idProveedor, int idPEspecifico, int? idGestionContacto);
    }
}
