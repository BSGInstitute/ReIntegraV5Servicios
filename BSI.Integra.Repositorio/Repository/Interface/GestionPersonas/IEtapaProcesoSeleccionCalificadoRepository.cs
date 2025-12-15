using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IEtapaProcesoSeleccionCalificadoRepository : IGenericRepository<TEtapaProcesoSeleccionCalificado>
    {
        #region Metodos Base
        TEtapaProcesoSeleccionCalificado Add(EtapaProcesoSeleccionCalificado entidad);
        TEtapaProcesoSeleccionCalificado Update(EtapaProcesoSeleccionCalificado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TEtapaProcesoSeleccionCalificado> Add(IEnumerable<EtapaProcesoSeleccionCalificado> listadoEntidad);
        IEnumerable<TEtapaProcesoSeleccionCalificado> Update(IEnumerable<EtapaProcesoSeleccionCalificado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        EtapaProcesoSeleccionCalificado? ObtenerPorId(int id);
        List<int> ObtenerIdsPostulanteEtapaProcesoSeleccionActual(EvaluacionPostulanteFiltroReporteDTO filtro);
        List<EtapaCalificadaPostulanteProcesoSeleccionDTO> ObtenerPorIdsPostulanteIdsProcesoSeleccion(List<int> idsPostulante, List<int> idsProcesoSeleccion);
        EtapaProcesoSeleccionCalificado? ObtenerEtapaActualPorIdPostulante(int idPostulante);
        EtapaProcesoSeleccionCalificado? ObtenerPorIdPostulanteIdProcesoSeleccionEtapa(int idPostulante, int idProcesoSeleccionEtapa);
        List<EtapaExamenesPorPostulanteDTO> ObtenerListaEtapaExamenesPorPostulante(int idProcesoSeleccion, int idPostulante);
        void ActualizarEtapaCalificada(EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificada);

    }
}
