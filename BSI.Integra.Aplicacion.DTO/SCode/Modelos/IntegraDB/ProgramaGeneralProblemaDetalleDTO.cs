using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaDetalleDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdProgramaGeneralProblemaFactor { get; set; }
        public int IdProgramaGeneralProblemaFactorDetalle { get; set; }
        public bool AplicaTituloDetalle { get; set; }
        public bool AplicaNombreDetalle { get; set; }
        public bool AplicaPieDePagina { get; set; }
        public bool AplicaDescripcionSolucion { get; set; }
        public bool AplicaTituloSolucion { get; set; }
        public bool AplicaSubTituloSolucion { get; set; }

    }
    public class ProgramaGeneralProblemaDetalleInsertarDTO
    {
        public int? Id { get; set; }
        public int IdPGeneral { get; set; }
        public int? IdProblema { get; set; }
        public int? IdProblemaDetalle { get; set; }
        public bool? DetalleDescripcion { get; set; }
        public bool? DetalleTitulo { get; set; }
        public bool? SolucionDescripcion { get; set; }
        public bool? SolucionTitulo { get; set; }
        public bool? SolucionSubTitulo { get; set; }

        public ProgramaGeneralProblemaSubSolucionesInsertarDTO Soluciones { get; set; }
    }

    public class ProgramaGeneralProblemaSubSolucionesInsertarDTO
    {
        public int Id { get; set; }
        public int? IdProgramaGeneralProblemaDetalle { get; set; }
        public int? IdProgramaGeneralProblemaFactorSolucion { get; set; }
    }
}
