using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: ComputrabajoConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de cuentas de empleador en Computrabajo.
    /// CRUD de la configuración (rating general, total evaluaciones, URL perfil).
    /// Captura manual periódica (quincenal).
    /// </summary>
    public class ComputrabajoConfiguracionService : IComputrabajoConfiguracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public ComputrabajoConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TComputrabajoConfiguracion, ComputrabajoConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene la configuración activa de la cuenta de empleador en Computrabajo.</summary>
        /// <returns>Primera configuración activa o null si no existe.</returns>
        public ComputrabajoConfiguracion Obtener()
        {
            var modelo = _unitOfWork.ComputrabajoConfiguracionRepository
                .GetBy(w => w.Estado == true).FirstOrDefault();
            return modelo != null ? _mapper.Map<ComputrabajoConfiguracion>(modelo) : null;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de cuenta y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>ComputrabajoConfiguracion</returns>
        public ComputrabajoConfiguracion Add(ComputrabajoConfiguracion entidad)
        {
            var modelo = _unitOfWork.ComputrabajoConfiguracionRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<ComputrabajoConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una configuración de cuenta y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>ComputrabajoConfiguracion</returns>
        public ComputrabajoConfiguracion Update(ComputrabajoConfiguracion entidad)
        {
            var modelo = _unitOfWork.ComputrabajoConfiguracionRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<ComputrabajoConfiguracion>(modelo);
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
            _unitOfWork.ComputrabajoConfiguracionRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }
    }
}
