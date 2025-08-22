using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: VerificacionManualDatosService
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 10/01/2023
    /// <summary>
    /// Gestión general de T_AsignacionAutomaticaError
    /// </summary>
    public class VerificacionManualDatosService : IVerificacionManualDatosService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public VerificacionManualDatosService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TAsignacionAutomatica, AsignacionAutomatica>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public IEnumerable<VerificacionManualDatosCompuestoDTO> ObtenerDatosVerificacion(FiltroBusquedaVerificacionManualDatosCompuestoDTO paginador)
        {
            try
            {
                var resultado = _unitOfWork.VerificacionManualDatosRepository.ObtenerDatosVerificacion(paginador);
                var alumnoService = new AlumnoService(_unitOfWork);
                foreach (var item in resultado)
                {
                    if (!string.IsNullOrWhiteSpace(item.Correo))
                        item.CorreoEncriptado = alumnoService.EncriptarCorreoHash(item.Correo);
                    if (!string.IsNullOrWhiteSpace(item.Movil))
                        item.MovilEncriptado = alumnoService.EncriptarNumeroHash(item.Movil);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
