using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IRecordatorioWebinarIvrService
    {
        #region Metodos Base
        RecordatorioWebinarIvr Add(RecordatorioWebinarIvr data, string Usuario);
        RecordatorioWebinarIvr Update(RecordatorioWebinarIvr data);
        bool Delete(int id, string usuario);

        List<RecordatorioWebinarIvr> Add(List<RecordatorioWebinarIvr> listadoEntidad);
        List<RecordatorioWebinarIvr> Update(List<RecordatorioWebinarIvr> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public RecordatorioWebinarIvr ObtenerRecordatorioWebinarIvrPorId(int Id);
        public DatoLLamadaRecordatorioWebinarDTO ObtenerDatoLlamadaRecordatorioWebinar();
        public bool ActualizarIntento(int Id);
        public bool ActualizarConcluido(int Id);
    }
}
