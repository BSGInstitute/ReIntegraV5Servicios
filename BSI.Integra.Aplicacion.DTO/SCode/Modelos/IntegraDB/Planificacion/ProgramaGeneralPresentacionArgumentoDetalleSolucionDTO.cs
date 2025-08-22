using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralPresentacionArgumento { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
        public int? IdPgeneral { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ProgramaGeneralPresentacionArgumentoDetalleSolucionComboDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralPresentacionArgumento { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
    }
    public class ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralPresentacionArgumento { get; set; }
        public string Detalle { get; set; } = null!;
        public string Solucion { get; set; } = null!;
   
    }
    public class ProgramaGeneralPresentacionArgumentoArgumentoDetalleSolucionDTO
    {
        public int? Id { get; set; }
        public string? Detalle { get; set; }
        public string? Solucion { get; set; }
    }
    public class PresentacionArgumentoDetalleSolucionDTO
    {
        public int? IdProgramaGeneralPresentacionArgumento { get; set; }
        public string? Detalle { get; set; }
        public string? Solucion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class PresentacionArgumentoDetalleSolucionAlternoDTO
    {
        public int? Id { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
    }
  
}
