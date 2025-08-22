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
    public interface IModeloPredictivoCargoRepository : IGenericRepository<TModeloPredictivoCargo>
    {
        #region Metodos Base
        TModeloPredictivoCargo Add(ModeloPredictivoCargo entidad);
        TModeloPredictivoCargo Update(ModeloPredictivoCargo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoCargo> Add(IEnumerable<ModeloPredictivoCargo> listadoEntidad);
        IEnumerable<TModeloPredictivoCargo> Update(IEnumerable<ModeloPredictivoCargo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoCargo? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoCargo> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoCargoDTO> ObtenerCargoPorPrograma(int idPGeneral);
    }
}
