using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RemitenteMailingDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class RemitenteMailingAsesorDTO
    {
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
    }
    public class RemitenteMailingCreacionDTO
    {
        public FormularioRemitenteMailingDTO formulario { get; set; }
        public List<AsesorRemitenteMailingDTO> asesores { get; set; }
    }
    public class FormularioRemitenteMailingDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class AsesorRemitenteMailingDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
    }
}
