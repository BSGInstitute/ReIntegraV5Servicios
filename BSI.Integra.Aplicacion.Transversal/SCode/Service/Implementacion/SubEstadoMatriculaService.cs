using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SubEstadoMatriculaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/07/2022
    /// <summary>
    /// Gestión general de T_SubEstadoMatricula
    /// </summary>
    public class SubEstadoMatriculaService : ISubEstadoMatriculaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SubEstadoMatriculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSubEstadoMatricula, SubEstadoMatricula>(MemberList.None).ReverseMap();
                    cfg.CreateMap<SubEStadoRecibidoDTO, SubEstadoMatricula>(MemberList.None).ReverseMap();
                });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SubEstadoMatricula Add(SubEStadoRecibidoDTO data, string usuario)
        {
            try
            {
                SubEstadoMatricula entidad = _mapper.Map<SubEstadoMatricula>(data);

                entidad.Id = 0;
                entidad.Estado = true;
                entidad.UsuarioCreacion = usuario;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.SubEstadoMatriculaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SubEstadoMatricula>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SubEstadoMatricula Update(SubEStadoRecibidoDTO data, string usuario)
        {
            try
            {

                var repositorio = _unitOfWork.SubEstadoMatriculaRepository;
                var antiguo = repositorio.FirstById(data.Id);
                SubEstadoMatricula entidadNueva = _mapper.Map<SubEstadoMatricula>(data);

                entidadNueva.UsuarioCreacion = antiguo.UsuarioCreacion;
                entidadNueva.UsuarioModificacion = usuario;
                entidadNueva.FechaModificacion = DateTime.Now;
                entidadNueva.FechaCreacion = antiguo.FechaCreacion;
                entidadNueva.Estado = antiguo.Estado;

                var modelo = repositorio.Update(entidadNueva);
                _unitOfWork.Commit();
                return _mapper.Map<SubEstadoMatricula>(modelo);
            
                
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
                _unitOfWork.SubEstadoMatriculaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubEstadoMatricula> Add(List<SubEstadoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SubEstadoMatriculaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SubEstadoMatricula>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubEstadoMatricula> Update(List<SubEstadoMatricula> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SubEstadoMatriculaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SubEstadoMatricula>>(modelo);
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
                _unitOfWork.SubEstadoMatriculaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public IEnumerable<TCRM_SubEstadoMatriculaDTO> ObtenerSubEstadoMatricula()
        {
            try
            {
                return _unitOfWork.SubEstadoMatriculaRepository.ObtenerSubEstadoMatricula();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<SubEstadoMatriculaFiltroDTO> ObtenerSubEstadoMatriculaFiltro()
        {
            try
            {
                return _unitOfWork.SubEstadoMatriculaRepository.ObtenerSubEstadoMatriculaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
