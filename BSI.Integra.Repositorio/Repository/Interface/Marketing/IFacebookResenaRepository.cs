using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IFacebookResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio para la tabla mkt.T_FacebookResena.
    /// Extiende GenericRepository con consultas por SP y acciones de visibilidad masiva.
    /// </summary>
    public interface IFacebookResenaRepository : IGenericRepository<TFacebookResena>
    {
        #region CRUD

        /// <summary>Inserta una reseña y retorna el modelo persistido.</summary>
        TFacebookResena Add(FacebookResena entidad);
        /// <summary>Actualiza una reseña existente con control de concurrencia (RowVersion).</summary>
        TFacebookResena Update(FacebookResena entidad);
        /// <summary>Elimina lógicamente una reseña (Estado=false).</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        IEnumerable<TFacebookResena> Add(IEnumerable<FacebookResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque con control de concurrencia.</summary>
        IEnumerable<TFacebookResena> Update(IEnumerable<FacebookResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        #region Consultas

        /// <summary>Obtiene la configuración de una página de Facebook por su identificador.</summary>
        FacebookConfiguracionPaginaDTO ObtenerFacebookConfiguracionPagina(string identificadorPagina);
        /// <summary>Busca una reseña activa por su IdentificadorHistoria y página.</summary>
        FacebookResena ObtenerPorIdentificadorHistoria(string identificadorHistoria, int idFacebookConfiguracion);
        /// <summary>Obtiene todas las reseñas activas de una página específica.</summary>
        List<FacebookResena> ObtenerResenasPorPagina(int idFacebookConfiguracion);
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_FacebookResenaObtenerDatos (modo Grilla).</summary>
        FacebookResenaGrillaPaginadaDTO ObtenerGrilla(FacebookResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna estadísticas por página ejecutando mkt.SP_FacebookResenaObtenerDatos (modo Pagina).</summary>
        List<FacebookResenaPaginaItemDTO> ObtenerPaginas();
        /// <summary>Retorna las cuentas de Facebook para el combo de filtros vía EF Core.</summary>
        List<FacebookResenaCuentaComboDTO> ObtenerCuentasCombo();

        #endregion

        #region Acciones de visibilidad

        /// <summary>Marca como visibles (Mostrar=true) las reseñas indicadas por Id.</summary>
        void MarcarResenaVisible(List<int> ids, string usuario);
        /// <summary>Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.</summary>
        void MarcarResenaOculta(List<int> ids, string usuario);

        #endregion
    }
}
