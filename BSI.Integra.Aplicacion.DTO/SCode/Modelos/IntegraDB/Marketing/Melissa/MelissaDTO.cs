using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Melissa
{
    public class MelissaDTO
    {
        public class CodigoPaisIsoDTO
        {
            public int? CodigoPais { get; set; }
            public string CodigoISO { get; set; }
            
        }
        public class MelissaVerificacionDTO
        {
            public string Version { get; set; }
            public string TransmissionReference { get; set; }
            public string TransmissionResults { get; set; }
            public List<Records> Records { get; set; }
        }

        public class Records
        {
            public string RecordID { get; set; }
            public string Results { get; set; }
            public string PhoneNumber { get; set; }
            public string AdministrativeArea { get; set; }
            public string CountryAbbreviation { get; set; }
            public string CountryName { get; set; }
            public string Carrier { get; set; }
            public string CallerID { get; set; }
            public string DST { get; set; }
            public string InternationalPhoneNumber { get; set; }
            public string Language { get; set; }
            public string Latitude { get; set; }
            public string Locality { get; set; }
            public string Longitude { get; set; }
            public string PhoneInternationalPrefix { get; set; }
            public string PhoneCountryDialingCode { get; set; }
            public string PhoneNationPrefix { get; set; }
            public string PhoneNationalDestinationCode { get; set; }
            public string PhoneSubscriberNumber { get; set; }
            public string UTC { get; set; }
            public string TimeZoneCode { get; set; }
            public string TimeZoneName { get; set; }
            public string PostalCode { get; set; }
            public object Suggestions { get; set; } // Puede ser null, por eso se define como object
        }
    }
}
