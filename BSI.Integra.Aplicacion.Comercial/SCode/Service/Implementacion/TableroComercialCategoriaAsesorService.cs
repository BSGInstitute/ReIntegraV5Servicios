using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: TableroComercialCategoriaAsesorService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_TableroComercialCategoriaAsesor
    /// </summary>
    public class TableroComercialCategoriaAsesorService : ITableroComercialCategoriaAsesorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TableroComercialCategoriaAsesorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTableroComercialCategoriaAsesor, TableroComercialCategoriaAsesor>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TableroComercialCategoriaAsesor Add(TableroComercialCategoriaAsesor entidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialCategoriaAsesorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TableroComercialCategoriaAsesor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TableroComercialCategoriaAsesor Update(TableroComercialCategoriaAsesor entidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialCategoriaAsesorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<TableroComercialCategoriaAsesor>(modelo);
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
                _unitOfWork.TableroComercialCategoriaAsesorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TableroComercialCategoriaAsesor> Add(List<TableroComercialCategoriaAsesor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialCategoriaAsesorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TableroComercialCategoriaAsesor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TableroComercialCategoriaAsesor> Update(List<TableroComercialCategoriaAsesor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TableroComercialCategoriaAsesorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TableroComercialCategoriaAsesor>>(modelo);
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
                _unitOfWork.TableroComercialCategoriaAsesorRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TableroComercialCategoriaAsesor
        /// </summary>
        /// <returns> List<TableroComercialCategoriaAsesorDTO> </returns>
        public IEnumerable<TableroComercialCategoriaAsesorDTO> ObtenerTableroComercialCategoriaAsesor()
        {
            try
            {
                return _unitOfWork.TableroComercialCategoriaAsesorRepository.ObtenerTableroComercialCategoriaAsesor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TableroComercialCategoriaAsesor para mostrarse en combo.
        /// </summary>
        /// <returns> List<TableroComercialCategoriaAsesorComboDTO> </returns>
        public IEnumerable<TableroComercialCategoriaAsesorComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TableroComercialCategoriaAsesorRepository.ObtenerCombo();
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
        /// Obtiene informacion de TableroComercialCategoriaAsesor.
        /// </summary>
        /// <returns> List<TableroComercialCategoriaAsesorDatosTableroDTO> </returns>
        public IEnumerable<TableroComercialCategoriaAsesorDatosTableroDTO> ObtenerDatosTablero()
        {
            try
            {
                return _unitOfWork.TableroComercialCategoriaAsesorRepository.ObtenerDatosTablero();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
