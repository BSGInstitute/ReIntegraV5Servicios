using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// DTO para Insertar/Actualizar PaqueteTutorVirtual con estructura completa
    /// </summary>
    public class PaqueteTutorVirtualGuardarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int CantidadCredito { get; set; }
        public List<PaqueteTutorVirtualPaisGuardarDTO> Paises { get; set; } = new List<PaqueteTutorVirtualPaisGuardarDTO>();
    }

    public class PaqueteTutorVirtualPaisGuardarDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public int IdMoneda { get; set; }
        public decimal CostoIndividual { get; set; }
        public decimal CostoPaquete { get; set; }
        public List<PaqueteTutorVirtualBeneficioGuardarDTO> Beneficios { get; set; } = new List<PaqueteTutorVirtualBeneficioGuardarDTO>();
    }

    public class PaqueteTutorVirtualBeneficioGuardarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
