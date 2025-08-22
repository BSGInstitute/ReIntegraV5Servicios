using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: SemaforoFinancieroDetalleService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_SemaforoFinancieroDetalle
    /// </summary>
    public class SemaforoFinancieroDetalleService : ISemaforoFinancieroDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public SemaforoFinancieroDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TSemaforoFinancieroDetalle, SemaforoFinancieroDetalle>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public SemaforoFinancieroDetalle Add(SemaforoFinancieroDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SemaforoFinancieroDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SemaforoFinancieroDetalle Update(SemaforoFinancieroDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SemaforoFinancieroDetalle>(modelo);
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
                _unitOfWork.SemaforoFinancieroDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinancieroDetalle> Add(List<SemaforoFinancieroDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinancieroDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SemaforoFinancieroDetalle> Update(List<SemaforoFinancieroDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SemaforoFinancieroDetalle>>(modelo);
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
                _unitOfWork.SemaforoFinancieroDetalleRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_SemaforoFinancieroDetalle
        /// </summary>
        /// <returns> List<SemaforoFinancieroDetalleDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetalle()
        {
            return _unitOfWork.SemaforoFinancieroDetalleRepository.ObtenerSemaforoFinancieroDetalle();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SemaforoFinancieroDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleComboDTO> ObtenerCombo()
        {
            return _unitOfWork.SemaforoFinancieroDetalleRepository.ObtenerCombo();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_SemaforoFinancieroDetalle asociados a un Semaforo Financiero
        /// </summary>
        /// <param name="idSemaforoFinanciero">Id del semaforo financiero</param>
        /// <returns> List<SemaforoFinancieroDetalleDTO> </returns>
        public IEnumerable<SemaforoFinancieroDetalleDTO> ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(int idSemaforoFinanciero)
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroDetalleRepository.ObtenerSemaforoFinancieroDetallePorIdSemaforoFinanciero(idSemaforoFinanciero);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta nuevo registro a la tabla T_SemaforoFinancieroDetalle.
        /// </summary>
        public bool InsertarNuevoSemaforoDetalle(SemaforoFinancieroDetalle objeto)
        {
            try
            {
                var modelo = _unitOfWork.SemaforoFinancieroDetalleRepository.InsertarNuevoSemaforoDetalle(objeto);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las variables segun el IdSemaforoFinancieroDetalle
        /// </summary>
        /// <returns> SemaforoFinancieroDetalleVariableInformacionDetalladaDTO </returns>
        public List<SemaforoFinancieroDetalleVariableInformacionDetalladaDTO> ObtenerVariables(int IdSemaforoFinancieroDetalle)
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroDetalleRepository.ObtenerVariables(IdSemaforoFinancieroDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Quispe
        /// Fecha: 02/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el T_SemaforoFinancieroDetalle por el id
        /// </summary>
        /// <param name="id"> id de la Entidad/param>
        /// <returns> SemaforoFinancieroDetalle </returns>
        public SemaforoFinancieroDetalle ObtenerSemaforoFinancieroDetallePorId(int id)
        {
            try
            {
                return _unitOfWork.SemaforoFinancieroDetalleRepository.ObtenerSemaforoFinancieroDetallePorId(id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza las tablas T_semaforoFinancieroDetalle y T_SemaforoFinancieroDetalleVariable
        /// </summary>
        /// <param name="Semaforo">DTO enviado desde la interfaz<</param>
        /// <returns>Response 200 o 400, dependiendo del flujo</returns>
        public SemaforoFinancieroDetalle ActualizarSemaforoFinancieroDetalle(SemaforoFinancieroDetalleV2DTO Semaforo)
        {
            try
            {

                var servicioSemaforoDetalle = new SemaforoFinancieroDetalleService(_unitOfWork);
                var semaforoDetalleNuevo = new SemaforoFinancieroDetalle();
                var servicioSemaforoVariable = new SemaforoFinancieroDetalleVariableService(_unitOfWork);

                semaforoDetalleNuevo = servicioSemaforoDetalle.ObtenerSemaforoFinancieroDetallePorId(Semaforo.Id);
                semaforoDetalleNuevo.Color = Semaforo.Color;
                semaforoDetalleNuevo.Mensaje = Semaforo.Mensaje;
                semaforoDetalleNuevo.Nombre = Semaforo.Nombre;
                semaforoDetalleNuevo.UsuarioModificacion = Semaforo.Usuario;
                semaforoDetalleNuevo.FechaModificacion = DateTime.Now;
                servicioSemaforoDetalle.Update(semaforoDetalleNuevo);
                if (Semaforo.Actualizar == 0)//insertar
                {
                    foreach (var item in Semaforo.Variable)
                    {
                        SemaforoFinancieroDetalleVariable semaforoVariable = new SemaforoFinancieroDetalleVariable();
                        semaforoVariable.IdSemaforoFinancieroDetalle = semaforoDetalleNuevo.Id;
                        semaforoVariable.IdSemaforoFinancieroVariable = (int)item.IdSemaforoFinancieroVariable;
                        semaforoVariable.ValorMinimo = item.ValorMinimo;
                        semaforoVariable.ValorMaximo = item.ValorMaximo;
                        semaforoVariable.IdMoneda = item.IdMoneda;
                        semaforoVariable.Estado = true;
                        semaforoVariable.UsuarioCreacion = Semaforo.Usuario;
                        semaforoVariable.UsuarioModificacion = Semaforo.Usuario;
                        semaforoVariable.FechaCreacion = DateTime.Now;
                        semaforoVariable.FechaModificacion = DateTime.Now;
                        servicioSemaforoVariable.Add(semaforoVariable);
                    }
                }
                else//Actualizar
                {
                    foreach (var item in Semaforo.Variable)
                    {
                        SemaforoFinancieroDetalleVariable semaforoVariable = new SemaforoFinancieroDetalleVariable();
                        semaforoVariable = servicioSemaforoVariable.ObtenerSemaforoDetalleVariablePorId((int)item.Id);
                        semaforoVariable.IdSemaforoFinancieroDetalle = semaforoDetalleNuevo.Id;
                        semaforoVariable.IdSemaforoFinancieroVariable = (int)item.IdSemaforoFinancieroVariable;
                        semaforoVariable.ValorMinimo = item.ValorMinimo;
                        semaforoVariable.ValorMaximo = item.ValorMaximo;
                        semaforoVariable.IdMoneda = item.IdMoneda;
                        semaforoVariable.UsuarioModificacion = Semaforo.Usuario;
                        semaforoVariable.FechaModificacion = DateTime.Now;
                        servicioSemaforoVariable.Update(semaforoVariable);
                    }
                }

                var resultado = servicioSemaforoDetalle.ObtenerSemaforoFinancieroDetallePorId(semaforoDetalleNuevo.IdSemaforoFinanciero);
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
