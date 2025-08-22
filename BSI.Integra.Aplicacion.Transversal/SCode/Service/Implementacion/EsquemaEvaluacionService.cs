using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EsquemaEvaluacionService
    /// Autor: Jonathan Caipo
    /// Fecha: 14/11/2022
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacion
    /// </summary>
    public class EsquemaEvaluacionService : IEsquemaEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EsquemaEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacion, EsquemaEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<EsquemaEvaluacionDTO, TEsquemaEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<EsquemaEvaluacionDetalleDTO, TEsquemaEvaluacionDetalle>(MemberList.None).ReverseMap();
            }
            ); ;

            _mapper = new Mapper(config);
        }

        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de EsquemaEvaluacion
        /// </summary>
        /// <returns> Lista EsquemaEvaluacionDTO </returns>

        public IEnumerable<EsquemaEvaluacionDTO> ObtenerTodo()
        {
            return _unitOfWork.EsquemaEvaluacionRepository.ObtenerTodo();
        }


        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Nombre Congelamiento Esquema por idMatricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> string </returns>
        public string ObtenerNombreCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var esquemaEvaluacionService = new EsquemaEvaluacionService(_unitOfWork);
                var esquema = _unitOfWork.EsquemaEvaluacionRepository.ObtenerNombreCongelamientoEsquemaPorMatricula(idMatriculaCabecera);
                if (esquema == null)
                {
                    List<ValorIdMatriculaDTO> matriculas = new List<ValorIdMatriculaDTO>();
                    ValorIdMatriculaDTO matricula = new ValorIdMatriculaDTO();
                    matricula.IdMatriculaCabecera = idMatriculaCabecera;
                    matriculas.Add(matricula);
                    EsquemaEvaluacionService esquemaEvaluacion = new EsquemaEvaluacionService(_unitOfWork);
                    var listado = esquemaEvaluacion.InsertarMatricula(matriculas);
                    esquema = _unitOfWork.EsquemaEvaluacionRepository.ObtenerNombreCongelamientoEsquemaPorMatricula2(idMatriculaCabecera);
                }
                return esquema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Detalle de la calificación por curso
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPEspecifico"></param>
        /// <param name="grupo"></param>
        /// <returns> EsquemaEvaluacionNotaCursoDTO </returns>
        public EsquemaEvaluacionNotaCursoDTO ObtenerDetalleCalificacionPorCurso(int idMatriculaCabecera, int idPEspecifico, int grupo)
        {
            try
            {
                var listadoEvaluaciones = _unitOfWork.EsquemaEvaluacionRepository.ListadoDetalladoItemEvaluablePorAlumno(idMatriculaCabecera, idPEspecifico, grupo);

                if (listadoEvaluaciones == null || listadoEvaluaciones.Count == 0)
                    throw new Exception("No se tiene configurado el esquema de evaluación");

                if (listadoEvaluaciones.Any(w => w.IdFormaCalificacionCriterio == null))
                    throw new Exception(
                        "No se configuró correctamente la Forma de Calificación de un Criterio de Evaluación");
                if (listadoEvaluaciones.Any(w => w.IdFormaCalculoEvaluacion_Parametro == null))
                    throw new Exception(
                        "No se configuró correctamente la Forma de Calculo de un Parametro de Evaluación");
                if (listadoEvaluaciones.Any(w => w.IdFormaCalculoEvaluacion_Criterio == null))
                    throw new Exception(
                    "No se configuró correctamente la Forma de Calculo de un Criterio de Evaluación");

                EsquemaEvaluacionNotaCursoDTO notaCurso = new EsquemaEvaluacionNotaCursoDTO()
                {
                    IdMatriculaCabecera = idMatriculaCabecera,
                    IdPEspecifico = idPEspecifico,
                    Grupo = grupo,
                    DetalleCalificacion = new List<EsquemaEvaluacionDetalleCalificacionDTO>()
                };

                foreach (var grupoCriterio in listadoEvaluaciones.GroupBy(g =>
                    new { g.IdCriterioEvaluacion, g.CriterioEvaluacion }))
                {
                    EsquemaEvaluacionDetalleCalificacionDTO detalleCriterio = new EsquemaEvaluacionDetalleCalificacionDTO()
                    {
                        IdCriterioEvaluacion = grupoCriterio.Key.IdCriterioEvaluacion,
                        CriterioEvaluacion = grupoCriterio.Key.CriterioEvaluacion,
                        Ponderacion = grupoCriterio.FirstOrDefault().Ponderacion_Criterio,
                        Valor = 0
                    };

                    //calculo de la nota por criterio
                    foreach (var grupoCriterioInstanciado in grupoCriterio.GroupBy(g =>
                        g.IdEsquemaEvaluacionPGeneralDetalle))
                    {
                        decimal notaCriterio = 0;

                        foreach (var grupoParametro in grupoCriterioInstanciado.GroupBy(g => g.IdParametroEvaluacion))
                        {
                            decimal notaParametro = 0;
                            foreach (var parametroIndividual in grupoParametro)
                            {
                                if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro ==
                                    (int)Enums.FormaCalculoEvaluacion.Suma)
                                    notaParametro += parametroIndividual.ValorEscala ?? 0;
                                if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro ==
                                    (int)Enums.FormaCalculoEvaluacion.Promedio)
                                    notaParametro += Convert.ToDecimal(
                                        ((double)(parametroIndividual.ValorEscala ?? 0)) *
                                        (parametroIndividual.Ponderacion_Parametro * 1.0) / 100.0
                                    );
                            }
                            notaCriterio += notaParametro;
                        }
                        detalleCriterio.Valor += notaCriterio;
                    }
                    if (grupoCriterio.FirstOrDefault().IdFormaCalculoEvaluacion_Criterio ==
                    (int)Enums.FormaCalculoEvaluacion.Promedio)
                        detalleCriterio.Valor = detalleCriterio.Valor /
                                                ((decimal)(grupoCriterio
                                                    .Select(s => s.IdEsquemaEvaluacionPGeneralDetalle).Distinct()
                                                    .Count() * 1.0));
                    //valida si el criterio es promedio
                    notaCurso.DetalleCalificacion.Add(detalleCriterio);
                }

                //calculo de la nota final
                if (notaCurso.DetalleCalificacion != null && notaCurso.DetalleCalificacion.Count > 0)
                {
                    notaCurso.NotaCurso = 0;
                    foreach (var calificacion in notaCurso.DetalleCalificacion)
                    {
                        notaCurso.NotaCurso += calificacion.Valor;
                    }

                    if (listadoEvaluaciones.FirstOrDefault().IdFormaCalculoEvaluacion_Esquema ==
                        (int)Enums.FormaCalculoEvaluacion.Promedio)
                        notaCurso.NotaCurso = Convert.ToDecimal(
                            ((double)notaCurso.NotaCurso * 1.0) / (notaCurso.DetalleCalificacion.Count * 1.0)
                        );
                }
                return notaCurso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> int </returns>
        public int InsertarMatricula(List<ValorIdMatriculaDTO> json)
        {
            try
            {
                MatriculaCabeceraService _repMatriculaCabecera = new MatriculaCabeceraService(_unitOfWork);
                EsquemaEvaluacionService _repEsquemaRepositorio = new EsquemaEvaluacionService(_unitOfWork);
                foreach (var matriculas in json)
                {
                    var matriculacabecera = _repMatriculaCabecera.ObtenerPorIdMatriculaCabecera(matriculas.IdMatriculaCabecera);
                    matriculas.Nuevo = _unitOfWork.EsquemaEvaluacionRepository.ExisteNuevaAulaVirtual(matriculacabecera.IdPespecifico);
                    //añadir el eliminar
                    _unitOfWork.EsquemaEvaluacionRepository.EliminarMatriculaCabecera(matriculas.IdMatriculaCabecera);

                    if (matriculas.Nuevo == true)
                    {
                        _unitOfWork.EsquemaEvaluacionRepository.InsertarMatriculaNueva(matriculas.IdMatriculaCabecera);
                    }
                    else
                    {
                        _unitOfWork.EsquemaEvaluacionRepository.InsertarMatriculaAntigua(matriculas.IdMatriculaCabecera);
                    }
                }
                return 1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Congelamiento Esquema Por Matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> Lista: List<CongelamientoPEspecificoMatriculaAlumnoDTO> </returns>
        public List<CongelamientoPEspecificoMatriculaAlumnoDTO> ObtenerCongelamientoEsquemaPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.EsquemaEvaluacionRepository.ObtenerCongelamientoEsquemaPorMatricula(idMatriculaCabecera);
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
        /// Actualiza el congelamiento del Esquema Evaluacion
        /// </summary>
        /// <param name="json"></param>
        /// <returns> bool </returns>
        public bool ActualizarCongelamientoEsquemaPorMatricula(EditarCongelamientoPEspecificoMatriculaAlumnoDTO json)
        {
            try
            {
                return _unitOfWork.EsquemaEvaluacionRepository.ActualizarCongelamientoEsquemaPorMatricula(json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EsquemaEvaluacion_NotaCursosDTO ListadoCriteriosEvaluacionPorCurso(int IdMatriculaCabecera, int IdPEspecifico, int Grupo)
        {
            ExcepcionRegistroDTO Excepcion = new ExcepcionRegistroDTO();
            var listadoEvaluaciones = _unitOfWork.EsquemaEvaluacionRepository.ListadoCriteriosEvaluacionPorCurso(IdMatriculaCabecera, IdPEspecifico, Grupo);
            var listaUnicos = listadoEvaluaciones.OrderByDescending(x => x.ValorEscala).GroupBy(g => new
            {
                g.IdCriterioEvaluacion,
                g.CriterioEvaluacion,
            }).Select(x => new
            {
                IdCriterioEvaluacion = x.Key.IdCriterioEvaluacion,
                CriterioEvaluacion = x.Key.CriterioEvaluacion,

            }).ToList();
            var listUnicos = new List<CriterioEvaluacionCursoDTO>();
            foreach (var item in listaUnicos)
            {
                listUnicos.Add(listadoEvaluaciones.Where(x => x.IdCriterioEvaluacion == item.IdCriterioEvaluacion && x.CriterioEvaluacion == item.CriterioEvaluacion).First());
            };
            listadoEvaluaciones = listUnicos;
            if (listadoEvaluaciones == null || listadoEvaluaciones.Count == 0)
            {
                Excepcion.ExcepcionGenerada = true;
                Excepcion.DescripcionGeneral = "No se tiene configurado el esquema de evaluación";
            };
            if (listadoEvaluaciones.Any(w => w.IdFormaCalificacionCriterio == null))
            {
                Excepcion.ExcepcionGenerada = true;
                Excepcion.DescripcionGeneral = "No se configuró correctamente la Forma de Calificación de un Criterio de Evaluación";
            };
            if (listadoEvaluaciones.Any(w => w.IdFormaCalculoEvaluacion_Parametro == null))
            {
                Excepcion.ExcepcionGenerada = true;
                Excepcion.DescripcionGeneral = "No se configuró correctamente la Forma de Calculo de un Parametro de Evaluación";
            };
            if (listadoEvaluaciones.Any(w => w.IdFormaCalculoEvaluacion_Criterio == null))
            {
                Excepcion.ExcepcionGenerada = true;
                Excepcion.DescripcionGeneral = "No se configuró correctamente la Forma de Calculo de un Criterio de Evaluación";
            };
            EsquemaEvaluacion_NotaCursosDTO notaCurso = new EsquemaEvaluacion_NotaCursosDTO()
            {
                IdMatriculaCabecera = IdMatriculaCabecera,
                Grupo = Grupo,
                DetalleCalificacion = new List<EsquemaEvaluacion_DetalleCalificacionDTO>()
            };
            var Tareas = 0;
            var TareasCalificadas = 0;
            var PreguntasReflexivas = 0;
            var agregarPonderacionTareas = 0;
            var _matricula = _unitOfWork.MatriculaCabeceraRepository.obtenerGrupoMatriculaIdPorCurso(IdMatriculaCabecera, IdPEspecifico);
            if (_matricula == null)
            {
                _matricula = _unitOfWork.MatriculaCabeceraRepository.obtenerGrupoMatriculaIdPorCursoGeneral(IdMatriculaCabecera, IdPEspecifico);
            }
            var _registroTareasCalificar = _unitOfWork.EstructuraEspecificaRepository.ObtenerRegistroEstructuraTareaCalificarCapitulo(_matricula.IdPGeneralHijo, _matricula.IdPGeneralPadre, _matricula.IdAlumno);
            Tareas = _registroTareasCalificar.Count();
            TareasCalificadas = _registroTareasCalificar.Where(x => x.Calificado == true).Count();
            foreach (var item in _registroTareasCalificar)
            {
                var estado = _unitOfWork.EstructuraEspecificaTareaRepository.EstadoEnvioAprendizajeReflexivo(IdMatriculaCabecera, _matricula.IdPEspecificoHijo, item.Id);
                if (estado == true)
                {
                    PreguntasReflexivas++;
                }
            }
            var g1 = listadoEvaluaciones.GroupBy(g => new { g.IdCriterioEvaluacion, g.CriterioEvaluacion }).ToList();
            foreach (var grupoCriterio in listadoEvaluaciones.GroupBy(g =>
                new { g.IdCriterioEvaluacion, g.CriterioEvaluacion }))
            {
                EsquemaEvaluacion_DetalleCalificacionDTO detalleCriterio = new EsquemaEvaluacion_DetalleCalificacionDTO()
                {
                    IdCriterioEvaluacion = grupoCriterio.Key.IdCriterioEvaluacion,
                    CriterioEvaluacion = grupoCriterio.Key.CriterioEvaluacion,
                    Ponderacion = grupoCriterio.FirstOrDefault().Ponderacion_Criterio,
                    Valor = 0,
                    IdParametroEvaluacion = listadoEvaluaciones.Where(x => x.IdCriterioEvaluacion == grupoCriterio.Key.IdCriterioEvaluacion && x.CriterioEvaluacion == grupoCriterio.Key.CriterioEvaluacion).FirstOrDefault().IdParametroEvaluacion,

                };

                var g2 = grupoCriterio.GroupBy(g => g.IdEsquemaEvaluacionPGeneralDetalle).ToList();
                //calculo de la nota por criterio
                //foreach (var grupoCriterioInstanciado in grupoCriterio.GroupBy(g =>
                //    g.IdEsquemaEvaluacionPGeneralDetalle))
                //{
                //    decimal notaCriterio = 0;

                //    foreach (var grupoParametro in grupoCriterioInstanciado.GroupBy(g => g.IdParametroEvaluacion))
                //    {
                //        decimal notaParametro = 0;
                //        foreach (var parametroIndividual in grupoParametro)
                //        {
                //            decimal valorInicial = 0;
                //            var total = 0;
                //            decimal divicion = 0;
                //            if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro == (int)Enums.FormaCalculoEvaluacion.Suma)
                //            {
                //                valorInicial = parametroIndividual.ValorEscala ?? 0;
                //                total = g2.Count();
                //                divicion = valorInicial / total;
                //                notaParametro += Math.Floor((divicion) * 10) / 10;
                //            }
                //            if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro == (int)Enums.FormaCalculoEvaluacion.Promedio)
                //            {
                //                valorInicial = parametroIndividual.ValorEscala ?? 0;
                //                total = g2.Count();
                //                divicion = valorInicial / total;
                //                notaParametro += Math.Floor((divicion) * 10) / 10;
                //                //notaParametro += Convert.ToDecimal(
                //                //    ((double)(parametroIndividual.ValorEscala ?? 0)) *
                //                //    (parametroIndividual.Ponderacion_Parametro * 1.0) / 100.0
                //                //);
                //            }
                //        }
                //        notaCriterio += notaParametro;
                //    }
                //    detalleCriterio.Valor += notaCriterio;
                //}
                foreach (var grupoCriterioInstanciado in grupoCriterio.GroupBy(g =>
                    g.IdEsquemaEvaluacionPGeneralDetalle))
                {
                    decimal notaCriterio = 0;

                    foreach (var grupoParametro in grupoCriterioInstanciado.GroupBy(g => g.IdParametroEvaluacion))
                    {
                        decimal notaParametro = 0;
                        foreach (var parametroIndividual in grupoParametro)
                        {
                            decimal valorInicial = 0;
                            var total = 0;
                            decimal divicion = 0;
                            if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro == (int)Enums.FormaCalculoEvaluacion.Suma)
                            {
                                valorInicial = parametroIndividual.ValorEscala ?? 0;
                                total = g2.Count();
                                divicion = valorInicial / total;
                                notaParametro += Math.Floor((divicion) * 10) / 10;
                            }
                            if (parametroIndividual.IdFormaCalculoEvaluacion_Parametro ==
                                (int)Enums.FormaCalculoEvaluacion.Promedio)
                            {
                                valorInicial = parametroIndividual.ValorEscala ?? 0;
                                total = g2.Count();
                                divicion = valorInicial / total;
                                notaParametro += Math.Floor((divicion) * 10) / 10;
                            }
                            //notaParametro += Convert.ToDecimal(
                            //    ((double)(parametroIndividual.ValorEscala ?? 0)) *
                            //    (parametroIndividual.Ponderacion_Parametro * 1.0) / 100.0
                            //);
                        }
                        notaCriterio += notaParametro;
                    }
                    detalleCriterio.Valor += notaCriterio;
                }
                //valida si el criterio es promedio

                if (grupoCriterio.FirstOrDefault().IdFormaCalculoEvaluacion_Criterio ==
                    (int)Enums.FormaCalculoEvaluacion.Promedio)
                    detalleCriterio.Valor = detalleCriterio.Valor /
                                            ((decimal)(grupoCriterio
                                                .Select(s => s.IdEsquemaEvaluacionPGeneralDetalle).Distinct()
                                                .Count() * 1.0));

                if (detalleCriterio.Valor <= 10)
                {
                    detalleCriterio.Valor *= 10;
                }
                if (Tareas == 0)
                {
                    if ((detalleCriterio.CriterioEvaluacion.Contains("Calificación de Trabajos de Pares") || detalleCriterio.CriterioEvaluacion.Contains("Ejercicios de Aprendizaje Reflexivo")))
                    {
                        agregarPonderacionTareas += detalleCriterio.Ponderacion;
                    }
                    else
                    {
                        notaCurso.DetalleCalificacion.Add(detalleCriterio);
                    }
                }
                else
                {
                    if (detalleCriterio.CriterioEvaluacion.Contains("Ejercicios de Aprendizaje Reflexivo"))
                    {
                        detalleCriterio.Valor = 100 * PreguntasReflexivas / Tareas;
                    }
                    if (detalleCriterio.CriterioEvaluacion.Contains("Calificación de Trabajos de Pares"))
                    {
                        detalleCriterio.Valor = 100 * TareasCalificadas / Tareas;
                    }
                    notaCurso.DetalleCalificacion.Add(detalleCriterio);
                }

            }

            if (notaCurso.DetalleCalificacion != null && notaCurso.DetalleCalificacion.Count > 0)
            {
                notaCurso.NotaCurso = 0;
                foreach (var calificacion in notaCurso.DetalleCalificacion)
                {
                    if (calificacion.CriterioEvaluacion.Contains("Tareas"))
                    {
                        calificacion.Ponderacion += agregarPonderacionTareas;
                    }
                    notaCurso.NotaCurso += calificacion.Valor * (calificacion.Ponderacion * 1);
                }
                notaCurso.NotaCurso = notaCurso.NotaCurso / 100;
                if (listadoEvaluaciones.FirstOrDefault().IdFormaCalculoEvaluacion_Esquema == (int)Enums.FormaCalculoEvaluacion.Promedio)
                    Excepcion.ExcepcionGenerada = false;
                Excepcion.DescripcionGeneral = "Correcto";
            }
            notaCurso.Excepcion = Excepcion;
            return notaCurso;
        }
        /// Autor: Max Mantilla
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene información de los esquemas de evaluación para combo
        /// </summary>
        /// <returns> List<EsquemaEvaluacionComboDTO> </returns>  
        public List<EsquemaEvaluacionComboDTO> ObtenerComboEsquemaEvaluacion()
        {
            try
            {
                return _unitOfWork.EsquemaEvaluacionRepository.ObtenerComboEsquemaEvaluacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene información de los criterios de evaluación por IdEsquemaEvaluacion
        /// </summary>
        /// <returns> List<EsquemaCriterioEvaluacionDTO> </returns>  
        public List<EsquemaCriterioEvaluacionDTO> ObtenerCriterioEvaluacionPorIdEsquema(int IdEsquemaEvaluacion)
        {
            try
            {
                return _unitOfWork.EsquemaEvaluacionRepository.ObtenerCriterioEvaluacionPorIdEsquema(IdEsquemaEvaluacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 09/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                return await _unitOfWork.EsquemaEvaluacionRepository.ObtenerComboAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene esquema asociado
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista DTO - EsquemaAsociadoRespuestaDTO - listadoFinal </returns>
        public List<EsquemaEvaluacionPgeneralAsociadoDTO> ObtenerEsquemaAsociado(int idPGeneral)
        {
            try
            {
                var listado = _unitOfWork.EsquemaEvaluacionPgeneralRepository.ObtenerPorIdPGeneral(idPGeneral).ToList();
                var respuesta = new List<EsquemaEvaluacionPgeneralAsociadoDTO>();

                foreach (var item in listado)
                {
                    var dto = new EsquemaEvaluacionPgeneralAsociadoDTO();
                    dto.Id = item.Id;
                    dto.IdEsquemaEvaluacion = item.IdEsquemaEvaluacion;
                    dto.IdPgeneral = item.IdPgeneral;
                    dto.FechaInicio = item.FechaInicio;
                    dto.FechaFin = item.FechaFin;
                    dto.EsquemaPredeterminado = item.EsquemaPredeterminado;
                    dto.Esquema = _unitOfWork.EsquemaEvaluacionRepository.ObtenerPorId(item.IdEsquemaEvaluacion)!.Nombre;
                    dto.ListadoModalidad = _unitOfWork.EsquemaEvaluacionPgeneralModalidadRepository.ObtenerPorIdEsquemaEvaluacionPGeneral(item.Id).Select(x => x.IdModalidadCurso).ToList();

                    var modalidad = _unitOfWork.EsquemaEvaluacionPgeneralModalidadRepository.ObtenerPorIdEsquemaEvaluacionPGeneral(item.Id).Select(x => x.IdModalidadCurso).ToList();
                    var nombreModalidad = _unitOfWork.ModalidadCursoRepository.ObtenerPorIds(modalidad).Select(x => x.Nombre).ToList();

                    dto.ModalidadMostrar = string.Join(",", nombreModalidad);
                    dto.ListadoProveedor = _unitOfWork.EsquemaEvaluacionPgeneralProveedorRepository.ObtenerPorIdEsquemaEvaluacionPGeneral(item.Id).Select(x => x.IdProveedor).ToList();
                    respuesta.Add(dto);
                }
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica o inserta un nuevo esquemas asi como sus detalles ,proveedores y  modalidades 
        /// </summary>
        /// <param name=”esquemaDTO”>DTO del esquema de evaluacion</param>
        /// <param name=”usuario”></param>
        /// <returns>bool<returns>
        public bool ActualizarAsignacion(EsquemaEvaluacionRegistrarAsignacionDTO esquemaDTO, string usuario)
        {
            try
            {
                if (!_unitOfWork.EsquemaEvaluacionPgeneralRepository.Exist(esquemaDTO.Id))
                {
                    throw new BadRequestException("Entidad no existente");
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    var esquema = _unitOfWork.EsquemaEvaluacionPgeneralRepository.ObtenerPorId(esquemaDTO.Id)!;
                    var esquemaPredeterminadoExistente = esquema.EsquemaPredeterminado;
                    esquema.IdEsquemaEvaluacion = esquemaDTO.IdEsquemaEvaluacion;
                    esquema.FechaInicio = esquemaDTO.FechaInicio;
                    esquema.FechaFin = esquemaDTO.FechaFin;
                    esquema.EsquemaPredeterminado = esquemaDTO.EsquemaPredeterminado;
                    esquema.FechaCreacion = DateTime.Now;
                    esquema.UsuarioModificacion = usuario;

                    var listaModalidadesBorrar = _unitOfWork.EsquemaEvaluacionPgeneralModalidadRepository.GetBy(x => x.IdEsquemaEvaluacionPgeneral == esquema.Id).ToList();
                    listaModalidadesBorrar.RemoveAll(x => esquemaDTO.IdModalidad.Any(s => s == x.IdModalidadCurso));
                    if (listaModalidadesBorrar != null && listaModalidadesBorrar.Count() > 0)
                    {
                        _unitOfWork.EsquemaEvaluacionPgeneralModalidadRepository.Delete(listaModalidadesBorrar.Select(s => s.Id), usuario);
                        _unitOfWork.Commit();
                    }
                    esquema.EsquemaEvaluacionPgeneralModalidads = new();
                    esquemaDTO.IdModalidad.ForEach(idModalidad =>
                    {
                        if (!_unitOfWork.EsquemaEvaluacionPgeneralModalidadRepository.Exist(s => s.IdEsquemaEvaluacionPgeneral == esquema.Id && s.IdModalidadCurso == idModalidad))
                        {
                            esquema.EsquemaEvaluacionPgeneralModalidads.Add(new EsquemaEvaluacionPgeneralModalidad()
                            {
                                IdModalidadCurso = idModalidad,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            });
                        }
                    });

                    var listaProveedoresBorrar = _unitOfWork.EsquemaEvaluacionPgeneralProveedorRepository.GetBy(x => x.IdEsquemaEvaluacionPgeneral == esquema.Id).ToList();
                    listaProveedoresBorrar.RemoveAll(x => esquemaDTO.IdProveedor.Any(s => s == x.IdProveedor));
                    if (listaProveedoresBorrar != null && listaProveedoresBorrar.Count() > 0)
                    {
                        _unitOfWork.EsquemaEvaluacionPgeneralProveedorRepository.Delete(listaProveedoresBorrar.Select(s => s.Id), usuario);
                        _unitOfWork.Commit();
                    }

                    esquema.EsquemaEvaluacionPgeneralProveedors = new();
                    esquemaDTO.IdProveedor.ForEach(idProvedor =>
                    {
                        if (!_unitOfWork.EsquemaEvaluacionPgeneralProveedorRepository.Exist(s => s.IdEsquemaEvaluacionPgeneral == esquema.Id && s.IdProveedor == idProvedor))
                        {
                            esquema.EsquemaEvaluacionPgeneralProveedors.Add(new EsquemaEvaluacionPgeneralProveedor()
                            {
                                IdProveedor = idProvedor,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            });
                        }
                    });

                    var listaDetallesBorrar = _unitOfWork.EsquemaEvaluacionPgeneralDetalleRepository.GetBy(x => x.IdEsquemaEvaluacionPgeneral == esquema.Id).ToList();
                    listaDetallesBorrar.RemoveAll(x => esquemaDTO.ListadoDetalleAsignacion.Any(s => s.Id == x.Id));
                    if (listaDetallesBorrar != null && listaDetallesBorrar.Count() > 0)
                    {
                        _unitOfWork.EsquemaEvaluacionPgeneralDetalleRepository.Delete(listaDetallesBorrar.Select(s => s.Id), usuario);
                        _unitOfWork.Commit();
                    }

                    esquema.EsquemaEvaluacionPgeneralDetalles = new();
                    esquemaDTO.ListadoDetalleAsignacion.ForEach(item =>
                    {
                        if (item.Id != 0 && _unitOfWork.EsquemaEvaluacionPgeneralDetalleRepository
                            .Exist(s => s.IdEsquemaEvaluacionPgeneral == esquema.Id && s.Id == item.Id))
                        {
                            var detalle = _unitOfWork.EsquemaEvaluacionPgeneralDetalleRepository.ObtenerPorId(item.Id)!;
                            detalle.IdCriterioEvaluacion = item.IdCriterioEvaluacion;
                            detalle.IdProveedor = item.IdProveedor;
                            detalle.Nombre = item.Nombre;
                            detalle.UrlArchivoInstrucciones = item.UrlArchivoInstrucciones;
                            detalle.UsuarioModificacion = usuario;
                            detalle.FechaModificacion = DateTime.Now;
                            esquema.EsquemaEvaluacionPgeneralDetalles.Add(detalle);
                        }
                        else
                        {
                            var detalle = new EsquemaEvaluacionPgeneralDetalle()
                            {
                                IdCriterioEvaluacion = item.IdCriterioEvaluacion,
                                IdProveedor = item.IdProveedor,
                                Nombre = item.Nombre,
                                UrlArchivoInstrucciones = item.UrlArchivoInstrucciones,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            esquema.EsquemaEvaluacionPgeneralDetalles.Add(detalle);
                        }
                    });

                    _unitOfWork.EsquemaEvaluacionPgeneralRepository.Update(esquema);
                    _unitOfWork.Commit();

                    if (esquemaPredeterminadoExistente.GetValueOrDefault() != esquemaDTO.EsquemaPredeterminado)
                    {
                        _unitOfWork.EsquemaEvaluacionRepository.ModificarEsquemaEvaluacionPredefinido(esquemaDTO.Id);
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Modifica o inserta un nuevo esquemas asi como sus detalles ,proveedores y  modalidades 
        /// </summary>
        /// <param name=”esquemaDTO”>DTO del esquema de evaluacion</param>
        /// <param name=”usuario”></param>
        /// <returns>bool<returns>
        public bool RegistrarAsignacion(EsquemaEvaluacionRegistrarAsignacionDTO esquemaDTO, string usuario)
        {
            try
            {
                EsquemaEvaluacionPgeneral esquemaEvaluacion = new EsquemaEvaluacionPgeneral()
                {
                    IdPgeneral = esquemaDTO.IdPGeneral,
                    IdEsquemaEvaluacion = esquemaDTO.IdEsquemaEvaluacion,
                    FechaInicio = esquemaDTO.FechaInicio,
                    FechaFin = esquemaDTO.FechaFin,
                    EsquemaPredeterminado = esquemaDTO.EsquemaPredeterminado,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                //añade la lista de detalles
                if (esquemaDTO.ListadoDetalleAsignacion != null && esquemaDTO.ListadoDetalleAsignacion.Count > 0)
                {
                    esquemaEvaluacion.EsquemaEvaluacionPgeneralDetalles = esquemaDTO.ListadoDetalleAsignacion.Select(s =>
                        new EsquemaEvaluacionPgeneralDetalle()
                        {
                            IdCriterioEvaluacion = s.IdCriterioEvaluacion,
                            IdProveedor = s.IdProveedor,
                            Nombre = s.Nombre,
                            UrlArchivoInstrucciones = s.UrlArchivoInstrucciones,

                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }).ToList();
                }
                //añade la lista de modalidad
                if (esquemaDTO.IdModalidad != null && esquemaDTO.IdModalidad.Count > 0)
                {
                    esquemaEvaluacion.EsquemaEvaluacionPgeneralModalidads = esquemaDTO.IdModalidad.Select(s =>
                        new EsquemaEvaluacionPgeneralModalidad()
                        {
                            IdModalidadCurso = s,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }).ToList();
                }
                //añade la lista de proveedor
                if (esquemaDTO.IdProveedor != null && esquemaDTO.IdProveedor.Count > 0)
                {
                    esquemaEvaluacion.EsquemaEvaluacionPgeneralProveedors = esquemaDTO.IdProveedor.Select(s =>
                        new EsquemaEvaluacionPgeneralProveedor()
                        {
                            IdProveedor = s,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }).ToList();
                }
                _unitOfWork.EsquemaEvaluacionPgeneralRepository.Add(esquemaEvaluacion);
                _unitOfWork.Commit();

                if (esquemaDTO.EsquemaPredeterminado == true)
                {
                    _unitOfWork.EsquemaEvaluacionRepository.ModificarEsquemaEvaluacionPredefinido(esquemaDTO.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name=”idEsquemaAsignado”>DTO del esquema de evaluacion</param>
        /// <returns>bool<returns>
        public IEnumerable<DetalleEsquemaAsignadoDTO> ObtenerDetalleEsquemaAsignado(int idEsquemaAsignado)
        {
            try
            {
                var listado = _unitOfWork.EsquemaEvaluacionPgeneralDetalleRepository.GetBy(w => w.IdEsquemaEvaluacionPgeneral == idEsquemaAsignado,
                    esquemaDetallePgeneral => new EsquemaEvaluacionPgeneralDetalleCompuestoDTO
                    {
                        Id = esquemaDetallePgeneral.Id,
                        IdCriterioEvaluacion = esquemaDetallePgeneral.IdCriterioEvaluacion,
                        IdProveedor = esquemaDetallePgeneral.IdProveedor,
                        NombreCriterioEvaluacion = esquemaDetallePgeneral.IdCriterioEvaluacionNavigation.Nombre,
                        Nombre = esquemaDetallePgeneral.Nombre,
                        UrlArchivoInstrucciones = esquemaDetallePgeneral.UrlArchivoInstrucciones
                    });

                var resultado = listado.GroupBy(g => g.IdCriterioEvaluacion)
                    .Select(s => new DetalleEsquemaAsignadoDTO { IdCriterioEvaluacion = s.Key, Detalle = s.ToList() });
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name=”idEsquemaEvaluacion”></param>
        /// <returns>bool<returns>
        public IEnumerable<EsquemaEvaluacionDetalleCompuestoDTO> ObtenerDetalleEsquema(int idEsquemaEvaluacion)
        {
            try
            {
                var resultado = _unitOfWork.EsquemaEvaluacionDetalleRepository.GetBy(w => w.IdEsquemaEvaluacion == idEsquemaEvaluacion,
                    s => new EsquemaEvaluacionDetalleCompuestoDTO
                    {
                        Id = s.Id,
                        IdEsquemaEvaluacion = s.IdEsquemaEvaluacion,
                        IdCriterioEvaluacion = s.IdCriterioEvaluacion,
                        NombreCriterioEvaluacion = s.IdCriterioEvaluacionNavigation.Nombre,
                        Ponderacion = s.Ponderacion
                    });
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// </summary>
        /// <param name=”archivos”></param>
        /// <returns>bool<returns>
        public string SubirArchivo(IList<IFormFile> archivos)
        {
            try
            {
                string url = "";
                foreach (var archivo in archivos)
                {
                    var nombreArchivo = string.Concat(DateTime.Now.ToString("yyyyMMddHHmmss"), '-', archivo.FileName);
                    url = SubirArchivoBlobStorage(archivo.ConvertToByte(), archivo.ContentType, nombreArchivo);
                }
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string SubirArchivoBlobStorage(byte[] archivo, string mimeType, string nombreArchivo)
        {
            try
            {
                string _nombreLink = string.Empty;
                try
                {
                    string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";

                    string _direccionBlob = @"planificacion/esquemaevaluacion/";

                    //Generar entrada al blob storage
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_direccionBlob);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombreArchivo);
                    blockBlob.Properties.ContentType = mimeType;
                    blockBlob.Metadata["filename"] = nombreArchivo;
                    blockBlob.Metadata["filemime"] = mimeType;
                    Stream stream = new MemoryStream(archivo);
                    //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                    var objRegistrado = blockBlob.UploadFromStreamAsync(stream);

                    objRegistrado.Wait();
                    var correcto = objRegistrado.IsCompletedSuccessfully;

                    if (correcto)
                    {
                        _nombreLink = "https://repositorioweb.blob.core.windows.net/" + _direccionBlob + nombreArchivo.Replace(" ", "%20");
                    }
                    else
                    {
                        _nombreLink = "";
                    }
                    return _nombreLink;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo EsquemaEvaluacion
        /// </summary>
        /// <param name="dto">EsquemaEvaluacion</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>EsquemaEvaluacionDTO</returns>
        public EsquemaEvaluacionDTO Insertar(EsquemaEvaluacionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    EsquemaEvaluacion entidad = new()
                    {
                        Nombre = dto.Nombre,
                        IdFormaCalculoEvaluacion = dto.IdFormaCalculoEvaluacion,
                        IdModalidadCurso = dto.IdModalidadCurso,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                   
                    //añade la lista de detalles
                    var variable = new List<EsquemaEvaluacionDetalleDTO>();
                    if (dto.EsquemaEvaluacionDetalles != null && dto.EsquemaEvaluacionDetalles.Count() > 0)
                    {
                        entidad.EsquemaEvaluacionDetalles = new List<EsquemaEvaluacionDetalle>();
                        var esquemaEvaluacionDetalles = dto.EsquemaEvaluacionDetalles.Select(x => new EsquemaEvaluacionDetalle
                        {
                            IdCriterioEvaluacion = x.IdCriterioEvaluacion,
                            Ponderacion = x.Ponderacion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now

                        });
                        entidad.EsquemaEvaluacionDetalles = esquemaEvaluacionDetalles.ToList();
                    }

                    var respuesta = _unitOfWork.EsquemaEvaluacionRepository.Add(entidad);
                    _unitOfWork.Commit();
                    var resultado = _mapper.Map<EsquemaEvaluacionDTO>(respuesta);
                    resultado.EsquemaEvaluacionDetalles = variable;
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un EsquemaEvaluacion
        /// </summary>
        /// <param name="dto">EsquemaEvaluacion</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>EsquemaEvaluacionDTO</returns>

        public EsquemaEvaluacionDTO Actualizar(EsquemaEvaluacionDTO dto, string usuario)
        {
            try
            {
                EsquemaEvaluacion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.EsquemaEvaluacionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdFormaCalculoEvaluacion = dto.IdFormaCalculoEvaluacion;
                            entidad.IdModalidadCurso = dto.IdModalidadCurso;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.EsquemaEvaluacionRepository.Update(entidad);
                            _unitOfWork.Commit();

                            var listaDetalle = _unitOfWork.EsquemaEvaluacionDetalleRepository.ObtenerPorIdEsquemaEvaluacion(entidad.Id).ToList();
                            if (listaDetalle != null && listaDetalle.Count() > 0)
                            {
                                if (dto.EsquemaEvaluacionDetalles != null && dto.EsquemaEvaluacionDetalles.Count() > 0)
                                {
                                    listaDetalle.RemoveAll(s => dto.EsquemaEvaluacionDetalles.Any(x => x.Id == s.Id));
                                }
                                if (listaDetalle.Count() > 0)
                                {
                                    _unitOfWork.EsquemaEvaluacionDetalleRepository.Delete(listaDetalle.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                            }
                            if (dto.EsquemaEvaluacionDetalles != null && dto.EsquemaEvaluacionDetalles.Count() > 0)
                            {
                                dto.EsquemaEvaluacionDetalles.ForEach(esquemaEvalDetalle =>
                                {
                                    EsquemaEvaluacionDetalle esquemaEvaluacionDetalle;
                                    if (esquemaEvalDetalle.Id != 0 && _unitOfWork.EsquemaEvaluacionDetalleRepository.Exist(esquemaEvalDetalle.Id))
                                    {
                                        esquemaEvaluacionDetalle = _unitOfWork.EsquemaEvaluacionDetalleRepository.ObtenerPorId(esquemaEvalDetalle.Id)!;

                                        esquemaEvaluacionDetalle.IdCriterioEvaluacion = esquemaEvalDetalle.IdCriterioEvaluacion;
                                        esquemaEvaluacionDetalle.Ponderacion = esquemaEvalDetalle.Ponderacion;

                                        esquemaEvaluacionDetalle.UsuarioModificacion = usuario;
                                        esquemaEvaluacionDetalle.FechaModificacion = DateTime.Now;
                                        _unitOfWork.EsquemaEvaluacionDetalleRepository.Update(esquemaEvaluacionDetalle);
                                        _unitOfWork.Commit();
                                    }
                                    else
                                    {
                                        esquemaEvaluacionDetalle = new EsquemaEvaluacionDetalle()
                                        {
                                            IdEsquemaEvaluacion = entidad.Id,
                                            Ponderacion = esquemaEvalDetalle.Ponderacion,
                                            IdCriterioEvaluacion = esquemaEvalDetalle.IdCriterioEvaluacion,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };
                                        var resultado = _unitOfWork.EsquemaEvaluacionDetalleRepository.Add(esquemaEvaluacionDetalle);
                                        _unitOfWork.Commit();
                                        esquemaEvalDetalle.Id = resultado.Id;
                                    }
                                });
                            }
                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de nFormaCalculoEvaluacio
        /// </summary>
        /// <returns> Lista ComboDTO </returns>
        public IEnumerable<ComboDTO> ObtenerComboFormaCalculoEvaluacion()
        {
            return _unitOfWork.FormaCalculoEvaluacionRepository.ObtenerCombo();
        }


        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina el registro EsquemaEvaluacion por id
        /// </summary>
        /// <param name="id">Id EsquemaEvaluacion</param>
        /// <returns> true/false </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.EsquemaEvaluacionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.EsquemaEvaluacionRepository.Delete(id, usuario);
                    var idsEsquemaEvalluacionDetalle = _unitOfWork.EsquemaEvaluacionDetalleRepository.ObtenerPorIdEsquemaEvaluacion(id).Select(x => x.Id);

                    if (idsEsquemaEvalluacionDetalle != null && idsEsquemaEvalluacionDetalle.Count() > 0)
                    {
                        _unitOfWork.EsquemaEvaluacionDetalleRepository.Delete(idsEsquemaEvalluacionDetalle, usuario);
                    }

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
