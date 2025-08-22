using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICriterioEvaluacionProcesoRepository : IGenericRepository<TCriterioEvaluacionProceso>
    {
        #region Metodos Base
        TCriterioEvaluacionProceso Add(CriterioEvaluacionProceso entidad);
        TCriterioEvaluacionProceso Update(CriterioEvaluacionProceso entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCriterioEvaluacionProceso> Add(IEnumerable<CriterioEvaluacionProceso> listadoEntidad);
        IEnumerable<TCriterioEvaluacionProceso> Update(IEnumerable<CriterioEvaluacionProceso> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CriterioEvaluacionProcesoDTO> Obtener();
        CriterioEvaluacionProceso? ObtenerPorId(int id);
    }
}
