using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteAccesoTemporalAulaVirtualRepository : IGenericRepository<TPostulanteAccesoTemporalAulaVirtual>
    {
        #region Metodos Base
        TPostulanteAccesoTemporalAulaVirtual Add(PostulanteAccesoTemporalAulaVirtual entidad);
        TPostulanteAccesoTemporalAulaVirtual Update(PostulanteAccesoTemporalAulaVirtual entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteAccesoTemporalAulaVirtual> Add(IEnumerable<PostulanteAccesoTemporalAulaVirtual> listadoEntidad);
        IEnumerable<TPostulanteAccesoTemporalAulaVirtual> Update(IEnumerable<PostulanteAccesoTemporalAulaVirtual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        RespuestaAccesosPostulanteDTO ObtenerAccesosPortalWebCorreo(string email);
        List<PostulanteAccesoTemporalAulaVirtual> ObtenerPorIdPostulantePespecificoHijoPadre(int idPostulante, int idPespecificoHijo, int idPespecificoPadre);
        bool ActualizarAccesosTemporalesPortalWeb(int idPostulante, string idUsuarioPortal, int idAlumno, int idPespecifico);
        PostulanteAccesoTemporalAulaVirtual? ObtenerPorIdPostulantePespecificoHijo(int idPostulante, int idPespecificoHijo);

    }
}
