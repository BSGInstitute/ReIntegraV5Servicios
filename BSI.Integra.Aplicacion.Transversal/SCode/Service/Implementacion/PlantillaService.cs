using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Ads.GoogleAds.V11.Services;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PlantillaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Plantilla
    /// </summary>
    public class PlantillaService : IPlantillaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantilla, Plantilla>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Plantilla segun su Id
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaDTO </returns>
        public Plantilla ObtenerPorId(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la plantilla inicial para envio de correo masivo por operaciones
        /// </summary>
        /// <param name="idPlantilla">Id de Plantilla</param>
        /// <returns> PlantillaBaseCorreoOperacionesDTO </returns>
        public PlantillaAsuntoCuerpoDTO ObtenerPlantillaCorreo(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de la Etiqueta Monto Pago para las Plantillas.
        /// </summary>
        /// <param name="argumentos">Argumentos requeridos para las funciones internas</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerEtiquetaMontoPago(EtiquetaMontoPagoArgumentosDTO argumentos)
        {
            string? valorEtiqueta = null;
            if (argumentos.IdOportunidad != null)
            {
                if (argumentos.IdPGeneral != null && argumentos.IdCentroCosto != null && argumentos.IdCodigoPais != null)
                {
                    valorEtiqueta = ObtenerEtiquetaMontoPagoPorPGeneral(argumentos).Valor;
                }
                if (valorEtiqueta == null || valorEtiqueta == "")
                {
                    valorEtiqueta = ObtenerEtiquetaMontoPagoPorOportunidad(argumentos).Valor;
                }
            }
            return new StringDTO() { Valor = valorEtiqueta };
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de la Etiqueta Monto Pago asociado a un Programa General.
        /// </summary>
        /// <param name="argumentos">Argumentos requeridos</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerEtiquetaMontoPagoPorPGeneral(EtiquetaMontoPagoArgumentosDTO argumentos)
        {
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var servicioPEspecifico = new PEspecificoService(_unitOfWork);
            var servicioBeneficioPGeneral = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);

            var versiones = servicioMontoPago.ObtenerVersionMontoPagoPorIdOportunidad(argumentos.IdOportunidad.Value);

            if (versiones.Count() == 0)
                return null;
            else
            {
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(argumentos.IdCentroCosto.Value);

                List<VersionProgramaDTO> versionesPrograma = _unitOfWork.VersionProgramaRepository.ObtenerVersionPrograma().ToList();
                List<string> listaBeneficios;

                string tabla = "";
                int contadorBeneficios = 0;

                tabla = "<table border cellpadding=2 cellspacing=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";

                foreach (VersionProgramaDTO item in versionesPrograma)
                {
                    listaBeneficios = servicioBeneficioPGeneral.ObtenerDescripcionPGeneralConfiguracionBeneficios(programaEspecifico.IdProgramaGeneral!.Value, argumentos.IdCodigoPais, item.Id);
                    contadorBeneficios += listaBeneficios.Count;

                    if (listaBeneficios.Count > 0)
                    {
                        List<MontoPagoVersionDTO> infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();

                        tabla += "<tr>";

                        tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count}'>{item.Nombre}</td>";

                        tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count}'><ul>";

                        foreach (string beneficio in listaBeneficios)
                        {
                            tabla += $"<li>{beneficio}</li>";
                        }

                        tabla += "</ul></td>";
                        int i = 0;

                        foreach (var re in infoVersiones)
                        {
                            tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                            tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString()
                            : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                            + "</td>";

                            i += 1;

                            if (i < infoVersiones.Count)
                                tabla += "</tr><tr>";
                        }

                        tabla += "</tr>";
                    }
                }

                tabla += "</table>";

                var pieBeneficio = _unitOfWork.PGeneralDocumentoPwRepository.ObtenerSeccionDocumentoPGeneral(argumentos.IdPGeneral.Value, "Beneficios");

                if (pieBeneficio != null)
                    tabla += $"<p>{pieBeneficio.PiePagina}</p>";

                if (contadorBeneficios == 0)
                    tabla = null;

                return new StringDTO() { Valor = tabla ?? "" };
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el valor de la Etiqueta Monto Pago asociado a una Oportunidad.
        /// </summary>
        /// <param name="argumentos">Argumentos requeridos</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerEtiquetaMontoPagoPorOportunidad(EtiquetaMontoPagoArgumentosDTO argumentos)
        {
            var servicioMontoPago = new MontoPagoService(_unitOfWork);


            var tabla = servicioMontoPago.ObtenerTablaHTMLVersionMontoPagoBeneficios(argumentos.IdOportunidad.Value).Valor;

            if (tabla == null)
            {
                string precio_normal = string.Empty;

                string precio_contado = getPrecioContado(argumentos.IdOportunidad.Value);
                string precio_coutas = getPrecioCuotas(argumentos.IdOportunidad.Value);

                if (!string.IsNullOrEmpty(precio_contado))
                {
                    precio_normal = "<b>Al Contado: </b>" + precio_contado;

                }

                if (!string.IsNullOrEmpty(precio_coutas))
                {
                    precio_normal += "<div style=' height:1px;'></div>" + "<b>Financiamiento: </b>" + precio_coutas;
                }

                return new StringDTO() { Valor = precio_normal };
            }
            else
            {
                return new StringDTO() { Valor = tabla };
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una Tabla Para el Precio al Contado de un Programa 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        public string getPrecioContado(int idOportunidad)
        {
            string returnn = "";
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var contado = servicioMontoPago.ObtenerMontoPagoContadoPorIdOportunidad(idOportunidad);

            if (contado != null)
            {
                returnn = GenerarGridCronogramaPago(contado).Valor;
            }

            return returnn;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        private string getPrecioCuotas(int idOportunidad)
        {
            string returnn = "";
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var montopagoCuotas = servicioMontoPago.ObtenerMontoPagoPorIdOportunidadParaTabla(idOportunidad);
            if (montopagoCuotas != null)
            {
                returnn = GenerarGridCronogramaPago(montopagoCuotas).Valor;
            }

            return returnn;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tabla de Pagos de acuerdo a Formula.
        /// </summary>
        /// <param name="data">Monto Pago</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO GenerarGridCronogramaPago(MontoPagoCompuestoDTO data)
        {
            StringDTO tablaRespuesta = new StringDTO();
            switch (data.tp_formula)
            {
                case 0://sin descuento                     
                    tablaRespuesta.Valor = GeneraHtmlPrecioCuotas(data).Valor;
                    break;
                case 1: //matricula
                    tablaRespuesta.Valor = GeneraHtml(_generarGridMatricula(data));
                    break;
                case 2: //cuotas
                    tablaRespuesta.Valor = GeneraHtmlPrecioCuotas(data).Valor;
                    break;
                case 3: //ambos
                    tablaRespuesta.Valor = GeneraHtml(_generarGridAmbos(data));
                    break;
                case 4: //general
                    tablaRespuesta.Valor = GeneraHtml(_generarGridGeneral(data));
                    break;
                case 5:
                    tablaRespuesta.Valor = GeneraHtmlPrecioContado(_generarGridNormal(data));
                    break;
            }
            return tablaRespuesta;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Html De Precio en Cuotas
        /// </summary>
        /// <param name="data">Monto Pago</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO GeneraHtmlPrecioCuotas(MontoPagoCompuestoDTO data)
        {
            string tabla = "";
            string moneda = "";

            var servicioMoneda = new MonedaService(_unitOfWork);
            var respuesta = servicioMoneda.ObtenerMonedaParaDocumento(data.mp_moneda);

            if (respuesta != null)
            {
                moneda = respuesta.Simbolo;
            }

            if (data != null && !string.IsNullOrEmpty(moneda))
            {
                tabla = "1 Matricula de " + moneda.Replace(".", " ") + " " + data.mp_matricula + " y " + data.mp_nro_cuotas + " cuotas de " + moneda + " " + data.mp_cuotas;
            }
            return new StringDTO() { Valor = tabla };
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Grid Matricula
        /// </summary>
        /// <param name="data">Monto Pago</param>
        /// <returns> ValorStringDTO </returns>
        private List<PagoCuotaDTO> _generarGridMatricula(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            DateTime fecha = new DateTime();
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;
            if (tamanioMatricula == 0) tamanioMatricula = 1;

            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Matricula " + (numeroco);
                obj.montoCuota = data.mp_matricula;
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }
            /////cuotas///////////////////////////////////////////////////
            var tamaniocuotas = 0;
            var tamanioContador = 0;
            var tamanio = data.mp_nro_cuotas;
            numeroco = numeroco - 1;
            tamaniocuotas = tamanio;
            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Cuota - " + (numeroco);
                obj.montoCuota = data.mp_cuotas;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                obj.ispagado = false;
                obj.es_matricula = false;
                fecha = _calcularFechaInicial(data, i);
                obj.fechapago = fecha;
                lista.Add(obj);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj1 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj1.numeroCuota = numeroco;
                        obj1.cuotaDescripcion = "Cuota - " + (numeroco);
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = _calcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el tipo de descuento.
        /// </summary>
        /// <param name="va">Valor</param>
        /// <param name="des">Valor</param>
        /// <returns> float </returns>
        private float _tipoDescuentoGeneral(double? va, int des)
        {
            float valor = float.Parse(va.ToString());
            float des2 = float.Parse(des.ToString());
            var d = float.Parse(Convert.ToString((valor * des2) / 100));
            return (valor - d);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula FechaInicial de la Primera Cuota
        /// </summary>
        /// <param name="obj"> Datos Compuestos de MontoPago</param>
        /// <param name="i"></param>
        /// <returns> DateTime </returns>
        private DateTime _calcularFechaInicial(MontoPagoCompuestoDTO obj, int i)
        {
            var myDate = new DateTime();
            myDate = DateTime.Now;
            // myDate = DateTime.Now;
            var mes = myDate.Month;
            int dia = 0;
            if (!string.IsNullOrEmpty(obj.mp_vencimiento))
            {
                dia = Int32.Parse(obj.mp_vencimiento);
            }
            else
            {
                myDate.AddDays(1);
            }
            if (obj.mp_primeraCuota != null)
            {
                DateTime fec_temp = new DateTime();
                fec_temp = DateTime.Now;
                myDate = ObtenerPrimeraFecha(obj.mp_primeraCuota, dia);
            }
            if (dia < 29)
            {
                myDate = myDate.AddDays(dia);
            }
            else
            {
                myDate = myDate.AddDays(28);
            }
            if (obj.mp_vencimiento != null)
            {
                mes = myDate.Month;
            }
            myDate = myDate.AddMonths(i);
            return myDate;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene primera Fecha
        /// </summary>
        /// <param name="MontName">Monto Pago primera Cuota</param>
        /// <param name="DiaInicio"></param>
        /// <returns> DateTime </returns>
        private DateTime ObtenerPrimeraFecha(string MontName, int DiaInicio)
        {
            int tmp = 0;
            DateTime res = new DateTime();
            string[] ssize = MontName.Split(new char[0]);
            string[] monthNames = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;
            for (int i = 0; i <= monthNames.Count() - 1; i++)
            {
                var re = ssize[0];
                var re2 = monthNames[i];
                if (ssize[0].Equals(monthNames[i]))
                {
                    tmp = i;
                    string tmpp = "";
                    string tmppdia = "";
                    if (tmp < 10)
                    {
                        tmp++;
                        tmpp = "0" + tmp.ToString();
                        if (DiaInicio < 10)
                        {
                            tmppdia = "0" + DiaInicio.ToString();
                        }
                        else
                        {
                            tmppdia = DiaInicio.ToString();
                        }

                    }
                    else
                    {
                        tmp++;
                        tmpp = tmp.ToString();
                        if (DiaInicio < 10)
                        {
                            tmppdia = "0" + DiaInicio.ToString();
                        }
                        else
                        {
                            tmppdia = DiaInicio.ToString();
                        }
                    }
                    string validFec = ssize[1] + tmpp + tmppdia;
                    res = DateTime.ParseExact(validFec, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            return res;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Una Tabla Con las Cuotas de Pago
        /// </summary>
        /// <param name="Lista">campos de Cuotas Para Generar tabla</param>
        /// <returns> string </returns>
        public string GeneraHtml(List<PagoCuotaDTO> Lista)
        {
            string tabla = "";
            tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
            tabla += "<tr>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
            tabla += "</tr>";
            foreach (var re in Lista)
            {
                tabla += "<tr>";
                tabla += "<td style='border: 1px solid #E6E6E6' >" + re.cuotaDescripcion + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6'> " + re.fechapago.ToShortDateString() + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.montoCuotaDescuento + "</td>";
                tabla += "</tr>";
            }
            tabla += "</TABLE>";
            return tabla;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla De monto Pago pa Contado y Credito
        /// </summary>
        /// <param name="data">datos Compuestos de MontoPago</param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> _generarGridAmbos(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            //matriculas///////////////////////////////////////////////////7
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;

            if (tamanioMatricula == 0) tamanioMatricula = 1;
            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Matricula " + (numeroco);
                obj.montoCuota = data.mp_matricula;
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }

            /////cuotas///////////////////////////////////////////////////
            var tamanio = 0;
            var tamanioContador = 0;
            tamanio = data.mp_nro_cuotas + data.tp_cuotas_adicionales;
            numeroco = numeroco - 1;
            var tamaniocuotas = tamanio;
            var sindescuento = data.mp_precio - data.mp_matricula;

            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Cuota - " + (numeroco);
                obj.montoCuota = data.mp_cuotas;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral(sindescuento / tamaniocuotas, data.tp_porcentaje_cuotas);
                obj.ispagado = false;
                obj.es_matricula = false;
                DateTime fecha = _calcularFechaInicial(data, i);
                obj.fechapago = fecha;
                lista.Add(obj);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month + 1;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj1 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj1.numeroCuota = numeroco;
                        obj1.cuotaDescripcion = "Cuota - " + (numeroco);
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = _tipoDescuentoGeneral(sindescuento / tamaniocuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = _calcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla de Cuotas de Pago de un Programa
        /// </summary>
        /// <param name="data"></param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> _generarGridGeneral(MontoPagoCompuestoDTO data)
        {
            string test = "";
            var tamanio = 0;
            var tamanioContador = 0;
            tamanio = data.mp_nro_cuotas;
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO();
            obj.numeroCuota = numeroco;
            obj.cuotaDescripcion = "Matricula ";
            obj.montoCuota = data.mp_matricula;
            DateTime fpag = DateTime.Now;
            fpag = fpag.AddMonths(1);
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_matricula, data.tp_porcentaje_general);
            obj.ispagado = false;
            obj.es_matricula = true;
            lista.Add(obj);
            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj1 = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj1.numeroCuota = numeroco;
                obj1.cuotaDescripcion = "Cuota - " + (numeroco - 1);
                obj1.montoCuota = data.mp_cuotas;
                obj1.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                obj1.ispagado = false;
                obj1.es_matricula = false;
                DateTime fecha = _calcularFechaInicial(data, i);
                obj1.fechapago = fecha;
                lista.Add(obj1);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj2 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj2.numeroCuota = numeroco;
                        obj2.cuotaDescripcion = "Cuota - " + (numeroco - 1);
                        obj2.montoCuota = data.mp_cuotas;
                        obj2.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                        obj2.es_matricula = false;
                        obj2.fechapago = _calcularFechaInicial(data, i);
                        lista.Add(obj2);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Html del Precio Contado
        /// </summary>
        /// <param name="Lista"></param>
        /// <returns> string </returns>
        private string GeneraHtmlPrecioContado(List<PagoCuotaDTO> Lista)
        {
            string tabla = "";
            tabla = Lista[0].SimboloMoneda.Replace(".", " ") + " " + Lista[0].montoCuota.ToString();
            return tabla;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Grid Normal
        /// </summary>
        /// <param name="data"></param>
        /// <returns> string </returns>
        private List<PagoCuotaDTO> _generarGridNormal(MontoPagoCompuestoDTO data)
        {
            string simbolo = "";

            var servicioMoneda = new MonedaService(_unitOfWork);
            var respuesta = servicioMoneda.ObtenerMonedaParaDocumento(data.mp_moneda);

            if (respuesta != null)
            {
                simbolo = respuesta.Simbolo;
            }
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO();
            obj.numeroCuota = 1;
            obj.cuotaDescripcion = "Contado";
            obj.montoCuota = data.mp_cuotas;
            DateTime fpag = DateTime.Now;
            //fpag = fpag.AddMonths(1);
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = float.Parse(data.mp_cuotas.ToString());
            obj.ispagado = false;
            obj.es_matricula = true;
            obj.SimboloMoneda = simbolo;
            lista.Add(obj);
            return lista;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Determina si una Plantilla Existe basado en su identificador
        /// </summary>
        /// <param name="idPlantilla">Id de la Plantilla</param>
        /// <returns> bool </returns>
        public bool ExistePorId(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.Exist(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene plantillas de mensajes para chat por idPlantilla
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns></returns>
        public IEnumerable<ComboFiltroDTO> ObtenerPlantillaChatIntegraSoporte(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPlantillaChatIntegraSoporte(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para notificacion de actualización de foro de programa general
        /// </summary>
        /// <param name="emailPersonal">Email de Personal</param>
        /// <param name="idPersonal">Id de Personal</param>
        /// <returns>Vacio, asigna a las propiedades locales los resultados</returns>
        public PlantillaEmailMandrillDTO ReemplazarSpeechChatSoporte(string emailPersonal, int idPersonal, int idPlantilla)
        {
            try
            {
                var listaObjetoWhasApp = new List<DatosPlantillaWhatsAppDTO>();
                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);
                var iPlantillaBase = plantilla.IdPlantillaBase;
                var texto = new PlantillaEmailMandrillDTO();
                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("codigo de plantilla no valido!");
                }

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("codigo de plantilla no valido!");
                }
                var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new DatosPlantillaWhatsAppDTO() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }
                //Valores Personal
                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonal);
                //reemplazo
                if (plantillaBase.Cuerpo.Contains("{T_Personal.Nombres}"))
                {
                    var valor = personal.Nombres;
                    plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.Nombres}", valor);
                }
                if (plantillaBase.Cuerpo.Contains("{T_Personal.Email}"))
                {
                    var valor = personal.Email;
                    plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Personal.Email}", valor);
                }

                var textoPlano = ConvertirHtmlEntextoPlano(plantillaBase.Cuerpo);
                texto.CuerpoHTML = textoPlano;
                return texto;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// <summary>
        /// Cambia HTML a texto plano
        /// </summary>
        /// <param name="html">Cadena en formato html</param>
        /// <returns>String cadena en formato plano</returns>
        private static string ConvertirHtmlEntextoPlano(string html)
        {
            const string espacioBlanco = @"(>|$)(\W|\n|\r)+<";
            const string darFormato = @"<[^>]*(>|$)";
            const string darFormatoLinea = @"<(br|BR)\s{0,1}\/{0,1}>";
            var reemplazoLinea = new Regex(darFormatoLinea, RegexOptions.Multiline);
            var reemplazoFormatoPlano = new Regex(darFormato, RegexOptions.Multiline);
            var reemplazoFormatoBlanco = new Regex(espacioBlanco, RegexOptions.Multiline);
            var texto = html;
            texto = System.Net.WebUtility.HtmlDecode(texto);
            texto = reemplazoFormatoBlanco.Replace(texto, "><");
            texto = reemplazoLinea.Replace(texto, Environment.NewLine);
            texto = reemplazoFormatoPlano.Replace(texto, string.Empty);
            return texto;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene tupla Por nombre
        /// </summary>
        /// <param name="nombreB"></param>
        /// <param name="nombreP"></param>
        /// <returns></returns>
        public PlantillaDTO ObtenerPorNombre(string nombreB, string nombreP)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPorNombre(nombreB, nombreP);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Plantillas de WhatsApp para Operaciones
        /// </summary>
        /// <returns> List<PlantillaDTO> </returns>  
        public List<PlantillaTipoEnvioDTO> ObtenerListaPlantillasConfiguracionEnvio()
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerListaPlantillasConfiguracionEnvio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una el registro de Plantilla 
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public DatosPlantillaDTO ObtenerPlantillaPorId(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPlantillaPorId(idPlantilla);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene La lista de plantillas
        /// </summary>
        /// <returns> DTO: List<PlantillaDatoDTO> </returns>

        public List<PlantillaDatoDTO> ObtenerListarPlantilla()
        {
            try
            {
                var resultado = _unitOfWork.PlantillaRepository.ObtenerListarPlantilla();
                var idsResultado = resultado.Select(r => r.Id).ToList(); // Obtener los IDs de los resultados

                var resultadoLista = _unitOfWork.PlantillaRepository.ObtenerPorPlantlla(idsResultado);

                foreach (var item in resultado)
                {
                    item.ListaPlantillaAsociacionModuloSistema = resultadoLista.Where(x => x.IdPlantilla == item.Id).ToList();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Plantills Asociacion modulo sistema por listas de idPlantilla
        /// </summary>
        /// <returns> DTO: List<PlantillaAsociacionModuloSistemaDTO> </returns>

        public List<PlantillaAsociacionModuloSistemaDTO> ObtenerPorPlantlla(List<int> listaIdPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPorPlantlla(listaIdPlantilla);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero
        /// Fecha: 17/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Plantills Clave Valoe
        /// </summary>
        /// <returns> DTO: PlantillaValorDetalleDTO </returns>

        public PlantillaValorDetalleDTO ObtenerPlantillaClaveValor(int idPlantilla)
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPlantillaClaveValor(idPlantilla);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Insertar(CompuestoPlantillaDTO Json, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Plantilla plantilla = new Plantilla
                    {
                        Id = Json.DatosPlantilla.Id,
                        Nombre = Json.DatosPlantilla.Nombre,
                        Descripcion = Json.DatosPlantilla.Descripcion,
                        IdPlantillaBase = Json.DatosPlantilla.IdPlantillaBase,
                        EstadoAgenda = Json.DatosPlantilla.EstadoAgenda,
                        EstadoPlantillaIntegra = Json.DatosPlantilla.EstadoPlantilla,
                        Documento = Json.DatosPlantilla.Documento,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                        IdPersonalAreaTrabajo = Json.DatosPlantilla.IdPersonalAreaTrabajo,
                    };

                    foreach (var item in Json.PlantillaClaveValor)
                    {
                        var plantillaClaveValor = new PlantillaClaveValor
                        {
                            Clave = item.Clave,
                            Valor = item.Valor,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        plantilla.PlantillaClaveValor.Add(plantillaClaveValor);
                    }

                    foreach (var item in Json.FasesPlantilla)
                    {
                        var fasePlantilla = new FaseByPlantilla
                        {
                            idFaseOrigen = item,
                            NombreFase = " ",
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        plantilla.FaseByPlantilla.Add(fasePlantilla);
                    }

                    foreach (var item in Json.ListaPlantillaAsociacionModuloSistema)
                    {
                        var plantillaAsociacionModuloSistema = new PlantillaAsociacionModuloSistema
                        {
                            IdModuloSistema = item.IdModuloSistema,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        plantilla.PlantillaAsociacionModuloSistema.Add(plantillaAsociacionModuloSistema);
                    }

                    var idPlantilla = _unitOfWork.PlantillaRepository.Add(plantilla);
                    _unitOfWork.Commit();

                    if (idPlantilla != null)
                    {

                        if (Json.DetallePlantilla.botones != null && Json.DetallePlantilla.botones.Any())
                        {
                            if (!string.IsNullOrEmpty(Json.DetallePlantilla.Imagen))
                            {

                                foreach (var item in Json.DetallePlantilla.botones)
                                {
                                    var plantillaDetalle = new InsertarDetallePlantillaDTO
                                    {
                                        Imagen = Json.DetallePlantilla.Imagen,
                                        Boton = item.Nombre,
                                        IdPlantilla = idPlantilla.Id,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    };

                                    _unitOfWork.PlantillaRepository.InsertarDetallePlantilla(plantillaDetalle);
                                }


                            }
                            else
                            {
                                foreach (var item in Json.DetallePlantilla.botones)
                                {
                                    var plantillaDetalle = new InsertarDetallePlantillaDTO
                                    {
                                        Imagen = string.Empty,
                                        Boton = item.Nombre,
                                        IdPlantilla = idPlantilla.Id,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    };

                                    _unitOfWork.PlantillaRepository.InsertarDetallePlantilla(plantillaDetalle);
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(Json.DetallePlantilla.Imagen) && Json.DetallePlantilla.botones.Count == 0)
                        {

                            var plantillaDetalle = new InsertarDetallePlantillaDTO
                            {
                                Imagen = Json.DetallePlantilla.Imagen,
                                Boton = string.Empty,
                                IdPlantilla = idPlantilla.Id,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };

                            _unitOfWork.PlantillaRepository.InsertarDetallePlantilla(plantillaDetalle);


                        }

                        scope.Complete();
                        return true;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return false; // Añadido para devolver un valor predeterminado en caso de error
        }


        public bool Actualizar(CompuestoPlantillaDTO Json, string usuario)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    if (!_unitOfWork.PlantillaRepository.Exist(Json.DatosPlantilla.Id))
                    {
                        return false;
                    }
                    else
                    {

                        _unitOfWork.PlantillaClaveValorRepository.Delete(_unitOfWork.PlantillaClaveValorRepository.GetBy(x => x.IdPlantilla == Json.DatosPlantilla.Id).Select(x => x.Id), usuario);
                        _unitOfWork.FaseByPlantillaRepository.Delete(_unitOfWork.FaseByPlantillaRepository.GetBy(x => x.IdPlantilla == Json.DatosPlantilla.Id).Select(x => x.Id), usuario);
                        _unitOfWork.PlantillaAsociacionModuloSistemaRepository.Delete(_unitOfWork.PlantillaAsociacionModuloSistemaRepository.GetBy(x => x.IdPlantilla == Json.DatosPlantilla.Id).Select(x => x.Id), usuario);

                        var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(Json.DatosPlantilla.Id);
                        plantilla.Nombre = Json.DatosPlantilla.Nombre;
                        plantilla.Descripcion = Json.DatosPlantilla.Descripcion;
                        plantilla.IdPlantillaBase = Json.DatosPlantilla.IdPlantillaBase;
                        plantilla.EstadoAgenda = Json.DatosPlantilla.EstadoAgenda;
                        plantilla.EstadoPlantillaIntegra = Json.DatosPlantilla.EstadoPlantilla;
                        plantilla.Documento = Json.DatosPlantilla.Documento;
                        plantilla.UsuarioModificacion = usuario;
                        plantilla.FechaModificacion = DateTime.Now;
                        plantilla.Estado = true;
                        plantilla.IdPersonalAreaTrabajo = Json.DatosPlantilla.IdPersonalAreaTrabajo;

                        foreach (var item in Json.PlantillaClaveValor)
                        {

                            PlantillaClaveValor plantillaClaveValor;
                            if (_unitOfWork.PlantillaClaveValorRepository.Exist(x => x.Clave == item.Clave && x.IdPlantilla == Json.IdPlantilla))
                            {
                                plantillaClaveValor = _unitOfWork.PlantillaClaveValorRepository.ObtenerPorClaveYPorIdPlantilla(item.Clave, Json.IdPlantilla);
                                plantillaClaveValor.Clave = item.Clave;
                                plantillaClaveValor.Valor = item.Valor;
                                plantillaClaveValor.UsuarioModificacion = usuario;
                                plantillaClaveValor.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                plantillaClaveValor = new PlantillaClaveValor
                                {
                                    Clave = item.Clave,
                                    Valor = item.Valor,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            plantilla.PlantillaClaveValor.Add(plantillaClaveValor);
                        }

                        foreach (var item in Json.FasesPlantilla)
                        {
                            FaseByPlantilla fasesPlantilla;
                            if (_unitOfWork.FaseByPlantillaRepository.Exist(x => x.IdFaseOrigen == item && x.IdPlantilla == Json.IdPlantilla))
                            {
                                fasesPlantilla = _unitOfWork.FaseByPlantillaRepository.ObtenerPorIdOrigenYPorIdPlantilla(item, Json.IdPlantilla);
                                fasesPlantilla.idFaseOrigen = item;
                                fasesPlantilla.NombreFase = " ";
                                fasesPlantilla.UsuarioModificacion = usuario;
                                fasesPlantilla.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                fasesPlantilla = new FaseByPlantilla
                                {
                                    idFaseOrigen = item,
                                    NombreFase = " ",
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            plantilla.FaseByPlantilla.Add(fasesPlantilla);
                        }

                        foreach (var item in Json.ListaPlantillaAsociacionModuloSistema)
                        {
                            PlantillaAsociacionModuloSistema plantillaAsociacionModuloSistema;
                            if (_unitOfWork.PlantillaAsociacionModuloSistemaRepository.Exist(x => x.IdModuloSistema == item.IdModuloSistema && x.IdPlantilla == Json.IdPlantilla))
                            {
                                plantillaAsociacionModuloSistema = _unitOfWork.PlantillaAsociacionModuloSistemaRepository.ObtenerPorIdModuloSistemaYPorIdPlantilla(item.IdModuloSistema, Json.IdPlantilla);
                                plantillaAsociacionModuloSistema.IdModuloSistema = item.IdModuloSistema;
                                plantillaAsociacionModuloSistema.UsuarioModificacion = usuario;
                                plantillaAsociacionModuloSistema.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                plantillaAsociacionModuloSistema = new PlantillaAsociacionModuloSistema
                                {
                                    IdModuloSistema = item.IdModuloSistema,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                            }
                            plantilla.PlantillaAsociacionModuloSistema.Add(plantillaAsociacionModuloSistema);
                        }

                        _unitOfWork.PlantillaRepository.Update(plantilla);
                        _unitOfWork.Commit();
                        scope.Complete();
                        
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Eliminar(int id, string usuario)
        {
            try
            {
                _unitOfWork.PlantillaRepository.Delete(id, usuario);
                _unitOfWork.PlantillaClaveValorRepository.Delete(_unitOfWork.PlantillaClaveValorRepository.GetBy(x => x.IdPlantilla == id).Select(x => x.Id), usuario);
                _unitOfWork.FaseByPlantillaRepository.Delete(_unitOfWork.FaseByPlantillaRepository.GetBy(x => x.IdPlantilla == id).Select(x => x.Id), usuario);
                _unitOfWork.PlantillaAsociacionModuloSistemaRepository.Delete(_unitOfWork.PlantillaAsociacionModuloSistemaRepository.GetBy(x => x.IdPlantilla == id).Select(x => x.Id), usuario);
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ComboDTO> ObtenerModulo()
        {
            try
            {
                return _unitOfWork.ModuloSistemaV5Repository.ObtenerCombo();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComboDTO> ObtenerPlantillasSpeech()
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerPlantillasSpeech();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComboDTO> ObtenerAllPlantillaSpeechDespedida()
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerAllPlantillaSpeechDespedida();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 24/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de plantillas certificado
        /// </summary>
        /// <returns> DTO: DatosPlantillaDTO </returns>
        public List<PlantilaCertificadoConstanciaDTO> ObtenerListaPlantillaCertificadoOperaciones()
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerListaPlantillaCertificadoOperaciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor:Eliot Arias F.
        /// Fecha: 25/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de plantillas Email para filtro GP.
        /// </summary>
        /// <returns> IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> </returns>
        public IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseEmail()
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerNombrePlantillaBaseEmail();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor:Eliot Arias F.
        /// Fecha: 25/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de plantillas Watsapp para filtro GP.
        /// </summary>
        /// <returns> IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> </returns>
        public IEnumerable<ComboPlantillaNombrePlantillaBaseDTO> ObtenerNombrePlantillaBaseWatsApp()
        {
            try
            {
                return _unitOfWork.PlantillaRepository.ObtenerNombrePlantillaBaseWatsApp();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Eliot Arias F.
        /// Fecha: 11/11/2024
        /// Version: 1.0
        /// <param name="FechaGP">Fecha de envio correo</param>
        /// <param name="IdPlantilla">id de la plantilla</param>
        /// <param name="IdPostulanteProcesoSeleccion">id PostulanteProcesoSeleccion</param>
        /// <param name="personal">TPersonal </param>
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de proceso de seleccion
        /// </summary>
        /// <returns>PlantillaEmailMandrillDTO</returns>
        public PlantillaEmailMandrillDTO ReemplazarEtiquetasProcesoSeleccionReportePostulanteCursoAsesorCapacitacion(int IdPlantilla, int IdPostulanteProcesoSeleccion, Personal personal, DateTime? FechaGP)
        {
            try
            {

                PlantillaWhatsAppAccesosDTO WhatsAppReemplazado = new PlantillaWhatsAppAccesosDTO();
                PlantillaEmailMandrillDTO EmailReemplazado = new PlantillaEmailMandrillDTO();
                var listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(IdPlantilla);

                var IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();


                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                }

                //reemplazo
                //var detalleEnvioMaterialProveedorImpresion = _repMaterialPEspecificoDetalle.ObtenerDetalleMaterialPEspecificoEnviarProveedor(this.IdMaterialPEspecificoDetalle);

                var postulanteProcesoSeleccion = _unitOfWork.PostulanteRepository.ObtenerProcesoSeleccionInscrito(IdPostulanteProcesoSeleccion);
                var postulante = _unitOfWork.PostulanteRepository.FirstById(postulanteProcesoSeleccion.IdPostulante);
                var datosPostulanteCurso = _unitOfWork.PostulanteRepository.ObtenerDatosMatriculaIdPostulante(postulanteProcesoSeleccion.IdPostulante);
                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre1}") || plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().Texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Usuario}"))
                {
                    var valor = datosPostulanteCurso.Email;

                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Usuario}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Usuario}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Clave}"))
                {
                    var valor = datosPostulanteCurso.Contraseña;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Clave}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Clave}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombreReclutador}"))
                {
                    var valor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombreReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.NombreReclutador}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NumeroReclutador}"))
                {
                    var valor = personal.MovilReferencia;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NumeroReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.NumeroReclutador}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Hora}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.ToString("hh:mm tt");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Hora}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.Hora}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Fecha}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.Date.ToString("dd-MM-yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Fecha}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.Fecha}")).FirstOrDefault().Texto = valor;
                    }
                }
                //asunto
                if (plantillaBase.Asunto.Contains("{T_Postulante.Nombre1}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }
                return EmailReemplazado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor:Eliot Arias F.
        /// Fecha: 14/11/2024
        /// Version: 1.0
        /// <param name="FechaGP">Fecha de envio correo</param>
        /// <param name="IdPlantilla">id de la plantilla</param>
        /// <param name="IdPostulanteProcesoSeleccion">id PostulanteProcesoSeleccion</param>
        /// <param name="personal">TPersonal </param>
        /// <summary>
        /// Implementa la logica para reemplazar las etiquetas por plantilla para casos de proceso de seleccion
        /// </summary>
        /// <returns>PlantillaEmailMandrillDTO</returns>
        public PlantillaEmailMandrillDTO ReemplazarEtiquetasProcesoSeleccion(int IdPlantilla, int IdPostulanteProcesoSeleccion, Personal personal, DateTime? FechaGP)
        {
            try
            {
                PlantillaWhatsAppAccesosDTO WhatsAppReemplazado = new PlantillaWhatsAppAccesosDTO();
                PlantillaEmailMandrillDTO EmailReemplazado = new PlantillaEmailMandrillDTO();
                var listaObjetoWhasApp = new List<DatoPlantillaWhatsAppDTO>();

                var plantilla = _unitOfWork.PlantillaRepository.FirstById(IdPlantilla);

                var IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();


                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new DatoPlantillaWhatsAppDTO() { Codigo = string.Concat("{", etiqueta, "}"), Texto = "" });
                }

                //reemplazo
                //var detalleEnvioMaterialProveedorImpresion = _repMaterialPEspecificoDetalle.ObtenerDetalleMaterialPEspecificoEnviarProveedor(this.IdMaterialPEspecificoDetalle);
                var postulanteProcesoSeleccion = _unitOfWork.PostulanteRepository.ObtenerProcesoSeleccionInscrito(IdPostulanteProcesoSeleccion);
                var postulante = _unitOfWork.PostulanteRepository.FirstById(postulanteProcesoSeleccion.IdPostulante);

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre1}") || plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().Texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombrePuesto}") || plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Nombre}"))
                {
                    var valor = postulanteProcesoSeleccion.PuestoTrabajo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombrePuesto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.NombrePuesto}")).FirstOrDefault().Texto = valor;
                    }
                }

                var token = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(postulanteProcesoSeleccion.Id);
                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.LinkExamenPostulante}"))
                {
                    var valor = "https://proceso-seleccion.bsginstitute.com/procesoseleccion/acceso?guid=" + token.GuidAccess;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.LinkExamenPostulante}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.LinkExamenPostulante}")).FirstOrDefault().Texto = valor;
                    }
                }



                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Usuario}"))
                {
                    var valor = postulante.NroDocumento;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Usuario}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Usuario}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Clave}"))
                {
                    var valor = token.Token;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Clave}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Clave}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombreReclutador}"))
                {
                    var valor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombreReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.NombreReclutador}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NumeroReclutador}"))
                {
                    var valor = personal.MovilReferencia;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NumeroReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.NumeroReclutador}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Hora}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.ToString("hh:mm tt");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Hora}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.Hora}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Fecha}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.Date.ToString("dd-MM-yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Fecha}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.Fecha}")).FirstOrDefault().Texto = valor;
                    }
                }
                //asunto
                if (plantillaBase.Asunto.Contains("{T_Postulante.Nombre1}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_ProcesoSeleccion.NombrePuesto}"))
                {
                    var valor = postulanteProcesoSeleccion.PuestoTrabajo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_ProcesoSeleccion.NombrePuesto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.Codigo.Equals("{T_ProcesoSeleccion.NombrePuesto}")).FirstOrDefault().Texto = valor;
                    }
                }

                if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.Codigo, item.Texto);
                    }
                }

                return EmailReemplazado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PlantillaWhatsAppPostulante ReemplazarEtiquetasProcesoSeleccionWhatsApp(int IdPlantilla, int IdPostulanteProcesoSeleccion, Personal personal, DateTime? FechaGP)
        {
            try
            {
                PlantillaWhatsAppPostulante WhatsAppReemplazado = new PlantillaWhatsAppPostulante();
                PlantillaEmailMandrillDTO EmailReemplazado = new PlantillaEmailMandrillDTO();
                var listaObjetoWhasApp = new List<DatosPlantillaWhatsAppDTO>();

                var plantilla = _unitOfWork.PlantillaRepository.FirstById(IdPlantilla);

                var IdPlantillaBase = plantilla.IdPlantillaBase;

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                if (!_unitOfWork.PlantillaBaseRepository.Exist(plantilla.IdPlantillaBase))
                {
                    throw new Exception("Plantilla no valida");
                }

                var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(IdPlantilla);
                var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();


                foreach (var etiqueta in listaEtiqueta)
                {
                    listaObjetoWhasApp.Add(new DatosPlantillaWhatsAppDTO() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                }

                //reemplazo
                //var detalleEnvioMaterialProveedorImpresion = _repMaterialPEspecificoDetalle.ObtenerDetalleMaterialPEspecificoEnviarProveedor(this.IdMaterialPEspecificoDetalle);
                var postulanteProcesoSeleccion = _unitOfWork.PostulanteRepository.ObtenerProcesoSeleccionInscrito(IdPostulanteProcesoSeleccion);
                var postulante = _unitOfWork.PostulanteRepository.FirstById(postulanteProcesoSeleccion.IdPostulante);

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre1}") || plantillaBase.Cuerpo.Contains("{T_Postulante.Nombre}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().texto = valor;
                    }
                }


                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombrePuesto}") || plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Nombre}"))
                {
                    var valor = postulanteProcesoSeleccion.PuestoTrabajo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombrePuesto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NombrePuesto}")).FirstOrDefault().texto = valor;
                    }
                }

                var token = _unitOfWork.PostulanteProcesoSeleccionRepository.ObtenerPostulanteProcesoSeleccion(postulanteProcesoSeleccion.Id);
                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.LinkExamenPostulante}"))
                {
                    var valor = "https://proceso-seleccion.bsginstitute.com/procesoseleccion/acceso?guid=" + token.GuidAccess;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.LinkExamenPostulante}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.LinkExamenPostulante}")).FirstOrDefault().texto = valor;
                    }
                }



                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Usuario}"))
                {
                    var valor = postulante.NroDocumento;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Usuario}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Usuario}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Postulante.Clave}"))
                {
                    var valor = token.Token;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Postulante.Clave}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Clave}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NombreReclutador}"))
                {
                    var valor = string.Concat(personal.Nombres, " ", personal.Apellidos);
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NombreReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NombreReclutador}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.NumeroReclutador}"))
                {
                    var valor = personal.MovilReferencia;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.NumeroReclutador}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NumeroReclutador}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Hora}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.ToString("hh:mm tt");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Hora}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.Hora}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_ProcesoSeleccion.Fecha}"))
                {
                    if (!FechaGP.HasValue)
                    {
                        FechaGP = DateTime.Now;
                    }
                    var valor = FechaGP.Value.Date.ToString("dd-MM-yyyy");
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_ProcesoSeleccion.Fecha}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.Fecha}")).FirstOrDefault().texto = valor;
                    }
                }
                //asunto
                if (plantillaBase.Asunto.Contains("{T_Postulante.Nombre1}"))
                {
                    var valor = postulante.Nombre;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_Postulante.Nombre1}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_Postulante.Nombre1}")).FirstOrDefault().texto = valor;
                    }
                }

                if (plantillaBase.Asunto.Contains("{T_ProcesoSeleccion.NombrePuesto}"))
                {
                    var valor = postulanteProcesoSeleccion.PuestoTrabajo;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{T_ProcesoSeleccion.NombrePuesto}", valor);
                    }
                    else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    {
                        listaObjetoWhasApp.Where(x => x.codigo.Equals("{T_ProcesoSeleccion.NombrePuesto}")).FirstOrDefault().texto = valor;
                    }
                }

                if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                else if (IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                {
                    WhatsAppReemplazado.Plantilla = plantillaBase.Cuerpo;
                    WhatsAppReemplazado.ListaEtiquetas = listaObjetoWhasApp;
                    WhatsAppReemplazado.Descripcion = plantilla.Descripcion;

                    foreach (var item in WhatsAppReemplazado.ListaEtiquetas)
                    {
                        WhatsAppReemplazado.Plantilla = WhatsAppReemplazado.Plantilla.Replace(item.codigo, item.texto);
                    }
                }

                return WhatsAppReemplazado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
