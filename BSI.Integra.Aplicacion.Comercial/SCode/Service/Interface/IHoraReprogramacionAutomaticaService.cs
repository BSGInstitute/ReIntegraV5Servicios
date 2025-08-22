namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IHoraReprogramacionAutomaticaService
    {
        string ObtenerFechaHoraActividadReprogramacionAutomatica(int idActividadCabecera, int idCategoriaOrigen, int idPersonal, string codigoFase, int idOcurrencia, List<List<TimeSpan?>> horario);
        DateTime CalcularProgramacionAutomaticaByAsesor(DateTime horaParaProgramar, int flujoNormal, int IntervaloSigProgramacionMin, List<List<TimeSpan?>> horario);
        DateTime CalcularProgramacionAutomaticaMediaNoche(List<List<TimeSpan?>> horario, DateTime horaParaReprogramar, int IntervaloSigProgramacionMin);
        DateTime CalcularProgramacionAutomatica(List<List<TimeSpan?>> horario, DateTime horaParaReprogramar);
        DateTime CalcularHorario(DateTime fecha, int idPersonal, List<List<TimeSpan?>> horario);
        string ObtenerFechaHoraReprogramacionAutomaticaOperaciones(int idOportunidad);
        string ObtenerFechaHoraReprogramacionManualOperaciones(DateTime fecha);
        DateTime ObtenerFechaReprogramacionManualOperaciones(DateTime fecha);
        bool ReprogramarAlumnoClasesOnline(int idAlumno);
    }
}
