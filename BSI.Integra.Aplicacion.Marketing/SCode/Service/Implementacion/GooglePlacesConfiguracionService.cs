using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: GooglePlacesConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de sedes de Google Places.
    /// CRUD de la configuración (nombre, identificador, valoración, total reseñas).
    /// </summary>
    public class GooglePlacesConfiguracionService : IGooglePlacesConfiguracionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public GooglePlacesConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TGooglePlacesConfiguracion, GooglePlacesConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene todas las configuraciones activas de sedes de Google Places.</summary>
        /// <returns>List de GooglePlacesConfiguracion</returns>
        public List<GooglePlacesConfiguracion> ObtenerTodos()
        {
            var modelos = _unitOfWork.GooglePlacesConfiguracionRepository
                .GetBy(w => w.Estado == true).ToList();
            return _mapper.Map<List<GooglePlacesConfiguracion>>(modelos);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Obtiene las sedes para el combo de selección del frontend.</summary>
        /// <returns>List de GooglePlacesConfiguracionComboDTO</returns>
        public List<GooglePlacesConfiguracionComboDTO> ObtenerCombo()
        {
            return _unitOfWork.GooglePlacesConfiguracionRepository
                .GetBy(w => w.Estado == true)
                .OrderBy(c => c.NombreSede)
                .Select(c => new GooglePlacesConfiguracionComboDTO
                {
                    Id = c.Id,
                    NombreSede = c.NombreSede,
                    IdentificadorCuenta = c.IdentificadorCuenta
                })
                .ToList();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de sede y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>GooglePlacesConfiguracion</returns>
        public GooglePlacesConfiguracion Add(GooglePlacesConfiguracion entidad)
        {
            var modelo = _unitOfWork.GooglePlacesConfiguracionRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GooglePlacesConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una configuración de sede y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>GooglePlacesConfiguracion</returns>
        public GooglePlacesConfiguracion Update(GooglePlacesConfiguracion entidad)
        {
            var modelo = _unitOfWork.GooglePlacesConfiguracionRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GooglePlacesConfiguracion>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una configuración de sede por su Id.</summary>
        /// <param name="id">Id de la configuración a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.GooglePlacesConfiguracionRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }
    }
}
