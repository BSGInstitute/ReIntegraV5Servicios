using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AgendaInformacionActividadService
    /// Autor: Flavio Rodrigo Mamani Fabian
    /// Fecha: 09/03/2023
    /// <summary>
    /// Gestión general de T_AgendaInformacionActividad
    /// </summary>
    public class AgendaInformacionActividadService : IAgendaInformacionActividadService
    {
        private IUnitOfWork _unitOfWork;
        public AgendaInformacionActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la Oportunidad Compuesta y el Programa Especifico asociados a una Actividad Detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public (OportunidadCompuestoDTO? Oportunidad, PEspecificoPorIdCentroCostoDTO? PEspecifico) ObtenerOportunidadYPEspecificoPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                var oportunidadCompuesto = _unitOfWork.OportunidadRepository.ObtenerOportunidadCompuestoPorIdActividadDetalle(idActividadDetalle);
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidadCompuesto.IdCentroCosto.GetValueOrDefault());
                if (oportunidadCompuesto.Id == 0)
                    oportunidadCompuesto = null;
                if (programaEspecifico.Id == 0)
                    programaEspecifico = null;
                return (oportunidadCompuesto, programaEspecifico);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Envia correo 
        /// </summary>
        /// <param name="idPlantilla"> Id de la plantilla </param>   
        /// <param name="idPersonal"> Id del personal </param>   
        /// <param name="emailPersonal"> Direccion email del personal </param>   
        /// <param name="emailAlumno"> Direccion email del alumno </param>   
        /// <param name="idoportunidad"> Id de la oportunidad </param>   
        /// <returns> Bool </returns>
        public bool EnvioCorreoAlumno(int idPlantilla, int idPersonal, string emailPersonal, string emailAlumno, int idoportunidad)
        {
            try
            {
                var plantillaService = new PlantillaService(_unitOfWork);
                var plantillaBaseService = new PlantillaBaseService(_unitOfWork);
                var reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                if (!plantillaService.ExistePorId(idPlantilla))
                {
                    return false;
                }

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);
                if (!plantillaBaseService.ExistePorId(plantilla.IdPlantillaBase))
                {
                    return false;
                }

                var plantillaBase = plantillaService.ObtenerPlantillaCorreo(idPlantilla);

                var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO
                {
                    IdOportunidad = idoportunidad,
                    IdPlantilla = idPlantilla,
                });

                //var destinatarios = "jvillena@bsginstitute.com";
                var destinatarios = emailAlumno;

                var Remitente = string.IsNullOrEmpty(emailPersonal) == true ? "matriculas@bsginstitute.com" : emailPersonal;
                //var Remitente ="jsalazart@bsginstitute.com";

                if (plantilla.IdPlantillaBase == 2) //ValorEstatico.IdPlantillaBaseEmail)
                {

                    var emailCalculado = resultadoReemplazo.EmailReemplazado;
                    List<string> correosPersonalizadosCopia = new List<string>();
                    //cuando la plantilla es condiciones y caracteristicas
                    //1227	Condiciones y Características - PERÚ OPERACIONES
                    //1245	Condiciones y Características - COLOMBIA OPERACIONES
                    if (Remitente == "matriculas@bsginstitute.com" && (idPlantilla == 1227 || idPlantilla == 1245))
                    {
                        correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
                    }
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        //"jquinones@bsginstitute.com",
                        //"modpru@bsginstitute.com",
                        //"ccrispin@bsginstitute.com",
                        //"wruiz@bsginstitute.com"
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                    };
                    correosPersonalizados.Add(destinatarios);

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = Remitente,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailService();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica guardar envio
                    var gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = Remitente,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = idPersonal,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        //IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };

                    _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// Autor: Jeremy Pacheco.
        /// Fecha: 21/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene encuestas de Alumno por Matricula 
        /// </summary>
        /// <param name="idMatricula">Matricula del Alumno</param>
        /// <returns> List<EncuestaAsignadoMatriculaDTO> </returns>
        public List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula)
        {
            try
            {
                var respuesta = _unitOfWork.DocumentoAgendaRepository.ObtenerEncuestaAlumnoMatriculaCurso(idMatricula);
                
                if (respuesta == null || !respuesta.Any())
                    return respuesta;

                foreach (var item in respuesta)
                {

                    //List<PEspecificoSesionEncuestaPreguntaCategoriaDTO > preguntasEncuestas = new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
                    var preguntasEncuestas = ObtenerPreguntasEncuestaIdPEspecificoSesion(item.IdPEspecificoSesion);
                    var respuestasEncuestas = ObtenerRespuestasEncuestasIdPEspecificoSesion(item.IdPEspecificoSesion, idMatricula);

                    if (preguntasEncuestas != null)
                    {
                        var preguntasFiltradas = preguntasEncuestas
                        ?.Where(p => p.IdEncuestaSesionPrograma == item.IdEncuestaSesionPrograma)
                        .ToList();

                        item.PreguntasEncuesta = preguntasFiltradas;

                    }
                    else
                    {
                        item.PreguntasEncuesta = new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
                    }

                    if (respuestasEncuestas != null)
                    {
                        var respuestasFiltradas = respuestasEncuestas
                       ?.Where(p => p.IdEncuestaSesionPrograma == item.IdEncuestaSesionPrograma)
                       .ToList();

                        item.RespuestasEncuesta = respuestasFiltradas;
                    }
                    else
                    {
                        item.RespuestasEncuesta = new List<PEspecificoSesionEncuestaAlumnoDTO>();
                    }


                }

                return respuesta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas de Encuesta del IdPEspecificoSesion
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <returns> List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> </returns>
        public List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> ObtenerPreguntasEncuestaIdPEspecificoSesion(int IdPEspecificoSesion)
        {
            try
            {
                List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> PreguntasCategorias = new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
                var preguntas = _unitOfWork.DocumentoAgendaRepository.ObtenerPreguntasSesionEncuestaIdPespecifico(IdPEspecificoSesion);
                var alternativas = _unitOfWork.DocumentoAgendaRepository.ObtenerPEspecificoSesionEncuestaPreguntaAlternativaPorIdSesion(IdPEspecificoSesion);
                if (preguntas != null)
                {
                    PreguntasCategorias = preguntas
                    .GroupBy(x => new
                    {
                        x.IdCategoria,
                        x.NombreCategoria,
                        x.IdEncuestaSesionPrograma
                    })
                    .Select(g => new PEspecificoSesionEncuestaPreguntaCategoriaDTO
                    {
                        IdCategoria = g.Key.IdCategoria,
                        NombreCategoria = g.Key.NombreCategoria,
                        IdEncuestaSesionPrograma = g.Key.IdEncuestaSesionPrograma,
                        Preguntas = g.Select(p => new PEspecificoSesionEncuestaPreguntaDTO
                        {
                            Id = p.Id,
                            IdEncuestaSesionPrograma = p.IdEncuestaSesionPrograma,
                            IdEncuestaOnline = p.IdEncuestaOnline,
                            IdPreguntaEncuestaTipo = p.IdPreguntaEncuestaTipo,
                            Pregunta = p.Pregunta,
                            DescripcionActiva = p.DescripcionActiva,
                            Descripcion = p.Descripcion,
                            NombreTipoPregunta = p.NombreTipoPregunta,
                            IdPEspecificoSesion = p.IdPEspecificoSesion,
                            IdCategoria = p.IdCategoria,
                            NombreCategoria = p.NombreCategoria,
                            PreguntaObligatoria = p.PreguntaObligatoria,
                            PreguntaActiva = p.PreguntaActiva
                        }).ToList()
                    })
                    .ToList();

                    foreach (var Categorias in PreguntasCategorias)
                    {
                        foreach (var pregunta in Categorias.Preguntas)
                        {
                            pregunta.Alternativas = new List<PEspecificoSesionEncuestaPreguntaAlternativaDTO>();
                            if (alternativas != null)
                            {
                                var alternativa =
                                    alternativas.Where(x => x.IdPreguntaEncuesta == pregunta.Id)
                                    .OrderBy(x => x.Orden)
                                    .Select(x => new PEspecificoSesionEncuestaPreguntaAlternativaDTO
                                    {
                                        Id = x.Id,
                                        IdEncuestaOnline = x.IdEncuestaOnline,
                                        IdEncuestaSesionPrograma = x.IdEncuestaSesionPrograma,
                                        IdPEspecificoSesion = x.IdPEspecificoSesion,
                                        IdPreguntaEncuesta = x.IdPreguntaEncuesta,
                                        Orden = x.Orden,
                                        Respuesta = x.Respuesta,
                                        Puntaje = x.Puntaje,
                                    }).ToList();
                                if (alternativa != null)
                                {
                                    pregunta.Alternativas.AddRange(alternativa);
                                }
                            }
                        }
                    }
                }
                return PreguntasCategorias;
            }
            catch (Exception ex)
            {
                return new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de las encuesta por IdPEspecifico y matricula
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <param name="IdMatriculaCabecera"> Identificador de la matricula</param>
        /// <returns> List<PEspecificoSesionEncuestaAlumnoDTO> </returns>
        public List<PEspecificoSesionEncuestaAlumnoDTO> ObtenerRespuestasEncuestasIdPEspecificoSesion(int IdPEspecificoSesion, int IdMatriculaCabecera)
        {
            try
            {
                var respuesta = _unitOfWork.DocumentoAgendaRepository.ObtenerEncuestaAlumnoPorIdPEspecificoSesion(IdPEspecificoSesion, IdMatriculaCabecera);
                var respuestas = _unitOfWork.DocumentoAgendaRepository.ObtenerPEspecificoSesionEncuestaAlumnoRespuestaPorIdSesion(IdPEspecificoSesion, IdMatriculaCabecera);
                if (respuesta != null)
                {
                    foreach (var res in respuesta)
                    {
                        res.Respuestas = new List<PEspecificoSesionEncuestaAlumnoRespuestaDTO>();
                        if (respuestas != null)
                        {
                            var pregunta =
                                respuestas.Where(x => x.IdPEspecificoSesionEncuestaAlumno == res.Id).Select(x => new PEspecificoSesionEncuestaAlumnoRespuestaDTO
                                {
                                    Id = x.Id,
                                    IdPEspecificoSesion = x.IdPEspecificoSesion,
                                    IdPreguntaEncuesta = x.IdPreguntaEncuesta,
                                    IdPEspecificoSesionEncuestaAlumno = x.IdPEspecificoSesionEncuestaAlumno,
                                    IdPreguntaRespuestaEncuesta = x.IdPreguntaRespuestaEncuesta,
                                    Valor = x.Valor,
                                    Puntos = x.Puntos,
                                    IdMatriculaCabecera = x.IdMatriculaCabecera
                                }).ToList();
                            res.Respuestas.AddRange(pregunta);
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Agrega encuesta de alumno 
        /// </summary>
        /// <param name="data">Datos para insertar Encuesta</param>
        /// <returns>retorna true o false </returns>
        public bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data)
        {
            try
            {
                foreach (var categoria in data.Categorias)
                {
                    foreach (var pregunta in categoria.Preguntas)
                    {
                        if (!pregunta.PreguntaObligatoria && pregunta.ValorRespuesta.Count == 0)
                        {
                            EncuestaAvancePreguntaRespuestaDTO RespuestaVacia = new EncuestaAvancePreguntaRespuestaDTO();
                            RespuestaVacia.IdRespuesta = 0;
                            RespuestaVacia.Respuesta = "";
                            RespuestaVacia.Puntaje = 0;
                            pregunta.ValorRespuesta.Add(RespuestaVacia);
                        }
                    }
                };
                var registro = _unitOfWork.DocumentoAgendaRepository.AgregarPEspecificoSesionEncuestaAlumno(data);
                return registro;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un comentario a una encuesta
        /// </summary>
        /// <param name="Encuesta">Datos para agregar comentario a un Alumno</param>
        /// <returns>Retorna true o false</returns>
        public bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta)
        {
            try
            {
                var registro = _unitOfWork.DocumentoAgendaRepository.AgregarComentarioEncuesta(Encuesta);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
