using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class DatoFamiliarPersonal:BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdSexo { get; set; }
        public int IdParentescoPersonal { get; set; }
        public int IdTipoDocumentoPersonal { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroReferencia1 { get; set; }
        public string NumeroReferencia2 { get; set; }
        public bool DerechoHabiente { get; set; }
        public bool EsContactoInmediato { get; set; }
    }
}
