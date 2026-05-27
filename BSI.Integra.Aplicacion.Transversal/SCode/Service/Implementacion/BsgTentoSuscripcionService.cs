using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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

        public List<PlanSuscripcionDTO> ObtenerPlanes()
        {
            var planes = _unitOfWork.BsgTentoSuscripcionRepository.ObtenerPlanes();
            var asignaciones = _unitOfWork.BsgTentoSuscripcionRepository.ObtenerTodosPlanSuscripcionBeneficio();
            foreach (var plan in planes)
            {
                plan.Beneficios = asignaciones.Where(a => a.IdPlanSuscripcion == plan.Id).ToList();
            }
            return planes;
        }

        public int InsertarPlan(PlanSuscripcionInsertarDTO dto, string usuarioCreacion)
        {
            using (var scope = new TransactionScope())
            {
                var idPlan = _unitOfWork.BsgTentoSuscripcionRepository.InsertarPlan(dto, usuarioCreacion);
                if (dto.Beneficios != null)
                {
                    foreach (var b in dto.Beneficios)
                        _unitOfWork.BsgTentoSuscripcionRepository.ActualizarPlanSuscripcionBeneficio(idPlan, b.IdBeneficio, b.Activo, usuarioCreacion);
                }
                scope.Complete();
                return idPlan;
            }
        }

        public void ActualizarPlan(PlanSuscripcionActualizarDTO dto, string usuarioModificacion)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.BsgTentoSuscripcionRepository.ActualizarPlan(dto, usuarioModificacion);
                if (dto.Beneficios != null)
                {
                    foreach (var b in dto.Beneficios)
                        _unitOfWork.BsgTentoSuscripcionRepository.ActualizarPlanSuscripcionBeneficio(dto.Id, b.IdBeneficio, b.Activo, usuarioModificacion);
                }
                scope.Complete();
            }
        }

        public void EliminarPlan(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoSuscripcionRepository.EliminarPlan(id, usuarioModificacion);

        public List<BsgTentoBeneficioDTO> ObtenerBeneficios() =>
            _unitOfWork.BsgTentoSuscripcionRepository.ObtenerBeneficios();

        public List<PlataformaTiendaDTO> ObtenerPlataformasTienda() =>
            _unitOfWork.BsgTentoSuscripcionRepository.ObtenerPlataformasTienda();
    }
}
