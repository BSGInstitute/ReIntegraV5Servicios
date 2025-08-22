namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string CuentaGeneral { get; set; } = null!;
        public string CuentaGeneralCodigo { get; set; } = null!;
        public string CuentaEspecifica { get; set; } = null!;
        public string CuentaEspecificaCodigo { get; set; } = null!;
        public int IdProductoPresentacion { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
    }
    public class ProductoDatosDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
        public string CuentaGeneral { get; set; } = null!;
        public string CuentaGeneralCodigo { get; set; } = null!;
        public string CuentaEspecifica { get; set; } = null!;
        public string CuentaEspecificaCodigo { get; set; } = null!;

        public int IdProductoPresentacion { get; set; }

    }


}
