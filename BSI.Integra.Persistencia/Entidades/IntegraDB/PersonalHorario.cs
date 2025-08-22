using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PersonalHorario : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public TimeSpan? Lunes1 { get; set; }
        public TimeSpan? Lunes2 { get; set; }
        public TimeSpan? Lunes3 { get; set; }
        public TimeSpan? Lunes4 { get; set; }
        public TimeSpan? Martes1 { get; set; }
        public TimeSpan? Martes2 { get; set; }
        public TimeSpan? Martes3 { get; set; }
        public TimeSpan? Martes4 { get; set; }
        public TimeSpan? Miercoles1 { get; set; }
        public TimeSpan? Miercoles2 { get; set; }
        public TimeSpan? Miercoles3 { get; set; }
        public TimeSpan? Miercoles4 { get; set; }
        public TimeSpan? Jueves1 { get; set; }
        public TimeSpan? Jueves2 { get; set; }
        public TimeSpan? Jueves3 { get; set; }
        public TimeSpan? Jueves4 { get; set; }
        public TimeSpan? Viernes1 { get; set; }
        public TimeSpan? Viernes2 { get; set; }
        public TimeSpan? Viernes3 { get; set; }
        public TimeSpan? Viernes4 { get; set; }
        public TimeSpan? Sabado1 { get; set; }
        public TimeSpan? Sabado2 { get; set; }
        public TimeSpan? Sabado3 { get; set; }
        public TimeSpan? Sabado4 { get; set; }
        public TimeSpan? Domingo1 { get; set; }
        public TimeSpan? Domingo2 { get; set; }
        public TimeSpan? Domingo3 { get; set; }
        public TimeSpan? Domingo4 { get; set; }
        public bool Activo { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
