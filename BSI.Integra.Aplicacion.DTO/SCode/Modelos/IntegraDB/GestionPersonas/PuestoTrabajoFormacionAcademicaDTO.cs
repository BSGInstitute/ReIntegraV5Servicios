using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoFormacionAcademicaDTO
    {

        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public List<int> IdTipoFormacion { get; set; }
        public List<int> IdNivelEstudio { get; set; }
        public List<int> IdAreaFormacion { get; set; }
        public List<int> IdCentroEstudio { get; set; }
        public List<int> IdGradoEstudio { get; set; }
    }
    public class PuestoTrabajoFormacionAcademicaFiltroDTO
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public string IdTipoFormacion { get; set; }
        public string IdNivelEstudio { get; set; }
        public string IdAreaFormacion { get; set; }
        public string IdCentroEstudio { get; set; }
        public string IdGradoEstudio { get; set; }

        //public string TipoFormacion { get; set; }
        //public string NivelEstudio { get; set; }
        //public string AreaFormacion { get; set; }
        //public string CentroEstudio { get; set; }
        //public string GradoEstudio { get; set; }

    }
}
