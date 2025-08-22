using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CrucigramaProgramaCapacitacion : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdPespecifico { get; set; }
        public int? OrdenFilaCapitulo { get; set; }
        public int? OrdenFilaSesion { get; set; }
        public string CodigoCrucigrama { get; set; }
        public int IdTipoMarcador { get; set; }
        public decimal ValorMarcador { get; set; }
        public int CantidadFila { get; set; }
        public int CantidadColumna { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
