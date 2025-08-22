using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper;
using CsvHelper.Configuration;
using Google.Api.Ads.Common.Util;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using System.Transactions;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: MaestroPreguntaProgramaCapacitacionService
    /// Autor: Gilmer Qm.
    /// Fecha: 21/07/2023
    /// <summary>
    /// Gestión general de  Maestro PreguntaProgramaCapacitacion Programa Capacitacion
    /// </summary>
    public class PreguntaProgramaCapacitacionService : IPreguntaProgramaCapacitacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaProgramaCapacitacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<PreguntaIntentoDetalleDTO, PreguntaIntentoDetalle>(MemberList.None).ReverseMap();
                    cfg.CreateMap<RespuestaPreguntaProgramaCapacitacionDTO, RespuestaPreguntaProgramaCapacitacion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<PreguntaIntento, TPreguntaIntento>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21-07-23
        /// Versión: 1
        /// <summary>
        /// Obtiene el reporte de preguntas interactivas para su exportación en excel
        /// </summary>
        /// <returns>Lista de objetos (PreguntaProgramaCapacitacionRegistradaDTO) con respuesta 200 o 400 con el mensaje de error</returns>
        public async Task<List<ReporteExcelPreguntasInteractivasDTO>> ObtenerReportePreguntasInteractivasExportacionExcel()
        {
            var ReporteInicial = await _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerReportePreguntasInteractivasExportacionExcel();
            var Sesiones = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSesionesPreguntasInteractivasExportacionExcel();
            var SubSesiones = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSubSesionesPreguntasInteractivasExportacionExcel();
            var IntentoDetalle = await _unitOfWork.PreguntaIntentoDetalleRepository.ObtenerListadoPreguntaIntentoDetallado();
            var registroCompleto = (from x in ReporteInicial
                                    join y in IntentoDetalle on x.IdPreguntaIntento equals y.IdPreguntaIntento
                                    join z in Sesiones on x.IdPGeneral equals z.IdPGeneral
                                    join w in SubSesiones on x.IdPGeneral equals w.IdPGeneral
                                    where x.NroOrden == y.Orden && x.OrdenFilaSesion == z.NumeroFila && x.OrdenFilaSesion == w.NumeroFila
                                    select new ReporteExcelPreguntasInteractivasDTO
                                    {
                                        Id = x.Id,
                                        IdPGeneral = x.IdPGeneral,
                                        IdPEspecifico = x.IdPEspecifico,
                                        OrdenFilaCapitulo = x.OrdenFilaCapitulo,
                                        Sesion = z.Contenido,
                                        SubSesion = w.Contenido,
                                        GrupoPregunta = x.GrupoPregunta,
                                        IdTipoMarcador = x.IdTipoMarcador,
                                        ValorMarcador = x.ValorMarcador,
                                        OrdenPreguntaGrupo = x.OrdenPreguntaGrupo,
                                        IdTipoRespuesta = x.IdTipoRespuesta,
                                        IdPreguntaTipo = (Int32)x.IdPreguntaTipo,
                                        EnunciadoPregunta = x.EnunciadoPregunta,
                                        MinutosPorPregunta = x.MinutosPorPregunta,
                                        RespuestaAleatoria = x.RespuestaAleatoria,
                                        ActivarFeedBackRespuestaCorrecta = x.ActivarFeedBackRespuestaCorrecta,
                                        ActivarFeedBackRespuestaIncorrecta = x.ActivarFeedBackRespuestaIncorrecta,
                                        MostrarFeedbackInmediato = x.MostrarFeedbackInmediato,
                                        MostrarFeedbackPorPregunta = x.MostrarFeedbackPorPregunta,
                                        NumeroMaximoIntento = x.NumeroMaximoIntento,
                                        ActivarFeedbackMaximoIntento = x.ActivarFeedbackMaximoIntento,
                                        MensajeFeedback = x.MensajeFeedback,
                                        IdTipoRespuestaCalificacion = x.IdTipoRespuestaCalificacion,
                                        FactorRespuesta = x.FactorRespuesta,
                                        RespuestaCorrecta = x.RespuestaCorrecta,
                                        NroOrden = x.NroOrden,
                                        EnunciadoRespuesta = x.EnunciadoRespuesta,
                                        Puntaje = x.Puntaje,
                                        FeedbackPositivo = x.FeedbackPositivo,
                                        FeedbackNegativo = x.FeedbackNegativo,
                                        PorcentajeCalificacion = y.PorcentajeCalificacion

                                    }).ToList();
            return (registroCompleto);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de una preguntaProgramaCapacitacion segun un id especifico
        /// </summary>
        /// <param name="IdPreguntaProgramaCapacitacion">Id de la preguntaProgramaCapacitacion de programa capacitacion(PK de la tabla ope.T_PreguntaProgramaCapacitacion)</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        public List<RespuestaPreguntaProgramaCapacitacionDTO> ObtenerRespuestasPregunta(int IdPreguntaProgramaCapacitacion)
        {
            return _mapper.Map<List<RespuestaPreguntaProgramaCapacitacionDTO>>(_unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.ObtenerPorIdPreguntaProgramaCapacitacion(IdPreguntaProgramaCapacitacion).ToList());
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de una preguntaProgramaCapacitacion segun un id especifico
        /// </summary>
        /// <param name="idPreguntaIntento">Id de la preguntaProgramaCapacitacion de programa capacitacion(PK de la tabla T_PReguntaIntento )</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        public List<PreguntaIntentoDetalleDTO> ObtenerPorIdPreguntaIntento(int idPreguntaIntento)
        {
            return _mapper.Map<List<PreguntaIntentoDetalleDTO>>(_unitOfWork.PreguntaIntentoDetalleRepository.ObtenerPorIdPreguntaIntento(idPreguntaIntento).ToList());
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de una preguntaProgramaCapacitacion segun un id especifico
        /// </summary>
        /// <param name="id">Id de la preguntaProgramaCapacitacion de programa capacitacion(PK de la tabla T_PReguntaIntento )</param>
        /// <param name="usuario"> Usuario integra </param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                bool respuesta = false;
                using (TransactionScope scope = new TransactionScope())
                {
                    var preguntaProgramaCapacitacionDTO = _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPorId(id);
                    if (preguntaProgramaCapacitacionDTO != null)
                    {
                        var respuestaPreguntaProgramaCapacitacions = _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.ObtenerPorIdPreguntaProgramaCapacitacion(preguntaProgramaCapacitacionDTO.Id);

                        if (preguntaProgramaCapacitacionDTO.IdPreguntaIntento.HasValue)
                        {
                            var preguntaIntentoDetalles = _unitOfWork.PreguntaIntentoDetalleRepository.ObtenerPorIdPreguntaIntento(preguntaProgramaCapacitacionDTO.IdPreguntaIntento.Value);
                            foreach (var item in preguntaIntentoDetalles)
                            {
                                _unitOfWork.PreguntaIntentoDetalleRepository.Delete(item.Id, usuario);
                            }
                            _unitOfWork.PreguntaIntentoRepository.Delete(preguntaProgramaCapacitacionDTO.IdPreguntaIntento.Value, usuario);
                        }

                        foreach (var item in respuestaPreguntaProgramaCapacitacions)
                        {
                            _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.Delete(item.Id, usuario);
                        }
                        respuesta = _unitOfWork.PreguntaProgramaCapacitacionRepository.Delete(preguntaProgramaCapacitacionDTO.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    scope.Complete();
                }
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de una preguntaProgramaCapacitacion segun un id especifico
        /// </summary>
        /// <param name="compuestoPreguntaProgramaCapacitacionDTO"> Objeto y detalles </param>
        /// <param name="usuario"> Usuario integra </param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        public bool Actualizar(CompuestoPreguntaProgramaCapacitacionDTO compuestoPreguntaProgramaCapacitacionDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PreguntaIntento preguntaIntento;
                    var preguntaProgramaCapacitacion = _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPorId(compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.Id.Value);
                    if (preguntaProgramaCapacitacion.IdPreguntaIntento.HasValue)
                    {
                        preguntaIntento = _unitOfWork.PreguntaIntentoRepository.ObtenerPorId(preguntaProgramaCapacitacion.IdPreguntaIntento.Value);
                        preguntaIntento.NumeroMaximoIntento = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.NumeroMaximoIntento;
                        preguntaIntento.ActivarFeedbackMaximoIntento = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.ActivarFeedbackMaximoIntento;
                        preguntaIntento.MensajeFeedback = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.MensajeFeedback;
                        preguntaIntento.UsuarioModificacion = usuario;
                        preguntaIntento.FechaModificacion = DateTime.Now;

                        var listaPreguntaIntento = _unitOfWork.PreguntaIntentoDetalleRepository.ObtenerPorIdPreguntaIntento(preguntaIntento.Id);
                        foreach (var item in listaPreguntaIntento)
                        {
                            if (!compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.PreguntaIntentoDetalles.Any(x => x.Id == item.Id))
                                _unitOfWork.PreguntaIntentoDetalleRepository.Delete(item.Id, usuario);
                        }
                        preguntaIntento.PreguntaIntentoDetalles = new List<PreguntaIntentoDetalle>();
                        foreach (var item in compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.PreguntaIntentoDetalles)
                        {
                            PreguntaIntentoDetalle preguntaIntentoDetalle;
                            if (item.Id > 0)
                            {
                                preguntaIntentoDetalle = _unitOfWork.PreguntaIntentoDetalleRepository.ObtenerPorId(item.Id.Value);
                                preguntaIntentoDetalle.PorcentajeCalificacion = item.PorcentajeCalificacion;
                                preguntaIntentoDetalle.UsuarioModificacion = usuario;
                                preguntaIntentoDetalle.FechaModificacion = DateTime.Now;
                                preguntaIntento.PreguntaIntentoDetalles.Add(preguntaIntentoDetalle);
                            }
                            else
                            {
                                preguntaIntentoDetalle = new PreguntaIntentoDetalle
                                {
                                    PorcentajeCalificacion = item.PorcentajeCalificacion,
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                preguntaIntento.PreguntaIntentoDetalles.Add(preguntaIntentoDetalle);
                            }
                        }
                        _unitOfWork.PreguntaIntentoRepository.Update(preguntaIntento);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        preguntaIntento = new PreguntaIntento()
                        {
                            NumeroMaximoIntento = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.NumeroMaximoIntento,
                            ActivarFeedbackMaximoIntento = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.ActivarFeedbackMaximoIntento,
                            MensajeFeedback = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.MensajeFeedback,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        preguntaIntento.PreguntaIntentoDetalles = new List<PreguntaIntentoDetalle>();
                        foreach (var item in compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.PreguntaIntentoDetalles)
                        {
                            PreguntaIntentoDetalle preguntaIntentoDetalle = new PreguntaIntentoDetalle
                            {
                                IdPreguntaIntento = preguntaIntento.Id,
                                PorcentajeCalificacion = item.PorcentajeCalificacion,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            preguntaIntento.PreguntaIntentoDetalles.Add(preguntaIntentoDetalle);
                        }
                        preguntaIntento = _mapper.Map<PreguntaIntento>(_unitOfWork.PreguntaIntentoRepository.Add(preguntaIntento));
                        _unitOfWork.Commit();

                        preguntaProgramaCapacitacion.IdPreguntaIntento = preguntaIntento.Id;
                    }
                    _unitOfWork.DetachAll();
                    preguntaProgramaCapacitacion.IdTipoRespuesta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuesta;
                    preguntaProgramaCapacitacion.EnunciadoPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.EnunciadoPregunta;
                    preguntaProgramaCapacitacion.MinutosPorPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.MinutosPorPregunta;
                    preguntaProgramaCapacitacion.RespuestaAleatoria = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.RespuestaAleatoria;
                    preguntaProgramaCapacitacion.ActivarFeedBackRespuestaCorrecta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaCorrecta;
                    preguntaProgramaCapacitacion.ActivarFeedBackRespuestaIncorrecta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaIncorrecta;
                    preguntaProgramaCapacitacion.MostrarFeedbackInmediato = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.MostrarFeedbackInmediato;
                    preguntaProgramaCapacitacion.MostrarFeedbackPorPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.MostrarFeedbackPorPregunta;
                    preguntaProgramaCapacitacion.UsuarioModificacion = usuario;
                    preguntaProgramaCapacitacion.FechaModificacion = DateTime.Now;
                    preguntaProgramaCapacitacion.IdTipoRespuestaCalificacion = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion;
                    preguntaProgramaCapacitacion.FactorRespuesta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta;
                    preguntaProgramaCapacitacion.IdPreguntaTipo = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdPreguntaTipo;
                    preguntaProgramaCapacitacion.IdPgeneral = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdPgeneral;
                    preguntaProgramaCapacitacion.IdPespecifico = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdPespecifico;
                    preguntaProgramaCapacitacion.OrdenFilaCapitulo = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.OrdenFilaCapitulo;
                    preguntaProgramaCapacitacion.OrdenFilaSesion = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.OrdenFilaSesion;
                    preguntaProgramaCapacitacion.GrupoPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.GrupoPregunta;
                    preguntaProgramaCapacitacion.IdTipoMarcador = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoMarcador;
                    preguntaProgramaCapacitacion.ValorMarcador = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.ValorMarcador;
                    preguntaProgramaCapacitacion.OrdenPreguntaGrupo = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.OrdenPreguntaGrupo;
                    _unitOfWork.PreguntaProgramaCapacitacionRepository.Update(preguntaProgramaCapacitacion);
                    _unitOfWork.Commit();

                    var rp = _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.ObtenerPorIdPreguntaProgramaCapacitacion(preguntaProgramaCapacitacion.Id);
                    foreach (var item in rp)
                    {
                        if (!compuestoPreguntaProgramaCapacitacionDTO.RespuestaPreguntaProgramaCapacitacions.Any(x => x.Id == item.Id))
                        {
                            _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();
                        }
                    }

                    foreach (var item in compuestoPreguntaProgramaCapacitacionDTO.RespuestaPreguntaProgramaCapacitacions)
                    {
                        int? puntajeTipoRespuesta = null;
                        if (compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.HasValue)
                        {
                            int tipoRes = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.Value;
                            if (tipoRes == 1) //Directo
                            {
                                puntajeTipoRespuesta = item.Puntaje;
                            }
                            else if (tipoRes == 2) //Inversa
                            {
                                if (compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.HasValue)
                                {
                                    int factorRes = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.Value;
                                    puntajeTipoRespuesta = factorRes - item.Puntaje;
                                }
                            }
                            else //negativo
                            {
                                if (compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.HasValue)
                                {
                                    int factorRes = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.Value;
                                    if (!item.RespuestaCorrecta.HasValue)
                                        puntajeTipoRespuesta = item.Puntaje - factorRes;
                                    else
                                        puntajeTipoRespuesta = item.Puntaje;
                                }
                            }
                        }
                        _unitOfWork.DetachAll();
                        RespuestaPreguntaProgramaCapacitacion respuestaPregunta;
                        if (item.Id > 0)
                        {
                            respuestaPregunta = _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.ObtenerPorId(item.Id);
                            respuestaPregunta.RespuestaCorrecta = item.RespuestaCorrecta;
                            respuestaPregunta.NroOrden = item.NroOrden;
                            respuestaPregunta.EnunciadoRespuesta = item.EnunciadoRespuesta;
                            respuestaPregunta.NroOrdenRespuesta = item.NroOrdenRespuesta;
                            respuestaPregunta.Puntaje = item.Puntaje;
                            respuestaPregunta.FeedbackPositivo = item.FeedbackPositivo;
                            respuestaPregunta.FeedbackNegativo = item.FeedbackNegativo;
                            respuestaPregunta.UsuarioModificacion = usuario;
                            respuestaPregunta.FechaModificacion = DateTime.Now;
                            respuestaPregunta.PuntajeTipoRespuesta = puntajeTipoRespuesta;
                            _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.Update(respuestaPregunta);
                        }
                        else
                        {
                            respuestaPregunta = new RespuestaPreguntaProgramaCapacitacion()
                            {
                                IdPreguntaProgramaCapacitacion = preguntaProgramaCapacitacion.Id,
                                RespuestaCorrecta = item.RespuestaCorrecta,
                                NroOrden = item.NroOrden,
                                EnunciadoRespuesta = item.EnunciadoRespuesta,
                                NroOrdenRespuesta = item.NroOrdenRespuesta,
                                Puntaje = item.Puntaje,
                                FeedbackPositivo = item.FeedbackPositivo,
                                FeedbackNegativo = item.FeedbackNegativo,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                PuntajeTipoRespuesta = puntajeTipoRespuesta
                            };
                            _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.Add(respuestaPregunta);
                        }
                        _unitOfWork.Commit();
                    }

                    scope.Complete();
                }
                return (true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 26/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Inserta una pregunta especifica
        /// </summary>
        /// <param name="compuestoPreguntaProgramaCapacitacionDTO">Objeto de tipo CompuestoCrucigramaProgramaCapacitacionDTO</param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        public bool Insertar(CompuestoPreguntaProgramaCapacitacionDTO compuestoPreguntaProgramaCapacitacionDTO, string usuario)
        {
            bool respuesta = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PreguntaIntento preguntaIntento = new PreguntaIntento()
                    {
                        NumeroMaximoIntento = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.NumeroMaximoIntento,
                        ActivarFeedbackMaximoIntento = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.ActivarFeedbackMaximoIntento,
                        MensajeFeedback = compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.MensajeFeedback,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    preguntaIntento.PreguntaIntentoDetalles = new List<PreguntaIntentoDetalle>();


                    foreach (var item in compuestoPreguntaProgramaCapacitacionDTO.PreguntaIntento.PreguntaIntentoDetalles)
                    {
                        PreguntaIntentoDetalle preguntaIntentoDetalle = new PreguntaIntentoDetalle
                        {
                            IdPreguntaIntento = preguntaIntento.Id,
                            PorcentajeCalificacion = item.PorcentajeCalificacion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        preguntaIntento.PreguntaIntentoDetalles.Add(preguntaIntentoDetalle);
                    }
                    preguntaIntento.PreguntaProgramaCapacitacions = new List<PreguntaProgramaCapacitacion>();
                    PreguntaProgramaCapacitacion preguntaProgramaCapacitacion = new PreguntaProgramaCapacitacion()
                    {
                        IdTipoRespuesta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuesta,
                        EnunciadoPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.EnunciadoPregunta,
                        MinutosPorPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.MinutosPorPregunta,
                        RespuestaAleatoria = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.RespuestaAleatoria,
                        ActivarFeedBackRespuestaCorrecta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaCorrecta,
                        ActivarFeedBackRespuestaIncorrecta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaIncorrecta,
                        MostrarFeedbackInmediato = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.MostrarFeedbackInmediato,
                        MostrarFeedbackPorPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.MostrarFeedbackPorPregunta,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        IdPreguntaIntento = preguntaIntento.Id,
                        IdPreguntaTipo = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdPreguntaTipo,
                        IdTipoRespuestaCalificacion = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion,
                        FactorRespuesta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta,
                        IdPgeneral = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdPgeneral,
                        IdPespecifico = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdPespecifico,
                        OrdenFilaCapitulo = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.OrdenFilaCapitulo,
                        OrdenFilaSesion = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.OrdenFilaSesion,
                        GrupoPregunta = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.GrupoPregunta,
                        IdTipoMarcador = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoMarcador,
                        ValorMarcador = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.ValorMarcador,
                        OrdenPreguntaGrupo = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.OrdenPreguntaGrupo
                    };

                    preguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacions = new List<RespuestaPreguntaProgramaCapacitacion>();
                    foreach (var item in compuestoPreguntaProgramaCapacitacionDTO.RespuestaPreguntaProgramaCapacitacions)
                    {
                        int? puntajeTipoRespuesta = null;
                        if (compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.HasValue)
                        {
                            int tipoRes = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.Value;
                            if (tipoRes == 1) //Directo
                            {
                                puntajeTipoRespuesta = item.Puntaje;
                            }
                            else if (tipoRes == 2) //Inversa
                            {
                                if (compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.HasValue)
                                {
                                    int factorRes = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.Value;
                                    puntajeTipoRespuesta = factorRes - item.Puntaje;
                                }
                            }
                            else //negativo
                            {
                                if (compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.HasValue)
                                {
                                    int factorRes = compuestoPreguntaProgramaCapacitacionDTO.PreguntaProgramaCapacitacion.FactorRespuesta.Value;
                                    if (!item.RespuestaCorrecta.HasValue)
                                        puntajeTipoRespuesta = item.Puntaje - factorRes;
                                    else
                                        puntajeTipoRespuesta = item.Puntaje;
                                }
                            }
                        }

                        RespuestaPreguntaProgramaCapacitacion respuestaPregunta = new RespuestaPreguntaProgramaCapacitacion()
                        {

                            RespuestaCorrecta = item.RespuestaCorrecta,
                            NroOrden = item.NroOrden,
                            EnunciadoRespuesta = item.EnunciadoRespuesta,
                            NroOrdenRespuesta = item.NroOrdenRespuesta,
                            Puntaje = item.Puntaje,
                            FeedbackPositivo = item.FeedbackPositivo,
                            FeedbackNegativo = item.FeedbackNegativo,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            PuntajeTipoRespuesta = puntajeTipoRespuesta
                        };
                        preguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacions.Add(respuestaPregunta);
                    }
                    preguntaIntento.PreguntaProgramaCapacitacions.Add(preguntaProgramaCapacitacion);
                    _unitOfWork.PreguntaIntentoRepository.Add(preguntaIntento);
                    _unitOfWork.Commit();
                    respuesta = true;
                    scope.Complete();
                }
                return (respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 26/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina una lista de PreguntaIntentoProgramaCapacitacion y sus detalles
        /// </summary>
        /// <param name="id"> (PK) de la preguntaProgramaCapacitacion  </param>
        /// <param name="usuario"> Usuario integra </param>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        public bool Eliminar(List<int> id, string usuario)
        {
            try
            {
                foreach (var registro in id)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        var preguntaProgramaCapacitacion = _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPorId(registro);
                        if (preguntaProgramaCapacitacion.Id > 0)
                        {
                            if (preguntaProgramaCapacitacion.IdPreguntaIntento.HasValue)
                            {
                                var preguntaIntentoDetalles = _unitOfWork.PreguntaIntentoDetalleRepository.ObtenerPorIdPreguntaIntento(preguntaProgramaCapacitacion.IdPreguntaIntento.Value);
                                if (preguntaIntentoDetalles.Count() >= 1)
                                    _unitOfWork.PreguntaIntentoDetalleRepository.Delete(preguntaIntentoDetalles.Select(x => x.Id), usuario);

                                _unitOfWork.PreguntaIntentoRepository.Delete(preguntaProgramaCapacitacion.IdPreguntaIntento.Value, usuario);
                                _unitOfWork.Commit();
                            }

                            var respuestaPreguntaProgramaCapacitacions = _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.ObtenerPorIdPreguntaProgramaCapacitacion(preguntaProgramaCapacitacion.Id);
                            if (respuestaPreguntaProgramaCapacitacions.Count() >= 1)
                                _unitOfWork.RespuestaPreguntaProgramaCapacitacionRepository.Delete(respuestaPreguntaProgramaCapacitacions.Select(x => x.Id), usuario);

                            _unitOfWork.PreguntaProgramaCapacitacionRepository.Delete(preguntaProgramaCapacitacion.Id, usuario);
                            _unitOfWork.Commit();
                        }
                        scope.Complete();
                    }
                }
                return (true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 27/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Obtiene los combos necesarios para el funcionamiento del modulo
        /// </summary>
        /// <returns>Objeto anonimo { PGeneral, TipoMarcador, ProgramaEspecifico }</returns>
        public PreguntaProgramaCapacitacionCombosModuloDTO ObtenerCombosModulo()
        {
            try
            {
                var preguntaProgramaCapacitacionCombosModulo = new PreguntaProgramaCapacitacionCombosModuloDTO();
                preguntaProgramaCapacitacionCombosModulo.PreguntaTipoRespuestas = _unitOfWork.PreguntaTipoRepository.ObtenerPreguntaTipoRespuesta().ToList();
                preguntaProgramaCapacitacionCombosModulo.PGenerals = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro().ToList();
                preguntaProgramaCapacitacionCombosModulo.TipoRespuestaCalificacions = _unitOfWork.TipoRespuestaCalificacionRepository.Obtener().Select(x => new ComboDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
                preguntaProgramaCapacitacionCombosModulo.TipoMarcadors = _unitOfWork.TipoMarcadorRepository.ObtenerCombo().ToList();
                preguntaProgramaCapacitacionCombosModulo.PEspecificos = _unitOfWork.PEspecificoRepository.ObtenerComboSinValidacion().ToList();
                return preguntaProgramaCapacitacionCombosModulo;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21/07/2023
        /// Versión: 1.5
        /// <summary>
        /// Funcion para obtener la lista de capitulos y sus sesiones respectivas con relacion al programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Booleano con respuesta 200 y la lista de objeto (CapituloSesionProgramaCapacitacionDTO) o 400 con el mensaje de error</returns> 
        public List<CapituloSesionProgramaCapacitacionDTO> ObtenerCapituloSesionesPGeneral(int idPGeneral)
        {
            try
            {
                List<CapituloSesionProgramaCapacitacionDTO> listaRegistro;
                if (idPGeneral == -1)
                {
                    listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();
                }
                else
                {
                    var respuesta = _unitOfWork.PGeneralDocumentoPwRepository.ObtenerPreConfigurarVideoPrograma(idPGeneral);
                    var listadoEstructura = (from x in respuesta
                                             group x by x.NumeroFila into newGroup
                                             select newGroup).ToList();
                    List<EstructuraCapituloProgramaDTO> lista = new List<EstructuraCapituloProgramaDTO>();

                    foreach (var item in listadoEstructura)
                    {
                        EstructuraCapituloProgramaDTO objeto = new EstructuraCapituloProgramaDTO();
                        objeto.OrdenFila = item.Key;
                        foreach (var itemRegistros in item)
                        {
                            switch (itemRegistros.NombreTitulo)
                            {
                                case "Capitulo":
                                    objeto.Nombre = itemRegistros.Nombre;
                                    objeto.Capitulo = itemRegistros.Contenido;
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetallePw;
                                    objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                    break;
                                case "Sesion":
                                    objeto.Sesion = itemRegistros.Contenido;
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetallePw;
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                    break;
                                case "SubSeccion":
                                    objeto.SubSesion = itemRegistros.Contenido;
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetallePw;
                                    if (!string.IsNullOrEmpty(objeto.SubSesion))
                                    {
                                        objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                    }
                                    break;
                                default:
                                    objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                    objeto.TotalSegundos = itemRegistros.TotalSegundos;
                                    break;
                            }
                        }
                        lista.Add(objeto);
                    }
                    var rpta = lista.OrderBy(x => x.OrdenFila).ToList();
                    var listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });
                    listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();

                    foreach (var capitulo in listas)
                    {
                        CapituloSesionProgramaCapacitacionDTO registro = new CapituloSesionProgramaCapacitacionDTO();
                        registro.IdPGeneral = capitulo.Key.IdPgeneral;
                        registro.CapituloProgramaCapacitacion = capitulo.Key.Capitulo;
                        registro.IdCapituloProgramaCapacitacion = capitulo.Key.OrdenCapitulo;

                        registro.ListaSesionesProgramaCapacitacion = new List<SesionSubSeccionProgramaCapacitacionDTO>();

                        registro.ListaSesionesProgramaCapacitacion = capitulo.GroupBy(x => x.Sesion).Select(x => new SesionSubSeccionProgramaCapacitacionDTO
                        {
                            SesionProgramaCapacitacion = x.Key,
                            IdSesionProgramaCapacitacion = capitulo.GroupBy(z => new { z.OrdenFila, z.Sesion, z.SubSesion }).Where(z => z.Key.Sesion == x.Key).FirstOrDefault().Key.OrdenFila,
                            ListaSubSeccionProgramaCapacitacion = capitulo.GroupBy(y => new { y.OrdenFila, y.Sesion, y.SubSesion }).Where(y => y.Key.Sesion == x.Key).Select(y => new SubSeccionProgramaCapacitacionDTO 
                            {
                                IdSesionProgramaCapacitacion = y.Key.OrdenFila,
                                SubSeccionProgramaCapacitacion = y.Key.SubSesion
                            }).ToList()
                        }).ToList();

                        listaRegistro.Add(registro);
                    }
                }
                return (listaRegistro);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza por el IdPGeneral y GrupoPregunta
        /// </summary>
        /// <param name="grupoPreguntaProgramaCapacitacions"> Listas de objetos a actualizarse </param>
        /// <param name="usuario"> usuario integra </param>
        /// <returns> Booleano con respuesta 200  o 400 con el mensaje de error </returns> 
        public bool ActualizarRespuestaPorSecuenciaVideo(List<GrupoPreguntaProgramaCapacitacionDTO> grupoPreguntaProgramaCapacitacions, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var itemPregunta in grupoPreguntaProgramaCapacitacions)
                    {
                        var preguntaProgramaCapacitacions = _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPorIdPGeneralYGrupoPregunta(itemPregunta.IdPgeneral, itemPregunta.GrupoPregunta).ToList();

                        foreach (var item in preguntaProgramaCapacitacions)
                        {
                            var preguntaProgramaCapacitacion = _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPorId(item.Id);

                            preguntaProgramaCapacitacion.IdTipoMarcador = itemPregunta.IdTipoVista;
                            preguntaProgramaCapacitacion.ValorMarcador = itemPregunta.Segundos;
                            preguntaProgramaCapacitacion.UsuarioModificacion = usuario;
                            preguntaProgramaCapacitacion.FechaModificacion = DateTime.Now;
                            _unitOfWork.PreguntaProgramaCapacitacionRepository.Update(preguntaProgramaCapacitacion);
                            _unitOfWork.Commit();
                        }
                    }
                    scope.Complete();
                }
                return (true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los registros asociados al IdPGeneral y GrupoPregunta
        /// </summary>
        /// <param name="idPgeneral"> Listas de objetos a actualizarse </param>
        /// <param name="grupoPregunta"> usuario integra </param>
        /// <returns> Booleano con respuesta 200  o 400 con el mensaje de error </returns> 
        public List<ListadoPreguntaPorEstructuraDTO> ObtenerPorEstructura(int idPgeneral, string grupoPregunta)
        {
            try
            {
                List<ListadoPreguntaPorEstructuraDTO> res = new List<ListadoPreguntaPorEstructuraDTO>();
                if (idPgeneral > 0 && !string.IsNullOrEmpty(grupoPregunta))
                    return _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPorEstructura(idPgeneral, grupoPregunta).ToList();
                else
                    return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// <summary>
        /// Obtiene preguntas registradas por programa capacitacion
        /// </summary>
        /// <returns>Lista</returns>
        public List<PreguntaProgramaCapacitacionRegistradaDTO> ObtenerPreguntasRegistradas()
        {
            try
            {
                var registros = _unitOfWork.PreguntaProgramaCapacitacionRepository.ObtenerPreguntasRegistradas();
                return registros;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 02/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los capitulos y sesiones para los programas capacitacciones
        /// </summary>
        /// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Lista de objeto de tipo CapituloSesionProgramaCapacitacionDTO</returns>
        public List<CapituloSesionProgramaCapacitacionDTO> ObtenerCapituloSesionProgramaCapacitacion(int IdPGeneral)
        {
            try
            {
                List<CapituloSesionProgramaCapacitacionDTO> listaRegistro;
                if (IdPGeneral == -1)
                {
                    listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();
                }
                else
                {
                    var respuesta = _unitOfWork.ConfigurarVideoProgramaRepository.ObtenerPreConfigurarVideoPrograma(IdPGeneral);
                    var listadoEstructura = (from x in respuesta
                                             group x by x.NumeroFila into newGroup
                                             select newGroup).ToList();


                    List<EstructuraCapituloProgramaDTO> lista = new List<EstructuraCapituloProgramaDTO>();

                    foreach (var item in listadoEstructura)
                    {
                        EstructuraCapituloProgramaDTO objeto = new EstructuraCapituloProgramaDTO();
                        objeto.OrdenFila = item.Key;
                        foreach (var itemRegistros in item)
                        {
                            switch (itemRegistros.NombreTitulo)
                            {
                                case "Capitulo":
                                    objeto.Nombre = itemRegistros.Nombre;
                                    objeto.Capitulo = itemRegistros.Contenido;
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    objeto.IdPgeneral = itemRegistros.IdPgeneral;
                                    break;
                                case "Sesion":
                                    objeto.Sesion = itemRegistros.Contenido;
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                    break;
                                case "SubSeccion":
                                    objeto.SubSesion = itemRegistros.Contenido;
                                    objeto.OrdenSeccion = itemRegistros.IdSeccionTipoDetalle_PW;
                                    if (!string.IsNullOrEmpty(objeto.SubSesion))
                                    {
                                        objeto.IdDocumentoSeccionPw = itemRegistros.Id;
                                    }
                                    break;
                                default:
                                    objeto.OrdenCapitulo = Convert.ToInt32(itemRegistros.Contenido);
                                    objeto.TotalSegundos = itemRegistros.TotalSegundos;
                                    break;
                            }
                        }
                        lista.Add(objeto);
                    }

                    var rpta = lista.OrderBy(x => x.OrdenFila).ToList();

                    var listas = rpta.GroupBy(x => new { x.IdPgeneral, x.Nombre, x.Capitulo, x.OrdenCapitulo });

                    listaRegistro = new List<CapituloSesionProgramaCapacitacionDTO>();

                    foreach (var capitulo in listas)
                    {
                        CapituloSesionProgramaCapacitacionDTO registro = new CapituloSesionProgramaCapacitacionDTO();
                        registro.IdPGeneral = capitulo.Key.IdPgeneral;
                        registro.CapituloProgramaCapacitacion = capitulo.Key.Capitulo;
                        registro.IdCapituloProgramaCapacitacion = capitulo.Key.OrdenCapitulo;

                        registro.ListaSesionesProgramaCapacitacion = new List<SesionSubSeccionProgramaCapacitacionDTO>();

                        registro.ListaSesionesProgramaCapacitacion = capitulo.GroupBy(x => x.Sesion).Select(x => new SesionSubSeccionProgramaCapacitacionDTO
                        {
                            SesionProgramaCapacitacion = x.Key,
                            IdSesionProgramaCapacitacion = capitulo.GroupBy(z => new { z.OrdenFila, z.Sesion, z.SubSesion }).Where(z => z.Key.Sesion == x.Key).FirstOrDefault().Key.OrdenFila,
                            ListaSubSeccionProgramaCapacitacion = capitulo.GroupBy(y => new { y.OrdenFila, y.Sesion, y.SubSesion }).Where(y => y.Key.Sesion == x.Key).Select(y => new SubSeccionProgramaCapacitacionDTO
                            {
                                IdSesionProgramaCapacitacion = y.Key.OrdenFila,
                                SubSeccionProgramaCapacitacion = y.Key.SubSesion
                            }).ToList()
                        }).ToList();

                        listaRegistro.Add(registro);
                    }
                }
                return listaRegistro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.5
        /// <summary>
        /// Funcion para importar desde un archivo CSV a la base de datos
        /// </summary>
        /// <param name="archivo">Objeto de tipo IFormFile con las preguntas</param>
        /// <returns>Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error</returns> 
        public ImportarExcelRespuestaDTO ImportarExcel(IFormFile archivo, string usuario)
        {
            CsvFile file = new CsvFile();
            List<string> listaErrores = new List<string>();
            try
            {
                int indexError = 0;
                int indexTotal = 0;
                List<ImportacionPreguntaRespuestaProgramaCapacitacionDTO> ListaExcel = new List<ImportacionPreguntaRespuestaProgramaCapacitacionDTO>();
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                StreamReader sw = new StreamReader(archivo.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                var csvConfig = new CsvConfiguration(new System.Globalization.CultureInfo("es-ES"))
                {
                    Delimiter = ",",
                    MissingFieldFound = null
                };
                using (CsvReader cvs = new CsvReader(sw, csvConfig))
                {
                    //cvs.Configuration.Delimiter = ";";
                    //cvs.Configuration.MissingFieldFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        try
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                ImportacionPreguntaRespuestaProgramaCapacitacionDTO preguntaRespuestaProgramaCapacitacion = new ImportacionPreguntaRespuestaProgramaCapacitacionDTO()
                                {
                                    NumeroMaximoIntento = cvs.GetField<int?>("NumeroMaximoIntento"),
                                    ActivarFeedbackMaximoIntento = cvs.GetField<bool?>("ActivarFeedbackMaximoIntento"),
                                    MensajeFeedback = cvs.GetField<string>("MensajeFeedback"),
                                    IdTipoRespuesta = cvs.GetField<int?>("IdTipoRespuesta"),
                                    EnunciadoPregunta = cvs.GetField<string>("EnunciadoPregunta"),
                                    MinutosPorPregunta = cvs.GetField<int?>("MinutosPorPregunta"),
                                    RespuestaAleatoria = cvs.GetField<bool?>("RespuestaAleatoria"),
                                    ActivarFeedBackRespuestaCorrecta = cvs.GetField<bool?>("ActivarFeedBackRespuestaCorrecta"),
                                    ActivarFeedBackRespuestaIncorrecta = cvs.GetField<bool?>("ActivarFeedBackRespuestaIncorrecta"),
                                    MostrarFeedbackInmediato = cvs.GetField<bool?>("MostrarFeedbackInmediato"),
                                    MostrarFeedbackPorPregunta = cvs.GetField<bool?>("MostrarFeedbackPorPregunta"),
                                    IdPreguntaTipo = cvs.GetField<int?>("IdPreguntaTipo"),
                                    IdTipoRespuestaCalificacion = cvs.GetField<int?>("IdTipoRespuestaCalificacion"),
                                    FactorRespuesta = cvs.GetField<int?>("FactorRespuesta"),
                                    IdPgeneral = cvs.GetField<int>("IdPGeneral"),
                                    IdPEspecifico = cvs.GetField<int?>("IdPEspecifico"),
                                    OrdenFilaCapitulo = cvs.GetField<int?>("OrdenFilaCapitulo"),
                                    Sesion = cvs.GetField<string>("Sesion"),
                                    SubSeccion = cvs.GetField<string>("Subsesion"),
                                    GrupoPregunta = cvs.GetField<string>("GrupoPregunta"),
                                    IdTipoMarcador = cvs.GetField<int?>("IdTipoMarcador"),
                                    ValorMarcador = cvs.GetField<decimal?>("ValorMarcador"),
                                    OrdenPreguntaGrupo = cvs.GetField<int?>("OrdenPreguntaGrupo"),
                                    //  ============ RESPUESTAS =============================
                                    RespuestaCorrecta = cvs.GetField<bool?>("RespuestaCorrecta"),
                                    NroOrden = cvs.GetField<int>("NroOrden"),
                                    EnunciadoRespuesta = cvs.GetField<string>("EnunciadoRespuesta"),
                                    NroOrdenRespuesta = cvs.GetField<int?>("NroOrdenRespuesta"),
                                    Puntaje = cvs.GetField<int?>("Puntaje"),
                                    FeedbackPositivo = cvs.GetField<string>("FeedbackPositivo"),
                                    FeedbackNegativo = cvs.GetField<string>("FeedbackNegativo"),
                                    //========== PREGUNTAINTENTODETALLE =======================
                                    PorcentajeCalificacion = cvs.GetField<int?>("PorcentajeCalificacion"),
                                };
                                ListaExcel.Add(preguntaRespuestaProgramaCapacitacion);
                                scope.Complete();
                            }
                        }
                        catch (Exception e)
                        {
                            indexError++;
                            listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
                        }

                    }
                }
                var agrupado = ListaExcel.GroupBy(x => new
                {
                    x.IdTipoRespuesta,
                    x.EnunciadoPregunta,
                    x.MinutosPorPregunta,
                    x.RespuestaAleatoria,
                    x.ActivarFeedBackRespuestaCorrecta,
                    x.ActivarFeedBackRespuestaIncorrecta,
                    x.MostrarFeedbackInmediato,
                    x.MostrarFeedbackPorPregunta,
                    x.IdPreguntaTipo,
                    x.IdTipoRespuestaCalificacion,
                    x.FactorRespuesta,
                    x.IdPgeneral,
                    x.IdPEspecifico,
                    x.OrdenFilaCapitulo,
                    x.Sesion,
                    x.SubSeccion,
                    x.GrupoPregunta,
                    x.IdTipoMarcador,
                    x.ValorMarcador,
                    x.OrdenPreguntaGrupo
                }).Select(x => new PreguntaProgramaCapacitacionExcelCompuestoDTO
                {
                    PreguntaProgramaCapacitacion = new PreguntaProgramaCapacitacionAgrupadoDTO
                    {
                        ActivarFeedBackRespuestaCorrecta = x.Key.ActivarFeedBackRespuestaCorrecta,
                        ActivarFeedBackRespuestaIncorrecta = x.Key.ActivarFeedBackRespuestaIncorrecta,
                        EnunciadoPregunta = x.Key.EnunciadoPregunta,
                        FactorRespuesta = x.Key.FactorRespuesta,
                        IdPgeneral = x.Key.IdPgeneral,
                        IdPEspecifico = x.Key.IdPEspecifico,
                        IdPreguntaTipo = x.Key.IdPreguntaTipo,
                        IdTipoRespuesta = x.Key.IdTipoRespuesta,
                        IdTipoRespuestaCalificacion = x.Key.IdTipoRespuestaCalificacion,
                        MinutosPorPregunta = x.Key.MinutosPorPregunta,
                        MostrarFeedbackInmediato = x.Key.MostrarFeedbackInmediato,
                        MostrarFeedbackPorPregunta = x.Key.MostrarFeedbackPorPregunta,
                        OrdenFilaCapitulo = x.Key.OrdenFilaCapitulo,
                        Sesion = x.Key.Sesion,
                        SubSeccion = x.Key.SubSeccion,
                        GrupoPregunta = x.Key.GrupoPregunta,
                        IdTipoMarcador = x.Key.IdTipoMarcador,
                        ValorMarcador = x.Key.ValorMarcador,
                        OrdenPreguntaGrupo = x.Key.OrdenPreguntaGrupo,
                        RespuestaAleatoria = x.Key.RespuestaAleatoria,
                        PreguntaIntento = x.GroupBy(y => new { y.ActivarFeedbackMaximoIntento, y.MensajeFeedback, y.NumeroMaximoIntento }).Select(y => new PreguntaIntentoAgrupadoDTO
                        {
                            ActivarFeedbackMaximoIntento = y.Key.ActivarFeedbackMaximoIntento,
                            MensajeFeedback = y.Key.MensajeFeedback,
                            NumeroMaximoIntento = y.Key.NumeroMaximoIntento,
                            PreguntaIntentoDetalle = y.GroupBy(z => z.PorcentajeCalificacion).Select(z => new PreguntaIntentoDetalleAgrupadoDTO
                            {
                                PorcentajeCalificacion = z.Key
                            }).ToList()
                        }).FirstOrDefault(),
                        RespuestaPreguntaProgramaCapacitacion = x.GroupBy(z => new { z.RespuestaCorrecta, z.NroOrden, z.EnunciadoRespuesta, z.NroOrdenRespuesta, z.Puntaje, z.FeedbackPositivo, z.FeedbackNegativo }).Select(z => new RespuestaPreguntaProgramaCapacitacionAgrupadoDTO
                        {
                            EnunciadoRespuesta = z.Key.EnunciadoRespuesta,
                            FeedbackPositivo = z.Key.FeedbackPositivo,
                            FeedbackNegativo = z.Key.FeedbackNegativo,
                            NroOrden = z.Key.NroOrden,
                            NroOrdenRespuesta = z.Key.NroOrdenRespuesta,
                            Puntaje = z.Key.Puntaje,
                            RespuestaCorrecta = z.Key.RespuestaCorrecta
                        }).ToList()
                    }
                }).ToList();

                foreach (var item in agrupado)
                {
                    var listaCompuesta = ObtenerCapituloSesionProgramaCapacitacion(item.PreguntaProgramaCapacitacion.IdPgeneral);
                    var listaCapitulos = listaCompuesta.Where(x => x.IdCapituloProgramaCapacitacion == item.PreguntaProgramaCapacitacion.OrdenFilaCapitulo).FirstOrDefault();
                    int? ordenFilaSesion = null;
                    if (listaCapitulos != null)
                    {
                        var sesion = listaCapitulos.ListaSesionesProgramaCapacitacion.Where(x => x.SesionProgramaCapacitacion.ToLower().Equals(item.PreguntaProgramaCapacitacion.Sesion.ToLower())).FirstOrDefault();

                        int idSesionTemporal = 0;

                        if (sesion != null)
                        {
                            idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
                        }

                        if (!string.IsNullOrEmpty(item.PreguntaProgramaCapacitacion.SubSeccion))
                        {
                            try
                            {
                                idSesionTemporal = sesion.ListaSubSeccionProgramaCapacitacion.FirstOrDefault(y => y.SubSeccionProgramaCapacitacion.ToLower().Equals(item.PreguntaProgramaCapacitacion.SubSeccion.ToLower())).IdSesionProgramaCapacitacion;
                            }
                            catch (Exception e)
                            {
                                idSesionTemporal = sesion.IdSesionProgramaCapacitacion;
                            }
                        }

                        if (idSesionTemporal > 0)
                        {
                            ordenFilaSesion = idSesionTemporal;
                        }
                    }
                    try
                    {
                        indexTotal++;
                        using (TransactionScope scope = new TransactionScope())
                        {
                            PreguntaIntento preguntaIntento = new PreguntaIntento()
                            {
                                ActivarFeedbackMaximoIntento = item.PreguntaProgramaCapacitacion.PreguntaIntento.ActivarFeedbackMaximoIntento,
                                NumeroMaximoIntento = item.PreguntaProgramaCapacitacion.PreguntaIntento.NumeroMaximoIntento,
                                MensajeFeedback = item.PreguntaProgramaCapacitacion.PreguntaIntento.MensajeFeedback,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            if (item.PreguntaProgramaCapacitacion.PreguntaIntento.PreguntaIntentoDetalle != null && item.PreguntaProgramaCapacitacion.PreguntaIntento.PreguntaIntentoDetalle.Count > 0)
                            {
                                preguntaIntento.PreguntaIntentoDetalles = new List<PreguntaIntentoDetalle>();
                                foreach (var pi in item.PreguntaProgramaCapacitacion.PreguntaIntento.PreguntaIntentoDetalle)
                                {
                                    if (pi.PorcentajeCalificacion != null)
                                    {
                                        PreguntaIntentoDetalle preguntaIntentoDetalle = new PreguntaIntentoDetalle
                                        {
                                            PorcentajeCalificacion = pi.PorcentajeCalificacion,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now
                                        };
                                        preguntaIntento.PreguntaIntentoDetalles.Add(preguntaIntentoDetalle);
                                    }
                                }
                            }
                            preguntaIntento.PreguntaProgramaCapacitacions = new List<PreguntaProgramaCapacitacion>();
                            PreguntaProgramaCapacitacion preguntaProgramaCapacitacion = new PreguntaProgramaCapacitacion()
                            {
                                IdTipoRespuesta = item.PreguntaProgramaCapacitacion.IdTipoRespuesta,
                                EnunciadoPregunta = item.PreguntaProgramaCapacitacion.EnunciadoPregunta,
                                MinutosPorPregunta = item.PreguntaProgramaCapacitacion.MinutosPorPregunta,
                                RespuestaAleatoria = item.PreguntaProgramaCapacitacion.RespuestaAleatoria,
                                ActivarFeedBackRespuestaCorrecta = item.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaCorrecta,
                                ActivarFeedBackRespuestaIncorrecta = item.PreguntaProgramaCapacitacion.ActivarFeedBackRespuestaIncorrecta,
                                MostrarFeedbackInmediato = item.PreguntaProgramaCapacitacion.MostrarFeedbackInmediato,
                                MostrarFeedbackPorPregunta = item.PreguntaProgramaCapacitacion.MostrarFeedbackPorPregunta,
                                IdPreguntaTipo = item.PreguntaProgramaCapacitacion.IdPreguntaTipo,
                                IdTipoRespuestaCalificacion = item.PreguntaProgramaCapacitacion.IdTipoRespuestaCalificacion,
                                FactorRespuesta = item.PreguntaProgramaCapacitacion.FactorRespuesta,
                                IdPgeneral = item.PreguntaProgramaCapacitacion.IdPgeneral,
                                IdPespecifico = item.PreguntaProgramaCapacitacion.IdPEspecifico,
                                OrdenFilaCapitulo = item.PreguntaProgramaCapacitacion.OrdenFilaCapitulo,
                                OrdenFilaSesion = ordenFilaSesion,
                                GrupoPregunta = item.PreguntaProgramaCapacitacion.GrupoPregunta,
                                IdTipoMarcador = item.PreguntaProgramaCapacitacion.IdTipoMarcador,
                                ValorMarcador = item.PreguntaProgramaCapacitacion.ValorMarcador,
                                OrdenPreguntaGrupo = item.PreguntaProgramaCapacitacion.OrdenPreguntaGrupo,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            };

                            if (item.PreguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacion != null && item.PreguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacion.Count > 0)
                            {
                                preguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacions = new List<RespuestaPreguntaProgramaCapacitacion>();
                                foreach (var respuesta in item.PreguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacion)
                                {
                                    int? puntajeTipoRespuesta = null;
                                    int? puntaje = respuesta.Puntaje;
                                    bool? respuestaCorrecta = respuesta.RespuestaCorrecta;
                                    if (preguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.HasValue)
                                    {
                                        int tipoRes = preguntaProgramaCapacitacion.IdTipoRespuestaCalificacion.Value;
                                        if (tipoRes == 1) //Directo
                                        {
                                            puntajeTipoRespuesta = puntaje;
                                        }
                                        else if (tipoRes == 2) //Inversa
                                        {
                                            if (preguntaProgramaCapacitacion.FactorRespuesta.HasValue)
                                            {
                                                int factorRes = preguntaProgramaCapacitacion.FactorRespuesta.Value;
                                                puntajeTipoRespuesta = factorRes - puntaje;
                                            }
                                        }
                                        else //negativo
                                        {
                                            if (preguntaProgramaCapacitacion.FactorRespuesta.HasValue)
                                            {
                                                int factorRes = preguntaProgramaCapacitacion.FactorRespuesta.Value;
                                                if (respuestaCorrecta.HasValue)
                                                    if (!respuestaCorrecta.Value)
                                                        puntajeTipoRespuesta = puntaje - factorRes;
                                                    else
                                                        puntajeTipoRespuesta = puntaje;
                                            }
                                        }
                                    }
                                    RespuestaPreguntaProgramaCapacitacion respuestaPregunta = new RespuestaPreguntaProgramaCapacitacion()
                                    {
                                        RespuestaCorrecta = respuesta.RespuestaCorrecta,
                                        NroOrden = respuesta.NroOrden,
                                        EnunciadoRespuesta = respuesta.EnunciadoRespuesta,
                                        NroOrdenRespuesta = respuesta.NroOrdenRespuesta,
                                        Puntaje = respuesta.Puntaje,
                                        FeedbackPositivo = respuesta.FeedbackPositivo,
                                        FeedbackNegativo = respuesta.FeedbackNegativo,
                                        Estado = true,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        PuntajeTipoRespuesta = puntajeTipoRespuesta
                                    };
                                    preguntaProgramaCapacitacion.RespuestaPreguntaProgramaCapacitacions.Add(respuestaPregunta);
                                }
                            }
                            preguntaIntento.PreguntaProgramaCapacitacions.Add(preguntaProgramaCapacitacion);
                            _unitOfWork.PreguntaIntentoRepository.Add(preguntaIntento);
                            scope.Complete();
                        }
                    }
                    catch (Exception e)
                    {
                        indexError++;
                        listaErrores.Add("Error - " + e.Message);
                    }
                }
                var importarExcelRespuesta = new ImportarExcelRespuestaDTO()
                {
                    Total = indexTotal,
                    Correcto = (indexTotal - indexError),
                    Error = indexError,
                    Errores = listaErrores
                };
                return (importarExcelRespuesta);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.5
        /// <summary>
        /// Funcion para importar respuesta excel desde un archivo CSV a la base de datos
        /// </summary>
        /// <param name="importarRespuestasExcel">Objeto de tipo IFormFile con las preguntas</param>
        /// <param name="usuario"> usuario integra </param>
        /// <returns>Booleano con respuesta 200 y la objeto anonimo con las propiedades({ Total, Correctos, Error, Errores }) o 400 con el mensaje de error</returns> 
        public ImportarExcelRespuestaDTO ImportarRespuestasExcel(ImportarRespuestasExcelDTO importarRespuestasExcel, string usuario)
        {
            CsvFile file = new CsvFile();
            List<string> listaErrores = new List<string>();
            try
            {
                int indexError = 0;
                int indexTotal = 0;
                var pregunta = _unitOfWork.PreguntumRepository.ObtenerPorId(importarRespuestasExcel.IdPregunta);

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                StreamReader sw = new StreamReader(importarRespuestasExcel.File.OpenReadStream(), System.Text.Encoding.GetEncoding("iso-8859-1"));
                var csvConfig = new CsvConfiguration(new System.Globalization.CultureInfo("es-ES"))
                {
                    Delimiter = ",",
                    MissingFieldFound = null
                };
                using (CsvReader cvs = new CsvReader(sw, csvConfig))
                {
                    //cvs.Configuration.Delimiter = ";";
                    //cvs.Configuration.MissingFieldFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        int? puntajeTipoRespuesta = null;
                        int? puntaje = cvs.GetField<int?>("Puntaje");
                        bool? respuestaCorrecta = cvs.GetField<bool?>("RespuestaCorrecta");
                        if (pregunta.IdTipoRespuestaCalificacion.HasValue)
                        {
                            int tipoRes = pregunta.IdTipoRespuestaCalificacion.Value;
                            if (tipoRes == 1) //Directo
                            {
                                puntajeTipoRespuesta = puntaje;
                            }
                            else if (tipoRes == 2) //Inversa
                            {
                                if (pregunta.FactorRespuesta.HasValue)
                                {
                                    int factorRes = pregunta.FactorRespuesta.Value;
                                    puntajeTipoRespuesta = factorRes - puntaje;
                                }
                            }
                            else //negativo
                            {
                                if (pregunta.FactorRespuesta.HasValue)
                                {
                                    int factorRes = pregunta.FactorRespuesta.Value;
                                    if (respuestaCorrecta.HasValue)
                                        if (!respuestaCorrecta.Value)
                                            puntajeTipoRespuesta = puntaje - factorRes;
                                        else
                                            puntajeTipoRespuesta = puntaje;
                                }
                            }
                        }

                        indexTotal++;
                        try
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                RespuestaPregunta respuestaPregunta = new RespuestaPregunta()
                                {
                                    IdPregunta = importarRespuestasExcel.IdPregunta,
                                    RespuestaCorrecta = cvs.GetField<bool?>("RespuestaCorrecta"),
                                    NroOrden = cvs.GetField<int>("NroOrden"),
                                    EnunciadoRespuesta = cvs.GetField<string>("EnunciadoRespuesta"),
                                    NroOrdenRespuesta = cvs.GetField<int?>("NroOrdenRespuesta"),
                                    Puntaje = cvs.GetField<int?>("Puntaje"),
                                    FeedbackPositivo = cvs.GetField<string>("FeedbackPositivo"),
                                    FeedbackNegativo = cvs.GetField<string>("FeedbackNegativo"),
                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    PuntajeTipoRespuesta = puntajeTipoRespuesta
                                };
                                _unitOfWork.RespuestaPreguntaRepository.Add(respuestaPregunta);
                                _unitOfWork.Commit();
                                scope.Complete();
                            }
                        }
                        catch (Exception e)
                        {
                            indexError++;
                            listaErrores.Add("Error en: " + cvs.GetField<string>("EnunciadoPregunta") + " - " + e.Message);
                        }

                    }
                }
                var importarExcelRespuesta = new ImportarExcelRespuestaDTO()
                {
                    Total = indexTotal,
                    Correcto = (indexTotal - indexError),
                    Error = indexError,
                    Errores = listaErrores
                };
                return (importarExcelRespuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
