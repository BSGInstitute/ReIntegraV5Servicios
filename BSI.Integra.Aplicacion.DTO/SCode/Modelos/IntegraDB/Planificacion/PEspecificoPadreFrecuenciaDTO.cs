using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PEspecificoPadreFrecuenciaDTO
    {
        public int Id { get; set; }
        public int IdFrecuencia { get; set; }
        public int IdPespecifico { get; set; }
        public int IdTiempoFrecuencia { get; set; }
        public string Nota { get; set; }
        public List<PEspecificoPadreFrecuenciaSesionDTO> Sesiones { get; set; }
    }

    public class PEspecificoPadreFrecuenciaSesionDTO
    {
        public int Id { get; set; }
        public int IdPespecificoPadreFrecuencia { get; set; }
        public int Sesion { get; set; }
        public int IdDiaSemana { get; set; }
        public string? Nombre { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public decimal Duracion { get; set; }
        public bool? Delete { get; set; }
    }
}
