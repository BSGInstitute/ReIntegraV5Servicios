using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Web.Helpers;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PostulanteAccesoTemporalAulaVirtualService : IPostulanteAccesoTemporalAulaVirtualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PostulanteAccesoTemporalAulaVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, PostulanteAccesoTemporalAulaVirtual>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCriterioEvaluacionProceso, PostulanteAccesoTemporalAulaVirtualDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteAccesoTemporalAulaVirtual, PostulanteAccesoTemporalAulaVirtualDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Edgar Serruto
        /// Fecha: 22/06/2021
        /// Version: 1.0
        /// <summary>
        /// Crea Accesos Temporales de Postulante
        /// </summary>
        /// <param name="dto"> Información de Postulante para Creación de Accesos al Portal </param>
        /// <returns> InformacionAccesoPostulanteDTO </returns>
        public InformacionAccesoPostulanteDTO CrearAccesosTemporalesPostulante(EnviarAccesoPostulanteDTO dto, string usuario)
        {
            try
            {
                InformacionAccesoPostulanteDTO respuestaCreacionAcceso = new InformacionAccesoPostulanteDTO();
                var postulanteProcesoSeleccion = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPorIdPostulante(dto.IdPostulante);

                if (postulanteProcesoSeleccion == null)
                {
                    throw new BadRequestException("No se encontro el proceso de seleccion");
                }
                var examen = _unitOfWork.ExamenRepository.ObtenerPorId(dto.IdExamen);
                if (examen == null)
                {
                    throw new BadRequestException("No se encontro el examen");
                }
                var cantidadDias = examen.CantidadDiasAcceso;
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(examen.IdCentroCosto!.Value);
                if (programaEspecifico == null)
                {
                    throw new BadRequestException("No se encontro el programa especifico");
                }

                var idPGeneral = programaEspecifico.IdProgramaGeneral;
                var idPEspecificoPadre = _unitOfWork.PespecificoPadrePespecificoHijoRepository.GetBy(x => x.PespecificoHijoId == programaEspecifico.Id).Select(x => x.PespecificoPadreId).FirstOrDefault();

                if (idPEspecificoPadre == 0)
                {
                    idPEspecificoPadre = programaEspecifico.Id;
                }
                DateTime fechaInicio = DateTime.Now.Date;
                DateTime fechaFin = fechaInicio.AddDays(cantidadDias.GetValueOrDefault() + 1);

                var postulante = _unitOfWork.PostulanteRepository.FirstById(dto.IdPostulante);
                var idPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ObtenerIdUsuarioPortalWebCorreo(postulante.Email);
                Alumno? alumno = _unitOfWork.AlumnoRepository.ObtenerPorEmail1(postulante.Email);
                //Validación de accesos en portal
                if (string.IsNullOrEmpty(idPortalWeb))
                {
                    // Logica para crear el usuario con sus registros correspondientes en la tabla de alumno, clasificacionpersona, todo si es que tiene un registro en persona
                    var persona = _unitOfWork.PersonaRepository.ObtenerPorEmail(postulante.Email);
                    if (persona == null)
                    {
                        respuestaCreacionAcceso.ValidacionRespuesta = false;
                        return respuestaCreacionAcceso;
                    }
                    if (alumno == null)
                    {
                        alumno = new Alumno();
                        //Nombres
                        var nombres = postulante.Nombre.Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                        if (nombres.Count == 1)
                        {
                            alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                            alumno.Nombre2 = string.Empty;
                        }
                        else if (nombres.Count == 2)
                        {
                            alumno.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                            alumno.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                        }
                        else if (nombres.Count > 2)
                        {
                            alumno.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                            alumno.Nombre2 = string.Empty;
                        }
                        //Apellidos
                        postulante.ApellidoPaterno = postulante.ApellidoPaterno ?? string.Empty;
                        postulante.ApellidoMaterno = postulante.ApellidoMaterno ?? string.Empty;
                        var apellidos = (postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno).Split(new char[] { ' ' }).ToList().Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                        if (apellidos.Count == 1)
                        {
                            alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                            alumno.ApellidoMaterno = string.Empty;
                        }
                        else if (apellidos.Count == 2)
                        {
                            alumno.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                            alumno.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                        }
                        else if (apellidos.Count > 2)
                        {
                            alumno.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                            alumno.ApellidoMaterno = string.Empty;
                        }
                        else
                        {
                            alumno.ApellidoPaterno = string.Empty;
                            alumno.ApellidoMaterno = string.Empty;
                        }
                        alumno.IdAformacion = 3/*Sin area de formacion*/;
                        alumno.IdAtrabajo = 3/*Sin area de trabajo*/;
                        alumno.IdCargo = 11/*Sin cargo*/;
                        alumno.IdIndustria = 48/*Sin industria*/;
                        alumno.Celular = "963852741";
                        alumno.IdCodigoRegionCiudad = null;
                        alumno.IdCodigoPais = 51/*Peru*/;
                        alumno.Telefono = string.Empty;
                        alumno.Email1 = postulante.Email;
                        alumno.Email2 = postulante.Email;
                        alumno.Estado = true;
                        alumno.UsuarioCreacion = "AccesosPostulante";
                        alumno.UsuarioModificacion = "SYSTEM";
                        alumno.FechaModificacion = DateTime.Now;
                        alumno.FechaCreacion = DateTime.Now;
                        alumno.IdEstadoContactoWhatsApp = 3;
                        var resultadoAlumno = _unitOfWork.AlumnoRepository.Add(alumno);
                        _unitOfWork.Commit();
                        respuestaCreacionAcceso.ValidacionRespuesta = false;
                        return respuestaCreacionAcceso;
                    }
                    var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.ObtenerPorIdPersonaTipoAlumno(persona.Id);/*Tipo Alumno*/
                    if (clasificacionPersona != null)
                    {
                        if (clasificacionPersona.IdTablaOriginal != alumno.Id)
                        {
                            clasificacionPersona.IdTablaOriginal = alumno.Id;
                            clasificacionPersona.FechaModificacion = DateTime.Now;
                            _unitOfWork.ClasificacionPersonaRepository.Update(clasificacionPersona);
                            _unitOfWork.Commit();
                        }
                    }
                    else
                    {
                        clasificacionPersona = new ClasificacionPersona();
                        clasificacionPersona.IdPersona = persona.Id;
                        clasificacionPersona.IdTipoPersona = 1;/*Tipo Alumno*/
                        clasificacionPersona.IdTablaOriginal = alumno.Id;
                        clasificacionPersona.Estado = true;
                        clasificacionPersona.UsuarioCreacion = "AccesosPostulante";
                        clasificacionPersona.UsuarioModificacion = "AccesosPostulante";
                        clasificacionPersona.FechaCreacion = DateTime.Now;
                        clasificacionPersona.FechaModificacion = DateTime.Now;
                        _unitOfWork.ClasificacionPersonaRepository.Add(clasificacionPersona);
                        _unitOfWork.Commit();
                    }
                    /*Logica para crear el contacto*/
                    if (string.IsNullOrEmpty(idPortalWeb))
                    {
                        Random letraRandom = new Random();
                        int numero = letraRandom.Next(26);
                        char letra = (char)(((int)'A') + numero);
                        string claveIntegra = alumno.Nombre1.ToLower().Substring(0, 2) + alumno.Email1.ToUpper().Substring(0, 1) + "AcTmp0" + letra;
                        string claveHash = string.Empty;
                        claveHash = Crypto.HashPassword(claveIntegra);

                        var resultadoAspNetUsers = _unitOfWork.MontoPagoCronogramaRepository.CrearUsuarioClavePortalWeb(alumno.Id, postulante.Email, claveIntegra, claveHash, postulante.Nombre, postulante.ApellidoPaterno + " " + postulante.ApellidoMaterno, alumno.Telefono, alumno.Celular, alumno.IdCodigoRegionCiudad, alumno.IdCodigoPais, DateTime.Now);
                        respuestaCreacionAcceso.IdAlumno = resultadoAspNetUsers.IdAlumno;
                        respuestaCreacionAcceso.Usuario = resultadoAspNetUsers.UserName;
                        respuestaCreacionAcceso.Clave = resultadoAspNetUsers.Password;
                        idPortalWeb = _unitOfWork.PersonalAccesoTemporalAulaVirtualRepository.ObtenerIdUsuarioPortalWebCorreo(postulante.Email);
                    }
                }
                //Obtengo la información de accesos al portal de postulante
                var datosAcceso = _unitOfWork.PostulanteAccesoTemporalAulaVirtualRepository.ObtenerAccesosPortalWebCorreo(postulante.Email);
                if (datosAcceso.IdAlumno > 0)
                {
                    respuestaCreacionAcceso.IdAlumno = datosAcceso.IdAlumno;
                    respuestaCreacionAcceso.Usuario = datosAcceso.Email;
                    respuestaCreacionAcceso.Clave = datosAcceso.Clave;
                    respuestaCreacionAcceso.ValidacionRespuesta = true;
                }
                else
                {
                    respuestaCreacionAcceso.ValidacionRespuesta = false;
                    return respuestaCreacionAcceso;
                }
                PostulanteCursoPortalNotasHistorico nuevoRegistroNotas;
                //Lógica para Accesos Temporales anteriores
                var listaPostulanteAnterior = _unitOfWork.PostulanteAccesoTemporalAulaVirtualRepository.ObtenerPorIdPostulantePespecificoHijoPadre(dto.IdPostulante, programaEspecifico.Id, idPEspecificoPadre);
                foreach (var accesoAnterior in listaPostulanteAnterior)
                {
                    //Función para obtener anteriores notas de postulante desde el portal
                    var notasPortalCursoAnterior = _unitOfWork.PostulanteCursoPortalNotasHistoricoRepository.ObtenerNotasAnteriores(datosAcceso.IdAlumno.GetValueOrDefault(), accesoAnterior.IdPespecificoPadre);

                    var pGeneral = 0;
                    var idUsuario = "";
                    foreach (var notaIndividual in notasPortalCursoAnterior)
                    {
                        pGeneral = notaIndividual.IdPgeneral;
                        idUsuario = notaIndividual.IdUsuario;
                        nuevoRegistroNotas = new PostulanteCursoPortalNotasHistorico()
                        {
                            IdPostulanteProcesoSeleccion = accesoAnterior.IdPostulanteProcesoSeleccion.GetValueOrDefault(),
                            IdPgeneral = notaIndividual.IdPgeneral,
                            OrdenFilaCapitulo = notaIndividual.OrdenFilaCapitulo,
                            OrdenFilaSesion = notaIndividual.OrdenFilaSesion,
                            GrupoPregunta = notaIndividual.GrupoPregunta,
                            Calificacion = notaIndividual.Calificacion,
                            IdUsuario = notaIndividual.IdUsuario,
                            IdAlumno = notaIndividual.IdAlumno,
                            IdPespecifico = notaIndividual.IdPespecifico,
                            AccesoPrueba = notaIndividual.AccesoPrueba,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        };
                        _unitOfWork.PostulanteCursoPortalNotasHistoricoRepository.Add(nuevoRegistroNotas);
                        _unitOfWork.Commit();
                    }
                    var visualizacionVideoAnterior = _unitOfWork.PostulanteCursoPortalNotasHistoricoRepository.ObtenerVisualizacionVideoAnteriores(idUsuario, pGeneral);
                    if (notasPortalCursoAnterior.Any() && visualizacionVideoAnterior.Any() && pGeneral > 0 && idUsuario.Length > 0)
                    {
                        var respuestaEliminacionFisicaNota = _unitOfWork.PostulanteCursoPortalNotasHistoricoRepository.EliminarFisicamenteAnterioresNotas(idUsuario, pGeneral, notasPortalCursoAnterior.Select(x => x.Id).ToList(), visualizacionVideoAnterior.Select(x => x.Id).ToList());
                        if (!respuestaEliminacionFisicaNota)
                        {
                            respuestaCreacionAcceso.ValidacionRespuesta = false;
                            return respuestaCreacionAcceso;
                        }
                    }
                    _unitOfWork.PostulanteAccesoTemporalAulaVirtualRepository.Delete(accesoAnterior.Id, usuario);
                    _unitOfWork.Commit();
                }
                //Agregar nuevos accesos
                PostulanteAccesoTemporalAulaVirtual agregar = new()
                {
                    IdPostulante = dto.IdPostulante,
                    IdPespecificoPadre = idPEspecificoPadre,
                    IdPespecificoHijo = programaEspecifico.Id,
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdAlumno = alumno.Id,
                    IdPostulanteProcesoSeleccion = postulanteProcesoSeleccion.Id,
                    IdExamen = dto.IdExamen,
                };

                try
                {
                    _unitOfWork.PostulanteAccesoTemporalAulaVirtualRepository.Add(agregar);
                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    respuestaCreacionAcceso.ValidacionRespuesta = false;
                    return respuestaCreacionAcceso;
                }
                try
                {
                    _unitOfWork.PostulanteAccesoTemporalAulaVirtualRepository.ActualizarAccesosTemporalesPortalWeb(postulante.Id, idPortalWeb, alumno.Id, programaEspecifico.Id);
                }
                catch (Exception ex)
                {
                    respuestaCreacionAcceso.ValidacionRespuesta = false;
                    return respuestaCreacionAcceso;
                }
                return respuestaCreacionAcceso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
