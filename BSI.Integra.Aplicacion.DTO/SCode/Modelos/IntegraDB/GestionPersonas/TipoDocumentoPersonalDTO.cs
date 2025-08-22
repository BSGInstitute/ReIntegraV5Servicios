using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class TipoDocumentoPersonalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NroDigitos { get; set; }
    }

    public class TipoDocumentoPersonalComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
