using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PuntoCorteService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PuntoCorte
    /// </summary>
    public class PuntoCorteService : IPuntoCorteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PuntoCorteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPuntoCorte, PuntoCorte>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PuntoCorte Add(PuntoCorte entidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntoCorteRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PuntoCorte>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PuntoCorte Update(PuntoCorte entidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntoCorteRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PuntoCorte>(modelo);
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
                _unitOfWork.PuntoCorteRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PuntoCorte> Add(List<PuntoCorte> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntoCorteRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PuntoCorte>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PuntoCorte> Update(List<PuntoCorte> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PuntoCorteRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PuntoCorte>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.PuntoCorteRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

}
