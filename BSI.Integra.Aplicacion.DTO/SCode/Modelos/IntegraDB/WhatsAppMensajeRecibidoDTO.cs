namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppMensajesRecibidosOperacionesDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonal_Asignado { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdActividadCabecera { get; set; }
        public string ActividadCabecera { get; set; }
        public int IdTipoDato { get; set; }
        public bool ReprogramacionManual { get; set; }
        public bool ReprogramacionAutomatica { get; set; }
        public string Asesor { get; set; }
        public string NombrePersonal { get; set; }
        public string Contacto { get; set; }
        public string CentroCosto { get; set; }
        public string Celular { get; set; }
        public int? IdCodigoPais { get; set; }
        public string Email1 { get; set; }
        public int? IdPadre { get; set; }
        public string? PEspecifico { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public string UltimoComentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdClasificacionPersona { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdEstadoMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public string DNI { get; set; }
        public int? GrupoCurso { get; set; }
        public string SubEstadoMatricula { get; set; }
        public int TipoMensaje { get; set; }
    }
}
