using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISolicitudCategoriaService
    {
        #region Metodos Base
        SolicitudCategoria Add(SolicitudCategoria entidad);
        SolicitudCategoria Update(SolicitudCategoria entidad);
        bool Delete(int id, string usuario);
        List<SolicitudCategoria> Add(List<SolicitudCategoria> listadoEntidad);
        List<SolicitudCategoria> Update(List<SolicitudCategoria> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        SolicitudCategoria ObtenerPorId(int id);
        IEnumerable<ComboSolicitudDTO> ObtenerCombo();
        IEnumerable<ComboSolicitudDTO> ObtenerComboPorTipoReporte(int idTipoReporte);
    }
}
