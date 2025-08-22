using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlanContableDTO
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public string? Descripcion { get; set; }
        public int Padre { get; set; }
        public bool Univel { get; set; }
        public string? Cbal { get; set; }
        public string? Debe { get; set; }
        public string? Haber { get; set; }
        public int? IdTipoCuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public string? Analisis { get; set; }
        public string? CentroCosto { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
    }
    public class PlanContableComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class PlanContableCuentasDTO
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public string Nombre { get; set; }
    }
    public class PlanContableConRubroDTO
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
        public string? Descripcion { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
        public string? NombreFurTipoSolicitud { get; set; }
    }
    public class PlanContableConRubroRequestDTO
    {
        public int IdPlanContable { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
        public string? UsuarioModificacion { get; set; }
    }

    public class PlanContableFiltroDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }

    }
    public class PlanContableDatosDTO
    {
        public int Id { get; set; }
        public long Cuenta { get; set; }
     
        public string Descripcion { get; set; } = null!;
        public int Padre { get; set; }
        public bool? Univel { get; set; }
 
        public string Cbal { get; set; } = null!;

        public string Debe { get; set; } = null!;
  
        public string Haber { get; set; } = null!;
        public int? IdPlanContableTipoCuenta { get; set; }
    
        public string? Analisis { get; set; }

        public string? CentroCosto { get; set; }
        public int? IdFurTipoSolicitud { get; set; }
    }

}



