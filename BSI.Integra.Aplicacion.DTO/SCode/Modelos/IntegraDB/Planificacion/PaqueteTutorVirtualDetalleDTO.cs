using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PaqueteTutorVirtualDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CantidadCreditos { get; set; }
        public List<PaquetePaisDetalleDTO> Paises { get; set; } = new List<PaquetePaisDetalleDTO>();
    }

    public class PaquetePaisDetalleDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public int IdMoneda { get; set; }
        public decimal CostoIndividual { get; set; }
        public decimal CostoPrograma { get; set; }
        public List<PaqueteTutorVirtualBeneficioDetalleDTO> Beneficios { get; set; } = new List<PaqueteTutorVirtualBeneficioDetalleDTO>();
    }

    public class PaqueteTutorVirtualBeneficioDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
