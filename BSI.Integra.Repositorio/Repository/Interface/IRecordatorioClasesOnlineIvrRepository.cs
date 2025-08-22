using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRecordatorioClasesOnlineIvrRepository : IGenericRepository<TRecordatorioClasesOnlineIvr>
    {
        #region Metodos Base
        TRecordatorioClasesOnlineIvr Add(RecordatorioClasesOnlineIvr entidad);
        TRecordatorioClasesOnlineIvr Update(RecordatorioClasesOnlineIvr entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRecordatorioClasesOnlineIvr> Add(IEnumerable<RecordatorioClasesOnlineIvr> listadoEntidad);
        IEnumerable<TRecordatorioClasesOnlineIvr> Update(IEnumerable<RecordatorioClasesOnlineIvr> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public RecordatorioClasesOnlineIvr ObtenerRecordatorioClasesOnlineIvrById(int Id);
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnline();
        public DatoLLamadaRecordatorioClasesOnlineDTO ObtenerDatoLlamadaRecordatorioClasesOnlineById(int Id);
    }
}
