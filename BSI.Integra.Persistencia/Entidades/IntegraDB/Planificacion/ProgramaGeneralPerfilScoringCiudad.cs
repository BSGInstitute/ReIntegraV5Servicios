using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPerfilScoringCiudad : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        [StringLength(500)]
        public string Nombre { get; set; }
        public int? IdCiudad { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
