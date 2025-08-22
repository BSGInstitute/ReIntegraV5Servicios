using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp
{
    public class WhatsAppMensajePublicidadService : IWhatsAppMensajePublicidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public WhatsAppMensajePublicidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppMensajePublicidad, WhatsAppMensajePublicidad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public WhatsAppMensajePublicidad Add(WhatsAppMensajePublicidadDTO entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppMensajePublicidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<WhatsAppMensajePublicidad> Add(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppMensajePublicidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.WhatsAppMensajePublicidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppMensajePublicidad Update(WhatsAppMensajePublicidadDTO entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppMensajePublicidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<WhatsAppMensajePublicidad> Update(IEnumerable<WhatsAppMensajePublicidadDTO> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppMensajePublicidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(List<WhatsAppMensajePublicidadDTO> listaNuevoWhatsAppMensajePublicidad)
        {
            try
            {
                _unitOfWork.WhatsAppMensajePublicidadRepository.InsertarWhatsAppMensajePublicidadMasivoCampaniaGeneral(listaNuevoWhatsAppMensajePublicidad);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertarWhatsAppMensajePublicidad(WhatsAppMensajePublicidadDTO filtro)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.InsertarWhatsAppMensajePublicidad(filtro);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO preprocesamientoWhatsAppCampaniaGeneral)
        {
            try
            {
                _unitOfWork.WhatsAppMensajePublicidadRepository.ActualizarContactosConPrimerPreprocesamientoCampaniaGeneral(preprocesamientoWhatsAppCampaniaGeneral);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppResultadoCampaniaGeneralDTO> ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppMensajePublicidadRepository.ObtenerListaWhatsAppPrimerProcesadoCampaniaGeneral(idCampaniaGeneralDetalle);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppResultadoCampaniaGeneralDTO>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DatosALumnoWhatsappDTO ObtenerDatosAlumnoIntegra(string WaFrom, string NumeroEmpresa)
        {
            return _unitOfWork.WhatsAppMensajePublicidadRepository.ObtenerDatosAlumnoIntegra(WaFrom, NumeroEmpresa);
        }
    }
}
