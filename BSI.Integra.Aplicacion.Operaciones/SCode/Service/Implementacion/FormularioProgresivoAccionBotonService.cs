using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: FormularioProgresivoAccionBotonService
    /// Autor: Jorge Gamero.
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivoAccionBoton
    /// </summary>
    public class FormularioProgresivoAccionBotonService : IFormularioProgresivoAccionBotonService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioProgresivoAccionBotonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioProgresivoAccionBoton, FormularioProgresivoAccionBoton>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public List<FormularioProgresivoAccionBoton> Add(FormularioProgresivoAccionBoton entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoAccionBotonRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoAccionBoton>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioProgresivoAccionBoton> Update(FormularioProgresivoAccionBoton entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoAccionBotonRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoAccionBoton>>(modelo);
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
                _unitOfWork.FormularioProgresivoAccionBotonRepository.Delete(id, usuario);
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
        /// Fecha: 29/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_FormularioProgresivoAccionBoton por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoAccionBoton </returns>
        public FormularioProgresivoAccionBoton ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.FormularioProgresivoAccionBotonRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 29/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoAccionBoton> ObtenerRegistros()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoAccionBotonRepository.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
