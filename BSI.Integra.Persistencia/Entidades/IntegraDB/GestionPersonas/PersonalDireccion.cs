using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalDireccion :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string Distrito { get; set; }
        public string TipoVia { get; set; }
        public string NombreVia { get; set; }
        public string Manzana { get; set; }
        public int? Lote { get; set; }
        public string TipoZonaUrbana { get; set; }
        public string NombreZonaUrbana { get; set; }
        public bool Activo { get; set; }
    }
}
