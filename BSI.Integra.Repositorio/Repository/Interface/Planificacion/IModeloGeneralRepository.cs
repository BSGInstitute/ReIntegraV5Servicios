using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IModeloGeneralRepository : IGenericRepository<TModeloGeneral>
    {
        #region Metodos Base
        TModeloGeneral Add(ModeloGeneral entidad);
        TModeloGeneral Update(ModeloGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloGeneral> Add(IEnumerable<ModeloGeneral> listadoEntidad);
        IEnumerable<TModeloGeneral> Update(IEnumerable<ModeloGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloGeneral? ObtenerPorId(int id);
    }
}
