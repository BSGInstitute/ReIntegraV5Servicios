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
    }
}
