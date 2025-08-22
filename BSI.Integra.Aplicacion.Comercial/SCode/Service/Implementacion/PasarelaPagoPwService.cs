using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: PasarelaPagoPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_PasarelaPagoPw
    /// </summary>
    public class PasarelaPagoPwService : IPasarelaPagoPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PasarelaPagoPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPasarelaPagoPw, PasarelaPagoPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PasarelaPagoPw Add(PasarelaPagoPwEnvioDTO entidadDTO,string Usuario)
        {
            try
            {
                var repPasarela = _unitOfWork.PasarelaPagoPwRepository;
                PasarelaPagoPw entidad = new PasarelaPagoPw();
                var selectPrioridad = repPasarela.FirstBy(x => x.IdPais == entidadDTO.IdPais && x.Prioridad == entidadDTO.Prioridad);
                if (selectPrioridad == null)
                {
                    entidad.Id = 0;
                    entidad.Nombre = entidadDTO.Nombre;
                    entidad.Prioridad = entidadDTO.Prioridad;
                    entidad.IdPais = entidadDTO.IdPais;
                    entidad.IdProveedor = entidadDTO.IdProveedor;
                    entidad.Estado = true;
                    entidad.UsuarioCreacion = Usuario;
                    entidad.UsuarioModificacion = Usuario;
                    entidad.FechaCreacion = DateTime.Now;
                    entidad.FechaModificacion = DateTime.Now;

                    var modelo = repPasarela.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<PasarelaPagoPw>(modelo);
                }
                else
                {
                    throw new Exception("Error, el país tiene una prioridad ya configurada, seleccione otra.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PasarelaPagoPw Update(PasarelaPagoPwEnvioDTO entidadDTO, string Usuario)
        {
            try
            {
                var repPasarela = _unitOfWork.PasarelaPagoPwRepository;
                PasarelaPagoPw entidad = new PasarelaPagoPw();
                var selectPrioridad = repPasarela.FirstBy(x => x.IdPais == entidadDTO.IdPais && x.Prioridad == entidadDTO.Prioridad);
                var objetoActualizar = repPasarela.FirstById(entidadDTO.Id);
                entidad = _mapper.Map<PasarelaPagoPw>(objetoActualizar);
                var estadoActualizar = false;

                if (selectPrioridad != null)
                {
                    if (selectPrioridad.Id == objetoActualizar.Id) estadoActualizar = true;
                    else estadoActualizar = false;
                }
                else estadoActualizar = true;
                if (estadoActualizar)
                {
                    entidad.IdPais = entidadDTO.IdPais;
                    entidad.IdProveedor = entidadDTO.IdProveedor;
                    entidad.Nombre = entidadDTO.Nombre;
                    entidad.Prioridad = entidadDTO.Prioridad;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioModificacion = Usuario;
                    var modelo = _unitOfWork.PasarelaPagoPwRepository.Update(entidad);
                    _unitOfWork.Commit();
                    entidad = _mapper.Map<PasarelaPagoPw>(modelo);
                }
                else
                {
                    throw new Exception("Error, el país tiene una prioridad ya configurada, seleccione otra.");
                }
                return entidad;
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
                _unitOfWork.PasarelaPagoPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PasarelaPagoPw> Add(List<PasarelaPagoPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PasarelaPagoPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PasarelaPagoPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PasarelaPagoPw> Update(List<PasarelaPagoPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PasarelaPagoPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PasarelaPagoPw>>(modelo);
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
                _unitOfWork.PasarelaPagoPwRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PasarelaPagoPw
        /// </summary>
        /// <returns> List<PasarelaPagoPwDTO> </returns>
        public IEnumerable<RegistroPasarelaPagoPWDTO> ObtenerPasarelaPagoPw()
        {
            try
            {
                return _unitOfWork.PasarelaPagoPwRepository.ObtenerPasarelaPagoPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PasarelaPagoPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<PasarelaPagoPwComboDTO> </returns>
        public IEnumerable<PasarelaPagoPwComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PasarelaPagoPwRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 26/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_PasarelaPagoPw relacionados a un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<PasarelaPagoPwAgendaDTO> </returns>
        public IEnumerable<PasarelaPagoPwAgendaDTO> ObtenerPasarelaPagoPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.PasarelaPagoPwRepository.ObtenerPasarelaPagoPorIdAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// registrar el metodo de pago seleccionado para la matricula
        /// </summary>
        /// <param name="modelo">Datos para el registro de un Medio de Pago</param>
        /// <returns> bool </returns>
        public bool RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO modelo)
        {
            var servicioMedioPago = new MedioPagoMatriculaCronogramaService(_unitOfWork);
            //Validar que no sea repetido
            var metodoPagoActual = servicioMedioPago.MedioPagoMatriculaCronogramaPorIdMatricula(modelo.IdMatriculaCabecera);
            if (metodoPagoActual != null)
            {
                if (metodoPagoActual.IdMedioPago != modelo.IdMedioPago)
                {
                    var desactivar = servicioMedioPago.DesactivarMedioPagoMatriculaCronogramaPorMatricula(modelo.IdMatriculaCabecera);
                    var _metodoPago = servicioMedioPago.RegistroMedioPagoMatriculaCronograma(modelo);
                    return true;
                }
            }
            else
            {
                var desactivar = servicioMedioPago.DesactivarMedioPagoMatriculaCronogramaPorMatricula(modelo.IdMatriculaCabecera);
                var _metodoPago = servicioMedioPago.RegistroMedioPagoMatriculaCronograma(modelo);
                return true;
            }

            return false;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 30/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Identificador de la Matricula basado en su Codigo
        /// </summary>
        /// <param name="codigoMatricula">Codigo Matricula</param>
        /// <returns> int </returns>
        public int BuscarIdMatriculaCabeceraPorCodigoMatricula(string codigoMatricula)
        {
            try
            {
                return _unitOfWork.PasarelaPagoPwRepository.BuscarIdMatriculaCabeceraPorCodigoMatricula(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int BuscarIdMatriculaCabecera(string codigoMatricula)
        {

            if (codigoMatricula != "")
            {

                var data = _unitOfWork.PasarelaPagoPwRepository.BuscarIdMatriculaCabeceraPrueba(codigoMatricula);

                if (data == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }

        }

    }
}
