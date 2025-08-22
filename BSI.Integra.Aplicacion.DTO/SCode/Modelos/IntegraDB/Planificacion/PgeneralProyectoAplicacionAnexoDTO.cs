using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralProyectoAplicacionAnexoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string RutaArchivo { get; set; } = null!;
        public bool EsEnlace { get; set; }
        public bool SoloLectura { get; set; }
    }
}
