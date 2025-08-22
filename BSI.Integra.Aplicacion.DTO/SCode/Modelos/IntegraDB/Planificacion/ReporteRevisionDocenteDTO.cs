using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteRevisionDocenteDTO
    {
        public List<int>? ListaArea { get; set; }
        public List<int>? ListaSubArea { get; set; }
        public List<int>? ListaProgramaGeneral { get; set; }
        public List<int>? ListaDocente { get; set; }
        public int? IdCategoriaRevision { get; set; }
    }
    public class CombosReporteRevisionDocenteDTO
    {
        public List<ComboDTO> AreaCapacitacion { get; set; }
        public List<SubAreaCapacitacionFiltroDTO>  SubAreaCapacitacion { get; set; }

        public List<PGeneralSubAreaCapacitacionFiltroDTO> PGeneral { get; set; }
        public List<ComboDTO> Proveedor { get; set; }

    }
    public class RespuestaReporteRevisionDocenteDTO
    {
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdPGeneral { get; set; }
        public int? IdCategoriaRevision { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdModalidadCurso { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string PGeneral { get; set; }
        public string CategoriaRevision { get; set; }
        public string Nombre { get; set; }
        public string PersonalAsignado { get; set; }
        public string ModalidadCurso { get; set; }
    }
}
