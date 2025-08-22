using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: MatriculaFormularioProgresivoService
    /// Autor: Max Mantilla.
    /// Fecha: 26/03/2025
    /// <summary>
    /// Gestión general de T_MatriculaFormularioProgresivo
    /// </summary>
    public class MatriculaFormularioProgresivoService : IMatriculaFormularioProgresivoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MatriculaFormularioProgresivoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMatriculaFormularioProgresivo, MatriculaFormularioProgresivo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public List<MatriculaFormularioProgresivo> Add(MatriculaFormularioProgresivo entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaFormularioProgresivoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaFormularioProgresivo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaFormularioProgresivo> Update(MatriculaFormularioProgresivo entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaFormularioProgresivoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaFormularioProgresivo>>(modelo);
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
                _unitOfWork.MatriculaFormularioProgresivoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla
        /// Fecha: 25/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene información del cupón de descuento por el correo del usuario
        /// </summary> 
        /// <returns> InformacionCupoDescuento </returns>
        public InformacionCupoDescuentoDTO ObtenerDescuentoProfiling(string emailUsuario)
        {
            try
            {
                return _unitOfWork.MatriculaFormularioProgresivoRepository.ObtenerDescuentoProfiling(emailUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
