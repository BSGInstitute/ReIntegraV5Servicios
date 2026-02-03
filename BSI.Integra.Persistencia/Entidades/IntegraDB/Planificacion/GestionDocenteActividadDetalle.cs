using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class GestionDocenteActividadDetalle : BaseIntegraEntity
    {
        public int IdGestionDocenteActividadCabecera { get; set; }
        public int IdGestionDocenteActividadDetalleTipo { get; set; }
        public int IdPlantillaMedioComunicacion { get; set; }
        public int IdGestionDocenteDisparadorDetalle { get; set; }
        public string Nombre { get; set; }
    }
}
