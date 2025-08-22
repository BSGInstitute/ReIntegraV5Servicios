
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPostulanteAccesoTemporalAulaVirtualService
    {
        InformacionAccesoPostulanteDTO CrearAccesosTemporalesPostulante(EnviarAccesoPostulanteDTO dto, string usuario);
    }
}
