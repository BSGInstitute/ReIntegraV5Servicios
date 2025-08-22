using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Melissa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Melissa.MelissaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.Melissa
{
    public class MelissaRepository : IMelissaRepository
    {
        private IDapperRepository _dapperRepository;
        private readonly HttpClient _httpClient;
  

      

        public MelissaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
            _httpClient = new HttpClient();

        }



        public List<CodigoPaisIsoDTO> ObtenerIsoPais(int? idCodigoPais)
        {
            try
            {
                var rpta = new List<CodigoPaisIsoDTO>();
                var query = @"
                               SELECT CodigoPais,
                                      CodigoISO 
                               FROM  mkt.V_ObtenerCodigIsoPais WHERE CodigoPais=@idCodigoPais ";
                var resultado = _dapperRepository.QueryDapper(query, new { idCodigoPais });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject <List<CodigoPaisIsoDTO>>(resultado)!;

                    return rpta;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error al ejecutar el método ObtenerIsoPais() {ex.Message}", ex);
            }
        }
        public async Task<MelissaVerificacionDTO> ValidarNumero(string numero, int? idCodigoPais)
        {
            string codigoPais = "";

            if (idCodigoPais.HasValue)
            {
                var pais = ObtenerIsoPais(idCodigoPais.Value);
                if (pais != null && pais.Any())
                {
                    codigoPais = pais[0].CodigoISO;
                }
            }

            string baseUrl = "https://globalphone.melissadata.net/v4/WEB/GlobalPhone/doGlobalPhone";
            //string licenseKey = "Q7tM8zEtpLao2FYiHdMwnt**";
            string licenseKey = "St7Qk7Xcz0wEvV3JlM7JJ8**nSAcwXpxhQ0PC2lXxuDAZ-**";
            string requestUrl = $"{baseUrl}?id={licenseKey}&opt=VerifyPhone:Premium&phone={numero}&format=json";

            if (!string.IsNullOrEmpty(codigoPais))
            {
                requestUrl += $"&ctry={codigoPais}";
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<MelissaVerificacionDTO>(responseData);
                    var respuesta = JsonConvert.SerializeObject(result, Formatting.Indented);
                    InsertarMelissaLog(numero, idCodigoPais, respuesta);
                    return result;
                }
                else
                {
                    throw new Exception($"Error al realizar la verificación: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error interno: {ex.Message}");
            }
        }

        public bool InsertarMelissaLog(string numero, int? idCodigoPais, string resultado)
        {
            try
            {
                var query = @"[mkt].[SP_MelissaPhoneVerification_Insertar]";
                var parametros = new
                {
                    IdCodigoPais = idCodigoPais, 
                    Numero = numero,
                    Resultado = resultado
                };
                var resultadoDb = _dapperRepository.QuerySPDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultadoDb) && resultadoDb != "null")
                {
                    return true; 
                }

                return false; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error al ejecutar el método InsertarMelissaLog(): {ex.Message}", ex);
            }
        }
    }
}
