using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PlantillaPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 08/08/2022
    /// <summary>
    /// Gestión general de T_PlantillaPw
    /// </summary>
    public class PlantillaPwService : IPlantillaPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private PlantillaPw? _plantillaPw;
        private List<SeccionEtiquetaDTO>? _datosEtiquetas;
        private List<CursosRelacionadosDTO>? _listaCursosRelacionados;
        private List<ProblemaCausaDTO>? _listaProblemasCausa;
        private List<PGeneralCursoRelacionadoDTO>? _urlCursosRelacionados;
        private List<ClaveValorDTO>? _listaTemplateV2ReemplazoEtiqueta;
        private OportunidadAlumnoDTO? _datosOportunidadAlumno;
        private string? _etiquetaMontosPagoPaquetes;
        private string? _cronogramaPagos;
        public OportunidadAlumnoDTO DatosOportunidadAlumno() => _datosOportunidadAlumno;
        public string CronogramaPagos() => _cronogramaPagos;
        public string EtiquetaMontosPagoPaquetes() => _etiquetaMontosPagoPaquetes;
        public List<ProblemaCausaDTO> ListaProblemasCausa() => _listaProblemasCausa;
        public List<ClaveValorDTO> ListaTemplateV2ReemplazoEtiqueta() => _listaTemplateV2ReemplazoEtiqueta;
        public List<PGeneralCursoRelacionadoDTO> UrlCursosRelacionados() => _urlCursosRelacionados;

        public PlantillaPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPlantillaPw, PlantillaPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);

        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlantillaPw
        /// </summary>
        /// <returns> List<PlantillaPwDTO> </returns>
        public IEnumerable<PlantillaPwDTO> Obtener()
        {
            try
            {
                return _unitOfWork.PlantillaPwRepository.Obtener();
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
        /// Obtiene registros de T_PlantillaPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlantillaPwRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaPw para mostrarse en combo.
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro de Costo</param>
        /// <param name="idFaseOportunidad">Id de Fase Oportunidad</param>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> List<ComboDTO> </returns>
        public void ObtenerValorEtiqueta(int idCentroCosto, int idOportunidad)
        {
            _datosEtiquetas = _unitOfWork.PEspecificoRepository.ObtenerSeccionEtiquetaPorIdCentroCosto(idCentroCosto);
            var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
            var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);

            _listaProblemasCausa = _unitOfWork.PlantillaClaveValorRepository.ObtenerCausaProblemaPorIdOportunidad(idOportunidad);
            _urlCursosRelacionados = _unitOfWork.PlantillaClaveValorRepository.ObtenerCursosRelacionadosPorIdCentroCosto(idCentroCosto);

            int idPGeneral = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto).IdProgramaGeneral.GetValueOrDefault();

            _etiquetaMontosPagoPaquetes = ObtenerEtiquetaMontoPagoV2(idPGeneral, oportunidad, alumno.IdCodigoPais.Value);
            _cronogramaPagos = GenerarCronograma(idOportunidad);
            _listaTemplateV2ReemplazoEtiqueta = ObtenerTemplatesV2ReemplazoEtiqueta(idCentroCosto);

            _datosOportunidadAlumno = new OportunidadAlumnoDTO();
            // Logica nuevas etiquetas
            _datosOportunidadAlumno.OportunidadAlumno = new OportunidadAlumnoDetalleDTO();
            _datosOportunidadAlumno.OportunidadAlumno.Nombre1 = alumno.Nombre1;
            _datosOportunidadAlumno.OportunidadAlumno.NombreCompleto = string.Concat(alumno.Nombre1, " ", alumno.Nombre2, " ", alumno.ApellidoPaterno, " ", alumno.ApellidoMaterno).ToUpper(); ;
            _datosOportunidadAlumno.OportunidadAlumno.Email1 = alumno.Email1;
            _datosOportunidadAlumno.OportunidadAlumno.NroDocumento = alumno.Dni == null ? "" : alumno.Dni.ToUpper();
            _datosOportunidadAlumno.OportunidadAlumno.Direccion = alumno.Direccion == null ? "" : alumno.Direccion.ToUpper();
            _datosOportunidadAlumno.OportunidadAlumno.NombreCiudad = _unitOfWork.AlumnoRepository.ObtenerCiudadOrigen(alumno.Id);
            _datosOportunidadAlumno.OportunidadAlumno.NombrePais = _unitOfWork.AlumnoRepository.ObtenerPaisOrigen(alumno.Id);
            _datosOportunidadAlumno.OportunidadAlumno.IdCodigoPais = alumno.IdCodigoPais;
            _datosOportunidadAlumno.CronogramaPagoCompleto = _unitOfWork.OportunidadRepository.ObtenerCronogramaPagoCompleto(idOportunidad);
            _datosOportunidadAlumno.MontoTotal = _unitOfWork.OportunidadRepository.ObtenerMontoTotal(idOportunidad);
            _datosOportunidadAlumno.Version = _unitOfWork.OportunidadRepository.ObtenerVersion(idOportunidad);
            _datosOportunidadAlumno.IdPEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidad.IdCentroCosto.GetValueOrDefault()).Id;

        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaPw para mostrarse en combo.
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro de Costo</param>
        /// <param name="idFaseOportunidad">Id de Fase Oportunidad</param>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> List<ComboDTO> </returns>
        public async Task ObtenerValorEtiquetaAsync(int idCentroCosto, int idOportunidad)
        {
            var taskDatosEtiquetas = _unitOfWork.PEspecificoRepository.ObtenerSeccionEtiquetaPorIdCentroCostoAsync(idCentroCosto);
            var oportunidad = await _unitOfWork.OportunidadRepository.ObtenerPorIdAsync(idOportunidad);
            var alumno = await _unitOfWork.AlumnoRepository.ObtenerPorIdAsync(oportunidad.IdAlumno.Value);

            var task1 = _unitOfWork.PlantillaClaveValorRepository.ObtenerCausaProblemaPorIdOportunidadAsync(idOportunidad);
            var task2 = _unitOfWork.PlantillaClaveValorRepository.ObtenerCursosRelacionadosPorIdCentroCostoAsync(idCentroCosto);

            int idPGeneral = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto).IdProgramaGeneral.GetValueOrDefault();

            var task3 = ObtenerEtiquetaMontoPagoV2Async(idPGeneral, oportunidad, alumno.IdCodigoPais.Value);
            var task4 = Task.Run(() => GenerarCronograma(idOportunidad));
            var task5 = ObtenerTemplatesV2ReemplazoEtiquetaAsync(idCentroCosto);

            var cronogramaPagoCompleto = _unitOfWork.OportunidadRepository.ObtenerCronogramaPagoCompletoAsync(idOportunidad);
            var nombreCiudad = _unitOfWork.AlumnoRepository.ObtenerCiudadOrigenAsync(alumno.Id);
            var nombrePais = _unitOfWork.AlumnoRepository.ObtenerPaisOrigenAsync(alumno.Id);
            var montoTotal = _unitOfWork.OportunidadRepository.ObtenerMontoTotalAsync(idOportunidad);
            var version = _unitOfWork.OportunidadRepository.ObtenerVersionAsync(idOportunidad);
            var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCostoAsync(oportunidad.IdCentroCosto.GetValueOrDefault());

            //_datosOportunidadAlumno = new OportunidadAlumnoDTO();
            // Logica nuevas etiquetas
            _datosOportunidadAlumno.OportunidadAlumno = new OportunidadAlumnoDetalleDTO();
            _datosOportunidadAlumno.OportunidadAlumno.Nombre1 = alumno.Nombre1;
            _datosOportunidadAlumno.OportunidadAlumno.NombreCompleto = string.Concat(alumno.Nombre1, " ", alumno.Nombre2, " ", alumno.ApellidoPaterno, " ", alumno.ApellidoMaterno).ToUpper(); ;
            _datosOportunidadAlumno.OportunidadAlumno.Email1 = alumno.Email1;
            _datosOportunidadAlumno.OportunidadAlumno.NroDocumento = alumno.Dni == null ? "" : alumno.Dni.ToUpper();
            _datosOportunidadAlumno.OportunidadAlumno.Direccion = alumno.Direccion == null ? "" : alumno.Direccion.ToUpper();
            _datosOportunidadAlumno.OportunidadAlumno.NombreCiudad = await nombreCiudad;
            _datosOportunidadAlumno.OportunidadAlumno.NombrePais = await nombrePais;
            _datosOportunidadAlumno.OportunidadAlumno.IdCodigoPais = alumno.IdCodigoPais;
            _datosOportunidadAlumno.CronogramaPagoCompleto = await cronogramaPagoCompleto;
            _datosOportunidadAlumno.MontoTotal = await montoTotal;
            _datosOportunidadAlumno.Version = await version;
            await pEspecifico;
            _datosOportunidadAlumno.IdPEspecifico = pEspecifico.Result.Id;

            _datosEtiquetas = await taskDatosEtiquetas;
            _listaProblemasCausa = await task1;
            _urlCursosRelacionados = await task2;
            _etiquetaMontosPagoPaquetes = await task3;
            _cronogramaPagos = await task4;
            _listaTemplateV2ReemplazoEtiqueta = await task5;
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 13/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del Programa General
        /// </summary>
        /// <param name="idProgramaGeneral"> Id del Programa General </param>
        /// <returns>  </returns>
        public async Task CargarDatosOportunidadAlumnoReprogramacion(int idProgramaGeneral, int idCentroCosto, int idOportunidad)
        {
            _datosOportunidadAlumno = new OportunidadAlumnoDTO();
            _datosOportunidadAlumno.OportunidadAlumno = new OportunidadAlumnoDetalleDTO();
            if (_unitOfWork.PGeneralRepository.Exist(idProgramaGeneral))
            {
                PGeneral pGeneral = await _unitOfWork.PGeneralRepository.ObtenerPorIdAsync(idProgramaGeneral);
                _datosOportunidadAlumno.DuracionMesesPGeneral = pGeneral.PwDuracion;
                _datosOportunidadAlumno.NombrePGeneral = pGeneral.Nombre;
            }

            var oportunidad = await _unitOfWork.OportunidadRepository.ObtenerPorIdAsync(idOportunidad);
            var alumno = await _unitOfWork.AlumnoRepository.ObtenerPorIdAsync(oportunidad.IdAlumno.Value);

            var pEspecifico = await _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCostoAsync(idCentroCosto);
            //var pEspecifico = await _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCostoAsync(oportunidad.IdCentroCosto.GetValueOrDefault());

            // Logica nuevas etiquetas
            _datosOportunidadAlumno.OportunidadAlumno.Nombre1 = alumno.Nombre1;
            _datosOportunidadAlumno.OportunidadAlumno.NombreCompleto = string.Concat(alumno.Nombre1, " ", alumno.Nombre2, " ", alumno.ApellidoPaterno, " ", alumno.ApellidoMaterno).ToUpper(); ;
            _datosOportunidadAlumno.OportunidadAlumno.Email1 = alumno.Email1;
            _datosOportunidadAlumno.OportunidadAlumno.NroDocumento = alumno.Dni == null ? "" : alumno.Dni.ToUpper();
            _datosOportunidadAlumno.OportunidadAlumno.Direccion = alumno.Direccion == null ? "" : alumno.Direccion.ToUpper();
            _datosOportunidadAlumno.OportunidadAlumno.NombreCiudad = await _unitOfWork.AlumnoRepository.ObtenerCiudadOrigenAsync(alumno.Id);
            _datosOportunidadAlumno.OportunidadAlumno.NombrePais = await _unitOfWork.AlumnoRepository.ObtenerPaisOrigenAsync(alumno.Id);
            _datosOportunidadAlumno.OportunidadAlumno.IdCodigoPais = alumno.IdCodigoPais;
            _datosOportunidadAlumno.CronogramaPagoCompleto = await _unitOfWork.OportunidadRepository.ObtenerCronogramaPagoCompletoAsync(idOportunidad);
            _datosOportunidadAlumno.CronogramaPagoCompletoChile = await _unitOfWork.OportunidadRepository.ObtenerCronogramaPagoCompletoChileAsync(idOportunidad);
            _datosOportunidadAlumno.MontoTotal = await _unitOfWork.OportunidadRepository.ObtenerMontoTotalAsync(idOportunidad);
            _datosOportunidadAlumno.Version = await _unitOfWork.OportunidadRepository.ObtenerVersionAsync(idOportunidad);
            _datosOportunidadAlumno.IdPEspecifico = pEspecifico.Id;

            var fechaActual = DateTime.Now;
            _datosOportunidadAlumno.DiaFechaActual = fechaActual.Day.ToString();
            _datosOportunidadAlumno.NombreMesFechaActual = fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
            _datosOportunidadAlumno.AnioFechaActual = fechaActual.Year.ToString();
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
                    var valor = ObtenerEtiquetaMontoPagoPorPGeneral(argumentos);
                    valorEtiqueta = (valor != null) ? valor.Valor : null;
                }
                if (valorEtiqueta == null || valorEtiqueta == "")
                {
                    var valor = ObtenerEtiquetaMontoPagoPorOportunidad(argumentos);
                    valorEtiqueta = (valor != null) ? valor.Valor : null;
                }
            }
            return new StringDTO() { Valor = valorEtiqueta };
        }
        public string ObtenerEtiquetaMontoPagoV2(int idPGeneral, Oportunidad oportunidad, int idCodigoPais)
        {
            string valorTemporal = ObtenerMontosPagoPaquetesV2(idPGeneral, oportunidad, idCodigoPais);
            return valorTemporal ?? EtiquetaMontosPago(oportunidad.Id);
        }
        public async Task<string> ObtenerEtiquetaMontoPagoV2Async(int idPGeneral, Oportunidad oportunidad, int idCodigoPais)
        {
            string valorTemporal = await ObtenerMontosPagoPaquetesV2Async(idPGeneral, oportunidad, idCodigoPais);
            if (valorTemporal == null)
                return await EtiquetaMontosPagoAsync(oportunidad.Id);
            else
                return valorTemporal;
        }
        private string ObtenerMontosPagoPaquetesV2(int idPGeneral, Oportunidad oportunidad, int idCodigoPais)
        {
            var versiones = _unitOfWork.MontoPagoRepository.ObtenerVersionesMontoPagoV2(oportunidad.Id);
            if (versiones.Count() == 0)
                return null;
            else
            {
                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidad.IdCentroCosto.Value);
                var versionPrograma = _unitOfWork.VersionProgramaRepository.ObtenerVersionPrograma().ToList();
                List<string> listaBeneficios;
                string tabla = "";
                int contadorBeneficios = 0;

                tabla = @"<table border cellpadding=2 cellspacing=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";

                var documentoAgendaService = new DocumentoAgendaService(_unitOfWork);

                foreach (VersionProgramaDTO item in versionPrograma)
                {
                    listaBeneficios = documentoAgendaService.ObtenerBeneficiosConfiguradosProgramaGeneral(pEspecifico.IdProgramaGeneral.Value, idCodigoPais, item.Id);
                    contadorBeneficios += listaBeneficios.Count();

                    if (listaBeneficios.Count() > 0)
                    {
                        var infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();

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

                var beneficioPGeneral = _unitOfWork.PGeneralRepository.SeccionIndividualPGeneral(idPGeneral, "Beneficios");
                if (beneficioPGeneral != null)
                    tabla += $"<p>{beneficioPGeneral.PiePagina}</p>";

                if (contadorBeneficios == 0)
                    tabla = null;

                return tabla;
            }
        }

        /// <summary>
        /// Autor: Eliot Arias F
        /// Fecha: 18-02-2025.
        /// Versión: 1.0
        /// Obtiene los montos de pago y los beneficios asociados a los paquetes de un Programa General en formato de tabla HTML.
        /// </summary>
        /// <param name="idPGeneral">Identificador del Programa General.</param>
        /// <param name="oportunidad">Objeto de tipo Oportunidad que contiene información relevante.</param>
        /// <param name="idCodigoPais">Identificador del código de país para filtrar los beneficios.</param>
        /// <returns>Una cadena en formato HTML que representa una tabla con los montos de pago y beneficios, o null si no hay datos.</returns>
        private async Task<string> ObtenerMontosPagoPaquetesV2Async(int idPGeneral, Oportunidad oportunidad, int idCodigoPais)
        {
            var introduccionVersionBeneficio = _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerIntroduccionBeneficio(idPGeneral);

            var versiones = _unitOfWork.MontoPagoRepository.ObtenerVersionesMontoPagoV2(oportunidad.Id);
            if (versiones.Count() == 0)
                return null;
            else
            {
                string tabla = "";
                tabla = @"<table border cellpadding=2 cellspacing=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";

                var pEspecifico = await _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCostoAsync(oportunidad.IdCentroCosto.Value);
                var versionPrograma = await _unitOfWork.VersionProgramaRepository.ObtenerVersionProgramaAsync();
                int contadorBeneficios = 0;

                List<BeneficiosConfiguradosProgramaGeneralDTO> listaBeneficiosConfigurado = new List<BeneficiosConfiguradosProgramaGeneralDTO>();
                if (idCodigoPais != 0)
                {
                    listaBeneficiosConfigurado = await _unitOfWork.ConfiguracionBeneficioProgramaGeneralRepository.ObtenerBeneficiosConfiguradosProgramaGeneralAsync(idPGeneral, idCodigoPais);
                }

                if (listaBeneficiosConfigurado.Count() > 0)
                {
                    foreach (VersionProgramaDTO item in versionPrograma.ToList())
                    {
                        List<string> listaBeneficios = new List<string>();
                        listaBeneficios = listaBeneficiosConfigurado.Where(x => x.IdVersionPrograma == item.Id && x.IdPais == idCodigoPais).Select(x => x.Descripcion).ToList();
                        contadorBeneficios += listaBeneficios.Count();

                        if (listaBeneficios.Count() > 0)
                        {
                            var infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();
                            tabla += "<tr>";
                            tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count()}'>{item.Nombre}</td>";
                            tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count()}'>";

                            var introduccion = introduccionVersionBeneficio?
                                .Where(x => x.IdVersionPrograma == item.Id)
                                .Select(x => x.Introduccion)
                                .FirstOrDefault();

                            if (!string.IsNullOrEmpty(introduccion))
                            {
                                tabla += $"<p style='margin-top: -3px; margin-bottom: 5px;'><strong>{introduccion}</strong></p>";
                            }

                            tabla += "<ul>";
                            foreach (string beneficio in listaBeneficios)
                            {
                                tabla += $"<li>{beneficio}</li>";
                            }
                            tabla += "</ul></td>";


                            //infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();
                            //tabla += "<tr>";
                            //tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count()}'>{item.Nombre}</td>";
                            //tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count()}'><ul>";
                            //foreach (string beneficio in listaBeneficios)
                            //{
                            //    tabla += $"<li>{beneficio}</li>";
                            //}
                            //tabla += "</ul></td>";
                            int i = 0;
                            foreach (var re in infoVersiones)
                            {
                                tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                                tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString()
                                : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                                + "</td>";
                                i++;
                                if (i < infoVersiones.Count)
                                    tabla += "</tr><tr>";
                            }
                            tabla += "</tr>";
                        }
                    }
                }
                tabla += "</table>";

                var beneficioPGeneral = await _unitOfWork.PGeneralRepository.SeccionIndividualPGeneralAsync(idPGeneral, "Beneficios");
                if (beneficioPGeneral != null)
                    tabla += $"<p>{beneficioPGeneral.PiePagina}</p>";

                if (contadorBeneficios == 0)
                    tabla = null;

                return tabla;
            }
        }


        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago para las Plantillas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public string EtiquetaMontosPago(int idOportunidad)
        {
            var tabla = ObtenerMontosPagoPaquetes(idOportunidad);
            if (tabla == null)
            {
                string precioNormal = string.Empty;

                string precioContado = ObtenerPrecioContado(idOportunidad);
                string precioCoutas = ObtenerPrecioCuotas(idOportunidad);

                if (!string.IsNullOrEmpty(precioContado))
                    precioNormal = "<b>Al Contado: </b>" + precioContado;

                if (!string.IsNullOrEmpty(precioCoutas))
                    precioNormal += "<div style=' height:1px;'></div>" + "<b>Financiamiento: </b>" + precioCoutas;

                return precioNormal;
            }
            else
                return tabla;
        }

        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago para las Plantillas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public async Task<string> EtiquetaMontosPagoAsync(int idOportunidad)
        {
            var tabla = ObtenerMontosPagoPaquetes(idOportunidad);
            if (tabla == null)
            {
                string precioNormal = string.Empty;
                var task1 = Task.Run(() => ObtenerPrecioContado(idOportunidad));
                var task2 = Task.Run(() => ObtenerPrecioCuotas(idOportunidad));
                string precioContado = await task1;
                string precioCoutas = await task2;

                if (!string.IsNullOrEmpty(precioContado))
                    precioNormal = "<b>Al Contado: </b>" + precioContado;

                if (!string.IsNullOrEmpty(precioCoutas))
                    precioNormal += "<div style=' height:1px;'></div>" + "<b>Financiamiento: </b>" + precioCoutas;

                return precioNormal;
            }
            else
                return tabla;
        }
        private string ObtenerMontosPagoPaquetes(int idOportunidad)
        {
            var versiones = _unitOfWork.MontoPagoRepository.ObtenerVersionesMontoPago(idOportunidad);

            var agrupado = (from x in versiones
                            orderby x.OrdenBeneficio
                            group x by new
                            {
                                x.Paquete,
                                x.tp_nombre,
                                x.tp_cuotas,
                                x.mp_precio,
                                x.Simbolo,
                                x.mp_matricula,
                                x.mp_nro_cuotas,
                                x.mp_cuotas
                            }
                into gj
                            select new MontoPagoEtiquetaAgrupadoDTO
                            {
                                Paquete = gj.Key.Paquete,
                                tp_nombre = gj.Key.tp_nombre,
                                tp_cuotas = gj.Key.tp_cuotas,
                                mp_precio = gj.Key.mp_precio,
                                Simbolo = gj.Key.Simbolo,
                                mp_matricula = gj.Key.mp_matricula,
                                mp_nro_cuotas = gj.Key.mp_nro_cuotas,
                                mp_cuotas = gj.Key.mp_cuotas,
                                Beneficios = gj.Select(x => x.Titulo).ToList()
                            }).ToList();
            if (agrupado.Count() == 0)
            {
                return null;
            }
            else
            {
                string tabla = "";
                tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";
                foreach (var re in agrupado)
                {
                    var credito = agrupado.Where(s => s.Paquete == re.Paquete && s.tp_cuotas != 2).FirstOrDefault(); //add roy
                    tabla += "<tr>";
                    tabla += "<td style='border: 1px solid #E6E6E6' >" + ObtenerPaquete(re.Paquete == null ? "" : re.Paquete.ToString()) + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6'> " + string.Join(", ", re.Beneficios.Distinct()) + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString() /*+ (credito == null? "<br /><span style='color:red;'>" + "Con 25% de descuento:" + re.Simbolo.Replace(".", " ") + Math.Round(((re.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>" : "<br /><span style='color:red;'>" + "Con 25% de descuento:" + credito.Simbolo.Replace(".", " ") + Math.Round(((credito.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>")*///descomnetar para 25 % descuento
                            : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                            + "</td>";
                    tabla += "</tr>";

                }
                tabla += "</TABLE>";
                return tabla;
            }
        }
        private string ObtenerPaquete(string val)
        {
            switch (val)
            {
                case "1":
                    return "Versión Basica";
                case "2":
                    return "Versión Profesional";
                case "3":
                    return "Versión Gerencial";
                default:
                    return "";
            }
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
            var servicioVersionPrograma = new VersionProgramaService(_unitOfWork);
            var servicioBeneficioPGeneral = new ConfiguracionBeneficioProgramaGeneralService(_unitOfWork);
            var servicioPGeneralDocumento = new PGeneralDocumentoPwService(_unitOfWork);

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

                var pieBeneficio = servicioPGeneralDocumento.ObtenerSeccionDocumentoPGeneral(argumentos.IdPGeneral.Value, "Beneficios");

                if (pieBeneficio != null)
                    tabla += $"<p>{pieBeneficio.PiePagina}</p>";

                if (contadorBeneficios == 0)
                    tabla = null;

                return new StringDTO() { Valor = tabla ?? "" };
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 08/08/2022
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

                string precio_contado = ObtenerPrecioContado(argumentos.IdOportunidad.Value);
                string precio_coutas = ObtenerPrecioCuotas(argumentos.IdOportunidad.Value);

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
                    tablaRespuesta.Valor = GeneraHtmlPrecioCuotas(data);
                    break;
                case 1: //matricula
                    tablaRespuesta.Valor = GeneraHtml(GenerarGridMatricula(data));
                    break;
                case 2: //cuotas
                    tablaRespuesta.Valor = GeneraHtmlPrecioCuotas(data);
                    break;
                case 3: //ambos
                    tablaRespuesta.Valor = GeneraHtml(GenerarGridAmbos(data));
                    break;
                case 4: //general
                    tablaRespuesta.Valor = GeneraHtml(GenerarGridGeneral(data));
                    break;
                case 5:
                    tablaRespuesta.Valor = GeneraHtmlPrecioContado(GenerarGridNormal(data));
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
        public string GeneraHtmlPrecioCuotas(MontoPagoCompuestoDTO data)
        {
            string tabla = "";
            string moneda = "";
            var respuesta = _unitOfWork.MonedaRepository.ObtenerMonedaParaDocumento(data.mp_moneda);

            if (respuesta != null)
                moneda = respuesta.Simbolo;

            if (data != null && !string.IsNullOrEmpty(moneda))
            {
                tabla = "1 Matricula de " + moneda.Replace(".", " ") + " " + data.mp_matricula + " y " + data.mp_nro_cuotas + " cuotas de " + moneda + " " + data.mp_cuotas;
            }
            return tabla;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Grid Matricula
        /// </summary>
        /// <param name="data">Monto Pago</param>
        /// <returns> ValorStringDTO </returns>
        private List<PagoCuotaDTO> GenerarGridMatricula(MontoPagoCompuestoDTO data)
        {
            var tamanioMatricula = data.tp_fracciones_matricula;
            if (tamanioMatricula == 0) tamanioMatricula = 1;
            var listaPagoCuota = new List<PagoCuotaDTO>();
            var contadorNumeroCuota = 0;
            for (var j = 0; j < tamanioMatricula; j++)
            {
                contadorNumeroCuota++;
                var fechaActual = DateTime.Now;
                var obj = new PagoCuotaDTO()
                {
                    numeroCuota = contadorNumeroCuota,
                    cuotaDescripcion = $"Matricula {contadorNumeroCuota}",
                    montoCuota = data.mp_matricula,
                    fechapago = fechaActual,
                    montoCuotaDescuento = TipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula),
                    ispagado = false,
                    es_matricula = true,
                };
                listaPagoCuota.Add(obj);
            }
            //Cuotas
            var tamanio = data.mp_nro_cuotas;
            var contadorTamanio = 0;
            for (var i = 0; i < tamanio; i++)
            {
                contadorNumeroCuota++;
                contadorTamanio++;
                var fechaPago = CalcularFechaInicial(data, i);
                var montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                var obj = new PagoCuotaDTO()
                {
                    numeroCuota = contadorNumeroCuota,
                    cuotaDescripcion = "Cuota - " + (contadorNumeroCuota),
                    montoCuota = data.mp_cuotas,
                    montoCuotaDescuento = montoCuotaDescuento,
                    ispagado = false,
                    es_matricula = false,
                    fechapago = fechaPago
                };
                listaPagoCuota.Add(obj);

                if (contadorTamanio != tamanio)
                {
                    var mes = fechaPago.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        contadorNumeroCuota++;
                        var obj1 = new PagoCuotaDTO()
                        {
                            numeroCuota = contadorNumeroCuota,
                            cuotaDescripcion = "Cuota - " + contadorNumeroCuota,
                            montoCuota = data.mp_cuotas,
                            montoCuotaDescuento = montoCuotaDescuento,
                            fechapago = fechaPago,
                            es_matricula = false,
                        };
                        listaPagoCuota.Add(obj1);
                        tamanio--;
                    }
                }
            }
            return listaPagoCuota;
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
        private float TipoDescuentoGeneral(double? va, int des)
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
        private DateTime CalcularFechaInicial(MontoPagoCompuestoDTO obj, int i)
        {
            var fechaActual = DateTime.Now;
            int dia = 0;
            if (!string.IsNullOrEmpty(obj.mp_vencimiento))
                dia = int.Parse(obj.mp_vencimiento);
            else
                fechaActual.AddDays(1);

            if (obj.mp_primeraCuota != null)
                fechaActual = ObtenerPrimeraFecha(obj.mp_primeraCuota, dia);

            if (dia < 29)
                fechaActual = fechaActual.AddDays(dia);
            else
                fechaActual = fechaActual.AddDays(28);

            return fechaActual.AddMonths(i);
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene primera Fecha
        /// </summary>
        /// <param name="montName">Monto Pago primera Cuota</param>
        /// <param name="diaInicio"></param>
        /// <returns> DateTime </returns>
        private DateTime ObtenerPrimeraFecha(string montName, int diaInicio)
        {
            DateTime res = new DateTime();
            string[] ssize = montName.Split(new char[0]);
            string[] monthNames = new CultureInfo("en-US").DateTimeFormat.MonthNames;
            for (int i = 0; i <= monthNames.Count() - 1; i++)
            {
                if (ssize[0].Equals(monthNames[i]))
                {
                    int tmp = i + 1;
                    string tmpp;
                    if (tmp < 10)
                        tmpp = "0" + tmp.ToString();
                    else
                        tmpp = tmp.ToString();

                    string tmppdia;
                    if (diaInicio < 10)
                        tmppdia = "0" + diaInicio.ToString();
                    else
                        tmppdia = diaInicio.ToString();

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
        /// Genera una tabla con las cuotas de Pago
        /// </summary>
        /// <param name="listaPagoCuota">campos de Cuotas Para Generar tabla</param>
        /// <returns> string </returns>
        public string GeneraHtml(List<PagoCuotaDTO> listaPagoCuota)
        {
            string tabla = "";
            tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
            tabla += "<tr>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
            tabla += "</tr>";
            foreach (var pagoCuota in listaPagoCuota)
            {
                tabla += "<tr>";
                tabla += "<td style='border: 1px solid #E6E6E6' >" + pagoCuota.cuotaDescripcion + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6'> " + pagoCuota.fechapago.ToShortDateString() + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + pagoCuota.montoCuotaDescuento + "</td>";
                tabla += "</tr>";
            }
            tabla += "</TABLE>";
            return tabla;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla de monto Pago pa Contado y Credito
        /// </summary>
        /// <param name="data">datos Compuestos de MontoPago</param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridAmbos(MontoPagoCompuestoDTO data)
        {
            var contadorNroCuota = 0;
            List<PagoCuotaDTO> listaPagoCuota = new List<PagoCuotaDTO>();
            //Matriculas
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;

            if (tamanioMatricula == 0) tamanioMatricula = 1;
            for (var j = 0; j < tamanioMatricula; j++)
            {
                contadorNroCuota++;
                var fechaActual = DateTime.Now;
                PagoCuotaDTO obj = new PagoCuotaDTO()
                {
                    numeroCuota = contadorNroCuota,
                    cuotaDescripcion = "Matricula " + contadorNroCuota,
                    montoCuota = data.mp_matricula,
                    fechapago = fechaActual,
                    montoCuotaDescuento = TipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula),
                    ispagado = false,
                    es_matricula = true
                };
                listaPagoCuota.Add(obj);
            }

            //Cuotas
            var tamanioContador = 0;
            var tamanio = data.mp_nro_cuotas + data.tp_cuotas_adicionales;
            var tamanioCuotas = tamanio;
            var sinDescuento = data.mp_precio - data.mp_matricula;

            for (var i = 0; i < tamanio; i++)
            {
                contadorNroCuota++;
                tamanioContador++;
                var fechaInicial = CalcularFechaInicial(data, i);
                var montoCuotaDescuento = TipoDescuentoGeneral(sinDescuento / tamanioCuotas, data.tp_porcentaje_cuotas);
                var pagoCuota = new PagoCuotaDTO()
                {
                    numeroCuota = contadorNroCuota,
                    cuotaDescripcion = "Cuota - " + (contadorNroCuota),
                    montoCuota = data.mp_cuotas,
                    montoCuotaDescuento = montoCuotaDescuento,
                    ispagado = false,
                    es_matricula = false,
                    fechapago = fechaInicial
                };
                listaPagoCuota.Add(pagoCuota);

                if (tamanioContador != tamanio)
                {
                    var mes = fechaInicial.Month + 1;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        contadorNroCuota++;
                        var pagoCuota1 = new PagoCuotaDTO()
                        {
                            numeroCuota = contadorNroCuota,
                            cuotaDescripcion = "Cuota - " + (contadorNroCuota),
                            montoCuota = data.mp_cuotas,
                            montoCuotaDescuento = montoCuotaDescuento,
                            fechapago = fechaInicial,
                            es_matricula = false
                        };
                        listaPagoCuota.Add(pagoCuota1);
                        tamanio--;
                    }
                }
            }
            return listaPagoCuota;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla de Cuotas de Pago de un Programa
        /// </summary>
        /// <param name="data"></param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridGeneral(MontoPagoCompuestoDTO data)
        {
            string test = "";
            var tamanioContador = 0;
            var tamanio = data.mp_nro_cuotas;
            var contadorNroCuota = 1;
            var listaPagoCuota = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO()
            {
                numeroCuota = contadorNroCuota,
                cuotaDescripcion = "Matricula ",
                montoCuota = data.mp_matricula,
                fechapago = DateTime.Now.AddMonths(1),
                montoCuotaDescuento = TipoDescuentoGeneral(data.mp_matricula, data.tp_porcentaje_general),
                ispagado = false,
                es_matricula = true
            };
            listaPagoCuota.Add(obj);

            for (var i = 0; i < tamanio; i++)
            {
                contadorNroCuota++;
                tamanioContador++;
                var fechaInicial = CalcularFechaInicial(data, i);
                var montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                var pagoCuota1 = new PagoCuotaDTO()
                {
                    numeroCuota = contadorNroCuota,
                    cuotaDescripcion = "Cuota - " + (contadorNroCuota - 1),
                    montoCuota = data.mp_cuotas,
                    montoCuotaDescuento = montoCuotaDescuento,
                    ispagado = false,
                    es_matricula = false,
                    fechapago = fechaInicial
                };
                listaPagoCuota.Add(pagoCuota1);
                if (tamanioContador != tamanio)
                {
                    var mes = fechaInicial.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        contadorNroCuota++;
                        var pagoCuota2 = new PagoCuotaDTO()
                        {
                            numeroCuota = contadorNroCuota,
                            cuotaDescripcion = "Cuota - " + (contadorNroCuota - 1),
                            montoCuota = data.mp_cuotas,
                            montoCuotaDescuento = montoCuotaDescuento,
                            es_matricula = false,
                            fechapago = fechaInicial,
                        };
                        listaPagoCuota.Add(pagoCuota2);
                        tamanio--;
                    }
                }
            }
            return listaPagoCuota;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Html del Precio Contado
        /// </summary>
        /// <param name="listaPagoCuota"></param>
        /// <returns> string </returns>
        private string GeneraHtmlPrecioContado(List<PagoCuotaDTO> listaPagoCuota)
        {
            string tabla = listaPagoCuota[0].SimboloMoneda.Replace(".", " ") + " " + listaPagoCuota[0].montoCuota.ToString();
            return tabla;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Grid Normal
        /// </summary>
        /// <param name="montoPagoCompuesto"></param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridNormal(MontoPagoCompuestoDTO montoPagoCompuesto)
        {
            string simbolo = "";
            var respuesta = _unitOfWork.MonedaRepository.ObtenerMonedaParaDocumento(montoPagoCompuesto.mp_moneda);

            if (respuesta != null)
                simbolo = respuesta.Simbolo;

            var listaPagoCuota = new List<PagoCuotaDTO>();
            var pagoCuota = new PagoCuotaDTO()
            {
                numeroCuota = 1,
                cuotaDescripcion = "Contado",
                montoCuota = montoPagoCompuesto.mp_cuotas,
                fechapago = DateTime.Now,
                montoCuotaDescuento = float.Parse(montoPagoCompuesto.mp_cuotas.ToString()),
                ispagado = false,
                es_matricula = true,
                SimboloMoneda = simbolo
            };
            listaPagoCuota.Add(pagoCuota);
            return listaPagoCuota;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una Tabla Para el Precio al Contado de un Programa 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        public string ObtenerPrecioContado(int idOportunidad)
        {
            string gridCronogramaPago = "";
            var contado = _unitOfWork.MontoPagoRepository.ObtenerMontoPagoContadoPorIdOportunidad(idOportunidad);
            if (contado != null)
                gridCronogramaPago = GenerarGridCronogramaPago(contado).Valor;
            return gridCronogramaPago;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 12/04/2023
        /// Version: 1.0
        /// <summary>
        /// Genera una Tabla Para el Precio al Contado de un Programa 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        public async Task<string> ObtenerPrecioContadoAsync(int idOportunidad)
        {
            string gridCronogramaPago = "";
            var contado = await _unitOfWork.MontoPagoRepository.ObtenerMontoPagoContadoPorIdOportunidadAsync(idOportunidad);
            if (contado != null)
                gridCronogramaPago = GenerarGridCronogramaPago(contado).Valor;
            return gridCronogramaPago;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        private string ObtenerPrecioCuotas(int idOportunidad)
        {
            string gridCronogramaPago = "";
            var servicioMontoPago = new MontoPagoService(_unitOfWork);
            var montopagoCuotas = servicioMontoPago.ObtenerMontoPagoPorIdOportunidadParaTabla(idOportunidad);
            if (montopagoCuotas != null)
                gridCronogramaPago = GenerarGridCronogramaPago(montopagoCuotas).Valor;
            return gridCronogramaPago;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Cronograma de Cuotas Para Speech
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        private string GenerarCronograma(int idOportunidad)
        {
            string cronogramaHtml = "";
            var montoPagoCronograma = _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidad(idOportunidad);

            if (montoPagoCronograma != null && montoPagoCronograma.Id != 0)
            {
                if (montoPagoCronograma.Formula != 5)
                {
                    var cuotas = _unitOfWork.MontoPagoRepository.ObtenerPorIdOportunidadV2(idOportunidad).ToList();
                    var rpt = ObtenerCronogramaDetalleValidado(cuotas, idOportunidad);
                    cronogramaHtml = GeneraCronogramaHtml(rpt);
                }
            }
            return cronogramaHtml;
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Cronograma de Cuotas Para Speech
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> string </returns>
        private async Task<string> GenerarCronogramaAsync(int idOportunidad)
        {
            string cronogramaHtml = "";
            var montoPagoCronograma = await _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidadAsync(idOportunidad);

            if (montoPagoCronograma != null && montoPagoCronograma.Id != 0)
            {
                if (montoPagoCronograma.Formula != 5)
                {
                    var taskMontoPago = await _unitOfWork.MontoPagoRepository.ObtenerPorIdOportunidadV2Async(idOportunidad);
                    var cuotas = taskMontoPago.ToList();
                    var rpt = await ObtenerCronogramaDetalleValidadoAsync(cuotas, idOportunidad);
                    cronogramaHtml = GeneraCronogramaHtml(rpt);
                }
            }
            return cronogramaHtml;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Detalle de Cronogrma Para Cuotas Specch
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> MontoPagoCronogramasDetalleCompuestoDTO </returns>
        private MontoPagoCronogramasDetalleCompuestoDTO? ObtenerCronogramaDetalleValidado(List<MontoPagoCronogramaCompuestoDTO> cuotas, int idOportunidad)
        {

            MontoPagoCronograma montoPago = _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidad(idOportunidad);
            TipoDescuento tipoDescuento = _unitOfWork.TipoDescuentoRepository.ObtenerPorId(montoPago.IdTipoDescuento.GetValueOrDefault());
            MontoPagoCronogramaCompuestoDTO cuota = cuotas.Where(x => x.Id == montoPago.IdMontoPago).FirstOrDefault();

            MontoPagoCronogramaCompuestoDTO montoPagoCompuesto = new MontoPagoCronogramaCompuestoDTO();
            if (cuota != null)
            {
                montoPagoCompuesto.Id = cuota.Id;
                montoPagoCompuesto.mp_precio = cuota.mp_precio;
                montoPagoCompuesto.mp_precio_letras = cuota.mp_precio_letras;
                montoPagoCompuesto.mp_moneda = cuota.mp_moneda.ToString();
                montoPagoCompuesto.mp_matricula = cuota.mp_matricula;
                montoPagoCompuesto.mp_cuotas = cuota.mp_cuotas;
                montoPagoCompuesto.mp_nro_cuotas = cuota.mp_nro_cuotas;
                montoPagoCompuesto.id_programa = cuota.id_programa;
                montoPagoCompuesto.id_tp = cuota.id_tp;
                montoPagoCompuesto.id_pais = cuota.id_pais;
                montoPagoCompuesto.mp_vencimiento = cuota.mp_vencimiento;
                montoPagoCompuesto.mp_primeraCuota = cuota.mp_primeraCuota;
                montoPagoCompuesto.mp_cuotaDoble = cuota.mp_cuotaDoble;
                montoPagoCompuesto.id_tipo_descuento = montoPago.IdTipoDescuento.Value;
                montoPagoCompuesto.mp_precioDescuento = montoPago.PrecioDescuento;
                montoPagoCompuesto.id_cronograma = montoPago.Id;
                montoPagoCompuesto.is_aprobado = montoPago.EsAprobado;
                montoPagoCompuesto.NombrePlural = montoPago.NombrePlural;
                montoPagoCompuesto.tp_formula = tipoDescuento.Formula;
                montoPagoCompuesto.tp_porcentaje_general = tipoDescuento.PorcentajeGeneral.Value;
                montoPagoCompuesto.tp_porcentaje_matricula = tipoDescuento.PorcentajeMatricula.Value;
                montoPagoCompuesto.tp_fracciones_matricula = tipoDescuento.FraccionesMatricula.Value;
                montoPagoCompuesto.tp_porcentaje_cuotas = tipoDescuento.PorcentajeCuotas.Value;
                montoPagoCompuesto.tp_cuotas_adicionales = tipoDescuento.CuotasAdicionales.Value;
                montoPagoCompuesto.matriculaEnProceso = montoPago.MatriculaEnProceso;
                montoPagoCompuesto.Simbolo = cuota.Simbolo;
                montoPagoCompuesto.CodigoMatricula = montoPago.CodigoMatricula;
            }

            if (montoPagoCompuesto != null && montoPagoCompuesto.Id != 0)
            {
                var Lista = _unitOfWork.MontoPagoCronogramaDetalleRepository.ObtenerMontoPagoCronogramaDetallePorIdCronograma(montoPagoCompuesto.id_cronograma).Where(m => !m.Pagado).OrderBy(x => x.NumeroCuota).ToList();

                var listaDetalle = (from item in Lista
                                    select new MontoPagoCronogramaDetalleValidoDTO
                                    {
                                        Id = item.Id,
                                        NumeroCuota = item.NumeroCuota,
                                        CuotaDescripcion = item.CuotaDescripcion,
                                        MontoCuota = item.MontoCuota,
                                        FechaPago = item.FechaPago,
                                        MontoCuotaDescuento = item.MontoCuotaDescuento,
                                        Pagado = item.Pagado,
                                        Matricula = item.Matricula,
                                        Cronograma = item.IdMontoPagoCronograma.ToString()
                                    }).ToList();

                return new MontoPagoCronogramasDetalleCompuestoDTO()
                {
                    cronograma = montoPagoCompuesto,
                    listaDetalle = listaDetalle
                };
            }
            return null;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Detalle de Cronogrma Para Cuotas Specch
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> MontoPagoCronogramasDetalleCompuestoDTO </returns>
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera Detalle de Cronogrma Para Cuotas Specch
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> MontoPagoCronogramasDetalleCompuestoDTO </returns>
        private async Task<MontoPagoCronogramasDetalleCompuestoDTO>? ObtenerCronogramaDetalleValidadoAsync(List<MontoPagoCronogramaCompuestoDTO> cuotas, int idOportunidad)
        {

            MontoPagoCronograma montoPago = await _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidadAsync(idOportunidad);
            TipoDescuento tipoDescuento = await _unitOfWork.TipoDescuentoRepository.ObtenerPorIdAsync(montoPago.IdTipoDescuento.GetValueOrDefault());
            var cuota = cuotas.Where(x => x.Id == montoPago.IdMontoPago).FirstOrDefault();
            List<MontoPagoCronograma> v = new List<MontoPagoCronograma>();

            var montoPagoCompuesto = new MontoPagoCronogramaCompuestoDTO
            {
                Id = cuota.Id,
                mp_precio = cuota.mp_precio,
                mp_precio_letras = cuota.mp_precio_letras,
                mp_moneda = cuota.mp_moneda.ToString(),
                mp_matricula = cuota.mp_matricula,
                mp_cuotas = cuota.mp_cuotas,
                mp_nro_cuotas = cuota.mp_nro_cuotas,
                id_programa = cuota.id_programa,
                id_tp = cuota.id_tp,
                id_pais = cuota.id_pais,
                mp_vencimiento = cuota.mp_vencimiento,
                mp_primeraCuota = cuota.mp_primeraCuota,
                mp_cuotaDoble = cuota.mp_cuotaDoble,
                id_tipo_descuento = montoPago.IdTipoDescuento.Value,
                mp_precioDescuento = montoPago.PrecioDescuento,
                id_cronograma = montoPago.Id,
                is_aprobado = montoPago.EsAprobado,
                NombrePlural = montoPago.NombrePlural,
                tp_formula = tipoDescuento.Formula,
                tp_porcentaje_general = tipoDescuento.PorcentajeGeneral.Value,
                tp_porcentaje_matricula = tipoDescuento.PorcentajeMatricula.Value,
                tp_fracciones_matricula = tipoDescuento.FraccionesMatricula.Value,
                tp_porcentaje_cuotas = tipoDescuento.PorcentajeCuotas.Value,
                tp_cuotas_adicionales = tipoDescuento.CuotasAdicionales.Value,
                matriculaEnProceso = montoPago.MatriculaEnProceso,
                Simbolo = cuota.Simbolo,
                CodigoMatricula = montoPago.CodigoMatricula
            };

            if (montoPagoCompuesto != null && montoPagoCompuesto.Id != 0)
            {
                var taskMontoCronograma = await _unitOfWork.MontoPagoCronogramaDetalleRepository.ObtenerMontoPagoCronogramaDetallePorIdCronogramaAsync(montoPagoCompuesto.id_cronograma);
                var Lista = taskMontoCronograma.Where(m => !m.Pagado).OrderBy(x => x.NumeroCuota).ToList();

                var listaDetalle = (from item in Lista
                                    select new MontoPagoCronogramaDetalleValidoDTO
                                    {
                                        Id = item.Id,
                                        NumeroCuota = item.NumeroCuota,
                                        CuotaDescripcion = item.CuotaDescripcion,
                                        MontoCuota = item.MontoCuota,
                                        FechaPago = item.FechaPago,
                                        MontoCuotaDescuento = item.MontoCuotaDescuento,
                                        Pagado = item.Pagado,
                                        Matricula = item.Matricula,
                                        Cronograma = item.IdMontoPagoCronograma.ToString()
                                    }).ToList();

                return new MontoPagoCronogramasDetalleCompuestoDTO()
                {
                    cronograma = montoPagoCompuesto,
                    listaDetalle = listaDetalle
                };
            }
            return null;
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera HTML de Cronograma
        /// </summary>
        /// <param name="Lista">Lista de MontoPago Detalle Compuesto</param>
        /// <returns> string </returns>
        private string GeneraCronogramaHtml(MontoPagoCronogramasDetalleCompuestoDTO Lista)
        {
            string tabla = "";

            string moneda = string.Empty;

            if (Lista != null)
            {
                if (!(string.IsNullOrEmpty(Lista.cronograma.NombrePlural)))
                {
                    switch (Lista.cronograma.NombrePlural.ToLower())
                    {
                        case "soles":
                            moneda = "S/. ";
                            break;
                        case "dolares":
                            moneda = "U$S ";
                            break;
                        case "pesos":
                            moneda = "COL $ ";
                            break;
                        default:
                            moneda = "";
                            break;
                    }
                }
                else
                {
                    moneda = "";
                }

                tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
                tabla += "</tr>";

                foreach (var re in Lista.listaDetalle)
                {
                    tabla += "<tr>";
                    tabla += "<td style='border: 1px solid #E6E6E6' >" + re.CuotaDescripcion + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6'> " + re.FechaPago.ToString("dd/MM/yyyy") + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + moneda + re.MontoCuotaDescuento + "</td>";
                    tabla += "</tr>";
                }
                tabla += "</TABLE>";
            }
            return tabla;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion"> Lista de programas por documento sección </param>
        /// <param name="conTitulo"> Validación de Título </param>
        /// <returns> List<ProgramaGeneralSeccionAnexosHTMLDTO> </returns>
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion, bool conTitulo)
        {
            try
            {
                var lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

                foreach (var item in listaProgramaGeneralDocumentoSeccion)
                {
                    string contenido = string.Empty;
                    conTitulo = item.Seccion == "Estructura Curricular";

                    foreach (var detalleSeccion in item.DetalleSeccion)
                    {
                        contenido += conTitulo ? $"<p><strong>{detalleSeccion.Titulo}</strong></p>" : string.Empty;

                        contenido += (detalleSeccion.Cabecera != string.Empty) ? $"<p>{detalleSeccion.Cabecera}</p><ul>" : "<ul>";
                        contenido = detalleSeccion.DetalleContenido.Aggregate(contenido, (current, contenidoSeccion) => current + "<li>" + contenidoSeccion + "</li>");
                        contenido += (detalleSeccion.PiePagina != string.Empty) ? $"</ul><p>{detalleSeccion.PiePagina}</p>" : "</ul>";
                    }

                    lista.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = item.Seccion,
                        Contenido = contenido
                    });
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio Rodrigo
        /// Fecha: 02/02/2022
        /// Version: 1.0
        /// <summary>
        /// Limpia la cadena de caracteres html y retiras las tildes de la cadena
        /// </summary>
        /// <returns>Cadena limpia</returns>
        private string LimpiarCadena(string valor)
        {
            string decodeString = HttpUtility.HtmlDecode(valor);
            string valorSinTildes = Regex.Replace(decodeString.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            return valorSinTildes;
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las etiquetas de V2 caso contrario devuelve V1 mediante el idCentroCosto
        /// </summary>
        /// <param name="idCentroCosto"> Id de Centro Costo </param>
        /// <returns> List<ProgramaGeneralSeccionAnexosHTMLDTO> </returns>
        public List<ClaveValorDTO> ObtenerTemplatesV2ReemplazoEtiqueta(int idCentroCosto)
        {
            const string ETIQUETAVACIO = "<vacio></vacio>";

            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);
            var servicioDocumentoSeccion = new DocumentoSeccionPwService(_unitOfWork);
            var servicioEtiqueta = new EtiquetaService(_unitOfWork);

            var listaResultado = new List<ClaveValorDTO>();
            string valor = string.Empty;

            var listaetiquetasV2 = servicioEtiqueta.ObtenerPorIdNodoPadre(ValorEstatico.IdPlantillaMaestroTemplateV2);
            int idPGeneral = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto).IdProgramaGeneral.GetValueOrDefault();
            var listaSecciones = servicioDocumentoAgenda.ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral);
            var listaSeccionesDocumentoV2 = servicioDocumentoSeccion.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);

            // Considera programa tecnico
            var ProgramaPadre = _unitOfWork.PGeneralRepository.ProgramaGeneralPadre(idPGeneral);
            var programaTecnico = _unitOfWork.PGeneralRepository.ProgramaGeneralEsTecnico(idPGeneral);

            string contenidoStructuraTecnico = "";
            if (programaTecnico)
            {
                if (ProgramaPadre)
                {
                    var listaCursosHijo = _unitOfWork.PGeneralRepository.ListaCursosHijoPorIdPGeneral(idPGeneral);
                    foreach (var item in listaCursosHijo)
                    {

                        contenidoStructuraTecnico += "<h5><strong>" + item.Curso + "</strong></h5>";
                        var duracionCurso = _unitOfWork.PGeneralRepository.ObtenerDuracionCursoHijo(item.IdHijo);
                        var CursosHijo = _unitOfWork.PGeneralRepository.ContenidoEstructuraHijoPadre(item.IdHijo);

                        contenidoStructuraTecnico += "<ul type='disc'>";
                        var listaContenidoCurso = CursosHijo.GroupBy(x => x.Contenido).Select(x => x.First()).ToList();
                        foreach (var contenidoCurso in listaContenidoCurso)
                        {
                            contenidoStructuraTecnico += "<li>&nbsp;&nbsp;&nbsp;" + contenidoCurso.Contenido + "</li>";
                        }
                        contenidoStructuraTecnico += "</ul>";
                    }
                }
            }
            var estructuraTecnico = contenidoStructuraTecnico;


            foreach (var item in listaetiquetasV2)
            {
                valor = string.Empty;

                string[] array = item.Nombre.Split(".");
                string nombreSeccion = array[array.Length - 1];
                bool conTitulo = nombreSeccion == "Estructura Curricular";
                string descripcionAdicional = string.Concat("Descripci&#243;n ", nombreSeccion.Split(" ")[0]);

                var seccion = servicioDocumentoSeccion.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                // Unir Descripcion adicional de etiquetas que tienen dicho contenido, previa verificacion
                if (listaSeccionesDocumentoV2.Exists(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(descripcionAdicional).ToLower()))
                {
                    string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(descripcionAdicional).ToLower()).Contenido;

                    valor += descripcion != ETIQUETAVACIO ? descripcion.Replace(ETIQUETAVACIO, string.Empty) : string.Empty;
                }

                // Sacar etiquetas no agrupadas de V2
                try
                {
                    valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).Contenido : string.Empty;
                }
                catch (Exception e)
                {
                    valor += string.Empty;
                }

                // Obtener etiquetas de V1 si en caso no encuentra
                if (valor.Equals(string.Empty))
                {
                    nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                    List<SeccionDocumentoDTO> seccionV1 = servicioDocumentoSeccion.ObtenerSecciones(idPGeneral).Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).ToList();

                    valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                }

                if (programaTecnico && estructuraTecnico != null && estructuraTecnico != "" && item.Nombre == "Estructura Curricular")
                {
                    valor = estructuraTecnico;
                }

                listaResultado.Add(new ClaveValorDTO()
                {
                    Clave = item.Nombre,
                    Valor = valor
                });
            }
            return listaResultado;
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las etiquetas de V2 caso contrario devuelve V1 mediante el idCentroCosto
        /// </summary>
        /// <param name="idCentroCosto"> Id de Centro Costo </param>
        /// <returns> List<ProgramaGeneralSeccionAnexosHTMLDTO> </returns>
        public async Task<List<ClaveValorDTO>> ObtenerTemplatesV2ReemplazoEtiquetaAsync(int idCentroCosto)
        {
            const string ETIQUETAVACIO = "<vacio></vacio>";

            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);
            var servicioDocumentoSeccion = new DocumentoSeccionPwService(_unitOfWork);

            var listaResultado = new List<ClaveValorDTO>();
            string valor = string.Empty;

            var task1 = _unitOfWork.EtiquetaRepository.ObtenerPorIdNodoPadreAsync(ValorEstatico.IdPlantillaMaestroTemplateV2);
            int idPGeneral = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto).IdProgramaGeneral.GetValueOrDefault();
            var task2 = Task.Run(() => servicioDocumentoAgenda.ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral));
            var task3 = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2Async(idPGeneral);

            // Considera programa tecnico
            var taskProgramaTecnico = _unitOfWork.PGeneralRepository.ProgramaGeneralEsTecnicoAsync(idPGeneral);
            var taskProgramaPadre = _unitOfWork.PGeneralRepository.ProgramaGeneralPadreAsync(idPGeneral);

            string contenidoStructuraTecnico = "";
            var programaTecnico = await taskProgramaTecnico;
            if (programaTecnico)
            {
                var ProgramaPadre = await taskProgramaPadre;
                if (ProgramaPadre)
                {
                    var listaCursosHijo = _unitOfWork.PGeneralRepository.ListaCursosHijoPorIdPGeneral(idPGeneral);
                    foreach (var item in listaCursosHijo)
                    {

                        contenidoStructuraTecnico += "<h5><strong>" + item.Curso + "</strong></h5>";
                        var duracionCurso = _unitOfWork.PGeneralRepository.ObtenerDuracionCursoHijo(item.IdHijo);
                        var CursosHijo = _unitOfWork.PGeneralRepository.ContenidoEstructuraHijoPadre(item.IdHijo);

                        contenidoStructuraTecnico += "<ul type='disc'>";
                        var listaContenidoCurso = CursosHijo.GroupBy(x => x.Contenido).Select(x => x.First()).ToList();
                        foreach (var contenidoCurso in listaContenidoCurso)
                        {
                            contenidoStructuraTecnico += "<li>&nbsp;&nbsp;&nbsp;" + contenidoCurso.Contenido + "</li>";
                        }
                        contenidoStructuraTecnico += "</ul>";
                    }
                }
            }
            var estructuraTecnico = contenidoStructuraTecnico;

            var listaetiquetasV2 = await task1;
            var listaSecciones = await task2;
            var listaSeccionesDocumentoV2 = await task3;

            //llamo y cargo la nueva informacion de duracion y modalidad

            var objetoDuracion = (_unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWDuracionRows(listaSeccionesDocumentoV2.FirstOrDefault().IdDocumentoPW == null ? 0 : listaSeccionesDocumentoV2.FirstOrDefault().IdDocumentoPW.Value) ?? Enumerable.Empty<DocumentoPWDuracionRowVM>()).ToList();
            if (objetoDuracion.Any())
            {
                var first1 = objetoDuracion.First();// de aqui saco titulo , introduccion , pie

                //genero el html

                var nuevohtmlDuracion = "<p><b>" + first1.Titulo + "</b></p>";
                nuevohtmlDuracion += "<p>" + first1.Introduccion + "</p>";

                nuevohtmlDuracion += "<div style='background:#faf5ff;border-radius:12px;padding:20px 24px;'>";
                nuevohtmlDuracion += "<ul>";
                foreach (var item in objetoDuracion)
                {
                    nuevohtmlDuracion += "Version: " + item.IdVersionPrograma == null ? "" : CalcularVersionPorId(item.IdVersionPrograma.Value);
                    nuevohtmlDuracion += "<li> " + item.DetalleMes + "</li>";
                    nuevohtmlDuracion += "<li> " + item.DetalleHora + "</li>";
                }
                nuevohtmlDuracion += "</ul>";
                nuevohtmlDuracion += "</div>";

                nuevohtmlDuracion += "<p><small>" + first1.PieDePagina + "</small></p>";
                listaSeccionesDocumentoV2.Where(w => w.Titulo == "Duración y Horarios").FirstOrDefault().Contenido = nuevohtmlDuracion;

            }







            var objetoModalidad = (_unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWModalidadRows(listaSeccionesDocumentoV2.FirstOrDefault().IdDocumentoPW == null ? 0 : listaSeccionesDocumentoV2.FirstOrDefault().IdDocumentoPW.Value) ?? Enumerable.Empty<DocumentoPWModalidadRowVM>()).ToList();
            if (objetoModalidad.Any())
            {
                var first2 = objetoModalidad.First();

                var response = new SeccionModalidadHorarioResponseDTO
                {
                    IdDocumentoPw = first2.IdDocumento_PW,
                    Introduccion = first2.Introduccion,
                    Modalidades = objetoModalidad
                        .GroupBy(x => new
                        {
                            x.IdDocumentoPWModalidad,
                            x.IdModalidadPortal,
                            x.SubTitulo,
                            x.Descripcion
                        })
                        .Select(g => new ModalidadHorarioResponseDTO
                        {
                            Id = g.Key.IdDocumentoPWModalidad,
                            IdModalidad = g.Key.IdModalidadPortal,
                            SubTitulo = g.Key.SubTitulo,
                            Descripcion = g.Key.Descripcion,
                            Detalles = g
                                .Where(x => (x.IdDocumentoPWModalidadDetalle ?? 0) > 0)
                                .Select(x => new ModalidadHorarioDetalleResponseDTO
                                {
                                    Id = x.IdDocumentoPWModalidadDetalle ?? 0,
                                    Orden = x.Orden ?? 0,
                                    Tipo = x.Tipo,
                                    IdPais = x.IdPais,
                                    Beneficio = x.Beneficio,
                                    Horario = x.Horario
                                })
                                .OrderBy(x => x.Orden)
                                .ToList()
                        })
                        .ToList()
                };

                //genero el html

                var nuevohtmlModalidad = "<br><p><b>Modalidad</b></p>";
                nuevohtmlModalidad += "<p>" + response.Introduccion + "</p><br>";


                //nuevohtmlModalidad += "<ul>";
                foreach (var item in response.Modalidades)
                {
                    nuevohtmlModalidad += "<p><b>Modalidad: " + item.IdModalidad == null ? "</b></p>" : CalcularModalidadPorId(item.IdModalidad.Value)+ "</b></p>";
                    nuevohtmlModalidad += "<p><small>" + item.SubTitulo + "</small></p>";
                    nuevohtmlModalidad += "<p> " + item.Descripcion + "</p>";


                    nuevohtmlModalidad += "<ul>";
                    //detalles
                    foreach (var hijo in item.Detalles)
                    {
                        nuevohtmlModalidad += "<li><span style='background:#16a34a;width:10px;height:10px;border-radius:50%;margin-top:6px;'> </span> " + hijo.Beneficio + "</li>";
                    }
                    nuevohtmlModalidad += "</ul>";

                }
                //nuevohtmlModalidad += "</ul>";

                listaSeccionesDocumentoV2.Where(w => w.Titulo == "Duración y Horarios").FirstOrDefault().Contenido += nuevohtmlModalidad;
            }

            



            //fin llamo y cargo la nueva informacion de duracion y modalidad



            foreach (var item in listaetiquetasV2)
            {
                valor = string.Empty;

                string[] array = item.Nombre.Split(".");
                string nombreSeccion = array[array.Length - 1];
                bool conTitulo = nombreSeccion == "Estructura Curricular";
                string descripcionAdicional = string.Concat("Descripci&#243;n ", nombreSeccion.Split(" ")[0]);

                var seccion = servicioDocumentoSeccion.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                ////SE COMENTA ESTA LINEA PORQUE YA EN VALOR TIENE EL PIE DE PAGINA Y LA DESCRIPCION EXTRA Y ESTO ESTA CAUSANDO DUPLICIDAD EN CERTIFICACION
                //// Unir Descripcion adicional de etiquetas que tienen dicho contenido, previa verificacion
                //if (listaSeccionesDocumentoV2.Exists(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(descripcionAdicional).ToLower()))
                //{
                //    string descripcion = listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(descripcionAdicional).ToLower()).Contenido;
                //    valor += descripcion != ETIQUETAVACIO ? descripcion.Replace(ETIQUETAVACIO, string.Empty) : string.Empty;
                //}

                // Sacar etiquetas no agrupadas de V2
                try
                {
                    valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.First(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).Contenido : string.Empty;
                }
                catch (Exception e)
                {
                    valor += string.Empty;
                }

                // Obtener etiquetas de V1 si en caso no encuentra
                if (valor.Equals(string.Empty))
                {
                    nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                    List<SeccionDocumentoDTO> seccionV1 = servicioDocumentoSeccion.ObtenerSecciones(idPGeneral).Where(x => LimpiarCadena(x.Titulo).ToLower() == LimpiarCadena(nombreSeccion).ToLower()).ToList();

                    valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                }

                if (programaTecnico && estructuraTecnico != null && estructuraTecnico != "" && item.Nombre == "Estructura Curricular")
                {
                    valor = estructuraTecnico;
                }

                listaResultado.Add(new ClaveValorDTO()
                {
                    Clave = item.Nombre,
                    Valor = valor
                });
            }
            return listaResultado;
        }

        public string CalcularVersionPorId(int version)
        {
            var resultado = "";
            switch (version)
            {
                case 1:
                    resultado = "Basica";
                    break;
                case 2:
                    resultado = "Profesional";
                    break;
                case 3:
                    resultado = "Gerencial";
                    break;
                case 4: // por defecto Sin Version
                    break;
                default: // Optional catch-all if no match is found
                    break;
            }
            return resultado;
        }
        public string CalcularModalidadPorId(int version)
        {
            var resultado = "";
            switch (version)
            {
                case 1:
                    resultado = "Online en Vivo";
                    break;
                case 2:
                    resultado = "Online a tu ritmo";
                    break;
                default: // Optional catch-all if no match is found
                    break;
            }
            return resultado;
        }
        public void ObtenerDatosProgramaGeneral(int idProgramaGeneral)
        {
            var pGeneralService = new PGeneralService(_unitOfWork);
            DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
            DocumentoAgendaService documentoAgendaService = new DocumentoAgendaService(_unitOfWork);
            if (_unitOfWork.PGeneralRepository.Exist(idProgramaGeneral))
            {
                var pGeneral = pGeneralService.ObtenerPorId(idProgramaGeneral);
                _datosOportunidadAlumno.DuracionMesesPGeneral = pGeneral.PwDuracion;
                _datosOportunidadAlumno.NombrePGeneral = pGeneral.Nombre;

                var seccionesAntiguas = documentoSeccionPwService.ObtenerSecciones(idProgramaGeneral);

                List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneral(idProgramaGeneral);
                var seccionEstructura = documentoAgendaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                string anexo1EstructuraCurricular = string.Empty;
                string anexo2Certificacion = string.Empty;

                var estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                try
                {
                    if (estructuraV2 != null)
                    {
                        anexo1EstructuraCurricular = "<strong>" + estructuraV2.Seccion + "</strong><br>";
                        anexo1EstructuraCurricular += estructuraV2.Contenido;
                        anexo1EstructuraCurricular = anexo1EstructuraCurricular.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoEstructuraCurricular = seccionesAntiguas.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();

                        anexo1EstructuraCurricular = string.Empty;
                        if (resultadoEstructuraCurricular != null)
                        {
                            anexo1EstructuraCurricular = "<h2>" + resultadoEstructuraCurricular.Titulo + "</h2>";
                            anexo1EstructuraCurricular += resultadoEstructuraCurricular.Contenido;
                        }
                    }

                    _datosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }
                catch (Exception ex)
                {
                    var resultadoEstructuraCurricular = seccionesAntiguas.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();

                    anexo1EstructuraCurricular = string.Empty;
                    if (resultadoEstructuraCurricular != null)
                    {
                        anexo1EstructuraCurricular = "<h2>" + resultadoEstructuraCurricular.Titulo + "</h2>";
                        anexo1EstructuraCurricular += resultadoEstructuraCurricular.Contenido;
                    }

                    _datosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }


                try
                {
                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        anexo2Certificacion = "<strong>" + certificacionV2.Seccion + "</strong><br>";
                        anexo2Certificacion += certificacionV2.Contenido;
                        anexo2Certificacion = anexo2Certificacion.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoCertificacion = seccionesAntiguas.Where(x => x.Titulo.Contains("Certificaci&#243;n")).FirstOrDefault();

                        anexo2Certificacion = string.Empty;
                        if (resultadoCertificacion != null)
                        {
                            anexo2Certificacion = "<h2>" + resultadoCertificacion.Titulo + "</h2>";
                            anexo2Certificacion += resultadoCertificacion.Contenido;
                        }
                    }

                    _datosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
                catch (Exception ex)
                {
                    var resultadoCertificacion = seccionesAntiguas.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();

                    anexo2Certificacion = string.Empty;
                    if (anexo2Certificacion != null)
                    {
                        anexo2Certificacion = "<h2>" + resultadoCertificacion.Titulo + "</h2>";
                        anexo2Certificacion += resultadoCertificacion.Contenido;
                    }

                    _datosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del Programa General
        /// </summary>
        /// <param name="idProgramaGeneral"> Id del Programa General </param>
        /// <returns>  </returns>
        public async Task ObtenerDatosProgramaGeneralAsync(int idProgramaGeneral)
        {
            DocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
            DocumentoAgendaService documentoAgendaService = new DocumentoAgendaService(_unitOfWork);
            if (_unitOfWork.PGeneralRepository.Exist(idProgramaGeneral))
            {
                PGeneral pGeneral = _unitOfWork.PGeneralRepository.ObtenerPorId(idProgramaGeneral);
                _datosOportunidadAlumno.DuracionMesesPGeneral = pGeneral.PwDuracion;
                _datosOportunidadAlumno.NombrePGeneral = pGeneral.Nombre;


                List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = await documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneralAsync(idProgramaGeneral);
                List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionEstructura = documentoAgendaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                List<SeccionDocumentoDTO> seccionesAntiguas = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionesAsync(idProgramaGeneral); ;
                string anexo1EstructuraCurricular = string.Empty;
                string anexo2Certificacion = string.Empty;

                var estructuraV2 = seccionEstructura.FirstOrDefault(x => x.Seccion.ToLower() == "estructura curricular");
                try
                {
                    if (estructuraV2 != null)
                    {
                        anexo1EstructuraCurricular = $"<strong>{estructuraV2.Seccion}</strong><br>{estructuraV2.Contenido}";
                        anexo1EstructuraCurricular = anexo1EstructuraCurricular.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoEstructuraCurricular = seccionesAntiguas.FirstOrDefault(x => x.Titulo.Contains("Estructura Curricular"));
                        anexo1EstructuraCurricular = string.Empty;
                        if (resultadoEstructuraCurricular != null)
                            anexo1EstructuraCurricular = $"<h2>{resultadoEstructuraCurricular.Titulo}</h2>{resultadoEstructuraCurricular.Contenido}";
                    }
                    _datosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }
                catch (Exception)
                {
                    var resultadoEstructuraCurricular = seccionesAntiguas.FirstOrDefault(x => x.Titulo.Contains("Estructura Curricular"));
                    anexo1EstructuraCurricular = string.Empty;
                    if (resultadoEstructuraCurricular != null)
                        anexo1EstructuraCurricular = $"<h2>{resultadoEstructuraCurricular.Titulo}</h2>{resultadoEstructuraCurricular.Contenido}";
                    _datosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }
                try
                {
                    var certificacionV2 = seccionEstructura.FirstOrDefault(x => LimpiarCadena(x.Seccion.ToLower()) == "certificacion");
                    if (certificacionV2 != null)
                    {
                        anexo2Certificacion = $"<strong>{certificacionV2.Seccion}</strong><br>{certificacionV2.Contenido}";
                        anexo2Certificacion = anexo2Certificacion.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoCertificacion = seccionesAntiguas.FirstOrDefault(x => LimpiarCadena(x.Titulo).ToLower().Contains("certificacion"));

                        anexo2Certificacion = string.Empty;
                        if (resultadoCertificacion != null)
                            anexo2Certificacion = $"<h2>{resultadoCertificacion.Titulo}</h2>{resultadoCertificacion.Contenido}";
                    }
                    _datosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
                catch
                {
                    var resultadoCertificacion = seccionesAntiguas.Where(x => LimpiarCadena(x.Titulo).ToLower().Contains("certificacion")).FirstOrDefault();
                    anexo2Certificacion = string.Empty;
                    if (anexo2Certificacion != null)
                        anexo2Certificacion = $"<h2>{resultadoCertificacion.Titulo}</h2>{resultadoCertificacion.Contenido}";

                    _datosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del Programa General
        /// </summary>
        /// <param name="idProgramaGeneral"> Id del Programa General </param>
        /// <returns>  </returns>
        public OportunidadAlumnoDTO ObtenerDatosProgramaGeneralObservado(int idProgramaGeneral, OportunidadAlumnoDTO datosOportunidadAlumno)
        {
            var servicioPGeneral = new PGeneralService(_unitOfWork);
            var servicioDocumentoSeccion = new DocumentoSeccionPwService(_unitOfWork);
            var servicioDocumentoAgenda = new DocumentoAgendaService(_unitOfWork);

            var pGeneral = servicioPGeneral.ObtenerPGeneralAtributosPrincipalesPorId(idProgramaGeneral);
            if (pGeneral != null && pGeneral.Id > 0)
            {
                datosOportunidadAlumno.DuracionMesesPGeneral = pGeneral.PwDuracion;
                datosOportunidadAlumno.NombrePGeneral = pGeneral.Nombre;

                var seccionesAntiguas = servicioDocumentoSeccion.ObtenerSecciones(idProgramaGeneral);

                List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = servicioDocumentoAgenda.ObtenerListaSeccionDocumentoProgramaGeneral(idProgramaGeneral);
                var seccionEstructura = servicioDocumentoAgenda.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                string anexo1EstructuraCurricular = string.Empty;
                string anexo2Certificacion = string.Empty;

                var estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                try
                {
                    if (estructuraV2 != null)
                    {
                        anexo1EstructuraCurricular = "<strong>" + estructuraV2.Seccion + "</strong><br>";
                        anexo1EstructuraCurricular += estructuraV2.Contenido;
                        anexo1EstructuraCurricular = anexo1EstructuraCurricular.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoEstructuraCurricular = seccionesAntiguas.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();

                        anexo1EstructuraCurricular = string.Empty;
                        if (resultadoEstructuraCurricular != null)
                        {
                            anexo1EstructuraCurricular = "<h2>" + resultadoEstructuraCurricular.Titulo + "</h2>";
                            anexo1EstructuraCurricular += resultadoEstructuraCurricular.Contenido;
                        }
                    }

                    datosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }
                catch (Exception ex)
                {
                    var resultadoEstructuraCurricular = seccionesAntiguas.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();

                    anexo1EstructuraCurricular = string.Empty;
                    if (resultadoEstructuraCurricular != null)
                    {
                        anexo1EstructuraCurricular = "<h2>" + resultadoEstructuraCurricular.Titulo + "</h2>";
                        anexo1EstructuraCurricular += resultadoEstructuraCurricular.Contenido;
                    }

                    datosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }


                try
                {
                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        anexo2Certificacion = "<strong>" + certificacionV2.Seccion + "</strong><br>";
                        anexo2Certificacion += certificacionV2.Contenido;
                        anexo2Certificacion = anexo2Certificacion.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoCertificacion = seccionesAntiguas.Where(x => x.Titulo.Contains("Certificaci&#243;n")).FirstOrDefault();

                        anexo2Certificacion = string.Empty;
                        if (resultadoCertificacion != null)
                        {
                            anexo2Certificacion = "<h2>" + resultadoCertificacion.Titulo + "</h2>";
                            anexo2Certificacion += resultadoCertificacion.Contenido;
                        }
                    }

                    datosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
                catch (Exception ex)
                {
                    var resultadoCertificacion = seccionesAntiguas.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();

                    anexo2Certificacion = string.Empty;
                    if (anexo2Certificacion != null)
                    {
                        anexo2Certificacion = "<h2>" + resultadoCertificacion.Titulo + "</h2>";
                        anexo2Certificacion += resultadoCertificacion.Contenido;
                    }

                    datosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
            }
            return datosOportunidadAlumno;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Fecha de Inicio del Programa.
        /// </summary>
        /// <returns> string </returns>
        public string ObtenerFechaInicioPrograma(int idProgramaGeneral, int idCentroCosto)
        {
            var servicioPEspecifico = new PEspecificoService(_unitOfWork);
            string resp = "";
            var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto);

            if (pEspecifico.Tipo.ToLower() == "online asincronica")
            {
                DateTime fechaAOnline;
                if (DateTime.Now.Day > 25)
                {
                    fechaAOnline = DateTime.Now;
                }
                else
                {
                    fechaAOnline = DateTime.Now.AddDays(5);
                }
                resp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fechaAOnline.ToString("MMMM yyyy"));
            }
            else
            {
                var dato_fecha = _unitOfWork.PEspecificoRepository.FechaProgramaEspecifico(idProgramaGeneral, pEspecifico.Id);

                if (dato_fecha != null)
                {
                    resp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dato_fecha.FechaHoraInicio);
                }
                else
                {
                    resp = "Por definir";
                }
            }
            return resp;
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Fecha de Inicio del Programa.
        /// </summary>
        /// <returns> string </returns>
        public async Task<string> ObtenerFechaInicioProgramaAsync(int idProgramaGeneral, int idCentroCosto)
        {
            var servicioPEspecifico = new PEspecificoService(_unitOfWork);
            string resp;
            var pEspecifico = await _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCostoAsync(idCentroCosto);

            if (pEspecifico.Tipo.ToLower() == "online asincronica")
            {
                DateTime fechaAOnline;
                if (DateTime.Now.Day > 25)
                    fechaAOnline = DateTime.Now;
                else
                    fechaAOnline = DateTime.Now.AddDays(5);

                resp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fechaAOnline.ToString("MMMM yyyy"));
            }
            else
            {
                var dato_fecha = await _unitOfWork.PEspecificoRepository.FechaProgramaEspecificoAsync(idProgramaGeneral, pEspecifico.Id);

                if (dato_fecha != null)
                    resp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dato_fecha.FechaHoraInicio);
                else
                    resp = "Por definir";
            }
            return resp;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Fecha de Inicio del Programa.
        /// </summary>
        /// <returns> PlantillaEmailMandrillDTO </returns>
        public PlantillaEmailMandrillDTO ReemplazarEtiquetas(int idPlantilla, int? idOportunidad)
        {
            try
            {
                if (_datosOportunidadAlumno == null)
                {
                    throw new BadRequestException("No se definio _datosOportunidadAlumno");
                }
                IDocumentoAgendaService documentoAgendaService = new DocumentoAgendaService(_unitOfWork);
                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);
                if (plantilla == null || plantilla.Id == 0)
                {
                    throw new BadRequestException("No existe la plantilla");
                }
                var idPlantillaBase = plantilla.IdPlantillaBase;
                var plantillaBase = _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreo(idPlantilla);

                //Logica asunto
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", _datosOportunidadAlumno.NombrePGeneral);
                }

                if (plantillaBase.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tpla_pgeneral.pw_duracion}", _datosOportunidadAlumno.DuracionMesesPGeneral);
                }

                //Logica cuerpo
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.MontoTotal}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.MontoTotal}", _datosOportunidadAlumno.MontoTotal);
                }

                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", _datosOportunidadAlumno.NombrePGeneral);
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}", _datosOportunidadAlumno.CronogramaPagoCompleto);
                }

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.DiaFechaActual}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.DiaFechaActual}", _datosOportunidadAlumno.DiaFechaActual);
                }

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.NombreMesFechaActual}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.NombreMesFechaActual}", _datosOportunidadAlumno.NombreMesFechaActual);
                }

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.AnioFechaActual}"))
                {
                    var valor = _datosOportunidadAlumno.AnioFechaActual;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.AnioFechaActual}", _datosOportunidadAlumno.AnioFechaActual);
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Version}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Version}", _datosOportunidadAlumno.Version);
                }

                //Datos alumno
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", _datosOportunidadAlumno.OportunidadAlumno.Nombre1);
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NombreCompleto}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NombreCompleto}", _datosOportunidadAlumno.OportunidadAlumno.NombreCompleto);
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NroDocumento}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NroDocumento}", _datosOportunidadAlumno.OportunidadAlumno.NroDocumento);
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.direccion}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.direccion}", _datosOportunidadAlumno.OportunidadAlumno.Direccion);
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombreCiudad}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombreCiudad}", _datosOportunidadAlumno.OportunidadAlumno.NombreCiudad);
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombrePais}"))
                {
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombrePais}", _datosOportunidadAlumno.OportunidadAlumno.NombrePais);
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}"))
                {
                    if (idOportunidad == null || idOportunidad == 0)
                    {
                        throw new BadRequestException("Id Oportunidad nulo o 0");
                    }
                    var idCentroCosto = _unitOfWork.OportunidadRepository.ObtenerIdCentroCostoPorId((int)idOportunidad);
                    if (idCentroCosto == null || idCentroCosto == 0)
                    {
                        throw new BadRequestException($"Oportunidad sin centro de costo: {idOportunidad}");
                    }
                    var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto.Value);

                    var listaSecciones = documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneral(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                    var seccionEstructura = documentoAgendaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                    var estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                    if (estructuraV2 != null)
                    {
                        var valor = "<strong>" + estructuraV2.Seccion + "</strong><br>";
                        valor += estructuraV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                    }
                    else
                    {
                        var valorFinal = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                        var detalle = valorFinal.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();
                        var valor = string.Empty;
                        if (detalle != null)
                        {
                            valor = "<h2>" + detalle.Titulo + "</h2>";
                            valor += detalle.Contenido;
                        }
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    if (idOportunidad == null || idOportunidad == 0)
                    {
                        throw new BadRequestException("Id Oportunidad nulo o 0");
                    }
                    var idCentroCosto = _unitOfWork.OportunidadRepository.ObtenerIdCentroCostoPorId((int)idOportunidad);
                    if (idCentroCosto == null || idCentroCosto == 0)
                    {
                        throw new BadRequestException($"Oportunidad sin centro de costo: {idOportunidad}");
                    }
                    var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto.Value);

                    var listaSecciones = documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneral(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                    var seccionEstructura = documentoAgendaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        var valor = "<strong>" + certificacionV2.Seccion + "</strong><br>";
                        valor += certificacionV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                    }
                    else
                    {
                        var valorFinal = _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                        var detalle = valorFinal.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();
                        var valor = string.Empty;
                        if (detalle != null)
                        {
                            valor = "<h2>" + detalle.Titulo + "</h2>";
                            valor += detalle.Contenido;
                        }
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                    }
                }
                var emailReemplazado = new PlantillaEmailMandrillDTO();
                if (idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = plantillaBase.Asunto;
                    emailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
                return emailReemplazado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PlantillaAsuntoCuerpoDTO _plantillaBase = new();
        private int _idPlantillaBase;
        private void CargarValorEtiquetaCuerpo(string etiqueta, string? valor)
        {
            if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                _plantillaBase.Cuerpo = _plantillaBase.Cuerpo.Replace(etiqueta, valor ?? "");
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Fecha de Inicio del Programa.
        /// </summary>
        /// <returns> PlantillaEmailMandrillDTO </returns>
        public async Task<PlantillaEmailMandrillDTO> ReemplazarEtiquetasAsync(int idPlantilla, int? idOportunidad)
        {
            try
            {
                _plantillaBase = new PlantillaAsuntoCuerpoDTO();
                if (_datosOportunidadAlumno == null)
                {
                    throw new BadRequestException("No se definio _datosOportunidadAlumno");
                }
                IDocumentoAgendaService documentoAgendaService = new DocumentoAgendaService(_unitOfWork);
                var plantilla = await _unitOfWork.PlantillaRepository.ObtenerPorIdAsync(idPlantilla);
                if (plantilla == null || plantilla.Id == 0)
                {
                    throw new BadRequestException("No existe la plantilla");
                }
                _plantillaBase = await _unitOfWork.PlantillaRepository.ObtenerPlantillaCorreoAsync(idPlantilla);
                _idPlantillaBase = plantilla.IdPlantillaBase;

                //Reemplazar Asunto
                if (_plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        _plantillaBase.Asunto = _plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", _datosOportunidadAlumno.NombrePGeneral);
                }

                if (_plantillaBase.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                    CargarValorEtiquetaCuerpo("{tpla_pgeneral.pw_duracion}", _datosOportunidadAlumno.DuracionMesesPGeneral);

                //Logica cuerpo
                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.MontoTotal}"))
                    CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.MontoTotal}", _datosOportunidadAlumno.MontoTotal);

                if (_plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                    CargarValorEtiquetaCuerpo("{tPLA_PGeneral.Nombre}", _datosOportunidadAlumno.NombrePGeneral);

                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}"))
                    CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}", _datosOportunidadAlumno.CronogramaPagoCompleto);

                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoTablaChile}"))
                    CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.CronogramaPagoCompletoTablaChile}", _datosOportunidadAlumno.CronogramaPagoCompletoChile);

                if (_plantillaBase.Cuerpo.Contains("{ValorDinamico.DiaFechaActual}"))
                    CargarValorEtiquetaCuerpo("{ValorDinamico.DiaFechaActual}", _datosOportunidadAlumno.DiaFechaActual);

                if (_plantillaBase.Cuerpo.Contains("{ValorDinamico.NombreMesFechaActual}"))
                    CargarValorEtiquetaCuerpo("{ValorDinamico.NombreMesFechaActual}", _datosOportunidadAlumno.NombreMesFechaActual);

                if (_plantillaBase.Cuerpo.Contains("{ValorDinamico.AnioFechaActual}"))
                    CargarValorEtiquetaCuerpo("{ValorDinamico.AnioFechaActual}", _datosOportunidadAlumno.AnioFechaActual);

                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Version}"))
                    CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.Version}", _datosOportunidadAlumno.Version);

                //Datos Alumno
                if (_plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                    CargarValorEtiquetaCuerpo("{tAlumnos.nombre1}", _datosOportunidadAlumno.OportunidadAlumno.Nombre1);

                if (_plantillaBase.Cuerpo.Contains("{T_Alumno.NombreCompleto}"))
                    CargarValorEtiquetaCuerpo("{T_Alumno.NombreCompleto}", _datosOportunidadAlumno.OportunidadAlumno.NombreCompleto);

                if (_plantillaBase.Cuerpo.Contains("{T_Alumno.NroDocumento}"))
                    CargarValorEtiquetaCuerpo("{T_Alumno.NroDocumento}", _datosOportunidadAlumno.OportunidadAlumno.NroDocumento);

                if (_plantillaBase.Cuerpo.Contains("{tAlumnos.direccion}"))
                    CargarValorEtiquetaCuerpo("{tAlumnos.direccion}", _datosOportunidadAlumno.OportunidadAlumno.Direccion);

                if (_plantillaBase.Cuerpo.Contains("{tAlumnos.NombreCiudad}"))
                    CargarValorEtiquetaCuerpo("{tAlumnos.NombreCiudad}", _datosOportunidadAlumno.OportunidadAlumno.NombreCiudad);

                if (_plantillaBase.Cuerpo.Contains("{tAlumnos.NombrePais}"))
                    CargarValorEtiquetaCuerpo("{tAlumnos.NombrePais}", _datosOportunidadAlumno.OportunidadAlumno.NombrePais);

                List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = new();
                List<ProgramaGeneralSeccionAnexosHTMLDTO> seccionEstructura = new();
                int idProgramaGeneral = 0;
                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}") || _plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    Oportunidad oportunidad = await _unitOfWork.OportunidadRepository.ObtenerPorIdAsync((int)idOportunidad);
                    PEspecificoPorIdCentroCostoDTO pEspecifico = await _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCostoAsync(oportunidad.IdCentroCosto.Value);
                    idProgramaGeneral = pEspecifico.IdProgramaGeneral.GetValueOrDefault();
                    listaSecciones = await documentoAgendaService.ObtenerListaSeccionDocumentoProgramaGeneralAsync(idProgramaGeneral);
                    seccionEstructura = documentoAgendaService.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);
                }

                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}"))
                {
                    var estructuraV2 = seccionEstructura.FirstOrDefault(x => x.Seccion.ToLower() == "estructura curricular");
                    if (estructuraV2 != null)
                    {
                        string valor = $"<strong>{estructuraV2.Seccion}</strong><br>{estructuraV2.Contenido}";
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                    }
                    else
                    {
                        var valorFinal = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionesAsync(idProgramaGeneral);
                        var detalle = valorFinal.FirstOrDefault(x => x.Titulo.Contains("Estructura Curricular"));
                        string valor = string.Empty;
                        if (detalle != null)
                            valor = $"<h2>{detalle.Titulo}</h2>{detalle.Contenido}";
                        CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                    }
                }

                if (_plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        string valor = $"<strong>{certificacionV2.Seccion}</strong><br>{certificacionV2.Contenido}";
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                    }
                    else
                    {
                        var valorFinal = await _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionesAsync(idProgramaGeneral);
                        var detalle = valorFinal.FirstOrDefault(x => x.Titulo.Contains("Certificación"));
                        string valor = "";
                        if (detalle != null)
                            valor = $"<h2>{detalle.Titulo}</h2>{detalle.Contenido}";

                        CargarValorEtiquetaCuerpo("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                    }
                }
                var emailReemplazado = new PlantillaEmailMandrillDTO();
                if (_idPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    emailReemplazado.Asunto = _plantillaBase.Asunto;
                    emailReemplazado.CuerpoHTML = _plantillaBase.Cuerpo;
                }
                return emailReemplazado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 19/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Los Valores Para Etiquetas Lista de Programas
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idAreaEtiqueta">Id del area de la etiqueta (PK de la tabla mkt.T_AreaCampoEtiqueta)</param>
        /// <returns>String</returns>
        public string ObtenerValorEtiquetaListas(int idOportunidad, int idAreaEtiqueta)
        {
            var servicioPlantillaClaveValor = new PlantillaClaveValorService(_unitOfWork);
            string result = "";
            int contador = 1;
            var listaCursosRelacionados = servicioPlantillaClaveValor.ObtenerMontosCursosRelacionados(idOportunidad, idAreaEtiqueta);
            foreach (var item in listaCursosRelacionados)
            {
                string url_video = "";
                if (item.Url_Video != null)
                {
                    url_video = "<a href='https://" + item.Url_Video + "' target = '_blank' >" + "Ver Presentaci&oacute;n" + "</a>"; //item.Presentacion
                }
                result = result + "<p><b>" + contador.ToString() + ". " + item.Nombre + "</b></p>";
                result = result + "<p><b>Modalidad: </b>" + item.Modalidad + " " + "<b> Duraci&oacute;n: </b>" + " " + item.Duracion + "<br/>";
                result = result + "<b>Presentaci&oacute;n: </b>" + url_video + "<br/>";
                result = result + "<b>Inversi&oacute;n Desde: </b>" + item.Inversion + "<br/>";
                contador++;
            }
            return result;
        }

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Plantilla para mostrarse en combo whatssapp.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<PlantillaPwComboWhatsappDTO> ObtenerComboWhatsapp()
        {
            try
            {
                return _unitOfWork.PlantillaPwRepository.ObtenerComboWhatsapp();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Un Compuesto de Valores de Etiqueta para Remplazar en las Plantillas
        /// </summary>
        /// <param name="idCentroCosto"> Id CentroCosto </param>
        /// <param name="idFaseOportunidad">Id de la Fase Oportunidad</param>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Retorna 200 y objeto para combo o 400 y mensaje de error </returns>
        public async Task<ValorEtiquetaDTO> CargarValoresEtiqueta(int idCentroCosto, int idOportunidad)
        {
            try
            {
                //new ValorEstatico(_unitOfWork);
                _datosOportunidadAlumno = new OportunidadAlumnoDTO();
                if (idOportunidad == 0)
                    throw new BadRequestException("#PWS-CVE001@Id Oportunidad Invalido");
                if (idOportunidad == 0)
                    throw new BadRequestException("#PWS-CVE002@Id Centro Costo Invalido");

                var task1 = ObtenerValorEtiquetaAsync(idCentroCosto, idOportunidad);
                var datosOportunidad = _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(idOportunidad);
                Task<BoolDTO> taskPromocion;
                if (datosOportunidad != null && datosOportunidad.Id != 0)
                    taskPromocion = _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerFlagPromocionAsync(datosOportunidad.IdPgeneral.Value, 143); // Descuento 25%
                else
                    throw new BadRequestException("#PWS-CVE003@No se encontro datos de la oportunidad");
                var pEspecificoService = new PEspecificoService(_unitOfWork);
                var taskFechaInicioPrograma = Task.Run(() => pEspecificoService.FechaInicioProgramaV2(datosOportunidad.IdPgeneral.Value, datosOportunidad.IdPespecifico.GetValueOrDefault()));
                Task<string> taskCostoTotalConDescuento = ObtenerCostoTotalConDescuentoAsync(idOportunidad);
                var task2 = Task.Run(() => ObtenerDatosProgramaGeneral(datosOportunidad.IdPgeneral.Value));
                // En caso no sea necesario Eliminar
                var fechaActual = DateTime.Now;
                _datosOportunidadAlumno.DiaFechaActual = fechaActual.Day.ToString();
                _datosOportunidadAlumno.NombreMesFechaActual = fechaActual.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"));
                _datosOportunidadAlumno.AnioFechaActual = fechaActual.Year.ToString();
                //await task1;
                //await task2;

                datosOportunidad.CostoTotalConDescuento = await taskCostoTotalConDescuento;
                var promocion = await taskPromocion;
                if (promocion != null)
                    datosOportunidad.Promocion25 = promocion.Valor;

                var FechaInicioPrograma = await taskFechaInicioPrograma;
                await task1;
                await task2;
                //Task.WaitAll(task1, task2);
                var objeto = new ObjetoValorEtiquetaDTO()
                {
                    CronogramaPagos = _cronogramaPagos == "" ? _datosOportunidadAlumno.CronogramaPagoCompleto : _cronogramaPagos,
                    DatosOportunidadAlumno = _datosOportunidadAlumno,
                    EtiquetaMontosPagoPaquetes = _etiquetaMontosPagoPaquetes,
                    ListaProblemasCausa = _listaProblemasCausa,
                    ListaTemplateV2ReemplazoEtiqueta = _listaTemplateV2ReemplazoEtiqueta,
                    UrlCursosRelacionados = _urlCursosRelacionados,
                };
                var respuesta = new ValorEtiquetaDTO()
                {
                    Objeto = objeto,
                    DatosOportunidad = datosOportunidad,
                    FechaInicioPrograma = FechaInicioPrograma ?? ""
                };
                return (respuesta);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> ObtenerCostoTotalConDescuentoAsync(int idOportunidad)
        {
            string result = "";
            var resultado = await _unitOfWork.MontoPagoCronogramaRepository.ObtenerPorIdOportunidadAsync(idOportunidad);
            if (resultado != null && resultado.Id != 0)
            {
                var paymentCurrency = await _unitOfWork.MonedaRepository.ObtenerMonedaParaDocumentoAsync(resultado.IdMoneda);
                result = $"{paymentCurrency.Simbolo}{resultado.PrecioDescuento} {paymentCurrency.NombrePlural}";
            }
            return result;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combos para el modulo de (C) Plantillas de documentos del portal web
        /// </summary> 
        /// <returns> plantillaPwComboModuloDTO </returns>
        public PlantillaPwComboModuloDTO ObtenerCombosModulo()
        {
            var plantillaPwComboModulo = new PlantillaPwComboModuloDTO();
            plantillaPwComboModulo.PlantillaMaestroPw = _unitOfWork.PlantillaMaestroPwRepository.ObtenerCombo().ToList();
            plantillaPwComboModulo.RevisionPw = _unitOfWork.RevisionPwRepository.ObtenerCombo().ToList();
            plantillaPwComboModulo.PlantillaPw = _unitOfWork.PlantillaPwRepository.Obtener().ToList();
            plantillaPwComboModulo.Pais = _unitOfWork.PaisRepository.ObtenerListaPais().ToList();
            plantillaPwComboModulo.SeccionTipoContenidoPw = _unitOfWork.SeccionTipoContenidoPwRepository.ObtenerCombo().ToList();
            return plantillaPwComboModulo;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el las secciones plantillas contenidos por el IdPlantillaPw agrupados
        /// </summary> 
        /// <param name="idPlantillaPw"> (PK) de T_PLantilla_PW </param>
        /// <returns> List<SeccionPwPlantillaPwAgrupadoDTO> </returns>
        public List<SeccionPwPlantillaPwAgrupadoDTO> ObtenerSeccionesPorIdPlantillaPW(int idPlantillaPw)
        {
            var seccionPlantillaContenidos = _unitOfWork.SeccionPwRepository.ObtenerSeccionesPorIdPlantillaPW(idPlantillaPw);
            List<SeccionPwPlantillaPwAgrupadoDTO> seccionPwPlantillaPwAgrupados = new List<SeccionPwPlantillaPwAgrupadoDTO>();
            seccionPwPlantillaPwAgrupados = seccionPlantillaContenidos.GroupBy(u => (u.IdPlantilla, u.Id, u.Nombre))
                                .Select(group =>
                                new SeccionPwPlantillaPwAgrupadoDTO
                                {
                                    Id = group.Key.Id,
                                    IdPlantilla = group.Key.IdPlantilla,
                                    IdPlantillaPw = group.Key.IdPlantilla,
                                    Nombre = group.Key.Nombre,
                                    Descripcion = group.Select(x => x.Descripcion).FirstOrDefault(),
                                    Contenido = group.Select(x => x.Contenido).FirstOrDefault(),
                                    VisibleWeb = group.Select(x => x.VisibleWeb).FirstOrDefault(),
                                    ZonaWeb = group.Select(x => x.ZonaWeb).FirstOrDefault(),
                                    OrdenEeb = group.Select(x => x.OrdenEeb).FirstOrDefault(),
                                    Titulo = group.Select(x => x.Titulo).FirstOrDefault(),
                                    Posicion = group.Select(x => x.Posicion).FirstOrDefault(),
                                    Tipo = group.Select(x => x.Tipo).FirstOrDefault(),
                                    IdSeccionTipoContenido = group.Select(x => x.IdSeccionTipoContenido).FirstOrDefault(),
                                    NombreSeccionTipoContenido = group.Select(x => x.NombreSeccionTipoContenido).FirstOrDefault(),
                                    SeccionTipoDetallePw = group.Select(x => new SeccionTipoDetallePwDTO { Id = x.IdSeccionTipoDetallePw, NombreTitulo = x.NombreSubSeccion, IdSeccionTipoContenido = x.IdSubSeccionTipoContenido }).ToList()
                                }).ToList();
            return seccionPwPlantillaPwAgrupados;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combos para el modulo de (C) Plantillas de documentos del portal web
        /// </summary> 
        /// <returns> plantillaPwComboModuloDTO </returns>
        public List<PlantillaPaisFiltroDTO> ObtenerPaisesPorIdPlantillaPw(int idPlantillaPw)
        {
            var respuesta = _unitOfWork.PaisRepository.ObtenerPaisesPorIdPlantillaPw(idPlantillaPw).ToList();
            return respuesta;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 26/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="plantillaPwParametros"> Parametros de la plantilla_PW y sus detalles </param>
        /// <param name="usuario"> Autor de mofiicacion del dato </param>
        /// <returns> bool </returns>
        public IEnumerable<PlantillaPwDTO> Insertar(PlantillaPwParametrosDTO plantillaPwParametros, string usuario)
        {
            try
            {
                PlantillaPw plantillaPw = new PlantillaPw();

                using (TransactionScope scope = new TransactionScope())
                {
                    plantillaPw.Nombre = plantillaPwParametros.PlantillaPw.Nombre;
                    plantillaPw.Descripcion = plantillaPwParametros.PlantillaPw.Descripcion;
                    plantillaPw.IdPlantillaMaestroPw = plantillaPwParametros.PlantillaPw.IdPlantillaMaestroPw;
                    plantillaPw.IdRevisionPw = plantillaPwParametros.PlantillaPw.IdRevisionPw;
                    plantillaPw.Estado = true;
                    plantillaPw.UsuarioCreacion = usuario;
                    plantillaPw.UsuarioModificacion = usuario;
                    plantillaPw.FechaCreacion = DateTime.Now;
                    plantillaPw.FechaModificacion = DateTime.Now;

                    plantillaPw.PlantillaPais = new List<PlantillaPais>();
                    plantillaPw.SeccionPws = new List<SeccionPw>();

                    if (plantillaPwParametros.Paises.Count > 0)
                        foreach (var item in plantillaPwParametros.Paises)
                        {
                            PlantillaPais plantillaPais = new PlantillaPais();
                            plantillaPais.IdPais = item;
                            plantillaPais.UsuarioCreacion = usuario;
                            plantillaPais.UsuarioModificacion = usuario;
                            plantillaPais.FechaCreacion = DateTime.Now;
                            plantillaPais.FechaModificacion = DateTime.Now;
                            plantillaPais.Estado = true;
                            plantillaPw.PlantillaPais.Add(plantillaPais);
                        }

                    if (plantillaPwParametros.SeccionPw.Count > 0)
                        foreach (var item in plantillaPwParametros.SeccionPw)
                        {
                            SeccionPw seccionPw = new SeccionPw();
                            seccionPw.Nombre = item.Nombre;
                            seccionPw.Descripcion = item.Descripcion;
                            seccionPw.Contenido = item.Contenido;
                            seccionPw.VisibleWeb = item.VisibleWeb;
                            seccionPw.ZonaWeb = item.ZonaWeb;
                            seccionPw.OrdenEeb = item.OrdenEeb;
                            seccionPw.UsuarioCreacion = usuario;
                            seccionPw.UsuarioModificacion = usuario;
                            seccionPw.FechaCreacion = DateTime.Now;
                            seccionPw.FechaModificacion = DateTime.Now;
                            seccionPw.Estado = true;
                            seccionPw.IdSeccionTipoContenido = item.IdSeccionTipoContenido;

                            if (item.SeccionTipoDetallePw.Count > 0)
                                seccionPw.SeccionTipoDetallePws = new List<SeccionTipoDetallePw>();
                            foreach (var item2 in item.SeccionTipoDetallePw)
                            {
                                SeccionTipoDetallePw seccionTipoDetallePw = new SeccionTipoDetallePw();
                                seccionTipoDetallePw.IdSeccionPw = item.Id.Value;
                                seccionTipoDetallePw.NombreTitulo = item2.NombreTitulo;
                                seccionTipoDetallePw.IdSeccionTipoContenido = item2.IdSeccionTipoContenido;
                                seccionTipoDetallePw.UsuarioCreacion = usuario;
                                seccionTipoDetallePw.UsuarioModificacion = usuario;
                                seccionTipoDetallePw.FechaCreacion = DateTime.Now;
                                seccionTipoDetallePw.FechaModificacion = DateTime.Now;
                                seccionTipoDetallePw.Estado = true;
                                seccionPw.SeccionTipoDetallePws.Add(seccionTipoDetallePw);
                            }
                            plantillaPw.SeccionPws.Add(seccionPw);
                        }

                    _unitOfWork.PlantillaPwRepository.Add(plantillaPw);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return _unitOfWork.PlantillaPwRepository.Obtener();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 26/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la ta tabla y sus detalles
        /// </summary>   
        /// <param name="plantillaPwParametros"> Parametros de la plantilla_PW y sus detalles </param>
        /// <param name="usuario"> Autor de mofiicacion del dato </param>
        /// <returns> bool </returns>
        public IEnumerable<PlantillaPwDTO> Actualizar(PlantillaPwParametrosDTO plantillaPwParametros, string usuario)
        {
            try
            {
                PlantillaPw plantillaPw = new PlantillaPw();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.PlantillaRepository.Exist(plantillaPwParametros.PlantillaPw.Id.Value))
                    {
                        #region Eliminacion Detalles
                        /*Eliminamos los registros que T_Seccion_PW que dejaran de usarle en la T_Plantilla_PW*/
                        var seccionPws = _unitOfWork.SeccionPwRepository.ObtenerPorIdPlantillaPw(plantillaPwParametros.PlantillaPw.Id.Value).ToList();
                        /*Eliminamos los registros que T_SeccionTipoDetalle_PW que dejaran de usarle en la T_Seccion_PW*/
                        foreach (var item in seccionPws.Select(x => x.Id))
                        {
                            var seccionTipoDetallePws = _unitOfWork.SeccionTipoDetallePwRepository.ObtenerPorIdSeccionPw(item).ToList();
                            foreach (var item2 in plantillaPwParametros.SeccionPw)
                            {
                                seccionTipoDetallePws.RemoveAll(x => item2.SeccionTipoDetallePw.Any(y => y.Id == x.Id));
                            }
                            _unitOfWork.SeccionTipoDetallePwRepository.Delete(seccionTipoDetallePws.Select(x => x.Id), usuario);
                        }
                        seccionPws.RemoveAll(x => plantillaPwParametros.SeccionPw.Any(y => y.Id == x.Id));
                        _unitOfWork.SeccionPwRepository.Delete(seccionPws.Select(x => x.Id), usuario);



                        /*Eliminamos los registros que T_PlantillaPais que dejaran de usarle en la T_Plantilla_PW*/
                        var plantillaPaises = _unitOfWork.PlantillaPaisRepository.ObtenerPorIdPlantillaPw(plantillaPwParametros.PlantillaPw.Id.Value).ToList();
                        plantillaPaises.RemoveAll(x => plantillaPwParametros.Paises.Any(y => y == x.IdPais));
                        _unitOfWork.PlantillaPaisRepository.Delete(plantillaPaises.Select(x => x.Id), usuario);

                        _unitOfWork.Commit();
                        #endregion

                        plantillaPw = _unitOfWork.PlantillaPwRepository.ObtenerPorId(plantillaPwParametros.PlantillaPw.Id.Value);
                        plantillaPw.Nombre = plantillaPwParametros.PlantillaPw.Nombre;
                        plantillaPw.Descripcion = plantillaPwParametros.PlantillaPw.Descripcion;
                        plantillaPw.IdPlantillaMaestroPw = plantillaPwParametros.PlantillaPw.IdPlantillaMaestroPw;
                        plantillaPw.IdRevisionPw = plantillaPwParametros.PlantillaPw.IdRevisionPw;
                        plantillaPw.UsuarioModificacion = usuario;
                        plantillaPw.FechaModificacion = DateTime.Now;

                        plantillaPw.SeccionPws = new List<SeccionPw>();
                        plantillaPw.PlantillaPais = new List<PlantillaPais>();

                        foreach (var item in plantillaPwParametros.Paises)
                        {
                            PlantillaPais plantillaPais;
                            plantillaPais = _unitOfWork.PlantillaPaisRepository.ObtenerPorIdPaisYIdPlantillaPw(item, plantillaPwParametros.PlantillaPw.Id.Value);
                            if (plantillaPais.Id != null && plantillaPais.Id > 0)
                            {
                                plantillaPais.UsuarioModificacion = usuario;
                                plantillaPais.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                plantillaPais = new PlantillaPais();
                                plantillaPais.IdPais = item;
                                plantillaPais.UsuarioCreacion = usuario;
                                plantillaPais.UsuarioModificacion = usuario;
                                plantillaPais.FechaCreacion = DateTime.Now;
                                plantillaPais.FechaModificacion = DateTime.Now;
                                plantillaPais.Estado = true;
                            }
                            plantillaPw.PlantillaPais.Add(plantillaPais);
                        }

                        foreach (var item in plantillaPwParametros.SeccionPw)
                        {
                            SeccionPw seccionPw;

                            if (item.Id.Value > 0 && item.Id != null)
                            {
                                seccionPw = _unitOfWork.SeccionPwRepository.ObtenerPorId(item.Id.Value);
                                seccionPw.Nombre = item.Nombre;
                                seccionPw.Descripcion = item.Descripcion;
                                seccionPw.Contenido = item.Contenido;
                                seccionPw.VisibleWeb = item.VisibleWeb;
                                seccionPw.ZonaWeb = item.ZonaWeb;
                                seccionPw.OrdenEeb = item.OrdenEeb;
                                seccionPw.Estado = true;
                                seccionPw.UsuarioCreacion = usuario;
                                seccionPw.UsuarioModificacion = usuario;
                                seccionPw.FechaCreacion = DateTime.Now;
                                seccionPw.FechaModificacion = DateTime.Now;
                                seccionPw.IdSeccionTipoContenido = item.IdSeccionTipoContenido;

                                if (item.SeccionTipoDetallePw.Count > 0)
                                    seccionPw.SeccionTipoDetallePws = new List<SeccionTipoDetallePw>();
                                foreach (var item2 in item.SeccionTipoDetallePw)
                                {
                                    SeccionTipoDetallePw seccionTipoDetallePw = new SeccionTipoDetallePw();
                                    if (item2.Id.Value == 0 || item2.Id == null)
                                    {
                                        seccionTipoDetallePw.NombreTitulo = item2.NombreTitulo;
                                        seccionTipoDetallePw.IdSeccionTipoContenido = item2.IdSeccionTipoContenido;
                                        seccionTipoDetallePw.UsuarioCreacion = usuario;
                                        seccionTipoDetallePw.UsuarioModificacion = usuario;
                                        seccionTipoDetallePw.FechaCreacion = DateTime.Now;
                                        seccionTipoDetallePw.FechaModificacion = DateTime.Now;
                                        seccionTipoDetallePw.Estado = true;
                                        seccionPw.SeccionTipoDetallePws.Add(seccionTipoDetallePw);
                                    }
                                }
                            }
                            else
                            {
                                seccionPw = new SeccionPw();
                                seccionPw.Nombre = item.Nombre;
                                seccionPw.Descripcion = item.Descripcion;
                                seccionPw.Contenido = item.Contenido;
                                seccionPw.VisibleWeb = item.VisibleWeb;
                                seccionPw.ZonaWeb = item.ZonaWeb;
                                seccionPw.OrdenEeb = item.OrdenEeb;
                                seccionPw.UsuarioCreacion = usuario;
                                seccionPw.UsuarioModificacion = usuario;
                                seccionPw.FechaCreacion = DateTime.Now;
                                seccionPw.FechaModificacion = DateTime.Now;
                                seccionPw.Estado = true;
                                seccionPw.IdSeccionTipoContenido = item.IdSeccionTipoContenido;

                                if (item.SeccionTipoDetallePw.Count > 0)
                                    seccionPw.SeccionTipoDetallePws = new List<SeccionTipoDetallePw>();
                                foreach (var item2 in item.SeccionTipoDetallePw)
                                {
                                    SeccionTipoDetallePw seccionTipoDetallePw = new SeccionTipoDetallePw();
                                    seccionTipoDetallePw.IdSeccionPw = item.Id.Value;
                                    seccionTipoDetallePw.NombreTitulo = item2.NombreTitulo;
                                    seccionTipoDetallePw.IdSeccionTipoContenido = item2.IdSeccionTipoContenido;
                                    seccionTipoDetallePw.UsuarioCreacion = usuario;
                                    seccionTipoDetallePw.UsuarioModificacion = usuario;
                                    seccionTipoDetallePw.FechaCreacion = DateTime.Now;
                                    seccionTipoDetallePw.FechaModificacion = DateTime.Now;
                                    seccionTipoDetallePw.Estado = true;
                                    seccionPw.SeccionTipoDetallePws.Add(seccionTipoDetallePw);
                                }
                            }
                            plantillaPw.SeccionPws.Add(seccionPw);
                        }
                    }
                    _unitOfWork.PlantillaPwRepository.Update(plantillaPw);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return _unitOfWork.PlantillaPwRepository.Obtener().ToList();
            }

            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary> 
        /// Realiza una eliminacion logica del registro y sus detalles
        /// </summary>   
        /// <param name="id"> (PK) </param>
        /// <returns> bool </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.PlantillaPwRepository.Exist(id))
                    {
                        _unitOfWork.PlantillaPwRepository.Delete(id, usuario);

                        /*Obtenemos los detalles*/
                        var plantillaPais = _unitOfWork.PlantillaPaisRepository.ObtenerPorIdPlantillaPw(id);
                        var seccionPws = _unitOfWork.SeccionPwRepository.ObtenerPorIdPlantillaPw(id);

                        foreach (var item in seccionPws)
                        {
                            /*Obtenemos los detalles del detalle T_Seccion_PW*/
                            var seccionTipoDetallePws = _unitOfWork.SeccionTipoDetallePwRepository.ObtenerPorIdSeccionPw(item.Id);
                            /*Eliminamos los detalles del detalle T_Seccion_PW*/
                            _unitOfWork.SeccionTipoDetallePwRepository.Delete(seccionTipoDetallePws.Select(x => x.Id), usuario);
                        }
                        /*Eliminamos los detalles*/
                        _unitOfWork.PlantillaPaisRepository.Delete(plantillaPais.Select(x => x.Id), usuario);
                        _unitOfWork.SeccionPwRepository.Delete(seccionPws.Select(x => x.Id), usuario);

                        _unitOfWork.Commit();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

