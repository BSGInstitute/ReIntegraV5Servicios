using System.Collections.Generic;
using System.Transactions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class BsgTentoLogrosService : IBsgTentoLogrosService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BsgTentoLogrosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BsgTentoTipoCondicionDTO> ObtenerTiposCondicion() =>
            _unitOfWork.BsgTentoLogrosRepository.ObtenerTiposCondicion();

        public List<BsgTentoLogroDTO> ObtenerLogros(int? tipoLogro) =>
            _unitOfWork.BsgTentoLogrosRepository.ObtenerLogros(tipoLogro);

        public int InsertarLogro(BsgTentoLogroInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoLogrosRepository.InsertarLogro(dto, usuarioCreacion);

        public void ActualizarLogro(BsgTentoLogroActualizarDTO dto, string usuarioModificacion) =>
            _unitOfWork.BsgTentoLogrosRepository.ActualizarLogro(dto, usuarioModificacion);

        public void ActualizarOrdenLogros(List<BsgTentoOrdenDTO> ordenList, string usuarioModificacion)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var item in ordenList)
                    _unitOfWork.BsgTentoLogrosRepository.ActualizarOrdenLogro(item.Id, item.Orden, usuarioModificacion);
                scope.Complete();
            }
        }

        public void EliminarLogro(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoLogrosRepository.EliminarLogro(id, usuarioModificacion);

        public List<BsgTentoTipoMisionDTO> ObtenerTiposMision() =>
            _unitOfWork.BsgTentoLogrosRepository.ObtenerTiposMision();

        public List<BsgTentoMisionDTO> ObtenerMisiones(int? tipoMision) =>
            _unitOfWork.BsgTentoLogrosRepository.ObtenerMisiones(tipoMision);

        public int InsertarMision(BsgTentoMisionInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoLogrosRepository.InsertarMision(dto, usuarioCreacion);

        public void ActualizarMision(BsgTentoMisionActualizarDTO dto, string usuarioModificacion) =>
            _unitOfWork.BsgTentoLogrosRepository.ActualizarMision(dto, usuarioModificacion);

        public void EliminarMision(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoLogrosRepository.EliminarMision(id, usuarioModificacion);
    }
}
