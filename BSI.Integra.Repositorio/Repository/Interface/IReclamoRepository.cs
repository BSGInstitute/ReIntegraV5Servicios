using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReclamoRepository : IGenericRepository<TReclamo>
    {
        TReclamo  Add (Reclamo entidad);
        List<ListarReclamosDTO> ListarReclamosAlumno(int idMatricula);
        List<ListarReclamosDTO> ListarReclamos();
        List<ListarReclamosDTO> ObtenerReclamosPorAlumno(int idMatricula);
        List<ListarReclamosAreasDTO> ListarReclamosAreas();
        ListarReclamosAreasDTO InsertarReclamosArea(ReclamoAreasDTO listadoBO);
        List<ListarReclamosDTO> GenerarReporteReclamo(ReclamoFiltroDTO listadoBO);
        List<registroTipoReclamoAlumnoDTO> ObtenerListaTipoReclamoAlumno();
        Boolean ConfirmarReclamo(int id, string usuario,string comentario);
        Boolean EnviarReclamo(int id, string usuario);
        Boolean EliminarReclamo(int id, string usuario);

        Boolean ReclamoSinContacto(int id, string usuario);
        Boolean ResolverReclamoAreas(ReclamoSolucionDTO reclamo);
        Reclamo ObtenerPorId(int id);
        TReclamo Update(Reclamo entidad);
        bool Delete(int id, string usuario);

    }
}
