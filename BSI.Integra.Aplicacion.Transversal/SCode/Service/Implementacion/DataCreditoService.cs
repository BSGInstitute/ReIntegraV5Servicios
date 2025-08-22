using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Net;
using System.Xml.Serialization;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DataCreditoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/09/2022
    /// <summary>
    /// Gestion general de DataCredito
    /// </summary>
    public class DataCreditoService : IDataCreditoService
    {
        private IUnitOfWork _unitOfWork;
        public DataCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public string ConsultarServicioHistoriaCreditoAlumnoColombia(string numeroDocumento, string primerApellido, int tipoIdentificacion)
        {
            WebClient wc = new WebClient();
            string url =
                $"http://167.71.100.135:8094/consultar?nroDoc={numeroDocumento}&primerApellido={primerApellido}&tipoIdentificacion={tipoIdentificacion}";
            //$"http://localhost:8094/consultar?nroDoc={numeroDocumento}&primerApellido={primerApellido}&tipoIdentificacion={tipoIdentificacion}";
            string resultado = wc.DownloadString(url);
            return resultado;
        }
        public bool ConsultarAlumnoColombia(string numeroDocumento, string primerApellido, int tipoIdentificacion, string usuario)
        {
            var servicioNaturalNacional = new DataCreditoDataNaturalNacionalService(_unitOfWork);
            var servicioScore = new DataCreditoDataScoreService(_unitOfWork);
            var servicioCuentaAhorro = new DataCreditoDataCuentaAhorroService(_unitOfWork);
            var servicioTarjetaCredito = new DataCreditoDataTarjetaCreditoService(_unitOfWork);
            var servicioCuentaCartera = new DataCreditoDataCuentaCarteraService(_unitOfWork);
            var servicioConsulta = new DataCreditoConsultumService(_unitOfWork);
            var servicioEndeudamientoGlobal = new DataCreditoDataEndeudamientoGlobalService(_unitOfWork);
            var servicioProductoValor = new DataCreditoDataProductoValorService(_unitOfWork);
            var servicioResumenPrincipal = new DataCreditoDataInfAgrResumenPrincipalService(_unitOfWork);
            var servicioResumenSaldo = new DataCreditoDataInfAgrResumenSaldoService(_unitOfWork);
            var servicioResumenSaldoSec = new DataCreditoDataInfAgrResumenSaldoSectorService(_unitOfWork);
            var servicioResumenSaldoMes = new DataCreditoDataInfAgrResumenSaldoMeService(_unitOfWork);
            var servicioResumenComportamniento = new DataCreditoDataInfAgrResumenComportamientoService(_unitOfWork);
            var servicioInfAgrTotal = new DataCreditoDataInfAgrTotalService(_unitOfWork);
            var servicioCompPortafolio = new DataCreditoDataInfAgrComposicionPortafolioService(_unitOfWork);
            var servicioEvolDeudaTrim = new DataCreditoDataInfAgrEvolucionDeudaTrimestreService(_unitOfWork);
            var servicioEvolDeudaAnalisisProm = new DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioService(_unitOfWork);
            var servicioHistoricoSaldoTipoCuent = new DataCreditoDataInfAgrHistoricoSaldoTipoCuentumService(_unitOfWork);
            var servicioHistoricoSaldoTotal = new DataCreditoDataInfAgrHistoricoSaldoTotalService(_unitOfWork);
            var servicioResumenEndeudamiento = new DataCreditoDataInfAgrResumenEndeudamientoService(_unitOfWork);
            var servicioMicroPerfilGeneral = new DataCreditoDataInfMicroPerfilGeneralService(_unitOfWork);
            var servicioMicroVectorSaldoMora = new DataCreditoDataInfMicroVectorSaldoMoraService(_unitOfWork);
            var servicioMicroEndeudamientoActu = new DataCreditoDataInfMicroEndeudamientoActualService(_unitOfWork);
            var servicioImagenTendencia = new DataCreditoDataInfMicroImagenTendenciaEndeudamientoService(_unitOfWork);
            var servicioMicroAnalisisVector = new DataCreditoDataInfMicroAnalisisVectorService(_unitOfWork);
            var servicioEvolucionDeuda = new DataCreditoDataInfMicroEvolucionDeudumService(_unitOfWork);
            var servicioDataCredito = new DataCreditoService(_unitOfWork);
            var servicioDataCreditoLog = new DataCreditoLogService(_unitOfWork);
            var servicioDataCreditoBusqueda = new DataCreditoBusquedumService(_unitOfWork);

            string respuestaServicio = servicioDataCredito.ConsultarServicioHistoriaCreditoAlumnoColombia(numeroDocumento, primerApellido, tipoIdentificacion);

            //Respaldo de la consulta en la db
            DataCreditoLog entidadDataCreditoLog = new DataCreditoLog()
            {
                NumeroDocumento = numeroDocumento,
                PrimerApellido = primerApellido,
                TipoIdentificacion = tipoIdentificacion,
                RespuestaXml = respuestaServicio,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            servicioDataCreditoLog.Add(entidadDataCreditoLog);

            //filtra por errores
            if (respuestaServicio.Contains("\"estado\":") && respuestaServicio.Contains("\"mensaje\":"))
            {
                DataCreditoErrorDTO error = JsonConvert.DeserializeObject<DataCreditoErrorDTO>(respuestaServicio);
                throw new Exception(error.Mensaje);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Informes), new XmlRootAttribute("Informes"));
            StringReader stringReader = new StringReader(respuestaServicio);
            Informes informe = (Informes)serializer.Deserialize(stringReader);

            //mapeo cabecera informe
            DataCreditoBusquedum busqueda = new DataCreditoBusquedum()
            {
                FechaConsulta = DateTime.Parse(informe.Informe.FechaConsulta),
                CodigoSeguridad = informe.Informe.CodSeguridad,
                TipoIdentificacion = Convert.ToInt32(informe.Informe.IdentificacionDigitada),
                NroDocumento = informe.Informe.IdentificacionDigitada,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            servicioDataCreditoBusqueda.Add(busqueda);

            //añadir mapeo de errores
            if (informe.Informe.Respuesta != "13")
                throw new Exception("Valide los datos proporcinados, no se recibió información.");

            //mapeo informe
            DataCreditoDataNaturalNacional naturalNacional = new DataCreditoDataNaturalNacional()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                NroDocumento = informe.Informe.NaturalNacional.Identificacion.Numero,
                Nombres = informe.Informe.NaturalNacional.Nombres,
                PrimerApellido = informe.Informe.NaturalNacional.PrimerApellido,
                SegundoApellido = informe.Informe.NaturalNacional.SegundoApellido,
                NombreCompleto = informe.Informe.NaturalNacional.NombreCompleto,
                Validada = informe.Informe.NaturalNacional.Validada == "true"
                    ? true
                    : (informe.Informe.NaturalNacional.Validada == "false" ? false : (bool?)null),
                Rut = informe.Informe.NaturalNacional.Rut == "true"
                    ? true
                    : (informe.Informe.NaturalNacional.Rut == "false" ? false : (bool?)null),
                Genero = informe.Informe.NaturalNacional.Genero,
                IdentificacionEstado = informe.Informe.NaturalNacional.Identificacion.Estado,
                IdentificacionFechaExpedicion =
                    DateTime.Parse(informe.Informe.NaturalNacional.Identificacion.FechaExpedicion),
                IdentificacionCiudad = informe.Informe.NaturalNacional.Identificacion.Ciudad,
                IdentificacionDepartamento = informe.Informe.NaturalNacional.Identificacion.Departamento,
                IdentificacionNumero = informe.Informe.NaturalNacional.Identificacion.Numero,
                EdadMinima = Convert.ToInt32(informe.Informe.NaturalNacional.Edad.Min),
                EdadMaxima = Convert.ToInt32(informe.Informe.NaturalNacional.Edad.Max),

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            DataCreditoDataScore score = new DataCreditoDataScore()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                Tipo = informe.Informe.Score?.Tipo,
                Puntaje = informe.Informe.Score?.Puntaje,
                Fecha = informe.Informe.Score != null ? DateTime.Parse(informe.Informe.Score.Fecha) : (DateTime?)null,
                Poblacion = informe.Informe.Score?.Poblacion,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            List<DataCreditoDataCuentaAhorro> listaCuentaAhorro = new List<DataCreditoDataCuentaAhorro>();
            informe.Informe.CuentaAhorro.ForEach(f =>
            {
                DataCreditoDataCuentaAhorro cuentaAhorro = new DataCreditoDataCuentaAhorro()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Bloqueada = f.Bloqueada == "true" ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    Entidad = f.Entidad,
                    Numero = f.Numero,
                    FechaApertura = f.FechaApertura != null ? DateTime.Parse(f.FechaApertura) : (DateTime?)null,
                    Calificacion = f.Calificacion,
                    SituacionTitular = f.SituacionTitular,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    CodigoDaneCiudad = f.CodigoDaneCiudad,
                    TipoIdentificacion = f.TipoIdentificacion != null ? Convert.ToInt32(f.TipoIdentificacion) : (int?)null,
                    Identificacion = f.Identificacion,
                    Sector = f.Sector,
                    CaracteristicaClase = f.Caracteristicas?.Clase,
                    ValorMoneda = f.Valores.Valor != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Moneda : null,
                    ValorFecha = f.Valores.Valor != null && f.Valores.Valor.Any()
                        ? DateTime.Parse(f.Valores.Valor.First().Fecha)
                        : (DateTime?)null,
                    ValorCalificacion = f.Valores.Valor != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Calificacion : null,
                    EstadoCodigo = f.Estado?.Codigo,
                    EstadoFecha = f.Estado != null ? DateTime.Parse(f.Estado.Fecha) : (DateTime?)null,
                    //Estadocantidad = f.Estado.Cantidad
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaCuentaAhorro.Add(cuentaAhorro);
            });

            List<DataCreditoDataTarjetaCredito> listaTarjeta = new List<DataCreditoDataTarjetaCredito>();
            informe.Informe.TarjetaCredito.ForEach(f =>
            {
                DataCreditoDataTarjetaCredito tarjeta = new DataCreditoDataTarjetaCredito()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Bloqueada = f.Bloqueada == "true" ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    Entidad = f.Entidad,
                    Numero = f.Numero,
                    FechaApertura = f.FechaApertura != null ? DateTime.Parse(f.FechaApertura) : (DateTime?)null,
                    FechaVencimiento = f.FechaVencimiento != null ? DateTime.Parse(f.FechaVencimiento) : (DateTime?)null,
                    Comportamiento = f.Comportamiento,
                    FormaPago = f.FormaPago,
                    ProbabilidadIncumplimiento = f.ProbabilidadIncumplimiento != null ? Convert.ToDecimal(f.ProbabilidadIncumplimiento) : (decimal?)null,
                    Calificacion = f.Calificacion,
                    SituacionTitular = f.SituacionTitular,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    CodigoDaneCiudad = f.CodigoDaneCiudad,
                    TipoIdentificacion = f.TipoIdentificacion != null ? Convert.ToInt32(f.TipoIdentificacion) : (int?)null,
                    Identificacion = f.Identificacion,
                    Sector = f.Sector,
                    CalificacionHd =
                        f.CalificacionHD == "true" ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    CaracteristicaFranquicia = f.Caracteristicas?.Franquicia,
                    CaracteristicaClase = f.Caracteristicas?.Clase,
                    CaracteristicaMarca = f.Caracteristicas?.Marca,
                    CaracteristicaAmparada = f.Caracteristicas?.Amparada == "true"
                        ? true
                        : (f.Bloqueada == "false" ? false : (bool?)null),
                    CaracteristicaCodigoAmparada = f.Caracteristicas?.CodigoAmparada,
                    CaracteristicaGarantia = f.Caracteristicas?.Garantia,
                    ValorMoneda = f.Valores != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Moneda : null,
                    ValorFecha = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Fecha != null
                        ? DateTime.Parse(f.Valores.Valor.First().Fecha)
                        : (DateTime?)null,
                    ValorCalificacion = f.Valores != null && f.Valores.Valor.Any() ? f.Valores.Valor.First().Calificacion : null,
                    ValorSaldoActual = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoActual)
                        : (decimal?)null,
                    ValorSaldoMora = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoMora)
                        : (decimal?)null,
                    ValorDisponible = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().Disponible)
                        : (decimal?)null,
                    ValorCuota = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().Cuota)
                        : (decimal?)null,
                    ValorCuotasMora = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().CuotasMora)
                        : (decimal?)null,
                    ValorDiasMora = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToInt32(f.Valores.Valor.First().DiasMora)
                        : (int?)null,
                    ValorFechaPagoCuota = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaPagoCuota != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaPagoCuota)
                        : (DateTime?)null,
                    ValorFechaLimitePago = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaLimitePago != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaLimitePago)
                        : (DateTime?)null,
                    ValorCupoTotal = f.Valores != null && f.Valores.Valor.Any()
                        ? Convert.ToDecimal(f.Valores.Valor.First().CupoTotal)
                        : (decimal?)null,
                    EstadoPlasticoCodigo = f.Estados?.EstadoPlastico?.Codigo,
                    EstadoPlasticoFecha = (f.Estados != null && f.Estados.EstadoPlastico != null && f.Estados.EstadoPlastico.Fecha != null) ?
                        Convert.ToDateTime(f.Estados.EstadoPlastico.Fecha) : (DateTime?)null,
                    EstadoCuentaCodigo = f.Estados?.EstadoCuenta?.Codigo,
                    EstadoCuentaFecha = f.Estados?.EstadoCuenta != null && f.Estados?.EstadoCuenta.Fecha != null
                        ? DateTime.Parse(f.Estados?.EstadoCuenta.Fecha)
                        : (DateTime?)null,
                    EstadoOrigenCodigo = f.Estados?.EstadoOrigen?.Codigo,
                    EstadoOrigenFecha = f.Estados?.EstadoOrigen != null && f.Estados.EstadoOrigen.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoOrigen.Fecha)
                        : (DateTime?)null,
                    EstadoPagoCodigo = f.Estados?.EstadoPago?.Codigo,
                    EstadoPagoFecha = f.Estados?.EstadoPago != null && f.Estados.EstadoPago.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoPago.Fecha)
                        : (DateTime?)null,
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTarjeta.Add(tarjeta);
            });

            List<DataCreditoDataCuentaCartera> listaCuenta = new List<DataCreditoDataCuentaCartera>();
            informe.Informe.CuentaCartera.ForEach(f =>
            {
                DataCreditoDataCuentaCartera cuenta = new DataCreditoDataCuentaCartera()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Bloqueada = (f.Bloqueada != null && f.Bloqueada == "true") ? true : (f.Bloqueada == "false" ? false : (bool?)null),
                    Entidad = f.Entidad,
                    Numero = f.Numero,
                    FechaApertura = f.FechaApertura != null ? DateTime.Parse(f.FechaApertura) : (DateTime?)null,
                    FechaVencimiento = f.FechaVencimiento != null ? DateTime.Parse(f.FechaVencimiento) : (DateTime?)null,
                    Comportamiento = f.Comportamiento,
                    FormaPago = f.FormaPago,
                    ProbabilidadIncumplimiento = f.ProbabilidadIncumplimiento != null ? Convert.ToDecimal(f.ProbabilidadIncumplimiento) : (decimal?)null,
                    Calificacion = f.Calificacion,
                    SituacionTitular = f.SituacionTitular,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    CodigoDaneCiudad = f.CodigoDaneCiudad,
                    TipoIdentificacion = f.TipoIdentificacion != null ? Convert.ToInt32(f.TipoIdentificacion) : (int?)null,
                    Identificacion = f.Identificacion,
                    Sector = f.Sector,
                    CalificacionHd =
                        f.CalificacionHD != null && f.CalificacionHD == "true" ? true : (f.CalificacionHD == "false" ? false : (bool?)null),

                    CaracteristicaTipoCuenta = f.Caracteristicas?.TipoCuenta,
                    CaracteristicaTipoObligacion = f.Caracteristicas?.TipoObligacion,
                    CaracteristicaTipoContrato = f.Caracteristicas?.TipoContrato,
                    CaracteristicaEjecucionContrato = f.Caracteristicas?.EjecucionContrato,
                    CaracteristicaMesesPermanencia = f.Caracteristicas?.MesesPermanencia,
                    CaracteristicaCalidadDeudor = f.Caracteristicas?.CalidadDeudor,
                    CaracteristicaGarantia = f.Caracteristicas?.Garantia,
                    ValorMoneda = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().Moneda
                        : null,
                    ValorFecha = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Fecha != null
                        ? DateTime.Parse(f.Valores.Valor.First().Fecha)
                        : (DateTime?)null,
                    ValorCalificacion = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().Calificacion
                        : null,
                    ValorSaldoActual = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().SaldoActual != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoActual)
                        : (decimal?)null,
                    ValorSaldoMora = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().SaldoMora != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().SaldoMora)
                        : (decimal?)null,
                    ValorDisponible = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Disponible != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().Disponible)
                        : (decimal?)null,
                    ValorCuota = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().Cuota != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().Cuota)
                        : (decimal?)null,
                    ValorCuotasMora = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().CuotasMora != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().CuotasMora)
                        : (decimal?)null,
                    ValorDiasMora = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().DiasMora != null
                        ? Convert.ToInt32(f.Valores.Valor.First().DiasMora)
                        : (int?)null,
                    ValorFechaPagoCuota = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaPagoCuota != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaPagoCuota)
                        : (DateTime?)null,
                    ValorFechaLimitePago = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().FechaLimitePago != null
                        ? DateTime.Parse(f.Valores.Valor.First().FechaLimitePago)
                        : (DateTime?)null,
                    ValorPeriodicidad = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().Periodicidad
                        : null,
                    ValorTotalCuotas = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().TotalCuotas
                        : null,
                    ValorValorInicial = f.Valores != null && f.Valores.Valor.Any() && f.Valores.Valor.First().ValorInicial != null
                        ? Convert.ToDecimal(f.Valores.Valor.First().ValorInicial)
                        : (decimal?)null,
                    ValorCuotasCanceladas = f.Valores != null && f.Valores.Valor.Any()
                        ? f.Valores.Valor.First().CuotasCanceladas
                        : null,
                    EstadoCuentaCodigo = f.Estados.EstadoCuenta?.Codigo,
                    EstadoCuentaFecha = f.Estados.EstadoCuenta != null && f.Estados.EstadoCuenta.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoCuenta.Fecha)
                        : (DateTime?)null,
                    EstadoOrigenCodigo = f.Estados.EstadoOrigen?.Codigo,
                    EstadoOrigenFecha = f.Estados.EstadoOrigen != null && f.Estados.EstadoOrigen.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoOrigen.Fecha)
                        : (DateTime?)null,
                    EstadoPagoCodigo = f.Estados.EstadoPago?.Codigo,
                    EstadoPagoFecha = f.Estados.EstadoPago != null && f.Estados.EstadoPago.Fecha != null
                        ? DateTime.Parse(f.Estados.EstadoPago.Fecha)
                        : (DateTime?)null,
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaCuenta.Add(cuenta);
            });

            List<DataCreditoConsultum> listaConsulta = new List<DataCreditoConsultum>();
            informe.Informe.Consulta.ForEach(f =>
            {
                DataCreditoConsultum consulta = new DataCreditoConsultum()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    TipoCuenta = f.TipoCuenta,
                    Entidad = f.Entidad,
                    Oficina = f.Oficina,
                    Ciudad = f.Ciudad,
                    Razon = f.Razon,
                    Cantidad = f.Cantidad,
                    NitSuscriptor = f.NitSuscriptor,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaConsulta.Add(consulta);
            });

            List<DataCreditoDataEndeudamientoGlobal> listaEndeudamiento = new List<DataCreditoDataEndeudamientoGlobal>();
            informe.Informe.EndeudamientoGlobal.ForEach(f =>
            {
                DataCreditoDataEndeudamientoGlobal endeudamiento = new DataCreditoDataEndeudamientoGlobal()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Calificacion = f.Calificacion,
                    Fuente = f.Fuente,
                    SaldoPendiente = f.SaldoPendiente,
                    TipoCredito = f.TipoCredito,
                    Moneda = f.Moneda,
                    NumeroCreditos = f.NumeroCreditos,
                    Independiente = f.Independiente,
                    FechaReporte = f.FechaReporte != null ? DateTime.Parse(f.FechaReporte) : (DateTime?)null,
                    EntidadNombre = f.Entidad?.Nombre,
                    EntidadNit = f.Entidad?.Nit,
                    EntidadSector = f.Entidad?.Sector,
                    GarantiaTipo = f.Garantia?.Tipo,
                    GarantiaValor = f.Garantia?.Valor,
                    Llave = f.Llave,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaEndeudamiento.Add(endeudamiento);
            });

            DataCreditoDataProductoValor productoValor = new DataCreditoDataProductoValor()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                Producto = informe.Informe.ProductosValores?.Producto,
                Valor1 = informe.Informe.ProductosValores?.Valor1,
                Valor2 = informe.Informe.ProductosValores?.Valor2,
                Valor3 = informe.Informe.ProductosValores?.Valor3,
                Valor4 = informe.Informe.ProductosValores?.Valor4,
                Valor5 = informe.Informe.ProductosValores?.Valor5,
                Valor6 = informe.Informe.ProductosValores?.Valor6,
                Valor7 = informe.Informe.ProductosValores?.Valor7,
                Valor8 = informe.Informe.ProductosValores?.Valor8,
                Valor9 = informe.Informe.ProductosValores?.Valor9,
                Valor10 = informe.Informe.ProductosValores?.Valor10,
                Valor1smlv = informe.Informe.ProductosValores.Valor1smlv,
                Valor2smlv = informe.Informe.ProductosValores.Valor2smlv,
                Valor3smlv = informe.Informe.ProductosValores.Valor3smlv,
                Valor4smlv = informe.Informe.ProductosValores.Valor4smlv,
                Valor5smlv = informe.Informe.ProductosValores.Valor5smlv,
                Valor6smlv = informe.Informe.ProductosValores.Valor6smlv,
                Valor7smlv = informe.Informe.ProductosValores.Valor7smlv,
                Valor8smlv = informe.Informe.ProductosValores.Valor8smlv,
                Valor9smlv = informe.Informe.ProductosValores.Valor9smlv,
                Valor10smlv = informe.Informe.ProductosValores.Valor10smlv,
                Razon1 = informe.Informe.ProductosValores?.Razon1,
                Razon2 = informe.Informe.ProductosValores?.Razon2,
                Razon3 = informe.Informe.ProductosValores?.Razon3,
                Razon4 = informe.Informe.ProductosValores?.Razon4,
                Razon5 = informe.Informe.ProductosValores?.Razon5,
                Razon6 = informe.Informe.ProductosValores?.Razon6,
                Razon7 = informe.Informe.ProductosValores?.Razon7,
                Razon8 = informe.Informe.ProductosValores?.Razon8,
                Razon9 = informe.Informe.ProductosValores?.Razon9,
                Razon10 = informe.Informe.ProductosValores?.Razon10,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            //calculo del agregado - resumen
            DataCreditoDataInfAgrResumenPrincipal resumenPrincipal = new DataCreditoDataInfAgrResumenPrincipal()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                CreditosVigentes = informe.Informe.InfoAgregada.Resumen.Principales?.CreditoVigentes,
                CreditosCerrados = informe.Informe.InfoAgregada.Resumen.Principales?.CreditosCerrados,
                CreditosActualesNegativos = informe.Informe.InfoAgregada.Resumen.Principales?.CreditosActualesNegativos,
                HistNegUlt12Meses = informe.Informe.InfoAgregada.Resumen.Principales?.HistNegUlt12Meses,
                CuentasAbiertasAhoccb = informe.Informe.InfoAgregada.Resumen.Principales?.CuentasAbiertasAHOCCB,
                CuentasCerradasAhoccb = informe.Informe.InfoAgregada.Resumen.Principales?.CuentasCerradasAHOCCB,
                ConsultadasUlt6meses = informe.Informe.InfoAgregada.Resumen.Principales?.ConsultadasUlt6meses,
                DesacuerdosAlaFecha = informe.Informe.InfoAgregada.Resumen.Principales?.DesacuerdosALaFecha,
                ReclamosVigentes = informe.Informe.InfoAgregada.Resumen.Principales?.ReclamosVigentes,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            DataCreditoDataInfAgrResumenSaldo resumenSaldo = new DataCreditoDataInfAgrResumenSaldo()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SaldoTotalEnMora = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotalEnMora != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotalEnMora)
                    : (decimal?)null,
                SaldoM30 = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM30 != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM30)
                    : (decimal?)null,
                SaldoM60 = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM60 != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM60)
                    : (decimal?)null,
                SaldoM90 = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM90 != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoM90)
                    : (decimal?)null,
                CuotaMensual = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.CuotaMensual != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.CuotaMensual)
                    : (decimal?)null,
                SaldoCreditoMasAlto = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoCreditoMasAlto != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoCreditoMasAlto)
                    : (decimal?)null,
                SaldoTotal = informe.Informe.InfoAgregada.Resumen.Saldos != null && informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotal != null
                    ? Convert.ToDecimal(informe.Informe.InfoAgregada.Resumen.Saldos.SaldoTotal)
                    : (decimal?)null,

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            List<DataCreditoDataInfAgrResumenSaldoSector> listaSaldoSector = new List<DataCreditoDataInfAgrResumenSaldoSector>();
            informe.Informe.InfoAgregada.Resumen.Saldos.Sector.ForEach(f =>
            {
                DataCreditoDataInfAgrResumenSaldoSector saldoSector = new DataCreditoDataInfAgrResumenSaldoSector()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Sector = f.Sectores,
                    Saldo = f.Saldo != null ? Convert.ToDecimal(f.Saldo) : (decimal?)null,
                    Participacion = f.Participacion,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaSaldoSector.Add(saldoSector);
            });

            List<DataCreditoDataInfAgrResumenSaldoMe> listaSaldoMes = new List<DataCreditoDataInfAgrResumenSaldoMe>();
            informe.Informe.InfoAgregada.Resumen.Saldos.Mes.ForEach(f =>
            {
                DataCreditoDataInfAgrResumenSaldoMe saldoMes = new DataCreditoDataInfAgrResumenSaldoMe()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    SaldoTotalMora = f.SaldoTotalMora != null ? Convert.ToDecimal(f.SaldoTotalMora) : (decimal?)null,
                    SaldoTotal = f.SaldoTotal != null ? Convert.ToDecimal(f.SaldoTotal) : (decimal?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaSaldoMes.Add(saldoMes);
            });

            List<DataCreditoDataInfAgrResumenComportamiento> listaResumenComportamiento = new List<DataCreditoDataInfAgrResumenComportamiento>();
            informe.Informe.InfoAgregada.Resumen.Comportamiento.Mes.ForEach(f =>
            {
                DataCreditoDataInfAgrResumenComportamiento resumenComportamiento =
                    new DataCreditoDataInfAgrResumenComportamiento()
                    {
                        IdDataCreditoBusqueda = busqueda.Id,
                        Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                        Comportamiento = f.Comportamiento,
                        Cantidad = f.Cantidad != null ? Convert.ToInt32(f.Cantidad) : (int?)null,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                listaResumenComportamiento.Add(resumenComportamiento);
            });

            //totales
            List<DataCreditoDataInfAgrTotal> listaTotalTipoCuenta = new List<DataCreditoDataInfAgrTotal>();
            informe.Informe.InfoAgregada.Totales.TipoCuenta.ForEach(f =>
            {
                DataCreditoDataInfAgrTotal totalTipoCuenta = new DataCreditoDataInfAgrTotal()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    TipoMapeo = "TipoCuenta",
                    CodigoTipo = f.CodigoTipo,
                    Tipo = f.Tipo,
                    CalidadDeudor = f.CalidadDeudor,
                    Participacion = null,
                    Cupo = f.Cupo,
                    Saldo = f.Saldo,
                    SaldoMora = f.SaldoMora,
                    Cuota = f.Cuota,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTotalTipoCuenta.Add(totalTipoCuenta);
            });

            informe.Informe.InfoAgregada.Totales.Total.ForEach(f =>
            {
                DataCreditoDataInfAgrTotal totalTipoCuenta = new DataCreditoDataInfAgrTotal()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    TipoMapeo = "Total ",
                    CodigoTipo = null,
                    Tipo = null,
                    CalidadDeudor = f.CalidadDeudor,
                    Participacion = f.Participacion,
                    Cupo = f.Cupo,
                    Saldo = f.Saldo,
                    SaldoMora = f.SaldoMora,
                    Cuota = f.Cuota,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTotalTipoCuenta.Add(totalTipoCuenta);
            });
            //ComposicionPortafolio
            List<DataCreditoDataInfAgrComposicionPortafolio> listaTotalComposicionPortafolio = new List<DataCreditoDataInfAgrComposicionPortafolio>();
            informe.Informe.InfoAgregada.ComposicionPortafolio.TipoCuenta.ForEach(f =>
            {
                DataCreditoDataInfAgrComposicionPortafolio totalComposicionPortafolio = new DataCreditoDataInfAgrComposicionPortafolio()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Tipo = f.Tipo,
                    CalidadDeudor = f.CalidadDeudor,
                    Porcentaje = f.Porcentaje != null ? Convert.ToDecimal(f.Porcentaje) : (decimal?)null,
                    Cantidad = f.Cantidad != null ? Convert.ToInt32(f.Cantidad) : (int?)null,
                    EstadoCodigo = f.Estado.Codigo,
                    EstadoCantidad = f.Estado.Cantidad != null ? Convert.ToInt32(f.Estado.Cantidad) : (int?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaTotalComposicionPortafolio.Add(totalComposicionPortafolio);
            });

            //EvolucionDeuda
            List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listaEvolucionDeudaTrimestre = new List<DataCreditoDataInfAgrEvolucionDeudaTrimestre>();
            informe.Informe.InfoAgregada.EvolucionDeuda.Trimestre.ForEach(f =>
            {
                DataCreditoDataInfAgrEvolucionDeudaTrimestre evolucionDeudaTrimestre =
                    new DataCreditoDataInfAgrEvolucionDeudaTrimestre()
                    {
                        IdDataCreditoBusqueda = busqueda.Id,
                        Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                        Cuota = f.Cuota != null ? Convert.ToDecimal(f.Cuota) : (decimal?)null,
                        Cupototal = f.CupoTotal != null ? Convert.ToDecimal(f.CupoTotal) : (decimal?)null,
                        Saldo = f.Saldo != null ? Convert.ToDecimal(f.Saldo) : (decimal?)null,
                        PorcentajeUso = f.PorcentajeUso,
                        Score = f.Score != null ? Convert.ToDecimal(f.Score) : (decimal?)null,
                        Calificacion = f.Calificacion,
                        AperturaCuentas = f.AperturaCuentas,
                        CierreCuentas = f.CierreCuentas,
                        TotalAbiertas = f.TotalAbiertas != null ? Convert.ToInt32(f.TotalAbiertas) : (int?)null,
                        TotalCerradas = f.TotalCerradas != null ? Convert.ToInt32(f.TotalCerradas) : (int?)null,
                        MoraMaxima = f.MoraMaxima,
                        MesesMoraMaxima = f.MesesMoraMaxima != null ? Convert.ToInt32(f.MesesMoraMaxima) : (int?)null,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                listaEvolucionDeudaTrimestre.Add(evolucionDeudaTrimestre);
            });

            DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio analisisPromedio =
                new DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Cuota = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Cuota != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Cuota)
                        : (decimal?)null,
                    CupoTotal = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CupoTotal != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CupoTotal)
                        : (decimal?)null,
                    Saldo = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Saldo != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Saldo)
                        : (decimal?)null,
                    Porcentaje = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.PorcentajeUso,
                    Score = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Score != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Score)
                        : (decimal?)null,
                    Calificacion = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Calificacion != null
                        ? Convert.ToInt32(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.Calificacion)
                        : (int?)null,
                    AperturaCuentas =
                        informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.AperturaCuentas != null
                            ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio
                                .AperturaCuentas)
                            : (decimal?)null,
                    CierreCuentas = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CierreCuentas != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.CierreCuentas)
                        : (decimal?)null,
                    TotalAbiertas = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.TotalAbiertas,
                    TotalCerradas = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.TotalCerradas,
                    MoraMaxima = informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.MoraMaxima != null
                        ? Convert.ToDecimal(informe.Informe.InfoAgregada.EvolucionDeuda.AnalisisPromedio.MoraMaxima)
                        : (decimal?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

            //HistoricoSaldos
            List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum> listaTipoCuenta = new List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentum>();
            informe.Informe.InfoAgregada.HistoricoSaldos.TipoCuenta.ForEach(f =>
            {
                f.Trimestre.ForEach(g =>
                {
                    DataCreditoDataInfAgrHistoricoSaldoTipoCuentum tipoCuenta =
                        new DataCreditoDataInfAgrHistoricoSaldoTipoCuentum()
                        {
                            IdDataCreditoBusqueda = busqueda.Id,
                            Tipo = f.Tipo,
                            Fecha = g.Fecha != null ? DateTime.Parse(g.Fecha) : (DateTime?)null,
                            TotalCuentas = g.TotalCuentas != null ? Convert.ToInt32(g.TotalCuentas) : (int?)null,
                            CuentasConsideradas = g.CuentasConsideradas != null
                                ? Convert.ToInt32(g.CuentasConsideradas)
                                : (int?)null,
                            Saldo = g.Saldo != null ? Convert.ToDecimal(g.Saldo) : (decimal?)null,

                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    listaTipoCuenta.Add(tipoCuenta);
                });
            });

            List<DataCreditoDataInfAgrHistoricoSaldoTotal> listaSaldoTotal = new List<DataCreditoDataInfAgrHistoricoSaldoTotal>();
            informe.Informe.InfoAgregada.HistoricoSaldos.Totales.ForEach(f =>
            {
                DataCreditoDataInfAgrHistoricoSaldoTotal saldoTotal = new DataCreditoDataInfAgrHistoricoSaldoTotal()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    TotalCuentas = f.TotalCuentas != null ? Convert.ToInt32(f.TotalCuentas) : (int?)null,
                    CuentasConsideradas = f.CuentasConsideradas != null ? Convert.ToInt32(f.CuentasConsideradas) : (int?)null,
                    Saldo = f.Saldo != null ? Convert.ToDecimal(f.Saldo) : (decimal?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaSaldoTotal.Add(saldoTotal);
            });

            //ResumenEndeudamiento
            List<DataCreditoDataInfAgrResumenEndeudamiento> listaResumenEndeudamiento = new List<DataCreditoDataInfAgrResumenEndeudamiento>();
            informe.Informe.InfoAgregada.ResumenEndeudamiento.Trimestre.ForEach(t =>
            {
                t.Sector.ForEach(s =>
                {
                    s.Cartera.ForEach(c =>
                    {
                        DataCreditoDataInfAgrResumenEndeudamiento resumenEndeudamiento =
                            new DataCreditoDataInfAgrResumenEndeudamiento()
                            {
                                IdDataCreditoBusqueda = busqueda.Id,
                                TrimestreFecha = t.Fecha != null ? Convert.ToDateTime(t.Fecha) : (DateTime?)null,
                                SectorSector = s.Sectores,
                                SectorCodigoSector = s.CodigoSector,
                                SectorGarantiaAdmisible = s.GarantiaAdmisible,
                                SectorGarantiaOtro = s.GarantiaOtro,
                                CarteraTipo = c.Tipo,
                                CarteraNumeroCuentas = c.NumeroCuentas != null
                                    ? Convert.ToInt32(c.NumeroCuentas)
                                    : (int?)null,
                                CarteraValor = c.Valor != null ? Convert.ToDecimal(c.Valor) : (decimal?)null,

                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        listaResumenEndeudamiento.Add(resumenEndeudamiento);
                    });
                });
            });

            ////Calculo Microcredito
            //resumen
            DataCreditoDataInfMicroPerfilGeneral boCreditoVigente = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosVigentes.TotalComoCodeudorYOtros,
                Tipo = "CreditosVigentes",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneral boCreditoCerrado = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosCerrados.TotalComoCodeudorYOtros,
                Tipo = "CreditosCerrados ",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneral boCreditosReestructurado = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosReestructurados.TotalComoCodeudorYOtros,
                Tipo = "CreditosReestructurados",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneral boCreditosRefinanciado = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.CreditosRefinanciados.TotalComoCodeudorYOtros,
                Tipo = "CreditosRefinanciados",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneral boConsultaUlt6Meses = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.ConsultaUlt6Meses.TotalComoCodeudorYOtros,
                Tipo = "ConsultaUlt6Meses",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneral boDesacuerdos = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorFinanciero,
                SectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorCooperativo,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.SectorTelcos,
                TotalComoPrincipal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.TotalComoPrincipal,
                TotalComoCodeudorYotros = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.Desacuerdos.TotalComoCodeudorYOtros,
                Tipo = "Desacuerdos",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };
            DataCreditoDataInfMicroPerfilGeneral boAntiguedadDesde = new DataCreditoDataInfMicroPerfilGeneral()
            {
                IdDataCreditoBusqueda = busqueda.Id,
                SectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.AntiguedadDesde.SectorFinanciero,
                SectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.AntiguedadDesde.SectorReal,
                SectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.PerfilGeneral.AntiguedadDesde.SectorTelcos,
                Tipo = "AntiguedadDesde",

                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            };

            List<DataCreditoDataInfMicroVectorSaldoMora> listaBoVectorSaldoMora = new List<DataCreditoDataInfMicroVectorSaldoMora>();
            informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.SaldosYMoras.ForEach(f =>
            {
                DataCreditoDataInfMicroVectorSaldoMora boVectorSaldoMora = new DataCreditoDataInfMicroVectorSaldoMora()
                {
                    IdDataCreditoBusqueda = busqueda.Id,
                    PoseeSectorCooperativo = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorCooperativo == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorCooperativo == "false" ? false : (bool?)null),
                    PoseeSectorFinanciero = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorFinanciero == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorFinanciero == "false" ? false : (bool?)null),
                    PoseeSectorReal = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorReal == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorReal == "false" ? false : (bool?)null),
                    PoseeSectorTelcos = informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorTelcos == "true"
                        ? true
                        : (informe.Informe.InfoAgregadaMicrocredito.Resumen.VectorSaldosYMoras.PoseeSectorTelcos == "false" ? false : (bool?)null),
                    Fecha = f.Fecha != null ? DateTime.Parse(f.Fecha) : (DateTime?)null,
                    TotalCuentasMora = f.TotalCuentasMora != null ? Convert.ToInt32(f.TotalCuentasMora) : (int?)null,
                    SaldoDeudaTotalMora = f.SaldoDeudaTotalMora != null ? Convert.ToDecimal(f.SaldoDeudaTotalMora) : (decimal?)null,
                    SaldoDeudaTotal = f.SaldoDeudaTotal != null ? Convert.ToDecimal(f.SaldoDeudaTotal) : (decimal?)null,
                    MorasMaxSectorFinanciero = f.MorasMaxSectorFinanciero,
                    MorasMaxSectorReal = f.MorasMaxSectorReal,
                    MorasMaxSectorTelcos = f.MorasMaxSectorTelcos,
                    MorasMaximas = f.MorasMaximas,
                    NumCreditos30 = f.NumCreditos30 != null ? Convert.ToInt32(f.NumCreditos30) : (int?)null,
                    NumCreditosMayorIgual60 = f.NumCreditosMayorIgual60 != null ? Convert.ToInt32(f.NumCreditosMayorIgual60) : (int?)null,

                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                listaBoVectorSaldoMora.Add(boVectorSaldoMora);
            });

            List<DataCreditoDataInfMicroEndeudamientoActual> listaBoEndeudamiento = new List<DataCreditoDataInfMicroEndeudamientoActual>();
            informe.Informe.InfoAgregadaMicrocredito.Resumen.EndeudamientoActual.Sector.ForEach(s =>
            {
                s.TipoCuenta.ForEach(t =>
                {
                    t.Usuario.ForEach(u =>
                    {
                        u.Cuenta.ForEach(uc =>
                        {
                            DataCreditoDataInfMicroEndeudamientoActual boEndeudamiento =
                                new DataCreditoDataInfMicroEndeudamientoActual()
                                {
                                    IdDataCreditoBusqueda = busqueda.Id,
                                    SectorCodigoSector = s.CodSector,
                                    TipoCuenta = t.TipoCuentas,
                                    TipoUsuario = u.TipoUsuario,
                                    EstadoActual = uc.EstadoActual,
                                    Calificacion = uc.Calificacion,
                                    ValorInicial = uc.ValorInicial != null
                                        ? Convert.ToDecimal(uc.ValorInicial)
                                        : (decimal?)null,
                                    SaldoActual = uc.SaldoActual != null
                                        ? Convert.ToDecimal(uc.SaldoActual)
                                        : (decimal?)null,
                                    SaldoMora =
                                        uc.SaldoMora != null ? Convert.ToDecimal(uc.SaldoMora) : (decimal?)null,
                                    CuotaMes = uc.CuotaMes != null ? Convert.ToDecimal(uc.CuotaMes) : (decimal?)null,
                                    ComportamientoNegativo = uc.ComportamientoNegativo == "true"
                                        ? true
                                        : (uc.ComportamientoNegativo == "false" ? false : (bool?)null),
                                    TotalDeudaCarteras = uc.TotalDeudaCarteras != null
                                        ? Convert.ToDecimal(uc.TotalDeudaCarteras)
                                        : (decimal?)null,

                                    Estado = true,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                            listaBoEndeudamiento.Add(boEndeudamiento);
                        });
                    });
                });
            });

            List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento> listaBoImagenTendencia = new List<DataCreditoDataInfMicroImagenTendenciaEndeudamiento>();
            informe.Informe.InfoAgregadaMicrocredito.Resumen.ImagenTendenciaEndeudamiento.Series.ForEach(s =>
            {
                s.Valores.Valor.ForEach(v =>
                {
                    DataCreditoDataInfMicroImagenTendenciaEndeudamiento boImagenTendencia =
                        new DataCreditoDataInfMicroImagenTendenciaEndeudamiento()
                        {
                            IdDataCreditoBusqueda = busqueda.Id,
                            Serie = s.Serie,
                            Valor = v.Valores != null ? Convert.ToDecimal(v.Valores) : (decimal?)null,
                            Fecha = v.Fecha != null ? DateTime.Parse(v.Fecha) : (DateTime?)null,

                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    listaBoImagenTendencia.Add(boImagenTendencia);
                });
            });

            //analisis vectores
            List<DataCreditoDataInfMicroAnalisisVector> listaBoAnalisisVector = new List<DataCreditoDataInfMicroAnalisisVector>();
            informe.Informe.InfoAgregadaMicrocredito.AnalisisVectores.Sector.ForEach(s =>
            {
                s.Cuenta.ForEach(c =>
                {
                    c.CaracterFecha.ForEach(cf =>
                    {
                        DataCreditoDataInfMicroAnalisisVector boAnalisisVector =
                            new DataCreditoDataInfMicroAnalisisVector()
                            {
                                IdDataCreditoBusqueda = busqueda.Id,
                                NombreSector = s.NombreSector,
                                CuentaEntidad = c.Entidad,
                                CuentaNumeroCuenta = c.NumeroCuenta,
                                CuentaTipoCuenta = c.TipoCuenta,
                                CuentaEstado = c.Estado,
                                ContieneDatos = c.ContieneDatos == "true"
                                    ? true
                                    : (c.ContieneDatos == "false" ? false : (bool?)null),
                                Fecha = cf.Fecha != null ? DateTime.Parse(cf.Fecha) : (DateTime?)null,
                                SaldoDeudaTotalMora = cf.SaldoDeudaTotalMora != null ? cf.SaldoDeudaTotalMora : null,

                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        listaBoAnalisisVector.Add(boAnalisisVector);
                    });
                });
            });

            //evolucion deuda
            List<DataCreditoDataInfMicroEvolucionDeudum> listaBoEvolucionDeuda = new List<DataCreditoDataInfMicroEvolucionDeudum>();
            informe.Informe.InfoAgregadaMicrocredito.EvolucionDeuda.EvolucionDeudaSector.ForEach(e =>
            {
                e.EvolucionDeudaTipoCuenta.ForEach(tc =>
                {
                    tc.EvolucionDeudaValorTrimestre.ForEach(evt =>
                    {
                        DataCreditoDataInfMicroEvolucionDeudum boEvolucionDeuda =
                            new DataCreditoDataInfMicroEvolucionDeudum()
                            {
                                IdDataCreditoBusqueda = busqueda.Id,
                                CodigoSector = e.CodSector,
                                NombreSector = e.NombreSector,
                                TipoCuenta = tc.TipoCuenta,
                                Trimestre = evt.Trimestre,
                                Num = evt.Num,
                                CupoInicial = evt.CupoInicial != null
                                    ? Convert.ToDecimal(evt.CupoInicial)
                                    : (decimal?)null,
                                Saldo = evt.Saldo != null ? Convert.ToDecimal(evt.Saldo) : (decimal?)null,
                                SaldoMora = evt.SaldoMora != null ? Convert.ToDecimal(evt.SaldoMora) : (decimal?)null,
                                Cuota = evt.Cuota != null ? Convert.ToDecimal(evt.Cuota) : (decimal?)null,
                                PorcentajeDeuda = evt.PorcentajeDeuda != null
                                    ? Convert.ToDecimal(evt.PorcentajeDeuda)
                                    : (decimal?)null,
                                CodigoMenorCalificacion = evt.CodMenorCalificacion,
                                TextoMenorCalificacion = evt.TextoMenorCalificacion,

                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                        listaBoEvolucionDeuda.Add(boEvolucionDeuda);
                    });
                });
            });

            servicioNaturalNacional.Add(naturalNacional);
            servicioScore.Add(score);
            servicioCuentaAhorro.Add(listaCuentaAhorro);
            servicioTarjetaCredito.Add(listaTarjeta);
            servicioCuentaCartera.Add(listaCuenta);
            servicioConsulta.Add(listaConsulta);
            servicioEndeudamientoGlobal.Add(listaEndeudamiento);
            servicioProductoValor.Add(productoValor);
            servicioResumenPrincipal.Add(resumenPrincipal);
            servicioResumenSaldo.Add(resumenSaldo);
            servicioResumenSaldoSec.Add(listaSaldoSector);
            servicioResumenSaldoMes.Add(listaSaldoMes);
            servicioResumenComportamniento.Add(listaResumenComportamiento);
            servicioInfAgrTotal.Add(listaTotalTipoCuenta);
            servicioCompPortafolio.Add(listaTotalComposicionPortafolio);
            servicioEvolDeudaTrim.Add(listaEvolucionDeudaTrimestre);
            servicioEvolDeudaAnalisisProm.Add(analisisPromedio);
            servicioHistoricoSaldoTipoCuent.Add(listaTipoCuenta);
            servicioHistoricoSaldoTotal.Add(listaSaldoTotal);
            servicioResumenEndeudamiento.Add(listaResumenEndeudamiento);
            servicioMicroPerfilGeneral.Add(boCreditoVigente);
            servicioMicroPerfilGeneral.Add(boCreditoCerrado);
            servicioMicroPerfilGeneral.Add(boCreditosReestructurado);
            servicioMicroPerfilGeneral.Add(boCreditosRefinanciado);
            servicioMicroPerfilGeneral.Add(boConsultaUlt6Meses);
            servicioMicroPerfilGeneral.Add(boAntiguedadDesde);
            servicioMicroVectorSaldoMora.Add(listaBoVectorSaldoMora);
            servicioMicroEndeudamientoActu.Add(listaBoEndeudamiento);
            servicioImagenTendencia.Add(listaBoImagenTendencia);
            servicioMicroAnalisisVector.Add(listaBoAnalisisVector);
            servicioEvolucionDeuda.Add(listaBoEvolucionDeuda);
            return true;
        }

    }
}
