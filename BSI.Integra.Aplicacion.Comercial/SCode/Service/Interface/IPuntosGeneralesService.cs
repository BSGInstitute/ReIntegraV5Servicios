using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
     public interface IPuntosGeneralesService
    {
        #region Metodos Base
        PuntosGeneralesCalificacion Add(PuntosGeneralesCalificacion entidad);
        PuntosGeneralesCalificacion Update(PuntosGeneralesCalificacion entidad);
        bool Delete(int id, string usuario);
        List<PuntosGeneralesCalificacion> Add(List<PuntosGeneralesCalificacion> listadoEntidad);
        List<PuntosGeneralesCalificacion> Update(List<PuntosGeneralesCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        PuntosGeneralesCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<PuntosGeneralesCalificacion> ObtenerPuntosGeneralesPorArea(int idPersonalAreaTrabajo);
    }
}
