using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class PlantillaSendinblueBaseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string HtmlContenido { get; set; }

    }




}
