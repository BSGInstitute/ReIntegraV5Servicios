using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
    /// Repositorio: TranscripcionLlamadaRepository
    /// Autor: Joseph Llanque
    /// Fecha: 08/03/2025
    /// <summary>
    /// Gestión general de TTranscripcionLlamadum
    /// </summary>
    public class TranscripcionLlamadaRepository : GenericRepository<TTranscripcionLlamadum>, ITranscripcionLlamadaRepository
    {
        private Mapper _mapper;

        public TranscripcionLlamadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTranscripcionLlamadum, TranscripcionLlamada>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFraseCombinadum, FraseCombinada>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFraseReconocidum, FraseReconocida>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDetalleFraseReconocidum, DetalleFraseReconocida>(MemberList.None).ReverseMap();
                cfg.CreateMap<TRecomendacionTranscripcion, RecomendacionTranscripcion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTranscripcionLlamadum MapeoEntidad(TranscripcionLlamada entidad)
        {
            try
            {
                //crea la entidad padre
                TTranscripcionLlamadum modelo = _mapper.Map<TTranscripcionLlamadum>(entidad);

                //mapea los hijos
                if (entidad.FraseCombinada != null && entidad.FraseCombinada.Count > 0)
                {
                    var listadoHijoNivel1 = _mapper.Map<List<TFraseCombinadum>>(entidad.FraseCombinada);
                    foreach (var hijoNivel1 in listadoHijoNivel1)
                    {
                        modelo.TFraseCombinada.Add(hijoNivel1);
                    }
                }
                if (entidad.FraseReconocida != null && entidad.FraseReconocida.Count > 0)
                {
                    //var listadoHijoNivel2 = _mapper.Map<List<TFraseReconocidum>>(entidad.FraseReconocida);
                    var listadoHijoNivel2 =new List<TFraseReconocidum>();
                    foreach (var hijoNivel2 in entidad.FraseReconocida)
                    {
                        var hijoNivel2Aux = new TFraseReconocidum();
                        hijoNivel2Aux = _mapper.Map<TFraseReconocidum>(hijoNivel2);
                        if (hijoNivel2.DetalleFraseReconocida != null && hijoNivel2.DetalleFraseReconocida.Count > 0)
                        {
                            hijoNivel2Aux.TDetalleFraseReconocida = new List<TDetalleFraseReconocidum>();
                            var listadoHijoNivel3 = _mapper.Map<List<TDetalleFraseReconocidum>>(hijoNivel2.DetalleFraseReconocida);
                            foreach (var hijoNivel3 in listadoHijoNivel3)
                            {
                                hijoNivel2Aux.TDetalleFraseReconocida.Add(hijoNivel3);
                            }
                        }
                        listadoHijoNivel2.Add(hijoNivel2Aux);
                    }
                    modelo.TFraseReconocida=listadoHijoNivel2;
                }
                if (entidad.RecomendacionTranscripcions != null && entidad.RecomendacionTranscripcions.Count > 0)
                {
                    var listadoHijoNivel3 = _mapper.Map<List<TRecomendacionTranscripcion>>(entidad.RecomendacionTranscripcions);
                    foreach (var hijoNivel3 in listadoHijoNivel3)
                    {
                        modelo.TRecomendacionTranscripcions.Add(hijoNivel3);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTranscripcionLlamadum Add(TranscripcionLlamada entidad)
        {
            try
            {
                var TranscripcionLlamada = MapeoEntidad(entidad);
                base.Insert(TranscripcionLlamada);
                actualizarTranscripcionLlamada(TranscripcionLlamada.IdLlamadaWebphoneCruceCentralTresCx);
                return TranscripcionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTranscripcionLlamadum Update(TranscripcionLlamada entidad)
        {
            try
            {
                var TranscripcionLlamada = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TranscripcionLlamada.RowVersion = entidadExistente.RowVersion;
                base.Update(TranscripcionLlamada);
                return TranscripcionLlamada;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TTranscripcionLlamadum> Add(IEnumerable<TranscripcionLlamada> listadoEntidad)
        {
            try
            {
                List<TTranscripcionLlamadum> listado = new List<TTranscripcionLlamadum>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TTranscripcionLlamadum> Update(IEnumerable<TranscripcionLlamada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTranscripcionLlamadum> listado = new List<TTranscripcionLlamadum>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Joseph Llanque
        /// Fecha: 04/08/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public async Task<IEnumerable<TranscripcionDetalleDTO>> ObtenerTranscripcion(int idLlamada)
        {
            try
            {
                var detalleTranscripcion = new List<TranscripcionDetalleDTO>();
                var query = @"SELECT Confidence	,
		                                DetalleFraseReconocidaId,
		                                DFR_Display,
		                                DFR_ITN,
		                                DFR_Lexical,
		                                DFR_MaskedITN,
		                                DFR_Sentiment,
		                                Duration,
		                                DurationInTicks,
		                                DurationInTicksFraseReconocida,
		                                DurationMilliseconds,
		                                DurationMillisecondsFraseReconocida,
		                                Estado,
		                                FC_Channel,
		                                FC_Display,
		                                FC_ITN,
		                                FC_Lexical,
		                                FC_MaskedITN,
		                                FechaCreacion,
		                                FechaModificacion,
		                                FR_Channel,
		                                FR_Duration,
		                                FraseCombinadaId,
		                                FraseReconocidaId,
		                                IdLlamadaWebphoneCruceCentralTresCx,
		                                OcurrenciaConsistente,
		                                Offset,
		                                OffsetInTicks,
		                                OffsetMilliseconds,
		                                RecognitionStatus,
		                                Recomendacion,
		                                RecomendacionId,
		                                RT_Estado,
		                                RT_FechaCreacion,
		                                RT_FechaModificacion,
		                                RT_UsuarioCreacion,
		                                RT_UsuarioModificacion,
		                                Source,
		                                Speaker,
		                                Summary,
		                                Timestamp,
		                                TranscripcionId,
		                                UsuarioCreacion,
		                                UsuarioModificacion 
                                FROM com.V_TranscripcionCompletaDetalle 
                                WHERE IdLlamadaWebphoneCruceCentralTresCx = @idLlamada";
                var resultado = _dapperRepository.QueryDapper(query, new { idLlamada = idLlamada });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    detalleTranscripcion = JsonConvert.DeserializeObject<List<TranscripcionDetalleDTO>>(resultado);
                }
                return detalleTranscripcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 04/08/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de la llamada a transcrita
        /// </summary> 
        /// <returns> bool </returns>
        public bool actualizarTranscripcionLlamada(int idLlamada)
        {
            try
            {
                var parameters = new { IdLlamada = idLlamada };
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ActualizarLlamadaTranscrita", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
