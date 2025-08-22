using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class PlantillaSendinblueImagenDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string Extension { get; set; }


    }

   
    public class PlantillaSendinblueImagenSubirDTO
    {
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string Extension { get; set; }
        public bool Estado { get; set; }
        public string? Usuario { get; set; }
        public DateTime Fecha { get; set; }


    }



}
