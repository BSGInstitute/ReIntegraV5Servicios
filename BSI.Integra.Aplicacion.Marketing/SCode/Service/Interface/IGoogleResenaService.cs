using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IGoogleResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de reseñas de Google Places
    /// (CRUD, consultas de administración vía SP y sincronización con Google API).
    /// </summary>
    public interface IGoogleResenaService
    {
        #region CRUD

        /// <summary>Inserta una reseña y persiste los cambios.</summary>
        GoogleResena Add(GoogleResena entidad);
        /// <summary>Actualiza una reseña y persiste los cambios.</summary>
        GoogleResena Update(GoogleResena entidad);
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        List<GoogleResena> Add(List<GoogleResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        List<GoogleResena> Update(List<GoogleResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        #region Consultas de Administración

        /// <summary>Retorna la grilla paginada de reseñas ejecutando mkt.SP_GoogleResenaObtenerDatos (modo Grilla).</summary>
        GoogleResenaGrillaPaginadaDTO ObtenerGrilla(GoogleResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna las sedes configuradas con estadísticas agregadas.</summary>
        List<GoogleResenaSedeItemDTO> ObtenerSedes();
        /// <summary>Retorna las sedes de Google Places para el combo de filtros del frontend.</summary>
        List<GoogleResenaSedeComboDTO> ObtenerSedesCombo();
        /// <summary>Marca las reseñas indicadas como visibles (Mostrar=true).</summary>
        bool MarcarResenaVisible(GoogleResenaMarcarMostrarDTO dto);
        /// <summary>Marca las reseñas indicadas como ocultas (Mostrar=false).</summary>
        bool MarcarResenaOculta(GoogleResenaMarcarMostrarDTO dto);

        #endregion

        #region Sincronización

        /// <summary>Sincroniza las reseñas desde la Google Places API para todas las sedes configuradas.</summary>
        Task<string> SincronizarGoogleApi(string nombreUsuario);

        #endregion
    }
}
