using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Configuracion
{
    public interface IAsentamientoMunicipioRepository
    {
        List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipio(int idCiudadRef, int IdMunicipioMexico);
        List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipioyCiudadMexico(int idCiudadRef, int IdMunicipioMexico, int? idCiudadMexico);
        List<DatosCodigoPostalDTO> BusquedaPorCodigoPostal(string codigoPostal);
    }
}
