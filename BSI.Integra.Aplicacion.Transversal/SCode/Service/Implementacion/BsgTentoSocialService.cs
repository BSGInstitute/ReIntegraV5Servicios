using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class BsgTentoSocialService : IBsgTentoSocialService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BsgTentoSocialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int InsertarUsuario(UsuarioBsgTentoInsertarDTO dto, string usuarioCreacion) =>
            _unitOfWork.BsgTentoSocialRepository.InsertarUsuario(dto, usuarioCreacion);

        public UsuarioBsgTentoDTO ObtenerUsuarioPorAspNetUser(string idAspNetUser) =>
            _unitOfWork.BsgTentoSocialRepository.ObtenerUsuarioPorAspNetUser(idAspNetUser);

        public List<PublicacionAdminDTO> ObtenerPublicaciones(bool? visible, DateTime fechaInicio, DateTime fechaFin) =>
            _unitOfWork.BsgTentoSocialRepository.ObtenerPublicaciones(visible, fechaInicio, fechaFin);

        public void ActualizarVisibilidadPublicacion(int id, bool visible, string usuarioModificacion) =>
            _unitOfWork.BsgTentoSocialRepository.ActualizarVisibilidadPublicacion(id, visible, usuarioModificacion);

        public void EliminarPublicacion(int id, string usuarioModificacion) =>
            _unitOfWork.BsgTentoSocialRepository.EliminarPublicacion(id, usuarioModificacion);

        public List<TipoReaccionDTO> ObtenerTiposReaccion() =>
            _unitOfWork.BsgTentoSocialRepository.ObtenerTiposReaccion();
    }
}
