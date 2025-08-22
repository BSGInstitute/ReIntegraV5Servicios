using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalAccesoTemporalAulaVirtualRepository : IGenericRepository<TPersonalAccesoTemporalAulaVirtual>
    {
        #region Metodos Base
        TPersonalAccesoTemporalAulaVirtual Add(PersonalAccesoTemporalAulaVirtual entidad);
        TPersonalAccesoTemporalAulaVirtual Update(PersonalAccesoTemporalAulaVirtual entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPersonalAccesoTemporalAulaVirtual> Add(IEnumerable<PersonalAccesoTemporalAulaVirtual> listadoEntidad);
        IEnumerable<TPersonalAccesoTemporalAulaVirtual> Update(IEnumerable<PersonalAccesoTemporalAulaVirtual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PersonalAccesoTemporalAulaVirtual? ObtenerPorId(int id);
        string? ObtenerIdUsuarioPortalWebCorreo(string email);
        List<MaestroPersonalAccesoTemporalDTO> ObtenerListaAccesoTemporal(int idPersonal);
        bool? EliminarAccesoTemporalPorIdPersonal(int idPersonal, string usuario);
        bool ActualizarAccesosTemporalesIntegra(ActualizarAccesoTemporalDTO datosAccesoTemporal);
        DatosBasicosPortalContactoDTO ObtenerDatosBasicosPortalWebUsername(string username);
        bool ActualizarIdAlumnoUsuarioPortalWeb(string idUsuarioPortalWeb, int idAlumno);
        bool ActualizarAccesosTemporalesPortalWeb(int idPersonal, string idUsuarioPortal, int idAlumno);
        bool EliminarAccesoTemporalPorIdPEspecificoPadre(int idPersonal, int idPEspecificoPadre, DateTime fechaInicio, DateTime fechaFin, string usuario);
    }
}
