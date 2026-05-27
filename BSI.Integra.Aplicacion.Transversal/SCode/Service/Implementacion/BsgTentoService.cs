using System.Collections.Generic;
using System.Transactions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class BsgTentoService : IBsgTentoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BsgTentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<BsgTentoAreaDTO> ObtenerAreasConRuta() =>
            _unitOfWork.BsgTentoRepository.ObtenerAreasConRuta();

        public List<BsgTentoUnidadDTO> ObtenerUnidadesPorArea(int idAreaCapacitacion) =>
            _unitOfWork.BsgTentoRepository.ObtenerUnidadesPorArea(idAreaCapacitacion);

        public int InsertarUnidad(BsgTentoUnidadInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoRepository.InsertarUnidad(dto, usuarioCreacion);

        public void ActualizarUnidad(BsgTentoUnidadActualizarDTO dto, string usuarioModificacion) =>
            _unitOfWork.BsgTentoRepository.ActualizarUnidad(dto, usuarioModificacion);

        public void ActualizarOrdenUnidades(List<BsgTentoOrdenDTO> ordenList, string usuarioModificacion)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var item in ordenList)
                    _unitOfWork.BsgTentoRepository.ActualizarOrdenUnidad(item.Id, item.Orden, usuarioModificacion);
                scope.Complete();
            }
        }

        public void EliminarUnidad(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoRepository.EliminarUnidad(id, usuarioModificacion);

        public List<BsgTentoPasoDTO> ObtenerPasosPorUnidad(int idBsgTentoUnidad) =>
            _unitOfWork.BsgTentoRepository.ObtenerPasosPorUnidad(idBsgTentoUnidad);

        public int InsertarPaso(BsgTentoPasoInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoRepository.InsertarPaso(dto, usuarioCreacion);

        public void ActualizarPaso(BsgTentoPasoActualizarDTO dto, string usuarioModificacion) =>
            _unitOfWork.BsgTentoRepository.ActualizarPaso(dto, usuarioModificacion);

        public void ActualizarOrdenPasos(List<BsgTentoOrdenDTO> ordenList, string usuarioModificacion)
        {
            using (var scope = new TransactionScope())
            {
                foreach (var item in ordenList)
                    _unitOfWork.BsgTentoRepository.ActualizarOrdenPaso(item.Id, item.Orden, usuarioModificacion);
                scope.Complete();
            }
        }

        public void EliminarPaso(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoRepository.EliminarPaso(id, usuarioModificacion);

        public List<BsgTentoComboDTO> ObtenerComboPrograma(int idAreaCapacitacion) =>
            _unitOfWork.BsgTentoRepository.ObtenerComboPrograma(idAreaCapacitacion);
    }
}
