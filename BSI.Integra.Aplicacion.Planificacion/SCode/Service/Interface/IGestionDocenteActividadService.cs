using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteActividadService
    {
        Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto);
        Task<int> InsertarCabeceraAsync(GestionDocenteActividadCabeceraDTO dto);
        Task<int> InsertarDetalleAsync(int idCabecera, GestionDocenteActividadDetalleDTO dto);
        Task<int> InsertarOcurrenciaAsync(int idDetalle, GestionDocenteOcurrenciaDTO dto);
    }
}
