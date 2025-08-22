using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DatosMatriculaCabeceraDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
    }

    public class DatosMatriculaEstadosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoMatricula { get; set; }
        public string EstadoPagoMatricula { get; set; }
        public int IdEstadoMatricula { get; set; }
        public string Estado_matricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }

    }
    public class registroDatosMatriculaCabeceraCompletoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int GrupoCurso { get; set; }
        public DateTime FechaMatricula { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public int IdPGeneralPadre { get; set; }
        public int IdPGeneralHijo { get; set; }
        public int IdPEspecificoHijo { get; set; }
    }
}
