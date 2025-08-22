using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProgramaGeneralPuntoCorteDetalleService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPuntoCorteDetalle
    /// </summary>
    public class ProgramaGeneralPuntoCorteDetalleService : IProgramaGeneralPuntoCorteDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralPuntoCorteDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralPuntoCorteDetalle, ProgramaGeneralPuntoCorteDetalle>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ProgramaGeneralPuntoCorteDetalle Add(ProgramaGeneralPuntoCorteDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralPuntoCorteDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramaGeneralPuntoCorteDetalle Update(ProgramaGeneralPuntoCorteDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ProgramaGeneralPuntoCorteDetalle>(modelo);
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
                _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralPuntoCorteDetalle> Add(List<ProgramaGeneralPuntoCorteDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralPuntoCorteDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProgramaGeneralPuntoCorteDetalle> Update(List<ProgramaGeneralPuntoCorteDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ProgramaGeneralPuntoCorteDetalle>>(modelo);
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
                _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProgramaGeneralPuntoCorteDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
    }
}