using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: LinkedinResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_LinkedinResena.
    /// Combina EF Core (CRUD, combos, visibilidad) con Dapper (grilla vía SP).
    /// </summary>
    public class LinkedinResenaRepository : GenericRepository<TLinkedinResena>, ILinkedinResenaRepository
    {
        private readonly Mapper _mapper;
        private readonly IntegraDBContext _integraContext;

        private const string SP_OBTENER_DATOS = "mkt.SP_LinkedinResenaObtenerDatos";
        private const int TAMANO_PAGINA_DEFAULT = 50;
        private const int TAMANO_PAGINA_TODOS = 10000;

        public LinkedinResenaRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            _integraContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLinkedinResena, LinkedinResena>(MemberList.None).ReverseMap();
                cfg.CreateMap<LinkedinResena, LinkedinResenaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<LinkedinResena, TLinkedinResena>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region CRUD

        private TLinkedinResena MapearAEntidad(LinkedinResena entidad)
            => _mapper.Map<TLinkedinResena>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un testimonio y retorna el modelo persistido.</summary>
        /// <param name="entidad">Entidad de testimonio a insertar.</param>
        /// <returns>Modelo persistido del testimonio insertado.</returns>
        public TLinkedinResena Add(LinkedinResena entidad)
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
        /// <summary>Actualiza un testimonio existente con control de concurrencia (RowVersion).</summary>
        /// <param name="entidad">Entidad de testimonio con los datos actualizados.</param>
        /// <returns>Modelo persistido del testimonio actualizado.</returns>
        public TLinkedinResena Update(LinkedinResena entidad)
        {
            var modelo = MapearAEntidad(entidad);
            var existente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion, s.FechaCreacion, s.UsuarioCreacion, s.Estado, s.Mostrar });
            modelo.RowVersion = existente.RowVersion;
            modelo.Estado = existente.Estado;
            modelo.FechaCreacion = existente.FechaCreacion;
            modelo.UsuarioCreacion = existente.UsuarioCreacion;
            modelo.Mostrar = existente.Mostrar;
            modelo.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            base.Update(modelo);
            return modelo;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un testimonio (Estado=false).</summary>
        /// <param name="id">Id del testimonio a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>True si la eliminación fue exitosa.</returns>
        public bool Delete(int id, string usuario)
        {
            base.Delete(id, usuario);
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un listado de testimonios en bloque.</summary>
        /// <param name="listadoEntidad">Listado de testimonios a insertar.</param>
        /// <returns>Listado de modelos persistidos.</returns>
        public IEnumerable<TLinkedinResena> Add(IEnumerable<LinkedinResena> listadoEntidad)
        {
            var ahora = DateTime.UtcNow.AddHours(-5);
            var listado = listadoEntidad.Select(e =>
            {
                var modelo = MapearAEntidad(e);
                modelo.Estado = true;
                modelo.FechaCreacion = ahora;
                modelo.FechaModificacion = ahora;
                return modelo;
            }).ToList();
            base.Insert(listado);
            return listado;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un listado de testimonios en bloque con control de concurrencia.</summary>
        /// <param name="listadoEntidad">Listado de testimonios con los datos actualizados.</param>
        /// <returns>Listado de modelos persistidos.</returns>
        public IEnumerable<TLinkedinResena> Update(IEnumerable<LinkedinResena> listadoEntidad)
        {
            if (listadoEntidad == null) throw new ArgumentNullException(nameof(listadoEntidad));

            var ahora = DateTime.UtcNow.AddHours(-5);
            var listado = listadoEntidad.Select(MapearAEntidad).ToList();
            var existentes = base.GetBy(
                w => listadoEntidad.Select(s => s.Id).Contains(w.Id),
                s => new { s.Id, s.RowVersion, s.FechaCreacion, s.UsuarioCreacion, s.Estado, s.Mostrar });

            foreach (var item in listado)
            {
                var existente = existentes.First(w => w.Id == item.Id);
                item.RowVersion = existente.RowVersion;
                item.Estado = existente.Estado;
                item.FechaCreacion = existente.FechaCreacion;
                item.UsuarioCreacion = existente.UsuarioCreacion;
                item.Mostrar = existente.Mostrar;
                item.FechaModificacion = ahora;
            }

            base.Update(listado);
            return listado;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un listado de testimonios por sus Ids.</summary>
        /// <param name="listadoIds">Listado de Ids a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>True si la eliminación fue exitosa.</returns>
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            base.Delete(listadoIds, usuario);
            return true;
        }

        #endregion

        #region Consultas

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_LinkedinResenaObtenerDatos (modo Grilla).</summary>
        /// <param name="filtro">Filtros: visibilidad, país, rango de fechas, paginación.</param>
        /// <returns>Grilla paginada con los testimonios y metadatos de paginación.</returns>
        public LinkedinResenaGrillaPaginadaDTO ObtenerGrilla(LinkedinResenaGrillaFiltroDTO filtro)
        {
            try
            {
                var numeroPagina = filtro.Pagina < 1 ? 1 : filtro.Pagina;
                var tamanoPagina = NormalizarTamanoPagina(filtro.TamanoPagina);

                var idsPaisStr = filtro.IdsPais != null && filtro.IdsPais.Any()
                    ? string.Join(",", filtro.IdsPais)
                    : (string)null;

                var resultado = _dapperRepository.QuerySPDapper(
                    SP_OBTENER_DATOS,
                    new
                    {
                        filtro.EsVisible,
                        IdPais_Lista = idsPaisStr,
                        FechaResena_Inicio = filtro.FechaInicio,
                        FechaResena_Fin = filtro.FechaFin,
                        NumeroPagina = numeroPagina,
                        TamanoPagina = tamanoPagina
                    });

                if (string.IsNullOrEmpty(resultado))
                    return new LinkedinResenaGrillaPaginadaDTO { Pagina = numeroPagina, TamanoPagina = tamanoPagina };

                var items = JsonConvert.DeserializeObject<List<LinkedinResenaGrillaItemConTotalDTO>>(resultado)
                            ?? new List<LinkedinResenaGrillaItemConTotalDTO>();

                var totalRegistros = items.Count > 0 ? items[0].TotalRegistros : 0;

                return new LinkedinResenaGrillaPaginadaDTO
                {
                    Data = items.Select(i => new LinkedinResenaGrillaItemDTO
                    {
                        IdLinkedinResena    = i.IdLinkedinResena,
                        NombreAutor         = i.NombreAutor,
                        Cargo               = i.Cargo,
                        Empresa             = i.Empresa,
                        FotoAutor           = i.FotoAutor,
                        UrlPublicacion      = i.UrlPublicacion,
                        TextoResena         = i.TextoResena,
                        Certificacion       = i.Certificacion,
                        IdPais              = i.IdPais,
                        NombrePais          = i.NombrePais,
                        RutaBandera         = i.RutaBandera,
                        IdCiudad            = i.IdCiudad,
                        NombreCiudad        = i.NombreCiudad,
                        FechaResena         = i.FechaResena,
                        Mostrar             = i.Mostrar,
                        FechaCreacion       = i.FechaCreacion
                    }).ToList(),
                    TotalRegistros = totalRegistros,
                    Pagina         = numeroPagina,
                    TamanoPagina   = tamanoPagina,
                    TotalPaginas   = (int)Math.Ceiling((double)totalRegistros / tamanoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[LinkedinResenaRepository.ObtenerGrilla] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna los países con testimonios activos para el combo de filtros vía EF Core.</summary>
        /// <returns>Listado de países para el combo.</returns>
        public List<LinkedinResenaPaisComboDTO> ObtenerPaisesCombo()
        {
            try
            {
                return _integraContext.TPais
                    .Where(p => p.Estado == true)
                    .OrderBy(p => p.NombrePais)
                    .Select(p => new LinkedinResenaPaisComboDTO
                    {
                        IdPais = p.Id,
                        NombrePais = p.NombrePais,
                        RutaBandera = p.RutaBandera
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[LinkedinResenaRepository.ObtenerPaisesCombo] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna las ciudades de un país para el combo de filtros vía EF Core.</summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>Listado de ciudades del país.</returns>
        public List<LinkedinResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais)
        {
            try
            {
                return _integraContext.TCiudads
                    .Where(c => c.Estado == true && c.IdPais == idPais)
                    .OrderBy(c => c.Nombre)
                    .Select(c => new LinkedinResenaCiudadComboDTO
                    {
                        IdCiudad = c.Id,
                        NombreCiudad = c.Nombre
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[LinkedinResenaRepository.ObtenerCiudadesCombo] {ex.Message}", ex);
            }
        }

        #endregion

        #region Acciones de visibilidad

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como visibles (Mostrar=true) los testimonios indicados por Id.</summary>
        /// <param name="ids">Listado de Ids de los testimonios a marcar como visibles.</param>
        /// <param name="usuario">Usuario que realiza la acción.</param>
        public void MarcarResenaVisible(List<int> ids, string usuario)
        {
            try
            {
                if (ids == null || !ids.Any()) return;
                ActualizarVisibilidadMasivo(ids, true, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"[LinkedinResenaRepository.MarcarResenaVisible] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como ocultos (Mostrar=false) los testimonios indicados por Id.</summary>
        /// <param name="ids">Listado de Ids de los testimonios a marcar como ocultos.</param>
        /// <param name="usuario">Usuario que realiza la acción.</param>
        public void MarcarResenaOculta(List<int> ids, string usuario)
        {
            try
            {
                if (ids == null || !ids.Any()) return;
                ActualizarVisibilidadMasivo(ids, false, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"[LinkedinResenaRepository.MarcarResenaOculta] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza el campo Mostrar en bloque para los Ids indicados.</summary>
        /// <param name="ids">Listado de Ids de los testimonios a actualizar.</param>
        /// <param name="mostrar">Valor de visibilidad a asignar.</param>
        /// <param name="usuario">Usuario que realiza la acción.</param>
        private void ActualizarVisibilidadMasivo(List<int> ids, bool mostrar, string usuario)
        {
            var entidades = base.GetBy(w => ids.Contains(w.Id) && w.Estado == true && w.Mostrar != mostrar);

            foreach (var entidad in entidades)
            {
                entidad.Mostrar = mostrar;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.UtcNow.AddHours(-5);
            }

            base.Update(entidades);
        }

        #endregion

        #region Helpers privados

        /// <summary>DTO interno para deserializar el resultado del SP incluyendo TotalRegistros.</summary>
        private class LinkedinResenaGrillaItemConTotalDTO : LinkedinResenaGrillaItemDTO
        {
            public int TotalRegistros { get; set; }
        }

        /// <summary>Normaliza el tamaño de página: 0 = todos, negativo = default, positivo = tal cual.</summary>
        /// <param name="tamanoPagina">Tamaño de página solicitado.</param>
        /// <returns>int</returns>
        private static int NormalizarTamanoPagina(int tamanoPagina)
        {
            if (tamanoPagina == 0) return TAMANO_PAGINA_TODOS;
            if (tamanoPagina < 0)  return TAMANO_PAGINA_DEFAULT;
            return tamanoPagina;
        }

        #endregion
    }
}
