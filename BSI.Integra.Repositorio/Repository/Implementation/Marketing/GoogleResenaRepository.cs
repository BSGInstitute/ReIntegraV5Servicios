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
    /// Repositorio: GoogleResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_GoogleResena.
    /// Combina EF Core (CRUD, combo, visibilidad) con Dapper (grilla y sedes vía SP).
    /// </summary>
    public class GoogleResenaRepository : GenericRepository<TGoogleResena>, IGoogleResenaRepository
    {
        private readonly Mapper _mapper;
        private readonly IntegraDBContext _integraContext;

        private const string SP_OBTENER_DATOS = "mkt.SP_GoogleResenaObtenerDatos";
        private const string MODO_GRILLA = "Grilla";
        private const string MODO_SEDE = "Sede";
        private const int TAMANO_PAGINA_DEFAULT = 50;
        private const int TAMANO_PAGINA_TODOS = 10000;

        public GoogleResenaRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            _integraContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGoogleResena, GoogleResena>(MemberList.None).ReverseMap();
                cfg.CreateMap<GoogleResena, GoogleResenaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<GoogleResena, TGoogleResena>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region CRUD

        private TGoogleResena MapearAEntidad(GoogleResena entidad)
            => _mapper.Map<TGoogleResena>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta una reseña y retorna el modelo persistido.
        /// </summary>
        /// <param name="entidad">Entidad de reseña a insertar.</param>
        /// <returns>Modelo persistido de la reseña insertada.</returns>
        public TGoogleResena Add(GoogleResena entidad)
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
        public TGoogleResena Update(GoogleResena entidad)
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
        public IEnumerable<TGoogleResena> Add(IEnumerable<GoogleResena> listadoEntidad)
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
        public IEnumerable<TGoogleResena> Update(IEnumerable<GoogleResena> listadoEntidad)
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
        /// Busca una reseña activa por su IdentificadorResena (clave de deduplicación de la API).
        /// </summary>
        /// <param name="identificadorResena">Identificador único de la reseña en Google (places/xxx/reviews/xxx).</param>
        /// <param name="idGooglePlacesConfiguracion">Id de la sede a la que pertenece.</param>
        /// <returns>La reseña encontrada o null si no existe.</returns>
        public GoogleResena ObtenerPorIdentificadorResena(string identificadorResena, int idGooglePlacesConfiguracion)
        {
            try
            {
                var entidad = base.FirstBy(
                    w => w.IdentificadorResena == identificadorResena
                      && w.IdGooglePlacesConfiguracion == idGooglePlacesConfiguracion
                      && w.Estado == true);
                return entidad == null ? null : _mapper.Map<GoogleResena>(entidad);
            }
            catch (Exception ex)
            {
                throw new Exception($"[GoogleResenaRepository.ObtenerPorIdentificadorResena] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las reseñas activas de una sede. Usado por la sincronización
        /// para construir el diccionario de deduplicación.
        /// </summary>
        /// <param name="idGooglePlacesConfiguracion">Id de la sede de Google Places.</param>
        /// <returns>Listado de reseñas activas de la sede.</returns>
        public List<GoogleResena> ObtenerResenasPorSede(int idGooglePlacesConfiguracion)
        {
            try
            {
                var entidades = base.GetBy(
                    w => w.IdGooglePlacesConfiguracion == idGooglePlacesConfiguracion
                      && w.Estado == true);
                return _mapper.Map<List<GoogleResena>>(entidades);
            }
            catch (Exception ex)
            {
                throw new Exception($"[GoogleResenaRepository.ObtenerResenasPorSede] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna la grilla paginada de reseñas ejecutando mkt.SP_GoogleResenaObtenerDatos (modo Grilla).
        /// </summary>
        /// <param name="filtro">Filtros: sedes, visibilidad, rating (1-5), rango de fechas, paginación.</param>
        /// <returns>Grilla paginada con las reseñas y metadatos de paginación.</returns>
        public GoogleResenaGrillaPaginadaDTO ObtenerGrilla(GoogleResenaGrillaFiltroDTO filtro)
        {
            try
            {
                var numeroPagina = filtro.Pagina < 1 ? 1 : filtro.Pagina;
                var tamanoPagina = NormalizarTamanoPagina(filtro.TamanoPagina);

                var idsSedeCsv = filtro.IdsSede != null && filtro.IdsSede.Count > 0
                    ? string.Join(",", filtro.IdsSede)
                    : (string)null;

                var resultado = _dapperRepository.QuerySPDapper(
                    SP_OBTENER_DATOS,
                    new
                    {
                        Modo = MODO_GRILLA,
                        IdGooglePlacesConfiguracion_Lista = idsSedeCsv,
                        filtro.EsVisible,
                        Rating = filtro.Valoracion,
                        FechaResena_Inicio = filtro.FechaInicio,
                        FechaResena_Fin = filtro.FechaFin,
                        NumeroPagina = numeroPagina,
                        TamanoPagina = tamanoPagina
                    });

                if (string.IsNullOrEmpty(resultado))
                    return new GoogleResenaGrillaPaginadaDTO { Pagina = numeroPagina, TamanoPagina = tamanoPagina };

                var items = JsonConvert.DeserializeObject<List<GoogleResenaGrillaItemConTotalDTO>>(resultado)
                            ?? new List<GoogleResenaGrillaItemConTotalDTO>();

                var totalRegistros = items.Count > 0 ? items[0].TotalRegistros : 0;

                return new GoogleResenaGrillaPaginadaDTO
                {
                    Data = items.Select(i => new GoogleResenaGrillaItemDTO
                    {
                        IdGoogleResena              = i.IdGoogleResena,
                        IdGooglePlacesConfiguracion = i.IdGooglePlacesConfiguracion,
                        NombreSede                  = i.NombreSede,
                        NombreAutor                 = i.NombreAutor,
                        FotoAutor                   = i.FotoAutor,
                        Valoracion                  = i.Valoracion,
                        TextoResena                 = i.TextoResena,
                        FechaResena                 = i.FechaResena,
                        DescripcionTiempoRelativo   = i.DescripcionTiempoRelativo,
                        Mostrar                     = i.Mostrar,
                        FechaCreacion               = i.FechaCreacion
                    }).ToList(),
                    TotalRegistros = totalRegistros,
                    Pagina         = numeroPagina,
                    TamanoPagina   = tamanoPagina,
                    TotalPaginas   = (int)Math.Ceiling((double)totalRegistros / tamanoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[GoogleResenaRepository.ObtenerGrilla] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna estadísticas agregadas por sede ejecutando mkt.SP_GoogleResenaObtenerDatos (modo Sede).
        /// </summary>
        /// <returns>Listado de estadísticas agregadas por sede.</returns>
        public List<GoogleResenaSedeItemDTO> ObtenerSedes()
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper(
                    SP_OBTENER_DATOS,
                    new { Modo = MODO_SEDE });

                if (string.IsNullOrEmpty(resultado))
                    return new List<GoogleResenaSedeItemDTO>();

                return JsonConvert.DeserializeObject<List<GoogleResenaSedeItemDTO>>(resultado)
                       ?? new List<GoogleResenaSedeItemDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"[GoogleResenaRepository.ObtenerSedes] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las sedes de Google Places para el combo de filtros del frontend.
        /// Consulta directa a mkt.T_GooglePlacesConfiguracion vía EF Core.
        /// </summary>
        /// <returns>Listado de sedes para el combo de filtros.</returns>
        public List<GoogleResenaSedeComboDTO> ObtenerSedesCombo()
        {
            try
            {
                return _integraContext.TGooglePlacesConfiguracion
                    .Where(c => c.Estado == true)
                    .OrderBy(c => c.NombreSede)
                    .Select(c => new GoogleResenaSedeComboDTO
                    {
                        IdGooglePlacesConfiguracion = c.Id,
                        NombreSede = c.NombreSede
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[GoogleResenaRepository.ObtenerSedesCombo] {ex.Message}", ex);
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
                throw new Exception($"[GoogleResenaRepository.MarcarResenaVisible] {ex.Message}", ex);
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
                throw new Exception($"[GoogleResenaRepository.MarcarResenaOculta] {ex.Message}", ex);
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
        private class GoogleResenaGrillaItemConTotalDTO : GoogleResenaGrillaItemDTO
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
