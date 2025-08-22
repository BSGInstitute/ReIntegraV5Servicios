using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class CampaniaMailingDetalleConProgramasDTO
    {
        public int Id { get; set; }
        public int Prioridad { get; set; }
        public int Tipo { get; set; }
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string Subject { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdHoraEnvio { get; set; }
        public int Proveedor { get; set; }
        public int EstadoEnvio { get; set; }
        public int IdFiltroSegmento { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string Campania { get; set; }
        public string CodMailing { get; set; }
        public int? CantidadContactos { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdCampaniaMailingDetallePrograma { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public string TipoPrograma { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? CantidadSubidosMailChimp { get; set; }
        public int? IdCentroCosto { get; set; }
    }
    public class CampaniaGeneralDetalleConProgramasDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public bool? EnEjecucion { get; set; }
        public bool? NoIncluyeWhatsaap { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdCampaniaGeneralDetallePrograma { get; set; }
        public int? IdPGeneral { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? CantidadSubidosMailChimp { get; set; }
        public int? IdCampaniaGeneralDetalleResponsable { get; set; }
        public int? IdResponsable { get; set; }
        public int? Dia1 { get; set; }
        public int? Dia2 { get; set; }
        public int? Dia3 { get; set; }
        public int? Dia4 { get; set; }
        public int? Dia5 { get; set; }
        public int? Total { get; set; }
    }
}
