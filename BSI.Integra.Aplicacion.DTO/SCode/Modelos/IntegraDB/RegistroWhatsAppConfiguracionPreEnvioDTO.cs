using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroWhatsAppConfiguracionPreEnvioDTO
    {
        public int NumeroValidos { get; set; }
        public int NumerosNoValidos { get; set; }
        public List<VistaWhatsAppConfiguracionPreEnvioDTO> ListaPreConfigurados { get; set; }
    }
    public class VistaWhatsAppConfiguracionPreEnvioDTO
    {
        public int Id { get; set; }
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Alumno { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public string Personal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public string WhatsAppEstadoValidacion { get; set; }
        public string objetoplantilla { get; set; }
    }
}
