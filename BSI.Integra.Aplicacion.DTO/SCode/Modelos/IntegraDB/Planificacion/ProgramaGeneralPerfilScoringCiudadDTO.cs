using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPerfilScoringCiudadDTO
    {
        public int? Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
}
