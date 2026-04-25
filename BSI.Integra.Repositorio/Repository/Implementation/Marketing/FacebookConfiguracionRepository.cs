using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: FacebookConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_FacebookConfiguracion.
    /// CRUD de páginas de Facebook (identificador, nombre, token, opiniones, valoración).
    /// </summary>
    public class FacebookConfiguracionRepository : GenericRepository<TFacebookConfiguracion>, IFacebookConfiguracionRepository
    {
        private readonly Mapper _mapper;

        public FacebookConfiguracionRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookConfiguracion, FacebookConfiguracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<FacebookConfiguracion, TFacebookConfiguracion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TFacebookConfiguracion MapearAEntidad(FacebookConfiguracion entidad)
            => _mapper.Map<TFacebookConfiguracion>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de página y retorna el modelo persistido.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>TFacebookConfiguracion</returns>
        public TFacebookConfiguracion Add(FacebookConfiguracion entidad)
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
        /// <summary>Actualiza una configuración de página con control de concurrencia.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>TFacebookConfiguracion</returns>
        public TFacebookConfiguracion Update(FacebookConfiguracion entidad)
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
        /// <summary>Elimina lógicamente una configuración de página.</summary>
        /// <param name="id">Id de la configuración a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            base.Delete(id, usuario);
            return true;
        }
    }
}
