using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: HoraBloqueadaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_HoraBloqueada
    /// </summary>
    public class HoraBloqueadaService : IHoraBloqueadaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public HoraBloqueadaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<THoraBloqueadum, HoraBloqueada>(MemberList.None).ReverseMap();
                    cfg.CreateMap<HoraBloqueadaDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public HoraBloqueada Add(HoraBloqueada entidad)
        {
            try
            {
                var modelo = _unitOfWork.HoraBloqueadaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<HoraBloqueada>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HoraBloqueada Update(HoraBloqueada entidad)
        {
            try
            {
                var modelo = _unitOfWork.HoraBloqueadaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<HoraBloqueada>(modelo);
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
                _unitOfWork.HoraBloqueadaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HoraBloqueada> Add(List<HoraBloqueada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.HoraBloqueadaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<HoraBloqueada>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HoraBloqueada> Update(List<HoraBloqueada> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.HoraBloqueadaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<HoraBloqueada>>(modelo);
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
                _unitOfWork.HoraBloqueadaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_HoraBloqueada
        /// </summary>
        /// <returns> List<HoraBloqueadaDTO> </returns>
        public IEnumerable<HoraBloqueadaDTO> ObtenerHoraBloqueada()
        {
            try
            {
                return _unitOfWork.HoraBloqueadaRepository.ObtenerHoraBloqueada();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_HoraBloqueada
        /// </summary>
        /// <returns> List<HoraBloqueadaDTO> </returns>
        public List<HoraBloqueadaRADTO> ObtenerHorasBloquedasReprogramacionPorAsesor(int idPersonal, DateTime fecha)
        {
            try
            {
                return _unitOfWork.HoraBloqueadaRepository.ObtenerHorasBloquedasReprogramacionPorAsesor(idPersonal, fecha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
