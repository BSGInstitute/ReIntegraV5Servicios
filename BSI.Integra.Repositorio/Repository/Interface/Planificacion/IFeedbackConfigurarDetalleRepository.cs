using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFeedbackConfigurarDetalleRepository:IGenericRepository<TFeedbackConfigurarDetalle>
    {

        #region Metodos Base
        TFeedbackConfigurarDetalle Add(FeedbackConfigurarDetalle entidad);
        TFeedbackConfigurarDetalle Update(FeedbackConfigurarDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFeedbackConfigurarDetalle> Add(IEnumerable<FeedbackConfigurarDetalle> listadoEntidad);
        IEnumerable<TFeedbackConfigurarDetalle> Update(IEnumerable<FeedbackConfigurarDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<FeedbackConfigurarDetalle> ObtenerDetallePorIdFeedbackConfigurar(int idFeedbackConfigurar);

        FeedbackConfigurarDetalle? ObtenerPorId(int id);

    }
}
