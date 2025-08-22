using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoIdentificador : BaseIntegraEntity
    {
        [StringLength(600)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string Descripcion { get; set; } = null!;
        public int IdPais { get; set; }
    }
}
