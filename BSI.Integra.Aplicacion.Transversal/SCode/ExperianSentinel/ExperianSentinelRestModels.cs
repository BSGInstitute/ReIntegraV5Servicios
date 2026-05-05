using System.Text.Json.Serialization;

namespace BSI.Integra.Aplicacion.Transversal.ExperianSentinel
{
    // ──────────────────────────────────────────────────────────────────────────
    // Modelos C# que mapean 1:1 la respuesta JSON de la API REST de Experian.
    // Nombres de propiedades usan JsonPropertyName para preservar los nombres
    // exactos del JSON sin renombrar campos en C#.
    // ──────────────────────────────────────────────────────────────────────────

    public class ExperianRespuestaRaiz
    {
        [JsonPropertyName("respuesta")]
        public ExperianRespuesta? Respuesta { get; set; }

        [JsonPropertyName("respuestaPersonalizada")]
        public string? RespuestaPersonalizada { get; set; }

        [JsonPropertyName("CodigoWS")]
        public string? CodigoWS { get; set; }
    }

    public class ExperianRespuesta
    {
        [JsonPropertyName("InfBas")]
        public ExperianInfBas? InfBas { get; set; }

        [JsonPropertyName("InfGen")]
        public ExperianInfGen? InfGen { get; set; }

        [JsonPropertyName("ConRap")]
        public ExperianConRap? ConRap { get; set; }
    }

    /// <summary>Informacion basica del consultado</summary>
    public class ExperianInfBas
    {
        [JsonPropertyName("TDoc")]
        public string? TDoc { get; set; }

        [JsonPropertyName("NDoc")]
        public string? NDoc { get; set; }

        [JsonPropertyName("ApePat")]
        public string? ApePat { get; set; }

        [JsonPropertyName("ApeMat")]
        public string? ApeMat { get; set; }

        [JsonPropertyName("Nom")]
        public string? Nom { get; set; }

        [JsonPropertyName("RazSoc")]
        public string? RazSoc { get; set; }

        [JsonPropertyName("NomCom")]
        public string? NomCom { get; set; }

        [JsonPropertyName("TipCon")]
        public string? TipCon { get; set; }

        [JsonPropertyName("IniAct")]
        public string? IniAct { get; set; }

        [JsonPropertyName("CIIU")]
        public string? CIIU { get; set; }

        [JsonPropertyName("ActEco")]
        public string? ActEco { get; set; }

        [JsonPropertyName("FchInsRRPP")]
        public string? FchInsRRPP { get; set; }

        [JsonPropertyName("NumParReg")]
        public string? NumParReg { get; set; }

        [JsonPropertyName("Fol")]
        public string? Fol { get; set; }

        [JsonPropertyName("Asi")]
        public string? Asi { get; set; }

        [JsonPropertyName("AgeRet")]
        public string? AgeRet { get; set; }

        [JsonPropertyName("EstConC")]
        public string? EstConC { get; set; }

        [JsonPropertyName("EstCon")]
        public string? EstCon { get; set; }

        [JsonPropertyName("EstDomC")]
        public string? EstDomC { get; set; }

        [JsonPropertyName("EstDom")]
        public string? EstDom { get; set; }

        [JsonPropertyName("RelTDoc")]
        public string? RelTDoc { get; set; }

        [JsonPropertyName("RelNDoc")]
        public string? RelNDoc { get; set; }
    }

    /// <summary>Informacion general: direcciones y representantes legales</summary>
    public class ExperianInfGen
    {
        [JsonPropertyName("Direcc")]
        public List<ExperianDireccion>? Direcc { get; set; }

        [JsonPropertyName("RepLeg")]
        public List<ExperianRepLeg>? RepLeg { get; set; }

        [JsonPropertyName("RepLegDe")]
        public List<ExperianRepLegDe>? RepLegDe { get; set; }
    }

    public class ExperianDireccion
    {
        [JsonPropertyName("Direccion")]
        public string? Direccion { get; set; }

        [JsonPropertyName("Fuente")]
        public string? Fuente { get; set; }
    }

    public class ExperianRepLeg
    {
        [JsonPropertyName("TDoc")]
        public string? TDoc { get; set; }

        [JsonPropertyName("NDoc")]
        public string? NDoc { get; set; }

        [JsonPropertyName("Nombre")]
        public string? Nombre { get; set; }

        [JsonPropertyName("FecIniCar")]
        public string? FecIniCar { get; set; }

        [JsonPropertyName("Cargo")]
        public string? Cargo { get; set; }

        [JsonPropertyName("SemAct")]
        public string? SemAct { get; set; }

        [JsonPropertyName("SemPre")]
        public string? SemPre { get; set; }

        [JsonPropertyName("SemPM12")]
        public string? SemPM12 { get; set; }
    }

    public class ExperianRepLegDe
    {
        [JsonPropertyName("TDoc")]
        public string? TDoc { get; set; }

        [JsonPropertyName("NDoc")]
        public string? NDoc { get; set; }

        [JsonPropertyName("FecIniCar")]
        public string? FecIniCar { get; set; }

        [JsonPropertyName("Cargo")]
        public string? Cargo { get; set; }
        [JsonPropertyName("Estado")]
        public string? Estado { get; set; }

        [JsonPropertyName("SemAct")]
        public string? SemAct { get; set; }

        [JsonPropertyName("SemPre")]
        public string? SemPre { get; set; }

        [JsonPropertyName("SemPM12")]
        public string? SemPM12 { get; set; }
    }

    /// <summary>Consolidado rapido: resumen, deuda SBS, vencidos, lineas de credito</summary>
    public class ExperianConRap
    {
        [JsonPropertyName("Resumen_ConRap")]
        public ExperianResumenConRap? Resumen_ConRap { get; set; }

        [JsonPropertyName("IndCre")]
        public List<ExperianIndCre>? IndCre { get; set; }

        [JsonPropertyName("DeuSBSMicro")]
        public List<ExperianDeuSBSMicro>? DeuSBSMicro { get; set; }

        [JsonPropertyName("DetVen")]
        public List<ExperianDetVen>? DetVen { get; set; }

        [JsonPropertyName("UtiLinCre")]
        public List<ExperianUtiLinCre>? UtiLinCre { get; set; }

        [JsonPropertyName("ResDocPro")]
        public List<object>? ResDocPro { get; set; }
    }

    public class ExperianResumenConRap
    {
        [JsonPropertyName("FechaProceso")]
        public string? FechaProceso { get; set; }

        [JsonPropertyName("Semaforos")]
        public string? Semaforos { get; set; }

        [JsonPropertyName("Nota")]
        public string? Nota { get; set; }

        [JsonPropertyName("NroEntFin")]
        public string? NroEntFin { get; set; }

        [JsonPropertyName("NEntFinNR")]
        public string? NEntFinNR { get; set; }

        [JsonPropertyName("SemaActual")]
        public string? SemaActual { get; set; }

        [JsonPropertyName("SemaPrevio")]
        public string? SemaPrevio { get; set; }

        [JsonPropertyName("SemaPeorMejor")]
        public string? SemaPeorMejor { get; set; }

        [JsonPropertyName("DeudaTotal")]
        public string? DeudaTotal { get; set; }

        [JsonPropertyName("Calificativo")]
        public string? Calificativo { get; set; }

        [JsonPropertyName("DeudaTributaria")]
        public string? DeudaTributaria { get; set; }

        [JsonPropertyName("DeudaLaboral")]
        public string? DeudaLaboral { get; set; }

        [JsonPropertyName("DeudaImpaga")]
        public string? DeudaImpaga { get; set; }

        [JsonPropertyName("DeudaProtestos")]
        public string? DeudaProtestos { get; set; }

        [JsonPropertyName("DeudaSBSMicrof")]
        public string? DeudaSBSMicrof { get; set; }
    }

    public class ExperianIndCre
    {
        [JsonPropertyName("Indicador")]
        public string? Indicador { get; set; }
    }

    /// <summary>Historico de deuda SBS/Microfinanciero por anio y mes</summary>
    public class ExperianDeuSBSMicro
    {
        [JsonPropertyName("Ano")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? Ano { get; set; }

        [JsonPropertyName("Mes")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? Mes { get; set; }

        [JsonPropertyName("Califi")]
        public string? Califi { get; set; }

        [JsonPropertyName("NroEnt")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? NroEnt { get; set; }

        [JsonPropertyName("Entidad")]
        public List<ExperianEntidadSBS>? Entidad { get; set; }
    }

    public class ExperianEntidadSBS
    {
        [JsonPropertyName("NomEnt")]
        public string? NomEnt { get; set; }

        [JsonPropertyName("Cal")]
        public string? Cal { get; set; }

        [JsonPropertyName("SalDeu")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? SalDeu { get; set; }

        [JsonPropertyName("DiaVen")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? DiaVen { get; set; }

        [JsonPropertyName("FchPro")]
        public string? FchPro { get; set; }

        [JsonPropertyName("FchRep")]
        public string? FchRep { get; set; }
    }

    /// <summary>Detalle de vencidos agrupado por anio, mes y fuente</summary>
    public class ExperianDetVen
    {
        [JsonPropertyName("Ano")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? Ano { get; set; }

        [JsonPropertyName("Mes")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? Mes { get; set; }

        [JsonPropertyName("Fuentes")]
        public List<ExperianFuente>? Fuentes { get; set; }
    }

    public class ExperianFuente
    {
        [JsonPropertyName("IdFue")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? IdFue { get; set; }

        [JsonPropertyName("NomFue")]
        public string? NomFue { get; set; }

        [JsonPropertyName("VenTot")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? VenTot { get; set; }

        [JsonPropertyName("MaxDiaVen")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? MaxDiaVen { get; set; }

        [JsonPropertyName("Entidad")]
        public List<ExperianEntidadVencida>? Entidad { get; set; }
    }

    public class ExperianEntidadVencida
    {
        [JsonPropertyName("NomEnt")]
        public string? NomEnt { get; set; }

        [JsonPropertyName("MontDeu")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? MontDeu { get; set; }

        [JsonPropertyName("DiaVen")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? DiaVen { get; set; }

        [JsonPropertyName("NumDoc")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public int? NumDoc { get; set; }

        [JsonPropertyName("TipEnt")]
        public string? TipEnt { get; set; }

        [JsonPropertyName("FlgCont")]
        public string? FlgCont { get; set; }
    }

    /// <summary>Utilizacion de lineas de credito por institucion</summary>
    public class ExperianUtiLinCre
    {
        [JsonPropertyName("Inst")]
        public string? Inst { get; set; }

        [JsonPropertyName("LinApr")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? LinApr { get; set; }

        [JsonPropertyName("LinNoUti")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? LinNoUti { get; set; }

        [JsonPropertyName("LinUti")]
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
        public decimal? LinUti { get; set; }

        [JsonPropertyName("TipCred")]
        public string? TipCred { get; set; }
    }

    /// <summary>DTO para la solicitud de token OAuth2 de Experian</summary>
    public class ExperianTokenRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }

    /// <summary>DTO para la respuesta del token OAuth2 de Experian</summary>
    public class ExperianTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public string? ExpiresIn { get; set; }
    }

    /// <summary>DTO para la solicitud de consulta de historial crediticio</summary>
    public class ExperianConsultaRequest
    {
        [JsonPropertyName("TipDoc")]
        public string TipDoc { get; set; } = null!;

        [JsonPropertyName("NroDoc")]
        public string NroDoc { get; set; } = null!;
    }
}
