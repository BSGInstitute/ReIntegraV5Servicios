using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialCriterioVerificacionDetalleRepository :IGenericRepository<TMaterialCriterioVerificacionDetalle>
    {
        #region Metodos Base
        TMaterialCriterioVerificacionDetalle Add(MaterialCriterioVerificacionDetalle entidad);
        TMaterialCriterioVerificacionDetalle Update(MaterialCriterioVerificacionDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialCriterioVerificacionDetalle> Add(IEnumerable<MaterialCriterioVerificacionDetalle> listadoEntidad);
        IEnumerable<TMaterialCriterioVerificacionDetalle> Update(IEnumerable<MaterialCriterioVerificacionDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
