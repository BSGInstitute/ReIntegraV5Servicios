using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EstadoProgramaEspecificoService
    /// Autor: Max Mantilla.
    /// Fecha: 20/07/2023
    /// <summary>
    /// Gestión general de T_EstadoPEspecifico
    /// </summary>
    public class EstadoProgramaEspecificoService : IEstadoProgramaEspecificoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EstadoProgramaEspecificoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEstadoPespecifico, EstadoPespecifico>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public EstadoPespecifico Add(EstadoPespecifico entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoProgramaEspecificoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoPespecifico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoPespecifico Update(EstadoPespecifico entidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoProgramaEspecificoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EstadoPespecifico>(modelo);
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
                _unitOfWork.EstadoProgramaEspecificoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoPespecifico> Add(List<EstadoPespecifico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoProgramaEspecificoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoPespecifico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoPespecifico> Update(List<EstadoPespecifico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EstadoProgramaEspecificoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EstadoPespecifico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.EstadoProgramaEspecificoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de EstadoPespecifico para combo
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FiltroDTO> ObtenerEstadoPespecificoParaCombo()
        {
            try
            {
                return _unitOfWork.EstadoProgramaEspecificoRepository.ObtenerEstadoPespecificoParaCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory  Ramirez .
        /// Fecha: 15/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de estado
        /// </summary>
        /// <returns> List<TipoCategoriaOrigenDTO> </returns>
         public IEnumerable<ComboDTO> ObtenerComboEstado()
        {
            try
            {
                return _unitOfWork.EstadoProgramaEspecificoRepository.ObtenerComboEstado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
