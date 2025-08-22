using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public partial  class   CampaniaFacebook : BaseIntegraEntity
    {
        public void TCampaniaFacebook()
        {
            TConjuntoAnuncioFacebooks = new HashSet<TConjuntoAnuncioFacebook>();
        }

     
        public string? FacebookIdCampania { get; set; }
        
        public string? FacebookNombreCampania { get; set; }
      
        public string? FacebookIdCuenta { get; set; }
        
        public int? IdMigracion { get; set; }

        public virtual ICollection<TConjuntoAnuncioFacebook> TConjuntoAnuncioFacebooks { get; set; }
    }
}
