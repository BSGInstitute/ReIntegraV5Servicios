using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Pais : BaseIntegraEntity
    {
        public int CodigoPais { get; set; }
        public string CodigoIso { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public decimal ZonaHoraria { get; set; }
        public int EstadoPublicacion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? CodigoGoogleId { get; set; }
        public string? CodigoPaisMoodle { get; set; }
        public string? RutaBandera { get; set; }
        public string? RutaIcono { get; set; }
        public int? EstadoVisualizacion { get; set; }
        public ICollection<WhatsAppMensajeRecibido> WhatsAppMensajeRecibidos { get; set; }
        ///<value>51</value>
        public const int CodigoPeru = 51;
        ///<value>52</value>
        public const int CodigoMexico = 52;
        ///<value>56</value>
        public const int CodigoChile = 56;
        ///<value>57</value>
        public const int CodigoColombia = 57;
        ///<value>591</value>
        public const int CodigoBolivia = 591;
    }
}
