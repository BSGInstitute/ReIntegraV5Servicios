using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IGlassdoorResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de reseñas de empleador en Glassdoor.
    /// CRUD manual (sin API externa), consultas de administración vía SP y gestión de visibilidad.
    /// API pública de Glassdoor descontinuada en 2023 — captura manual periódica.
    /// </summary>
    public interface IGlassdoorResenaService
    {
        #region CRUD

        /// <summary>Inserta una reseña y persiste los cambios.</summary>
        GlassdoorResena Add(GlassdoorResena entidad);
        /// <summary>Actualiza una reseña y persiste los cambios.</summary>
        GlassdoorResena Update(GlassdoorResena entidad);
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        List<GlassdoorResena> Add(List<GlassdoorResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        List<GlassdoorResena> Update(List<GlassdoorResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        #region Consultas de Administración

        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_GlassdoorResenaObtenerDatos.</summary>
        GlassdoorResenaGrillaPaginadaDTO ObtenerGrilla(GlassdoorResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna los países con reseñas activas para el combo de filtros.</summary>
        List<GlassdoorResenaPaisComboDTO> ObtenerPaisesCombo();
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        List<GlassdoorResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais);
        /// <summary>Marca las reseñas indicadas como visibles (Mostrar=true).</summary>
        bool MarcarResenaVisible(GlassdoorResenaMarcarMostrarDTO dto);
        /// <summary>Marca las reseñas indicadas como ocultas (Mostrar=false).</summary>
        bool MarcarResenaOculta(GlassdoorResenaMarcarMostrarDTO dto);

        #endregion
    }
}
