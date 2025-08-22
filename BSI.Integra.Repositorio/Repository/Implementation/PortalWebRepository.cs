using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: Portal Web Repository
    /// Autor: Gilmer Quispe.
    /// Fecha: 08/07/2023
    /// <summary>
    /// Gestión general de consultas a integraDB_PortalWeb
    /// </summary>
    public class PortalWebRepository : IPortalWebRepository
    {
        private IDapperRepository _dapperRepository;
        public PortalWebRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 08/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el (PK)
        /// </summary>
        /// <param name="nombreServicio"> campo NombreServicio de T_NumeroOrden </param>
        /// <returns> NumeroOrdenDTO </returns>
        public NumeroOrdenDTO buscarNumeroOrden(string nombreServicio)
        {
            try
            {
                var query = @"SELECT Id,
                                   ContadorActual,
                                   NombreServicio
                            FROM fin.V_TNumeroOrden
                            WHERE NombreServicio = @NombreServicio;";
                var respuesta = _dapperRepository.FirstOrDefault(query, new { NombreServicio = nombreServicio });
                if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                    return JsonConvert.DeserializeObject<NumeroOrdenDTO>(respuesta);

                return new NumeroOrdenDTO();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 08/07/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el NumeroOden de T_NumeroOrden por el (PK)
        /// </summary>
        /// <param name="numeroOrden"> DTO que contiene el (PK) y el NumeroOrden nuevo </param>
        /// <returns> bool </returns>
        public bool actualizarNumeroOrden(NumeroOrdenDTO numeroOrden)
        {
            try
            {
                // string _queryInsertar = "[fin].[SP_ActualizarNumeroOrden]";   //Porduccion
                string _queryInsertar = "[fin].[SP_ActualizarNumeroOrden]";
                var queryInsert = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                    Id = numeroOrden.Id,
                    ContadorActual = numeroOrden.ContadorActual
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 08/07/2023
        /// Version: 1.0
        /// <summary>
        /// Crea un nuevo registri en T_NumeroOrden
        /// </summary>
        /// <param name="numeroOrden"> Nuevos parametros a registrarse </param>
        /// <returns> bool </returns>
        public bool registrarNumeroOrden(NumeroOrdenDTO numeroOrden)
        {
            try
            {
                //  string _queryInsertar = "[fin].[SP_RegistrarNumeroOrden]";      //Produccion
                string _queryInsertar = "[fin].[SP_RegistrarNumeroOrden]";

                var queryInsert = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                    NombreServicio = numeroOrden.NombreServicio,
                    ContadorActual = numeroOrden.ContadorActual
                });
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 10/07/2023
        /// Version: 1.0
        /// <summary>
        /// Crea un nuevo registro en [40.76.216.5].integraDB_PortalWeb.dbo.T_TransaccionAuditoriaPago y retorna el (PK)
        /// </summary>
        /// <param name="transaccion"> Nuevos parametros a registrarse </param>
        /// <returns> int </returns>
        public int RegistrarTransaccionAuditoriaPago(RegistroProcesoPagoDTO transaccion)
        {
            try
            {
                //string _queryInsertar = "[fin].[SP_RegistrarTransaccionAuditoriaPago]";     //Produccion
                string _queryInsertar = "[fin].[SP_RegistrarTransaccionAuditoriaPago]";

                var queryInsert = _dapperRepository.QuerySPFirstOrDefault(_queryInsertar, new
                {
                    IdUsuario = transaccion.IdUsuario,
                    IdAlumno = transaccion.IdAlumno,
                    IdMatriculaCabecera = transaccion.IdMatriculaCabecera,
                    IdentificadorTransaccion = transaccion.IdentificadorTransaccion,
                    IdFormaPago = transaccion.IdFormaPago,
                    NumeroPedidoOrden = transaccion.NumeroPedidoOrden,
                    TokenComercio = transaccion.TokenComercio,
                    NombreServicio = transaccion.NombreServicio,
                    EstadoOperacion = transaccion.EstadoOperacion,
                    TipoPago = transaccion.TipoPago,
                    CodigoComercio = transaccion.CodigoComercio,
                    RegistroEnvioComercio = transaccion.RegistroEnvioComercio,
                    RegistroRespuestaComercio = transaccion.RegistroRespuestaComercio,
                    RegistroTransaccionJson = transaccion.RegistroTransaccionJson,
                    MontoTotal = transaccion.MontoTotal,
                    Correo = transaccion.Correo,
                    NumeroPedidoComercio = transaccion.NumeroPedidoComercio,
                    RequiereDatoTarjeta = transaccion.RequiereDatoTarjeta,
                    PagoOrganico = transaccion.PagoOrganico
                });
                var aux = JsonConvert.DeserializeObject<ValorIntDTO>(queryInsert);
                return aux.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 04/08/2023
        /// Version: 1.0
        /// <param name="numeroCelular"> (PK) de [integraDB_PortalWeb].[dbo].[T_TransaccionAuditoriaPago] </param>
        /// <summary>
        /// Obtiene el registro de la transaccion generada para el pago
        /// </summary> 
        /// <returns>   </returns>
        public TransaccionAuditoriaPagoRespuestaDTO ObtenerTransactionPorCelular(string numeroCelular)
        {
            try
            {
                if (numeroCelular.Length > 8)
                    numeroCelular = numeroCelular.Substring(numeroCelular.Length - 8);

                string query = @"ope.SP_ProcesoPagoIvrObtenerDetalle";

                var queryResultado = _dapperRepository.QuerySPFirstOrDefault(query, new { NumeroCelular = numeroCelular });

                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "null")
                {
                    var dato = JsonConvert.DeserializeObject<TransaccionAuditoriaPagoDTO>(queryResultado);
                    var datosExtra = JsonConvert.DeserializeObject<TransaccionAuditoriaPagoDTO>(dato.RegistroTransaccionJson);
                    var respuesta = new TransaccionAuditoriaPagoRespuestaDTO
                    {
                        Id = dato.Id,
                        IdMatriculaCabecera = dato.IdMatriculaCabecera,
                        IdAlumno = dato.IdAlumno,
                        IdentificadorTransaccion = dato.IdentificadorTransaccion,
                        IdPasarelaPagoPw = datosExtra.IdPasarelaPago,
                        MedioPago = datosExtra.MedioPago,
                        Anexo = dato.Anexo,
                        MontoTotal = dato.MontoTotal,
                        Moneda = dato.Moneda
                    };
                    return respuesta;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
