using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RecordatorioClasesOnlineIvr : BaseIntegraEntity
    {
       
        public int IdMatriculaCabecera { get; set; }
        public int IntentoMaximo { get; set; }
        public int Intento { get; set; }
        public bool Concluido { get; set; }
        public bool Ejecutado { get; set; }
    }
}
