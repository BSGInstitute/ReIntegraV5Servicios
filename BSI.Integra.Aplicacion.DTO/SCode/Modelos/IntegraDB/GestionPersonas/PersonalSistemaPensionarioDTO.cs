using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalSistemaPensionarioDTO
    {
        public string? CodigoAfiliado { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public int? IdSistemaPensionario { get; set; }
        public bool EsModificado { get; set; }
        public bool Activo { get; set; }
        public int Id {  get; set; }

    }
}
