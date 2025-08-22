namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TagsEstiloDTO
    {
        public int Id { get; set; }
        public int IdTag { get; set; }
        public int IdEstilo { get; set; }
        public string Valor { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class TagsEstiloEnvio
    {
        public int Id { get; set; }
        public int IdTag { get; set; }
        public int IdEstilo { get; set; }
        public string? Valor { get; set; }
        public string Usuario { get; set; } = null!;
    }




    public class EstiloValor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }

        public string IdTag { get; set; }

        public string NombreTipo { get; set; }

        public string Tipo { get; set; }

    }

}
