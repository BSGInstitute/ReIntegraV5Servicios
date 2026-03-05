using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class DocumentoPw : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int IdPlantillaPw { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
        public string? UrlArchivoInstruccionTarea { get; set; }
        public string? UrlArchivoCalificacionExcelente { get; set; }
        public List<PGeneralDocumentoPw> PGeneralDocumentoPws { get; set; }
        public List<DocumentoSeccionPw> DocumentoSeccionPws { get; set; }
        public List<BandejaPendientePw> BandejaPendientePws { get; set; }
    }
}
