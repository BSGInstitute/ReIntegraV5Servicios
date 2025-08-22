using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralCodigoPartnerDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Codigo { get; set; } = null!;
        public List<PgeneralCodigoPartnerModalidadCursoDTO> PgeneralCodigoPartnerModalidadCurso { get; set; }
        public List<PgeneralCodigoPartnerVersionProgramaDTO> PgeneralCodigoPartnerVersionPrograma { get; set; }
    }
    public class PgeneralCodigoPartnerAlternoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public List<int> ModalidadesCurso { get; set; }
        public List<int> VersionesPrograma { get; set; }
        public int? Pdu { get; set; }
    }
    public class PEspecificoCodigoPartnerAlternoDTO
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public int IdPEspecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public int? Pdu { get; set; }
    }
}
