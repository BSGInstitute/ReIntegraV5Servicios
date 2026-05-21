using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Interaccion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Implementacion
{
    /// Service: LlamadasWhatsAppService
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// Servicio de lectura del historial de llamadas de WhatsApp Business Calling.
    /// La tabla com.T_WhatsappLlamada vive en IntegraDB principal, por eso se usa IUnitOfWork
    /// (no IUnitOfWorkInteraccion).
    /// </summary>
    public class LlamadasWhatsAppService : ILlamadasWhatsAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LlamadasWhatsAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LlamadasHistorialResultadoDTO ObtenerHistorial(LlamadasHistorialFiltroDTO filtro)
        {
            try
            {
                if (filtro == null)
                {
                    filtro = new LlamadasHistorialFiltroDTO();
                }

                if (filtro.Pagina < 1) filtro.Pagina = 1;
                if (filtro.RegistrosPorPagina < 1) filtro.RegistrosPorPagina = 50;
                if (filtro.RegistrosPorPagina > 500) filtro.RegistrosPorPagina = 500;

                return _unitOfWork.LlamadasWhatsAppRepository.ObtenerHistorialPaginado(filtro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Aplica las reglas de vigencia del consentimiento sobre los datos crudos del repo:
        ///  · aceptado + ConsentimientoExpira > now → vigente, puede llamar directo (sin re-solicitar).
        ///  · pendiente + ConsentimientoExpira > now → esperando respuesta del cliente.
        ///  · rechazado + ConsentimientoExpira > now → en cooldown, no se puede solicitar.
        ///  · estado expirado (ConsentimientoExpira ≤ now) o sin solicitud → puede solicitar nuevo.
        ///
        /// Filtrado por IdNumeroWhatsApp (resuelto a partir del idPersonal): si dos asesores
        /// usan distintos números de WhatsApp Business, el consent del Asesor 1 NO debe contar
        /// como vigente para el Asesor 2. Meta valida el consent por par (WABA, consumer).
        /// </summary>
        public LlamadaConsentimientoEstadoDTO ObtenerEstadoConsentimiento(string numeroWhatsApp, int idPais, int? idPersonal = null)
        {
            try
            {
                // Normalizar el número: sin `+`, sin espacios ni guiones (mismo criterio que el backend de envío).
                string numeroNormalizado = string.IsNullOrEmpty(numeroWhatsApp)
                    ? numeroWhatsApp
                    : numeroWhatsApp.TrimStart('+').Replace(" ", "").Replace("-", "");

                // Si vino idPersonal, resolver el IdNumeroWhatsApp (phone_number_id) para
                // filtrar el consent al par (NumeroNegocio, NumeroCliente). Sin idPersonal,
                // el filtro queda solo por (numero, idPais) — comportamiento legacy para
                // callers que no necesitan distinguir entre asesores.
                string? idNumeroWhatsApp = null;
                if (idPersonal.HasValue && idPersonal.Value > 0)
                {
                    idNumeroWhatsApp = _unitOfWork.LlamadasWhatsAppRepository
                        .ResolverIdNumeroWhatsApp(idPais, idPersonal.Value);
                }

                var ultimo = _unitOfWork.LlamadasWhatsAppRepository
                    .ObtenerUltimoConsentimiento(numeroNormalizado, idPais, idNumeroWhatsApp);

                var resultado = new LlamadaConsentimientoEstadoDTO();

                if (ultimo == null || string.IsNullOrEmpty(ultimo.ConsentimientoEstado))
                {
                    resultado.Estado         = "sin_solicitud";
                    resultado.PuedeSolicitar = true;
                    resultado.PuedeLlamar    = false;
                    resultado.Mensaje        = "No hay solicitud previa. Podés enviar el template de consentimiento.";
                    return resultado;
                }

                bool vigente = ultimo.ConsentimientoExpira.HasValue
                            && ultimo.ConsentimientoExpira.Value > DateTime.Now;

                string estadoLower = ultimo.ConsentimientoEstado.ToLowerInvariant();

                resultado.IdWhatsappLlamada    = ultimo.IdWhatsappLlamada;
                resultado.ConsentimientoFecha  = ultimo.ConsentimientoFecha;
                resultado.ConsentimientoExpira = ultimo.ConsentimientoExpira;

                if (!vigente)
                {
                    // El último consentimiento expiró. Se puede pedir uno nuevo.
                    resultado.Estado         = "sin_solicitud";
                    resultado.PuedeSolicitar = true;
                    resultado.PuedeLlamar    = false;
                    resultado.Mensaje        = "Consentimiento anterior expirado. Podés solicitarlo nuevamente.";
                    return resultado;
                }

                switch (estadoLower)
                {
                    case "aceptado":
                        resultado.Estado         = "aceptado";
                        resultado.PuedeSolicitar = false;
                        resultado.PuedeLlamar    = true;
                        resultado.Mensaje        = $"Consentimiento vigente hasta {ultimo.ConsentimientoExpira:yyyy-MM-dd HH:mm}. Podés llamar directo.";
                        break;
                    case "pendiente":
                        resultado.Estado         = "pendiente";
                        resultado.PuedeSolicitar = false;
                        resultado.PuedeLlamar    = false;
                        resultado.Mensaje        = "Esperando respuesta del cliente al template enviado.";
                        break;
                    case "rechazado":
                        resultado.Estado         = "rechazado";
                        resultado.PuedeSolicitar = false;
                        resultado.PuedeLlamar    = false;
                        resultado.Mensaje        = $"Cliente rechazó. Volvé a intentar después de {ultimo.ConsentimientoExpira:yyyy-MM-dd HH:mm}.";
                        break;
                    default:
                        resultado.Estado         = estadoLower;
                        resultado.PuedeSolicitar = true;
                        resultado.PuedeLlamar    = false;
                        resultado.Mensaje        = $"Estado desconocido: {estadoLower}";
                        break;
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
