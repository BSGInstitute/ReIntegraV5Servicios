using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICorreoGmailRepository : IGenericRepository<TCorreoGmail>
    {
        #region Metodos Base
        TCorreoGmail Add(CorreoGmail entidad);
        bool Update(CorreoGmail entidad);
        bool Delete(int id, string usuario);
        #endregion
        List<CorreoDTO> FiltroCorreosPorPersona(int idFolder, string queryFiltro);
        List<CorreoDTO> FiltroCorreosPorPersonaGmailCorreo(string queryFiltro);
        int ContadorCorreosPorPersona(int idPersonal, int idFolder);
        ListaCorreosGrupoDTO ObtenerCorreosGruposSinVersion(int idCentroCosto, List<int> estado, List<int> subEstado);
        ListaCorreosGrupoDTO ObtenerCorreosGruposConVersion(int idCentroCosto, int idPaquete, List<int> estado, List<int> subEstado);

        AccesosMoodleDTO obtenerAccesosInicialesMoodle( int idAlumno);
        Plantilla obtenerPlantilla();
        Plantilla obtenerPlantillaAccesoMoodleAlumnoWhatsApp();
        Plantilla obtenerPlantillaAccesoMoodleAlumno();
        long ObtenerUltimoUidPorPersonal(int idPersonal);
    }
}
