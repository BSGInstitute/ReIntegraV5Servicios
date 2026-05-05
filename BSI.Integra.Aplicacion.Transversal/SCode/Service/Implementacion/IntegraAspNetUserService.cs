using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: IntegraAspNetUserService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión general de T_IntegraAspNetUsers
    /// </summary>
    public class IntegraAspNetUserService : IIntegraAspNetUserService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public IntegraAspNetUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TIntegraAspNetUser, IntegraAspNetUser>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Version: 1.0
        /// <summary>
        /// Valida el acceso por ip
        /// </summary>
        /// <param name="ipPublica"></param>
        public void ValidarAcceso(string ipPublica)
        {
            try
            {

                var resultado = _unitOfWork.IntegraAspNetUserRepository.ObtenerAccesoPorIp(ipPublica);

                if (resultado == null)
                {
                    throw new UnauthorizedAccessRequestException("Usted no puede ingresar mediante su red actual");
                }
                else
                {
                    if (resultado.FechaExpira != null)
                    {
                        if (resultado.FechaExpira < DateTime.Now)
                        {
                            throw new UnauthorizedAccessRequestException("Los permisos de red ha finalizado");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Version: 1.0
        /// <summary>
        /// Valida el acceso por ip
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns> StringDTO </returns>
        public StringDTO ValidarReLogin(string usuario)
        {
            return _unitOfWork.IntegraAspNetUserRepository.ValidarReLogin(usuario);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Version: 1.0
        /// <summary>
        /// Valida el acceso por ip
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns> StringDTO </returns>
        public StringDTO ActualizarReLogin(string usuario)
        {
            return _unitOfWork.IntegraAspNetUserRepository.ActualizarReLogin(usuario);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Integra_AspNetUsers por Usuario
        /// </summary>
        /// <returns> List<IntegraAspNetUserDTO> </returns>
        public IEnumerable<IntegraAspNetUserDTO> ObtenerPorUsuario(string usuarioNombre)
        {
            try
            {
                return _unitOfWork.IntegraAspNetUserRepository.ObtenerPorUsuario(usuarioNombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener información por usuario para visualización de módulos agrupados
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <returns>Lista de registros por Usuario</returns>
        /// <returns> lista de objetos DTO: List<ModuloCreacionAgrupadoDTO> </returns>
        public List<ModuloAgrupacionDTO> ObtenerDatosParaModuloAgrupado(string usuario)
        {
            try
            {
                return _unitOfWork.IntegraAspNetUserRepository.ObtenerDatosParaModuloAgrupado(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_Integra_AspNetUsers por PerId.
        /// </summary>
        /// <param name="perId"></param>
        /// <returns> IntegraAspNetUser </returns>
        public IntegraAspNetUser ObtenerPorId(int perId)
        {
            try
            {
                return _unitOfWork.IntegraAspNetUserRepository.ObtenerPorIdPersonal(perId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Entidad: IntegraAspNetUser </returns>
        public IntegraAspNetUser ObtenerPorNombreUsuario(string nombreUsuario)
        {
            return _unitOfWork.IntegraAspNetUserRepository.ObtenerPorNombreUsuario(nombreUsuario);
        }

        ///Autor: Jose V.
        ///Fecha: 28/04/2021
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Email Usuario</returns>
        public string ObtenerEmailPorNombreUsuario(string nombreUsuario)
        {
            try
            {
                return _unitOfWork.IntegraAspNetUserRepository.FirstBy(x => x.UserName == nombreUsuario).Email;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
