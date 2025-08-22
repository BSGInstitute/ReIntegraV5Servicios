using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampaniaGeneral: BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdCategoriaOrigen { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
        public int? IdHoraEnvioMailing { get; set; }
        public int? IdTipoAsociacion { get; set; }
        public int? IdProbabilidadRegistroPw { get; set; }
        public int? NroMaximoSegmentos { get; set; }
        public int? CantidadPeriodoSinCorreo { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int? IdPlantillaMailing { get; set; }
        public int? IdRemitenteMailing { get; set; }
        public bool? IncluyeWhatsapp { get; set; }
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        public DateTime? FechaFinEnvioWhatsapp { get; set; }
        public int? NumeroMinutosPrimerEnvio { get; set; }
        public int? IdHoraEnvioWhatsapp { get; set; }
        public int? DiasSinWhatsapp { get; set; }
        public int? IdPlantillaWhatsapp { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
        public bool? IncluirRebotes { get; set; }
        public int IdEstadoEnvioMailing { get; set; }
        public int IdEstadoEnvioWhatsapp { get; set; }

    }
}
