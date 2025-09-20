using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    public class FacturamaService : IFacturamaService
    {
        private IUnitOfWork _unitOfWork;
        private string permisosHeader;
        private StringDTO token;
        //private string token;
        public FacturamaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;


        }


        public async Task<(string resultado, HttpStatusCode statusCode)> CrearFacturaAsync(FacturamaFacturaDTO factura)
        {
            var credenciales = _unitOfWork.FacturamaRepository.ObtenerCredencialesActivas();
            var urlBase = credenciales.Sandbox ? "https://apisandbox.facturama.mx/api/3/cfdis" : "https://api.facturama.mx/api/3/cfdis";

            // Validar si GlobalInformation está vacío (factura normal)
            bool esGlobalInfoVacia =
                factura.GlobalInformation != null &&
                string.IsNullOrWhiteSpace(factura.GlobalInformation.Periodicity) &&
                string.IsNullOrWhiteSpace(factura.GlobalInformation.Months) &&
                string.IsNullOrWhiteSpace(factura.GlobalInformation.Year);

            if (esGlobalInfoVacia)
            {
                factura.GlobalInformation = null;
            }

            var facturaJson = JsonConvert.SerializeObject(factura);
            var content = new StringContent(facturaJson, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credenciales.UserName}:{credenciales.Password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var response = await client.PostAsync(urlBase, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return (responseContent, response.StatusCode);
                }
                else
                {
                    if (!string.IsNullOrEmpty(responseContent) && responseContent.Trim().StartsWith("{") && responseContent.Trim().EndsWith("}"))
                    {
                        try
                        {
                            JObject errorResponse = JObject.Parse(responseContent);

                            // Revisar si hay errores en "ModelState"
                            if (errorResponse["ModelState"] != null)
                            {
                                var modelStateErrors = errorResponse["ModelState"].ToObject<JObject>();

                                if (modelStateErrors != null)
                                {
                                    // Recorrer los campos dentro de ModelState
                                    foreach (var property in modelStateErrors)
                                    {
                                        var errorMessages = property.Value.ToObject<JArray>();

                                        // Si se encuentra un mensaje de error en este campo
                                        if (errorMessages != null && errorMessages.Count > 0)
                                        {
                                            // Retornar el primer error encontrado con el nombre del campo
                                            string mensajeError = $"Error en el campo '{property.Key}': {errorMessages[0]}";
                                            return (mensajeError, response.StatusCode);
                                        }
                                    }
                                }
                            }

                            // Si no hay errores en ModelState, buscar el campo "Message" o "message"
                            string errorMessage = errorResponse["Message"]?.ToString() ?? errorResponse["message"]?.ToString();

                            // Si no se encuentra un mensaje, usar uno predeterminado
                            if (string.IsNullOrEmpty(errorMessage))
                            {
                                errorMessage = "Este RFC del receptor no existe en la lista de RFC inscritos no cancelados del SAT";
                            }

                            // Retornar el mensaje de error obtenido
                            string mensajeErrorFinal = $"Error: {errorMessage}";
                            return (mensajeErrorFinal, response.StatusCode);
                        }
                        catch (JsonException ex)
                        {
                            return ($"Error al procesar el contenido JSON recibido: {ex.Message}", response.StatusCode);
                        }
                    }
                    else
                    {
                        return ($"Error: Respuesta inesperada del servidor - {responseContent}", response.StatusCode);
                    }
                }
            }
        }


        public async Task<(string resultado, HttpStatusCode statusCode)> CrearClienteAsync(FacturamaClienteDTO cliente)
        {
            var credenciales = _unitOfWork.FacturamaRepository.ObtenerCredencialesActivas();
            if (credenciales == null)
            {
                return ("Error: No se encontraron credenciales activas.", HttpStatusCode.BadRequest);
            }

            var urlBase = credenciales.Sandbox ? "https://apisandbox.facturama.mx/Client" : "https://api.facturama.mx/Client";
            var clienteJson = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(clienteJson, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credenciales.UserName}:{credenciales.Password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var response = await client.PostAsync(urlBase, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return (responseContent, response.StatusCode);
                }
                else
                {
                    return ($"Error: {response.StatusCode} - {responseContent}", response.StatusCode);
                }
            }
        }

        public async Task<(string resultado, HttpStatusCode statusCode)> BuscarClienteAsync(string keyword)
        {
            var credenciales = _unitOfWork.FacturamaRepository.ObtenerCredencialesActivas();
            if (credenciales == null)
            {
                return ("Error: No se encontraron credenciales activas.", HttpStatusCode.BadRequest);
            }

            var urlBase = credenciales.Sandbox ? $"https://apisandbox.facturama.mx/Client?keyword={Uri.EscapeDataString(keyword)}" : $"https://api.facturama.mx/Client?keyword={Uri.EscapeDataString(keyword)}";

            using (HttpClient client = new HttpClient())
            {
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credenciales.UserName}:{credenciales.Password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var response = await client.GetAsync(urlBase);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return (responseContent, response.StatusCode); 
                }
                else
                {
                    return ($"Error: {response.StatusCode} - {responseContent}", response.StatusCode);
                }
            }
        }



        public async Task<(string resultado, HttpStatusCode statusCode)> DatosCompletosFacturama2(FacturamaFacturaClienteDTO datos)
        {
            try
            {
                var (resultado, statusCode) = await CrearFacturaAsync(datos.factura);

                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
                {
                    return (resultado, statusCode); 
                }
                else
                {
                    if (resultado.Contains("Error: Este RFC del receptor no existe en la lista de RFC inscritos no cancelados del SAT"))
                    {
                        var (clienteResultado, clienteStatusCode) = await CrearClienteAsync(datos.cliente);

                        if (clienteStatusCode == HttpStatusCode.Created || clienteStatusCode == HttpStatusCode.OK)
                        {
                            var (nuevoResultado, nuevoStatusCode) = await CrearFacturaAsync(datos.factura);
                            return (nuevoResultado, nuevoStatusCode);  
                        }
                        else
                        {
                            return ($"Error al crear el cliente: {clienteResultado}", clienteStatusCode); 
                        }
                    }

                    return ($"Error al crear la factura: {resultado}", statusCode);
                }
            }
            catch (Exception ex)
            {
                return ($"Error inesperado: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }



        public async Task<(string resultado, HttpStatusCode statusCode)> DatosCompletosFacturama(FacturamaFacturaClienteDTO datos)
        {
            try
            {
                if (datos == null || datos.factura == null || datos.cliente == null)
                {
                    return ("Error: Los datos de la factura o del cliente no pueden ser nulos.", HttpStatusCode.BadRequest);
                }

                string nombreCliente = datos.cliente.Name;
                string rfcCliente = datos.cliente.Rfc;

                var (resultadoBusqueda, statusCodeBusqueda) = await BuscarClienteAsync(nombreCliente);

                //if (statusCodeBusqueda == HttpStatusCode.OK && !string.IsNullOrWhiteSpace(resultadoBusqueda) && resultadoBusqueda != "[]")
                //{
                //    var clientesEncontrados = JsonConvert.DeserializeObject<List<FacturamaClienteDTO>>(resultadoBusqueda);
                //    var clienteExistente = clientesEncontrados.FirstOrDefault(c => c.Rfc == rfcCliente);

                //    if (clienteExistente != null)
                //    {
                //        datos.cliente = clienteExistente;
                //    }
                //}

                // Actualizar la factura con los datos del cliente existente si es necesario
                datos.factura.Receiver.Name = datos.cliente.Name;
                datos.factura.Receiver.Rfc = datos.cliente.Rfc;
                datos.factura.Receiver.FiscalRegime = datos.cliente.FiscalRegime;
                datos.factura.Receiver.TaxZipCode = datos.cliente.Address?.ZipCode;

                var (resultadoFactura, statusCodeFactura) = await CrearFacturaAsync(datos.factura);

                if (statusCodeFactura == HttpStatusCode.OK || statusCodeFactura == HttpStatusCode.Created)
                {
                    return (resultadoFactura, statusCodeFactura); 
                }
                else if (resultadoFactura.Contains("Error: Este RFC del receptor no existe en la lista de RFC inscritos no cancelados del SAT"))
                {
                    var (resultadoCliente, statusCodeCliente) = await CrearClienteAsync(datos.cliente);

                    if (statusCodeCliente == HttpStatusCode.Created || statusCodeCliente == HttpStatusCode.OK)
                    {
                        var (nuevoResultado, nuevoStatusCode) = await CrearFacturaAsync(datos.factura);
                        return (nuevoResultado, nuevoStatusCode);
                    }
                    else
                    {
                        return ($"Error al crear el cliente: {resultadoCliente}", statusCodeCliente);
                    }
                }
                else
                {
                    return ($"Error al crear la factura: {resultadoFactura}", statusCodeFactura);
                }
            }
            catch (Exception ex)
            {
                return ($"Error inesperado: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }


        public bool ActualizaEnviadoFacturama(int id, String UsuarioModificacion)
        {
            try
            {
                return _unitOfWork.FacturamaRepository.ActualizaEnviadoFacturama(id, UsuarioModificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<RegimenFiscalDTO> ObtenerListaRegimenFiscal()
        {
            try
            {
                var listaRegimenFiscal = _unitOfWork.FacturamaRepository.ObtenerListaRegimenFiscal();

                List<RegimenFiscalDTO> resultadoConvertido = listaRegimenFiscal
                    .Select(r => new RegimenFiscalDTO
                    {
                        Id = r.Id,
                        FiscalRegime = r.FiscalRegime,
                        Descripcion = r.Descripcion
                    }).ToList();

                return resultadoConvertido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsoCfdiDTO> ObtenerListaUsoCfdi()
        {
            try
            {
                var lista = _unitOfWork.FacturamaRepository.ObtenerListaUsoCfdi();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los usos CFDI: {ex.Message}", ex);
            }
        }



        public List<FormapagoFacturamaDTO> ObtenerFormapagoFacturama()
        {
            try
            {
                var lista = _unitOfWork.FacturamaRepository.ObtenerFormapagoFacturama();
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener los usos CFDI: {ex.Message}", ex);
            }
        }


        public string  InsertarRegimenFiscal(string clave, string descripcion, string usuario)
        {
            try
            {
                return _unitOfWork.FacturamaRepository.InsertarRegimenFiscal( clave,descripcion,usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarRegimenFiscal( int  id ,string clave, string descripcion, string usuario)
        {
            try
            {
                return _unitOfWork.FacturamaRepository.ActualizarRegimenFiscal(id,clave, descripcion, usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool EliminarRegimenFiscal(int id, string usuario)
        { 
            try
            {
                return _unitOfWork.FacturamaRepository.EliminarRegimenFiscal(id,usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string InsertarUsoComprobante(string clave, string descripcion, string usuario)
        {
            try
            {
                return _unitOfWork.FacturamaRepository.InsertarUsoComprobante(clave, descripcion, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarUsoComprobante(int id, string clave, string descripcion, string usuario)
        {
            try
            {
                return _unitOfWork.FacturamaRepository.ActualizarUsoComprobante(id, clave, descripcion, usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool EliminarUsoComprobante(int id, string usuario)
        {
            try
            {
                return _unitOfWork.FacturamaRepository.EliminarUsoComprobante(id, usuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ResumenMatriculaDTO> ObtenerResumenMatriculas(FiltroFechaDTO filtro)

        {
            try
            {
                return _unitOfWork.FacturamaRepository.ObtenerResumenMatriculas(filtro);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GuardarFacturaClienteCompleta(FacturamaFacturaClienteDTO dto, string codigoMatricula, int idCronogramaPagoDetalleFinal, string usuario)

        {
            var idCliente = _unitOfWork.FacturamaRepository.InsertarCliente(dto.cliente, usuario);

            _unitOfWork.FacturamaRepository.InsertarDireccionCliente(dto.cliente.Address, idCliente, usuario);

            var idFactura = _unitOfWork.FacturamaRepository.InsertarFactura(dto.factura, idCliente, codigoMatricula, idCronogramaPagoDetalleFinal, usuario);

            foreach (var item in dto.factura.Items)
            {
                var idItem = _unitOfWork.FacturamaRepository.InsertarItemFactura(item, idFactura, usuario);

                foreach (var tax in item.Taxes)
                {
                    _unitOfWork.FacturamaRepository.InsertarImpuestoItem(tax, idItem, usuario);
                }
            }


            var idAlumno = _unitOfWork.FacturamaRepository.ObtenerIdAlumnoPorIdCronograma(idCronogramaPagoDetalleFinal);
            _unitOfWork.FacturamaRepository.InsertarActualizarFacturamaAlumno(idAlumno, dto.cliente, dto.factura, usuario);

            return idFactura;
        }



        public async Task EnviarFacturaDesdeBaseDeDatos(int idFactura, string usuario)
        {
            var datos = _unitOfWork.FacturamaRepository.ObtenerDatosFacturaClientePorId(idFactura);

            var (resultado, statusCode) = await DatosCompletosFacturama(datos);

            if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
            {
                var uuid = ObtenerUUIDDesdeResultado(resultado); 
               
                var cfdiId = ObtenerCfdiIdDesdeResultado(resultado); 
                _unitOfWork.FacturamaRepository.ActualizarFacturaComoEnviada(idFactura, uuid, cfdiId, DateTime.Now, usuario);
                var listaDestinatariosExitosoFacturama = _unitOfWork.GmailCorreoRepository.listaDestinatariosExitosoFacturama();
                listaDestinatariosExitosoFacturama.Insert(0, new StringDTO { Valor = datos.cliente.Email });
                foreach (var destinatario in listaDestinatariosExitosoFacturama)
                {
                    await EnviarFacturaPorCorreoAsync(cfdiId, destinatario.Valor);
                }
            }
            else
            {
                var listaDestinatariosErroneoFacturama = _unitOfWork.GmailCorreoRepository.listaDestinatariosErroneoFacturama();
                List<string> listaCorreos = listaDestinatariosErroneoFacturama.Select(x => x.Valor).ToList();
                var DetalleFactura = _unitOfWork.FacturamaRepository.ObtenerDetalleFacturaFacturamaCronograma(idFactura);
                var mensaje = mensajeErrorFacturama(DetalleFactura,resultado);
                HelperCorreo.EnvioCorreoNotificacionesFacturas(listaCorreos, "BSG Institute", "Fallo en Envío a Facturama – Verificar Datos de Facturación del Alumno", mensaje);
                throw new Exception("Error al enviar a la API: " + resultado);
            }
        }

       

        private string ObtenerCfdiIdDesdeResultado(string json)
        {
            var jsonObject = JObject.Parse(json);
            return jsonObject["Id"]?.ToString() ?? throw new Exception("No se pudo obtener el Id del CFDI.");
        }
        public string ObtenerUUIDDesdeResultado(string json)
        {
            var jsonObject = JObject.Parse(json);
            var uuid = jsonObject["Complement"]?["TaxStamp"]?["Uuid"]?.ToString();
            return uuid ?? string.Empty;
        }

        public FacturamaFacturaClienteCronogrmaDTO ObtenerFacturaClientePorCodigoMatricula(string codigoMatricula)

 
        {
            try
            {
                return _unitOfWork.FacturamaRepository.ObtenerFacturaClientePorCodigoMatricula(codigoMatricula);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ObtenerIdFacturaPorCodigoMatricula(string codigoMatricula)
        {
            return _unitOfWork.FacturamaRepository.ObtenerIdFacturaPorCodigoMatricula(codigoMatricula);
        }

        public async Task<(string resultado, HttpStatusCode statusCode)> EnviarFacturaPorCorreoAsync(string cfdiId, string emailCliente)
        {
            var credenciales = _unitOfWork.FacturamaRepository.ObtenerCredencialesActivas();
            if (credenciales == null)
            {
                return ("Error: No se encontraron credenciales activas.", HttpStatusCode.BadRequest);
            }

            var urlBase = credenciales.Sandbox
                ? "https://apisandbox.facturama.mx/api/cfdi"
                : "https://api.facturama.mx/api/cfdi";

            var parametros = new Dictionary<string, string>
            {
                { "CfdiType", "issued" },
                { "CfdiId", cfdiId },
                { "Email", emailCliente },
                { "Subject", "Tu factura electrónica" },
                { "Comments", "Gracias por tu compra" },
                { "IssuerEmail", "mdelgadog@bsginstitute.com" }, 
                { "IncludePayBtn", "false" }
            };

            var queryString = string.Join("&", parametros.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
            var fullUrl = $"{urlBase}?{queryString}";

            using (HttpClient client = new HttpClient())
            {
                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{credenciales.UserName}:{credenciales.Password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var response = await client.PostAsync(fullUrl, null);
                var responseContent = await response.Content.ReadAsStringAsync();

                return (responseContent, response.StatusCode);
            }
        }

        public List<FacturamaFacturaMasivoDTO> ObtenerFacturasPendientesEnvio()

        {
            return _unitOfWork.FacturamaRepository.ObtenerFacturasPendientesEnvio();
        }
        public async Task EnviarFacturasMasivasDesdeBaseDeDatos(EnvioMasivoLoteDTO datos)
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
                    var datosFactura = _unitOfWork.FacturamaRepository.ObtenerDatosFacturaClientePorId(idFactura);

                    var (resultado, statusCode) = await DatosCompletosFacturama(datosFactura);

                    if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)
                    {

                        var uuid = ObtenerUUIDDesdeResultado(resultado);

                        var cfdiId = ObtenerCfdiIdDesdeResultado(resultado);
                        _unitOfWork.FacturamaRepository.ActualizarFacturaComoEnviada(idFactura, uuid, cfdiId, DateTime.Now, datos.Usuario);
                        var cronogramafinal = _unitOfWork.FacturamaRepository.ObtenerIdCronogramaPorIdFactura(idFactura);
                        var actulizarCronogramafinal = _unitOfWork.FacturamaRepository.ActualizaEnviadoFacturama(cronogramafinal, datos.Usuario);
                        
                        var listaDestinatariosExitosoFacturama = _unitOfWork.GmailCorreoRepository.listaDestinatariosExitosoFacturama();
                        listaDestinatariosExitosoFacturama.Insert(0, new StringDTO { Valor = datosFactura.cliente.Email });
                        foreach (var destinatario in listaDestinatariosExitosoFacturama)
                        {
                            await EnviarFacturaPorCorreoAsync(cfdiId, destinatario.Valor);
                        }
                    }
                    else
                    {
                        var listaDestinatariosErroneoFacturama = _unitOfWork.GmailCorreoRepository.listaDestinatariosErroneoFacturama();
                        List<string> listaCorreos = listaDestinatariosErroneoFacturama.Select(x => x.Valor).ToList();
                        var DetalleFactura = _unitOfWork.FacturamaRepository.ObtenerDetalleFacturaFacturamaCronograma(idFactura);
                        var mensaje = mensajeErrorFacturama(DetalleFactura, resultado);
                        HelperCorreo.EnvioCorreoNotificacionesFacturas(listaCorreos, "BSG Institute", "Fallo en Envío a Facturama – Verificar Datos de Facturación del Alumno", mensaje);
                        throw new Exception($"Error al enviar factura {idFactura}: {resultado}");
                    }
                }

                await Task.Delay(1000); 
            }
        }
        public static string mensajeErrorFacturama(FacturamaFacturaCronogramaDetalleDTO DetalleFactura, string DescripcionError)
        {
            string NombreAlumno = DetalleFactura.NombreCompletoAlumno;
            string CodigoMatricula = DetalleFactura.CodigoMatricula;
            string DescripcionPago = DetalleFactura.DescripcionPago;
            string MontoPago = DetalleFactura.MontoPago;
            DateTime fechaActual = DateTime.Now;
            CultureInfo cultura = new CultureInfo("es-ES");
            string fechaNotificacion = fechaActual.ToString("dd 'de' MMMM 'del' yyyy", cultura);
            string mensaje = string.Empty;

            string headear = "<head> <style> .container { max-width: 600px; margin: 0 auto; background-color: #f4f1f1; border: 1px solid #f4f1f1; border-radius: 5px; margin-top: 50px; } .section { margin-bottom: 20px; color: #333; } .section p { margin: 0; } .footer { text-align: center; color: #777; font-size: 12px; margin-top: 30px; } </style> </head>";

            string body = $@"
                    <div style='margin-left:8rem;margin-right:8rem'>
                        <div style='display: flex; align-items: center; border-bottom: 2px solid black; padding-bottom: 4px; width: 85%;'>
                            <img src='https://bsginstitute.com/favicon.ico' style='width: 30px; height: 30px;'>
                            <div style='display: flex; font-size: 25px; color: #414140; margin-left: 7px;'>
                                <div style='letter-spacing: -4px;'>BSG</div>
                                <div style='margin-left: 7px;'>Institute</div>
                            </div>
                        </div>
                        <div style='font-weight:bold;font-size:15px;padding-top:20px'>Estimado equipo,</div>
                        <br>
                        <div style='font-size:14px;width:85%'>
                            Se ha detectado un error en el proceso automático relacionado con un alumno. A continuación, se detallan los datos del caso para su revisión y corrección manual:
                        </div>
                        <br>
                        <div style='background:#FFF3CD;border-radius:5px;width:85%;border:1px solid #FFEEBA'>
                            <div style='padding:20px'>
                                <div style='font-size:14px;'><strong>Fecha de notificación:</strong> {fechaNotificacion}</div>
                                <div style='font-size:14px;'><strong>Nombre del alumno:</strong> {NombreAlumno}</div>
                                <div style='font-size:14px;'><strong>Código de matrícula:</strong> {CodigoMatricula}</div>
                                <div style='font-size:14px;'><strong>Descripción de Pago:</strong> {DescripcionPago}</div>
                                <div style='font-size:14px;'><strong>Monto de Pago:</strong> {MontoPago} Pesos Mexicanos</div>
                                <div style='font-size:14px;'><strong>Descripción del error:</strong></div>
                                <div style='margin-top:5px;font-size:14px;background-color:#f8d7da;border:1px solid #f5c6cb;border-radius:4px;padding:10px;color:#721c24'>
                                    {DescripcionError}
                                </div>
                            </div>
                        </div>
                        <br>
                        <div style='font-size: 13px;width:85%'>Por favor, tomar las acciones correspondientes lo antes posible para asegurar la continuidad del proceso.</div>
                        <br><br>
                        <div style='font-size: 13px;width:85%'>Atentamente,</div>
                        <div style='font-size: 13px;width:85% padding-bottom: 25px;'>Sistema de Notificaciones - BSG Institute</div>                        
                    </div>";

            mensaje = mensaje + headear + body;
            return mensaje;
        }

        public bool ExisteFacturaConfigurada(int idCronogramaPagoDetalleFinal)

        {
            return _unitOfWork.FacturamaRepository.ExisteFacturaConfigurada(idCronogramaPagoDetalleFinal);
        }

        /// Autor: Humberto Oscata
        /// Fecha: 18/09/2025
        /// Version: 1.0
        /// <summary>
        /// Elimina facturas creadas y pendientes de emitir Facturama
        /// </summary>
        /// <param name="idsFacturas">Lista de ids de las facturas a eliminar</param>
        /// <param name="usuario">Nombre del usuario que modificara</param>
        /// <returns>Resultado (true o false) del eliminar</returns>
        public bool EliminarFacturasPendientesFacturama(List<int> idsFacturas, string usuario)
        {
            return _unitOfWork.FacturamaRepository.EliminarFacturasPendientesFacturama(idsFacturas, usuario);
        }

    }

}

