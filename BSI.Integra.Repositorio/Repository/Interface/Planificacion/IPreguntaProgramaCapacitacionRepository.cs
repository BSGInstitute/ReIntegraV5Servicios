using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPreguntaProgramaCapacitacionRepository : IGenericRepository<TPreguntaProgramaCapacitacion>
    {
        #region Metodos Base
        TPreguntaProgramaCapacitacion Add(PreguntaProgramaCapacitacion entidad);
        TPreguntaProgramaCapacitacion Update(PreguntaProgramaCapacitacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaProgramaCapacitacion> Add(IEnumerable<PreguntaProgramaCapacitacion> listadoEntidad);
        IEnumerable<TPreguntaProgramaCapacitacion> Update(IEnumerable<PreguntaProgramaCapacitacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PreguntaPorProgramaDTO> ObtenerEnunciadoPreguntaPorIdPGeneral(int idPGeneral);
        IEnumerable<GrupoPreguntaProgramaCapacitacionDTO> ObtenerConfiguracionGrupoPreguntasEstructura(int idPGeneral, int seccion, int fila);
        Task<IEnumerable<ReporteExcelPreguntasInteractivasPrevioDTO>> ObtenerReportePreguntasInteractivasExportacionExcel();
        PreguntaProgramaCapacitacion ObtenerPorId(int id);
        IEnumerable<PreguntaProgramaCapacitacion> ObtenerPorIdPGeneralYGrupoPregunta(int idPGeneral, string grupoPregunta);
        IEnumerable<ListadoPreguntaPorEstructuraDTO> ObtenerPorEstructura(int idPgeneral, string grupoPregunta);
        List<PreguntaProgramaCapacitacionRegistradaDTO> ObtenerPreguntasRegistradas();
        List<PreguntaProgramaCapacitacionDificultadDTO> ObtenerDificultades();
        void ActualizarDificultad(int id, int idPreguntaProgramaCapacitacionDificultad, string usuarioModificacion);
        PreguntaProgramaCapacitacionDificultadDTO ObtenerDificultadPorIdPregunta(int id);
    }
}
