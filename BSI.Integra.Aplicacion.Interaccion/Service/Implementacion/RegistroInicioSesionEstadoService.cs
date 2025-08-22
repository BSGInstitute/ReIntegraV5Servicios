using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Aplicacion.Interaccion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Implementacion
{
    /// Service: RegistroInicioSesionService
    /// Autor: Max Mantilla R.
    /// Fecha: 03/06/2024
    /// <summary>
    /// Gestión de estado de la interacción de inicio de sesión en integra
    /// </summary>
    public class RegistroInicioSesionEstadoService : IRegistroInicioSesionEstadoService
    {
        private IUnitOfWorkInteraccion _unitOfWork;
        private Mapper _mapper;


        public RegistroInicioSesionEstadoService(IUnitOfWorkInteraccion unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRegistroInicioSesionEstado, RegistroInicioSesionEstado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        public List<RegistroInicioSesionEstadoDTO> Obtener()
        {
            return _unitOfWork.RegistroInicioSesionEstadoRepository.Obtener();
        }

        public bool RegistrarInicioSesionEstado(RegistroInicioSesionEstadoLogueoDTO Model)
        {
            try
            {
                var Registro = _unitOfWork.RegistroInicioSesionEstadoRepository.RegistrarInicioSesionEstado(Model);
                return Registro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
