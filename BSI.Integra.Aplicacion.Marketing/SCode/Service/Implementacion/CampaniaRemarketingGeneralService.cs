using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    public class CampaniaRemarketingGeneralService : ICampaniaRemarketingGeneralService
    {
        private readonly ICampaniaRemarketingGeneralRepository _campaniaRemarketingGeneralRepository;

        public CampaniaRemarketingGeneralService(ICampaniaRemarketingGeneralRepository campaniaRemarketingGeneralRepository)
        {
            _campaniaRemarketingGeneralRepository = campaniaRemarketingGeneralRepository;
        }

        public List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania()
        {
            return _campaniaRemarketingGeneralRepository.ObtenerListadoCampania();
        }

        public List<object> ObtenerRendimientoListadoCampanias(List<int> ids)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerRendimientoListadoCampanias(ids);
        }

        public CombosConfiguracionCampaniaDTO ObtenerCombosConfiguracionCampania()
        {
            var medioEnvio = _campaniaRemarketingGeneralRepository.ObtenerMediosEnvio();
            var tipoMensaje = _campaniaRemarketingGeneralRepository.ObtenerTiposMensaje();
            var logicaEnvio = _campaniaRemarketingGeneralRepository.ObtenerLogicasEnvio();
            var argumento = _campaniaRemarketingGeneralRepository.ObtenerArgumentos();

            return new CombosConfiguracionCampaniaDTO
            {
                MedioEnvio = medioEnvio,
                TipoMensaje = tipoMensaje,
                LogicaEnvio = logicaEnvio,
                Argumento = argumento
            };
        }

        public List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados()
        {
            return _campaniaRemarketingGeneralRepository.ObtenerListadoSegmentosCreados();
        }

        public List<ResultadoTextoGeneradoDTO> ObtenerResultadosGeneracionTextoPorCampania(int id)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerResultadosGeneracionTextoPorCampania(id);
        }

        public bool EjecutarEnvioCampaniaRemarketing(EnvioCampaniaRemarketingDTO request, string usuario)
        {
            request.UsuarioCreacion = usuario;
            if (request.FechaEnvio == null)
                request.FechaEnvio = DateTime.Now;

            var respuesta = _campaniaRemarketingGeneralRepository.InsertarCampaniaRemarketing(request);

            return respuesta;
        }

        public DetallesCampaniaDTO VerDetallesCampania(int id)
        {
            return _campaniaRemarketingGeneralRepository.VerDetallesCampania(id);
        }

        public CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerCampaniaRemarketingPorId(id);
        }

        public bool EditarCampania()
        {
            return _campaniaRemarketingGeneralRepository.EditarCampania();
        }

        public bool EliminarCampania(int id, string usuario)
        {
            return _campaniaRemarketingGeneralRepository.EliminarCampania(id, usuario);
        }

        public MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id)
        {
            return _campaniaRemarketingGeneralRepository.ObtenerMensajeGeneradoPorId(id);
        }

        public bool ReenviarMensajeGenerado(int id)
        {
            return true;
        }

    }
}
