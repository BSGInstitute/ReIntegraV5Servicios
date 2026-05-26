using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlanSuscripcionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPremium { get; set; }
        public int Orden { get; set; }
        public List<PlanSuscripcionBeneficioDTO> Beneficios { get; set; }
    }

    public class PlanSuscripcionInsertarDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPremium { get; set; }
        public int Orden { get; set; }
        public List<BeneficioActualizarDTO> Beneficios { get; set; }
    }

    public class PlanSuscripcionActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool EsPremium { get; set; }
        public int Orden { get; set; }
        public List<BeneficioActualizarDTO> Beneficios { get; set; }
    }

    public class BsgTentoBeneficioDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }

    public class BeneficioActualizarDTO
    {
        public int IdBeneficio { get; set; }
        public bool Activo { get; set; }
    }

    public class PlanSuscripcionBeneficioDTO
    {
        public int Id { get; set; }
        public int IdPlanSuscripcion { get; set; }
        public int IdBeneficio { get; set; }
        public bool Activo { get; set; }
        public string CodigoBeneficio { get; set; }
        public string NombreBeneficio { get; set; }
    }

    public class PlataformaTiendaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
    }
}
