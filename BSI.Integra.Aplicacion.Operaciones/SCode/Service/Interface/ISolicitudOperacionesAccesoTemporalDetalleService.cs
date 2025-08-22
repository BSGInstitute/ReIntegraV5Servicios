using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISolicitudOperacionesAccesoTemporalDetalleService
    {
        #region Metodos Base
        SolicitudOperacionesAccesoTemporalDetalle Add(SolicitudOperacionesAccesoTemporalDetalle entidad);
        SolicitudOperacionesAccesoTemporalDetalle Update(SolicitudOperacionesAccesoTemporalDetalle entidad);
        bool Delete(int id, string usuario);

        List<SolicitudOperacionesAccesoTemporalDetalle> Add(List<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad);
        List<SolicitudOperacionesAccesoTemporalDetalle> Update(List<SolicitudOperacionesAccesoTemporalDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
