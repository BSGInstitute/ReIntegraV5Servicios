using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Repositorio.Repository.Interface.Finanzas.Siigo;
using Dapper;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace BSI.Integra.Repositorio.Repository.Implementation.Finanzas.Siigo
{
    public class SiigoApiRepository : ISiigoApiRepository
    {
        private IDapperRepository _dapperRepository;
        public SiigoApiRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public StringDTO ObtenerToken()
        {
            try
            {
                StringDTO respuesta = new StringDTO();
                var query = @"SELECT Token As Valor FROM fin.T_SiigoToken WHERE Estado = 1 ";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ActualizaTokenSiigo(string token, DateTime fechaExpiracion, string usuarioModificacion)
        {
            try
            {
                var fechaModificacion = DateTime.Now;
                await _dapperRepository.QuerySPFirstOrDefaultAsync("fin.SP_ActualizarTokenSiigo", new { token, fechaExpiracion, fechaModificacion, usuarioModificacion });
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public async Task<string> GuardarFacturaSiigoInterna(DatosCompletosDTO datos, string codigoMatricula, int idCronogramaPagoDetalleFinal, string usuario)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@Json", JsonConvert.SerializeObject(datos));
            parametros.Add("@IdCronogramaPagoDetalleFinal ", idCronogramaPagoDetalleFinal);
            parametros.Add("@Usuario", usuario);

            var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync(
                "[fin].[SP_SiigoFactura_InsertarJson]",
                parametros
            );

            if (resultado != null && resultado.ToString().Contains("OK"))
                return "Factura insertada correctamente.";
            else
                throw new Exception("No se pudo insertar la factura internamente. Respuesta: " + resultado);
        }
        public void ActualizarFacturaComoEnviada(int idFactura, string usuario)
        {
            var parametros = new
            {
                Id = idFactura,

                UsuarioModificacion = usuario
            };

            _dapperRepository.QuerySPDapper("[fin].[SP_SiigoFactura_ActualizarEnvio]", parametros);
        }

        public DatosCompletosDTO ObtenerDatosFacturaClientePorId(int idFactura)
        {
            var resultado = _dapperRepository.QuerySPDapper(
                "[fin].[SP_ObtenerSiigoFacturaCliente]",
                new { IdSiigoFactura = idFactura });

            if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                throw new Exception("No se encontró información para la factura solicitada.");

            var contenedor = JsonConvert.DeserializeObject<JArray>(resultado)?[0];
            if (contenedor == null)
                throw new Exception("Formato JSON inválido en SP_SiigoFacturaCliente_ObtenerPorId.");

            var facturaJson = contenedor["factura"]?.ToString();
            var clienteJson = contenedor["cliente"]?.ToString();

            var dto = new DatosCompletosDTO
            {
                Factura = JsonConvert.DeserializeObject<CrearFacturaDeVentaSiigoDTO>(facturaJson),
                Cliente = JsonConvert.DeserializeObject<CrearClienteSiigoDTO>(clienteJson)
            };

            return dto;
        }
        public int ObtenerIdFacturaPorCodigoMatricula(int IdCronogramaPagoDetalleFinal)
        {
            var result = _dapperRepository.QuerySPFirstOrDefault("[fin].[SP_SiigoFactura_ObtenerIdPorCronograma]", new { IdCronogramaPagoDetalleFinal });

            var match = Regex.Match(result?.ToString() ?? "", "\"Id\"\\s*:\\s*(\\d+)");
            if (match.Success)
                return int.Parse(match.Groups[1].Value);

            return 0;
        }

        public List<SiigoFacturaMasivoDTO> ObtenerFacturasPendientesEnvioSiigo()
        {


            var resultado = _dapperRepository.QueryDapper(
                "SELECT * FROM [fin].[V_SiigoFacturaPendienteEnvio]", null);
       
            if (string.IsNullOrWhiteSpace(resultado))
                return new List<SiigoFacturaMasivoDTO>();

            var array = JArray.Parse(resultado);

            var lista = array.Select(item => new SiigoFacturaMasivoDTO
            {
                IdFactura = (int)item["IdSiigoFactura"],
                IdCliente = (int)item["IdSiigoCliente"],
                CodigoMatricula = (string)item["CodigoMatricula"],
                EstadoEnvio = item["EstadoEnvio"]?.Value<bool?>() ?? false,
                Identificador = (string)item["Identificador"] ?? "",
                Pais = (string?)item["Pais"],
                Monto = (decimal?)item["Monto"] ?? 0,
                Nombre = (string)item["Nombre"] ?? "",

                ApiDestino = (string?)item["ApiDestino"]
            }).ToList();

            return lista;
        }
        public int ObtenerIdCronogramaPorIdFactura(int idFactura)
        {
            try
            {
                var sql = @"
            SELECT IdCronogramaPagoDetalleFinal 
            FROM fin.T_SiigoFactura 
            WHERE Id = @idFactura";

                var resultado = _dapperRepository.QueryDapper(sql, new { idFactura });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var lista = JsonConvert.DeserializeObject<List<CornogramaFacturmaDTO>>(resultado);
                    return lista.FirstOrDefault()?.IdCronogramaPagoDetalleFinal ?? 0;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}

