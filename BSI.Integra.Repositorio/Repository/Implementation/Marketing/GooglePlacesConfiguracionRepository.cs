using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: GooglePlacesConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_GooglePlacesConfiguracion.
    /// CRUD de sedes de Google Places (nombre, identificador, valoración, total reseñas).
    /// </summary>
    public class GooglePlacesConfiguracionRepository : GenericRepository<TGooglePlacesConfiguracion>, IGooglePlacesConfiguracionRepository
    {
        private readonly Mapper _mapper;

        public GooglePlacesConfiguracionRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGooglePlacesConfiguracion, GooglePlacesConfiguracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<GooglePlacesConfiguracion, TGooglePlacesConfiguracion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TGooglePlacesConfiguracion MapearAEntidad(GooglePlacesConfiguracion entidad)
            => _mapper.Map<TGooglePlacesConfiguracion>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de sede y retorna el modelo persistido.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>Modelo persistido de la configuración insertada.</returns>
        public TGooglePlacesConfiguracion Add(GooglePlacesConfiguracion entidad)
        {
            var modelo = MapearAEntidad(entidad);
            var ahora = DateTime.UtcNow.AddHours(-5);
            modelo.Estado = true;
            modelo.FechaCreacion = ahora;
            modelo.FechaModificacion = ahora;
            base.Insert(modelo);
            return modelo;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una configuración de sede con control de concurrencia.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>Modelo persistido de la configuración actualizada.</returns>
        public TGooglePlacesConfiguracion Update(GooglePlacesConfiguracion entidad)
        {
            var modelo = MapearAEntidad(entidad);
            var existente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion, s.FechaCreacion, s.UsuarioCreacion, s.Estado });
            modelo.RowVersion = existente.RowVersion;
            modelo.Estado = existente.Estado;
            modelo.FechaCreacion = existente.FechaCreacion;
            modelo.UsuarioCreacion = existente.UsuarioCreacion;
            modelo.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            base.Update(modelo);
            return modelo;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una configuración de sede.</summary>
        /// <param name="id">Id de la configuración a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>True si la eliminación fue exitosa.</returns>
        public bool Delete(int id, string usuario)
        {
            base.Delete(id, usuario);
            return true;
        }
    }
}
