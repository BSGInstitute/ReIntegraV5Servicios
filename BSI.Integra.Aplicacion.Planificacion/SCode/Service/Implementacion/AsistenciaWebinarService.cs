using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class AsistenciaWebinarService : IAsistenciaWebinarService
    {
        private IUnitOfWork _unitOfWork;
        private const int ID_PLANTILLA_CANCELACION_WEBINAR = 2076;

        public AsistenciaWebinarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public RptaAsistenciaWebinarDTO AsistenciaWebinar(WebinarAlumnoAsistenciaDTO asistencia)
        {
            if(!_unitOfWork.MatriculaCabeceraRepository.Exist(asistencia.IdMatriculaCabecera))
            {
                throw new Exception("La matrícula no es válida.");
            }
            if (!_unitOfWork.PEspecificoSesionRepository.Exist(asistencia.IdPEspecificoSesion))
            {
                throw new Exception("El webinar no es válido.");
            }
            if (_unitOfWork.PEspecificoSesionRepository.EsWebinarPasado(asistencia.IdPEspecificoSesion))
            {
                throw new Exception("El webinar ya finalizó");
            }
            var webinarConfirmacion = _unitOfWork.ConfirmacionWebinarRepository.ObtenerConfirmacionWebinarPorIdMatriculaYIdSesion(asistencia.IdMatriculaCabecera, asistencia.IdPEspecificoSesion);
            if (_unitOfWork.PEspecificoSesionRepository.ObtenerPorId(asistencia.IdPEspecificoSesion).EsWebinarConfirmado is false)
            {
                throw new Exception("Este webinar ya fue cancelado");
            }

            if (webinarConfirmacion != null)
            {
                if (webinarConfirmacion.Confirmo != asistencia.EstadoAsistencia)
                {
                    webinarConfirmacion.Confirmo = asistencia.EstadoAsistencia;
                    webinarConfirmacion.Asistio = false;
                    webinarConfirmacion.UsuarioModificacion = "system";
                    webinarConfirmacion.FechaModificacion = DateTime.Now;
                } else
                {
                    if (webinarConfirmacion.Confirmo) {
                        throw new Exception("Su participación en este webinar ya fue confirmada. Solo puede realizar esta acción una vez por webinar.");
                    }
                    else
                    {
                        throw new Exception("Su participación en este webinar ya fue cancelada. No puede realizar esta acción nuevamente.");
                    }
                }
            }
            else
            {
                webinarConfirmacion = new AsistenciaConfirmacionWebinarDTO()
                {
                    IdMatriculaCabecera = asistencia.IdMatriculaCabecera,
                    IdPEspecificoSesion = asistencia.IdPEspecificoSesion,
                    Confirmo = asistencia.EstadoAsistencia,
                    Asistio = false,
                    Estado = true,
                    UsuarioCreacion = "system",
                    UsuarioModificacion = "system",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
            }
            if (webinarConfirmacion == null)
            {
                throw new Exception("Ocurrió un inconveniente, por favor intente nuevamente.");
            }
            if (webinarConfirmacion.Id != 0 && webinarConfirmacion.Id != null)
            {
                var update = _unitOfWork.ConfirmacionWebinarRepository.Actualizar(webinarConfirmacion);
            }
            else
            {
                var insert = _unitOfWork.ConfirmacionWebinarRepository.Insertar(webinarConfirmacion);
            }
            var alumnoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerDetalleSesionAlumnoPorIdSesionYIdMatriculaCabecera(asistencia.IdPEspecificoSesion, asistencia.IdMatriculaCabecera);
            var msg = "";
            if (!webinarConfirmacion.Confirmo)
            {
                msg = "Su participación en este webinar ya fue cancelada.";
            } else
            {
                msg = "Su participación en este webinar se confirmo. Agradecemos su participación.";
            }

            return new RptaAsistenciaWebinarDTO
            {
                Alumno = alumnoSesion,
                Mensaje = msg
            };
        }
        public RptaConfirmacionWebinarAutomaticaDTO ConfirmacionWebinarAutomatica(int IdPEspecificoSesion)
        {
            try
            {
                if (_unitOfWork.PEspecificoSesionRepository.EsWebinarPasado(IdPEspecificoSesion))
                {
                    return new RptaConfirmacionWebinarAutomaticaDTO { Estado = false, Mensaje = "El webinar ya finalizó" };
                }
                var sesion = _unitOfWork.PGeneralRepository.ObtenerWebinarPorIdPEspecificoSesion(IdPEspecificoSesion);
                if (sesion is null)
                {
                    return new RptaConfirmacionWebinarAutomaticaDTO { Estado = false, Mensaje = "Webinar no encontrado" };
                }
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(IdPEspecificoSesion);
                if (programaEspecificoSesion.EsWebinarConfirmado is false) {
                    return new RptaConfirmacionWebinarAutomaticaDTO { Estado = false, Mensaje = "Este webinar ya fue cancelado" };
                }
                else if (sesion.TotalParticipantesConfirmados > 0) {
                    if (programaEspecificoSesion.EsWebinarConfirmado is true)
                    {
                        return new RptaConfirmacionWebinarAutomaticaDTO { Estado = false, Mensaje = "Este webinar ya fue confirmado"};
                    }
                    programaEspecificoSesion.EsWebinarConfirmado = true;
                } else {
                    programaEspecificoSesion.EsWebinarConfirmado = null;
                }
                programaEspecificoSesion.UsuarioModificacion = "SYSTEM";
                programaEspecificoSesion.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoSesionRepository.Update(programaEspecificoSesion);
                _unitOfWork.Commit();
                string msg = programaEspecificoSesion.EsWebinarConfirmado is true ? "Webinar confirmado" : "Webinar por confirmar, sin participantes";
                return new RptaConfirmacionWebinarAutomaticaDTO
                {
                    Estado = true,
                    Mensaje = msg,
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool CancelarWebinar(CancelarWebinarDTO dto, string usuario)
        {
            try
            {
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(dto.IdPEspecificoSesion);
                if (programaEspecificoSesion == null)
                    throw new Exception($"No se encontró la sesión con Id {dto.IdPEspecificoSesion}.");
                if (programaEspecificoSesion.FechaCancelacionWebinar != null)
                    throw new Exception("El webinar ya fue cancelado anteriormente. No se puede volver a cancelar ni reenviar las notificaciones.");

                programaEspecificoSesion.FechaCancelacionWebinar = DateTime.Now;
                programaEspecificoSesion.ComentarioCancelacionWebinar = dto.ComentarioCancelacion;
                programaEspecificoSesion.UsuarioModificacion = !string.IsNullOrWhiteSpace(usuario) ? usuario : "SYSTEM";
                programaEspecificoSesion.EsWebinarConfirmado = dto.Confirmo;
                _unitOfWork.PEspecificoSesionRepository.Update(programaEspecificoSesion);
                _unitOfWork.Commit();

                var motivoCancelacion = dto.ComentarioCancelacion;
                var nombreWebinar = _unitOfWork.PEspecificoRepository.ObtenerNombrePEspecifico(programaEspecificoSesion.IdPespecifico);
                var alumnos = _unitOfWork.PEspecificoSesionRepository
                    .ObtenerDetalleSesionesPorAlumnosFiltrado(new SesionFiltroDTO { IdSesion = dto.IdPEspecificoSesion })
                    ?.Where(x => x.Confirmo == "CONFIRMADO")
                    .ToList() ?? new List<DetalleSesionesAlumnosDTO>();

                var correos = alumnos
                    .Where(x => !string.IsNullOrWhiteSpace(x.Email))
                    .Select(x => new AlumnoCorreoCoordinadoraDTO
                    {
                        EmailCoordinadora = x.EmailCoordinadoraAcademica?.Trim() ?? string.Empty,
                        EmailAlumno = x.Email.Trim()
                    })
                    .Distinct()
                    .ToList();

                var alumnosWsp = alumnos
                    .Where(x => !string.IsNullOrWhiteSpace(x.CelularWhatsApp))
                    .Select(x =>
                    {
                        string celular = x.CelularWhatsApp.Trim();
                        string codigoPais = x.IdPais.ToString();
                        if (celular.StartsWith("00" + codigoPais))
                            celular = celular.Substring(2);
                        else if (!celular.StartsWith(codigoPais))
                            celular = codigoPais + celular;
                        return new AlumnoWhatsAppCancelacionDTO
                        {
                            CelularWhatsApp = celular,
                            IdAlumno = x.IdAlumno,
                            IdPais = x.IdPais,
                            NombreAlumno = x.NombreAlumno ?? "",
                            IdPersonal_Asignado = x.IdCoordinadoraAcademica
                        };
                    })
                    .ToList();

                if (correos.Count > 0)
                {
                    _ = Task.Run(() => EnviarMailWebinarCancelado(correos, motivoCancelacion, nombreWebinar));
                }
                if (alumnosWsp.Count > 0)
                {
                    EnviarWhatsAppWebinarCancelado(alumnosWsp, motivoCancelacion, nombreWebinar);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void EnviarWhatsAppWebinarCancelado(List<AlumnoWhatsAppCancelacionDTO> alumnos, string motivoCancelacion, string nombreWebinar)
        {
            const string urlAtc = "https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphAtc";
            var plantilla = _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(ID_PLANTILLA_CANCELACION_WEBINAR);

            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            };

            using var client = new HttpClient(handler);

            foreach (var alumno in alumnos)
            {
                try
                {
                    string caption = (plantilla?.Texto ?? "")
                        .Replace("{tAlumnos.nombre1}", alumno.NombreAlumno)
                        .Replace("{webinar}", nombreWebinar ?? "")
                        .Replace("{comentario}", motivoCancelacion);

                    var body = new
                    {
                        Id = 0,
                        WaTo = alumno.CelularWhatsApp,
                        WaType = "hsm",
                        WaTypeMensaje = 8,
                        WaRecipientType = "hsm",
                        WaBody = plantilla?.Descripcion ?? "",
                        WaCaption = caption,
                        IdPais = alumno.IdPais,
                        EsMigracion = true,
                        IdMigracion = 0,
                        IdPersonal = alumno.IdPersonal_Asignado,
                        IdAlumno = alumno.IdAlumno,
                        usuario = "SYSTEM",
                        datosPlantillaWhatsApp = new[]
                        {
                            new { codigo = "{tAlumnos.nombre1}", texto = alumno.NombreAlumno },
                            new { codigo = "{webinar}", texto = nombreWebinar ?? "" },
                            new { codigo = "{comentario}", texto = motivoCancelacion }
                        }
                    };

                    var json = JsonConvert.SerializeObject(body);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    client.PostAsync(urlAtc, content).Wait();
                }
                catch { }
            }
        }

        private void EnviarMailWebinarCancelado(
            List<AlumnoCorreoCoordinadoraDTO> alumnosSendMail,
            string mensajeMotivoCancelado,
            string nombreWebinar
            )
        {
            string mensaje = @"
                <div style='font-family: Arial, sans-serif; max-width: 620px; margin: 0 auto; color: #333;'>
                    <div style='background-color: #002855; padding: 24px 30px;'>
                        <h1 style='color: #ffffff; margin: 0; font-size: 22px; letter-spacing: 1px;'>BSG Institute</h1>
                    </div>
                    <div style='padding: 32px 30px; background-color: #ffffff;'>
                        <h2 style='color: #002855; font-size: 18px; margin-top: 0;'>Notificación de Cancelación de Webinar</h2>
                        <p style='margin-bottom: 16px;'>Estimado/a participante,</p>
                        <p style='margin-bottom: 20px;'>
                            Le comunicamos que el webinar <strong>" + nombreWebinar + @"</strong> en el que se encontraba inscrito/a ha sido <strong>cancelado</strong>.
                        </p>
                        <div style='background-color: #f4f6f9; border-left: 4px solid #002855; padding: 16px 20px; margin-bottom: 24px; border-radius: 0 4px 4px 0;'>
                            <p style='margin: 0; font-size: 14px;'><strong>Motivo:</strong></p>
                            <p style='margin: 8px 0 0 0; font-size: 14px;'>" + mensajeMotivoCancelado + @"</p>
                        </div>
                        <p style='margin-bottom: 8px;'>
                            Lamentamos los inconvenientes que esto pueda ocasionar. Para mayor información o consultas,
                            comuníquese con su coordinadora académica.
                        </p>
                        <p style='margin-top: 32px; margin-bottom: 4px;'>Atentamente,</p>
                        <p style='margin: 0; font-weight: bold; color: #002855;'>BSG Institute</p>
                    </div>
                    <div style='background-color: #f4f6f9; padding: 14px 30px; text-align: center; font-size: 11px; color: #888;'>
                        <p style='margin: 0;'>Este mensaje fue generado automáticamente. Por favor, no responda a este correo.</p>
                    </div>
                </div>";

            var correosEnCopia = string.Join(",", new[]
            {
                "cobandot@bsginstitute.com, coordinaciondocente@bsginstitute.com, ccrispin@bsginstitute.com, ctumir@bsginstitute.com"
            });

            var correosUnicos = alumnosSendMail
                .Where(x => !string.IsNullOrWhiteSpace(x.EmailAlumno) && !string.IsNullOrWhiteSpace(x.EmailCoordinadora))
                .GroupBy(x => x.EmailAlumno.Trim().ToLower())
                .Select(g => g.First())
                .ToList();

            foreach (var alumno in correosUnicos)
            {
                try
                {
                    var mailData = new TMKMailDataDTO
                    {
                        Sender = alumno.EmailCoordinadora,
                        Recipient = alumno.EmailAlumno,
                        Subject = "Webinar cancelado",
                        Message = mensaje,
                        Cc = correosEnCopia,
                        Bcc = ""
                    };

                    var mailService = new TMK_MailService();
                    mailService.SetData(mailData);
                    mailService.SendMessageTask();

                    try
                    {
                        var gmailCorreo = new GmailCorreo
                        {
                            IdEtiqueta = 1, // sent:1, inbox:2
                            Asunto = mailData.Subject,
                            Fecha = DateTime.Now,
                            EmailBody = mailData.Message,
                            Seen = false,
                            Remitente = alumno.EmailCoordinadora,
                            Cc = correosEnCopia,
                            Bcc = "",
                            Destinatarios = alumno.EmailAlumno,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "SYSTEM",
                            UsuarioModificacion = "SYSTEM",
                            IdClasificacionPersona = 5
                        };
                        _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);
                    }
                    catch { }
                }
                catch { }
            }
        }


    }
}
