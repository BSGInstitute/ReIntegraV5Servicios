using AutoMapper; 
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: InteraccionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 24/08/2022
    /// <summary>
    /// Gestión general de T_Interaccion
    /// </summary>
    public class InteraccionService : IInteraccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public InteraccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInteraccion, Interaccion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public Interaccion Add(Interaccion entidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Interaccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Interaccion> Add(IEnumerable<Interaccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Interaccion>>(modelo);
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
                var modelo = _unitOfWork.InteraccionRepository.Delete(id, usuario);
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
                _unitOfWork.InteraccionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Interaccion Update(Interaccion entidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Interaccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Interaccion> Update(IEnumerable<Interaccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Interaccion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
