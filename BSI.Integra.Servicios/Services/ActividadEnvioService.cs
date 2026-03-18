using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion.WhatsAppMensajeEnviadoApiPlanificacionDTO;

namespace BSI.Integra.Servicios.Services
{
    /// Autor: Lolo Zaa
    /// Fecha: 17/03/2026
    /// Version: 1.0
    /// <summary>
    /// Encapsula el flujo completo de envio de una actividad automatica congelada:
    /// resolucion del docente, generacion de plantilla y envio por canal (Email o WhatsApp).
    /// Vive en la capa de Servicios porque necesita acceder tanto a
    /// BSI.Integra.Aplicacion.Marketing como a BSI.Integra.Aplicacion.Transversal,
    /// lo cual no es posible desde BSI.Integra.Aplicacion.Planificacion sin generar
    /// una dependencia circular (Marketing ya referencia Planificacion).
    /// </summary>
    public class ActividadEnvioService : IActividadEnvioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGestionDocenteActividadService _gestionDocenteActividadService;
        private readonly IWhatsAppMensajeEnviadoApiPlanificacionService _whatsAppService;
        private readonly ILogger<ActividadEnvioService> _logger;

        public ActividadEnvioService(
            IUnitOfWork unitOfWork,
            IGestionDocenteActividadService gestionDocenteActividadService,
            IWhatsAppMensajeEnviadoApiPlanificacionService whatsAppService,
            ILogger<ActividadEnvioService> logger)
        {
            _unitOfWork = unitOfWork;
            _gestionDocenteActividadService = gestionDocenteActividadService;
            _whatsAppService = whatsAppService;
            _logger = logger;
        }

        /// <summary>
        /// Ejecuta el envio de una actividad automatica (Email o WhatsApp).
        /// Resuelve el docente, genera la plantilla y envia por el canal
        /// correspondiente segun IdPlantillaBase (2 = Email, 8 = WhatsApp).
        /// </summary>
        public async Task<string> EnviarActividadAutomaticaAsync(
            ActividadPendienteDTO actividad, int idPersonal, string usuario)
        {
            Console.WriteLine($"     → Enviando actividad automatica");
            Console.WriteLine($"       IdGestionContacto: {actividad.IdGestionContacto}");
            Console.WriteLine($"       IdPlantilla:       {actividad.IdPlantilla}");
            Console.WriteLine($"       Canal:             {(actividad.IdPlantillaBase == 2 ? "EMAIL" : "WHATSAPP")}");
            _logger.LogInformation(
                "Iniciando envio automatico - IdGestionContacto: {IdGC}, IdPlantilla: {IdP}, IdPlantillaBase: {IdPB}, Usuario: {U}",
                actividad.IdGestionContacto, actividad.IdPlantilla, actividad.IdPlantillaBase, usuario);

            // 1. Resolver la gestion de contacto
            Console.WriteLine($"       [1] Buscando GestionContacto Id={actividad.IdGestionContacto}");
            var gestionContacto = await _unitOfWork.GestionContactoRepository
                .ObtenerPorIdAsync(actividad.IdGestionContacto);

            if (gestionContacto == null || gestionContacto.Id == 0)
                throw new Exception($"No se encontro la gestion de contacto con Id {actividad.IdGestionContacto}");

            if (!gestionContacto.IdClasificacionPersona.HasValue)
                throw new Exception($"La gestion de contacto {actividad.IdGestionContacto} no tiene clasificacion de persona asociada");

            Console.WriteLine($"       [1] OK — IdClasificacionPersona={gestionContacto.IdClasificacionPersona}");

            // 2. Resolver el docente
            Console.WriteLine($"       [2] Buscando docente por IdClasificacionPersona={gestionContacto.IdClasificacionPersona.Value}");
            var docente = _unitOfWork.DocentePostulanteRepository
                .ObtenerDocenteDTOPorIdClasificacionPersona(gestionContacto.IdClasificacionPersona.Value);

            if (docente == null)
                throw new Exception($"No se encontro el docente para la clasificacion de persona {gestionContacto.IdClasificacionPersona.Value}");

            Console.WriteLine($"       [2] OK — Docente: {docente.Correo ?? "(sin correo)"} | Cel: {docente.Celular ?? "(sin celular)"}");

            // 3. Generar plantilla con etiquetas reemplazadas
            Console.WriteLine($"       [3] Generando plantilla Id={actividad.IdPlantilla}");
            _logger.LogInformation("Generando plantilla {IdPlantilla}", actividad.IdPlantilla);
            var plantillaGenerada = _gestionDocenteActividadService.GenerarPlantillaDocente(
                new ReemplazoEtiquetaPlantillaDocenteDTO
                {
                    IdGestionContacto = actividad.IdGestionContacto,
                    IdPlantilla = actividad.IdPlantilla
                });

            // 4. Enviar por canal segun IdPlantillaBase
            return actividad.IdPlantillaBase switch
            {
                2 => await EnviarCorreoAsync(actividad, gestionContacto, docente, plantillaGenerada, idPersonal, usuario),
                8 =>       EnviarWhatsApp   (actividad, gestionContacto, docente, plantillaGenerada, idPersonal, usuario),
                _ => throw new Exception($"Tipo de plantilla base no soportado: {actividad.IdPlantillaBase}")
            };
        }

        // ─────────────────────────────────────────────────────────────────────
        // Canal Email (IdPlantillaBase = 2)
        // ─────────────────────────────────────────────────────────────────────

        private async Task<string> EnviarCorreoAsync(
            ActividadPendienteDTO actividad,
            GestionContactoSimpleDTO gestionContacto,
            DocentePostulanteDTO docente,
            (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) plantilla,
            int idPersonal,
            string usuario)
        {
            if (string.IsNullOrWhiteSpace(docente.Correo))
                throw new Exception("El docente no tiene correo registrado");

            if (string.IsNullOrWhiteSpace(plantilla.EmailReemplazado.Asunto) ||
                string.IsNullOrWhiteSpace(plantilla.EmailReemplazado.CuerpoHTML))
                throw new Exception("La plantilla de email generada esta vacia");

            var asesor = _unitOfWork.PersonalRepository.FirstById(idPersonal);
            if (asesor == null)
                throw new Exception($"No se encontro el personal con Id {idPersonal}");

            _logger.LogInformation(
                "Enviando correo a {Correo} con asunto: {Asunto}", docente.Correo, plantilla.EmailReemplazado.Asunto);

            var parametrosCorreo = new ParametrosEnviarMensajePlaDTO
            {
                Remitente            = asesor.Email,
                Destinatario         = docente.Correo,
                Asunto               = plantilla.EmailReemplazado.Asunto,
                Mensaje              = Convert.ToBase64String(Encoding.UTF8.GetBytes(plantilla.EmailReemplazado.CuerpoHTML)),
                DestinatarioCc       = "",
                DestinatarioBcc      = "",
                IdGestionContacto    = actividad.IdGestionContacto,
                IdClasificacionPersona = gestionContacto.IdClasificacionPersona,
                Files                = null
            };

            var gmailCorreoService = new GmailCorreoService(_unitOfWork);
            var enviado = await gmailCorreoService.EnviarMensajeCorreoPla(
                parametrosCorreo, new List<IFormFile>(), usuario);

            if (!enviado)
                throw new Exception("Error al enviar correo");

            Console.WriteLine($"       ✅ EMAIL enviado → {docente.Correo}");
            _logger.LogInformation("Correo enviado exitosamente a {Correo}", docente.Correo);
            return $"Email enviado exitosamente a {docente.Correo} - Asunto: {plantilla.EmailReemplazado.Asunto}";
        }

        // ─────────────────────────────────────────────────────────────────────
        // Canal WhatsApp (IdPlantillaBase = 8)
        // ─────────────────────────────────────────────────────────────────────

        private string EnviarWhatsApp(
            ActividadPendienteDTO actividad,
            GestionContactoSimpleDTO gestionContacto,
            DocentePostulanteDTO docente,
            (PlantillaEmailMandrillDTO EmailReemplazado, PlantillaWhatsAppCalculadoDTO WhatsAppReemplazado) plantilla,
            int idPersonal,
            string usuario)
        {
            if (string.IsNullOrWhiteSpace(docente.Celular))
                throw new Exception("El docente no tiene numero de celular registrado");

            if (string.IsNullOrWhiteSpace(plantilla.WhatsAppReemplazado.Plantilla))
                throw new Exception("La plantilla de WhatsApp generada esta vacia");

            // Resolver IdProveedor desde ClasificacionPersona
            var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository
                .FirstById(gestionContacto.IdClasificacionPersona.Value);

            if (clasificacionPersona == null)
                throw new Exception($"No se encontro la clasificacion de persona {gestionContacto.IdClasificacionPersona.Value}");

            if (clasificacionPersona.IdTipoPersona != 4)
                throw new Exception(
                    $"La clasificacion de persona debe ser de tipo Proveedor (IdTipoPersona = 4), " +
                    $"pero es {clasificacionPersona.IdTipoPersona}");

            int idProveedor = clasificacionPersona.IdTablaOriginal;

            _logger.LogInformation(
                "Enviando WhatsApp a {Celular} (IdProveedor: {IdProveedor})", docente.Celular, idProveedor);

            var mensajeDto = new WhatsAppMensajeTextoPlaDTO
            {
                WaTo        = docente.Celular,
                WaBody      = plantilla.WhatsAppReemplazado.Plantilla,
                IdPais      = 51, // Peru
                IdProveedor = idProveedor,
                IdPersonal  = idPersonal
            };

            bool enviado = _whatsAppService.EnvioMensajePorTexto(mensajeDto, usuario, idPersonal);

            if (!enviado)
                throw new Exception("Error al enviar WhatsApp");

            Console.WriteLine($"       ✅ WHATSAPP enviado → {docente.Celular}");
            _logger.LogInformation("WhatsApp enviado exitosamente a {Celular}", docente.Celular);
            return $"WhatsApp enviado exitosamente a {docente.Celular}";
        }
    }
}
