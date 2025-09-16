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
    }
    public class TransicionFaseCriterioOportunidadEntradaDTO
    {
        public int Id { get; set; }
        public int IdTransicionFaseOportunidad { get; set; }
        public int IdCriterioCalificacionFaseOportunidad { get; set; }   
        public string Usuario { get; set; }
    }


}
