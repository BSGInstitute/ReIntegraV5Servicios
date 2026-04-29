using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using System;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: LinkedinConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_LinkedinConfiguracion.
    /// CRUD de páginas de LinkedIn (nombre, enlace, total opiniones).
    /// </summary>
    public class LinkedinConfiguracionRepository : GenericRepository<TLinkedinConfiguracion>, ILinkedinConfiguracionRepository
    {
        private readonly Mapper _mapper;

        public LinkedinConfiguracionRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLinkedinConfiguracion, LinkedinConfiguracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<LinkedinConfiguracion, TLinkedinConfiguracion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TLinkedinConfiguracion MapearAEntidad(LinkedinConfiguracion entidad)
            => _mapper.Map<TLinkedinConfiguracion>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una configuración de página y retorna el modelo persistido.</summary>
        /// <param name="entidad">Entidad de configuración a insertar.</param>
        /// <returns>Modelo persistido de la configuración insertada.</returns>
        public TLinkedinConfiguracion Add(LinkedinConfiguracion entidad)
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
        /// <returns>Modelo persistido de la configuración actualizada.</returns>
        public TLinkedinConfiguracion Update(LinkedinConfiguracion entidad)
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
        /// <returns>True si la eliminación fue exitosa.</returns>
        public bool Delete(int id, string usuario)
        {
            base.Delete(id, usuario);
            return true;
        }
    }
}
