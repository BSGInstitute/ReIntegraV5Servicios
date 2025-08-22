using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
	public class ObtenerPrioridadesDeFiltroWppDTO
	{
        public int Id { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public TConfiguracionDeEnvioParaWhatsAppHelper Configuracion { get; set; }
        public List<TPersonalEncargadoDeEnvioDeConsultaHelper> Encargados { get; set; }
    }
    public class ObtenerPrioridadesDeFiltroWppSQL
    {

        public int Id { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int IdPersonalEncargadoDeEnvioDeConsulta { get; set; }
        public int IdPersonal { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public string FechaDia1 { get; set; }
        public string FechaDia2 { get; set; }
        public string FechaDia3 { get; set; }
        public string FechaDia4 { get; set; }
        public string FechaDia5 { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConfiguracionDeEnvioParaWhatsApp { get; set; }
        public string FechaDeEnvio { get; set; }
        public string FechaFinDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public string HoraDeEnvio { get; set; }
    }
    public class TPersonalEncargadoDeEnvioDeConsultaHelper
    {
        public int IdPersonalEncargadoDeEnvioDeConsulta { get; set; }
        public int IdPersonal { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public string? FechaDia1 { get; set; }
        public string? FechaDia2 { get; set; }
        public string? FechaDia3 { get; set; }
        public string? FechaDia4 { get; set; }
        public string? FechaDia5 { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
    }
    public class TConfiguracionDeEnvioParaWhatsAppHelper
    {
        public int IdPlantilla { get; set; }
        public int IdConfiguracionDeEnvioParaWhatsApp { get; set; }
        public string FechaDeEnvio { get; set; }
        public string FechaFinDeEnvio { get; set; }
        public int TiempoEntreEnvios { get; set; }
        public string HoraDeEnvio { get; set; }
    }
}
