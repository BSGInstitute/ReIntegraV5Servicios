using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MaestroCursoMoodleService
    /// Autor: Gretel Canasa
    /// Fecha: 12/05/2023
    /// <summary>
    /// Gestión general de Maestro Curso Moodle
    /// </summary>
    public class MaestroCursoMoodleService : IMaestroCursoMoodleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MaestroCursoMoodleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMoodleCategorium, MoodleCategoria>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMoodleCurso, MoodleCursoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gretel Canasa
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// obtiene el combo de T_MoodleCursoTipo
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public List<MoodleCategoriaDetalle> ObtenerComboMoodleCategoria()
        {
            try
            {
                return _unitOfWork.MoodleCategoriaRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Este método obtiene una lista de categorias Moodle registrados en la base de datos
        /// </summary>
        /// <returns> List<CursoMoodleDTO> </returns>
        public List<MoodleCursoDTO> ObtenerCursosMoodleRegistradas()
        {
            try
            {
                return _unitOfWork.MoodleCursoRepository.ObtenerCursosMoodleRegistradas();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Sergio Yepez
        /// Fecha: 11/12/2024
        /// Version: 1.0
        /// <summary>
        /// verifica si existe un Curso Moodle
        /// </summary>
        /// <returns> bool </returns>
        public bool ExisteCursoMoodle(int idCursoMoodle)
        {
            try
            {
                return _unitOfWork.MoodleCursoRepository.ExisteCursoMoodle(idCursoMoodle);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        /// Autor: Gretel Canasa
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// inserta un nuedo dato a T_MoodleCurso
        /// </summary>
        /// <returns> bool </returns>
        public MoodleCursoDTO InsertarMoodleCurso(MoodleCursoDTO moodleCursoDTO, string usuario)
        {
            try
            {
                IMaestroCursoMoodleService moodleCursoService = new MaestroCursoMoodleService(_unitOfWork);
                MoodleCurso moodleCurso = new MoodleCurso()
                {
                    IdCursoMoodle = moodleCursoDTO.IdCursoMoodle.Value,
                    IdCategoriaMoodle = moodleCursoDTO.IdCategoriaMoodle.Value,
                    Nombre = moodleCursoDTO.NombreCursoMoodle,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var _moodleCurso = _unitOfWork.MoodleCursoRepository.Add(moodleCurso);
                _unitOfWork.Commit();

                MoodleCursoDTO moodleCursodto = new MoodleCursoDTO();
                moodleCursodto = _unitOfWork.MoodleCursoRepository.ObtenerCursoPorId(_moodleCurso.Id);
                return moodleCursodto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un dato a T_MoodleCurso
        /// </summary>
        /// <returns> bool </returns>
        public MoodleCursoDTO ActualizarMoodleCurso(MoodleCursoDTO moodleCursoDTO, string usuario)
        {
            try
            {
                if (_unitOfWork.MoodleCursoRepository.Exist(moodleCursoDTO.Id.Value))
                {

                    var cursoMoodle = _unitOfWork.MoodleCursoRepository.ObtenerPorId(moodleCursoDTO.Id.Value);
                    cursoMoodle.IdCursoMoodle = moodleCursoDTO.IdCursoMoodle;
                    cursoMoodle.IdCategoriaMoodle = moodleCursoDTO.IdCategoriaMoodle;
                    cursoMoodle.Nombre = moodleCursoDTO.NombreCursoMoodle;
                    cursoMoodle.UsuarioModificacion = usuario;
                    cursoMoodle.FechaModificacion = DateTime.Now;
                    var nuevoDato = _unitOfWork.MoodleCursoRepository.Update(cursoMoodle);
                    _unitOfWork.Commit();
                    return _unitOfWork.MoodleCursoRepository.ObtenerCursoPorId(moodleCursoDTO.Id.Value);
                }
                else
                {
                    throw new Exception("No se encontro dato en T_MoodleCurso");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 12/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina un dato a T_MoodleCurso
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarMoodleCurso(int id, string usuario)
        {
            if (_unitOfWork.MoodleCursoRepository.Exist(id))
            {
                var res = _unitOfWork.MoodleCursoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return (true);
            }
            else
            {
                return (false);
            }
        }
    }
}
