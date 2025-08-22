namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MedioPagoMatriculaCronogramaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdMedioPago { get; set; }
        public bool Activo { get; set; }
    }
}
