using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class PlantillaSendinblueDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string HtmlContenido { get; set; }
        public string HtmlEditado { get; set; }
        public int IdPlantillaSendinblueBase { get; set; }


    }



    public class PlantillaSendinblueInsertarDTO
    {
        public string Nombre { get; set; }
        public string HtmlContenido { get; set; }
        public string HtmlEditado { get; set; }
        public int IdPlantillaSendinblueBase { get; set; }
        public List<PlantillaSendinblueDatoDTO> DatosEtiqueta { get; set; }      

    }
    public class PlantillaSendinblueActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string HtmlContenido { get; set; }
        public string HtmlEditado { get; set; }
        public int IdPlantillaSendinblueBase { get; set; }
        public List<PlantillaSendinblueDatoActualizarDTO> DatosEtiqueta { get; set; }

    }


}
