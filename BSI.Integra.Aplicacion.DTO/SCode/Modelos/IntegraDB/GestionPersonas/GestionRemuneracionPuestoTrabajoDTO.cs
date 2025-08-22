using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class GestionRemuneracionPuestoTrabajoDTO
    {
        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string? PuestoTrabajo { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string? PersonalAreaTrabajo { get; set; }
        public int IdPais { get; set; }
        public string? Pais { get; set; }
        public int IdCategoria { get; set; }
        public string? Categoria { get; set; }
        public string? Usuario { get; set; }
        public IEnumerable<PuestoTrabajoRemuneracionDetalleDTO> ListaPuestoTrabajoRemuneracionDetalle { get; set; }
    }
    public class ObtenerDataComboPuestoTrabajoDTO
    {
        public List<ComboDTO> ObtenerArea { get; set; }
        public List<ComboDTO> ObtenerPuestoTrabajo { get; set; }
        public List<ComboDTO> ObtenerPais { get; set; }
        public List<TableroComercialCategoriaAsesorComboDTO> ObtenerCategoria { get; set; }
        public List<ComboDTO> ObtenerRemuneracion { get; set; }
        public List<ComboDTO> ObtenerTipoRemuneracion { get; set; }
        public List<ComboDTO> ObtenerClaseRemuneracion { get; set; }
        public List<ComboDTO> ObtenerPeriodoRemuneracion { get; set; }
        public List<ComboDTO> ObtenerMoneda { get; set; }
        public List<ComboDTO> ObtenerDescripcionMonetaria { get; set; }
    }
}
