using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class BsgTentoSuscripcionService : IBsgTentoSuscripcionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BsgTentoSuscripcionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PlanSuscripcionDTO> ObtenerPlanes() =>
            _unitOfWork.BsgTentoSuscripcionRepository.ObtenerPlanes();

        public int InsertarPlan(PlanSuscripcionInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoSuscripcionRepository.InsertarPlan(dto, usuarioCreacion);

        public void ActualizarPlan(PlanSuscripcionActualizarDTO dto, string usuarioModificacion) =>
            _unitOfWork.BsgTentoSuscripcionRepository.ActualizarPlan(dto, usuarioModificacion);

        public void EliminarPlan(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoSuscripcionRepository.EliminarPlan(id, usuarioModificacion);
    }
}
