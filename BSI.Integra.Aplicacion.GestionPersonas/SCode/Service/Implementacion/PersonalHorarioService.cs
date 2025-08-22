using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: PersonalHorarioService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_PersonalHorario
    /// </summary>
    public class PersonalHorarioService : IPersonalHorarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PersonalHorarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPersonalHorario, PersonalHorario>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PersonalHorario Add(PersonalHorario entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalHorarioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PersonalHorario>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonalHorario Update(PersonalHorario entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalHorarioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PersonalHorario>(modelo);
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
                _unitOfWork.PersonalHorarioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalHorario> Add(List<PersonalHorario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalHorarioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PersonalHorario>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PersonalHorario> Update(List<PersonalHorario> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonalHorarioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PersonalHorario>>(modelo);
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
                _unitOfWork.PersonalHorarioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PersonalHorario
        /// </summary>
        /// <returns> List<PersonalHorarioDTO> </returns>
        public IEnumerable<PersonalHorarioDTO> ObtenerPersonalHorario()
        {
            try
            {
                return _unitOfWork.PersonalHorarioRepository.ObtenerPersonalHorario();
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Horario del Asesor como Tabla
        /// </summary>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns> List<List<TimeSpan?>> </returns>
        public List<List<TimeSpan?>> ObtenerHorarioAsTable(int idPersonal)
        {
            try
            {
                var horario = _unitOfWork.PersonalHorarioRepository.ObtenerHorarioAsesor(idPersonal);
                if (horario == null)
                    throw new BadRequestException("#PHS-OHAT001@No existe Horario para el Personal " + idPersonal);

                List<List<TimeSpan?>> tablaHorario = new List<List<TimeSpan?>>
                {
                    new List<TimeSpan?> { horario.Domingo1, horario.Domingo2, horario.Domingo3, horario.Domingo4 },
                    new List<TimeSpan?> { horario.Lunes1, horario.Lunes2, horario.Lunes3, horario.Lunes4 },
                    new List<TimeSpan?> { horario.Martes1, horario.Martes2, horario.Martes3, horario.Martes4 },
                    new List<TimeSpan?> { horario.Miercoles1, horario.Miercoles2, horario.Miercoles3, horario.Miercoles4 },
                    new List<TimeSpan?> { horario.Jueves1, horario.Jueves2, horario.Jueves3, horario.Jueves4 },
                    new List<TimeSpan?> { horario.Viernes1, horario.Viernes2, horario.Viernes3, horario.Viernes4 },
                    new List<TimeSpan?> { horario.Sabado1, horario.Sabado2, horario.Sabado3, horario.Sabado4 }
                };
                return tablaHorario;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Horario del Asesor como Tabla
        /// </summary>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns> List<List<TimeSpan?>> </returns>
        public HorarioAsesorSemanaDTO ObtenerHorario(int idPersonal)
        {
            try
            {
                var horario = _unitOfWork.PersonalHorarioRepository.ObtenerHorarioAsesor(idPersonal);
                if (horario == null)
                    throw new BadRequestException("#PHS-OHAT001@No existe Horario para el Personal " + idPersonal);
                var horarioPersonal = new HorarioAsesorSemanaDTO()
                {
                    Domingo = new List<TimeSpan?> { horario.Domingo1, horario.Domingo2, horario.Domingo3, horario.Domingo4 },
                    Lunes = new List<TimeSpan?> { horario.Lunes1, horario.Lunes2, horario.Lunes3, horario.Lunes4 },
                    Martes = new List<TimeSpan?> { horario.Martes1, horario.Martes2, horario.Martes3, horario.Martes4 },
                    Miercoles = new List<TimeSpan?> { horario.Miercoles1, horario.Miercoles2, horario.Miercoles3, horario.Miercoles4 },
                    Jueves = new List<TimeSpan?> { horario.Jueves1, horario.Jueves2, horario.Jueves3, horario.Jueves4 },
                    Viernes = new List<TimeSpan?> { horario.Viernes1, horario.Viernes2, horario.Viernes3, horario.Viernes4 },
                    Sabado = new List<TimeSpan?> { horario.Sabado1, horario.Sabado2, horario.Sabado3, horario.Sabado4 }
                };
                return horarioPersonal;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Victor Hinojosa
        /// Fecha: 22/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Horario del Asesor como Tabla para GP
        /// </summary>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns> List<List<TimeSpan?>> </returns>
        public HorarioAsesorSemanaDTO ObtenerHorarioGP(int idPersonal)
        {
            try
            {
                var horario = _unitOfWork.PersonalHorarioRepository.ObtenerHorarioAsesor(idPersonal);

                if (horario == null)
                {
                    return new HorarioAsesorSemanaDTO
                    {
                        Domingo = new List<TimeSpan?>(),
                        Lunes = new List<TimeSpan?>(),
                        Martes = new List<TimeSpan?>(),
                        Miercoles = new List<TimeSpan?>(),
                        Jueves = new List<TimeSpan?>(),
                        Viernes = new List<TimeSpan?>(),
                        Sabado = new List<TimeSpan?>()
                    };
                }

                var horarioPersonal = new HorarioAsesorSemanaDTO()
                {
                    Domingo = new List<TimeSpan?> { horario.Domingo1, horario.Domingo2, horario.Domingo3, horario.Domingo4 },
                    Lunes = new List<TimeSpan?> { horario.Lunes1, horario.Lunes2, horario.Lunes3, horario.Lunes4 },
                    Martes = new List<TimeSpan?> { horario.Martes1, horario.Martes2, horario.Martes3, horario.Martes4 },
                    Miercoles = new List<TimeSpan?> { horario.Miercoles1, horario.Miercoles2, horario.Miercoles3, horario.Miercoles4 },
                    Jueves = new List<TimeSpan?> { horario.Jueves1, horario.Jueves2, horario.Jueves3, horario.Jueves4 },
                    Viernes = new List<TimeSpan?> { horario.Viernes1, horario.Viernes2, horario.Viernes3, horario.Viernes4 },
                    Sabado = new List<TimeSpan?> { horario.Sabado1, horario.Sabado2, horario.Sabado3, horario.Sabado4 }
                };

                return horarioPersonal;
            }
            catch
            {
                throw;
            }
        }
    }
}
