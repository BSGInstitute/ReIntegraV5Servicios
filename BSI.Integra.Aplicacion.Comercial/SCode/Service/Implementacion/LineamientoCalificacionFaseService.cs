using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    /// Servicio: LineamientoCalificacionFaseService
    /// Autor: José Vega.
    /// Fecha: 15/09/2025
    /// <summary>
    /// Servicio para la gestión de LineamientoCalificacionFase
    /// </summary>
    public class LineamientoCalificacionFaseService : ILineamientoCalificacionFaseService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LineamientoCalificacionFaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TLineamientoCalificacionFase, LineamientoCalificacionFase>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        public LineamientoCalificacionFase Add(LineamientoCalificacionFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionFaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LineamientoCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LineamientoCalificacionFase Update(LineamientoCalificacionFase entidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionFaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LineamientoCalificacionFase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.LineamientoCalificacionFaseRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_LineamientoCalificacionFase por el Id.
        /// </summary>
        /// <param name="id">ID de la transición</param>
        /// <returns>LineamientoCalificacionFase</returns>
        public LineamientoCalificacionFase ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionFaseRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: José Vega
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns>LineamientoCalificacionFaseDTO</returns>
        public List<LineamientoCalificacionFaseDTO> Obtener()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionFaseRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
