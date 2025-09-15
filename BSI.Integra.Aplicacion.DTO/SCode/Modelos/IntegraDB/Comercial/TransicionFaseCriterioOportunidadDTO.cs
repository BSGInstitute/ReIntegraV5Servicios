using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{

    public class TransicionFaseCriterioOportunidadDTO
    {
        public int Id { get; set; }
        public int IdTransicionFaseOportunidad { get; set; }
        public int IdCriterioCalificacionFaseOportunidad { get; set; }   
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class TransicionFaseCriterioOportunidadEntradaDTO
    {
        public int Id { get; set; }
        public int IdTransicionFaseOportunidad { get; set; }
        public int IdCriterioCalificacionFaseOportunidad { get; set; }   
        public string Usuario { get; set; }
    }


}
