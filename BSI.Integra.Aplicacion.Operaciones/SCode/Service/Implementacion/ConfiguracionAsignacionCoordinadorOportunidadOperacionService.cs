using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: ConfiguracionAsignacionCoordinadorOportunidadOperacionService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionAsignacionCoordinadorOportunidadOperaciones
    /// </summary>
    public class ConfiguracionAsignacionCoordinadorOportunidadOperacionService : IConfiguracionAsignacionCoordinadorOportunidadOperacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionAsignacionCoordinadorOportunidadOperacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionAsignacionCoordinadorOportunidadOperacione, ConfiguracionAsignacionCoordinadorOportunidadOperacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO, ConfiguracionAsignacionCoordinadorOportunidadOperacion>(MemberList.None);
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ConfiguracionAsignacionCoordinadorOportunidadOperacion Add(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionAsignacionCoordinadorOportunidadOperacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionAsignacionCoordinadorOportunidadOperacion Update(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionAsignacionCoordinadorOportunidadOperacion>(modelo);
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
                _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> Add(List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionAsignacionCoordinadorOportunidadOperacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> Update(List<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionAsignacionCoordinadorOportunidadOperacion>>(modelo);
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
                _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea desde ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO a ConfiguracionAsignacionCoordinadorOportunidadOperacion
        /// </summary>
        /// <param name="dto">ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO</param>
        /// <returns> ConfiguracionAsignacionCoordinadorOportunidadOperacion </returns>
        public ConfiguracionAsignacionCoordinadorOportunidadOperacion MapeoEntidadDesdeDTO(ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<ConfiguracionAsignacionCoordinadorOportunidadOperacion>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el coordinador de un determinado programa especifico mediante la configuracion ingresada en el modulo de configuracion coordinador
        /// </summary>
        /// <param name="idPEspecifico">Id del Programa Especifico</param>
        /// <returnsList<ConfiguracionCoordinadoraCentroCostoDTO></returns>
        public ConfiguracionCoordinadoraCentroCostoCantidadDTO ObtenerCoordinadorAsignacion(int idPEspecifico, int? idEstadoMatricula, int? idSubEstadoMatricula, int idMatriculaCabecera)
        {
            try
            {
                //Si se matriculo correctamente se hace la asignacion de coordinadora
                List<ConfiguracionCoordinadoraCentroCostoCantidadDTO> coordinadorCantidad = new List<ConfiguracionCoordinadoraCentroCostoCantidadDTO>();
                //Validar los casos y saber los de seguimiento academico
                //1:REGULAR, 6:REINCORPORADO , 11:ABANDONO REINCORPORADO
                if (idEstadoMatricula == EstadoMatricula.Regular
                    || idEstadoMatricula == EstadoMatricula.Reincorporado
                    || idEstadoMatricula == EstadoMatricula.AbandonoReincorporado)
                {
                    var subestado = _unitOfWork.MatriculaCabeceraRepository.ObtenerSubEstadoPorIdMatricula(idMatriculaCabecera); //13
                    if (subestado != null)
                        idSubEstadoMatricula = subestado.Valor;
                }
                var coordinadores = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerPorIdPEspecifico(idPEspecifico);
                var coordinadoresEstadoSubEstado = coordinadores.Where(w => w.IdEstadoMatricula == idEstadoMatricula && w.IdSubEstadoMatricula == idSubEstadoMatricula).ToList();
                if (coordinadoresEstadoSubEstado.Count() > 0)
                {
                    coordinadores = coordinadoresEstadoSubEstado;
                    if (coordinadores.Count() > 0)
                    {
                        foreach (var item in coordinadores)
                        {
                            ConfiguracionCoordinadoraCentroCostoCantidadDTO dto = new ConfiguracionCoordinadoraCentroCostoCantidadDTO();
                            dto.IdPersonal = item.IdPersonal;
                            dto.UsuarioPersonal = item.UsuarioPersonal;
                            dto.IdPespecifico = idPEspecifico;
                            dto.Cantidad = _unitOfWork.MatriculaCabeceraRepository.ObtenerCantidadPorIdPespecificoUsuarioCoordinador(idPEspecifico, item.UsuarioPersonal).Valor!.Value;
                            coordinadorCantidad.Add(dto);
                        }
                        var coordinador = coordinadorCantidad.OrderBy(x => x.Cantidad).FirstOrDefault();
                        if (coordinador != null && coordinador.UsuarioPersonal.Equals("esanchez1"))
                        {
                            coordinador.UsuarioPersonal = "esanchez";
                        }
                        return coordinador;
                    }
                    else
                    {
                        throw new BadRequestException("No existe configuracion de coordinador que cumpla con los criterios");
                    }
                }
                else
                {
                    throw new BadRequestException("No existe configuracion de coordinador que cumpla con los criterios");
                }


            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 04/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtener Configuracion Coordinadores
		/// </summary>
		/// <param></param>
		/// <returns>List<ConfiguracionCentroCostoCoordinadorDTO></returns>
        public List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerConfiguracionCoordinadores()
        {
            try
            {
                return _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerConfiguracionCoordinadores();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 04/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene todos los centros de costo sin asignacion de coordinadora
		/// </summary>
		/// <param></param>
		/// <returns>List<ConfiguracionCentroCostoCoordinadorDTO></returns>
        public List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerCentroCostoSigAsignacion()
        {
            try
            {
                return _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerCentroCostoSigAsignacion();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2022
        /// Version: 1.0
		/// <summary>
		/// Inserta o actualiza un registro de configuracion de coordinadoras
		/// </summary>
		/// <param name="ConfiguracionCoordinador">List<ConfiguracionCoordinadorDTO></param>
		/// <returns>Bool: True/False</returns>
		public bool InsertarActualizarConfiguracionCoordinador(List<ConfiguracionCoordinadorDTO> ConfiguracionCoordinador)
        {
            try
            {
                foreach (var configuracion in ConfiguracionCoordinador)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        foreach (var personal in configuracion.ListaPersonal)
                        {
                            if (configuracion.ListaEstadoMatricula.Length > 0)
                            {
                                foreach (var estadomatricula in configuracion.ListaEstadoMatricula)
                                {
                                    if (configuracion.ListaSubEstadoMatricula.Length > 0)
                                    {
                                        foreach (var subestadomatricula in configuracion.ListaSubEstadoMatricula)
                                        {
                                            foreach (var centroCosto in configuracion.ListaCentroCosto)
                                            {
                                                ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();

                                                var centroCostoHijos = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerCentroCostoHijos(centroCosto);
                                                if (centroCostoHijos.Count > 0) // Es padre
                                                {
                                                    foreach (var item in centroCostoHijos)
                                                    {
                                                        configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();
                                                        configuracionCoordinador.IdPersonal = personal;
                                                        configuracionCoordinador.IdCentroCosto = centroCosto;
                                                        configuracionCoordinador.IdCentroCostoHijo = item.IdCentroCostoHijo;
                                                        configuracionCoordinador.IdEstadoMatricula = estadomatricula;
                                                        configuracionCoordinador.IdSubEstadoMatricula = subestadomatricula;
                                                        configuracionCoordinador.Estado = true;
                                                        configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
                                                        configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
                                                        configuracionCoordinador.FechaCreacion = DateTime.Now;
                                                        configuracionCoordinador.FechaModificacion = DateTime.Now;
                                                        var mapeoConfiguracionCoordinador = MapeoEntidadDesdeDTO(configuracionCoordinador);
                                                        _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(mapeoConfiguracionCoordinador);
                                                        _unitOfWork.Commit();
                                                    }
                                                }
                                                else // Es Individual
                                                {
                                                    configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();
                                                    configuracionCoordinador.IdPersonal = personal;
                                                    configuracionCoordinador.IdCentroCosto = centroCosto;
                                                    configuracionCoordinador.IdEstadoMatricula = estadomatricula;
                                                    configuracionCoordinador.IdSubEstadoMatricula = subestadomatricula;
                                                    configuracionCoordinador.Estado = true;
                                                    configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
                                                    configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
                                                    configuracionCoordinador.FechaCreacion = DateTime.Now;
                                                    configuracionCoordinador.FechaModificacion = DateTime.Now;
                                                    var mapeoConfiguracionCoordinador = MapeoEntidadDesdeDTO(configuracionCoordinador);
                                                    _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(mapeoConfiguracionCoordinador);
                                                    _unitOfWork.Commit();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (var centroCosto in configuracion.ListaCentroCosto)
                                        {
                                            ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();

                                            var centroCostoHijos = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerCentroCostoHijos(centroCosto);
                                            if (centroCostoHijos.Count > 0) // Es padre
                                            {
                                                foreach (var item in centroCostoHijos)
                                                {
                                                    configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();
                                                    configuracionCoordinador.IdPersonal = personal;
                                                    configuracionCoordinador.IdCentroCosto = centroCosto;
                                                    configuracionCoordinador.IdCentroCostoHijo = item.IdCentroCostoHijo;
                                                    configuracionCoordinador.IdEstadoMatricula = estadomatricula;
                                                    configuracionCoordinador.IdSubEstadoMatricula = null;
                                                    configuracionCoordinador.Estado = true;
                                                    configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
                                                    configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
                                                    configuracionCoordinador.FechaCreacion = DateTime.Now;
                                                    configuracionCoordinador.FechaModificacion = DateTime.Now;
                                                    var mapeoConfiguracionCoordinador = MapeoEntidadDesdeDTO(configuracionCoordinador);
                                                    _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(mapeoConfiguracionCoordinador);
                                                    _unitOfWork.Commit();
                                                }
                                            }
                                            else // Es Individual
                                            {
                                                configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();
                                                configuracionCoordinador.IdPersonal = personal;
                                                configuracionCoordinador.IdCentroCosto = centroCosto;
                                                configuracionCoordinador.IdEstadoMatricula = estadomatricula;
                                                configuracionCoordinador.IdSubEstadoMatricula = null;
                                                configuracionCoordinador.Estado = true;
                                                configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
                                                configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
                                                configuracionCoordinador.FechaCreacion = DateTime.Now;
                                                configuracionCoordinador.FechaModificacion = DateTime.Now;
                                                var mapeoConfiguracionCoordinador = MapeoEntidadDesdeDTO(configuracionCoordinador);
                                                _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(mapeoConfiguracionCoordinador);
                                                _unitOfWork.Commit();
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var centroCosto in configuracion.ListaCentroCosto)
                                {
                                    ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();

                                    var centroCostoHijos = _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerCentroCostoHijos(centroCosto);
                                    if (centroCostoHijos.Count > 0) // Es padre
                                    {
                                        foreach (var item in centroCostoHijos)
                                        {
                                            configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();
                                            configuracionCoordinador.IdPersonal = personal;
                                            configuracionCoordinador.IdCentroCosto = centroCosto;
                                            configuracionCoordinador.IdCentroCostoHijo = item.IdCentroCostoHijo;
                                            configuracionCoordinador.IdEstadoMatricula = null;
                                            configuracionCoordinador.IdSubEstadoMatricula = null;
                                            configuracionCoordinador.Estado = true;
                                            configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
                                            configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
                                            configuracionCoordinador.FechaCreacion = DateTime.Now;
                                            configuracionCoordinador.FechaModificacion = DateTime.Now;
                                            var mapeoConfiguracionCoordinador = MapeoEntidadDesdeDTO(configuracionCoordinador);
                                            _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(mapeoConfiguracionCoordinador);
                                            _unitOfWork.Commit();
                                        }
                                    }
                                    else // Es Individual
                                    {
                                        configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO();
                                        configuracionCoordinador.IdPersonal = personal;
                                        configuracionCoordinador.IdCentroCosto = centroCosto;
                                        configuracionCoordinador.IdEstadoMatricula = null;
                                        configuracionCoordinador.IdSubEstadoMatricula = null;
                                        configuracionCoordinador.Estado = true;
                                        configuracionCoordinador.UsuarioCreacion = configuracion.Usuario;
                                        configuracionCoordinador.UsuarioModificacion = configuracion.Usuario;
                                        configuracionCoordinador.FechaCreacion = DateTime.Now;
                                        configuracionCoordinador.FechaModificacion = DateTime.Now;
                                        var mapeoConfiguracionCoordinador = MapeoEntidadDesdeDTO(configuracionCoordinador);
                                        _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.Add(mapeoConfiguracionCoordinador);
                                        _unitOfWork.Commit();
                                    }
                                }
                            }
                        }
                        scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Personal asociado al identificador.
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<PersonalDTO> </returns>
        public IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO> ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                return _unitOfWork.ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository.ObtenerPorIdPersonal(idPersonal);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }




    }
}
