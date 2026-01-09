namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteCambioDeFaseCombosGeneralDTO
    {
        public IEnumerable<ComboDTO> CentroCostos { get; set; }
        public List<ReportePersonalDTO> Asesores { get; set; }
    }
    public class ReporteCambioFaseFiltrosDTO
    {
        public List<int> Asesores { get; set; }
        public List<int> CentroCostos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Acumulado { get; set; }
    }
    public class ReporteCambioFaseSPFiltrosDTO
    {
        public string Asesores { get; set; }
        public string CentroCostos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Acumulado { get; set; }
    }
    public class ReporteCambioFaseFiltroProcesadoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Filtro { get; set; }
    }
    public class ReporteCambiosDeFaseOportunidadDTO
    {
        public int? Numero { get; set; }
        public int NumeroRegistros { get; set; }
        public string FaseOrigen { get; set; }
        public string FaseDestino { get; set; }
        public string TipoDato { get; set; }
        public decimal MetaLanzamiento { get; set; }
        public int IndicadorLanzamiento { get; set; }
    }
    public class ReporteCalidadProcesamientoDTO
    {
        public string DatosAsesor { get; set; }
        public string NombreFase { get; set; }
        public int? Registros { get; set; }
        public DateTime Fecha { get; set; }
        public double PromedioPerfil { get; set; }
        public double PromedioHistorialFinanciero { get; set; }
        public double PromedioPGeneral { get; set; }
        public double PromedioPEspecifico { get; set; }
        public double PromedioBeneficios { get; set; }
        public double PromedioCompetidores { get; set; }
        public double PromedioProblemaSeleccionados { get; set; }
        public double PromedioProblemaSolucionados { get; set; }
    }
    public class ReporteCalidadProcesamientoAlternoDTO
    {
        public string DatosAsesor { get; set; }
        public string NombreFase { get; set; }
        public int? Registros { get; set; }
        public double PromedioPerfil { get; set; }
        public double PromedioDNI { get; set; }
        public double PromedioSentinel { get; set; }
        public double PromedioPGeneralMotivacion { get; set; }
        public double PromedioPublicoObjetivo { get; set; }
        public double PromedioPrerequisitoPrograma { get; set; }
        public double PromedioRequisitoCertificacion { get; set; }
        public double PromedioBeneficios { get; set; }
        public double PromedioInicioPrograma { get; set; }
        public double PromedioCompetidores { get; set; }
        public double PromedioProblemaSeleccionados { get; set; }
        public double PromedioProblemaSolucionados { get; set; }
        public double PromedioHistorialFinanciero { get; set; }
    }
    public class ControlCambioDeFaseDTO
    {
        public string FaseOrigen { get; set; }
        public int IdFaseOrigen { get; set; }
        public int ActividadesEjecutadas { get; set; }
        public int ActividadesProgramadasAutomaticas { get; set; }
        public int ActividadesProgramadasManuales { get; set; }
        public int ProgramadasEjecutadasSinLlamada { get; set; }
        public int ActividadesTotales { get; set; }
        public int Contactabilidad { get; set; }
        public decimal MinPromedioEjecutadas { get; set; }
        public decimal MinPromedioprogramadasmanuales { get; set; }
        public decimal NumIntentoLlamadasPromedio { get; set; }
        public int Orden { get; set; }
    }
    public class ControlCambioDeFaseV2DTO
    {
        public string FaseOrigen { get; set; }
        public int IdFaseOrigen { get; set; }
        public int ActividadesEjecutadas { get; set; }
        public int ActividadesProgramadasAutomaticas { get; set; }
        public int ActividadesProgramadasManuales { get; set; }
        public int ActividadesContestaCorta { get; set; }
        public int ProgramadasEjecutadasSinLlamada { get; set; }
        public int ProgramadasAutomaticasSinLlamada { get; set; }
        public int ProgramadasManualesSinLlamada { get; set; }
        public int ActividadesTotales { get; set; }
        public int Contactabilidad { get; set; }
        public decimal MinPromedioEjecutadas { get; set; }
        public decimal MinPromedioprogramadasmanuales { get; set; }
        public decimal MinPromedioContestaCorta { get; set; }
        public decimal NumIntentoLlamadasPromedio { get; set; }
        public decimal TotalTimbradoAutomatica { get; set; }
        public decimal TiempoUtil { get; set; }
        public int Orden { get; set; }
    }
    public class ControlOtroMedioDTO
    {
        public string FaseOrigen { get; set; }
        public int IdFaseOrigen { get; set; }
        public int ProgramadasEjecutadasOtroMedio { get; set; }
        public int ProgramadasAutomaticasOtroMedio { get; set; }
        public int ProgramadasManualesOtroMedio { get; set; }
        public int Contactabilidad { get; set; }
        public int Orden { get; set; }
    }
    public class EjecutadasSinCambiodeFaseDTO
    {
        public int IdFaseOrigen { get; set; }
        public string FaseOrigen { get; set; }
        public int Orden { get; set; }
        public int? DiaActual { get; set; }
        public int Uno { get; set; }
        public int Dos { get; set; }
        public int Tres { get; set; }
        public int Cuatro { get; set; }
        public int MasDeCuatro { get; set; }
        public decimal TiempoTotal { get; set; }
        public decimal? DuracionContesto { get; set; }
        public decimal? DuracionLlamadaultimaActividad { get; set; }
    }
    public class EjecutadasSinCambiodeFaseAlternoDTO
    {
        public int IdFaseOrigen { get; set; }
        public string FaseOrigen { get; set; }
        public int Orden { get; set; }
        //public int? DiaActual { get; set; }
        public int Uno { get; set; }
        public int Dos { get; set; }
        public int Tres { get; set; }
        public int Cuatro { get; set; }
        public int MasDeCuatro { get; set; }
        public decimal TiempoTotal { get; set; }
        //public decimal? DuracionContesto { get; set; }
        //public decimal? DuracionLlamadaultimaActividad { get; set; }
    }
    public class CambiosDeFaseControlBICYEDTO
    {
        public string Cambio { get; set; }
        public string FaseOrigen { get; set; }
    }
    public class ReporteCambioFaseFiltroProcedimientoDTO
    {
        public string IdPersonal { get; set; }
        public string IdCentroCosto { get; set; }
        public string IdCategoriaOrigen { get; set; }
        public string IdTipo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public class DiferenciaLlamadasBloqueDTO
    {
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
    }
    public class ConteoDatosFaseDTO
    {
        public string Fase { get; set; }
        public int Inicio { get; set; }
        public int Momento { get; set; }
    }
    public class ConteoDatosFaseAlternoDTO
    {
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaMomento { get; set; }
        public List<ConteoDatosFaseDTO> ConteoDatosFase { get; set; }
    }
    public class ResultadoDiferenciaLlamadasBloqueDTO
    {
        public int? Cero { get; set; }
        public int? MasCero { get; set; }
        public int? MasUno { get; set; }
        public int? MasDos { get; set; }
        public int? MasTres { get; set; }
        public int? MasCuatro { get; set; }
        public int? MasCinco { get; set; }
        public int? MasSeis { get; set; }
    }
    public class ResultadoConteoDatosFaseDTO
    {
        public string FaseOportunidad { get; set; }
        public int Total { get; set; }
    }
    public class ResultadoConteoDatosFaseAlternoDTO
    {
        public string FaseOportunidad { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int Total { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaMomento { get; set; }
    }
    public class LlamadaObservadaDTO
    {
        public string Caso { get; set; }
        public string FaseOportunidad { get; set; }
        public int Cantidad { get; set; }
    }
    public class AcumuladoTiempoContactoEfectivoDTO
    {
        public string FaseOportunidad { get; set; }
        public string TiempoContacto { get; set; }
        public int Total { get; set; }
    }
    public class ActividadEjecutadaFaseActualDTO
    {
        public string FaseOportunidad { get; set; }
        public string Caso { get; set; }
        public int Cantidad { get; set; }
        public float PromedioLlamada { get; set; }
    }
    public class ControlOportunidadPredictivaDTO
    {
        public DateTime FechaCreacion { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public int OA { get; set; }
        public int OC { get; set; }
        public int ISM { get; set; }
        public int ENS { get; set; }
        public int BIC { get; set; }
        public int ODOM { get; set; }
    }
}