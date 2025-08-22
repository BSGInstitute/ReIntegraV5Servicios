using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class AmbienteDTO
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Nombre { get; set; }
        public int IdLocacion { get; set; }
        public int IdTipoAmbiente { get; set; }
        public int Capacidad { get; set; }
        public bool Virtual { get; set; }
    }
    public class AmbienteCiudadFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdLocacion { get; set; }
        public int IdCiudad { get; set; }
    }
}
