using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Google.Api;
using Nancy.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionRegularAutomaticaService
    /// Autor: Margiory  Ramirez
    /// Fecha: 26/08/2022
    /// <summary>
    /// Gestión general de T_OrigenSector
    /// </summary>
    public class AsignacionRegularAutomaticaService : IAsignacionRegularAutomaticaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        private static readonly SemaphoreSlim _semaforo = new SemaphoreSlim(1, 1);
        private static CancellationTokenSource _cts = new CancellationTokenSource();
        private static bool _ejecucionManualEnCurso = false; //  Bandera para saber si el proceso es manual
        private static DateTime _ultimaEjecucionManual = DateTime.MinValue; //  Última vez que se ejecutó un manual
        private static bool _ejecucionAutomaticaEnCurso = false;
        private static readonly object _lock = new object(); //  Evita concurrencias incorrectas

        public AsignacionRegularAutomaticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionRegular, AsignacionRegular>(MemberList.None).ReverseMap();
                cfg.CreateMap<ObtenerAsesorConfiguracionPorPaisDTO, PaisConfiguracionAsignacionRegularDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);

        }
        #region Metodos Base
        public AsignacionRegular Add(AsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionRegular>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionRegular Update(AsignacionRegular entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionRegular>(modelo);
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
                _unitOfWork.AsignacionRegularRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionRegular> Add(List<AsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionRegular>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionRegular> Update(List<AsignacionRegular> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionRegularRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionRegular>>(modelo);
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
                _unitOfWork.AsignacionRegularRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        public async Task<bool> EjecutarAsignacionDatosAutomatizada2(string Usuario)
        {
            bool esManual = !Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase);

            if (!_semaforo.Wait(0))
            {
                if (esManual)
                {
                    Console.WriteLine("Cancelando asignación automática para priorizar la manual...");

                    _cts.Cancel();
                    await Task.Delay(500);

                    _cts = new CancellationTokenSource();
                    _ejecucionManualEnCurso = true;
                    _ultimaEjecucionManual = DateTime.Now;
                }
                else
                {
                    Console.WriteLine("Ya hay una asignación en proceso, omitiendo ejecución automática.");
                    return false;
                }
            }

            try
            {

                if (!esManual && (DateTime.Now - _ultimaEjecucionManual).TotalMinutes < 15)
                {
                    Console.WriteLine("Se ejecutó recientemente una asignación manual. Bloqueando la automática.");
                    return false;
                }

                Console.WriteLine($" Iniciando asignación {(esManual ? "MANUAL" : "AUTOMÁTICA")}...");


                bool resultadoWhatsapp = await AsignacionAutomatizadaAsesorWhatsapp(Usuario, _cts.Token);
                bool resultadoNormal = await AsignacionAutomatizadaAsesor(Usuario, _cts.Token);

                return resultadoWhatsapp || resultadoNormal;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(" La asignación fue cancelada antes de completarse.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error en la asignación automatizada: {ex.Message}");
                return false;
            }
            finally
            {
                _semaforo.Release();
                _ejecucionManualEnCurso = false;
                Console.WriteLine("Asignación finalizada, permitiendo nuevas ejecuciones.");
            }
        }

        public async Task<bool> EjecutarAsignacionDatosAutomatizada(string Usuario)
        {
            bool esManual = !Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase);

            if (!_semaforo.Wait(0))
            {
                lock (_semaforo)
                {
                    if (esManual)
                    {
                        Console.WriteLine("Cancelando asignación automática para priorizar la manual...");
                        _cts.Cancel(); // Cancela cualquier asignación automática en proceso
                        Task.Delay(500).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Ya hay una asignación en proceso. Bloqueando el automático.");
                        return false;
                    }
                }
            }

            lock (_semaforo)
            {
                if (esManual)
                {
                    _ejecucionManualEnCurso = true;
                }
                else
                {
                    if (_ejecucionAutomaticaEnCurso)
                    {
                        Console.WriteLine(" Ya hay una asignación automática en curso. Bloqueando nuevo automático.");
                        return false;
                    }
                    _ejecucionAutomaticaEnCurso = true;
                }
            }

            try
            {
                Console.WriteLine($" Iniciando asignación {(esManual ? "MANUAL" : "AUTOMÁTICA")}...");

                bool resultadoWhatsapp = await AsignacionAutomatizadaAsesorWhatsapp(Usuario, _cts.Token);
                bool resultadoNormal = await  AsignacionAutomatizadaAsesor(Usuario, _cts.Token);

                return resultadoWhatsapp || resultadoNormal;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(" La asignación fue cancelada antes de completarse.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error en la asignación automatizada: {ex.Message}");
                return false;
            }
            finally
            {
                lock (_semaforo)
                {
                    if (esManual)
                    {
                        _ejecucionManualEnCurso = false;
                    }
                    else
                    {
                        _ejecucionAutomaticaEnCurso = false;
                    }
                    if (_semaforo.CurrentCount == 0)
                    {
                        _semaforo.Release();
                    }
                }

                //  _semaforo.Release();
                Console.WriteLine(" Asignación finalizada, permitiendo nuevas ejecuciones.");
            }
        }

        public async Task<bool> EjecutarAsignacionDatosAuto(string Usuario)
        {
            bool esManual = !Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase);

            if (!_semaforo.Wait(0))
            {
                lock (_semaforo)
                {
                    if (esManual)
                    {
                        Console.WriteLine("Cancelando asignación automática para priorizar la manual...");
                        _cts.Cancel(); // Cancela cualquier asignación automática en proceso
                        Task.Delay(500).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Ya hay una asignación en proceso. Bloqueando el automático.");
                        return false;
                    }
                }
            }

            lock (_semaforo)
            {
                if (esManual)
                {
                    _ejecucionManualEnCurso = true;
                }
                else
                {
                    if (_ejecucionAutomaticaEnCurso)
                    {
                        Console.WriteLine(" Ya hay una asignación automática en curso. Bloqueando nuevo automático.");
                        return false;
                    }
                    _ejecucionAutomaticaEnCurso = true;
                }
            }

            try
            {
                Console.WriteLine($" Iniciando asignación {(esManual ? "MANUAL" : "AUTOMÁTICA")}...");

                bool resultadoWhatsapp = await AsignacionAutoAsesorWhatsapp(Usuario, _cts.Token);
                bool resultadoNormal = await AsignacionAutomatizadaAsesorAuto(Usuario, _cts.Token);

                return resultadoWhatsapp || resultadoNormal;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(" La asignación fue cancelada antes de completarse.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Error en la asignación automatizada: {ex.Message}");
                return false;
            }
            finally
            {
                lock (_semaforo)
                {
                    if (esManual)
                    {
                        _ejecucionManualEnCurso = false;
                    }
                    else
                    {
                        _ejecucionAutomaticaEnCurso = false;
                    }
                    if (_semaforo.CurrentCount == 0)
                    {
                        _semaforo.Release();
                    }
                }

                //  _semaforo.Release();
                Console.WriteLine(" Asignación finalizada, permitiendo nuevas ejecuciones.");
            }
        }






        public async Task<bool> AsignacionAutomatizadaAsesorWhatsapp(string Usuario, CancellationToken token)
        {
            try
            {
                //EnvioCorreoAsignacion("Inicio de la asignacion de datos");

                if (!Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase))
                {
                    EnvioCorreoAsignacion("Inicio de la asignación de datos");
                }
                List<ObtenerOportunidadConfiguradaV2DTO> ListaOportunidadesConfiguradas = _unitOfWork.AsignacionRegularRepository.ObtenerOportunidadConfigurada();

                int indiceAsesor = 0;
                var asesoresActivosPorConfiguracion = new Dictionary<int, List<ObtenerAsesoresPorOportunidadDTO>>();

                foreach (var oportunidad in ListaOportunidadesConfiguradas)
                {

                    try
                    {

                        if (!asesoresActivosPorConfiguracion.ContainsKey(oportunidad.IdPGeneral.Value))
                        {
                            List<ObtenerAsesoresPorOportunidadDTO> asesores = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresPorOportunidad(oportunidad.IdPGeneral);
                            asesoresActivosPorConfiguracion[oportunidad.IdPGeneral.Value] = asesores.Where(asesor =>
                                asesor.ActivarAsignacionPaisConfiguracion == true && asesor.ActivarAsignacionAutomatica == true && asesor.IdPGeneral == oportunidad.IdPGeneral).ToList();
                        }

                        var asesoresActivos = asesoresActivosPorConfiguracion[oportunidad.IdPGeneral.Value];

                        if (asesoresActivos.Count == 0)
                        {
                            continue;
                        }

                        var asesor = asesoresActivos[indiceAsesor % asesoresActivos.Count];

                        if (asesor.CantidadTotal < asesor.TopeOportunidad || oportunidad.AsignacionDirecta == true || oportunidad.AsignacionDirectaWhatsapp == true || oportunidad.AsigancionDirectaMailing == true)
                        {
                            AsignarAsesorManualDTO data = new AsignarAsesorManualDTO
                            {
                                IdOportunidades = new int?[] { oportunidad.Id },
                                IdAsesor = asesor.IdPersonal,
                                FechaProgramada = null,
                                IdCentroCosto = new int(),
                                SegunMejorPro = false
                            };

                            var ListaDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetalle(asesor.Id.Value);
                            bool estadoAsignacion = false;

                            foreach (var detalleConfiguracionPais in ListaDetalle)
                            {

                                if (detalleConfiguracionPais.IdPais == oportunidad.IdPais)
                                {

                                    if (oportunidad.AsignacionDirectaWhatsapp == true && detalleConfiguracionPais.DatoCalidadWhatsapp == true)
                                    {
                                        data.SegunMejorPro = true;
                                        data.envioWhats = true;

                                        AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                        whats.AsignarAsesor(data, Usuario);
                                        estadoAsignacion = true;
                                        break;
                                    }

                                }
                            }

                            if (estadoAsignacion)
                            {
                                indiceAsesor++;

                            }
                        }
                    }
                    catch (Exception exAsesores)
                    {
                        Console.WriteLine($"Error al obtener asesores para la oportunidad {oportunidad.Id}: {exAsesores.Message}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool>   AsignacionAutoAsesorWhatsapp(string Usuario, CancellationToken token)
        {
            try
            {
                EnvioCorreoAsignacion("Inicio de la asignacion de datos");

                if (!Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase))
                {
                    EnvioCorreoAsignacion("Inicio de la asignación de datos");
                }
                List<ObtenerOportunidadConfiguradaV2DTO> ListaOportunidadesConfiguradas = _unitOfWork.AsignacionRegularRepository.ObtenerOportunidadConfigurada();

                int indiceAsesor = 0;
                var asesoresActivosPorConfiguracion = new Dictionary<int, List<ObtenerAsesoresPorOportunidadDTO>>();

                foreach (var oportunidad in ListaOportunidadesConfiguradas)
                {

                    try
                    {

                        if (!asesoresActivosPorConfiguracion.ContainsKey(oportunidad.IdPGeneral.Value))
                        {
                            List<ObtenerAsesoresPorOportunidadDTO> asesores = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresPorOportunidad(oportunidad.IdPGeneral);
                            asesoresActivosPorConfiguracion[oportunidad.IdPGeneral.Value] = asesores.Where(asesor =>
                                asesor.ActivarAsignacionPaisConfiguracion == true && asesor.ActivarAsignacionAutomatica == true && asesor.IdPGeneral == oportunidad.IdPGeneral).ToList();
                        }

                        var asesoresActivos = asesoresActivosPorConfiguracion[oportunidad.IdPGeneral.Value];

                        if (asesoresActivos.Count == 0)
                        {
                            continue;
                        }

                        var asesor = asesoresActivos[indiceAsesor % asesoresActivos.Count];

                        if (asesor.CantidadTotal < asesor.TopeOportunidad || oportunidad.AsignacionDirecta == true || oportunidad.AsignacionDirectaWhatsapp == true || oportunidad.AsigancionDirectaMailing == true)
                        {
                            AsignarAsesorManualDTO data = new AsignarAsesorManualDTO
                            {
                                IdOportunidades = new int?[] { oportunidad.Id },
                                IdAsesor = asesor.IdPersonal,
                                FechaProgramada = null,
                                IdCentroCosto = new int(),
                                SegunMejorPro = false
                            };

                            var ListaDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetalle(asesor.Id.Value);
                            bool estadoAsignacion = false;

                            foreach (var detalleConfiguracionPais in ListaDetalle)
                            {

                                if (detalleConfiguracionPais.IdPais == oportunidad.IdPais)
                                {

                                    if (oportunidad.AsignacionDirectaWhatsapp == true && detalleConfiguracionPais.DatoCalidadWhatsapp == true)
                                    {
                                        data.SegunMejorPro = true;
                                        data.envioWhats = true;

                                        AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                        whats.AsignarAsesorAuto(data, Usuario);
                                        estadoAsignacion = true;
                                        break;
                                    }

                                }
                            }

                            if (estadoAsignacion)
                            {
                                indiceAsesor++;

                            }
                        }
                    }
                    catch (Exception exAsesores)
                    {
                        Console.WriteLine($"Error al obtener asesores para la oportunidad {oportunidad.Id}: {exAsesores.Message}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AsignacionAutomatizadaAsesor(string Usuario, CancellationToken token)
        {
            try
            {
                if (!Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase))
                {
                    EnvioCorreoAsignacion("Inicio de la asignación de datos");
                }
                // EnvioCorreoAsignacion("Inicio de la asignacion de datos");

                bool? EstadoActualizacion = false;
                List<ObtenerOportunidadConfiguradaV2DTO> ListaOportunidadesConfiguradas = new List<ObtenerOportunidadConfiguradaV2DTO>();
                ListaOportunidadesConfiguradas = _unitOfWork.AsignacionRegularRepository.ObtenerOportunidadConfigurada();

                foreach (ObtenerOportunidadConfiguradaV2DTO Opor in ListaOportunidadesConfiguradas)
                {
                    try
                    {


                        List<ObtenerAsesoresPorOportunidadDTO> ListaAsesoresPorCofiguracion = new List<ObtenerAsesoresPorOportunidadDTO>();
                        ListaAsesoresPorCofiguracion = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresPorOportunidad(Opor.IdPGeneral);
                        foreach (ObtenerAsesoresPorOportunidadDTO asesor in ListaAsesoresPorCofiguracion)
                        {
                            if (asesor.ActivarAsignacionPaisConfiguracion == true && asesor.ActivarAsignacionAutomatica == true && asesor.IdPGeneral == Opor.IdPGeneral)
                            {
                                if (asesor.CantidadTotal < asesor.TopeOportunidad || (Opor.AsignacionDirecta == true || Opor.AsignacionDirectaWhatsapp == true || Opor.AsigancionDirectaMailing == true))
                                {
                                    AsignarAsesorManualDTO? data = new AsignarAsesorManualDTO();
                                    data.IdOportunidades = new int?[] { Opor.Id };
                                    data.IdAsesor = asesor.IdPersonal;
                                    data.FechaProgramada = null;
                                    data.IdCentroCosto = new int();
                                    data.SegunMejorPro = false;
                                    var ListaDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetalle(asesor.Id.Value);
                                    var estadoAsignacion = false;
                                    foreach (var detalleConfiguracionPais in ListaDetalle)
                                    {
                                        if (detalleConfiguracionPais.IdPais == Opor.IdPais)
                                        {
                                            if (Opor.AsignacionDirecta == true && detalleConfiguracionPais.DatoCalidad == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                whats.AsignarAsesor(data, Usuario);
                                                estadoAsignacion = true;
                                                //EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                break;
                                            }
                                            if (Opor.AsignacionDirectaWhatsapp == true && detalleConfiguracionPais.DatoCalidadWhatsapp == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);

                                                whats.AsignarAsesor(data, Usuario);
                                                //EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                estadoAsignacion = true;
                                                break;
                                            }
                                            if (Opor.AsigancionDirectaMailing == true && detalleConfiguracionPais.DatoCalidadMailing == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                whats.AsignarAsesor(data, Usuario);
                                                //EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                estadoAsignacion = true;
                                                break;
                                            }

                                            if (Opor.AsignacionRegular == true && Opor.AsignacionDirecta == false && Opor.AsignacionDirectaWhatsapp == false && Opor.AsigancionDirectaMailing == false)
                                            {

                                                var cantidad = 0;

                                                switch (detalleConfiguracionPais.IdPais)
                                                {
                                                    case 51:
                                                        cantidad = asesor.CantidadTotalPeru.GetValueOrDefault();
                                                        break;
                                                    case 52:
                                                        cantidad = asesor.CantidadTotalMexico.GetValueOrDefault();
                                                        break;
                                                    case 56:
                                                        cantidad = asesor.CantidadTotalChile.GetValueOrDefault();
                                                        break;
                                                    case 57:
                                                        cantidad = asesor.CantidadTotalColombia.GetValueOrDefault();
                                                        break;
                                                    case 591:
                                                        cantidad = asesor.CantidadTotalBolivia.GetValueOrDefault();
                                                        break;
                                                    case 0:
                                                        cantidad = asesor.CantidadTotalInternacional.GetValueOrDefault();
                                                        break;
                                                }

                                                if (cantidad < detalleConfiguracionPais.Distribucion && detalleConfiguracionPais.Distribucion > 0)
                                                {
                                                    data.envioWhats = true;

                                                    AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                    whats.AsignarAsesor(data, Usuario);
                                                    estadoAsignacion = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (estadoAsignacion == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }

                if (!Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase))
                {
                    EnvioCorreoAsignacion("Fin Asignacion de datos");
                }

                return true;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AsignacionAutomatizadaAsesorAuto(string Usuario, CancellationToken token)
        {
            try
            {
                if (!Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase))
                {
                    EnvioCorreoAsignacion("Inicio de la asignación de datos");
                }
                EnvioCorreoAsignacion("Inicio de la asignacion de datos");

                bool? EstadoActualizacion = false;
                List<ObtenerOportunidadConfiguradaV2DTO> ListaOportunidadesConfiguradas = new List<ObtenerOportunidadConfiguradaV2DTO>();
                ListaOportunidadesConfiguradas = _unitOfWork.AsignacionRegularRepository.ObtenerOportunidadConfigurada();

                foreach (ObtenerOportunidadConfiguradaV2DTO Opor in ListaOportunidadesConfiguradas)
                {
                    try
                    {


                        List<ObtenerAsesoresPorOportunidadDTO> ListaAsesoresPorCofiguracion = new List<ObtenerAsesoresPorOportunidadDTO>();
                        ListaAsesoresPorCofiguracion = _unitOfWork.AsignacionRegularRepository.ObtenerAsesoresPorOportunidad(Opor.IdPGeneral);
                        foreach (ObtenerAsesoresPorOportunidadDTO asesor in ListaAsesoresPorCofiguracion)
                        {
                            if (asesor.ActivarAsignacionPaisConfiguracion == true && asesor.ActivarAsignacionAutomatica == true && asesor.IdPGeneral == Opor.IdPGeneral)
                            {
                                if (asesor.CantidadTotal < asesor.TopeOportunidad || (Opor.AsignacionDirecta == true || Opor.AsignacionDirectaWhatsapp == true || Opor.AsigancionDirectaMailing == true))
                                {
                                    AsignarAsesorManualDTO? data = new AsignarAsesorManualDTO();
                                    data.IdOportunidades = new int?[] { Opor.Id };
                                    data.IdAsesor = asesor.IdPersonal;
                                    data.FechaProgramada = null;
                                    data.IdCentroCosto = new int();
                                    data.SegunMejorPro = false;
                                    var ListaDetalle = _unitOfWork.AsignacionRegularRepository.ObtenerConfiguracionDetalle(asesor.Id.Value);
                                    var estadoAsignacion = false;
                                    foreach (var detalleConfiguracionPais in ListaDetalle)
                                    {
                                        if (detalleConfiguracionPais.IdPais == Opor.IdPais)
                                        {
                                            if (Opor.AsignacionDirecta == true && detalleConfiguracionPais.DatoCalidad == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                whats.AsignarAsesorAuto(data, Usuario);
                                                estadoAsignacion = true;
                                                EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                break;
                                            }
                                            if (Opor.AsignacionDirectaWhatsapp == true && detalleConfiguracionPais.DatoCalidadWhatsapp == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);

                                                whats.AsignarAsesorAuto(data, Usuario);
                                                EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                estadoAsignacion = true;
                                                break;
                                            }
                                            if (Opor.AsigancionDirectaMailing == true && detalleConfiguracionPais.DatoCalidadMailing == true)
                                            {
                                                data.SegunMejorPro = true;
                                                data.envioWhats = true;

                                                AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                whats.AsignarAsesorAuto(data, Usuario);
                                                EnvioCorreoAsignacion("Fin Asignacion de datos");
                                                estadoAsignacion = true;
                                                break;
                                            }

                                            if (Opor.AsignacionRegular == true && Opor.AsignacionDirecta == false && Opor.AsignacionDirectaWhatsapp == false && Opor.AsigancionDirectaMailing == false)
                                            {

                                                var cantidad = 0;

                                                switch (detalleConfiguracionPais.IdPais)
                                                {
                                                    case 51:
                                                        cantidad = asesor.CantidadTotalPeru.GetValueOrDefault();
                                                        break;
                                                    case 52:
                                                        cantidad = asesor.CantidadTotalMexico.GetValueOrDefault();
                                                        break;
                                                    case 56:
                                                        cantidad = asesor.CantidadTotalChile.GetValueOrDefault();
                                                        break;
                                                    case 57:
                                                        cantidad = asesor.CantidadTotalColombia.GetValueOrDefault();
                                                        break;
                                                    case 591:
                                                        cantidad = asesor.CantidadTotalBolivia.GetValueOrDefault();
                                                        break;
                                                    case 0:
                                                        cantidad = asesor.CantidadTotalInternacional.GetValueOrDefault();
                                                        break;
                                                }

                                                if (cantidad < detalleConfiguracionPais.Distribucion && detalleConfiguracionPais.Distribucion > 0)
                                                {
                                                    data.envioWhats = true;

                                                    AsignacionManualService whats = new AsignacionManualService(_unitOfWork);
                                                    whats.AsignarAsesorAuto(data, Usuario);
                                                    estadoAsignacion = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (estadoAsignacion == true)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                }

                if (!Usuario.Equals("SYSTEMV5", StringComparison.OrdinalIgnoreCase))
                {
                    EnvioCorreoAsignacion("Fin Asignacion de datos");
                }

                return true;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioCorreoAsignacion(string mensaje)
        {
            try
            {
                List<string> correosAlerta = new List<string>();
                correosAlerta.Add("balmanzam@bsginstitute.com");
                correosAlerta.Add("jrivera@bsginstitute.com");
                correosAlerta.Add("loscataf@bsginstitute.com");
                correosAlerta.Add("jllanque@bsginstitute.com");


                var mailServiceAlerta = new TMK_MailService();
                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                mailDataAlerta.Subject = "Asignacion de datos";
                mailDataAlerta.Message = mensaje;
                mailDataAlerta.Bcc = string.Empty;
                mailDataAlerta.AttachedFiles = null;
                mailServiceAlerta.SetData(mailDataAlerta);
                mailServiceAlerta.SendMessageTask();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EnvioCorreoAsignacion: {ex.Message}");
                return false;
            }
        }


        public bool EnvioCorreoValidado(string mensaje)
        {
            try
            {
                List<string> correosAlerta = new List<string>();
                correosAlerta.Add("jrivera@bsginstitute.com");
                correosAlerta.Add("balmanzam@bsginstitute.com");
                correosAlerta.Add("loscataf@bsginstitute.com");
                correosAlerta.Add("jllanque@bsginstitute.com");
                var mailServiceAlerta = new TMK_MailService();
                TMKMailDataDTO mailDataAlerta = new TMKMailDataDTO();
                mailDataAlerta.Sender = "ccrispin@bsginstitute.com";
                mailDataAlerta.Recipient = string.Join(",", correosAlerta);
                mailDataAlerta.Subject = "Asignacion de datos Validacion";
                mailDataAlerta.Message = mensaje;
                mailDataAlerta.Bcc = string.Empty;
                mailDataAlerta.AttachedFiles = null;
                mailServiceAlerta.SetData(mailDataAlerta);
                mailServiceAlerta.SendMessageTask();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in EnvioCorreoAsignacion: {ex.Message}");
                return false;
            }
        }

    }


}

