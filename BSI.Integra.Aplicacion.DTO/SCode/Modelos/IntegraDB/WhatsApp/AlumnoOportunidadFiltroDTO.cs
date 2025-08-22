using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class AlumnoOportunidadFiltroDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }

    }

    public class AlumnoOportunidadEnvioAutomaticoDTO
    {
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime HoraMinima { get; set; }
        public string FaseOportunidad { get; set; }
        public int ConsiderarEnviados { get; set; }
        public int IdPlantilla { get; set; }
    }
}
