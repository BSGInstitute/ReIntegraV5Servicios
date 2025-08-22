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
    public interface IConfigurarEvaluacionTrabajoRepository : IGenericRepository<TConfigurarEvaluacionTrabajo>
    {
        #region Metodos Base
        TConfigurarEvaluacionTrabajo Add(ConfigurarEvaluacionTrabajo entidad);
        TConfigurarEvaluacionTrabajo Update(ConfigurarEvaluacionTrabajo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfigurarEvaluacionTrabajo> Add(IEnumerable<ConfigurarEvaluacionTrabajo> listadoEntidad);
        IEnumerable<TConfigurarEvaluacionTrabajo> Update(IEnumerable<ConfigurarEvaluacionTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConDetallePorIdConfigurarEvaluacionTrabajo(int idConfigurarEvaluacionTrabajo);
        IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConDetallePorIdConfigurarEvaluacionTrabajoIdSeccionFila(int idConfigurarEvaluacionTrabajo, int idSeccion, int fila);
        IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerPorIdPGeneralIdSeccionFila(int idPGeneral, int idSeccion, int fila);
        IEnumerable<ConfigurarEvaluacionTrabajoDetalleDTO> ObtenerConDetallePorIdPGeneral(int idPGeneral);
        ConfigurarEvaluacionTrabajo ObtenerPorId(int id);
    }
}
