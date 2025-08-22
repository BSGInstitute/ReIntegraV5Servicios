using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{

    public class CampaniaGeneralDetalleEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public int Prioridad { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public string Usuario { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }

    public class CampaniaGeneralDetalleDTO
    {

        public int Id { get; set; }

        public int IdCampaniaGeneral { get; set; }

        public string Nombre { get; set; } = null!;

        public int Prioridad { get; set; }

        public string Asunto { get; set; } = null!;

        public int IdPersonal { get; set; }

        public int? IdCentroCosto { get; set; }

        public int? CantidadContactosMailing { get; set; }

        public int? CantidadContactosWhatsapp { get; set; }

        public bool? NoIncluyeWhatsaap { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }

        public int? IdConjuntoAnuncio { get; set; }

        public bool EnEjecucion { get; set; }
        public string UrlFormulario { get; set; }

    }
    public class CampaniaGeneralDetalleAreaSubAreaPrograma
    {
        public int? IdAreaCapacitacion { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
        public int? Prioridad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public int? IdPersonal { get; set; }
        public bool NoIncluyeWhatsaap { get; set; }
        public bool Estado { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int? Orden { get; set; }
        public string? urlFormulario { get; set; }
        public bool EnEjecucion { get; set; }
        public int? Id { get; set; }

    }
    public class CampaniaGeneralDetalleAreaSubAreaProgramaReturn
    {
        public int? IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
        public int? Prioridad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public int? IdPersonal { get; set; }
        public bool NoIncluyeWhatsaap { get; set; }
        public bool Estado { get; set; }
        public string? UrlFormulario { get; set; }
        public bool EnEjecucion { get; set; }
        public int? Id { get; set; }
        public List<int?> IdAreaCapacitacion { get; set; }
        public List<ProgramaGeneralReturnSqlList> IdCampaniaGeneralDetallePrograma { get; set; }
        public List<int?> IdSubAreaCapacitacion { get; set; }
    }
    public class ProgramaGeneralReturnSqlList
    {
        public int? id { get; set; }
        public int? idCampaniaGeneralDetalle { get; set; }
        public int? idPgeneral { get; set; }
        public string nombreProgramaGeneral { get; set; }
        public int? orden { get; set; }
    }

    public class CmapnaiGeneralDetalleCompleto  
    {
        public int IdCampaniageneral { get; set; }
        public String Nombre { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdHoraEnvio_Mailing { get; set; }
        public int? IdTipoAsociacion { get; set; }
        public int? IdProbabilidadRegistro_PW { get; set; }
        public int? NroMaximoSegmentos { get; set; }
        public int? CantidadPeriodoSinCorreo { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int? IdPlantilla_Mailing { get; set; }
        public bool? IncluyeWhatsapp { get; set; }
        public int? IdRemitenteMailing { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        public int? NumeroMinutosPrimerEnvio { get; set; }
        public int? IdHoraEnvio_Whatsapp { get; set; }
        public int? DiasSinWhatsapp { get; set; }
        public int? IdPlantilla_Whatsapp { get; set; }
        public bool? IncluirRebotes { get; set; }
        public int? IdEstadoEnvio_Mailing { get; set; }
        public int? IdEstadoEnvio_Whatsapp { get; set; }
        public int? idCampaniaGeneralDetalle { get; set; }
        public string NombreCampaniGeneralDetalle { get; set; }
        public int? Prioridad { get; set; }
        public string Asunto { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public bool? NoIncluyeWhatsaap { get; set; }
        public string urlFormulario { get; set; }
        public bool? EnEjecucion { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public bool? Estado { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? Orden { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdCampaniaGeneralDetallePrograma { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdResponsable { get; set; }
        public int? IdSubArea { get; set; }
    }
}
