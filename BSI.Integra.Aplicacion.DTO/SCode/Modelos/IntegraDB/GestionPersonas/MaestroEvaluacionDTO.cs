using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class MaestroEvaluacionDTO
    {
        public int Id { get; set; }
    }
    public class ObtenerDataComboMaestroEvaluacionDTO
    {
        public List<ComboDTO> ObtenerCategoria { get; set; }
        public List<ComboDTO> ObtenerSexo { get; set; }
        public List<ComboDTO> ObtenerFormula { get; set; }
    }
}
