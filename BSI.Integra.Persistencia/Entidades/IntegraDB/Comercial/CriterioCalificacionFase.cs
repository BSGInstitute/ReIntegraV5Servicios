using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CriterioCalificacionFase : BaseIntegraEntity
    {
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

    }
}