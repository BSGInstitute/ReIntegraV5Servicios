using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ICriterioCalificacionService
    {
        #region Metodos Base
        CriterioCalificacionLlamada Add(CriterioCalificacionLlamada entidad);
        CriterioCalificacionLlamada Update(CriterioCalificacionLlamada entidad);
        bool Delete(int id, string usuario);
        List<CriterioCalificacionLlamada> Add(List<CriterioCalificacionLlamada> listadoEntidad);
        List<CriterioCalificacionLlamada> Update(List<CriterioCalificacionLlamada> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        CriterioCalificacionLlamada ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
