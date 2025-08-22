using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionDatoRemarketingTipoCategoriaOrigenService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 06/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketingTipoCategoriaOrigen
    /// </summary>
    public class ConfiguracionDatoRemarketingTipoCategoriaOrigenService : IConfiguracionDatoRemarketingTipoCategoriaOrigenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingTipoCategoriaOrigenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionDatoRemarketingTipoCategoriaOrigen, ConfiguracionDatoRemarketingTipoCategoriaOrigen>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionDatoRemarketingTipoCategoriaOrigen Add(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingTipoCategoriaOrigen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionDatoRemarketingTipoCategoriaOrigen Update(ConfiguracionDatoRemarketingTipoCategoriaOrigen entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingTipoCategoriaOrigen>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> Add(List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingTipoCategoriaOrigen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> Update(List<ConfiguracionDatoRemarketingTipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingTipoCategoriaOrigen>>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_ConfiguracionDatoRemarketingTipoCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="listaIdTipoCategoriaOrigen">Lista de id de los tipos de datos (PK de la tabla mkt.T_TipoCategoriaOrigen)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(int idConfiguracionDatoRemarketing, List<int> listaIdTipoCategoriaOrigen, string usuario)
        {
            try

            {
                var _repConfiguracionDatoRemarketingTipoCategoriaOrigen = _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository;
                var listaExistente = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdTipoCategoriaOrigen.Contains(x.IdTipoCategoriaOrigen));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de tipos de categoria origen anteriores");
                }

                var listaAMantenerTipoCategoriaOrigen = listaExistente.Where(x => listaIdTipoCategoriaOrigen.Contains(x.IdTipoCategoriaOrigen)).Select(s => s.IdTipoCategoriaOrigen);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdTipoCategoriaOrigen.Where(x => !listaAMantenerTipoCategoriaOrigen.Contains(x));

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.Insert(listaAInsertar.Select(s => new TConfiguracionDatoRemarketingTipoCategoriaOrigen()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdTipoCategoriaOrigen = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));
                    _unitOfWork.Commit();

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de tipos de categoria origen nuevos");
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
        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de tipo categoria origen
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var _repConfiguracionDatoRemarketingTipoCategoriaOrigen = _unitOfWork.ConfiguracionDatoRemarketingTipoCategoriaOrigenRepository;
                var listaIdAEliminar = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingTipoCategoriaOrigen.Delete(listaIdAEliminar, usuarioResponsable);
                _unitOfWork.Commit();

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de tipos de categoria origen");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
