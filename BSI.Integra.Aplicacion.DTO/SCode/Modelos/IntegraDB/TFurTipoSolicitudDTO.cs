using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TFurTipoSolicitudDTO : BaseIntegraEntity
    {

        public string Nombre {get;set;}
        public string Descripcion { get; set; }
    }
    public class TFurTipoSolicitudDTOV2
    {
        public int Id {get;set;}
        public string Nombre {get;set;}
        public string Descripcion { get; set; }
    }
    public class FurTipoSolicitudDTO 
    {
        public int? Id { get; set; } = null;
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
