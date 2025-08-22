namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class ProcesoSeleccionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
    public class ProcesoSeleccionConvocatoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public string CodigoConvocatoriaPersonal { get; set; }
    }
    public class ProcesoSeleccionCompuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public List<ConvocatoriaPersonalDetalleDTO> DetalleConvocatoria { get; set; }
    }

    public class ConvocatoriaPersonalDetalleDTO
    {
        public int? IdConvocatoriaPersonal { get; set; }
        public string CodigoConvocatoriaPersonal { get; set; }
        public int? UltimaSecuencia { get; set; }
    }
    public class PostulanteUltimoProcesoSeleccionDTO
    {
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int VersionCentil { get; set; }
    }
    public class ProcesoSeleccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public string Codigo { get; set; }
        public string Url { get; set; }
        public string Activo { get; set; }
        public int? IdSede { get; set; }
        public string Sede { get; set; }
    }


    public class ProcesoSeleccionEstadoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

    public class ProcesoSeleccionComboReporteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPuestoTrabajo { get; set; }
    }

}
