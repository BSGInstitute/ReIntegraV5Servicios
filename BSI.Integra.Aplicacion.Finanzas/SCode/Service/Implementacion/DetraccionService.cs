using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: DetraccionService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Detraccion
    /// </summary>
    public class DetraccionService : IDetraccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DetraccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDetraccion, Detraccion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Detraccion Add(Detraccion entidad)
        {
            try
            {
                var modelo = _unitOfWork.DetraccionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Detraccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Detraccion Update(Detraccion entidad)
        {
            try
            {
                var modelo = _unitOfWork.DetraccionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Detraccion>(modelo);
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
                _unitOfWork.DetraccionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Detraccion> Add(List<Detraccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DetraccionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Detraccion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Detraccion> Update(List<Detraccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DetraccionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Detraccion>>(modelo);
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
                _unitOfWork.DetraccionRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 06/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Detraccion
        /// </summary>
        /// <returns> List<DetraccionDTO> </returns>
        /// <param name="filtro"> Indetificadores de Origen,Fecha de emision de Detraccion,Fecha de vencimiento de Detraccion</param>
        public IEnumerable<ReporteDetraccionDTO> ObtenerReporteDetraccion(ReporteDetraccionFiltroDTO filtro)
        {
            try
            {
                return _unitOfWork.DetraccionRepository.ObtenerReporteDetraccion(filtro.IdSede, filtro.FechaInicio, filtro.FechaFinal,filtro.IdProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Detraccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<DetraccionComboDTO> </returns>
        public IEnumerable<DetraccionComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DetraccionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 06/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Valor] de Detraccion segun el pais
        /// </summary>
        /// <returns> List<DetraccionComboDTO> </returns>
        /// <param name="IdPais">Identificador del Pais</param>
        public IEnumerable<DetraccionComboDTO> ObtenerValorDetraccionPorPais(int IdPais)
        {
            try
            {
                return _unitOfWork.DetraccionRepository.ObtenerValorDetraccionPorPais(IdPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
