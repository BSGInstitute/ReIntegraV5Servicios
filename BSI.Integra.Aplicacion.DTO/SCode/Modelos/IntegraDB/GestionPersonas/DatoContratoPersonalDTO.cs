using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class DatoContratoPersonalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdTipoContrato { get; set; }
        public bool EstadoContrato { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal RemuneracionFija { get; set; }
        public int IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinancieraPago { get; set; }
        public string? NumeroCuentaPago { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdCargo { get; set; }
        public int IdTipoPerfil { get; set; }
        public int? IdPersonalJefe { get; set; }
        public int? IdEntidadFinancieraCts { get; set; }
        public string? NumeroCuentaCts { get; set; }
        public bool? EsPeridoPrueba { get; set; }
        public DateTime? FechaFinPeriodoPrueba { get; set; }
        public int? IdContratoEstado { get; set; }
        public int? IdMigracion { get; set; }
        public string Usuario { get; set; }
        public List<RemuneracionVariableDTO> ListaRemuneracionVariable { get; set; }
    }

    public class RemuneracionVariableDTO
    {
        public string TipoRemuneracionVariable { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }

    }
    public class ContratoFiltroDTO
    {
        public List<int> ListaPersonalAreaTrabajo { get; set; }
        public List<int> ListaPuestoTrabajo { get; set; }
        public List<int> ListaPersonal { get; set; }
        public List<int> ListaSedeTrabajo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? OpcionFecha { get; set; }
    }

    public class DatoContratoPersonalFiltroDTO
    {
        public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPersonal_Jefe { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreDireccion { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public string PaisNacimiento { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int? IdSexo { get; set; }
        public string Sexo { get; set; }
        public int? IdEstadoCivil { get; set; }
        public string EstadoCivil { get; set; }
        public int? IdTipoEstudio { get; set; }
        public string TipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string AreaFormacion { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public int? IdTipoContrato { get; set; }
        public string TipoContrato { get; set; }
        public bool? EstadoContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? RemuneracionFija { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public string TipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera_Pago { get; set; }
        public string EntidadFinanciera_Pago { get; set; }
        public string NumeroCuentaPago { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdTipoPerfil { get; set; }
        public string TipoPerfil { get; set; }
        public int? IdContratoEstado { get; set; }
        public string ContratoEstado { get; set; }
        public bool? Estado { get; set; }

    }

    public class PersonalFormularioDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string DistritoDireccion { get; set; }
        public string FijoReferencia { get; set; }
        public string MovilReferencia { get; set; }
        public string EmailReferencia { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public string SistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string EntidadSistemaPensionario { get; set; }
        public bool Estado { get; set; }
    }

    public class PersonalExperienciaFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public string AreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public string MotivoRetiro { get; set; }
        public string NombreJefeInmediato { get; set; }
        public string TelefonoJefeInmediato { get; set; }
        public bool Estado { get; set; }

    }

    public class PersonalFormacionFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public int? IdTipoEstudio { get; set; }
        public string TipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string AreaFormacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? AlaActualidad { get; set; }
        public int? IdEstadoEstudio { get; set; }
        public string EstadoEstudio { get; set; }
        public string Logro { get; set; }
        public bool Estado { get; set; }
    }

    public class PersonalIdiomaFormularioDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdIdioma { get; set; }
        public string Idioma { get; set; }
        public int? IdNivelIdioma { get; set; }
        public string NivelIdioma { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public bool Estado { get; set; }

    }

    public class ContratoHistoricoRegistroDTO
    {
        public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPersonal_Jefe { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NumeroDocumento { get; set; }
        public string NombreDireccion { get; set; }
        public int? IdPaisNacimiento { get; set; }
        public string PaisNacimiento { get; set; }
        public int? IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public int? IdSexo { get; set; }
        public string Sexo { get; set; }
        public int? IdEstadoCivil { get; set; }
        public string EstadoCivil { get; set; }
        public int? IdTipoEstudio { get; set; }
        public string TipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string AreaFormacion { get; set; }
        public int? IdCentroEstudio { get; set; }
        public string CentroEstudio { get; set; }
        public int? IdTipoContrato { get; set; }
        public string TipoContrato { get; set; }
        public bool? EstadoContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? RemuneracionFija { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public string TipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera_Pago { get; set; }
        public string EntidadFinanciera_Pago { get; set; }
        public string NumeroCuentaPago { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdTipoPerfil { get; set; }
        public string TipoPerfil { get; set; }
        public int? IdContratoEstado { get; set; }
        public string ContratoEstado { get; set; }
        public bool? Estado { get; set; }
        public decimal? Monto { get; set; }
        public string Concepto { get; set; }
        public string TipoRemuneracionVariable { get; set; }
    }

    public class ContratoHistoricoRegistroRDTO
    {
        public int? Id { get; set; }
        public int IdPersonal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int? IdTipoContrato { get; set; }
        public string TipoContrato { get; set; }
        public bool? EstadoContrato { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal? RemuneracionFija { get; set; }
        public int? IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSedeTrabajo { get; set; }
        public string SedeTrabajo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdContratoEstado { get; set; }
        public string ContratoEstado { get; set; }
        public bool? Estado { get; set; }
        public List<ContratoHistoricoRegistroRVDTO> ListaRemuneracionVariable { get; set; }
    }

    public class ContratoHistoricoRegistroRVDTO
    {
        // public int? id { get; set; }
        public decimal? Monto { get; set; }
        public string Concepto { get; set; }
        public string TipoRemuneracionVariable { get; set; }
    }

    public class PuestoTrabajoGestionContratoDTO
    {
        public int Id { get; set; }
        public int? IdPuestoTrabajoRemuneracion { get; set; }
        public int? IdRemuneracion { get; set; }
        public int? IdTipoRemuneracion { get; set; }
        public int? IdClaseRemuneracion { get; set; }
        public int? IdPeriodoRemuneracion { get; set; }
        public bool? Tasa { get; set; }
        public decimal? Monto { get; set; }
        public int? IdMoneda { get; set; }
        public decimal? PorcentajeTasa { get; set; }
        public string DescripcionEquipo { get; set; }
        public bool? TieneCondicion { get; set; }
        public int? IdDescripcionMonetaria { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMonedaValorVariable { get; set; }
        public decimal? IngresoMensual { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class DatosFormularioPersonalDTO
    {
        public int idPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public int? IdSexo { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? NombreTipoDocumento { get; set; }
        public int? IdPaisDireccion { get; set; }
        public string? NombrePais { get; set; }
        public int? IdCiudad { get; set; }
        public string? NombreCiudad { get; set; }
        public string? NombreDireccion { get; set; }
        public string? DistritoDireccion { get; set; }
        public string? Emailreferencia { get; set; }
        public string? MovilReferencia { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public string? SistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string? EntidadSistemaPensionario { get; set; }
        public bool? Estado { get; set; }
    }
    public class DatosRemuneracionVariableDTO
    {
        public int IdPuestoTrabajoRemuneracionDetalle { get; set; }
        public int IdPuestoTrabajoRemuneracion { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdRemuneracionTipo { get; set; }
        public string NombrePuestoTrabajo { get; set; }
        public Boolean TieneCondicion { get; set; }
        public Boolean EsTasa { get; set; }
        public float RangoValorMinimo { get; set; }
        public float RangoValorMaximo { get; set; }
        public int IdMonedaMontoFijo { get; set; }
        public int IdMonedaRango { get; set; }
    }


    public class ComboPlantillaContratoDTO
    {
        public int IdPlantillaBase { get; set; }
        public string NombrePlantillaBase { get; set; }
        public string DescripcionPlantillaBase { get; set; }
        public int IdPlantilla { get; set; }
        public string NombrePlantilla { get; set; }
        public int IdPlantillaClaveValor { get; set; }
        public string ClavePlatilla { get; set; }
        public string ValorPlantilla { get; set; }
        public string EtiquetasPlantilla { get; set; }
        public List<string> ListaEtiquetas { get; set; }
        public int IdTipoContrato { get; set; }
        public int IdContratoPlantilla { get; set; }
    }

    public class FuncionPuestoTrabajoDTO
    {
        public int FuncionNumeroOrden { get; set; }
        public string NombreFuncion { get; set; }
        public int IdPuestoTrabajoFuncion { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int VersionPuestoTrabajo { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string NombrePuestoTrabajo { get; set; }
    }

}
