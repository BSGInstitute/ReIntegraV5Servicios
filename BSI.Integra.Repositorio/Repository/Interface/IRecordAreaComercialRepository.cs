using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRecordAreaComercialRepository : IGenericRepository<TRecordAreaComercial>
    {
        #region Metodos Base
        TRecordAreaComercial Add(RecordAreaComercial entidad);
        TRecordAreaComercial Update(RecordAreaComercial entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRecordAreaComercial> Add(IEnumerable<RecordAreaComercial> listadoEntidad);
        IEnumerable<TRecordAreaComercial> Update(IEnumerable<RecordAreaComercial> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RecordAreaComercialDTO> ObtenerRecordAreaComercial();
        IEnumerable<RecordAreaComercialComboDTO> ObtenerCombo();
        IEnumerable<RecordAreaComercialCompuestoDTO> ObtenerRecordAreaComercialParaTabla();
        RecordAreaComercial ObtenerPorId(int id);
    }
}
