using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: FormularioProgresivoTipoService
    /// Autor: Jorge Gamero.
    /// Fecha: 29/11/2024
    /// <summary>
    /// Gestión general de T_FormularioProgresivoTipo
    /// </summary>
    public class FormularioProgresivoTipoService : IFormularioProgresivoTipoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioProgresivoTipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioProgresivoTipo, FormularioProgresivoTipo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public List<FormularioProgresivoTipo> Add(FormularioProgresivoTipo entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoTipoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoTipo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioProgresivoTipo> Update(FormularioProgresivoTipo entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioProgresivoTipoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioProgresivoTipo>>(modelo);
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
                _unitOfWork.FormularioProgresivoTipoRepository.Delete(id, usuario);
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
        /// Obtiene todos los campos de T_FormularioProgresivoTipo por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> FormularioProgresivoTipo </returns>
        public FormularioProgresivoTipo ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.FormularioProgresivoTipoRepository.ObtenerPorId(id);
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
        public IEnumerable<FormularioProgresivoTipo> ObtenerRegistros()
        {
            try
            {
                return _unitOfWork.FormularioProgresivoTipoRepository.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
