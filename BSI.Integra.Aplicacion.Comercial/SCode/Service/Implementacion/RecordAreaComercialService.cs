using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: RecordAreaComercialService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_RecordAreaComercial
    /// </summary>
    public class RecordAreaComercialService : IRecordAreaComercialService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public RecordAreaComercialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TRecordAreaComercial, RecordAreaComercial>(MemberList.None).ReverseMap();
                    cfg.CreateMap<RecordAreaComercialDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public RecordAreaComercial Add(RecordAreaComercial entidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordAreaComercialRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecordAreaComercial>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RecordAreaComercial Update(RecordAreaComercial entidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordAreaComercialRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RecordAreaComercial>(modelo);
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
                _unitOfWork.RecordAreaComercialRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordAreaComercial> Add(List<RecordAreaComercial> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordAreaComercialRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecordAreaComercial>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecordAreaComercial> Update(List<RecordAreaComercial> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RecordAreaComercialRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RecordAreaComercial>>(modelo);
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
                _unitOfWork.RecordAreaComercialRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_RecordAreaComercial
        /// </summary>
        /// <returns> List<RecordAreaComercialDTO> </returns>
        public IEnumerable<RecordAreaComercialDTO> ObtenerRecordAreaComercial()
        {
            try
            {
                return _unitOfWork.RecordAreaComercialRepository.ObtenerRecordAreaComercial();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RecordAreaComercial para mostrarse en combo.
        /// </summary>
        /// <returns> List<RecordAreaComercialComboDTO> </returns>
        public IEnumerable<RecordAreaComercialComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.RecordAreaComercialRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de datos para (M)Record del area comercial
        /// </summary>
        /// <returns> List<RecordAreaComercialCompuestoDTO> </returns>
        public IEnumerable<RecordAreaComercialCompuestoDTO> ObtenerRecordAreaComercialParaTabla()
        {
            try
            {
                return _unitOfWork.RecordAreaComercialRepository.ObtenerRecordAreaComercialParaTabla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_RecordAreaComercial por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> RecordAreaComercial </returns>
        public RecordAreaComercial ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.RecordAreaComercialRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
