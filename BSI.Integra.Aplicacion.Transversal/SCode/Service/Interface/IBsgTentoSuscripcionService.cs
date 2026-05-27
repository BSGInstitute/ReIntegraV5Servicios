using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IBsgTentoSuscripcionService
    {
        List<PlanSuscripcionDTO> ObtenerPlanes();
        int InsertarPlan(PlanSuscripcionInsertarDTO dto, string usuarioCreacion);
        void ActualizarPlan(PlanSuscripcionActualizarDTO dto, string usuarioModificacion);
        void EliminarPlan(int id, string usuarioModificacion);
        List<BsgTentoBeneficioDTO> ObtenerBeneficios();
        List<PlataformaTiendaDTO> ObtenerPlataformasTienda();
    }
}
