
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{





    public class FacturamaCredencialesDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? IssuerEmail { get; set; }
        
        public string Password { get; set; }
        public bool Sandbox { get; set; }
        public bool Estado { get; set; }
    }
   


   
    public class RegimenFiscalDTO
    {
        public int Id { get; set; }
        public string FiscalRegime { get; set; }
        public string Descripcion { get; set; }
    }

    public class UsoCfdiDTO
    {
        public int Id { get; set; }
        public string CfdiUse { get; set; }
        public string Descripcion { get; set; }
    }
    public class FormapagoFacturamaDTO
    {
        public int Id { get; set; }
        public string PaymentForm { get; set; }
        public string Descripcion { get; set; }
    }
    public class RegimenFiscalDatosDTO
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
    public class RegimenFiscalAcDTO
    {
        public int Id { get; set; }    
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class RegimenFiscalEliDTO
    {
        public int Id { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class UsoCfdiDatosDTO
    {
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
    public class ResumenMatriculaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string NombrePais { get; set; }
        public string EstadoMatricula { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public DateTime UltimaFechaVencimiento { get; set; }
    }

}
