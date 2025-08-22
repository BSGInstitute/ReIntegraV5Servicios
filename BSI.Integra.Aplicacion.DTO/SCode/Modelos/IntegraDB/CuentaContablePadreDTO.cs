namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CuentaContablePadreDTO
    {
        public int Id { get; set; }
        public int CuentaPadre { get; set; }
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class CuentaContablePadreComboDTO
    {
        public int Id { get; set; }
        public int CuentaPadre { get; set; }
    }
    public class CuentasContablePadreDTO
    {
        public int Id { get; set; }
        public int CuentaPadre { get; set; }
        public string Descripcion { get; set; } = null!;
        
    }


}
