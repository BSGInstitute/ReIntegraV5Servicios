using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICentroEstudioRepository : IGenericRepository<TCentroEstudio>
    {

        #region Metodos Base
        TCentroEstudio Add(CentroEstudio entidad);
        TCentroEstudio Update(CentroEstudio entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCentroEstudio> Add(IEnumerable<CentroEstudio> listadoEntidad);
        IEnumerable<TCentroEstudio> Update(IEnumerable<CentroEstudio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CentroEstudioDTO> Obtener();
        
        CentroEstudio? ObtenerPorId(int idCentroEstudio);

        IEnumerable<CentroEstudioComboDTO> ObtenerComboCentroEstudio();

        IEnumerable<CentroEstudioComboDTO> ObtenerListaEstadoEstudioCombo();
    }
}
