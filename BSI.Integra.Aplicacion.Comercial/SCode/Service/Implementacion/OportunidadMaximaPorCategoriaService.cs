using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: OportunidadMaximaPorCategoriaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_OportunidadMaximaPorCategoria
    /// </summary>
    public class OportunidadMaximaPorCategoriaService : IOportunidadMaximaPorCategoriaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OportunidadMaximaPorCategoriaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TOportunidadMaximaPorCategorium, OportunidadMaximaPorCategoria>(MemberList.None).ReverseMap();
                    cfg.CreateMap<OportunidadMaximaPorCategoriaDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public OportunidadMaximaPorCategoria Add(OportunidadMaximaPorCategoria entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadMaximaPorCategoriaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadMaximaPorCategoria>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OportunidadMaximaPorCategoria Update(OportunidadMaximaPorCategoria entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadMaximaPorCategoriaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OportunidadMaximaPorCategoria>(modelo);
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
                _unitOfWork.OportunidadMaximaPorCategoriaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadMaximaPorCategoria> Add(List<OportunidadMaximaPorCategoria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadMaximaPorCategoriaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadMaximaPorCategoria>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OportunidadMaximaPorCategoria> Update(List<OportunidadMaximaPorCategoria> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadMaximaPorCategoriaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OportunidadMaximaPorCategoria>>(modelo);
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
                _unitOfWork.OportunidadMaximaPorCategoriaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_OportunidadMaximaPorCategoria
        /// </summary>
        /// <returns> List<OportunidadMaximaPorCategoriaDTO> </returns>
        public IEnumerable<OportunidadMaximaPorCategoriaDTO> ObtenerOportunidadMaximaPorCategoria()
        {
            try
            {
                return _unitOfWork.OportunidadMaximaPorCategoriaRepository.ObtenerOportunidadMaximaPorCategoria();
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
        /// Obtiene registros de T_OportunidadMaximaPorCategoria para mostrarse en combo.
        /// </summary>
        /// <returns> List<OportunidadMaximaPorCategoriaComboDTO> </returns>
        public IEnumerable<OportunidadMaximaPorCategoriaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OportunidadMaximaPorCategoriaRepository.ObtenerCombo();
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
        /// Obtiene el Conteo de Oportunidades Cerradas por el Asesor por Grupos
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idCategoriaOrigen">Id de Categoria Origen</param>
        /// <param name="estadoPantalla">Flag: 0 -> IS, 1 -> Opo. Cerrada, 2-> Solo para mostrar</param>
        /// <returns> SeguimientoAsesorDTO </returns>
        public SeguimientoAsesorDTO ObtenerSeguimientoAsesor(int idPersonal, int idCategoriaOrigen, int estadoPantalla)
        {
            try
            {
                return _unitOfWork.OportunidadMaximaPorCategoriaRepository.ObtenerSeguimientoAsesor(idPersonal, idCategoriaOrigen, estadoPantalla);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Conteo de Oportunidades Cerradas por el Asesor por Grupos (ejemplo: Grupo 1, Grupo 2, etc) 
        /// </summary>
        /// <param name="idAsesor"> id del asesor </param>
        /// oportunidad cerrada y 2 = visualizacion solo obtiene datos para mostrar</param> 
        public void ActualizarDatosEstaticosPantalla2(int idAsesor, int idCategoriaOrigen, int estadoISOM)
        {
            try
            {
                _unitOfWork.OportunidadMaximaPorCategoriaRepository.ActualizarDatosEstaticosPantalla2(idAsesor, idCategoriaOrigen, estadoISOM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
