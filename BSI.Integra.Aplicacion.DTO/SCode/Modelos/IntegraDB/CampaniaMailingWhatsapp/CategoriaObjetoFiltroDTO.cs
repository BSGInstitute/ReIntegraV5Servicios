using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class CategoriaObjetoFiltroDTO
    {
        public string Nombre { get; set; }
        public string NombreObjeto { get; set; }
        public bool EsTabla { get; set; }
        public bool AplicaConjuntoLista { get; set; }
        public bool AplicaFiltroSegmento { get; set; }
        public int? IdMigracion { get; set; }
    }
}
