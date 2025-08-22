using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CriterioEvaluacionPEspecificoDTO
    {

    }
	public class DatosListaPespecificoEsquemaDTO
    {
		public int TotalRegistros { get; set; }
		public int? IdArea { get; set; }
		public string Area { get; set; }
		public int? IdSubArea { get; set; }
		public string SubArea { get; set; }
		public int? IdPGeneral { get; set; }
		public string PGeneral { get; set; }
		public int? IdProgramaEspecifico { get; set; }
		public string ProgramaEspecifico { get; set; }
		public int? IdCentroCosto { get; set; }
		public string CentroCosto { get; set; }
		public int? IdEstadoPEspecifico { get; set; }
		public string EstadoProgramaEspecifico { get; set; }
		public int? CodigoBs { get; set; }
		public string Ciudad { get; set; }
		public int? IdModalidadCurso { get; set; }
		public string ModalidadCurso { get; set; }
		public string TipoSesion { get; set; }
		public string TipoProgramaGeneral { get; set; }
	}
	public class FiltroProgramaEspecificoEsquemaFiltroCompuestoDTO
	{
		public PaginadorDTO paginador { get; set; }
		public ProgramaEspecificoEsquemaFiltroDTO? filtroRegistros { get; set; }
	}
	public class ProgramaEspecificoEsquemaFiltroDTO
	{
		public string IdArea { get; set; }
		public string IdSubArea { get; set; }
		public string IdPGeneral { get; set; }
		public string IdProgramaEspecifico { get; set; }
		public string IdCentroCosto { get; set; }
		public string IdEstadoPEspecifico { get; set; }
		public string CodigoBs { get; set; }
		public string IdCentroCostoD { get; set; }
	}
}
