using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text;
using System.Transactions;
using System.Net;
using System.Web.Helpers;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using RestSharp;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using Google.Api.Ads.Common.Util;
using CsvHelper.Configuration;
using System.Globalization;
using System.Web.WebPages.Html;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Text.RegularExpressions;
using Google.Rpc;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PostulanteService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/10/2022
    /// <summary>
    /// Gestión general de Informacion de Postulante
    /// </summary>
    public class PostulanteService : IPostulanteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private IPersonaService _personaService;
        private IPlantillaService _plantillaService;
        private IOportunidadService _oportunidadService;
        private IMatriculaCabeceraService _matriculaCabeceraService;
        private ICronogramaService _cronogramaService;

        public PostulanteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _personaService = new PersonaService(unitOfWork);
            _plantillaService = new PlantillaService(unitOfWork);
            _oportunidadService = new OportunidadService(unitOfWork);
            _matriculaCabeceraService = new MatriculaCabeceraService(unitOfWork);
            _cronogramaService = new CronogramaService(unitOfWork);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulante, Postulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPostulante, PostulanteDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteDTO, Postulante>(MemberList.None).ReverseMap();
                cfg.CreateMap<Postulante, PostulanteFormulariDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPostulante, PostulanteFormulariDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteExperiencia, PostulanteExperienciaDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R.M.F..
        /// Fecha: 10/06/204
        /// Version: 1.0
        /// <summary>
        /// Obtiene registro de postulantes por el nombre
        /// </summary>
        /// <param name="valor"> Nombre del postulante </param>
        /// <returns> PostulanteDTO </returns>
        public IEnumerable<ComboDTO> ObtenerPostulanteFiltroAutocomplete(string valor)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerPostulanteFiltroAutocomplete(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias Flores
        /// Fecha: 21/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registro de postulantes inscritos
        /// </summary>
        /// <returns> Lista de PostulanteDTO </returns>
        public ResultadoDatosPostulanteDTO ObtenerPostulantesInscritos(PaginadorDTO paginador)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerPostulantesInscritos(paginador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //julius
        public IEnumerable<PostulanteInformacionProcesoDTO> ObtenerPostulantesInscritosConProcesos()
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerPostulantesInscritosConProcesos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener postulantes inscritos con procesos: {ex.Message}");
            }
        }

        public IEnumerable<PostulanteProcesoFormatDTO> ObtenerPostulantesInscritosConProcesosExamenes(PostulanteProcesoDTO DataPostulante)
        {
            try
            {
                var resultado = _unitOfWork.PostulanteRepository.ObtenerPostulantesInscritosConProcesosExamenes(DataPostulante);
                var resultadoFormat = resultado
                    .GroupBy(u => new
                    {
                        IdEvaluacion = u.IdEvaluacion,
                        NombreEvaluacion = u.NombreEvaluacion,
                    }).Select(g => new PostulanteProcesoFormatDTO
                    {
                        IdEvaluacion = g.Key.IdEvaluacion,
                        NombreEvaluacion = g.Key.NombreEvaluacion,
                        ListaExamenes = g.Select(x => new PostulanteProcesoExamenesDTO
                        {
                            IdExamen = x.IdExamen,
                            NombreExamen = x.NombreExamen,
                            EstadoExamen = x.EstadoExamen,
                        }).ToList()
                    }).ToList();
                return resultadoFormat;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener evaluaciones de postulante: {ex.Message}");
            }
        }

        /// Autor: Eliot Arias Flores
        /// Fecha: 28/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene postulantes inscritos con filtro y paginacion
        /// </summary>
        /// <returns> Lista de PostulanteDTO </returns>
        public ResultadoDatosPostulanteDTO ObtenerFiltroDatosPostulanteManual(
            DatosPostulanteDTO datosPostulanteDTO,
            FiltroKendoGridDTO filtroKendoGridDTO,
            IEnumerable<OperadorComparacionDTO> operadoresComparacionDTO)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerFiltroDatosPostulanteManual(datosPostulanteDTO, filtroKendoGridDTO, operadoresComparacionDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 4/11/2024
        /// Version: 1.0
        /// <summary>
        /// Valida si existe ya un postulante por su e-mail
        /// </summary>
        /// <returns> True o False </returns>
        public Boolean ObtenerPostulantePorEmail(string email)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerPostulantePorEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// Autor: Eliot Arias F.
        /// Fecha: 04/11/2024
        /// Version: 1.0
        /// <summary>
        /// Guardar un nuevo registro depostulante
        /// </summary>
        /// <param name="InformacionPostulante"> InsertarPostulanteDTO, dto con los datos del postulante y el nombre del usuario</param>
        /// <returns> true si se registro con exito, si no, retorna false</returns>
        public ResultadoInsertarPostulante InsertarPostulante(InsertarPostulanteDTO informacionPostulante)
        {
            try
            {
                TPostulante postulanteRegistroNuevo = new TPostulante();

                if (_unitOfWork.PostulanteRepository.ObtenerPostulantePorEmail(informacionPostulante.DatosPostulanteFormulario.Email))
                {
                    return new ResultadoInsertarPostulante { Mensaje = "El correo que ingreso ya esta en uso", Valor = false };
                }

                // Obtención del usuario que realiza el registro
                var personal = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(informacionPostulante.Usuario);

                var IdExamenAsignadoEvaluadorCriterioDesaprobatorio = 0;
                TExamenAsignadoEvaluador examenAsignadoEvaluadorCriterioDesaprobatorio = new TExamenAsignadoEvaluador();

                using (var scope = new TransactionScope())
                {
                    postulanteRegistroNuevo.Nombre = informacionPostulante.DatosPostulanteFormulario.Nombre;
                    postulanteRegistroNuevo.ApellidoPaterno = informacionPostulante.DatosPostulanteFormulario.ApellidoPaterno;
                    postulanteRegistroNuevo.ApellidoMaterno = informacionPostulante.DatosPostulanteFormulario.ApellidoMaterno;
                    postulanteRegistroNuevo.IdTipoDocumento = informacionPostulante.DatosPostulanteFormulario.IdTipoDocumento;
                    postulanteRegistroNuevo.NroDocumento = informacionPostulante.DatosPostulanteFormulario.NroDocumento;
                    postulanteRegistroNuevo.Email = informacionPostulante.DatosPostulanteFormulario.Email;
                    postulanteRegistroNuevo.Celular = informacionPostulante.DatosPostulanteFormulario.Celular;
                    postulanteRegistroNuevo.IdPais = informacionPostulante.DatosPostulanteFormulario.IdPais;
                    postulanteRegistroNuevo.IdCiudad = informacionPostulante.DatosPostulanteFormulario.IdCiudad;
                    postulanteRegistroNuevo.IdPersonalOperadorProceso = informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso;
                    postulanteRegistroNuevo.IdPaginaReclutadoraPersonal = informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal;
                    postulanteRegistroNuevo.IdConvocatoriaPersonal = informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal;
                    postulanteRegistroNuevo.IdPostulanteNivelPotencial = informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial;
                    postulanteRegistroNuevo.UsuarioCreacion = informacionPostulante.Usuario;
                    postulanteRegistroNuevo.UsuarioModificacion = informacionPostulante.Usuario;
                    postulanteRegistroNuevo.Estado = true;
                    postulanteRegistroNuevo.FechaCreacion = DateTime.Now;
                    postulanteRegistroNuevo.FechaModificacion = DateTime.Now;
                    _unitOfWork.PostulanteRepository.Insert(postulanteRegistroNuevo);
                    _unitOfWork.Commit();

                    //Guardado en el T_PostulanteLOG
                    var listaPostulanteLog = GenerarListaPostulanteLogV2(postulanteRegistroNuevo, informacionPostulante, informacionPostulante.Usuario, true);
                    _unitOfWork.PostulanteLogRepository.Add(listaPostulanteLog);
                    _unitOfWork.Commit();

                    int? idCreacionCorrecta = _personaService.InsertarPersona(postulanteRegistroNuevo.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Postulante, informacionPostulante.Usuario);
                    if (idCreacionCorrecta == null)
                    {
                        return new ResultadoInsertarPostulante { Mensaje = "No se creo clasificacion persona", Valor = false }; ;
                    }


                    if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.HasValue)
                    {
                        TPostulanteProcesoSeleccion postulanteProcesoSeleccion = new TPostulanteProcesoSeleccion
                        {
                            IdPostulante = postulanteRegistroNuevo.Id,
                            IdProcesoSeleccion = informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value,
                            FechaRegistro = DateTime.Now,
                            Estado = true,
                            UsuarioCreacion = informacionPostulante.Usuario,
                            UsuarioModificacion = informacionPostulante.Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            IdEstadoProcesoSeleccion = informacionPostulante.DatosPostulanteFormulario.IdEstadoProcesoSeleccion,
                            IdPostulanteNivelPotencial = informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial,
                            IdProveedor = informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal,
                            IdPersonalOperadorProceso = informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso,
                            IdConvocatoriaPersonal = informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal
                        };
                        var res = _unitOfWork.PostulanteProcesoSeleccionRepository.Insert(postulanteProcesoSeleccion);
                        _unitOfWork.Commit();
                        if (res)
                        {   
                            //SE comento por peticion de GP
                            //var postulacion = _unitOfWork.ExamenAsignadoRepository.ObtenerPorIdProcesoSeleccionYPorIdPostulante(postulanteProcesoSeleccion.Id, postulanteRegistroNuevo.Id);
                            //if (postulacion == null)
                            //{
                            //    //var procesoSeleccion = _repProcesoSeleccion.GetBy(x => x.Id == Postulante.InformacionPostulante.IdProcesoSeleccion).First();
                            //    var procesoSeleccion = _unitOfWork.ProcesoSeleccionRepository.FirstById(informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value);
                            //    //var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                            //    var examenPorProceso = _unitOfWork.ExamenAsignadoRepository.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                            //    var examenEvaluadorPorProceso = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                            //    foreach (var item in examenPorProceso)
                            //    {
                            //        TExamenAsignado examenAsignado = new TExamenAsignado();
                            //        examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                            //        examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                            //        examenAsignado.IdExamen = item.IdExamen;
                            //        examenAsignado.EstadoExamen = false;
                            //        examenAsignado.Estado = true;
                            //        examenAsignado.UsuarioCreacion = informacionPostulante.Usuario;
                            //        examenAsignado.UsuarioModificacion = informacionPostulante.Usuario;
                            //        examenAsignado.FechaCreacion = DateTime.Now;
                            //        examenAsignado.FechaModificacion = DateTime.Now;
                            //        _unitOfWork.ExamenAsignadoRepository.Insert(examenAsignado);
                            //        _unitOfWork.Commit();
                            //    }
                            //    foreach (var item in examenEvaluadorPorProceso)
                            //    {
                            //        TExamenAsignadoEvaluador examenAsignadoEvaluador = new TExamenAsignadoEvaluador();
                            //        examenAsignadoEvaluador.IdPersonal = personal.Id;
                            //        examenAsignadoEvaluador.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                            //        examenAsignadoEvaluador.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                            //        examenAsignadoEvaluador.IdExamen = item.IdExamen;
                            //        examenAsignadoEvaluador.EstadoExamen = false;
                            //        examenAsignadoEvaluador.Estado = true;
                            //        examenAsignadoEvaluador.UsuarioCreacion = informacionPostulante.Usuario;
                            //        examenAsignadoEvaluador.UsuarioModificacion = informacionPostulante.Usuario;
                            //        examenAsignadoEvaluador.FechaCreacion = DateTime.Now;
                            //        examenAsignadoEvaluador.FechaModificacion = DateTime.Now;

                            //        if (examenAsignadoEvaluador.IdExamen == 111 && informacionPostulante.DatosPostulanteFormulario.ListaRespuestaDesaprobatoria != null)
                            //        {
                            //            examenAsignadoEvaluador.EstadoExamen = true;
                            //        }
                            //        _unitOfWork.ExamenAsignadoEvaluadorRepository.Insert(examenAsignadoEvaluador);
                            //        _unitOfWork.Commit();

                            //        if (examenAsignadoEvaluador.IdExamen == 111)
                            //        {
                            //            IdExamenAsignadoEvaluadorCriterioDesaprobatorio = examenAsignadoEvaluador.Id;
                            //            examenAsignadoEvaluadorCriterioDesaprobatorio = examenAsignadoEvaluador;
                            //        }
                            //    }

                            //    if (IdExamenAsignadoEvaluadorCriterioDesaprobatorio > 0 && informacionPostulante.DatosPostulanteFormulario.ListaRespuestaDesaprobatoria != null)
                            //    {

                            //        foreach (var item in informacionPostulante.DatosPostulanteFormulario.ListaRespuestaDesaprobatoria)
                            //        {
                            //            TExamenRealizadoRespuestaEvaluador evaluadorExamen = new TExamenRealizadoRespuestaEvaluador();
                            //            evaluadorExamen.IdExamenAsignadoEvaluador = IdExamenAsignadoEvaluadorCriterioDesaprobatorio;
                            //            evaluadorExamen.IdPregunta = 761; // Id de Pregunta de Examen de Criterio de Desaprobacion
                            //            evaluadorExamen.IdRespuestaPregunta = item.IdRespuestaDesaprobatoria;
                            //            evaluadorExamen.TextoRespuesta = null;
                            //            evaluadorExamen.Estado = true;
                            //            evaluadorExamen.UsuarioCreacion = informacionPostulante.Usuario;
                            //            evaluadorExamen.UsuarioModificacion = informacionPostulante.Usuario;
                            //            evaluadorExamen.FechaCreacion = DateTime.Now;
                            //            evaluadorExamen.FechaModificacion = DateTime.Now;

                            //            _unitOfWork.ExamenRealizadoRespuestaEvaluadorRepository.Insert(evaluadorExamen);
                            //        }
                            //        _unitOfWork.Commit();
                            //    }
                            //}

                            //Se asignan todas las etapas del proceso al postulante
                            //var EtapasProceso =  _repProcesoSeleccionEtapa.GetBy(x => x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion && x.Estado == true);
                            var EtapasProcesoSeleccion = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value);
                            foreach (var item in EtapasProcesoSeleccion)
                            {
                                TEtapaProcesoSeleccionCalificado etapaCalificacion = new TEtapaProcesoSeleccionCalificado();
                                etapaCalificacion.IdProcesoSeleccionEtapa = item.Id;
                                etapaCalificacion.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa == item.Id)
                                {
                                    etapaCalificacion.EsEtapaAprobada = informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                    etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == null || informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion;
                                    etapaCalificacion.EsEtapaActual = true;
                                }
                                else
                                {
                                    etapaCalificacion.EsEtapaAprobada = false;
                                    etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
                                    etapaCalificacion.EsEtapaActual = false;
                                }
                                etapaCalificacion.EsContactado = false;
                                etapaCalificacion.Estado = true;
                                etapaCalificacion.UsuarioCreacion = informacionPostulante.Usuario;
                                etapaCalificacion.UsuarioModificacion = informacionPostulante.Usuario;
                                etapaCalificacion.FechaCreacion = DateTime.Now;
                                etapaCalificacion.FechaModificacion = DateTime.Now;
                                _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Insert(etapaCalificacion);
                                _unitOfWork.Commit();
                            }
                        }
                        else
                        {
                            return new ResultadoInsertarPostulante { Mensaje = "Error al Registrar Postulante", Valor = true }; ;
                        }
                    }

                    scope.Complete();
                    return new ResultadoInsertarPostulante { Mensaje = "Postulante Registrado con exito", Valor = true };

                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el registro del postulante: {ex.Message}");
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 06/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza un registro de postulante
        /// </summary>
        /// <param name="InformacionPostulante"> InsertarPostulanteDTO, dto con los datos del postulante y el nombre del usuario</param>
        /// <returns> true si se registro con exito, si no, retorna false</returns>
        public ResultadoInsertarPostulante ActualizarPostulante(InsertarPostulanteDTO informacionPostulante)
        {
            try
            {
                if (_unitOfWork.PostulanteRepository.Exist(informacionPostulante.DatosPostulanteFormulario.Id))
                {
                    using (var scope = new TransactionScope())
                    {
                        var personal = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(informacionPostulante.Usuario);
                        var postulanteParaActualizar = _unitOfWork.PostulanteRepository.FirstById(informacionPostulante.DatosPostulanteFormulario.Id);

                        //Compara si el campo email, del postulante fue actualizado, si es que fue, se registra de nuevo en la Tabla T_Persona
                        if (postulanteParaActualizar.Email != informacionPostulante.DatosPostulanteFormulario.Email)
                        {
                            var cantidadPersona = _unitOfWork.PersonaRepository.GetBy(x => x.Email1 == informacionPostulante.DatosPostulanteFormulario.Email).ToList();

                            if (cantidadPersona.Count > 0)
                            {
                                return new ResultadoInsertarPostulante { Mensaje = "ya existe un registro con ese correo", Valor = true };

                            }
                            else
                            {
                                var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository.FirstBy(x => x.IdTablaOriginal == informacionPostulante.DatosPostulanteFormulario.Id && x.IdTipoPersona == 5);
                                var persona = _unitOfWork.PersonaRepository.FirstBy(x => x.Id == clasificacionPersona.IdPersona);
                                persona.Email1 = informacionPostulante.DatosPostulanteFormulario.Email;
                                persona.UsuarioModificacion = informacionPostulante.Usuario;
                                persona.FechaModificacion = DateTime.Now;
                                _unitOfWork.PersonaRepository.Update(persona);
                                _unitOfWork.Commit();
                                _unitOfWork.DetachAll();

                            }
                        }

                        //Guardado en PostulanteLog:
                        var listaPostulanteLog = GenerarListaPostulanteLogV2(postulanteParaActualizar, informacionPostulante, informacionPostulante.Usuario, false);
                        _unitOfWork.PostulanteLogRepository.Add(listaPostulanteLog);
                        //_unitOfWork.Commit();

                        postulanteParaActualizar.Nombre = informacionPostulante.DatosPostulanteFormulario.Nombre;
                        postulanteParaActualizar.ApellidoPaterno = informacionPostulante.DatosPostulanteFormulario.ApellidoPaterno;
                        postulanteParaActualizar.ApellidoMaterno = informacionPostulante.DatosPostulanteFormulario.ApellidoMaterno;
                        postulanteParaActualizar.IdTipoDocumento = informacionPostulante.DatosPostulanteFormulario.IdTipoDocumento;
                        postulanteParaActualizar.NroDocumento = informacionPostulante.DatosPostulanteFormulario.NroDocumento;
                        postulanteParaActualizar.IdPais = informacionPostulante.DatosPostulanteFormulario.IdPais;
                        postulanteParaActualizar.IdCiudad = informacionPostulante.DatosPostulanteFormulario.IdCiudad;
                        postulanteParaActualizar.Celular = informacionPostulante.DatosPostulanteFormulario.Celular;
                        postulanteParaActualizar.Email = informacionPostulante.DatosPostulanteFormulario.Email;
                        postulanteParaActualizar.FechaNacimiento = informacionPostulante.DatosPostulanteFormulario.FechaNacimiento;
                        postulanteParaActualizar.IdSexo = informacionPostulante.DatosPostulanteFormulario.IdSexo;
                        postulanteParaActualizar.TieneHijo = informacionPostulante.DatosPostulanteFormulario.TieneHijo;
                        postulanteParaActualizar.CantidadHijo = informacionPostulante.DatosPostulanteFormulario.CantidadHijo;
                        postulanteParaActualizar.UrlPerfilFacebook = informacionPostulante.DatosPostulanteFormulario.UrlPerfilFacebook;
                        postulanteParaActualizar.IdConvocatoriaPersonal = informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal;
                        postulanteParaActualizar.UrlPerfilLinkedin = informacionPostulante.DatosPostulanteFormulario.UrlPerfilLinkedin;
                        postulanteParaActualizar.UsuarioModificacion = informacionPostulante.Usuario;
                        postulanteParaActualizar.IdPaginaReclutadoraPersonal = informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal;
                        postulanteParaActualizar.IdPostulanteNivelPotencial = informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial;
                        postulanteParaActualizar.IdPersonalOperadorProceso = informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso;
                        postulanteParaActualizar.FechaModificacion = DateTime.Now;
                        postulanteParaActualizar.EsProcesoAnterior = null;
                        _unitOfWork.PostulanteRepository.Update(postulanteParaActualizar);
                        //_unitOfWork.Commit();

                        if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.HasValue)
                        {
                            var procesos = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerProcesoSeleccionInscrito(postulanteParaActualizar.Id);
                            foreach (var item in procesos)
                            {
                                if (item.IdProcesoSeleccion != informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value)
                                {
                                    var pps = _unitOfWork.PostulanteProcesoSeleccionRepository.FirstBy(x => x.IdPostulante == postulanteParaActualizar.Id && x.IdProcesoSeleccion == item.IdProcesoSeleccion);
                                    _unitOfWork.PostulanteProcesoSeleccionRepository.EliminarProcesoSeleccionAsociado(postulanteParaActualizar.Id, item.IdProcesoSeleccion);
                                    _unitOfWork.PostulanteProcesoSeleccionRepository.Delete(pps.Id, informacionPostulante.Usuario);
                                    _unitOfWork.Commit();
                                    _unitOfWork.DetachAll();
                                }
                            }
                            var ppss = _unitOfWork.PostulanteProcesoSeleccionRepository.FirstBy(x => x.IdPostulante == postulanteParaActualizar.Id && x.IdProcesoSeleccion == informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion);
                            if (ppss == null)
                            {
                                List<PostulanteLog> postulanteLogs = new List<PostulanteLog>();
                                var Postlog = AgregarPostulanteLog(informacionPostulante, "CodigoConvocatoria", informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal, id => _unitOfWork.ConvocatoriaPersonalRepository.FirstById(id).Nombre);
                                
                                var Postlog1 = AgregarPostulanteLog(informacionPostulante, "EstadoProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdEstadoProcesoSeleccion, id => _unitOfWork.ProcesoSeleccionRepository.ObtenerEstadoProcesoSeleccionPorId(id).Nombre);
                                
                                var Postlog2 = AgregarPostulanteLog(informacionPostulante, "EtapaProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa, id => _unitOfWork.ProcesoSeleccionEtapaRepository.FirstById(id).Nombre);
                                
                                var Postlog3 = AgregarPostulanteLog(informacionPostulante, "EstadoEtapaProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion, id => _unitOfWork.EstadoEtapaProcesoSeleccionRepository.FirstById(id).Nombre);
                                
                                var Postlog4 = AgregarPostulanteLog(informacionPostulante, "PotencialProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial, id => _unitOfWork.PostulanteNivelPotencialRepository.FirstById(id).Nombre);
                                
                                var Postlog5 = AgregarPostulanteLog(informacionPostulante, "ProveedorProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal, id => _unitOfWork.ProveedorRepository.FirstById(id).RazonSocial);
                                
                                var Postlog6 = AgregarPostulanteLog(informacionPostulante, "Operador", informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso, id => _unitOfWork.PersonalRepository.ObtenerPersonalGestionPersonasPorId(id).Nombre);

                                postulanteLogs.Add(Postlog);
                                postulanteLogs.Add(Postlog1);
                                postulanteLogs.Add(Postlog2);
                                postulanteLogs.Add(Postlog3);
                                postulanteLogs.Add(Postlog4);
                                postulanteLogs.Add(Postlog5);
                                postulanteLogs.Add(Postlog6);

                                _unitOfWork.PostulanteLogRepository.Add(postulanteLogs);
                                //_unitOfWork.Commit();

                                TPostulanteProcesoSeleccion postulanteProcesoSeleccion = new TPostulanteProcesoSeleccion
                                {
                                    IdPostulante = postulanteParaActualizar.Id,
                                    IdProcesoSeleccion = informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value,
                                    FechaRegistro = DateTime.Now,
                                    Estado = true,
                                    UsuarioCreacion = informacionPostulante.Usuario,
                                    UsuarioModificacion = informacionPostulante.Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    IdEstadoProcesoSeleccion = informacionPostulante.DatosPostulanteFormulario.IdEstadoProcesoSeleccion,
                                    IdPostulanteNivelPotencial = informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial,
                                    IdProveedor = informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal,
                                    IdPersonalOperadorProceso = informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso,
                                    IdConvocatoriaPersonal = informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal
                                };
                                var res = _unitOfWork.PostulanteProcesoSeleccionRepository.Insert(postulanteProcesoSeleccion);
                                _unitOfWork.Commit();
                                _unitOfWork.DetachAll();
                                if (res)
                                {
                                    var postulacion = _unitOfWork.ExamenAsignadoRepository.FirstBy(x => x.IdPostulante == postulanteProcesoSeleccion.IdPostulante && x.IdProcesoSeleccion == postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    if (postulacion == null)
                                    {
                                        var examenPorProceso = _unitOfWork.ExamenAsignadoRepository.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                        var examenEvaluadorPorProceso = _unitOfWork.ExamenAsignadoRepository.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                        foreach (var item in examenPorProceso)
                                        {
                                            TExamenAsignado examenAsignado = new TExamenAsignado();
                                            examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                            examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                                            examenAsignado.IdExamen = item.IdExamen;
                                            examenAsignado.EstadoExamen = false;
                                            examenAsignado.Estado = true;
                                            examenAsignado.UsuarioCreacion = informacionPostulante.Usuario;
                                            examenAsignado.UsuarioModificacion = informacionPostulante.Usuario;
                                            examenAsignado.FechaCreacion = DateTime.Now;
                                            examenAsignado.FechaModificacion = DateTime.Now;
                                            _unitOfWork.ExamenAsignadoRepository.Insert(examenAsignado);
                                            _unitOfWork.Commit();
                                        }
                                        foreach (var item in examenEvaluadorPorProceso)
                                        {
                                            TExamenAsignadoEvaluador examenAsignadoEvaluador = new TExamenAsignadoEvaluador();
                                            examenAsignadoEvaluador.IdPersonal = personal.Id;
                                            examenAsignadoEvaluador.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                            examenAsignadoEvaluador.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                                            examenAsignadoEvaluador.IdExamen = item.IdExamen;
                                            examenAsignadoEvaluador.EstadoExamen = false;
                                            examenAsignadoEvaluador.Estado = true;
                                            examenAsignadoEvaluador.UsuarioCreacion = informacionPostulante.Usuario;
                                            examenAsignadoEvaluador.UsuarioModificacion = informacionPostulante.Usuario;
                                            examenAsignadoEvaluador.FechaCreacion = DateTime.Now;
                                            examenAsignadoEvaluador.FechaModificacion = DateTime.Now;
                                            _unitOfWork.ExamenAsignadoEvaluadorRepository.Insert(examenAsignadoEvaluador);
                                            _unitOfWork.Commit();
                                        }
                                    }
                                }
                                else
                                {
                                    return new ResultadoInsertarPostulante { Mensaje = "No se encontro el proceso seleccion enviado", Valor = false };
                                }
                                var EtapasProceso = _unitOfWork.ProcesoSeleccionEtapaRepository.GetBy(x => x.Estado == true && x.IdProcesoSeleccion == informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion).ToList();
                                var etapaCalificada = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerListaEtapaExamenesPorPostulante(informacionPostulante.DatosPostulanteFormulario.Id, informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value).ToList();
                                foreach (var item in etapaCalificada)
                                {
                                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Delete(item.Id, informacionPostulante.Usuario);
                                }
                                foreach (var item in EtapasProceso)
                                {
                                    TEtapaProcesoSeleccionCalificado etapaCalificacion = new TEtapaProcesoSeleccionCalificado();
                                    etapaCalificacion.IdProcesoSeleccionEtapa = item.Id;
                                    etapaCalificacion.IdPostulante = informacionPostulante.DatosPostulanteFormulario.Id;
                                    if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa == item.Id)
                                    {
                                        etapaCalificacion.EsEtapaAprobada = informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                        etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == null || informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion;
                                        etapaCalificacion.EsEtapaActual = true;
                                    }
                                    else
                                    {
                                        etapaCalificacion.EsEtapaAprobada = false;
                                        etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
                                        etapaCalificacion.EsEtapaActual = false;
                                    }
                                    etapaCalificacion.EsContactado = false;
                                    etapaCalificacion.Estado = true;
                                    etapaCalificacion.UsuarioCreacion = informacionPostulante.Usuario;
                                    etapaCalificacion.UsuarioModificacion = informacionPostulante.Usuario;
                                    etapaCalificacion.FechaCreacion = DateTime.Now;
                                    etapaCalificacion.FechaModificacion = DateTime.Now;
                                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Insert(etapaCalificacion);
                                    //_unitOfWork.Commit();
                                }
                            }
                            else
                            {                                

                                if (ppss.IdConvocatoriaPersonal != informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal)
                                {

                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "CodigoConvocatoria", informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal, id => _unitOfWork.ConvocatoriaPersonalRepository.FirstById(id).Nombre);
                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                    //_unitOfWork.Commit();
                                }

                                if (ppss.IdEstadoProcesoSeleccion != informacionPostulante.DatosPostulanteFormulario.IdEstadoProcesoSeleccion)
                                {
                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "EstadoProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdEstadoProcesoSeleccion, id => _unitOfWork.ProcesoSeleccionRepository.ObtenerEstadoProcesoSeleccionPorId(id).Nombre);
                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                    //_unitOfWork.Commit();
                                }

                                if (ppss.IdPostulanteNivelPotencial != informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial)
                                {
                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "PotencialProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial, id => _unitOfWork.PostulanteNivelPotencialRepository.ObtenerPorId(id).Nombre);
                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                    //_unitOfWork.Commit();
                                }

                                if (ppss.IdProveedor != informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal)
                                {
                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "ProveedorProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal, id => _unitOfWork.ProveedorRepository.ObtenerProveedorPorId(id).RazonSocial);
                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                    //_unitOfWork.Commit();

                                }

                                if (ppss.IdPersonalOperadorProceso != informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso)
                                {
                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "Operador", informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso, id => _unitOfWork.PersonalRepository.ObtenerPersonalGestionPersonasPorId(id).Nombre);
                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                    //_unitOfWork.Commit();
                                }
                                _unitOfWork.Commit();
                                ppss.IdEstadoProcesoSeleccion = informacionPostulante.DatosPostulanteFormulario.IdEstadoProcesoSeleccion;
                                ppss.IdPostulanteNivelPotencial = informacionPostulante.DatosPostulanteFormulario.IdPostulanteNivelPotencial;
                                ppss.IdProveedor = informacionPostulante.DatosPostulanteFormulario.IdPaginaReclutadoraPersonal;
                                ppss.IdPersonalOperadorProceso = informacionPostulante.DatosPostulanteFormulario.IdPersonalOperadorProceso;
                                ppss.IdConvocatoriaPersonal = informacionPostulante.DatosPostulanteFormulario.IdConvocatoriaPersonal;

                                ppss.UsuarioModificacion = informacionPostulante.Usuario;
                                ppss.FechaModificacion = DateTime.Now;
                                _unitOfWork.PostulanteProcesoSeleccionRepository.Update(ppss);
                                _unitOfWork.Commit();
                                _unitOfWork.DetachAll();

                                var EtapasProceso = _unitOfWork.ProcesoSeleccionRepository.GetBy(x => x.Estado == true && x.Id == informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion).Select(x => x.Id).ToList();
                                var etapaCalificada = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.ObtenerListaEtapaExamenesPorPostulante(informacionPostulante.DatosPostulanteFormulario.Id, informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccion.Value).ToList();
                                var listaEtapasCalificadas = etapaCalificada.Select(x => x.IdProcesoSeleccionEtapa).ToList();
                                var flag1 = true;
                                var flag2 = true;
                                if (etapaCalificada.Count == 0 || etapaCalificada == null)
                                {
                                    foreach (var item in EtapasProceso)
                                    {
                                        TEtapaProcesoSeleccionCalificado etapaCalificacion = new TEtapaProcesoSeleccionCalificado();
                                        etapaCalificacion.IdProcesoSeleccionEtapa = item;
                                        etapaCalificacion.IdPostulante = informacionPostulante.DatosPostulanteFormulario.Id;
                                        if (flag1)
                                        {
                                            if (etapaCalificacion.IdProcesoSeleccionEtapa != informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa)
                                            {

                                                var log1 = AgregarPostulanteLog(informacionPostulante, "EtapaProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa, id => _unitOfWork.ProcesoSeleccionEtapaRepository.FirstById(id).Nombre);
                                                _unitOfWork.PostulanteLogRepository.Add(log1);
                                                //_unitOfWork.Commit();
                                                flag1 = false;
                                            }

                                        }
                                        if (flag2)
                                        {
                                            if (etapaCalificacion.IdEstadoEtapaProcesoSeleccion != informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion)
                                            {
                                                var log1 = AgregarPostulanteLog(informacionPostulante, "EstadoEtapaProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion, id => _unitOfWork.EstadoEtapaProcesoSeleccionRepository.FirstById(id).Nombre);
                                                _unitOfWork.PostulanteLogRepository.Add(log1);
                                                //_unitOfWork.Commit();
                                                flag2 = false;
                                            }

                                        }
                                        if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa == item)
                                        {
                                            etapaCalificacion.EsEtapaAprobada = informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                            etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == null || informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion;
                                            etapaCalificacion.EsEtapaActual = true;
                                        }
                                        else
                                        {
                                            etapaCalificacion.EsEtapaAprobada = false;
                                            etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
                                            etapaCalificacion.EsEtapaActual = false;
                                        }
                                        etapaCalificacion.EsContactado = false;
                                        etapaCalificacion.Estado = true;
                                        etapaCalificacion.UsuarioCreacion = informacionPostulante.Usuario;
                                        etapaCalificacion.UsuarioModificacion = informacionPostulante.Usuario;
                                        etapaCalificacion.FechaCreacion = DateTime.Now;
                                        etapaCalificacion.FechaModificacion = DateTime.Now;
                                        _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Insert(etapaCalificacion);
                                        //_unitOfWork.Commit();
                                    }
                                }
                                else
                                {
                                    foreach (var item in etapaCalificada)
                                    {
                                        if (!EtapasProceso.Contains(item.IdProcesoSeleccionEtapa))
                                        {
                                            _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Delete(item.Id, informacionPostulante.Usuario);
                                            _unitOfWork.Commit();
                                            _unitOfWork.DetachAll();
                                        }
                                    }

                                    foreach (var item in EtapasProceso)
                                    {
                                        //si ya existe el registro en EtapaCalificada
                                        if (listaEtapasCalificadas.Contains(item))
                                        {
                                            TEtapaProcesoSeleccionCalificado etapaCalificacion = _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.FirstBy(x => x.IdPostulante == informacionPostulante.DatosPostulanteFormulario.Id && x.IdProcesoSeleccionEtapa == item);

                                            if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa == item)
                                            {

                                                if (etapaCalificacion.IdEstadoEtapaProcesoSeleccion != informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion)
                                                {
                                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "EstadoEtapaProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion, id => _unitOfWork.EstadoEtapaProcesoSeleccionRepository.FirstById(id).Nombre);
                                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                                    //_unitOfWork.Commit();
                                                }
                                                etapaCalificacion.EsEtapaAprobada = informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                                etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == null || informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion; ;
                                                if (etapaCalificacion.EsEtapaActual != true)
                                                {
                                                    var Postlog = AgregarPostulanteLog(informacionPostulante, "EtapaProcesoSeleccion", informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa, id => _unitOfWork.ProcesoSeleccionEtapaRepository.FirstById(id).Nombre);
                                                    _unitOfWork.PostulanteLogRepository.Add(Postlog);
                                                    //_unitOfWork.Commit();
                                                }
                                                etapaCalificacion.EsEtapaActual = true;
                                            }
                                            else
                                            {
                                                etapaCalificacion.EsEtapaActual = false;
                                            }
                                            etapaCalificacion.EsContactado = false;
                                            etapaCalificacion.Estado = true;
                                            etapaCalificacion.UsuarioModificacion = informacionPostulante.Usuario;
                                            etapaCalificacion.FechaModificacion = DateTime.Now;
                                            _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Update(etapaCalificacion);
                                            //_unitOfWork.Commit();
                                        }
                                        else
                                        {//Si no existe el registro de esa etapa se crea uno nuevo

                                            TEtapaProcesoSeleccionCalificado etapaCalificacion = new TEtapaProcesoSeleccionCalificado();
                                            etapaCalificacion.IdProcesoSeleccionEtapa = item;
                                            etapaCalificacion.IdPostulante = informacionPostulante.DatosPostulanteFormulario.Id;
                                            if (informacionPostulante.DatosPostulanteFormulario.IdProcesoSeleccionEtapa == item)
                                            {
                                                etapaCalificacion.EsEtapaAprobada = informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                                etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == null || informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : informacionPostulante.DatosPostulanteFormulario.IdEstadoEtapaProcesoSeleccion;
                                                etapaCalificacion.EsEtapaActual = true;
                                            }
                                            else
                                            {
                                                etapaCalificacion.EsEtapaAprobada = false;
                                                etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
                                                etapaCalificacion.EsEtapaActual = false;
                                            }
                                            etapaCalificacion.EsContactado = false;
                                            etapaCalificacion.Estado = true;
                                            etapaCalificacion.UsuarioCreacion = informacionPostulante.Usuario;
                                            etapaCalificacion.UsuarioModificacion = informacionPostulante.Usuario;
                                            etapaCalificacion.FechaCreacion = DateTime.Now;
                                            etapaCalificacion.FechaModificacion = DateTime.Now;
                                            _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Insert(etapaCalificacion);
                                            //_unitOfWork.Commit();
                                        }
                                    }
                                }


                            }
                        }
                        _unitOfWork.Commit();
                        scope.Complete();
                        return new ResultadoInsertarPostulante { Mensaje = "Postulante actualizado con exito", Valor = true };
                    }
                }
                else
                {
                    return new ResultadoInsertarPostulante { Mensaje = "El Postulante no pudo ser Actualizado revise los datos enviados", Valor = false };
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

                
        

        /// Autor: Eliot Arias F.
        /// Fecha: 06/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza un registro de postulante
        /// </summary>
        /// <param name="InformacionPostulante"> InsertarPostulanteDTO, dto con los datos del postulante y el nombre del usuario</param>
        /// <returns> true si se registro con exito, si no, retorna false</returns>
        private List<PostulanteLog> GenerarListaPostulanteLogV2(TPostulante postulanteOriginal, InsertarPostulanteDTO informacionPostulante, string integraUser, Boolean registroNuevo)
        {
            var postulanteLogEntries = new List<PostulanteLog>();
            var propiedades = postulanteOriginal.GetType().GetProperties();
            var propiedadesExcluir = new HashSet<string> { "Id", "FechaCreacion", "FechaModificacion", "UsuarioCreacion", "UsuarioModificacion", "RowVersion", "TPostulanteComparacions", "TExamenAsignadoEvaluadors", "TEtapaProcesoSeleccionCalificados", "IdMigracion", "Estado", "IdConvocatoriaPersonal", "IdPersonalOperadorProceso", "IdPaginaReclutadoraPersonal", "IdPostulanteNivelPotencial" };
           
            foreach (var propiedad in propiedades)
            {
                string nombrePropiedad = propiedad.Name;
                if (propiedadesExcluir.Contains(nombrePropiedad)) continue;

                var valorPropiedadOriginal = propiedad.GetValue(postulanteOriginal)?.ToString();
                var valorPropiedadNueva = informacionPostulante.DatosPostulanteFormulario.GetType().GetProperty(nombrePropiedad)?.GetValue(informacionPostulante.DatosPostulanteFormulario)?.ToString();

                if (registroNuevo)
                {
                    var valorPropiedad = propiedad.GetValue(postulanteOriginal)?.ToString();

                    if (!string.IsNullOrEmpty(valorPropiedad))
                    {
                        postulanteLogEntries.Add(new PostulanteLog
                        {
                            IdPostulante = postulanteOriginal.Id,
                            Clave = nombrePropiedad,
                            Valor = valorPropiedad,
                            Estado = true,
                            UsuarioCreacion = integraUser,
                            UsuarioModificacion = integraUser,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                    }
                }
                else
                {
                    if (valorPropiedadOriginal != valorPropiedadNueva)
                    {
                        postulanteLogEntries.Add(new PostulanteLog
                        {
                            IdPostulante = postulanteOriginal.Id,
                            Clave = nombrePropiedad,
                            Valor = valorPropiedadNueva, // Almacena el nuevo valor o una cadena vacía si es nulo
                            Estado = true,
                            UsuarioCreacion = integraUser,
                            UsuarioModificacion = integraUser,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                    }

                }

            }

            return postulanteLogEntries;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 06/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza un registro de postulante
        /// </summary>
        /// <param name="clave">
        /// <param name="idValor">
        /// <param name="informacionPostulante">
        /// <param name="obtenerNombre">Funcion lamda Obtener Nombre</param>
        /// <returns>PostulanteLog</returns>
        private PostulanteLog AgregarPostulanteLog(InsertarPostulanteDTO informacionPostulante, string clave, int? idValor, Func<int, string> obtenerNombre)
        {
            var postulanteLog = new PostulanteLog
            {
                IdPostulante = informacionPostulante.DatosPostulanteFormulario.Id,
                Clave = clave,
                Valor = idValor.HasValue ? obtenerNombre(idValor.Value) : "",
                Estado = true,
                UsuarioCreacion = informacionPostulante.Usuario,
                UsuarioModificacion = informacionPostulante.Usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            return postulanteLog;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 06/11/2024
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de postulante
        /// </summary>
        /// <param name="PostulanteEliminar"> DTO con el id postulante y el nombre de usuario</param>
        /// <returns> true si se elimino con exito, si no, retorna false</returns>
        public Object EliminarPostulante(EliminarDTO PostulanteEliminar)
        {
            try
            {
                var postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(PostulanteEliminar.Id);
                if (postulante == null)
                {
                    return (new { Mensaje = "El postulante ya fue eliminado", Valor = false });
                }
                else
                {

                    var eliminado = _unitOfWork.PostulanteRepository.Delete(PostulanteEliminar.Id, PostulanteEliminar.NombreUsuario);
                    _unitOfWork.Commit();
                    if (eliminado)
                    {
                        return (new { Mensaje = "Postulante eliminado con exito", Valor = true });
                    }
                    else
                    {
                        return (new { Mensaje = "Ocrurrio un error,El postulante no fue eliminado", Valor = false });
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de postulante si existe a travez de su email
        /// </summary>
        /// <param name="IdPostulante"> email, para validar la existencia</param>
        /// <param name="Clave">clave, para traer la lista de cambios</param>
        /// <returns>Lista de Postulante log, con los cambios a un campo</returns>
        public List<PostulanteLogHistorialDTO> ObtenerHistorialPostulante(int IdPostulante, string Clave)
        {
            try
            {
                return _unitOfWork.PostulanteLogRepository.ObtenerHistorialPostulante(IdPostulante, Clave);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de experiencia del postulante si existe a travez de su email
        /// </summary>
        /// <param name="IdPostulante"> email, para validar la existencia</param>
        /// <returns>Lista de Postulante log, con los cambios a un campo</returns>
        public IEnumerable<PostulanteExperienciaDTO> ObtenerPostulanteExperiencia(int IdPostulante)
        {
            try
            {
                //IEnumerable<PostulanteExperienciaDTO> listaResult = new IEnumerable<TPostulanteExperiencium>();

                return _unitOfWork.PostulanteExperienciaRepository.ObtenerPostulanteExperiencia(IdPostulante);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Registra Experiencia del Postulante
        /// </summary>
        /// <param name="postulanteExperienciaFormulario"> DTO con los datos del formulario y el nombre del usuario</param>
        /// <returns>Object con el tipo de respuesta</returns>
        public Object InsertarPostulanteExperiencia(PostulanteExperienciaFormularioDTO postulanteExperienciaFormulario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    //PostulanteExperienciaRepositorio _repPostulanteExperiencia = new PostulanteExperienciaRepositorio(_integraDBContext);
                    var cantidadRegistros = _unitOfWork.PostulanteExperienciaRepository.ObtenerPostulanteExperiencia(postulanteExperienciaFormulario.ExperienciaPostulante.IdPostulante).ToList();
                    if (cantidadRegistros.Count < 5)
                    {
                        TPostulanteExperiencium experienciaPostulanteGuardar = new TPostulanteExperiencium();
                        experienciaPostulanteGuardar.IdPostulante = postulanteExperienciaFormulario.ExperienciaPostulante.IdPostulante;
                        experienciaPostulanteGuardar.IdEmpresa = postulanteExperienciaFormulario.ExperienciaPostulante.IdEmpresa;
                        experienciaPostulanteGuardar.OtraEmpresa = postulanteExperienciaFormulario.ExperienciaPostulante.OtraEmpresa;
                        experienciaPostulanteGuardar.IdCargo = postulanteExperienciaFormulario.ExperienciaPostulante.IdCargo;
                        experienciaPostulanteGuardar.IdAreaTrabajo = postulanteExperienciaFormulario.ExperienciaPostulante.IdAreaTrabajo;
                        experienciaPostulanteGuardar.IdIndustria = postulanteExperienciaFormulario.ExperienciaPostulante.IdIndustria;
                        experienciaPostulanteGuardar.FechaInicio = postulanteExperienciaFormulario.ExperienciaPostulante.FechaInicio;
                        experienciaPostulanteGuardar.FechaFin = postulanteExperienciaFormulario.ExperienciaPostulante.FechaFin;
                        experienciaPostulanteGuardar.NombreJefe = postulanteExperienciaFormulario.ExperienciaPostulante.NombreJefe;
                        experienciaPostulanteGuardar.NumeroJefe = postulanteExperienciaFormulario.ExperienciaPostulante.NumeroJefe;
                        experienciaPostulanteGuardar.AlaActualidad = postulanteExperienciaFormulario.ExperienciaPostulante.AlaActualidad;
                        experienciaPostulanteGuardar.EsUltimoEmpleo = postulanteExperienciaFormulario.ExperienciaPostulante.EsUltimoEmpleo;
                        experienciaPostulanteGuardar.Salario = postulanteExperienciaFormulario.ExperienciaPostulante.Salario;
                        experienciaPostulanteGuardar.Funcion = postulanteExperienciaFormulario.ExperienciaPostulante.Funcion;
                        experienciaPostulanteGuardar.SalarioComision = postulanteExperienciaFormulario.ExperienciaPostulante.SalarioComision;
                        experienciaPostulanteGuardar.IdMoneda = postulanteExperienciaFormulario.ExperienciaPostulante.IdMoneda;
                        experienciaPostulanteGuardar.Estado = true;
                        experienciaPostulanteGuardar.UsuarioCreacion = postulanteExperienciaFormulario.Usuario;
                        experienciaPostulanteGuardar.UsuarioModificacion = postulanteExperienciaFormulario.Usuario;
                        experienciaPostulanteGuardar.FechaCreacion = DateTime.Now;
                        experienciaPostulanteGuardar.FechaModificacion = DateTime.Now;

                        _unitOfWork.PostulanteExperienciaRepository.Insert(experienciaPostulanteGuardar);
                        _unitOfWork.Commit();
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        return ("Solo puede tener 5 registros de Experiencia");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 07/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza la experiencia del Postulante
        /// </summary>
        /// <param name="postulanteExperienciaFormulario"> DTO con los datos del formulario y el nombre del usuario</param>
        /// <returns>Lista de Postulante log, con los cambios a un campo</returns>
        public Object ActualizarPostulanteExperiencia(PostulanteExperienciaFormularioDTO postulanteExperienciaFormulario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    PostulanteExperiencia experienciaPostulanteGuardar = new PostulanteExperiencia();

                    experienciaPostulanteGuardar = _unitOfWork.PostulanteExperienciaRepository.ObtenerPorId(postulanteExperienciaFormulario.ExperienciaPostulante.Id);

                    var experienciaPostulanteLog = generarExperienciaLogAlCambio(experienciaPostulanteGuardar, postulanteExperienciaFormulario.ExperienciaPostulante, postulanteExperienciaFormulario.Usuario);
                    _unitOfWork.PostulanteExperienciaLogRepository.Add(experienciaPostulanteLog);

                    experienciaPostulanteGuardar.IdPostulante = postulanteExperienciaFormulario.ExperienciaPostulante.IdPostulante;
                    experienciaPostulanteGuardar.IdEmpresa = postulanteExperienciaFormulario.ExperienciaPostulante.IdEmpresa;
                    experienciaPostulanteGuardar.OtraEmpresa = postulanteExperienciaFormulario.ExperienciaPostulante.OtraEmpresa;
                    experienciaPostulanteGuardar.IdCargo = postulanteExperienciaFormulario.ExperienciaPostulante.IdCargo;
                    experienciaPostulanteGuardar.IdAreaTrabajo = postulanteExperienciaFormulario.ExperienciaPostulante.IdAreaTrabajo;
                    experienciaPostulanteGuardar.IdIndustria = postulanteExperienciaFormulario.ExperienciaPostulante.IdIndustria;
                    experienciaPostulanteGuardar.FechaInicio = postulanteExperienciaFormulario.ExperienciaPostulante.FechaInicio;
                    experienciaPostulanteGuardar.FechaFin = postulanteExperienciaFormulario.ExperienciaPostulante.FechaFin;
                    experienciaPostulanteGuardar.NombreJefe = postulanteExperienciaFormulario.ExperienciaPostulante.NombreJefe;
                    experienciaPostulanteGuardar.NumeroJefe = postulanteExperienciaFormulario.ExperienciaPostulante.NumeroJefe;
                    experienciaPostulanteGuardar.AlaActualidad = postulanteExperienciaFormulario.ExperienciaPostulante.AlaActualidad;
                    experienciaPostulanteGuardar.EsUltimoEmpleo = postulanteExperienciaFormulario.ExperienciaPostulante.EsUltimoEmpleo;
                    experienciaPostulanteGuardar.Salario = postulanteExperienciaFormulario.ExperienciaPostulante.Salario;
                    experienciaPostulanteGuardar.Funcion = postulanteExperienciaFormulario.ExperienciaPostulante.Funcion;
                    experienciaPostulanteGuardar.SalarioComision = postulanteExperienciaFormulario.ExperienciaPostulante.SalarioComision;
                    experienciaPostulanteGuardar.IdMoneda = postulanteExperienciaFormulario.ExperienciaPostulante.IdMoneda;
                    experienciaPostulanteGuardar.UsuarioModificacion = postulanteExperienciaFormulario.Usuario;
                    experienciaPostulanteGuardar.FechaModificacion = DateTime.Now;
                    _unitOfWork.PostulanteExperienciaRepository.Update(experienciaPostulanteGuardar);
                    _unitOfWork.Commit();
                    scope.Complete();

                    return (new { mensaje = "Experiencia del Postulante Actualizada con exito", Valor = true });
                }
            }
            catch (Exception ex)
            {
                return new Exception(ex.Message);
            }
        }

        private PostulanteExperienciaLog generarExperienciaLogAlCambio(PostulanteExperiencia postulanteExperienciaOriginal, PostulanteExperienciaDTO postulanteExperienciaNuevo, string usuario)
        {
            var log = new PostulanteExperienciaLog();
            bool cambioParametros = false;

            if (postulanteExperienciaOriginal.IdPostulante != postulanteExperienciaNuevo.IdPostulante)
            {
                log.IdPostulante = postulanteExperienciaNuevo.IdPostulante;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.IdEmpresa != postulanteExperienciaNuevo.IdEmpresa)
            {
                log.IdEmpresa = postulanteExperienciaNuevo.IdEmpresa;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.OtraEmpresa != postulanteExperienciaNuevo.OtraEmpresa)
            {
                log.OtraEmpresa = postulanteExperienciaNuevo.OtraEmpresa;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.IdCargo != postulanteExperienciaNuevo.IdCargo)
            {
                log.IdCargo = postulanteExperienciaNuevo.IdCargo;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.IdAreaTrabajo != postulanteExperienciaNuevo.IdAreaTrabajo)
            {
                log.IdAreaTrabajo = postulanteExperienciaNuevo.IdAreaTrabajo;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.IdIndustria != postulanteExperienciaNuevo.IdIndustria)
            {
                log.IdIndustria = postulanteExperienciaNuevo.IdIndustria;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.FechaInicio != postulanteExperienciaNuevo.FechaInicio)
            {
                log.FechaInicio = postulanteExperienciaNuevo.FechaInicio;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.FechaFin != postulanteExperienciaNuevo.FechaFin)
            {
                log.FechaFin = postulanteExperienciaNuevo.FechaFin;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.NombreJefe != postulanteExperienciaNuevo.NombreJefe)
            {
                log.NombreJefe = postulanteExperienciaNuevo.NombreJefe;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.NumeroJefe != postulanteExperienciaNuevo.NumeroJefe)
            {
                log.NumeroJefe = postulanteExperienciaNuevo.NumeroJefe;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.AlaActualidad != postulanteExperienciaNuevo.AlaActualidad)
            {
                log.AlaActualidad = postulanteExperienciaNuevo.AlaActualidad;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.EsUltimoEmpleo != postulanteExperienciaNuevo.EsUltimoEmpleo)
            {
                log.EsUltimoEmpleo = postulanteExperienciaNuevo.EsUltimoEmpleo;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.Salario != postulanteExperienciaNuevo.Salario)
            {
                log.Salario = postulanteExperienciaNuevo.Salario;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.Funcion != postulanteExperienciaNuevo.Funcion)
            {
                log.Funcion = postulanteExperienciaNuevo.Funcion;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.SalarioComision != postulanteExperienciaNuevo.SalarioComision)
            {
                log.SalarioComision = postulanteExperienciaNuevo.SalarioComision;
                cambioParametros = true;
            }
            if (postulanteExperienciaOriginal.IdMoneda != postulanteExperienciaNuevo.IdMoneda)
            {
                log.IdMoneda = postulanteExperienciaNuevo.IdMoneda;
                cambioParametros = true;
            }

            if (cambioParametros)
            {
                log.IdPostulanteExperiencia = postulanteExperienciaOriginal.Id;
                log.TipoActualizacion = "Editado";
                log.Estado = true;
                log.UsuarioCreacion = usuario;
                log.UsuarioModificacion = usuario;
                log.FechaCreacion = DateTime.Now;
                log.FechaModificacion = DateTime.Now;

                return log;
            }

            return null;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de experiencia de postulante
        /// </summary>
        /// <param name="PostulanteEliminar"> DTO con el id postulante y el nombre de usuario</param>
        /// <returns> true si se elimino con exito, si no, retorna false</returns>
        public Object EliminarPostulanteExperiencia(EliminarDTO postulanteExperiencia)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    PostulanteExperiencia experienciaPostulanteBO = new PostulanteExperiencia();
                    TPostulanteExperienciaLog experienciaPostulanteLog = new TPostulanteExperienciaLog();


                    experienciaPostulanteBO = _unitOfWork.PostulanteExperienciaRepository.ObtenerPorId(postulanteExperiencia.Id);

                    experienciaPostulanteLog.IdPostulante = experienciaPostulanteBO.IdPostulante;
                    experienciaPostulanteLog.IdPostulanteExperiencia = experienciaPostulanteBO.Id;
                    experienciaPostulanteLog.IdEmpresa = experienciaPostulanteBO.IdEmpresa;
                    experienciaPostulanteLog.OtraEmpresa = experienciaPostulanteBO.OtraEmpresa;
                    experienciaPostulanteLog.IdCargo = experienciaPostulanteBO.IdCargo;
                    experienciaPostulanteLog.IdAreaTrabajo = experienciaPostulanteBO.IdAreaTrabajo;
                    experienciaPostulanteLog.IdIndustria = experienciaPostulanteBO.IdIndustria;
                    experienciaPostulanteLog.FechaInicio = experienciaPostulanteBO.FechaInicio;
                    experienciaPostulanteLog.FechaFin = experienciaPostulanteBO.FechaFin;
                    experienciaPostulanteLog.NombreJefe = experienciaPostulanteBO.NombreJefe;
                    experienciaPostulanteLog.NumeroJefe = experienciaPostulanteBO.NumeroJefe;
                    experienciaPostulanteLog.AlaActualidad = experienciaPostulanteBO.AlaActualidad;
                    experienciaPostulanteLog.EsUltimoEmpleo = experienciaPostulanteBO.EsUltimoEmpleo;
                    experienciaPostulanteLog.Salario = experienciaPostulanteBO.Salario;
                    experienciaPostulanteLog.Funcion = experienciaPostulanteBO.Funcion;
                    experienciaPostulanteLog.SalarioComision = experienciaPostulanteBO.SalarioComision;
                    experienciaPostulanteLog.IdMoneda = experienciaPostulanteBO.IdMoneda;
                    experienciaPostulanteLog.TipoActualizacion = "Eliminado";
                    experienciaPostulanteLog.Estado = true;
                    experienciaPostulanteLog.UsuarioCreacion = postulanteExperiencia.NombreUsuario;
                    experienciaPostulanteLog.UsuarioModificacion = postulanteExperiencia.NombreUsuario;
                    experienciaPostulanteLog.FechaCreacion = DateTime.Now;
                    experienciaPostulanteLog.FechaModificacion = DateTime.Now;
                    _unitOfWork.PostulanteExperienciaLogRepository.Insert(experienciaPostulanteLog);
                    _unitOfWork.PostulanteExperienciaRepository.Delete(postulanteExperiencia.Id, postulanteExperiencia.NombreUsuario);
                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return (new { mensaje = "Experiencia del Postulante Eliminada con exito", Valor = true });

            }
            catch (Exception e)
            {
                return (new { mensaje = e.Message, Valor = false });
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de historial de Experiencia del Postulantes
        /// </summary>
        /// <returns> Lista de HistorialPostulanteLog Agrupado </returns>
        public IEnumerable<PostulanteExperienciaLogV2DTO> ObtenerHistorialPostulanteExperiencia(int id)
        {
            try
            {
                return _unitOfWork.PostulanteExperienciaLogRepository.ObtenerHistorialPostulanteExperiencia(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la de Formacion Educativa del Postulantes
        /// </summary>
        /// <returns> Lista de PostulanteFormacionDTO Agrupado </returns>
        public IEnumerable<PostulanteFormacionDTO> ObtenerPostulanteFormacion(int IdPostulante)
        {
            try
            {
                return _unitOfWork.PostulanteFormacionRepository.ObtenerPostulanteFormacion(IdPostulante);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Version: 1.0
        /// <summary>
        /// Insertar nuevo registro de Formacion Educativa del Postulantes
        /// </summary>
        /// <returns> Objeto de Notificacion </returns>
        public Object InsertarPostulanteFormacion(PostulanteFormacionFormularioDTO formacionPostulante)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    TPostulanteFormacion formacionPostulanteNuevo = new TPostulanteFormacion();
                    var cantidadRegistros = _unitOfWork.PostulanteFormacionRepository.ObtenerPostulanteFormacion(formacionPostulante.FormacionPostulante.IdPostulante).ToList();
                    if (cantidadRegistros.Count < 5)
                    {
                        formacionPostulanteNuevo.IdPostulante = formacionPostulante.FormacionPostulante.IdPostulante;
                        formacionPostulanteNuevo.IdCentroEstudio = formacionPostulante.FormacionPostulante.IdCentroEstudio ?? default(int);
                        formacionPostulanteNuevo.IdTipoEstudio = formacionPostulante.FormacionPostulante.IdTipoEstudio ?? default(int);
                        formacionPostulanteNuevo.IdAreaFormacion = formacionPostulante.FormacionPostulante.IdAreaFormacion ?? default(int);
                        formacionPostulanteNuevo.IdEstadoEstudio = formacionPostulante.FormacionPostulante.IdEstadoEstudio;
                        formacionPostulanteNuevo.FechaInicio = formacionPostulante.FormacionPostulante.FechaInicio;
                        formacionPostulanteNuevo.FechaFin = formacionPostulante.FormacionPostulante.FechaFin;
                        formacionPostulanteNuevo.OtraInstitucion = formacionPostulante.FormacionPostulante.OtraInstitucion;
                        formacionPostulanteNuevo.OtraCarrera = formacionPostulante.FormacionPostulante.OtraCarrera;
                        formacionPostulanteNuevo.AlaActualidad = formacionPostulante.FormacionPostulante.AlaActualidad;
                        formacionPostulanteNuevo.TurnoEstudio = formacionPostulante.FormacionPostulante.TurnoEstudio;
                        formacionPostulanteNuevo.IdPais = formacionPostulante.FormacionPostulante.IdPais;
                        formacionPostulanteNuevo.Estado = true;
                        formacionPostulanteNuevo.UsuarioCreacion = formacionPostulante.Usuario;
                        formacionPostulanteNuevo.UsuarioModificacion = formacionPostulante.Usuario;
                        formacionPostulanteNuevo.FechaCreacion = DateTime.Now;
                        formacionPostulanteNuevo.FechaModificacion = DateTime.Now;

                        _unitOfWork.PostulanteFormacionRepository.Insert(formacionPostulanteNuevo);
                        _unitOfWork.Commit();
                        scope.Complete();
                        return (new { mensaje = "Formacion del Postulante Insertada con exito", Valor = true });
                    }
                    else
                    {
                        return (new { mensaje = "Solo puede tener 5 registros de Formacion", Valor = true });
                    }
                }
            }
            catch (Exception ex)
            {
                return new Exception(ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 08/11/2024
        /// Version: 1.0
        /// <summary>
        /// Actualiza un registro de Formacion Educativa del Postulante
        /// </summary>
        /// <returns> Objeto de Notificacion </returns>
        public Object ActualizarPostulanteFormacion(PostulanteFormacionFormularioDTO formacionPostulante)
        {
            try
            {
                using (var scope = new TransactionScope())
                {

                    PostulanteFormacion formacionPostulanteOriginal = new PostulanteFormacion();

                    formacionPostulanteOriginal = _unitOfWork.PostulanteFormacionRepository.ObtenerPorId(formacionPostulante.FormacionPostulante.Id);

                    var experienciaPostulanteLog = generarFormacionLogAlCambio(formacionPostulanteOriginal, formacionPostulante.FormacionPostulante, formacionPostulante.Usuario);
                    _unitOfWork.PostulanteFormacionLogRepository.Add(experienciaPostulanteLog);


                    formacionPostulanteOriginal.IdCentroEstudio = formacionPostulante.FormacionPostulante.IdCentroEstudio ?? default(int);
                    formacionPostulanteOriginal.IdTipoEstudio = formacionPostulante.FormacionPostulante.IdTipoEstudio ?? default(int);
                    formacionPostulanteOriginal.IdAreaFormacion = formacionPostulante.FormacionPostulante.IdAreaFormacion ?? default(int);
                    formacionPostulanteOriginal.IdEstadoEstudio = formacionPostulante.FormacionPostulante.IdEstadoEstudio;
                    formacionPostulanteOriginal.FechaInicio = formacionPostulante.FormacionPostulante.FechaInicio;
                    formacionPostulanteOriginal.FechaFin = formacionPostulante.FormacionPostulante.FechaFin;
                    formacionPostulanteOriginal.OtraInstitucion = formacionPostulante.FormacionPostulante.OtraInstitucion;
                    formacionPostulanteOriginal.OtraCarrera = formacionPostulante.FormacionPostulante.OtraCarrera;
                    formacionPostulanteOriginal.AlaActualidad = formacionPostulante.FormacionPostulante.AlaActualidad;
                    formacionPostulanteOriginal.TurnoEstudio = formacionPostulante.FormacionPostulante.TurnoEstudio;
                    formacionPostulanteOriginal.IdPais = formacionPostulante.FormacionPostulante.IdPais;
                    formacionPostulanteOriginal.UsuarioModificacion = formacionPostulante.Usuario;
                    formacionPostulanteOriginal.FechaModificacion = DateTime.Now;


                    _unitOfWork.PostulanteFormacionRepository.Update(formacionPostulanteOriginal);
                    _unitOfWork.Commit();

                    scope.Complete();
                    return (new { mensaje = "Formacion del Postulante Actualizada con exito", Valor = true });
                }
            }
            catch (Exception ex)
            {
                return new Exception(ex.Message);
            }
        }

        private PostulanteFormacionLog generarFormacionLogAlCambio(PostulanteFormacion formacionOriginal, PostulanteFormacionDTO formacionNuevo, string usuario)
        {
            var log = new PostulanteFormacionLog();
            bool cambioParametros = false;

            if (formacionOriginal.IdPostulante != formacionNuevo.IdPostulante)
            {
                log.IdPostulante = formacionNuevo.IdPostulante;
                cambioParametros = true;
            }
            if (formacionOriginal.IdCentroEstudio != formacionNuevo.IdCentroEstudio)
            {
                log.IdCentroEstudio = formacionNuevo.IdCentroEstudio;
                cambioParametros = true;
            }
            if (formacionOriginal.IdTipoEstudio != formacionNuevo.IdTipoEstudio)
            {
                log.IdTipoEstudio = formacionNuevo.IdTipoEstudio;
                cambioParametros = true;
            }
            if (formacionOriginal.IdAreaFormacion != formacionNuevo.IdAreaFormacion)
            {
                log.IdAreaFormacion = formacionNuevo.IdAreaFormacion;
                cambioParametros = true;
            }
            if (formacionOriginal.IdEstadoEstudio != formacionNuevo.IdEstadoEstudio)
            {
                log.IdEstadoEstudio = formacionNuevo.IdEstadoEstudio;
                cambioParametros = true;
            }
            if (formacionOriginal.FechaInicio != formacionNuevo.FechaInicio)
            {
                log.FechaInicio = formacionNuevo.FechaInicio;
                cambioParametros = true;
            }
            if (formacionOriginal.FechaFin != formacionNuevo.FechaFin)
            {
                log.FechaFin = formacionNuevo.FechaFin;
                cambioParametros = true;
            }
            if (formacionOriginal.OtraInstitucion != formacionNuevo.OtraInstitucion)
            {
                log.OtraInstitucion = formacionNuevo.OtraInstitucion;
                cambioParametros = true;
            }
            if (formacionOriginal.OtraCarrera != formacionNuevo.OtraCarrera)
            {
                log.OtraCarrera = formacionNuevo.OtraCarrera;
                cambioParametros = true;
            }
            if (formacionOriginal.AlaActualidad != formacionNuevo.AlaActualidad)
            {
                log.AlaActualidad = formacionNuevo.AlaActualidad;
                cambioParametros = true;
            }
            if (formacionOriginal.TurnoEstudio != formacionNuevo.TurnoEstudio)
            {
                log.TurnoEstudio = formacionNuevo.TurnoEstudio;
                cambioParametros = true;
            }
            if (formacionOriginal.IdPais != formacionNuevo.IdPais)
            {
                log.IdPais = formacionNuevo.IdPais;
                cambioParametros = true;
            }

            if (cambioParametros)
            {
                log.IdPostulanteFormacion = formacionOriginal.Id;
                log.TipoActualizacion = "Editado";
                log.Estado = true;
                log.UsuarioCreacion = usuario;
                log.UsuarioModificacion = usuario;
                log.FechaCreacion = DateTime.Now;
                log.FechaModificacion = DateTime.Now;

                return log;
            }

            return null;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Version: 1.0
        /// <summary>
        /// Elimina un registro de formacion de postulante
        /// </summary>
        /// <param name="PostulanteEliminar"> DTO con el id postulante y el nombre de usuario</param>
        /// <returns> true si se elimino con exito, si no, retorna false</returns>
        public Object EliminarPostulanteFormacion(EliminarDTO postulanteFormacion)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    PostulanteFormacion formacionPostulante = new PostulanteFormacion();
                    TPostulanteFormacionLog formacionPostulanteLog = new TPostulanteFormacionLog();


                    formacionPostulante = _unitOfWork.PostulanteFormacionRepository.ObtenerPorId(postulanteFormacion.Id);

                    formacionPostulanteLog.IdPostulante = formacionPostulante.IdPostulante;
                    formacionPostulanteLog.IdPostulanteFormacion = formacionPostulante.Id;
                    formacionPostulanteLog.IdCentroEstudio = formacionPostulante.IdCentroEstudio;
                    formacionPostulanteLog.IdTipoEstudio = formacionPostulante.IdTipoEstudio;
                    formacionPostulanteLog.IdAreaFormacion = formacionPostulante.IdAreaFormacion;
                    formacionPostulanteLog.IdEstadoEstudio = formacionPostulante.IdEstadoEstudio;
                    formacionPostulanteLog.FechaInicio = formacionPostulante.FechaInicio;
                    formacionPostulanteLog.FechaFin = formacionPostulante.FechaFin;
                    formacionPostulanteLog.OtraInstitucion = formacionPostulante.OtraInstitucion;
                    formacionPostulanteLog.OtraCarrera = formacionPostulante.OtraCarrera;
                    formacionPostulanteLog.AlaActualidad = formacionPostulante.AlaActualidad;
                    formacionPostulanteLog.TurnoEstudio = formacionPostulante.TurnoEstudio;
                    formacionPostulanteLog.IdPais = formacionPostulante.IdPais;
                    formacionPostulanteLog.TipoActualizacion = "Eliminado";
                    formacionPostulanteLog.Estado = true;
                    formacionPostulanteLog.UsuarioCreacion = postulanteFormacion.NombreUsuario;
                    formacionPostulanteLog.UsuarioModificacion = postulanteFormacion.NombreUsuario;
                    formacionPostulanteLog.FechaCreacion = DateTime.Now;
                    formacionPostulanteLog.FechaModificacion = DateTime.Now;

                    _unitOfWork.PostulanteFormacionLogRepository.Insert(formacionPostulanteLog);
                    _unitOfWork.PostulanteFormacionRepository.Delete(postulanteFormacion.Id, postulanteFormacion.NombreUsuario);
                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return (new { mensaje = "Formacion del Postulante Eliminada con exito", Valor = true });

            }
            catch (Exception e)
            {
                return (new { mensaje = e.Message, Valor = false });
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Version: 1.0
        /// <param name="idPostulante">idPostulante</param>
        /// <summary>
        /// Obtiene la lista de historial de Formacion del Postulante        /// 
        /// </summary>
        /// <returns> Lista de HistorialPostulanteLog Agrupado </returns>
        public IEnumerable<PostulanteFormacionLogDTO> ObtenerHistorialPostulanteFormacion(int idPostulante)
        {
            try
            {
                return _unitOfWork.PostulanteFormacionLogRepository.ObtenerHistorialPostulanteFormacion(idPostulante);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// Autor: Eliot Arias F.
        /// Fecha: 09/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Valida el correo Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public IEnumerable<ResultadoFinalTextoDTO> ValidarCorreoPostulante(int idPostulante)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ValidarCorreoPostulante(idPostulante);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// Autor: Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia correo a los Postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public Object EnviarPlantillaEmailMasivo(EnvioPlantillaPostulanteDTO Postulantes)
        {
            try
            {
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                //IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                //PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                //PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //MatriculaManualController _repMatriculaManual = new MatriculaManualController();
                //CronogramaController _contCronograma = new CronogramaController();

                using (var scope = new TransactionScope())
                {
                    var personalUsuario = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(Postulantes.Usuario);
                    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(personalUsuario.Id);
                    foreach (var idPostulanteProcesoSeleccion in Postulantes.ListaIdPostulanteProcesoSeleccion)
                    {
                        var ultimoRegistro = _unitOfWork.PostulanteProcesoSeleccionRepository.VerificacionTokenPresente(idPostulanteProcesoSeleccion);
                        var postulanteProcesoSeleccion = (ultimoRegistro.Id == 0) ? _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion) : ultimoRegistro;

                        //Plantilla Invitación mas envio accesos
                        if (Postulantes.IdPlantilla == 1255)
                        {
                            if (ultimoRegistro.Id == 0) // NO TIENE REGISTROS Y CREARA EL PRIMER REGISTRO
                            {
                                if (postulanteProcesoSeleccion.Token == null)
                                {
                                    TokenPostulanteProcesoSeleccion tokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccion();
                                    var token = GenerarClave(8);
                                    var tokenHash = Crypto.HashPassword(token);
                                    var tokenPostulante = _unitOfWork.TokenPostulanteProcesoSeleccionRepository.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);

                                    TTokenPostulanteProcesoSeleccion tokenNueva = new TTokenPostulanteProcesoSeleccion()
                                    {
                                        IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                                        Token = token,
                                        TokenHash = tokenHash,
                                        GuidAccess = Guid.NewGuid(),
                                        Activo = true,
                                        Estado = true,
                                        UsuarioCreacion = Postulantes.Usuario,
                                        UsuarioModificacion = Postulantes.Usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        FechaEnvioAccesos = DateTime.Now
                                    };

                                    _unitOfWork.TokenPostulanteProcesoSeleccionRepository.Insert(tokenNueva);
                                    _unitOfWork.Commit();
                                    _unitOfWork.DetachAll();
                                    postulanteProcesoSeleccion.Token = token;
                                    postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
                                }
                            }
                        }

                        if (Postulantes.IdPlantilla == 1818)
                        {
                            var postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(postulanteProcesoSeleccion.IdPostulante);
                            var DatosPostulanteLogin = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(postulante.Id);
                            //var _reemplazoEtiquetaPlantilla = new 
                            //{
                            //    IdPlantilla = Postulantes.IdPlantilla,
                            //    IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                            //    Personal = personal,
                            //};
                            var reemplazoEtiquetaPlantilla = _plantillaService.ReemplazarEtiquetasProcesoSeleccionReportePostulanteCursoAsesorCapacitacion(Postulantes.IdPlantilla, idPostulanteProcesoSeleccion, personal, Postulantes.Fecha);
                            //_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccionReportePostulanteCursoAsesorCapacitacion();

                            var emailCalculado = reemplazoEtiquetaPlantilla;
                            List<string> correosPersonalizadosCopiaOculta = new List<string>
                            {
                                personal.Email
                            };

                            List<string> correosPersonalizados = new List<string>{
							//proveedor.Email
							    DatosPostulanteLogin.Email.Trim()};
                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = personal.Email,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = emailCalculado.Asunto,
                                Message = emailCalculado.CuerpoHTML,
                                Cc = "",
                                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                            };
                            var mailServie = new TMK_MailService();

                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();

                            var gmailCorreo = new TGmailCorreo
                            {
                                IdEtiqueta = 1,//sent:1 , inbox:2
                                Asunto = emailCalculado.Asunto,
                                Fecha = DateTime.Now,
                                EmailBody = emailCalculado.CuerpoHTML,
                                Seen = false,
                                Remitente = personal.Email,
                                Cc = "",
                                Bcc = "",
                                Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                                IdPersonal = personal.Id,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                IdClasificacionPersona = 5
                            };
                            //var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                            //_repGmailCorreo.Insert(gmailCorreo);
                            _unitOfWork.GmailCorreoRepository.Insert(gmailCorreo);
                            //_unitOfWork.Commit();

                        }
                        if (Postulantes.IdPlantilla == 1821)
                        {
                            //AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                            var postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(postulanteProcesoSeleccion.IdPostulante);
                            var alumnoPostulante = _unitOfWork.PostulanteRepository.ObtenerIdAlumnoDesdeidPostulanteSinMatricula(postulante.Id);
                            if (alumnoPostulante.IdAlumno == 0 || alumnoPostulante.IdAlumno == null)
                            {
                                _unitOfWork.PostulanteRepository.CreacionAlumnoDesdePostulante(postulante.Id, Postulantes.Usuario);
                                alumnoPostulante = _unitOfWork.PostulanteRepository.ObtenerIdAlumnoDesdeidPostulanteSinMatricula(postulante.Id);
                            }


                            //OportunidadRepositorio _repOportunidad = new OportunidadRepositorio();

                            //var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(alumnoPostulante.IdAlumno.GetValueOrDefault());
                            //if (oportunidad == null)
                            //{
                            //    CrearOportunidaddesdePostulante(postulante.Id, Postulantes.Usuario);
                            //}
                            var matricula = _unitOfWork.PostulanteRepository.ObtenerMatriculaconIdAlumno(alumnoPostulante.IdAlumno.GetValueOrDefault());
                            if (matricula == null)
                            {
                                var matriculaCabecera = _matriculaCabeceraService.GenerarMatriculaCabeceraPorPostulante(postulante.Id, Postulantes.Usuario);
                                //var matriculaManual = _matriculaCabeceraService.ActualizarCronogramaPagoPorPostulante(postulante.Id, Postulantes.Usuario);
                                //Me quede aqui
                                //_unitOfWork.CronogramaRepository.Guar
                                //_cronogramaService.GuardarPagoPostulante(postulante.Id, Postulantes.Usuario);

                            }
                            var DatosAlumno = _unitOfWork.AlumnoRepository.FirstById(alumnoPostulante.IdAlumno.GetValueOrDefault());
                            var DatosPostulanteLogin = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(postulante.Id);
                            if (DatosPostulanteLogin.Usuario == null || DatosPostulanteLogin.Contraseña == null)
                            {
                                _unitOfWork.PostulanteRepository.CreacionCuentaPortaldePostulante(DatosPostulanteLogin.IdAlumno, DatosPostulanteLogin.Email, DatosPostulanteLogin.NombrePostulante, DatosPostulanteLogin.ApellidoPostulante, DatosAlumno.IdPais, DatosAlumno.IdCiudad, DatosAlumno.Celular);
                            }
                            postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(postulanteProcesoSeleccion.IdPostulante);
                            DatosPostulanteLogin = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(postulante.Id);
                            //var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                            //{
                            //    IdPlantilla = Postulantes.IdPlantilla,
                            //    IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                            //    Personal = personal,

                            //};
                            var reemplazoEtiquetaPlantilla = _plantillaService.ReemplazarEtiquetasProcesoSeleccionReportePostulanteCursoAsesorCapacitacion(Postulantes.IdPlantilla, idPostulanteProcesoSeleccion, personal, Postulantes.Fecha);
                            //_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccionReportePostulanteCursoAsesorCapacitacion();
                            var emailCalculado = reemplazoEtiquetaPlantilla;
                            //var emailCalculado = reemplazoEtiquetaPlantilla.EmailReemplazado;
                            List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            personal.Email
                        };

                            List<string> correosPersonalizados = new List<string>{
							//proveedor.Email
							    DatosPostulanteLogin.Email.Trim()
                        };
                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = personal.Email,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = emailCalculado.Asunto,
                                Message = emailCalculado.CuerpoHTML,
                                Cc = "",
                                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                            };
                            var mailServie = new TMK_MailService();

                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();

                            var gmailCorreo = new TGmailCorreo
                            {
                                IdEtiqueta = 1,//sent:1 , inbox:2
                                Asunto = emailCalculado.Asunto,
                                Fecha = DateTime.Now,
                                EmailBody = emailCalculado.CuerpoHTML,
                                Seen = false,
                                Remitente = personal.Email,
                                Cc = "",
                                Bcc = "",
                                Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                                IdPersonal = personal.Id,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = "SYSTEM",
                                UsuarioModificacion = "SYSTEM",
                                IdClasificacionPersona = 5
                            };
                            //var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                            _unitOfWork.GmailCorreoRepository.Insert(gmailCorreo);
                            //_unitOfWork.Commit();

                        }
                        else
                        {

                            var postulante = _unitOfWork.PostulanteRepository.ObtenerPorId(postulanteProcesoSeleccion.IdPostulante);
                            if (idPostulanteProcesoSeleccion > 0 && idPostulanteProcesoSeleccion != null)
                            {
                                DateTime? fecha = null;
                                if (Postulantes.Fecha.HasValue)
                                {
                                    fecha = Postulantes.Fecha.Value.AddHours(-5);
                                }
                                //var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                                //{
                                //    IdPlantilla = Postulantes.IdPlantilla,
                                //    IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                                //    Personal = personal,
                                //    FechaGP = fecha
                                //};
                                var reemplazoEtiquetaPlantilla = _plantillaService.ReemplazarEtiquetasProcesoSeleccion(Postulantes.IdPlantilla, idPostulanteProcesoSeleccion, personal, Postulantes.Fecha);
                                //_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccion();
                                var emailCalculado = reemplazoEtiquetaPlantilla;
                                List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            personal.Email
                        };

                                List<string> correosPersonalizados = new List<string>();

                                if (Postulantes.IdPlantilla == 1258)//tiene que llegarle al postulante
                                {
                                    correosPersonalizados.Add(postulante.Email.Trim());
                                }
                                else if (Postulantes.IdPlantilla == 1257) //tiene que llegarle al postulante
                                {
                                    correosPersonalizados.Add(postulante.Email.Trim());
                                }
                                else
                                {
                                    correosPersonalizados.Add(personal.Email.Trim());
                                }


                                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                                {
                                    Sender = personal.Email,
                                    Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                    Subject = emailCalculado.Asunto,

                                    Message = emailCalculado.CuerpoHTML,
                                    Cc = "",
                                    Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                    AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                                };
                                var mailServie = new TMK_MailService();

                                mailServie.SetData(mailDataPersonalizado);
                                mailServie.SendMessageTask();

                                var gmailCorreo = new GmailCorreo
                                {
                                    IdEtiqueta = 1,//sent:1 , inbox:2
                                    Asunto = emailCalculado.Asunto,
                                    Fecha = DateTime.Now,
                                    EmailBody = emailCalculado.CuerpoHTML,
                                    Seen = false,
                                    Remitente = personal.Email,
                                    Cc = "",
                                    Bcc = "",
                                    Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                                    IdPersonal = personal.Id,
                                    Estado = true,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    UsuarioCreacion = "SYSTEM",
                                    UsuarioModificacion = "SYSTEM",
                                    IdClasificacionPersona = 5
                                };
                                //var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                                _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);
                                //_unitOfWork.Commit();
                                //_unitOfWork.DetachAll();

                            }

                        }
                    }
                    _unitOfWork.Commit();
                    scope.Complete();
                    return (new { mensaje = "Email enviado con exito", Valor = true });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public string GenerarClave(int longitud)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            return res.ToString();
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Envia correo a postulante y genera su token
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public Object EnviarMensajeEmailPostulante(PostulanteProcesoSeleccionIdDTO PostulanteProcesoSeleccion)
        {
            try
            {
                //PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);

                var postulanteProcesoSeleccion = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(PostulanteProcesoSeleccion.Id);
                var emailPersonal = _unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(PostulanteProcesoSeleccion.Usuario);

                if (postulanteProcesoSeleccion.Token == null)
                {
                    TTokenPostulanteProcesoSeleccion tokenPostulanteProcesoSeleccion = new TTokenPostulanteProcesoSeleccion();
                    var token = GenerarClave(8);
                    var tokenHash = Crypto.HashPassword(token);
                    var tokenPostulante = _unitOfWork.TokenPostulanteProcesoSeleccionRepository.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(PostulanteProcesoSeleccion.Id);

                    TTokenPostulanteProcesoSeleccion tokenNueva = new TTokenPostulanteProcesoSeleccion()
                    {
                        IdPostulanteProcesoSeleccion = PostulanteProcesoSeleccion.Id,
                        Token = token,
                        TokenHash = tokenHash,
                        GuidAccess = Guid.NewGuid(),
                        Activo = true,
                        Estado = true,
                        UsuarioCreacion = PostulanteProcesoSeleccion.Usuario,
                        UsuarioModificacion = PostulanteProcesoSeleccion.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        FechaEnvioAccesos = DateTime.Now
                    };

                    _unitOfWork.TokenPostulanteProcesoSeleccionRepository.Insert(tokenNueva);
                    _unitOfWork.Commit();
                    postulanteProcesoSeleccion.Token = token;
                    postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
                }
                var mensaje = "<p style=‘text - align: justify;’><strong style=‘text - align: justify; font - family: Calibri, sans - serif; font - size: 14.6667px;’>Estimado(a) {T_Postulante.Nombre}</strong> <span style=‘text - align: justify;’>&nbsp;</span> <br /><br /><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’>Me es grato saludarle y a la vez le hago llegar los datos para acceder al proceso de selecci&oacute;n al que usted se inscribio:</span></p><p style=‘text - align: center;’><strong style=‘font - family: Calibri, sans - serif; font - size: 14.6667px; text - align: center;’>{T_ProcesoSeleccion.Nombre}</strong></p><p style=‘margin - left: 30px; text - align: justify;’><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’><strong>Usuario:</strong> {T_Postulante.Dni} </span> <br /><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’><strong>Contrase&ntilde;a:</strong> {T_Postulante.Clave} </span> <br /><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’><strong>Link: </strong>{Link.UrlProcesoSeleccion} </span></p><p style=‘text - align: justify;’><span style=‘font - family: Calibri, sans - serif; font - size: 11pt;’>Recuerde que los accesos enviados caducan dentro de 72 hrs o al primer inicio de sesi&oacute;n, es importante continuar el proceso sin salir de la p&aacute;gina ya que al salir estos accesos no tendr&aacute;n validez. En caso ya no pueda acceder al proceso de selecci&oacute;n favor de responder el correo solicitandolos.</span></p><p style=‘text - align: justify;’>Saludos cordiales.</p>";
                var asunto = "{T_Postulante.Nombre}, acceda al proceso de selección - BSG INSTITUTE";
                var url = "https://proceso-seleccion.bsginstitute.com/procesoseleccion/acceso?guid=" + postulanteProcesoSeleccion.GuidAccess;

                mensaje = mensaje.Replace("{T_Postulante.Nombre}", postulanteProcesoSeleccion.Postulante);
                mensaje = mensaje.Replace("{T_ProcesoSeleccion.Nombre}", postulanteProcesoSeleccion.ProcesoSeleccion);
                mensaje = mensaje.Replace("{T_Postulante.Dni}", postulanteProcesoSeleccion.Dni);
                mensaje = mensaje.Replace("{T_Postulante.Clave}", postulanteProcesoSeleccion.Token);
                mensaje = mensaje.Replace("{Link.UrlProcesoSeleccion}", url);

                asunto = asunto.Replace("{T_Postulante.Nombre}", postulanteProcesoSeleccion.Postulante);

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = emailPersonal,
                    Recipient = emailPersonal,//se quito la opc que le llegue al postulante
                    Subject = asunto,
                    Message = mensaje,
                    AttachedFiles = null,
                    Bcc = emailPersonal,
                    RemitenteC = PostulanteProcesoSeleccion.Usuario
                };
                var mailServie = new TMK_MailService();

                mailServie.SetData(mailDataPersonalizado);
                mailServie.SendMessageTask();
                return (new { mensaje = "Correo Enviado con exito", valor = true });
            }
            catch (Exception ex)
            {

                throw new Exception("Excepcion generada en EnviarMensajeEmailPostulante" + ex.Message);
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 28/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Se crea la oportunidad y el alumno en ventas
        /// </summary>
        /// <returns>objeto anonimo (Ok en cadena y el objeto de clase Oportunidad</returns>
        public OportunidadBoDTO CrearOportunidaddesdePostulante(int idPostulante, string Usuario)
        {
            try
            {
                //OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                //AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                //OportunidadController _controlOportunidad = new OportunidadController(_integraDBContext);
                var idalumno = _unitOfWork.PostulanteRepository.ObtenerIdAlumnoDesdeidPostulanteSinMatricula(idPostulante);
                var datosAlumno = _unitOfWork.AlumnoRepository.FirstById(idalumno.IdAlumno.GetValueOrDefault());
                var idCentroCosto = 25376;
                var idPersonaleAsignado = 125;
                var idTipoDato = 32;
                var idFaseOportunidad = 13;
                var idOrigen = 1173;
                var ultimoComentario = "Sin Comentario";
                OportunidadBoDTO oportunidad = new OportunidadBoDTO
                {
                    IdCentroCosto = idCentroCosto,
                    IdPersonalAsignado = idPersonaleAsignado,
                    IdTipoDato = idTipoDato,
                    IdFaseOportunidad = idFaseOportunidad,
                    IdOrigen = idOrigen,
                    UltimoComentario = ultimoComentario,
                    FechaRegistroCampania = DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = Usuario,
                    UsuarioModificacion = Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdAlumno = datosAlumno.Id,
                    IdClasificacionPersona = idalumno.IdClasificacionPersona,
                    IdPersonalAreaTrabajo = 8
                };

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;//Programada  6
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;//No programada 2
                _oportunidadService.CrearOportunidad(ref oportunidad, false, TipoPersona.Alumno);
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el proceso de seleccion de un postulante sin nota asociada
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public Object ActualizarProcesoPostulanteSinNota(PostulanteProcesoNuevoDTO Informacion)
        {
            try
            {
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                //PostulanteLogRepositorio _repPostulanteLog = new PostulanteLogRepositorio(_integraDBContext);
                var postulanteHistorial = _unitOfWork.PostulanteRepository.ActualizarProcesoPostulanteSinNota(Informacion);
                //ProcesoSeleccionRepositorio _repProcesoSeleccion = new ProcesoSeleccionRepositorio(_integraDBContext);
                var Postulantelog1 = PostulanteLog(Informacion, "ProcesoSeleccion", Informacion.IdProcesoSeleccionDestino, id => _unitOfWork.ProcesoSeleccionRepository.FirstById(id).Nombre);

                var Postulantelog2 = PostulanteLog(Informacion, "ConvocatoriaPersonal", Informacion.IdProcesoSeleccionDestino, id => _unitOfWork.ProcesoSeleccionRepository.FirstById(id).Codigo);
                var list = new List<PostulanteLog>();
                list.Add(Postulantelog1);
                list.Add(Postulantelog2);
                _unitOfWork.PostulanteLogRepository.Add(list);
                _unitOfWork.Commit();
                _unitOfWork.Commit();
                return (new { mensaje = "Proceso de Seleccion Actualizado Con Exito", valor = true });
            }
            catch (Exception ex)
            {

                throw new Exception("Error en ActualizarProcesoPostulante " + ex.Message);
            }
        }

        public List<ComparacionProcesosSeleccionDTO> CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.CompararProcesosSeleccion(IdPostulante, ProcesoOrigen, ProcesoDestino);
            }
            catch (Exception ex)
            {

                throw new Exception("Error en CompararProceso seleccion:" + ex.Message);
            }
        }

        private PostulanteLog PostulanteLog(PostulanteProcesoNuevoDTO informacion, string clave, int? idValor, Func<int, string> obtenerNombre)
        {
            var postulanteLog = new PostulanteLog
            {
                IdPostulante = informacion.IdPostulante,
                Clave = clave,
                Valor = idValor.HasValue ? obtenerNombre(idValor.Value) : "",
                Estado = true,
                UsuarioCreacion = informacion.Usuario,
                UsuarioModificacion = informacion.Usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            return postulanteLog;
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el proceso de seleccion de un postulante sin nota asociada
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public Object ActualizarProcesoPostulante(PostulanteProcesoNuevoDTO Informacion)
        {
            try
            {
                var postulanteHistorial = _unitOfWork.PostulanteRepository.ActualizarProcesoPostulante(Informacion);

                var Postulantelog1 = PostulanteLog(Informacion, "IdProcesoSeleccion", Informacion.IdProcesoSeleccionDestino, id => _unitOfWork.ProcesoSeleccionRepository.FirstById(id).Nombre);

                var Postulantelog2 = PostulanteLog(Informacion, "IdConvocatoriaPersonal", Informacion.IdProcesoSeleccionDestino, id => _unitOfWork.ProcesoSeleccionRepository.FirstById(id).Codigo);
                var list = new List<PostulanteLog>();
                list.Add(Postulantelog1);
                list.Add(Postulantelog2);
                _unitOfWork.PostulanteLogRepository.Add(list);
                _unitOfWork.Commit();


                return (new { mensaje = "Proceso de Seleccion Actualizado Con Exito", valor = true });
            }
            catch (Exception ex)
            {

                throw new Exception("Error en ActualizarProcesoPostulante " + ex.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Envio de WhatsApp a postulantes
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public Object EnviarMensajeWhatsAppPostulante(EnvioPlantillaPostulanteDTO Postulantes)
        {
            try
            {
                ////PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                ////IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                ////PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                ////PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
                ////TokenPostulanteProcesoSeleccionRepositorio _repTokenPostulanteProcesoSeleccion = new TokenPostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //List<string> Incorrectos = new List<string>();
                //List<string> Incorrectos2 = new List<string>();
                //var personalUsuario = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(Postulantes.Usuario);
                //    var personal = _unitOfWork.PersonalRepository.ObtenerPorId(personalUsuario.Id);
                //foreach (var idPostulanteProcesoSeleccion in Postulantes.ListaIdPostulanteProcesoSeleccion)
                //{
                //    var postulanteProcesoSeleccion = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);

                //    if (Postulantes.IdPlantilla == 1300)
                //    {
                //        if (postulanteProcesoSeleccion.Token == null)
                //        {
                //            TTokenPostulanteProcesoSeleccion tokenPostulanteProcesoSeleccion = new TTokenPostulanteProcesoSeleccion();
                //            var token = GenerarClave(8);
                //            var tokenHash = Crypto.HashPassword(token);
                //            var tokenPostulante = _unitOfWork.TokenPostulanteProcesoSeleccionRepository.ObtenerUltimoTokenPorPostulanteProcesoSeleccion(idPostulanteProcesoSeleccion);

                //            TTokenPostulanteProcesoSeleccion tokenNueva = new TTokenPostulanteProcesoSeleccion()
                //            {
                //                IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                //                Token = token,
                //                TokenHash = tokenHash,
                //                GuidAccess = Guid.NewGuid(),
                //                Activo = true,
                //                Estado = true,
                //                UsuarioCreacion = Postulantes.Usuario,
                //                UsuarioModificacion = Postulantes.Usuario,
                //                FechaCreacion = DateTime.Now,
                //                FechaModificacion = DateTime.Now,
                //                FechaEnvioAccesos = DateTime.Now
                //            };

                //            _unitOfWork.TokenPostulanteProcesoSeleccionRepository.Insert(tokenNueva);
                //            _unitOfWork.Commit();
                //            postulanteProcesoSeleccion.Token = token;
                //            postulanteProcesoSeleccion.GuidAccess = tokenNueva.GuidAccess;
                //        }
                //    }

                //    var postulante = _unitOfWork.PostulanteRepository.FirstById(postulanteProcesoSeleccion.IdPostulante);
                //    if (idPostulanteProcesoSeleccion > 0 && idPostulanteProcesoSeleccion != null)
                //    {
                //        DateTime? fecha = null;
                //        if (Postulantes.Fecha.HasValue)
                //        {
                //            fecha = Postulantes.Fecha.Value.AddHours(-5);
                //        }
                //        //var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                //        //{
                //        //    IdPlantilla = Postulantes.IdPlantilla,
                //        //    IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion,
                //        //    Personal = personal,
                //        //    FechaGP = fecha
                //        //};
                //        var reemplazoEtiquetaPlantilla = _plantillaService.ReemplazarEtiquetasProcesoSeleccionWhatsApp(Postulantes.IdPlantilla, idPostulanteProcesoSeleccion, personal, Postulantes.Fecha);
                //        //_reemplazoEtiquetaPlantilla.ReemplazarEtiquetasProcesoSeleccion();
                //        var whatsAppCalculado = reemplazoEtiquetaPlantilla;
                //        var celular = ObtenerNumeroWhatsApp(postulante.IdPais.Value, postulante.Celular);
                //        ValidarNumerosWhatsAppAsyncDTO arr = new ValidarNumerosWhatsAppAsyncDTO();
                //        List<string> contacts = new List<string>();
                //        contacts.Add(("+" + celular));
                //        arr.contacts = contacts;
                //        try
                //        {
                //            EnvioAutomaticoPlantilla(whatsAppCalculado.Plantilla, whatsAppCalculado.ListaEtiquetas, Postulantes.IdPlantilla, celular, postulante.IdPais.Value, personal.Id, postulante.Id, Postulantes.Usuario);
                //        }
                //        catch (Exception e)
                //        {

                //            Incorrectos.Add(postulante.NroDocumento);
                //        }

                //    }
                //}

                return (new { mensaje = "Mensaje Enviado con Exito", valor = true });
            }
            catch (Exception ex)
            {
                throw new Exception("Error en EnviarMensajeWhatsAppPostulante " + ex.Message);
            }
        }

        private void EnvioAutomaticoPlantilla(string PlantillaReemplazada, List<DatoPlantillaWhatsAppDTO> PlantillaWhatsApp, int IdPlantilla, string Celular, int IdPais, int IdPersonal, int IdPostulante, string Usuario)
        {
            ValidarNumerosWhatsAppAsyncDTO arr = new ValidarNumerosWhatsAppAsyncDTO();
            //TPostulante postulante = new TPostulante();
            List<string> contacts = new List<string>();
            contacts.Add(("+" + Celular));
            arr.contacts = contacts;
            var res = ValidarNumeroEnvioWhatsApp(IdPersonal, IdPais, arr);

            if (res)
            {
                //PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;
                var Plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaPorId(IdPlantilla);
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = PlantillaReemplazada,
                    datosPlantillaWhatsApp = PlantillaWhatsApp,
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    //WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    //WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(IdPais);
                    //personal debe tener accesos
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(IdPersonal, IdPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    TWhatsAppMensajeEnviadoPostulante mensajeEnviado = new TWhatsAppMensajeEnviadoPostulante();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

                                using (WebClient client = new WebClient())
                                {
                                    //client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeTexto);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "hsm":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                //_waType = "hsm";
                                _waType = "template";

                                MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = "template";
                                _mensajePlantilla.template = new template();

                                _mensajePlantilla.template.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.template.name = DTO.WaBody;

                                _mensajePlantilla.template.language = new language();
                                _mensajePlantilla.template.language.policy = "deterministic";
                                _mensajePlantilla.template.language.code = "es";

                                _mensajePlantilla.template.components = new List<components>();
                                components Componente = new components();
                                Componente.type = "body";

                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    //_mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                    Componente.parameters = new List<parameters>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        //localizable_params _dato = new localizable_params();
                                        //_dato.@default = listaDatos.texto;

                                        //_mensajePlantilla.hsm.localizable_params.Add(_dato);
                                        parameters Dato = new parameters();
                                        Dato.type = "text";
                                        Dato.text = listaDatos.Texto;

                                        Componente.parameters.Add(Dato);
                                    }
                                }
                                _mensajePlantilla.template.components.Add(Componente);

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeImagen);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "document":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "document";

                                MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeDocumento);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                        }
                        var datoRespuesta = JsonConvert.DeserializeObject<respuestaMensaje>(resultado);
                        foreach (var itemGuardar in datoRespuesta.messages)
                        {
                            //WhatsAppMensajeEnviadoPostulanteRepositorio _repMensajeEnviadoPostulante = new WhatsAppMensajeEnviadoPostulanteRepositorio(_integraDBContext);

                            mensajeEnviado.WaId = itemGuardar.id;
                            mensajeEnviado.WaTo = DTO.WaTo;
                            mensajeEnviado.WaType = _waType;
                            mensajeEnviado.WaRecipientType = DTO.WaRecipientType;
                            mensajeEnviado.WaBody = DTO.WaBody;
                            mensajeEnviado.WaCaption = DTO.WaCaption;
                            mensajeEnviado.WaLink = DTO.WaLink;
                            mensajeEnviado.WaFileName = DTO.WaFileName;
                            mensajeEnviado.IdPais = IdPais;
                            if (IdPostulante != 0)
                            {
                                mensajeEnviado.IdPostulante = IdPostulante;
                            }
                            else
                            {
                                mensajeEnviado.IdPostulante = null;
                            }

                            mensajeEnviado.IdPersonal = IdPersonal;
                            mensajeEnviado.Estado = true;
                            mensajeEnviado.FechaCreacion = DateTime.Now;
                            mensajeEnviado.FechaModificacion = DateTime.Now;
                            mensajeEnviado.UsuarioCreacion = Usuario;
                            mensajeEnviado.UsuarioModificacion = Usuario;
                            _unitOfWork.WhatsAppMensajeEnviadoPostulanteRepository.Insert(mensajeEnviado);
                            _unitOfWork.Commit();
                            //_repMensajeEnviadoPostulante.Insert(mensajeEnviado);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error generado en EnvioAutomaticoPlantilla() " + ex.Message);
                }
                System.Threading.Thread.Sleep(5000);
            }

        }

        public string ObtenerNumeroWhatsApp(int codigoPais, string celular)
        {
            try
            {
                if (celular.Contains("-"))
                {
                    var index = celular.IndexOf("-");
                    celular = celular.Substring(index + 1);
                }
                if (codigoPais == 51)
                {
                    if (celular.Length == 9)
                    {
                        celular = "51" + celular;
                    }
                }
                else if (codigoPais == 57)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 12)
                    {
                        celular = "57" + celular;
                    }
                }
                else if (codigoPais == 591)
                {
                    if (celular.StartsWith("00"))
                    {
                        celular = celular.Substring(2, celular.Length - 2);
                    }
                    if (celular.Length < 11)
                    {
                        celular = "591" + celular;
                    }
                }
                return celular;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool ValidarNumeroEnvioWhatsApp(int IdPersonal, int IdPais, ValidarNumerosWhatsAppAsyncDTO DTO)
        {
            if (DTO != null)
            {
                string urlToPost;
                bool banderaLogin = false;
                string _tokenComunicacion = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    //WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio();
                    //WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio();

                    var _credencialesHost = _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(IdPais);
                    var tokenValida = _unitOfWork.WhatsAppUsuarioCredencialRepository.ValidarCredencialesUsuario(IdPersonal, IdPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _unitOfWork.WhatsAppUsuarioCredencialRepository.CredencialUsuarioLogin(IdPersonal);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);
                            banderaLogin = true;
                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _unitOfWork.WhatsAppUsuarioCredencialRepository.Insert(modelCredencial);
                                _unitOfWork.Commit();
                                _tokenComunicacion = item.token;
                            }
                        }
                        else
                        {
                            banderaLogin = false;
                        }
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);
                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);
                        if (datoRespuesta.contacts[0].status == "invalid")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error generado en ValidarNumeroEnvioWhatsApp() " + ex.Message); ;
                }
            }
            else
            {
                return false;
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Importación de datos de un excel
        /// </summary>
        /// <returns> objeto Agrupado </returns>
        public ImportacionPostulanteResultadoDTO ImportarExcel(IFormFile files)
        {
            ImportacionPostulanteResultadoDTO respuesta = new ImportacionPostulanteResultadoDTO();
            var ListaPostulante = new List<PostulanteImportadoDTO>();
            var ListaPostulanteRepetido = new List<PostulanteImportadoDTO>();
            CsvFile file = new CsvFile();
            try
            {
                //integraDBContext integraDB = new integraDBContext();
                //CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio(integraDB);
                //PostulanteRepositorio postulanteRep = new PostulanteRepositorio();
                int index = 0;
                int NregistrosNuevo = 0;
                int NregistrosRepetido = 0;
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    MissingFieldFound = null,
                };
                using (var reader = new StreamReader(files.OpenReadStream()))
                using (var cvs = new CsvReader(reader, config))
                {
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;
                        PostulanteImportadoDTO postulante = new PostulanteImportadoDTO();
                        bool ExisteDNI = _unitOfWork.PostulanteRepository.Exist(x => x.NroDocumento.Trim().Equals(cvs.GetField<string>("NroDocumento").Trim()));
                        bool ExisteEmailApellido = _unitOfWork.PostulanteRepository.Exist(x => (x.ApellidoPaterno.ToLower().Equals(cvs.GetField<string>("ApellidoPaterno").ToLower()) && x.Email.ToLower().Equals(cvs.GetField<string>("Email").ToLower())) || (x.Email.ToLower().Equals(cvs.GetField<string>("Email").ToLower())));
                        if (ExisteDNI || ExisteEmailApellido)
                        {
                            NregistrosRepetido += 2;
                            postulante.Nombre = cvs.GetField<string>("Nombre");
                            postulante.ApellidoPaterno = cvs.GetField<string>("ApellidoPaterno");
                            postulante.ApellidoMaterno = cvs.GetField<string>("ApellidoMaterno");
                            postulante.IdTipoDocumento = cvs.GetField<int?>("IdTipoDocumento");
                            postulante.NroDocumento = cvs.GetField<string>("NroDocumento");
                            postulante.Celular = cvs.GetField<string>("Celular");
                            postulante.Email = cvs.GetField<string>("Email");
                            postulante.IdPais = cvs.GetField<int?>("IdPais");
                            postulante.IdCiudad = cvs.GetField<int?>("IdCiudad");
                            postulante.IdEstadoEtapaProcesoSeleccion = cvs.GetField<int?>("IdEstadoEtapa");
                            postulante.IdPostulanteNivelPotencial = cvs.GetField<int?>("IdNivelPotencial");
                            postulante.Origen = "Excel";

                            ListaPostulanteRepetido.Add(postulante);

                            var postulanteRepetido = _unitOfWork.PostulanteRepository.GetBy(x => x.NroDocumento.Trim().Equals(postulante.NroDocumento.Trim()) || (x.Email.ToLower().Equals(postulante.Email.ToLower()) && x.ApellidoPaterno.ToLower().Equals(postulante.ApellidoPaterno.ToLower()))).FirstOrDefault();
                            postulante = new PostulanteImportadoDTO();
                            postulante.Nombre = postulanteRepetido.Nombre;
                            postulante.ApellidoPaterno = postulanteRepetido.ApellidoPaterno;
                            postulante.ApellidoMaterno = postulanteRepetido.ApellidoMaterno;
                            postulante.IdTipoDocumento = postulanteRepetido.IdTipoDocumento;
                            postulante.NroDocumento = postulanteRepetido.NroDocumento;
                            postulante.Celular = postulanteRepetido.Celular;
                            postulante.Email = postulanteRepetido.Email;
                            postulante.IdPais = postulanteRepetido.IdPais;
                            postulante.IdCiudad = postulanteRepetido.IdCiudad;
                            postulante.IdEstadoEtapaProcesoSeleccion = null;
                            postulante.IdPostulanteNivelPotencial = null;
                            postulante.Origen = "Integra";

                            ListaPostulanteRepetido.Add(postulante);


                        }
                        else
                        {
                            NregistrosNuevo++;
                            postulante.Nombre = cvs.GetField<string>("Nombre");
                            postulante.ApellidoPaterno = cvs.GetField<string>("ApellidoPaterno");
                            postulante.ApellidoMaterno = cvs.GetField<string>("ApellidoMaterno");
                            postulante.IdTipoDocumento = cvs.GetField<int?>("IdTipoDocumento");
                            postulante.NroDocumento = cvs.GetField<string>("NroDocumento");
                            postulante.Celular = cvs.GetField<string>("Celular");
                            postulante.Email = cvs.GetField<string>("Email");
                            postulante.IdPais = cvs.GetField<int?>("IdPais");
                            postulante.IdCiudad = cvs.GetField<int?>("IdCiudad");
                            postulante.IdEstadoEtapaProcesoSeleccion = cvs.GetField<int?>("IdEstadoEtapa");
                            postulante.IdPostulanteNivelPotencial = cvs.GetField<int?>("IdNivelPotencial");
                            postulante.Origen = "NUEVO";

                            var IdRespuestas = cvs.GetField<string>("IdFactorDesaprobatorio");

                            if (IdRespuestas != null)
                            {
                                var x = IdRespuestas.Split("/");
                                foreach (var idRespuesta in x)
                                {
                                    if (!idRespuesta.Equals(""))
                                    {
                                        if (postulante.ListaRespuestaDesaprobatoria == null)
                                        {
                                            postulante.ListaRespuestaDesaprobatoria = new List<int>();
                                        }
                                        int y = Int32.Parse(idRespuesta);
                                        postulante.ListaRespuestaDesaprobatoria.Add(Int32.Parse(idRespuesta));
                                    }
                                }
                            }


                            ListaPostulante.Add(postulante);
                        }
                    }
                }
                var Nregistros = index;
                respuesta.ListaPostulante = ListaPostulante;
                respuesta.ListaPostulanteRepetido = ListaPostulanteRepetido;
                respuesta.NregistrosNuevo = NregistrosNuevo;
                respuesta.NregistrosRepetido = NregistrosRepetido;

                return respuesta;
            }
            catch (Exception ex)
            {

                throw new Exception("Error en ImportarExcel " + ex.Message);
            }

        }        

        /// Autor: Eliot Arias F.
        /// Fecha: 15/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Insertar Postulantes por Importacion 
        /// </summary>
        /// <returns>Ok 200, mas mensaje de confirmación</returns>
        public ResultadoInsertarPostulante InsertarPostulantePorImportacion(PostulanteProcesoSeleccionConsolidadoDTO lista)
        {
            try
            {
                if (lista.listaPostulante.Count == 0 && lista.listaPostulante == null)
                {
                    return new ResultadoInsertarPostulante { Mensaje = "La tabla de postulantes esta vacia", Valor = false };
                }

                ResultadoInsertarPostulante mensaje = new ResultadoInsertarPostulante();
                var insertadoCorrecto = 0;
                var insertadosError = 0;
                ExamenAsignado examenPostulante = new ExamenAsignado();
                List<PostulanteInformacionDTO> ListaInformacionPostulante = new List<PostulanteInformacionDTO>();
                foreach (var item in lista.listaPostulante)
                {
                    PostulanteInformacionDTO postulante = new PostulanteInformacionDTO();
                    postulante.Nombre = item.Nombre;
                    postulante.ApellidoPaterno = item.ApellidoPaterno;
                    postulante.ApellidoMaterno = item.ApellidoMaterno;
                    postulante.NroDocumento = item.NroDocumento;
                    postulante.Celular = item.Celular;
                    postulante.Email = item.Email;
                    postulante.IdTipoDocumento = item.IdTipoDocumento;
                    postulante.IdPais = item.IdPais;
                    postulante.IdCiudad = item.IdCiudad;
                    postulante.IdProcesoSeleccion = lista.IdProcesoSeleccion;
                    postulante.IdPostulanteNivelPotencial = item.IdPostulanteNivelPotencial;
                    postulante.IdProveedor = lista.IdProveedor;
                    postulante.IdPersonal_OperadorProceso = lista.IdPersonalOperadorProceso;
                    postulante.IdConvocatoriaPersonal = lista.IdConvocatoria;
                    postulante.IdProcesoSeleccionEtapa = lista.IdEtapaProcesoSeleccion;
                    postulante.IdEstadoEtapaProcesoSeleccion = item.IdEstadoEtapaProcesoSeleccion;
                    postulante.ListaRespuestaDesaprobatoria = item.ListaRespuestaDesaprobatoria;
                    ListaInformacionPostulante.Add(postulante);
                }

                foreach (var item in ListaInformacionPostulante)
                {
                    InsertarPostulanteImportadoDTO usuarioPostulante = new InsertarPostulanteImportadoDTO();
                    usuarioPostulante.Usuario = lista.Usuario;
                    usuarioPostulante.DatosPostulanteImportación = item;
                    mensaje = InsertarPostulanteNuevo(usuarioPostulante);
                    if (mensaje.Valor)
                    {
                        insertadoCorrecto++;
                    }
                    else
                    {
                        insertadosError++;
                    }
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en InsertarPostulantePorImportacion " + ex.Message);
            }
        }

        public ResultadoInsertarPostulante InsertarPostulanteNuevo(InsertarPostulanteImportadoDTO Postulante)
        {
            try
            {
                //PostulanteRepositorio _repPostulante = new PostulanteRepositorio(_integraDBContext);
                //PostulanteProcesoSeleccionRepositorio _repPostulanteProcesoSeleccion = new PostulanteProcesoSeleccionRepositorio(_integraDBContext);
                //ExamenAsignadoRepositorio _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
                TPostulante postulanteRegistroNuevo = new TPostulante();
                //ExamenAsignadoEvaluadorRepositorio _repExamenAsignadoEvaluador = new ExamenAsignadoEvaluadorRepositorio(_integraDBContext);
                //IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                var personal = _unitOfWork.IntegraAspNetUserRepository.ObtenerIdentidadUsusario(Postulante.Usuario);
                TPersona persona = new TPersona();
                //ProcesoSeleccionEtapaRepositorio _repProcesoSeleccionEtapa = new ProcesoSeleccionEtapaRepositorio(_integraDBContext);
                //EtapaProcesoSeleccionCalificadoRepositorio _reEtapaCalificacion = new EtapaProcesoSeleccionCalificadoRepositorio(_integraDBContext);
                //ExamenRealizadoRespuestaEvaluadorRepositorio _repExamenRealizadoRespuestaEvaluador = new ExamenRealizadoRespuestaEvaluadorRepositorio(_integraDBContext);
                var IdExamenAsignadoEvaluadorCriterioDesaprobatorio = 0;
                TExamenAsignadoEvaluador examenAsignadoEvaluadorCriterioDesaprobatorio = new TExamenAsignadoEvaluador();

                if (_unitOfWork.PostulanteRepository.ObtenerPostulantePorEmail(Postulante.DatosPostulanteImportación.Email))
                {
                    return new ResultadoInsertarPostulante { Mensaje = "El correo que ingreso ya esta en uso", Valor = false };
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        postulanteRegistroNuevo.Nombre = Postulante.DatosPostulanteImportación.Nombre;
                        postulanteRegistroNuevo.ApellidoPaterno = Postulante.DatosPostulanteImportación.ApellidoPaterno;
                        postulanteRegistroNuevo.ApellidoMaterno = Postulante.DatosPostulanteImportación.ApellidoMaterno;
                        postulanteRegistroNuevo.NroDocumento = Postulante.DatosPostulanteImportación.NroDocumento;
                        postulanteRegistroNuevo.Celular = Postulante.DatosPostulanteImportación.Celular;
                        postulanteRegistroNuevo.Email = Postulante.DatosPostulanteImportación.Email;
                        postulanteRegistroNuevo.IdTipoDocumento = Postulante.DatosPostulanteImportación.IdTipoDocumento;
                        postulanteRegistroNuevo.IdPais = Postulante.DatosPostulanteImportación.IdPais;
                        postulanteRegistroNuevo.IdCiudad = Postulante.DatosPostulanteImportación.IdCiudad;
                        postulanteRegistroNuevo.Estado = true;
                        postulanteRegistroNuevo.UsuarioCreacion = Postulante.Usuario;
                        postulanteRegistroNuevo.UsuarioModificacion = Postulante.Usuario;
                        postulanteRegistroNuevo.FechaCreacion = DateTime.Now;
                        postulanteRegistroNuevo.FechaModificacion = DateTime.Now;
                        _unitOfWork.PostulanteRepository.Insert(postulanteRegistroNuevo);
                        _unitOfWork.Commit();
                        _unitOfWork.DetachAll();

                        int? idCreacionCorrecta = _personaService.InsertarPersona(postulanteRegistroNuevo.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Postulante, Postulante.Usuario);
                        if (idCreacionCorrecta == null)
                        {
                            return new ResultadoInsertarPostulante { Mensaje = "No se creo clasificacion persona", Valor = false };
                        }
                        if (Postulante.DatosPostulanteImportación.IdProcesoSeleccion.HasValue)
                        {
                            TPostulanteProcesoSeleccion postulanteProcesoSeleccion = new TPostulanteProcesoSeleccion
                            {
                                IdPostulante = postulanteRegistroNuevo.Id,
                                IdProcesoSeleccion = Postulante.DatosPostulanteImportación.IdProcesoSeleccion.Value,
                                FechaRegistro = DateTime.Now,
                                Estado = true,
                                UsuarioCreacion = Postulante.Usuario,
                                UsuarioModificacion = Postulante.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                IdEstadoProcesoSeleccion = Postulante.DatosPostulanteImportación.IdEstadoProcesoSeleccion,
                                IdPostulanteNivelPotencial = Postulante.DatosPostulanteImportación.IdPostulanteNivelPotencial,
                                IdProveedor = Postulante.DatosPostulanteImportación.IdProveedor,
                                IdPersonalOperadorProceso = Postulante.DatosPostulanteImportación.IdPersonal_OperadorProceso,
                                IdConvocatoriaPersonal = Postulante.DatosPostulanteImportación.IdConvocatoriaPersonal
                            };
                            var res = _unitOfWork.PostulanteProcesoSeleccionRepository.Insert(postulanteProcesoSeleccion);
                            if (res)
                            {
                                var postulacion = _unitOfWork.ExamenAsignadoRepository.ObtenerPorIdProcesoSeleccionYPorIdPostulante(postulanteProcesoSeleccion.Id, postulanteRegistroNuevo.Id);
                                if (postulacion == null)
                                {
                                    //var procesoSeleccion = _repProcesoSeleccion.GetBy(x => x.Id == Postulante.InformacionPostulante.IdProcesoSeleccion).First();
                                    var procesoSeleccion = _unitOfWork.ProcesoSeleccionRepository.FirstById(Postulante.DatosPostulanteImportación.IdProcesoSeleccion.Value);
                                    //var examenPorProceso = _repExamenAsignado.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    var examenPorProceso = _unitOfWork.ExamenAsignadoRepository.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    var examenEvaluadorPorProceso = _unitOfWork.ExamenAsignadoEvaluadorRepository.ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(postulanteProcesoSeleccion.IdProcesoSeleccion);
                                    foreach (var item in examenPorProceso)
                                    {
                                        TExamenAsignado examenAsignado = new TExamenAsignado();
                                        examenAsignado.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                        examenAsignado.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                                        examenAsignado.IdExamen = item.IdExamen;
                                        examenAsignado.EstadoExamen = false;
                                        examenAsignado.Estado = true;
                                        examenAsignado.UsuarioCreacion = Postulante.Usuario;
                                        examenAsignado.UsuarioModificacion = Postulante.Usuario;
                                        examenAsignado.FechaCreacion = DateTime.Now;
                                        examenAsignado.FechaModificacion = DateTime.Now;
                                        _unitOfWork.ExamenAsignadoRepository.Insert(examenAsignado);
                                        //_unitOfWork.Commit();
                                    }
                                    foreach (var item in examenEvaluadorPorProceso)
                                    {
                                        TExamenAsignadoEvaluador examenAsignadoEvaluador = new TExamenAsignadoEvaluador();
                                        examenAsignadoEvaluador.IdPersonal = personal.Id;
                                        examenAsignadoEvaluador.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                        examenAsignadoEvaluador.IdProcesoSeleccion = postulanteProcesoSeleccion.IdProcesoSeleccion;
                                        examenAsignadoEvaluador.IdExamen = item.IdExamen;
                                        examenAsignadoEvaluador.EstadoExamen = false;
                                        examenAsignadoEvaluador.Estado = true;
                                        examenAsignadoEvaluador.UsuarioCreacion = Postulante.Usuario;
                                        examenAsignadoEvaluador.UsuarioModificacion = Postulante.Usuario;
                                        examenAsignadoEvaluador.FechaCreacion = DateTime.Now;
                                        examenAsignadoEvaluador.FechaModificacion = DateTime.Now;

                                        if (examenAsignadoEvaluador.IdExamen == 111 && Postulante.DatosPostulanteImportación.ListaRespuestaDesaprobatoria != null)
                                        {
                                            examenAsignadoEvaluador.EstadoExamen = true;
                                        }
                                        _unitOfWork.ExamenAsignadoEvaluadorRepository.Insert(examenAsignadoEvaluador);
                                        //_unitOfWork.Commit();

                                        if (examenAsignadoEvaluador.IdExamen == 111)
                                        {
                                            IdExamenAsignadoEvaluadorCriterioDesaprobatorio = examenAsignadoEvaluador.Id;
                                            examenAsignadoEvaluadorCriterioDesaprobatorio = examenAsignadoEvaluador;
                                        }
                                    }

                                    if (IdExamenAsignadoEvaluadorCriterioDesaprobatorio > 0 && Postulante.DatosPostulanteImportación.ListaRespuestaDesaprobatoria != null)
                                    {

                                        foreach (var item in Postulante.DatosPostulanteImportación.ListaRespuestaDesaprobatoria)
                                        {
                                            TExamenRealizadoRespuestaEvaluador evaluadorExamen = new TExamenRealizadoRespuestaEvaluador();
                                            evaluadorExamen.IdExamenAsignadoEvaluador = IdExamenAsignadoEvaluadorCriterioDesaprobatorio;
                                            evaluadorExamen.IdPregunta = 761; // Id de Pregunta de Examen de Criterio de Desaprobacion
                                            evaluadorExamen.IdRespuestaPregunta = item;
                                            evaluadorExamen.TextoRespuesta = null;
                                            evaluadorExamen.Estado = true;
                                            evaluadorExamen.UsuarioCreacion = Postulante.Usuario;
                                            evaluadorExamen.UsuarioModificacion = Postulante.Usuario;
                                            evaluadorExamen.FechaCreacion = DateTime.Now;
                                            evaluadorExamen.FechaModificacion = DateTime.Now;

                                            //_repExamenRealizadoRespuestaEvaluador.Insert(evaluadorExamen);
                                            _unitOfWork.ExamenRealizadoRespuestaEvaluadorRepository.Insert(evaluadorExamen);
                                        }
                                    }
                                }

                                //Se asignan todas las etapas del proceso al postulante
                                //var EtapasProceso =  _repProcesoSeleccionEtapa.GetBy(x => x.IdProcesoSeleccion == Postulante.InformacionPostulante.IdProcesoSeleccion && x.Estado == true);
                                var EtapasProcesoSeleccion = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(Postulante.DatosPostulanteImportación.IdProcesoSeleccion.Value);
                                foreach (var item in EtapasProcesoSeleccion)
                                {
                                    TEtapaProcesoSeleccionCalificado etapaCalificacion = new TEtapaProcesoSeleccionCalificado();
                                    etapaCalificacion.IdProcesoSeleccionEtapa = item.Id;
                                    etapaCalificacion.IdPostulante = postulanteProcesoSeleccion.IdPostulante;
                                    if (Postulante.DatosPostulanteImportación.IdProcesoSeleccionEtapa == item.Id)
                                    {
                                        etapaCalificacion.EsEtapaAprobada = Postulante.DatosPostulanteImportación.IdEstadoEtapaProcesoSeleccion == 1 ? true : false;
                                        etapaCalificacion.IdEstadoEtapaProcesoSeleccion = (Postulante.DatosPostulanteImportación.IdEstadoEtapaProcesoSeleccion == null || Postulante.DatosPostulanteImportación.IdEstadoEtapaProcesoSeleccion == 0) ? 9 : Postulante.DatosPostulanteImportación.IdEstadoEtapaProcesoSeleccion;
                                        etapaCalificacion.EsEtapaActual = true;
                                    }
                                    else
                                    {
                                        etapaCalificacion.EsEtapaAprobada = false;
                                        etapaCalificacion.IdEstadoEtapaProcesoSeleccion = 9;//Las demas etapas ingresan con el estado Sin rendir
                                        etapaCalificacion.EsEtapaActual = false;
                                    }
                                    etapaCalificacion.EsContactado = false;
                                    etapaCalificacion.Estado = true;
                                    etapaCalificacion.UsuarioCreacion = Postulante.Usuario;
                                    etapaCalificacion.UsuarioModificacion = Postulante.Usuario;
                                    etapaCalificacion.FechaCreacion = DateTime.Now;
                                    etapaCalificacion.FechaModificacion = DateTime.Now;
                                    _unitOfWork.EtapaProcesoSeleccionCalificadoRepository.Insert(etapaCalificacion);
                                    //_unitOfWork.Commit();
                                }
                            }
                            else
                            {
                                return new ResultadoInsertarPostulante { Mensaje = "Falla al insertar proceso selección", Valor = false };
                            }
                        }
                        _unitOfWork.Commit();
                        scope.Complete();
                        return new ResultadoInsertarPostulante { Mensaje = "Postulantes Registrados con Exito", Valor = true };
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error en la insersion de Postulante\\" + e.Message);
            }
        }

        /// Autor: Eliot Arias F.
        /// Fecha: 15/11/2024
        /// Versión: 1.0
        /// <summary>
        /// Insertar Postulantes por Importacion 
        /// </summary>
        /// <returns>Ok 200, mas mensaje de confirmación</returns>
        public Postulante ObtenerPostulanteInformacion(int IdPostulante)
        {
            try
            {
                return _unitOfWork.PostulanteRepository.ObtenerPorId(IdPostulante);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Obtener Postulante Informacion: {ex.Message}");
            }
        }


        public IEnumerable<PostulanteProcesoFormatDTO> HabilitarExamenesEvaluaciones(PostulanteExamenesDTO parametros)
        {
            try
            {
                var resultado = _unitOfWork.PostulanteRepository.HabilitarExamenesEvaluaciones(parametros);
                var resultadoFormat = resultado
                    .GroupBy(x => new
                    {
                        IdEvaluacion = x.IdEvaluacion,
                        NombreEvaluacion = x.NombreEvaluacion,
                    }).Select(g => new PostulanteProcesoFormatDTO
                    {
                        IdEvaluacion = g.Key.IdEvaluacion,
                        NombreEvaluacion = g.Key.NombreEvaluacion,
                        ListaExamenes = g.Select(x => new PostulanteProcesoExamenesDTO
                        {
                            IdExamen = x.IdExamen,
                            NombreExamen = x.NombreExamen,
                            EstadoExamen = x.EstadoExamen,
                        }).ToList()
                    }).ToList();

                var ExamEval = "";
                if (parametros.IdEvaluacion != 0)
                {
                    ExamEval = $"El Id de la evaluacion fue {parametros.IdExamen}";
                }
                else if (parametros.IdExamen != 0)
                {
                    ExamEval = $"El Id del examen fue {parametros.IdExamen}";
                }
                else
                {
                    ExamEval = "Se realizo una apertura general";
                }

                TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                {
                    Sender = "sistemenabled@bsginstitute.com",
                    Recipient = "cquispem@bsginstitute.com",
                    Subject = "Examenes o Evaluaciones habilitadas",
                    Message = $"Se realizo una reaperturacion a las {DateTime.Now} del postulante:\n - IdPostulante {parametros.IdPostulante}\n - IdProcesoSeleccion {parametros.IdProcesoSeleccion}\n - {ExamEval}",
                };
                var mailService = new TMK_MailService();
                mailService.SetData(mailDataPersonalizado);
                mailService.SendMessageTask();
                return resultadoFormat;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener evaluaciones de postulante: {ex.Message}");
            }
        }


    }
}