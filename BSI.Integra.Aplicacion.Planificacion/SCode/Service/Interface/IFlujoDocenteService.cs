using System.Collections.Generic;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IFlujoDocenteService
    {
        Task<bool> RegistrarOportunidad(DocentePostulanteDTO docentePostulante, string usuario, int idEstadoGestionContacto, int? idCentroCosto);
    }
}
