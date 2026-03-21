using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.EsquemaRespuestas;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.EsquemaRespuestas
{
    public class EsquemaRespuestasService : IEsquemaRespuestasService
    {
        private readonly IEsquemaRespuestasRepository _esquemaRespuestasRepository;

        public EsquemaRespuestasService(IEsquemaRespuestasRepository esquemaRespuestasRepository)
        {
            _esquemaRespuestasRepository = esquemaRespuestasRepository;
        }

        public async Task<List<EsquemaListadoCompletoDTO>> ObtenerListadoEsquemasAsync()
            => await _esquemaRespuestasRepository.ObtenerListadoEsquemasAsync();

        public async Task<EsquemaDetalleResponseDTO> ObtenerEsquemaPorIdAsync(int id)
            => await _esquemaRespuestasRepository.ObtenerEsquemaPorIdAsync(id);

        public int EliminarEsquema(int id, string usuario)
        {
            return _esquemaRespuestasRepository.EliminarEsquema(id, usuario);
        }

        public List<ChatbotEsquemaAsignacionFaseDTO> ObtenerListadoFase()
        {
            return _esquemaRespuestasRepository.ObtenerListadoFase();
        }

        public List<AsistenteWhatsAppAsignacionNumeroDTO> ObtenerListadoNumero()
        {
            return _esquemaRespuestasRepository.ObtenerListadoNumero();
        }

        public int UpsertAsistenteWhatsAppAsignacion(UpsertAsistenteWhatsAppAsignacionDTO request, string usuario)
        {
            return _esquemaRespuestasRepository.UpsertAsistenteWhatsAppAsignacion(request, usuario);
        }

        public List<ChatbotEsquemaAsignacionMensajeExactoDTO> ObtenerListadoMensajeExacto()
        {
            return _esquemaRespuestasRepository.ObtenerListadoMensajeExacto();
        }

        public List<ChatbotEsquemaAsignacionPerfilDTO> ObtenerListadoPerfil()
        {
            return _esquemaRespuestasRepository.ObtenerListadoPerfil();
        }

        public int InsertarEsquema(CrearEsquemaRequestDTO request, string usuario)
        {
            return _esquemaRespuestasRepository.InsertarEsquema(request, usuario);
        }

        public int ActualizarEsquema(ActualizarEsquemaRequestDTO request, string usuario)
        {
            return _esquemaRespuestasRepository.ActualizarEsquema(request, usuario);
        }
    }
}
