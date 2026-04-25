using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IComputrabajoResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de reseñas de empleador en Computrabajo.
    /// Combina EF Core (CRUD, combos, visibilidad) con Dapper (grilla vía SP).
    /// Captura manual periódica — Computrabajo NO tiene API pública.
    /// </summary>
    public interface IComputrabajoResenaRepository : IGenericRepository<TComputrabajoResena>
    {
        #region CRUD

        /// <summary>Inserta una reseña y retorna el modelo persistido.</summary>
        TComputrabajoResena Add(ComputrabajoResena entidad);
        /// <summary>Actualiza una reseña con control de concurrencia (RowVersion).</summary>
        TComputrabajoResena Update(ComputrabajoResena entidad);
        /// <summary>Elimina lógicamente una reseña (Estado=false).</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        IEnumerable<TComputrabajoResena> Add(IEnumerable<ComputrabajoResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque con control de concurrencia.</summary>
        IEnumerable<TComputrabajoResena> Update(IEnumerable<ComputrabajoResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        #region Consultas

        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_ComputrabajoResenaObtenerDatos.</summary>
        ComputrabajoResenaGrillaPaginadaDTO ObtenerGrilla(ComputrabajoResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna los países que tienen reseñas activas para el combo de filtros.</summary>
        List<ComputrabajoResenaPaisComboDTO> ObtenerPaisesCombo();
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        List<ComputrabajoResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais);

        #endregion

        #region Acciones de visibilidad

        /// <summary>Marca como visibles (Mostrar=true) las reseñas indicadas por Id.</summary>
        void MarcarResenaVisible(List<int> ids, string usuario);
        /// <summary>Marca como ocultas (Mostrar=false) las reseñas indicadas por Id.</summary>
        void MarcarResenaOculta(List<int> ids, string usuario);

        #endregion
    }
}
