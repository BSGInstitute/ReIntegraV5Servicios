using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralTagsPwDTO
    {
        public int? IdPgeneral { get; set; }
        public int IdTagPw { get; set; }
    }
    public class CompuestoTagDTO
    {
        public int IdPGeneral { get; set; }
        public TagPwDTO ObjetoTag { get; set; }
        public List<ParametroSeoAsociadosDTO> ListaParametro { get; set; }
    }
}
