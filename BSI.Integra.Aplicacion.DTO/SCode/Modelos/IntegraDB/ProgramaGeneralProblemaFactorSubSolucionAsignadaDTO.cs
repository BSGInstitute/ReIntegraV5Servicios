using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; } 
    }
    public class ProgramaGeneralProblemaFactorSubSolucionAsignadaObtenerDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
    }
    public class ProgramaGeneralProblemaFactorSubSolucionAsignadaAgendaDTO
    {
        public int IdProgramaGeneralProblemaFactorSubSolucionAsignada { get; set; }
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
        public string SubSolucion { get; set; }
        public int SubSolucionOrden { get; set; }
        public int SubSolucionNivel { get; set; }
    }
    public class ProblemaAgendaRow
    {
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public string ProblemaClienteNombre { get; set; }
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public string? ProblemaClienteDetalleTabs { get; set; }
        public string? ProblemaClienteDetalleTitulo { get; set; }
        public string? ProblemaClienteDetallePiePagina { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public int? IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public string? ProblemaClienteSolucionDescripcion { get; set; }
        public string? ProblemaClienteSolucionTitulo { get; set; }
        public string? ProblemaClienteSolucionSubTitulo { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }

      
        public int IdProgramaGeneralProblemaFactorSubSolucionAsignada { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
        public string SubSolucion { get; set; }
        public int SubSolucionOrden { get; set; }
        public int SubSolucionNivel { get; set; }
    }
}
