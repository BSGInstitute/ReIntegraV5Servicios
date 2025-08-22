using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IExamenTestRepository : IGenericRepository<TExamenTest>
    {
        #region Metodos Base
        TExamenTest Add(ExamenTest entidad);
        TExamenTest Update(ExamenTest entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TExamenTest> Add(IEnumerable<ExamenTest> listadoEntidad);
        IEnumerable<TExamenTest> Update(IEnumerable<ExamenTest> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ExamenTest? ObtenerPorId(int id);
        public IEnumerable<ExamenTestResumidoDTO> Obtener();
        public List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int idEvaluacion);
        public IEnumerable<ComboDTO> ObtenerComponentes(int idEvaluacion);
        List<EstructuraBasicaDTO> ObtenerEvaluacionNoAsignadoProcesoSeleccion(int IdProcesoSeleccion);
        List<EvaluacionAsignadoProcesoDTO> ObtenerEvaluacionAsignadoProcesoSeleccion(int IdProcesoSeleccion);
        List<NombreEvaluacionAgrupadaComponenteDTO> ObtenerNombreEvaluacionPuntaje(int IdProcesoSeleccion);
    }
}
