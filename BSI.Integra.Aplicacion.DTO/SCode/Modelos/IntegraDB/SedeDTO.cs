using BSI.Integra.Aplicacion.Base;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SedeDTO
    {
        public int IdPais { get; set; }
        public string Codigo { get; set; }
        public int? IdCiudad { get; set; }
    }
    public class TSedeDTO : BaseIntegraEntity
    {
        public int IdPais { get; set; }
        public string Codigo { get; set; }
        public int? IdCiudad { get; set; }
    }
}
