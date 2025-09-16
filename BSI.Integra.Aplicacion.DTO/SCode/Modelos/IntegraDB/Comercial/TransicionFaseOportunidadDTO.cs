using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TransicionFaseOportunidadDTO
    {
        public int? Id { get; set; }
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public List<TransicionFaseCriterioOportunidadDTO> TransicionFaseCriterioOportunidad { get; set; }
    }

    public class TransicionFaseOportunidadEntradaDTO
    {
        public int Id { get; set; }
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public List<TransicionFaseCriterioOportunidadDTO> TransicionFaseCriterioOportunidad { get; set; }
    }

}