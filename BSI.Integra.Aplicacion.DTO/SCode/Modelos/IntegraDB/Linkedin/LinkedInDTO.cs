using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin
{
    public class LinkedInLeadDTO
    {
        public string Form { get; set; }
        public string LeadType { get; set; }
        public string Campaign { get; set; }
        public string Id { get; set; }
        public string Creative { get; set; }
        public string Account { get; set; }
        public bool TestLead { get; set; }
        public FormResponseDto FormResponse { get; set; }
    }

    public class FormResponseDto
    {
        public List<ConsentResponseDto> ConsentResponses { get; set; }
        public long SubmittedAt { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class ConsentResponseDto
    {
        // Define las propiedades necesarias
    }

    public class AnswerDto
    {
        public string Question { get; set; }
        public AnswerDetailsDto AnswerDetails { get; set; }
    }

    public class AnswerDetailsDto
    {
        [JsonProperty("com.linkedin.ads.TextQuestionAnswer")]
        public TextQuestionAnswerDto? TextQuestionAnswer { get; set; }
        [JsonProperty("com.linkedin.ads.MultipleChoiceAnswer")]
        public MultipleChoiceAnswerDto? MultipleChoiceAnswer { get; set; }
    }

    public class TextQuestionAnswerDto
    {
        public string? Answer { get; set; }
    }

    public class MultipleChoiceAnswerDto
    {
        [JsonProperty("options")]
        public List<TextOptionAnswerDto> Options { get; set; }
    }

    public class TextOptionAnswerDto
    {
        [JsonProperty("com.linkedin.ads.TextOptionAnswer")]
        public IndexDto IndexDto { get; set; }
    }

    public class IndexDto
    {
        public int Index { get; set; }
    }
    public class LinkedInLeadApiDTO
    {
        public string? IdLinkedInLead { get; set; }
        public int? IdLinkedInCampaign { get; set; }
        public int? IdLinkedInForm { get; set; }
        public string? LeadType { get; set; }
        public DateTime? FechaLead { get; set; }
        public string? Question { get; set; }
        public bool? TestLead { get; set; }
        

    }
    public class QuestionLeadDTO
    {
        public string? IdLinkedInLead { get; set; }
        public int? IdQuestionForm { get; set; }
        public int? IdLinkedInForm { get; set; }
        public string? Answer { get; set; }
    }


    public class InformacionLeadDTO
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Celular { get; set; }
        public string? Pais { get; set; }
        public string? Cargo { get; set; }
        public string? Formacion { get; set; }
        public string? Trabajo { get; set; }
        public string? Industria { get; set; }
        public string? CentroCosto { get; set; }
        public string? Origen { get; set; }
        public string? Asesor { get; set; }
        public string? TipoDato { get; set; }
        public string? FaseOportunindad { get; set; }

    }

    public class ValidacionOportunidadCreadaLeadDTO
    {
        public DateTime? Fecha { get; set; }
        public int? Registros { get; set; }
    }
    public class ReporteLeadsDTO
    {
        public string? GuidLinkedInLead { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Celular { get; set; }
        public string? Pais { get; set; }
        public string? Cargo { get; set; }
        public string? AreaFormacion { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? Industria { get; set; }
        public string? GrupoCampania { get; set; }
        public string? CentroCosto { get; set; }
        public string? Origen { get; set; }
        public bool? OportunidadRegistrada { get; set; }
        public DateTime? FechaLead { get; set; }
        public DateTime? FechaIntegra { get; set; }
        public int? CuentaAsociada { get; set; }
        public int? IdAlumno { get; set; }
        public string? UrlPerfilLinkedIn { get; set;}
    }


    public class FiltroLandingPagePortaLinkedInDTO
    {
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? IdTipoFecha { get; set; }
    }


    public class EstadoEnvioDTO
    {
        public int? Id { get; set; }
        public bool? EstadoEnvio { get; set; }
    }

    public class RegistroOportunidadAlumnoLinkedinDTO
    {
        public Alumno Alumno { get; set; }
        public Oportunidad Oportunidad { get; set; }
        public string Usuario { get; set; }
        public int? TipoInteraccion { get; set; }
        public int? TipoPersona { get; set; }
    }



    public class ReporteLeadsPendientesDTO
    {
        public string? GuidLinkedInLead { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Celular { get; set; }
        public string? Pais { get; set; }
        public string? Cargo { get; set; }
        public string? CargoOriginal { get; set; }
        public string? AreaFormacion { get; set; }
        public string? AreaFormacionOriginal { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? AreaTrabajoOriginal { get; set; }
        public string? Industria { get; set; }
        public string? IndustriaOriginal { get; set; }
        public string? GrupoCampania { get; set; }
        public string? CentroCosto { get; set; }
        public string? Origen { get; set; }
        public bool? OportunidadRegistrada { get; set; }
        public DateTime? FechaLead { get; set; }
        public DateTime? FechaIntegra { get; set; }
        public int CuentaAsociada { get; set; }
        public string? UrlPerfilLinkedIn { get; set; }  
    }

    public class LinkedInActualizarDTO
    {
        public string? GuidLinkedInLead { get; set; }
        public string? AreaFormacion { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? Cargo { get; set; }
        public string? Industria { get; set; }
        public string? Pais { get; set; }
        public string? UrlPerfil { get; set; }
    }


    public class QuestionLeadFormDTO
    {
        public string? GuidLinkedInLead { get; set; }
        public string? AreaFormacion { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? Cargo { get; set; }
        public string? Industria { get; set; }
        public string? Pais { get; set; }
        public string? Nombre { get; set; }
        public int Id { get; set; }
    }

}
