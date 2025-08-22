using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{
    /// Service: GrabacionesClasesOnlineService
    /// Autor: Jorge Gamero
    /// Fecha: 14/01/2025
    /// <summary>
    /// </summary>
    public class GrabacionesClasesOnlineService : IGrabacionesClasesOnlineService
    {
        private IUnitOfWork _unitOfWork;

        public GrabacionesClasesOnlineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jorge Gamero
        /// Fecha: 14/01/2025
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla principal.
        /// </summary>
        /// <returns> List<GrabacionesClasesOnlineDTO> </returns>
        public List<GrabacionesClasesOnlineDTO> GenerarVistaProgramasOnline(GrabacionesClasesOnlineFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.GrabacionesClasesOnlineRepository.GenerarVistaProgramasOnline(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla de modal de sesiones.
        /// </summary>
        /// <returns> List<SesionesClasesOnlineResumenDTO> </returns>
        public List<SesionesClasesOnlineResumenDTO> ObtenerSesiones(SesionesFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.GrabacionesClasesOnlineRepository.ObtenerSesiones(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data para la grilla de modal de detalle de resumen de sesiones
        /// </summary>
        /// <returns> List<SesionesClasesOnlineDetalleResumenDTO> </returns>
        public List<SesionesClasesOnlineDetalleResumenDTO> ObtenerDetalleResumenGrabacionSesion(SesionesFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.GrabacionesClasesOnlineRepository.ObtenerDetalleResumenGrabacionSesion(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza sesiones
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizarSesiones(SesionesClasesOnlineModificarFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.GrabacionesClasesOnlineRepository.ActualizarSesiones(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Modifica numeroDia de T_DisponibilidadProgramaSincronicoDefecto
        /// </summary>
        /// <returns> bool </returns>
        public bool ModificarDisponibilidadProgramaDefecto(DataDisponibilidadProgramaDefectoDTO filtro)
        {
            try
            {
                return _unitOfWork.GrabacionesClasesOnlineRepository.ModificarDisponibilidadProgramaDefecto(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene disponibilidad de programa
        /// </summary>
        /// <returns> List<DataDisponibilidadProgramaDefectoDTO> </returns>
        public List<DataDisponibilidadProgramaDefectoDTO> ObtenerDisponibilidadPrograma()
        {
           try
            {
                return _unitOfWork.GrabacionesClasesOnlineRepository.ObtenerDisponibilidadPrograma();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
