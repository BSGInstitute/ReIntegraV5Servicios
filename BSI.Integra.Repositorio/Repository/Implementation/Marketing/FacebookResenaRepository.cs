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
    /// Repositorio: FacebookResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_FacebookResena.
    /// Combina EF Core (CRUD, combo, visibilidad) con Dapper (grilla y páginas vía SP).
    /// </summary>
    public class FacebookResenaRepository : GenericRepository<TFacebookResena>, IFacebookResenaRepository
    {
        private readonly Mapper _mapper;
        private readonly IntegraDBContext _integraContext;

        private const string SP_OBTENER_DATOS = "mkt.SP_FacebookResenaObtenerDatos";
        private const string MODO_GRILLA = "Grilla";
        private const string MODO_PAGINA = "Pagina";
        private const int TAMANO_PAGINA_DEFAULT = 50;
        private const int TAMANO_PAGINA_TODOS = 10000;

        public FacebookResenaRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            _integraContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFacebookResena, FacebookResena>(MemberList.None).ReverseMap();
                cfg.CreateMap<FacebookResena, FacebookResenaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<FacebookResena, TFacebookResena>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region CRUD

        private TFacebookResena MapearAEntidad(FacebookResena entidad)
            => _mapper.Map<TFacebookResena>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta una reseña y retorna el modelo persistido.
        /// </summary>
        /// <param name="entidad">Entidad de reseña a insertar.</param>
        /// <returns>Modelo persistido de la reseña insertada.</returns>
        public TFacebookResena Add(FacebookResena entidad)
        {
            var modelo = MapearAEntidad(entidad);
            base.Insert(modelo);
            return modelo;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una reseña existente con control de concurrencia (RowVersion).
        /// </summary>
        /// <param name="entidad">Entidad de reseña con los datos actualizados.</param>
        /// <returns>Modelo persistido de la reseña actualizada.</returns>
        public TFacebookResena Update(FacebookResena entidad)
        {
            var modelo = MapearAEntidad(entidad);
            modelo.RowVersion = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion }).RowVersion;
            base.Update(modelo);
            return modelo;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente una reseña (Estado=false).
        /// </summary>
        /// <param name="id">Id de la reseña a eliminar.</param>
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
        /// <summary>
        /// Inserta un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listadoEntidad">Listado de reseñas a insertar.</param>
        /// <returns>Listado de modelos persistidos de las reseñas insertadas.</returns>
        public IEnumerable<TFacebookResena> Add(IEnumerable<FacebookResena> listadoEntidad)
        {
            var listado = listadoEntidad.Select(MapearAEntidad).ToList();
            base.Insert(listado);
            return listado;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un listado de reseñas en bloque con control de concurrencia.
        /// </summary>
        /// <param name="listadoEntidad">Listado de reseñas con los datos actualizados.</param>
        /// <returns>Listado de modelos persistidos de las reseñas actualizadas.</returns>
        public IEnumerable<TFacebookResena> Update(IEnumerable<FacebookResena> listadoEntidad)
        {
            if (listadoEntidad == null) throw new ArgumentNullException(nameof(listadoEntidad));

            var listado = listadoEntidad.Select(MapearAEntidad).ToList();
            var rowVersions = base.GetBy(
                w => listadoEntidad.Select(s => s.Id).Contains(w.Id),
                s => new { s.Id, s.RowVersion });

            foreach (var item in listado)
                item.RowVersion = rowVersions.First(w => w.Id == item.Id).RowVersion;

            base.Update(listado);
            return listado;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente un listado de reseñas por sus Ids.
        /// </summary>
        /// <param name="listadoIds">Listado de Ids de las reseñas a eliminar.</param>
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
        /// <summary>
        /// Obtiene la configuración de una página de Facebook por su identificador.
        /// Ejecuta mkt.SP_TMessengerConfiguracionPagina_Obtener.
        /// </summary>
        /// <param name="identificadorPagina">Identificador de la página de Facebook (ej: "174599872598131").</param>
        /// <returns>Configuración de la página o un DTO vacío si no se encuentra.</returns>
        public FacebookConfiguracionPaginaDTO ObtenerFacebookConfiguracionPagina(string identificadorPagina)
        {
            try
            {
                var pagina = _integraContext.TFacebookConfiguracion
                    .Where(p => p.Estado == true && p.IdentificadorPagina == identificadorPagina)
                    .Select(p => new FacebookConfiguracionPaginaDTO
                    {
                        Id = p.Id,
                        IdentificadorPagina = p.IdentificadorPagina,
                        NombrePagina = p.Nombre,
                        TokenAccesoPagina = p.TokenAccesoPagina
                    })
                    .FirstOrDefault();

                return pagina ?? new FacebookConfiguracionPaginaDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"[FacebookResenaRepository.ObtenerFacebookConfiguracionPagina] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Busca una reseña activa por su IdentificadorHistoria (clave de deduplicación de la Graph API).
        /// </summary>
        /// <param name="identificadorHistoria">open_graph_story.id de Facebook.</param>
        /// <param name="idFacebookConfiguracion">Id de la página a la que pertenece.</param>
        /// <returns>La reseña encontrada o null si no existe.</returns>
        public FacebookResena ObtenerPorIdentificadorHistoria(string identificadorHistoria, int idFacebookConfiguracion)
        {
            try
            {
                var entidad = base.FirstBy(
                    w => w.IdentificadorHistoria == identificadorHistoria
                      && w.IdFacebookConfiguracion == idFacebookConfiguracion
                      && w.Estado == true);
                return entidad == null ? null : _mapper.Map<FacebookResena>(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception($"[FacebookResenaRepository.ObtenerPorIdentificadorHistoria] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las reseñas activas de una página. Usado por la sincronización
        /// para construir el diccionario de deduplicación.
        /// </summary>
        /// <param name="idFacebookConfiguracion">Id de la página de Facebook.</param>
        /// <returns>Listado de reseñas activas de la página.</returns>
        public List<FacebookResena> ObtenerResenasPorPagina(int idFacebookConfiguracion)
        {
            try
            {
                var entidades = base.GetBy(
                    w => w.IdFacebookConfiguracion == idFacebookConfiguracion
                      && w.Estado == true);
                return _mapper.Map<List<FacebookResena>>(entidades);
            }
            catch (Exception ex)
            {
                throw new Exception($"[FacebookResenaRepository.ObtenerResenasPorPagina] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna la grilla paginada de reseñas ejecutando mkt.SP_FacebookResenaObtenerDatos (modo Grilla).
        /// </summary>
        /// <param name="filtro">Filtros: páginas, visibilidad, rango de fechas, paginación.</param>
        /// <returns>Grilla paginada con las reseñas y metadatos de paginación.</returns>
        public FacebookResenaGrillaPaginadaDTO ObtenerGrilla(FacebookResenaGrillaFiltroDTO filtro)
        {
            try
            {
                var numeroPagina = filtro.Pagina < 1 ? 1 : filtro.Pagina;
                var tamanoPagina = NormalizarTamanoPagina(filtro.TamanoPagina);

                var idsPaginasCsv = filtro.IdsPaginasFacebook != null && filtro.IdsPaginasFacebook.Count > 0
                    ? string.Join(",", filtro.IdsPaginasFacebook)
                    : (string)null;

                var resultado = _dapperRepository.QuerySPDapper(
                    SP_OBTENER_DATOS,
                    new
                    {
                        Modo = MODO_GRILLA,
                        IdFacebookConfiguracion_Lista = idsPaginasCsv,
                        filtro.EsVisible,
                        FechaResena_Inicio = filtro.FechaInicio,
                        FechaResena_Fin = filtro.FechaFin,
                        NumeroPagina = numeroPagina,
                        TamanoPagina = tamanoPagina
                    });

                if (string.IsNullOrEmpty(resultado))
                    return new FacebookResenaGrillaPaginadaDTO { Pagina = numeroPagina, TamanoPagina = tamanoPagina };

                var items = JsonConvert.DeserializeObject<List<FacebookResenaGrillaItemConTotalDTO>>(resultado)
                            ?? new List<FacebookResenaGrillaItemConTotalDTO>();

                var totalRegistros = items.Count > 0 ? items[0].TotalRegistros : 0;

                return new FacebookResenaGrillaPaginadaDTO
                {
                    Data = items.Select(i => new FacebookResenaGrillaItemDTO
                    {
                        IdFacebookResena              = i.IdFacebookResena,
                        IdFacebookConfiguracion = i.IdFacebookConfiguracion,
                        NombrePagina                  = i.NombrePagina,
                        IdentificadorHistoria         = i.IdentificadorHistoria,
                        Recomienda                    = i.Recomienda,
                        TieneTexto                    = i.TieneTexto,
                        TextoResena                   = i.TextoResena,
                        FechaResena                   = i.FechaResena,
                        Mostrar                       = i.Mostrar,
                        FechaCreacion                 = i.FechaCreacion
                    }).ToList(),
                    TotalRegistros = totalRegistros,
                    Pagina         = numeroPagina,
                    TamanoPagina   = tamanoPagina,
                    TotalPaginas   = (int)Math.Ceiling((double)totalRegistros / tamanoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[FacebookResenaRepository.ObtenerGrilla] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna estadísticas agregadas por página ejecutando mkt.SP_FacebookResenaObtenerDatos (modo Pagina).
        /// </summary>
        /// <param name="idsPaginasConfiguradas">Identificadores de las páginas de Facebook a consultar.</param>
        /// <returns>Listado de estadísticas agregadas por página.</returns>
        public List<FacebookResenaPaginaItemDTO> ObtenerPaginas()
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper(
                    SP_OBTENER_DATOS,
                    new
                    {
                        Modo = MODO_PAGINA,
                        IdFacebookConfiguracion_Lista = (string)null
                    });

                if (string.IsNullOrEmpty(resultado))
                    return new List<FacebookResenaPaginaItemDTO>();

                return JsonConvert.DeserializeObject<List<FacebookResenaPaginaItemDTO>>(resultado)
                       ?? new List<FacebookResenaPaginaItemDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"[FacebookResenaRepository.ObtenerPaginas] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las cuentas de Facebook para el combo de filtros del frontend.
        /// Consulta directa a mkt.T_MessengerConfiguracionPagina vía EF Core.
        /// </summary>
        /// <param name="idsPaginasConfiguradas">Identificadores de las páginas de Facebook a consultar.</param>
        /// <returns>Listado de cuentas de Facebook para el combo de filtros.</returns>
        public List<FacebookResenaCuentaComboDTO> ObtenerCuentasCombo()
        {
            try
            {
                return _integraContext.TFacebookConfiguracion
                    .Where(p => p.Estado == true)
                    .OrderBy(p => p.Nombre)
                    .Select(p => new FacebookResenaCuentaComboDTO
                    {
                        Id = p.Id,
                        Nombre = p.Nombre,
                        IdentificadorPagina = p.IdentificadorPagina
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[FacebookResenaRepository.ObtenerCuentasCombo] {ex.Message}", ex);
            }
        }

        #endregion

        #region Acciones de visibilidad

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca como visibles (Mostrar=true) las reseñas indicadas por Id.
        /// </summary>
        /// <param name="ids">Listado de Ids de las reseñas a marcar como visibles.</param>
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
                throw new Exception($"[FacebookResenaRepository.MarcarResenaVisible] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.
        /// </summary>
        /// <param name="ids">Listado de Ids de las reseñas a marcar como ocultas.</param>
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
                throw new Exception($"[FacebookResenaRepository.MarcarResenaOculta] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza el campo Mostrar en bloque para los Ids indicados.</summary>
        /// <param name="ids">Listado de Ids de las reseñas a actualizar.</param>
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
        private class FacebookResenaGrillaItemConTotalDTO : FacebookResenaGrillaItemDTO
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
