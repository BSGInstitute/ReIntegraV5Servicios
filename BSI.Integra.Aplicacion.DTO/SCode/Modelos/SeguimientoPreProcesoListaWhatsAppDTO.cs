using BSI.Integra.Aplicacion.Base;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos
{
    public class SeguimientoPreProcesoListaWhatsAppDTO : BaseIntegraEntity
    {
        public int IdEstadoSeguimientoPreProcesoListaWhatsApp { get; set; }
        public int IdConjuntoLista { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }
    }
}
