using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class DatoFamiliarPersonalDTO
    {
        public string Apellidos { get; set; }
        public bool DerechoHabiente { get; set; }
        public bool EsContactoInmediato { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Id { get; set; }
        public int IdParentescoPersonal { get; set; }
        public int IdPersonal { get; set; }
        public int IdSexo { get; set; }
        public int IdTipoDocumentoPersonal { get; set; }
        public string Nombres { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroReferencia { get; set; }
    }

}
