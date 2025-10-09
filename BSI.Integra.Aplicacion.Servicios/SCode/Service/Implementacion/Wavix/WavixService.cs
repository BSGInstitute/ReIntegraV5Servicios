using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Aplicacion.Servicios.SCode.Service.Interface.Wolkbox;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.SCode.Service.Implementacion.Wavix
{
    public class WavixService : IWavixService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public WavixService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion wavix del personal
        /// </summary> 
        /// <returns> IEnumerable<WavixPersonalDTO> </returns>
        public WavixPersonalDTO GetUserAccess(int idPersonal)
        {
            try
            {
                return _unitOfWork.WavixRepository.GetUserAccess(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion de numero por usuario
        /// </summary> 
        /// <returns> IEnumerable<WavixPersonalDTO> </returns>
        public List<NumeroAsesorWavixDTO>? GetNumberByUser(int idPersonal)
        {
            try
            {
                return _unitOfWork.WavixRepository.GetNumberByUser(idPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Carlos Crispin
        /// Fecha: 20/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de la ultima llamada realizada en wavix
        /// </summary> 
        /// <returns> IEnumerable<WavixPersonalDTO> </returns>
        public EstadoLlamadaDTO ObtenerEstadoUltimaLlamada(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada)
        {
            try
            {
                return _unitOfWork.WavixRepository.ObtenerEstadoUltimaLlamada(idPersonal, idOportunidad, idActividadDetalle, idAlumno, nroIntentoLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
