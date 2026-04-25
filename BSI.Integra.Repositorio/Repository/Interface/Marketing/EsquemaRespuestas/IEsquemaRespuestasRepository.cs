using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas
{
    public interface IEsquemaRespuestasRepository
    {
        Task<List<EsquemaListadoCompletoDTO>> ObtenerListadoEsquemasAsync();
        Task<EsquemaDetalleResponseDTO> ObtenerEsquemaPorIdAsync(int idChatbotEsquema);
        int EliminarEsquema(int id, string usuario);
        List<ChatbotEsquemaAsignacionFaseDTO> ObtenerListadoFase();
        List<AsistenteWhatsAppAsignacionNumeroDTO> ObtenerListadoNumero();
        int UpsertAsistenteWhatsAppAsignacion(UpsertAsistenteWhatsAppAsignacionDTO request, string usuario);
        List<ChatbotEsquemaAsignacionMensajeExactoDTO> ObtenerListadoMensajeExacto();
        List<ChatbotEsquemaAsignacionPerfilDTO> ObtenerListadoPerfil();
        int InsertarEsquema(CrearEsquemaRequestDTO request, string usuario);
        int ActualizarEsquema(ActualizarEsquemaRequestDTO request, string usuario);
    }
}
