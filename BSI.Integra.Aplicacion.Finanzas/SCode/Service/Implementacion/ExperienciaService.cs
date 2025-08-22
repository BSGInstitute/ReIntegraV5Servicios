using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ExperienciaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Experiencia
    /// </summary>
    public class ExperienciaService : IExperienciaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ExperienciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExperiencium, Experiencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExperienciaRecibidoDTO, Experiencia>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public Experiencia Add(ExperienciaRecibidoDTO data,string Usuario)
        {
            try
            {
                Experiencia entidad = _mapper.Map<Experiencia>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.ExperienciaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Experiencia>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Experiencia Update(ExperienciaRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorioExperiencia = _unitOfWork.ExperienciaRepository;
                var entidad = _mapper.Map<Experiencia>(repositorioExperiencia.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.IdAreaTrabajo = data.IdAreaTrabajo;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.ExperienciaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Experiencia>(modelo);
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
                _unitOfWork.ExperienciaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Experiencia> Add(List<Experiencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ExperienciaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Experiencia>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Experiencia> Update(List<Experiencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ExperienciaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Experiencia>>(modelo);
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
                _unitOfWork.ExperienciaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Experiencia para mostrarse en combo.
        /// </summary>
        /// <returns> List<ExperienciaComboDTO> </returns>
        public IEnumerable<ExperienciaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ExperienciaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
