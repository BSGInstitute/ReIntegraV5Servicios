using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISolicitudTipoReporteService
    {
        #region Metodos Base
        SolicitudTipoReporte Add(SolicitudTipoReporte entidad);
        SolicitudTipoReporte Update(SolicitudTipoReporte entidad);
        bool Delete(int id, string usuario);
        List<SolicitudTipoReporte> Add(List<SolicitudTipoReporte> listadoEntidad);
        List<SolicitudTipoReporte> Update(List<SolicitudTipoReporte> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        SolicitudTipoReporte ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
