using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IFacebookResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de reseñas de Facebook
    /// (CRUD, consultas de administración y sincronización con Graph API).
    /// </summary>
    public interface IFacebookResenaService
    {
        #region CRUD

        /// <summary>Inserta una reseña y persiste los cambios.</summary>
        FacebookResena Add(FacebookResena entidad);
        /// <summary>Actualiza una reseña y persiste los cambios.</summary>
        FacebookResena Update(FacebookResena entidad);
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        List<FacebookResena> Add(List<FacebookResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        List<FacebookResena> Update(List<FacebookResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        #region Consultas de Administración

        /// <summary>Retorna la grilla paginada de reseñas con filtros opcionales.</summary>
        FacebookResenaGrillaPaginadaDTO ObtenerGrilla(FacebookResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna las páginas configuradas con estadísticas agregadas.</summary>
        List<FacebookResenaPaginaItemDTO> ObtenerPaginas();
        /// <summary>Retorna las cuentas de Facebook para el combo de filtros.</summary>
        List<FacebookResenaCuentaComboDTO> ObtenerCuentasCombo();
        /// <summary>Marca las reseñas indicadas como visibles (Mostrar=true).</summary>
        bool MarcarResenaVisible(FacebookResenaMarcarMostrarDTO dto);
        /// <summary>Marca las reseñas indicadas como ocultas (Mostrar=false).</summary>
        bool MarcarResenaOculta(FacebookResenaMarcarMostrarDTO dto);

        #endregion

        #region Sincronización

        /// <summary>Sincroniza las reseñas desde la Graph API de Facebook para todas las páginas configuradas.</summary>
        Task<string> SincronizarFacebookApi(string nombreUsuario);

        #endregion
    }
}
