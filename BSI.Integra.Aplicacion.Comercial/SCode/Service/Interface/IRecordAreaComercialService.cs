using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IRecordAreaComercialService
    {
        #region Metodos Base
        RecordAreaComercial Add(RecordAreaComercial entidad);
        RecordAreaComercial Update(RecordAreaComercial entidad);
        bool Delete(int id, string usuario);

        List<RecordAreaComercial> Add(List<RecordAreaComercial> listadoEntidad);
        List<RecordAreaComercial> Update(List<RecordAreaComercial> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RecordAreaComercialDTO> ObtenerRecordAreaComercial();
        IEnumerable<RecordAreaComercialComboDTO> ObtenerCombo();
        IEnumerable<RecordAreaComercialCompuestoDTO> ObtenerRecordAreaComercialParaTabla();
        RecordAreaComercial ObtenerPorId(int id);
    }
}
