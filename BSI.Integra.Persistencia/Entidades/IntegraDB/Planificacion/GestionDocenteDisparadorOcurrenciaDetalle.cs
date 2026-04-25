using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteDisparadorOcurrenciaDetalle : BaseIntegraEntity
    {
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int IdGestionDocenteOcurrenciaPrevia { get; set; }
    }
}
