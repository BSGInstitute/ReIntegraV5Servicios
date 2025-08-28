using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IPreguntumService
    {
        #region Metodos Base
        Preguntum Add(Preguntum entidad);
        Preguntum Update(Preguntum entidad);
        bool Delete(int id, string usuario);
        List<Preguntum> Add(List<Preguntum> listadoEntidad);
        List<Preguntum> Update(List<Preguntum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<BancoPreguntumDTO> ObtenerPreguntaEncuestaAsincronica();
        IEnumerable<DTO.SCode.Modelos.IntegraDB.PreguntaRegistradaDTO> Obtener();
        IEnumerable<PreguntaTipoRespuestaDTO> ObtenerComboTipoPregunta();
        bool InsertarPregunta(CompuestoPreguntaDTO dto,string usuario);
        IEnumerable<TipoRespuestaCalificacionDTO> ObtenerTipoRespuestaCategoria();
        IEnumerable<RespuestaPreguntaDTO> ObtenerRespuestaPregunta(int id);
        bool ActualizarPregunta(CompuestoPreguntaDTO dto, string usuario);
        bool EliminarPregunta(int id, string usuario);
        ImportarExcelRespuestaDTO ImportarExcel(RespuestaPreguntaImportadaDTO Dto, string usuario);
    }
}