using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: ILinkedinResenaRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de testimonios de LinkedIn.
    /// Combina EF Core (CRUD, combos, visibilidad) con Dapper (grilla vía SP).
    /// </summary>
    public interface ILinkedinResenaRepository : IGenericRepository<TLinkedinResena>
    {
        #region CRUD

        /// <summary>Inserta un testimonio y retorna el modelo persistido.</summary>
        TLinkedinResena Add(LinkedinResena entidad);
        /// <summary>Actualiza un testimonio con control de concurrencia (RowVersion).</summary>
        TLinkedinResena Update(LinkedinResena entidad);
        /// <summary>Elimina lógicamente un testimonio (Estado=false).</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de testimonios en bloque.</summary>
        IEnumerable<TLinkedinResena> Add(IEnumerable<LinkedinResena> listadoEntidad);
        /// <summary>Actualiza un listado de testimonios en bloque con control de concurrencia.</summary>
        IEnumerable<TLinkedinResena> Update(IEnumerable<LinkedinResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de testimonios por sus Ids.</summary>
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        #region Consultas

        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_LinkedinResenaObtenerDatos (modo Grilla).</summary>
        LinkedinResenaGrillaPaginadaDTO ObtenerGrilla(LinkedinResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna los países que tienen testimonios activos para el combo de filtros.</summary>
        List<LinkedinResenaPaisComboDTO> ObtenerPaisesCombo();
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        List<LinkedinResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais);

        #endregion

        #region Acciones de visibilidad

        /// <summary>Marca como visibles (Mostrar=true) los testimonios indicados por Id.</summary>
        void MarcarResenaVisible(List<int> ids, string usuario);
        /// <summary>Marca como ocultos (Mostrar=false) los testimonios indicados por Id.</summary>
        void MarcarResenaOculta(List<int> ids, string usuario);

        #endregion
    }
}
