using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PlantillaPaisDTO
    {
        public int Id { get; set; }
        public int IdPlantilla { get; set; }
        public int IdPais { get; set; }
    }
    public class PlantillaDocumentoAsociadoDTO
    {
        public int IdDocumentos { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaPW { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
        public int IdPGeneralDocumento { get; set; }
    }
    public class PlantillaDocumentoNoAsociadoDTO
    {
        public int IdDocumentos { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaPW { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
    }
    public class PlantillaDocumentoDTO
    {
        public List<PlantillaDocumentoAsociadoDTO> PlantillaDocumentoAsociado { get; set; }
        public List<PlantillaDocumentoNoAsociadoDTO> PlantillaDocumentoNoAsociado { get; set; }
    }
}
