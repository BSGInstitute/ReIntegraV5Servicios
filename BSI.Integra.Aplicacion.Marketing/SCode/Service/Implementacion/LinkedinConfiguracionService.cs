using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: LinkedinConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de páginas de LinkedIn.
    /// CRUD de la configuración (nombre, enlace, total opiniones).
    /// </summary>
    public class LinkedinConfiguracionService : ILinkedinConfiguracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public LinkedinConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TLinkedinConfiguracion, LinkedinConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene la configuración activa de la página de LinkedIn.</summary>
        /// <returns>LinkedinConfiguracion</returns>
        public LinkedinConfiguracion Obtener()
        {
            var modelo = _unitOfWork.LinkedinConfiguracionRepository
                .GetBy(w => w.Estado == true).FirstOrDefault();
            return modelo != null ? _mapper.Map<LinkedinConfiguracion>(modelo) : null;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de página y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>LinkedinConfiguracion</returns>
        public LinkedinConfiguracion Add(LinkedinConfiguracion entidad)
        {
            var modelo = _unitOfWork.LinkedinConfiguracionRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<LinkedinConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una configuración de página y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>LinkedinConfiguracion</returns>
        public LinkedinConfiguracion Update(LinkedinConfiguracion entidad)
        {
            var modelo = _unitOfWork.LinkedinConfiguracionRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<LinkedinConfiguracion>(modelo);
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
            _unitOfWork.LinkedinConfiguracionRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }
    }
}
