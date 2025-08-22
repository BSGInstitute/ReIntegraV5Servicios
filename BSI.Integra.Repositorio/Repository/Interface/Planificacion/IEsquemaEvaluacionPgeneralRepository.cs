using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEsquemaEvaluacionPgeneralRepository : IGenericRepository<TEsquemaEvaluacionPgeneral>
    {
        #region Metodos Base
        TEsquemaEvaluacionPgeneral Add(EsquemaEvaluacionPgeneral entidad);
        TEsquemaEvaluacionPgeneral Update(EsquemaEvaluacionPgeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEsquemaEvaluacionPgeneral> Add(IEnumerable<EsquemaEvaluacionPgeneral> listadoEntidad);
        IEnumerable<TEsquemaEvaluacionPgeneral> Update(IEnumerable<EsquemaEvaluacionPgeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EsquemaEvaluacionPgeneral> ObtenerPorIdPGeneral(int idPGeneral);
        EsquemaEvaluacionPgeneral? ObtenerPorId(int id);
    }
}
