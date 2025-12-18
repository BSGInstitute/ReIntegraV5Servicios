using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    internal class GestionContactoDTO
    {
    }
    public class CrearGestionContactoDTO
    {
        public int IdCentroCosto { get; set; }
        public int IdPersonal_Asignado { get; set; }    // Asesor
        public int IdClasificacionPersona { get; set; } // Docente
        public int IdFaseGestionContacto { get; set; }
        public int IdOrigen { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Comentario { get; set; }
    }
}
