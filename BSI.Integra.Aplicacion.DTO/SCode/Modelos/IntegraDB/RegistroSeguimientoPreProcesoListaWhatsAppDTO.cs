using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroSeguimientoPreProcesoListaWhatsAppDTO
    {
        public int Id { get; set; }
        public int IdEstadoSeguimientoPreProcesoListaWhatsApp { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int IdConjuntoLista { get; set; }
    }
}
