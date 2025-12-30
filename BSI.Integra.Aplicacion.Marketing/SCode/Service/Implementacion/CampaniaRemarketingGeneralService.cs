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
            return _campaniaRemarketingGeneralRepository.ObtenerCombosConfiguracionCampania();
        }

        public DetallesCampaniaDTO VerDetallesCampania(int id)
        {
            return _campaniaRemarketingGeneralRepository.VerDetallesCampania(id);
        }

        public bool EditarCampania()
        {
            return _campaniaRemarketingGeneralRepository.EditarCampania();
        }

        public bool EliminarCampania(int id)
        {
            return _campaniaRemarketingGeneralRepository.EliminarCampania(id);
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
