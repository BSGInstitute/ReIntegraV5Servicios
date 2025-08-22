using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReporteSeguimientoOportunidadService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 25/11/2022
    /// <summary>
    /// Gestión general de T_ReporteSeguimientoOportunidad
    /// </summary>
    public class ReporteSeguimientoOportunidadService : IReporteSeguimientoOportunidadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteSeguimientoOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TReporteSeguimientoOportunidad, ReporteSeguimientoOportunidad>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        /// <param name="idLlamada"></param>
        /// <param name="nroBytes"></param>
        /// <returns>ValorIntDTO</returns>
        public ReporteSeguimientoOportunidadComboDTO ObtenerCombosReporte(int idPersonal)
        {
            try
            {
                var comboCentroCosto = _unitOfWork.CentroCostoRepository.ObtenerCombo();
                var comboFaseOportunidad = _unitOfWork.FaseOportunidadRepository.ObtenerCombo();
                List<PersonalAsignadoDTO> comboAsesores;
                if (idPersonal == 213)
                    comboAsesores = _unitOfWork.PersonalRepository.ObtenerAsesoresVentasOficialReporteSeguimiento();
                else
                    comboAsesores = _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoVentasRS(idPersonal).Where(w=>w.TipoPersonal=="Asesor" || w.TipoPersonal == "otro").ToList();

                var comboCriterio = ObtenerComboCriterioCalificacion();
                var comboObservacionMatricula = _unitOfWork.MatriculaCabeceraRepository.ObtenerObservacionMatricula();

                ReporteSeguimientoOportunidadComboDTO combosReporte = new ReporteSeguimientoOportunidadComboDTO()
                {
                    CentroCostos = comboCentroCosto.ToList(),
                    FaseOportunidades = comboFaseOportunidad.ToList(),
                    Asesores = comboAsesores,
                    CriteriosCalificacion = comboCriterio.ToList(),
                    ObservacionMatricula = comboObservacionMatricula.ToList()
                };
                return combosReporte;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Combos para el reporte de Seguimiento
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        private List<ComboDTO> ObtenerComboCriterioCalificacion()
        {
            try
            {
                var criterios = _unitOfWork.CriterioCalificacionRepository.ObtenerCombo();
                List<ComboDTO> lista = new List<ComboDTO>();
                foreach (var item in criterios)
                {
                    string? nombre = "";
                    switch (item.Nombre)
                    {
                        case "D":
                            nombre = "Se acepta(Convenio de Voz)";
                            break;
                        case "DE":
                            nombre = "Se acepta(Convenio Firmado)";
                            break;
                        case "EM":
                            nombre = "Empresa";
                            break;
                        case "OBS":
                            nombre = "Observaciones";
                            break;
                    }
                    if (!string.IsNullOrEmpty(nombre))
                    {
                        lista.Add(new ComboDTO
                        {
                            Id = item.Id,
                            Nombre = nombre
                        });
                    }
                }
                return lista;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica una llamada del repositorio
        /// </summary>
        /// <param name="idLlamada"></param>
        /// <param name="nroBytes"></param>
        /// <returns>ValorIntDTO</returns>
        public string ModificarLlamadaWebphone(EditarActividadLlamadaDTO obj, string usuario)
        {
            try
            {
                IGestionArchivoLlamadaService gestionArchivoLlamadaService = new GestionArchivoLlamadaService(_unitOfWork);
                string url = string.Empty;
                const string rutaBlob = "asterisk/2023/Regularizacion/";
                const string rutaCompleta = $"https://repositorioaudiollamada.blob.core.windows.net/{rutaBlob}";

                url = gestionArchivoLlamadaService.SubirArchivoAudioLlamada(obj.File.ConvertToByte(), obj.File.ContentType, obj.NombreArchivo, rutaCompleta, rutaBlob);

                _unitOfWork.LlamadaWebphoneAsteriskRepository.ModificarLlamadaWebphone(obj.IdLlamada, url, usuario, obj.DuracionContesto, obj.NroBytes);
                return url;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 21/03/2023
        /// Version: 1.0
        /// <summary>
        /// Generar una llamada en la actividad trabajada
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nroBytes"></param>
        /// <returns>ValorIntDTO</returns>
        public (int IdLlamadaWebphoneAsterisk, int IdLlamadaWebphoneCruceCentral, string url) GenerarNuevaLlamadaActividad(NuevaLlamadaActividadDTO obj, string usuario)
        {
            try
            {
                IGestionArchivoLlamadaService gestionArchivoBo = new GestionArchivoLlamadaService(_unitOfWork);
                string url = string.Empty;
                const string rutaBlob = "asterisk/2023/Regularizacion/";
                const string rutaCompleta = $"https://repositorioaudiollamada.blob.core.windows.net/{rutaBlob}";

                url = gestionArchivoBo.SubirArchivoAudioLlamada(obj.File.ConvertToByte(), obj.File.ContentType, obj.NombreArchivo, rutaCompleta, rutaBlob);

                //url = rutaCompleta + obj.NombreArchivo;
                //var anexo3CX = _unitOfWork.PersonalRepository.ObtenerAnexo3CXPersonal(obj.IdPersonalAsignado).Valor;

                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        ILlamadaWebphoneAsteriskService llamadaWebphoneAsteriskService = new LlamadaWebphoneAsteriskService(_unitOfWork);
                        ILlamadaWebphoneCruceCentralService llamadaWebphoneCruceCentralService = new LlamadaWebphoneCruceCentralService(_unitOfWork);
                        LlamadaWebphoneAsterisk nuevaLlamadaWebphoneAsterisk = llamadaWebphoneAsteriskService.Insertar(obj, url, usuario);
                        LlamadaWebphoneCruceCentral nuevaLlamadaWebphoneCruceCentral = llamadaWebphoneCruceCentralService.Insertar(obj, nuevaLlamadaWebphoneAsterisk, usuario);

                        scope.Complete();
                        return (nuevaLlamadaWebphoneAsterisk.Id, nuevaLlamadaWebphoneCruceCentral.Id, url);
                    }
                    catch (Exception)
                    {
                        scope.Dispose();
                        throw;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos necesarios para registrar nueva llamada
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idPersonalAsignado"></param>
        /// <returns>ValorIntDTO</returns>
        public DatosLlamadaDTO ObtenerDatosNuevaLlamada(int idAlumno, int idPersonalAsignado)
        {
            try
            {
                Alumno alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                Personal personal = _unitOfWork.PersonalRepository.ObtenerPorId(idPersonalAsignado);
                DatosLlamadaDTO datos = new DatosLlamadaDTO()
                {
                    Celular = alumno.Celular,
                    IdCodigoPais = alumno.IdCodigoPais.Value,
                    Anexo3CX = $"1{personal.Anexo}",
                    Central = personal.Central,
                };
                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        ///Autor: Flavio R. Mamani Fabian
        ///Fecha: 08/08
        /// <summary>
        /// Metodo para actualizar CronogramaVersionFinal del dia actual
        /// </summary>
        /// <returns></returns>
        public bool ActualizarCronogramaVersionFinal()
        {
            try
            {
                var res = _unitOfWork.ReportesRepository.ActualizarCronogramaVersionFinal();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 28/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene le reporte de seguimiento oportunidad por filtros
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>List<ReporteSeguimientoOportunidadesDTO> </returns>
        public List<ReporteSeguimientoOportunidadDTO> ReporteSeguimientoOportunidadTresCx(ReporteSeguimientoOportunidadesFiltrosDTO filtros)
        {
            try
            {
                var filtroOrdenado = new SeguimientoFiltroFinalDTO();

                if (filtros.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = string.Join(",", filtros.Asesores);
                }
                if (filtros.FasesOportunidad.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidad = string.Join(",", filtros.FasesOportunidad);
                }
                if (filtros.FaseOportunidadOrigen.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidadOrigen = string.Join(",", filtros.FaseOportunidadOrigen);
                }
                if (filtros.FaseOportunidadDestino.Count() > 0)
                {
                    filtroOrdenado.FasesOportunidadDestino = string.Join(",", filtros.FaseOportunidadDestino);
                }
                if (filtros.CentroCostos.Count() > 0)
                {
                    filtroOrdenado.CentroCostos = string.Join(",", filtros.CentroCostos);
                }
                filtroOrdenado.OpcionFase = filtros.OpcionFase;
                filtroOrdenado.FechaFin = new DateTime(filtros.FechaFin.Year, filtros.FechaFin.Month, filtros.FechaFin.Day, 23, 59, 59);
                filtroOrdenado.FechaInicio = new DateTime(filtros.FechaInicio.Year, filtros.FechaInicio.Month, filtros.FechaInicio.Day, 0, 0, 0);

                var data = _unitOfWork.ReportesRepository.ObtenerReporteSeguimientoTresCx(filtroOrdenado);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 28/11/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene le reporte de seguimiento oportunidad por filtros
        /// </summary>
        /// <param name="filtros"> Filtros de búsqueda </param>
        /// <returns>List<ReporteSeguimientoOportunidadesDTO> </returns>
        public (List<ReporteSeguimientoNWActividadAlternoDTO>, List<BloqueHorarioProcesarBicDTO>) ObtenerListaOportunidadLog3cx(int idOportunidad)
        {
            var servicioReporte = new ReporteService(_unitOfWork);
            IOportunidadLogService oportunidadLogService = new OportunidadLogService(_unitOfWork);
            var fechas = _unitOfWork.ReportesRepository.ObtenerActividadesNoEjecutadas(idOportunidad);
            var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerCombo().ToList();
            bloques.ForEach(x =>
            {
                x.Contador = 0;
            });
            var nombreTurnoUltimo = string.Empty;
            DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
            foreach (var fecha in fechas)
            {
                TimeSpan horaFecha = fecha.TimeOfDay;
                foreach (var bloque in bloques)
                {
                    if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                    {
                        if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                        else
                        {
                            nombreTurnoUltimo = bloque.Nombre;
                            fechaUltima = fecha.Date;
                            bloque.Contador++;
                            break;
                        }
                    }
                }
            }
            //var reporteSeguimientoOportunidadTresCxes = oportunidadLogService.ObtenerReporteSeguimientoNWActividadesPorIdOportunidad3cx(idOportunidad);
            var reporteSeguimientoOportunidadTresCxes = oportunidadLogService.ObtenerReporteSeguimientoActividadesPorIdOportunidad(idOportunidad);
            return (reporteSeguimientoOportunidadTresCxes, bloques);
        }
    }
}
