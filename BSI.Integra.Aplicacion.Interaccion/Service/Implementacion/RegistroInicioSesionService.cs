using BSI.Integra.Aplicacion.Interaccion.Service.Interface;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Implementacion
{
    /// Service: RegistroInicioSesionService
    /// Autor: Max Mantilla R.
    /// Fecha: 03/06/2024
    /// <summary>
    /// Gestión de registro sesión de integra
    /// </summary>

    public class RegistroInicioSesionService : IRegistroInicioSesionService
    {

        private IUnitOfWorkInteraccion _unitOfWork;
        private Mapper _mapper;


        public RegistroInicioSesionService(IUnitOfWorkInteraccion unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRegistroInicioSesion, RegistroInicioSesion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        public List<RegistroInicioSesionDTO> Obtener()
        {
            return _unitOfWork.RegistroInicioSesionRepository.Obtener();
        }

        public int RegistrarInicioSesion(RegistroInicioSesionLogueoDTO Model)
        {
            try
            {
                var Registro = _unitOfWork.RegistroInicioSesionRepository.RegistrarInicioSesion(Model);
                return Registro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
