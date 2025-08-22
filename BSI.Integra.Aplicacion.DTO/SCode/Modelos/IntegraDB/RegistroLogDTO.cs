namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroLogDTO
    {
        public string Ip { get; set; }
        public string Usuario { get; set; }
        public string Maquina { get; set; }
        public string Ruta { get; set; }
        public string Parametros { get; set; }
        public string Mensaje { get; set; }
        public string Excepcion { get; set; }
        public string Tipo { get; set; }
        public int IdPadre { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
