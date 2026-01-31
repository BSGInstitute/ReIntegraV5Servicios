using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteActividadService
    {
        Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto);
        Task<int> InsertarCabeceraAsync(GestionDocenteActividadCabeceraDTO dto);
        Task<int> InsertarDetalleAsync(GestionDocenteActividadDetalleDTO dto);
        Task<int> InsertarOcurrenciaAsync(int idDetalle, GestionDocenteOcurrenciaDTO dto);
        Task<int> AsociarActividadAFlujoAsync(GestionDocenteActividadCabeceraFlujoDTO dto);
        Task<bool> DesasociarActividadDeFlujoAsync(int id, string usuario);
        Task<List<GestionDocenteActividadCabeceraFlujoDTO>> ObtenerActividadesPorFlujoAsync(int idFlujo);
    }
}
