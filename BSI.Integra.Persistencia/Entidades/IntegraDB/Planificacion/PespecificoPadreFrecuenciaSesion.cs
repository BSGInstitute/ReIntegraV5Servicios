using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoPadreFrecuenciaSesion : BaseIntegraEntity
    {
        public int IdPespecificoPadreFrecuencia { get; set; }
        public int Sesion { get; set; }
        public int IdDiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public decimal Duracion { get; set; }

    }
}
