using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionDatoRemarketingConfiguracionDatoRemarketingTipoDatoService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 06/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketingTipoDato
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoDatoService : IConfiguracionDatoRemarketingTipoDatoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingTipoDatoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionDatoRemarketingTipoDato, ConfiguracionDatoRemarketingTipoDato>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionDatoRemarketingTipoDato Add(ConfiguracionDatoRemarketingTipoDato entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingTipoDato>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionDatoRemarketingTipoDato Update(ConfiguracionDatoRemarketingTipoDato entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingTipoDato>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingTipoDato> Add(List<ConfiguracionDatoRemarketingTipoDato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingTipoDato>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingTipoDato> Update(List<ConfiguracionDatoRemarketingTipoDato> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingTipoDato>>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionDatoRemarketingTipoDato para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.
        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdTipoDato">Lista de id de los tipos de datos (PK de la tabla mkt.T_TipoDato)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingTipoDato(int idConfiguracionDatoRemarketing, List<int> listaIdTipoDato, string usuario)
        {
            try
            {


                var _repConfiguracionDatoRemarketingTipoDato = _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository;
                var listaExistente = _repConfiguracionDatoRemarketingTipoDato.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdTipoDato.Contains(x.IdTipoDato));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingTipoDato.Delete(listaAEliminar.Select(s => s.Id), usuario);
                    _unitOfWork.Commit();
                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de tipos de datos anteriores");

                }

                var listaAMantenerTipoDato = listaExistente.Where(x => listaIdTipoDato.Contains(x.IdTipoDato)).Select(s => s.IdTipoDato);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdTipoDato.Where(x => !listaAMantenerTipoDato.Contains(x)).ToList();

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingTipoDato.Insert(listaAInsertar.Select(s => new TConfiguracionDatoRemarketingTipoDato()
                    
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdTipoDato = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));
                    _unitOfWork.Commit();

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de tipos de datos nuevos");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.
        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de probabilidad de registro
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingTipoDato(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var _repConfiguracionDatoRemarketingTipoDato = _unitOfWork.ConfiguracionDatoRemarketingTipoDatoRepository;
                var listaIdAEliminar = _repConfiguracionDatoRemarketingTipoDato.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingTipoDato.Delete(listaIdAEliminar, usuarioResponsable);
                _unitOfWork.Commit();

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de tipo de dato");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
