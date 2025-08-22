using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaBaseService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_PlantillaBase
    /// </summary>
    public class PlantillaBaseService : IPlantillaBaseService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaBaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaBase, PlantillaBase>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PlantillaBase Add(PlantillaBase entidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaBaseRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaBase>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlantillaBase Update(PlantillaBase entidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaBaseRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PlantillaBase>(modelo);
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
                _unitOfWork.PlantillaBaseRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaBase> Add(List<PlantillaBase> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaBaseRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaBase>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlantillaBase> Update(List<PlantillaBase> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaBaseRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlantillaBase>>(modelo);
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
                _unitOfWork.PlantillaBaseRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlantillaBase
        /// </summary>
        /// <returns> List<PlantillaBaseDTO> </returns>
        public IEnumerable<PlantillaBaseDTO> ObtenerPlantillaBase()
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.ObtenerPlantillaBase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaBase para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaBaseComboDTO> </returns>
        public IEnumerable<PlantillaBaseComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.ObtenerCombo();
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
        /// Obtiene el registro de T_PlantillaBase asociado a un Identificador.
        /// </summary>
        /// <param name="idPlantillaBase">Id de Plantilla Base</param>
        /// <returns> PlantillaBaseDTO </returns>
        public PlantillaBase ObtenerPorId(int idPlantillaBase)
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.ObtenerPorId(idPlantillaBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene el idPlantilla por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>PlantillaBaseDTO</returns>
        public PlantillaBaseDTO ObtenerIdPorNombre(string nombre)
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.ObtenerIdPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene el idPlantilla por IdActividad y idPlantillaBase
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <param name="idActividadDetalle"></param>
        /// <returns>SpeechBienvenidaDespedidaDTO</returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechBienvenida(int idActividadDetalle, int idPlantillaBase)
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.ObtenerIdPlantillaSpeechBienvenida(idActividadDetalle, idPlantillaBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idPlantilla de Speech-Despedidad por IdActividad y idPlantillaBase
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <param name="idActividadDetalle"></param>
        /// <returns>SpeechBienvenidaDespedidaDTO</returns>
        public SpeechBienvenidaDespedidaDTO ObtenerIdPlantillaSpeechDespedida(int idActividadDetalle, int idPlantillaBase)
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.ObtenerIdPlantillaSpeechDespedida(idActividadDetalle, idPlantillaBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si una Plantilla Existe basado en su identificador
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public bool ExistePorId(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaBaseRepository.Exist(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
