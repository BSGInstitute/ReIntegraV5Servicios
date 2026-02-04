using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TransicionFase : BaseIntegraEntity
    {
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public List<CriterioCalificacionFase> CriteriosCalificacion { get; set; }
    }
}