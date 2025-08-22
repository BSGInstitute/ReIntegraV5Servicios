using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class LocacionDTO
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int IdRegion { get; set; }
        public int IdCiudad { get; set; }
        [StringLength(300)]
        public string Direccion { get; set; }
    }
    public class LocacionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
    }
}
