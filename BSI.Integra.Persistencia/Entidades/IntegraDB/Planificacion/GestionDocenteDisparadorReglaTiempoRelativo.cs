using BSI.Integra.Aplicacion.Base;
using System;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteDisparadorReglaTiempoRelativo : BaseIntegraEntity
    {
        public int IdGestionDocenteDisparadorReglaTiempo { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public int Cantidad { get; set; }
        public int IdGestionDocenteUnidadTiempo { get; set; }
    }
}
