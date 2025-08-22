using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionEnvioMailingService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/11/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioMailing
    /// </summary>
    public class ConfiguracionEnvioMailingService : IConfiguracionEnvioMailingService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionEnvioMailingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionEnvioMailing, ConfiguracionEnvioMailing>(MemberList.None).ReverseMap());
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 11/10/2022
        /// <summary>
        /// Obtiene los envios masivos
        /// </summary>
        /// <param name="email"> Correo. </param>
        /// <returns> Lista de objetos (CorreoDTO) </returns>
        public List<CorreoDTO> ObtenerEnviosMasivos(string email)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingRepository.ObtenerEnviosMasivos(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 01/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los envios masivos
        /// </summary>
        /// <param name="id"> Id de correo</param>
        /// <returns>ObjetoDTO: CorreoDTO</returns>
        public CorreoDTO ObtenerEnvioMasivo(int id)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioMailingRepository.ObtenerEnvioMasivo(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta la configuracion de envio mailing
        /// </summary>
        /// <param Entidad="ObjetoJson"> usuario</param>
        /// <param name="Usuario"> usuario</param>
        /// <returns>true</returns>

        public bool Insertar(ConfiguracionEnvioMailingDTO ObjetoJson, string Usuario)
        {

            try
            {
                var _repConfiguracionEnvioMailingRepositorio = _unitOfWork.ConfiguracionEnvioMailingRepository;

                    if (ObjetoJson.Id != 0)
                    {
                        if (_repConfiguracionEnvioMailingRepositorio.Exist(ObjetoJson.Id))
                        {
                            var configuracionEnvioMailingBO = _repConfiguracionEnvioMailingRepositorio.FirstById(ObjetoJson.Id);
                            configuracionEnvioMailingBO.Activo = false;
                            configuracionEnvioMailingBO.UsuarioModificacion = Usuario;
                            configuracionEnvioMailingBO.FechaModificacion = DateTime.Now;
                            _repConfiguracionEnvioMailingRepositorio.Update(configuracionEnvioMailingBO);
                        }
                    }

                    var configuracionEnvioMailing = new ConfiguracionEnvioMailing()
                    {
                        Nombre = ObjetoJson.Nombre,
                        Descripcion = ObjetoJson.Descripcion,
                        IdConjuntoListaDetalle = ObjetoJson.IdConjuntoListaDetalle,
                        IdPlantilla = ObjetoJson.IdPlantilla,
                        Estado = true,
                        Activo = true,
                        UsuarioCreacion = Usuario,
                        UsuarioModificacion = Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                _repConfiguracionEnvioMailingRepositorio.Add(configuracionEnvioMailing);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
