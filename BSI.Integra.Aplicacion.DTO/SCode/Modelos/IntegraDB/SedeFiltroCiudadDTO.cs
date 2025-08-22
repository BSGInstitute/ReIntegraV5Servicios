using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SedeFiltroCiudadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class SedeComboDTO
    {
        public int IdEmpresa { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public string Nombre { get; set; }
    }
}
