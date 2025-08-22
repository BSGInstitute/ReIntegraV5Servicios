using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PlantillaPw : BaseIntegraEntity
    {
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        [StringLength(250)]
        public string? Descripcion { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int IdRevisionPw { get; set; } 
        public List<PlantillaPais> PlantillaPais { get; set; }
        public List<SeccionPw> SeccionPws { get; set; }
    }
}
