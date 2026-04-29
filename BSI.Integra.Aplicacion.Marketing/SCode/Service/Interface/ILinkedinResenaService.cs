using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: ILinkedinResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de testimonios de LinkedIn.
    /// CRUD manual (sin API externa), consultas de administración vía SP y gestión de visibilidad.
    /// </summary>
    public interface ILinkedinResenaService
    {
        #region CRUD

        /// <summary>Inserta un testimonio y persiste los cambios.</summary>
        LinkedinResena Add(LinkedinResena entidad);
        /// <summary>Actualiza un testimonio y persiste los cambios.</summary>
        LinkedinResena Update(LinkedinResena entidad);
        /// <summary>Elimina lógicamente un testimonio por su Id.</summary>
        bool Delete(int id, string usuario);
        /// <summary>Inserta un listado de testimonios en bloque.</summary>
        List<LinkedinResena> Add(List<LinkedinResena> listadoEntidad);
        /// <summary>Actualiza un listado de testimonios en bloque.</summary>
        List<LinkedinResena> Update(List<LinkedinResena> listadoEntidad);
        /// <summary>Elimina lógicamente un listado de testimonios por sus Ids.</summary>
        bool Delete(List<int> listadoIds, string usuario);

        #endregion

        #region Consultas de Administración

        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_LinkedinResenaObtenerDatos (modo Grilla).</summary>
        LinkedinResenaGrillaPaginadaDTO ObtenerGrilla(LinkedinResenaGrillaFiltroDTO filtro);
        /// <summary>Retorna los países con testimonios activos para el combo de filtros.</summary>
        List<LinkedinResenaPaisComboDTO> ObtenerPaisesCombo();
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        List<LinkedinResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais);
        /// <summary>Marca los testimonios indicados como visibles (Mostrar=true).</summary>
        bool MarcarResenaVisible(LinkedinResenaMarcarMostrarDTO dto);
        /// <summary>Marca los testimonios indicados como ocultos (Mostrar=false).</summary>
        bool MarcarResenaOculta(LinkedinResenaMarcarMostrarDTO dto);

        #endregion
    }
}
