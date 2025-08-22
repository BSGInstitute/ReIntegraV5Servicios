using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: OcurrenciaActividadAlternoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_OcurrenciaActividadAlterno
    /// </summary>
    public class OcurrenciaActividadAlternoService : IOcurrenciaActividadAlternoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OcurrenciaActividadAlternoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TOcurrenciaActividadAlterno, OcurrenciaActividadAlterno>(MemberList.None).ReverseMap();
                    cfg.CreateMap<OcurrenciaActividadAlternoDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public OcurrenciaActividadAlterno Add(OcurrenciaActividadAlterno entidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaActividadAlternoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OcurrenciaActividadAlterno>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OcurrenciaActividadAlterno Update(OcurrenciaActividadAlterno entidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaActividadAlternoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OcurrenciaActividadAlterno>(modelo);
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
                _unitOfWork.OcurrenciaActividadAlternoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OcurrenciaActividadAlterno> Add(List<OcurrenciaActividadAlterno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaActividadAlternoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OcurrenciaActividadAlterno>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OcurrenciaActividadAlterno> Update(List<OcurrenciaActividadAlterno> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaActividadAlternoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OcurrenciaActividadAlterno>>(modelo);
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
                _unitOfWork.OcurrenciaActividadAlternoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OcurrenciaActividadAlterno
        /// </summary>
        /// <returns> List<OcurrenciaActividadAlternoDTO> </returns>
        public IEnumerable<OcurrenciaActividadAlternoDTO> ObtenerOcurrenciaActividadAlterno()
        {
            try
            {
                return _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerOcurrenciaActividadAlterno();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OcurrenciaActividadAlterno para mostrarse en combo.
        /// </summary>
        /// <returns> List<OcurrenciaActividadAlternoComboDTO> </returns>
        public IEnumerable<OcurrenciaActividadAlternoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Arbol de Ocurrencias Alterno.
        /// </summary>
        /// <param name="idActividadCabecera">Id de la Actividad Cabecera</param>
        /// <param name="idOcurrenciaPadre">Id de la Ocurrencia Padre</param>
        /// <returns> List<ArbolOcurenciaAlternoDTO> </returns>
        public IEnumerable<ArbolOcurenciaAlternoDTO> ObtenerArbolOcurrenciaAlterno(int idActividadCabecera, int idOcurrenciaPadre)
        {
            try
            {
                return _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerArbolOcurrenciaAlterno(idActividadCabecera, idOcurrenciaPadre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Arbol de Ocurrencias Alterno.
        /// </summary>
        /// <param name="idActividadCabecera">Id de la Actividad Cabecera</param>
        /// <param name="idOcurrenciaPadre">Id de la Ocurrencia Padre</param>
        /// <returns> List<ArbolOcurenciaAlternoDTO> </returns>
        public ArbolOcurenciaAlternoDTO ObtenerOcurrenciaMarcador(int idActividadCabecera)
        {
            try
            {
                //381 Sin contacto telefónico con el cliente
                var ocurrenciaPadre = _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerArbolOcurrenciaAlternoV2(idActividadCabecera, 0, 381);

                var ocurrencia = _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerArbolOcurrenciaAlternoV2(idActividadCabecera, ocurrenciaPadre.IdOcurrenciaActividad.Value, 129);
                return ocurrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de OcurrenciaActividades por el Id de OcurrenciaActividad.
        /// </summary>
        /// <param name="idOcurrenciaActividad">Id de la Ocurrencia Actividad</param>
        /// <returns> OcurenciaActividadCompletoDTO </returns>
        public OcurenciaActividadCompletoDTO ObtenerOcurrenciaActividadPorId(int? idOcurrenciaActividad)
        {
            try
            {
                return _unitOfWork.OcurrenciaActividadAlternoRepository.ObtenerOcurrenciaActividadPorId(idOcurrenciaActividad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
