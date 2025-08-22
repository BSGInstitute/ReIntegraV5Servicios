using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Repositorio.Repository.Interface.Configuracion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Configuracion
{
    public class MunicipioRepository : IMunicipioRepository
    {
        private IDapperRepository _dapperRepository;
        public MunicipioRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<ComboDTO> ObtenerMunicipioPorCiudad(int idCiudadRef)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT IdMunicipioMexico AS Id, MunicipioMexico AS Nombre FROM conf.V_CodigoPostalMexico
                                WHERE MunicipioMexicoEstado = 1
                                AND IdCiudad = @idCiudadRef
                                AND IdCiudadMexico IS NULL
                                GROUP BY IdMunicipioMexico, MunicipioMexico";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idCiudadRef });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ComboDTO> ObtenerMunicipioPorEstadoyCiudad(int idCiudadRef, int? idCiudadMexico)
        {
            try
            {
                List<ComboDTO> items = new List<ComboDTO>();
                var _query = @"SELECT IdMunicipioMexico AS Id, MunicipioMexico AS Nombre FROM conf.V_CodigoPostalMexico
                                WHERE MunicipioMexicoEstado = 1
                                AND IdCiudad = @idCiudadRef
                                AND IdCiudadMexico =@idCiudadMexico
                                GROUP BY IdMunicipioMexico, MunicipioMexico";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { idCiudadRef, idCiudadMexico });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaDapper);
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
