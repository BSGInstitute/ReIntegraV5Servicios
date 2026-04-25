using AutoMapper;
using Azure.Core;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// <summary>
    /// Gestión general de  T_RemarketingEmbudoHistorico
    /// Autor: Max Mantilla Rodriguez
    /// Fecha: 27/12/2025
    /// </summary>
    public class RemarketingEmbudoHistoricoService : IRemarketingEmbudoHistoricoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly HashSet<string> _faseCerrada = new() { "RN1", "RN2-A", "RN2-B", "RN2-C", "RN3", "RN4", "RN5", "RN8", "BIC", "E", "NS", "M", "IS" };
        private static readonly HashSet<string> _faseAbierta = new() { "BNC", "IT", "IP", "PF", "IC" };
        private static readonly HashSet<string> _faseNoCuenta = new() { "OD", "OM", "BRM1" };

        public RemarketingEmbudoHistoricoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRemarketingEmbudoHistorico, RemarketingEmbudoHistorico>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public RemarketingEmbudoHistorico Add(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoHistoricoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemarketingEmbudoHistorico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RemarketingEmbudoHistorico Update(RemarketingEmbudoHistorico entidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoHistoricoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RemarketingEmbudoHistorico>(modelo);
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
                _unitOfWork.RemarketingEmbudoHistoricoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemarketingEmbudoHistorico> Add(List<RemarketingEmbudoHistorico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoHistoricoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemarketingEmbudoHistorico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RemarketingEmbudoHistorico> Update(List<RemarketingEmbudoHistorico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RemarketingEmbudoHistoricoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RemarketingEmbudoHistorico>>(modelo);
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
                _unitOfWork.RemarketingEmbudoHistoricoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Función que evalúa y registra el nivel de embudo de remarketing para todos los alumnos de forma masiva (histórico).
        /// Obtiene la información consolidada de oportunidades, ocurrencias, interacciones, centro de costo,
        /// WhatsApp y score, luego clasifica a cada alumno en su nivel de embudo correspondiente
        /// según los esquemas 1 y 2 definidos.
        /// </summary>
        /// <returns>true si el proceso se ejecutó correctamente, false en caso de error.</returns>
        public async Task<bool> EvaluarEmbudoRemarketing()
        {
            try
            {
                var listaNivelEmbudo = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInformacionRemarketingEmbudoNivel();
                var ocurrenciasEjecutadas = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerOcurrenciaEjecutada();
                var ultimaInteraccion = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInteraccionPortalUltimaInteraccion();
                var centroCostoValidoRegistrado = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerCentroCostoRegistro();
                var whatsAppMensajeUltimo = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerWhatsAppMensajeUltimo();
                var oportunidadUltimoCambio = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambio();
                var listaScoreOportunidad = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerScoreOportunidadAlumno(5000);

                var scoresMasRecientesPorOportunidad = listaScoreOportunidad
                    .GroupBy(s => s.IdOportunidad)
                    .Select(grupo => grupo
                        .OrderByDescending(s => s.FechaProcesamiento)
                        .First())
                    .ToDictionary(s => s.IdOportunidad, s => s.ScoreTextual);

                // Convertir todas las listas a Dictionary para búsqueda O(1)
                var ocurrenciasDict = ocurrenciasEjecutadas?
                    .ToDictionary(x => x.IdAlumno) ?? new Dictionary<int, ActividadEjecutadaReporteDTO>();

                var interaccionDict = ultimaInteraccion?
                    .ToDictionary(x => x.IdAlumno) ?? new Dictionary<int, InteracccionPortalUltimaInteraccionDTO>();

                var centroCostoDict = centroCostoValidoRegistrado?
                    .ToDictionary(x => x.IdAlumno) ?? new Dictionary<int, AlumnoCentroCostoRegistroDTO>();

                var whatsappDict = whatsAppMensajeUltimo?
                    .ToDictionary(x => x.IdAlumno) ?? new Dictionary<int, WhatsappUltimoMensajeEnviadoDTO>();

                var registrosOportunidadProcesada = oportunidadUltimoCambio
                    .GroupBy(o => o.IdAlumno)
                    .Select(grupo =>
                    {
                        var oportunidad = grupo.First();

                        ocurrenciasDict.TryGetValue(oportunidad.IdAlumno, out var ocurrencia);
                        interaccionDict.TryGetValue(oportunidad.IdAlumno, out var interaccion);
                        centroCostoDict.TryGetValue(oportunidad.IdAlumno, out var centroCosto);
                        whatsappDict.TryGetValue(oportunidad.IdAlumno, out var whatsapp);

                        string scoreTexto = "Sin Score";
                        if (scoresMasRecientesPorOportunidad.TryGetValue(oportunidad.IdOportunidad, out var score))
                            scoreTexto = score;

                        return new OportunidadCompletaDTO
                        {
                            IdOportunidad = oportunidad.IdOportunidad,
                            IdAlumno = oportunidad.IdAlumno,
                            FaseOportunidadActual = oportunidad.FaseOportunidadActual,
                            ClasificacionProbabilidad = oportunidad.ClasificacionProbabilidad,
                            FechaCreacionOportunidad = oportunidad.FechaCreacionOportunidad,
                            CantidadOportunidad = oportunidad.CantidadOportunidad,
                            OcurrenciasEjecutadas = ocurrencia?.NumeroOcurrenciasEjecutadas ?? 0,
                            UltimaInteraccionPortal = interaccion?.FechaUltimaInteraccion ?? null,
                            CentroCostoRegistrado = centroCosto?.OportunidadCantidad ?? 0,
                            FechaUltimoWhatsapp = whatsapp?.WhatsappMensajeFechaEnvio ?? null,
                            Score = scoreTexto,
                        };
                    })
                    .ToList();

                var FechaClasificacion = DateTime.Now;
                var fechaLimite6Meses = DateTime.Now.AddMonths(-6);
                var fechaLimite2Meses = DateTime.Now.AddMonths(-2);

                string[] _probabilidadesAltaMedia = { "Media", "Alta" };
                string[] _probabilidadesMuyAltaAltaMedia = { "Muy Alta", "Alta", "Media" };
                string Usuario = "EmbudoRemarketing";

                foreach (var nivel in registrosOportunidadProcesada)
                {
                    bool faseCerrada = _faseCerrada.Contains(nivel.FaseOportunidadActual);
                    bool faseAbierta = _faseAbierta.Contains(nivel.FaseOportunidadActual);
                    bool faseNoCuenta = _faseNoCuenta.Contains(nivel.FaseOportunidadActual);

                    foreach (var embudo in listaNivelEmbudo)
                    {
                        // ══════════════════════════════════════════
                        // ESQUEMA 1
                        // ══════════════════════════════════════════
                        if (embudo.Id == 1)
                        {
                            // Nivel 0 - Sin contacto previo (no aplica parámetros)
                        }
                        else if (embudo.Id == 2)
                        {
                            // Nivel 1 - Interacción con contenidos/mensajes publicitarios
                            bool faseValida = faseCerrada || faseAbierta || faseNoCuenta;

                            if (nivel.CentroCostoRegistrado > 0
                                && nivel.ClasificacionProbabilidad == "Sin Probabilidad"
                                && faseValida)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 3)
                        {
                            // Nivel 2 - Primera solicitud de información no calificada
                            if (nivel.CantidadOportunidad == 1
                                && _probabilidadesAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                                && nivel.OcurrenciasEjecutadas == 0
                                && faseAbierta)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 4)
                        {
                            // Nivel 3 - Gestión de venta telefónica activa
                            bool condicionProbabilidad = nivel.ClasificacionProbabilidad == "Muy Alta"
                                                      || nivel.OcurrenciasEjecutadas >= 1;

                            if (nivel.CantidadOportunidad == 1
                                && condicionProbabilidad
                                && faseAbierta)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }

                        // ══════════════════════════════════════════
                        // ESQUEMA 2
                        // ══════════════════════════════════════════
                        else if (embudo.Id == 5)
                        {
                            // Nivel 0 - Sin interacción reciente (todo mayor a 6 meses)
                            if (nivel.CantidadOportunidad >= 1
                                && _probabilidadesMuyAltaAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                                && (nivel.FechaUltimoWhatsapp == null || nivel.FechaUltimoWhatsapp < fechaLimite6Meses)
                                && nivel.FechaCreacionOportunidad < fechaLimite6Meses
                                && (nivel.UltimaInteraccionPortal == null || nivel.UltimaInteraccionPortal < fechaLimite6Meses)
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 6)
                        {
                            // Nivel 1 - Al menos un contacto con contenidos en los últimos 6 meses
                            if (nivel.CantidadOportunidad >= 1
                                && _probabilidadesMuyAltaAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                                && nivel.UltimaInteraccionPortal.HasValue
                                && nivel.UltimaInteraccionPortal >= fechaLimite6Meses
                                && nivel.FechaCreacionOportunidad < fechaLimite6Meses
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 7)
                        {
                            // Nivel 2 - Solicitud de información en últimos 6 meses no calificada
                            if (nivel.CantidadOportunidad >= 1
                                && _probabilidadesAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                                && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                                && nivel.OcurrenciasEjecutadas == 0
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 8)
                        {
                            // Nivel 3 - Score bajo con gestión de venta telefónica previa
                            bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                                   || nivel.OcurrenciasEjecutadas >= 1;

                            if (nivel.CantidadOportunidad >= 1
                                && nivel.Score == "Baja"
                                && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                                && condicionContacto
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 9)
                        {
                            // Nivel 4 - Score medio con gestión de venta telefónica previa
                            bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                                   || nivel.OcurrenciasEjecutadas >= 1;

                            if (nivel.CantidadOportunidad >= 1
                                && nivel.Score == "Media"
                                && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                                && nivel.FechaCreacionOportunidad < fechaLimite2Meses
                                && condicionContacto
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 10)
                        {
                            // Nivel 5 - Score alto con gestión de venta telefónica previa
                            bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                                   || nivel.OcurrenciasEjecutadas >= 1;

                            if (nivel.CantidadOportunidad >= 1
                                && nivel.Score == "Alta"
                                && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                                && nivel.FechaCreacionOportunidad < fechaLimite2Meses
                                && condicionContacto
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 11)
                        {
                            // Nivel 6 - Nueva solicitud de información activa no calificada
                            if (nivel.CantidadOportunidad > 1
                                && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                                && _probabilidadesAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                                && nivel.OcurrenciasEjecutadas == 0
                                && faseAbierta)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 12)
                        {
                            // Nivel 7 - Gestión de venta telefónica activa
                            bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                                   || nivel.OcurrenciasEjecutadas >= 1;

                            if (nivel.CantidadOportunidad > 1
                                && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                                && condicionContacto
                                && faseAbierta)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else if (embudo.Id == 13)
                        {
                            // Nivel 8 - Nurturing (oportunidad creada en los últimos 2 meses)
                            if ((nivel.Score == "Medio" || nivel.Score == "Alto")
                                && nivel.OcurrenciasEjecutadas >= 1
                                && nivel.FechaCreacionOportunidad >= fechaLimite2Meses
                                && faseCerrada)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository
                                    .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                            }
                        }
                        else
                        {
                            // Sin Nivel
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EvaluarEmbudoRemarketing: {ex.Message}");
                return false;
            }
            return true;
        }

        /// Autor: Max Mantilla
        /// Fecha: 04/03/2026
        /// Version: 1.0
        /// <summary>
        /// Función que evalúa y registra el nivel de embudo de remarketing para un alumno específico.
        /// A diferencia del proceso histórico masivo, esta función opera sobre un único IdAlumno,
        /// obteniendo sus datos individuales y clasificándolo en su nivel de embudo correspondiente
        /// según los esquemas 1 y 2 definidos. Al ser una operación puntual, no requiere async.
        /// </summary>
        /// <param name="IdAlumno">Identificador único del alumno a evaluar.</param>
        /// <returns>true si el proceso se ejecutó correctamente, false en caso de error o si el alumno no tiene oportunidad registrada.</returns>
        public bool EvaluarEmbudoRemarketingAlumno(int IdAlumno, string Usuario)
        {
            try
            {
                var listaNivelEmbudo = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInformacionRemarketingEmbudoNivel();
                var ocurrenciasEjecutadas = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerOcurrenciaEjecutadaAlumno(IdAlumno);
                var ultimaInteraccion = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInteraccionPortalUltimaInteraccionAlumno(IdAlumno);
                var centroCostoValidoRegistrado = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerCentroCostoRegistroAlumno(IdAlumno);
                var whatsAppMensajeUltimo = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerWhatsAppMensajeUltimoAlumno(IdAlumno);
                var oportunidadUltimoCambio = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerRemarketingEmbudoInformacionOportunidadUltimoCambioAlumno(IdAlumno);

                if (oportunidadUltimoCambio == null)
                    return false;

                var listaScoreOportunidad = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerScoreOportunidadAlumnoIndividual(oportunidadUltimoCambio.IdOportunidad);

                var scoreMasReciente = listaScoreOportunidad
                    .OrderByDescending(s => s.FechaProcesamiento)
                    .FirstOrDefault();

                string scoreTexto = scoreMasReciente?.ScoreTextual ?? "Sin Score";

                var nivel = new OportunidadCompletaDTO
                {
                    IdOportunidad = oportunidadUltimoCambio.IdOportunidad,
                    IdAlumno = oportunidadUltimoCambio.IdAlumno,
                    FaseOportunidadActual = oportunidadUltimoCambio.FaseOportunidadActual,
                    ClasificacionProbabilidad = oportunidadUltimoCambio.ClasificacionProbabilidad,
                    FechaCreacionOportunidad = oportunidadUltimoCambio.FechaCreacionOportunidad,
                    CantidadOportunidad = oportunidadUltimoCambio.CantidadOportunidad,
                    OcurrenciasEjecutadas = ocurrenciasEjecutadas?.NumeroOcurrenciasEjecutadas ?? 0,
                    UltimaInteraccionPortal = ultimaInteraccion?.FechaUltimaInteraccion ?? null,
                    CentroCostoRegistrado = centroCostoValidoRegistrado?.OportunidadCantidad ?? 0,
                    FechaUltimoWhatsapp = whatsAppMensajeUltimo?.WhatsappMensajeFechaEnvio ?? null,
                    Score = scoreTexto,
                };

                var FechaClasificacion = DateTime.Now;
                var fechaLimite6Meses = DateTime.Now.AddMonths(-6);
                var fechaLimite2Meses = DateTime.Now.AddMonths(-2);

                string[] _probabilidadesAltaMedia = { "Media", "Alta" };
                string[] _probabilidadesMuyAltaAltaMedia = { "Muy Alta", "Alta", "Media" };

                bool faseCerrada = _faseCerrada.Contains(nivel.FaseOportunidadActual);
                bool faseAbierta = _faseAbierta.Contains(nivel.FaseOportunidadActual);
                bool faseNoCuenta = _faseNoCuenta.Contains(nivel.FaseOportunidadActual);
                foreach (var embudo in listaNivelEmbudo)
                {
                    // ══════════════════════════════════════════
                    // ESQUEMA 1
                    // ══════════════════════════════════════════
                    if (embudo.Id == 1)
                    {
                        // Nivel 0 - Sin contacto previo (no aplica parámetros)
                    }
                    else if (embudo.Id == 2)
                    {
                        // Nivel 1 - Interacción con contenidos/mensajes publicitarios
                        bool faseValida = faseCerrada || faseAbierta || faseNoCuenta;

                        if (nivel.CentroCostoRegistrado > 0
                            && nivel.ClasificacionProbabilidad == "Sin Probabilidad"
                            && faseValida)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 3)
                    {
                        // Nivel 2 - Primera solicitud de información no calificada
                        if (nivel.CantidadOportunidad == 1
                            && _probabilidadesAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                            && nivel.OcurrenciasEjecutadas == 0
                            && faseAbierta)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 4)
                    {
                        // Nivel 3 - Gestión de venta telefónica activa
                        bool condicionProbabilidad = nivel.ClasificacionProbabilidad == "Muy Alta"
                                                  || nivel.OcurrenciasEjecutadas >= 1;

                        if (nivel.CantidadOportunidad == 1
                            && condicionProbabilidad
                            && faseAbierta)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }

                    // ══════════════════════════════════════════
                    // ESQUEMA 2
                    // ══════════════════════════════════════════
                    else if (embudo.Id == 5)
                    {
                        // Nivel 0 - Sin interacción reciente (todo mayor a 6 meses)
                        if (nivel.CantidadOportunidad >= 1
                            && _probabilidadesMuyAltaAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                            && (nivel.FechaUltimoWhatsapp == null || nivel.FechaUltimoWhatsapp < fechaLimite6Meses)
                            && nivel.FechaCreacionOportunidad < fechaLimite6Meses
                            && (nivel.UltimaInteraccionPortal == null || nivel.UltimaInteraccionPortal < fechaLimite6Meses)
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 6)
                    {
                        // Nivel 1 - Al menos un contacto con contenidos en los últimos 6 meses
                        if (nivel.CantidadOportunidad >= 1
                            && _probabilidadesMuyAltaAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                            && nivel.UltimaInteraccionPortal.HasValue
                            && nivel.UltimaInteraccionPortal >= fechaLimite6Meses
                            && nivel.FechaCreacionOportunidad < fechaLimite6Meses
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 7)
                    {
                        // Nivel 2 - Solicitud de información en últimos 6 meses no calificada
                        if (nivel.CantidadOportunidad >= 1
                            && _probabilidadesAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                            && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                            && nivel.OcurrenciasEjecutadas == 0
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 8)
                    {
                        // Nivel 3 - Score bajo con gestión de venta telefónica previa
                        bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                               || nivel.OcurrenciasEjecutadas >= 1;

                        if (nivel.CantidadOportunidad >= 1
                            && nivel.Score == "Baja"
                            && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                            && condicionContacto
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 9)
                    {
                        // Nivel 4 - Score medio con gestión de venta telefónica previa
                        bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                               || nivel.OcurrenciasEjecutadas >= 1;

                        if (nivel.CantidadOportunidad >= 1
                            && nivel.Score == "Media"
                            && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                            && nivel.FechaCreacionOportunidad < fechaLimite2Meses
                            && condicionContacto
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 10)
                    {
                        // Nivel 5 - Score alto con gestión de venta telefónica previa
                        bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                               || nivel.OcurrenciasEjecutadas >= 1;

                        if (nivel.CantidadOportunidad >= 1
                            && nivel.Score == "Alta"
                            && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                            && nivel.FechaCreacionOportunidad < fechaLimite2Meses
                            && condicionContacto
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 11)
                    {
                        // Nivel 6 - Nueva solicitud de información activa no calificada
                        if (nivel.CantidadOportunidad > 1
                            && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                            && _probabilidadesAltaMedia.Contains(nivel.ClasificacionProbabilidad)
                            && nivel.OcurrenciasEjecutadas == 0
                            && faseAbierta)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 12)
                    {
                        // Nivel 7 - Gestión de venta telefónica activa
                        bool condicionContacto = nivel.ClasificacionProbabilidad == "Muy Alta"
                                               || nivel.OcurrenciasEjecutadas >= 1;

                        if (nivel.CantidadOportunidad > 1
                            && nivel.FechaCreacionOportunidad >= fechaLimite6Meses
                            && condicionContacto
                            && faseAbierta)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else if (embudo.Id == 13)
                    {
                        // Nivel 8 - Nurturing (oportunidad creada en los últimos 2 meses)
                        if ((nivel.Score == "Medio" || nivel.Score == "Alto")
                            && nivel.OcurrenciasEjecutadas >= 1
                            && nivel.FechaCreacionOportunidad >= fechaLimite2Meses
                            && faseCerrada)
                        {
                            _unitOfWork.RemarketingEmbudoHistoricoRepository
                                .RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion, Usuario);
                        }
                    }
                    else
                    {
                        // Sin Nivel
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EvaluarEmbudoRemarketingAlumno: {ex.Message}");
                return false;
            }
        }
        public List<RemarketingEmbudoEsquemaNivelDTO> ObtenerNivelEsquemaEmbudoRemarketing()
        {
            try
            {
                var registros = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerNivelEsquemaEmbudoRemarketing();

                return registros;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ObtenerNivelEsquemaEmbudoRemarketing: {ex.Message}");
                return null;
            }
        }        
    }
}
