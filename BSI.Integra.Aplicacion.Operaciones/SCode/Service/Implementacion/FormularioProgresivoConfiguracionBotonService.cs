using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: FormularioProgresivoConfiguracionBotonService
    /// Autor: Jorge Gamero.
    /// Fecha: 03/03/2025
    /// <summary>
    /// Gestión general de T_FormularioProgresivoConfiguracionBoton
    /// </summary>
    public class FormularioProgresivoConfiguracionBotonService : IFormularioProgresivoConfiguracionBotonService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioProgresivoConfiguracionBotonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioProgresivoConfiguracionBoton, FormularioProgresivoConfiguracionBoton>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public List<FormularioProgresivoConfiguracionBoton> Add(FormularioProgresivoConfiguracionBoton entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoConfiguracionBotonRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoConfiguracionBoton>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioProgresivoConfiguracionBoton> Update(FormularioProgresivoConfiguracionBoton entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoConfiguracionBotonRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoConfiguracionBoton>>(modelo);
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
                _unitOfWork.FormularioProgresivoConfiguracionBotonRepository.Delete(id, usuario);
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
        /// Fecha: 03/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoConfiguracionBoton por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoConfiguracionBoton </returns>
        public FormularioProgresivoConfiguracionBoton ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.FormularioProgresivoConfiguracionBotonRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 04/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoConfiguracionBoton por IdFormularioProgresivo.
        /// </summary>
        /// <param name="idFormularioProgresivo"> IdFormularioProgresivo de la entidad </param>
        /// <returns> FormularioProgresivoConfiguracionBoton </returns>
        public IEnumerable<FormularioProgresivoConfiguracionBoton> ObtenerPorIdFormularioProgresivo(int idFormularioProgresivo)
        {
            try
            {
                return _unitOfWork.FormularioProgresivoConfiguracionBotonRepository.ObtenerPorIdFormularioProgresivo(idFormularioProgresivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
