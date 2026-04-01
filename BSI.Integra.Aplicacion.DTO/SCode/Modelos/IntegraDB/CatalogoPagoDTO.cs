namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para lectura de modalidad de pago
    /// </summary>
    public class ModalidadPagoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    /// <summary>
    /// DTO para lectura de estado de pago
    /// </summary>
    public class PagoEstadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
