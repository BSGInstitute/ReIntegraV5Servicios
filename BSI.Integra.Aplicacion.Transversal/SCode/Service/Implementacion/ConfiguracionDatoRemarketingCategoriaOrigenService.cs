using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionDatoRemarketingCategoriaOrigenService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 06/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketingCategoriaOrigen
    /// </summary>
    public class ConfiguracionDatoRemarketingCategoriaOrigenService : IConfiguracionDatoRemarketingCategoriaOrigenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingCategoriaOrigenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionDatoRemarketingCategoriaOrigen, ConfiguracionDatoRemarketingCategoriaOrigen>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionDatoRemarketingCategoriaOrigen Add(ConfiguracionDatoRemarketingCategoriaOrigen entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingCategoriaOrigen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionDatoRemarketingCategoriaOrigen Update(ConfiguracionDatoRemarketingCategoriaOrigen entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingCategoriaOrigen>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingCategoriaOrigen> Add(List<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingCategoriaOrigen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingCategoriaOrigen> Update(List<ConfiguracionDatoRemarketingCategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingCategoriaOrigen>>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_ConfiguracionDatoRemarketingCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdCategoriaOrigen">Lista de id de los tipos de datos (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingCategoriaOrigen(int idConfiguracionDatoRemarketing, List<int> listaIdCategoriaOrigen, string usuario)
        {
            try
            {
                var _repConfiguracionDatoRemarketingCategoriaOrigen = _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository;

                var listaExistente = _repConfiguracionDatoRemarketingCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdCategoriaOrigen.Contains(x.IdCategoriaOrigen));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingCategoriaOrigen.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de categoria origen anteriores");
                }

                var listaAMantenerCategoriaOrigen = listaExistente.Where(x => listaIdCategoriaOrigen.Contains(x.IdCategoriaOrigen)).Select(s => s.IdCategoriaOrigen);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdCategoriaOrigen.Where(x => !listaAMantenerCategoriaOrigen.Contains(x));

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingCategoriaOrigen.Insert(listaAInsertar.Select(s => new TConfiguracionDatoRemarketingCategoriaOrigen()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdCategoriaOrigen = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));
                    _unitOfWork.Commit();

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de categoria origen nuevos");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de categoria origen
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingCategoriaOrigen(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var _repConfiguracionDatoRemarketingCategoriaOrigen = _unitOfWork.ConfiguracionDatoRemarketingCategoriaOrigenRepository;
                var listaIdAEliminar = _repConfiguracionDatoRemarketingCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingCategoriaOrigen.Delete(listaIdAEliminar, usuarioResponsable);
                _unitOfWork.Commit();
                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de categoria origen");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
