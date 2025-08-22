using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;


namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: OportunidadVerificadaService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_OportunidadIsVerificadum
    /// </summary>
    public class OportunidadIsVerificadaService : IOportunidadIsVerificadaService

    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OportunidadIsVerificadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<TOportunidadIsVerificadum, OportunidadIsVerificada>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadIsVerificadum, OportunidadIsVerificadaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadIsVerificadaDTO, OportunidadIsVerificada>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConfiguracionAsignacionCoordinadorOportunidadOperacione, ConfiguracionAsignacionCoordinadorOportunidadOperacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TMatriculaCabecera, MatriculaCabecera>(MemberList.None).ReverseMap();
                cfg.CreateMap<Plantilla, TPlantilla>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);

        }

        #region Metodos Base
        public OportunidadIsVerificada Add(OportunidadIsVerificada entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadIsVerificadaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadIsVerificada>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadIsVerificada Update(OportunidadIsVerificada entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadIsVerificadaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadIsVerificada>(modelo);
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
                _unitOfWork.OportunidadIsVerificadaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadIsVerificada> Add(List<OportunidadIsVerificada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadIsVerificadaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadIsVerificada>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadIsVerificada> Update(List<OportunidadIsVerificada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadIsVerificadaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadIsVerificada>>(modelo);
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
                _unitOfWork.OportunidadIsVerificadaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene tupla ya sea por idOportunidad o idMatriculaCabecera
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idOportunidadMatriculaCabecera"></param>
        /// <returns></returns>
        public OportunidadIsVerificada ObtenerPorIdOportunidadOIdMatriculaCabecera(int idOportunidad, int idOportunidadMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.OportunidadIsVerificadaRepository.ObtenerPorIdOportunidadOIdMatriculaCabecera(idOportunidad, idOportunidadMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidades verificacdas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idOportunidadMatriculaCabecera"></param>
        /// <returns></returns>

        public List<OportunidadesVerificadasDTO> ObtenerOportunidadesVerificadas()
        {
            try
            {
                var _repOportunidadIsVerificada = _unitOfWork.OportunidadIsVerificadaRepository;
                var oportunidadesVerificadas = _repOportunidadIsVerificada.ObtenerOportunidadesVerificadas();
                return (oportunidadesVerificadas);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidades verificacdas coon periodo y sin periodo
        /// </summary>
        /// <returns></returns>

        public object ObtenerOportunidadesISM()
        {

            try
            {
                var oportunidadVerificacion = new List<OportunidadIsVerificadaDTO>();
                var _repOportunidadIsVerificada = _unitOfWork.OportunidadIsVerificadaRepository;

                {
                    oportunidadVerificacion = _repOportunidadIsVerificada.ObtenerOportunidadIsVerificadaSinPeriodo();
                }

                return (oportunidadVerificacion);
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene listad de periodo por mes
        /// </summary>
        /// <returns></returns>

        public object ObtenerCombosVerificacionOportunidadISM()
        {

            try
            {
                var _repPeriodo = _unitOfWork.PeriodoRepository;
                var periodos = _repPeriodo.ObtenerPeriodos();
                return (periodos);
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        public object InsertarOportunidadVerificadaV3(OportunidadVerificadaDTO OportunidadVerificada, string usuario)
        {

            try
            {
                //Validacion UsuarioMoodle

                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repAlumno = _unitOfWork.AlumnoRepository;
                var _repPEspecifico = _unitOfWork.PEspecificoRepository;

                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;

                var matricula = _repMatriculaCabecera.FirstById(OportunidadVerificada.IdMatriculaCabecera);
                var pespecifico = _repPEspecifico.FirstById(matricula.IdPespecifico);
                /*var pEspecificoNuevaAulaVirtual = _repPEspecifico.ObtenerPEspecificoNuevaAulaVirtual();
                if (pEspecificoNuevaAulaVirtual.Exists(x => x.Id == pespecifico.Id))
                {*/
                var oportunidad = _repOportunidad.FirstById(OportunidadVerificada.IdOportunidad);
                var alumno = _repAlumno.FirstById(oportunidad.IdAlumno.Value);

                //Si se matriculo correctamente se hace la asignacion de coordinadora
                var configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(_unitOfWork);
                var _repPersonal = _unitOfWork.PersonalRepository;

                var configuracion = configuracionCoordinador.ObtenerCoordinadorAsignacion(matricula.IdPespecifico, matricula.IdEstadoMatricula == 0 ? 1 : matricula.IdEstadoMatricula, matricula.IdSubEstadoMatricula, matricula.Id);
                var personal = _repPersonal.FirstById(configuracion.IdPersonal);
                matricula.UsuarioCoordinadorAcademico = configuracion.UsuarioPersonal;
                matricula.UsuarioModificacion = OportunidadVerificada.Usuario;
                matricula.FechaModificacion = DateTime.Now;

                _repMatriculaCabecera.Update(matricula);


                try
                {
                    _repMatriculaCabecera.ActualizarTMatriculaCabecera(matricula.CodigoMatricula, configuracion.UsuarioPersonal);
                }
                catch (Exception e)
                {
                }
                _unitOfWork.Commit();
                //Crear Oportunidad
                OportunidadService oportunidadBO = new OportunidadService(_unitOfWork);
                int idOportunidadOpe;
                var oportunidadOperacionesExiste = _repOportunidad.ObtenerOportunidadOperacionesPorIdMatricula(matricula.Id);
                if (oportunidadOperacionesExiste == null)
                {
                    var oportunidadOperaciones = oportunidadBO.GenerarOportunidadOperacionesConParametros(oportunidad.Id, OportunidadVerificada.Usuario, oportunidad.IdCentroCosto.Value, 47, configuracion.IdPersonal, matricula.Id);
                    _unitOfWork.OportunidadClasificacionOperacionesRepository.CalcularPorIdOportunidad(oportunidadOperaciones.Id.Value);
                    idOportunidadOpe = oportunidadOperaciones.Id.Value;
                }
                else
                {
                    if (oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones == null)
                    {
                        _unitOfWork.OportunidadClasificacionOperacionesRepository.CalcularPorIdOportunidad(oportunidadOperacionesExiste.Id);
                    }
                    idOportunidadOpe = oportunidadOperacionesExiste.Id;
                }
                _unitOfWork.Commit();
                //Envia Correo

                var _repPlantilla = _unitOfWork.PlantillaRepository;
                var _repPlantillaBase = _unitOfWork.PlantillaBaseRepository;
                Plantilla plantilla;
                EnvioMasivoPlantillaService _envioMasivoPlantilla = new EnvioMasivoPlantillaService(_unitOfWork);


                plantilla = _mapper.Map<Plantilla>(_repPlantilla.FirstBy(x => x.Nombre.Contains("Bienvenida") && x.Nombre.Contains("Presencial")));

                if (plantilla != null)
                {
                    //var envioCorreo = plantillaController.EnvioC(coordinador.Email, matricula.CodigoMatricula, alumno.Email1, plantilla.Id);
                    if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                    {
                        //
                        throw new Exception(""); ;
                    }

                    var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(plantilla.Id);
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                       "lhuallpa@bsginstitute.com",
                        "controldeaccesos@bsginstitute.com",
                        "bamontoya@bsginstitute.com",
                        "jcacerest@bsginstitute.com",
                        personal.Email
                    };


                    var resultadoReemplazo = new ReemplazoEtiquetaPlantillaService(_unitOfWork).ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO()
                    {
                        IdOportunidad = idOportunidadOpe,
                        IdPlantilla = plantilla.Id
                    });

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        var emailCalculado = resultadoReemplazo.EmailReemplazado;
                        var archivosAdjuntos = _envioMasivoPlantilla.ObtenerArchivosAdjuntos(emailCalculado.CuerpoHTML);
                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = personal.Email,
                            Recipient = alumno.Email1,
                            Subject = emailCalculado.Asunto,
                            Message = _envioMasivoPlantilla.QuitarEtiquetasArchivosAdjuntos(emailCalculado.CuerpoHTML),
                            Cc = "",
                            Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            AttachedFiles = archivosAdjuntos
                        };
                        var mailServie = new TMK_MailService();

                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();
                    }
                }

                //Inserta en la tabla de verificacion
                var _repOportunidadIsVerificada = _unitOfWork.OportunidadIsVerificadaRepository;
                var oportunidadVerificada = _repOportunidadIsVerificada.FirstBy(x => x.IdOportunidad == OportunidadVerificada.IdOportunidad || x.IdMatriculaCabecera == OportunidadVerificada.IdMatriculaCabecera);
                if (oportunidadVerificada == null)
                {
                    OportunidadIsVerificada oportunidadIsVerificada = new OportunidadIsVerificada();
                    oportunidadIsVerificada.IdOportunidad = OportunidadVerificada.IdOportunidad;
                    oportunidadIsVerificada.IdMatriculaCabecera = OportunidadVerificada.IdMatriculaCabecera;
                    oportunidadIsVerificada.Verificado = OportunidadVerificada.Verificado;
                    oportunidadIsVerificada.Estado = true;
                    oportunidadIsVerificada.UsuarioCreacion = OportunidadVerificada.Usuario;
                    oportunidadIsVerificada.UsuarioModificacion = usuario;
                    oportunidadIsVerificada.FechaCreacion = DateTime.Now;
                    oportunidadIsVerificada.FechaModificacion = DateTime.Now;

                    _repOportunidadIsVerificada.Add(oportunidadIsVerificada);
                    _unitOfWork.Commit();

                    return (oportunidadIsVerificada);
                }
                else
                {
                    return (oportunidadVerificada);
                }
                /*}
                else
                {
                    return BadRequest("El Programa seleccionado no tiene una relación con el Aula Virtual.");
                }*/
            }
            catch (Exception e)
            {
                //SE REVIERTE LA ASIGNACION EN MATRICULA
                var _repMatriculaCabecera = _unitOfWork.MatriculaCabeceraRepository;
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var matricula = _repMatriculaCabecera.FirstById(OportunidadVerificada.IdMatriculaCabecera);
                matricula.UsuarioCoordinadorAcademico = "0";
                matricula.UsuarioModificacion = OportunidadVerificada.Usuario;
                matricula.FechaModificacion = DateTime.Now;

                _repMatriculaCabecera.Update(matricula);
                _unitOfWork.Commit();
                _repMatriculaCabecera.ActualizarTMatriculaCabecera(matricula.CodigoMatricula, "0");
                _unitOfWork.Commit();
                //Si se creo la oportunidad de operaciones se revierte
                var oportunidadOperacionesExiste = _repOportunidad.ObtenerOportunidadOperacionesPorIdMatricula(matricula.Id);
                if (oportunidadOperacionesExiste != null)
                {
                    if (oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones != null)
                    {
                        var _repOportunidadClasificacionOperaciones = _unitOfWork.OportunidadClasificacionOperacionesRepository;
                        var opClasificacion = _repOportunidadClasificacionOperaciones.FirstById(oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones.Value);
                        if (opClasificacion != null)
                        {
                            _repOportunidad.EliminarOportunidadFisicaOperacionesV3V4(opClasificacion.IdOportunidad);
                            _unitOfWork.Commit();
                        }
                    }
                }
                return (e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 07/02/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta Oportunidad Verificadad
        /// </summary>
        /// <returns></returns>
        public OportunidadIsVerificadaDTO InsertarOportunidadVerificada(OportunidadCodigoMatriculaDTO dto)
        {
            try
            {

                var matricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorId(dto.IdMatriculaCabecera);

                if (matricula == null)
                {
                    throw new BadRequestException($"No existe la matricula cabecera {dto.IdMatriculaCabecera}");
                }
                if (matricula.EstadoMatricula == "pormatricular")
                {
                    throw new BadRequestException($"El estado de la matricula debe ser diferente a {matricula.EstadoMatricula}");
                }
                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(dto.IdOportunidad);
                if (oportunidad == null)
                {
                    throw new BadRequestException($"No existe la oportunidad {dto.IdOportunidad}");
                }
                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno!.Value);
                if (alumno == null)
                {
                    throw new BadRequestException($"No existe el alumno {oportunidad.IdAlumno.Value}");
                }
                //Si se matriculo correctamente se hace la asignacion de coordinadora
                IConfiguracionAsignacionCoordinadorOportunidadOperacionService configuracionCoordinadorService = new ConfiguracionAsignacionCoordinadorOportunidadOperacionService(_unitOfWork);
                var configuracion = configuracionCoordinadorService.ObtenerCoordinadorAsignacion(matricula.IdPespecifico, matricula.IdEstadoMatricula, matricula.IdSubEstadoMatricula, matricula.Id);
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(configuracion.IdPersonal);
                if (personal == null)
                {
                    throw new BadRequestException($"No existe el personal {configuracion.IdPersonal}");
                }
                matricula.UsuarioCoordinadorAcademico = configuracion.UsuarioPersonal;
                matricula.UsuarioModificacion = dto.Usuario;
                matricula.FechaModificacion = DateTime.Now;
                _unitOfWork.MatriculaCabeceraRepository.Update(matricula);
                _unitOfWork.Commit();
                try
                {
                    _unitOfWork.MatriculaCabeceraRepository.ActualizarTMatriculaCabecera(matricula.CodigoMatricula, configuracion.UsuarioPersonal);
                }
                catch (Exception ex)
                {
                }
                //Crear Oportunidad
                int idOportunidadOpe;
                var oportunidadOperacionesExiste = _unitOfWork.OportunidadRepository.ObtenerOportunidadOperacionesPorIdMatricula(matricula.Id);

                if (oportunidadOperacionesExiste == null)
                {
                    var oportunidadOperaciones = _unitOfWork.OportunidadRepository.GenerarOportunidadOperacionesConParametros(oportunidad.Id, dto.Usuario, oportunidad.IdCentroCosto.Value, 47, configuracion.IdPersonal, matricula.Id);
                    _unitOfWork.OportunidadClasificacionOperacionesRepository.CalcularPorIdOportunidad(oportunidadOperaciones.Id.Value);
                    idOportunidadOpe = oportunidadOperaciones.Id.Value;
                }
                else
                {
                    if (oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones == null)
                    {
                        _unitOfWork.OportunidadClasificacionOperacionesRepository.CalcularPorIdOportunidad(oportunidadOperacionesExiste.Id);
                    }
                    idOportunidadOpe = oportunidadOperacionesExiste.Id;
                }

                //Envia Correo
                EnvioMasivoPlantillaService envioMasivoPlantillaService = new EnvioMasivoPlantillaService(_unitOfWork);

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorNombre("Bienvenida", "Presencial");
                if (plantilla != null)
                {
                    var condicion = _unitOfWork.PlantillaBaseRepository.ObtenerPorId(plantilla.IdPlantillaBase);
                    if (condicion == null)
                    {
                        throw new BadRequestException("No existe la plantilla base");
                    }

                    var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(plantilla.Id);
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "lhuallpa@bsginstitute.com",
                        "controldeaccesos@bsginstitute.com",
                        "bamontoya@bsginstitute.com",
                        "afloresl@bsginstitute.com",
                        "dpacheco@bsginstitute.com",
                        personal.Email,
                    };

                    IReemplazoEtiquetaPlantillaService reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                    var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO()
                    {
                        IdOportunidad = idOportunidadOpe,
                        IdPlantilla = plantilla.Id
                    });

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        var emailCalculado = resultadoReemplazo.EmailReemplazado;
                        var archivosAdjuntos = envioMasivoPlantillaService.ObtenerArchivosAdjuntos(emailCalculado.CuerpoHTML);
                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = personal.Email,
                            Recipient = alumno.Email1,
                            Subject = emailCalculado.Asunto,
                            Message = envioMasivoPlantillaService.QuitarEtiquetasArchivosAdjuntos(emailCalculado.CuerpoHTML),
                            Cc = "",
                            Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            AttachedFiles = archivosAdjuntos
                        };
                        var mailServie = new TMK_MailService();
                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();
                    }
                }

                //Inserta en la tabla de verificacion
                var oportunidadVerificada = _unitOfWork.OportunidadIsVerificadaRepository.ObtenerPorIdOportunidadOIdMatriculaCabecera(dto.IdOportunidad, dto.IdMatriculaCabecera);

                if (oportunidadVerificada == null || oportunidadVerificada.Id == 0)
                {
                    OportunidadIsVerificada oportunidadIsVerificada = new OportunidadIsVerificada();
                    oportunidadIsVerificada.IdOportunidad = dto.IdOportunidad;
                    oportunidadIsVerificada.IdMatriculaCabecera = dto.IdMatriculaCabecera;
                    oportunidadIsVerificada.Verificado = dto.Verificado;
                    oportunidadIsVerificada.Estado = true;
                    oportunidadIsVerificada.UsuarioCreacion = dto.Usuario;
                    oportunidadIsVerificada.UsuarioModificacion = dto.Usuario;
                    oportunidadIsVerificada.FechaCreacion = DateTime.Now;
                    oportunidadIsVerificada.FechaModificacion = DateTime.Now;
                    var resultadoOV = _unitOfWork.OportunidadIsVerificadaRepository.Add(oportunidadIsVerificada);
                    _unitOfWork.Commit();
                    oportunidadIsVerificada.Id = resultadoOV.Id;
                    return _mapper.Map<OportunidadIsVerificadaDTO>(oportunidadIsVerificada);
                }
                else
                {
                    return _mapper.Map<OportunidadIsVerificadaDTO>(oportunidadVerificada);
                }

            }
            catch (Exception ex)
            {
                //SE REVIERTE LA ASIGNACION EN MATRICULA
                var matricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerPorIdMatriculaCabecera(dto.IdMatriculaCabecera);
                if (matricula != null)
                {
                    matricula.UsuarioCoordinadorAcademico = "0";
                    matricula.UsuarioModificacion = dto.Usuario;
                    matricula.FechaModificacion = DateTime.Now;
                    _unitOfWork.MatriculaCabeceraRepository.ActualizarTMatriculaCabecera(matricula.CodigoMatricula, "0");
                }

                //Si se creo la oportunidad de operaciones se revierte
                var oportunidadOperacionesExiste = _unitOfWork.OportunidadRepository.ObtenerOportunidadOperacionesPorIdMatricula(matricula.Id);
                if (oportunidadOperacionesExiste != null)
                {
                    if (oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones != null)
                    {
                        var opClasificacion = _unitOfWork.OportunidadClasificacionOperacionesRepository.ObtenerPorIdOportunidad(oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones.Value);
                        if (opClasificacion != null)
                        {
                            _unitOfWork.OportunidadRepository.EliminarOportunidadFisicaOperacionesV3V4(opClasificacion.IdOportunidad);
                        }
                    }
                }
                throw ex;
            }
        }

    }
}
