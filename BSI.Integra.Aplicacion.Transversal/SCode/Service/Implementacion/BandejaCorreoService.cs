using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using MailBee.ImapMail;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: BandejaCorreoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_BandejaCorreo
    /// </summary>
    public class BandejaCorreoService : IBandejaCorreoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public BandejaCorreoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la bandeja de Entrada del servicio Imap, de acuerdo al filtrado solicitado por el Asesor.
        /// </summary>
        /// <param name="email"> Correo </param>
        /// <param name="passwordCorreo"> Contraseña del correo</param>
        /// <returns> BandejaCorreoDTO </returns>
        public BandejaCorreoDTO ObtenerBandejaEntradaMailInbox(ObtenerBandejaEntradaArgumentosDTO argumentos)
        {
            int firstIndex = 0, lastIndex = 0;
            int primero = 0;
            int segundo = 0;
            bool buscarFiltro = false;
            string rango_datos = string.Empty;
            int totalCorreos = 0;

            try
            {
                BandejaCorreoDTO bandejaCorreo = new BandejaCorreoDTO();
                bandejaCorreo.ListaCorreos = new List<CorreoDTO>();
                MessageNumberCollection numeroMensajes = new();
                EnvelopeCollection msgsc = new();

                TMK_ImapService imapService = new TMK_ImapService(argumentos.Folder, argumentos.Email, argumentos.Password);
                imapService.Folders();

                if (argumentos.FiltroKendo == null || argumentos.FiltroKendo.Filters.Count == 0)
                {
                    buscarFiltro = false;
                    totalCorreos = imapService.CantidadCorreosSinFiltro();
                }
                else if (argumentos.FiltroKendo.Filters.Count == 1)
                {
                    string valor = argumentos.FiltroKendo.Filters[0].Value;
                    switch (argumentos.FiltroKendo.Filters[0].Field.ToLower())
                    {
                        case "asunto":
                            numeroMensajes = imapService.CantidadCorreosConFiltroAsunto(valor);
                            break;
                        case "remitente":
                            numeroMensajes = imapService.CantidadCorreosConFiltroRemitente(valor);
                            break;
                        case "destinatario":
                            numeroMensajes = imapService.CantidadCorreosConFiltroDestinatario(valor);
                            break;
                        case "destinatarios":
                            numeroMensajes = imapService.CantidadCorreosConFiltroDestinatario(valor);
                            break;
                        case "fecha":
                            numeroMensajes = imapService.CantidadCorreosConFiltroFecha(DateTime.Today.AddDays(-10));//ayer
                            break;
                    }
                    totalCorreos = numeroMensajes.Count;
                    buscarFiltro = true;
                }
                else if (argumentos.FiltroKendo.Filters.Count == 2)
                {
                    string asunto = string.Empty, remitente = string.Empty;
                    foreach (var item in argumentos.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "Asunto":
                                asunto = item.Value;
                                break;
                            case "Remitente":
                                remitente = item.Value;
                                break;
                        }
                    }
                    numeroMensajes = imapService.CantidadCorreosConFiltroAsuntoRemitente(asunto, remitente);
                    totalCorreos = numeroMensajes.Count;
                    buscarFiltro = true;
                }

                if (totalCorreos > 0 && argumentos.PageSize > 0)
                {
                    if (!buscarFiltro)
                    {
                        if (totalCorreos > argumentos.PageSize)
                        {
                            firstIndex = totalCorreos - argumentos.Skip;
                            if (firstIndex <= argumentos.PageSize)
                            {
                                lastIndex = 1;
                            }
                            else
                            {
                                lastIndex = firstIndex - (argumentos.PageSize - 1);
                            }
                        }
                        else
                        {
                            firstIndex = 1;
                            lastIndex = totalCorreos;
                        }
                        msgsc = imapService.ObtenerCorreos(firstIndex.ToString() + ":" + lastIndex.ToString());
                    }
                    else
                    {
                        if (totalCorreos > argumentos.PageSize)
                        {
                            primero = totalCorreos - argumentos.Skip;
                            segundo = primero - argumentos.PageSize;
                            if (segundo < 0) segundo = 0;
                        }
                        else
                        {
                            primero = totalCorreos;
                            segundo = 0;
                            argumentos.PageSize = totalCorreos;
                        }

                        string[] datos_buscados = new string[argumentos.PageSize];
                        int contador = 0;
                        for (int i = segundo; i < primero; i++)
                        {
                            datos_buscados[contador] = numeroMensajes[i].ToString();
                            contador++;
                        }
                        rango_datos = string.Join(",", datos_buscados);
                        msgsc = imapService.ObtenerCorreos(rango_datos);
                    }
                    string adjunto = string.Empty;
                    bool hasAttachments = false;
                    bool correoLeido = false;

                    foreach (Envelope msg in msgsc)
                    {
                        var objHeader = new CorreoDTO();
                        objHeader.Id = msg.MessageNumber;
                        objHeader.Remitente = msg.From;
                        objHeader.From = argumentos.Email;
                        objHeader.Destinatarios = msg.To;

                        if (msg.Cc.Count > 0)
                        {
                            objHeader.ConCopia = msg.Cc.ToString();
                        }
                        else
                        {
                            objHeader.ConCopia = "";
                        }

                        ImapBodyStructureCollection parts = msg.BodyStructure.GetAllParts();
                        foreach (ImapBodyStructure part in parts)
                        {
                            if ((part.Disposition != null && part.Disposition.ToLower() == "attachment") ||
                                (part.Filename != null && part.Filename != string.Empty) ||
                                (part.ContentType != null && part.ContentType.ToLower() == "message/rfc822"))
                            {
                                hasAttachments = true;
                                break;
                            }
                        }
                        if (msg.Flags != null)
                        {
                            if (msg.Flags.SystemFlags.ToString().Contains("Seen"))
                                correoLeido = true;
                            else
                                correoLeido = false;
                        }
                        if (hasAttachments)
                        {
                            adjunto = "(A) ";
                            hasAttachments = false;
                        }
                        else
                        {
                            adjunto = string.Empty;
                        }

                        objHeader.Fecha = msg.DateReceived;
                        objHeader.Asunto = adjunto + msg.Subject.ToString();
                        objHeader.Seen = correoLeido;
                        if (objHeader.Remitente == "openvox@bsginstitute.com")
                        {
                            var datos = msg.Subject.ToString().Split(" ");
                            if (datos[0] != null)
                            {
                                var celular = datos[0].Replace("+51", "").Replace("+57", "").Replace("+591", "");
                                var alumno = _unitOfWork.AlumnoRepository.ObtenerAlumnoPorCelular(celular);
                                var existeAsesor = _unitOfWork.PersonalRepository.ExistePersonalPorCorreo(argumentos.Email);
                                if (alumno != null && existeAsesor.Valor!.Value)
                                {

                                    var oportunidad = _unitOfWork.OportunidadRepository.ObtenerOportunidadBandejaCorreoPorIdAlumno(alumno.Id);
                                    var areaVentasMasivo = _unitOfWork.AlumnoRepository.ObtenerEnvioMasivoSMSPorIdAlumno(alumno.Id);
                                    if (oportunidad != null)
                                    {
                                        if (areaVentasMasivo != null)
                                        {
                                            objHeader.Asunto = "ENVIO MASIVO - " + areaVentasMasivo.Valor + " - Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - CC: " + oportunidad.NombreCentroCosto + " - FASE: " + oportunidad.Codigo + " - ASESOR: " + oportunidad.Asesor;
                                        }
                                        else
                                        {
                                            objHeader.Asunto = "Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - CC: " + oportunidad.NombreCentroCosto + " - FASE: " + oportunidad.Codigo + " - ASESOR: " + oportunidad.Asesor;
                                        }
                                    }
                                    else
                                    {
                                        if (areaVentasMasivo != null)
                                        {
                                            objHeader.Asunto = "ENVIO MASIVO - " + areaVentasMasivo.Valor + " - Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - Sin Oportunidad en Seguimiento";
                                        }
                                        else
                                        {
                                            objHeader.Asunto = "Respuesta SMS de : (" + alumno.Celular + " - " + alumno.Email1 + ") " + alumno.Nombre1 + " " + alumno.Nombre2 + " " + alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno + " - Sin Oportunidad en Seguimiento";
                                        }
                                    }
                                }
                            }
                        }
                        bandejaCorreo.ListaCorreos.Add(objHeader);
                    }
                }
                imapService.Desconectar();

                if (bandejaCorreo.ListaCorreos != null)
                {
                    bandejaCorreo.ListaCorreos = bandejaCorreo.ListaCorreos.OrderByDescending(x => x.Id).ToList();
                    bandejaCorreo.TotalEnviados = totalCorreos;

                }
                return bandejaCorreo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BandejaCorreoDTO ObtenerCorreoRecibido(FiltroBandejaCorreoDTO filtroBandejaCorreo)
        {
            if (filtroBandejaCorreo.IdAsesor <= 0)
            {
                throw new BadRequestException("#CS-OCR-001@El Asesor no Existe.");
            }
            try
            {
                CorreoClienteCredencialDTO correoClienteCredencialDTO = _unitOfWork.GmailClienteRepository.ObtenerClienteCredencial(filtroBandejaCorreo.IdAsesor);
                BandejaCorreoDTO lista = new BandejaCorreoDTO();
                if (correoClienteCredencialDTO != null)
                {
                    var argumentos = new ObtenerBandejaEntradaArgumentosDTO()
                    {
                        Email = correoClienteCredencialDTO.EmailAsesor,
                        Password = correoClienteCredencialDTO.PasswordCorreo,
                        Folder = filtroBandejaCorreo.Folder,
                        FiltroKendo = filtroBandejaCorreo.FiltroKendo,
                        PageSize = filtroBandejaCorreo.PageSize,
                        Skip = filtroBandejaCorreo.Skip
                    };
                    lista = ObtenerBandejaEntradaMailInbox(argumentos);
                    if (filtroBandejaCorreo.IdAlumno != null)
                    {
                        var interacciones = _unitOfWork.MandrilRepository.ListaInteraccionCorreoAlumnoCorreo((int)filtroBandejaCorreo.IdAlumno, filtroBandejaCorreo.IdAsesor);
                        lista.ListaCorreos.AddRange(interacciones);
                        lista.ListaCorreos = lista.ListaCorreos.OrderBy(x => x.Fecha).ToList();
                        lista.TotalEnviados = lista.ListaCorreos.Count;
                    }
                    return lista;
                }
                if (filtroBandejaCorreo.IdAlumno != null)
                {
                    var interacciones = _unitOfWork.MandrilRepository.ListaInteraccionCorreoAlumnoCorreo((int)filtroBandejaCorreo.IdAlumno, filtroBandejaCorreo.IdAsesor);
                    lista.ListaCorreos.AddRange(interacciones);
                    lista.ListaCorreos = lista.ListaCorreos.OrderBy(x => x.Fecha).ToList();
                    lista.TotalEnviados = lista.ListaCorreos.Count;
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
