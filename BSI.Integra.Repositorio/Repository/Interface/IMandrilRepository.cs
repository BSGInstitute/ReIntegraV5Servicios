using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMandrilRepository : IGenericRepository<TMandril>
    {
        #region Metodos Base
        TMandril Add(Mandril entidad);
        TMandril Update(Mandril entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMandril> Add(IEnumerable<Mandril> listadoEntidad);
        IEnumerable<TMandril> Update(IEnumerable<Mandril> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MandrilDTO> ObtenerMandril();
        IEnumerable<CorreoInteraccionV2AgendaDTO> ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(int idAlumno, int idPersonal);
        CorreoAlumnoSpeechDTO VerCorreoAlumnoSpeech(string correoReceptor, string messageId);
        List<CorreoDTO> ListaInteraccionCorreoAlumnoCorreo(int idAlumno, int idAsesor);
        List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumno(int idAlumno, int idAsesor, string messageId);
    }
}