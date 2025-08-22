using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IRecordatorioClasesOnlineIvrService
    {
        #region Metodos Base
        RecordatorioClasesOnlineIvr Add(RecordatorioClasesOnlineIvr data, string Usuario);
        RecordatorioClasesOnlineIvr Update(RecordatorioClasesOnlineIvr data);
        bool Delete(int id, string usuario);
        List<RecordatorioClasesOnlineIvr> Add(List<RecordatorioClasesOnlineIvr> listadoEntidad);
        List<RecordatorioClasesOnlineIvr> Update(List<RecordatorioClasesOnlineIvr> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public RecordatorioClasesOnlineIvr ObtenerRecordatorioClasesOnlineIvrById(int Id);
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnline();
        public bool ActualizarIntento(int Id);
        public bool ActualizarConcluido(int Id);
    }
}
