using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Coordinadora : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public string AliasCorreo { get; set; }
        public string Clave { get; set; }
        public string Firma { get; set; }
        public string Usuario { get; set; }
        public int Anexo { get; set; }
        public string Modalidad { get; set; }
        public bool Genero { get; set; }
        public int IdSede { get; set; }
        public string? HtmlNumero { get; set; }
        public string? HtmlHorario { get; set; }
        public string Iniciales { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
