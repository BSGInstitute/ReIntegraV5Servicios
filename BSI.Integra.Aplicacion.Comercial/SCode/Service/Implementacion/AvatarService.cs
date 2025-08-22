using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: AvatarService
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/09/2022
    /// <summary>
    /// Gestión general de la tabla T_Avatar
    /// </summary>
    public class AvatarService : IAvatarService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AvatarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAvatar, Avatar>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene avatar por usuario
        /// </summary>
        /// <param name="usuario"> Nombre de usuario </param>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
        public AvatarDTO ObtenerAvatar(string usuario)
        {
            try
            {
                return _unitOfWork.AvatarRepository.ObtenerAvatar(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
