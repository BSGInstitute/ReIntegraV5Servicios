using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.AulaVirtual;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: PEspecificoMatriculaAlumnoService
    /// Autor: Gilmer Quipse
    /// Fecha: 12/11/2022
    /// <summary>
    /// Gestión general de T_PEspecificoMatriculaAlumno
    /// </summary>
    public class PEspecificoMatriculaAlumnoService : IPEspecificoMatriculaAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PEspecificoMatriculaAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPespecificoMatriculaAlumno, PEspecificoMatriculaAlumno>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        

        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// <summary>
        /// Obtiene los PEspecificos asociados a una matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerTodoFiltroAutoComplete(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PEspecificoMatriculaAlumnoRepository.ObtenerTodoFiltroAutoComplete(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Programas Especificos asociados a una matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerPEspecificoPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var notaService = new NotaService(_unitOfWork);
                var pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                var listaCursosMatriculados = pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(idMatriculaCabecera);
                var listaNotasPorMatricula = notaService.ListadoNotaPorMatriculaCabeceraPromedio(idMatriculaCabecera);
                EsquemaEvaluacionService esquemaEvaluacionService = new EsquemaEvaluacionService(_unitOfWork);

                if (listaNotasPorMatricula != null && listaNotasPorMatricula.Count() > 0)
                {
                    foreach (var item in listaCursosMatriculados)
                    {
                        item.TipoPrograma = 1;
                        var datoNota = listaNotasPorMatricula.Where(x => x.IdPEspecifico == item.IdPEspecifico).FirstOrDefault();

                        if (datoNota != null)
                        {
                            item.IdMatriculaCabecera = idMatriculaCabecera;
                            item.FechaFin = datoNota.FechaTermino;
                            item.FechaInicio = datoNota.FechaInicio;
                            item.Promedio = datoNota.Promedio;
                        }
                        else
                        {
                            item.IdMatriculaCabecera = idMatriculaCabecera;
                            item.FechaFin = "-";
                            item.FechaInicio = "-";
                            item.Promedio = "0";
                        }
                    }
                }
                else
                {
                    var evaluacionesBO = new MoodleCronogramaEvaluacionService(_unitOfWork);
                    var resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(idMatriculaCabecera);
                    for (int i = 0; i < listaCursosMatriculados.Count; i++)
                    {
                        listaCursosMatriculados[i].IdMatriculaCabecera = idMatriculaCabecera;
                        listaCursosMatriculados[i].TipoPrograma = 2;
                        if (i < resultado.Count)
                        {
                            listaCursosMatriculados[i].IdPEspecifico = resultado[i].IdCursoMoodle;
                            listaCursosMatriculados[i].FechaInicio = resultado[i].FechaCronograma.Value.ToString("dd/MM/yyyy");
                            listaCursosMatriculados[i].FechaFin = resultado[i].FechaRendicion != null ? resultado[i].FechaRendicion.Value.ToString("dd/MM/yyyy") : "-";
                            listaCursosMatriculados[i].Promedio = resultado[i].Promedio != null ? resultado[i].Promedio.Value.ToString("0") : "0";
                            listaCursosMatriculados[i].Nombre = resultado[i].NombreCurso;
                        }
                        else
                        {
                            var listado = esquemaEvaluacionService.ListadoCriteriosEvaluacionPorCurso(idMatriculaCabecera, listaCursosMatriculados[i].IdPEspecifico, 0);
                            listaCursosMatriculados[i].FechaInicio = "-";
                            listaCursosMatriculados[i].FechaFin = "-";
                            listaCursosMatriculados[i].Promedio = listado.NotaCurso == null ? "0" : Convert.ToString(listado.NotaCurso);
                        }
                    }
                }
                return listaCursosMatriculados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Juan Huanaco Quispe
        /// Fecha: 18/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Programas Especificos asociados a una matricula (modificado para el modulo de ATC Portal Academico)
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerPEspecificoPorMatriculaParaPortalAcademico(int idMatriculaCabecera)
        {
            try
            {
                var notaService = new NotaService(_unitOfWork);
                var pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                var listaCursosMatriculados = pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(idMatriculaCabecera);
                var listaNotasPorMatricula = notaService.ListadoNotaPorMatriculaCabeceraPromedio(idMatriculaCabecera);
                EsquemaEvaluacionService esquemaEvaluacionService = new EsquemaEvaluacionService(_unitOfWork);

                if (listaNotasPorMatricula != null && listaNotasPorMatricula.Count() > 0)
                {
                    foreach (var item in listaCursosMatriculados)
                    {
                        item.TipoPrograma = 1;
                        var datoNota = listaNotasPorMatricula.Where(x => x.IdPEspecifico == item.IdPEspecifico).FirstOrDefault();

                        if (datoNota != null)
                        {
                            item.IdMatriculaCabecera = idMatriculaCabecera;
                            item.FechaFin = datoNota.FechaTermino;
                            item.FechaInicio = datoNota.FechaInicio;
                            item.Promedio = datoNota.Promedio;
                        }
                        else
                        {
                            item.IdMatriculaCabecera = idMatriculaCabecera;
                            item.FechaFin = "-";
                            item.FechaInicio = "-";
                            item.Promedio = "0";
                        }
                    }
                }
                else
                {
                    var evaluacionesBO = new MoodleCronogramaEvaluacionService(_unitOfWork);
                    var resultado = evaluacionesBO.ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(idMatriculaCabecera);
                    for (int i = 0; i < listaCursosMatriculados.Count; i++)
                    {
                        listaCursosMatriculados[i].IdMatriculaCabecera = idMatriculaCabecera;
                        listaCursosMatriculados[i].TipoPrograma = 2;
                        if (i < resultado.Count)
                        {
                            listaCursosMatriculados[i].FechaInicio = resultado[i].FechaCronograma != null ? resultado[i].FechaCronograma.Value.ToString("dd/MM/yyyy") : "-";
                            listaCursosMatriculados[i].FechaFin = resultado[i].FechaRendicion != null ? resultado[i].FechaRendicion.Value.ToString("dd/MM/yyyy") : "-";
                        }
                    }
                }
                return listaCursosMatriculados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Daniel Huaita Carpio
        /// Fecha: 22/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id curso moodle mediante el ID
        /// </summaryidEspecifico
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> int </returns>
        public int? IdCursoMoodle(int idEspecifico)
        {

            try
            {
                return _unitOfWork.PEspecificoMatriculaAlumnoRepository.IdCursoMoodle(idEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Juan Diego Huanaco Quispe
        /// Fecha: 10/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene detalle de los entregables de un Alumno para un criterio de un curso en especifico.
        /// </summary>
        public List<PEspecificoCriterioDetalleEntregableDelAlumno> ObtenerCriterioDetalleEntregablesAlumno(int idCriterioEvaluacion, int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PEspecificoMatriculaAlumnoRepository.ObtenerCriterioDetalleEntregablesAlumno(idCriterioEvaluacion, idPEspecifico, idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Daniel Huaita Carpio
        /// Fecha: 22/02/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza pespecifico matricula alumno
        /// </summaryidEspecifico
        /// <param name="idMatriculaCabecera" name="IdPEspecifico"></param>
        public void ActualizacionTipoMatriculaPEspecifico(int IdPEspecifico, int IdMatriculaCabecera)
        {
            try
            {
                _unitOfWork.PEspecificoMatriculaAlumnoRepository.ActualizacionTipoMatriculaPEspecifico(IdPEspecifico, IdMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PEspecificoMatriculaAlumno Insert(PEspecificoMatriculaAlumno objetoBO)
        {
            try
            {
                var letData = _unitOfWork.PEspecificoMatriculaAlumnoRepository.Add(objetoBO);
                _unitOfWork.Commit();

                return _mapper.Map<PEspecificoMatriculaAlumno>(letData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 22/02/2023
        /// Version: 1.0
        /// <summary>
        /// Insertar nuevo curso en pespecifico matricula alumno
        /// </summaryidEspecifico
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<PEspecificoMatriculaAlumnoAgendaDTO> InsertarPEspecificoMatriculaAlumnoRepositorio(PEspecificoMatriculaAlumnoDTO pEspecificoMatriculaAlumnoDTO)
        {
            RespuestaWebDTO cronograma = new RespuestaWebDTO();
            MoodleCronogramaEvaluacionService OjetoCongelarCronograma = new MoodleCronogramaEvaluacionService(_unitOfWork);
            MdlUser moodleUser = new MdlUser();
            try
            {
                AlumnoService _repAlumno = new AlumnoService(_unitOfWork);
                MatriculaCabeceraService matriculaCabeceraRepositorio = new MatriculaCabeceraService(_unitOfWork);
                PEspecificoMatriculaAlumnoService pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                PEspecificoMatriculaAlumnoService repActualizacionMatricula = new PEspecificoMatriculaAlumnoService(_unitOfWork);
                PEspecificoService pespecificoRepositorio = new PEspecificoService(_unitOfWork);
                PaisService _repPais = new PaisService(_unitOfWork);
                CiudadService _repCiudad = new CiudadService(_unitOfWork);
                PersonalService _repPersonal = new PersonalService(_unitOfWork);
                OportunidadService _repOportunidad = new OportunidadService(_unitOfWork);

                List<PespecificoPadrePespecificoHijoDTO> listaPEspecificoPadrePespecificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();

                var matricula = matriculaCabeceraRepositorio.ObtenerMatriculaCabeceraPorId(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                var pespecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                var codigoMatricula = matriculaCabeceraRepositorio.ObtenerMatriculaCabeceraPorId(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                var nombreCursoRecuperacion = _unitOfWork.PEspecificoRepository.ObtenerNombrePEspecifico(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                var nombreCursoAnterior = _unitOfWork.PEspecificoRepository.ObtenerNombrePEspecifico(pEspecificoMatriculaAlumnoDTO.IdPEspecificoRecuperacion);
                var IdCursoMoodle = pEspecificoMatriculaAlumnoRepositorio.IdCursoMoodle(pEspecificoMatriculaAlumnoDTO.IdPespecifico);
                var oportunidad = _repOportunidad.ObtenerPorId(pEspecificoMatriculaAlumnoDTO.IdOportunidad);
                var alumno = _repAlumno.ObtenerPorId(oportunidad.IdAlumno.Value);
                var matriculaCabecera = matriculaCabeceraRepositorio.ObtenerMatriculaCabeceraPorId(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);

                repActualizacionMatricula.ActualizacionTipoMatriculaPEspecifico(pEspecificoMatriculaAlumnoDTO.IdPEspecificoRecuperacion, pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                PEspecificoMatriculaAlumno pEspecificoMatriculaAlumnoBO = new PEspecificoMatriculaAlumno();
                pEspecificoMatriculaAlumnoBO.IdMatriculaCabecera = pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera;
                pEspecificoMatriculaAlumnoBO.IdPespecifico = pEspecificoMatriculaAlumnoDTO.IdPespecifico;
                pEspecificoMatriculaAlumnoBO.IdPespecificoTipoMatricula = 2;
                pEspecificoMatriculaAlumnoBO.Estado = true;
                pEspecificoMatriculaAlumnoBO.AplicaNuevaAulaVirtual = true;
                pEspecificoMatriculaAlumnoBO.IdCursoMoodle = IdCursoMoodle;
                pEspecificoMatriculaAlumnoBO.IdUsuarioMoodle = Convert.ToInt32(moodleUser.Id);
                pEspecificoMatriculaAlumnoBO.Grupo = pEspecificoMatriculaAlumnoDTO.Grupo == 0 ? 1 : pEspecificoMatriculaAlumnoDTO.Grupo;
                pEspecificoMatriculaAlumnoBO.UsuarioCreacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                pEspecificoMatriculaAlumnoBO.UsuarioModificacion = pEspecificoMatriculaAlumnoDTO.Usuario;
                pEspecificoMatriculaAlumnoBO.FechaCreacion = DateTime.Now;
                pEspecificoMatriculaAlumnoBO.FechaModificacion = DateTime.Now;

                pEspecificoMatriculaAlumnoRepositorio.Insert(pEspecificoMatriculaAlumnoBO);
                if (pespecifico.Tipo == "Online Asincronica")
                {
                    return pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);
                }
                else
                {
                    var personal = _repPersonal.ObtenerPorId(oportunidad.IdPersonalAsignado.Value);
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "aarcana@bsginsitute.com"
                    };
                    string mensaje = "Saludos. <br> Se matriculo al codigo:<strong> " + codigoMatricula + " </strong>del Programa Especifico Anterior:<strong> " + nombreCursoAnterior +
                        "</strong> al nuevo Programa Especifico:<strong> " + nombreCursoRecuperacion + "</strong><br>Atentamente.";
                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        //Recipient = "aarcana@bsginsitute.com",
                        Recipient = "aarcana@bsginsitute.com",
                        Subject = "Recuperacion en Otra Modalidad",
                        Message = mensaje,
                        Cc = "",
                        //Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        Bcc = "",
                        //AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                        //AttachedFiles = "";
                    };
                    var mailServie = new TMK_MailService();
                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();
                    return pEspecificoMatriculaAlumnoRepositorio.ObtenerTodoFiltroAutoComplete(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera);

                }
                //return _unitOfWork.PEspecificoMatriculaAlumnoRepository.InsertarPEspecificoMatriculaAlumnoRepositorio(pEspecificoMatriculaAlumnoDTO);
            }
            catch (Exception ex)
            {
                if (cronograma.Estado == true) OjetoCongelarCronograma.EliminarUltimaVersionCongelada(pEspecificoMatriculaAlumnoDTO.IdMatriculaCabecera, moodleUser.Username);
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 09/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los cursos con sus programas y centros de costo
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<DatosCursoMatriculaDTO> ObtenerDatosCursosPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PEspecificoMatriculaAlumnoRepository.ObtenerDatosCursosPorMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}
