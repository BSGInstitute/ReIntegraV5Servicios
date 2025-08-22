using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionDatoRemarketingProbabilidadRegistroService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 06/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketingProbabilidadRegistro
    /// </summary>
    public class ConfiguracionDatoRemarketingProbabilidadRegistroService : IConfiguracionDatoRemarketingProbabilidadRegistroService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionDatoRemarketingProbabilidadRegistroService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionDatoRemarketingProbabilidadRegistro, ConfiguracionDatoRemarketingProbabilidadRegistro>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionDatoRemarketingProbabilidadRegistro Add(ConfiguracionDatoRemarketingProbabilidadRegistro entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingProbabilidadRegistro>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionDatoRemarketingProbabilidadRegistro Update(ConfiguracionDatoRemarketingProbabilidadRegistro entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketingProbabilidadRegistro>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingProbabilidadRegistro> Add(List<ConfiguracionDatoRemarketingProbabilidadRegistro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingProbabilidadRegistro>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingProbabilidadRegistro> Update(List<ConfiguracionDatoRemarketingProbabilidadRegistro> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketingProbabilidadRegistro>>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_ConfiguracionDatoRemarketingProbabilidadRegistro para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository.ObtenerCombo();
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
        /// <param name="listaIdProbabilidadRegistro">Lista de id de los tipos de datos (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingProbabilidadRegistro(int idConfiguracionDatoRemarketing, List<int> listaIdProbabilidadRegistro, string usuario)
        {
            try
            {
                var _repConfiguracionDatoRemarketingProbabilidadRegistro = _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository;
                var listaExistente = _repConfiguracionDatoRemarketingProbabilidadRegistro.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing);

                // Eliminado de datos no existentes
                var listaAEliminar = listaExistente.Where(x => !listaIdProbabilidadRegistro.Contains(x.IdProbabilidadRegistroPw));

                if (listaAEliminar.Any())
                {
                    bool resultadoEliminacion = _repConfiguracionDatoRemarketingProbabilidadRegistro.Delete(listaAEliminar.Select(s => s.Id), usuario);

                    if (!resultadoEliminacion)
                        throw new Exception("Fallo en la eliminacion de tipos de probabilidades de registro anteriores");
                }

                var listaAMantenerProbabilidadRegistro = listaExistente.Where(x => listaIdProbabilidadRegistro.Contains(x.IdProbabilidadRegistroPw)).Select(s => s.IdProbabilidadRegistroPw);

                // Insercion de nuevos datos
                var listaAInsertar = listaIdProbabilidadRegistro.Where(x => !listaAMantenerProbabilidadRegistro.Contains(x));

                if (listaAInsertar.Any())
                {
                    bool resultadoInsercion = _repConfiguracionDatoRemarketingProbabilidadRegistro.Insert(listaAInsertar.Select(s => new TConfiguracionDatoRemarketingProbabilidadRegistro()
                    {
                        IdConfiguracionDatoRemarketing = idConfiguracionDatoRemarketing,
                        IdProbabilidadRegistroPw = s,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    }));
                    _unitOfWork.Commit();

                    if (!resultadoInsercion)
                        throw new Exception("Fallo en la insercion de tipos de probabilidades de registro nuevos");
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
        /// Version: 1.0
        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de probabilidad de registro
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketingProbabilidadRegistro(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var _repConfiguracionDatoRemarketingProbabilidadRegistro = _unitOfWork.ConfiguracionDatoRemarketingProbabilidadRegistroRepository;
                var listaIdAEliminar = _repConfiguracionDatoRemarketingProbabilidadRegistro.GetBy(x => x.IdConfiguracionDatoRemarketing == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketingProbabilidadRegistro.Delete(listaIdAEliminar, usuarioResponsable);
                _unitOfWork.Commit();

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de probabilidad de registro");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
