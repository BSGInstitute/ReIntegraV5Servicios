using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CompuestoPlantillaDTO
    {
        public DatosPlantillaDTO DatosPlantilla { get; set; }
        public List<PlantillaClavesValoresDTO> PlantillaClaveValor { get; set; }
        public List<int> FasesPlantilla { get; set; }
        public string Usuario { get; set; }
        public int IdPlantilla { get; set; }
        public List<PlantillaAsociacionModuloSistemaDTO> ListaPlantillaAsociacionModuloSistema { get; set; }
        public bool Estado { get; set; }

        public DetallePlantillaDTO? DetallePlantilla { get; set; }

    }

    public class PlantillaClavesValoresDTO
    {
        public string Clave { get; set; }
        public string Valor { get; set; }
    }

    public class DetallePlantillaDTO
    {
        public int? id { get; set; }
        public string? Imagen { get; set; }
        public List<Boton>? botones {get;set; }
    }
    public class DetallePlantillasDTO
    {
        public string Imagen { get; set; }
        public string Boton { get; set; }
    }

    public class Boton
    {
        public string Nombre { get; set; }
    }

    public class InsertarDetallePlantillaDTO
    {
        public string Imagen { get; set; }
        public string Boton { get; set; }
        public int IdPlantilla { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
    public class RespuestaValDTO
    {
        public bool Valor { get; set; }

    }

}
