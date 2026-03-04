using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProyectoAplicacionService : IProyectoAplicacionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProyectoAplicacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ProyectoAplicacionPorMatriculaCabeceraDTO> ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ProyectoAplicacionRepository.ObtenerPorIdMatriculaCabecera(idMatriculaCabecera);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
