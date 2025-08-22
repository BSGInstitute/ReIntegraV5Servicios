using AutoMapper;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CajaEgresoAprobadoService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_CajaEgresoAprobado
    /// </summary>
    public class CajaEgresoAprobadoService : ICajaEgresoAprobadoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CajaEgresoAprobadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCajaEgresoAprobado, CajaEgresoAprobado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CajaEgresoAprobado Add(CajaEgresoAprobado entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoAprobadoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaEgresoAprobado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CajaEgresoAprobado Update(CajaEgresoAprobado entidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoAprobadoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CajaEgresoAprobado>(modelo);
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
                _unitOfWork.CajaEgresoAprobadoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaEgresoAprobado> Add(List<CajaEgresoAprobado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoAprobadoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaEgresoAprobado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CajaEgresoAprobado> Update(List<CajaEgresoAprobado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaEgresoAprobadoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CajaEgresoAprobado>>(modelo);
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
                _unitOfWork.CajaEgresoAprobadoRepository.Delete(listadoIds, usuario);
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
