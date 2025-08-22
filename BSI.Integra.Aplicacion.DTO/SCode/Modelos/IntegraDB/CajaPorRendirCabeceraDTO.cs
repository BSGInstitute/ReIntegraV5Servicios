namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CajaPorRendirCabeceraDTO
    {
        public int Id { get; set; }
        public int IdCaja { get; set; }
        public string Codigo { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public bool EsRendido { get; set; }
        public decimal MontoDevolucion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    public class CajaPorRendirCabeceraComboDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
    }

}
