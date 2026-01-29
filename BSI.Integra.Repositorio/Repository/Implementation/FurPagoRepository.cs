using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using System.Drawing;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FurPagoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_FurPago
    /// </summary>
    public class FurPagoRepository : GenericRepository<TFurPago>, IFurPagoRepository
    {
        private Mapper _mapper;

        public FurPagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurPago, FurPago>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFurPago MapeoEntidad(FurPago entidad)
        {
            try
            {
                //crea la entidad padre
                TFurPago modelo = _mapper.Map<TFurPago>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurPago Add(FurPago entidad)
        {
            try
            {
                var FurPago = MapeoEntidad(entidad);
                base.Insert(FurPago);
                return FurPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFurPago Update(FurPago entidad)
        {
            try
            {
                var FurPago = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FurPago.RowVersion = entidadExistente.RowVersion;

                base.Update(FurPago);
                return FurPago;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TFurPago> Add(IEnumerable<FurPago> listadoEntidad)
        {
            try
            {
                List<TFurPago> listado = new List<TFurPago>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TFurPago> Update(IEnumerable<FurPago> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFurPago> listado = new List<TFurPago>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve  el mayor numero de pago
        /// </summary>
        /// <param name="idFur"> Identificador del fur</param>
        /// <returns> int </returns>
        public int obtenerNumeroPagoByFur(int idFur)
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.IdFur == idFur).Max(x => x.NumeroPago);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todos la lista de furpagos relacionado a un fur.
        /// </summary>
        /// <returns> IEnumerable<FurPagoDTO></returns>
        public IEnumerable<FurPagoDTO> BuscarListaFurPagos(int? area, int? ciudad, int? anio, int? semana, int? moneda, bool? estado)
        {
            try
            {
                List<FurPagoDTO> listaFurPago = new List<FurPagoDTO>();
                area = (area != 0) ? area : null;
                ciudad = (ciudad != 0) ? ciudad : null;
                anio = (anio!= 0) ? anio : null;
                semana = (semana != 0) ? semana : null;
                moneda = (moneda != 0) ? moneda : null;
                estado = (estado == true || estado == false) ? estado : null;


                var _query = @"
                    SELECT IdFur, Codigo, IdProveedor, NombreProveedor, NombreProveedorComprobante, IdProducto, NombreProducto, IdCC, IdPais, NombreCentroCosto, 
                            NumeroCuenta, DescripcionCuenta, Cantidad, MonedaFur, NombreMonedaFur, PrecioUnitarioSoles, PrecioUnitarioDolares, PrecioTotalSoles, PrecioTotalDolares, IdDocumentoPago, 
                            NombreDocumento, NumeroRecibo, Descripcion, NumeroComprobante, FechaEfectivo, PagoMonedaOrigen, PagoDolares, Usuario, FechaModificacion, IdPersonaTrabajo, 
                            IdCiudad, NumeroSemana, EstadoCancelado, FechaAnio, MonedaPagoRealizado, NombreMonedaPagoRealizado 
                    FROM fin.V_ObtenerFurConFurPago 
                    WHERE 
                        (IdPersonaTrabajo = @area OR @area is null) AND 
                        (IdCiudad = @ciudad OR @ciudad is null) AND 
                        (NumeroSemana = @semana OR @semana is null) AND 
                        (EstadoCancelado = @estado OR @estado is null) AND 
                        (FechaAnio = @anio OR @anio is null) AND 
                        (MonedaPagoRealizado = @moneda OR @moneda is null) AND 
                        FaseAprobacion = 5 AND (Antiguo = 0 OR Antiguo is null) AND
                        Estado = 1 AND EstadoMoneda = 1 AND 
                        EstadoCiudad = 1 AND EstadoArea = 1";

                var listaFurPagoDB = _dapperRepository.QueryDapper(_query, new { area, ciudad, anio, semana, moneda, estado });
                if (!string.IsNullOrEmpty(listaFurPagoDB) && !listaFurPagoDB.Contains("[]"))
                {
                    listaFurPago = JsonConvert.DeserializeObject<List<FurPagoDTO>>(listaFurPagoDB);
                }
                return listaFurPago;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todos la lista de fur  pagos relacionado a un fur.
        /// </summary>
        /// <returns> IEnumerable<FurPagoRealizadoDTO></returns>
        public IEnumerable<FurPagoRealizadoDTO> ObtenerPagosRealizadosPorFur(int IdFur)
        {
            try
            {
                List<FurPagoRealizadoDTO> furPagoRealizado = new List<FurPagoRealizadoDTO>();
                var _query = "SELECT Id, IdComprobantePagoPorFur, NombreComprobantePagoPorFur, NumeroPago, IdMoneda, NombreMoneda, NumeroCuenta, NumeroRecibo, IdFormaPago, NombreFormaPago, FechaCobroBanco, PrecioTotalMonedaOrigen, PrecioTotalMonedaDolares, IdCancelado, NombreCancelado FROM fin.V_ObtenerPagosRealizadosPorFur WHERE IdFur = @IdFur AND Estado = 1";
                var furPagoRealizadoDB = _dapperRepository.QueryDapper(_query, new { IdFur = IdFur });
                if (!string.IsNullOrEmpty(furPagoRealizadoDB) && !furPagoRealizadoDB.Contains("[]"))
                {
                    furPagoRealizado = JsonConvert.DeserializeObject<List<FurPagoRealizadoDTO>>(furPagoRealizadoDB);
                }
                return furPagoRealizado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene todos los pagos de un respectivo FUR.
        /// </summary>
        /// <returns></returns>
        public int ObtenerFurPago(int? IdFur)
        {
            try
            {
                //FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                var correlativo = GetBy(x => x.Estado == true && x.IdFur == IdFur, x => new { x.NumeroPago }).ToList();

                if (correlativo.Count() == 0 || correlativo == null)
                {
                    return 1;
                }
                else
                {
                    var correlativo2 = correlativo.Max(x => x.NumeroPago);

                    if (correlativo2 > 0)
                    {
                        return correlativo2 + 1;
                    }
                    else
                    {
                        return 1;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Obtiene una Lista de FormaPago (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerListaFormaPago()
        {
            try
            {
                List<ComboDTO> lista = new List<ComboDTO>();
                var _query = "SELECT Id, Descripcion AS Nombre FROM fin.T_FormaPago  where Estado=1";
                var listaDB = _dapperRepository.QueryDapper(_query, null);
                if (!listaDB.Contains("[]") && !string.IsNullOrEmpty(listaDB))
                {
                    lista = JsonConvert.DeserializeObject<List<ComboDTO>>(listaDB);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        ///  Genera el Reporte de Estado de Cuenta por Proveedor
        /// </summary>
        /// <returns></returns>
        public List<ReporteEstadoCuentaProveedorDTO> GenerarReporteEstadoCuentaProveedor(string? Empresa, string? Ciudad,int? Proveedor ,string Comprobante, string FechaInicio,string FechaFin,string CuentaContable)
        {
            try
            {
                string serie = null;
                string NumeroComprobante = null;
                if (Comprobante != null)
                {
                    if (Comprobante != string.Empty)
                    {
                        serie = Comprobante.Split("-")[0];
                        NumeroComprobante = Comprobante.Split("-")[1];
                    }
                }
                List<ReporteEstadoCuentaProveedorDTO> EstadoCuentaProveedor = new List<ReporteEstadoCuentaProveedorDTO>();

                string query = "FIN.SP_ObtenerResultadoParaReporteEstadoCuentaPorProveedorV1";

                var EstadoCuentaProveedorDB = _dapperRepository.QuerySPDapper(query, new
                {
                    Empresa,
                    Ciudad,
                    Proveedor,
                    CuentaContable,
                    NumeroComprobante,
                    serie,
                    FechaInicio,
                    FechaFin
                });
                if (!EstadoCuentaProveedorDB.Contains("[]") && !string.IsNullOrEmpty(EstadoCuentaProveedorDB))
                {
                    EstadoCuentaProveedor = JsonConvert.DeserializeObject<List<ReporteEstadoCuentaProveedorDTO>>(EstadoCuentaProveedorDB);
                }
                return EstadoCuentaProveedor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// Autor: Miguel Valdivia
        /// Fecha: 24/01/2026
        /// Version: 1.2
        /// <summary>
        /// Convierte un monto de una moneda origen a una moneda destino usando el tipo de cambio mas reciente
        /// IMPORTANTE: La BD tiene MonedaADolar y DolarAMoneda con el mismo valor (error de datos)
        ///            Por eso calculamos MonedaADolar = 1 / DolarAMoneda
        /// </summary>
        /// <param name="idMonedaOrigen">Id de la moneda de origen</param>
        /// <param name="idMonedaDestino">Id de la moneda de destino</param>
        /// <param name="monto">Monto a convertir</param>
        /// <returns>ConversionMonedaDTO con el resultado de la conversion</returns>
        public ConversionMonedaDTO ConvertirMoneda(int idMonedaOrigen, int idMonedaDestino, decimal monto)
        {
            try
            {
                const int ID_DOLAR = 19;
                ConversionMonedaDTO resultado = new ConversionMonedaDTO();

                // Si las monedas son iguales, retornar el mismo monto
                if (idMonedaOrigen == idMonedaDestino)
                {
                    var queryMoneda = @"SELECT NombrePlural FROM pla.T_Moneda WHERE Id = @idMoneda AND Estado = 1";
                    var monedaDB = _dapperRepository.QueryDapper(queryMoneda, new { idMoneda = idMonedaOrigen });
                    string nombreMoneda = "Moneda";
                    if (!string.IsNullOrEmpty(monedaDB) && !monedaDB.Contains("[]"))
                    {
                        var monedaInfo = JsonConvert.DeserializeObject<List<dynamic>>(monedaDB);
                        if (monedaInfo != null && monedaInfo.Count > 0)
                        {
                            nombreMoneda = monedaInfo[0].NombrePlural?.ToString() ?? "Moneda";
                        }
                    }

                    resultado.MontoConvertido = Math.Round(monto, 2);
                    resultado.TipoCambioUtilizado = 1;
                    resultado.FechaTipoCambio = DateTime.Now;
                    resultado.MonedaOrigenNombre = nombreMoneda;
                    resultado.MonedaDestinoNombre = nombreMoneda;
                    return resultado;
                }

                // Query para obtener tipo de cambio con nombre de moneda
                // NOTA: Solo necesitamos DolarAMoneda porque calcularemos MonedaADolar = 1 / DolarAMoneda
                var queryTipoCambio = @"
            SELECT TOP 1
                DolarAMoneda,
                Fecha,
                (SELECT NombrePlural FROM pla.T_Moneda WHERE Id = @idMoneda AND Estado = 1) as NombreMoneda
            FROM fin.T_TipoCambioMoneda
            WHERE IdMoneda = @idMoneda
              AND Estado = 1
            ORDER BY Fecha DESC, Id DESC";

                decimal montoConvertido = 0;
                decimal tipoCambioUtilizado = 1;
                DateTime fechaTipoCambio = DateTime.Now;
                string monedaOrigenNombre = string.Empty;
                string monedaDestinoNombre = string.Empty;

                // Caso 1: Origen es DÓLAR → Destino es OTRA MONEDA
                // Fórmula: monto * DolarAMoneda_Destino
                if (idMonedaOrigen == ID_DOLAR)
                {
                    var tipoCambioDestinoDB = _dapperRepository.QueryDapper(queryTipoCambio, new { idMoneda = idMonedaDestino });

                    if (string.IsNullOrEmpty(tipoCambioDestinoDB) || tipoCambioDestinoDB.Contains("[]"))
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    var tipoCambioDestino = JsonConvert.DeserializeObject<List<TipoCambioMonedaDTO>>(tipoCambioDestinoDB);
                    if (tipoCambioDestino == null || tipoCambioDestino.Count == 0 || tipoCambioDestino[0].DolarAMoneda == 0)
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    // Convertir DE dólares A moneda destino
                    montoConvertido = monto * tipoCambioDestino[0].DolarAMoneda;
                    tipoCambioUtilizado = tipoCambioDestino[0].DolarAMoneda;
                    fechaTipoCambio = tipoCambioDestino[0].Fecha;
                    monedaOrigenNombre = "Dólares Americanos";
                    monedaDestinoNombre = tipoCambioDestino[0].NombreMoneda ?? "Moneda Destino";
                }
                // Caso 2: Origen es OTRA MONEDA → Destino es DÓLAR
                // Fórmula: monto * (1 / DolarAMoneda_Origen) = monto / DolarAMoneda_Origen
                else if (idMonedaDestino == ID_DOLAR)
                {
                    var tipoCambioOrigenDB = _dapperRepository.QueryDapper(queryTipoCambio, new { idMoneda = idMonedaOrigen });

                    if (string.IsNullOrEmpty(tipoCambioOrigenDB) || tipoCambioOrigenDB.Contains("[]"))
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    var tipoCambioOrigen = JsonConvert.DeserializeObject<List<TipoCambioMonedaDTO>>(tipoCambioOrigenDB);
                    if (tipoCambioOrigen == null || tipoCambioOrigen.Count == 0 || tipoCambioOrigen[0].DolarAMoneda == 0)
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    // INGENIERÍA INVERSA: MonedaADolar_Real = 1 / DolarAMoneda
                    decimal monedaADolarReal = 1 / tipoCambioOrigen[0].DolarAMoneda;

                    // Convertir DE moneda origen A dólares
                    montoConvertido = monto * monedaADolarReal;
                    tipoCambioUtilizado = monedaADolarReal;
                    fechaTipoCambio = tipoCambioOrigen[0].Fecha;
                    monedaOrigenNombre = tipoCambioOrigen[0].NombreMoneda ?? "Moneda Origen";
                    monedaDestinoNombre = "Dólares Americanos";
                }
                // Caso 3: Conversión cruzada (ninguna es dólar)
                // Paso 1: Origen → Dólares usando (1 / DolarAMoneda_Origen)
                // Paso 2: Dólares → Destino usando DolarAMoneda_Destino
                else
                {
                    var tipoCambioOrigenDB = _dapperRepository.QueryDapper(queryTipoCambio, new { idMoneda = idMonedaOrigen });
                    var tipoCambioDestinoDB = _dapperRepository.QueryDapper(queryTipoCambio, new { idMoneda = idMonedaDestino });

                    if (string.IsNullOrEmpty(tipoCambioOrigenDB) || tipoCambioOrigenDB.Contains("[]"))
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    if (string.IsNullOrEmpty(tipoCambioDestinoDB) || tipoCambioDestinoDB.Contains("[]"))
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    var tipoCambioOrigen = JsonConvert.DeserializeObject<List<TipoCambioMonedaDTO>>(tipoCambioOrigenDB);
                    var tipoCambioDestino = JsonConvert.DeserializeObject<List<TipoCambioMonedaDTO>>(tipoCambioDestinoDB);

                    if (tipoCambioOrigen == null || tipoCambioOrigen.Count == 0 || tipoCambioOrigen[0].DolarAMoneda == 0 ||
                        tipoCambioDestino == null || tipoCambioDestino.Count == 0 || tipoCambioDestino[0].DolarAMoneda == 0)
                    {
                        throw new Exception("TIPO-CAMBIO-NO-ENCONTRADO: Tipo de cambio no actualizado para el día de hoy");
                    }

                    // INGENIERÍA INVERSA: MonedaADolar_Real = 1 / DolarAMoneda
                    decimal monedaADolarOrigenReal = 1 / tipoCambioOrigen[0].DolarAMoneda;

                    // Paso 1: Convertir origen A dólares
                    decimal montoDolares = monto * monedaADolarOrigenReal;

                    // Paso 2: Convertir DE dólares A destino
                    montoConvertido = montoDolares * tipoCambioDestino[0].DolarAMoneda;

                    tipoCambioUtilizado = monedaADolarOrigenReal * tipoCambioDestino[0].DolarAMoneda;
                    fechaTipoCambio = tipoCambioOrigen[0].Fecha > tipoCambioDestino[0].Fecha
                        ? tipoCambioDestino[0].Fecha
                        : tipoCambioOrigen[0].Fecha;
                    monedaOrigenNombre = tipoCambioOrigen[0].NombreMoneda ?? "Moneda Origen";
                    monedaDestinoNombre = tipoCambioDestino[0].NombreMoneda ?? "Moneda Destino";
                }

                resultado.MontoConvertido = Math.Round(montoConvertido, 2);
                resultado.TipoCambioUtilizado = Math.Round(tipoCambioUtilizado, 6);
                resultado.FechaTipoCambio = fechaTipoCambio;
                resultado.MonedaOrigenNombre = monedaOrigenNombre;
                resultado.MonedaDestinoNombre = monedaDestinoNombre;

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
