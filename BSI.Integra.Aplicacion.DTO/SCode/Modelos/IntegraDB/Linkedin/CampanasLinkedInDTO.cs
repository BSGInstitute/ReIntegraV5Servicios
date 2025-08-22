using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin
{
    public class CampanasLinkedInDTO
    {
        public bool StoryDeliveryEnabled { get; set; }
        public TargetingCriteria TargetingCriteria { get; set; }
        public string PacingStrategy { get; set; }
        public Locale Locale { get; set; }
        public string Type { get; set; }
        public string OptimizationTargetType { get; set; }
        public RunScheduleCampaignDTO RunSchedule { get; set; }
        public ChangeAuditStampsCampaignDTO ChangeAuditStamps { get; set; }
        public string CostType { get; set; }
        public string CreativeSelection { get; set; }
        public bool OffsiteDeliveryEnabled { get; set; }
        public long Id { get; set; }
        public bool AudienceExpansionEnabled { get; set; }
        public bool Test { get; set; }
        public string Format { get; set; }
        public List<string> ServingStatuses { get; set; }
        public Version Version { get; set; }
        public string ObjectiveType { get; set; }
        public string AssociatedEntity { get; set; }
        public OffsitePreferences OffsitePreferences { get; set; }
        public string CampaignGroup { get; set; }
        public Budget DailyBudget { get; set; }
        public Budget UnitCost { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Status { get; set; }
    }

    public class TargetingCriteria
    {
        public Include Include { get; set; }
        public Exclude Exclude { get; set; }
    }

    public class Include
    {
        public List<And> And { get; set; }
    }

    public class And
    {
        public Or Or { get; set; }
    }

    public class Or
    {
        public List<string> Titles { get; set; }
        public List<string> Industries { get; set; }
        public List<string> Skills { get; set; }
        public List<string> ProfileLocations { get; set; }
        public List<string> InterfaceLocales { get; set; }
    }

    public class Exclude
    {
        public Or Or { get; set; }
    }

    public class Locale
    {
        public string Country { get; set; }
        public string Language { get; set; }
    }

    public class RunScheduleCampaignDTO
    {
        public long Start { get; set; }
    }

    public class ChangeAuditStampsCampaignDTO
    {
        public CreatedCampaignDto Created { get; set; }
        public LastModifiedDTO LastModified { get; set; }
    }

    public class CreatedCampaignDto
    {
        public string Actor { get; set; }
        public long Time { get; set; }
    }

    public class LastModifiedDTO
    {
        public string Actor { get; set; }
        public long Time { get; set; }
    }

    public class Version
    {
        public string VersionTag { get; set; }
    }

    public class OffsitePreferences
    {
        public IabCategories IabCategories { get; set; }
        public PublisherRestrictionFiles PublisherRestrictionFiles { get; set; }
    }

    public class IabCategories
    {
        public List<object> Exclude { get; set; }
    }

    public class PublisherRestrictionFiles
    {
        public List<object> Include { get; set; }
        public List<object> Exclude { get; set; }
    }

    public class Budget
    {
        public string CurrencyCode { get; set; }
        public string Amount { get; set; }
    }
    public class CampaignLinkedInDTO
    {
        public long? Id { get; set; }
        public long? IdLinkedInGroupCampaign { get; set; }
        public string Nombre { get; set; }
        public string? Status { get; set; }
        public string? ObjectiveType { get; set; }
        public string? PacingStrategy { get; set; }
        public string? TypeCampaign { get; set; }
        public DateTime? UltimaModificacion { get; set; }





    }


}
