using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class FiltroControlDocumentoDTO
    {
        public int IdAsesor { get; set; }
        public int IdCoordinador { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        //public string CodigoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int Estado { get; set; }


    }
}
