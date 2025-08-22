using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConjuntoListaDetalleWhatsAppDTO
    {
        public int? Id { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdConjuntoLista { get; set; }
        public List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaPrincipal { get; set; }
        public List<WhatsAppConfiguracionEnvioPorProgramaDTO> ProgramaSecundario { get; set; }
    }

    public class ConjuntoListaDetalleSmsDTO
    {
        public int? Id { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdConjuntoLista { get; set; }
        public int? IdPGeneral { get; set; }
    }
}
