using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IGlassdoorResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de reseñas de empleador en Glassdoor.
    /// Combina EF Core (CRUD, combos, visibilidad) con Dapper (grilla vía SP).
    /// API pública descontinuada — captura manual periódica.
    /// </summary>
    public interface IGlassdoorResenaRepository : IGenericRepository<TGlassdoorResena>
    {
        #region CRUD

        /// <summary>Inserta una reseña y retorna el modelo persistido.</summary>
        TGlassdoorResena Add(GlassdoorResena entidad);
        /// <summary>Actualiza una reseña con control de concurrencia (RowVersion).</summary>
        TGlassdoorResena Update(GlassdoorResena entidad);
        /// <summary>Elimina lógicamente una reseña (Estado=false).</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        IEnumerable<TGlassdoorResena> Add(IEnumerable<GlassdoorResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque con control de concurrencia.</summary>
        IEnumerable<TGlassdoorResena> Update(IEnumerable<GlassdoorResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        #region Consultas

        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_GlassdoorResenaObtenerDatos.</summary>
        GlassdoorResenaGrillaPaginadaDTO ObtenerGrilla(GlassdoorResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna los países que tienen reseñas activas para el combo de filtros.</summary>
        List<GlassdoorResenaPaisComboDTO> ObtenerPaisesCombo();
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        List<GlassdoorResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais);

        #endregion

        #region Acciones de visibilidad

        /// <summary>Marca como visibles (Mostrar=true) las reseñas indicadas por Id.</summary>
        void MarcarResenaVisible(List<int> ids, string usuario);
        /// <summary>Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.</summary>
        void MarcarResenaOculta(List<int> ids, string usuario);

        #endregion
    }
}
