using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModeloPredictivoProbabilidadRepository : IGenericRepository<TModeloPredictivoProbabilidad>
    {
        #region Metodos Base
        TModeloPredictivoProbabilidad Add(ModeloPredictivoProbabilidad entidad);
        TModeloPredictivoProbabilidad Update(ModeloPredictivoProbabilidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoProbabilidad> Add(IEnumerable<ModeloPredictivoProbabilidad> listadoEntidad);
        IEnumerable<TModeloPredictivoProbabilidad> Update(IEnumerable<ModeloPredictivoProbabilidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
      
    }
}
