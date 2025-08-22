using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICriterioEvaluacionTipoPersonaRepository : IGenericRepository<TCriterioEvaluacionTipoPersona>
    {
        #region Metodos Base
        TCriterioEvaluacionTipoPersona Add(CriterioEvaluacionTipoPersona entidad);
        TCriterioEvaluacionTipoPersona Update(CriterioEvaluacionTipoPersona entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioEvaluacionTipoPersona> Add(IEnumerable<CriterioEvaluacionTipoPersona> listadoEntidad);
        IEnumerable<TCriterioEvaluacionTipoPersona> Update(IEnumerable<CriterioEvaluacionTipoPersona> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<CriterioEvaluacionTipoPersonaDTO> ObtenerPorIdCriterioEvaluacion(int idCriterioEvaluacion);
        CriterioEvaluacionTipoPersona ObtenerPorIdTipoPersonaYIdCriterioEvaluacion(int idTipoPersona, int idCriterioEvaluacion);
    }
}
