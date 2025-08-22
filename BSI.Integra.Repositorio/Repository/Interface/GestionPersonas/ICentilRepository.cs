using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICentilRepository : IGenericRepository<TCentil>
    {
        #region Metodos Base
        TCentil Add(Centil entidad);
        TCentil Update(Centil entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCentil> Add(IEnumerable<Centil> listadoEntidad);
        IEnumerable<TCentil> Update(IEnumerable<Centil> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<StringDTO> ObtenerVersionesCentil();
        List<CentilDTO> ObtenerCentilesSinExamenTest();
        List<CentilDTO> ObtenerCentilesPorEvaluacion(int idEvaluacion);
        List<CentilDTO> ObtenerGrupoEvaluacionDesglosadoPorComponente(int idGrupoComponenteEvaluacion);
        public List<CentilDTO> ObtenerCentilesEvaluacion(int idExamenTest);

    }
}
