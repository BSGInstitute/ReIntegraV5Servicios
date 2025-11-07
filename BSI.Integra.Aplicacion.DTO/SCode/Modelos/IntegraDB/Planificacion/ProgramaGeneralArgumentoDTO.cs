namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{

    public class ProgramaGeneralArgumentoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProgramaGeneralArgumentoModalidadDTO> Modalidades { get; set; } = [];
        public List<ProgramaGeneralArgumentoDetalleDTO> ArgumentoDetalle { get; set; } = [];
    }

    public class ProgramaGeneralArgumentoModalidadDTO
    {
        public int Id { get; set; } //nullable
        public int IdModalidad { get; set; } //ModalidadCurso
        public string Nombre { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleDTO
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public PGArgumentoDetalleMotivacionDTO Motivacion { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleModelDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumento { get; set; }

        public string Detalle { get; set; }
   
    }

    public class PGArgumentoDetalleMotivacionDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleMotivacionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumentoDetalle { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string NombreMotivacion { get; set; }
    }
    public class ArgumentoMotivacionProgramaGeneralDTO
    {
        public string MotivacionPrincipal { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> GarantiaDePrograma { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> EstructuraCurricular { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> DemostracionDeValor { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> AspectosDiferenciadores { get; set; }
        public List<ArgumentoMotivacionEstructuraDTO> ArgumentosDePerdidaPotencial { get; set; }
    }
    public class ArgumentoMotivacionEstructuraDTO
    {
        public ProgramaGeneralArgumentoDTO Argumento { get; set; }
        public List<ProgramaGeneralArgumentoDetalleDTO> ArgumentoDetalle { get; set; }
        public List<ProgramaGeneralArgumentoModalidadDTO> Modalidades { get; set; }
    }
    public class ProgramaGeneralArgumentoMotivacionDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
       
    }

    public class ProgramaArgumentoMotivacionSeleccionDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPGeneral { get; set; }
        public List<SeleccionMotivacionDTO> SeleccionMotivacion { get; set; } = [];
    }

    public class SeleccionMotivacionDTO
    {
        public int IdMotivacion { get; set; }
        public string descripcionMotivacion { get; set; }
        public bool seleccionado { get; set; }
    }
    public class ProgramaGeneralArgumentoDetalleMotivacionNombreDTO
    {
        public int IdProgramaGeneralArgumentoDetalleMotivacion { get; set; }
	    public int IdProgramaGeneralArgumentoDetalle { get; set; }
	    public int IdProgramaMotivacion { get; set; }
        public string NombreMotivacion { get; set; }
    }
      
    public class FactorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class FactorDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
    }
    public class FactorSolucionDTO
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
    }
    public class SubSolucionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public string Solucion { get; set; }
        public int Orden { get; set; }
        public int Nivel { get; set; }
    }
    public class ProgramaGeneralProblemaDetalleObtener2
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public int? IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
        public List<int> SubSolucionIds { get; set; }
    }
    public class ConfiguracionProblemaJerarquicaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public FactorDTO Factor { get; set; }
        public FactorDetalleDTO Detalle { get; set; }
        public FactorSolucionDTO Solucion { get; set; }
        public List<SubSolucionDTO> SubSoluciones { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
    }
    public class OportunidadMotivacionSeleccionViewDTO
    {
        public int IdOportunidadProgramaMotivacionSeleccion { get; set; }
        public int IdOportunidad { get; set; }
        public int IdProgramaMotivacion { get; set; }
        public string NombreMotivacion { get; set; }
        public int? Prioridad { get; set; }
    }

    public class MotivacionDiccionarioViewDTO
    {
        public int IdProgramaMotivacion { get; set; }
        public string DescripcionProgramaMotivacion { get; set; }
        public string NombreMotivacionAlterno { get; set; }
    }
}
