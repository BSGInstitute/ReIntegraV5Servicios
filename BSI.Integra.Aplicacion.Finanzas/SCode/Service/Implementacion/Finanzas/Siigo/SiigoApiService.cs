using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface.Finanzas.Siigo;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion.Finanzas.Siigo
{
    public class SiigoApiService : ISiigoApiService
    {
        private IUnitOfWork _unitOfWork;
        private string permisosHeader;
        private StringDTO token;
        //private string token;
        public SiigoApiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            token = _unitOfWork.SiigoApiRepository.ObtenerToken();
            //token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjExNDQzRDg2OUYxMzgwODlEREUwOTdENTNBN0YxNzVCNkQwNzIxNzdSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkVVUTlocDhUZ0luZDRKZlZPbjhYVzIwSElYYyJ9.eyJuYmYiOjE3MjYwNzY3ODUsImV4cCI6MTcyODY2ODc4NSwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5OjUwMDAiLCJhdWQiOiJodHRwOi8vbXMtc2VjdXJpdHk6NTAwMC9yZXNvdXJjZXMiLCJjbGllbnRfaWQiOiJTaWlnb0FQSSIsInN1YiI6IjIwMzk5MzUiLCJhdXRoX3RpbWUiOjE3MjYwNzY3ODUsImlkcCI6ImxvY2FsIiwibmFtZSI6ImNtZWppYW1AYnNnaW5zdGl0dXRlLmNvbSIsIm1haWxfc2lpZ28iOiJjbWVqaWFtQGJzZ2luc3RpdHV0ZS5jb20iLCJjbG91ZF90ZW5hbnRfY29tcGFueV9rZXkiOiJCU0dSVVBPQ09MT01CSUFTQVMiLCJ1c2Vyc19pZCI6IjM1OTQzIiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDExNjY2MiIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiMzQiLCJjb21wYW5pZXMiOiIwIiwiYXBpX3N1YnNjcmlwdGlvbl9rZXkiOiI3MzUwM2ZhNzJhNmE0NTZlYWJkYWY1YTYwNDIxNmUyNSIsImFwaV91c2VyX2NyZWF0ZWRfYXQiOiIxNzIzNTg0MjY3IiwiYWNjb3VudGFudCI6ImZhbHNlIiwianRpIjoiNzQ1NTUwNDM4QzMyNTY3QzczMjcyODgzNkEwNjE3MzMiLCJpYXQiOjE3MjYwNzY3ODUsInNjb3BlIjpbIlNpaWdvQVBJIl0sImFtciI6WyJjdXN0b20iXX0.PT2pZPoJZi9iQqDT0ZooQKFuCnZzShjE3j4x1aBPz-4QhTSyHyKl1sshUuZi45Pu1JorM8OIUV7REqEjl-4Glq2T1Y_q2cCIXfwMvTkzgNfp9cVVOfdKZTgpXCXefbYo0JAZ-FKoXO6m1SfdzsJCpa5KNs08FD4l-aBbcZZ2AdfhStiihrpt4SGH4uyHPTR60f2rNeHBC9fC32JrNAQUMAeqUFH8mmYzifeFnmT1hezZsIyTOMJKmsu4IBFUNNyzFDYXRBjGNzi9dTiCGfgiT8b-PrK9FC8VmWbhnScDjQgrwut8ncUms6bbY66wlsgKLHt6tMujXmOgcPtqjSMCSQ";
        }

        public async Task<string> ObtenerTokenSiigo(string usuarioModificacion)
        {
            var urlBase = "https://api.siigo.com/auth/token";
            var body = new
            {
                username = "cmejiam@bsginstitute.com",
                access_key = "OTI1MDMzYzQtZmY0OS00MTVhLWFiMDAtNzk3NzYxZWU3ZDFlOjRNLzkzc1h0KFQ="
            };
            var jsonContent = JsonConvert.SerializeObject(body);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(urlBase, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var (accessToken, expiresIn) = ExtraerDatosToken(jsonString);
                        var fechaExpiracion = CalcularFechaExpiracion(expiresIn);
                        await ActualizaTokenSiigo(accessToken, fechaExpiracion, usuarioModificacion);
                        token = _unitOfWork.SiigoApiRepository.ObtenerToken();
                        return accessToken;
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Exception: {ex.Message}";
                }
            }
        }

        private (string accessToken, int expiresIn) ExtraerDatosToken(string jsonString)
        {
            dynamic jsonObj = JsonConvert.DeserializeObject(jsonString);
            string accessToken = jsonObj.access_token;
            int expiresIn = jsonObj.expires_in;
            return (accessToken, expiresIn);
        }

        private DateTime CalcularFechaExpiracion(int expiresInSegundos)
        {
            // Convertir expires_in de segundos a días
            int expiresInDias = expiresInSegundos / (60 * 60 * 24);
            // Sumar el valor de expires_in a la fecha actual
            return DateTime.Now.AddDays(expiresInDias);
        }

        public async Task<bool> ActualizaTokenSiigo(string token, DateTime fechaExpiracion, string usuarioModificacion)
        {
            try
            {
                return await _unitOfWork.SiigoApiRepository.ActualizaTokenSiigo(token, fechaExpiracion, usuarioModificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string resultado, HttpStatusCode statusCode)> DatosCompletos(DatosCompletosDTO datos, string usuarioModificacion)
        {
            try
            {
                var (resultado, statusCode) = await CrearFacturaDeVentaSiigo(datos.Factura);
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
                {
                    return (resultado, statusCode);
                }
                else
                {
                    if (resultado.StartsWith("Error:"))
                    {
                        var errorParts = resultado.Split(new[] { " - " }, 2, StringSplitOptions.None);
                        var errorCode = errorParts.Length > 0 ? errorParts[0].Substring(6) : string.Empty;
                        var errorMessage = errorParts.Length > 1 ? errorParts[1] : string.Empty;
                        if (errorCode == " unauthorized" && errorMessage == "Verify the authorization header: the token may be invalid or expired, or the user may have been blocked.") //Entra si error fue por token inválido
                        {
                            var nuevoToken = await ObtenerTokenSiigo(usuarioModificacion);
                            var (nuevoResultado, nuevoStatusCode) = await CrearFacturaDeVentaSiigo(datos.Factura);
                            if (nuevoStatusCode == HttpStatusCode.OK || nuevoStatusCode == HttpStatusCode.Created)
                            {
                                return (nuevoResultado, nuevoStatusCode);
                            }
                            else
                            {
                                if (nuevoResultado.StartsWith("Error:"))
                                {
                                    errorParts = nuevoResultado.Split(new[] { " - " }, 2, StringSplitOptions.None);
                                    errorCode = errorParts.Length > 0 ? errorParts[0].Substring(6) : string.Empty;
                                    errorMessage = errorParts.Length > 1 ? errorParts[1] : string.Empty;
                                    if (errorCode == " invalid_reference" && errorMessage.Contains("The customer doesn't exist:"))
                                    {
                                        var (clienteResultado, clienteStatusCode) = await CrearClienteSiigo(datos.Cliente);
                                        if (clienteStatusCode == HttpStatusCode.OK || clienteStatusCode == HttpStatusCode.Created)
                                        {
                                            var (nuevoResultado2, nuevoStatusCode2) = await CrearFacturaDeVentaSiigo(datos.Factura);
                                            return (nuevoResultado2, nuevoStatusCode2);
                                        }
                                        else
                                        {
                                            return (clienteResultado, clienteStatusCode);
                                        }
                                    }
                                }
                                return (nuevoResultado, nuevoStatusCode);
                            }
                        }
                        else if (errorCode == " invalid_reference" && errorMessage.Contains("The customer doesn't exist:")) // Entra si el error es porque no existe el cliente/tercero
                        {
                            var (clienteResultado, clienteStatusCode) = await CrearClienteSiigo(datos.Cliente);
                            if (clienteStatusCode == HttpStatusCode.OK || clienteStatusCode == HttpStatusCode.Created) //Si tercero fue creado, crear factura
                            {
                                var (nuevoResultado, nuevoStatusCode) = await CrearFacturaDeVentaSiigo(datos.Factura);
                                return (nuevoResultado, nuevoStatusCode);
                            }
                            else
                            {
                                return (clienteResultado, clienteStatusCode);
                            }
                        }
                    }
                    return (resultado, statusCode);
                }
            }
            catch (Exception ex)
            {
                return ($"Error inesperado: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<(string resultado, HttpStatusCode statusCode)> CrearFacturaDeVentaSiigo(CrearFacturaDeVentaSiigoDTO factura)
        {
            try
            {
                var urlBase = "https://api.siigo.com/v1/invoices";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Valor);
                    client.DefaultRequestHeaders.Add("Partner-Id", "BSGrupoColombiaSAS");
                    var nuevoFactura = new //Cuerpo de solicitud JSON para crear factura
                    {
                        document = new { id = factura.Documento.Id },
                        date = factura.Fecha,
                        customer = new { identification = factura.Cliente.NumeroIdentification },
                        seller = factura.Vendedor,
                        observations = factura.Observaciones,
                        items = factura.Items.Select(item => new
                        {
                            code = item.Codigo,
                            description = item.Descripcion,
                            quantity = item.Cantidad,
                            price = item.Precio
                        }).ToArray(),
                        payments = factura.Pagos.Select(payment => new
                        {
                            id = payment.Id,
                            value = payment.Valor,
                            due_date = payment.FechaVencimiento
                        }).ToArray()
                    };
                    var jsonContent = JsonConvert.SerializeObject(nuevoFactura);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(urlBase, content);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return (jsonString, response.StatusCode);
                    }
                    else
                    {
                        try
                        {
                            JObject errorResponse = JObject.Parse(jsonString);
                            var errorCode = errorResponse["Errors"]?[0]?["Code"]?.ToString();
                            var errorMessage = errorResponse["Errors"]?[0]?["Message"]?.ToString();
                            if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                            {
                                return ($"Error: {errorCode} - {errorMessage}", response.StatusCode);
                            }
                            else
                            {
                                return ($"Error desconocido: {response.ReasonPhrase}", response.StatusCode);
                            }
                        }
                        catch (JsonException)
                        {
                            return ($"Error al procesar la respuesta del servidor: {response.ReasonPhrase}", response.StatusCode);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return ($"Error en la solicitud HTTP: {ex.Message}", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return ($"Error inesperado: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<(string resultado, HttpStatusCode statusCode)> CrearClienteSiigo(CrearClienteSiigoDTO cliente)
        {
            try
            {
                var urlBase = "https://api.siigo.com/v1/customers";
                var nuevoUsuario = new //Cuerpo de solicitud JSON para crear cliente
                {
                    type = cliente.TipoCliente,
                    person_type = cliente.TipoPersona,
                    id_type = cliente.TipoIdentificacion,
                    identification = cliente.Identificacion,
                    name = cliente.Nombres,
                    fiscal_responsibilities = cliente.CodigosFiscal.Select(codigo => new { code = codigo }).ToArray(),
                    address = new
                    {
                        address = cliente.Direccion,
                        city = new
                        {
                            country_code = cliente.CodigoPais,
                            state_code = cliente.CodigoDepartamento,
                            city_code = cliente.CodigoCiudad
                        }
                    },
                    phones = new[]
                    {
                        new
                        {
                            indicative = cliente.TelefonoIndicativo,
                            number = cliente.TelefonoNumero,
                            extension = cliente.TelefonoExtension
                        }
                    },
                    contacts = new[]
                    {
                        new
                        {
                            first_name = cliente.ContactoNombre,
                            last_name = cliente.ContactoApellido,
                            email = cliente.ContactoEmail
                        }
                    }
                };
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Valor);
                    client.DefaultRequestHeaders.Add("Partner-Id", "BSGrupoColombiaSAS");
                    var jsonContent = JsonConvert.SerializeObject(nuevoUsuario);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(urlBase, content);
                    var jsonString = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return (jsonString, response.StatusCode);
                    }
                    else
                    {
                        try
                        {
                            JObject errorResponse = JObject.Parse(jsonString);
                            var errorCode = errorResponse["Errors"]?[0]?["Code"]?.ToString();
                            var errorMessage = errorResponse["Errors"]?[0]?["Message"]?.ToString();

                            if (!string.IsNullOrEmpty(errorCode) && !string.IsNullOrEmpty(errorMessage))
                            {
                                return ($"Error: {errorCode} - {errorMessage}", response.StatusCode);
                            }
                            else
                            {
                                return ($"Error desconocido: {response.ReasonPhrase}", response.StatusCode);
                            }
                        }
                        catch (JsonException)
                        {
                            return ($"Error al procesar la respuesta del servidor: {response.ReasonPhrase}", response.StatusCode);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                return ($"Error en la solicitud HTTP: {ex.Message}", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                return ($"Error inesperado: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }


      


        public async Task EnviarSiigoMasivasDesdeBaseDeDatos(EnvioMasivoSiigoLoteDTO datos)
        {
            var lotes = datos.IdsFacturas
                .Select((id, index) => new { id, index })
                .GroupBy(x => x.index / 4)
                .Select(g => g.Select(x => x.id).ToList())
                .ToList();

            foreach (var lote in lotes)
            {
                foreach (var idFactura in lote)
                {
                    var datosFactura = _unitOfWork.SiigoApiRepository.ObtenerDatosFacturaClientePorId(idFactura);

                    var (resultado, statusCode) = await DatosCompletos(datosFactura, datos.Usuario);



                    if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
                    {


                        _unitOfWork.SiigoApiRepository.ActualizarFacturaComoEnviada(idFactura, datos.Usuario);

                        var id = _unitOfWork.SiigoApiRepository.ObtenerIdCronogramaPorIdFactura(idFactura);
                        var actulizarCronogramafinal = _unitOfWork.CronogramaPagoDetalleFinalRepository.ActualizaEnviadoSiigo(id);



                    }
                    else
                    {
                        throw new Exception($"Error al enviar factura {idFactura}: {resultado}");
                    }
                }

                await Task.Delay(1000); // opcional
            }
        }


        public int ObtenerIdFacturaPorCodigoMatricula(int IdCronogramaPagoDetalleFinal)
        {
            return _unitOfWork.SiigoApiRepository.ObtenerIdFacturaPorCodigoMatricula(IdCronogramaPagoDetalleFinal);
        }
        public async Task EnviarFacturaSiigoDesdeBaseDeDatos(int idFactura, string usuario)
        {
            var datos = _unitOfWork.SiigoApiRepository.ObtenerDatosFacturaClientePorId(idFactura);

            (string resultado, HttpStatusCode statusCode) = await DatosCompletos(datos, usuario);

            if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
            {
                // Registrar como enviada
                  _unitOfWork.SiigoApiRepository.ActualizarFacturaComoEnviada(idFactura, usuario);
                return;
            }
            else
            {
                throw new Exception("Error al enviar a la API: " + resultado);
            }
        }

       
        public async Task GuardarDatosAntesDeEnviarASiigo(DatosCompletosDTO datos, string codigoMatricula, int idCronogramaPagoDetalleFinal, string usuario)
        {
            try
            {
                await _unitOfWork.SiigoApiRepository.GuardarFacturaSiigoInterna(datos, codigoMatricula, idCronogramaPagoDetalleFinal, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la factura internamente: " + ex.Message);
            }
        }

        public List<SiigoFacturaMasivoDTO> ObtenerFacturasPendientesEnvioSiigo()

        {
            return _unitOfWork.SiigoApiRepository.ObtenerFacturasPendientesEnvioSiigo();
        }
    }
}
