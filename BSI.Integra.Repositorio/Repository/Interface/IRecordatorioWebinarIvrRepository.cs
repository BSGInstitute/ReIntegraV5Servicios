using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRecordatorioWebinarIvrRepository : IGenericRepository<TRecordatorioWebinarIvr>
    {
        #region Metodos Base
        TRecordatorioWebinarIvr Add(RecordatorioWebinarIvr entidad);
        TRecordatorioWebinarIvr Update(RecordatorioWebinarIvr entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRecordatorioWebinarIvr> Add(IEnumerable<RecordatorioWebinarIvr> listadoEntidad);
        IEnumerable<TRecordatorioWebinarIvr> Update(IEnumerable<RecordatorioWebinarIvr> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public RecordatorioWebinarIvr ObtenerRecordatorioWebinarIvrPorId(int Id);
        public DatoLLamadaRecordatorioWebinarDTO ObtenerDatoLlamadaRecordatorioWebinar();
        public DatoLLamadaRecordatorioWebinarDTO ObtenerDatoLlamadaRecordatorioWebinarPorId(int Id);
    }
}
