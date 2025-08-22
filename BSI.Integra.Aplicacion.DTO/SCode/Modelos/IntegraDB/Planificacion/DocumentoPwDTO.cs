using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class DocumentoPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPlantillaPw { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
    }
    public class DocumentoAsociadoProgramaDTO
    {
        public List<PGeneralDocumentoPwDTO> PGeneralDocumentoPws { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class CompuestoDocumentoPwDTO
    {
        public DocumentoPwDTO ObjetoDocumento { get; set; }
        public List<DocumentoSeccionPwFiltroDTO> Lista { get; set; }
        public List<DocumentoPwVersionesDTO>? ListaIntroduccionBeneficios { get; set; }
        //public List<RevisionNivelPwFiltroIdPlantillaDTO> ListaRevision { get; set; }
        //public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionPresencial { get; set; }
        //public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionOnline { get; set; }
        //public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionAOnline { get; set; }
    }
    public class CompuestoDocumentoDTO
    {
        public DocumentoPwDTO ObjetoDocumento { get; set; }
        public List<SeccionPwFiltroPlantillaPwDTO> Lista { get; set; }
        //public List<RevisionNivelPwFiltroIdPlantillaDTO> ListaRevision { get; set; }
    }
    public class RevisionNivelPwFiltroIdPlantillaDTO
    {
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdDocumento { get; set; }
        public int Cambio { get; set; }
    }


    public class CursoHijoDuracionPdusDTO
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string CodigoPartner { get; set; }
        public int? CantidadPdus { get; set; }
    }
}
