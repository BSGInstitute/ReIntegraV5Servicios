using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix
{

    public class WavixPersonalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string IdSipTrunk { get; set; }
        public string UrlServer { get; set; }

    }
    public class NumeroAsesorWavixDTO
    {
        public string IdPersonal { get; set; }
        public string NombreAsesor { get; set; }
        public string IdSipTrunk { get; set; }
        public string UrlServer { get; set; }
        public int IdPais { get; set; }
        public string Numero { get; set; }
        public bool Predeterminado { get; set; }
    }


    public class EstadoLlamadaDTO
    {
        public string uuid { get; set; }
        public int idOportunidad { get; set; }
        public int idActividadDetalle { get; set; }
        public string disposition { get; set; }
        public string answered_by { get; set; }

    }
}
