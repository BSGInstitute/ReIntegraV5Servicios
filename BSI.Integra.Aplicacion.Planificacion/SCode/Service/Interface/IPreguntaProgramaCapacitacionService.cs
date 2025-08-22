using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPreguntaProgramaCapacitacionService
    {
        Task<List<ReporteExcelPreguntasInteractivasDTO>> ObtenerReportePreguntasInteractivasExportacionExcel();
        List<RespuestaPreguntaProgramaCapacitacionDTO> ObtenerRespuestasPregunta(int IdPreguntaProgramaCapacitacion);
        List<PreguntaIntentoDetalleDTO> ObtenerPorIdPreguntaIntento(int idPreguntaIntento);
        bool Eliminar(int id, string usuario);
        bool Actualizar(CompuestoPreguntaProgramaCapacitacionDTO compuestoPreguntaProgramaCapacitacionDTO, string usuario);
        bool Insertar(CompuestoPreguntaProgramaCapacitacionDTO compuestoPreguntaProgramaCapacitacionDTO, string usuario);
        bool Eliminar(List<int> id, string usuario);
        PreguntaProgramaCapacitacionCombosModuloDTO ObtenerCombosModulo();
        List<CapituloSesionProgramaCapacitacionDTO> ObtenerCapituloSesionesPGeneral(int idPGeneral);
        bool ActualizarRespuestaPorSecuenciaVideo(List<GrupoPreguntaProgramaCapacitacionDTO> grupoPreguntaProgramaCapacitacions, string usuario);
        List<ListadoPreguntaPorEstructuraDTO> ObtenerPorEstructura(int idPgeneral, string grupoPregunta);
        List<PreguntaProgramaCapacitacionRegistradaDTO> ObtenerPreguntasRegistradas();
        ImportarExcelRespuestaDTO ImportarExcel(IFormFile archivo, string usuario);
        ImportarExcelRespuestaDTO ImportarRespuestasExcel(ImportarRespuestasExcelDTO importarRespuestasExcel, string usuario);
        List<CapituloSesionProgramaCapacitacionDTO> ObtenerCapituloSesionProgramaCapacitacion(int IdPGeneral);
    }
}
