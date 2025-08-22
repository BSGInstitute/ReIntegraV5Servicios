using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoFuncionDTO
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int NroOrden { get; set; }
        public string Funcion { get; set; }
        public int IdPersonalTipoFuncion { get; set; }
        public string PersonalTipoFuncion { get; set; }
        public int IdFrecuenciaPuestoTrabajo { get; set; }
        public string FrecuenciaPuestoTrabajo { get; set; }
        public int Version { get; set; }
    }
}
