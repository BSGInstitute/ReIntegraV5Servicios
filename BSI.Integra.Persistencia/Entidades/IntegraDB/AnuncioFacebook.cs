using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public  partial class AnuncioFacebook : BaseIntegraEntity
    {
        public  void TAnuncioFacebook()
        {
            TAnuncioFacebookMetricas = new HashSet<TAnuncioFacebookMetrica>();
            TOportunidads = new HashSet<TOportunidad>();
        }

    
        public string? FacebookIdAnuncio { get; set; }
       
        public string? FacebookNombreAnuncio { get; set; }
     
        public string? FacebookIdConjuntoAnuncio { get; set; }
       
        public int? IdConjuntoAnuncioFacebook { get; set; }
   
        
        public int? IdMigracion { get; set; }

        public virtual TConjuntoAnuncioFacebook? IdConjuntoAnuncioFacebookNavigation { get; set; }
        public virtual ICollection<TAnuncioFacebookMetrica> TAnuncioFacebookMetricas { get; set; }
        public virtual ICollection<TOportunidad> TOportunidads { get; set; }
    }
}

