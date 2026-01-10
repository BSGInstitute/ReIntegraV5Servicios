namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    /// <summary>
    /// DTO para actualizar el centro de costo de una actividad
    /// </summary>
    public class ActualizarCentroCostoDTO
    {
        /// <summary>
        /// Id del Centro de Costo
        /// </summary>
        public int IdCentroCosto { get; set; }

        /// <summary>
        /// Id de la Actividad
        /// </summary>
        public int IdActividad { get; set; }
    }
}
