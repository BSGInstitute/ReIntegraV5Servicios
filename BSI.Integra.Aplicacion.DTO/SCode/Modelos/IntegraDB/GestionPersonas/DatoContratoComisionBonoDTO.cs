using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class DatoContratoComisionBonoDTO
    {
        public int IdDatoContratoPersonal { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } = null!;
        public string? TipoRemuneracionVariable { get; set; }
    }
}
