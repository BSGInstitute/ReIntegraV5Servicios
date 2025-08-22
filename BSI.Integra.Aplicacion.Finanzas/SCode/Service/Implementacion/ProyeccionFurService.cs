using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ProyeccionFurService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de ProyeccionFur
    /// </summary>
    public class ProyeccionFurService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        
        public ProyeccionFurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<TProyeccionFur, ProyeccionFur>(MemberList.None).ReverseMap();
            //    cfg.CreateMap<ProyeccionFurFrontDTO, ProyeccionFur>(MemberList.None).ReverseMap();
            //});
            //_mapper = new Mapper(config);
        }

 
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Proyecta FURS para costos Fijos
        /// </summary>
        /// <returns> List<ProyeccionFurDTO> </returns>
        public object ProyectarFurCostosFijos(ProyeccionFurDTO data,string Usuario)
        {
            try
            {
                LogProyectadosDTO LogProyeccionInsert = new LogProyectadosDTO();
                List<ErrorFurDTO> detalleLogError = new List<ErrorFurDTO>();
                List<FurParaProyeccionDTO> ListaFurInsertar = new List<FurParaProyeccionDTO>();

                var serializerProceso = new JavaScriptSerializer();

                var serLog = new LogProyeccionFurService(_unitOfWork);

                var repAreaTrabajo = _unitOfWork.PersonalAreaTrabajoRepository;
                var repFurConfiguracionAutomatica = _unitOfWork.FurConfiguracionAutomaticaRepository;
                var repConfiguracionProyeccion = _unitOfWork.ConfiguracionProyeccionFurRepository;
                var repPeriodoProyeccion = _unitOfWork.PeriodoMesProyeccionRepository;
                var repCabeceraProyeccion = _unitOfWork.CabeceraFurConfiguracionAutomaticaRepository;

                var detalleProyeccion = repFurConfiguracionAutomatica.ObtenerFurConfiguracionAutomaticaByIdAreaActivo(data.IdAreas);
                var cabeceraProyeccion = repCabeceraProyeccion.ObtenerCabeceraFurConfiguracionAutomaticaEnRevisionByIdAreas(data.IdAreas);
                var configuracionProyeccion = repConfiguracionProyeccion.ObtenerConfiguracionProyeccionFurById(data.IdConfiguracionProyeccion);
                if (configuracionProyeccion==null) throw new Exception("No se encontraron datos de la configuración, solicita al encargado de Proyección Furs que active o cree una configuración!.");

                var periodoProyeccion = repPeriodoProyeccion.ObtenerPeriodoMesProyeccionById(configuracionProyeccion.IdPeriodoProyeccion);
                var contador = 0;
                var correctos = 0;
                var error = 0;
                if (detalleProyeccion != null && detalleProyeccion.Count() > 0)
                {
                    foreach (var item in detalleProyeccion)
                    {
                        for (int i = 0; i < periodoProyeccion.Cantidad; i++)
                        {
                            contador++;
                            var dataFur = obtenerFurInsertarAlterno(item, configuracionProyeccion.FechaSemilla, Usuario, i);
                            if(dataFur.error == true)
                            {
                                error++;
                                if(!detalleLogError.Exists(e=> e.Configuracion.Id == item.Id)) 
                                    detalleLogError.Add(new ErrorFurDTO { Configuracion=item, MensajeError= dataFur.mensajeError });
                            }
                            else if(dataFur.existe == true)
                            {
                                correctos++;
                                ListaFurInsertar.Add(dataFur.fur);
                            }
                        }
                        var index = cabeceraProyeccion.FindIndex(e => e.IdArea == item.IdPersonalAreaTrabajo);
                        if(index != -1) cabeceraProyeccion[index].DetalleFurConfiguracionAutomatica.Add(item);

                    }
                    LogProyeccionInsert.TipoProyectado = "Costos Fijos";
                    LogProyeccionInsert.TotalProcesado = contador;
                    LogProyeccionInsert.TotalProyectados = correctos;
                    LogProyeccionInsert.FechaSemilla = configuracionProyeccion.FechaSemilla;
                    LogProyeccionInsert.FechaInicioProyeccion = configuracionProyeccion.FechaSemilla.AddMonths(1);
                    LogProyeccionInsert.FechaFinProyeccion = configuracionProyeccion.FechaSemilla.AddMonths(periodoProyeccion.Cantidad);
                    LogProyeccionInsert.TotalErrores = error;
                    LogProyeccionInsert.ConfiguracionesConError = serializerProceso.Serialize(detalleLogError);

                    serLog.Add(LogProyeccionInsert,Usuario);
                    var respuesta  = repFurConfiguracionAutomatica.InsertarFursParaProyeccionCostosFijos(serializerProceso.Serialize(ListaFurInsertar));
                    if(respuesta==true)
                    {
                        List<DetalleProyeccionJsonFur> jsonEnvio = new List<DetalleProyeccionJsonFur>();
                        foreach (var item in cabeceraProyeccion)
                        {
                            jsonEnvio.Add(new DetalleProyeccionJsonFur
                            {
                                IdCabeceraConfiguracion= item.IdCabeceraConfiguracion,
                                IdArea = item.IdArea,
                                DetalleFurConfiguracionAutomatica = serializerProceso.Serialize(item.DetalleFurConfiguracionAutomatica)
                            });
                        };
                        var jsonString = serializerProceso.Serialize(jsonEnvio);
                        var stringConf = serializerProceso.Serialize(configuracionProyeccion);
                        var congelar = repCabeceraProyeccion.CongelarProyeccionYCambiarEstadoProyeccionFur(jsonString, stringConf,Usuario);
                }
                    else throw new Exception("Ocurrio un error al intertar guardar la proyeccion!.");
                }
                else throw new Exception("No se encontraron datos para proyectar, revisa que las solicitudes tengan almenos un detalle!.");

                return new {TotalProcesados= contador , FurProyectado=correctos, FurError = error };

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Prepara la Data para insertar 
        /// </summary>
        /// <returns> List<ProyeccionFurDTO> </returns>
        private dynamic obtenerFurInsertarAlterno(FurConfiguracionAutomaticaVersionDetalleDTO furConfiguracionAutomatica, DateTime FechaSemilla,string Usuario, int mes)
        {
            var estado = false;
            try
            {
                FurParaProyeccionDTO dataFur = new FurParaProyeccionDTO();
                var _repHistoricoProductoProveedor = _unitOfWork.HistoricoProductoProveedorRepository;
                
                var fechaActualOriginal = DateTime.Now.AddMonths(mes+1);
                var fechaActual = new DateTime(fechaActualOriginal.Year,fechaActualOriginal.Month, fechaActualOriginal.Day, 0, 0, 0);
                if (fechaActual >= furConfiguracionAutomatica.FechaInicioConfiguracion && fechaActual <= furConfiguracionAutomatica.FechaFinConfiguracion)
                {
                    var detalleFur = _repHistoricoProductoProveedor.ObtenerDetalleHistoricoProyeccionById(furConfiguracionAutomatica.IdHistoricoProductoProveedor);
                    int diaMes = 0;
                    if (furConfiguracionAutomatica.IdFrecuencia == 3)// Mensual
                    {
                        diaMes = FechaSemilla.Day;
                        int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
                        if (diaMes > numeroDias)
                        {
                            diaMes = numeroDias;
                        }
                        if (fechaActual.Day == diaMes)
                        {
                            DateTime fechaFur = fechaActual;

                            fechaFur = fechaFur.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana);

                            int semana = obtenerNumeroSemana(fechaFur);
                        
                            dataFur.IdEmpresa = furConfiguracionAutomatica.IdEmpresa;
                            dataFur.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
                            dataFur.IdCiudad = furConfiguracionAutomatica.IdSede;
                            dataFur.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
                            dataFur.NumeroSemana = semana;
                            dataFur.UsuarioSolicitud = furConfiguracionAutomatica.UsuarioSolicitud;
                            dataFur.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
                            dataFur.NumeroCuenta = detalleFur.NumeroCuenta;
                            dataFur.Cuenta = detalleFur.CuentaDescripcion;
                            dataFur.IdProveedor = detalleFur.IdProveedor;
                            dataFur.IdProducto = detalleFur.IdProducto;
                            dataFur.Cantidad = furConfiguracionAutomatica.Cantidad;
                            dataFur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                            dataFur.Descripcion = furConfiguracionAutomatica.Descripcion;
                            dataFur.FechaLimite = (
                                string.Format("{0000}", fechaFur.Year) + '-' 
                                +string.Format("{00}", fechaFur.Day) + '-' 
                                +string.Format("{00}", fechaFur.Month)+ " 00:00:00.000"); 
                            dataFur.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
                            dataFur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                            dataFur.IdMonedaProveedor = detalleFur.IdMoneda;
                            dataFur.IdFurFaseAprobacion1 = 1;
                            dataFur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
                            dataFur.MontoProyectado = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.Monto = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
                            dataFur.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
                            dataFur.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
                            dataFur.Cancelado = false;
                            dataFur.Antiguo = 0;
                            dataFur.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
                            dataFur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                            dataFur.EstadoAprobadoObservado = false;
                            dataFur.OcupadoSolicitud = false;
                            dataFur.OcupadoRendicion = false;
                            dataFur.Estado = true;
                            dataFur.UsuarioCreacion = "PROYECCION - "+ Usuario;
                            dataFur.UsuarioModificacion = "PROYECCION - " + Usuario;
                            dataFur.FechaCreacion = DateTime.Now;
                            dataFur.FechaModificacion = DateTime.Now;
                            estado = true;
                        }
                    }
                    else if (furConfiguracionAutomatica.IdFrecuencia == 8)//Bimestral
                    {
                        var mesGeneracion = FechaSemilla.Month % 2;

                        var mesActualGenaracion = fechaActual.Month % 2;
                        int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
                        var dia = fechaActual.Day;
                        if (dia > numeroDias)
                        {
                            dia = numeroDias;
                }
                        if (mesGeneracion == mesActualGenaracion && fechaActual.Day == dia)
                        {
                            DateTime fechaFur = fechaActual;

                            fechaFur = fechaFur.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana);

                            int semana = obtenerNumeroSemana(fechaFur);

                            dataFur.IdEmpresa = furConfiguracionAutomatica.IdEmpresa;
                            dataFur.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
                            dataFur.IdCiudad = furConfiguracionAutomatica.IdSede;
                            dataFur.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
                            dataFur.NumeroSemana = semana;
                            dataFur.UsuarioSolicitud = furConfiguracionAutomatica.UsuarioSolicitud;
                            dataFur.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
                            dataFur.NumeroCuenta = detalleFur.NumeroCuenta;
                            dataFur.Cuenta = detalleFur.CuentaDescripcion;
                            dataFur.IdProveedor = detalleFur.IdProveedor;
                            dataFur.IdProducto = detalleFur.IdProducto;
                            dataFur.Cantidad = furConfiguracionAutomatica.Cantidad;
                            dataFur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                            dataFur.Descripcion = furConfiguracionAutomatica.Descripcion;
                            dataFur.FechaLimite = (
                                string.Format("{0000}", fechaFur.Year) + '-'
                                + string.Format("{00}", fechaFur.Day) + '-'
                                + string.Format("{00}", fechaFur.Month) + " 00:00:00.000");
                            dataFur.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
                            dataFur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                            dataFur.IdMonedaProveedor = detalleFur.IdMoneda;
                            dataFur.IdFurFaseAprobacion1 = 1;
                            dataFur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
                            dataFur.MontoProyectado = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.Monto = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
                            dataFur.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
                            dataFur.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
                            dataFur.Cancelado = false;
                            dataFur.Antiguo = 0;
                            dataFur.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
                            dataFur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                            dataFur.EstadoAprobadoObservado = false;
                            dataFur.OcupadoSolicitud = false;
                            dataFur.OcupadoRendicion = false;
                            dataFur.Estado = true;
                            dataFur.UsuarioCreacion = "PROYECCION - " + Usuario;
                            dataFur.UsuarioModificacion = "PROYECCION - " + Usuario;
                            dataFur.FechaCreacion = DateTime.Now;
                            dataFur.FechaModificacion = DateTime.Now;
                            estado = true;
                        }
                    }
                    else if (furConfiguracionAutomatica.IdFrecuencia == 5)//Trimestral
                    {
                        var mesGeneracion = FechaSemilla.Month % 3;

                        var mesActualGenaracion = fechaActual.Month % 3;
                        int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
                        var dia = fechaActual.Day;
                        if (dia > numeroDias)
                        {
                            dia = numeroDias;
                        }
                        if (mesGeneracion == mesActualGenaracion && fechaActual.Day == dia)
                        {
                            DateTime fechaFur = fechaActual;

                            fechaFur = fechaFur.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana);

                            int semana = obtenerNumeroSemana(fechaFur);

                            dataFur.IdEmpresa = furConfiguracionAutomatica.IdEmpresa;
                            dataFur.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
                            dataFur.IdCiudad = furConfiguracionAutomatica.IdSede;
                            dataFur.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
                            dataFur.NumeroSemana = semana;
                            dataFur.UsuarioSolicitud = furConfiguracionAutomatica.UsuarioSolicitud;
                            dataFur.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
                            dataFur.NumeroCuenta = detalleFur.NumeroCuenta;
                            dataFur.Cuenta = detalleFur.CuentaDescripcion;
                            dataFur.IdProveedor = detalleFur.IdProveedor;
                            dataFur.IdProducto = detalleFur.IdProducto;
                            dataFur.Cantidad = furConfiguracionAutomatica.Cantidad;
                            dataFur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                            dataFur.Descripcion = furConfiguracionAutomatica.Descripcion;
                            dataFur.FechaLimite = (
                                string.Format("{0000}", fechaFur.Year) + '-'
                                + string.Format("{00}", fechaFur.Day) + '-'
                                + string.Format("{00}", fechaFur.Month) + " 00:00:00.000");
                            dataFur.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
                            dataFur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                            dataFur.IdMonedaProveedor = detalleFur.IdMoneda;
                            dataFur.IdFurFaseAprobacion1 = 1;//Proyectado
                            dataFur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
                            dataFur.MontoProyectado = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.Monto = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
                            dataFur.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
                            dataFur.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
                            dataFur.Cancelado = false;
                            dataFur.Antiguo = 0;
                            dataFur.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
                            dataFur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                            dataFur.EstadoAprobadoObservado = false;
                            dataFur.OcupadoSolicitud = false;
                            dataFur.OcupadoRendicion = false;
                            dataFur.Estado = true;
                            dataFur.UsuarioCreacion = "PROYECCION - " + Usuario;
                            dataFur.UsuarioModificacion = "PROYECCION - " + Usuario;
                            dataFur.FechaCreacion = DateTime.Now;
                            dataFur.FechaModificacion = DateTime.Now;
                            estado = true;
                        }
                    }
                    else if (furConfiguracionAutomatica.IdFrecuencia == 6)//Semestral
                    {
                        var mesGeneracion = FechaSemilla.Month % 6;

                        var mesActualGenaracion = fechaActual.Month % 6;
                        int numeroDias = DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month);
                        var dia = fechaActual.Day;
                        if (dia > numeroDias)
                        {
                            dia = numeroDias;
                        }
                        if (mesGeneracion == mesActualGenaracion && fechaActual.Day == dia)
                        {
                            DateTime fechaFur = fechaActual;

                            fechaFur = fechaFur.AddDays(furConfiguracionAutomatica.AjusteNumeroSemana);

                            int semana = obtenerNumeroSemana(fechaFur);

                            dataFur.IdEmpresa = furConfiguracionAutomatica.IdEmpresa;
                            dataFur.IdPersonalAreaTrabajo = furConfiguracionAutomatica.IdPersonalAreaTrabajo;
                            dataFur.IdCiudad = furConfiguracionAutomatica.IdSede;
                            dataFur.IdFurTipoPedido = furConfiguracionAutomatica.IdFurTipoPedido;
                            dataFur.NumeroSemana = semana;
                            dataFur.UsuarioSolicitud = furConfiguracionAutomatica.UsuarioSolicitud;
                            dataFur.IdCentroCosto = furConfiguracionAutomatica.IdCentroCosto;
                            dataFur.NumeroCuenta = detalleFur.NumeroCuenta;
                            dataFur.Cuenta = detalleFur.CuentaDescripcion;
                            dataFur.IdProveedor = detalleFur.IdProveedor;
                            dataFur.IdProducto = detalleFur.IdProducto;
                            dataFur.Cantidad = furConfiguracionAutomatica.Cantidad;
                            dataFur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                            dataFur.Descripcion = furConfiguracionAutomatica.Descripcion;
                            dataFur.FechaLimite =  (
                                string.Format("{0000}", fechaFur.Year) + '-' 
                                +string.Format("{00}", fechaFur.Day) + '-' 
                                +string.Format("{00}", fechaFur.Month)+ " 00:00:00.000"); 
                            dataFur.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
                            dataFur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                            dataFur.IdMonedaProveedor = detalleFur.IdMoneda;
                            dataFur.IdFurFaseAprobacion1 = 1;
                            dataFur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad);
                            dataFur.MontoProyectado = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.Monto = dataFur.PrecioTotalMonedaOrigen;
                            dataFur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad);
                            dataFur.PagoMonedaOrigen = detalleFur.PrecioOrigen * furConfiguracionAutomatica.Cantidad;
                            dataFur.PagoDolares = detalleFur.PrecioDolares * furConfiguracionAutomatica.Cantidad;
                            dataFur.Cancelado = false;
                            dataFur.Antiguo = 0;
                            dataFur.IdMonedaPagoReal = furConfiguracionAutomatica.IdMonedaPagoReal;
                            dataFur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                            dataFur.EstadoAprobadoObservado = false;
                            dataFur.OcupadoSolicitud = false;
                            dataFur.OcupadoRendicion = false;
                            dataFur.Estado = true;
                            dataFur.UsuarioCreacion = "PROYECCION - " + Usuario;
                            dataFur.UsuarioModificacion = "PROYECCION - " + Usuario;
                            dataFur.FechaCreacion = DateTime.Now;
                            dataFur.FechaModificacion = DateTime.Now;
                            estado = true;
                        }
                    }
                }
                return new {existe=estado,error=false,fur=dataFur};
            }
            catch (Exception e)
            {
                return new {existe=false, error=true, mensajeError = e.Message };
            }
        }

        private int obtenerNumeroSemana(DateTime fecha)
        {
            DateTime primerDiaAnio = new DateTime(fecha.Year, 1, 1);
            TimeSpan diferencia = fecha - primerDiaAnio;
            int numeroSemana = (int)Math.Ceiling(diferencia.TotalDays / 7);

            return numeroSemana;
        }
    }

}
