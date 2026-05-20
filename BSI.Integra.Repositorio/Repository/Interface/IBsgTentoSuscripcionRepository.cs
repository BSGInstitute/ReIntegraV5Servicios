using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBsgTentoSuscripcionRepository
    {
        List<PlanSuscripcionDTO> ObtenerPlanes();
        int InsertarPlan(PlanSuscripcionInsertarDTO dto, string usuarioCreacion);
        void ActualizarPlan(PlanSuscripcionActualizarDTO dto, string usuarioModificacion);
        void EliminarPlan(int id, string usuarioModificacion);
    }
}
