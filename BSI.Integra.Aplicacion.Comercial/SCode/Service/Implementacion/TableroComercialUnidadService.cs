using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: TableroComercialUnidadService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 02/08/2022
    /// <summary>
    /// Gestión general de T_TableroComercialUnidad
    /// </summary>
    public class TableroComercialUnidadService : ITableroComercialUnidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TableroComercialUnidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TTableroComercialUnidad, TableroComercialUnidad>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TableroComercialUnidadDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public TableroComercialUnidad Add(TableroComercialUnidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialUnidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TableroComercialUnidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TableroComercialUnidad Update(TableroComercialUnidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialUnidadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TableroComercialUnidad>(modelo);
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
                _unitOfWork.TableroComercialUnidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TableroComercialUnidad> Add(List<TableroComercialUnidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialUnidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TableroComercialUnidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TableroComercialUnidad> Update(List<TableroComercialUnidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialUnidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TableroComercialUnidad>>(modelo);
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
                _unitOfWork.TableroComercialUnidadRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TableroComercialUnidad
        /// </summary>
        /// <returns> List<TableroComercialUnidadDTO> </returns>
        public IEnumerable<TableroComercialUnidadDTO> ObtenerTableroComercialUnidad()
        {
            try
            {
                return _unitOfWork.TableroComercialUnidadRepository.ObtenerTableroComercialUnidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TableroComercialUnidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<TableroComercialUnidadComboDTO> </returns>
        public IEnumerable<TableroComercialUnidadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TableroComercialUnidadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_TableroComercialUnidad exceptuando campos de auditoria.
        /// </summary>
        /// <returns> List<TableroComercialUnidadSinAuditoriaDTO> </returns>
        public IEnumerable<TableroComercialUnidadSinAuditoriaDTO> ObtenerTableroComercialUnidadSinAuditoria()
        {
            try
            {
                return _unitOfWork.TableroComercialUnidadRepository.ObtenerTableroComercialUnidadSinAuditoria();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
