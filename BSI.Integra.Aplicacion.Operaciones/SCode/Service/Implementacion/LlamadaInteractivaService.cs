using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Operaciones;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operaciones;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: LlamadaInteractivaService
    /// Autor: Gilmer Quipse
    /// Fecha: 06/07/2023
    /// <summary>
    /// Gestión general de Llamadas interactivas
    /// </summary>
    public class LlamadaInteractivaService : ILlamadaInteractivaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        protected string _merchantIdSoles = string.Empty;
        protected string _tiendaIdSoles = string.Empty;
        protected string _merchantIdDolares = string.Empty;
        protected string _tiendaIdDolares = string.Empty;
        protected string _merchantId = string.Empty;
        protected string _tiendaId = string.Empty;
        protected string _moneda = string.Empty;
        protected string _monedaIso = string.Empty;
        public string emailFinanzas = "bamontoya@bsginstitute.com";
        public LlamadaInteractivaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProcesoPagoIvr, ProcesoPagoIvrDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 05/07/2023
        /// Version: 1.0
        /// <summary>
        /// Arma la lista de cuotas de pago del alumno por el idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> (PK) de T_MatriculaCabecera </param>    
        /// <returns> (CronogramaPagoDetalleDTO, ExcepcionDTO) </returns>
        public (CronogramaPagoDetalleDTO, ExcepcionRegistroDTO) ListaMatriculaPagoAlumnoMatricula(int idMatriculaCabecera)
        {
            var excepcionRegistroDTO = new ExcepcionRegistroDTO();
            var cronogramaPagoDetalleDTO = new CronogramaPagoDetalleDTO();

            try
            {
                MatriculaCabeceraDetallesDTO matriculaCabeceraDetalles = _unitOfWork.MatriculaCabeceraRepository.ObtenerDetallesPorId(idMatriculaCabecera);


                if (matriculaCabeceraDetalles != null && matriculaCabeceraDetalles.IdMatriculaCabecera > 0)
                {
                    var listaCuotaPagos = _unitOfWork.MatriculaCabeceraRepository.ObtenerCuotasMatriculaoPorId(idMatriculaCabecera).ToList();
                    var resumenCronogramaPago = _unitOfWork.MatriculaCabeceraRepository.ObtenerResumenCronogramaCuotaPorId(idMatriculaCabecera);

                    if (listaCuotaPagos != null && listaCuotaPagos.Count > 0)
                    {
                        var lista = (from dt in listaCuotaPagos
                                     where dt.IdMatriculaCabecera == matriculaCabeceraDetalles.IdMatriculaCabecera
                                     orderby dt.NroCuota ascending
                                     group new { dt } by new { dt.IdMatriculaCabecera } into g
                                     select new CronogramaPagoDetalleDTO
                                     {
                                         IdMatriculaCabecera = g.Key.IdMatriculaCabecera,
                                         CodigoMatricula = matriculaCabeceraDetalles.CodigoMatricula,
                                         IdAlumno = matriculaCabeceraDetalles.IdAlumno,
                                         PGeneral = matriculaCabeceraDetalles.PGeneral,
                                         IdBusqueda = matriculaCabeceraDetalles.IdBusqueda,
                                         IdPGeneral = matriculaCabeceraDetalles.IdPGeneral,
                                         IdPEspecifico = matriculaCabeceraDetalles.IdPEspecifico,
                                         CuotasPagadas = resumenCronogramaPago.CuotasPagadas,
                                         CuotasPendientes = resumenCronogramaPago.CuotasPendientes,
                                         FechaVencimiento = resumenCronogramaPago.FechaVencimiento,
                                         RegistroCuota = g.Select(x => new ListaCuotaPagoDTO
                                         {
                                             IdCuota = x.dt.IdCuota,
                                             NroCuota = x.dt.NroCuota,
                                             Cuota = x.dt.Cuota,
                                             Mora = x.dt.Mora,
                                             MontoPagado = x.dt.MontoPagado,
                                             TipoCuota = x.dt.TipoCuota,
                                             FechaVencimiento = x.dt.FechaVencimiento,
                                             FechaPago = x.dt.FechaPago,
                                             Cancelado = x.dt.Cancelado,
                                             Moneda = x.dt.Moneda,
                                             WebMoneda = x.dt.WebMoneda,
                                             IdMatriculaCabecera = x.dt.IdMatriculaCabecera,
                                             Simbolo = x.dt.Simbolo,
                                             NombreMoneda = x.dt.NombreMoneda,
                                             MoraCalculada = x.dt.MoraCalculada,
                                             Version = x.dt.Version
                                         }).ToList()
                                     }).FirstOrDefault();

                        cronogramaPagoDetalleDTO = lista;
                        excepcionRegistroDTO.ExcepcionGenerada = false;
                        excepcionRegistroDTO.DescripcionGeneral = "";
                    }
                    else
                    {
                        excepcionRegistroDTO.ExcepcionGenerada = true;
                        excepcionRegistroDTO.DescripcionGeneral = "No se tiene cuotas asignadas ni matriculas registradas.";
                    }
                }
                else
                {
                    excepcionRegistroDTO.ExcepcionGenerada = true;
                    excepcionRegistroDTO.DescripcionGeneral = "No se tiene matriculas registradas.";
                }
            }
            catch (Exception ex)
            {
                excepcionRegistroDTO.ExcepcionGenerada = true;
                excepcionRegistroDTO.DescripcionGeneral = "Excepcion generada al procesar la consulta, para mas informacion ver el estado de Error";
                excepcionRegistroDTO.Error = new ExcepcionErrorDTO();
                excepcionRegistroDTO.Error.Message = ex.Message;
                excepcionRegistroDTO.Error.Source = ex.Source;
                if (ex.InnerException != null)
                {
                    excepcionRegistroDTO.Error.InnerException = ex.InnerException.ToString();
                }
                excepcionRegistroDTO.Error.Descripcion = ex.ToString();
            }

            return (cronogramaPagoDetalleDTO, excepcionRegistroDTO);
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 06/07/2023
        /// Version: 1.0
        /// <summary>
        /// realiza la validacion del medio de pago y tarjeta
        /// </summary>
        /// <param name="registroPreProcesoPagoDTO"> informacion de la matricula y detalles del pago </param>    
        /// <returns> RespuestaRegistroPreProcesoPagoDTO </returns>
        public RespuestaRegistroPreProcesoPagoDTO PreProcesoPagoCuotaAlumno(RegistroPreProcesoPagoDTO registroPreProcesoPagoDTO, RegistroTokenDTO registroToken)
        {
            var pasarelaPagoPw = _unitOfWork.PasarelaPagoPwRepository.ObtenerPorId(registroPreProcesoPagoDTO.IdPasarelaPago);
            RespuestaRegistroPreProcesoPagoDTO _Repuesta = new RespuestaRegistroPreProcesoPagoDTO();

            switch (pasarelaPagoPw.IdPais)
            {
                case 51:
                case 0:
                    switch (registroPreProcesoPagoDTO.IdPasarelaPago)
                    {

                        case 13:
                            // _Repuesta = _IPasarelaPagoPeruIzipayService.ProcesoPreValidacionIzipay(registroPreProcesoPagoDTO, _respuestaCorrecta.RegistroClaimToken.IdAlumno, _respuestaCorrecta.RegistroClaimToken.IdUsuario);
                            break;
                        default:
                            switch (registroPreProcesoPagoDTO.MedioPago.ToLower())
                            {
                                case "visa":
                                    //       _Repuesta = _IPasarelaPeruVisaService.ProcesoPreValidacionVisa(registroPreProcesoPagoDTO, _respuestaCorrecta.RegistroClaimToken.IdAlumno, _respuestaCorrecta.RegistroClaimToken.IdUsuario);
                                    break;
                                case "mastercard":
                                case "amex":
                                case "dinerclub":
                                    _Repuesta = ProcesoPreValidacionMasterCard(registroPreProcesoPagoDTO, registroToken);
                                    break;
                                default:
                                    break;
                            }
                            break;
                    }
                    break;


                default:
                    break;
            }
            return _Repuesta;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/07/2023
        /// Version: 1.0
        /// <summary>
        /// genera un registro en la tabla integraDB_PortalWeb.dbo.T_TransaccionAuditoriaPago para generar el proceso de pre-pago
        /// </summary>
        /// <param name="idUsuario"> informacion de la matricula y detalles del pago </param>    
        /// <param name="ProcesoPago"> informacion de la matricula y detalles del pago </param>    
        /// <returns> bool </returns>
        private RespuestaRegistroPreProcesoPagoDTO ProcesoPreValidacionMasterCard(RegistroPreProcesoPagoDTO ProcesoPago, RegistroTokenDTO registroToken)
        {
            RespuestaRegistroPreProcesoPagoDTO _objetoRespuesta = new RespuestaRegistroPreProcesoPagoDTO();
            var integraAspNetUser = _unitOfWork.IntegraAspNetUserRepository.ObtenerPorIdPersonal(registroToken.RegistroClaimToken.IdPersonal);
            try
            {
                decimal MontoTotal = 0;
                string Monto = string.Empty, Referencia = string.Empty, Correo = string.Empty;

                if (ProcesoPago.WebMoneda == "1")//Si es dolares
                {
                    _merchantId = _merchantIdDolares;
                    _tiendaId = _tiendaIdDolares;
                    _moneda = "dolares";
                    _monedaIso = "USD";
                }
                else
                {
                    _merchantId = _merchantIdSoles;
                    _tiendaId = _tiendaIdSoles;
                    _moneda = "soles";
                    _monedaIso = "PEN";
                }

                string _Referencia = obtenerNumeroOrden("MASTERCARD");

                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(ProcesoPago.IdAlumno);

                ProcesoPago.RegistroAlumno = new RegistroPreDatoAlumnoDTO();
                ProcesoPago.RegistroAlumno.IdAlumno = ProcesoPago.IdAlumno;
                ProcesoPago.RegistroAlumno.Nombre = alumno.Nombre1 + " " + alumno.Nombre2;
                ProcesoPago.RegistroAlumno.Apellido = alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno;
                ProcesoPago.RegistroAlumno.Ciudad = alumno.NombreCiudad;
                ProcesoPago.RegistroAlumno.Direccion = alumno.Direccion;
                ProcesoPago.RegistroAlumno.NumeroDocumento = alumno.Dni;
                ProcesoPago.RegistroAlumno.Correo = alumno.Email1;
                ProcesoPago.RegistroAlumno.NumeroCelular = alumno.Celular;

                if (ProcesoPago.MedioCodigo == "AMX")
                {
                    ProcesoPago.MedioCodigo = "AE";
                }
                else if (ProcesoPago.MedioCodigo == "DC")
                {
                    ProcesoPago.MedioCodigo = "DN";
                }

                foreach (var itemPago in ProcesoPago.ListaCuota)
                {
                    MontoTotal = MontoTotal + itemPago.CuotaTotal;
                }
                Monto = MontoTotal.ToString("0.00");
                Monto = Monto.Replace(",", ".");

                var serializerProceso = new JavaScriptSerializer();
                var objetoPagoSerealizado = serializerProceso.Serialize(ProcesoPago);


                RegistroProcesoPagoDTO _ProcesarRegistro = new RegistroProcesoPagoDTO();
                _ProcesarRegistro.IdUsuario = integraAspNetUser.Id;
                _ProcesarRegistro.IdAlumno = ProcesoPago.IdAlumno;
                _ProcesarRegistro.IdMatriculaCabecera = ProcesoPago.IdMatriculaCabecera;
                _ProcesarRegistro.IdentificadorTransaccion = Guid.NewGuid();
                _ProcesarRegistro.IdFormaPago = ProcesoPago.IdFormaPago;
                _ProcesarRegistro.NumeroPedidoOrden = "";
                _ProcesarRegistro.TokenComercio = "";
                _ProcesarRegistro.NombreServicio = "MasterCard";
                _ProcesarRegistro.EstadoOperacion = "Sent";
                _ProcesarRegistro.TipoPago = "Cronograma";
                _ProcesarRegistro.CodigoComercio = _merchantId;
                _ProcesarRegistro.RegistroEnvioComercio = "";
                _ProcesarRegistro.RegistroRespuestaComercio = "";
                _ProcesarRegistro.RegistroTransaccionJson = objetoPagoSerealizado;
                _ProcesarRegistro.MontoTotal = Convert.ToDecimal(MontoTotal);
                _ProcesarRegistro.Correo = alumno.Email1;
                _ProcesarRegistro.NumeroPedidoComercio = _Referencia;
                _ProcesarRegistro.RequiereDatoTarjeta = true;

                var IdRegistro = _unitOfWork.PortalWebRepository.RegistrarTransaccionAuditoriaPago(_ProcesarRegistro);

                // _objetoRespuesta.ProcesoPagoBotonVisa = new procesoPagoBotonDTO();
                _objetoRespuesta.IdMatriculaCabecera = ProcesoPago.IdMatriculaCabecera;
                _objetoRespuesta.IdentificadorTransaccion = _ProcesarRegistro.IdentificadorTransaccion;
                _objetoRespuesta.MedioCodigo = ProcesoPago.MedioCodigo;
                _objetoRespuesta.RequiereDatosTarjeta = true;
                _objetoRespuesta.IdTransaccionAuditoriaPago = IdRegistro;
            }
            catch (Exception)
            {
                throw;

            }
            return _objetoRespuesta;
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 08/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Contador actual del serivicio
        /// </summary>
        /// <param name="NombreServicio"> Nombre servicio (Visa, Mastercard, etc) </param>
        /// <returns> bool </returns>
        private string obtenerNumeroOrden(string NombreServicio)
        {
            long ContadorActual = 0;
            var rptNumero = _unitOfWork.PortalWebRepository.buscarNumeroOrden(NombreServicio);

            if (rptNumero != null && rptNumero.NombreServicio != null)
            {
                ContadorActual = rptNumero.ContadorActual;
                rptNumero.ContadorActual = rptNumero.ContadorActual + 1;
                _unitOfWork.PortalWebRepository.actualizarNumeroOrden(rptNumero);
            }
            else
            {
                ContadorActual = 1;
                NumeroOrdenDTO nroContador = new NumeroOrdenDTO();
                nroContador.ContadorActual = ContadorActual;
                nroContador.NombreServicio = NombreServicio;
                _unitOfWork.PortalWebRepository.registrarNumeroOrden(nroContador);

            }
            return ContadorActual.ToString().PadLeft(9, '0'); ;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 03/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Información de los medios de pagos
        //  Solo retornamos para pagos por mastercard: Comercio electrónico Mastercard (Perú) y Comercio electrónico PayU (Colombia)
        /// </summary>
        /// <returns> ListaAlumnoDTO </returns>
        public List<MedioPagoActivoPasarelaDTO> ObtenerMedioPagoPasarelaPorMatricula(int idMatriculaCabecera)
        {
            var respuesta = new List<MedioPagoActivoPasarelaDTO>();
            int comercioElectronicoMCPeru = 48;
            int comercioElectronicoPayUColombia = 64;
            List<int> masterCardPeruColombia = new List<int> { comercioElectronicoMCPeru, comercioElectronicoPayUColombia };
            var medioPago = _unitOfWork.MatriculaCabeceraRepository.ObtenerRegistroMedioPagoPorId(idMatriculaCabecera);
            if (medioPago != null && medioPago.Valor != 0)
            {
                var medioPagoActivoPasarelas = _unitOfWork.MatriculaCabeceraRepository.ObtenerListaMedioPagoMatriculaCronograma(true, idMatriculaCabecera, medioPago.Valor!.Value).ToList();

                if (medioPagoActivoPasarelas != null && medioPagoActivoPasarelas.Count > 0)
                    respuesta = medioPagoActivoPasarelas;
                else
                    respuesta = _unitOfWork.MatriculaCabeceraRepository.ObtenerListaMedioPagoMatriculaCronograma(false, idMatriculaCabecera, 0).ToList();
            }
            else
            {
                respuesta = _unitOfWork.MatriculaCabeceraRepository.ObtenerListaMedioPagoMatriculaCronograma(false, idMatriculaCabecera, 0).ToList();
            }
            /*Solo retornamos para pagos por mastercard: Comercio electrónico Mastercard (Perú) y Comercio electrónico PayU (Colombia)*/
            return respuesta.Where(x => masterCardPeruColombia.Contains(x.IdFormaPago)).ToList();
        }
        /// Autor: Gilmer Qm
        /// Fecha: 04/08/2023
        /// Version: 1.0
        /// <param name="idTransaccionAuditoriaPago"> (PK) de [integraDB_PortalWeb].[dbo].[T_TransaccionAuditoriaPago] </param>
        /// <summary>
        /// Obtiene el registro de la transaccion generada para el pago
        /// </summary> 
        /// <returns>   </returns>
        public TransaccionAuditoriaPagoRespuestaDTO ObtenerTransactionPorCelular(string numeroCelular)
        {
            var resultado = _unitOfWork.PortalWebRepository.ObtenerTransactionPorCelular(numeroCelular);
            return resultado;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/08/2023
        /// <summary>
        /// Usando el Algoritmo de Luhn valida si el número de tarjeta ingresado es valida 
        /// </summary> 
        /// <param name="numeroTarjeta"> numero de tarjeta </param>
        /// <returns> bool </returns> 
        public bool ValidarNumeroTarjeta(string numeroTarjeta)
        {
            int suma = 0;
            int digito = 0;
            int sumando = 0;
            bool digitoPar = false;
            StringBuilder digitsOnly = new StringBuilder();
            foreach (char c in numeroTarjeta.Where(c => char.IsDigit(c)))
            {
                digitsOnly.Append(c);
            }

            for (int i = digitsOnly.Length - 1; i >= 0; i--)
            {
                digito = Int32.Parse(digitsOnly.ToString(i, 1));
                if (digitoPar)
                {
                    sumando = digito * 2;
                    if (sumando > 9)
                        sumando -= 9;
                }
                else
                    sumando = digito;

                suma += sumando;

                digitoPar = !digitoPar;

            }
            return (suma % 10) == 0;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 28/08/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza un insercion basica a la tabla ProcesoPagoIvr
        /// </summary> 
        /// <param name="procesoPagoIvr"> (PK) de T_MatriculaCabecera </param>  
        /// <returns> RegistroTransactionDTO </returns>
        public bool InsertarProcesoPagoIvr(ProcesoPagoIvrDTO procesoPagoIvr, string usuario)
        {
            try
            {
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(procesoPagoIvr.IdPersonal.Value);
                var nuevaEntidad = _mapper.Map<ProcesoPagoIvr>(procesoPagoIvr);
                nuevaEntidad.Anexo = personal.Anexo;
                nuevaEntidad.Estado = true;
                nuevaEntidad.FechaCreacion = DateTime.Now;
                nuevaEntidad.FechaModificacion = DateTime.Now;
                nuevaEntidad.UsuarioCreacion = usuario;
                nuevaEntidad.UsuarioModificacion = usuario;
                var respuesta = _unitOfWork.ProcesoPagoIvrRepository.Add(nuevaEntidad);
                _unitOfWork.Commit();
                if (respuesta.Id > 0)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw new BadImageFormatException($"Error en InsertarProcesoPagoIvr {ex.Message}");
            }
        }
    }
}
