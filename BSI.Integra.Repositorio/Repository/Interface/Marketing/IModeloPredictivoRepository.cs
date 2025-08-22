using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IModeloPredictivoRepository : IGenericRepository<TModeloPredictivo>
    {
        #region Metodos Base
        TModeloPredictivo Add(ModeloPredictivo entidad);
        TModeloPredictivo Update(ModeloPredictivo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivo> Add(IEnumerable<ModeloPredictivo> listadoEntidad);
        IEnumerable<TModeloPredictivo> Update(IEnumerable<ModeloPredictivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivo? ObtenerPorId(int id);
        ModeloPredictivoInterceptoDTO ObtenerInterceptoPorPrograma(int idPGeneral);
    }
}
