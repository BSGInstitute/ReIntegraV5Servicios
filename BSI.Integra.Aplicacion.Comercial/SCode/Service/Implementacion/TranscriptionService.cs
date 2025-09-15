using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial.TranscriptionDTO;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    public class TranscriptionService : ITranscriptionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TranscriptionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TranscripcionLlamada, TranscriptionData>(MemberList.None).ReverseMap();
                cfg.CreateMap<FraseCombinada, CombinedRecognizedPhrase>(MemberList.None).ReverseMap();
                cfg.CreateMap<FraseReconocida, RecognizedPhrase>(MemberList.None).ReverseMap();
                cfg.CreateMap<DetalleFraseReconocida, NBestOption>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }

        private void AsignarValoresComunes(dynamic entity)
        {
            entity.UsuarioCreacion = "webHookIa";
            entity.UsuarioModificacion = "webHookIa";
            entity.FechaCreacion = DateTime.Now;
            entity.FechaModificacion = DateTime.Now;
            entity.Estado = true;
        }

        public async Task InsertTranscriptionDataAsync(TranscriptionWebhookPayloadDTO payload)
        {
            try
            {
                if (payload.Transcription == null)
                    throw new ArgumentNullException("La propiedad Transcription es nula.");
                payload.Transcription.OcurrenciaConsistente = payload.Transcription.OcurrenciaConsistente.ToUpper() == "SI" ? "True" : "False";
                var transcriptionEntity = _mapper.Map<TranscripcionLlamada>(payload.Transcription);
                transcriptionEntity.IdLlamadaWebphoneCruceCentralTresCx = int.Parse(payload.IdLlamada);
                AsignarValoresComunes(transcriptionEntity);

                
                

                // Mapear e insertar las CombinedRecognizedPhrases
                if (payload.Transcription.CombinedRecognizedPhrases != null)
                {
                    transcriptionEntity.FraseCombinada = new List<FraseCombinada>();
                    var combinedEntities = _mapper.Map<List<FraseCombinada>>(payload.Transcription.CombinedRecognizedPhrases);
                    foreach (var combined in combinedEntities)
                    {
                        //combined.IdTranscripcionLlamada = transcriptionEntity.Id;
                        AsignarValoresComunes(combined);
                        transcriptionEntity.FraseCombinada.Add(combined);
                    }
                }

                // Mapear e insertar las RecognizedPhrases y sus opciones nBest
                if (payload.Transcription.RecognizedPhrases != null)
                {
                    transcriptionEntity.FraseReconocida = new List<FraseReconocida>();
                    foreach (var recognized in payload.Transcription.RecognizedPhrases)
                    {
                        var recognizedEntity = _mapper.Map<FraseReconocida>(recognized);
                        //recognizedEntity.IdTranscripcionLlamada = transcriptionEntity.Id;
                        AsignarValoresComunes(recognizedEntity);
                        //transcriptionEntity.FraseReconocida.Add(recognizedEntity);

                        if (recognized.NBest != null)
                        {
                            recognizedEntity.DetalleFraseReconocida = new List<DetalleFraseReconocida>();
                            var nBestEntities = _mapper.Map<List<DetalleFraseReconocida>>(recognized.NBest);
                            foreach (var nBest in nBestEntities)
                            {
                                //nBest.IdFraseReconocida = recognizedEntity.Id;
                                AsignarValoresComunes(nBest);
                                recognizedEntity.DetalleFraseReconocida.Add(nBest);
                            }

                        }
                        transcriptionEntity.FraseReconocida.Add(recognizedEntity);

                    }

                }

                // Mapear e insertar las Recomendaciones
                if (payload.Transcription.Recomendaciones != null)
                {
                    //var recommendationEntities = _mapper.Map<List<RecomendacionTranscripcion>>(payload.Transcription.Recomendaciones);
                    transcriptionEntity.RecomendacionTranscripcions = new List<RecomendacionTranscripcion>();

                    foreach (var rec in payload.Transcription.Recomendaciones)
                    {
                        //rec.IdTranscripcionLlamada = transcriptionEntity.Id;
                        var recomendacionTranscripcion = new RecomendacionTranscripcion();
                        AsignarValoresComunes(recomendacionTranscripcion);
                        recomendacionTranscripcion.Recomendacion = rec;
                        transcriptionEntity.RecomendacionTranscripcions.Add(recomendacionTranscripcion);
                    }
                }

                _unitOfWork.TranscripcionLlamadaRepository.Add(transcriptionEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 25/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<TranscripcionCompletaResponseDto> </returns>
        public async Task<TranscripcionCompletaResponseDTO> ObtenerTranscripcion(int idLlamada)
        {
            try
            {
                var data = (await _unitOfWork.TranscripcionLlamadaRepository.ObtenerTranscripcion(idLlamada)).ToList();
                if (!data.Any()) return null;

                var transcripcion = data.First();

                var dto = new TranscripcionCompletaResponseDTO
                {
                    IdLlamada = transcripcion.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                    IdActividadDetalle = null,
                    Status = "success",
                    Transcription = new TranscriptionDto
                    {
                        Source = transcripcion.Source,
                        Timestamp = transcripcion.Timestamp,
                        DurationInTicks = transcripcion.DurationInTicks ?? 0,
                        DurationMilliseconds = transcripcion.DurationMilliseconds ?? 0,
                        Duration = transcripcion.Duration,
                        Summary = transcripcion.Summary,
                        Ocurrencia_Consistente = transcripcion.OcurrenciaConsistente.HasValue && transcripcion.OcurrenciaConsistente.Value ? "si" : "no",
                        ComentarioConsistenciaOcurrencia = transcripcion?.ComentarioConsistenciaOcurrencia,

                        CombinedRecognizedPhrases = data
                            .Where(x => x.FraseCombinadaId != null)
                            .GroupBy(x => x.FraseCombinadaId)
                            .Select(g => new CombinedRecognizedPhraseDto
                            {
                                Channel = g.First().FC_Channel,
                                Lexical = g.First().FC_Lexical,
                                Itn = g.First().FC_ITN,
                                MaskedITN = g.First().FC_MaskedITN,
                                Display = g.First().FC_Display
                            }).ToList(),

                        RecognizedPhrases = data
                            .Where(x => x.FraseReconocidaId != null)
                            .GroupBy(x => x.FraseReconocidaId)
                            .Select(g => new RecognizedPhraseDto
                            {
                                RecognitionStatus = g.First().RecognitionStatus,
                                Channel = g.First().FR_Channel,
                                Speaker = g.First().Speaker,
                                Offset = g.First().Offset,
                                Duration = g.First().FR_Duration,
                                OffsetInTicks = g.First().OffsetInTicks,
                                DurationInTicks = g.First().DurationInTicksFraseReconocida,
                                DurationMilliseconds = g.First().DurationMillisecondsFraseReconocida,
                                OffsetMilliseconds = g.First().OffsetMilliseconds,
                                NBest = g.Select(x => new NBestDto
                                {
                                    Confidence = x.Confidence,
                                    Lexical = x.DFR_Lexical,
                                    Itn = x.DFR_ITN,
                                    MaskedITN = x.DFR_MaskedITN,
                                    Display = x.DFR_Display,
                                    Sentiment = x.DFR_Sentiment
                                }).ToList()
                            }).ToList(),

                        Recomendaciones = data
                            .Where(x => x.RecomendacionId != null)
                            .Select(x => x.Recomendacion.ToString())
                            .Distinct()
                            .ToList()
                    }
                };

                return dto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}