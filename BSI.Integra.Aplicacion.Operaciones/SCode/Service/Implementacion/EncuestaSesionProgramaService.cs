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
    /// Service: EncuestaSesionProgramaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudTipoReportes
    /// </summary>
    public class EncuestaSesionProgramaService : IEncuestaSesionProgramaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EncuestaSesionProgramaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEncuestaSesionPrograma, EncuestaSesionPrograma>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public EncuestaSesionPrograma Add(EncuestaSesionPrograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaSesionProgramaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EncuestaSesionPrograma>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EncuestaSesionPrograma Update(EncuestaSesionPrograma entidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaSesionProgramaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<EncuestaSesionPrograma>(modelo);
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
                _unitOfWork.EncuestaSesionProgramaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EncuestaSesionPrograma> Add(List<EncuestaSesionPrograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaSesionProgramaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EncuestaSesionPrograma>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EncuestaSesionPrograma> Update(List<EncuestaSesionPrograma> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EncuestaSesionProgramaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<EncuestaSesionPrograma>>(modelo);
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
                _unitOfWork.EncuestaSesionProgramaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public EncuestaSesionPrograma ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.EncuestaSesionProgramaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 25/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EncuestaSesionProgramaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 25/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<EncuestaProgramaDTO> ObtenerEncuestasPrograma(int idPespecifico)
        {
            try
            {
                return _unitOfWork.EncuestaSesionProgramaRepository.ObtenerEncuestasPrograma(idPespecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 25/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<EncuestaSesionAsignadaDTO> ObtenerEncuestaAsignada(int idPespecificoSesion)
        {
            try
            {
                return _unitOfWork.EncuestaSesionProgramaRepository.ObtenerEncuestaAsignada(idPespecificoSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
