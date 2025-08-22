using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface ISedeService
    {
        List<SedeComboDTO> ObtenerComboListaSedes();
        List<SedeFiltroCiudadDTO> ObtenerListaSedesSegunFur();
        List<FiltroDTO> ObtenerListaSedesConComprobanteDetraccion();
        public List<SedeComboDTO> ObtenerComboPorNombreSede(string Sede);
    }
}
