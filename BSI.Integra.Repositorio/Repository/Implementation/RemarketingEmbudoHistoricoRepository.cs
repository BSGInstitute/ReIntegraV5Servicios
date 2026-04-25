using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RemarketingEmbudoHistoricoRepository
    /// Autor: Max Mantilla
    /// Fecha: 06/01/2026
    /// <summary>
    /// Gestión general de T_RemarketingEmbudoHistorico
    /// </summary>
    public class RemarketingEmbudoHistoricoRepository : GenericRepository<TRemarketingEmbudoHistorico>, IRemarketingEmbudoHistoricoRepository
    {
        private Mapper _mapper;

        public RemarketingEmbudoHistoricoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRemarketingEmbudoHistorico, RemarketingEmbudoHistorico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TRemarketingEmbudoHistorico MapeoEntidad(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                //crea la entidad padre
                TRemarketingEmbudoHistorico modelo = _mapper.Map<TRemarketingEmbudoHistorico>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemarketingEmbudoHistorico Add(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                var RemarketingEmbudoHistorico = MapeoEntidad(entidad);
                base.Insert(RemarketingEmbudoHistorico);
                return RemarketingEmbudoHistorico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRemarketingEmbudoHistorico Update(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                var RemarketingEmbudoHistorico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RemarketingEmbudoHistorico.RowVersion = entidadExistente.RowVersion;

                base.Update(RemarketingEmbudoHistorico);
                return RemarketingEmbudoHistorico;
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


        public IEnumerable<TRemarketingEmbudoHistorico> Add(IEnumerable<RemarketingEmbudoHistorico> listadoEntidad)
        {
            try
            {
                List<TRemarketingEmbudoHistorico> listado = new List<TRemarketingEmbudoHistorico>();
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

        public IEnumerable<TRemarketingEmbudoHistorico> Update(IEnumerable<RemarketingEmbudoHistorico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRemarketingEmbudoHistorico> listado = new List<TRemarketingEmbudoHistorico>();
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

        public List<OportunidadRemarketingEmbudoDTO> ObtenerInformacionOportunidadRemarketing(int Pagina, int RegistrosPorPagina, DateTime? FechaCorte = null)
        {
            try
            {
                List<OportunidadRemarketingEmbudoDTO> informacionOportunidad = new List<OportunidadRemarketingEmbudoDTO>();

                var query = "ia.SP_RemarketingEmbudoInformacionOportunidad";  // Cambié el nombre para consistencia
                var parametros = new
                {
                    FechaCorte,
                    Pagina,
                    RegistrosPorPagina
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    informacionOportunidad = JsonConvert.DeserializeObject<List<OportunidadRemarketingEmbudoDTO>>(resultado)!;
                    return informacionOportunidad;
                }

                return new List<OportunidadRemarketingEmbudoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerInformacionOportunidadRemarketing: {ex.Message}", ex);
            }
        }
        public long ObtenerInformacionOportunidadRemarketingTotal(DateTime? FechaCorte = null)
        {
            try
            {
                LongTotalDTO valores = new LongTotalDTO();
                var query = @"ia.SP_RemarketingEmbudoInformacionOportunidadTotal";

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    FechaCorte
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    valores = JsonConvert.DeserializeObject<LongTotalDTO>(resultado);
                    return valores.Total;
                }
                return 0;
            }
            catch (Exception e)
            {
                // No solo relanzar, agregar contexto
                return 0;
            }
        }
        public List<RemarketingEmbudoNivelDescripcionDTO> ObtenerInformacionRemarketingEmbudoNivel()
        {
            try
            {
                List<RemarketingEmbudoNivelDescripcionDTO> informacionRemarketingEmbudoNivel = new List<RemarketingEmbudoNivelDescripcionDTO>();
                var query = @"SELECT Id,Codigo,Nombre FROM ia.T_RemarketingEmbudoNivel";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    informacionRemarketingEmbudoNivel = JsonConvert.DeserializeObject<List<RemarketingEmbudoNivelDescripcionDTO>>(resultado);
                    return informacionRemarketingEmbudoNivel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void RegistrarEmbudoRemarketing(int IdRemarketingEmbudoNivel, int IdAlumno, DateTime FechaClasificacion, string Usuario)
        {
            try
            {
                var query = "ia.SP_TRemarketingEmbudoHistorico_Insertar";

                var parametros = new
                {
                    IdRemarketingEmbudoNivel = IdRemarketingEmbudoNivel,
                    IdAlumno = IdAlumno,
                    FechaClasificacion = FechaClasificacion,
                    Usuario = Usuario
                };
                _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Error al insertar en RegistrarEmbudoRemarketing", ex);
            }
        }
        public List<RemarketingEmbudoNivelLlamadaEfectivaDTO> ObtenerLlamadasEfectivasOportunidadAlumno()
        {
            try
            {
                var query = "ia.SP_RemarketingEmbudoObtenerLlamadasEfectivas";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<RemarketingEmbudoNivelLlamadaEfectivaDTO>>(resultado)!;
                }
                return new List<RemarketingEmbudoNivelLlamadaEfectivaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerColorPerfilProgramaPorIdOportunidad: {ex.Message}", ex);
            }
        }
        public List<RemarketingEmbudoEsquemaNivelDTO> ObtenerNivelEsquemaEmbudoRemarketing()
        {
            try
            {
                List<RemarketingEmbudoEsquemaNivelDTO> informacionRemarketingEmbudoNivel = new List<RemarketingEmbudoEsquemaNivelDTO>();
                var query = @"SELECT REN.Id AS IdNivel,Ren.Nombre AS Nivel,REE.Id AS IdEsquema,REE.Nombre AS Esquema, REN.IdRemarketingEmbudoEsquema  
                            FROM ia.T_RemarketingEmbudoNivel AS REN
                            INNER JOIN ia.T_RemarketingEmbudoEsquema AS REE ON REE.Id=REN.IdRemarketingEmbudoEsquema AND REE.Estado=1
                            WHERE REN.Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    informacionRemarketingEmbudoNivel = JsonConvert.DeserializeObject<List<RemarketingEmbudoEsquemaNivelDTO>>(resultado);
                    return informacionRemarketingEmbudoNivel;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<RemarketingEmbudoNivelInteraccionProgresivoDTO> ObtenerInteraccionFormularioProgresivo()
        {
            try
            {
                var query = @"SELECT Correo, FechaCreacion  
                            FROM [192.168.2.5].integraDB_PortalWeb.mkt.T_ProgressiveProfilingCodigoDescuentoCorreo
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<RemarketingEmbudoNivelInteraccionProgresivoDTO>>(resultado)!;
                }
                return new List<RemarketingEmbudoNivelInteraccionProgresivoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerInteraccionFormularioProgresivo: {ex.Message}", ex);
            }
        }
        public List<OportunidadScoreDTO> ObtenerScoreOportunidadAlumno(int registrosPorPagina)
        {
            try
            {
                var todosLosScores = new List<OportunidadScoreDTO>();
                var totalRegistros = 0;
                var queryCount = "SELECT COUNT(*) AS Valor FROM ia.T_RemarketingScoreContacto WHERE estado = 1";
                var resultadoCount = _dapperRepository.FirstOrDefault(queryCount, null);
                if (!string.IsNullOrEmpty(resultadoCount) && resultadoCount != "[]" && resultadoCount != "null")
                {
                    var total = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoCount);
                    totalRegistros = total.Valor.Value;
                }
                if (totalRegistros == 0)
                    return todosLosScores;

                int totalPaginas = (int)Math.Ceiling((double)totalRegistros / registrosPorPagina);
                for (int paginaActual = 1; paginaActual <= totalPaginas; paginaActual++)
                {
                    var parametros = new
                    {
                        NroPagina = paginaActual,
                        RegistrosPagina = registrosPorPagina
                    };

                    var query = "ia.SP_RemarketingObtenerScoresEnBloque";
                    var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                    if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                    {
                        var scoresPagina = JsonConvert.DeserializeObject<List<OportunidadScoreDTO>>(resultado);

                        if (scoresPagina != null && scoresPagina.Any())
                        {
                            todosLosScores.AddRange(scoresPagina);
                        }
                    }
                }
                return todosLosScores;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerTodosScoresOportunidadAlumno: {ex.Message}", ex);
            }
        }
        public List<OportunidadScoreDTO> ObtenerScoreOportunidadAlumnoIndividual(int IdOportunidad)
        {
            try
            {
                var scoreAlumno = new List<OportunidadScoreDTO>();
                var query = "ia.SP_RemarketingObtenerScoreOportunidad";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    scoreAlumno = JsonConvert.DeserializeObject<List<OportunidadScoreDTO>>(resultado);
                    return scoreAlumno;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#OR-OCPPIO@Error en ObtenerTodosScoresOportunidadAlumno: {ex.Message}", ex);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la última interacción en el portal para todos los alumnos.
        /// </summary>
        /// <returns>List&lt;InteracccionPortalUltimaInteraccionDTO&gt;</returns>
        public List<InteracccionPortalUltimaInteraccionDTO> ObtenerInteraccionPortalUltimaInteraccion()
        {
            try
            {
                var query = "mkt.SP_InteraccionPortalUltimaInteraccion";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<InteracccionPortalUltimaInteraccionDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerInteraccionPortalUltimaInteraccion: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la última interacción en el portal para un alumno específico.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno.</param>
        /// <returns>InteracccionPortalUltimaInteraccionDTO</returns>
        public InteracccionPortalUltimaInteraccionDTO ObtenerInteraccionPortalUltimaInteraccionAlumno(int IdAlumno)
        {
            try
            {
                var query = "mkt.SP_InteraccionPortalUltimaInteraccion";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<InteracccionPortalUltimaInteraccionDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerInteraccionPortalUltimaInteraccionAlumno: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de ocurrencias ejecutadas para todos los alumnos.
        /// </summary>
        /// <returns>List&lt;ActividadEjecutadaReporteDTO&gt;</returns>
        public List<ActividadEjecutadaReporteDTO> ObtenerOcurrenciaEjecutada()
        {
            try
            {
                var query = "mkt.SP_OportunidadUltimaActividadEjecutadaConteo";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<ActividadEjecutadaReporteDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerOcurrenciaEjecutada: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el reporte de ocurrencias ejecutadas para un alumno específico.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno.</param>
        /// <returns>ActividadEjecutadaReporteDTO</returns>
        public ActividadEjecutadaReporteDTO ObtenerOcurrenciaEjecutadaAlumno(int IdAlumno)
        {
            try
            {
                var query = "mkt.SP_OportunidadUltimaActividadEjecutadaConteo";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ActividadEjecutadaReporteDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerOcurrenciaEjecutadaAlumno: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de centro de costo por oportunidad para todos los alumnos.
        /// </summary>
        /// <returns>List&lt;AlumnoCentroCostoRegistroDTO&gt;</returns>
        public List<AlumnoCentroCostoRegistroDTO> ObtenerCentroCostoRegistro()
        {
            try
            {
                var query = "mkt.SP_OportunidadRegistroPortalConteo";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<AlumnoCentroCostoRegistroDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerCentroCostoRegistro: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de centro de costo por oportunidad para un alumno específico.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno.</param>
        /// <returns>AlumnoCentroCostoRegistroDTO</returns>
        public AlumnoCentroCostoRegistroDTO ObtenerCentroCostoRegistroAlumno(int IdAlumno)
        {
            try
            {
                var query = "mkt.SP_OportunidadRegistroPortalConteo";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<AlumnoCentroCostoRegistroDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerCentroCostoRegistroAlumno: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el último mensaje de WhatsApp enviado para todos los alumnos.
        /// </summary>
        /// <returns>List&lt;WhatsappUltimoMensajeEnviadoDTO&gt;</returns>
        public List<WhatsappUltimoMensajeEnviadoDTO> ObtenerWhatsAppMensajeUltimo()
        {
            try
            {
                var query = "mkt.SP_AlumnoMensajeWhatsappUltimoEnvio";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<WhatsappUltimoMensajeEnviadoDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerWhatsAppMensajeUltimo: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el último mensaje de WhatsApp enviado para un alumno específico.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno.</param>
        /// <returns>WhatsappUltimoMensajeEnviadoDTO</returns>
        public WhatsappUltimoMensajeEnviadoDTO ObtenerWhatsAppMensajeUltimoAlumno(int IdAlumno)
        {
            try
            {
                var query = "mkt.SP_AlumnoMensajeWhatsappUltimoEnvio";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<WhatsappUltimoMensajeEnviadoDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerWhatsAppMensajeUltimoAlumno: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la información de la última oportunidad con su último cambio de fase para todos los alumnos,
        /// utilizada como base para la evaluación del embudo de remarketing.
        /// </summary>
        /// <returns>List&lt;OportunidadUltimoCambioDTO&gt;</returns>
        public List<OportunidadUltimoCambioDTO> ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambio()
        {
            try
            {
                var query = "mkt.SP_RemarketingEmbudoInformacionOportunidadUltimoCambio";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<OportunidadUltimoCambioDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambio: {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene la información de la última oportunidad con su último cambio de fase para un alumno específico,
        /// utilizada como base para la evaluación individual del embudo de remarketing.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno.</param>
        /// <returns>OportunidadUltimoCambioDTO</returns>
        public OportunidadUltimoCambioDTO ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambioAlumno(int IdAlumno)
        {
            try
            {
                var query = "mkt.SP_RemarketingEmbudoInformacionOportunidadUltimoCambio";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]" && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<OportunidadUltimoCambioDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambioAlumno: {ex.Message}", ex);
            }
        }
    }
}

