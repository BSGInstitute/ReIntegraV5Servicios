using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: FormularioProgresivoCondicionMostrarService
    /// Autor: Jorge Gamero.
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivoCondicionMostrar
    /// </summary>
    public class FormularioProgresivoCondicionMostrarService : IFormularioProgresivoCondicionMostrarService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioProgresivoCondicionMostrarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioProgresivoCondicionMostrar, FormularioProgresivoCondicionMostrar>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public List<FormularioProgresivoCondicionMostrar> Add(FormularioProgresivoCondicionMostrar entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoCondicionMostrarRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoCondicionMostrar>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioProgresivoCondicionMostrar> Update(FormularioProgresivoCondicionMostrar entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoCondicionMostrarRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoCondicionMostrar>>(modelo);
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
                _unitOfWork.FormularioProgresivoCondicionMostrarRepository.Delete(id, usuario);
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
        /// Obtiene todos los campos de T_FormularioProgresivoCondicionMostrar por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoCondicionMostrar </returns>
        public FormularioProgresivoCondicionMostrar ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.FormularioProgresivoCondicionMostrarRepository.ObtenerPorId(id);
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
        public IEnumerable<FormularioProgresivoCondicionMostrar> ObtenerRegistros()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoCondicionMostrarRepository.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
