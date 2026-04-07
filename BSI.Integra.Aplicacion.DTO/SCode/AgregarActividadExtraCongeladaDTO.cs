using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTO
{
    public class AgregarActividadExtraCongeladaDTO
    {
        public int IdGestionContactoFlujoCongelado { get; set; }
        public int IdGestionDocenteActividadCabecera { get; set; }
        public List<int> IdPEspecificoSesion_Lista { get; set; }
    }
}
