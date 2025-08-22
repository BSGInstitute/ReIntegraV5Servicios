using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEsquemaEvaluacionPgeneralProveedorRepository : IGenericRepository<TEsquemaEvaluacionPgeneralProveedor>
    {
        #region Metodos Base
        TEsquemaEvaluacionPgeneralProveedor Add(EsquemaEvaluacionPgeneralProveedor entidad);
        TEsquemaEvaluacionPgeneralProveedor Update(EsquemaEvaluacionPgeneralProveedor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEsquemaEvaluacionPgeneralProveedor> Add(IEnumerable<EsquemaEvaluacionPgeneralProveedor> listadoEntidad);
        IEnumerable<TEsquemaEvaluacionPgeneralProveedor> Update(IEnumerable<EsquemaEvaluacionPgeneralProveedor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EsquemaEvaluacionPgeneralProveedor> ObtenerPorIdEsquemaEvaluacionPGeneral(int idEsquemaEvaluacionPGeneral);
    }
}
