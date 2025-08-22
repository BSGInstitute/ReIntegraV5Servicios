using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin
{
    public class FormularioFormDTO 
    {

        public reviewInfoDTO? reviewInfo { get; set; }
        public changeAuditStampsDTO? changeAuditStamps { get; set; }
        public FormDTO? form { get; set; }
        public createdDTO? created { get; set; }
        public string? versionTag { get; set; }
        public int? id { get; set; }
        public lastModifiedDTO? lastModified { get; set; }
        public lastModifiedDTO? locale { get; set; }
        public versionDTO? version { get; set; }
        public string? account { get; set; }
        public string? status { get; set; }

    }


    public class reviewInfoDTO
    {
        public List<string>? rejectionReasons { get; set; }
        public long? reviewedAt { get; set; }
        public string? reviewStatus { get; set; }
    }





    public class changeAuditStampsDTO
    {
        public createdDTO? created { get; set; }
        public lastModifiedDTO? lastModified { get; set; }
    }

    public class createdDTO
    {
        public long? time { get; set; }
    }

    public class lastModifiedDTO
    {
        public long? time { get; set; }
    }
    public class localeDTO
    {
        public string? country { get; set; }
        public string? language { get; set; }
    }
    public class versionDTO
    {
        public string? versionTag { get; set; }
    }
    public class FormDTO
    {
        public List<hiddenFieldsDTO>? hiddenFields { get; set; }
        public string? thankYouPageCallToAction { get; set; }

        public string? privacyPolicy { get; set; }
        public string? landingPage { get; set; }
        public List<ConsentDTO>? consents { get; set; }
        public List<QuestionDTO>? questions { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? thankYouMessage { get; set; }
        public string? headline { get; }

    }

    public class hiddenFieldsDTO
    {
        public string? name { get; set; }
        public string? value { get; set; }

    }
    public class ConsentDTO
    {
        public bool? consentRequired { get; set; }
        public int? consentId { get; set; }
        public string? content { get; set; }
    }
    public class QuestionDTO
    {
        public string? name { get; set; }
        public string? predefinedField { get; set; }
        public int? questionId { get; set; }
        public string? question { get; set; }
        public string? memberProfileField { get; set; }

        public TypeSpecificQuestionDetails TypeSpecificQuestionDetails { get; set; }


    }
    public class TypeSpecificQuestionDetails
    {
        [JsonProperty("com.linkedin.ads.TextQuestionDetails")]
        public TextQuestionDetails ComLinkedinAdsTextQuestionDetails { get; set; }
        [JsonProperty("com.linkedin.ads.MultipleChoiceQuestionDetails")]
        public MultipleChoiceQuestionDetails ComLinkedinAdsMultipleChoiceQuestionDetails { get; set; }
    }


    public class TextQuestionDetails
    {
        
    }

    public class MultipleChoiceQuestionDetails
    {
        [JsonProperty("options")]
        public List<Option>? Options { get; set; }
    }

    public class Option
    {
        [JsonProperty("com.linkedin.ads.TextOptionQuestion")]
        public TextOptionQuestion? ComLinkedinAdsTextOptionQuestion { get; set; }
    }

    public class TextOptionQuestion
    {
        public string? Text { get; set; }
    }

    public class FormLinkedinDTO
    {
        public int? Id {  get; set; }
        public string? Form_name { get; set; }
        public string? Form_description { get; set; }
        public string? Form_headline { get; set; }
        public string? Question {  get; set; }
        public int? PaddingStart { get; set; }
    }
    public class QuestionFormDTO
    {
        public int? IdLinkedInForm { get; set; }
        public int? IdQuestionForm { get; set; }
        public string? Question { get; set; }
        public int? IdMultiSelect { get; set; }
        public string? Respuesta { get; set; }
    }


    public class FormStart
    {
        public int? IdLinkedInForm { get; set; }
        public int? value { get; set; }
    }

}
