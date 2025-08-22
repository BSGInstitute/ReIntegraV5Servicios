using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using PdfSharp.Pdf.Filters;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{

    public class WhatsAppConfiguracionLogEjecucionService : IWhatsAppConfiguracionLogEjecucionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper _mapperActividad;

        public WhatsAppConfiguracionLogEjecucionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<ActividadDetalleDTO, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TWhatsAppConfiguracionLogEjecucion, WhatsAppConfiguracionLogEjecucionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
            _mapperActividad = new Mapper(config);
        }

        public IEnumerable<WhatsAppConfiguracionLogEjecucion> GetBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter)
        {
            try
            {
                var res = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.GetBy(filter);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppConfiguracionLogEjecucion FirstById(int id)
        {
            try
            {
                var res = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstById(id);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppConfiguracionLogEjecucion FirstBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter)
        {
            try
            {
                var res = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.FirstBy(filter);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(objetoBO);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(WhatsAppConfiguracionLogEjecucionDTO objetoBO)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(_mapper.Map<TWhatsAppConfiguracionLogEjecucion>(objetoBO));
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(IEnumerable<WhatsAppConfiguracionLogEjecucion> listadoBO)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(listadoBO);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Insert(IEnumerable<WhatsAppConfiguracionLogEjecucionDTO> listadoBO)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Insert(_mapper.Map<List<TWhatsAppConfiguracionLogEjecucion>>(listadoBO));
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Update(WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(objetoBO);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(IEnumerable<WhatsAppConfiguracionLogEjecucion> listadoBO)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.Update(listadoBO);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignacionId(TWhatsAppConfiguracionLogEjecucion entidad, WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.AsignacionId(entidad, objetoBO);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionLogEjecucion MapeoEntidad(WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                var res = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.MapeoEntidad(objetoBO);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int VerificadEnvioDuplicado(string Celular)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.VerificadEnvioDuplicado(Celular);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertarWhatsappConfiguracionLogEjecucion(WhatsAppConfiguracionLogEjecucion filtro)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.InsertarWhatsappConfiguracionLogEjecucion(filtro);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarWhatsappConfiguracionLogEjecucionFechaFin(WhatsAppConfiguracionLogEjecucion filtro)
        {
            try
            {
                var res = _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ActualizarWhatsappConfiguracionLogEjecucionFechaFin(filtro);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObtenerLogActivo(int IdWhasAppConfiguracionLogEjecucion)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.ObtenerLogActivo(IdWhasAppConfiguracionLogEjecucion);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int obtenerOtrosLogActivos(int idwhatsappConfiguracionEnvio)
        {
            try
            {
                var res= _unitOfWork.WhatsAppConfiguracionLogEjecucionRepository.obtenerOtrosLogActivos(idwhatsappConfiguracionEnvio);
                _unitOfWork.Commit();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
