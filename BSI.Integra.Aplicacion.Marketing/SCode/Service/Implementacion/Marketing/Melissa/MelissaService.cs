using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Melissa;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Melissa.MelissaDTO;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Melissa
{
    public class MelissaService : IMelissaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private readonly HttpClient _httpClient;

        public MelissaService(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
        }

        public async Task<MelissaVerificacionDTO> ValidarNumero(string numero, int? idCodigoPais)
        {
            string codigoPais = "";
            if (idCodigoPais.HasValue)
            {
                var pais = _unitOfWork.MelissaRepository.ObtenerIsoPais(idCodigoPais);
                if (pais != null && pais.Any())
                {
                    codigoPais = pais[0].CodigoISO.ToString();
                }
            }

            string baseUrl = "https://globalphone.melissadata.net/v4/WEB/GlobalPhone/doGlobalPhone";
            string licenseKey = "St7Qk7Xcz0wEvV3JlM7JJ8**nSAcwXpxhQ0PC2lXxuDAZ-**";//temporalPrueba
            //string licenseKey = "Q7tM8zEtpLao2FYiHdMwnt**";//Oficial
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
                    _unitOfWork.MelissaRepository.InsertarMelissaLog(numero, idCodigoPais, respuesta);
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
    }
}
