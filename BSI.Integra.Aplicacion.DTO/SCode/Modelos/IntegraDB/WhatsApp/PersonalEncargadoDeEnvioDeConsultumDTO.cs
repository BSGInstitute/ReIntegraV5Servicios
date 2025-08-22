using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class PersonalEncargadoDeEnvioDeConsultumDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public DateTime? FechaDia1 { get; set; }
        public DateTime? FechaDia2 { get; set; }
        public DateTime? FechaDia3 { get; set; }
        public DateTime? FechaDia4 { get; set; }
        public DateTime? FechaDia5 { get; set; }
        public int IdConfiguracionDeEnvioParaWhatsApp { get; set; }
    }

    public class PersonalEncargadoDeEnvioDeConsultumObtenerDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public DateTime? FechaDia1 { get; set; }
        public DateTime? FechaDia2 { get; set; }
        public DateTime? FechaDia3 { get; set; }
        public DateTime? FechaDia4 { get; set; }
        public DateTime? FechaDia5 { get; set; }
        public int IdConfiguracionDeEnvioParaWhatsApp { get; set; }
        public string? Nombre { get; set; }
    }
}
