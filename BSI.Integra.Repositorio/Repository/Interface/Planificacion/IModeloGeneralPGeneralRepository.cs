using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IModeloGeneralPGeneralRepository : IGenericRepository<TModeloGeneralPgeneral>
    {
        #region Metodos Base
        TModeloGeneralPgeneral Add(ModeloGeneralPGeneral entidad);
        TModeloGeneralPgeneral Update(ModeloGeneralPGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloGeneralPgeneral> Add(IEnumerable<ModeloGeneralPGeneral> listadoEntidad);
        IEnumerable<TModeloGeneralPgeneral> Update(IEnumerable<ModeloGeneralPGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloGeneralPGeneral? ObtenerPorIdPGeneral(int idProgramaGeneral);
    }
}
