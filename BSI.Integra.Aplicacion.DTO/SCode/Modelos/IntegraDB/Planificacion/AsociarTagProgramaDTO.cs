using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class AsociarTagProgramaComboDTO
    {
        public IEnumerable<ComboDTO> Area { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> SubArea { get; set; }
        public IEnumerable<ComboDTO> CategoriaPrograma { get; set; }
        public IEnumerable<ParametroSeoPwDTO> ParametroSeo { get; set; }
    }
}
