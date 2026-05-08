using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppEnvioMasivoService: IWhatsAppEnvioMasivoService
    {
        private Mapper _mapper;
        private readonly IUnitOfWork unitOfWork;

        //private const string IA_MASIVO_BASE = "http://ia-asistente-marketing-whatsapp-api.bsginstitute.com/testing";
        private const string IA_MASIVO_BASE = "http://ia-asistente-marketing-whatsapp-api.bsginstitute.com";


        public WhatsAppEnvioMasivoService(IUnitOfWork unitOfWork)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
            this.unitOfWork = unitOfWork;
        }

        // ---------------------------------------------------------------------------
        // Historial de Oportunidades por Alumno
        // ---------------------------------------------------------------------------

        /// <summary>
        /// Obtiene el historial de oportunidades de un alumno
        /// mediante el SP mkt.SP_OportunidadHistorialPorAlumno.
        /// </summary>
        public List<HistorialOportunidadMasivoDTO> ObtenerHistorialOportunidadesPorAlumno(int idAlumno)
        {
            try
            {
                var resultado = unitOfWork.WhatsAppMensajeEnviadoRepository
                    .ObtenerHistorialOportunidadesPorAlumno(idAlumno);
                return resultado ?? new List<HistorialOportunidadMasivoDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ---------------------------------------------------------------------------
        // Calificacion Batch V2 — IA (perfil_lead + historial_oportunidades)
        // ---------------------------------------------------------------------------

        /// <summary>
        /// Inicia una calificacion batch V2 con perfil_lead e historial_oportunidades.
        /// POST /api/oportunidades/calificacion/llamadas
        /// </summary>
        public async Task<string> IniciarCalificacionBatchV2(CalificacionBatchV2RequestDTO request)
        {
            using (var client = new HttpClient())
            {
                var iaPayload = new
                {
                    tenant_id = request.TenantId,
                    oportunidades = request.Oportunidades?.Select(o => new
                    {
                        identificador_lead = o.IdentificadorLead,
                        agent_id = o.AgentId,
                        canal = "whatsapp",
                        origen = o.Origen,
                        mensajes = (o.Mensajes ?? new List<ExtraccionMensajeDTO>()).Select(m => new
                        {
                            role = m.Role,
                            content = m.Content,
                            timestamp = m.Timestamp
                        }).ToList(),
                        perfil_lead = o.PerfilLead == null ? null : new
                        {
                            area_formacion = o.PerfilLead.AreaFormacion,
                            cargo_actual = o.PerfilLead.CargoActual,
                            area_trabajo = o.PerfilLead.AreaTrabajo,
                            industria = o.PerfilLead.Industria
                        },
                        historial_oportunidades = (o.HistorialOportunidades ?? new List<CalificacionHistorialV2ItemDTO>())
                            .Where(h =>
                                new[] { "IS", "M", "IC", "PF", "IP", "IT", "BNC" }.Contains(h.FaseMaxima) &&
                                new[] { "IS", "M", "RN2", "RN3", "RN4", "BIC", "NS" }.Contains(h.FaseCierre)
                            )
                            .Select(h => new
                            {
                                id_oportunidad = h.IdOportunidad,
                                fase_maxima = h.FaseMaxima,
                                fase_cierre = h.FaseCierre
                            }).ToList()
                    }).ToList()
                };

                var json = JsonConvert.SerializeObject(iaPayload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{IA_MASIVO_BASE}/api/oportunidades/calificacion/llamadas", content);
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al llamar al API de IA (IniciarCalificacionBatchV2): {body}");
                return body;
            }
        }

        /// <summary>
        /// Consulta el estado de una calificacion batch V2 por su llamadaId.
        /// GET /api/oportunidades/calificacion/llamadas/{llamadaId}/status
        /// </summary>
        public async Task<string> ObtenerEstadoCalificacionV2(string llamadaId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{IA_MASIVO_BASE}/api/oportunidades/calificacion/llamadas/{llamadaId}/status");
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al llamar al API de IA (ObtenerEstadoCalificacionV2): {body}");
                return body;
            }
        }

        /// <summary>
        /// Obtiene los resultados de una calificacion batch V2 por su llamadaId.
        /// GET /api/oportunidades/calificacion/llamadas/{llamadaId}/resultados
        /// </summary>
        public async Task<string> ObtenerResultadosCalificacionV2(string llamadaId)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{IA_MASIVO_BASE}/api/oportunidades/calificacion/llamadas/{llamadaId}/resultados");
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al llamar al API de IA (ObtenerResultadosCalificacionV2): {body}");
                return body;
            }
        }
    }
}
