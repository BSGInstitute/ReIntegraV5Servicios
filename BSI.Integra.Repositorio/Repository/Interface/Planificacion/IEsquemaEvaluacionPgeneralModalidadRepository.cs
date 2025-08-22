using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEsquemaEvaluacionPgeneralModalidadRepository : IGenericRepository<TEsquemaEvaluacionPgeneralModalidad>
    {
        #region Metodos Base
        TEsquemaEvaluacionPgeneralModalidad Add(EsquemaEvaluacionPgeneralModalidad entidad);
        TEsquemaEvaluacionPgeneralModalidad Update(EsquemaEvaluacionPgeneralModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEsquemaEvaluacionPgeneralModalidad> Add(IEnumerable<EsquemaEvaluacionPgeneralModalidad> listadoEntidad);
        IEnumerable<TEsquemaEvaluacionPgeneralModalidad> Update(IEnumerable<EsquemaEvaluacionPgeneralModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EsquemaEvaluacionPgeneralModalidad> ObtenerPorIdEsquemaEvaluacionPGeneral(int idEsquemaEvaluacionPGeneral);
    }
}
