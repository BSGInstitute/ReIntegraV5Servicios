using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: FacebookConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de páginas de Facebook.
    /// CRUD de la configuración (identificador, nombre, token, opiniones, valoración).
    /// </summary>
    public class FacebookConfiguracionService : IFacebookConfiguracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public FacebookConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TFacebookConfiguracion, FacebookConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene todas las configuraciones activas de páginas de Facebook.</summary>
        /// <returns>Lista de configuraciones activas.</returns>
        public List<FacebookConfiguracion> ObtenerTodos()
        {
            var modelos = _unitOfWork.FacebookConfiguracionRepository
                .GetBy(w => w.Estado == true).ToList();
            return _mapper.Map<List<FacebookConfiguracion>>(modelos);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de página y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>FacebookConfiguracion</returns>
        public FacebookConfiguracion Add(FacebookConfiguracion entidad)
        {
            var modelo = _unitOfWork.FacebookConfiguracionRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<FacebookConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una configuración de página y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>FacebookConfiguracion</returns>
        public FacebookConfiguracion Update(FacebookConfiguracion entidad)
        {
            var modelo = _unitOfWork.FacebookConfiguracionRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<FacebookConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una configuración de página por su Id.</summary>
        /// <param name="id">Id de la configuración a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.FacebookConfiguracionRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }
    }
}
