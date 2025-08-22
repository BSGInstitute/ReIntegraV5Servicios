using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsignacionAutomaticaTempRepository : IGenericRepository<TAsignacionAutomaticaTemp>
    {
        #region Metodos Base
        TAsignacionAutomaticaTemp Add(AsignacionAutomaticaTemp entidad);
        TAsignacionAutomaticaTemp Update(AsignacionAutomaticaTemp entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsignacionAutomaticaTemp> Add(IEnumerable<AsignacionAutomaticaTemp> listadoEntidad);
        IEnumerable<TAsignacionAutomaticaTemp> Update(IEnumerable<AsignacionAutomaticaTemp> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion
        public NombreCampaniaAsiAsignacionAutomaticaTempDTO ObtenerNombreCampaniaPorIdFaseOportunidad(string IdFaseOportunidad);
        public List<AsignacionAutomaticaTempDTO> ObtenerNuevosRegistros();
        public AsignacionAutomaticaTemp ObtenerPorIdFaseOportunidadPortal(string idFaseOportunidadPortal);
        public AsignacionAutomaticaTemp ObtenerPorId(int idAsignacionAutomatica);
        public IdDTO InsertarAsignacionAutomatica(InsertarAsignacionAutomaticaTempDTO asignacion);
        public AsignacionAutomaticaTemModeloDTO ObtenerNuevosRegistroById(string idRegistroPortalWeb, int idPagina);


    }
}
