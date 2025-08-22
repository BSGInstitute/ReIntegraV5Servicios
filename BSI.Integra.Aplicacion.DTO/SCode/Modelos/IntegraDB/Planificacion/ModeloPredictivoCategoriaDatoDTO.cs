using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ModeloPredictivoCategoriaDatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public double Valor { get; set; }
        public bool Validar { get; set; }
        public int IdSubCategoriaDato { get; set; }
    }
}
