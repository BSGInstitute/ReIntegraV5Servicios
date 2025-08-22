using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion
{
    public class AsentamientoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? CodigoPostal { get; set; }

        public int? IdMunicipioMexico { get; set; }
        public int? IdEstadoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public int? IdTipoAsentamiento { get; set; }
        public int? IdZonaAsentamiento { get; set; }
        public string? Identificador { get; set; }
        public string? ClaveCiudad { get; set; }
    }

    public class AsentamientoMunicipioDTO
    {
        public int IdAsentamientoMexico { get; set; }
        public string CodigoPostal { get; set; }
        public string AsentamientoMexico { get; set; }
    }


    public class DatosCodigoPostalDTO
    {
        public string CodigoPostal { get; set; }
        public int IdAsentamientoMexico { get; set; }
        public string AsentamientoMexico { get; set; }
        public int IdMunicipioMexico { get; set; }
        public string MunicipioMexico { get; set; }
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? CiudadMexico { get; set; }


    }
    public class CodigoPostalAsentammiento
    {
        public int Id { get; set; }
        public string CodigoPostal { get; set; }

    }
}
