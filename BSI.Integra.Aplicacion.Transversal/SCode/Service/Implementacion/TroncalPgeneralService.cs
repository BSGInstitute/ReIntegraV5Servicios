using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TroncalPgeneralService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TroncalPgeneral
    /// </summary>
    public class TroncalPgeneralService : ITroncalPgeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper _mapperTroncal;


        public TroncalPgeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTroncalPgeneral, TroncalPgeneral>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            _mapperTroncal = new Mapper(config);
        }

        #region Metodos Base
        public TroncalPgeneral Add(TroncalPgeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.TroncalPgeneralRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TroncalPgeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TroncalPgeneral Update(TroncalPgeneral entidad)
        {
            try
            {
                var modelo = _unitOfWork.TroncalPgeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TroncalPgeneral>(modelo);
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
                _unitOfWork.TroncalPgeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TroncalPgeneral> Add(List<TroncalPgeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TroncalPgeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TroncalPgeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TroncalPgeneral> Update(List<TroncalPgeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TroncalPgeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TroncalPgeneral>>(modelo);
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
                _unitOfWork.TroncalPgeneralRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public object ObtenerTroncalPgeneral()
        {
            try
            {
                var troncalPgeneralRepositorio = _unitOfWork.TroncalPgeneralRepository;

                return (troncalPgeneralRepositorio.ObtenerTroncalPgeneralFiltro());
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }




    }
}
