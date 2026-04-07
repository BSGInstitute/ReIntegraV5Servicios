using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using MailBee.ImapMail;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Servicios.Jobs
{
    /// Autor: Jose Vega
    /// Fecha: 12/03/2026
    /// Version: 1.0
    /// <summary>
    /// Worker en segundo plano que monitorea correos entrantes de Gmail para asesores de Planificacion.
    /// Sincroniza correos no procesados y notifica via SignalR en tiempo real.
    /// </summary>
    public class GmailPlaBackgroundWorker : BackgroundService
    {
        private readonly ILogger<GmailPlaBackgroundWorker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private HubConnection? _hubConnection;
        private readonly string _signalRUrl;
        private readonly int _pollingIntervalSeconds;

        public GmailPlaBackgroundWorker(
            ILogger<GmailPlaBackgroundWorker> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _signalRUrl = configuration["SignalR:ChatCorreoPlanificacionUrl"] ?? "https://localhost:7288/hubChatCorreoPlanificacion";
            _pollingIntervalSeconds = configuration.GetValue<int>("SignalR:PollingIntervalSeconds", 180);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando GmailPlaBackgroundWorker...");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_signalRUrl)
                .WithAutomaticReconnect()
                .Build();

            await ConectarSignalRAsync(cancellationToken);
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await AsegurarConexionSignalRAsync(stoppingToken);

                try
                {
                    await ProcesarSincronizacionCorreosAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error durante el ciclo de sincronizacion de correos.");
                }

                await Task.Delay(TimeSpan.FromSeconds(_pollingIntervalSeconds), stoppingToken);
            }
        }

        private async Task ProcesarSincronizacionCorreosAsync(CancellationToken token)
        {
            // Scope aislado solo para obtener la lista de clientes Gmail
            List<TGmailCliente> clientesGmail;
            using (var scopeListado = _serviceProvider.CreateScope())
            {
                var uowListado = scopeListado.ServiceProvider.GetRequiredService<IUnitOfWork>();
                clientesGmail = uowListado.GmailClienteRepository
                    .ObtenerGmailClientePorAreaTrabajo(7)
                    .ToList();
            }

            if (!clientesGmail.Any())
            {
                _logger.LogDebug("No hay asesores configurados para monitoreo de Gmail.");
                return;
            }

            // Scope aislado por cada asesor para evitar contaminacion del DbContext
            foreach (var cliente in clientesGmail)
            {
                if (token.IsCancellationRequested) break;

                try
                {
                    using (var scopeAsesor = _serviceProvider.CreateScope())
                    {
                        var unitOfWork = scopeAsesor.ServiceProvider.GetRequiredService<IUnitOfWork>();
                        await SincronizarBuzonAsesorAsync(cliente, unitOfWork, token);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sincronizando buzon para {Email}", cliente.EmailAsesor);
                }
            }
        }

        private async Task SincronizarBuzonAsesorAsync(TGmailCliente cliente, IUnitOfWork unitOfWork, CancellationToken token)
        {
            if (cliente.IdAsesor == null) return;

            _logger.LogDebug("Sincronizando buzon: {Email}", cliente.EmailAsesor);

            // 1. Obtener el ultimo UID guardado para este asesor en el INBOX (FolderId 1)
            long lastUid = unitOfWork.CorreoGmailRepository.ObtenerUltimoUidPorPersonal(cliente.IdAsesor.Value);

            if (string.IsNullOrWhiteSpace(cliente.PasswordCorreo))
            {
                _logger.LogWarning("El asesor {Email} no tiene password configurado. Saltando sincronización.", 
                    cliente.EmailAsesor);
                return;
            }

            // 2. Conectar al servicio IMAP usando AliasEmailAsesor (credencial de login IMAP) con fallback a EmailAsesor
            var loginEmail = cliente.AliasEmailAsesor ?? cliente.EmailAsesor;
            var imapService = new TMK_ImapService("INBOX", loginEmail, cliente.PasswordCorreo);

            try
            {
                // 3. Obtener correos nuevos (Envelopes) desde el ultimo UID
                var sobresNuevos = imapService.ObtenerCorreosNuevosDesdeUid(lastUid);

                if (sobresNuevos != null && sobresNuevos.Count > 0)
                {
                    _logger.LogInformation("Se detectaron {Count} correos nuevos para {Email}", sobresNuevos.Count, cliente.EmailAsesor);

                    // Almacenamos los TCorreoGmail retornados por Add() para obtener el Id real tras el Commit
                    var modelosInsertados = new List<TCorreoGmail>();

                    foreach (Envelope msg in sobresNuevos)
                    {
                        if (token.IsCancellationRequested) break;

                        // 4. Deteccion de adjuntos via BodyStructure
                        bool hasAttachments = false;
                        if (msg.BodyStructure != null)
                        {
                            foreach (ImapBodyStructure part in msg.BodyStructure.GetAllParts())
                            {
                                if ((part.Disposition != null && part.Disposition.ToLower() == "attachment") ||
                                    (part.Filename != null && part.Filename != string.Empty) ||
                                    (part.ContentType != null && part.ContentType.ToLower() == "message/rfc822"))
                                {
                                    hasAttachments = true;
                                    break;
                                }
                            }
                        }

                        var nuevoCorreo = new CorreoGmail
                        {
                            IdPersonal = cliente.IdAsesor,
                            IdGmailFolder = 1,
                            GmailCorreoId = msg.Uid,
                            Asunto = msg.Subject,
                            Fecha = msg.Date,
                            EmailRemitente = msg.From?.Email,
                            NombreRemitente = msg.From?.DisplayName,
                            Destinatarios = msg.To?.ToString() ?? string.Empty,
                            EmailConCopia = msg.Cc?.ToString(),
                            EmailConCopiaOculta = msg.Bcc?.ToString(),
                            EsLeido = msg.Flags != null && msg.Flags.SystemFlags.ToString().Contains("Seen"),
                            CuerpoHtml = hasAttachments ? "(A)" : "",
                            AplicaCrearOportunidad = false,
                            CumpleCriterioCrearOportunidad = false,
                            SeCreoOportunidad = false,
                            Estado = true,
                            UsuarioCreacion = "sv-pla-worker",
                            UsuarioModificacion = "sv-pla-worker",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };

                        // 5. Add retorna el TCorreoGmail trackeado por EF Core (su Id se poblara tras Commit)
                        var modeloInsertado = unitOfWork.CorreoGmailRepository.Add(nuevoCorreo);
                        modelosInsertados.Add(modeloInsertado);
                    }

                    // 6. Commit unico por asesor (Persistencia en Bloque)
                    if (modelosInsertados.Any())
                    {
                        unitOfWork.Commit();
                        _logger.LogDebug("Se persistieron {Count} registros en bloque para {Email}", modelosInsertados.Count, cliente.EmailAsesor);

                        // 7. Notificacion via SignalR usando el Id real generado por la BD
                        if (_hubConnection?.State == HubConnectionState.Connected)
                        {
                            foreach (var modelo in modelosInsertados)
                            {
                                var dtoNotificacion = new CorreoRecibidoNotificacionPlaDTO
                                {
                                    IdCorreo = modelo.Id,
                                    Asunto = modelo.Asunto ?? "(Sin Asunto)",
                                    Remitente = modelo.EmailRemitente ?? "Desconocido",
                                    FechaEnvio = modelo.Fecha,
                                    IdAsesor = cliente.IdAsesor.Value,
                                    Folder = "INBOX",
                                    Seen = modelo.EsLeido
                                };

                                await _hubConnection.InvokeAsync("NotificarNuevoCorreoPla", dtoNotificacion, token);
                            }
                            _logger.LogDebug("Se enviaron {Count} notificaciones SignalR para {Email}", modelosInsertados.Count, cliente.EmailAsesor);
                        }
                    }
                }
            }
            finally
            {
                imapService.Desconectar();
            }
        }

        private async Task ConectarSignalRAsync(CancellationToken token)
        {
            try
            {
                await _hubConnection!.StartAsync(token);
                _logger.LogInformation("GmailPlaBackgroundWorker conectado exitosamente al Hub de SignalR.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "No se pudo establecer conexion inicial con SignalR. El worker reintentara automaticamente.");
            }
        }

        /// <summary>
        /// Reintenta la conexion a SignalR si esta desconectado.
        /// WithAutomaticReconnect solo funciona despues de una conexion exitosa,
        /// por lo que si la conexion inicial falla, debemos reintentar manualmente.
        /// </summary>
        private async Task AsegurarConexionSignalRAsync(CancellationToken token)
        {
            if (_hubConnection != null && _hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _hubConnection.StartAsync(token);
                    _logger.LogInformation("Reconexion exitosa al Hub de SignalR.");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "No se pudo reconectar a SignalR. Se reintentara en el proximo ciclo.");
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deteniendo GmailPlaBackgroundWorker...");
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
            await base.StopAsync(cancellationToken);
        }
    }
}
