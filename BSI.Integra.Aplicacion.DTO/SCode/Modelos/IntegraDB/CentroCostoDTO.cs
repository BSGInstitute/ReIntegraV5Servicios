using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CentroCostoDTO
    {
        public int Id { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        [StringLength(50)]
        public string IdPgeneral { get; set; } = null!;
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        [StringLength(10)]
        public string? IdAreaCc { get; set; }
        public int? Ismtotales { get; set; }
        public int? Icpftotales { get; set; }
    }
    public class CentroCostoMasAdicionalesDTO
    {
        public int Id { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        [StringLength(50)]
        public string? IdPgeneral { get; set; }
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string Codigo { get; set; } = null!;
        public string CTroncal { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdArea1 { get; set; }
        public int? IdSubNivel { get; set; }
        [StringLength(10)]
        public string? IdAreaCc { get; set; }
    }

    public class CentroCostoGeneradoDTO
    {
        public CentroCostoDTO CentroCosto { get; set; }
        public string Codigo { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public string NombreProgramaEspecificoNumerico { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string CodigoBanco { get; set; }
        public int? GruposAsignados { get; set; }
        public int GruposCreados { get; set; }
        public bool HaAlcanzadoLimiteGrupos { get; set; }
    }
    public class PlantillaCentroCostoDTO
    {
        public int IdCentroCosto { get; set; }
        public string NombrePartner { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombrePGeneral { get; set; }
    }
    public class CentroCostoProbableDTO
    {
        public int IdPEspecifico { get; set; }
        public string Tipo { get; set; }
        public int IdPersonal { get; set; }
        public decimal Precio { get; set; }
    }
    public class TCRM_CentroCostoPorAsesorAgrupadoDTO
    {
        public int IdAsesor { get; set; }
        public double PrecioPromedio { get; set; }
        public double IngresoReal { get; set; }
        public double IngresoMes { get; set; }
        public double DescuentoPromedio { get; set; }
        public int OportunidadesOCAnyIS { get; set; }
        public int OportunidadesOCTotal { get; set; }
        public bool EstadoAsesor { get; set; }
    }
    public class TCRM_CentroCostoPorAsesorAgrupadoAlternoDTO
    {
        public int idasesor { get; set; }
        public double precioPromedio { get; set; }
        public double ingresoReal { get; set; }
        public double ingresoMes { get; set; }
        public double DescuentoPromedio { get; set; }
        public int oportunidadesOCAnyIS { get; set; }
        public int oportunidadesOCTotal { get; set; }
        public bool estadoAsesor { get; set; }

    }


    public class CentroCostoSubAreaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public string NombreAreaCapacitacion { get; set; }
        public string NombreSubAreaCapacitacion { get; set; }
    }
    public class CambioCentroCostoDTO
    {
        public int IdOportunidadV4 { get; set; }
        public Guid IdOportunidadV3 { get; set; }
        public Guid? IdOportunidadPadreV3 { get; set; }
        public int? IdOportunidadPadreV4 { get; set; }
        public string IdMatriculaCabeceraV3 { get; set; }
        public int IdMatriculaCabeceraV4 { get; set; }
        public int IdCronogramaPagoV4 { get; set; }
        public string IdCronogramaPagoV3 { get; set; }
        public int IdCentroCostoV3 { get; set; }
        public int IdCentroCostoV4 { get; set; }
        public int IdPespecificoV3 { get; set; }
        public int IdPespecificoV4 { get; set; }
        public string Usuario { get; set; }
    }
    public class CentroCostoPadreCentroCostoIndividualDTO
    {
        public int IdCentroCosto { get; set; }
        public int IdProgramaEspecifico { get; set; }
        public string CentroCosto { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string EstadoProgramaEspecifico { get; set; }
        public string Tipo { get; set; }
    }
    public class ConfiguracionCentroCostoCoordinadorDTO
    {
        public int? Id { get; set; }
        public int? IdPersonal { get; set; }
        public string Personal { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdProgramaEspecifico { get; set; }
        public string CentroCosto { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string EstadoProgramaEspecifico { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool EsAsignado { get; set; }
    }
    public class ConfiguracionCoordinadorPorPersonalDTO
    {
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public List<ConfiguracionCoordinadorPorPersonalDetalleCentroCosto> DetalleCentroCosto { get; set; }
        public List<ConfiguracionCoordinadorPorPersonalDetalleEstadoMatricula> DetalleEstadoMatricula { get; set; }
        public List<ConfiguracionCoordinadorPorPersonalDetalleSubEstadoMatricula> DetalleSubEstadoMatricula { get; set; }
    }
    public class ConfiguracionCoordinadorPorPersonalDetalleCentroCosto
    {
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
    }
    public class ConfiguracionCoordinadorPorPersonalDetalleEstadoMatricula
    {
        public int? IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
    }
    public class ConfiguracionCoordinadorPorPersonalDetalleSubEstadoMatricula
    {
        public int? IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
    }
    public class CentroCostoHijoDTO
    {
        public int IdCentroCosto { get; set; }
        public int PEspecificoPadreId { get; set; }
        public int IdCentroCostoHijo { get; set; }
        public int PEspecificoHijoId { get; set; }
    }
    public class ProgramaCentroCostoDTO
    {
        public int? IdPGeneral { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? TipoId { get; set; }
    }
    public class DatosCentroCostoDTO
    {
        public string CodigoBanco { get; set; }
        public string CentroCosto { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
    }

    public class CentroCostoCampaniaDTO
    {
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public string Codigo { get; set; }
        public string Campania { get; set; }
        public string IdConjuntoAnuncio { get; set; }
    }
    public class CentroCostoPEspecificoDTO
    {
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
    }
    public class CentroCostoNombreDTO
    {
        public string NombreCentroCosto { get; set; }
    }
    public class CentroCostoPEspecificoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPEspecifico { get; set; }
    }
    public class CentroCostoDatosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string IdPgeneral { get; set; }
        public string IdAreaCc { get; set; }
        public string Codigo { get; set; }
    }
    public class CentroCostoCombosPadreDTO
    {
        public IEnumerable<AreaCCDTO> AreaCc { get; set; }
        public IEnumerable<SubNivelCcDTO> SubNivelCc { get; set; }
        public IEnumerable<ComboDTO> Ciudad { get; set; }
        //public IEnumerable<ComboDTO> TroncalCiudad { get; set; }
        public IEnumerable<ComboDTO> Area { get; set; }
        public IEnumerable<SubAreaDTO> SubArea { get; set; }
        public IEnumerable<TroncalPGeneralSubAreaCodigoDTO> PGeneral { get; set; }

    }
    public class CentroCostoProgramaEspecificoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPEspecifico { get; set; }
    }
    public partial class CentroCostoCompuestoDTO
    {
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string IdAreaCc { get; set; }
        public int? Ismtotales { get; set; }
        public int? Icpftotales { get; set; }
        //Adicional
        public string CTroncal { get; set; }
        public int? IdCiudad { get; set; }
        public int? Total { get; set; }
        public int? IdArea1 { get; set; }
        public int? IdSubNivel { get; set; }
        public bool? Estado { get; set; }
        public int? Id { get; set; }
        //Auditoria
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public partial class FiltroCentroCostoDTO
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string CentroCosto { get; set; }
    }

    public partial class CentroCostoUsuariosDTO { 
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        //Auditoria
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }


}
