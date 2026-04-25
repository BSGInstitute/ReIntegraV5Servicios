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
    /// Repositorio: ComputrabajoResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Acceso a datos de la tabla mkt.T_ComputrabajoResena.
    /// Combina EF Core (CRUD, combos, visibilidad) con Dapper (grilla vía SP).
    /// Computrabajo NO tiene API pública — captura manual periódica (quincenal).
    /// </summary>
    public class ComputrabajoResenaRepository : GenericRepository<TComputrabajoResena>, IComputrabajoResenaRepository
    {
        private readonly Mapper _mapper;
        private readonly IntegraDBContext _integraContext;

        private const string SP_OBTENER_DATOS = "mkt.SP_ComputrabajoResenaObtenerDatos";
        private const int TAMANO_PAGINA_DEFAULT = 50;
        private const int TAMANO_PAGINA_TODOS = 10000;

        public ComputrabajoResenaRepository(
            IntegraDBContext context,
            IConnectionFactory connectionFactory,
            IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            _integraContext = context;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TComputrabajoResena, ComputrabajoResena>(MemberList.None).ReverseMap();
                cfg.CreateMap<ComputrabajoResena, ComputrabajoResenaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ComputrabajoResena, TComputrabajoResena>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region CRUD

        private TComputrabajoResena MapearAEntidad(ComputrabajoResena entidad)
            => _mapper.Map<TComputrabajoResena>(entidad);

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una reseña y retorna el modelo persistido.</summary>
        /// <param name="entidad">Entidad de reseña a insertar.</param>
        /// <returns>TComputrabajoResena</returns>
        public TComputrabajoResena Add(ComputrabajoResena entidad)
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
        /// <summary>Actualiza una reseña con control de concurrencia (RowVersion).</summary>
        /// <param name="entidad">Entidad de reseña con los datos actualizados.</param>
        /// <returns>TComputrabajoResena</returns>
        public TComputrabajoResena Update(ComputrabajoResena entidad)
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
        /// <summary>Elimina lógicamente una reseña (Estado=false).</summary>
        /// <param name="id">Id de la reseña a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            base.Delete(id, usuario);
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        /// <param name="listadoEntidad">Listado de reseñas a insertar.</param>
        /// <returns>IEnumerable de TComputrabajoResena</returns>
        public IEnumerable<TComputrabajoResena> Add(IEnumerable<ComputrabajoResena> listadoEntidad)
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
        /// <summary>Actualiza un listado de reseñas en bloque con control de concurrencia.</summary>
        /// <param name="listadoEntidad">Listado de reseñas con los datos actualizados.</param>
        /// <returns>IEnumerable de TComputrabajoResena</returns>
        public IEnumerable<TComputrabajoResena> Update(IEnumerable<ComputrabajoResena> listadoEntidad)
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
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        /// <param name="listadoIds">Listado de Ids de las reseñas a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
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
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_ComputrabajoResenaObtenerDatos.</summary>
        /// <param name="filtro">Filtros de la grilla (visibilidad, país, tipo empleado, fechas, paginación).</param>
        /// <returns>ComputrabajoResenaGrillaPaginadaDTO</returns>
        public ComputrabajoResenaGrillaPaginadaDTO ObtenerGrilla(ComputrabajoResenaGrillaFiltroDTO filtro)
        {
            try
            {
                var numeroPagina = filtro.Pagina < 1 ? 1 : filtro.Pagina;
                var tamanoPagina = NormalizarTamanoPagina(filtro.TamanoPagina);

                var idsPaisStr = filtro.IdPaisLista != null && filtro.IdPaisLista.Any()
                    ? string.Join(",", filtro.IdPaisLista)
                    : (string)null;

                var tipoEmpleadoParaBD = string.IsNullOrEmpty(filtro.TipoEmpleado)
                    ? null
                    : filtro.TipoEmpleado;
                var resultado = _dapperRepository.QuerySPDapper(
                    SP_OBTENER_DATOS,
                    new
                    {
                        filtro.Mostrar,
                        IdPais_Lista = idsPaisStr,
                        TipoEmpleado = tipoEmpleadoParaBD,
                        FechaResena_Inicio = filtro.FechaInicio,
                        FechaResena_Fin = filtro.FechaFin,
                        NumeroPagina = numeroPagina,
                        TamanoPagina = tamanoPagina
                    });

                if (string.IsNullOrEmpty(resultado))
                    return new ComputrabajoResenaGrillaPaginadaDTO { Pagina = numeroPagina, TamanoPagina = tamanoPagina };

                var items = JsonConvert.DeserializeObject<List<ComputrabajoResenaGrillaItemConTotalDTO>>(resultado)
                            ?? new List<ComputrabajoResenaGrillaItemConTotalDTO>();

                var totalRegistros = items.Count > 0 ? items[0].TotalRegistros : 0;

                return new ComputrabajoResenaGrillaPaginadaDTO
                {
                    Data = items.Select(i => new ComputrabajoResenaGrillaItemDTO
                    {
                        IdComputrabajoResena     = i.IdComputrabajoResena,
                        IdComputrabajoConfiguracion = i.IdComputrabajoConfiguracion,
                        Contenido                = i.Contenido,
                        Valoracion               = i.Valoracion,
                        Cargo                    = i.Cargo,
                        TipoEmpleado             = i.TipoEmpleado,
                        Ventaja                  = i.Ventaja,
                        Desventaja               = i.Desventaja,
                        IdPais                   = i.IdPais,
                        NombrePais               = i.NombrePais,
                        RutaBandera              = i.RutaBandera,
                        IdCiudad                 = i.IdCiudad,
                        NombreCiudad             = i.NombreCiudad,
                        FechaResena              = i.FechaResena,
                        Mostrar                  = i.Mostrar,
                        FechaCreacion            = i.FechaCreacion
                    }).ToList(),
                    TotalRegistros = totalRegistros,
                    Pagina         = numeroPagina,
                    TamanoPagina   = tamanoPagina,
                    TotalPaginas   = (int)Math.Ceiling((double)totalRegistros / tamanoPagina)
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[ComputrabajoResenaRepository.ObtenerGrilla] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna los países con reseñas activas para el combo de filtros vía EF Core.</summary>
        /// <returns>List de ComputrabajoResenaPaisComboDTO</returns>
        public List<ComputrabajoResenaPaisComboDTO> ObtenerPaisesCombo()
        {
            try
            {
                return _integraContext.TPais
                    .Where(p => p.Estado == true)
                    .OrderBy(p => p.NombrePais)
                    .Select(p => new ComputrabajoResenaPaisComboDTO
                    {
                        IdPais = p.Id,
                        NombrePais = p.NombrePais,
                        RutaBandera = p.RutaBandera
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[ComputrabajoResenaRepository.ObtenerPaisesCombo] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna las ciudades de un país para el combo de filtros vía EF Core.</summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>List de ComputrabajoResenaCiudadComboDTO</returns>
        public List<ComputrabajoResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais)
        {
            try
            {
                return _integraContext.TCiudads
                    .Where(c => c.Estado == true && c.IdPais == idPais)
                    .OrderBy(c => c.Nombre)
                    .Select(c => new ComputrabajoResenaCiudadComboDTO
                    {
                        IdCiudad = c.Id,
                        NombreCiudad = c.Nombre
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[ComputrabajoResenaRepository.ObtenerCiudadesCombo] {ex.Message}", ex);
            }
        }

        #endregion

        #region Acciones de visibilidad

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como visibles (Mostrar=true) las reseñas indicadas por Id.</summary>
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
                throw new Exception($"[ComputrabajoResenaRepository.MarcarResenaVisible] {ex.Message}", ex);
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.</summary>
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
                throw new Exception($"[ComputrabajoResenaRepository.MarcarResenaOculta] {ex.Message}", ex);
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
        private class ComputrabajoResenaGrillaItemConTotalDTO : ComputrabajoResenaGrillaItemDTO
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
