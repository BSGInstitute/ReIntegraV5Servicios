using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteFlujo : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteEstado { get; set; }
        public int IdGestionDocenteCategoria { get; set; }
    }
}
