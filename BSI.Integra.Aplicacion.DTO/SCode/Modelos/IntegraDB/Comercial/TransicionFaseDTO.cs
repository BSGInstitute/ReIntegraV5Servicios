using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TransicionFaseDTO
    {
        public int? Id { get; set; }
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public List<CriterioCalificacionFaseOportunidadDTO>? CriteriosCalificacion { get; set; }
    }
}