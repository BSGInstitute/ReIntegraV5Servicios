using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IMandrilService
    {
        #region Metodos Base
        Mandril Add(Mandril entidad);
        Mandril Update(Mandril entidad);
        bool Delete(int id, string usuario);

        List<Mandril> Add(List<Mandril> listadoEntidad);
        List<Mandril> Update(List<Mandril> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MandrilDTO> ObtenerMandril();
        IEnumerable<CorreoInteraccionV2AgendaDTO> ObtenerCorreoInteraccionV2EnviadosPorPersonalParaAgenda(int idAlumno, int idPersonal);
        CorreoAlumnoSpeechDTO VerCorreoAlumnoSpeech(string correoReceptor, string messageId);
        List<CorreoDTO> ListaInteraccionCorreoAlumnoCorreo(int idAlumno, int idAsesor);
        List<CorreoInteraccionesAlumnoDTO> ListaInteraccionCorreoAlumno(int idAlumno, int idAsesor, string messageId);
    }
}
