using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PersonalEncargadoDeEnvioDeConsultum : BaseIntegraEntity
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
}
