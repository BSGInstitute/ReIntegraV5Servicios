using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Google.Api.Ads.Common.Util;
using Google.Apis.Util;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Transactions;
using Twilio.Rest.Trunking.V1;



namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{


    /// Service: PreguntumService
    /// Autor: Jorge Gamero
    /// Fecha: 06/05/2025
    /// <summary>
    /// Gestión general de T_Pregunta
    /// </summary>
    public class PreguntumService : IPreguntumService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaEncuestum, Preguntum>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPreguntum, Preguntum>();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Preguntum Add(Preguntum entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Preguntum>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Preguntum Update(Preguntum entidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Preguntum>(modelo);
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
                _unitOfWork.PreguntumRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Preguntum> Add(List<Preguntum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Preguntum>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Preguntum> Update(List<Preguntum> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PreguntumRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Preguntum>>(modelo);
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
                _unitOfWork.PreguntumRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public Preguntum ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.PreguntumRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 06/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public List<BancoPreguntumDTO> ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                return _unitOfWork.PreguntumRepository.ObtenerPreguntaEncuestaAsincronica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DTO.SCode.Modelos.IntegraDB.PreguntaRegistradaDTO> Obtener()
        {
            return _unitOfWork.PreguntumRepository.Obtener();
        }

        public IEnumerable<RespuestaPreguntaDTO> ObtenerRespuestaPregunta(int id)
        {
            return _unitOfWork.PreguntumRepository.ObtenerRespuestaPregunta(id);
        }

        public IEnumerable<PreguntaTipoRespuestaDTO> ObtenerComboTipoPregunta()
        {
            return _unitOfWork.PreguntaTipoRepository.ObtenerPreguntaTipoRespuesta();
        }

        public IEnumerable<TipoRespuestaCalificacionDTO> ObtenerTipoRespuestaCategoria()
        {
            return _unitOfWork.PreguntumRepository.ObtenerTipoRespuestaCategoria();
        }

        public bool InsertarPregunta(CompuestoPreguntaDTO dto, string usuario)
        {
            try
            {
                PreguntaIntento preguntaIntento = new PreguntaIntento()
                {
                    NumeroMaximoIntento = dto.PreguntaIntento.NumeroMaximoIntento,
                    ActivarFeedbackMaximoIntento = dto.PreguntaIntento.ActivarFeedbackMaximoIntento,
                    MensajeFeedback = dto.PreguntaIntento.MensajeFeedback,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var PreguntaIntentoInsertado = _unitOfWork.PreguntaIntentoRepository.Add(preguntaIntento);
                _unitOfWork.Commit();

                Preguntum pregunta = new Preguntum()
                {
                    IdTipoRespuesta = dto.Pregunta.IdTipoRespuesta,
                    EnunciadoPregunta = dto.Pregunta.Enunciado,
                    MinutosPorPregunta = dto.Pregunta.MinutosPorPregunta,
                    RespuestaAleatoria = dto.Pregunta.RespuestaAleatoria,
                    ActivarFeedBackRespuestaCorrecta = dto.Pregunta.ActivarFeedBackRespuestaCorrecta,
                    ActivarFeedBackRespuestaIncorrecta = dto.Pregunta.ActivarFeedBackRespuestaIncorrecta,
                    MostrarFeedbackInmediato = dto.Pregunta.MostrarFeedbackInmediato,
                    MostrarFeedbackPorPregunta = dto.Pregunta.MostrarFeedbackPorPregunta,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    IdPreguntaIntento = PreguntaIntentoInsertado.Id,
                    IdPreguntaTipo = dto.Pregunta.IdPreguntaTipo,
                    IdTipoRespuestaCalificacion = dto.Pregunta.IdTipoRespuestaCalificacion,
                    FactorRespuesta = dto.Pregunta.FactorRespuesta,
                    IdPreguntaCategoria = dto.Pregunta.IdPreguntaCategoria
                };
                var PreguntaInsertada = _unitOfWork.PreguntumRepository.Add(pregunta);
                _unitOfWork.Commit();
                if (dto.Examen.ListaExamen != null)
                {
                    foreach (var item in dto.Examen.ListaExamen)
                    {
                        AsignacionPreguntaExamen asignacionPreguntaExamen = new AsignacionPreguntaExamen()
                        {
                            IdExamen = item,
                            IdPregunta = PreguntaInsertada.Id,
                            NroOrden = 1,
                            Puntaje = 1,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        };
                        _unitOfWork.AsignacionPreguntaExamenRepository.Add(asignacionPreguntaExamen);

                    }
                    _unitOfWork.Commit();
                }
                else { dto.Examen.ListaExamen = new List<int>(); }
                foreach (var item in dto.RespuestaPregunta)
                {
                    int? puntajeTipoRespuesta = null;
                    if (dto.Pregunta.IdTipoRespuestaCalificacion.HasValue)
                    {
                        int tipoRes = dto.Pregunta.IdTipoRespuestaCalificacion.Value;
                        if (tipoRes == 1) //Directo
                        {
                            puntajeTipoRespuesta = item.Puntaje;
                        }
                        else if (tipoRes == 2) //Inversa
                        {
                            if (dto.Pregunta.FactorRespuesta.HasValue)
                            {
                                int factorRes = dto.Pregunta.FactorRespuesta.Value;
                                puntajeTipoRespuesta = factorRes - item.Puntaje;
                            }
                        }
                        else //negativo
                        {
                            if (dto.Pregunta.FactorRespuesta.HasValue)
                            {
                                int factorRes = dto.Pregunta.FactorRespuesta.Value;
                                if ((bool)!item.RespuestaCorrecta)
                                    puntajeTipoRespuesta = item.Puntaje - factorRes;
                                else
                                    puntajeTipoRespuesta = item.Puntaje;
                            }
                        }
                    }
                    RespuestaPregunta respuestaPregunta = new RespuestaPregunta()
                    {
                        IdPregunta = PreguntaInsertada.Id,
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
                    var respuestaPreguntaInsertada = _unitOfWork.RespuestaPreguntaRepository.Add(respuestaPregunta);
                    _unitOfWork.Commit();
                }

                return (true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActualizarPregunta(CompuestoPreguntaDTO dto, string usuario)
        {
            try
            {
                PreguntaIntento preguntaIntento;
                List<AsignacionPreguntaExamen> listaAsignacionPreguntaExamen;
                var pregunta = _unitOfWork.PreguntumRepository.FirstById(dto.Pregunta.Id);
                if (pregunta.IdPreguntaIntento.HasValue)
                {
                    preguntaIntento = _unitOfWork.PreguntaIntentoRepository.ObtenerPorId(pregunta.IdPreguntaIntento.Value);
                    preguntaIntento.NumeroMaximoIntento = dto.PreguntaIntento.NumeroMaximoIntento;
                    preguntaIntento.ActivarFeedbackMaximoIntento = dto.PreguntaIntento.ActivarFeedbackMaximoIntento;
                    preguntaIntento.MensajeFeedback = dto.PreguntaIntento.MensajeFeedback;
                    preguntaIntento.UsuarioModificacion = usuario;
                    preguntaIntento.FechaModificacion = DateTime.Now;
                    _unitOfWork.PreguntaIntentoRepository.Update(preguntaIntento);
                    _unitOfWork.Commit();
                }
                else
                {
                    preguntaIntento = new PreguntaIntento()
                    {
                        NumeroMaximoIntento = dto.PreguntaIntento.NumeroMaximoIntento,
                        ActivarFeedbackMaximoIntento = dto.PreguntaIntento.ActivarFeedbackMaximoIntento,
                        MensajeFeedback = dto.PreguntaIntento.MensajeFeedback,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    _unitOfWork.PreguntaIntentoRepository.Add(preguntaIntento);
                    _unitOfWork.Commit();

                    pregunta.IdPreguntaIntento = preguntaIntento.Id;
                }

                pregunta.IdTipoRespuesta = dto.Pregunta.IdTipoRespuesta;
                pregunta.EnunciadoPregunta = dto.Pregunta.Enunciado;
                pregunta.MinutosPorPregunta = dto.Pregunta.MinutosPorPregunta;
                pregunta.RespuestaAleatoria = dto.Pregunta.RespuestaAleatoria;
                pregunta.ActivarFeedBackRespuestaCorrecta = dto.Pregunta.ActivarFeedBackRespuestaCorrecta;
                pregunta.ActivarFeedBackRespuestaIncorrecta = dto.Pregunta.ActivarFeedBackRespuestaIncorrecta;
                pregunta.MostrarFeedbackInmediato = dto.Pregunta.MostrarFeedbackInmediato;
                pregunta.MostrarFeedbackPorPregunta = dto.Pregunta.MostrarFeedbackPorPregunta;
                pregunta.UsuarioModificacion = usuario;
                pregunta.FechaModificacion = DateTime.Now;
                pregunta.IdTipoRespuestaCalificacion = dto.Pregunta.IdTipoRespuestaCalificacion;
                pregunta.FactorRespuesta = dto.Pregunta.FactorRespuesta;
                pregunta.IdPreguntaTipo = dto.Pregunta.IdPreguntaTipo;
                pregunta.IdPreguntaCategoria = dto.Pregunta.IdPreguntaCategoria;
                _unitOfWork.PreguntumRepository.Update(pregunta);
                _unitOfWork.Commit();

                listaAsignacionPreguntaExamen = _unitOfWork.AsignacionPreguntaExamenRepository.ObtenerAsignacionesPreguntaExamenbyId(pregunta.Id);
                foreach (var item in listaAsignacionPreguntaExamen)
                {
                    if (!dto.Examen.ListaExamen.Any(x => x == item.IdExamen))
                    {
                        _unitOfWork.AsignacionPreguntaExamenRepository.Delete(item.Id, usuario);
                    }
                    else
                    { }
                }
                listaAsignacionPreguntaExamen = _unitOfWork.AsignacionPreguntaExamenRepository.ObtenerAsignacionesPreguntaExamenbyId(pregunta.Id);
                foreach (var item in dto.Examen.ListaExamen)
                {
                    AsignacionPreguntaExamen asignacionPreguntaExamen;
                    if (listaAsignacionPreguntaExamen.Any(x => x.IdExamen == item))
                    {
                        asignacionPreguntaExamen = _unitOfWork.AsignacionPreguntaExamenRepository.ObtenerExamenPregunta(x => x.IdExamen == item);
                        asignacionPreguntaExamen.NroOrden = 1;
                        asignacionPreguntaExamen.Puntaje = 1;
                        asignacionPreguntaExamen.UsuarioModificacion = usuario;
                        asignacionPreguntaExamen.FechaModificacion = DateTime.Now;

                        _unitOfWork.AsignacionPreguntaExamenRepository.Update(asignacionPreguntaExamen);
                    }
                    else
                    {
                        asignacionPreguntaExamen = new AsignacionPreguntaExamen();
                        asignacionPreguntaExamen.IdExamen = item;
                        asignacionPreguntaExamen.IdPregunta = pregunta.Id;
                        asignacionPreguntaExamen.NroOrden = 1;
                        asignacionPreguntaExamen.Puntaje = 1;
                        asignacionPreguntaExamen.Estado = true;
                        asignacionPreguntaExamen.UsuarioCreacion = usuario;
                        asignacionPreguntaExamen.UsuarioModificacion = usuario;
                        asignacionPreguntaExamen.FechaCreacion = DateTime.Now;
                        asignacionPreguntaExamen.FechaModificacion = DateTime.Now;

                        _unitOfWork.AsignacionPreguntaExamenRepository.Add(asignacionPreguntaExamen);
                    }
                }
                _unitOfWork.Commit();
                //commit al final


                var rp = _unitOfWork.RespuestaPreguntaRepository.ObtenerRespuesta(pregunta.Id);
                foreach (var item in rp)
                {
                    if (!dto.RespuestaPregunta.Any(x => x.id == item.IdRespuestaPregunta))
                    {
                        _unitOfWork.RespuestaPreguntaRepository.Delete(item.IdRespuestaPregunta, usuario);


                    }
                }
                _unitOfWork.Commit();

                foreach (var item in dto.RespuestaPregunta)
                {
                    int? puntajeTipoRespuesta = null;
                    if (dto.Pregunta.IdTipoRespuestaCalificacion.HasValue)
                    {
                        int tipoRes = dto.Pregunta.IdTipoRespuestaCalificacion.Value;
                        if (tipoRes == 1) //Directo
                        {
                            puntajeTipoRespuesta = item.Puntaje;
                        }
                        else if (tipoRes == 2) //Inversa
                        {
                            if (dto.Pregunta.FactorRespuesta.HasValue)
                            {
                                int factorRes = dto.Pregunta.FactorRespuesta.Value;
                                puntajeTipoRespuesta = factorRes - item.Puntaje;
                            }
                        }
                        else //negativo
                        {
                            if (dto.Pregunta.FactorRespuesta.HasValue)
                            {
                                int factorRes = dto.Pregunta.FactorRespuesta.Value;
                                if ((bool)!item.RespuestaCorrecta)
                                    puntajeTipoRespuesta = item.Puntaje - factorRes;
                                else
                                    puntajeTipoRespuesta = item.Puntaje;
                            }
                        }
                    }

                    RespuestaPregunta respuestaPregunta;
                    if (item.IdPregunta > 0)
                    {
                        respuestaPregunta = _unitOfWork.RespuestaPreguntaRepository.ObtenerPorId((int)item.IdPregunta);
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
                        _unitOfWork.RespuestaPreguntaRepository.Update(respuestaPregunta);


                    }
                    else
                    {
                        respuestaPregunta = new RespuestaPregunta()
                        {
                            IdPregunta = pregunta.Id,
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
                        _unitOfWork.RespuestaPreguntaRepository.Add(respuestaPregunta);

                    }
                    _unitOfWork.Commit();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool EliminarPregunta(int id, string usuario)
        {
            try
            {
                var pregunta = _unitOfWork.PreguntumRepository.ObtenerPorId(id);
                if (pregunta != null)
                {
                    var listaAsignacionPreguntaExamen = _unitOfWork.AsignacionPreguntaExamenRepository.ObtenerPorIdPregunta(pregunta.Id);
                    var respuestaPregunta = _unitOfWork.RespuestaPreguntaRepository.ObtenerRespuestaPorIdPregunta(pregunta.Id);
                    if (pregunta.IdPreguntaIntento.HasValue)
                    {
                        _unitOfWork.PreguntaIntentoRepository.Delete(pregunta.IdPreguntaIntento.Value, usuario);
                        _unitOfWork.Commit();

                    }
                    if (listaAsignacionPreguntaExamen != null)
                    {
                        foreach (var item in listaAsignacionPreguntaExamen)
                        {
                            _unitOfWork.AsignacionPreguntaExamenRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();

                        }
                    }
                    if (respuestaPregunta != null)
                    {
                        foreach (var item in respuestaPregunta)
                        {
                            _unitOfWork.RespuestaPreguntaRepository.Delete(item.Id, usuario);
                            _unitOfWork.Commit();

                        }
                    }
                    _unitOfWork.PreguntumRepository.Delete(pregunta.Id, usuario);
                    _unitOfWork.Commit();

                    return true;
                }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        
        public ImportarExcelRespuestaDTO ImportarExcel(RespuestaPreguntaImportadaDTO Dto, string usuario)
        {
            CsvFile file = new CsvFile();
            List<string> listaErrores = new List<string>();
            try
            {
                int indexError = 0;
                int indexTotal = 0;
                var pregunta = _unitOfWork.PreguntumRepository.FirstById(Dto.IdPregunta);
                using (var reader = new StreamReader(Dto.File.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    MissingFieldFound = null,   // ignora si falta un campo
                    HeaderValidated = null      // ignora si el header no coincide
                }))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        int? puntajeTipoRespuesta = null;
                        int? puntaje = csv.GetField<int?>("Puntaje");
                        bool? respuestaCorrecta = csv.GetField<bool?>("RespuestaCorrecta");

                        if (pregunta.IdTipoRespuestaCalificacion.HasValue)
                        {
                            int tipoRes = pregunta.IdTipoRespuestaCalificacion.Value;

                            if (tipoRes == 1) // Directo
                            {
                                puntajeTipoRespuesta = puntaje;
                            }
                            else if (tipoRes == 2) // Inversa
                            {
                                if (pregunta.FactorRespuesta.HasValue)
                                {
                                    int factorRes = pregunta.FactorRespuesta.Value;
                                    puntajeTipoRespuesta = factorRes - puntaje;
                                }
                            }
                            else // Negativo
                            {
                                if (pregunta.FactorRespuesta.HasValue)
                                {
                                    int factorRes = pregunta.FactorRespuesta.Value;
                                    if (respuestaCorrecta.HasValue)
                                    {
                                        if (!respuestaCorrecta.Value)
                                            puntajeTipoRespuesta = puntaje - factorRes;
                                        else
                                            puntajeTipoRespuesta = puntaje;
                                    }
                                }
                            }
                        }

                        indexTotal++;

                        try
                        {
                            var respuestaPregunta = new RespuestaPregunta()
                            {
                                IdPregunta = Dto.IdPregunta,
                                RespuestaCorrecta = csv.GetField<bool?>("RespuestaCorrecta"),
                                NroOrden = csv.GetField<int>("NroOrden"),
                                EnunciadoRespuesta = csv.GetField<string>("EnunciadoRespuesta"),
                                NroOrdenRespuesta = csv.GetField<int?>("NroOrdenRespuesta"),
                                Puntaje = csv.GetField<int?>("Puntaje"),
                                FeedbackPositivo = csv.GetField<string>("FeedbackPositivo"),
                                FeedbackNegativo = csv.GetField<string>("FeedbackNegativo"),
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                PuntajeTipoRespuesta = puntajeTipoRespuesta
                            };

                            _unitOfWork.RespuestaPreguntaRepository.Add(respuestaPregunta);
                        }
                        catch (Exception e)
                        {
                            indexError++;
                            listaErrores.Add("Error en: " + csv.GetField<string>("EnunciadoRespuesta") + " - " + e.Message);
                        }
                    }

                    _unitOfWork.Commit();
                }
                ImportarExcelRespuestaDTO resultadoImportarExcel = new()
                {
                    Total = indexTotal,
                    Correcto = (indexTotal - indexError),
                    Error = indexError,
                    Errores = listaErrores
                };
                return resultadoImportarExcel;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
