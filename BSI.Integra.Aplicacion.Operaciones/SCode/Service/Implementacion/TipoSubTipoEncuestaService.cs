using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{
    /// Service: TipoSubTipoEncuestaService
    /// Autor:  Junior Llerena
    /// Fecha: 2026-05-28
    /// <summary>
    /// Gestión general de T_TipoSubTipoEncuesta
    /// </summary>
    public class TipoSubTipoEncuestaService : ITipoSubTipoEncuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoSubTipoEncuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TTipoSubTipoEncuesta, TipoSubTipoEncuesta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public TipoSubTipoEncuesta Add(TipoSubTipoEncuesta entidad)
        {
            try
            {
                _unitOfWork.TipoSubTipoEncuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoSubTipoEncuesta Update(TipoSubTipoEncuesta entidad)
        {
            try
            {
                _unitOfWork.TipoSubTipoEncuestaRepository.Update(entidad);
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
                var resultado = _unitOfWork.TipoSubTipoEncuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoSubTipoEncuestaDTO> ObtenerTodo()
        {
            try
            {
                return _unitOfWork.TipoSubTipoEncuestaRepository.ObtenerTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoSubTipoEncuestaDTO> ObtenerPorTipoEncuesta(int idTipoEncuesta)
        {
            try
            {
                return _unitOfWork.TipoSubTipoEncuestaRepository.ObtenerPorTipoEncuesta(idTipoEncuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExisteAsociacion(int idTipoEncuesta, int idSubTipoEncuesta)
        {
            try
            {
                return _unitOfWork.TipoSubTipoEncuestaRepository.ExisteAsociacion(idTipoEncuesta, idSubTipoEncuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
