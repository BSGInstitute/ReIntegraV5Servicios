using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RecuperacionSesionDTO
    {
        public int? IdRecuperacionSesion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoSesion { get; set; }
        public bool Recupera { get; set; }
        public string Usuario { get; set; }
    }
}
