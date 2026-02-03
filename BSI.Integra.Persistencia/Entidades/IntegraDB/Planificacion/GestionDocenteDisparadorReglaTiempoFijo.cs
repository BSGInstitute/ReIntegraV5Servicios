using BSI.Integra.Aplicacion.Base;
using System;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteDisparadorReglaTiempoFijo : BaseIntegraEntity
    {
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public DateTime Fecha { get; set; }
    }
}
