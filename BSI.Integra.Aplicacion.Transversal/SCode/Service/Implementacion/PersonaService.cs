using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PersonaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de T_Persona
    /// </summary>
    public class PersonaService : IPersonaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PersonaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPersona, Persona>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Persona Add(Persona entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Persona>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Persona Update(Persona entidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Persona>(modelo);
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
                _unitOfWork.PersonaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Persona> Add(List<Persona> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Persona>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Persona> Update(List<Persona> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PersonaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Persona>>(modelo);
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
                _unitOfWork.PersonaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de la tabla por su Id.
        /// </summary>
        /// <param name="idPersona">Id de la persona</param>
        /// <returns> Entidad: Persona </returns>
        public Persona ObtenerPorId(int idPersona)
        {
            try
            {
                return _unitOfWork.PersonaRepository.ObtenerPorId(idPersona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el email dependiendo de la persona
        /// </summary>
        /// <param name="idTablaOriginal">Id persona</param>
        /// <param name="tipoPersona">Tipo de Persona</param>
        /// <returns> Email de Persona : String </returns>
        private string ObtenerEmailPorIdTablaOriginalTipoPersona(int idTablaOriginal, TipoPersona tipoPersona)
        {

            try
            {
                var email = "";
                switch (tipoPersona) //ObtenerEmail
                {
                    case TipoPersona.Alumno:
                        email = _unitOfWork.AlumnoRepository.ObtenerEmailAlumno(idTablaOriginal).Email1;
                        break;
                    case TipoPersona.Personal:
                        email = _unitOfWork.PersonalRepository.ObtenerPorId(idTablaOriginal).Email;
                        break;
                    case TipoPersona.Docente:
                        email = _unitOfWork.ExpositorRepository.ObtenerPorId(idTablaOriginal).Email1;
                        break;
                    case TipoPersona.Proveedor:
                        email = _unitOfWork.ProveedorRepository.ObtenerProveedorPorId(idTablaOriginal).Email;
                        break;
                    case TipoPersona.Postulante:
                        var rpta = _unitOfWork.PostulanteRepository.ObtenerPorId(idTablaOriginal);
                        if (rpta != null)
                        {
                            email = rpta.Email;
                        }
                        break;
                    default:
                        break;
                }
                return email;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// Autor: Gilmer Quispe.
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta una persona y su clasificacion persona
        /// </summary>
        /// <param name="idTablaOriginal">Id de la tabla original</param>
        /// <param name="tipoPersona">Tipo de Persona</param>
        /// <param name="usuario">Usuario</param>
        /// <returns> Si se logra insertar correctamente retorna el id de clasificacion persona, en caso no termine el proceso correctamente retorna null: Int?</returns>
        public int? InsertarPersona(int idTablaOriginal, Aplicacion.Base.Enums.Enums.TipoPersona tipoPersona, string usuario)
        {
            var servicioCorreo = new CorreoService(_unitOfWork);
            try
            {
                var personaNueva = new Persona();
                var email = ObtenerEmailPorIdTablaOriginalTipoPersona(idTablaOriginal, tipoPersona);
                if (!servicioCorreo.EsCorreoValido(email))
                {
                    throw new Exception("#PS-IP-006@Email no valido");
                }

                //Si no existe el email en persona, lo insertamos
                var existePersona = _unitOfWork.PersonaRepository.ExistePorEmail(email);
                if (existePersona)
                {
                    //si no ->llenamos la persona actual con los registros actuales
                    personaNueva = _unitOfWork.PersonaRepository.ObtenerPorEmail(email);
                    if (personaNueva == null || personaNueva.Id == 0)
                    {
                        throw new Exception("#PS-IP-005@No existe persona");
                    }
                }
                else
                {
                    personaNueva.Email1 = email;
                    personaNueva.Estado = true;
                    personaNueva.FechaCreacion = DateTime.Now;
                    personaNueva.FechaModificacion = DateTime.Now;
                    personaNueva.UsuarioCreacion = usuario;
                    personaNueva.UsuarioModificacion = usuario;
                }
                //Si la persona no tiene un registro en clasificacionPersona, lo insertamos
                bool existeClasificacionPersona = false;
                if (personaNueva.Id != 0)
                {
                    existeClasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ExistePorIdPersonaTipoPersona(personaNueva.Id, tipoPersona);
                }
                if (existeClasificacionPersona)
                {
                    personaNueva.ClasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdTablaOriginalIdPersonaTipoPersona(idTablaOriginal, tipoPersona, personaNueva.Id);
                    if (personaNueva.ClasificacionPersona == null || personaNueva.ClasificacionPersona.Id == 0)
                    {

                        var cp = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdPersonaTipoPersona(personaNueva.Id, tipoPersona);
                        if (cp != null && cp.Id != 0)
                        {
                            throw new Exception($"#PS-IP-004@Existe persona pero para otro alumno: IdCP:{cp.Id}/IdPersona:{cp.IdPersona}/IdTablaOriginal: {cp.IdTablaOriginal}");
                        }
                        throw new Exception($"#PS-IP-003@No existe clasificacion persona para la entidad {tipoPersona} - {idTablaOriginal}");
                    }
                }
                else
                {
                    personaNueva.ClasificacionPersona = new ClasificacionPersona()
                    {
                        IdPersona = personaNueva.Id,
                        IdTipoPersona = (int)tipoPersona,
                        IdTablaOriginal = idTablaOriginal,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };
                }

                if (personaNueva.Id != 0)
                {
                    if (personaNueva.ClasificacionPersona.Id != 0)
                    {
                        return personaNueva.ClasificacionPersona.Id;
                    }
                    else
                    {
                        try
                        {
                            _unitOfWork.PersonaRepository.Update(personaNueva);
                            try
                            {
                                _unitOfWork.Commit();
                            }
                            catch (Exception ex)
                            {
                                throw new BadRequestException($"#PS-IP-002@Error en guardar Persona: {ex.Message}");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new BadRequestException($"#PS-IP-001@Error en Actualizar Persona: {ex.Message}");
                        }
                    }
                }
                else
                {
                    var respuestaPersona = _unitOfWork.PersonaRepository.Add(personaNueva);
                    _unitOfWork.Commit();
                    personaNueva.Id = respuestaPersona.Id;
                }
                //obtenemos el id de clasificacion persona\
                var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdTablaOriginalIdPersonaTipoPersona(idTablaOriginal, tipoPersona, personaNueva.Id);
                return clasificacionPersona.Id;
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                var log = new Log
                {
                    Ip = "-",
                    Usuario = "-",
                    Maquina = "-",
                    Ruta = "InsertarPersona V5",
                    Parametros = $"{idTablaOriginal}/{tipoPersona}/{usuario}",
                    Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}",
                    Excepcion = $"{e}",
                    Tipo = "VALIDATE",
                    IdPadre = 0,
                    UsuarioCreacion = "IntegraV5",
                    UsuarioModificacion = "IntegraV5",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true
                };
                _unitOfWork.LogRepository.Add(log);
                _unitOfWork.Commit();
                return null;
            }
        }
    }
}
