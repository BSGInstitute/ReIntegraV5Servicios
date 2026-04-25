using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IGoogleResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de reseñas de Google Places.
    /// Combina EF Core (CRUD, combo, visibilidad) con Dapper (grilla y sedes vía SP).
    /// </summary>
    public interface IGoogleResenaRepository : IGenericRepository<TGoogleResena>
    {
        #region CRUD

        /// <summary>Inserta una reseña y retorna el modelo persistido.</summary>
        TGoogleResena Add(GoogleResena entidad);
        /// <summary>Actualiza una reseña con control de concurrencia (RowVersion).</summary>
        TGoogleResena Update(GoogleResena entidad);
        /// <summary>Elimina lógicamente una reseña (Estado=false).</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        IEnumerable<TGoogleResena> Add(IEnumerable<GoogleResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque con control de concurrencia.</summary>
        IEnumerable<TGoogleResena> Update(IEnumerable<GoogleResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        #region Consultas

        /// <summary>Busca una reseña activa por su IdentificadorResena y sede.</summary>
        GoogleResena ObtenerPorIdentificadorResena(string identificadorResena, int idGooglePlacesConfiguracion);
        /// <summary>Obtiene todas las reseñas activas de una sede (para deduplicación en sincronización).</summary>
        List<GoogleResena> ObtenerResenasPorSede(int idGooglePlacesConfiguracion);
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_GoogleResenaObtenerDatos (modo Grilla).</summary>
        GoogleResenaGrillaPaginadaDTO ObtenerGrilla(GoogleResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna estadísticas agregadas por sede ejecutando mkt.SP_GoogleResenaObtenerDatos (modo Sede).</summary>
        List<GoogleResenaSedeItemDTO> ObtenerSedes();
        /// <summary>Retorna las sedes para el combo de filtros vía EF Core.</summary>
        List<GoogleResenaSedeComboDTO> ObtenerSedesCombo();

        #endregion

        #region Acciones de visibilidad

        /// <summary>Marca como visibles (Mostrar=true) las reseñas indicadas por Id.</summary>
        void MarcarResenaVisible(List<int> ids, string usuario);
        /// <summary>Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.</summary>
        void MarcarResenaOculta(List<int> ids, string usuario);

        #endregion
    }
}
