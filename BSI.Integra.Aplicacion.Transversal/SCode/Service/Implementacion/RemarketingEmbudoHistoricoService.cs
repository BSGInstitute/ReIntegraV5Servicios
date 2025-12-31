using AutoMapper;
using Azure.Core;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private static readonly HashSet<string> _probabilidadesValidas = new() { "Media", "Alta"};
        static RemarketingEmbudoHistoricoService()
        {
        }
        public RemarketingEmbudoHistoricoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TFacebookFormularioLeadgenLog, FacebookFormularioLeadgenLog>()
                   .ReverseMap());
            _mapper = new Mapper(config);
        }

        public async Task<bool> EvaluarEmbudoRemarketing(DateTime? FechaCorte)
        {
            try
            {
                // Cambia esto a la versión async (debe devolver Task<List<>>)
                var listaNivelEmbudo = _unitOfWork.OportunidadRepository.ObtenerInformacionRemarketingEmbudoNivel();
                var listaLlamadasEfectivas = _unitOfWork.OportunidadRepository.ObtenerLlamadasEfectivasOportunidadAlumno();
                var llamadasEfectivasAgrupadas = listaLlamadasEfectivas
                    .GroupBy(l => l.IdAlumno)
                    .Select(grupo => new RemarketingEmbudoNivelLlamadaEfectivaAgrupadoDTO
                    {
                        IdAlumno = grupo.Key,
                        LlamadasEfectivas = grupo.Count()
                    })
                    .ToDictionary(g => g.IdAlumno, g => g.LlamadasEfectivas);
                var registrosOportunidad = await _unitOfWork.OportunidadRepository.ObtenerInformacionOportunidadRemarketing(FechaCorte);
                var registrosOportunidadProcesada = registrosOportunidad
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
                            Score = "Medio"
                        };
                    })
                    .ToList();
                
                foreach(var nivel in registrosOportunidadProcesada)
                {
                    foreach(var embudo in listaNivelEmbudo)
                    {
                        // Esquema 1
                        if (embudo.Id == 1)
                        {
                            // Nivel 0
                        }
                        else if (embudo.Id == 2)
                        {
                            // Nivel 1                            
                        }
                        else if (embudo.Id == 3)
                        {
                            // Nivel 2
                            if (nivel.CantidadTotalOportunidades == 1 
                                && nivel.CantidadOportunidadesCerradas == 0 
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
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
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
                            }
                        }
                        // Esquema 2
                        else if (embudo.Id == 5)
                        {
                            // Nivel 0
                        }
                        else if (embudo.Id == 6)
                        {
                            // Nivel 1
                        }
                        else if (embudo.Id == 7)
                        {
                            // Nivel 2
                        }
                        else if (embudo.Id == 8)
                        {
                            // Nivel 3
                            if (nivel.CantidadOportunidadesCerradas >= 1 
                                && nivel.Score == "Bajo" 
                                && nivel.LlamadasEfectivas >= 1)
                            {
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
                            }
                        }
                        else if (embudo.Id == 9)
                        {
                            // Nivel 4
                            if (nivel.CantidadOportunidadesCerradas >= 1 
                                && nivel.Score == "Medio" 
                                && nivel.LlamadasEfectivas >= 1)
                            {
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
                            }
                        }
                        else if (embudo.Id == 10)
                        {
                            // Nivel 5
                            if (nivel.CantidadOportunidadesCerradas >= 1 
                                && nivel.Score == "Alto" 
                                && nivel.LlamadasEfectivas >= 1)
                            {
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
                            }
                        }
                        else if (embudo.Id == 11)
                        {
                            // Nivel 6
                            DateTime fechaLimite = DateTime.Now.AddMonths(-6);
                            if (nivel.CantidadOportunidadesCerradas >= 1 
                                && nivel.FechaCreacionOportunidad <= fechaLimite 
                                && _probabilidadesValidas.Contains(nivel.ClasificacionProbabilidad))
                            {
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
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
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
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
                                _unitOfWork.OportunidadRepository.RegistrarEmbudoRemarketing(embudo.Id, nivel.IdAlumno);
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
    }
}
