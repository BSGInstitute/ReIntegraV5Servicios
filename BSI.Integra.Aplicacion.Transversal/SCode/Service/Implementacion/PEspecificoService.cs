using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Protobuf.WellKnownTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Transactions;
using Twilio.TwiML.Voice;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PEspecificoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_PEspecifico
    /// </summary>
    public class PEspecificoService : IPEspecificoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private CloudBlobContainer _container;
        private CloudBlockBlob _blockBlob;
        public PEspecificoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecifico, PEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoDTO, PEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoDTO, TPespecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCentroCosto, CentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<CentroCostoDTO, CentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<CentroCostoDTO, TCentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<CentroCostoDTO, TCentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<FeriadoDTO, Feriado>(MemberList.None).ReverseMap();
                cfg.CreateMap<FeriadoDTO, TFeriado>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesion, TPespecificoSesion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionDTO, TPespecificoSesion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PespecificoParticipacionExpositor, TPespecificoParticipacionExpositor>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/04/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza la obtencion de programas especificos por id programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general</param>
        /// <returns> Lista de Programas especificos  </returns>
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ObtenerFiltroPorIdPGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerFiltroPorIdPGeneral(idPGeneral);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene en HTML las fechas de inicio de los programas V2
        /// </summary>
        /// <param name="idPGeneral"> Id de programa General </param>
        /// <returns>Retorna un HTML con las fechas de inicio de los programas</returns>
        public string FechaInicioProgramaV2(int idPGeneral, int idPespecifico)
        {
            var fechas = ObtenerFechaInicioProgramaTodos(idPGeneral);
            if (fechas == null) return "Por definir";
            string stringHtml = string.Empty;

            var filtro = fechas.Where(x => x.Id == idPespecifico).FirstOrDefault();
            if (filtro != null)
            {
                return $"<b>{filtro.Tipo}:</b> {filtro.FechaInicioTexto}<br />";
            }
            foreach (var fecha in fechas)
            {
                if (fecha.Tipo.ToLower() == "online asincronica" && fecha.Nombre.ToLower().Contains("lima"))
                    stringHtml += $"<b>{fecha.Tipo}:</b> {fecha.FechaInicioTexto}<br />";
                if (fecha.Tipo.ToLower() == "online sincronica" && fecha.Nombre.ToLower().Contains("lima") || fecha.Nombre.ToLower().Contains("aqp"))
                    stringHtml += $"<b>{fecha.Tipo}:</b> {fecha.FechaInicioTexto}<br />";
            }
            return stringHtml;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene en un objeto las fechas de las modalidades según la lógica del portal
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns> List<ValorStringDTO> </returns>
        public List<PEspecificoPorIdPGeneral> ObtenerFechaInicioProgramaTodos(int idPGeneral)
        {
            List<PEspecificoPorIdPGeneral> pEspecificos = _unitOfWork.PEspecificoRepository.ObtenerPorIdPGeneral(idPGeneral);
            if (pEspecificos == null || pEspecificos.Count() == 0)
                return null;

            int IdCategoria = pEspecificos.Select(w => w.IdCategoria).FirstOrDefault();
            List<int> idsPEspecificos = pEspecificos.Select(x => x.Id).ToList();
            List<PEspecificoSesionFechaHoraInicioDTO> fechasHoraInicioSesion = new();

            IPEspecificoSesionService pEspecificoSesionService = new PEspecificoSesionService(_unitOfWork);
            if (IdCategoria == CategoriaPrograma.CURSOS
                || IdCategoria == CategoriaPrograma.BOOTCAMP
                || IdCategoria == CategoriaPrograma.CARRERA_PROFESIONAL)
            {
                fechasHoraInicioSesion = pEspecificoSesionService.ObtenerFechaHoraInicioSesionPorIdPEspecifico(idsPEspecificos, 2);
            }
            else if (IdCategoria == CategoriaPrograma.PROGRAMAS)
            {
                fechasHoraInicioSesion = pEspecificoSesionService.ObtenerFechaHoraInicioSesionPorIdPEspecifico(idsPEspecificos, 1);
            }
            List<PEspecificoPorIdPGeneral> listaPrevioPEspecifico = new();
            foreach (var item in pEspecificos)
            {
                if (item.Tipo.ToLower() == "online asincronica")
                {
                    DateTime fechaAOnine = (DateTime.Now.Day < 25) ? DateTime.Now : DateTime.Now.AddDays(8);
                    item.FechaInicio = fechaAOnine;
                    item.FechaInicioTexto = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fechaAOnine.ToString("MMMM yyyy"));
                }
                else
                {
                    if (item.FechaInicio == null)
                    {
                        DateTime? fechaHoraInicio = fechasHoraInicioSesion.Where(x => x.IdPEspecifico == item.Id && x.FechaHoraInicio.Value > DateTime.Now).OrderBy(x => x.FechaHoraInicio).Select(x => x.FechaHoraInicio).FirstOrDefault();

                        if (fechaHoraInicio != null)
                        {
                            item.FechaInicio = fechaHoraInicio.Value;
                            item.FechaInicioTexto = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.FechaInicio.Value.ToString("dd MMMM yyyy"));
                        }
                        else item.FechaInicioTexto = "Por definir";
                    }
                    else item.FechaInicioTexto = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.FechaInicio.Value.ToString("dd MMMM yyyy"));
                }
                listaPrevioPEspecifico.Add(item);
            }
            var listaCiudades = (from x in listaPrevioPEspecifico
                                 group x by new { x.Tipo, x.Ciudad } into gy
                                 select new
                                 {
                                     gy.Key.Tipo,
                                     gy.Key.Ciudad
                                 }).ToList();

            List<PEspecificoPorIdPGeneral> listaPEspecifico = new();

            foreach (var item in listaCiudades)
            {
                var pEspecificoLanzamientoPorEjecucion = (from x in listaPrevioPEspecifico where (x.EstadoPId == EstadoPespecifico.LANZAMIENTO || x.EstadoPId == EstadoPespecifico.POR_EJECUCION) && x.Tipo == item.Tipo && x.Ciudad == item.Ciudad && x.IdCategoria != CategoriaPrograma.SUBCRIPCIONES && x.FechaInicio != null select x).OrderBy(x => x.FechaInicio).Take(3).ToList();
                
                if (pEspecificoLanzamientoPorEjecucion != null && pEspecificoLanzamientoPorEjecucion.Count >0)
                    foreach (var item2 in pEspecificoLanzamientoPorEjecucion)
                        listaPEspecifico.Add(item2);
                else
                {
                    var pEspecificoCiudad = listaPrevioPEspecifico.Where(x => x.Tipo == item.Tipo && x.Ciudad == item.Ciudad && x.IdCategoria != CategoriaPrograma.SUBCRIPCIONES).OrderBy(x => x.FechaCreacion).FirstOrDefault();
                    if (pEspecificoCiudad != null)
                        listaPEspecifico.Add(pEspecificoCiudad);
                }
            }
            listaPEspecifico.AddRange(listaPrevioPEspecifico.Where(x => x.IdCategoria == CategoriaPrograma.SUBCRIPCIONES).ToList());

            listaPEspecifico = listaPEspecifico.Select(c =>
            {
                c.FechaInicioTexto = (c.EstadoPId != EstadoPespecifico.LANZAMIENTO && c.EstadoPId != EstadoPespecifico.POR_EJECUCION) ? "Por definir" : c.FechaInicioTexto;
                return c;
            }).OrderBy(x => x.FechaInicio).ToList();

            return listaPEspecifico;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el periodo duracion del programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico del que se desea obtener la duracion (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="idMatriculaCabecera">Id de la matricula cabecera (PK de la tabla fin.T_MatriculaCabecera)</param>
        /// <returns>Cadena del periodo y duracion</returns>
        public string ObtenerPeriodoDuracion(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                PeriodoDuracionProgramaEspecificoDTO resultadoFinal = _unitOfWork.PEspecificoRepository.ObtenerPeriodoDuracion(idPEspecifico, idMatriculaCabecera);
                string htmlFinal = "<span>";
                htmlFinal += $@"
                    <strong>Fecha de Inicio: </strong> {resultadoFinal.FechaInicio.ToString("dd/MM/yyyy")}
                    <br/>
                    <strong>Duración total: </strong> {resultadoFinal.DuracionTotalAproximadaMeses} Meses Aprox.
                    <br/>
                    <strong>Fecha de Culminación: </strong> {resultadoFinal.FechaTermino.ToString("dd/MM/yyyy")}
                    <br/>
                    <strong>Fecha aproximada de certificación: </strong> {resultadoFinal.FechaAproximadaCertificacion.ToString("dd/MM/yyyy")}";
                htmlFinal += "</span>";
                return htmlFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico del cual se desea averiguar las sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena formateada con el conjunto de sesiones del curso mencionado</returns>
        public string ObtenerConjuntoSesion(int idPEspecifico)
        {
            try
            {
                var resultadoFinal = _unitOfWork.PEspecificoRepository.ObtenerConjuntoSesionProgramaEspecifico(idPEspecifico);
                var resultadoAgrupado = resultadoFinal.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico })
                    .Select(y => new ConjuntoSesionProgramaEspecificoMaestroDTO
                    {
                        IdPEspecifico = y.Key.IdPEspecifico,
                        NombrePEspecifico = y.Key.NombrePEspecifico,
                        Sesiones = y.Select(w => new ConjuntoSesionProgramaEspecificoDetalleDTO()
                        {
                            DuracionSesionHoras = w.DuracionSesionHoras,
                            FechaSesion = w.FechaSesion,
                            HorarioSesion = w.HorarioSesion
                        }).ToList()
                    });

                string htmlFinal = "";
                foreach (var item in resultadoAgrupado)
                {
                    htmlFinal += $"<p>Curso: {item.NombrePEspecifico}</p>";
                    foreach (var sesion in item.Sesiones)
                    {
                        htmlFinal += $@"
							<p>
							Fecha: {sesion.FechaSesion.ToString("dd/MM/yyyy")}
							<br/>
							Horarios: {sesion.HorarioSesion}
							<br/>
							Duración: {sesion.DuracionSesionHoras} horas
							<br/>
							</p>
						";
                    }
                    htmlFinal += "</br>";
                }
                return htmlFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico que tienen una sesion en base a la fecha actual + cantidad de dias dentro de la semana actual
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico que se desea saber su proximo conjunto de sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el momento de la consulta</param>
        /// <returns>Cadena formateada con el proximo conjunto de sesion</returns>
        public string ObtenerProximoConjuntoSesion(int idPEspecifico, int cantidadDias)
        {
            try
            {
                var resultado = _unitOfWork.PEspecificoRepository.ObtenerProximoConjuntoSesionProgramaEspecifico(idPEspecifico, cantidadDias);
                var resultadoAgrupado = resultado.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico })
                    .Select(y => new ConjuntoSesionProgramaEspecificoMaestroDTO
                    {
                        IdPEspecifico = y.Key.IdPEspecifico,
                        NombrePEspecifico = y.Key.NombrePEspecifico,
                        Sesiones = y.Select(w => new ConjuntoSesionProgramaEspecificoDetalleDTO()
                        {
                            DuracionSesionHoras = w.DuracionSesionHoras,
                            FechaSesion = w.FechaSesion,
                            HorarioSesion = w.HorarioSesion
                        }).ToList()
                    });

                string htmlFinal = "";
                foreach (var item in resultadoAgrupado)
                {
                    htmlFinal += $"<p><strong>Curso:</strong> {item.NombrePEspecifico}</p>";
                    foreach (var sesion in item.Sesiones)
                    {
                        htmlFinal += $@"
						    <p>
						    <strong>Fecha:</strong> {sesion.FechaSesion.ToString("dd/MM/yyyy")}
						    <br/>
						    <strong>Horarios:</strong> {sesion.HorarioSesion}
						    <br/>
						    <strong>Duración:</strong> {sesion.DuracionSesionHoras} horas
                            <br/>
						    </p>
						";
                    }
                    htmlFinal += "</br>";
                }
                return htmlFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las sesiones por cursos del programa especifico que tienen una sesion en base a la fecha actual + cantidad de dias dentro de la semana actual
        /// </summary>
        /// <param name="idPEspecifico">Id del PEspecifico que se desea saber su proximo conjunto de sesiones (PK de la tabla pla.T_PEspecifico)</param>
        /// <param name="cantidadDias">Cantidad de dias transcurridos desde el momento de la consulta</param>\
        /// <param name="incrementoZonaHoraria">Segun la diferencia horaria calcula una nueva hora</param>
        /// <param name="nombrePais">Nombre del pais del cual se va a evaluar la hora</param>
        /// <param name="incluirNombreCurso">Flag para validar si se incluye el nombre del curso en la cadena formateada</param>
        /// <returns>Cadena formateada con el proximo conjunto de sesion Webex</returns>
        public string ObtenerProximoConjuntoSesionWebex(int idPEspecifico, int cantidadDias, int incrementoZonaHoraria, string nombrePais, bool incluirNombreCurso)
        {
            try
            {
                var respuesta = _unitOfWork.PEspecificoRepository.ObtenerProximoConjuntoSesionProgramaEspecificoWebex(idPEspecifico, cantidadDias);

                var resultadoAgrupado = respuesta.GroupBy(x => new { x.IdPEspecifico, x.NombrePEspecifico })
                    .Select(y => new ConjuntoSesionProgramaEspecificoMaestroDTO
                    {
                        IdPEspecifico = y.Key.IdPEspecifico,
                        NombrePEspecifico = y.Key.NombrePEspecifico,
                        Sesiones = y.Select(w => new ConjuntoSesionProgramaEspecificoDetalleDTO()
                        {
                            DuracionSesionHoras = w.DuracionSesionHoras,
                            FechaSesion = w.FechaSesion,
                            HorarioSesion = w.FechaSesion.AddHours(incrementoZonaHoraria).ToString("HH:mm") + " a " + w.FechaSesion.AddHours(w.DuracionSesionHoras).AddHours(incrementoZonaHoraria).ToString("HH:mm")
                        }).ToList()
                    }).ToList();

                string htmlFinal = "";
                foreach (var item in resultadoAgrupado)
                {
                    if (incluirNombreCurso)
                        htmlFinal += $"<p><strong>Curso:</strong> {item.NombrePEspecifico}</p>";

                    foreach (var sesion in item.Sesiones)
                    {
                        htmlFinal += $@"
							<p>
							<strong>Fecha:</strong> {sesion.FechaSesion.ToString("dd/MM/yyyy")}
							<br/>
							<strong>Horarios:</strong> {sesion.HorarioSesion} horario de {nombrePais}
							<br/>
							<strong>Duración:</strong> {sesion.DuracionSesionHoras} horas
                            <br/>
							</p>";
                    }
                    htmlFinal += "</br>";
                }
                return htmlFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 02/08/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene cursos relacionados mediante id Pgeneral
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPorIdPGeneral(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerPEspecificoRelacionadoPorIdPGeneral(idPEspecifico, idMatriculaCabecera);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 02/09/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene cursos relacionados de irca por programa especifico
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoIrca(int idPEspecifico, int idMatriculaCabecera, bool esCursoDSig)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerPEspecificoRelacionadoIrca(idPEspecifico, idMatriculaCabecera, esCursoDSig);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Lista de programas especificos por nombre
        /// </summary>
        /// <param name="valor">Filtro de Programa especifico</param>
        /// <returns>List<ComboDTO></returns>
        public List<ComboDTO> ObtenerPorNombreAutocomplete(string valor)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerPorNombreAutocomplete(valor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 02/08/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene sesiones relacionados de  Pgeneral
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public List<PEspecificoComboDTO> ObtenerPEspecificoRelacionadoPGeneral(int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerPEspecificoRelacionadoPGeneral(idPEspecifico, idMatriculaCabecera);
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// Autor: Gretel Canasa
        /// Fecha: 29/08/2023
        /// Version: 2.0
        /// <summary>
        /// Obtiene Programas especificos Adicionales
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public IEnumerable<ComboDTO> ObtenerProgramasEspecificosAdicional()
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerProgramasEspecificosAdicional();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de programas especificos padre mediante filtros
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public PEspecificoModuloComboDTO ObtenerCombosModulo()
        {
            try
            {
                var producto = _unitOfWork.ProductoRepository.ObtenerCombo();
                var proveedor = _unitOfWork.ProveedorRepository.ObtenerInformacionProductoProveedor();
                var proveedorCurso = _unitOfWork.ProveedorRepository.ObtenerProveedorFiltro();
                var productoPresentacion = _unitOfWork.ProductoPresentacionRepository.ObtenerCombo();
                var pGeneral = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro();
                var centroCosto = _unitOfWork.CentroCostoRepository.ObtenerCombo();
                var modalidades = _unitOfWork.ModalidadCursoRepository.ObtenerCombo();
                var locacionTroncal = _unitOfWork.TroncalPgeneralRepository.ObtenerLocacionTroncal();
                var ambiente = _unitOfWork.AmbienteRepository.ObtenerAmbienteCiudadFiltro();
                var origenProgramas = _unitOfWork.OrigenProgramaRepository.ObtenerDatosOrigenPrograma();
                var locacion = _unitOfWork.LocacionRepository.ObtenerLocacionParaFiltro();
                var expositores = _unitOfWork.ExpositorRepository.ObtenerCombo();
                var frecuencia = _unitOfWork.FrecuenciaRepository.ObtenerCombo();
                var estadoPespecifico = _unitOfWork.EstadoPespecificoRepository.ObtenerCombo();
                var personalAreaTrabajo = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerCombo();
                var ciudad = _unitOfWork.CiudadRepository.ObtenerCiudadFiltro();

                var plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaNombreCorreoOperaciones();
                var plantillaWhatsApp = _unitOfWork.PlantillaRepository.ObtenerPlantillaNombreWhatsAppOperaciones();
                var TiempoFrecuencia = _unitOfWork.TiempoFrecuenciaRepository.ObtenerComboPorIds(new int[] { 6, 7 });
                var dias = _unitOfWork.DiaSemanaRepository.ObtenerCombo();

                var ciudadBs = _unitOfWork.RegionCiudadRepository.ObtenerCiudadBs();
                var area = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltro();
                var subArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro();
                var programaGeneral = _unitOfWork.PGeneralRepository.ObtenerFiltroPorTipo(false);
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerFiltroPorTipo(false);
                var programaEspecificoHijos = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoHijoFiltro();
                var centroCostoPersonalizado = _unitOfWork.CentroCostoRepository.ObtenerFiltroPorTipo(false);
                var programaespecificoWebinar = _unitOfWork.PEspecificoRepository.ObtenerListaPEspecificosRelacionados();

                PEspecificoModuloComboDTO resultado = new()
                {
                    Producto = producto,
                    Proveedor = proveedor,
                    ProveedorCurso = proveedorCurso,
                    ProductoPresentacion = productoPresentacion,
                    ProgramaGeneral = pGeneral,
                    CentroCosto = centroCosto,
                    Modalidad = modalidades,
                    LocacionTroncal = locacionTroncal,
                    Ambiente = ambiente,
                    Origen = origenProgramas,
                    Locacion = locacion,
                    Expositor = expositores,
                    Frecuencia = frecuencia,
                    EstadoPEspecifico = estadoPespecifico,
                    PersonalAreaTrabajo = personalAreaTrabajo,
                    Ciudad = ciudad,
                    CiudadBS = ciudadBs,
                    AreaCapacitacion = area,
                    SubAreaCapacitacion = subArea,
                    ProgramaGeneralP = programaGeneral,
                    ProgramaEspecifico = programaEspecifico,
                    ProgramaEspecificoHijos = programaEspecificoHijos,
                    CentroCostoP = centroCostoPersonalizado,
                    ProgramaEspecificoWebinar = programaespecificoWebinar,
                    PlantillaCorreo = plantillaCorreo,
                    PlantillaWhatsApp = plantillaWhatsApp,
                    TiempoFrecuencia = TiempoFrecuencia,
                    Dias = dias
                };
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de programas especificos padre mediante filtros
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public async Task<PEspecificoModuloComboDTO> ObtenerCombosModuloAsync()
        {
            try
            {
                var task_producto = _unitOfWork.ProductoRepository.ObtenerComboAsync();
                var task_proveedor = _unitOfWork.ProveedorRepository.ObtenerInformacionProductoProveedorAsync();
                var task_proveedorCurso = _unitOfWork.ProveedorRepository.ObtenerProveedorFiltroAsync();
                var task_productoPresentacion = _unitOfWork.ProductoPresentacionRepository.ObtenerComboAsync();
                var task_pGeneral = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltroAsync();
                var task_centroCosto = _unitOfWork.CentroCostoRepository.ObtenerComboAsync();
                var task_modalidades = _unitOfWork.ModalidadCursoRepository.ObtenerComboAsync();
                var task_locacionTroncal = _unitOfWork.TroncalPgeneralRepository.ObtenerLocacionTroncalAsync();
                var task_ambiente = _unitOfWork.AmbienteRepository.ObtenerAmbienteCiudadFiltroAsync();
                var task_origenProgramas = _unitOfWork.OrigenProgramaRepository.ObtenerDatosOrigenProgramaAsync();
                var task_locacion = _unitOfWork.LocacionRepository.ObtenerLocacionParaFiltroAsync();
                var task_expositores = _unitOfWork.ExpositorRepository.ObtenerComboAsync();
                var task_frecuencia = _unitOfWork.FrecuenciaRepository.ObtenerComboAsync();
                var task_estadoPespecifico = _unitOfWork.EstadoPespecificoRepository.ObtenerComboAsync();
                var task_personalAreaTrabajo = _unitOfWork.PersonalAreaTrabajoRepository.ObtenerComboAsync();
                var task_ciudad = _unitOfWork.CiudadRepository.ObtenerComboAsync();
                var task_ciclo = _unitOfWork.CicloRepository.ObtenerComboAsync();
                var task_periodoLectivo = _unitOfWork.PeriodoLectivoRepository.ObtenerComboAsync();
                var task_plantillaCorreo = _unitOfWork.PlantillaRepository.ObtenerPlantillaNombreCorreoOperacionesAsync();
                var task_plantillaWhatsApp = _unitOfWork.PlantillaRepository.ObtenerPlantillaNombreWhatsAppOperacionesAsync();
                var task_tiempoFrecuencia = _unitOfWork.TiempoFrecuenciaRepository.ObtenerComboPorIdsAsync(new int[] { 6, 7 });
                var task_dias = _unitOfWork.DiaSemanaRepository.ObtenerComboAsync();

                var task_ciudadBs = _unitOfWork.RegionCiudadRepository.ObtenerCiudadBsAsync();
                var task_area = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltroAsync();
                var task_subArea = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltroAsync();
                var task_programaGeneral = _unitOfWork.PGeneralRepository.ObtenerFiltroPorTipoAsync(false);
                var task_programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerFiltroPorTipoAsync(false);
                var task_programaEspecificoHijos = _unitOfWork.PEspecificoRepository.ObtenerPEspecificoHijoFiltroAsync();
                var task_centroCostoPersonalizado = _unitOfWork.CentroCostoRepository.ObtenerFiltroPorTipoAsync(false);
                var task_programaespecificoWebinar = _unitOfWork.PEspecificoRepository.ObtenerListaPEspecificosRelacionadosAsync();

                PEspecificoModuloComboDTO resultado = new();
                resultado.Producto = await task_producto;
                resultado.Proveedor = await task_proveedor;
                resultado.ProveedorCurso = await task_proveedorCurso;
                resultado.ProductoPresentacion = await task_productoPresentacion;
                resultado.ProgramaGeneral = await task_pGeneral;
                resultado.CentroCosto = await task_centroCosto;
                resultado.Modalidad = await task_modalidades;
                resultado.LocacionTroncal = await task_locacionTroncal;
                resultado.Ambiente = await task_ambiente;
                resultado.Origen = await task_origenProgramas;
                resultado.Locacion = await task_locacion;
                resultado.Expositor = await task_expositores;
                resultado.Frecuencia = await task_frecuencia;
                resultado.EstadoPEspecifico = await task_estadoPespecifico;
                resultado.PersonalAreaTrabajo = await task_personalAreaTrabajo;
                resultado.Ciudad = await task_ciudad;
                resultado.Ciclo = await task_ciclo;
                resultado.PeriodoLectivo = await task_periodoLectivo;
                resultado.CiudadBS = await task_ciudadBs;
                resultado.AreaCapacitacion = await task_area;
                resultado.SubAreaCapacitacion = await task_subArea;
                resultado.ProgramaGeneralP = await task_programaGeneral;
                resultado.ProgramaEspecifico = await task_programaEspecifico;
                resultado.ProgramaEspecificoHijos = await task_programaEspecificoHijos;
                resultado.CentroCostoP = await task_centroCostoPersonalizado;
                resultado.ProgramaEspecificoWebinar = await task_programaespecificoWebinar;
                resultado.PlantillaCorreo = await task_plantillaCorreo;
                resultado.PlantillaWhatsApp = await task_plantillaWhatsApp;
                resultado.TiempoFrecuencia = await task_tiempoFrecuencia;
                resultado.Dias = await task_dias;
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Combo completo de Programa Especifico
        /// </summary>
        /// <returns> Lista DTO - List<PEspecificoComboDTO> - rpta </returns>
        public IEnumerable<ComboDTO> ObtenerProgramaEspecifico()
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerProgramaEspecifico();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Version 1.0
        /// <summary>
        /// Obtiene combo de PEspecificos por PGeneral
        /// </summary>
        /// <param name="idPgeneral"></param>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerCombosPEpecificoPorProgramaGeneral(List<int> idPGeneral)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerProgramaEspecificoPorIdPGeneral(idPGeneral);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuracion de webinar por pEspecifico
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns> Datos de configuracion de programas webex -rpta </returns>
        public DatosConfiguracionProgramasWebexDTO ObtenerConfiguracionWebinarPEspecifico(int idPEspecifico)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerConfiguracionWebinarPEspecifico(idPEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma para modulo por codigo
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <returns></returns>
        public string ObtenerCronogramaParaModuloAlterno(int idPespecifico)
        {
            try
            {
                DatosProgramaEspecificoDTO programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerProgramaEspecificoPorCodigo(idPespecifico);
                IPEspecificoSesionService pEspecificoSesionService = new PEspecificoSesionService(_unitOfWork);
                var sesiones = pEspecificoSesionService.ObtenerCronogramaIndividualPorPEspecificoAlterno(programaEspecifico);
                string url = GenerarPDFCronograma(programaEspecifico.Id, programaEspecifico.Nombre, "System", sesiones);
                if (programaEspecifico.IdCiudad == 2)
                {
                    var ListaDiferencia = _unitOfWork.DiferenciaHorariaRepository.ObtenerPorIdPaisOrigen(51);
                    foreach (var item in ListaDiferencia)
                    {
                        var sesionHoraria = sesiones.ToList();
                        sesionHoraria.ForEach(f => { f.FechaHoraInicio = f.FechaHoraInicio.AddHours(item.DiferenciaHoraria); });
                        GenerarPDFCronogramaAlterno(programaEspecifico.Id, true, $"{programaEspecifico.Nombre}-{item.IdPaisDestino}", "System", sesionHoraria, item.IdPaisDestino);
                        sesionHoraria.ForEach(f => { f.FechaHoraInicio = f.FechaHoraInicio.AddHours(-item.DiferenciaHoraria); });
                    }
                }
                return url;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupo individual
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoIndividual"> Curso Individual</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> usuario</param>
        /// <param name="sesiones"> sesiones</param>        
        /// <returns>urlFile</returns>
        private string GenerarPDFCronograma(int idPespecifico, string cursoNombre, string usuario, IEnumerable<PespecificoSesionCompuestoDTO> sesiones)
        {
            try
            {
                var sesionesAsicronas = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo.Equals("Online Asincronica"));
                var sesionesNoAsicronas = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();
                IEnumerable<PespecificoSesionCompuestoDTO> sesionesOrdernada;

                if (sesionesAsicronas != null && sesionesAsicronas.Count() > 0)
                {
                    sesionesNoAsicronas.AddRange(sesionesAsicronas.DistinctBy(x => x.Curso));
                    sesionesOrdernada = sesionesNoAsicronas.OrderBy(x => x.FechaHoraInicio);
                }
                else
                {
                    sesionesOrdernada = sesiones.OrderBy(x => x.FechaHoraInicio);
                }
                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idPespecifico);
                if (pEspecifico == null || pEspecifico.Id == 0)
                {
                    throw new BadRequestException("No existe la entidad pespecifico");
                }
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                _container = blobClient.GetContainerReference(pEspecifico.Codigo);
                _container.CreateIfNotExistsAsync();
                _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });

                var fileResultBytes = ProcesarDatosPDF(sesionesOrdernada, cursoNombre);
                string urlFile = _blockBlob.Uri.AbsoluteUri.ToString();

                pEspecifico.UrlDocumentoCronograma = urlFile;
                pEspecifico.UsuarioModificacion = usuario;
                pEspecifico.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                _unitOfWork.Commit();
                return urlFile;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupo individual version 2
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoIndividual"> Curso Individual</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> Usuario</param>
        /// <param name="sesiones"> sesiones</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>UrlFile</returns>
        public string GenerarPDFCronogramaV2(int idPespecifico, bool? cursoIndividual, string cursoNombre, string usuario, List<PespecificoSesionCompuestoDTO> sesiones, int grupo)
        {
            try
            {
                //se añaden las sesiones de los grupos anteriores
                sesiones = CalcularSesionesCrongoramaCompletoDesdeGrupo(idPespecifico, grupo);

                if (sesiones.Any(w => w.Tipo == null))
                {
                    throw new Exception("Hay una sesion con modalidadcurso nula o tipo nulo el pespecifico");
                }

                string tmpDir = "cronogramas//";
                var ListaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                var ListaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();
                List<PespecificoSesionCompuestoDTO> ListaOrdernada = new List<PespecificoSesionCompuestoDTO>();
                var registrosCronograma = ListaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                string nombreCurso = string.Empty;

                if (registrosCronograma != null && registrosCronograma.Count > 0)
                {
                    foreach (var iAsincro in registrosCronograma)
                    {
                        if (nombreCurso != iAsincro.Curso)
                        {
                            ListaOrdernadaAux.Add(iAsincro);
                            nombreCurso = iAsincro.Curso;
                        }
                    }
                    ListaOrdernada = ListaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                else
                {
                    ListaOrdernada = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                RegistroProgramaEspecificoDTO especifico = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(idPespecifico)!;
                tmpDir += especifico.Codigo;
                //--------------------------------------------------------
                //Configura los nombres de los programas como programa general              
                var cursos = sesiones.GroupBy(test => test.Curso).Select(grp => grp.First()).ToList();
                foreach (var cur in cursos)
                {
                    var especificoCurso = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(cur.PEspecificoHijoId.Value);
                }
                //Configura texto sesion especial
                foreach (var sesi in sesiones)
                {
                    if (sesi.PEspecificoHijoId == 6009)
                    {
                        sesi.Curso = "Sesión audiovisual";
                    }
                }
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                _container = blobClient.GetContainerReference(especifico.Codigo);
                _container.CreateIfNotExistsAsync();
                _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                if (cursoIndividual.HasValue)
                {
                    if (cursoIndividual.Value)
                    {
                        var fileResultBytes1 = ProcesarDatosPDF(ListaOrdernada, cursoNombre);
                        string UrlFile2 = _blockBlob.Uri.AbsoluteUri.ToString();
                        var finalPespecifico = _unitOfWork.PEspecificoRepository.FirstById(especifico.Id);
                        finalPespecifico.UrlDocumentoCronograma = UrlFile2;
                        finalPespecifico.UsuarioModificacion = usuario;
                        finalPespecifico.FechaModificacion = DateTime.Now;
                        _unitOfWork.PEspecificoRepository.Update(finalPespecifico);
                        _unitOfWork.Commit();
                        return UrlFile2;
                    }
                }
                var fileResultBytes = ProcesarDatosPDF(ListaOrdernada, cursoNombre);
                string UrlFile = _blockBlob.Uri.AbsoluteUri.ToString();
                var finalPespecifico2 = _unitOfWork.PEspecificoRepository.ObtenerPorId(especifico.Id)!;
                finalPespecifico2.UrlDocumentoCronograma = UrlFile;
                finalPespecifico2.UsuarioModificacion = usuario;
                finalPespecifico2.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoRepository.Update(finalPespecifico2);
                _unitOfWork.Commit();
                return UrlFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 07/09/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupo individual version 2
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoIndividual"> Curso Individual</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> Usuario</param>
        /// <param name="sesiones"> sesiones</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>UrlFile</returns>
        public string GenerarPDFCronogramaSemanal(int idPespecifico, bool? cursoIndividual, string cursoNombre, string usuario, List<PespecificoSesionCompuestoDTO> sesiones, int grupo)
        {
            try
            {
                //se añaden las sesiones de los grupos anteriores
                //sesiones = CalcularSesionesCrongoramaCompletoDesdeGrupo(idPespecifico, grupo);

                if (sesiones.Any(w => w.Tipo == null))
                {
                    throw new Exception("Hay una sesion con modalidadcurso nula o tipo nulo el pespecifico");
                }

                string tmpDir = "cronogramas//";
                var ListaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                var ListaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();
                List<PespecificoSesionCompuestoDTO> ListaOrdernada = new List<PespecificoSesionCompuestoDTO>();
                var registrosCronograma = ListaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                string nombreCurso = string.Empty;

                if (registrosCronograma != null && registrosCronograma.Count > 0)
                {
                    foreach (var iAsincro in registrosCronograma)
                    {
                        if (nombreCurso != iAsincro.Curso)
                        {
                            ListaOrdernadaAux.Add(iAsincro);
                            nombreCurso = iAsincro.Curso;
                        }
                    }
                    ListaOrdernada = ListaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                else
                {
                    ListaOrdernada = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                RegistroProgramaEspecificoDTO especifico = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(idPespecifico)!;
                tmpDir += especifico.Codigo;
                //--------------------------------------------------------
                //Configura los nombres de los programas como programa general              
                var cursos = sesiones.GroupBy(test => test.Curso).Select(grp => grp.First()).ToList();
                foreach (var cur in cursos)
                {
                    var especificoCurso = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(cur.PEspecificoHijoId.Value);
                }
                //Configura texto sesion especial
                foreach (var sesi in sesiones)
                {
                    if (sesi.PEspecificoHijoId == 6009)
                    {
                        sesi.Curso = "Sesión audiovisual";
                    }
                }
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                _container = blobClient.GetContainerReference(especifico.Codigo);
                _container.CreateIfNotExistsAsync();
                _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                if (cursoIndividual.HasValue)
                {
                    if (cursoIndividual.Value)
                    {
                        var fileResultBytes1 = ProcesarDatosPDFSemanal(ListaOrdernada, cursoNombre);
                        string UrlFile2 = _blockBlob.Uri.AbsoluteUri.ToString();
                        var finalPespecifico = _unitOfWork.PEspecificoRepository.FirstById(especifico.Id);
                        finalPespecifico.UrlDocumentoCronograma = UrlFile2;
                        finalPespecifico.UsuarioModificacion = usuario;
                        finalPespecifico.FechaModificacion = DateTime.Now;
                        _unitOfWork.PEspecificoRepository.Update(finalPespecifico);
                        _unitOfWork.Commit();
                        return UrlFile2;
                    }
                }
                var fileResultBytes = ProcesarDatosPDFSemanal(ListaOrdernada, cursoNombre);
                string UrlFile = _blockBlob.Uri.AbsoluteUri.ToString();
                var finalPespecifico2 = _unitOfWork.PEspecificoRepository.ObtenerPorId(especifico.Id)!;
                finalPespecifico2.UrlDocumentoCronograma = UrlFile;
                finalPespecifico2.UsuarioModificacion = usuario;
                finalPespecifico2.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoRepository.Update(finalPespecifico2);
                _unitOfWork.Commit();
                return UrlFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio .
        /// Fecha: 23/04/2021
        /// Version: 1.0
        /// <summary>
        /// Calcular Sesiones Crongorama Completo Desde Grupo
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="grupo"> grupo</param>
        /// <returns>sesiones</returns>
        private List<PespecificoSesionCompuestoDTO> CalcularSesionesCrongoramaCompletoDesdeGrupo(int idPespecifico, int grupo)
        {
            List<PespecificoSesionCompuestoDTO> listado = new List<PespecificoSesionCompuestoDTO>();

            for (int grupoAnterior = grupo; grupoAnterior > 0; grupoAnterior--)
            {
                var sesionesPEspecifico = _unitOfWork.PEspecificoSesionRepository.ObtenerSesionesPorPEspecificoGrupoAnterior(idPespecifico, grupoAnterior);

                if (sesionesPEspecifico != null && sesionesPEspecifico.Count() > 0)
                {
                    var listadoIdPespecificosExistentes = listado.Select(s => s.IdPespecifico).Distinct().ToList();
                    var listadoSesionesAdicionar = sesionesPEspecifico.Where(w => !listadoIdPespecificosExistentes.Contains(w.IdPEspecifico)).ToList();
                    listado.AddRange(listadoSesionesAdicionar.Select(s => new PespecificoSesionCompuestoDTO()
                    {
                        Curso = s.Curso,
                        Duracion = s.Duracion,
                        DuracionTotal = s.DuracionTotal,
                        FechaHoraInicio = s.FechaHoraInicio,
                        IdExpositor = s.IdExpositor,
                        IdPespecifico = s.IdPEspecifico,
                        Id = s.Id,
                        Tipo = s.ModalidadSesion,
                        PEspecificoHijoId = s.IdPEspecifico
                    }));
                }
            }
            return listado;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaOrdernada"> Id Programa Especifico</param>
        /// <param name="cursoNombre"> Curso Individual</param>
        /// <param name="tmpDir"> Curso Nombre</param>
        /// <returns>urlFile</returns>
        private byte[] ProcesarDatosPDF(IEnumerable<PespecificoSesionCompuestoDTO> listaOrdernada, string cursoNombre)
        {
            try
            {
                IList<string[]> rows = new List<string[]>();
                foreach (var item in listaOrdernada)
                {
                    string modalidad = item.Tipo ?? string.Empty;
                    DateTime fecha = item.FechaHoraInicio;
                    decimal duracionTotal = (modalidad == "Online Asincronica" ? item.DuracionTotal.Value : item.Duracion.Value);
                    /*
                     * [0] Año
                     * [1] Mes, 
                     * [2] Dia, 
                     * [3] Fecha, 
                     * [4] Horarios, 
                     * [5] Curso, 
                     * [6] Duración
                     * [7] Fecha
                     * [8] Modalidad
                     */
                    string[] columnas = new string[9];
                    columnas[0] = fecha.Year.ToString();
                    columnas[1] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")));
                    columnas[2] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es")));
                    columnas[3] = fecha.Day.ToString();
                    DateTime fechaF = fecha.AddHours(Convert.ToDouble(item.Duracion.Value));
                    columnas[4] = (modalidad == "Online Asincronica" ? "" : fecha.ToString("HH:mm") + " a " + fechaF.ToString("HH:mm") + " horas");
                    columnas[5] = item.Curso;
                    columnas[6] = string.Format("{0:0.##}", duracionTotal).Replace(",", ".");
                    columnas[7] = fecha.ToString("yyyy/MM/dd");
                    columnas[8] = modalidad;
                    rows.Add(columnas);
                }

                rows.Insert(0, new string[] { "Año", "Mes", "Día", "Fecha", "Horarios", "Curso", "Duración", "Modalidad" });
                //Aqui Generamos el PDF en memoria y retornamos un Byte[]
                var pdf = GenerarBytePDF(rows, cursoNombre);
                _blockBlob = _container.GetBlockBlobReference(ToURLSlug(cursoNombre) + ".pdf");
                _blockBlob.Properties.ContentType = "application/pdf";
                _blockBlob.Metadata["filename"] = ToURLSlug(cursoNombre) + ".pdf";
                _blockBlob.Metadata["filemime"] = "application/pdf";
                Stream stream = new MemoryStream(pdf);
                _blockBlob.UploadFromStreamAsync(stream).Wait();
                return pdf;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaOrdernada"> Id Programa Especifico</param>
        /// <param name="cursoNombre"> Curso Individual</param>
        /// <param name="tmpDir"> Curso Nombre</param>
        /// <returns>urlFile</returns>
        private byte[] ProcesarDatosPDFSemanal(IEnumerable<PespecificoSesionCompuestoDTO> listaOrdernada, string cursoNombre)
        {
            try
            {
                IList<string[]> rows = new List<string[]>();
                foreach (var item in listaOrdernada)
                {
                    string modalidad = item.Tipo ?? string.Empty;
                    DateTime fecha = item.FechaHoraInicio;
                    decimal duracionTotal = (modalidad == "Online Asincronica" ? item.DuracionTotal.Value : item.Duracion.Value);
                    /*
                     * [0] Año
                     * [1] Mes, 
                     * [2] Dia, 
                     * [3] Fecha, 
                     * [4] Horarios, 
                     * [5] Curso, 
                     * [6] Duración
                     * [7] Fecha
                     * [8] Modalidad
                     */
                    string[] columnas = new string[5];
                    columnas[0] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es")));
                    DateTime fechaF = fecha.AddHours(Convert.ToDouble(item.Duracion.Value));
                    columnas[1] = (modalidad == "Online Asincronica" ? "" : fecha.ToString("HH:mm") + " a " + fechaF.ToString("HH:mm") + " horas");
                    columnas[2] = item.Curso;
                    columnas[3] = string.Format("{0:0.##}", duracionTotal).Replace(",", ".");
                    columnas[4] = modalidad;
                    rows.Add(columnas);
                }

                rows.Insert(0, new string[] { "Día", "Horarios", "Curso", "Duración", "Modalidad" });
                //Aqui Generamos el PDF en memoria y retornamos un Byte[]
                var pdf = GenerarBytePDFSemanal(rows, cursoNombre);
                _blockBlob = _container.GetBlockBlobReference(ToURLSlug(cursoNombre) + ".pdf");
                _blockBlob.Properties.ContentType = "application/pdf";
                _blockBlob.Metadata["filename"] = ToURLSlug(cursoNombre) + ".pdf";
                _blockBlob.Metadata["filemime"] = "application/pdf";
                Stream stream = new MemoryStream(pdf);
                _blockBlob.UploadFromStreamAsync(stream).Wait();
                return pdf;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private IList<string[]> ProcesarDatosPDFGrupal(IEnumerable<PEspecificoCronogramaGrupalDTO> listaOrdernada)
        {
            try
            {
                IList<string[]> rows = new List<string[]>();

                foreach (var item in listaOrdernada)
                {
                    string modalidad = (string.IsNullOrEmpty(item.Tipo) ? "" : item.Tipo);
                    double dur = item.Duracion;
                    DateTime fecha = item.FechaHoraInicio;
                    string curso = item.Curso;
                    string durTot = "0";
                    durTot = modalidad == "Online Asincronica" ? item.DuracionTotal : item.Duracion.ToString();
                    string[] columnas = new string[9]; //año, mes, día, fecha, horarios, curso, duración
                    columnas[0] = fecha.Year.ToString();
                    columnas[1] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("MMMM", CultureInfo.CreateSpecificCulture("es")));
                    columnas[2] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fecha.ToString("dddd", CultureInfo.CreateSpecificCulture("es")));
                    columnas[3] = fecha.Day.ToString();
                    DateTime fechaF = fecha.AddHours(Convert.ToDouble(dur));
                    columnas[4] = (modalidad == "Online Asincronica" ? "" : fecha.ToString("HH:mm") + " a " + fechaF.ToString("HH:mm") + " horas");
                    columnas[5] = curso;
                    //columnas[6] = duracionTotal;
                    columnas[6] = String.Format("{0:0.##}", durTot).Replace(",", ".");
                    columnas[7] = columnas[0] + "/" + fecha.ToString("MM") + "/" + columnas[3];
                    columnas[8] = modalidad;

                    rows.Add(columnas);
                }

                IList<string[]> newRows = rows.ToList(); //OrderBy(a => a[7])
                newRows.Insert(0, new string[] { "Año", "Mes", "Día", "Fecha", "Horarios", "Curso", "Duración", "Modalidad" });
                return newRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Genera el cuerpo del cronograma
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="nombreCurso"></param>
        /// <returns></returns>
        public byte[] GenerarBytePDF(IList<string[]> rows, string nombreCurso)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document _document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 65f, 65f, 120f, 65f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsCronograma();

                    _document.AddTitle("BSG Institute - Cronograma " + nombreCurso);
                    _document.AddCreator("BSG institute");
                    _document.Open();

                    iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                    var para = new iTextSharp.text.Paragraph("Cronograma de alumnos del " + nombreCurso, _standardFont2);
                    para.Alignment = Element.ALIGN_CENTER;
                    _document.Add(para);
                    _document.Add(Chunk.NEWLINE);

                    //De aquí en adelante se crerá la tabla del cronograma:
                    //html += "<tr style='background-color: rgba(0, 0, 0, 0.15);'>";
                    iTextSharp.text.Font _FEncabezadoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                    iTextSharp.text.Font _FCuerpoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.5f, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                    PdfPTable TablaCronograma = new PdfPTable(8);
                    TablaCronograma.DefaultCell.Padding = 4f;
                    TablaCronograma.WidthPercentage = 100;
                    TablaCronograma.HorizontalAlignment = Element.ALIGN_CENTER;
                    float[] widthsTCronograma = new float[] { 10f, 15f, 15f, 10f, 15f, 45f, 15f, 15f };
                    TablaCronograma.SetWidths(widthsTCronograma);
                    bool FirstRow = true;
                    int cont = 0;
                    foreach (var Row in rows)
                    {
                        if (FirstRow)
                        {
                            //cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 220);
                            PdfPCell cell2 = new PdfPCell();
                            cell2 = new PdfPCell(new Phrase(Row[0], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[1], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[2], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[3], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[4], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[5], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[7], _FEncabezadoCrograma)); // tipo de modalidad
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);
                            FirstRow = false;

                            cell2 = new PdfPCell(new Phrase(Row[6] + " (*)", _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);
                            FirstRow = false;
                        }
                        else
                        {
                            PdfPCell cell3 = new PdfPCell();
                            cell3 = new PdfPCell(new Phrase(Row[0], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[1], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[2], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[3], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[4], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[5], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            //cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[8], _FCuerpoCrograma)); // tipo de modalidad
                            cell3.Padding = 4f;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.HorizontalAlignment = 1;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[6], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.HorizontalAlignment = 1;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);
                        }
                    }
                    _document.Add(TablaCronograma);

                    _document.Close();

                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Gretel Canasa
        /// Genera el cuerpo del cronograma
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="nombreCurso"></param>
        /// <returns></returns>
        public byte[] GenerarBytePDFSemanal(IList<string[]> rows, string nombreCurso)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document _document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 65f, 65f, 120f, 65f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsCronograma();

                    _document.AddTitle("BSG Institute - Cronograma " + nombreCurso);
                    _document.AddCreator("BSG institute");
                    _document.Open();

                    iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                    var para = new iTextSharp.text.Paragraph("Cronograma de alumnos del " + nombreCurso, _standardFont2);
                    para.Alignment = Element.ALIGN_CENTER;
                    _document.Add(para);
                    _document.Add(Chunk.NEWLINE);

                    //De aquí en adelante se crerá la tabla del cronograma:
                    //html += "<tr style='background-color: rgba(0, 0, 0, 0.15);'>";
                    iTextSharp.text.Font _FEncabezadoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                    iTextSharp.text.Font _FCuerpoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.5f, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                    PdfPTable TablaCronograma = new PdfPTable(5);
                    TablaCronograma.DefaultCell.Padding = 4f;
                    TablaCronograma.WidthPercentage = 100;
                    TablaCronograma.HorizontalAlignment = Element.ALIGN_CENTER;
                    float[] widthsTCronograma = new float[] { 10f, 15f, 35f, 10f, 15f };
                    TablaCronograma.SetWidths(widthsTCronograma);
                    bool FirstRow = true;
                    int cont = 0;
                    foreach (var Row in rows)
                    {
                        if (FirstRow)
                        {
                            //cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 220);
                            PdfPCell cell2 = new PdfPCell();
                            cell2 = new PdfPCell(new Phrase(Row[0], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[1], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[2], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[3], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);

                            cell2 = new PdfPCell(new Phrase(Row[4], _FEncabezadoCrograma));
                            cell2.Padding = 4f;
                            cell2.HorizontalAlignment = 1;
                            cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell2.BorderWidth = 1f;
                            cell2.BorderColor = BaseColor.LIGHT_GRAY;
                            cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                            TablaCronograma.AddCell(cell2);
                            FirstRow = false;
                        }
                        else
                        {
                            PdfPCell cell3 = new PdfPCell();
                            cell3 = new PdfPCell(new Phrase(Row[0], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[1], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[2], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[3], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);

                            cell3 = new PdfPCell(new Phrase(Row[4], _FCuerpoCrograma));
                            cell3.Padding = 4f;
                            cell3.HorizontalAlignment = 1;
                            cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell3.BorderWidth = 1f;
                            cell3.BorderColor = BaseColor.LIGHT_GRAY;
                            TablaCronograma.AddCell(cell3);
                        }
                    }
                    _document.Add(TablaCronograma);

                    _document.Close();

                }
                return ms.ToArray();
            }
        }
        public byte[] GenerarBytePDFGrupal(string nombreCurso, IEnumerable<PEspecificoCronogramaGrupalGrupoDTO> listaSesiones)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document _document = new Document(iTextSharp.text.PageSize.A4.Rotate(), 65f, 65f, 120f, 65f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsCronograma();

                    _document.AddTitle("BSG Institute - Cronograma " + nombreCurso);
                    _document.AddCreator("BSG institute");
                    _document.Open();

                    iTextSharp.text.Font _standardFont2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                    var para = new iTextSharp.text.Paragraph("Cronograma de alumnos del " + nombreCurso, _standardFont2);
                    para.Alignment = Element.ALIGN_CENTER;
                    _document.Add(para);
                    _document.Add(Chunk.NEWLINE);

                    IList<string[]> rows;
                    foreach (var grupo in listaSesiones)
                    {
                        rows = ProcesarDatosPDFGrupal(grupo.Lista);

                        iTextSharp.text.Font _standardFontGrupo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                        var textGrupo = new iTextSharp.text.Paragraph("Grupo " + grupo.Grupo, _standardFontGrupo);
                        para.Alignment = Element.ALIGN_LEFT;
                        _document.Add(textGrupo);
                        _document.Add(new Paragraph("\n"));

                        //De aquí en adelante se crerá la tabla del cronograma:
                        //html += "<tr style='background-color: rgba(0, 0, 0, 0.15);'>";
                        iTextSharp.text.Font _FEncabezadoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                        iTextSharp.text.Font _FCuerpoCrograma = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.5f, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);

                        PdfPTable TablaCronograma = new PdfPTable(8);
                        TablaCronograma.DefaultCell.Padding = 4f;
                        TablaCronograma.WidthPercentage = 100;
                        TablaCronograma.HorizontalAlignment = Element.ALIGN_CENTER;
                        float[] widthsTCronograma = new float[] { 10f, 15f, 15f, 10f, 15f, 45f, 15f, 15f };
                        TablaCronograma.SetWidths(widthsTCronograma);
                        bool FirstRow = true;
                        int cont = 0;
                        foreach (var Row in rows)
                        {
                            if (FirstRow)
                            {
                                //cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 220);
                                PdfPCell cell2 = new PdfPCell();
                                cell2 = new PdfPCell(new Phrase(Row[0], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[1], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[2], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[3], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[4], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[5], _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);

                                cell2 = new PdfPCell(new Phrase(Row[7], _FEncabezadoCrograma)); // tipo de modalidad
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);
                                FirstRow = false;

                                cell2 = new PdfPCell(new Phrase(Row[6] + " (*)", _FEncabezadoCrograma));
                                cell2.Padding = 4f;
                                cell2.HorizontalAlignment = 1;
                                cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell2.BorderWidth = 1f;
                                cell2.BorderColor = BaseColor.LIGHT_GRAY;
                                cell2.BackgroundColor = new iTextSharp.text.BaseColor(204, 255, 255);
                                TablaCronograma.AddCell(cell2);
                                FirstRow = false;
                            }
                            else
                            {
                                PdfPCell cell3 = new PdfPCell();
                                cell3 = new PdfPCell(new Phrase(Row[0], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[1], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[2], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[3], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[4], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[5], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                //cell3.HorizontalAlignment = 1;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[8], _FCuerpoCrograma)); // tipo de modalidad
                                cell3.Padding = 4f;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.HorizontalAlignment = 1;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);

                                cell3 = new PdfPCell(new Phrase(Row[6], _FCuerpoCrograma));
                                cell3.Padding = 4f;
                                cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell3.HorizontalAlignment = 1;
                                cell3.BorderWidth = 1f;
                                cell3.BorderColor = BaseColor.LIGHT_GRAY;
                                TablaCronograma.AddCell(cell3);
                            }
                        }
                        _document.Add(TablaCronograma);
                        _document.Add(Chunk.NEXTPAGE);
                    }

                    _document.Close();

                }
                return ms.ToArray();
            }
        }

        public string ToURLSlug(string s)
        {
            return Regex.Replace(s, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase)
                .Trim(new char[] { '-' })
                .ToLower();
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 07/07/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupo individual alterno
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoIndividual"> Curso Individual</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="usuario"> usuario</param>
        /// <param name="sesiones"> sesiones</param>    
        /// <param name="idPais"> grupo</param>
        /// <returns>urlFile</returns>
        public string GenerarPDFCronogramaAlterno(int idPespecifico, bool? cursoIndividual, string cursoNombre, string usuario, List<PespecificoSesionCompuestoDTO> sesiones, int idPais)
        {
            try
            {
                string tmpDir = "cronogramas//";
                var listaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                var listaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();
                List<PespecificoSesionCompuestoDTO> listaOrdernada;
                var registrosCronograma = listaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                string nombreCurso = string.Empty;

                if (registrosCronograma != null && registrosCronograma.Count > 0)
                {
                    foreach (var iAsincro in registrosCronograma)
                    {
                        if (nombreCurso != iAsincro.Curso)
                        {
                            listaOrdernadaAux.Add(iAsincro);
                            nombreCurso = iAsincro.Curso;
                        }
                    }
                    listaOrdernada = listaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                else
                {
                    listaOrdernada = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                }
                RegistroProgramaEspecificoDTO especifico = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(idPespecifico)!;
                if (especifico == null || especifico.Id == 0)
                {
                    throw new BadRequestException("No existe pespecifico");
                }
                tmpDir += especifico.Codigo;
                //Configura los nombres de los programas como programa general              
                var cursos = sesiones.GroupBy(test => test.Curso).Select(grp => grp.First()).ToList();
                foreach (var cur in cursos)
                {
                    var especificoCurso = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(cur.PEspecificoHijoId.Value);
                }
                //Configura texto sesion especial
                foreach (var sesi in sesiones)
                {
                    if (sesi.PEspecificoHijoId == 6009)
                    {
                        sesi.Curso = "Sesión audiovisual";
                    }
                }
                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                _container = blobClient.GetContainerReference(especifico.Codigo);
                _container.CreateIfNotExistsAsync();
                _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                if (cursoIndividual.HasValue)
                {
                    if (cursoIndividual.Value)
                    {
                        var fileResultBytes1 = ProcesarDatosPDF(listaOrdernada, cursoNombre);
                        string UrlFile2 = _blockBlob.Uri.AbsoluteUri.ToString();
                        var cronogramaPEspecifico = _unitOfWork.PespecificoCronogramaRepository.ObtenerPorIdPespecificoPorIdPais(especifico.Id, idPais);
                        if (cronogramaPEspecifico == null)
                        {
                            PespecificoCronograma nuevoCronograma = new PespecificoCronograma()
                            {
                                IdPespecifico = especifico.Id,
                                IdPais = idPais,
                                UrlDocumentoCronograma = UrlFile2,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                            };
                            _unitOfWork.PespecificoCronogramaRepository.Add(nuevoCronograma);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            cronogramaPEspecifico.UrlDocumentoCronograma = UrlFile2;
                            cronogramaPEspecifico.UsuarioModificacion = usuario;
                            cronogramaPEspecifico.FechaModificacion = DateTime.Now;
                            _unitOfWork.PespecificoCronogramaRepository.Update(cronogramaPEspecifico);
                            _unitOfWork.Commit();
                        }
                        return UrlFile2;
                    }
                }
                var fileResultBytes = ProcesarDatosPDF(listaOrdernada, cursoNombre);
                string UrlFile = _blockBlob.Uri.AbsoluteUri.ToString();
                var cronogramaPEspecifico2 = _unitOfWork.PespecificoCronogramaRepository.ObtenerPorIdPespecificoPorIdPais(especifico.Id, idPais);
                if (cronogramaPEspecifico2 == null)
                {
                    PespecificoCronograma nuevoCronograma = new PespecificoCronograma();
                    nuevoCronograma.IdPespecifico = especifico.Id;
                    nuevoCronograma.IdPais = idPais;
                    nuevoCronograma.UrlDocumentoCronograma = UrlFile;
                    nuevoCronograma.Estado = true;
                    nuevoCronograma.UsuarioCreacion = usuario;
                    nuevoCronograma.UsuarioModificacion = usuario;
                    nuevoCronograma.FechaCreacion = DateTime.Now;
                    nuevoCronograma.FechaModificacion = DateTime.Now;
                    _unitOfWork.PespecificoCronogramaRepository.Add(nuevoCronograma);
                    _unitOfWork.Commit();
                }
                else
                {
                    cronogramaPEspecifico2.UrlDocumentoCronograma = UrlFile;
                    cronogramaPEspecifico2.UsuarioModificacion = usuario;
                    cronogramaPEspecifico2.FechaModificacion = DateTime.Now;
                    _unitOfWork.PespecificoCronogramaRepository.Update(cronogramaPEspecifico2);
                    _unitOfWork.Commit();
                }
                return UrlFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerarCronogramaGrupal(int idPespecifico, string usuario)
        {
            try
            {
                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idPespecifico);
                var resultado = GenerarPDFCronogramaGrupal(idPespecifico, pEspecifico.Nombre, usuario);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Genera Pdf Cronograma Programa Grupal
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="cursoNombre"> Curso Nombre</param>
        /// <param name="Usuario"> usuario</param>
        /// <returns>urlFile</returns>
        public string GenerarPDFCronogramaGrupal(int idPespecifico, string cursoNombre, string usuario)
        {
            try
            {
                IPespecificoCronogramaService pEspecificoCronogramaService = new PespecificoCronogramaService(_unitOfWork);
                var listaSesiones = pEspecificoCronogramaService.ObtenerPEspecificoCronogramaGrupal(idPespecifico);
                var progEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idPespecifico);
                if (listaSesiones.Count() == 0)
                {
                    throw new Exception("Este programa no tiene sesiones");
                }
                foreach (var item in listaSesiones)
                {
                    item.Lista = pEspecificoCronogramaService.CalcularSesionesCronogramaGrupoCompletoDesdeGrupo(idPespecifico, item.Grupo);
                }
                foreach (var listaSesion in listaSesiones)
                {
                    var sesiones = listaSesion.Lista;
                    var ListaOrdernadaInicio = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                    var ListaOrdernadaAux = sesiones.OrderBy(x => x.FechaHoraInicio).Where(x => x.Tipo != "Online Asincronica").ToList();
                    var registrosCronograma = ListaOrdernadaInicio.Where(x => x.Tipo.Equals("Online Asincronica")).ToList();
                    string nombreCurso = string.Empty;

                    if (registrosCronograma != null && registrosCronograma.Count > 0)
                    {
                        foreach (var iAsincro in registrosCronograma)
                        {
                            if (nombreCurso != iAsincro.Curso)
                            {
                                ListaOrdernadaAux.Add(iAsincro);
                                nombreCurso = iAsincro.Curso;
                            }
                        }
                        listaSesion.Lista = ListaOrdernadaAux.OrderBy(x => x.FechaHoraInicio).ToList();
                    }
                    else
                    {
                        listaSesion.Lista = sesiones.OrderBy(x => x.FechaHoraInicio).ToList();
                    }
                }

                RegistroProgramaEspecificoDTO especifico = _unitOfWork.PEspecificoRepository.ObtenerRegistroPespecificoPorId(idPespecifico);

                CloudStorageAccount storageAccount = new CloudStorageAccount(new StorageCredentials("repositoriointegra", "gIaT0DXh2VL1BeK8lWvp5FU8LcJXkS8mzydcO3aB8n7R0TSQ5cEb1NPcz+ZSr7PVq5trhtYjdZHbAQaStAe2ZA=="), true);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                _container = blobClient.GetContainerReference("000000000");
                _container.CreateIfNotExistsAsync();
                _container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                var fileResultBytes = GenerarBytePDFGrupal(cursoNombre, listaSesiones);
                _blockBlob = _container.GetBlockBlobReference(ToURLSlug(especifico.Codigo + "-" + cursoNombre) + ".pdf");
                _blockBlob.Properties.ContentType = "application/pdf";
                _blockBlob.Metadata["filename"] = ToURLSlug(especifico.Codigo + "-" + cursoNombre) + ".pdf";
                _blockBlob.Metadata["filemime"] = "application/pdf";
                Stream stream = new MemoryStream(fileResultBytes);
                var objetoBlob = _blockBlob.UploadFromStreamAsync(stream);
                objetoBlob.Wait();
                string UrlFile = "error";

                if (objetoBlob.IsCompletedSuccessfully)
                {
                    UrlFile = _blockBlob.Uri.AbsoluteUri.ToString();
                    var finalPespecifico2 = _unitOfWork.PEspecificoRepository.ObtenerPorId(especifico.Id);
                    finalPespecifico2.UrlDocumentoCronogramaGrupos = UrlFile;
                    finalPespecifico2.UsuarioModificacion = usuario;
                    finalPespecifico2.FechaModificacion = DateTime.Now;
                    _unitOfWork.PEspecificoRepository.Update(finalPespecifico2);
                    _unitOfWork.Commit();
                }
                else
                {
                    throw new Exception("Error al subir el Documento al BlobStorage");
                }
                if (progEspecifico.IdCiudad == 2)
                {
                    var listaDiferencia = _unitOfWork.DiferenciaHorariaRepository.ObtenerPorIdPaisOrigen(51);
                    foreach (var horario in listaDiferencia)
                    {
                        var sesionHoraria = listaSesiones;
                        foreach (var sesion in sesionHoraria)
                        {
                            sesion.Lista.ToList().ForEach(x => { x.FechaHoraInicio = x.FechaHoraInicio.AddHours(horario.DiferenciaHoraria); });
                        }

                        var fileResultBytes2 = GenerarBytePDFGrupal(cursoNombre, sesionHoraria);
                        _blockBlob = _container.GetBlockBlobReference(ToURLSlug(especifico.Codigo + "-" + cursoNombre + "-" + horario.IdPaisDestino) + ".pdf");
                        _blockBlob.Properties.ContentType = "application/pdf";
                        _blockBlob.Metadata["filename"] = ToURLSlug(especifico.Codigo + "-" + cursoNombre + "-" + horario.IdPaisDestino) + ".pdf";
                        _blockBlob.Metadata["filemime"] = "application/pdf";
                        Stream stream2 = new MemoryStream(fileResultBytes2);
                        var objetoBlob2 = _blockBlob.UploadFromStreamAsync(stream2);
                        objetoBlob2.Wait();
                        string UrlFile2 = "error";

                        if (objetoBlob2.IsCompletedSuccessfully)
                        {
                            UrlFile2 = _blockBlob.Uri.AbsoluteUri.ToString();

                            var cronogramaGrupalPEspecifico = _unitOfWork.PespecificoCronogramaGrupoRepository.ObtenerPorIdPespecificoPorIdPais(especifico.Id, horario.IdPaisDestino);
                            if (cronogramaGrupalPEspecifico == null)
                            {
                                PespecificoCronogramaGrupo nuevoCronograma = new();
                                nuevoCronograma.IdPespecifico = especifico.Id;
                                nuevoCronograma.IdPais = horario.IdPaisDestino;
                                nuevoCronograma.UrlDocumentoCronogramaGrupo = UrlFile2;
                                nuevoCronograma.Estado = true;
                                nuevoCronograma.UsuarioCreacion = usuario;
                                nuevoCronograma.UsuarioModificacion = usuario;
                                nuevoCronograma.FechaCreacion = DateTime.Now;
                                nuevoCronograma.FechaModificacion = DateTime.Now;
                                _unitOfWork.PespecificoCronogramaGrupoRepository.Add(nuevoCronograma);
                                _unitOfWork.Commit();
                            }
                            else
                            {
                                cronogramaGrupalPEspecifico.UrlDocumentoCronogramaGrupo = UrlFile2;
                                cronogramaGrupalPEspecifico.UsuarioModificacion = usuario;
                                cronogramaGrupalPEspecifico.FechaModificacion = DateTime.Now;
                                _unitOfWork.PespecificoCronogramaGrupoRepository.Update(cronogramaGrupalPEspecifico);
                                _unitOfWork.Commit();
                            }
                        }
                        else
                        {
                            throw new Exception("Error al subir el Documento al BlobStorage");
                        }
                        foreach (var sesion in sesionHoraria)
                        {
                            sesion.Lista.ToList().ForEach(x => { x.FechaHoraInicio = x.FechaHoraInicio.AddHours(-horario.DiferenciaHoraria); });
                        }
                    }
                }
                return UrlFile;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado del programa especifico
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <param name="idEstadoPrograma"> Id del estado del programa</param>
        /// <param name="usuario"> usuario modificacion</param>
        /// <returns>estado e id del programa especifico</returns>
        public (bool Estado, int IdProgramaEspecifico) ActualizarEstadoPrograma(int idPespecifico, int idEstadoPrograma, string usuario)

        {
            try
            {
                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idPespecifico);
                if (pEspecifico == null || pEspecifico.Id == 0)
                {
                    throw new BadRequestException("No se encontro Pespecifico");
                }
                var estadoPEspecifico = _unitOfWork.EstadoPespecificoRepository.ObtenerPorId(idEstadoPrograma);
                if (estadoPEspecifico == null)
                {
                    throw new BadRequestException("No se encontro Estado Pespecifico");
                }

                var estadoAnterior = pEspecifico.IdEstadoPespecifico;

                pEspecifico.EstadoPid = idEstadoPrograma;
                pEspecifico.IdEstadoPespecifico = idEstadoPrograma;
                pEspecifico.EstadoP = estadoPEspecifico.Nombre;
                pEspecifico.UsuarioModificacion = usuario;
                pEspecifico.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                _unitOfWork.Commit();
                if (estadoPEspecifico.Id == EstadoPespecifico.EJECUCION)
                {
                    var hijos = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerDetallePEspecificoHijosPorIdPespecificoPadre(idPespecifico);
                    if (hijos != null && hijos.Count() > 0)
                    {
                        var estadoPorEjecucion = _unitOfWork.EstadoPespecificoRepository.ObtenerPorId(EstadoPespecifico.POR_EJECUCION);
                        var pEspecificos = _unitOfWork.PEspecificoRepository.ObtenerPorIds(hijos.Select(x => x.PEspecificoHijoId));
                        if (pEspecificos != null && pEspecificos.Count() > 0)
                        {
                            pEspecificos.ForEach(x =>
                            {
                                if (x.IdEstadoPespecifico == EstadoPespecifico.LANZAMIENTO)
                                {
                                    x.EstadoPid = estadoPorEjecucion.Id;
                                    x.IdEstadoPespecifico = estadoPorEjecucion.Id;
                                    x.EstadoP = estadoPorEjecucion.Nombre;
                                    x.UsuarioModificacion = usuario;
                                    x.FechaModificacion = DateTime.Now;
                                    _unitOfWork.PEspecificoRepository.Update(x);
                                    _unitOfWork.Commit();
                                }
                            });
                        }
                    }
                }
                if (estadoAnterior == EstadoPespecifico.EJECUCION && estadoPEspecifico.Id == EstadoPespecifico.LANZAMIENTO)
                {
                    List<string> correosPersonalizados = new List<string>
                    {
                        "wvalencia@bsginstitute.com",
                        "wruiz@bsginstitute.com",
                        "sruiz@bsginstitute.com",
                        "lcarpio@bsginstitute.com"
                    };
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "gcanasac@bsginstitute.com",
                        "dhuaita@bsginstitute.com",
                    };

                    string mensaje = "<h3> NOTIFICACIÓN: SE MODIFICÓ EL ESTADO DE UN PROGRAMA ESPECIFICO! </h3> <br>\n\n";
                    mensaje += "<br>\n\n";
                    mensaje += "Se modifico el estado a Lanzamiento del Programa en Ejecución: <br>\n\n";
                    mensaje += "<br>\n\n";
                    mensaje += "Nombre Programa: " + pEspecifico.Nombre + "<br>\n\n";
                    mensaje += "Fecha Modificación: " + DateTime.Now + "<br>\n\n";
                    mensaje += "Usuario Modificación: " + usuario + "<br>\n\n";

                    var mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "soportetecnico@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = "Notificación: Se cambio al estado en Lanzamiento",
                        Message = mensaje,
                        Cc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        Bcc = ""
                    };
                    try
                    {
                        var mailService = new TMK_MailService();
                        mailService.SetData(mailDataPersonalizado);
                        mailService.SendMessageTask();
                    }
                    catch { }
                }

                return (true, pEspecifico.Id);
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Generar Centro Costo Codigo Nombre
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns></returns>
        public CentroCostoGeneradoDTO GenerarCentroCostoCodigoNombre(PEspecificoGeneracionAutomaticaDTO dto)
        {
            try
            {
                string codigoCentroCosto = string.Empty;
                string nombrePEspecificoNuevo;
                var nombreCiudad = dto.NombreCiudad;
                var pGeneral = _unitOfWork.PEspecificoRepository.ObtenerDatosPGeneralParaPEspecifico(dto.IdProgramaGeneral);
                if (pGeneral.IdCategoria == 0)
                    throw new BadRequestException("#PES-GCCCN-001@Programa General no tiene Categoria");

                if (!dto.IdCiudad.HasValue)
                {
                    throw new BadRequestException("#PES-GCCCN-002@IdCiudad no valido");
                }
                var ciudadCategoria = _unitOfWork.PEspecificoRepository.ObtenerCiudadCategoria(dto.IdCiudad.Value, pGeneral.IdCategoria);
                if (ciudadCategoria == null || ciudadCategoria.Id == 0)
                    throw new Exception($"#PES-GCCCN-002@No existe Troncal para esta categoria en esta ciudad: {dto.IdCiudad}");

                string codigo = ciudadCategoria.TroncalCompleto;
                var ccTemp = _unitOfWork.CentroCostoRepository.GetBy(w => w.Codigo.Contains(codigo));
                var ultimoCentroCosto = ccTemp.OrderByDescending(x => Convert.ToInt64(x.Codigo)).FirstOrDefault();
                if (ultimoCentroCosto == null)
                {
                    codigoCentroCosto = codigo + "001";
                }
                else
                {
                    string codigoPrimeraParte = ultimoCentroCosto.Codigo.Substring(0, 6);
                    string codigoUltimosDigitos = ultimoCentroCosto.Codigo.Substring(6);
                    string sumado = (long.Parse(codigoUltimosDigitos) + 1).ToString();
                    if (codigoUltimosDigitos.Substring(0, 1).Equals("0"))
                        sumado = "0" + sumado;
                    if (codigoUltimosDigitos.Substring(0, 2).Equals("00"))
                        sumado = "0" + sumado;

                    codigoCentroCosto = codigoPrimeraParte + sumado;
                }

                if (nombreCiudad.ToUpper() == "AREQUIPA")
                    nombreCiudad = "AQP";

                var modalidad = dto.Modalidad.ToUpper();
                var condicion = string.Empty;

                if (modalidad == "PRESENCIAL")
                {
                    condicion = " (Nombre NOT LIKE '%ONLINE%') ";
                    modalidad = string.Empty;
                }
                if (modalidad == "ONLINE ASINCRONICA")
                {
                    condicion = " (Nombre LIKE '%AONLINE%') ";
                    modalidad = "AONLINE";
                }
                if (modalidad == "ONLINE SINCRONICA")
                {
                    condicion = " (Nombre LIKE '%ONLINE%' AND Nombre NOT LIKE '%AONLINE%') ";
                    modalidad = "ONLINE";
                }
                var listaCentroCosto = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoParaPEspecifico(pGeneral.Codigo, condicion, dto.Anio.ToString(), nombreCiudad);
                var filtradoPorCodigo = listaCentroCosto.Where(s => s.IdPgeneral.Equals(pGeneral.Codigo));

                string roman = ToRoman(filtradoPorCodigo.Count() + 1);
                modalidad = string.IsNullOrEmpty(modalidad) ? string.Empty : $"{modalidad} ";

                nombrePEspecificoNuevo = $"{pGeneral.Nombre} {modalidad}{dto.Anio} {roman} {nombreCiudad}";
                CentroCostoGeneradoDTO nuevoCentroCosto = new();
                nuevoCentroCosto.CentroCosto = new CentroCostoDTO();
                nuevoCentroCosto.CentroCosto.IdArea = pGeneral.IdArea;
                nuevoCentroCosto.CentroCosto.IdSubArea = pGeneral.IdSubArea;
                nuevoCentroCosto.CentroCosto.IdPgeneral = pGeneral.Codigo;
                nuevoCentroCosto.CentroCosto.Nombre = $"{pGeneral.Codigo} {modalidad}{dto.Anio} {roman} {nombreCiudad}";
                nuevoCentroCosto.CentroCosto.Codigo = codigoCentroCosto;
                nuevoCentroCosto.Codigo = codigoCentroCosto;
                nuevoCentroCosto.CentroCosto.IdAreaCc = "9-3";
                nuevoCentroCosto.NombreProgramaEspecifico = nombrePEspecificoNuevo;
                nuevoCentroCosto.CodigoBanco = "A" + (_unitOfWork.CentroCostoRepository.ObtenerUltimoIdCentroCosto() + 1);
                nuevoCentroCosto.NombreProgramaGeneral = pGeneral.Nombre;

                return nuevoCentroCosto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string ToRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("Valores entre 1 y 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Generar Centro Costo Codigo Nombre
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns></returns>
        public PEspecificoDTO ActualizarPespecifico(PEspecificoDTO dto, string usuario)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                {
                    throw new BadRequestException("Id Pespecifico no valido");
                }
                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.Id);
                if (pEspecifico == null || pEspecifico.Id == 0)
                {
                    throw new BadRequestException("No se encontro la entidad");
                }
                if (dto.EstadoPid == 0)
                {
                    var furs = _unitOfWork.FurRepository.ObtenerFurProgramaEspecifico(dto.Id, false);
                    if (furs.Count() > 0)
                    {
                        _unitOfWork.PEspecificoRepository.Delete(furs.Select(x => x.Id), usuario);
                    }
                }

                pEspecifico.Nombre = dto.Nombre;
                pEspecifico.Codigo = dto.Codigo;
                pEspecifico.IdCentroCosto = dto.IdCentroCosto;
                pEspecifico.EstadoP = dto.EstadoP;
                pEspecifico.IdEstadoPespecifico = dto.EstadoPid;
                pEspecifico.Tipo = dto.Tipo;
                pEspecifico.TipoAmbiente = dto.TipoAmbiente;
                pEspecifico.IdProgramaGeneral = dto.IdProgramaGeneral;
                pEspecifico.Ciudad = dto.Ciudad;
                pEspecifico.CodigoBanco = dto.CodigoBanco;
                pEspecifico.EstadoPid = dto.EstadoPid;
                pEspecifico.TipoId = dto.TipoId;
                pEspecifico.OrigenPrograma = dto.OrigenPrograma;
                pEspecifico.IdCiudad = dto.IdCiudad;
                pEspecifico.Duracion = dto.Duracion;
                pEspecifico.ActualizacionAutomatica = dto.ActualizacionAutomatica;
                pEspecifico.IdCursoMoodle = dto.IdCursoMoodle;
                pEspecifico.IdCursoMoodlePrueba = dto.IdCursoMoodlePrueba;
                pEspecifico.CursoIndividual = dto.CursoIndividual;
                pEspecifico.UrlDocumentoCronograma = dto.UrlDocumentoCronograma;
                pEspecifico.IdTipoProgramaCarrera = dto.IdTipoProgramaCarrera;
                pEspecifico.UsuarioModificacion = usuario;
                pEspecifico.FechaModificacion = DateTime.Now;
                pEspecifico.ResumenClaseActivo = dto.ResumenClaseActivo;
                pEspecifico.TutorVirtualActivo = dto.TutorVirtualActivo;
                var resultado = _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                var listaCursosHijos = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerInformacionPespecificosHijos(dto.Id);
                foreach(var EspecificoHijo in listaCursosHijos)
                {
                    var pEspecificoHijo = _unitOfWork.PEspecificoRepository.ObtenerPorId(EspecificoHijo.Id);
                    pEspecificoHijo.ResumenClaseActivo = dto.ResumenClaseActivo;
                    pEspecificoHijo.TutorVirtualActivo = dto.TutorVirtualActivo; 
                    _unitOfWork.PEspecificoRepository.Update(pEspecificoHijo);
                    if (dto.ResumenClaseActivo == true)
                    {
                        _unitOfWork.PEspecificoRepository.ActualizarConfiguracionPEspecificoAlumnoResumen(EspecificoHijo.Id, usuario);
                    }
                }
                _unitOfWork.Commit();
                return _mapper.Map<PEspecificoDTO>(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetAnio(string centroCostoNombre)
        {
            MatchCollection numeros = Regex.Matches(centroCostoNombre, @"\d+");

            // Itera sobre las coincidencias y obtiene los números
            string anio = "";
            foreach (Match match in numeros)
            {
                anio += match.Value;
            }
            return int.Parse(anio);
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Generar Centro Costo Codigo Nombre
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns></returns>
        public RegistroProgramaEspecificoDTO InsertarCrearCursosConCentroCosto(FiltroInsertarPEspecificoDTO dto, string usuario)
        {
            try
            {
                CentroCosto centroCostoPadre = new();
                if (dto.CentroCosto != null)
                {
                    centroCostoPadre.IdArea = dto.CentroCosto.IdArea;
                    centroCostoPadre.IdSubArea = dto.CentroCosto.IdSubArea;
                    centroCostoPadre.IdPgeneral = dto.CentroCosto.IdPgeneral;
                    centroCostoPadre.Nombre = dto.CentroCosto.Nombre;
                    centroCostoPadre.Codigo = dto.CentroCosto.Codigo;
                    centroCostoPadre.IdAreaCc = dto.CentroCosto.IdAreaCc;
                    centroCostoPadre.Ismtotales = dto.CentroCosto.Ismtotales;
                    centroCostoPadre.Icpftotales = dto.CentroCosto.Icpftotales;
                    centroCostoPadre.Estado = true;
                    centroCostoPadre.UsuarioCreacion = usuario;
                    centroCostoPadre.UsuarioModificacion = usuario;
                    centroCostoPadre.FechaCreacion = DateTime.Now;
                    centroCostoPadre.FechaModificacion = DateTime.Now;
                }
                if (_unitOfWork.PEspecificoRepository.Exist(w => w.CodigoBanco == dto.Pespecifico.CodigoBanco))
                {
                    throw new BadRequestException("Ya existe otro PEspecifico con este codigo Banco!");
                }
                PEspecifico pEspecificoPadre = new()
                {
                    Nombre = dto.Pespecifico.Nombre,
                    Codigo = dto.Pespecifico.Codigo,
                    IdCentroCosto = dto.Pespecifico.IdCentroCosto,
                    EstadoP = dto.Pespecifico.EstadoP,
                    Tipo = dto.Pespecifico.Tipo,
                    TipoAmbiente = dto.Pespecifico.TipoAmbiente,
                    IdProgramaGeneral = dto.Pespecifico.IdProgramaGeneral,
                    Ciudad = dto.Pespecifico.Ciudad,
                    Categoria = dto.Pespecifico.Categoria,
                    CodigoBanco = dto.Pespecifico.CodigoBanco,
                    EstadoPid = dto.Pespecifico.EstadoPid,
                    TipoId = dto.Pespecifico.TipoId,
                    OrigenPrograma = dto.Pespecifico.OrigenPrograma,
                    IdCiudad = dto.Pespecifico.IdCiudad,
                    Duracion = dto.Pespecifico.Duracion,
                    ActualizacionAutomatica = dto.Pespecifico.ActualizacionAutomatica,
                    IdCursoMoodle = dto.Pespecifico.IdCursoMoodle,
                    IdCursoMoodlePrueba = dto.Pespecifico.IdCursoMoodlePrueba,
                    CursoIndividual = dto.Pespecifico.CursoIndividual,
                    IdExpositorReferencia = dto.Pespecifico.IdExpositorReferencia,
                    IdAmbiente = dto.Pespecifico.IdAmbiente,
                    IdEstadoPespecifico = dto.Pespecifico.EstadoPid,
                    UrlDocumentoCronograma = dto.Pespecifico.UrlDocumentoCronograma,
                    IdTipoProgramaCarrera = dto.Pespecifico.IdTipoProgramaCarrera,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    ResumenClaseActivo = dto.Pespecifico.ResumenClaseActivo
                };
                pEspecificoPadre.CursoPespecificos = new List<CursoPespecifico>()
                {
                    new CursoPespecifico
                    {
                        IdPespecifico = pEspecificoPadre.Id,
                        Nombre = pEspecificoPadre.Nombre,
                        Duracion = 1,
                        Orden = 1,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        Estado = true
                    }
                };

                if (dto.CentroCosto != null)
                {
                    centroCostoPadre.Pespecificos = new List<PEspecifico>()
                    {
                        pEspecificoPadre
                    };
                }
                int idCentroCostoTemporal;

                using (TransactionScope scope = new TransactionScope())
                {
                    if (dto.CentroCosto != null)
                    {
                        var resultado = _unitOfWork.CentroCostoRepository.Add(centroCostoPadre);
                        _unitOfWork.Commit();
                        centroCostoPadre.Id = resultado.Id;
                        var pEspecificoTemp = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(centroCostoPadre.Id);
                        if (pEspecificoTemp != null)
                        {
                            centroCostoPadre.Pespecificos.ElementAt(0).Id = pEspecificoTemp.Id;
                            pEspecificoPadre.Id = pEspecificoTemp.Id;
                        }
                        else
                        {
                            throw new BadRequestException("No existe pespecifico");
                        }
                    }
                    else
                    {
                        _unitOfWork.PEspecificoRepository.Add(pEspecificoPadre);
                        _unitOfWork.Commit();
                    }

                    if (dto.CentroCosto == null)
                        idCentroCostoTemporal = dto.Pespecifico.IdCentroCosto!.Value;
                    else
                        idCentroCostoTemporal = centroCostoPadre.Id;

                    var listaPGenerales = _unitOfWork.PGeneralRepository.ObtenerPgeneralCursos(dto.Pespecifico.IdProgramaGeneral!.Value);
                    int contador = 0;
                    foreach (var item in listaPGenerales)
                    {
                        contador++;
                        string nombrePEspecifico;
                        var pGeneral = _unitOfWork.PGeneralRepository.ObtenerPGeneralParaPEspecifico(item.IdPGeneral_Hijo);
                        if (pGeneral == null)
                        {
                            throw new BadRequestException($"No existe programa general {item.IdPGeneral_Hijo}");
                        }
                        string codigo = ObtenerCodigoPEspecifico(pGeneral, dto.IdCiudad);

                        var modalidad = _unitOfWork.ModalidadCursoRepository.ObtenerPorId(dto.Pespecifico.TipoId!.Value);
                        string codigoModalidad = string.Empty;
                        if (modalidad != null && modalidad.Id != 0)
                        {
                            codigoModalidad = modalidad.Codigo;
                        }


                        var centroCostoFinal = _unitOfWork.CentroCostoRepository.ObtenerPorId(idCentroCostoTemporal);
                        int anio = 0, cont = 4;

                        //anio = GetAnio(centroCostoFinal.Nombre);

                        while (anio < 2000)
                        {
                            var longitud = ($"{centroCostoFinal.IdPgeneral} {codigoModalidad} ").Trim().Length;
                            var anio2 = centroCostoFinal.Nombre.Substring(longitud, cont);
                            anio = int.Parse(anio2);
                            cont++;
                        }

                        CentroCostoDatosDTO centroCosto2 = ObtenerCentroCostoPEspecifico(pGeneral, codigo, dto.Pespecifico.Ciudad, anio, codigoModalidad, out nombrePEspecifico);

                        CentroCosto nuevoCentroCosto = new()
                        {
                            IdArea = centroCosto2.IdArea,
                            IdSubArea = centroCosto2.IdSubArea,
                            IdPgeneral = centroCosto2.IdPgeneral,
                            Nombre = centroCosto2.Nombre,
                            IdAreaCc = centroCosto2.IdAreaCc,
                            Codigo = centroCosto2.Codigo,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        var resultado = _unitOfWork.CentroCostoRepository.Add(nuevoCentroCosto);
                        _unitOfWork.Commit();
                        nuevoCentroCosto.Id = resultado.Id;
                        PEspecifico programaEspecifico = new()
                        {
                            Nombre = nombrePEspecifico,
                            Codigo = codigo,
                            IdCentroCosto = nuevoCentroCosto.Id,
                            EstadoP = dto.Pespecifico.EstadoP,
                            Tipo = dto.Pespecifico.Tipo,
                            TipoAmbiente = "1",
                            Categoria = dto.Pespecifico.Categoria,
                            IdProgramaGeneral = item.IdPGeneral_Hijo,
                            Ciudad = dto.Pespecifico.Ciudad,
                            CodigoBanco = "A" + nuevoCentroCosto.Id.ToString(),
                            EstadoPid = dto.Pespecifico.EstadoPid,
                            IdEstadoPespecifico = dto.Pespecifico.EstadoPid,
                            TipoId = dto.Pespecifico.TipoId,
                            OrigenPrograma = dto.Pespecifico.OrigenPrograma,
                            IdCiudad = dto.Pespecifico.IdCiudad,
                            Duracion = "0",
                            ActualizacionAutomatica = "0",
                            CursoIndividual = dto.Pespecifico.CursoIndividual,
                            IdAmbiente = dto.Pespecifico.IdAmbiente,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };

                        if (!_unitOfWork.PEspecificoRepository.Exist(w => w.CodigoBanco == programaEspecifico.CodigoBanco))
                        {
                            /* Se crean cursos en la tabla antigua solo para que permita a los encargados de finanzas    *
                             * matricular a los alumnos, cuando se reemplaze el modulo de "Procesos de Matricula" borrar *
                             * esta parte del codigo y el parametro orden  */
                            var resultadoPEspecifico = _unitOfWork.PEspecificoRepository.Add(programaEspecifico);
                            _unitOfWork.Commit();
                            programaEspecifico = _mapper.Map<PEspecifico>(resultadoPEspecifico);
                            CursoPespecifico CursoPespecifico = new()
                            {
                                IdPespecifico = programaEspecifico.Id,
                                Nombre = programaEspecifico.Nombre,
                                Duracion = 1,
                                Orden = contador,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                Estado = true
                            };
                            _unitOfWork.CursoPespecificoRepository.Add(CursoPespecifico);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            throw new Exception("Ya existe otro PEspecifico con este codigo Banco!");
                        }

                        PespecificoParticipacionExpositor pEspecificoParticipacionExpositor = new()
                        {
                            IdPespecifico = programaEspecifico.Id,
                            Orden = contador,
                            Grupo = 1,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        _unitOfWork.PespecificoParticipacionExpositorRepository.Add(pEspecificoParticipacionExpositor);
                        _unitOfWork.Commit();
                        var pEspecificoPadre2 = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(centroCostoPadre.Id);
                        PespecificoPadrePespecificoHijo programaEspecificoPadreHijo = new()
                        {
                            PespecificoPadreId = pEspecificoPadre2.Id,
                            PespecificoHijoId = programaEspecifico.Id,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            Estado = true
                        };
                        _unitOfWork.PespecificoPadrePespecificoHijoRepository.Add(programaEspecificoPadreHijo);
                        _unitOfWork.Commit();

                    }
                    scope.Complete();
                }
                RegistroProgramaEspecificoDTO resultadoProgramaEspecifico = new();
                resultadoProgramaEspecifico.Id = pEspecificoPadre.Id;
                resultadoProgramaEspecifico.Nombre = dto.Pespecifico.Nombre;
                resultadoProgramaEspecifico.Codigo = dto.Pespecifico.Codigo!;
                resultadoProgramaEspecifico.IdCentroCosto = idCentroCostoTemporal;
                resultadoProgramaEspecifico.EstadoP = dto.Pespecifico.EstadoP;
                resultadoProgramaEspecifico.IdProgramageneral = dto.Pespecifico.IdProgramaGeneral;
                resultadoProgramaEspecifico.Ciudad = dto.Pespecifico.Ciudad;
                resultadoProgramaEspecifico.CursoIndividual = dto.Pespecifico.CursoIndividual;
                if (dto.Pespecifico.CursoIndividual == true)
                {
                    PespecificoParticipacionExpositor pEspecificoParticipacionExpositor = new();
                    pEspecificoParticipacionExpositor.IdPespecifico = resultadoProgramaEspecifico.Id;
                    pEspecificoParticipacionExpositor.Orden = 1;
                    pEspecificoParticipacionExpositor.Grupo = 1;
                    pEspecificoParticipacionExpositor.Estado = true;
                    pEspecificoParticipacionExpositor.FechaCreacion = DateTime.Now;
                    pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                    pEspecificoParticipacionExpositor.UsuarioCreacion = usuario;
                    pEspecificoParticipacionExpositor.UsuarioModificacion = usuario;

                    _unitOfWork.PespecificoParticipacionExpositorRepository.Add(pEspecificoParticipacionExpositor);

                    // Agregar curso adicional 
                    if (dto.idPespecificoAdicional != null)
                    {
                        PespecificoCursoAdicional pEspecificoCursoAdicional = new();
                        pEspecificoCursoAdicional.IdPespecifico = resultadoProgramaEspecifico.Id;
                        pEspecificoCursoAdicional.IdPespecificoAdicional = dto.idPespecificoAdicional.Value;
                        pEspecificoCursoAdicional.Estado = true;
                        pEspecificoCursoAdicional.FechaCreacion = DateTime.Now;
                        pEspecificoCursoAdicional.FechaModificacion = DateTime.Now;
                        pEspecificoCursoAdicional.UsuarioCreacion = usuario;
                        pEspecificoCursoAdicional.UsuarioModificacion = usuario;
                        _unitOfWork.PespecificoCursoAdicionalRepository.Add(pEspecificoCursoAdicional);
                    }
                    _unitOfWork.Commit();
                }
                return resultadoProgramaEspecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Generar Centro Costo Codigo Nombre
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns></returns>
        private string ObtenerCodigoPEspecifico(PGeneralDatosDTO pGeneral, int idCiudad)
        {
            try
            {
                if (pGeneral.IdCategoria == null || pGeneral.IdCategoria == 0)
                    throw new Exception("Programa General no tiene Categoria");

                var codigoTroncal = _unitOfWork.CategoriaCiudadRepository.ObtenerTroncalPorIdCiudadIdCategoria(idCiudad, pGeneral.IdCategoria.Value);

                if (codigoTroncal == null)
                    throw new Exception("No existe Troncal para esta categoria en esta ciudad: " + idCiudad);

                var ultimoCodigoCentroCosto = _unitOfWork.CentroCostoRepository.GetBy(w => w.Codigo.Contains(codigoTroncal) && w.Estado == true, w => new { w.Codigo }).OrderByDescending(w => Convert.ToInt64(w.Codigo)).FirstOrDefault();

                if (ultimoCodigoCentroCosto == null)
                {
                    return codigoTroncal + "001";
                }
                else
                {
                    string codigoPrimeraParte = ultimoCodigoCentroCosto.Codigo.Substring(0, 6);
                    string codigoUltimosDigitos = ultimoCodigoCentroCosto.Codigo.Substring(6);
                    string sumado = (long.Parse(codigoUltimosDigitos) + 1).ToString();
                    if (codigoUltimosDigitos.Substring(0, 1).Equals("0"))
                        sumado = "0" + sumado;
                    if (codigoUltimosDigitos.Substring(0, 2).Equals("00"))
                        sumado = "0" + sumado;

                    return $"{codigoPrimeraParte}{sumado}";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Generar Centro Costo Codigo Nombre
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns></returns>
        public CentroCostoDatosDTO ObtenerCentroCostoPEspecifico(PGeneralDatosDTO pGeneral, string codigo, string nombreCiudad, int anio, string modalidad, out string nombrePEspecifico)
        {
            var condicion = string.Empty;
            if (nombreCiudad.ToUpper() == "AREQUIPA")
                nombreCiudad = "AQP";

            if (string.IsNullOrEmpty(modalidad))
                condicion = " (Nombre NOT LIKE '%ONLINE%') ";

            if (modalidad == "AONLINE")
                condicion = " (Nombre LIKE '%AONLINE%') ";

            if (modalidad == "ONLINE")
                condicion = " (Nombre LIKE '%ONLINE%' AND Nombre NOT LIKE '%AONLINE%') ";

            var listaCentroCosto = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoParaPEspecifico(pGeneral.Codigo, condicion, anio.ToString(), nombreCiudad);
            var filtroPorCodigo = listaCentroCosto.Where(s => s.IdPgeneral.Equals(pGeneral.Codigo)).ToList();

            string roman = ToRoman(filtroPorCodigo.Count() + 1);

            modalidad = string.IsNullOrEmpty(modalidad) ? string.Empty : $"{modalidad} ";
            nombrePEspecifico = $"{pGeneral.Nombre} {modalidad}{anio} {roman} {nombreCiudad}";
            CentroCostoDatosDTO centroCostoDatos = new CentroCostoDatosDTO();

            centroCostoDatos.IdArea = pGeneral.IdArea;
            centroCostoDatos.IdSubArea = pGeneral.IdSubArea;
            centroCostoDatos.IdPgeneral = pGeneral.Codigo;
            centroCostoDatos.Nombre = $"{pGeneral.Codigo} {modalidad}{anio} {roman} {nombreCiudad}";
            centroCostoDatos.Codigo = codigo;
            centroCostoDatos.IdAreaCc = "9-3";
            return centroCostoDatos;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Generar Centro Costo Codigo Nombre
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns></returns>
        public IEnumerable<InformacionPespecificoHijoDTO> ObtenerTodoPespecificosRelacionados(int idPespecifico)
        {
            try
            {
                var programasEspecificosHijo = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerPespecificosRelacionados(idPespecifico);
                var listaGrupos = _unitOfWork.PEspecificoRepository.ObtenerGruposSesiones(idPespecifico);
                foreach (var item in programasEspecificosHijo)
                {
                    item.Grupos = listaGrupos;
                    item.GruposEdicion = _unitOfWork.PEspecificoRepository.ObtenerGrupoEdicionDisponible(item.Id);
                }
                return programasEspecificosHijo;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/06/2023
        /// Version: 2.0
        /// <summary>
        /// Actualiza o inserta un modulo pespecifico
        /// </summary>
        /// <returns>Lista de objetos de tipo PEspecificoComboDTO</returns>
        public bool ActualizarInsertarModuloWebinar(InsertarActualizarModuloWebinaDTO dto, string usuario)
        {
            try
            {
                if (dto.Id == 0)
                {
                    PespecificoPadrePespecificoHijo pEspecificoPadrePespecificoHijo = new()
                    {
                        PespecificoPadreId = dto.IdPespecificoPadre,
                        PespecificoHijoId = dto.IdPespecifico,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario
                    };
                    _unitOfWork.PespecificoPadrePespecificoHijoRepository.Add(pEspecificoPadrePespecificoHijo);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    var pEspecificoPadrePespecificoHijo = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerPorIdPadreIdHijo(dto.IdPespecificoPadre, dto.Id);
                    if (pEspecificoPadrePespecificoHijo != null && pEspecificoPadrePespecificoHijo.Id != 0)
                    {
                        pEspecificoPadrePespecificoHijo.PespecificoHijoId = dto.IdPespecifico;
                        pEspecificoPadrePespecificoHijo.FechaModificacion = DateTime.Now;
                        pEspecificoPadrePespecificoHijo.UsuarioModificacion = usuario;
                        _unitOfWork.PespecificoPadrePespecificoHijoRepository.Update(pEspecificoPadrePespecificoHijo);
                        _unitOfWork.Commit();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Configuración Webinar
        /// </summary>
        /// <param name="idPespecifico"> Id Programa Especifico</param>
        /// <returns> Lista DTO- List<ConfigurarWebinarDTO> - json </returns>
        public bool ActualizarConfigurarWebinar(List<ConfigurarWebinarDTO> dto, string usuario)
        {
            try
            {
                //var configurarwebinars = _unitOfWork.ConfigurarWebinarRepository.ObtenerPorIds(dto.Select(x => x.Id));
                foreach (var item in dto)
                {
                    ConfigurarWebinar? configurarwebinar = _unitOfWork.ConfigurarWebinarRepository.ObtenerPorId(item.Id);
                    if (configurarwebinar != null && configurarwebinar.Id != 0)
                    {
                        configurarwebinar.IdPespecifico = item.IdPespecifico;
                        configurarwebinar.IdOperadorComparacionAvance = item.IdOperadorComparacionAvance;
                        configurarwebinar.ValorAvance = item.ValorAvance;
                        configurarwebinar.ValorAvanceOpc = item.ValorAvanceOpc;
                        configurarwebinar.IdOperadorComparacionPromedio = item.IdOperadorComparacionPromedio;
                        configurarwebinar.ValorPromedio = item.ValorPromedio;
                        configurarwebinar.ValorPromedioOpc = item.ValorPromedioOpc;
                        configurarwebinar.UsuarioModificacion = usuario;
                        configurarwebinar.FechaModificacion = DateTime.Now;
                        _unitOfWork.ConfigurarWebinarRepository.Update(configurarwebinar);
                        _unitOfWork.Commit();
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Configuración Webinar
        /// </summary>
        /// <param name="json"></param>
        /// <param name="usuario"></param>
        /// <returns> Lista DTO- List<ConfigurarWebinarDTO> - json </returns>
        public bool EliminarConfiguracionWebinar(List<int> ids, string usuario)
        {
            try
            {
                var configurarwebinars = _unitOfWork.ConfigurarWebinarRepository.ObtenerPorIds(ids);
                if (configurarwebinars != null && configurarwebinars.Count() > 0)
                {
                    _unitOfWork.ConfigurarWebinarRepository.Delete(configurarwebinars.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Configuración Webinar
        /// </summary>
        /// <param name="json"></param>
        /// <param name="usuario"></param>
        /// <returns> Lista DTO- List<ConfigurarWebinarDTO> - json </returns>
        public (bool Estado, string Nombre) VerificarSiTienePadrePEspecifico(int idPespecifico)
        {
            var resultado = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerPespecificoPadrePorId(idPespecifico);
            var estado = false;
            string nombre = string.Empty;
            if ((resultado != null && resultado.Id != 0))
            {
                estado = true;
                var padre = _unitOfWork.PEspecificoRepository.ObtenerPorId(resultado.PespecificoPadreId);
                nombre = padre.Nombre;
            }
            return (estado, nombre);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza Configuración Webinar
        /// </summary>
        /// <param name="json"></param>
        /// <param name="usuario"></param>
        /// <returns> Lista DTO- List<ConfigurarWebinarDTO> - json </returns>
        public bool VerificarEsPespecificoIndividual(int idPespecifico)
        {
            var rpta = _unitOfWork.PEspecificoRepository.Exist(w => w.Estado == true && w.CursoIndividual == true && w.Id == idPespecifico);
            return rpta;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma por id pespecifico
        /// </summary>
        /// <param name="json"></param>
        /// <param name="usuario"></param>
        /// <returns> Lista DTO- List<ConfigurarWebinarDTO> - json </returns>
        public string ObtenerCronogramaParaModulo(int idPespecifico, string usuario)
        {
            try
            {
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerProgramaEspecificoPorCodigo(idPespecifico);
                string url = string.Empty;
                if (programaEspecifico != null)
                {
                    var sesiones = _unitOfWork.PEspecificoSesionRepository.ObtenerCronogramaIndividualPorPEspecifico(programaEspecifico);
                    if (sesiones.Count() > 0)
                    {
                        url = GenerarPDFCronograma(programaEspecifico.Id, programaEspecifico.Nombre, usuario, sesiones);
                    }
                }
                return url;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el número de grupos de sesión 
        /// </summary>
        /// <param name="pEspecificoId"></param>
        /// <param name="cursoIndividual"></param>
        /// <returns> Lista DTO - IEnumerable<ComboDTO> - grupos </returns>
        public IEnumerable<ComboDTO> ObtenerNumeroGrupos(int pEspecificoId, bool cursoIndividual)
        {
            try
            {
                if (cursoIndividual)
                {
                    if (pEspecificoId > 0)
                    {
                        var gruposIndividuales = _unitOfWork.PEspecificoRepository.ObtenerGruposSesionesIndividuales(pEspecificoId);
                        return gruposIndividuales;
                    }
                }
                var grupos = _unitOfWork.PEspecificoRepository.ObtenerGruposSesiones(pEspecificoId).ToList();

                if (grupos.Count() == 0)
                {
                    grupos.Add(new ComboDTO
                    {
                        Id = 1,
                        Nombre = "Grupo 1"
                    });
                }
                return grupos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta Configuraciones Webinar
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public ConfigurarWebinarDTO InsertarConfiguracionWebinar(ConfigurarWebinarDTO dto, string usuario)
        {
            try
            {
                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.IdPespecifico);
                if (pEspecifico == null || pEspecifico.Id == 0)
                {
                    throw new BadRequestException($"No existe pespecifico {dto.IdPespecifico}");
                }
                ConfigurarWebinar configurarwebinar = new()
                {
                    IdPespecifico = dto.IdPespecifico,
                    Modalidad = pEspecifico.Tipo,
                    Codigo = pEspecifico.Codigo ?? string.Empty,
                    IdOperadorComparacionAvance = dto.IdOperadorComparacionAvance,
                    ValorAvance = dto.ValorAvance,
                    ValorAvanceOpc = dto.ValorAvanceOpc,
                    IdOperadorComparacionPromedio = dto.IdOperadorComparacionPromedio,
                    ValorPromedio = dto.ValorPromedio,
                    ValorPromedioOpc = dto.ValorPromedioOpc,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    IdPespecificoPadre = dto.IdPespecificoPadre,
                };
                var resultado = _unitOfWork.ConfigurarWebinarRepository.Add(configurarwebinar);
                _unitOfWork.Commit();
                dto.Id = resultado.Id;
                return dto;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Cronograma PEspecifico
        /// </summary>
        /// <param name="dto"></param>
        /// <returns> Lista DTO - IEnumerable<CronogramaGrupoDTO> </returns>
        public IEnumerable<CronogramaGrupoDTO> ObtenerCronogramaPEspecifico(FiltroObtenerSesionesDTO dto)
        {
            try
            {
                if (dto.NroGrupo == null)
                    dto.NroGrupo = 1;
                if (dto.CursoIndividual)
                {
                    if (dto.PEspecificoId > 0)
                    {
                        return _unitOfWork.PEspecificoRepository.ObtenerCronogramaPEspecificoGrupoSesionIndividual(dto.PEspecificoId, dto.NroGrupo.Value);
                    }
                }
                return _unitOfWork.PEspecificoRepository.ObtenerCronogramaPEspecificoGrupo(dto.PEspecificoId, dto.ListaPEspecificos, dto.NroGrupo.Value);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Verifica si tiene duracion
        /// </summary>
        /// <param name="idPespecificoPadre">Id Pespecifico</param>
        /// <returns> Lista DTO - IEnumerable<CronogramaGrupoDTO> </returns>
        public bool VerificarDuracionPorIdPespecificoPadre(int idPespecificoPadre)
        {
            try
            {
                var duracionPespecifico = _unitOfWork.PEspecificoRepository.ObtenerDatosDuracionPorIdPespecificoPadre(idPespecificoPadre);
                if (duracionPespecifico == null || duracionPespecifico.Count() == 0)
                    return true;

                return !duracionPespecifico.Any(x => x.Duracion == "0");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta frecuencia
        /// </summary>
        /// <param name="dto">ParametrosInsertaFrecuenciaDTO</param>
        /// <param name="usuario">Usuario</param>
        /// <returns> Lista DTO - IEnumerable<CronogramaGrupoDTO> </returns>
        public bool InsertarFrecuencia(ParametrosInsertaFrecuenciaDTO dto, string usuario)
        {
            try
            {
                PespecificoFrecuencia? frecuencia = new PespecificoFrecuencia();
                PEspecifico? pEspecifico = new PEspecifico();
                List<PespecificoFrecuenciaDetalle> frecuenciasDetalle = new List<PespecificoFrecuenciaDetalle>();

                using (TransactionScope scope = new TransactionScope())
                {
                    pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.IdPespecifico);
                    if (pEspecifico == null)
                    {
                        throw new BadRequestException($"No existe pespecifico {dto.IdPespecifico}");
                    }
                    if (_unitOfWork.PespecificoFrecuenciaRepository.Exist(w => w.IdPespecifico == dto.IdPespecifico))
                    {
                        frecuencia = _unitOfWork.PespecificoFrecuenciaRepository.ObtenerPorIdPespecifico(dto.IdPespecifico)!;
                        pEspecifico.IdSesionInicio = null;
                        frecuencia.FechaInicio = dto.FechaInicio;
                        frecuencia.FechaFin = dto.FechaFin;
                        frecuencia.Frecuencia = -1;
                        frecuencia.NroSesiones = dto.listaDetalles.Count;
                        frecuencia.IdFrecuencia = dto.IdFrecuencia;
                        frecuencia.FechaModificacion = DateTime.Now;
                        frecuencia.UsuarioModificacion = usuario;
                        _unitOfWork.PespecificoFrecuenciaRepository.Update(frecuencia);
                        _unitOfWork.Commit();

                        var listaDetalle = _unitOfWork.PespecificoFrecuenciaDetalleRepository.ObtenerPorIdPespecificoFrecuencia(frecuencia.Id);
                        ////eliminamos los detalles de la frecuencia
                        _unitOfWork.PespecificoFrecuenciaDetalleRepository.Delete(listaDetalle.Select(x => x.Id.Value), usuario);
                        _unitOfWork.Commit();

                        //eliminamos las sesiones de todos los pespecificos hijos(su estado cambia a 0)
                        var detalleCursos = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerDetallePEspecificoHijosPorIdPespecificoPadre(dto.IdPespecifico);

                        //tiene cursos asociados
                        List<int> idSesiones = new List<int>();
                        if (detalleCursos != null && detalleCursos.Count() != 0)
                        {
                            foreach (var hijo in detalleCursos)
                            {
                                var lista_sesiones = _unitOfWork.PEspecificoSesionRepository.ListaPespecificoSesiones(hijo.PEspecificoHijoId);
                                idSesiones.AddRange(lista_sesiones);
                            }
                        }
                        //es individual
                        else
                        {
                            var lista_sesiones_ind = _unitOfWork.PEspecificoSesionRepository.ListaPespecificoSesiones(dto.IdPespecifico);
                            idSesiones.AddRange(lista_sesiones_ind);
                        }
                        _unitOfWork.PEspecificoSesionRepository.Delete(idSesiones, usuario);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        frecuencia = new PespecificoFrecuencia()
                        {
                            IdPespecifico = dto.IdPespecifico,
                            FechaInicio = dto.FechaInicio,
                            FechaFin = dto.FechaFin,
                            Frecuencia = -1,
                            NroSesiones = dto.listaDetalles.Count,
                            IdFrecuencia = dto.IdFrecuencia,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        var resultadoFrecuencia = _unitOfWork.PespecificoFrecuenciaRepository.Add(frecuencia);
                        _unitOfWork.Commit();
                        frecuencia.Id = resultadoFrecuencia.Id;
                    }
                    //se insertan nuevos detalles
                    foreach (var detalle in dto.listaDetalles)
                    {
                        PespecificoFrecuenciaDetalle frecuenciaDetalleTemp = new()
                        {
                            IdPespecificoFrecuencia = frecuencia.Id,
                            DiaSemana = detalle.DiaSemana,
                            HoraDia = detalle.HoraDia,
                            Duracion = detalle.Duracion,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario
                        };
                        _unitOfWork.PespecificoFrecuenciaDetalleRepository.Add(frecuenciaDetalleTemp);
                        _unitOfWork.Commit();
                    }

                    var listaCursos = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerInformacionPespecificosHijos(dto.IdPespecifico);
                    var frecuenciaPespecifico = _unitOfWork.PespecificoFrecuenciaRepository.ObtenerPespecificoFrecuenciaPorIdPespecifico(dto.IdPespecifico).FirstOrDefault();
                    if (frecuenciaPespecifico == null || frecuenciaPespecifico.Id == null || frecuenciaPespecifico.Id == 0)
                    {
                        throw new BadRequestException($"No existe el registro frecuenciaPespecifico {dto.IdPespecifico}");
                    }
                    var frecuenciaDetalle = _unitOfWork.PespecificoFrecuenciaDetalleRepository.ObtenerPorIdPespecificoFrecuencia(frecuenciaPespecifico.Id.Value).ToList();
                    var listaFeriados = _mapper.Map<IEnumerable<FeriadoDTO>>(_unitOfWork.FeriadoRepository.ObtenerPorTipo(0));
                    if (frecuenciaPespecifico.IdFrecuencia == null || frecuenciaPespecifico.IdFrecuencia == 0)
                    {
                        throw new BadRequestException($"IdFrecuencia no definido");
                    }
                    var frecuenciaGeneral = _unitOfWork.FrecuenciaRepository.ObtenerFrecuenciaPorId(frecuenciaPespecifico.IdFrecuencia.Value);

                    if (frecuenciaGeneral == null)
                    {
                        throw new BadRequestException($"No existe frecuencia {frecuenciaPespecifico.IdFrecuencia.Value}");
                    }

                    DateTime fechaAsignar = frecuencia.FechaInicio;
                    List<PEspecificoSesion> listaSesiones = new List<PEspecificoSesion>();
                    List<EsquemaSesionesDTO> estructuraSesiones = new List<EsquemaSesionesDTO>();
                    byte[] diasFrecuencia = frecuenciaDetalle.Select(s => s.DiaSemana).ToArray();

                    int cont = 0;
                    if (listaCursos.Count() != 0) //Si tiene cursos asociados
                    {
                        foreach (var curso in listaCursos)
                        {
                            if (dto.ListaPEspecificos.Contains(curso.Id))
                            {
                                decimal duracion = Convert.ToDecimal(curso.Duracion);
                                while (duracion > 0)
                                {
                                    EsquemaSesionesDTO esquemaSesion = new();
                                    esquemaSesion.Curso = curso;
                                    esquemaSesion.Duracion = duracion - frecuenciaDetalle[cont].Duracion < 0 ? duracion : frecuenciaDetalle[cont].Duracion;
                                    esquemaSesion.Dia = frecuenciaDetalle[cont].DiaSemana;
                                    estructuraSesiones.Add(esquemaSesion);
                                    duracion = duracion - frecuenciaDetalle[cont].Duracion;
                                    cont = (cont + 1) % diasFrecuencia.Length;
                                }
                            }
                        }
                    }
                    else // Si es un curso individual
                    {
                        DatosListaPespecificoDTO? datosPespecifico = _unitOfWork.PEspecificoRepository.ObtenerDatosCompletosPespecificoPorId(dto.IdPespecifico);

                        if (datosPespecifico == null)
                        {
                            throw new BadRequestException($"No se encontro pespecifico {dto.IdPespecifico}");
                        }
                        InformacionPespecificoHijoDTO pEspecificoIndividual = new()
                        {
                            Id = datosPespecifico.Id,
                            Nombre = datosPespecifico.Nombre,
                            Duracion = datosPespecifico.Duracion,
                            IdCiudad = datosPespecifico.IdCiudad,
                            TipoAmbiente = datosPespecifico.TipoAmbiente,
                            IdAmbiente = datosPespecifico.IdAmbiente,
                            IdProgramaGeneral = datosPespecifico.IdProgramaGeneral,
                            IdModalidadCurso = datosPespecifico.TipoId
                        };

                        decimal Duracion = Convert.ToDecimal(pEspecificoIndividual.Duracion);

                        while (Duracion > 0)
                        {
                            EsquemaSesionesDTO esquemaSesion = new();
                            esquemaSesion.Curso = pEspecificoIndividual;
                            esquemaSesion.Duracion = Duracion - frecuenciaDetalle[cont].Duracion < 0 ? Duracion : frecuenciaDetalle[cont].Duracion;
                            esquemaSesion.Dia = frecuenciaDetalle[cont].DiaSemana;
                            estructuraSesiones.Add(esquemaSesion);
                            Duracion = Duracion - frecuenciaDetalle[cont].Duracion;
                            cont = (cont + 1) % diasFrecuencia.Length;
                        }
                    }

                    //Se obtiene las horas de las sesiones
                    DateTime fechaTemp = fechaAsignar;
                    //DateTime fechaTemp = fechaAsignar;
                    PEspecificoSesion? sesionAnterior = null;
                    int countGrupoSesiones = 1;

                    IPEspecificoSesionService pEspecificoSesionService = new PEspecificoSesionService(_unitOfWork);
                    for (int i = 0; i < estructuraSesiones.Count; i++)
                    {
                        PEspecificoSesion pespecificoSesion = new();
                        if (i % diasFrecuencia.Length == 0 && i != 0)
                        {
                            fechaAsignar = fechaTemp.AddDays(frecuenciaGeneral.NumDias);
                            fechaTemp = fechaAsignar;
                        }

                        fechaAsignar = pEspecificoSesionService.ObtenerFechaAsignar(estructuraSesiones[i].Curso, fechaAsignar, estructuraSesiones[i].Dia.Value, diasFrecuencia, listaFeriados);
                        fechaAsignar = fechaAsignar.Date + frecuenciaDetalle[i % diasFrecuencia.Length].HoraDia;
                        estructuraSesiones[i].FechaAsignar = fechaAsignar;

                        //Se inserta la sesion

                        pespecificoSesion.IdPespecifico = estructuraSesiones[i].Curso.Id;
                        pespecificoSesion.FechaHoraInicio = estructuraSesiones[i].FechaAsignar.Value;
                        pespecificoSesion.Duracion = estructuraSesiones[i].Duracion.Value;
                        pespecificoSesion.IdAmbiente = estructuraSesiones[i].Curso.IdAmbiente;
                        pespecificoSesion.IdModalidadCurso = estructuraSesiones[i].Curso.IdModalidadCurso;
                        pespecificoSesion.SesionAutoGenerada = true;

                        if (listaCursos.Count() == 0)//si es curso individual se guarda el expositor del curso en la sesion
                            pespecificoSesion.IdExpositor = estructuraSesiones[i].Curso.IdExpositor_Referencia;
                        //271117 - para que arme la duracion y el hoario como con los que tienen esete flag activo
                        pespecificoSesion.Grupo = 1;
                        pespecificoSesion.Predeterminado = true;
                        if (sesionAnterior == null)
                        {
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico != pespecificoSesion.IdPespecifico)
                        {
                            countGrupoSesiones = 1;
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico == pespecificoSesion.IdPespecifico)
                        {
                            TimeSpan t = pespecificoSesion.FechaHoraInicio - sesionAnterior.FechaHoraInicio;
                            if (t.Days > 1)
                                countGrupoSesiones++;
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }

                        sesionAnterior = pespecificoSesion;
                        pespecificoSesion.Estado = true;
                        pespecificoSesion.FechaCreacion = DateTime.Now;
                        pespecificoSesion.FechaModificacion = DateTime.Now;
                        pespecificoSesion.UsuarioCreacion = usuario;
                        pespecificoSesion.UsuarioModificacion = usuario;
                        listaSesiones.Add(pespecificoSesion);
                    }

                    var resultadoListaSesiones = _unitOfWork.PEspecificoSesionRepository.Add(listaSesiones);
                    _unitOfWork.Commit();
                    listaSesiones = _mapper.Map<List<PEspecificoSesion>>(resultadoListaSesiones);
                    if (listaSesiones.Count > 0)
                    {
                        pEspecifico.IdSesionInicio = listaSesiones[0].Id;
                        pEspecifico.FechaModificacion = DateTime.Now;
                        pEspecifico.UsuarioModificacion = usuario;

                        _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                        _unitOfWork.Commit();
                    }

                    if (dto.CheckTiempoFrecuencia == true
                        || dto.CheckEnvioCorreo == true
                        || dto.CheckEnvioWhatsApp == true
                        || dto.CheckEnvioCorreoConfirmacion == true
                        || dto.CheckEnvioCorreoDocente == true)
                    {
                        var listado = _unitOfWork.PEspecificoRepository.ObtenerConfiguracionWebinarPEspecifico(dto.IdPespecifico);
                        if (listado == null)
                        {
                            var resultado = _unitOfWork.PEspecificoRepository.InsertarFrecuenciaWebinar(dto);
                        }
                        else
                        {
                            var resultado = _unitOfWork.PEspecificoRepository.EliminarFrecuenciaWebinar(dto.IdPespecifico);
                            resultado = _unitOfWork.PEspecificoRepository.InsertarFrecuenciaWebinar(dto);
                        }
                    }
                    else
                    {
                        var resultado = _unitOfWork.PEspecificoRepository.EliminarFrecuenciaWebinar(dto.IdPespecifico);
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/07/2023
        /// Version: 1.0
        /// <summary>
        /// Clonar Sesiones
        /// </summary>
        /// <param name="dto">ParametrosInsertaFrecuenciaDTO</param>
        /// <param name="usuario">Usuario</param>
        /// <returns> Lista DTO - IEnumerable<CronogramaGrupoDTO> </returns>
        public bool ClonarSesiones(int idPespecifico, string usuario, int idPespecificoHijo)
        {
            try
            {
                var estado = false;
                var idPespecificoTemp = (idPespecificoHijo == 0) ? idPespecifico : idPespecificoHijo;
                if (idPespecificoHijo != 0)
                {
                    if (!_unitOfWork.PespecificoPadrePespecificoHijoRepository.Exist(x => x.PespecificoPadreId == idPespecifico && x.PespecificoHijoId == idPespecificoHijo))
                    {
                        throw new BadRequestException("No se Encontro Programa Asociado");
                    }
                }
                var listaSesionesGrupo1 = _unitOfWork.PEspecificoSesionRepository.GetBy(x => x.IdPespecifico == idPespecificoTemp && x.Grupo == 1);

                var grupoSesiones = _unitOfWork.PEspecificoSesionRepository.GetBy(x => x.IdPespecifico == idPespecificoTemp).OrderByDescending(x => x.Grupo)!;
                int ultimoGrupo = grupoSesiones.FirstOrDefault()!.Grupo;

                int nuevoGrupo = 0;
                if (ultimoGrupo != 0)
                    nuevoGrupo = ultimoGrupo + 1;
                else
                    nuevoGrupo = 1;
                int orden = 0;

                foreach (var item in listaSesionesGrupo1.OrderBy(w => w.Id))
                {
                    PEspecificoSesion sesion = new()
                    {
                        IdPespecifico = item.IdPespecifico,
                        FechaHoraInicio = item.FechaHoraInicio,
                        Duracion = item.Duracion,
                        IdAmbiente = item.IdAmbiente,
                        Grupo = nuevoGrupo,
                        SesionAutoGenerada = item.SesionAutoGenerada,
                        IdExpositor = item.IdExpositor,
                        Predeterminado = item.Predeterminado,
                        Version = item.Version,
                        GrupoSesion = item.GrupoSesion,
                        IdModalidadCurso = item.IdModalidadCurso,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                    };
                    var sesionTemp = _unitOfWork.PEspecificoSesionRepository.Add(sesion);
                    _unitOfWork.Commit();
                    _unitOfWork.DetachAll();
                    sesion.Id = sesionTemp.Id;
                    if (sesion.Id == _unitOfWork.PEspecificoSesionRepository.ObtenerSesionInicial(sesion.IdPespecifico, sesion.Grupo))
                    {
                        orden++;
                        Proveedor proveedor = new();
                        string? expositorGrupo = null;
                        if (sesion.IdProveedor != null && sesion.IdProveedor.Value != 0)
                        {
                            proveedor = _unitOfWork.ProveedorRepository.ObtenerPorId(sesion.IdProveedor.Value)!;
                            expositorGrupo = proveedor.Nombre1 + " " + proveedor.Nombre2 + " " + proveedor.ApePaterno + " " + proveedor.ApeMaterno;
                        }

                        if (_unitOfWork.PespecificoParticipacionExpositorRepository.Exist(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo))
                        {
                            var pEspecificoParticipacionExpositor = _mapper.Map<PespecificoParticipacionExpositor>(_unitOfWork.PespecificoParticipacionExpositorRepository.FirstBy(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo));
                            pEspecificoParticipacionExpositor.IdExpositorGrupo = sesion.IdExpositor;
                            pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                            pEspecificoParticipacionExpositor.ExpositorGrupo = expositorGrupo;
                            pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.UsuarioModificacion = usuario;
                            _unitOfWork.PespecificoParticipacionExpositorRepository.Update(pEspecificoParticipacionExpositor);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            PespecificoParticipacionExpositor pEspecificoParticipacionExpositor = new()
                            {
                                IdPespecifico = sesion.IdPespecifico,
                                IdExpositorGrupo = sesion.IdExpositor,
                                IdProveedorPlanificacionGrupo = sesion.IdProveedor,
                                ExpositorGrupo = expositorGrupo,
                                Grupo = sesion.Grupo,
                                Orden = orden,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                            };
                            _unitOfWork.PespecificoParticipacionExpositorRepository.Add(pEspecificoParticipacionExpositor);
                            _unitOfWork.Commit();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarFechaPorSesion(ActualizarFechaPorSesionDTO dto, string usuario)
        {

            try
            {
                //integraDBContext contexto = new integraDBContext();
                //PespecificoSesionBO PespecificoSesion = new PespecificoSesionBO(contexto); ;
                //PespecificoSesionRepositorio _repPespecificoSesion = new PespecificoSesionRepositorio(contexto);
                //PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(contexto);
                //PEspecificoParticipacionExpositorRepositorio _repPEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorRepositorio(contexto);
                //ProveedorRepositorio _repProveedor = new ProveedorRepositorio(contexto);
                PEspecifico pEspecifico = new();
                IPEspecificoSesionService pEspecificoSesionService = new PEspecificoSesionService(_unitOfWork);
                //var datosSesionAntiguo = _repPespecificoSesion;
                var pEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(dto.SesionId);
                if (pEspecificoSesion == null || pEspecificoSesion.Id == 0)
                {
                    throw new BadRequestException("Pespecifico sesion no existente");
                }
                pEspecificoSesion.UsuarioModificacion = usuario;
                pEspecificoSesion.FechaModificacion = DateTime.Now;

                if (dto.EsFechaInicio)
                {
                    var pEspecificoPadre = _unitOfWork.PEspecificoSesionRepository.ObtenerDatosPespecificoHijoPorSesion(dto.SesionId);

                    if (pEspecificoPadre != null && pEspecificoPadre.Id != 0) //Si es Curso Padre
                        pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(pEspecificoPadre.PEspecificoPadreId);
                    else//Si es Curso Individual
                        pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(pEspecificoSesion.IdPespecifico);

                    if (pEspecifico != null && pEspecifico.Id != 0)
                    {
                        pEspecifico.IdSesionInicio = pEspecificoSesion.Id;
                        pEspecifico.FechaModificacion = DateTime.Now;
                        pEspecifico.UsuarioModificacion = usuario;
                    }
                    else
                    {
                        throw new BadRequestException("No existe pEspecifico");
                    }
                }
                //using (TransactionScope scope = new TransactionScope())
                //{
                //    if (dto.EsFechaInicio)
                //    {
                //        _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                //        _unitOfWork.Commit();
                //    }
                //    if (dto.RecorrerFecha)
                //        pEspecificoSesionService.ActualizarFechaParaSesionRecorrerFechas(usuario);
                //    else
                //    {
                //        pEspecificoSesion.EsSesionInicio = dto.esFechaInicio;
                //        pEspecificoSesion.Comentario = dto.Comentario;
                //        pEspecificoSesion.FechaHoraInicio = dto.fecha;
                //        pEspecificoSesion.FechaModificacion = DateTime.Now;
                //        pEspecificoSesion.UsuarioModificacion = dto.usuario;
                //        _unitOfWork.PEspecificoSesionRepository.Update(pEspecificoSesion);
                //        _unitOfWork.Commit();
                //    }

                //    scope.Complete();
                //}

                //if (dto.SesionId == _repPespecificoSesion.ObtenerSesionInicial(pEspecificoSesion.IdPespecifico, pEspecificoSesion.Grupo))
                //{
                //    ProveedorBO proveedor = new ProveedorBO();
                //    if (pEspecificoSesion.IdProveedor != null)
                //    {
                //        proveedor = _repProveedor.FirstById(pEspecificoSesion.IdProveedor.Value);
                //    }
                //    PEspecificoParticipacionExpositorBO pEspecificoParticipacionExpositor = new PEspecificoParticipacionExpositorBO();
                //    if (_repPEspecificoParticipacionExpositor.Exist(w => w.IdPespecifico == pEspecificoSesion.IdPespecifico && w.Grupo == pEspecificoSesion.Grupo))
                //    {
                //        pEspecificoParticipacionExpositor = _repPEspecificoParticipacionExpositor.FirstBy(w => w.IdPespecifico == pEspecificoSesion.IdPespecifico && w.Grupo == pEspecificoSesion.Grupo);
                //        pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = pEspecificoSesion.IdProveedor;
                //        pEspecificoParticipacionExpositor.IdExpositorGrupo = pEspecificoSesion.IdExpositor;
                //        pEspecificoParticipacionExpositor.ExpositorGrupo = pEspecificoSesion.IdProveedor == null ? null : proveedor.Nombre1 + " " + proveedor.Nombre2 + " " + proveedor.ApePaterno + " " + proveedor.ApeMaterno;
                //        pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                //        pEspecificoParticipacionExpositor.UsuarioModificacion = dto.usuario;

                //    }
                //    else
                //    {
                //        pEspecificoParticipacionExpositor.IdPespecifico = pEspecificoSesion.IdPespecifico;
                //        pEspecificoParticipacionExpositor.IdExpositorGrupo = pEspecificoSesion.IdExpositor;
                //        pEspecificoParticipacionExpositor.ExpositorGrupo = pEspecificoSesion.IdProveedor == null ? null : proveedor.Nombre1 + " " + proveedor.Nombre2 + " " + proveedor.ApePaterno + " " + proveedor.ApeMaterno;
                //        pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = pEspecificoSesion.IdProveedor;
                //        pEspecificoParticipacionExpositor.Grupo = pEspecificoSesion.Grupo;
                //        pEspecificoParticipacionExpositor.Estado = true;
                //        pEspecificoParticipacionExpositor.FechaCreacion = DateTime.Now;
                //        pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                //        pEspecificoParticipacionExpositor.UsuarioCreacion = dto.usuario;
                //        pEspecificoParticipacionExpositor.UsuarioModificacion = dto.usuario;
                //    }
                //    _repPEspecificoParticipacionExpositor.Update(pEspecificoParticipacionExpositor);
                //}

                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Genera PDF Cronograma
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> string - rpta </returns>
        public string GenerarPDFCronogramaModulo(FiltroObtenerPDFDTO dto, string usuario)
        {
            try
            {
                //reduccion de tiempo de la vista
                var rpta = GenerarPDFCronogramaV2(dto.IdPespecifico, dto.CursoIndividual, dto.CursoNombre, usuario, null, dto.Grupo);
                var idCiudad = dto.Sesion.ElementAt(0).IdCiudad;
                if (idCiudad == 2)
                {
                    var diferenciasHorarias = _unitOfWork.DiferenciaHorariaRepository.ObtenerPorIdPaisOrigen(51);
                    foreach (var item in diferenciasHorarias)
                    {
                        var sesionHoraria = dto.Sesion;
                        sesionHoraria.ForEach(f => { f.FechaHoraInicio = f.FechaHoraInicio.AddHours(item.DiferenciaHoraria); });
                        GenerarPDFCronogramaAlterno(dto.IdPespecifico, dto.CursoIndividual, dto.CursoNombre + "-" + item.IdPaisDestino.ToString(), usuario, sesionHoraria, item.IdPaisDestino);
                        sesionHoraria.ForEach(f => { f.FechaHoraInicio = f.FechaHoraInicio.AddHours(-item.DiferenciaHoraria); });
                    }
                }
                return rpta;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Genera PDF Cronograma
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> string - rpta </returns>
        public string GenerarPDFCronogramaSemanal(FiltroObtenerPDFDTO dto, string usuario)
        {
            try
            {
                //reduccion de tiempo de la vista
                var rpta = GenerarPDFCronogramaSemanal(dto.IdPespecifico, dto.CursoIndividual, dto.CursoNombre, usuario, dto.Sesion, dto.Grupo);
                return rpta;
            }
            catch
            {
                throw;
            }
        }
        public byte[]? GenerarReporteAmbienteExcel(FiltroReporteAmbienteDTO filtro)
        {
            try
            {
                var resultado = _unitOfWork.PEspecificoRepository.ObtenerExcelReporteAmbiente(filtro);
                IExcelService excelService = new ExcelService(_unitOfWork);
                var data = excelService.ReporteAmbientePespecifico(resultado);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new BadRequestException("No se pudo cargar la informacion");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ModificarFrecuencia(ParametrosInsertaFrecuenciaDTO dto, string usuario)
        {
            try
            {
                PespecificoFrecuencia pEspecificoFrecuencia = new PespecificoFrecuencia();
                PEspecifico? pEspecifico = new();
                List<PespecificoFrecuenciaDetalle> frecuenciaDetalle = new List<PespecificoFrecuenciaDetalle>();

                using (TransactionScope scope = new TransactionScope())
                {
                    pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.IdPespecifico);
                    if (pEspecifico == null)
                    {
                        throw new BadRequestException("No se econtro la entidad pespecifico");
                    }
                    if (_unitOfWork.PespecificoFrecuenciaRepository.Exist(w => w.IdPespecifico == dto.IdPespecifico))
                    {
                        //tiene cursos asociados
                        List<int> IdSesiones = new List<int>();

                        foreach (var hijo in dto.ListaPEspecificos)
                        {
                            var lista_sesiones = _unitOfWork.PEspecificoSesionRepository.ListaPespecificoSesiones(hijo);
                            IdSesiones.AddRange(lista_sesiones);
                        }
                        _unitOfWork.PEspecificoSesionRepository.Delete(IdSesiones, usuario);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        pEspecificoFrecuencia = new PespecificoFrecuencia()
                        {
                            IdPespecifico = dto.IdPespecifico,
                            FechaInicio = dto.FechaInicio,
                            Frecuencia = -1,
                            NroSesiones = dto.listaDetalles.Count,
                            IdFrecuencia = dto.IdFrecuencia,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                        };
                        _unitOfWork.PespecificoFrecuenciaRepository.Add(pEspecificoFrecuencia);
                        _unitOfWork.Commit();
                    }

                    var listaPespecificoSesion = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerInformacionPespecificoSesion(dto.ListaPEspecificos[0]);
                    var listaFeriados = _mapper.Map<List<FeriadoDTO>>(_unitOfWork.FeriadoRepository.ObtenerPorTipo(0));

                    DateTime fechaAsignar = dto.FechaInicio;
                    List<PEspecificoSesion> listaSesiones = new List<PEspecificoSesion>();
                    List<EsquemaSesionesDTO> estructuraSesiones = new List<EsquemaSesionesDTO>();
                    byte[] diasFrecuencia = dto.listaDetalles.Select(s => s.DiaSemana).ToArray();

                    int cont = 0;

                    if (listaPespecificoSesion.Count() != 0) // Si tiene cursos asociados
                    {
                        foreach (var curso in listaPespecificoSesion)
                        {
                            if (dto.ListaPEspecificos.Contains(curso.Id))
                            {
                                decimal Duracion = Convert.ToDecimal(curso.Duracion);
                                while (Duracion > 0)
                                {
                                    EsquemaSesionesDTO esquemaSesion = new EsquemaSesionesDTO();
                                    esquemaSesion.Curso = curso;
                                    esquemaSesion.Duracion = Duracion - dto.listaDetalles[cont].Duracion < 0 ? Duracion : dto.listaDetalles[cont].Duracion;
                                    esquemaSesion.Dia = dto.listaDetalles[cont].DiaSemana;
                                    estructuraSesiones.Add(esquemaSesion);
                                    Duracion = Duracion - dto.listaDetalles[cont].Duracion;
                                    cont = (cont + 1) % diasFrecuencia.Length;
                                }
                            }
                        }
                    }

                    //Se obtiene las horas de las sesiones
                    DateTime fechaTemp = fechaAsignar;
                    PEspecificoSesion? sesionAnterior = null;
                    int countGrupoSesiones = 1;
                    PEspecifico pespecificohijo = new PEspecifico();
                    for (int i = 0; i < estructuraSesiones.Count; i++)
                    {
                        IPEspecificoSesionService pEspecificoSesionService = new PEspecificoSesionService(_unitOfWork);
                        PEspecificoSesion pespecificoSesion = new PEspecificoSesion();
                        if (i % diasFrecuencia.Length == 0 && i != 0)
                        {
                            fechaAsignar = fechaTemp.AddDays(dto.listaDetalles.Count);
                            fechaTemp = fechaAsignar;
                        }

                        fechaAsignar = pEspecificoSesionService.ObtenerFechaAsignar(estructuraSesiones[i].Curso, fechaAsignar, estructuraSesiones[i].Dia.Value, diasFrecuencia, listaFeriados);
                        fechaAsignar = fechaAsignar.Date + dto.listaDetalles[i % diasFrecuencia.Length].HoraDia;
                        estructuraSesiones[i].FechaAsignar = fechaAsignar;

                        //Se inserta la sesion
                        if (pespecificohijo.Id != estructuraSesiones[i].Curso.Id)
                        {
                            pespecificohijo = _unitOfWork.PEspecificoRepository.ObtenerPorId(estructuraSesiones[i].Curso.Id);
                        }

                        pespecificoSesion.IdPespecifico = estructuraSesiones[i].Curso.Id;
                        pespecificoSesion.FechaHoraInicio = estructuraSesiones[i].FechaAsignar.Value;
                        pespecificoSesion.Duracion = estructuraSesiones[i].Duracion.Value;
                        pespecificoSesion.IdAmbiente = estructuraSesiones[i].Curso.IdAmbiente;
                        pespecificoSesion.IdModalidadCurso = pespecificohijo.TipoId;
                        pespecificoSesion.SesionAutoGenerada = true;

                        if (listaPespecificoSesion.Count() == 0)//si es curso individual se guarda el expositor del curso en la sesion
                            pespecificoSesion.IdExpositor = estructuraSesiones[i].Curso.IdExpositor_Referencia;
                        //271117 - para que arme la duracion y el hoario como con los que tienen esete flag activo
                        pespecificoSesion.Grupo = 1;
                        pespecificoSesion.Predeterminado = true;
                        if (sesionAnterior == null)
                        {
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico != pespecificoSesion.IdPespecifico)
                        {
                            countGrupoSesiones = 1;
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }
                        if (sesionAnterior != null && sesionAnterior.IdPespecifico == pespecificoSesion.IdPespecifico)
                        {
                            TimeSpan t = pespecificoSesion.FechaHoraInicio - sesionAnterior.FechaHoraInicio;
                            if (t.Days > 1)
                            {
                                countGrupoSesiones++;
                            }
                            pespecificoSesion.GrupoSesion = countGrupoSesiones;
                        }

                        sesionAnterior = pespecificoSesion;
                        pespecificoSesion.Estado = true;
                        pespecificoSesion.FechaCreacion = DateTime.Now;
                        pespecificoSesion.FechaModificacion = DateTime.Now;
                        pespecificoSesion.UsuarioCreacion = usuario;
                        pespecificoSesion.UsuarioModificacion = usuario;
                        listaSesiones.Add(pespecificoSesion);

                    }

                    _unitOfWork.PEspecificoSesionRepository.Add(listaSesiones);
                    _unitOfWork.Commit();

                    if (listaSesiones.Count > 0)
                    {
                        pEspecifico.IdSesionInicio = listaSesiones[0].Id;
                        pEspecifico.FechaModificacion = DateTime.Now;
                        pEspecifico.UsuarioModificacion = usuario;

                        _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                        _unitOfWork.Commit();
                    }

                    if (dto.CheckTiempoFrecuencia == true || dto.CheckEnvioCorreo == true || dto.CheckEnvioWhatsApp == true || dto.CheckEnvioCorreoConfirmacion == true)
                    {
                        var resultadoEliminado = _unitOfWork.PEspecificoRepository.EliminarFrecuenciaWebinar(dto.IdPespecifico, usuario);
                        var resultado = _unitOfWork.PEspecificoRepository.InsertarFrecuenciaWebinar(dto);
                    }
                    else
                    {
                        var resultado = _unitOfWork.PEspecificoRepository.EliminarFrecuenciaWebinar(dto.IdPespecifico);
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina Cronograma Aprobado
        /// </summary>
        /// <param name="idPespecifico"></param>
        /// <param name="numeroGrupo"></param>
        /// <param name="usuario"></param>
        /// <returns> string - rpta </returns>
        public bool EliminarCronogramaDuplicado(int idPespecifico, int numeroGrupo, string usuario)
        {
            try
            {
                var listaCursos = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerPorPEspecificoPadreId(idPespecifico).Select(x => x.PespecificoHijoId);
                if (listaCursos.Count() != 0)
                {
                    var listaSesiones = _unitOfWork.PEspecificoSesionRepository.GetBy(x => listaCursos.Contains(x.IdPespecifico) && x.Grupo == numeroGrupo);
                    var listaParticipacion = _unitOfWork.PespecificoParticipacionExpositorRepository.GetBy(w => listaCursos.Contains(w.IdPespecifico) && w.Grupo == numeroGrupo).Select(w => w.Id);

                    _unitOfWork.PEspecificoSesionRepository.Delete(listaSesiones.Select(x => x.Id), usuario);
                    _unitOfWork.PespecificoParticipacionExpositorRepository.Delete(listaParticipacion, usuario);
                    _unitOfWork.Commit();
                }
                else
                {
                    var listaSesiones = _unitOfWork.PEspecificoSesionRepository.GetBy(x => x.IdPespecifico == idPespecifico && x.Grupo == numeroGrupo);
                    var listaParticipacion = _unitOfWork.PespecificoParticipacionExpositorRepository.GetBy(w => w.IdPespecifico == idPespecifico && w.Grupo == numeroGrupo).Select(w => w.Id);

                    _unitOfWork.PEspecificoSesionRepository.Delete(listaSesiones.Select(x => x.Id), usuario);
                    _unitOfWork.PespecificoParticipacionExpositorRepository.Delete(listaParticipacion, usuario);
                    _unitOfWork.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool InsertarEventoEspecial(FiltroSesionEspecialDTO dto, string usuario)
        {
            try
            {
                PEspecifico pespecifico;
                pespecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.PEspecificoPadreId);
                ProgramaGeneralTroncalDTO pgeneral = _unitOfWork.PGeneralRepository.ObtenerProgramaGeneralParaPespecificoPorId(pespecifico.IdProgramaGeneral.Value);

                string codigo = "000000";

                string? CentroCostoCodigoUltimo = _unitOfWork.CentroCostoRepository.ObtenerUltimoCentroCostoPorCodigo(codigo);

                if (CentroCostoCodigoUltimo == null)
                    codigo = codigo + "001";
                else
                    codigo = (Int32.Parse(CentroCostoCodigoUltimo) + 1).ToString();

                CentroCosto centroCostoNuevo = new CentroCosto();
                centroCostoNuevo.IdArea = pgeneral.IdArea;
                centroCostoNuevo.IdSubArea = pgeneral.IdSubArea;
                centroCostoNuevo.IdPgeneral = pgeneral.Codigo;
                centroCostoNuevo.Nombre = "Evento Especial";
                centroCostoNuevo.Codigo = codigo;
                centroCostoNuevo.IdAreaCc = "0-0";
                centroCostoNuevo.Estado = true;
                centroCostoNuevo.FechaCreacion = DateTime.Now;
                centroCostoNuevo.FechaModificacion = DateTime.Now;
                centroCostoNuevo.UsuarioCreacion = usuario;
                centroCostoNuevo.UsuarioModificacion = usuario;

                PEspecifico especificoNuevo = new PEspecifico();
                especificoNuevo.Nombre = dto.Nombre;
                especificoNuevo.Codigo = centroCostoNuevo.Codigo;
                especificoNuevo.EstadoP = pespecifico.EstadoP;
                especificoNuevo.Tipo = pespecifico.Tipo;
                especificoNuevo.TipoAmbiente = "1";
                especificoNuevo.Categoria = pespecifico.Categoria;
                especificoNuevo.IdProgramaGeneral = pespecifico.IdProgramaGeneral;
                especificoNuevo.Ciudad = pespecifico.Ciudad;
                especificoNuevo.FechaInicio = dto.Fecha;
                especificoNuevo.FechaTermino = dto.Fecha.AddHours((double)dto.Duracion);


                especificoNuevo.EstadoPid = pespecifico.EstadoPid;
                especificoNuevo.TipoId = pespecifico.TipoId;
                especificoNuevo.OrigenPrograma = pespecifico.OrigenPrograma;
                especificoNuevo.IdCiudad = pespecifico.IdCiudad;
                especificoNuevo.Duracion = dto.Duracion.ToString();
                especificoNuevo.ActualizacionAutomatica = pespecifico.ActualizacionAutomatica;
                especificoNuevo.CursoIndividual = true;
                especificoNuevo.IdAmbiente = pespecifico.IdAmbiente;
                especificoNuevo.IdEstadoPespecifico = pespecifico.IdEstadoPespecifico;
                especificoNuevo.Estado = true;
                especificoNuevo.FechaCreacion = DateTime.Now;
                especificoNuevo.FechaModificacion = DateTime.Now;
                especificoNuevo.UsuarioCreacion = usuario;
                especificoNuevo.UsuarioModificacion = usuario;
                especificoNuevo.EsEspecial = true; 

                var resCentroCosto = _unitOfWork.CentroCostoRepository.Add(centroCostoNuevo);
                _unitOfWork.Commit();
                centroCostoNuevo.Id = resCentroCosto.Id;
                especificoNuevo.CodigoBanco = "SesEs" + centroCostoNuevo.Id;
                especificoNuevo.IdCentroCosto = centroCostoNuevo.Id;

                var resPesp = _unitOfWork.PEspecificoRepository.Add(especificoNuevo);
                _unitOfWork.Commit();
                especificoNuevo.Id = resPesp.Id;
                PespecificoPadrePespecificoHijo dtoPadreHijo = new PespecificoPadrePespecificoHijo();
                dtoPadreHijo.PespecificoPadreId = dto.PEspecificoPadreId;
                dtoPadreHijo.PespecificoHijoId = especificoNuevo.Id;
                dtoPadreHijo.Estado = true;
                dtoPadreHijo.FechaCreacion = DateTime.Now;
                dtoPadreHijo.FechaModificacion = DateTime.Now;
                dtoPadreHijo.UsuarioCreacion = usuario;
                dtoPadreHijo.UsuarioModificacion = usuario;


                _unitOfWork.PespecificoPadrePespecificoHijoRepository.Add(dtoPadreHijo);
                _unitOfWork.Commit();
                PEspecificoSesion dtoSesion = new PEspecificoSesion();

                dtoSesion.IdPespecifico = especificoNuevo.Id;
                dtoSesion.FechaHoraInicio = dto.Fecha;
                dtoSesion.Duracion = dto.Duracion;
                dtoSesion.SesionAutoGenerada = false;
                dtoSesion.Grupo = dto.Grupo;
                dtoSesion.IdModalidadCurso = especificoNuevo.TipoId;
                dtoSesion.Estado = true;
                dtoSesion.FechaCreacion = DateTime.Now;
                dtoSesion.FechaModificacion = DateTime.Now;
                dtoSesion.UsuarioCreacion = usuario;
                dtoSesion.UsuarioModificacion = usuario;

                _unitOfWork.PEspecificoSesionRepository.Add(dtoSesion);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public RptaActualizarDuracionInsertarSesionDTO ActualizarDuracionInsertarSesion(InformacionPespecificoSesionDTO dto, string usuario)
        {
            try
            {
                PEspecifico? pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.IdPespecifico);
                if (pEspecifico == null || pEspecifico.Id == 0)
                {
                    throw new BadRequestException("Pespecifico no existente");
                }
                pEspecifico.Duracion = (Convert.ToDecimal(pEspecifico.Duracion) + dto.Duracion).ToString();
                pEspecifico.FechaModificacion = DateTime.Now;
                pEspecifico.UsuarioModificacion = usuario;

                PEspecificoSesion especificoSesion = new PEspecificoSesion()
                {
                    IdPespecifico = dto.IdPespecifico,
                    FechaHoraInicio = dto.FechaHoraInicio,
                    Duracion = dto.Duracion,
                    IdExpositor = dto.IdPespecifico,
                    IdAmbiente = dto.IdAmbiente,
                    Comentario = dto.Comentario,
                    SesionAutoGenerada = dto.SesionAutoGenerada,
                    Grupo = (dto.Grupo != 0) ? dto.Grupo : 1,
                    Version = 0,
                    IdModalidadCurso = pEspecifico.TipoId,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                };

                _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                var pesesion = _unitOfWork.PEspecificoSesionRepository.Add(especificoSesion);
                _unitOfWork.Commit();

                int idTipoPrograma = 0;

                if (pEspecifico.IdProgramaGeneral > 0)
                {
                    var pGeneral = _unitOfWork.PGeneralRepository
                        .ObtenerPGeneralPorId(pEspecifico.IdProgramaGeneral.Value);

                    idTipoPrograma = pGeneral?.IdTipoPrograma ?? 0;
                }

                return new RptaActualizarDuracionInsertarSesionDTO()
                {
                    IdTipoPrograma = idTipoPrograma,
                    IdPEspecificoSesion = pesesion.Id,
                    FechaSesion = pesesion.FechaHoraInicio
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/06/2023
        /// Version: 2.0
        /// <summary>
        /// Actualiza docente ambiente programa especifico
        /// </summary>
        /// <returns>bool estado actualizacion</returns>
        public (bool EstadoCruce, List<CruceSesionPEspecificoDTO>? Cruces) ActualizarDocenteAmbienteProgramaEspecifico(DocenteAmbientePEspecificoDTO dto, string usuario)
        {
            try
            {
                var fechasSesiones = _unitOfWork.PEspecificoSesionRepository.GetByQuery(x => x.IdPespecifico == dto.Id, x => new PEspecificoSesionFechasDTO
                {
                    Id = x.Id,
                    IdPEspecifico =
                    x.IdPespecifico,
                    FechaHoraInicio = x.FechaHoraInicio,
                    FechaHoraFin = x.FechaHoraInicio.AddHours(Convert.ToDouble(x.Duracion)),
                }).ToList();
                var fechasCruces = _unitOfWork.PEspecificoRepository.ValidarFechaExpositorCruce(dto, fechasSesiones);
                if (fechasCruces.Count != 0)
                {
                    return (true, fechasCruces);
                }
                else
                {
                    var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.Id);
                    if (pEspecifico == null || pEspecifico.Id == 0)
                    {
                        throw new BadRequestException("#PES-ADAPE-001@No existe la entidad pespecifico");
                    }

                    if (!string.IsNullOrEmpty(dto.Duracion))
                        pEspecifico.Duracion = dto.Duracion;
                    if (dto.IdAmbiente != null)
                        pEspecifico.IdAmbiente = dto.IdAmbiente;
                    if (dto.IdExpositor_Referencia != null)
                        pEspecifico.IdExpositorReferencia = dto.IdExpositor_Referencia;
                    if (dto.IdEstadoPEspecifico != null)
                    {
                        pEspecifico.EstadoPid = dto.IdEstadoPEspecifico;
                        pEspecifico.EstadoP =
                            _unitOfWork.EstadoPespecificoRepository.ObtenerPorId(dto.IdEstadoPEspecifico.Value).Nombre;
                        pEspecifico.IdEstadoPespecifico = dto.IdEstadoPEspecifico;
                    }
                    if (dto.IdModalidadCurso != null)
                    {
                        pEspecifico.TipoId = dto.IdModalidadCurso;
                        pEspecifico.Tipo = _unitOfWork.ModalidadCursoRepository.ObtenerPorId(dto.IdModalidadCurso.Value)!.Nombre;
                    }
                    if (dto.IdCiclo != null)
                    {
                        pEspecifico.IdCiclo = dto.IdCiclo;
                        // pEspecifico.Tipo = _unitOfWork.ModalidadCursoRepository.ObtenerPorId(dto.IdCiclo.Value)!.Nombre;
                    }
                    if (dto.IdPeriodoLectivo != null)
                    {
                        pEspecifico.IdPeriodoLectivo = dto.IdPeriodoLectivo;
                        // pEspecifico. = _unitOfWork.ModalidadCursoRepository.ObtenerPorId(dto.IdPeriodoLectivo.Value)!.Nombre;
                    }
                    if (dto.IdCursoMoodle != null)
                    {
                        pEspecifico.IdCursoMoodle = dto.IdCursoMoodle == 0 ? null : dto.IdCursoMoodle;
                    }
                    if (dto.IdCursoMoodlePrueba != null)
                    {
                        pEspecifico.IdCursoMoodlePrueba = dto.IdCursoMoodlePrueba == 0 ? null : dto.IdCursoMoodlePrueba;
                    }

                    pEspecifico.UsuarioModificacion = usuario;
                    pEspecifico.FechaModificacion = DateTime.Now;
                    _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                    _unitOfWork.Commit();
                    PespecificoParticipacionExpositor pEspecificoParticipacionExpositor = new PespecificoParticipacionExpositor();

                    if (dto.IdProveedor != null)
                    {
                        var proveedor = _unitOfWork.ProveedorRepository.ObtenerPorId(dto.IdProveedor.Value);
                        if (proveedor == null || proveedor.Id == 0)
                        {
                            throw new BadRequestException("#PES-ADAPE-001@No existe la entidad proveedor");
                        }
                        if (_unitOfWork.PespecificoParticipacionExpositorRepository.Exist(w => w.IdPespecifico == pEspecifico.Id && w.Grupo == 1))
                        {
                            pEspecificoParticipacionExpositor = _mapper.Map<PespecificoParticipacionExpositor>(_unitOfWork.PespecificoParticipacionExpositorRepository.FirstBy(w => w.IdPespecifico == pEspecifico.Id && w.Grupo == 1));
                            pEspecificoParticipacionExpositor.IdExpositorGrupo = dto.IdExpositor_Referencia;
                            pEspecificoParticipacionExpositor.ExpositorGrupo = dto.IdProveedor == null ? null : $"{proveedor.Nombre1} {proveedor.Nombre2} {proveedor.ApePaterno} {proveedor.ApeMaterno}";
                            pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = dto.IdProveedor;
                            pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.UsuarioModificacion = usuario;
                            _unitOfWork.PespecificoParticipacionExpositorRepository.Update(pEspecificoParticipacionExpositor);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            pEspecificoParticipacionExpositor.IdPespecifico = pEspecifico.Id;
                            pEspecificoParticipacionExpositor.IdExpositorGrupo = dto.IdProveedor;
                            pEspecificoParticipacionExpositor.ExpositorGrupo = dto.IdProveedor == null ? null : $"{proveedor.Nombre1} {proveedor.Nombre2} {proveedor.ApePaterno}";
                            pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = dto.IdProveedor;
                            pEspecificoParticipacionExpositor.Grupo = 1;
                            pEspecificoParticipacionExpositor.Estado = true;
                            pEspecificoParticipacionExpositor.FechaCreacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.UsuarioCreacion = usuario;
                            pEspecificoParticipacionExpositor.UsuarioModificacion = usuario;
                            _unitOfWork.PespecificoParticipacionExpositorRepository.Add(pEspecificoParticipacionExpositor);
                            _unitOfWork.Commit();
                        }
                    }

                    // ========== CREAR GESTIONCONTACTO PARA ASIGNACIÓN DE DOCENTE ==========
                    if (dto.IdProveedor != null && pEspecifico.IdCentroCosto.HasValue)
                    {
                        var proveedor = _unitOfWork.ProveedorRepository.ObtenerPorId(dto.IdProveedor.Value);
                        if (proveedor == null || proveedor.Id == 0)
                        {
                            throw new BadRequestException("#PES-ADAPE-002@No existe el proveedor especificado");
                        }

                        var persona = _unitOfWork.PersonaRepository.ObtenerPorEmail(proveedor.Email);
                        if (persona == null)
                        {
                            throw new BadRequestException($"#PES-ADAPE-003@El proveedor {proveedor.Nombre1} {proveedor.ApePaterno} no tiene una Persona asociada");
                        }

                        var clasificacionPersona = _unitOfWork.ClasificacionPersonaRepository
                            .FirstBy(w => w.IdPersona == persona.Id
                                       && w.IdTipoPersona == 4
                                       && w.IdTablaOriginal == proveedor.Id
                                       && w.Estado == true);

                        if (clasificacionPersona == null)
                        {
                            throw new BadRequestException($"#PES-ADAPE-004@El proveedor {proveedor.Nombre1} {proveedor.ApePaterno} (ID: {proveedor.Id}) no tiene ClasificacionPersona válida");
                        }

                        // Verificar si ya existe gestión activa
                        var gestionExistente = _unitOfWork.GestionContactoRepository
                            .FirstBy(w => w.IdClasificacionPersona == clasificacionPersona.Id
                                       && w.IdCentroCosto == pEspecifico.IdCentroCosto
                                       && w.Estado == true);

                        if (gestionExistente == null)
                        {
                            var gestionDTO = new CrearGestionContactoDTO
                            {
                                IdCentroCosto = pEspecifico.IdCentroCosto,
                                IdPersonal_Asignado = 6205,
                                IdClasificacionPersona = clasificacionPersona.Id,
                                IdFaseGestionContacto = 1,
                                IdOrigen = 1124,
                                IdEstadoGestionContacto = 2,
                                UsuarioCreacion = usuario,
                                Comentario = $"Asignación automática de docente a curso: {pEspecifico.Nombre} - Proveedor: {proveedor.Nombre1} {proveedor.ApePaterno}"
                            };

                            try
                            {
                                var gestionContactoService = new GestionContactoService(_unitOfWork);
                                await gestionContactoService.ProcesarInsercionGestionAsync(gestionDTO);
                            }
                            catch (Exception ex)
                            {
                                throw new BadRequestException($"#PES-ADAPE-005@Error al crear GestionContacto: {ex.Message}");
                            }
                        }
                    }
                    // ========== FIN GESTIONCONTACTO ==========

                    var listaSesiones = _unitOfWork.PEspecificoSesionRepository.GetBy(x => x.IdPespecifico == dto.Id && x.Grupo == 1);
                    foreach (var item in listaSesiones)
                    {
                        if (dto.IdAmbiente != null)
                            item.IdAmbiente = dto.IdAmbiente;
                        if (dto.IdExpositor_Referencia != null)
                            item.IdExpositor = dto.IdExpositor_Referencia;
                        if (dto.IdProveedor != null)
                            item.IdProveedor = dto.IdProveedor;
                        item.UsuarioModificacion = usuario;
                        item.FechaModificacion = DateTime.Now;
                    }
                    _unitOfWork.PEspecificoSesionRepository.Update(listaSesiones);
                    _unitOfWork.Commit();
                    return (false, null);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Max Mantilla.
        /// Fecha: 20/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de programas especificos padre
        /// </summary>
        /// <returns>List<PEspecificoProgramaGeneralFiltroDTO></returns>
        public List<PEspecificoProgramaGeneralFiltroDTO> ObtenerProgramasEspecificosPadres(int? tipo)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerProgramasEspecificosPadres(tipo);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 04/06/2023
        /// Version: 2.0
        /// <summary>
        /// Ejecuta procedimiento almacenado [pla].[SP_ProgramaEspecificoFiltro]
        /// </summary>
        /// <returns>Todos los programas especificos Padre e individuales con filtros especificos</returns>

        public List<ProgramaEspecificoMaterialDTO> ObtenerPorFiltro(ProgramaEspecificoMaterialFiltroDTO filtro)
        {
            return _unitOfWork.PEspecificoRepository.ObtenerPorFiltro(filtro);

        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public class ITextEventsCronograma : PdfPageEventHelper
        {
            public ITextEventsCronograma()
            {
            }
            // This is the contentbyte object of the writer
            PdfContentByte cb;

            // we will put the final number of pages in a template
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer
            BaseFont bf = null;

            // This keeps track of the creation time
            DateTime PrintTime = DateTime.Now;

            private string _header;


            #region Properties
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    PrintTime = DateTime.Now;
                    bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(100, 100);
                    footerTemplate = cb.CreateTemplate(50, 50);
                }
                catch (DocumentException de)
                {

                }
                catch (System.IO.IOException ioe)
                {

                }
            }
            public override void OnEndPage(PdfWriter writer, Document document)
            {

                iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

                iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

                Image jpg2 = Image.GetInstance("C:\\imagenes\\logotipo.png");

                PdfPTable TablaTop = new PdfPTable(3);
                float[] widths = new float[] { 10, 65, 25f };
                TablaTop.SetWidths(widths);

                PdfPCell cellx1 = new PdfPCell(jpg2, false);
                cellx1.Padding = 8f;
                cellx1.HorizontalAlignment = 1;
                cellx1.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellx1.Rowspan = 3;
                cellx1.BorderWidth = 1f;
                cellx1.BorderColor = BaseColor.LIGHT_GRAY;
                TablaTop.AddCell(cellx1);

                iTextSharp.text.Font _FSGC = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                iTextSharp.text.Font _FUColumns = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.NORMAL, BaseColor.DARK_GRAY);
                iTextSharp.text.Font _FUColumnsNegrita = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.DARK_GRAY);
                PdfPCell cellx2 = new PdfPCell(new Phrase("SISTEMA DE GESTIÓN DE LA CALIDAD", _FSGC));
                cellx2.HorizontalAlignment = 1;
                cellx2.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellx2.BorderColor = BaseColor.LIGHT_GRAY;
                cellx2.PaddingTop = 3f;
                cellx2.PaddingBottom = 3f;
                cellx2.BorderWidth = 1f;
                TablaTop.AddCell(cellx2);

                PdfPCell cellx3 = new PdfPCell(new Phrase("RE-PLA-009", _FUColumns));
                cellx3.HorizontalAlignment = 1;
                cellx3.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellx3.BorderColor = BaseColor.LIGHT_GRAY;
                cellx3.BorderWidth = 1f;
                TablaTop.AddCell(cellx3);

                iTextSharp.text.Font _FCronogramaAlumnos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                PdfPCell cellx4 = new PdfPCell(new Phrase("Cronograma de alumnos", _FUColumnsNegrita));
                cellx4.HorizontalAlignment = 1;
                cellx4.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellx4.Rowspan = 2;
                cellx4.BorderColor = BaseColor.LIGHT_GRAY;
                cellx4.BorderWidth = 1f;
                TablaTop.AddCell(cellx4);

                PdfPCell cellx5 = new PdfPCell(new Phrase("Revisión 02", _FUColumns));
                cellx5.HorizontalAlignment = 1;
                cellx5.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellx5.BorderColor = BaseColor.LIGHT_GRAY;
                cellx5.BorderWidth = 1f;
                TablaTop.AddCell(cellx5);

                PdfPCell cellx6 = new PdfPCell(new Phrase(DateTime.Now.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("es")), _FUColumns));
                cellx6.HorizontalAlignment = 1;
                cellx6.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellx6.BorderColor = BaseColor.LIGHT_GRAY;
                cellx6.BorderWidth = 1f;
                TablaTop.AddCell(cellx6);
                TablaTop.TotalWidth = document.PageSize.Width - 80f;
                TablaTop.WidthPercentage = 70;

                TablaTop.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 12);
                    cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
                    cb.EndText();
                    float len = bf.GetWidthPoint("hello", 12);
                    cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45), true);
                }
                //Add paging to footer
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 8.5f);
                    cb.SetColorStroke(BaseColor.DARK_GRAY);
                    cb.SetTextMatrix(document.PageSize.GetLeft(65f), document.PageSize.GetBottom(45));
                    cb.ShowText("(*) Duración total en horas cronológicas.\n");
                    cb.EndText();
                    //Linea 2
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 8.5f);
                    cb.SetColorStroke(BaseColor.DARK_GRAY);
                    cb.SetTextMatrix(document.PageSize.GetLeft(65f), document.PageSize.GetBottom(35));
                    cb.ShowText("* BSG Institute se reserva el derecho de reprogramar las fechas, los horarios o los cursos de acuerdo con su proceso de mejora continua o por causa de fuerza mayor. Las fechas de inicio \n");
                    cb.EndText();
                    //Linea 3
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 8.5f);
                    cb.SetColorStroke(BaseColor.DARK_GRAY);
                    cb.SetTextMatrix(document.PageSize.GetLeft(65f), document.PageSize.GetBottom(25));
                    cb.ShowText("están sujetas a contar con un número mínimo de inscritos.\n");
                    cb.EndText();
                    //Linea 4
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 8.5f);
                    cb.SetColorStroke(BaseColor.DARK_GRAY);
                    cb.SetTextMatrix(document.PageSize.GetLeft(65f), document.PageSize.GetBottom(15));
                    cb.ShowText("*El acceso a todos los cursos contenidos en el cronograma de alumnos está sujeto a los beneficios incluidos en la versión del programa (básica, profesional o gerencial) adquirida durante\n");
                    cb.EndText();
                    //Linea 5
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 8.5f);
                    cb.SetColorStroke(BaseColor.DARK_GRAY);
                    cb.SetTextMatrix(document.PageSize.GetLeft(65f), document.PageSize.GetBottom(5));
                    cb.ShowText("el proceso de matrícula.");
                    cb.EndText();

                    float len = bf.GetWidthPoint("", 12);
                    cb.AddTemplate(footerTemplate, document.PageSize.GetLeft(65) + len, document.PageSize.GetBottom(30), true);
                }

            }
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                headerTemplate.BeginText();
                headerTemplate.SetFontAndSize(bf, 12);
                headerTemplate.SetTextMatrix(0, 0);
                headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 12);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.EndText();
            }
        }

        public IEnumerable<PEspecificoDetalleFechaByPGeneral> ObtenerFiltroV2PorIdPGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerFiltroV2PorIdPGeneral(idPGeneral);
            }
            catch (Exception)
            {
                throw;
            }
        }
		public IEnumerable<PEspecificoByPGeneral> ObtenerPEspecificoByPGeneral(int idPGeneral)
		{
			return _unitOfWork.PEspecificoRepository.ObtenerPEspecificoByProgramaGeneral(idPGeneral);
		}
	}
}