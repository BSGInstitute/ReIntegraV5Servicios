using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoFormularioService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_TipoFormulario
    /// </summary>
    public class TipoFormularioService : ITipoFormularioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoFormularioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, TipoFormulario>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoFormulario Add(TipoFormulario entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoFormularioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoFormulario>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoFormulario Update(TipoFormulario entidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoFormularioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TipoFormulario>(modelo);
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
                _unitOfWork.TipoFormularioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoFormulario> Add(List<TipoFormulario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoFormularioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoFormulario>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoFormulario> Update(List<TipoFormulario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoFormularioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoFormulario>>(modelo);
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
                _unitOfWork.TipoFormularioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


       public List<DTO.ComboDTO> ObtenerListaTipoFormulario()
        {
            try
            {
                return _unitOfWork.TipoFormularioRepository.ObtenerListaTipoFormulario();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
