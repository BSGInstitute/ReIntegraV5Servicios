using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: GlassdoorConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_GlassdoorConfiguracion.
    /// CRUD de la cuenta de empleador en Glassdoor (rating general, total evaluaciones).
    /// API pública descontinuada en 2023 — captura manual.
    /// </summary>
    public class GlassdoorConfiguracionRepository : GenericRepository<TGlassdoorConfiguracion>, IGlassdoorConfiguracionRepository
    {
        private readonly Mapper _mapper;

        public GlassdoorConfiguracionRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGlassdoorConfiguracion, GlassdoorConfiguracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<GlassdoorConfiguracion, TGlassdoorConfiguracion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TGlassdoorConfiguracion MapearAEntidad(GlassdoorConfiguracion entidad)
            => _mapper.Map<TGlassdoorConfiguracion>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de cuenta y retorna el modelo persistido.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>TGlassdoorConfiguracion</returns>
        public TGlassdoorConfiguracion Add(GlassdoorConfiguracion entidad)
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
        /// <summary>Actualiza una configuración de cuenta con control de concurrencia.</summary>
        /// <param name="entidad">Entidad de configuración con los datos actualizados.</param>
        /// <returns>TGlassdoorConfiguracion</returns>
        public TGlassdoorConfiguracion Update(GlassdoorConfiguracion entidad)
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
        /// <summary>Elimina lógicamente una configuración de cuenta.</summary>
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
