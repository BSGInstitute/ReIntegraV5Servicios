using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class DocumentoPwVersionesDTO
    {
        public int? Id { get; set; }
        public int IdDocumentoPw { get; set; }
        public string Introduccion { get; set; }
        public int IdVersionPrograma { get; set; }
    }
    public class DocumentoPwVersionesPGeneralDTO
    {
        public int? Id { get; set; }
        public int IdDocumentoPw { get; set; }
        public string Introduccion { get; set; }
        public int IdVersionPrograma { get; set; }
        public int IdPGeneral { get; set; }
    }
}
