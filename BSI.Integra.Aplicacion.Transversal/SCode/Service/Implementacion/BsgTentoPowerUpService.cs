using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class BsgTentoPowerUpService : IBsgTentoPowerUpService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BsgTentoPowerUpService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BsgTentoPowerUpDTO> ObtenerPowerUps()
        {
            var powerUps = _unitOfWork.BsgTentoPowerUpRepository.ObtenerPowerUps();
            var asignaciones = _unitOfWork.BsgTentoPowerUpRepository.ObtenerTodosPowerUpCanalDistribucion();
            foreach (var pu in powerUps)
            {
                pu.Canales = asignaciones.Where(a => a.IdPowerUp == pu.Id).ToList();
            }
            return powerUps;
        }

        public int InsertarPowerUp(BsgTentoPowerUpInsertarDTO dto, string usuarioCreacion)
        {
            using (var scope = new TransactionScope())
            {
                var idPowerUp = _unitOfWork.BsgTentoPowerUpRepository.InsertarPowerUp(dto, usuarioCreacion);
                if (dto.Canales != null)
                {
                    foreach (var c in dto.Canales)
                        _unitOfWork.BsgTentoPowerUpRepository.ActualizarPowerUpCanalDistribucion(idPowerUp, c.IdCanalDistribucion, c.Disponible, usuarioCreacion);
                }
                scope.Complete();
                return idPowerUp;
            }
        }

        public void ActualizarPowerUp(BsgTentoPowerUpActualizarDTO dto, string usuarioModificacion)
        {
            using (var scope = new TransactionScope())
            {
                _unitOfWork.BsgTentoPowerUpRepository.ActualizarPowerUp(dto, usuarioModificacion);
                if (dto.Canales != null)
                {
                    foreach (var c in dto.Canales)
                        _unitOfWork.BsgTentoPowerUpRepository.ActualizarPowerUpCanalDistribucion(dto.Id, c.IdCanalDistribucion, c.Disponible, usuarioModificacion);
                }
                scope.Complete();
            }
        }

        public void ActualizarOrdenPowerUps(List<BsgTentoOrdenDTO> ordenList, string usuarioModificacion)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var item in ordenList)
                    _unitOfWork.BsgTentoPowerUpRepository.ActualizarOrdenPowerUp(item.Id, item.Orden, usuarioModificacion);
                scope.Complete();
            }
        }

        public void EliminarPowerUp(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoPowerUpRepository.EliminarPowerUp(id, usuarioModificacion);

        public List<CanalDistribucionDTO> ObtenerCanalesDistribucion() =>
            _unitOfWork.BsgTentoPowerUpRepository.ObtenerCanalesDistribucion();
    }
}
