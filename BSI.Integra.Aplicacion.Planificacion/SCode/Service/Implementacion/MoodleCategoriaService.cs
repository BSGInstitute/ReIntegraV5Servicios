using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MaestroCategoriaMoodleService
    /// Autor: Gilmer Quispe.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión general de Maestro Categoria Moodle
    /// </summary>
    public class MoodleCategoriaService : IMoodleCategoriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MoodleCategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMoodleCategoriaTipo, MoodleCategoriaTipo>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMoodleCategorium, MoodleCategoriaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<MoodleCategoriaTipo, MoodleCategoriaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// inserta un nuedo dato a T_MoodleCategoria
        /// </summary>
        /// <returns> bool </returns>
        public MoodleCategoriaDTO Insertar(MoodleCategoriaDTO moodleCategoriaDTO, string usuario)
        {
            try
            {
                MoodleCategoria moodleCategoria = new MoodleCategoria()
                {
                    IdCategoriaMoodle = moodleCategoriaDTO.IdCategoriaMoodle,
                    NombreCategoria = moodleCategoriaDTO.NombreCategoria,
                    IdMoodleCategoriaTipo = moodleCategoriaDTO.IdMoodleCategoriaTipo,
                    AplicaProyecto = true,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var moodleCategorium = _unitOfWork.MoodleCategoriaRepository.Add(moodleCategoria);
                _unitOfWork.Commit();
                return _mapper.Map<MoodleCategoriaDTO>(moodleCategorium);
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un dato a T_MoodleCategoria
        /// </summary>
        /// <returns> bool </returns>
        public MoodleCategoriaDTO Actualizar(MoodleCategoriaDTO moodleCategoriaDTO, string usuario)
        {
            try
            {
                if (_unitOfWork.MoodleCategoriaRepository.Exist(moodleCategoriaDTO.Id.Value))
                {
                    var categoriaMoodle = _unitOfWork.MoodleCategoriaRepository.ObtenerPorId(moodleCategoriaDTO.Id.Value);
                    if (categoriaMoodle != null)
                    {
                        categoriaMoodle.IdCategoriaMoodle = moodleCategoriaDTO.IdCategoriaMoodle;
                        categoriaMoodle.NombreCategoria = moodleCategoriaDTO.NombreCategoria;
                        categoriaMoodle.IdMoodleCategoriaTipo = moodleCategoriaDTO.IdMoodleCategoriaTipo;
                        categoriaMoodle.UsuarioModificacion = usuario;
                        categoriaMoodle.FechaModificacion = DateTime.Now;
                        _unitOfWork.MoodleCategoriaRepository.Update(categoriaMoodle);
                        _unitOfWork.Commit();
                    }
                    return moodleCategoriaDTO;
                }
                else
                {
                    throw new Exception("No se encontro dato en T_MoodleCategoria");
                }
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un dato a T_MoodleCategoria
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarMoodleCategoria(int idMoodleCategoria, string usuario)
        {
            try
            {
                if (_unitOfWork.MoodleCategoriaRepository.Exist(idMoodleCategoria))
                {
                    _unitOfWork.MoodleCategoriaRepository.Delete(idMoodleCategoria, usuario);
                    _unitOfWork.Commit();
                    return (true);
                }
                else
                {
                    _unitOfWork.Dispose();
                    return (false);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
