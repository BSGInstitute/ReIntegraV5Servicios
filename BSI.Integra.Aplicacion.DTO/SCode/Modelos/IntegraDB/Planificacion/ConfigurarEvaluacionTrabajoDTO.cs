using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ConfigurarEvaluacionTrabajoDTO
    {
        public int? Id { get; set; }
        public int IdTipoEvaluacionTrabajo { get; set; }
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(750)]
        public string Descripcion { get; set; } = null!;
        public int? IdDocumentoPw { get; set; }
        [StringLength(150)]
        public string? ArchivoNombre { get; set; }
        [StringLength(750)]
        public string? ArchivoCarpeta { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccion { get; set; }
        public int? Fila { get; set; }
        [StringLength(750)]
        public string? DescripcionPregunta { get; set; }
        public int? OrdenCapitulo { get; set; }
        public bool? HabilitarInstrucciones { get; set; }
        public bool? HabilitarArchivo { get; set; }
        public bool? HabilitarPreguntas { get; set; }
        public int? OrdenEvaluacion { get; set; }
        public List<PreguntaEvaluacionTrabajoDTO> PreguntaEvaluacionTrabajos { get; set; }
    }
    public class ConfigurarEvaluacionTrabajoDetalleDTO
    {
        public int Id { get; set; }
        public int IdTipoEvaluacionTrabajo { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreTipoEvaluacion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int? IdDocumentoPw { get; set; }
        public string? ArchivoNombre { get; set; }
        public string? ArchivoCarpeta { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSeccion { get; set; }
        public int? Fila { get; set; }
        public string? DescripcionPregunta { get; set; }
        public int? OrdenCapitulo { get; set; }
        public bool? HabilitarInstrucciones { get; set; }
        public bool? HabilitarArchivo { get; set; }
        public bool? HabilitarPreguntas { get; set; }
    }
}