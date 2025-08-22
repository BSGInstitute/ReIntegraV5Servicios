using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: FurFaseAprobacionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_FurFaseAprobacion
    /// </summary>
    public class FurFaseAprobacionService : IFurFaseAprobacionService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperFurFaseAprobacion;

        public FurFaseAprobacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFurFaseAprobacion, FurFaseAprobacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperFurFaseAprobacion = new Mapper(config);
        }

        #region Metodos Base
        public FurFaseAprobacion Add(FurFaseAprobacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurFaseAprobacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurFaseAprobacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FurFaseAprobacion Update(FurFaseAprobacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurFaseAprobacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurFaseAprobacion>(modelo);
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
                _unitOfWork.FurFaseAprobacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurFaseAprobacion> Add(List<FurFaseAprobacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurFaseAprobacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurFaseAprobacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurFaseAprobacion> Update(List<FurFaseAprobacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurFaseAprobacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurFaseAprobacion>>(modelo);
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
                _unitOfWork.FurFaseAprobacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion



        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros T_FurFaseAprobacion para un combo.
        /// </summary>
        /// <returns> List<Object{id:int,nombre:string}> </returns>
        public IEnumerable<Object> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FurFaseAprobacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
