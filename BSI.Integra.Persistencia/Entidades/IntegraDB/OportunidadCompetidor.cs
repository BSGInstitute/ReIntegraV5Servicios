using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadCompetidor : BaseIntegraEntity
    {
        public int? IdOportunidad { get; set; }
        [StringLength(50)]
        public string OtroBeneficio { get; set; } = null!;
        public int Respuesta { get; set; }
        [StringLength(10)]
        public string Completado { get; set; } = null!;
        public byte[] RowVersion { get; set; } = null!;
        public ICollection<OportunidadPrerequisitoGeneral> OportunidadPrerequisitoGenerals { get; set; }
        public ICollection<OportunidadPrerequisitoEspecifico> OportunidadPrerequisitoEspecificos { get; set; }
        public ICollection<DetalleOportunidadCompetidor> DetalleOportunidadCompetidors { get; set; }
        public ICollection<OportunidadBeneficio> OportunidadBeneficios { get; set; }
    }
}
