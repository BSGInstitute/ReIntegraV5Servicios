using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISolicitudTipoReporteRepository : IGenericRepository<TSolicitudTipoReporte>
    {
        #region Metodos Base
        TSolicitudTipoReporte Add(SolicitudTipoReporte entidad);
        TSolicitudTipoReporte Update(SolicitudTipoReporte entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSolicitudTipoReporte> Add(IEnumerable<SolicitudTipoReporte> listadoEntidad);
        IEnumerable<TSolicitudTipoReporte> Update(IEnumerable<SolicitudTipoReporte> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        SolicitudTipoReporte ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
