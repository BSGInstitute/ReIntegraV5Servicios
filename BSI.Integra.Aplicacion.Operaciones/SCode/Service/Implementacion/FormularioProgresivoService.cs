using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: FormularioProgresivoService
    /// Autor: Jorge Gamero.
    /// Fecha: 04/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivo
    /// </summary>
    public class FormularioProgresivoService : IFormularioProgresivoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioProgresivoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioProgresivo, FormularioProgresivo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public List<FormularioProgresivo> Add(FormularioProgresivo entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioProgresivo> Update(FormularioProgresivo entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivo>>(modelo);
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
                _unitOfWork.FormularioProgresivoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jorge Gamero
        /// Fecha: 04/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivo por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivo </returns>
        public FormularioProgresivo ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.FormularioProgresivoRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 04/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<FormularioProgresivo> </returns>
        public IEnumerable<FormularioProgresivo> ObtenerRegistros()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoRepository.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 06/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de todos los formularios iniciales (tipo 1)
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosIniciales()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoRepository.ObtenerFormulariosIniciales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene id y nombre de todos los formularios iniciales (tipo 1) sin formulario de respuesta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosInicialesSinFormularioRespuesta()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoRepository.ObtenerFormulariosInicialesSinFormularioRespuesta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
