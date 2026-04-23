using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SolicitudOperacionesService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_SolicitudOperacionesService
    /// </summary>
    public class SolicitudOperacionesService : ISolicitudOperacionesService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SolicitudOperacionesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSolicitudOperacione, SolicitudOperaciones>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SolicitudOperaciones Add(SolicitudOperaciones entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudOperaciones>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SolicitudOperaciones Update(SolicitudOperaciones entidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SolicitudOperaciones>(modelo);
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
                _unitOfWork.SolicitudOperacionesRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudOperaciones> Add(List<SolicitudOperaciones> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudOperaciones>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SolicitudOperaciones> Update(List<SolicitudOperaciones> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SolicitudOperacionesRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SolicitudOperaciones>>(modelo);
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
                _unitOfWork.SolicitudOperacionesRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valor Nuevo a través de los parámetros
        /// </summary>
        /// <param name="aprobado"></param>
        /// <param name="realizado"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="iPlantillaBaseWhatsAppFacebook"></param>
        /// <returns></returns>
        public SolicitudOperaciones ObtenerValorNuevo(bool aprobado, bool realizado, int idOportunidad, int iPlantillaBaseWhatsAppFacebook)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerValorNuevo(aprobado, realizado, idOportunidad, iPlantillaBaseWhatsAppFacebook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene tupla por IdSolicitudOperaciones
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns></returns>
        public SolicitudOperaciones ObtenerPorIdSolicitudOperaciones(int idSolicitudOperaciones)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerPorIdSolicitudOperaciones(idSolicitudOperaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  solicitudes de operaciones realizados
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <param name="usuario"></param>
        /// <param name="observacion"></param>
        /// <returns> SolicitudOperacionesRealizadoDTO </returns>
        public SolicitudOperacionesRealizadoDTO RealizadoSolicitudOperaciones(int idSolicitudOperaciones, string usuario, string observacion)
        {
            try
            {
                var resultado = new SolicitudOperacionesRealizadoDTO();

                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                MatriculasMoodleService matriculaMoodle = new MatriculasMoodleService(_unitOfWork);
                PEspecificoService pespecificoService = new PEspecificoService(_unitOfWork);
                OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                AlumnoService alumnoService = new AlumnoService(_unitOfWork);
                PersonalService personalService = new PersonalService(_unitOfWork);

                var pEspecificoNuevaAulaVirtual = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoNuevaAulaVirtual();
                var solicitudOperaciones = this.ObtenerPorIdSolicitudOperaciones(idSolicitudOperaciones);

                solicitudOperaciones.Realizado = true;
                solicitudOperaciones.Observacion = observacion;
                solicitudOperaciones.UsuarioModificacion = usuario;
                solicitudOperaciones.FechaModificacion = DateTime.Now;
                int IdMatriculaCabecera = 0;
                string CodigoMatricula = "";

                resultado.CodigoMatricula = CodigoMatricula;
                resultado.IdMatriculaCabecera = IdMatriculaCabecera;

                if (solicitudOperaciones.IdTipoSolicitudOperaciones == 1)//centrocosto
                {
                    var Registros = matriculaCabeceraService.ObtenerRegistrosParaActualizar(solicitudOperaciones.Id);
                    IdMatriculaCabecera = Registros.IdMatriculaCabeceraV4;
                    CodigoMatricula = Registros.IdMatriculaCabeceraV3;
                    if (matriculaMoodle.QuitarMatricula(IdMatriculaCabecera, Registros.IdPespecificoV4, usuario))
                    {
                        var esCorrecto = matriculaCabeceraService.ActualizarCentroCosto(Registros);
                        if (esCorrecto)
                        {
                            RespuestaWebDTO cronograma = new RespuestaWebDTO();
                            MoodleCronogramaEvaluacionService objetoCongelarCronograma = new MoodleCronogramaEvaluacionService(_unitOfWork);
                            //MdlUser moodleUser = new MdlUser();
                            try
                            {
                                var idPlantilla = 0;
                                if (_unitOfWork.PEspecificoRepository.ExisteId(Registros.IdPespecificoV4))
                                {
                                    var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(Registros.IdPespecificoV4);
                                    if (pEspecifico.TipoId == 1 //ValorEstatico.IdModalidadCursoOnlineAsincronica
                                        && !pEspecificoNuevaAulaVirtual.Exists(x => x.Id == Registros.IdPespecificoV4))
                                    {
                                        idPlantilla = 1109;// ValorEstatico.IdPlantillaBienvenidaAlumnoAOnline;
                                        var prueba = 1109;
                                    }
                                    else
                                    {
                                        idPlantilla = 1108;//ValorEstatico.IdPlantillaBienvenidaAlumnoPresencialOnline;
                                        var prueba2 = 1108;
                                    }
                                    IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                                    var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO()
                                    {
                                        IdOportunidad = Registros.IdOportunidadV4,
                                        IdPlantilla = idPlantilla
                                    });//JST
                                    //envio correo
                                    var oportunidad = oportunidadService.ObtenerPorId(Registros.IdOportunidadV4);
                                    var personal = personalService.ObtenerPorId(oportunidad.IdPersonalAsignado.GetValueOrDefault());
                                    var alumno = alumnoService.ObtenerPorId(oportunidad.IdAlumno.GetValueOrDefault());

                                    List<string> correosPersonalizados = new List<string>
                                    {
                                        alumno.Email1
                                    };
                                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                                    {
                                        //"fvaldez@bsginstitute.com",
                                        "lhuallpa@bsginstitute.com",
                                        "controldeaccesos@bsginstitute.com",
                                        "bamontoya@bsginstitute.com",
                                        personal.Email
                                    };
                                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                                    {
                                        Sender = personal.Email,
                                        //Sender = "w.choque.itusaca@isur.edu.pe",
                                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                        Subject = resultadoReemplazo.EmailReemplazado.Asunto,
                                        Message = resultadoReemplazo.EmailReemplazado.CuerpoHTML,
                                        Cc = "",
                                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                        AttachedFiles = resultadoReemplazo.EmailReemplazado.ListaArchivosAdjuntos,
                                    };
                                    var mailServie = new TMK_MailService();
                                    //mailServie.SetData(mailDataPersonalizado);
                                    //mailServie.SendMessageTask();
                                }
                            }
                            catch (Exception e)
                            {
                                if (cronograma.Estado == true) objetoCongelarCronograma.EliminarUltimaVersionCongelada(IdMatriculaCabecera, usuario);//moodleUser.Username);
                            }
                        }
                    }
                    else
                    {
                        solicitudOperaciones.Realizado = false;
                    }
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 3)//Version
                {
                    try
                    {
                        var Registros = matriculaCabeceraService.ObtenerRegistrosParaActualizarVersion(solicitudOperaciones.Id);
                        IdMatriculaCabecera = Registros.IdMatriculaCabeceraV4;
                        CodigoMatricula = Registros.IdMatriculaCabeceraV3;

                        var matriculacabecera = matriculaCabeceraService.ObtenerMatriculaCabeceraPorId(IdMatriculaCabecera);

                        int valorNuevo = 0;
                        switch (solicitudOperaciones.ValorNuevo)
                        {
                            case "Básica":
                                valorNuevo = 1;
                                break;
                            case "Basica":
                                valorNuevo = 1;
                                break;
                            case "Profesional":
                                valorNuevo = 2;
                                break;
                            case "Gerencial":
                                valorNuevo = 3;
                                break;
                        }
                        matriculacabecera.IdPaquete = valorNuevo;
                        matriculacabecera.FechaModificacion = DateTime.Now;
                        matriculacabecera.UsuarioModificacion = usuario;
                        matriculacabecera.Estado = true;
                        matriculacabecera.IdEstadoMatricula = 1;

                        //if (matriculacabecera.IdCronograma == 0 || matriculacabecera.IdCronograma == null)
                        //{
                        //    throw new Exception("Id Cronograma no tiene valor");
                        //}

                        var resultadoEliminacion = matriculaCabeceraService.EliminarBeneficiosMatriculaCabeceraIdMatricula(IdMatriculaCabecera);
                        var listaNuevosBeneficios = matriculaCabeceraService.InsertarBeneficiosMatriculaCabeceraIdMatricula(IdMatriculaCabecera, valorNuevo, (int)(matriculacabecera.IdCronograma?? 0));
                        matriculaCabeceraService.Update(matriculacabecera);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                this.Update(solicitudOperaciones);
                try
                {
                    if (solicitudOperaciones.IdPersonalSolicitante != 0)
                    {
                        AgendaSocket.getInstance().SolicitudOperacionesRealizadaCancelada(solicitudOperaciones.IdOportunidad, solicitudOperaciones.IdPersonalSolicitante, 1);
                    }
                }
                catch (Exception)
                {
                }
                resultado.CodigoMatricula = CodigoMatricula;
                resultado.IdMatriculaCabecera = IdMatriculaCabecera;
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  solicitudes de operaciones realizados
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns>  List<DatosSolicitudOperacionesDTO> </returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperaciones(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerSolicitudOperaciones(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las   solicitudes de operaciones realizados
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns>  List<DatosSolicitudOperacionesDTO> </returns>
        public List<TodoSolicitudOperacionesDTO> ObtenerTodoSolicitudOperaciones()
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerTodoSolicitudOperaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<TodoSolicitudOperacionesDTO> ObtenerTodoFiltroOperaciones(filtroReporteDTO filtroSolicitudReporte)
        {
            try
            {
                TodoSolicitudOperacionesDTO TodoSolicitud = new TodoSolicitudOperacionesDTO();

                    filtroReportetipo4DTO filtro = new filtroReportetipo4DTO
                    {
                        fechaFin = filtroSolicitudReporte.fechaFin,
                        fechaInicio = filtroSolicitudReporte.fechaInicio,
                        asesores = filtroSolicitudReporte.asesores,
                        estadoSolicitud = filtroSolicitudReporte.estadoSolicitud,
                        tipoSolicitud = filtroSolicitudReporte.tipoSolicitud
                    };

                return _unitOfWork.SolicitudOperacionesRepository.ObtenerTodoFiltroOperaciones(filtro);
   

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<TipoSolicitudDTO> ObtenerTipoSolicitud()
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerTipoSolicitud();
            }
            catch (Exception ex)
            {
                throw ex;

                /// Autor: Jashin Salazar Taco
                /// Fecha: 02/11/2022
                /// Version: 1.0
                /// <summary>
                /// Obtiene  solicitudes de operaciones realizados
                /// </summary>
                /// <param name="idSolicitudOperaciones"></param>
                /// <param name="usuario"></param>
                /// <param name="observacion"></param>
                /// <returns> SolicitudOperacionesRealizadoDTO </returns>
                /// 
                /// }

            }
        }
        public SolicitudOperaciones CancelarSolicitudOperaciones(int idSolicitudOperaciones, string usuario, string observacion)
                {
            try
            {
                MatriculaCabeceraService matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);
                var solicitudOperaciones = this.ObtenerPorIdSolicitudOperaciones(idSolicitudOperaciones);
                solicitudOperaciones.EsCancelado = true;
                solicitudOperaciones.Observacion = observacion;
                solicitudOperaciones.UsuarioModificacion = usuario;
                solicitudOperaciones.FechaModificacion = DateTime.Now;

                this.Update(solicitudOperaciones);

                try
                {
                    if (solicitudOperaciones.IdPersonalSolicitante != 0)
                    {
                        AgendaSocket.getInstance().SolicitudOperacionesRealizadaCancelada(solicitudOperaciones.IdOportunidad, solicitudOperaciones.IdPersonalSolicitante, 0);
                    }
                }
                catch (Exception)
                {
                }

                return solicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene solicitud de Operaciones Realizadas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<DatosSolicitudOperacionesDTO </returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesRealizadas(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerSolicitudOperacionesRealizadas(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 31/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene historial de asesoras
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<DatosSolicitudOperacionesDTO </returns>
        public List<HistorialAsesoraDTO> ObtenerHistorialAsesora(int IdMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerHistorialAsesora(IdMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
		/// Se obtiene el historial de acceso temporal por IdOportunidad
		/// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
		/// <returns>Lista de datos de solicitud de operaciones</returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerHistorialAccesoTemporal(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerHistorialAccesoTemporal(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="valorNuevo"></param>
        /// <returns> IntDTO </returns>
        public IntDTO ValidarCambioSubEstado(int idOportunidad, string valorNuevo)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ValidarCambioSubEstado(idOportunidad, valorNuevo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> IntDTO </returns>
        public IntDTO ActualizarTerminosPortalWeb(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ActualizarTerminosPortalWeb(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Aprobar solicitud de cambio categoria
        /// </summary> 
        public void AprobarCambioCategoriaAlumno(int idOportunidad, String categoria)
        {
            try
            {
                _unitOfWork.SolicitudOperacionesRepository.AprobarCambioCategoriaAlumno(idOportunidad, categoria);
            }
            catch (Exception ex)
            {

            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un valor bool por condiciones de la query
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns> bool </returns>
        public bool ExisteTotal(int idOportunidad, int idTipoSolicitudOperaciones)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ExisteTotal(idOportunidad, idTipoSolicitudOperaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta Solicitud de varias Operaciones
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> Entidad: SolicitudOperaciones </returns>
        public SolicitudOperaciones InsertarSolicitudOperaciones(SolicitudOperacionesDTO obj)
        {
            try
            {
                string nombreArchivo = string.Empty;
                string contentType = string.Empty;

                SolicitudOperacionesService solicitudOperacionesService = new SolicitudOperacionesService(_unitOfWork);

                if (solicitudOperacionesService.ExisteTotal(obj.IdOportunidad, obj.IdTipoSolicitudOperaciones))
                {
                    throw new Exception("La oportunidad ya tiene una solicitud de este tipo");
                }

                if (obj.IdTipoSolicitudOperaciones == 4 && obj.ValorNuevoSubestado != null)
                {
                    var cumplecriterio = solicitudOperacionesService.ValidarCambioSubEstado(obj.IdOportunidad, obj.ValorNuevoSubestado); //0 :no cumple con los requisitos , 1 :cumple pero no requiere validar la informacion portal web, 2 :cumple pero si requiere validar la informacion portal web
                    if (cumplecriterio.Valor == 0)
                    {
                        throw new Exception("No cumple con los criterios para pasar al nuevo subestado");
                    }
                }

                // Valido cambio de sbetsado para aplicar sus reglas y definir si pasa
                if (obj.IdTipoSolicitudOperaciones == 5)
                {
                    var cumplecriterio = solicitudOperacionesService.ValidarCambioSubEstado(obj.IdOportunidad, obj.ValorNuevo); //0 :no cumple con los requisitos , 1 :cumple pero no requiere validar la informacion portal web, 2 :cumple pero si requiere validar la informacion portal web
                    if (cumplecriterio.Valor == 0)
                    {
                        throw new Exception("No cumple con los criterios para pasar al nuevo subestado");
                    }
                    else if (cumplecriterio.Valor == 1)// No valida informacion portal web
                    {
                        // Pasa noma // No se hace nada
                    }
                    else if (cumplecriterio.Valor == 2)// Valida informacion portal web
                    {
                        // Actualiza campo terminos a false
                        var acualizado = solicitudOperacionesService.ActualizarTerminosPortalWeb(obj.IdOportunidad);

                        // Aqui ira el correo con la plantilla para el alumno
                    }
                }

                if (obj.IdTipoSolicitudOperaciones == 7)
                {
                    string fecha = obj.ValorNuevo;
                    string format = "dd/MM/yyyy";
                    DateTime fechaDia = DateTime.ParseExact(fecha, format, CultureInfo.InvariantCulture);
                    obj.ValorNuevo = fechaDia.ToString("yyyy/MM/dd");
                }
                if (obj.IdTipoSolicitudOperaciones == 9)
                {
                    solicitudOperacionesService.AprobarCambioCategoriaAlumno(obj.IdOportunidad, obj.ValorNuevo);
                }

                SolicitudOperaciones solicitudOperaciones = new SolicitudOperaciones
                {
                    IdOportunidad = obj.IdOportunidad,
                    IdTipoSolicitudOperaciones = obj.IdTipoSolicitudOperaciones,
                    FechaSolicitud = DateTime.Now,
                    IdPersonalSolicitante = obj.IdPersonalSolicitante,
                    IdPersonalAprobacion = obj.IdPersonalAprobacion,
                    ValorAnterior = obj.ValorAnterior,
                    ValorNuevo = obj.ValorNuevo,
                    Aprobado = obj.Aprobado,
                    EsCancelado = false,
                    ComentarioSolicitante = obj.ComentarioSolicitante,
                    Observacion = obj.Observacion,
                    IdUrlBlockStorage = 1,
                    NombreArchivo = nombreArchivo,
                    ContentType = contentType,
                    Realizado = false,
                    ObservacionEncargado = obj.ObservacionEncargado,
                    Estado = true,
                    UsuarioCreacion = obj.Usuario,
                    UsuarioModificacion = obj.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    RelacionEstadoSubEstado = obj.RelacionEstadoSubEstado
                };

                solicitudOperaciones = solicitudOperacionesService.Add(solicitudOperaciones);

                if (obj.IdTipoSolicitudOperaciones == 8) /*Accesos temporales*/
                {
                    foreach (var idPEspecifico in obj.ListaIdPEspecificos)
                    {
                        SolicitudOperacionesAccesoTemporalDetalle solicitudOperacionesAccesoTemporal = new SolicitudOperacionesAccesoTemporalDetalle
                        {
                            IdSolicitudOperaciones = solicitudOperaciones.Id,
                            IdPEspecifico = idPEspecifico,
                            Estado = true,
                            UsuarioCreacion = obj.Usuario,
                            UsuarioModificacion = obj.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        _unitOfWork.SolicitudOperacionesAccesoTemporalDetalleRepository.Add(solicitudOperacionesAccesoTemporal);
                        _unitOfWork.Commit();
                    }
                }
                return solicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdMatriculaCabecera por la oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> int: respuesta.Valor </returns>
        public int ObtenerMatriculaPorOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SolicitudOperacionesRepository.ObtenerMatriculaPorOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Registra en el nuevo aula virtual los cursos de prueba segun la solicitud ingresada
        /// </summary>
        /// <param name="idSolicitudOperaciones">Id de la solicitud de operaciones (PK de la tabla ope.T_SolicitudOperaciones)</param>
        public void RegistrarCursoPrueba(int idSolicitudOperaciones)
        {
            try
            {
                _unitOfWork.SolicitudOperacionesRepository.RegistrarCursoPrueba(idSolicitudOperaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Amplia Accesos Temporales
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="fechaExpiracion"></param>
        /// <param name="idPEspecifico"></param>
        public void AmpliacionAccesosTemporales(int idAlumno, string fechaExpiracion, string idPEspecifico)
        {
            try
            {
                _unitOfWork.SolicitudOperacionesRepository.AmpliacionAccesosTemporales(idAlumno, fechaExpiracion, idPEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DatosSolicitudOperacionesDTO> ObtenerConfirmacionSolicitudes(int id)
        {
            try
            {


                //SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                List<DatosSolicitudOperacionesDTO> rpta = new List<DatosSolicitudOperacionesDTO>();

                var resultado = _unitOfWork.SolicitudOperacionesRepository.ObtenerPorIdAprobadoSolicitudOperaciones(id);
                //    _repSolicitudOperaciones.GetBy(x => x.Id == Id && x.Aprobado == false).FirstOrDefault();
                if (resultado != null)
                {

                    var llamarDatos = _unitOfWork.SolicitudOperacionesRepository.ObtenerSolicitudOperacionesEnBloque(resultado.IdOportunidad);
                    //_repSolicitudOperaciones.ObtenerSolicitudOperacionesEnBloque(resultado.IdOportunidad);
                    var resultadoEstado = new DatosSolicitudOperacionesDTO();
                    var resultadoSubEstado = new List<DatosSolicitudOperacionesDTO>();

                    if (resultado.IdTipoSolicitudOperaciones == 4)
                    {
                        resultadoEstado = llamarDatos.Where(x => x.Id == id && x.Aprobado == false).FirstOrDefault();
                        resultadoSubEstado = llamarDatos.Where(x => x.RelacionEstadoSubEstado == id && x.Aprobado == false).ToList();
                    }
                    else if (resultado.IdTipoSolicitudOperaciones == 5)
                    {
                        resultadoSubEstado = llamarDatos.Where(x => x.Id == id && x.Aprobado == false).ToList();
                        resultadoEstado = llamarDatos.Where(x => x.Id == resultado.RelacionEstadoSubEstado && x.Aprobado == false).FirstOrDefault();
                    }

                    if (resultadoEstado != null)
                    {
                        rpta.Add(resultadoEstado);
                    }
                    if (resultadoSubEstado != null && resultadoSubEstado.Count > 0)
                    {
                        rpta.AddRange(resultadoSubEstado);
                    }

                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public bool envioPlantillaOperaciones(string Remitente, string CodigoAlumno, string Destinatarios, int IdPlantilla)
        //{
        //    try
        //    {

        //        //asi estaba haciendolo esta bien ??
        //       if (!_unitOfWork.MatriculaCabeceraRepository.Exist(x => x.CodigoMatricula == CodigoAlumno))
        //        {
        //            return false;
        //        }



        //        //_unitOfWork.SolicitudCertificadoFisicoRepository.Exist(x=>x.IdEstadoCertificadoFisico=1)
        //        //if (!_repMatriculaCabecera.Exist(x => x.CodigoMatricula == CodigoAlumno))
        //        //{
        //        //    return BadRequest("Codigo de alumno no valido!");
        //        //}0
        //        //Efe, usa nomas el exists
        //        var matriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorCodigoMatricula(CodigoAlumno);
        //        var detalleMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetalleMatricula(matriculaCabecera.Id);

        //        //if (!_repAlumno.Exist(matriculaCabecera.IdAlumno))
        //        //{
        //        //    return BadRequest(ModelState);
        //        //}
        //        if (!_unitOfWork.AlumnoRepository.Exist(x => x.Id == matriculaCabecera.IdAlumno))
        //        {
        //            return false;
        //        }

        //        if(!_unitOfWork.PlantillaRepository.Exist(IdPlantilla))
        //        {
        //            return false;
        //        }

        //        //if (!_repPlantilla.Exist(IdPlantilla))
        //        //{
        //        //    return BadRequest(ModelState);
        //        //}
        //        var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(IdPlantilla);
        //        //var plantilla = _repPlantilla.FirstById(IdPlantilla);
        //        if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
        //        {
        //            return false;
        //        }

        //        //if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
        //        //{
        //        //    return BadRequest(ModelState);
        //        //}

        //        //var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
        //        var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(IdPlantilla);



        //        var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(matriculaCabecera.IdAlumno);
        //        // _repAlumno.FirstById(matriculaCabecera.IdAlumno);


        //        var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(detalleMatriculaCabecera.IdOportunidad);
        //        //  _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
        //        var personal = _unitOfWork.PersonalRepository.ObtenerPorId((int)oportunidad.IdPersonalAsignado);
        //        //   _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

        //        ReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
        //        reemplazoEtiquetaPlantillaService.reemplazoEtiquetaPlantillaBoDTO = new ReemplazoEtiquetaPlantillaDTO();
        //        {
        //            IdOportunidad = oportunidad.Id,
        //            IdPlantilla = IdPlantilla
        //        };
        //        reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(reemplazoEtiquetaPlantillaService.reemplazoEtiquetaPlantillaBoDTO);

        //        //_reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
        //        //{
        //        //    IdOportunidad = oportunidad.Id,
        //        //    IdPlantilla = IdPlantilla
        //        //};
        //        //_reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

        //        //_unitOfWork.PlantillaRepository.

        //        var destinatarios = Destinatarios.Split(";");

        //        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
        //        {

        //            var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
        //            List<string> correosPersonalizadosCopia = new List<string>();
        //            //cuando la plantilla es condiciones y caracteristicas
        //            //1227	Condiciones y Características - PERÚ OPERACIONES
        //            //1245	Condiciones y Características - COLOMBIA OPERACIONES
        //            if (Remitente == "matriculas@bsginstitute.com" && (IdPlantilla == 1227 || IdPlantilla == 1245))
        //            {
        //                correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
        //            }
        //            List<string> correosPersonalizadosCopiaOculta = new List<string>
        //            {
        //                "lhuallpa@bsginstitute.com",
        //            };

        //            List<string> correosPersonalizados = new List<string>
        //            {
        //            };
        //            correosPersonalizados.AddRange(destinatarios.ToList());

        //            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
        //            {
        //                Sender = Remitente,
        //                //Sender = personal.Email,
        //                //Sender = "w.choque.itusaca@isur.edu.pe",
        //                Recipient = string.Join(",", correosPersonalizados.Distinct()),
        //                Subject = emailCalculado.Asunto,
        //                Message = emailCalculado.CuerpoHTML,
        //                Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
        //                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
        //                AttachedFiles = emailCalculado.ListaArchivosAdjuntos
        //            };
        //            var mailServie = new TMK_MailServiceImpl();

        //            mailServie.SetData(mailDataPersonalizado);
        //            mailServie.SendMessageTask();

        //            //logica guardar envio
        //            var gmailCorreo = new GmailCorreoBO
        //            {
        //                IdEtiqueta = 1,//sent:1 , inbox:2
        //                Asunto = emailCalculado.Asunto,
        //                Fecha = DateTime.Now,
        //                EmailBody = emailCalculado.CuerpoHTML,
        //                Seen = false,
        //                Remitente = Remitente,
        //                Cc = "",
        //                Bcc = "",
        //                Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
        //                IdPersonal = personal.Id,
        //                Estado = true,
        //                FechaCreacion = DateTime.Now,
        //                FechaModificacion = DateTime.Now,
        //                UsuarioCreacion = "SYSTEM",
        //                UsuarioModificacion = "SYSTEM",
        //                IdClasificacionPersona = oportunidad.IdClasificacionPersona
        //            };
        //            var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
        //            _repGmailCorreo.Insert(gmailCorreo);
        //        }
        //        else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
        //        {//logica whatsapp
        //            var whatsAppCalculado = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

        //            var listaWhatsappConjuntoListaResultado = new List<WhatsAppResultadoConjuntoListaDTO>();

        //            foreach (var destinatario in destinatarios)
        //            {
        //                listaWhatsappConjuntoListaResultado.Add(new WhatsAppResultadoConjuntoListaDTO()
        //                {
        //                    IdAlumno = alumno.Id,
        //                    Celular = destinatario,
        //                    IdPersonal = personal.Id,
        //                    IdCodigoPais = alumno.IdCodigoPais ?? default,
        //                    IdConjuntoListaResultado = 0,
        //                    IdPgeneral = null,
        //                    IdPlantilla = IdPlantilla,
        //                    NroEjecucion = 1,
        //                    objetoplantilla = whatsAppCalculado.ListaEtiquetas,
        //                    Plantilla = whatsAppCalculado.Plantilla,
        //                    Validado = false
        //                });
        //            }

        //            this.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
        //            listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado == true).ToList();
        //            listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
        //            this.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
        //        }
        //        return Ok(true);



        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


    }
}
