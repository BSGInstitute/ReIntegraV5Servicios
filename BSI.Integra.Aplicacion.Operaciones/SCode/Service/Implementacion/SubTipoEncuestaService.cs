using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{
    /// Service: SubTipoEncuestaService
    /// Autor:  Junior Llerena
    /// Fecha: 2026-05-28
    /// <summary>
    /// Gestión general de T_SubTipoEncuesta
    /// </summary>
    public class SubTipoEncuestaService : ISubTipoEncuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SubTipoEncuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TSubTipoEncuesta, SubTipoEncuesta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public SubTipoEncuesta Add(SubTipoEncuesta entidad)
        {
            try
            {
                _unitOfWork.SubTipoEncuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SubTipoEncuesta Update(SubTipoEncuesta entidad)
        {
            try
            {
                _unitOfWork.SubTipoEncuestaRepository.Update(entidad);
                _unitOfWork.Commit();
                return entidad;
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
                var resultado = _unitOfWork.SubTipoEncuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SubTipoEncuesta ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.SubTipoEncuestaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SubTipoEncuestaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubTipoEncuestaDTO> ObtenerTodo()
        {
            try
            {
                return _unitOfWork.SubTipoEncuestaRepository.ObtenerTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
