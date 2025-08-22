using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoPuntajeCalificacionRepository : IGenericRepository<TPuestoTrabajoPuntajeCalificacion>
    {
        #region Metodos Base
        TPuestoTrabajoPuntajeCalificacion Add(PuestoTrabajoPuntajeCalificacion entidad);

        TPuestoTrabajoPuntajeCalificacion Update(PuestoTrabajoPuntajeCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoPuntajeCalificacion> Add(IEnumerable<PuestoTrabajoPuntajeCalificacion> listadoEntidad);
        IEnumerable<TPuestoTrabajoPuntajeCalificacion> Update(IEnumerable<PuestoTrabajoPuntajeCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> ObtenerNombreEvaluacionPuntaje();

        PuestoTrabajoPuntajeCalificacion ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacion(int? idPerfilPuestoTrabajo, int? idExamenTest);
        PuestoTrabajoPuntajeCalificacion ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacionAndIdGrupoComponenteEvaluacion(int? idPerfilPuestoTrabajo, int? idExamenTest, int? idGrupo);
        PuestoTrabajoPuntajeCalificacion ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacionANDIdComponente(int? idPerfilPuestoTrabajo, int? idExamenTest, int? idComponente);
        PuestoTrabajoPuntajeCalificacion BuscarPorIdPerfilAndIdEvaluacionAndIdGrupoAndIdComponente(int? idPerfilPuestoTrabajo, int? idEvaluacion, int? idGrupo, int? idComponente);
    }
}
