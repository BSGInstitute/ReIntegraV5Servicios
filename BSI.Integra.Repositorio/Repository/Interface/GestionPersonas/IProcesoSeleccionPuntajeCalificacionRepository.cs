using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IProcesoSeleccionPuntajeCalificacionRepository
    {
        #region Metodos Base
        TProcesoSeleccionPuntajeCalificacion Add(ProcesoSeleccionPuntajeCalificacion entidad);
        TProcesoSeleccionPuntajeCalificacion Update(ProcesoSeleccionPuntajeCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProcesoSeleccionPuntajeCalificacion> Add(IEnumerable<ProcesoSeleccionPuntajeCalificacion> listadoEntidad);
        IEnumerable<TProcesoSeleccionPuntajeCalificacion> Update(IEnumerable<ProcesoSeleccionPuntajeCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamen(int? IdProcesoSeleccion, int? IdEvaluacion, int? IdGrupo);
        ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamenIdGrupoComponenteEvaluacion(int? idProcesoSeleccion, int? idEvaluacion, int? idComponente);
        ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamenIdGrupoComponenteEvaluacionPorComponente(int? idProcesoSeleccion, int? idEvaluacion, int? idComponente, int? idGrupo);
        ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacion(int? IdProcesoSeleccion, int? IdEvaluacion);
        ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdEvaluacionIdGrupoComponenteEvaluacionIdExamen(int? IdProcesoSeleccion, int? IdEvaluacion, int? IdGrupo, int? idComponente);
    }
}
