using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IGestionDocenteAgendaRepository : IGenericRepository<TProveedor>
    {
        List<DocenteConCursoDTO> ObtenerDocentesConCursos();
        DocenteAgendaCabeceraDTO ObtenerCabeceraDocente(int idProveedor);
        DocenteAgendaFlujoDTO ObtenerFlujoDocente(int idGestionContacto);
        List<DocenteAgendaCronogramaDTO> ObtenerCronogramasDocente(int idProveedor, int idPEspecificoPrioridad);
        List<DocenteAgendaSesionDTO> ObtenerSesionesPorCursoYDocente(int idProveedor, int idPEspecifico);
    }
}
