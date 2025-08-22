using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionProyeccionFurService
    /// Autor: Margiory Ramirez.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionProyeccionFur
    /// </summary>
    public class ConfiguracionProyeccionFurService : IConfiguracionProyeccionFurService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionProyeccionFurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionProyeccionFur, ConfiguracionProyeccionFur>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionProyeccionFurDTO, ConfiguracionProyeccionFur>(MemberList.None).ReverseMap();
            }
           );

         
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionProyeccionFur Add(ConfiguracionProyeccionFur entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionProyeccionFurRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionProyeccionFur>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionProyeccionFur Update(ConfiguracionProyeccionFur entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionProyeccionFurRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionProyeccionFur>(modelo);
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
                _unitOfWork.ConfiguracionProyeccionFurRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionProyeccionFur> Add(List<ConfiguracionProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionProyeccionFurRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionProyeccionFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionProyeccionFur> Update(List<ConfiguracionProyeccionFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionProyeccionFurRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionProyeccionFur>>(modelo);
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
                _unitOfWork.ConfiguracionProyeccionFurRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta datois a la tabla T_ConfiguracionProyeccionFur
        /// </summary>
        /// <returns></returns>

        public ConfiguracionProyeccionFur InsertarConfiguracionProyeccionFur(ConfiguracionProyeccionFurDTO entidad, string Usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var repConf = _unitOfWork.ConfiguracionProyeccionFurRepository;
                    ConfiguracionProyeccionFur data = _mapper.Map<ConfiguracionProyeccionFur>(entidad);
                    data.Id = 0;
                    data.UsuarioModificacion = Usuario;
                    data.UsuarioCreacion = Usuario;
                    data.FechaCreacion = DateTime.Now;
                    data.FechaModificacion = DateTime.Now;
                    data.Estado = true;
                    if (data.Activo == true)
                    {
                        var ConfActivos = repConf.ObtenerConfiguracionProyeccionFurActivos();
                        if (ConfActivos.Count() > 0)
                        {
                            foreach (var item in ConfActivos)
                            {
                                this.desactivarConfiguracion(item.Id, Usuario);
                            }
                        }
                    }
                    var modelo = this.Add(data);
                    scope.Complete();
                    return modelo;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Actuliza datos en la tabla T_ConfiguracionProyeccionFur
        /// </summary>
        /// <returns></returns>
        public ConfiguracionProyeccionFur ActualizarConfiguracionProyeccionFur(ConfiguracionProyeccionFurDTO entidad, string Usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var repConf = _unitOfWork.ConfiguracionProyeccionFurRepository;
                    var entidadActual = _mapper.Map<ConfiguracionProyeccionFur>(repConf.ObtenerConfiguracionProyeccionFurById(entidad.Id));
                    entidadActual.UsuarioModificacion = Usuario;
                    entidadActual.FechaModificacion = DateTime.Now;
                    entidadActual.IdPeriodoProyeccion = entidad.IdPeriodoProyeccion;
                    entidadActual.FechaSemilla = entidad.FechaSemilla;
                    entidadActual.Activo = entidad.Activo;
                    entidadActual.FechaLimiteEnvio = entidad.FechaLimiteEnvio;

                    if (entidadActual.Activo == true)
                    {
                        var ConfActivos = repConf.ObtenerConfiguracionProyeccionFurActivos();
                        if (ConfActivos.Count() > 0)
                        {
                            foreach (var item in ConfActivos)
                            {
                                if (item.Id != entidadActual.Id) this.desactivarConfiguracion(item.Id, Usuario);
                            }
                        }
                    }
                    var modelo = this.Update(entidadActual);
                    scope.Complete();
                    return modelo;
                    
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionProyeccionFur
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<ConfiguracionProyeccionFurDTO> ObtenerConfiguracionProyeccionFur()
        {
            try
            {
                return _mapper.Map<IEnumerable<ConfiguracionProyeccionFurDTO>>(_unitOfWork.ConfiguracionProyeccionFurRepository.ObtenerConfiguracionProyeccionFur());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Registro Activo para generar solicitudes
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public ConfiguracionProyeccionFur ObtenerConfiguracionProyeccionFurActivos()
        {
            try
            {
                var respuesta = _unitOfWork.ConfiguracionProyeccionFurRepository.ObtenerConfiguracionProyeccionFurActivos();

                if (respuesta.Count() >= 2)
                {
                    throw new Exception("Existen dos configuraciones activas, solicita al encargado de la configuración mantener solo un registroa activo!");
                }
                else if (respuesta.Count() == 1) return respuesta[0];
                else return new ConfiguracionProyeccionFur();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Cambia los estados de las configuraciones
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public bool CambiarActivoConfiguracion(List<int> IdActual,int IdNuevo,string Usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var repConfiguracionProyeccionFur = _unitOfWork.ConfiguracionProyeccionFurRepository;
                    foreach (var id in IdActual)
                    {
                        if (id != 0)
                        {
                            var entidad = repConfiguracionProyeccionFur.ObtenerConfiguracionProyeccionFurById(id);
                            if (entidad == null || entidad.Id != id) throw new Exception("No se encontro la configuracion a desactivar!");
                            else
                            {
                                entidad.Activo = false;
                                entidad.UsuarioModificacion = Usuario;
                                this.Update(entidad);
                            }
                        }
                    }
                    var entidadNueva = repConfiguracionProyeccionFur.ObtenerConfiguracionProyeccionFurById(IdNuevo);
                    if (entidadNueva == null || entidadNueva.Id != IdNuevo) throw new Exception("No se encontro la configuracion a activar!");
                    else
                    {
                        entidadNueva.Activo = true;
                        entidadNueva.UsuarioModificacion = Usuario;
                        this.Update(entidadNueva);
                    }
                    scope.Complete();
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// descativa la configuracion
        /// </summary>
        /// <returns> bool</returns>
        public bool desactivarConfiguracion(int Id,string Usuario)
        {
            try
            {
                var repConfiguracionProyeccionFur = _unitOfWork.ConfiguracionProyeccionFurRepository;

                var entidadNueva = repConfiguracionProyeccionFur.ObtenerConfiguracionProyeccionFurById(Id);
                if (entidadNueva.Id != Id) throw new Exception("No se encontro la configuracion a desactivar!");
                else
                {
                    entidadNueva.Activo = false;
                    entidadNueva.UsuarioModificacion = Usuario;
                    this.Update(entidadNueva);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ConfiguracionProyeccionFur ObtenerConfiguracionProyeccionFurById(int Id)
        {
            try
            {
                return _unitOfWork.ConfiguracionProyeccionFurRepository.ObtenerConfiguracionProyeccionFurById(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
