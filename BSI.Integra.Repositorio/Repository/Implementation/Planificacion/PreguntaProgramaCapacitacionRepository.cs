using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{

    /// Repositorio: PreguntaProgramaCapacitacionRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 14/06/2023
    /// <summary>
    /// Gestión general de T_PreguntaProgramaCapacitacion
    /// </summary>
    public class PreguntaProgramaCapacitacionRepository : GenericRepository<TPreguntaProgramaCapacitacion>, IPreguntaProgramaCapacitacionRepository
    {
        private Mapper _mapper;
        public PreguntaProgramaCapacitacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaProgramaCapacitacion, PreguntaProgramaCapacitacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaProgramaCapacitacion MapeoEntidad(PreguntaProgramaCapacitacion entidad)
        {
            try
            {
                //Mapea la entidad padre
                TPreguntaProgramaCapacitacion modelo = _mapper.Map<TPreguntaProgramaCapacitacion>(entidad);

                //if (entidad.PGeneralPreguntaProgramaCapacitacions != null && entidad.PGeneralPreguntaProgramaCapacitacions.Count > 0)
                //    modelo.TPgeneralPreguntaProgramaCapacitacions = _mapper.Map<List<TPgeneralPreguntaProgramaCapacitacion>>(entidad.PGeneralPreguntaProgramaCapacitacions);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaProgramaCapacitacion Add(PreguntaProgramaCapacitacion entidad)
        {
            try
            {
                var PreguntaProgramaCapacitacion = MapeoEntidad(entidad);
                base.Insert(PreguntaProgramaCapacitacion);
                return PreguntaProgramaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaProgramaCapacitacion Update(PreguntaProgramaCapacitacion entidad)
        {
            try
            {
                var PreguntaProgramaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaProgramaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaProgramaCapacitacion);
                return PreguntaProgramaCapacitacion;
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


        public IEnumerable<TPreguntaProgramaCapacitacion> Add(IEnumerable<PreguntaProgramaCapacitacion> listadoEntidad)
        {
            try
            {
                List<TPreguntaProgramaCapacitacion> listado = new List<TPreguntaProgramaCapacitacion>();
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

        public IEnumerable<TPreguntaProgramaCapacitacion> Update(IEnumerable<PreguntaProgramaCapacitacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaProgramaCapacitacion> listado = new List<TPreguntaProgramaCapacitacion>();
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
        /// Autor Modificacion: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <summary>
        /// Obtiene la pregunta enunciado por el IdPGeneral
        /// </summary>
        public IEnumerable<PreguntaPorProgramaDTO> ObtenerEnunciadoPreguntaPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _queryfiltrocapitulo = @"Select Id,EnunciadoPregunta FROM pla.V_PreguntasPorPrograma Where OrdenFilaCapitulo IS NULL AND OrdenFilaSesion IS NULL AND IdPgeneral=@IdPGeneral";
                var SubfiltroCapitulo = _dapperRepository.QueryDapper(_queryfiltrocapitulo, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(SubfiltroCapitulo) && !SubfiltroCapitulo.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaPorProgramaDTO>>(SubfiltroCapitulo);

                return new List<PreguntaPorProgramaDTO>();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <param name="seccion"> Orden del capitulo </param>
        /// <param name="fila"> Orden final de sesion </param>
        /// <summary>
        /// Obtiene pregunta programa estructura agrupado por filtros
        /// </summary>
        public IEnumerable<GrupoPreguntaProgramaCapacitacionDTO> ObtenerConfiguracionGrupoPreguntasEstructura(int idPGeneral, int seccion, int fila)
        {
            string _query = "Select IdPgeneral,GrupoPregunta,IdTipoVista,Segundos From pla.V_ListadoGrupoPreguntaPorEstructura Where IdPgeneral=@IdPGeneral AND OrdenFilaCapitulo=@Seccion AND OrdenFilaSesion=@Fila";
            string query = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral, Seccion = seccion, Fila = fila });
            if (!string.IsNullOrEmpty(query) && !query.Equals("null"))
                return JsonConvert.DeserializeObject<IEnumerable<GrupoPreguntaProgramaCapacitacionDTO>>(query);
            return new List<GrupoPreguntaProgramaCapacitacionDTO>();
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21-07-23
        /// Versión: 1
        /// <summary>
        /// Obtiene el reporte de preguntas interactivas para su exportación en excel
        /// </summary>
        /// <returns>List<ReporteExcelPreguntasInteractivasPrevioDTO></returns>
        public async Task<IEnumerable<ReporteExcelPreguntasInteractivasPrevioDTO>> ObtenerReportePreguntasInteractivasExportacionExcel()
        {
            try
            {
                var query = @"SELECT  Id,IdPgeneral IdPGeneral,IdPEspecifico,OrdenFilaCapitulo,GrupoPregunta,IdTipoMarcador,ValorMarcador,OrdenPreguntaGrupo,
					IdTipoRespuesta,IdPreguntaTipo,EnunciadoPregunta,MinutosPorPregunta,RespuestaAleatoria,ActivarFeedBackRespuestaCorrecta,
					ActivarFeedBackRespuestaIncorrecta,MostrarFeedbackInmediato,MostrarFeedbackPorPregunta,NumeroMaximoIntento,ActivarFeedbackMaximoIntento,
					MensajeFeedback,IdTipoRespuestaCalificacion,FactorRespuesta,RespuestaCorrecta,NroOrden,EnunciadoRespuesta,Puntaje,FeedbackPositivo,
					FeedbackNegativo,IdPreguntaIntento,OrdenFilaSesion 
					FROM [pw].[V_PW_ObtenerReportePreguntasInteractivas]";
                var res = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<ReporteExcelPreguntasInteractivasPrevioDTO>>(res);
                return new List<ReporteExcelPreguntasInteractivasPrevioDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <param name="id"> (PK) </param>
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        public PreguntaProgramaCapacitacion ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdPgeneral,
                                       IdPEspecifico IdPespecifico,
                                       OrdenFilaCapitulo,
                                       OrdenFilaSesion,
                                       IdTipoRespuesta,
                                       IdPreguntaEscalaValor,
                                       EnunciadoPregunta,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       RequiereTiempo,
                                       MinutosPorPregunta,
                                       RespuestaAleatoria,
                                       ActivarFeedBackRespuestaCorrecta,
                                       ActivarFeedBackRespuestaIncorrecta,
                                       MostrarFeedbackInmediato,
                                       MostrarFeedbackPorPregunta,
                                       IdPreguntaIntento,
                                       IdPreguntaTipo,
                                       IdTipoRespuestaCalificacion,
                                       FactorRespuesta,
                                       GrupoPregunta,
                                       IdTipoMarcador,
                                       ValorMarcador,
                                       OrdenPreguntaGrupo
                                FROM ope.T_PreguntaProgramaCapacitacion
                                WHERE Estado = 1
                                      AND Id = @Id;";
                var res = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<PreguntaProgramaCapacitacion>(res);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <param name="grupoPregunta"> GrupoPregunta </param>
        /// <summary>
        /// Obtiene los registros asociados al IdPGeneral y GrupoPregunta
        /// </summary>
        /// <returns> IEnumerable<PreguntaProgramaCapacitacion>  </returns>
        public IEnumerable<PreguntaProgramaCapacitacion> ObtenerPorIdPGeneralYGrupoPregunta(int idPGeneral, string grupoPregunta)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdPgeneral,
                                       IdPEspecifico IdPespecifico,
                                       OrdenFilaCapitulo,
                                       OrdenFilaSesion,
                                       IdTipoRespuesta,
                                       IdPreguntaEscalaValor,
                                       EnunciadoPregunta,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       RequiereTiempo,
                                       MinutosPorPregunta,
                                       RespuestaAleatoria,
                                       ActivarFeedBackRespuestaCorrecta,
                                       ActivarFeedBackRespuestaIncorrecta,
                                       MostrarFeedbackInmediato,
                                       MostrarFeedbackPorPregunta,
                                       IdPreguntaIntento,
                                       IdPreguntaTipo,
                                       IdTipoRespuestaCalificacion,
                                       FactorRespuesta,
                                       GrupoPregunta,
                                       IdTipoMarcador,
                                       ValorMarcador,
                                       OrdenPreguntaGrupo
                                FROM ope.T_PreguntaProgramaCapacitacion
                                WHERE Estado = 1
                                      AND IdPgeneral = @IdPgeneral AND GrupoPregunta=@GrupoPregunta;";
                var res = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPGeneral, GrupoPregunta = grupoPregunta });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaProgramaCapacitacion>>(res);
                else
                    return new List<PreguntaProgramaCapacitacion>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <param name="idPgeneral"> (PK) de T_PGeneral </param>
        /// <param name="grupoPregunta"> GrupoPregunta </param>
        /// <summary>
        /// Obtiene los registros asociados al IdPGeneral y GrupoPregunta
        /// </summary>
        /// <returns> IEnumerable<PreguntaProgramaCapacitacion>  </returns>
        public IEnumerable<ListadoPreguntaPorEstructuraDTO> ObtenerPorEstructura(int idPgeneral, string grupoPregunta)
        {
            try
            {
                string query = @"SELECT Id,
                                       IdPgeneral,
                                       OrdenFilaCapitulo,
                                       OrdenFilaSesion,
                                       GrupoPregunta,
                                       IdTipoVista,
                                       Segundos,
                                       OrdenPreguntaGrupo,
                                       EnunciadoPregunta,
                                       RespuestaAleatoria,
                                       MostrarFeedbackInmediato,
                                       MostrarFeedbackPorPregunta,
                                       NumeroMaximoIntento,
                                       TipoRespuesta
                                FROM pla.V_ListadoPreguntaPorEstructura
                                WHERE IdPgeneral = @IdPgeneral
                                      AND GrupoPregunta = @GrupoPregunta;";
                var res = _dapperRepository.QueryDapper(query, new { IdPgeneral = idPgeneral, GrupoPregunta = grupoPregunta });
                if (!string.IsNullOrEmpty(res) && !res.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ListadoPreguntaPorEstructuraDTO>>(res);
                return new List<ListadoPreguntaPorEstructuraDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el listado de niveles de dificultad disponibles para preguntas
        /// </summary>
        /// <returns>Lista de objetos de tipo PreguntaProgramaCapacitacionDificultadDTO</returns>
        public List<PreguntaProgramaCapacitacionDificultadDTO> ObtenerDificultades()
        {
            try
            {
                var res = _dapperRepository.QuerySPDapper("ope.SP_TPreguntaProgramaCapacitacionDificultad_Obtener", null);
                if (!string.IsNullOrEmpty(res) && res != "null" && !res.Contains("[]"))
                    return JsonConvert.DeserializeObject<List<PreguntaProgramaCapacitacionDificultadDTO>>(res)
                                     .OrderBy(x => x.IdPreguntaProgramaCapacitacionDificultad)
                                     .ToList();
                return new List<PreguntaProgramaCapacitacionDificultadDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza el nivel de dificultad de una pregunta de programa de capacitacion
        /// </summary>
        /// <param name="id">Id de la pregunta (PK)</param>
        /// <param name="idPreguntaProgramaCapacitacionDificultad">Id del nivel de dificultad</param>
        /// <param name="usuarioModificacion">Usuario que realiza la modificacion</param>
        public void ActualizarDificultad(int id, int idPreguntaProgramaCapacitacionDificultad, string usuarioModificacion)
        {
            var parametros = new
            {
                IdPreguntaProgramaCapacitacion = id,
                IdPreguntaProgramaCapacitacionDificultad = idPreguntaProgramaCapacitacionDificultad,
                UsuarioModificacion = usuarioModificacion
            };
            _dapperRepository.QuerySPDapper("ope.SP_TPreguntaProgramaCapacitacion_ActualizarDificultad", parametros);
        }

        /// <summary>
        /// Obtiene el nivel de dificultad asociado a una pregunta de programa de capacitacion
        /// </summary>
        /// <param name="id">Id de la pregunta (PK)</param>
        /// <returns>Objeto de tipo PreguntaProgramaCapacitacionDificultadDTO o null si no existe</returns>
        public PreguntaProgramaCapacitacionDificultadDTO ObtenerDificultadPorIdPregunta(int id)
        {
            var res = _dapperRepository.QuerySPFirstOrDefault("ope.SP_TPreguntaProgramaCapacitacionObtenerDificultadPorId", new { IdPreguntaProgramaCapacitacion = id });
            if (!string.IsNullOrEmpty(res) && res != "null")
                return JsonConvert.DeserializeObject<PreguntaProgramaCapacitacionDificultadDTO>(res);
            return null;
        }

        /// <summary>
		/// Obtiene todas las preguntas de programa de capacitacion registradas en el sistema
		/// </summary>
		/// <returns>Lista de objetos de tipo PreguntaProgramaCapacitacionRegistradaDTO</returns>
		public List<PreguntaProgramaCapacitacionRegistradaDTO> ObtenerPreguntasRegistradas()
        {
            try
            {
                var query = "SELECT Id, Enunciado, IdTipoRespuesta, IdPreguntaTipo, MinutosPorPregunta, RespuestaAleatoria, ActivarFeedBackRespuestaCorrecta, ActivarFeedBackRespuestaIncorrecta, MostrarFeedbackInmediato, MostrarFeedbackPorPregunta, NumeroMaximoIntento, ActivarFeedbackMaximoIntento, MensajeFeedbackIntento, IdPGeneral, IdPEspecifico, PGeneral, IdTipoRespuestaCalificacion, FactorRespuesta, IdCapitulo, IdSesion, GrupoPregunta, IdTipoMarcador, ValorMarcador, OrdenPreguntaGrupo, IdPreguntaIntento FROM [ope].[V_TPreguntaProgramaCapacitacion_ObtenerPreguntasRegistradas] WHERE Estado = 1 AND RowNumber = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<List<PreguntaProgramaCapacitacionRegistradaDTO>>(res);
                else
                    return new List<PreguntaProgramaCapacitacionRegistradaDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
