using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MontoPagoDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        [StringLength(250)]
        public string PrecioLetras { get; set; } = null!;
        public int IdMoneda { get; set; }
        public decimal? Matricula { get; set; }
        public decimal? Cuotas { get; set; }
        public int? NroCuotas { get; set; }
        public int? IdTipoDescuento { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdPais { get; set; }
        [StringLength(100)]
        public string? Vencimiento { get; set; }
        [StringLength(14)]
        public string? PrimeraCuota { get; set; }
        public bool? CuotaDoble { get; set; }
        [StringLength(200)]
        public string? Descripcion { get; set; }
        public bool? VisibleWeb { get; set; }
        public int? Paquete { get; set; }
        public bool? PorDefecto { get; set; }
        public decimal? MontoDescontado { get; set; }
        public List<int> PlataformasPagos { get; set; }
        public List<int> SuscripcionesPagos { get; set; }
    }
    public class PgeneralMontoPagoDetalleDTO
    {
        public IEnumerable<ComboDTO> Suscripciones { get; set; }
        public IEnumerable<ComboDTO> TipoCategoria { get; set; }
        public IEnumerable<MontoPagoDTO> MontoPagos { get; set; }
    }
    public class MontoPagoComboDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdPais { get; set; }
        public string? Descripcion { get; set; }
    }
    public class MontoPagoOportunidadDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public string PrecioLetras { get; set; } = null!;
        public int IdMoneda { get; set; }
        public decimal Matricula { get; set; }
        public decimal Cuotas { get; set; }
        public int NroCuotas { get; set; }
        public int IdPrograma { get; set; }
        public int IdTipoPago { get; set; }
        public int IdPais { get; set; }
        public string? Vencimiento { get; set; }
        public string? PrimeraCuota { get; set; }
        public bool CuotaDoble { get; set; }
        public int IdTipoDescuento { get; set; }
        public int Formula { get; set; }
        public int PorcentajeGeneral { get; set; }
        public int PorcentajeMatricula { get; set; }
        public int FraccionesMatricula { get; set; }
        public int PorcentajeCuotas { get; set; }
        public int CuotasAdicionales { get; set; }
        public string NombrePlural { get; set; } = null!;
        public int CuotasTipoPago { get; set; }
        public int? Paquete { get; set; }
        public string Nombre { get; set; } = null!;
        public bool? VisibleWeb { get; set; }
        public decimal MontoDescontado { get; set; }
    }
    public class MontoPagoPaqueteDTO
    {
        public int Id { get; set; }
        public int? Paquete { get; set; }
    }
    public class MontoPagoBeneficiosDTO
    {
        public int? IdProgramaGeneral { get; set; }
        public int? Paquete { get; set; }
        public string NombrePaquete { get; set; }
        public string Beneficios { get; set; }
        public int? IdPais { get; set; }
        public int? IdMoneda { get; set; }
    }
    public class MontoPagoVersionDTO
    {
        public int? Paquete { get; set; }
        public string tp_nombre { get; set; } = null!;
        public int tp_cuotas { get; set; }
        public double mp_precio { get; set; }
        public string Simbolo { get; set; } = null!;
        public double? mp_matricula { get; set; }
        public int? mp_nro_cuotas { get; set; }
        public double? mp_cuotas { get; set; }
    }
    public class MontoPagoVersionBeneficiosDTO
    {
        public int? Paquete { get; set; }
        public string tp_nombre { get; set; } = null!;
        public int tp_cuotas { get; set; }
        public double mp_precio { get; set; }
        public string Simbolo { get; set; } = null!;
        public double? mp_matricula { get; set; }
        public int? mp_nro_cuotas { get; set; }
        public double? mp_cuotas { get; set; }
        public string Titulo { get; set; } = null!;
        public int? OrdenBeneficio { get; set; }
    }
    public class MontoPagoVersionBeneficiosDetalleDTO
    {
        public int? Paquete { get; set; }
        public string tp_nombre { get; set; } = null!;
        public int tp_cuotas { get; set; }
        public double mp_precio { get; set; }
        public string Simbolo { get; set; } = null!;
        public double? mp_matricula { get; set; }
        public int? mp_nro_cuotas { get; set; }
        public double? mp_cuotas { get; set; }
        public List<string> Beneficios { get; set; } = new List<string>();
    }
    public class MontoPagoCompuestoDTO
    {
        public string Id { get; set; }
        public double mp_precio { get; set; }
        public string mp_precio_letras { get; set; }
        public int mp_moneda { get; set; }
        public double? mp_matricula { get; set; }
        public double? mp_cuotas { get; set; }
        public int mp_nro_cuotas { get; set; }
        public int id_programa { get; set; }
        public int id_tp { get; set; }
        public int id_pais { get; set; }
        public int id_tipo_descuento { get; set; }
        public string mp_vencimiento { get; set; }
        public string mp_primeraCuota { get; set; }
        public bool mp_cuotaDoble { get; set; }
        public int tp_formula { get; set; }
        public int tp_porcentaje_general { get; set; }
        public int tp_porcentaje_matricula { get; set; }
        public int tp_fracciones_matricula { get; set; }
        public int tp_porcentaje_cuotas { get; set; }
        public int tp_cuotas_adicionales { get; set; }
        public string NombrePlural { get; set; }
        public int matriculaEnProceso { get; set; }
        public string Simbolo { get; set; }
        public string CodigoMatricula { get; set; }
    }
    public class MontoPagoCronogramaCompuestoDTO
    {
        public int Id { get; set; }
        public double mp_precio { get; set; }
        public double mp_precioDescuento { get; set; }
        public string mp_precio_letras { get; set; }
        public string mp_moneda { get; set; }
        public Nullable<double> mp_matricula { get; set; }
        public Nullable<double> mp_cuotas { get; set; }
        public int mp_nro_cuotas { get; set; }
        public int id_programa { get; set; }
        public int id_tp { get; set; }
        public int id_pais { get; set; }
        public int id_tipo_descuento { get; set; }
        public int id_cronograma { get; set; }
        public bool is_aprobado { get; set; }
        public string mp_vencimiento { get; set; }
        public string mp_primeraCuota { get; set; }
        public bool mp_cuotaDoble { get; set; }
        public int tp_formula { get; set; }
        public int tp_porcentaje_general { get; set; }
        public int tp_porcentaje_matricula { get; set; }
        public int tp_fracciones_matricula { get; set; }
        public int tp_porcentaje_cuotas { get; set; }
        public int tp_cuotas_adicionales { get; set; }
        public string NombrePlural { get; set; }
        public int matriculaEnProceso { get; set; }
        public string Simbolo { get; set; }
        public string CodigoMatricula { get; set; }
    }
    public class MontoPagoCronogramasDetalleCompuestoDTO
    {
        public MontoPagoCronogramaCompuestoDTO cronograma { get; set; }
        public List<MontoPagoCronogramaDetalleValidoDTO> listaDetalle { get; set; }
    }
    public class MontoPagoModalidadDTO
    {
        public int IdPGeneral { get; set; }
        public int Paquete { get; set; }
        public string NombrePaquete { get; set; }
        public string InversionContado { get; set; }
        public string InversionCredito { get; set; }
        public double ContadoByDolares { get; set; }
        public int Pais { get; set; }
        public string Beneficios { get; set; }
    }
    public class MontoPagoProgramaDTO
    {
        public int Id { get; set; }
        public double Precio { get; set; }
        public string PrecioLetras { get; set; }
        public int IdMoneda { get; set; }
        public string SimboloMoneda { get; set; }
        public double? Matricula { get; set; }
        public double? Cuotas { get; set; }
        public int? NroCuotas { get; set; }
        public int IdPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public string DuracionPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        public string TipoPago { get; set; }
        public int? IdPais { get; set; }
        public string Descripcion { get; set; }
        public bool? VisibleWeb { get; set; }
        public int? Paquete { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
    }
    public class MontoProgramaDetalleDTO
    {
        public string Version { get; set; }
        public List<MontoProgramaVersionDetalle> VersionDetalle { get; set; }
    }
    public class MontoProgramaVersionDetalle
    {
        public int? IdTipoPago { get; set; }
        public string TipoPago { get; set; }
        public string SimboloMoneda { get; set; }
        public double? Matricula { get; set; }
        public double? Cuotas { get; set; }
        public int? NroCuotas { get; set; }
    }
    public class MontoProgramaAgrupadoDTO
    {
        public int IdPrograma { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public string Duracion { get; set; }
        public ProgramaGeneralSeccionDocumentoDTO SeccionCertificadoV2 { get; set; }
        public SeccionDocumentoDTO SeccionCertificadoV1 { get; set; }
        public List<MontoProgramaDetalleDTO> MontoDetalle { get; set; }
    }
    public class MontoPagoEtiquetaDTO
    {
        public int? Paquete { get; set; }
        public string tp_nombre { get; set; }
        public int tp_cuotas { get; set; }
        public double mp_precio { get; set; }
        public string Simbolo { get; set; }
        public double mp_matricula { get; set; }
        public int mp_nro_cuotas { get; set; }
        public double mp_cuotas { get; set; }
        public string Titulo { get; set; }
        public int? OrdenBeneficio { get; set; }
    }
    public class MontoPagoEtiquetaAgrupadoDTO
    {
        public int? Paquete { get; set; }
        public string tp_nombre { get; set; }
        public int tp_cuotas { get; set; }
        public double mp_precio { get; set; }
        public string Simbolo { get; set; }
        public double mp_matricula { get; set; }
        public int mp_nro_cuotas { get; set; }
        public double mp_cuotas { get; set; }
        public List<string> Beneficios { get; set; }
    }
    public class PaqueteCentroCostoDTO
    {
        public int IdPaquete { get; set; }
        public string Paquete { get; set; }
        public int IdCentroCosto { get; set; }
    }
    public class ComboMontoPagoDTO
    {
        public IEnumerable<SubAreaCapacitacionFiltroDTO> SubAreas { get; set; }
        public IEnumerable<ComboDTO> Areas { get; set; }
        public IEnumerable<TipoDescuentoComboDTO> Descuento { get; set; }
        public IEnumerable<PaisMonedaComboDTO> Paises { get; set; }
        public IEnumerable<MonedaComboDTO> Monedas { get; set; }
        public IEnumerable<ComboDTO> CategoriasProgramas { get; set; }
        public IEnumerable<TipoPagoComboDTO> TipoPago { get; set; }
        public IEnumerable<ComboDTO> Suscripciones { get; set; }
        public IEnumerable<ComboDTO> PlataformaPago { get; set; }
    }
}
