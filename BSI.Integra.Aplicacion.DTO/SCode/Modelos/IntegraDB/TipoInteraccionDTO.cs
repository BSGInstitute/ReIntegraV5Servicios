namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoInteraccionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Canal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class TipoInteraccionFiltroDTO
    {
        public int Id { get; set; }
        public int IdTipoInteraccion { get; set; }
        public string Nombre { get; set; }

        public string Canal { get; set; }

    }
    public class TipoInteraccionCanalDTO
    {
        public string Canal { get; set; }
    }
    public class TipoInteraccionesDTO   {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Canal { get; set; }
       


    }

}
