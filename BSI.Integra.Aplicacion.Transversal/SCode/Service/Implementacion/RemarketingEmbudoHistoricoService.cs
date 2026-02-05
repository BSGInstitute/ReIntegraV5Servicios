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
        private static readonly HashSet<string> _oportunidadesCerradas = new() { "RN2", "RN3", "RN4", "RN5", "RN8", "BIC", "E", "NS", "IS", "M" };
        private static readonly HashSet<string> _probabilidadesValidas = new() { "Media", "Alta" };

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

        public async Task<bool> EvaluarEmbudoRemarketing(DateTime? FechaCorte)
        {
            try
            {
                var listaNivelEmbudo = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInformacionRemarketingEmbudoNivel();
                var listaLlamadasEfectivas = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerLlamadasEfectivasOportunidadAlumno();

                var llamadasEfectivasAgrupadas = listaLlamadasEfectivas
                    .GroupBy(l => l.IdAlumno)
                    .Select(grupo => new RemarketingEmbudoNivelLlamadaEfectivaAgrupadoDTO
                    {
                        IdAlumno = grupo.Key,
                        LlamadasEfectivas = grupo.Count()
                    })
                    .ToDictionary(g => g.IdAlumno, g => g.LlamadasEfectivas);

                var listaInteraccionFormularioProgresivo = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInteraccionFormularioProgresivo();
                var interaccionesRecientes = listaInteraccionFormularioProgresivo
                    .GroupBy(i => i.Correo)
                    .Select(grupo => grupo
                        .OrderByDescending(i => i.FechaCreacion)
                        .First())
                    .ToDictionary(i => i.Correo, i => i.FechaCreacion);

                var listaScoreOportunidad = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerScoreOportunidadAlumno(5000);
                var scoresMasRecientesPorOportunidad = listaScoreOportunidad
                    .GroupBy(s => s.IdOportunidad)
                    .Select(grupo => grupo
                        .OrderByDescending(s => s.FechaProcesamiento)
                        .First())
                    .ToDictionary(s => s.IdOportunidad, s => s.ScoreTextual);
                int RegistrosPorPagina = 5000;
                long totalRegistrosOportunidad = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInformacionOportunidadRemarketingTotal(FechaCorte);
                long totalPaginas = (totalRegistrosOportunidad + RegistrosPorPagina - 1) / RegistrosPorPagina;
                var registrosOportunidad = new List<OportunidadRemarketingEmbudoDTO>();
                for (int paginaActual = 1; paginaActual <= totalPaginas; paginaActual++)
                {
                    List < OportunidadRemarketingEmbudoDTO > registrosOportunidadPorPagina = new List<OportunidadRemarketingEmbudoDTO>();
                    registrosOportunidadPorPagina = _unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerInformacionOportunidadRemarketing(paginaActual, RegistrosPorPagina, FechaCorte);
                    registrosOportunidad.AddRange(registrosOportunidadPorPagina);
                }

                var oportunidadesUnicas = registrosOportunidad
                    .GroupBy(o => o.IdOportunidad)
                    .Select(grupo =>
                    {
                        var oportunidadesOrdenadas = grupo
                            .OrderByDescending(o => o.FechaCambioOportunidad)
                            .ThenByDescending(o => o.FechaRegistroEnvioWhatsapp)
                            .ToList();

                        return oportunidadesOrdenadas.First();
                    })
                    .ToList();

                var registrosOportunidadProcesada = oportunidadesUnicas
                    .GroupBy(o => o.IdAlumno)
                    .Select(grupo =>
                    {
                        var oportunidadesOrdenadas = grupo
                            .OrderByDescending(o => o.FechaCreacionOportunidad)
                            .ToList();

                        var masReciente = oportunidadesOrdenadas.First();
                        var cantidadTotal = oportunidadesOrdenadas.Count;
                        var cantidadCerradas = oportunidadesOrdenadas
                            .Count(o => _oportunidadesCerradas.Contains(o.CodigoFaseOportunidadActual));
                        var llamadasEfectivas = llamadasEfectivasAgrupadas.TryGetValue(masReciente.IdAlumno, out var cantidad) ? cantidad : 0;

                        DateTime? ultimaInteraccionProgresivo = null;
                        if (interaccionesRecientes.TryGetValue(masReciente.Correo, out var fechaInteraccion))
                        {
                            ultimaInteraccionProgresivo = fechaInteraccion;
                        }

                        string scoreTexto = "Sin Score";
                        if (scoresMasRecientesPorOportunidad.TryGetValue(masReciente.IdOportunidad, out var score))
                        {
                            scoreTexto = score;
                        }

                        return new OportunidadCompletaDTO
                        {
                            IdOportunidad = masReciente.IdOportunidad,
                            IdAlumno = masReciente.IdAlumno,
                            FaseOportunidadAnterior = masReciente.FaseOportunidadAnterior,
                            CodigoFaseOportunidadAnterior = masReciente.CodigoFaseOportunidadAnterior,
                            FaseOportunidadActual = masReciente.FaseOportunidadActual,
                            CodigoFaseOportunidadActual = masReciente.CodigoFaseOportunidadActual,
                            ClasificacionProbabilidad = masReciente.ClasificacionProbabilidad,
                            FechaCreacionOportunidad = masReciente.FechaCreacionOportunidad,
                            FechaCambioOportunidad = masReciente.FechaCambioOportunidad,
                            CantidadTotalOportunidades = cantidadTotal,
                            CantidadOportunidadesCerradas = cantidadCerradas,
                            LlamadasEfectivas = llamadasEfectivas,
                            Score = scoreTexto,
                            IdCentroCosto = masReciente.IdCentroCosto,
                            UltimaInteraccionProgresivo = ultimaInteraccionProgresivo
                        };
                    })
                    .ToList();
                var FechaClasificacion = DateTime.Now;
                foreach (var nivel in registrosOportunidadProcesada)
                {
                    foreach (var embudo in listaNivelEmbudo)
                    {
                        // Esquema 1
                        if (embudo.Id == 1)
                        {
                            // Nivel 0
                        }
                        else if (embudo.Id == 2)
                        {
                            // Nivel 1
                            int[] _centroCostoValido = { 9504, 9505, 15906, 15907, 15908 };


                            if (nivel.CantidadTotalOportunidades == 1
                                && _centroCostoValido.Contains(nivel.IdCentroCosto))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 3)
                        {
                            // Nivel 2
                            if (nivel.CantidadTotalOportunidades == 1
                                && nivel.CantidadOportunidadesCerradas == 0
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 4)
                        {
                            // Nivel 3
                            string[] fasesValidas = { "BNC", "IT", "IP" };

                            if (nivel.CantidadTotalOportunidades == 1
                                && nivel.CantidadOportunidadesCerradas == 0
                                && nivel.LlamadasEfectivas >= 1
                                && fasesValidas.Contains(nivel.CodigoFaseOportunidadActual))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        // Esquema 2
                        else if (embudo.Id == 5)
                        {
                            // Nivel 0
                            DateTime fechaLimite = DateTime.Now.AddMonths(-6);

                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && (nivel.FechaRegistroEnvioWhatsapp ?? DateTime.MaxValue) < fechaLimite
                                && nivel.FechaCreacionOportunidad < fechaLimite
                                && nivel.UltimaInteraccionProgresivo?.Date < fechaLimite.Date
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 6)
                        {
                            // Nivel 1
                            DateTime fechaLimite = DateTime.Now.AddMonths(-6);
                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.FechaCreacionOportunidad >= fechaLimite
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 7)
                        {
                            // Nivel 2
                            DateTime fechaLimite = DateTime.Now.AddMonths(-6);
                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.FechaCreacionOportunidad == fechaLimite
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 8)
                        {
                            // Nivel 3
                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.Score == "Baja"
                                && nivel.LlamadasEfectivas >= 1)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 9)
                        {
                            // Nivel 4
                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.Score == "Media"
                                && nivel.LlamadasEfectivas >= 1)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 10)
                        {
                            // Nivel 5
                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.Score == "Alta"
                                && nivel.LlamadasEfectivas >= 1)
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 11)
                        {
                            // Nivel 6
                            DateTime fechaLimite = DateTime.Now.AddMonths(-6);
                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.FechaCreacionOportunidad < fechaLimite
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 12)
                        {
                            // Nivel 7
                            string[] fasesValidas = { "BNC", "IT", "IP", "PF" };

                            if (nivel.CantidadOportunidadesCerradas >= 1
                                && nivel.LlamadasEfectivas >= 1
                                && fasesValidas.Contains(nivel.CodigoFaseOportunidadActual))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
                            }
                        }
                        else if (embudo.Id == 13)
                        {
                            // Nivel 8
                            var fechaLimite = DateTime.Now.AddMonths(-2);
                            string[] fasesValidas = { "RN2", "RN3", "RN4", "IT", "IP", "PF", "IC", "A", "BIC" };

                            if ((nivel.Score == "Medio" || nivel.Score == "Alto")
                                && nivel.LlamadasEfectivas >= 1
                                && nivel.FechaCreacionOportunidad <= fechaLimite
                                && fasesValidas.Contains(nivel.CodigoFaseOportunidadActual))
                            {
                                _unitOfWork.RemarketingEmbudoHistoricoRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno, FechaClasificacion);
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
            }
            return true;
        }
        public List<RemarketingEmbudoEsquemaNivelDTO> ObtenerNivelEsquemaEmbudoRemarketing()
        {
            try
            {
                var registros =_unitOfWork.RemarketingEmbudoHistoricoRepository.ObtenerNivelEsquemaEmbudoRemarketing();

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
