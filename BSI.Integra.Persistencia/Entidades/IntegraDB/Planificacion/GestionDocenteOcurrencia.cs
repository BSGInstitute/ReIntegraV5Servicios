using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteOcurrencia : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdGestionDocenteOcurrenciaTipo { get; set; }
        public int IdGestionDocenteActividadDetalle { get; set; }
        public int IdGestionDocenteModoMarcado { get; set; }
        public bool RequiereComentario { get; set; }
        public bool RequiereFechaHora { get; set; }
    }
}
