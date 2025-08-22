using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: OcurrenciaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_Ocurrencia
    /// </summary>
    public class OcurrenciaService : IOcurrenciaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OcurrenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TOcurrencium, Ocurrencia>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public Ocurrencia Add(Ocurrencia entidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Ocurrencia>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ocurrencia Update(Ocurrencia entidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Ocurrencia>(modelo);
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
                _unitOfWork.OcurrenciaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ocurrencia> Add(List<Ocurrencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Ocurrencia>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ocurrencia> Update(List<Ocurrencia> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OcurrenciaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Ocurrencia>>(modelo);
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
                _unitOfWork.OcurrenciaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_Ocurrencia
        /// </summary>
        /// <returns> List<OcurrenciaDTO> </returns>
        public IEnumerable<OcurrenciaDTO> ObtenerOcurrencia()
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ObtenerOcurrencia();
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
        /// Obtiene registros de T_Ocurrencia para mostrarse en combo.
        /// </summary>
        /// <returns> List<OcurrenciaComboDTO> </returns>
        public IEnumerable<OcurrenciaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ObtenerCombo().OrderBy(combo => combo.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las Ocurrencias de Actividad por Ocurrencia Alterno
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> List<HojaActividadesDTO> </returns>
        public List<HojaActividadesDTO> ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ObtenerHojaActividadesPorIdOcurrenciaAlterno(idOcurrencia);
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
        /// Devuelve 1 o 0 si la Ocurrencia debe cambiar su estado.
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> bool </returns>
        public bool ValidarEstadoOcurrencia(int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(idOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si pertenece a Workshop o Prelanzamiento
        /// </summary>
        /// <param name="idCategoria"> Id de Categoría </param>
        /// <returns> int </returns>
        public int ValidarGrupoPreLanzamiento(int idCategoria)
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ValidarGrupoPreLanzamiento(idCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Ocurrencia asociado al Identificador.
        /// </summary>
        /// <param name="idOcurrencia"> Id de la Ocurrencia </param>
        /// <returns> OcurrenciaDTO </returns>
        public OcurrenciaDTO ObtenerPorId(int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OcurrenciaRepository.ObtenerPorId(idOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
