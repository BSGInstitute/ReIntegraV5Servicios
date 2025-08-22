namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteProgramasCriticosDTO
    {
        public string Grupo { get; set; }
        public int IdPadre { get; set; }
        public string Padre { get; set; }
        public int IdProgramaGeneral { get; set; }
        public string ProgramaGeneral { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string Modalidad { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public int IdCentroCosto { get; set; }
        public string Programa { get; set; }
        public string FechaInicio { get; set; }
        public DateTime? FechaInicioAuxiliar { get; set; }
        public int BNC { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M_Acumulado { get; set; }
        public double PrecioPromedio { get; set; }
        public double PrecioPromedio10Descuento { get; set; }
        public double IngresoPromedioIS { get; set; }
        public double CostoPrograma { get; set; }
        public int? PuntoEquilibrio { get; set; }
    }

    public class ReporteProgramasCriticosFiltroDTO
    {
        public List<int>? Grupos { get; set; }
        public List<int>? Areas { get; set; }
        public List<int>?  Subareas { get; set; }
        public List<int>? Pais { get; set; }
        public List<string>? EstadoPrograma { get; set; }
        public List<int>? Periodo { get; set; }

    }

    public class ReporteEstructuradoAsignacionProgramasCriticosDTO
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
        public int OrdenAsesorGrupo { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string NombrePaisPersonal { get; set; }
        public string AsignacionPais { get; set; }
        public int CantidadGrupoActual { get; set; }
        public int CantidadOtrosGrupos { get; set; }
        public int BNC_MuyAlta { get; set; }
        public int BNC_Historico { get; set; }
        public int BNC_AltaMediaRemarketing { get; set; }
        public int BNC_TotalDatos { get; set; }
        public int RN { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M { get; set; }
        public int IS_M_Acumulado { get; set; }
        public PaisesReporteProgramasCriticosDTO Paises { get; set; }
    }
    public class PaisesReporteProgramasCriticosDTO
    {
        public int CantidadPeru { get; set; }
        public int CantidadColombia { get; set; }
        public int? CantidadBolivia { get; set; }
        public int CantidadChile { get; set; }
        public int CantidadMexico { get; set; }
        public int CantidadOtros { get; set; }
    }

    public class ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
        public int IdGrupoFiltroProgramaCriticoExterno { get; set; }
        public int OrdenAsesorGrupo { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string NombrePaisPersonal { get; set; }
        public string AsignacionPais { get; set; }
        public int IdCodigoPais { get; set; }
        public int BNC_MuyAlta { get; set; }
        public int BNC_Historico { get; set; }
        public int BNC_AltaMediaRemarketing { get; set; }
        public int BNC_TotalDatos { get; set; }
        public int RN { get; set; }
        public int IT { get; set; }
        public int IP { get; set; }
        public int PF { get; set; }
        public int IC { get; set; }
        public int Seguimiento { get; set; }
        public int TotalDatos { get; set; }
        public int IS_M { get; set; }
        public int IS_M_Acumulado { get; set; }
    }

}


