using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IGrupoComponenteEvaluacionRepository : IGenericRepository<TGrupoComponenteEvaluacion>
    {
        #region Metodos Base
        TGrupoComponenteEvaluacion Add(GrupoComponenteEvaluacion entidad);
        TGrupoComponenteEvaluacion Update(GrupoComponenteEvaluacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TGrupoComponenteEvaluacion> Add(IEnumerable<GrupoComponenteEvaluacion> listadoEntidad);
        IEnumerable<TGrupoComponenteEvaluacion> Update(IEnumerable<GrupoComponenteEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        GrupoComponenteEvaluacion? ObtenerPorId(int id);
        List<ComboDTO> ObtenerGrupoPorIds(List<int> idsGrupos);
        List<ComboDTO> ObtenerGrupoPorIdEvaluacion(int idEvaluacion);
        List<GrupoEvaluacionDTO> ObtenerGrupoEvaluacion(int IdEvaluacion); //configuracion
        List<GrupoComponenteDTO> ObtenerGrupoEvaluacionDesglosadoPorComponente(int IdEvaluacion); //calificacion
        public IEnumerable<ComboDTO> ObtenerComboPorId(int idExamenTest);

    }
}
