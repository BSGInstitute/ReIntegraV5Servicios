using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IModeloPredictivoIndustriaRepository : IGenericRepository<TModeloPredictivoIndustrium>
    {
        #region Metodos Base
        TModeloPredictivoIndustrium Add(ModeloPredictivoIndustria entidad);
        TModeloPredictivoIndustrium Update(ModeloPredictivoIndustria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoIndustrium> Add(IEnumerable<ModeloPredictivoIndustria> listadoEntidad);
        IEnumerable<TModeloPredictivoIndustrium> Update(IEnumerable<ModeloPredictivoIndustria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoIndustria? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoIndustria> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoIndustriaDTO> ObtenerIndustriaPorPrograma(int idPGeneral);
    }
}
