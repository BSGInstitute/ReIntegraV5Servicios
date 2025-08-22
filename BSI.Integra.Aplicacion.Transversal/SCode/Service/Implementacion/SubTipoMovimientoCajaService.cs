using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SubTipoMovimientoCajaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class SubTipoMovimientoCajaService : ISubTipoMovimientoCajaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SubTipoMovimientoCajaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSubTipoMovimientoCaja, SubTipoMovimientoCaja>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SubTipoMovimientoCaja Add(SubTipoMovimientoCaja entidad)
        {
            try
            {
                var modelo = _unitOfWork.SubTipoMovimientoCajaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SubTipoMovimientoCaja>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SubTipoMovimientoCaja Update(SubTipoMovimientoCaja entidad)
        {
            try
            {
                var modelo = _unitOfWork.SubTipoMovimientoCajaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SubTipoMovimientoCaja>(modelo);
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
                _unitOfWork.SubTipoMovimientoCajaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubTipoMovimientoCaja> Add(List<SubTipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SubTipoMovimientoCajaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SubTipoMovimientoCaja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubTipoMovimientoCaja> Update(List<SubTipoMovimientoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SubTipoMovimientoCajaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SubTipoMovimientoCaja>>(modelo);
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
                _unitOfWork.SubTipoMovimientoCajaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// Autor:Margiory Ramkirez.
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Lista de Tipo de Datos para filtro en formularios
        /// </summary>
        /// <returns></returns>
        public List<SubTipoMovimientoCajaDTO> ObtenerListaSubTipoMovimientoCaja()
        {
            try
            {
                return _unitOfWork.SubTipoMovimientoCajaRepository.ObtenerListaSubTipoMovimientoCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
