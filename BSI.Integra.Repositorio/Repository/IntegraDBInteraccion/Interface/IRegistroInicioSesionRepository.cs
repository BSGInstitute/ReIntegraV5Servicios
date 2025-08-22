using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface
{
    public interface IRegistroInicioSesionRepository : IGenericRepository<TRegistroInicioSesion>
    {
        public List<RegistroInicioSesionDTO> Obtener();
        public int RegistrarInicioSesion(RegistroInicioSesionLogueoDTO Model);

    }
}
