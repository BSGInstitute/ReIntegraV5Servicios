using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IFormulaPuntajeRepository : IGenericRepository<TSexo>
    {
        #region Metodos Base
        TSexo Add(FormulaPuntaje entidad);
        TSexo Update(FormulaPuntaje entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSexo> Add(IEnumerable<FormulaPuntaje> listadoEntidad);
        IEnumerable<TSexo> Update(IEnumerable<FormulaPuntaje> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        FormulaPuntaje? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        public IEnumerable<FormulaPuntajeDTO> Obtener();

    }
}
