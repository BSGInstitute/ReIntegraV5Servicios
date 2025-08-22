using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class BloqueHorarioProcesaOportunidad : BaseIntegraEntity
    {
        public bool Activo { get; set; }
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;
        [StringLength(30)]
        public string Sede { get; set; } = null!;
        [StringLength(20)]
        public string Dia { get; set; } = null!;
        public bool TurnoM { get; set; }
        public TimeSpan HoraInicioM { get; set; }
        public TimeSpan HoraFinM { get; set; }
        public bool TurnoT { get; set; }
        public TimeSpan HoraInicioT { get; set; }
        public TimeSpan HoraFinT { get; set; }
        [StringLength(150)]
        public string ProbabilidadOportunidad { get; set; } = null!;
        public bool Prelanzamiento { get; set; }
        public int? IdDiaSemana { get; set; }
    }
}
