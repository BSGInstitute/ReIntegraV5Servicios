using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Interface
{
    public interface IRegistroInicioSesionService
    {
        public List<RegistroInicioSesionDTO> Obtener();
        public int RegistrarInicioSesion(RegistroInicioSesionLogueoDTO Model);
    }
}
