using System.Collections.Generic;
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

        public List<BsgTentoPowerUpDTO> ObtenerPowerUps() =>
            _unitOfWork.BsgTentoPowerUpRepository.ObtenerPowerUps();

        public int InsertarPowerUp(BsgTentoPowerUpInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoPowerUpRepository.InsertarPowerUp(dto, usuarioCreacion);

        public void ActualizarPowerUp(BsgTentoPowerUpActualizarDTO dto, string usuarioModificacion) =>
            _unitOfWork.BsgTentoPowerUpRepository.ActualizarPowerUp(dto, usuarioModificacion);

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
    }
}
