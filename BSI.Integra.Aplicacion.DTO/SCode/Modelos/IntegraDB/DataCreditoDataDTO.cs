using System.Xml.Serialization;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DataCreditoDataDTO
    {
        public string NroDocumento { get; set; }
        public string ApellidoPaterno { get; set; }
        public int? Id { get; set; }
    }
    [XmlRoot(ElementName = "Identificacion")]
    public class Identificacion
    {
        [XmlAttribute(AttributeName = "estado")]
        public string Estado { get; set; }
        [XmlAttribute(AttributeName = "fechaExpedicion")]
        public string FechaExpedicion { get; set; }
        [XmlAttribute(AttributeName = "ciudad")]
        public string Ciudad { get; set; }
        [XmlAttribute(AttributeName = "departamento")]
        public string Departamento { get; set; }
        [XmlAttribute(AttributeName = "genero")]
        public string Genero { get; set; }
        [XmlAttribute(AttributeName = "numero")]
        public string Numero { get; set; }
        [XmlAttribute(AttributeName = "idRegistro")]
        public string IdRegistro { get; set; }
        [XmlAttribute(AttributeName = "lugarExpedicion")]
        public string LugarExpedicion { get; set; }
        [XmlAttribute(AttributeName = "nitReporta")]
        public string NitReporta { get; set; }
        [XmlAttribute(AttributeName = "razonSocial")]
        public string RazonSocial { get; set; }
    }

    [XmlRoot(ElementName = "Edad")]
    public class Edad
    {
        [XmlAttribute(AttributeName = "min")]
        public string Min { get; set; }
        [XmlAttribute(AttributeName = "max")]
        public string Max { get; set; }
    }

    [XmlRoot(ElementName = "ActividadEconomica")]
    public class ActividadEconomica
    {
        [XmlAttribute(AttributeName = "idRegistro")]
        public string IdRegistro { get; set; }
        [XmlAttribute(AttributeName = "tipo")]
        public string Tipo { get; set; }
        [XmlAttribute(AttributeName = "CIIU")]
        public string CIIU { get; set; }
        [XmlAttribute(AttributeName = "estado")]
        public string Estado { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "nitReporta")]
        public string NitReporta { get; set; }
        [XmlAttribute(AttributeName = "razonSocial")]
        public string RazonSocial { get; set; }
    }

    [XmlRoot(ElementName = "OperacionesInternacionales")]
    public class OperacionesInternacionales
    {
        [XmlAttribute(AttributeName = "idRegistro")]
        public string IdRegistro { get; set; }
        [XmlAttribute(AttributeName = "operaInt")]
        public string OperaInt { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "nitReporta")]
        public string NitReporta { get; set; }
        [XmlAttribute(AttributeName = "razonSocial")]
        public string RazonSocial { get; set; }
    }

    [XmlRoot(ElementName = "InfoDemografica")]
    public class InfoDemografica
    {
        [XmlElement(ElementName = "ActividadEconomica")]
        public List<ActividadEconomica> ActividadEconomica { get; set; }
        [XmlElement(ElementName = "OperacionesInternacionales")]
        public List<OperacionesInternacionales> OperacionesInternacionales { get; set; }
        [XmlElement(ElementName = "Identificacion")]
        public Identificacion Identificacion { get; set; }
    }

    [XmlRoot(ElementName = "NaturalNacional")]
    public class NaturalNacional
    {
        [XmlElement(ElementName = "Identificacion")]
        public Identificacion Identificacion { get; set; }
        [XmlElement(ElementName = "Edad")]
        public Edad Edad { get; set; }
        [XmlElement(ElementName = "InfoDemografica")]
        public InfoDemografica InfoDemografica { get; set; }
        [XmlAttribute(AttributeName = "nombres")]
        public string Nombres { get; set; }
        [XmlAttribute(AttributeName = "primerApellido")]
        public string PrimerApellido { get; set; }
        [XmlAttribute(AttributeName = "segundoApellido")]
        public string SegundoApellido { get; set; }
        [XmlAttribute(AttributeName = "nombreCompleto")]
        public string NombreCompleto { get; set; }
        [XmlAttribute(AttributeName = "validada")]
        public string Validada { get; set; }
        [XmlAttribute(AttributeName = "rut")]
        public string Rut { get; set; }
        [XmlAttribute(AttributeName = "genero")]
        public string Genero { get; set; }
    }

    [XmlRoot(ElementName = "Razon")]
    public class Razon
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
    }

    [XmlRoot(ElementName = "Score")]
    public class Score
    {
        [XmlElement(ElementName = "Razon")]
        public List<Razon> Razon { get; set; }
        [XmlAttribute(AttributeName = "tipo")]
        public string Tipo { get; set; }
        [XmlAttribute(AttributeName = "puntaje")]
        public string Puntaje { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "poblacion")]
        public string Poblacion { get; set; }
    }

    [XmlRoot(ElementName = "Caracteristicas")]
    public class Caracteristicas
    {
        [XmlAttribute(AttributeName = "clase")]
        public string Clase { get; set; }
        [XmlAttribute(AttributeName = "franquicia")]
        public string Franquicia { get; set; }
        [XmlAttribute(AttributeName = "marca")]
        public string Marca { get; set; }
        [XmlAttribute(AttributeName = "amparada")]
        public string Amparada { get; set; }
        [XmlAttribute(AttributeName = "codigoAmparada")]
        public string CodigoAmparada { get; set; }
        [XmlAttribute(AttributeName = "garantia")]
        public string Garantia { get; set; }
        [XmlAttribute(AttributeName = "tipoCuenta")]
        public string TipoCuenta { get; set; }
        [XmlAttribute(AttributeName = "tipoObligacion")]
        public string TipoObligacion { get; set; }
        [XmlAttribute(AttributeName = "tipoContrato")]
        public string TipoContrato { get; set; }
        [XmlAttribute(AttributeName = "ejecucionContrato")]
        public string EjecucionContrato { get; set; }
        [XmlAttribute(AttributeName = "mesesPermanencia")]
        public string MesesPermanencia { get; set; }
        [XmlAttribute(AttributeName = "calidadDeudor")]
        public string CalidadDeudor { get; set; }
    }

    [XmlRoot(ElementName = "Valor")]
    public class Valor
    {
        [XmlAttribute(AttributeName = "moneda")]
        public string Moneda { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "cantidadChequesDevueltos")]
        public string CantidadChequesDevueltos { get; set; }
        [XmlAttribute(AttributeName = "cantidadChequesPagados")]
        public string CantidadChequesPagados { get; set; }
        [XmlAttribute(AttributeName = "valorChequesDevueltos")]
        public string ValorChequesDevueltos { get; set; }
        [XmlAttribute(AttributeName = "valorChequesPagados")]
        public string ValorChequesPagados { get; set; }
        [XmlAttribute(AttributeName = "saldoActual")]
        public string SaldoActual { get; set; }
        [XmlAttribute(AttributeName = "saldoMora")]
        public string SaldoMora { get; set; }
        [XmlAttribute(AttributeName = "disponible")]
        public string Disponible { get; set; }
        [XmlAttribute(AttributeName = "cuota")]
        public string Cuota { get; set; }
        [XmlAttribute(AttributeName = "cuotasMora")]
        public string CuotasMora { get; set; }
        [XmlAttribute(AttributeName = "diasMora")]
        public string DiasMora { get; set; }
        [XmlAttribute(AttributeName = "fechaPagoCuota")]
        public string FechaPagoCuota { get; set; }
        [XmlAttribute(AttributeName = "fechaLimitePago")]
        public string FechaLimitePago { get; set; }
        [XmlAttribute(AttributeName = "cupoTotal")]
        public string CupoTotal { get; set; }
        [XmlAttribute(AttributeName = "periodicidad")]
        public string Periodicidad { get; set; }
        [XmlAttribute(AttributeName = "totalCuotas")]
        public string TotalCuotas { get; set; }
        [XmlAttribute(AttributeName = "valorInicial")]
        public string ValorInicial { get; set; }
        [XmlAttribute(AttributeName = "cuotasCanceladas")]
        public string CuotasCanceladas { get; set; }
        [XmlAttribute(AttributeName = "valor")]
        public string Valores { get; set; }
    }

    [XmlRoot(ElementName = "Valores")]
    public class Valores
    {
        [XmlElement(ElementName = "Valor")]
        public List<Valor> Valor { get; set; }
    }

    [XmlRoot(ElementName = "Estado")]
    public class Estado
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "cantidad")]
        public string Cantidad { get; set; }
    }

    [XmlRoot(ElementName = "CuentaAhorro")]
    public class CuentaAhorro
    {
        [XmlElement(ElementName = "Caracteristicas")]
        public Caracteristicas Caracteristicas { get; set; }
        [XmlElement(ElementName = "Valores")]
        public Valores Valores { get; set; }
        [XmlElement(ElementName = "Estado")]
        public Estado Estado { get; set; }
        [XmlElement(ElementName = "Llave")]
        public string Llave { get; set; }
        [XmlAttribute(AttributeName = "bloqueada")]
        public string Bloqueada { get; set; }
        [XmlAttribute(AttributeName = "entidad")]
        public string Entidad { get; set; }
        [XmlAttribute(AttributeName = "numero")]
        public string Numero { get; set; }
        [XmlAttribute(AttributeName = "fechaApertura")]
        public string FechaApertura { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "situacionTitular")]
        public string SituacionTitular { get; set; }
        [XmlAttribute(AttributeName = "oficina")]
        public string Oficina { get; set; }
        [XmlAttribute(AttributeName = "ciudad")]
        public string Ciudad { get; set; }
        [XmlAttribute(AttributeName = "codigoDaneCiudad")]
        public string CodigoDaneCiudad { get; set; }
        [XmlAttribute(AttributeName = "tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }
        [XmlAttribute(AttributeName = "identificacion")]
        public string Identificacion { get; set; }
        [XmlAttribute(AttributeName = "sector")]
        public string Sector { get; set; }
    }

    [XmlRoot(ElementName = "Sobregiro")]
    public class Sobregiro
    {
        [XmlAttribute(AttributeName = "valor")]
        public string Valor { get; set; }
        [XmlAttribute(AttributeName = "dias")]
        public string Dias { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
    }

    [XmlRoot(ElementName = "CuentaCorrienteDTO")]
    public class CuentaCorrienteDTO
    {
        [XmlElement(ElementName = "Caracteristicas")]
        public Caracteristicas Caracteristicas { get; set; }
        [XmlElement(ElementName = "Valores")]
        public Valores Valores { get; set; }
        [XmlElement(ElementName = "Estado")]
        public Estado Estado { get; set; }
        [XmlElement(ElementName = "Sobregiro")]
        public Sobregiro Sobregiro { get; set; }
        [XmlElement(ElementName = "Llave")]
        public string Llave { get; set; }
        [XmlAttribute(AttributeName = "bloqueada")]
        public string Bloqueada { get; set; }
        [XmlAttribute(AttributeName = "entidad")]
        public string Entidad { get; set; }
        [XmlAttribute(AttributeName = "numero")]
        public string Numero { get; set; }
        [XmlAttribute(AttributeName = "fechaApertura")]
        public string FechaApertura { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "situacionTitular")]
        public string SituacionTitular { get; set; }
        [XmlAttribute(AttributeName = "oficina")]
        public string Oficina { get; set; }
        [XmlAttribute(AttributeName = "ciudad")]
        public string Ciudad { get; set; }
        [XmlAttribute(AttributeName = "codigoDaneCiudad")]
        public string CodigoDaneCiudad { get; set; }
        [XmlAttribute(AttributeName = "codSuscriptor")]
        public string CodSuscriptor { get; set; }
        [XmlAttribute(AttributeName = "tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }
        [XmlAttribute(AttributeName = "identificacion")]
        public string Identificacion { get; set; }
        [XmlAttribute(AttributeName = "sector")]
        public string Sector { get; set; }
    }

    [XmlRoot(ElementName = "EstadoPlastico")]
    public class EstadoPlastico
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
    }

    [XmlRoot(ElementName = "EstadoCuenta")]
    public class EstadoCuenta
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
    }

    [XmlRoot(ElementName = "EstadoOrigen")]
    public class EstadoOrigen
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
    }

    [XmlRoot(ElementName = "EstadoPago")]
    public class EstadoPago
    {
        [XmlAttribute(AttributeName = "codigo")]
        public string Codigo { get; set; }
        [XmlAttribute(AttributeName = "meses")]
        public string Meses { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
    }

    [XmlRoot(ElementName = "Estados")]
    public class Estados
    {
        [XmlElement(ElementName = "EstadoPlastico")]
        public EstadoPlastico EstadoPlastico { get; set; }
        [XmlElement(ElementName = "EstadoCuenta")]
        public EstadoCuenta EstadoCuenta { get; set; }
        [XmlElement(ElementName = "EstadoOrigen")]
        public EstadoOrigen EstadoOrigen { get; set; }
        [XmlElement(ElementName = "EstadoPago")]
        public EstadoPago EstadoPago { get; set; }
    }

    [XmlRoot(ElementName = "TarjetaCredito")]
    public class TarjetaCredito
    {
        [XmlElement(ElementName = "Caracteristicas")]
        public Caracteristicas Caracteristicas { get; set; }
        [XmlElement(ElementName = "Valores")]
        public Valores Valores { get; set; }
        [XmlElement(ElementName = "Estados")]
        public Estados Estados { get; set; }
        [XmlElement(ElementName = "Llave")]
        public string Llave { get; set; }
        [XmlAttribute(AttributeName = "bloqueada")]
        public string Bloqueada { get; set; }
        [XmlAttribute(AttributeName = "entidad")]
        public string Entidad { get; set; }
        [XmlAttribute(AttributeName = "numero")]
        public string Numero { get; set; }
        [XmlAttribute(AttributeName = "fechaApertura")]
        public string FechaApertura { get; set; }
        [XmlAttribute(AttributeName = "fechaVencimiento")]
        public string FechaVencimiento { get; set; }
        [XmlAttribute(AttributeName = "comportamiento")]
        public string Comportamiento { get; set; }
        [XmlAttribute(AttributeName = "formaPago")]
        public string FormaPago { get; set; }
        [XmlAttribute(AttributeName = "probabilidadIncumplimiento")]
        public string ProbabilidadIncumplimiento { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "situacionTitular")]
        public string SituacionTitular { get; set; }
        [XmlAttribute(AttributeName = "ciudad")]
        public string Ciudad { get; set; }
        [XmlAttribute(AttributeName = "codigoDaneCiudad")]
        public string CodigoDaneCiudad { get; set; }
        [XmlAttribute(AttributeName = "tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }
        [XmlAttribute(AttributeName = "identificacion")]
        public string Identificacion { get; set; }
        [XmlAttribute(AttributeName = "sector")]
        public string Sector { get; set; }
        [XmlAttribute(AttributeName = "calificacionHD")]
        public string CalificacionHD { get; set; }
        [XmlAttribute(AttributeName = "oficina")]
        public string Oficina { get; set; }
    }

    [XmlRoot(ElementName = "CuentaCartera")]
    public class CuentaCartera
    {
        [XmlElement(ElementName = "Caracteristicas")]
        public Caracteristicas Caracteristicas { get; set; }
        [XmlElement(ElementName = "Valores")]
        public Valores Valores { get; set; }
        [XmlElement(ElementName = "Estados")]
        public Estados Estados { get; set; }
        [XmlElement(ElementName = "Llave")]
        public string Llave { get; set; }
        [XmlAttribute(AttributeName = "bloqueada")]
        public string Bloqueada { get; set; }
        [XmlAttribute(AttributeName = "entidad")]
        public string Entidad { get; set; }
        [XmlAttribute(AttributeName = "numero")]
        public string Numero { get; set; }
        [XmlAttribute(AttributeName = "fechaApertura")]
        public string FechaApertura { get; set; }
        [XmlAttribute(AttributeName = "fechaVencimiento")]
        public string FechaVencimiento { get; set; }
        [XmlAttribute(AttributeName = "comportamiento")]
        public string Comportamiento { get; set; }
        [XmlAttribute(AttributeName = "formaPago")]
        public string FormaPago { get; set; }
        [XmlAttribute(AttributeName = "probabilidadIncumplimiento")]
        public string ProbabilidadIncumplimiento { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "situacionTitular")]
        public string SituacionTitular { get; set; }
        [XmlAttribute(AttributeName = "oficina")]
        public string Oficina { get; set; }
        [XmlAttribute(AttributeName = "ciudad")]
        public string Ciudad { get; set; }
        [XmlAttribute(AttributeName = "codigoDaneCiudad")]
        public string CodigoDaneCiudad { get; set; }
        [XmlAttribute(AttributeName = "codSuscriptor")]
        public string CodSuscriptor { get; set; }
        [XmlAttribute(AttributeName = "tipoIdentificacion")]
        public string TipoIdentificacion { get; set; }
        [XmlAttribute(AttributeName = "identificacion")]
        public string Identificacion { get; set; }
        [XmlAttribute(AttributeName = "sector")]
        public string Sector { get; set; }
        [XmlAttribute(AttributeName = "calificacionHD")]
        public string CalificacionHD { get; set; }
    }

    [XmlRoot(ElementName = "Entidad")]
    public class Entidad
    {
        [XmlAttribute(AttributeName = "nombre")]
        public string Nombre { get; set; }
        [XmlAttribute(AttributeName = "nit")]
        public string Nit { get; set; }
        [XmlAttribute(AttributeName = "sector")]
        public string Sector { get; set; }
    }

    [XmlRoot(ElementName = "Garantia")]
    public class Garantia
    {
        [XmlAttribute(AttributeName = "tipo")]
        public string Tipo { get; set; }
        [XmlAttribute(AttributeName = "valor")]
        public string Valor { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
    }

    [XmlRoot(ElementName = "EndeudamientoGlobal")]
    public class EndeudamientoGlobal
    {
        [XmlElement(ElementName = "Entidad")]
        public Entidad Entidad { get; set; }
        [XmlElement(ElementName = "Garantia")]
        public Garantia Garantia { get; set; }
        [XmlElement(ElementName = "Llave")]
        public string Llave { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "fuente")]
        public string Fuente { get; set; }
        [XmlAttribute(AttributeName = "saldoPendiente")]
        public string SaldoPendiente { get; set; }
        [XmlAttribute(AttributeName = "tipoCredito")]
        public string TipoCredito { get; set; }
        [XmlAttribute(AttributeName = "moneda")]
        public string Moneda { get; set; }
        [XmlAttribute(AttributeName = "numeroCreditos")]
        public string NumeroCreditos { get; set; }
        [XmlAttribute(AttributeName = "independiente")]
        public string Independiente { get; set; }
        [XmlAttribute(AttributeName = "fechaReporte")]
        public string FechaReporte { get; set; }
    }

    [XmlRoot(ElementName = "Consulta")]
    public class Consulta
    {
        [XmlElement(ElementName = "Llave")]
        public string Llave { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "tipoCuenta")]
        public string TipoCuenta { get; set; }
        [XmlAttribute(AttributeName = "entidad")]
        public string Entidad { get; set; }
        [XmlAttribute(AttributeName = "oficina")]
        public string Oficina { get; set; }
        [XmlAttribute(AttributeName = "ciudad")]
        public string Ciudad { get; set; }
        [XmlAttribute(AttributeName = "razon")]
        public string Razon { get; set; }
        [XmlAttribute(AttributeName = "cantidad")]
        public string Cantidad { get; set; }
        [XmlAttribute(AttributeName = "nitSuscriptor")]
        public string NitSuscriptor { get; set; }
        [XmlAttribute(AttributeName = "sector")]
        public string Sector { get; set; }
    }

    [XmlRoot(ElementName = "productosValores")]
    public class ProductosValores
    {
        [XmlAttribute(AttributeName = "producto")]
        public string Producto { get; set; }
        [XmlAttribute(AttributeName = "valor1")]
        public string Valor1 { get; set; }
        [XmlAttribute(AttributeName = "valor2")]
        public string Valor2 { get; set; }
        [XmlAttribute(AttributeName = "valor3")]
        public string Valor3 { get; set; }
        [XmlAttribute(AttributeName = "valor4")]
        public string Valor4 { get; set; }
        [XmlAttribute(AttributeName = "valor5")]
        public string Valor5 { get; set; }
        [XmlAttribute(AttributeName = "valor6")]
        public string Valor6 { get; set; }
        [XmlAttribute(AttributeName = "valor7")]
        public string Valor7 { get; set; }
        [XmlAttribute(AttributeName = "valor8")]
        public string Valor8 { get; set; }
        [XmlAttribute(AttributeName = "valor9")]
        public string Valor9 { get; set; }
        [XmlAttribute(AttributeName = "valor10")]
        public string Valor10 { get; set; }
        [XmlAttribute(AttributeName = "valor1smlv")]
        public string Valor1smlv { get; set; }
        [XmlAttribute(AttributeName = "valor2smlv")]
        public string Valor2smlv { get; set; }
        [XmlAttribute(AttributeName = "valor3smlv")]
        public string Valor3smlv { get; set; }
        [XmlAttribute(AttributeName = "valor4smlv")]
        public string Valor4smlv { get; set; }
        [XmlAttribute(AttributeName = "valor5smlv")]
        public string Valor5smlv { get; set; }
        [XmlAttribute(AttributeName = "valor6smlv")]
        public string Valor6smlv { get; set; }
        [XmlAttribute(AttributeName = "valor7smlv")]
        public string Valor7smlv { get; set; }
        [XmlAttribute(AttributeName = "valor8smlv")]
        public string Valor8smlv { get; set; }
        [XmlAttribute(AttributeName = "valor9smlv")]
        public string Valor9smlv { get; set; }
        [XmlAttribute(AttributeName = "valor10smlv")]
        public string Valor10smlv { get; set; }
        [XmlAttribute(AttributeName = "razon1")]
        public string Razon1 { get; set; }
        [XmlAttribute(AttributeName = "razon2")]
        public string Razon2 { get; set; }
        [XmlAttribute(AttributeName = "razon3")]
        public string Razon3 { get; set; }
        [XmlAttribute(AttributeName = "razon4")]
        public string Razon4 { get; set; }
        [XmlAttribute(AttributeName = "razon5")]
        public string Razon5 { get; set; }
        [XmlAttribute(AttributeName = "razon6")]
        public string Razon6 { get; set; }
        [XmlAttribute(AttributeName = "razon7")]
        public string Razon7 { get; set; }
        [XmlAttribute(AttributeName = "razon8")]
        public string Razon8 { get; set; }
        [XmlAttribute(AttributeName = "razon9")]
        public string Razon9 { get; set; }
        [XmlAttribute(AttributeName = "razon10")]
        public string Razon10 { get; set; }
    }

    [XmlRoot(ElementName = "Principales")]
    public class Principales
    {
        [XmlAttribute(AttributeName = "creditoVigentes")]
        public string CreditoVigentes { get; set; }
        [XmlAttribute(AttributeName = "creditosCerrados")]
        public string CreditosCerrados { get; set; }
        [XmlAttribute(AttributeName = "creditosActualesNegativos")]
        public string CreditosActualesNegativos { get; set; }
        [XmlAttribute(AttributeName = "histNegUlt12Meses")]
        public string HistNegUlt12Meses { get; set; }
        [XmlAttribute(AttributeName = "cuentasAbiertasAHOCCB")]
        public string CuentasAbiertasAHOCCB { get; set; }
        [XmlAttribute(AttributeName = "cuentasCerradasAHOCCB")]
        public string CuentasCerradasAHOCCB { get; set; }
        [XmlAttribute(AttributeName = "consultadasUlt6meses")]
        public string ConsultadasUlt6meses { get; set; }
        [XmlAttribute(AttributeName = "desacuerdosALaFecha")]
        public string DesacuerdosALaFecha { get; set; }
        [XmlAttribute(AttributeName = "antiguedadDesde")]
        public string AntiguedadDesde { get; set; }
        [XmlAttribute(AttributeName = "reclamosVigentes")]
        public string ReclamosVigentes { get; set; }
    }

    [XmlRoot(ElementName = "Sector")]
    public class Sector
    {
        [XmlAttribute(AttributeName = "sector")]
        public string Sectores { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
        [XmlAttribute(AttributeName = "participacion")]
        public string Participacion { get; set; }
        [XmlElement(ElementName = "Cartera")]
        public List<Cartera> Cartera { get; set; }
        [XmlAttribute(AttributeName = "codigoSector")]
        public string CodigoSector { get; set; }
        [XmlAttribute(AttributeName = "garantiaAdmisible")]
        public string GarantiaAdmisible { get; set; }
        [XmlAttribute(AttributeName = "garantiaOtro")]
        public string GarantiaOtro { get; set; }
        [XmlElement(ElementName = "TipoCuenta")]
        public List<TipoCuenta> TipoCuenta { get; set; }
        [XmlAttribute(AttributeName = "codSector")]
        public string CodSector { get; set; }
        [XmlElement(ElementName = "Cuenta")]
        public List<Cuenta> Cuenta { get; set; }
        [XmlElement(ElementName = "MorasMaximas")]
        public MorasMaximas MorasMaximas { get; set; }
        [XmlAttribute(AttributeName = "nombreSector")]
        public string NombreSector { get; set; }
    }

    [XmlRoot(ElementName = "Mes")]
    public class Mes
    {
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "saldoTotalMora")]
        public string SaldoTotalMora { get; set; }
        [XmlAttribute(AttributeName = "saldoTotal")]
        public string SaldoTotal { get; set; }
        [XmlAttribute(AttributeName = "comportamiento")]
        public string Comportamiento { get; set; }
        [XmlAttribute(AttributeName = "cantidad")]
        public string Cantidad { get; set; }
    }

    [XmlRoot(ElementName = "Saldos")]
    public class Saldos
    {
        [XmlElement(ElementName = "Sector")]
        public List<Sector> Sector { get; set; }
        [XmlElement(ElementName = "Mes")]
        public List<Mes> Mes { get; set; }
        [XmlAttribute(AttributeName = "saldoTotalEnMora")]
        public string SaldoTotalEnMora { get; set; }
        [XmlAttribute(AttributeName = "saldoM30")]
        public string SaldoM30 { get; set; }
        [XmlAttribute(AttributeName = "saldoM60")]
        public string SaldoM60 { get; set; }
        [XmlAttribute(AttributeName = "saldoM90")]
        public string SaldoM90 { get; set; }
        [XmlAttribute(AttributeName = "cuotaMensual")]
        public string CuotaMensual { get; set; }
        [XmlAttribute(AttributeName = "saldoCreditoMasAlto")]
        public string SaldoCreditoMasAlto { get; set; }
        [XmlAttribute(AttributeName = "saldoTotal")]
        public string SaldoTotal { get; set; }
    }

    [XmlRoot(ElementName = "Comportamiento")]
    public class Comportamiento
    {
        [XmlElement(ElementName = "Mes")]
        public List<Mes> Mes { get; set; }
    }

    [XmlRoot(ElementName = "Resumen")]
    public class Resumen
    {
        [XmlElement(ElementName = "Principales")]
        public Principales Principales { get; set; }
        [XmlElement(ElementName = "Saldos")]
        public Saldos Saldos { get; set; }
        [XmlElement(ElementName = "Comportamiento")]
        public Comportamiento Comportamiento { get; set; }
        [XmlElement(ElementName = "PerfilGeneral")]
        public PerfilGeneral PerfilGeneral { get; set; }
        [XmlElement(ElementName = "VectorSaldosYMoras")]
        public VectorSaldosYMoras VectorSaldosYMoras { get; set; }
        [XmlElement(ElementName = "EndeudamientoActual")]
        public EndeudamientoActual EndeudamientoActual { get; set; }
        [XmlElement(ElementName = "ImagenTendenciaEndeudamiento")]
        public ImagenTendenciaEndeudamiento ImagenTendenciaEndeudamiento { get; set; }
    }

    [XmlRoot(ElementName = "TipoCuenta")]
    public class TipoCuenta
    {
        [XmlAttribute(AttributeName = "codigoTipo")]
        public string CodigoTipo { get; set; }
        [XmlAttribute(AttributeName = "tipo")]
        public string Tipo { get; set; }
        [XmlAttribute(AttributeName = "calidadDeudor")]
        public string CalidadDeudor { get; set; }
        [XmlAttribute(AttributeName = "cupo")]
        public string Cupo { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
        [XmlAttribute(AttributeName = "saldoMora")]
        public string SaldoMora { get; set; }
        [XmlAttribute(AttributeName = "cuota")]
        public string Cuota { get; set; }
        [XmlElement(ElementName = "Estado")]
        public Estado Estado { get; set; }
        [XmlAttribute(AttributeName = "porcentaje")]
        public string Porcentaje { get; set; }
        [XmlAttribute(AttributeName = "cantidad")]
        public string Cantidad { get; set; }
        [XmlElement(ElementName = "Trimestre")]
        public List<Trimestre> Trimestre { get; set; }
        [XmlElement(ElementName = "Usuario")]
        public List<UsuarioData> Usuario { get; set; }
        [XmlAttribute(AttributeName = "tipoCuenta")]
        public string TipoCuentas { get; set; }
    }

    [XmlRoot(ElementName = "Total")]
    public class Total
    {
        [XmlAttribute(AttributeName = "calidadDeudor")]
        public string CalidadDeudor { get; set; }
        [XmlAttribute(AttributeName = "participacion")]
        public string Participacion { get; set; }
        [XmlAttribute(AttributeName = "cupo")]
        public string Cupo { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
        [XmlAttribute(AttributeName = "saldoMora")]
        public string SaldoMora { get; set; }
        [XmlAttribute(AttributeName = "cuota")]
        public string Cuota { get; set; }
    }

    [XmlRoot(ElementName = "Totales")]
    public class Totales
    {
        [XmlElement(ElementName = "TipoCuenta")]
        public List<TipoCuenta> TipoCuenta { get; set; }
        [XmlElement(ElementName = "Total")]
        public List<Total> Total { get; set; }
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "totalCuentas")]
        public string TotalCuentas { get; set; }
        [XmlAttribute(AttributeName = "cuentasConsideradas")]
        public string CuentasConsideradas { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
    }

    [XmlRoot(ElementName = "ComposicionPortafolio")]
    public class ComposicionPortafolio
    {
        [XmlElement(ElementName = "TipoCuenta")]
        public List<TipoCuenta> TipoCuenta { get; set; }
    }

    [XmlRoot(ElementName = "Trimestre")]
    public class Trimestre
    {
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "cantidadDevueltos")]
        public string CantidadDevueltos { get; set; }
        [XmlAttribute(AttributeName = "valorDevueltos")]
        public string ValorDevueltos { get; set; }
        [XmlAttribute(AttributeName = "cantidadPagados")]
        public string CantidadPagados { get; set; }
        [XmlAttribute(AttributeName = "valorPagados")]
        public string ValorPagados { get; set; }
        [XmlAttribute(AttributeName = "cuota")]
        public string Cuota { get; set; }
        [XmlAttribute(AttributeName = "cupoTotal")]
        public string CupoTotal { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
        [XmlAttribute(AttributeName = "porcentajeUso")]
        public string PorcentajeUso { get; set; }
        [XmlAttribute(AttributeName = "score")]
        public string Score { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "aperturaCuentas")]
        public string AperturaCuentas { get; set; }
        [XmlAttribute(AttributeName = "cierreCuentas")]
        public string CierreCuentas { get; set; }
        [XmlAttribute(AttributeName = "totalAbiertas")]
        public string TotalAbiertas { get; set; }
        [XmlAttribute(AttributeName = "totalCerradas")]
        public string TotalCerradas { get; set; }
        [XmlAttribute(AttributeName = "moraMaxima")]
        public string MoraMaxima { get; set; }
        [XmlAttribute(AttributeName = "mesesMoraMaxima")]
        public string MesesMoraMaxima { get; set; }
        [XmlAttribute(AttributeName = "totalCuentas")]
        public string TotalCuentas { get; set; }
        [XmlAttribute(AttributeName = "cuentasConsideradas")]
        public string CuentasConsideradas { get; set; }
        [XmlElement(ElementName = "Sector")]
        public List<Sector> Sector { get; set; }
    }

    [XmlRoot(ElementName = "Cheques")]
    public class Cheques
    {
        [XmlElement(ElementName = "Trimestre")]
        public List<Trimestre> Trimestre { get; set; }
    }

    [XmlRoot(ElementName = "AnalisisPromedio")]
    public class AnalisisPromedio
    {
        [XmlAttribute(AttributeName = "cuota")]
        public string Cuota { get; set; }
        [XmlAttribute(AttributeName = "cupoTotal")]
        public string CupoTotal { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
        [XmlAttribute(AttributeName = "porcentajeUso")]
        public string PorcentajeUso { get; set; }
        [XmlAttribute(AttributeName = "score")]
        public string Score { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "aperturaCuentas")]
        public string AperturaCuentas { get; set; }
        [XmlAttribute(AttributeName = "cierreCuentas")]
        public string CierreCuentas { get; set; }
        [XmlAttribute(AttributeName = "totalAbiertas")]
        public string TotalAbiertas { get; set; }
        [XmlAttribute(AttributeName = "totalCerradas")]
        public string TotalCerradas { get; set; }
        [XmlAttribute(AttributeName = "moraMaxima")]
        public string MoraMaxima { get; set; }
    }

    [XmlRoot(ElementName = "EvolucionDeuda")]
    public class EvolucionDeuda
    {
        [XmlElement(ElementName = "Trimestre")]
        public List<Trimestre> Trimestre { get; set; }
        [XmlElement(ElementName = "AnalisisPromedio")]
        public AnalisisPromedio AnalisisPromedio { get; set; }
        [XmlElement(ElementName = "Trimestres")]
        public Trimestres Trimestres { get; set; }
        [XmlElement(ElementName = "EvolucionDeudaSector")]
        public List<EvolucionDeudaSector> EvolucionDeudaSector { get; set; }
    }

    [XmlRoot(ElementName = "HistoricoSaldos")]
    public class HistoricoSaldos
    {
        [XmlElement(ElementName = "TipoCuenta")]
        public List<TipoCuenta> TipoCuenta { get; set; }
        [XmlElement(ElementName = "Totales")]
        public List<Totales> Totales { get; set; }
    }

    [XmlRoot(ElementName = "Cartera")]
    public class Cartera
    {
        [XmlAttribute(AttributeName = "tipo")]
        public string Tipo { get; set; }
        [XmlAttribute(AttributeName = "numeroCuentas")]
        public string NumeroCuentas { get; set; }
        [XmlAttribute(AttributeName = "valor")]
        public string Valor { get; set; }
    }

    [XmlRoot(ElementName = "ResumenEndeudamiento")]
    public class ResumenEndeudamiento
    {
        [XmlElement(ElementName = "Trimestre")]
        public List<Trimestre> Trimestre { get; set; }
    }

    [XmlRoot(ElementName = "InfoAgregada")]
    public class InfoAgregada
    {
        [XmlElement(ElementName = "Resumen")]
        public Resumen Resumen { get; set; }
        [XmlElement(ElementName = "Totales")]
        public Totales Totales { get; set; }
        [XmlElement(ElementName = "ComposicionPortafolio")]
        public ComposicionPortafolio ComposicionPortafolio { get; set; }
        [XmlElement(ElementName = "Cheques")]
        public Cheques Cheques { get; set; }
        [XmlElement(ElementName = "EvolucionDeuda")]
        public EvolucionDeuda EvolucionDeuda { get; set; }
        [XmlElement(ElementName = "HistoricoSaldos")]
        public HistoricoSaldos HistoricoSaldos { get; set; }
        [XmlElement(ElementName = "ResumenEndeudamiento")]
        public ResumenEndeudamiento ResumenEndeudamiento { get; set; }
    }

    [XmlRoot(ElementName = "CreditosVigentes")]
    public class CreditosVigentes
    {
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "totalComoPrincipal")]
        public string TotalComoPrincipal { get; set; }
        [XmlAttribute(AttributeName = "totalComoCodeudorYOtros")]
        public string TotalComoCodeudorYOtros { get; set; }
    }

    [XmlRoot(ElementName = "CreditosCerrados")]
    public class CreditosCerrados
    {
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "totalComoPrincipal")]
        public string TotalComoPrincipal { get; set; }
        [XmlAttribute(AttributeName = "totalComoCodeudorYOtros")]
        public string TotalComoCodeudorYOtros { get; set; }
    }

    [XmlRoot(ElementName = "CreditosReestructurados")]
    public class CreditosReestructurados
    {
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "totalComoPrincipal")]
        public string TotalComoPrincipal { get; set; }
        [XmlAttribute(AttributeName = "totalComoCodeudorYOtros")]
        public string TotalComoCodeudorYOtros { get; set; }
    }

    [XmlRoot(ElementName = "CreditosRefinanciados")]
    public class CreditosRefinanciados
    {
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "totalComoPrincipal")]
        public string TotalComoPrincipal { get; set; }
        [XmlAttribute(AttributeName = "totalComoCodeudorYOtros")]
        public string TotalComoCodeudorYOtros { get; set; }
    }

    [XmlRoot(ElementName = "ConsultaUlt6Meses")]
    public class ConsultaUlt6Meses
    {
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "totalComoPrincipal")]
        public string TotalComoPrincipal { get; set; }
        [XmlAttribute(AttributeName = "totalComoCodeudorYOtros")]
        public string TotalComoCodeudorYOtros { get; set; }
    }

    [XmlRoot(ElementName = "Desacuerdos")]
    public class Desacuerdos
    {
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "totalComoPrincipal")]
        public string TotalComoPrincipal { get; set; }
        [XmlAttribute(AttributeName = "totalComoCodeudorYOtros")]
        public string TotalComoCodeudorYOtros { get; set; }
    }

    [XmlRoot(ElementName = "AntiguedadDesde")]
    public class AntiguedadDesde
    {
        [XmlAttribute(AttributeName = "sectorCooperativo")]
        public string SectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "sectorFinanciero")]
        public string SectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "sectorReal")]
        public string SectorReal { get; set; }
        [XmlAttribute(AttributeName = "sectorTelcos")]
        public string SectorTelcos { get; set; }
    }

    [XmlRoot(ElementName = "PerfilGeneral")]
    public class PerfilGeneral
    {
        [XmlElement(ElementName = "CreditosVigentes")]
        public CreditosVigentes CreditosVigentes { get; set; }
        [XmlElement(ElementName = "CreditosCerrados")]
        public CreditosCerrados CreditosCerrados { get; set; }
        [XmlElement(ElementName = "CreditosReestructurados")]
        public CreditosReestructurados CreditosReestructurados { get; set; }
        [XmlElement(ElementName = "CreditosRefinanciados")]
        public CreditosRefinanciados CreditosRefinanciados { get; set; }
        [XmlElement(ElementName = "ConsultaUlt6Meses")]
        public ConsultaUlt6Meses ConsultaUlt6Meses { get; set; }
        [XmlElement(ElementName = "Desacuerdos")]
        public Desacuerdos Desacuerdos { get; set; }
        [XmlElement(ElementName = "AntiguedadDesde")]
        public AntiguedadDesde AntiguedadDesde { get; set; }
    }

    [XmlRoot(ElementName = "SaldosYMoras")]
    public class SaldosYMoras
    {
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "totalCuentasMora")]
        public string TotalCuentasMora { get; set; }
        [XmlAttribute(AttributeName = "saldoDeudaTotalMora")]
        public string SaldoDeudaTotalMora { get; set; }
        [XmlAttribute(AttributeName = "saldoDeudaTotal")]
        public string SaldoDeudaTotal { get; set; }
        [XmlAttribute(AttributeName = "morasMaxSectorFinanciero")]
        public string MorasMaxSectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "morasMaxSectorReal")]
        public string MorasMaxSectorReal { get; set; }
        [XmlAttribute(AttributeName = "morasMaxSectorTelcos")]
        public string MorasMaxSectorTelcos { get; set; }
        [XmlAttribute(AttributeName = "morasMaximas")]
        public string MorasMaximas { get; set; }
        [XmlAttribute(AttributeName = "numCreditos30")]
        public string NumCreditos30 { get; set; }
        [XmlAttribute(AttributeName = "numCreditosMayorIgual60")]
        public string NumCreditosMayorIgual60 { get; set; }
    }

    [XmlRoot(ElementName = "VectorSaldosYMoras")]
    public class VectorSaldosYMoras
    {
        [XmlElement(ElementName = "SaldosYMoras")]
        public List<SaldosYMoras> SaldosYMoras { get; set; }
        [XmlAttribute(AttributeName = "poseeSectorCooperativo")]
        public string PoseeSectorCooperativo { get; set; }
        [XmlAttribute(AttributeName = "poseeSectorFinanciero")]
        public string PoseeSectorFinanciero { get; set; }
        [XmlAttribute(AttributeName = "poseeSectorReal")]
        public string PoseeSectorReal { get; set; }
        [XmlAttribute(AttributeName = "poseeSectorTelcos")]
        public string PoseeSectorTelcos { get; set; }
    }

    [XmlRoot(ElementName = "Cuenta")]
    public class Cuenta
    {
        [XmlAttribute(AttributeName = "estadoActual")]
        public string EstadoActual { get; set; }
        [XmlAttribute(AttributeName = "calificacion")]
        public string Calificacion { get; set; }
        [XmlAttribute(AttributeName = "valorInicial")]
        public string ValorInicial { get; set; }
        [XmlAttribute(AttributeName = "saldoActual")]
        public string SaldoActual { get; set; }
        [XmlAttribute(AttributeName = "saldoMora")]
        public string SaldoMora { get; set; }
        [XmlAttribute(AttributeName = "cuotaMes")]
        public string CuotaMes { get; set; }
        [XmlAttribute(AttributeName = "comportamientoNegativo")]
        public string ComportamientoNegativo { get; set; }
        [XmlAttribute(AttributeName = "totalDeudaCarteras")]
        public string TotalDeudaCarteras { get; set; }
        [XmlElement(ElementName = "CaracterFecha")]
        public List<CaracterFecha> CaracterFecha { get; set; }
        [XmlAttribute(AttributeName = "entidad")]
        public string Entidad { get; set; }
        [XmlAttribute(AttributeName = "numeroCuenta")]
        public string NumeroCuenta { get; set; }
        [XmlAttribute(AttributeName = "tipoCuenta")]
        public string TipoCuenta { get; set; }
        [XmlAttribute(AttributeName = "estado")]
        public string Estado { get; set; }
        [XmlAttribute(AttributeName = "contieneDatos")]
        public string ContieneDatos { get; set; }
    }

    [XmlRoot(ElementName = "UsuarioData")]
    public class UsuarioData
    {
        [XmlElement(ElementName = "Cuenta")]
        public List<Cuenta> Cuenta { get; set; }
        [XmlAttribute(AttributeName = "tipoUsuario")]
        public string TipoUsuario { get; set; }
    }

    [XmlRoot(ElementName = "EndeudamientoActual")]
    public class EndeudamientoActual
    {
        [XmlElement(ElementName = "Sector")]
        public List<Sector> Sector { get; set; }
    }

    [XmlRoot(ElementName = "Series")]
    public class Series
    {
        [XmlElement(ElementName = "Valores")]
        public Valores Valores { get; set; }
        [XmlAttribute(AttributeName = "serie")]
        public string Serie { get; set; }
    }

    [XmlRoot(ElementName = "ImagenTendenciaEndeudamiento")]
    public class ImagenTendenciaEndeudamiento
    {
        [XmlElement(ElementName = "Series")]
        public List<Series> Series { get; set; }
    }

    [XmlRoot(ElementName = "CaracterFecha")]
    public class CaracterFecha
    {
        [XmlAttribute(AttributeName = "fecha")]
        public string Fecha { get; set; }
        [XmlAttribute(AttributeName = "saldoDeudaTotalMora")]
        public string SaldoDeudaTotalMora { get; set; }
    }

    [XmlRoot(ElementName = "MorasMaximas")]
    public class MorasMaximas
    {
        [XmlElement(ElementName = "CaracterFecha")]
        public List<CaracterFecha> CaracterFecha { get; set; }
    }

    [XmlRoot(ElementName = "AnalisisVectores")]
    public class AnalisisVectores
    {
        [XmlElement(ElementName = "Sector")]
        public List<Sector> Sector { get; set; }
    }

    [XmlRoot(ElementName = "Trimestres")]
    public class Trimestres
    {
        [XmlElement(ElementName = "Trimestre")]
        public List<string> Trimestre { get; set; }
    }

    [XmlRoot(ElementName = "EvolucionDeudaValorTrimestre")]
    public class EvolucionDeudaValorTrimestre
    {
        [XmlAttribute(AttributeName = "trimestre")]
        public string Trimestre { get; set; }
        [XmlAttribute(AttributeName = "tipoCuenta")]
        public string TipoCuenta { get; set; }
        [XmlAttribute(AttributeName = "num")]
        public string Num { get; set; }
        [XmlAttribute(AttributeName = "cupoInicial")]
        public string CupoInicial { get; set; }
        [XmlAttribute(AttributeName = "saldo")]
        public string Saldo { get; set; }
        [XmlAttribute(AttributeName = "saldoMora")]
        public string SaldoMora { get; set; }
        [XmlAttribute(AttributeName = "cuota")]
        public string Cuota { get; set; }
        [XmlAttribute(AttributeName = "porcentajeDeuda")]
        public string PorcentajeDeuda { get; set; }
        [XmlAttribute(AttributeName = "codMenorCalificacion")]
        public string CodMenorCalificacion { get; set; }
        [XmlAttribute(AttributeName = "textoMenorCalificacion")]
        public string TextoMenorCalificacion { get; set; }
    }

    [XmlRoot(ElementName = "EvolucionDeudaTipoCuenta")]
    public class EvolucionDeudaTipoCuenta
    {
        [XmlElement(ElementName = "EvolucionDeudaValorTrimestre")]
        public List<EvolucionDeudaValorTrimestre> EvolucionDeudaValorTrimestre { get; set; }
        [XmlAttribute(AttributeName = "tipoCuenta")]
        public string TipoCuenta { get; set; }
    }

    [XmlRoot(ElementName = "EvolucionDeudaSector")]
    public class EvolucionDeudaSector
    {
        [XmlElement(ElementName = "EvolucionDeudaTipoCuenta")]
        public List<EvolucionDeudaTipoCuenta> EvolucionDeudaTipoCuenta { get; set; }
        [XmlAttribute(AttributeName = "codSector")]
        public string CodSector { get; set; }
        [XmlAttribute(AttributeName = "nombreSector")]
        public string NombreSector { get; set; }
    }

    [XmlRoot(ElementName = "InfoAgregadaMicrocredito")]
    public class InfoAgregadaMicrocredito
    {
        [XmlElement(ElementName = "Resumen")]
        public Resumen Resumen { get; set; }
        [XmlElement(ElementName = "AnalisisVectores")]
        public AnalisisVectores AnalisisVectores { get; set; }
        [XmlElement(ElementName = "EvolucionDeuda")]
        public EvolucionDeuda EvolucionDeuda { get; set; }
    }

    [XmlRoot(ElementName = "Informe")]
    public class Informe
    {
        [XmlElement(ElementName = "NaturalNacional")]
        public NaturalNacional NaturalNacional { get; set; }
        [XmlElement(ElementName = "Score")]
        public Score Score { get; set; }
        [XmlElement(ElementName = "CuentaAhorro")]
        public List<CuentaAhorro> CuentaAhorro { get; set; }
        [XmlElement(ElementName = "CuentaCorrienteDTO")]
        public List<CuentaCorrienteDTO> CuentaCorriente { get; set; }
        [XmlElement(ElementName = "TarjetaCredito")]
        public List<TarjetaCredito> TarjetaCredito { get; set; }
        [XmlElement(ElementName = "CuentaCartera")]
        public List<CuentaCartera> CuentaCartera { get; set; }
        [XmlElement(ElementName = "EndeudamientoGlobal")]
        public List<EndeudamientoGlobal> EndeudamientoGlobal { get; set; }
        [XmlElement(ElementName = "Consulta")]
        public List<Consulta> Consulta { get; set; }
        [XmlElement(ElementName = "productosValores")]
        public ProductosValores ProductosValores { get; set; }
        [XmlElement(ElementName = "InfoAgregada")]
        public InfoAgregada InfoAgregada { get; set; }
        [XmlElement(ElementName = "InfoAgregadaMicrocredito")]
        public InfoAgregadaMicrocredito InfoAgregadaMicrocredito { get; set; }
        [XmlAttribute(AttributeName = "fechaConsulta")]
        public string FechaConsulta { get; set; }
        [XmlAttribute(AttributeName = "respuesta")]
        public string Respuesta { get; set; }
        [XmlAttribute(AttributeName = "codSeguridad")]
        public string CodSeguridad { get; set; }
        [XmlAttribute(AttributeName = "tipoIdDigitado")]
        public string TipoIdDigitado { get; set; }
        [XmlAttribute(AttributeName = "identificacionDigitada")]
        public string IdentificacionDigitada { get; set; }
        [XmlAttribute(AttributeName = "apellidoDigitado")]
        public string ApellidoDigitado { get; set; }
    }

    [XmlRoot(ElementName = "Informes")]
    public class Informes
    {
        [XmlElement(ElementName = "Informe")]
        public Informe Informe { get; set; }
    }
}
