using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CeldaDTO
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
    }
    public class CampoObligatorioCeldaDTO
    {
        public string Campo { get; set; }
        public int Columna { get; set; }
        public bool FlagObligatorio { get; set; }
    }
    public class CampoObligatorioDTO
    {
        public string Campo { get; set; }
        public bool FlagObligatorio { get; set; }
    }
}
