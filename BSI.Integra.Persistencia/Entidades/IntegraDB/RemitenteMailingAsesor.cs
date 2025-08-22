using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RemitenteMailingAsesor: BaseIntegraEntity
    {
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public int IdSenderSendinBlue { get; set; }
    }
}
