using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: GlassdoorConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de cuentas de empleador en Glassdoor.
    /// CRUD de la configuración (rating general, total evaluaciones, URL perfil, EmployerId).
    /// API pública descontinuada en 2023 — captura manual.
    /// </summary>
    public class GlassdoorConfiguracionService : IGlassdoorConfiguracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public GlassdoorConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TGlassdoorConfiguracion, GlassdoorConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene la configuración activa de la cuenta de empleador en Glassdoor.</summary>
        /// <returns>Primera configuración activa o null si no existe.</returns>
        public GlassdoorConfiguracion Obtener()
        {
            var modelo = _unitOfWork.GlassdoorConfiguracionRepository
                .GetBy(w => w.Estado == true).FirstOrDefault();
            return modelo != null ? _mapper.Map<GlassdoorConfiguracion>(modelo) : null;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de cuenta y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>GlassdoorConfiguracion</returns>
        public GlassdoorConfiguracion Add(GlassdoorConfiguracion entidad)
        {
            var modelo = _unitOfWork.GlassdoorConfiguracionRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GlassdoorConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una configuración de cuenta y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>GlassdoorConfiguracion</returns>
        public GlassdoorConfiguracion Update(GlassdoorConfiguracion entidad)
        {
            var modelo = _unitOfWork.GlassdoorConfiguracionRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GlassdoorConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una configuración de cuenta por su Id.</summary>
        /// <param name="id">Id de la configuración a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.GlassdoorConfiguracionRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }
    }
}
