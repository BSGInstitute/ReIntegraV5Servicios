using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PerfilPuestoTrabajoPersonalAprobacionDTO
    {
        public List<int>? ListaPersonal { get; set; }
        public List<int>? ListaPuestoTrabajo { get; set; }
    }
    public class DatoPersonalPersonalAprobacionDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
    }
    public class DatosCombosPersonalAprobacionDTO
    {
        public IEnumerable<DatoPersonalPersonalAprobacionDTO>? ListaPersonal { get; set; }
        public IEnumerable<ComboDTO>? ListaPuestoTrabajo { get; set; }
    }
    public class PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO
    {
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public List<int> ListaPuestoTrabajo { get; set; }
        public List<string> PuestoTrabajo { get; set; }
    }
    public class PerfilPuestoTrabajoPersonalAprobacionDatosDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public string Personal { get; set; }
    }

    public class EliminarPuestoTrabajoDTO
    {
        public int IdPersonal { get; set;}

        public List<int> IdsPuestoTrabajo { get; set; }
    }
}
