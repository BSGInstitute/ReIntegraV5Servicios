using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Configuracion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Configuracion
{
    public class AsentamientoMunicipioRepository : IAsentamientoMunicipioRepository
    {
        private IDapperRepository _dapperRepository;
        public AsentamientoMunicipioRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipio(int idCiudadRef, int IdMunicipioMexico)
        {
            try
            {
                List<AsentamientoMunicipioDTO> items = new List<AsentamientoMunicipioDTO>();
                var _query = @"SELECT IdAsentamientoMexico, CodigoPostal, AsentamientoMexico FROM conf.V_CodigoPostalMexico WHERE EstadoAsentamientoMexico = 1 AND IdCiudad = @idCiudadRef AND IdMunicipioMexico = @IdMunicipioMexico AND IdCiudadMexico IS NULL ";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idCiudadRef, IdMunicipioMexico });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<AsentamientoMunicipioDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<AsentamientoMunicipioDTO> ObtenerAsentamientoPorMunicipioyCiudadMexico(int idCiudadRef, int IdMunicipioMexico, int? idCiudadMexico)
        {
            try
            {
                List<AsentamientoMunicipioDTO> items = new List<AsentamientoMunicipioDTO>();
                
                    var _query = @"SELECT IdAsentamientoMexico, CodigoPostal, AsentamientoMexico FROM conf.V_CodigoPostalMexico WHERE EstadoAsentamientoMexico = 1 AND IdCiudad = @idCiudadRef AND IdMunicipioMexico = @IdMunicipioMexico and IdCiudadMexico =@idCiudadMexico";
                    var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idCiudadRef, IdMunicipioMexico, idCiudadMexico });
                    if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    {
                        items = JsonConvert.DeserializeObject<List<AsentamientoMunicipioDTO>>(respuestaDapper);
                    }
                    return items;
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<DatosCodigoPostalDTO> BusquedaPorCodigoPostal(string codigoPostal)
        {
            try
            {
                List<DatosCodigoPostalDTO> items = new List<DatosCodigoPostalDTO>();
                var _query = @"SELECT CodigoPostal,IdAsentamientoMexico,AsentamientoMexico,IdMunicipioMexico,MunicipioMexico,IdCiudad,NombreCiudad,IdCiudadMexico,CiudadMexico  FROM conf.V_CodigoPostalMexico WHERE EstadoAsentamientoMexico = 1 AND CodigoPostal =@codigoPostal";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { codigoPostal });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<DatosCodigoPostalDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
