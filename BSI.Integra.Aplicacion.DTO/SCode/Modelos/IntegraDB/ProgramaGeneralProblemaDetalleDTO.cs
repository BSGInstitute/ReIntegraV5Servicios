using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaDetalleDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
        public List<ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO> Soluciones { get; set; }
       
    }
    public class ProgramaGeneralProblemaDetalleInsertarDTO
    {
        public int? Id { get; set; }
        public int IdPGeneral { get; set; }
        public int? IdProblema { get; set; }
        public int? IdProblemaDetalle { get; set; }
        public bool DetalleDescripcion { get; set; }
        public bool DetalleTitulo { get; set; }
        public bool DetallePiePagina { get; set; }
        public bool SolucionDescripcion { get; set; }
        public bool SolucionTitulo { get; set; }
        public bool SolucionSubTitulo { get; set; }

        public List<ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO> Soluciones { get; set; }
    }

    public class ProgramaGeneralProblemaSubSolucionesInsertarDTO
    {
        public int? Id { get; set; }
        public int? IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
    }


    public class ProblemaClienteByPGeneral
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public int? IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
        public int? IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
        public int? IdProgramaGeneralProblemaFactorSubSolucionAsignada { get; set; }

    }


    public class ProgramaGeneralProblemaDetalleObtener
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public int? IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public int? IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }
        public int? IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
        public List<ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO> SubSoluciones { get; set; }
    }
    public class ProgramaGeneralProblemaDetalleObtenerAgenda
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
        public List<ProgramaGeneralProblemaFactorSubSolucionAsignadaAgendaDTO> SubSoluciones { get; set; }
    }
}
