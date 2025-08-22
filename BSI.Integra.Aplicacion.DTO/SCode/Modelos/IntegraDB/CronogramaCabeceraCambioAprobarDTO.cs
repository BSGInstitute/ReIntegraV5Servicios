namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CronogramaCabeceraCambioAprobarDTO
    {
        public string CodigoMatricula { get; set; }
        public string IdsCambios { get; set; }
        public int Version { get; set; }
        public string NombreSolicitante { get; set; }
        public int IdPersonalAprobador { get; set; }//quien aprueba
        public string Observacion { get; set; }
        public string Cambios { get; set; }
    }
    public class AprobarRechazarCronogramaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string IdCambio { get; set; }
        public int Version { get; set; }
        public bool Aprobado { get; set; }
        public bool Cancelado { get; set; }
    }

    public class CambiosCronogramaCabeceraDTO
    {
        public int Valor { get; set; }
    }
    public class CambioCronogramaDTO
    {
        public string TipoModificacion { get; set; }
        public string SubTipo { get; set; }
        public string EmailAprueba { get; set; }
        public string EmailSolicita { get; set; }
    }


}
