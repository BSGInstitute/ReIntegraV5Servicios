using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICriterioEvaluacionRepository : IGenericRepository<TCriterioEvaluacion>
    {
        #region Metodos Base
        TCriterioEvaluacion Add(CriterioEvaluacion entidad);
        TCriterioEvaluacion Update(CriterioEvaluacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioEvaluacion> Add(IEnumerable<CriterioEvaluacion> listadoEntidad);
        IEnumerable<TCriterioEvaluacion> Update(IEnumerable<CriterioEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CriterioEvaluacionDTO> ObtenerCriteriosEvaluacion();
        CriterioEvaluacionDTO? ObtenerCriterioEvaluacionPorId(int idCriterioEvaluacion);
        bool EliminarDetalles(int id);
        CriterioEvaluacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        List<ComboDTO> ObtenerCriterio(int tipoprograma, int modalidadprograma);

    }
}
