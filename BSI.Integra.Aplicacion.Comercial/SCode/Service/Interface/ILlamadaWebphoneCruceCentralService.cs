using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ILlamadaWebphoneCruceCentralService
    {
        public LlamadaWebphoneCruceCentral Insertar(NuevaLlamadaActividadDTO obj, LlamadaWebphoneAsterisk llamadaWebphone, string usuario);
    }
}
