using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IComputrabajoResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de reseñas de empleador en Computrabajo.
    /// CRUD manual (sin API externa), consultas de administración vía SP y gestión de visibilidad.
    /// Computrabajo NO tiene API pública — captura manual periódica (quincenal).
    /// </summary>
    public interface IComputrabajoResenaService
    {
        #region CRUD

        /// <summary>Inserta una reseña y persiste los cambios.</summary>
        ComputrabajoResena Add(ComputrabajoResena entidad);
        /// <summary>Actualiza una reseña y persiste los cambios.</summary>
        ComputrabajoResena Update(ComputrabajoResena entidad);
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        List<ComputrabajoResena> Add(List<ComputrabajoResena> listadoEntidad);
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        List<ComputrabajoResena> Update(List<ComputrabajoResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        #region Consultas de Administración

        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_ComputrabajoResenaObtenerDatos.</summary>
        ComputrabajoResenaGrillaPaginadaDTO ObtenerGrilla(ComputrabajoResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna los países con reseñas activas para el combo de filtros.</summary>
        List<ComputrabajoResenaPaisComboDTO> ObtenerPaisesCombo();
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        List<ComputrabajoResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais);
        /// <summary>Marca las reseñas indicadas como visibles (Mostrar=true).</summary>
        bool MarcarResenaVisible(ComputrabajoResenaMarcarMostrarDTO dto);
        /// <summary>Marca las reseñas indicadas como ocultas (Mostrar=false).</summary>
        bool MarcarResenaOculta(ComputrabajoResenaMarcarMostrarDTO dto);

        #endregion
    }
}
