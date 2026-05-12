using AutoMapper;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using OfficeOpenXml;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{

    /// Service: OportunidadService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión general de T_Oportunidad
    /// </summary>
    public class OportunidadService : IOportunidadService
    {
        //operaciones
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper _maperOportunidad;
        public AsignacionOportunidadManualDTO _asignacionManual;
        public Alumno _alumno;
        public LlamadaActividad _llamadaActividad;
        public Oportunidad _oportunidad;
        public OportunidadBoDTO _oportunidadBo;
        private FaseOportunidadService faseOportunidadService;

        public OportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidad, Oportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadDTO, Oportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadBoDTO, Oportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<ActividadDetalleDTO, TActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadLogDTO, TOportunidadLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<ExpositorDTO, TExpositor>(MemberList.None).ReverseMap();
                cfg.CreateMap<Expositor, TExpositor>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadBoDTO, TOportunidadLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<OportunidadDTO, AsignacionOportunidadManual>(MemberList.None);
                cfg.CreateMap<TOportunidadLog, OportunidadLog>(MemberList.None).ReverseMap();
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCalidadProcesamiento, CalidadProcesamiento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSolucionClienteByActividad, SolucionClienteByActividad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDetalleOportunidadCompetidor, DetalleOportunidadCompetidor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadCompetidor, OportunidadCompetidor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TComprobantePagoOportunidad, ComprobantePagoOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadBeneficio, OportunidadBeneficio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadPrerequisitoEspecifico, OportunidadPrerequisitoEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TOportunidadPrerequisitoGeneral, OportunidadPrerequisitoGeneral>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAsignacionOportunidad, AsignacionOportunidad>(MemberList.None).ReverseMap();
                cfg.CreateMap<TModeloDataMining, ModeloDataMining>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAsignacionAutomatica, AsignacionAutomaticaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAsignacionAutomatica, AsignacionAutomatica>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsignacionAutomatica, AsignacionAutomaticaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<Expositor, TExpositor>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAlumno, Alumno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Oportunidad Add(Oportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Oportunidad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Oportunidad Update(Oportunidad entidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRepository.Update(entidad);
                var teste = 0;
                _unitOfWork.Commit();
                return _mapper.Map<Oportunidad>(modelo);
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
                _unitOfWork.OportunidadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Oportunidad> Add(List<Oportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Oportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Oportunidad> Update(List<Oportunidad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OportunidadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Oportunidad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.OportunidadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Oportunidad
        /// </summary>
        /// <returns> List<OportunidadDTO> </returns>
        public IEnumerable<Oportunidad> ObtenerOportunidad()
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerOportunidad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Oportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<OportunidadComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de Venta Cruzada para Agenda relacionada a un Id Clasificacion Persona.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> List<OportunidadVentaCruzadaAgendaDTO> </returns>
        public IEnumerable<OportunidadVentaCruzadaAgendaDTO> ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(idClasificacionPersona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 23/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Historial de Oportunidades para Agenda relacionado a un Id Clasificacion Persona.
        /// </summary>
        /// <param name="idClasificacionPersona">Id de Clasificacion Persona</param>
        /// <returns> List<OportunidadHistorialAgendaDTO> </returns>
        public IEnumerable<OportunidadHistorialAgendaDTO> ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(int idClasificacionPersona)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(idClasificacionPersona);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 11/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene tiempo de capacitacion por Id Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> Task<TiempoCapacitacionPorIdOportunidadDTO> </returns>
        public async Task<TiempoCapacitacionPorIdOportunidadDTO> ObtenerOportunidadTiempoCapacitacionAsync(int idOportunidad)
        {
            if (idOportunidad == 0)
            {
                throw new BadRequestException($"Id Oportunidad No Valido: {idOportunidad}");
            }
            var task1 = _unitOfWork.TiempoCapacitacionRepository.ObtenerComboAsync();
            var task2 = _unitOfWork.OportunidadRepository.ObtenerTiempoCapacitacionPorIdOportunidadAsync(idOportunidad);
            var comboTiempoCapacitacion = await task1;
            var oportunidad = await task2;
            var respuesta = new TiempoCapacitacionPorIdOportunidadDTO()
            {
                Records = comboTiempoCapacitacion,
                Record = oportunidad.IdTiempoCapacitacion,
                Lista = false,
                ListaValidacion = false,
                RecordValidado = oportunidad.IdTiempoCapacitacionValidacion
            };
            if (oportunidad.IdTiempoCapacitacion == null)
            {
                if (oportunidad.IdTiempoCapacitacionValidacion != null)
                    respuesta.ListaValidacion = true;
            }
            else
            {
                respuesta.Lista = true;
                if (oportunidad.IdTiempoCapacitacionValidacion != null)
                    respuesta.ListaValidacion = true;
            }
            return respuesta;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos Compuestos de la Oportunidad asociada a una Actividad Detalle.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> OportunidadCompuestoDTO </returns>
        public OportunidadCompuestoDTO ObtenerOportunidadCompuestoPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerOportunidadCompuestoPorIdActividadDetalle(idActividadDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id de Solicitud Visualizacion, la Fecha Limite y Valor de Visualizacion
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> ResultadoVisualizarOportunidadDTO </returns>
        public ResultadoVisualizarOportunidadDTO ValidarVisualizarAgendaPorIdOportunidad(int idOportunidad, int idPersonal)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ValidarVisualizarAgendaPorIdOportunidad(idOportunidad, idPersonal);
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
        /// Obtiene todos los datos de T_Oportunidad asociados a un Id
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public Oportunidad ObtenerPorId(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public string ObtenerCronogramaPagoCompleto(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerCronogramaPagoCompleto(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public string ObtenerMontoTotal(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerMontoTotal(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadDTO </returns>
        public string ObtenerVersion(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerVersion(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por Id de la Oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> OportunidadCompuestoDTO </returns>
        public OportunidadCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si existe una oportunidadNuevaEntidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <param name="idContacto">Id Contacto</param>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <param name="idOcurrencia">Id de Ocurrencia</param>
        /// <returns> bool </returns>
        public bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ValidarRN2(idContacto, idCentroCosto, idOcurrencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos necesarios de la oportunidadNuevaEntidad para calcualr la fecha de programacion Automatica
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <returns> DatosOportunidadReprogramacionAutomaticaDTO </returns>
        public DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramacionAutomatica(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerDatosParaReprogramacionAutomatica(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// Autor Modificacion: Gilmer Quispe.
        /// Fecha Modificacion: 13/01/2023
        /// Version: 1.1
        /// <summary> Modificacion
        /// Se cambia condicion: [Antes IdFaseOportunidad] [Actual:IdActividadDetalle_Ultima]
        /// </summary>
        /// <summary>
        /// Determina si una Oportunidad existe y si tiene la Fase consultada
        /// </summary>
        /// <param name="idOportunidad">Id de Oportunidad</param>
        /// <param name="idFaseOportunidad"> Ultimo Id de la T_ActividadDetalle </param>
        /// <returns> bool </returns>
        public ActividadTrabajadaDTO ValidarFaseOportunidad(int idOportunidad, int idFaseOportunidad, int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ValidarFaseOportunidad(idOportunidad, idFaseOportunidad, idActividadDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de entidad Oportunidad a OportunidadBoDTO
        /// </summary>
        /// <returns> Oportunidad </returns>
        public OportunidadBoDTO MapeoOportunidadBaseObjetDesdeEntidad(Oportunidad dto)
        {
            try
            {
                var entidad = _mapper.Map<OportunidadBoDTO>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de OportunidadBoDTO a Entidad Oportunidad
        /// </summary>
        /// <returns> Oportunidad </returns>
        public Oportunidad MapeoBoDTO(OportunidadBoDTO objetoBO)
        {
            _unitOfWork.DetachAll();
            try
            {

                Oportunidad entidad = _mapper.Map<Oportunidad>(objetoBO);
                entidad.ActividadDetalles = new List<ActividadDetalle>();
                entidad.OportunidadLogs = new List<OportunidadLog>();
                entidad.ComprobantePagoOportunidads = new List<ComprobantePagoOportunidad>();
                entidad.OportunidadCompetidors = new List<OportunidadCompetidor>();
                entidad.CalidadProcesamientos = new List<CalidadProcesamiento>();
                entidad.SolucionClienteByActividads = new List<SolucionClienteByActividad>();
                entidad.ModeloDataMinings = new List<ModeloDataMining>();
                entidad.AsignacionOportunidads = new List<AsignacionOportunidad>();

                if (entidad != null) entidad.Estado = true;

                //mapea los hijos
                if (objetoBO.ActividadAntigua != null)
                {
                    //ActividadDetalle entidadHijo = new ActividadDetalle();
                    //entidadHijo = _mapper.Map<ActividadDetalle>(objetoBO.ActividadAntigua);
                    entidad.ActividadDetalles.Add(objetoBO.ActividadAntigua);
                }
                if (objetoBO.ActividadNueva != null)
                {
                    //ActividadDetalle entidadHijo = new ActividadDetalle();
                    //entidadHijo = _mapper.Map<ActividadDetalle>(objetoBO.ActividadNueva);
                    entidad.ActividadDetalles.Add(objetoBO.ActividadNueva);
                }
                if (objetoBO.ActividadNuevaProgramarActividad != null)
                {
                    //ActividadDetalle entidadHijo = new ActividadDetalle();
                    //entidadHijo = _mapper.Map<ActividadDetalle>(objetoBO.ActividadNuevaProgramarActividad);
                    entidad.ActividadDetalles.Add(objetoBO.ActividadNuevaProgramarActividad);
                }
                if (objetoBO.Actividades != null && objetoBO.Actividades.Count > 0)
                {
                    foreach (var hijo in objetoBO.Actividades)
                    {
                        //ActividadDetalle entidadHijo = new ActividadDetalle();
                        //entidadHijo = _mapper.Map<ActividadDetalle>(hijo);
                        entidad.ActividadDetalles.Add(hijo);
                    }
                }
                if (objetoBO.OportunidadLogAntigua != null)
                {
                    //OportunidadLog entidadHijo = new OportunidadLog();
                    //entidadHijo = _mapper.Map<OportunidadLog>(objetoBO.OportunidadLogAntigua);
                    entidad.OportunidadLogs.Add(objetoBO.OportunidadLogAntigua);
                }
                if (objetoBO.OportunidadLogNueva != null)
                {
                    //OportunidadLog entidadHijo = new OportunidadLog();
                    //entidadHijo = _mapper.Map<OportunidadLog>(objetoBO.OportunidadLogNueva);
                    entidad.OportunidadLogs.Add(objetoBO.OportunidadLogNueva);
                }
                if (objetoBO.ComprobantePago != null)
                {
                    //ComprobantePagoOportunidad entidadHijo = new ComprobantePagoOportunidad();
                    //entidadHijo = _mapper.Map<ComprobantePagoOportunidad>(objetoBO.ComprobantePago);
                    entidad.ComprobantePagoOportunidads.Add(objetoBO.ComprobantePago);
                }
                if (objetoBO.OportunidadCompetidor != null)
                {
                    //OportunidadCompetidor entidadHijo = new OportunidadCompetidor();
                    //entidadHijo = _mapper.Map<OportunidadCompetidor>(objetoBO.OportunidadCompetidor);
                    entidad.OportunidadCompetidors.Add(objetoBO.OportunidadCompetidor);

                    //mapea a los hijos del nivel
                    //if (objetoBO.OportunidadCompetidor.OportunidadPrerequisitoGenerals != null && objetoBO.OportunidadCompetidor.OportunidadPrerequisitoGenerals.Count > 0)
                    //{
                    //    foreach (var hijo in objetoBO.OportunidadCompetidor.OportunidadPrerequisitoGenerals)
                    //    {
                    //        OportunidadPrerequisitoGeneral entidadHijo2 = new OportunidadPrerequisitoGeneral();
                    //        entidadHijo2 = _mapper.Map<OportunidadPrerequisitoGeneral>(hijo);
                    //        entidadHijo.OportunidadPrerequisitoGenerals.Add(entidadHijo2);
                    //    }
                    //}
                    //if (objetoBO.OportunidadCompetidor.OportunidadPrerequisitoEspecificos != null && objetoBO.OportunidadCompetidor.OportunidadPrerequisitoEspecificos.Count > 0)
                    //{
                    //    foreach (var hijo in objetoBO.OportunidadCompetidor.OportunidadPrerequisitoEspecificos)
                    //    {
                    //        OportunidadPrerequisitoEspecifico entidadHijo2 = new OportunidadPrerequisitoEspecifico();
                    //        entidadHijo2 = _mapper.Map<OportunidadPrerequisitoEspecifico>(hijo);
                    //        entidadHijo.OportunidadPrerequisitoEspecificos.Add(entidadHijo2);
                    //    }
                    //}
                    //if (objetoBO.OportunidadCompetidor.OportunidadBeneficios != null && objetoBO.OportunidadCompetidor.OportunidadBeneficios.Count > 0)
                    //{
                    //    foreach (var hijo in objetoBO.OportunidadCompetidor.OportunidadBeneficios)
                    //    {
                    //        OportunidadBeneficio entidadHijo2 = new OportunidadBeneficio();
                    //        entidadHijo2 = _mapper.Map<OportunidadBeneficio>(hijo);
                    //        entidadHijo.OportunidadBeneficios.Add(entidadHijo2);
                    //    }
                    //}
                    //if (objetoBO.OportunidadCompetidor.DetalleOportunidadCompetidors != null && objetoBO.OportunidadCompetidor.DetalleOportunidadCompetidors.Count > 0)
                    //{
                    //    foreach (var hijo in objetoBO.OportunidadCompetidor.DetalleOportunidadCompetidors)
                    //    {
                    //        DetalleOportunidadCompetidor entidadHijo2 = new DetalleOportunidadCompetidor();
                    //        entidadHijo2 = _mapper.Map<DetalleOportunidadCompetidor>(hijo);
                    //        entidadHijo.DetalleOportunidadCompetidors.Add(entidadHijo2);
                    //    }
                    //}
                }
                if (objetoBO.CalidadProcesamiento != null)
                {
                    //CalidadProcesamiento entidadHijo = new CalidadProcesamiento();
                    //entidadHijo = _mapper.Map<CalidadProcesamiento>(objetoBO.CalidadProcesamiento);
                    entidad.CalidadProcesamientos.Add(objetoBO.CalidadProcesamiento);
                }
                if (objetoBO.ListaSoluciones != null && objetoBO.ListaSoluciones.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaSoluciones)
                    {
                        //SolucionClienteByActividad entidadHijo = new SolucionClienteByActividad();
                        //entidadHijo = _mapper.Map<SolucionClienteByActividad>(hijo);
                        entidad.SolucionClienteByActividads.Add(hijo);
                    }
                }

                //Creacion de oportunidades
                if (objetoBO.ModeloDataMining != null)
                {
                    //ModeloDataMining entidadHijo = new ModeloDataMining();
                    //entidadHijo = _mapper.Map<ModeloDataMining>(objetoBO.ModeloDataMining);
                    entidad.ModeloDataMinings.Add(objetoBO.ModeloDataMining);
                }
                if (objetoBO.AsignacionOportunidad != null)
                {
                    //AsignacionOportunidad entidadHijo = new AsignacionOportunidad();
                    //entidadHijo = _mapper.Map<AsignacionOportunidad>(objetoBO.AsignacionOportunidad);

                    entidad.AsignacionOportunidads.Add(objetoBO.AsignacionOportunidad);
                }
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Evalua si una Oportunidad Existe
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> bool </returns>
        public bool ExistePorId(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.Exist(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener probabildiades de modelos predictivos
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>3
        /// <returns> Obtiene Probabilidad según un modelo predictivo : ProbabilidadOportunidadResumenDTO </returns>
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivo(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerProbabilidadModeloPredictivo(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivoMarketing(int idOportunidad, int tipo)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerProbabilidadModeloPredictivoMarketing(idOportunidad, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtenerProbabilidadTodosProgramasPorAlumno(int idAlumno)
        {
            try
            {
                _unitOfWork.OportunidadRepository.ObtenerProbabilidadTodosProgramasPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Jonathan Caipo
        /// Fecha: 09/03/2023
        /// Version: 1.0
        /// <summary>
        /// Llena los datos necesarios de la oportunidadNuevaEntidad y generar sus hijos
        /// </summary>
        /// <param name="oportunidadNuevaEntidad">Entidad oportunidadNuevaEntidad</param>
        private void LlenarOportunidadHijos(ref OportunidadBoDTO oportunidad)
        {

            oportunidad.IdPagina = 1;
            if (oportunidad.IdOrigen == 0)
                throw new Exception("La Oportunidad no tiene Origen.");

            var CategoriaOrigen = _unitOfWork.OrigenRepository.ObtenerIdCategoriaOrigenPorOrigen(oportunidad.IdOrigen.Value);

            if (CategoriaOrigen.IdCategoriaOrigen == 0)
                throw new Exception("No se encontro origen no se puede crear categoria.");

            oportunidad.IdCategoriaOrigen = oportunidad.IdCategoriaOrigen.GetValueOrDefault() != 0 ? oportunidad.IdCategoriaOrigen : CategoriaOrigen.IdCategoriaOrigen;
            //VALIDACIÓN SI PARA CATEGORIA DATO NULL
            if (oportunidad.IdSubCategoriaDato == null || oportunidad.IdSubCategoriaDato == 0)
            {
                if (oportunidad != null && oportunidad.IdTipoInteraccion != null)
                {
                    oportunidad.IdTipoInteraccion = oportunidad.IdTipoInteraccion == 0 ? 18 : oportunidad.IdTipoInteraccion;
                    var categoriadato = _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigenSubCategoriaDato(oportunidad.IdCategoriaOrigen.Value, oportunidad.IdTipoInteraccion.Value);
                    if (categoriadato == null || categoriadato.IdSubCategoriaDato == 0)
                        oportunidad.IdSubCategoriaDato = 2599;
                    else
                        oportunidad.IdSubCategoriaDato = categoriadato.IdSubCategoriaDato != 0 ? categoriadato.IdSubCategoriaDato : 0;
                }
                else
                {
                    oportunidad.IdSubCategoriaDato = 2599;
                }
            }

            if (!string.IsNullOrEmpty(oportunidad.CodMailing))
            {
                var campaniaMailing = _unitOfWork.CampaniaMailingDetalleRepository.ObtenerIdCampaniaMailing(oportunidad.CodMailing);
                if (campaniaMailing != null)
                {
                    oportunidad.IdConjuntoAnuncio = campaniaMailing.Valor;
                }
            }
            oportunidad.IdFaseOportunidadInicial = oportunidad.IdFaseOportunidad;
            oportunidad.IdFaseOportunidadMaxima = oportunidad.IdFaseOportunidad;
            oportunidad.IdEstadoOportunidad = oportunidad.UltimaFechaProgramada == null || oportunidad.UltimaFechaProgramada.Equals("              ") || oportunidad.UltimaFechaProgramada.Equals("00000000000000") ? 2 : 6;

            OportunidadLog oportunidadLog = new OportunidadLog()
            {
                IdPersonalAsignado = oportunidad.IdPersonalAsignado,
                IdAsesorAnt = oportunidad.IdPersonalAsignado,
                IdContacto = oportunidad.IdAlumno,
                IdFaseOportunidad = oportunidad.IdFaseOportunidad,
                IdFaseOportunidadAnt = oportunidad.IdFaseOportunidad,
                IdOportunidad = oportunidad.Id,
                IdCentroCosto = oportunidad.IdCentroCosto,
                IdCentroCostoAnt = oportunidad.IdCentroCosto,
                IdOrigen = oportunidad.IdOrigen,
                IdTipoDato = oportunidad.IdTipoDato,
                FechaLog = DateTime.Now,
                FechaFinLog = DateTime.Now,
                FechaCambioFase = DateTime.Now,
                FechaCambioFaseAnt = DateTime.Now,
                CambioFase = true,
                CambioFaseIs = false,
                Comentario = oportunidad.UltimoComentario,
                IdConjuntoAnuncio = oportunidad.IdConjuntoAnuncio,
                FechaRegistroCampania = oportunidad.FechaRegistroCampania,
                CicloRn2 = 1,
                CambioFaseAsesor = 1,
                FechaCambioAsesor = DateTime.Now,
                FechaCambioAsesorAnt = DateTime.Now,
                IdCategoriaOrigen = oportunidad.IdCategoriaOrigen,
                IdSubCategoriaDato = oportunidad.IdSubCategoriaDato,
                UsuarioCreacion = oportunidad.UsuarioCreacion,
                UsuarioModificacion = oportunidad.UsuarioCreacion,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
                IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                IdPersonalAreaTrabajo = oportunidad.IdPersonalAreaTrabajo

            };
            oportunidad.OportunidadLogAntigua = oportunidadLog;

            ActividadDetalle actividadDetalle = new ActividadDetalle()
            {
                FechaProgramada = oportunidad.UltimaFechaProgramada,
                Actor = "A",
                Comentario = "Sin Comentario",
                IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(oportunidad.IdFaseOportunidad, oportunidad.IdTipoDato, oportunidad.IdPersonalAreaTrabajo, oportunidad.IdActividadCabeceraUltima),
                IdAlumno = oportunidad.IdAlumno,
                IdEstadoActividadDetalle = 1,   //ValorEstatico.IdEstadoActividadDetalleNoEjecutado,
                IdOportunidad = 1,//oportunidad.Id
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioCreacion = oportunidad.UsuarioCreacion,
                UsuarioModificacion = oportunidad.UsuarioCreacion,
                Estado = true,
                IdClasificacionPersona = oportunidad.IdClasificacionPersona
            };
            if (oportunidad.IdCategoriaOrigen == 360)   //ValorEstatico.IdCategoriaOrigenFacebookPreLanC2FormularioPropio)//Facebook PreLan C2 Formulario Propio 360
            {
                actividadDetalle.IdActividadCabecera = 27;  // ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProgPrelan;//15;
            }

            var resAt = _unitOfWork.ActividadDetalleRepository.Add(actividadDetalle);
            _unitOfWork.Commit();
            actividadDetalle = _mapper.Map<ActividadDetalle>(resAt);

            oportunidad.ActividadNueva = actividadDetalle;
            oportunidad.ActividadAntigua = null;

            oportunidad.UltimoComentario = oportunidad.UltimoComentario == null ? actividadDetalle.Comentario : oportunidad.UltimoComentario;
            oportunidad.IdActividadCabeceraUltima = actividadDetalle.IdActividadCabecera == null ? 0 : actividadDetalle.IdActividadCabecera.Value;
            oportunidad.IdActividadDetalleUltima = actividadDetalle.Id;
            //int divisor = 0;
            //var resultado = 1 / divisor;
            oportunidad.UltimaFechaProgramada = actividadDetalle.FechaProgramada;
            oportunidad.IdEstadoActividadDetalleUltimoEstado = actividadDetalle.IdEstadoActividadDetalle;
            //Registramos la asignacion de oportunidad
            AsignacionOportunidad asignacionOportunidad = null;
            if (_unitOfWork.AsignacionOportunidadRepository.Exist(oportunidad.Id))
            {
                asignacionOportunidad = _unitOfWork.AsignacionOportunidadRepository.ObtenerPorIdOportunidad(oportunidad.Id);
            }
            if (asignacionOportunidad == null)
            {
                asignacionOportunidad = new AsignacionOportunidad
                {
                    FechaAsignacion = DateTime.Now,
                    IdAlumno = oportunidad.IdAlumno,
                    IdPersonal = oportunidad.IdPersonalAsignado,
                    //IdCentroCosto = oportunidad.IdCentroCosto.Value,
                    IdCentroCosto = oportunidad.IdCentroCosto is null ? default : oportunidad.IdCentroCosto.Value,
                    IdOportunidad = oportunidad.Id,
                    IdTipoDato = oportunidad.IdTipoDato,
                    IdFaseOportunidad = oportunidad.IdFaseOportunidad,
                    UsuarioCreacion = oportunidad.UsuarioCreacion,
                    UsuarioModificacion = oportunidad.UsuarioCreacion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                    IdClasificacionPersona = oportunidad.IdClasificacionPersona,
                };
            }
            else
            {
                asignacionOportunidad.IdPersonal = oportunidad.IdPersonalAsignado == 0 ? asignacionOportunidad.IdPersonal : oportunidad.IdPersonalAsignado;
                asignacionOportunidad.IdCentroCosto = oportunidad.IdCentroCosto == 0 ? asignacionOportunidad.IdCentroCosto : oportunidad.IdCentroCosto.Value;
                asignacionOportunidad.IdAlumno = oportunidad.IdAlumno == 0 ? asignacionOportunidad.IdAlumno : oportunidad.IdAlumno;
                asignacionOportunidad.FechaAsignacion = DateTime.Now;
                asignacionOportunidad.FechaModificacion = DateTime.Now;
                asignacionOportunidad.UsuarioModificacion = oportunidad.UsuarioCreacion;
                asignacionOportunidad.UsuarioCreacion = oportunidad.UsuarioCreacion;
                asignacionOportunidad.Estado = true;
                asignacionOportunidad.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
            }

            AsignacionOportunidadLog asignacionOportunidadLog = new AsignacionOportunidadLog
            {
                FechaLog = DateTime.Now,
                IdPersonalAnterior = asignacionOportunidad.IdPersonal,
                IdAsignacionOportunidad = asignacionOportunidad.Id,
                IdCentroCostoAnt = asignacionOportunidad.IdCentroCosto,
                IdOportunidad = asignacionOportunidad.Id,
                IdTipoDato = asignacionOportunidad.IdTipoDato,
                IdFaseOportunidad = asignacionOportunidad.IdFaseOportunidad,
                IdAlumno = asignacionOportunidad.IdAlumno,
                IdPersonal = asignacionOportunidad.IdPersonal,
                IdCentroCosto = asignacionOportunidad.IdCentroCosto,
                UsuarioCreacion = oportunidad.UsuarioCreacion,
                UsuarioModificacion = oportunidad.UsuarioCreacion,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = true,
                IdClasificacionPersona = oportunidad.IdClasificacionPersona
            };
            asignacionOportunidad.AsignacionOportunidadLogs = new List<AsignacionOportunidadLog>();
            asignacionOportunidad.AsignacionOportunidadLogs.Add(asignacionOportunidadLog);

            oportunidad.AsignacionOportunidad = asignacionOportunidad;
        }
        public void CrearOportunidad(ref OportunidadBoDTO oportunidad, bool flagVentaCruzada, TipoPersona tipoPersona)
        {
            // Llenamos valores oportunidad/hijos
            var modeloDataMiningService = new ModeloDataMiningService(_unitOfWork);
            var asignacionOportunidadService = new AsignacionOportunidadService(_unitOfWork);
            this.LlenarOportunidadHijos(ref oportunidad);
            if (oportunidad != null)
            {
                var actividadNuevaTemp = oportunidad.ActividadNueva;
                oportunidad.ActividadNueva = null;
                var asignacionOporturtunidadTemp = oportunidad.AsignacionOportunidad;
                asignacionOporturtunidadTemp.IdClasificacionPersona = oportunidad.IdClasificacionPersona;

                asignacionOporturtunidadTemp.AsignacionOportunidadLogs = oportunidad.AsignacionOportunidad.AsignacionOportunidadLogs;
                asignacionOporturtunidadTemp.AsignacionOportunidadLogs.FirstOrDefault().IdClasificacionPersona = oportunidad.IdClasificacionPersona;

                oportunidad.AsignacionOportunidad = null;
                oportunidad = _mapper.Map<OportunidadBoDTO>(Add(MapeoBoDTO(oportunidad)));

                var idOportunidad = oportunidad.Id;

                asignacionOporturtunidadTemp.IdOportunidad = idOportunidad;
                asignacionOporturtunidadTemp.AsignacionOportunidadLogs.FirstOrDefault().IdOportunidad = idOportunidad;

                if (asignacionOporturtunidadTemp.Id != 0 && asignacionOporturtunidadTemp.Id != null)
                {
                    asignacionOportunidadService.Update(asignacionOporturtunidadTemp);
                    _unitOfWork.Commit();
                }
                else
                {
                    asignacionOportunidadService.Add(asignacionOporturtunidadTemp);
                    _unitOfWork.Commit();
                }

                bool flagValidaActividadDetalle = false;
                int nroIntentos = 0;

                while (!flagValidaActividadDetalle && nroIntentos < 5)
                {
                    try
                    {
                        _unitOfWork.DetachAll();
                        var actividadDetalle = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(actividadNuevaTemp.Id);
                        actividadDetalle.IdOportunidad = oportunidad.Id;
                        actividadDetalle.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
                        _unitOfWork.DetachAll();
                        _unitOfWork.ActividadDetalleRepository.Update(actividadDetalle);
                        _unitOfWork.Commit();
                        //_unitOfWork.ActividadDetalleRepository.ActualizarOportunidadYClasificacionPersona(oportunidad.Id, oportunidad.IdClasificacionPersona.Value, actividadNuevaTemp.Id);

                        flagValidaActividadDetalle = true;
                    }
                    catch (Exception ex)
                    {
                        nroIntentos++;

                        if (nroIntentos == 4)
                        {
                            _unitOfWork.DetachAll();
                            _unitOfWork.LogRepository.Add(new Log { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarActividadDetalle", Parametros = $"idActividadDetalle={actividadNuevaTemp.Id}&idOportunidad={oportunidad.Id}&idClasificacionPersona={oportunidad.IdClasificacionPersona}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "UPDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                            _unitOfWork.Commit();
                            throw new Exception(ex.Message);
                        }
                        Thread.Sleep(2000);
                    }
                }
                var cantidad = _unitOfWork.ModeloDataMiningRepository.ObtenerListaPorOportunidad(oportunidad.Id);
                ModeloDataMining modeloDataMining;

                if (tipoPersona == TipoPersona.Alumno)
                {
                    if (cantidad != null && cantidad.Count > 1)
                    {
                        modeloDataMining = modeloDataMiningService.ObtenerPorId(cantidad.FirstOrDefault().Id);
                        modeloDataMiningService.ObtenerProbabilidad(oportunidad.Id, ref modeloDataMining);

                        if (modeloDataMining.ProbabilidadInicial == null)
                        {
                            new Exception("No se pudo crear Probabilidad Inicial con OportunidadId: " + oportunidad.Id);
                            modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                        }
                        modeloDataMining.IdPersonal = oportunidad.IdPersonalAsignado;
                        modeloDataMining.IdCentroCosto = oportunidad.IdCentroCosto;
                        modeloDataMining.IdTipoDato = oportunidad.IdTipoDato;
                        modeloDataMining.IdAlumno = oportunidad.IdAlumno;
                        modeloDataMining.UsuarioModificacion = oportunidad.UsuarioModificacion;
                        modeloDataMining.FechaModificacion = DateTime.Now;
                        modeloDataMining.Estado = true;
                        modeloDataMiningService.Update(modeloDataMining);
                    }
                    else
                    {
                        modeloDataMining = new ModeloDataMining();
                        modeloDataMining.IdOportunidad = oportunidad.Id;
                        modeloDataMiningService.ObtenerProbabilidad(oportunidad.Id, ref modeloDataMining);

                        if (modeloDataMining.ProbabilidadInicial == null)
                        {
                            new Exception("No se pudo crear Probabilidad Inicial con OportunidadId: " + oportunidad.Id);
                            modeloDataMining.IdProbabilidadRegistroPwInicial = 1;
                        }
                        modeloDataMining.IdPersonal = oportunidad.IdPersonalAsignado;
                        modeloDataMining.IdCentroCosto = oportunidad.IdCentroCosto;
                        modeloDataMining.IdTipoDato = oportunidad.IdTipoDato;
                        modeloDataMining.IdAlumno = oportunidad.IdAlumno;
                        modeloDataMining.UsuarioCreacion = oportunidad.UsuarioCreacion;
                        modeloDataMining.UsuarioModificacion = oportunidad.UsuarioModificacion;
                        modeloDataMining.FechaCreacion = DateTime.Now;
                        modeloDataMining.FechaModificacion = DateTime.Now;
                        modeloDataMining.FechaCreacionContacto = DateTime.Now;
                        modeloDataMining.FechaCreacionOportunidad = oportunidad.FechaCreacion;
                        modeloDataMining.Estado = true;
                        modeloDataMiningService.Add(modeloDataMining);
                    }
                    decimal? puntoCorte = null;
                    //modeloDataMining.PuntoCorte

                    oportunidad.ValorProbabilidad = modeloDataMining.ProbabilidadActual < puntoCorte ? -1 : Convert.ToDecimal(modeloDataMining.IdProbabilidadRegistroPwActual);

                }
            }
            //validamos venta cruzada
            OportunidadVentaCruzada(ref oportunidad, flagVentaCruzada, tipoPersona);
        }

        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Hace la logica de validaciones por venta cruzada, reasignar las oportunidadNuevaEntidades medias y altas    
        /// </summary>
        /// <param name="oportunidadNuevaEntidad"> Confirmación para mantener el estado de oportunidadNuevaEntidad </param>
        /// <param name="flagVentaCruzada"> Datos de Oportunidad </param>
        private void OportunidadVentaCruzada(ref OportunidadBoDTO oportunidad, bool flagVentaCruzada, Enums.TipoPersona tipoPersona)
        {
            if (tipoPersona == TipoPersona.Alumno)
            {
                var idAsesorVentaCruzada = ObtenerAsesorVentaCruzada(oportunidad.IdAlumno.Value);
                // if (oportunidad.IdPersonalAsignado == ValorEstatico.IdPersonalAsignacionAutomatica || oportunidad.IdPersonalAsignado == idAsesorVentaCruzada || flagVentaCruzada == true)
                if (oportunidad.IdPersonalAsignado == 125 || oportunidad.IdPersonalAsignado == idAsesorVentaCruzada || flagVentaCruzada == true)
                {
                    if (oportunidad.ModeloDataMining != null && (_unitOfWork.OportunidadRepository.ValidarProbabilidadVentaCruzada(oportunidad.ModeloDataMining.IdProbabilidadRegistroPwActual)))
                    {
                        try
                        {
                            //si encontramos almenos un programa en lanzamiento por venta cruzada, reasignamos la oportunidad a un asesor que tenga meta en ese programa
                            if (idAsesorVentaCruzada != 0 && idAsesorVentaCruzada != -1)
                            {
                                //NO ENVIA CORREO PORQUE NO HAY OTRAS CON CUAL COMPARAR
                                this.ActualizarOportunidadVentaCruzada(ref oportunidad, idAsesorVentaCruzada, "UsuarioVentaCruzada", false, false);//NO ENVIA CORREO PORQUE NO HAY OTRAS CONCUAL COMPARAR
                                MapeoBoDTO(oportunidad);
                                // Update(mapeoEntidad);
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Error al reasignar la oportunidad por por venta cruzada " + e.Message);
                        }
                    }
                }
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualizar la oportunidadNuevaEntidad de la venta cruzada
        /// </summary>
        /// <param name="oportunidadNuevaEntidad"> entidad oportunidadNuevaEntidad </param>
        /// <param name="idAsesorReasignacion"> id del asesor de reasignacion </param>
        /// <param name="usuario"> usuario </param>
        /// <param name="enviaCorreo"> true o false </param>
        /// <param name="permaneceEstado"> true o false </param>
        /// <returns> Vacio </returns>
        private void ActualizarOportunidadVentaCruzada(ref OportunidadBoDTO oportunidadNuevaEntidad, int idAsesorReasignacion, string usuario, bool enviaCorreo, bool permaneceEstado)
        {

            var servicioPersonal = new PersonalService(_unitOfWork);
            try
            {
                //obtenemos los datos de la oportunidadNuevaEntidad
                var datosOportunidadReasignacion = _unitOfWork.OportunidadRepository.ObtenerDatosOportunidadReasignacion(oportunidadNuevaEntidad.Id);
                PersonalMinReasignacionDTO asesorAntiguo = new PersonalMinReasignacionDTO
                {
                    IdAsesor = datosOportunidadReasignacion.IdAsesor,
                    EmailAsesor = datosOportunidadReasignacion.EmailAsesor,
                    EmailJefe = datosOportunidadReasignacion.EmailAsesor,
                    IdJefe = datosOportunidadReasignacion.IdJefe,
                    NombreCompletoAsesor = datosOportunidadReasignacion.NombreCompletoAsesor,
                    NombreCompletoJefe = datosOportunidadReasignacion.NombreCompletoJefe
                };
                FaseOportunidadReasignacionDTO faseOportunidad = new FaseOportunidadReasignacionDTO()
                {
                    Codigo = datosOportunidadReasignacion.CodigoFaseOportunidad
                };
                var datosAsesorNuevo = servicioPersonal.ObtenerPersonalReasignacion(idAsesorReasignacion);
                AlumnoReasignacionDTO alumnoReasignacion = new AlumnoReasignacionDTO()
                {
                    Nombre1 = datosOportunidadReasignacion.Nombre1,
                    Nombre2 = datosOportunidadReasignacion.Nombre1,
                    ApellidoPaterno = datosOportunidadReasignacion.ApellidoPaterno,
                    ApellidoMaterno = datosOportunidadReasignacion.ApellidoMaterno
                };
                //fin obtenemos los datos de la oportunidadNuevaEntidad 200418
                using (TransactionScope scope = new TransactionScope())
                {
                    //actualizamos la oportunidadNuevaEntidad
                    this.ActualizarOportunidadVentaCruzadaAsesor(ref oportunidadNuevaEntidad, idAsesorReasignacion, permaneceEstado, usuario);
                    //Actualizamos en nuevo log
                    oportunidadNuevaEntidad.OportunidadLogNueva.IdPersonalAsignado = idAsesorReasignacion;
                    oportunidadNuevaEntidad.OportunidadLogNueva.FechaLog = DateTime.Now;
                    MapeoBoDTO(oportunidadNuevaEntidad);//actualizamos el log      
                    //Registramos la asignacion con los nuevos datos
                    ActualizarAsignacionOportunidad(oportunidadNuevaEntidad.Id, idAsesorReasignacion, oportunidadNuevaEntidad.IdCentroCosto.Value, oportunidadNuevaEntidad.IdAlumno.Value, usuario);
                    //actualizamos la asignacion 
                    scope.Complete();
                }
                try
                {
                    //enviamos correo de la reasignacion 200418
                    if (enviaCorreo == true)
                    {
                        EnvioCorreoReasignacion(oportunidadNuevaEntidad, asesorAntiguo, datosAsesorNuevo, alumnoReasignacion, faseOportunidad);
                    }
                }
                catch (Exception)
                {
                    //si no se envio el correopar que igual se crre la oportunidadNuevaEntidad
                }
                //fin enviamos correo de la reasignacion 200418
            }
            catch (Exception e)
            {
                throw new Exception("Me cai al actualizar el asesor por venta cruzada con envio correo", e);
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Envia un correo, cuando se reasigna la oportunidadNuevaEntidad
        /// </summary>
        /// <param name="Oportunidad">Objeto de clase OportunidadBO</param>
        /// <param name="PersonalAntiguo">Objeto de clase PersonalMinReasignacionDTO</param>
        /// <param name="PersonalNuevo">Objeto de clase PersonalMinReasignacionDTO</param>
        /// <param name="Alumno">Objeto de clase AlumnoReasignacionDTO</param>
        /// <param name="FaseOportunidad">Objeto de clase FaseOportunidadReasignacionDTO</param>
        private void EnvioCorreoReasignacion(OportunidadBoDTO Oportunidad, PersonalMinReasignacionDTO PersonalAntiguo, PersonalMinReasignacionDTO PersonalNuevo, AlumnoReasignacionDTO Alumno, FaseOportunidadReasignacionDTO FaseOportunidad)
        {
            string cuerpoMensaje, asuntoMensaje;
            try
            {
                cuerpoMensaje = "<p>" +
                    "Estimado " + PersonalNuevo.NombreCompletoAsesor + "<br/><br/>" +
                    "Se te ha reasignado el contacto " + (Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno) + " en fase <b>" + FaseOportunidad.Codigo + "</b> que estaba asignado al asesor " + PersonalAntiguo.NombreCompletoAsesor + "<br/>" +
                    "Verifica las llamadas y la informacion previa registradas en el sistema para que puedas continuar de manera adecuada el proceso de venta.<br/><br/>" +
                    "Saludos <br/>" +
                    "Integra Reasignacion" +
                    "</p>";
                asuntoMensaje = "Reasignacion de Oportunidad " + (Alumno.Nombre1 + " " + Alumno.Nombre2 + " " + Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno).ToUpper(CultureInfo.InvariantCulture);
                var servicioMail = new TMK_MailService();
                var data = new TMKMailDataDTO
                {
                    Sender = "reasignacion@bsginstitute.com",
                    Recipient = PersonalNuevo.EmailAsesor,
                    Cc = PersonalNuevo.EmailJefe,
                    Subject = asuntoMensaje,
                    Message = cuerpoMensaje,
                    RemitenteC = "Integra - Reasignacion Automatica "
                };
                servicioMail.SetData(data);
                var resultado = servicioMail.VerifyData();
                servicioMail.SendMessageTask();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// Tipo de funcion: Interna
        /// <summary>
        /// Actualizar los campos de asignacion oportunidadNuevaEntidad 
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidadNuevaEntidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idAsesorReasignacion">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="IdCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="IdAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        public void ActualizarAsignacionOportunidad(int IdOportunidad, int idAsesorReasignacion, int IdCentroCosto, int IdAlumno, string Usuario)
        {
            var servicioAsignacionOportunidad = new AsignacionOportunidadService(_unitOfWork);
            var servicioAsignacionOportunidadLog = new AsignacionOportunidadLogService(_unitOfWork);
            var asignacionOportunidad = servicioAsignacionOportunidad.AsignacionPorIdOportunidad(IdOportunidad);

            if (asignacionOportunidad != null)
            {
                //Actualizamos la asignacion
                asignacionOportunidad.IdPersonal = idAsesorReasignacion;
                asignacionOportunidad.UsuarioModificacion = Usuario;
                servicioAsignacionOportunidad.Update(asignacionOportunidad);
            }
            var logNuevo = new AsignacionOportunidadLog
            {
                FechaLog = DateTime.Now,
                Id = 0,
                IdPersonalAnterior = asignacionOportunidad.IdPersonal,
                IdAsignacionOportunidad = asignacionOportunidad.Id,
                IdCentroCostoAnt = asignacionOportunidad.IdCentroCosto,
                IdOportunidad = asignacionOportunidad.IdOportunidad,
                IdTipoDato = asignacionOportunidad.IdTipoDato,
                IdFaseOportunidad = asignacionOportunidad.IdFaseOportunidad,
                IdAlumno = IdAlumno == 0 ? asignacionOportunidad.IdAlumno : IdAlumno,
                IdPersonal = idAsesorReasignacion == 0 ? asignacionOportunidad.IdPersonal : idAsesorReasignacion,
                IdCentroCosto = IdCentroCosto == 0 ? asignacionOportunidad.IdCentroCosto : IdCentroCosto,
                Estado = true,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                UsuarioCreacion = Usuario,
                UsuarioModificacion = Usuario,
                IdClasificacionPersona = asignacionOportunidad.IdClasificacionPersona
            };
            servicioAsignacionOportunidadLog.Add(logNuevo);
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el flag de venta cruzada y actualizar el asesor al que pertenece la oportunidadNuevaEntidad
        /// </summary>
        /// <param name="oportunidadEntidad">Referencia a entidad Oportunidad </param>
        /// <param name="idAsesorReasignacion">Id del asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="PermaneceEstado">Flag para validar si permanece el estado anterior</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        private void ActualizarOportunidadVentaCruzadaAsesor(ref OportunidadBoDTO oportunidadEntidad, int idAsesorReasignacion, bool PermaneceEstado, string Usuario)
        {
            if (PermaneceEstado == true)//viene de reasignacion OD
            {
                oportunidadEntidad.IdPersonalAsignado = idAsesorReasignacion == 0 ? oportunidadEntidad.IdPersonalAsignado : idAsesorReasignacion;
                oportunidadEntidad.FlagVentaCruzada = null; //marcamos esta oportunidadNuevaEntidad para poder darles seguimiento y mostrarlas en el reporte de metas
                oportunidadEntidad.FechaModificacion = DateTime.Now;
                oportunidadEntidad.UsuarioModificacion = Usuario;
            }
            else//logica normal
            {
                oportunidadEntidad.IdPersonalAsignado = idAsesorReasignacion == 0 ? oportunidadEntidad.IdPersonalAsignado : idAsesorReasignacion;
                oportunidadEntidad.IdEstadoOportunidad = 7;    // ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                oportunidadEntidad.FlagVentaCruzada = 1; //marcamos esta oportunidadNuevaEntidad para poder darles seguimiento y mostrarlas en el reporte de metas
                oportunidadEntidad.FechaModificacion = DateTime.Now;
                oportunidadEntidad.UsuarioModificacion = Usuario;
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el asesor de venta cruzada por el idAlumno
        /// </summary>
        /// <param name="idAlumno"> id del alumno </param>
        /// <returns> Vacio </returns>
        public int ObtenerAsesorVentaCruzada(int idAlumno)
        {
            try
            {
                var servicioAlumno = new AlumnoService(_unitOfWork);
                var servicioAsignacionOportunidad = new AsignacionOportunidadService(_unitOfWork);
                var servicioPEspecifico = new PEspecificoService(_unitOfWork);
                var fechaActual = DateTime.Now;
                int _idPEspecifico;
                string _modalidad;
                int _idAsesor;
                int? idCiudad;
                int idPais;
                int idPEspecifico = 0;
                int asesorActual = 0;//obtiene el valor de asesor
                int cantidadOportunidades = 0;
                int maxOportunidades = 0;

                var alumnoCiudadPais = servicioAlumno.ObtenerCiudadPaisPorIdAlumno(idAlumno);
                idCiudad = alumnoCiudadPais.IdCiudad;
                idPais = Convert.ToInt16(alumnoCiudadPais.IdCodigoPais);
                var ventaCruzada = _unitOfWork.OportunidadRepository.ObtenerCentroCostoProbable(idAlumno, fechaActual).OrderByDescending(x => x.Precio).ToList();
                foreach (var item in ventaCruzada)
                {
                    _idPEspecifico = item.IdPEspecifico;
                    _modalidad = item.Tipo;
                    _idAsesor = item.IdPersonal;
                    if (_modalidad == "Presencial")
                    {
                        if (idCiudad == _unitOfWork.PEspecificoRepository.ObtenerIdCiudad(_idPEspecifico).Valor)
                        {
                            asesorActual = _idAsesor;
                            cantidadOportunidades = servicioAsignacionOportunidad.ObtenerCantidadOportunidadesAsesor(asesorActual, fechaActual).Valor.Value;
                            maxOportunidades = servicioAsignacionOportunidad.ObtenerMaximaAsignacionAsesor(asesorActual).Valor.Value;
                            if (cantidadOportunidades <= maxOportunidades)
                            {
                                idPEspecifico = _idPEspecifico;
                                break;
                            }
                        }
                    }
                    else
                    {
                        asesorActual = _idAsesor;
                        cantidadOportunidades = servicioAsignacionOportunidad.ObtenerCantidadOportunidadesAsesor(asesorActual, fechaActual).Valor.Value;
                        maxOportunidades = servicioAsignacionOportunidad.ObtenerMaximaAsignacionAsesor(asesorActual).Valor.Value;
                        if (cantidadOportunidades <= maxOportunidades)
                        {
                            idPEspecifico = _idPEspecifico;
                            break;
                        }
                    }
                    if (idPEspecifico != 0)
                    {
                        break;
                    }
                }
                if (idPEspecifico != 0)
                {
                    return asesorActual;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void FinalizarActividad(bool mantenerestadooportunidad, OportunidadDTO datosOportunidad)
        {
            string flagError = "";
            try
            {
                flagError = "ObtenerUltimoOportunidadLog";
                _oportunidadBo.OportunidadLogAntigua = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog(_oportunidadBo.ActividadAntigua.IdOportunidad.Value);

                var fechaFinLLamada = DateTime.Now;

                if (_oportunidadBo.ActividadAntigua.IdOcurrencia == 0)
                    throw new ArgumentException("Debe seleccionar una ocurrencia");

                _oportunidadBo.ActividadAntigua.DuracionReal = 13;
                _oportunidadBo.ActividadAntigua.IdCentralLlamada = 3;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _unitOfWork.OcurrenciaRepository.ObtenerOcurrenciaPorActividad(_oportunidadBo.ActividadAntigua.IdOcurrencia.Value);

                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = 2;  // ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                }


                _oportunidadBo.ActividadAntigua.IdEstadoActividadDetalle = 2;    // ValorEstatico.IdEstadoActividadDetalleEjecutado;

                // Actualizar Actividad Detalle
                if (!_unitOfWork.ActividadDetalleRepository.Exist(_oportunidadBo.ActividadAntigua.Id)) throw new Exception("La oportunidad no tiene actividad detalle");

                _oportunidadBo.ActividadNueva = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_oportunidadBo.ActividadAntigua.Id);
                _oportunidadBo.ActividadNueva.FechaReal = DateTime.Now;
                _oportunidadBo.ActividadNueva.DuracionReal = _oportunidadBo.ActividadAntigua.DuracionReal;
                _oportunidadBo.ActividadNueva.IdEstadoActividadDetalle = _oportunidadBo.ActividadAntigua.IdEstadoActividadDetalle;
                _oportunidadBo.ActividadNueva.Comentario = _oportunidadBo.ActividadAntigua.Comentario;
                _oportunidadBo.ActividadNueva.IdOcurrencia = _oportunidadBo.ActividadAntigua.IdOcurrencia;
                _oportunidadBo.ActividadNueva.IdOcurrenciaActividad = _oportunidadBo.ActividadAntigua.IdOcurrenciaActividad;
                _oportunidadBo.ActividadNueva.IdCentralLlamada = _oportunidadBo.ActividadAntigua.IdCentralLlamada;
                _oportunidadBo.ActividadNueva.FechaModificacion = DateTime.Now;
                _oportunidadBo.ActividadNueva.UsuarioModificacion = _oportunidadBo.Usuario;
                _oportunidadBo.ActividadNueva.IdClasificacionPersona = _oportunidadBo.ActividadAntigua.IdClasificacionPersona;

                if (ocurrencia.IdFaseOportunidad != 31) // ValorEstatico.IdFaseOportunidadNulo)
                {
                    _oportunidadBo.IdFaseOportunidad = ocurrencia.IdFaseOportunidad;
                }

                _oportunidadBo.UltimoComentario = _oportunidadBo.ActividadAntigua.Comentario;
                _oportunidadBo.UltimaFechaProgramada = _oportunidadBo.ActividadAntigua.FechaProgramada == null ? DateTime.Now : _oportunidadBo.ActividadAntigua.FechaProgramada.Value;

                _oportunidadBo.IdEstadoActividadDetalleUltimoEstado = 2; // ValorEstatico.IdEstadoActividadDetalleEjecutado;
                _oportunidadBo.IdActividadDetalleUltima = _oportunidadBo.ActividadAntigua.Id;
                //    if (datosOportunidad.IdEstadoOportunidad != 0 && datosOportunidad.IdEstadoOportunidad == ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada && mantenerestadooportunidad)
                if (datosOportunidad.IdEstadoOportunidad != 0 && datosOportunidad.IdEstadoOportunidad == 7 && mantenerestadooportunidad)
                {
                    _oportunidadBo.IdEstadoOportunidad = 7;  // ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
                else
                {
                    _oportunidadBo.IdEstadoOportunidad = 1;  // ValorEstatico.IdEstadoOportunidadEjecutada;
                }

                _oportunidadBo.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                if (_oportunidadBo.IdFaseOportunidad != 0 && datosOportunidad.IdFaseOportunidad != _oportunidadBo.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    _oportunidadBo.IdFaseOportunidadMaxima = _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria(datosOportunidad.IdFaseOportunidad.Value, _oportunidadBo.IdFaseOportunidad);
                }
                _oportunidadBo.FechaModificacion = DateTime.Now;
                _oportunidadBo.UsuarioModificacion = _oportunidadBo.Usuario;

                //Guardar Log
                _oportunidadBo.OportunidadLogNueva = new OportunidadLog();
                _oportunidadBo.OportunidadLogNueva.IdClasificacionPersona = _oportunidadBo.OportunidadLogAntigua.IdClasificacionPersona;
                _oportunidadBo.OportunidadLogNueva.IdPersonalAreaTrabajo = _oportunidadBo.OportunidadLogAntigua.IdPersonalAreaTrabajo;
                _oportunidadBo.OportunidadLogNueva.IdCentroCosto = _oportunidadBo.IdCentroCosto;
                _oportunidadBo.OportunidadLogNueva.IdPersonalAsignado = _oportunidadBo.IdPersonalAsignado;
                _oportunidadBo.OportunidadLogNueva.IdTipoDato = _oportunidadBo.IdTipoDato;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad = _oportunidadBo.IdFaseOportunidad;
                _oportunidadBo.OportunidadLogNueva.IdOrigen = _oportunidadBo.IdOrigen;
                _oportunidadBo.OportunidadLogNueva.IdContacto = _oportunidadBo.IdAlumno;
                _oportunidadBo.OportunidadLogNueva.FechaFinLog = _oportunidadBo.OportunidadLogAntigua.FechaLog;
                _oportunidadBo.OportunidadLogNueva.IdCentroCostoAnt = _oportunidadBo.OportunidadLogAntigua.IdCentroCosto;
                _oportunidadBo.OportunidadLogNueva.IdAsesorAnt = _oportunidadBo.OportunidadLogAntigua.IdPersonalAsignado;
                _oportunidadBo.OportunidadLogNueva.FechaLog = DateTime.Now;
                _oportunidadBo.OportunidadLogNueva.IdActividadDetalle = _oportunidadBo.ActividadAntigua.Id;
                _oportunidadBo.OportunidadLogNueva.IdOcurrencia = _oportunidadBo.ActividadAntigua.IdOcurrencia;
                _oportunidadBo.OportunidadLogNueva.IdOcurrenciaActividad = _oportunidadBo.ActividadAntigua.IdOcurrenciaActividad;
                _oportunidadBo.OportunidadLogNueva.Comentario = _oportunidadBo.UltimoComentario;
                _oportunidadBo.OportunidadLogNueva.IdOportunidad = _oportunidadBo.Id;
                _oportunidadBo.OportunidadLogNueva.IdCategoriaOrigen = _oportunidadBo.IdCategoriaOrigen;
                _oportunidadBo.OportunidadLogNueva.IdSubCategoriaDato = _oportunidadBo.IdSubCategoriaDato;
                _oportunidadBo.OportunidadLogNueva.FechaRegistroCampania = _oportunidadBo.FechaRegistroCampania;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                _oportunidadBo.OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                _oportunidadBo.OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                _oportunidadBo.OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                _oportunidadBo.OportunidadLogNueva.FasesActivas = _oportunidadBo.OportunidadLogAntigua.FasesActivas;
                _oportunidadBo.OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                _oportunidadBo.OportunidadLogNueva.IdClasificacionPersona = _oportunidadBo.IdClasificacionPersona;
                _oportunidadBo.OportunidadLogNueva.IdPersonalAreaTrabajo = _oportunidadBo.IdPersonalAreaTrabajo;

                if (_oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt != _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad)
                {
                    _oportunidadBo.OportunidadLogNueva.CambioFase = true;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioFase = _oportunidadBo.OportunidadLogNueva.FechaLog;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioFaseAnt = _oportunidadBo.OportunidadLogAntigua.FechaCambioFase;
                    _oportunidadBo.OportunidadLogNueva.CambioFaseAsesor = 1;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioAsesor = _oportunidadBo.OportunidadLogNueva.FechaLog;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioAsesorAnt = _oportunidadBo.OportunidadLogAntigua.FechaCambioAsesor;

                    if (_oportunidadBo.OportunidadLogNueva.IdFaseOportunidad != 0 && _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad == 5)   //ValorEstatico.IdFaseOportunidadIS)
                    {
                        _oportunidadBo.OportunidadLogNueva.FechaCambioFaseIs = _oportunidadBo.OportunidadLogNueva.FechaLog;
                        _oportunidadBo.OportunidadLogNueva.CambioFaseIs = true;
                    }
                    else
                    {
                        _oportunidadBo.OportunidadLogNueva.FechaCambioFaseIs = _oportunidadBo.OportunidadLogNueva.FechaCambioFaseIs;
                        _oportunidadBo.OportunidadLogNueva.CambioFaseIs = false;
                    }
                    //    if (oportunidadBo.OportunidadLogNueva.IdFaseOportunidad != 0 && oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt != 0 && oportunidadBo.OportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2 && oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2)
                    if (_oportunidadBo.OportunidadLogNueva.IdFaseOportunidad != 0 && _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt != 0 && _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad == 10 && _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt == 10)
                    {
                        _oportunidadBo.OportunidadLogNueva.CicloRn2 = _oportunidadBo.OportunidadLogAntigua.CicloRn2 + 1;
                    }
                    else
                    {
                        _oportunidadBo.OportunidadLogNueva.CicloRn2 = _oportunidadBo.OportunidadLogAntigua.CicloRn2;
                    }
                }
                else
                {
                    _oportunidadBo.OportunidadLogNueva.CambioFase = false;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioFase = _oportunidadBo.OportunidadLogAntigua.FechaCambioFase;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioFaseAnt = _oportunidadBo.OportunidadLogAntigua.FechaCambioFase;//ultima 1***
                    _oportunidadBo.OportunidadLogNueva.FechaCambioFaseIs = _oportunidadBo.OportunidadLogAntigua.FechaCambioFaseIs;
                    _oportunidadBo.OportunidadLogNueva.CambioFaseIs = false;
                    _oportunidadBo.OportunidadLogNueva.CambioFaseAsesor = 0;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioAsesor = _oportunidadBo.OportunidadLogAntigua.FechaCambioAsesor;
                    _oportunidadBo.OportunidadLogNueva.FechaCambioAsesorAnt = _oportunidadBo.OportunidadLogAntigua.FechaCambioAsesor;
                    _oportunidadBo.OportunidadLogNueva.CicloRn2 = _oportunidadBo.OportunidadLogAntigua.CicloRn2;
                }
                _oportunidadBo.OportunidadLogNueva.FechaCreacion = DateTime.Now;
                _oportunidadBo.OportunidadLogNueva.FechaModificacion = DateTime.Now;
                _oportunidadBo.OportunidadLogNueva.UsuarioCreacion = _oportunidadBo.Usuario;
                _oportunidadBo.OportunidadLogNueva.UsuarioModificacion = _oportunidadBo.Usuario;
                _oportunidadBo.OportunidadLogNueva.Estado = true;

                if (_oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt != _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad)
                {
                    _oportunidadBo.PreCalculadaCambioFase = new PreCalculadaCambioFase();
                    _oportunidadBo.PreCalculadaCambioFase.IdPersonal = _oportunidadBo.OportunidadLogNueva.IdPersonalAsignado;
                    _oportunidadBo.PreCalculadaCambioFase.Fecha = DateTime.Now;
                    _oportunidadBo.PreCalculadaCambioFase.IdCategoriaOrigen = _oportunidadBo.OportunidadLogNueva.IdCategoriaOrigen;
                    _oportunidadBo.PreCalculadaCambioFase.IdCentroCosto = _oportunidadBo.OportunidadLogNueva.IdCentroCosto;
                    _oportunidadBo.PreCalculadaCambioFase.IdFaseOportunidadDestino = _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad;
                    _oportunidadBo.PreCalculadaCambioFase.IdFaseOportunidadOrigen = _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt;
                    _oportunidadBo.PreCalculadaCambioFase.IdOrigen = _oportunidadBo.IdOrigen;
                    _oportunidadBo.PreCalculadaCambioFase.IdTipoDato = _oportunidadBo.IdTipoDato;
                    _oportunidadBo.PreCalculadaCambioFase.FechaCreacion = DateTime.Now;
                    _oportunidadBo.PreCalculadaCambioFase.FechaModificacion = DateTime.Now;
                    _oportunidadBo.PreCalculadaCambioFase.UsuarioCreacion = _oportunidadBo.Usuario;
                    _oportunidadBo.PreCalculadaCambioFase.UsuarioModificacion = _oportunidadBo.Usuario;
                    _oportunidadBo.PreCalculadaCambioFase.Estado = true;


                    try
                    {
                        var repo = _unitOfWork.OportunidadRepository as dynamic;
                        using (var conn = (SqlConnection)repo._connectionFactory.GetConnection)
                        {
                            conn.Open();
                            using (var cmd = new SqlCommand("[mkt].[sp_RegistrarCambioFaseGoogleAds]", conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@IdOportunidad", _oportunidadBo.Id);
                                cmd.Parameters.AddWithValue("@IdFaseOportunidad_Anterior", _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@IdFaseOportunidad_Actual", _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad);
                                cmd.Parameters.AddWithValue("@FechaHoraConversion", DateTime.Now);
                                cmd.Parameters.AddWithValue("@Usuario", _oportunidadBo.Usuario);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        System.Diagnostics.Debug.WriteLine($"Google Ads Conversion: {ex.Message}");
                    }

                }
                if (_oportunidadBo.OportunidadCompetidor != null && _oportunidadBo.OportunidadCompetidor.Id != 0)
                {
                    flagError = "EliminarOportunidadCompetidorDetalle";
                    //_repCalidadProcesamiento.EliminarOportunidadCompetidorDetalle(OportunidadCompetidor.Id, usuario);
                    _oportunidadBo.OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    _oportunidadBo.OportunidadCompetidor.UsuarioModificacion = _oportunidadBo.Usuario;
                }
                _oportunidadBo.ActividadAntigua = null;
                _oportunidadBo.OportunidadLogAntigua = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + '-' + flagError);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 14/03/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Registro de cada Oportunidad por filtro
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paginador"></param>
        /// <returns></returns>
        public object ObtenerPorFiltroRegistrarOportunidadV2(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador, string usuario, string area, string tipoPersonal)
        {
            try
            {
                AlumnoService _alumnoService = new AlumnoService(_unitOfWork);
                var resultado = new ResultadoOportunidadesDTO();
                if (usuario == "AdminInst")
                {
                    resultado = _unitOfWork.OportunidadRepository.ObtenerPorFiltroRegistrarOportunidad(obj, paginador, 2);

                }
                else if (area == "VE" && (tipoPersonal == "Coordinador" || tipoPersonal == "Asesor") && usuario != "juancarlos")
                {
                    resultado = _unitOfWork.OportunidadRepository.ObtenerPorFiltroRegistrarOportunidadSinAA(obj, paginador, area, tipoPersonal);
                }
                else
                {
                    resultado = _unitOfWork.OportunidadRepository.ObtenerPorFiltroRegistrarOportunidad(obj, paginador, null);
                }

                foreach (var item in resultado.data)
                {

                    if (!string.IsNullOrWhiteSpace(item.Email1))
                        item.Email1 = _alumnoService.EncriptarCorreoHash(item.Email1);

                    if (!string.IsNullOrWhiteSpace(item.Celular))
                        item.Celular = _alumnoService.EncriptarNumeroHash(Regex.Replace(item.Celular, @"[^\d]", ""));

                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Registro de cada Oportunidad por filtro
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paginador"></param>
        /// <returns></returns>
        public object ObtenerPorFiltroRegistrarOportunidad(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerPorFiltroRegistrarOportunidad(obj, paginador, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Crear una oportunidad y persona
        /// </summary>
        /// <param name="oportunidad"> Datos de la oportunidad </param>
        /// <param name="flagVentaCruzada"> true o false </param>
        /// <param name="tipoPersona"> Tipo de persona </param>
        /// <returns> Nueva Entidad Oportunidad </returns>
        public void CrearOportunidadCrearPersona(ref OportunidadBoDTO oportunidad, bool flagVentaCruzada, Enums.TipoPersona tipoPersona)
        {
            var alumnoService = new AlumnoService(_unitOfWork);
            var personaService = new PersonaService(_unitOfWork);
            var expositorService = new ExpositorService(_unitOfWork);
            if (tipoPersona == TipoPersona.Alumno)
            {
                if (!_unitOfWork.AlumnoRepository.ExisteContacto(oportunidad.Alumno.Email1, oportunidad.Alumno.Email2))
                {
                    alumnoService.Alumno = oportunidad.Alumno;
                    alumnoService.ValidarEstadoContactoWhatsAppTemporal();
                    var empresaAlumno = alumnoService.Alumno.IdEmpresa;
                    alumnoService.Alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                    // alumnoService.Alumno = alumnoService.Add(alumnoService.Alumno);
                    var resAlumno = _unitOfWork.AlumnoRepository.Add(alumnoService.Alumno);
                    _unitOfWork.Commit();
                    alumnoService.Alumno = _mapper.Map<Alumno>(resAlumno);
                    oportunidad.Alumno = _mapper.Map<Alumno>(resAlumno);

                    int? creacionCorrecta = personaService.InsertarPersona(oportunidad.Alumno.Id, TipoPersona.Alumno, oportunidad.UsuarioCreacion);

                    if (creacionCorrecta == null)
                    {
                        var nombreTablaV4 = "mkt.T_Alumno";
                        var nombreTablaV3 = "talumnos" +
                            "";
                        var resultado = alumnoService.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, oportunidad.Alumno.Id, null, 0);
                        if (resultado == true)
                        {
                            throw new Exception("Se elimino el alumno");
                        }
                        else
                        {
                            throw new Exception("No se elimino alumno");
                        }
                    }
                    oportunidad.IdAlumno = oportunidad.Alumno.Id;
                    oportunidad.IdClasificacionPersona = creacionCorrecta;
                    oportunidad.IdPersonalAreaTrabajo = 8;
                }
                else
                {
                    throw new Exception("Alumno ya Existe!");
                }
            }
            else if (tipoPersona == TipoPersona.Docente)
            {
                var modelo = _unitOfWork.ExpositorRepository.Add(oportunidad.Expositor);
                _unitOfWork.Commit();
                oportunidad.Expositor = _mapper.Map<Expositor>(modelo);

                int? idClasificacionPersona = personaService.InsertarPersona(oportunidad.Expositor.Id, TipoPersona.Docente, oportunidad.UsuarioCreacion);
                if (idClasificacionPersona == null)
                {
                    var nombreTablaV3 = "texpositor";
                    var nombreTablaV4 = "pla.T_Expositor";
                    var resultado = alumnoService.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, oportunidad.Expositor.Id, null, 0);
                    if (resultado == true)
                    {
                        throw new Exception("Se elimino el expositor");
                    }
                    else
                    {
                        throw new Exception("No se elimino expositor");
                    }
                }

                oportunidad.IdClasificacionPersona = idClasificacionPersona;
            }
            CrearOportunidad(ref oportunidad, flagVentaCruzada, tipoPersona);
        }
        /// Autor:  Gilmer Quispe
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Envia correo de la nueva oportunidad al personal
        /// </summary>
        /// <param name="idOportunidad"> id de la Oportunidad </param>
        /// <returns> Nueva Entidad Oportunidad </returns>
        public void EnviarCorreoOportunidad(int idOportunidad)
        {
            try
            {
                var _repPersonal = new PersonalService(_unitOfWork);

                var oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad);
                var personal = _repPersonal.ObtenerPersonalPorId(oportunidad.IdPersonalAsignado.Value);
                //if (oportunidad.IdPersonal_Asignado != ValorEstatico.IdPersonalAsignacionAutomatica && oportunidad.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC && oportunidad.IdTipoDato == ValorEstatico.IdTipoDatoLanzamiento && personal.AreaAbrev == ValorEstatico.AreaTrabajoVentas)
                if (oportunidad.IdPersonalAsignado != 125 && oportunidad.IdFaseOportunidad == 2 && oportunidad.IdTipoDato == 8 && personal.AreaAbrev == "VE")
                {
                    bool enviarIdPersonalPorDefecto = false;
                    //string uri = $"https://integraV5-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdPlantillaInformacionCursoVentas}/{enviarIdPersonalPorDefecto}";
                    string uri = $"https://integrav5-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantilla/{oportunidad.Id}/{827}/{enviarIdPersonalPorDefecto}";

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uri);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/2/2023
        /// Versión: 1.0
        /// <summary>
        /// Valida los casos en los que puede convertirse en OD u OM la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        public void MetodoODyOM(int idOportunidad)
        {
            //Valores Generales
            OportunidadService oportunidadService = new OportunidadService(_unitOfWork);
            OportunidadBoDTO oportunidad = _mapper.Map<OportunidadBoDTO>(_unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad));
            //var oportunidad = oportunidadService.ObtenerPorId(idOportunidad);

            var asesorAsociado = new ResultadoDTO();
            //int probabilidadActual = 0;
            var diaDto = new BloqueHorarioProcesaOportunidad();

            try
            {
                _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnterior(idOportunidad);
            }
            catch (Exception ex)
            {
            }

            bool enviarNotificacion = true; //para que no se envie notificacion a la agenda si cerramos la oportunidad en OM, OD
                                            //Valores Precalculados

            int idCentroCosto = oportunidad.IdCentroCosto.Value;
            int idCategoriaOrigen = oportunidad.IdCategoriaOrigen.Value;

            var resultadoPEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(idCentroCosto);
            int idProgramaGeneral = resultadoPEspecifico.IdProgramaGeneral == null ? 0 : resultadoPEspecifico.IdProgramaGeneral.Value;

            var resultadoModeloDataMining = _unitOfWork.ModeloDataMiningRepository.ObtenerPorOportunidad(idOportunidad);
            int idProbRegisPW = resultadoModeloDataMining.IdProbabilidadRegistroPwActual == null ? 0 : resultadoModeloDataMining.IdProbabilidadRegistroPwActual.Value;

            string pesoDescripcion = _unitOfWork.ProbabilidadRegistroPwRepository.ObtenerPorId(idProbRegisPW).Nombre;
            //Peso de la Probabilidad de la Oportunidad
            int pesoOportunidad = pesoDescripcion == "Muy Alta" ? 2 : ((pesoDescripcion == "Alta" || pesoDescripcion == "Media") ? 1 : 0);
            //Peso de la Categoria de la Oportunidad
            int pesoCategoriaOportunidad = _unitOfWork.CategoriaOrigenRepository.ObtenerPorId(idCategoriaOrigen).Meta;

            var objetoAlumno = _unitOfWork.AlumnoRepository.ObtenerPorId(oportunidad.IdAlumno.Value);
            /*Caracter especial para evitar registros coincidentes con celular vacio*/
            objetoAlumno.Celular = string.IsNullOrEmpty(objetoAlumno.Celular) ? "-|!x!|-" : objetoAlumno.Celular.Trim();
            ///////////////////////////////////// CASO 1:Valida si hay oportunidades en IS o M //////////////////////////////////////////////////////
            var listaISM = _unitOfWork.OportunidadRepository.ValidarOportunidadesISM(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaISM.Count() > 0)//Si hay un IS o M me tengo que cerrar
            {
                int[] listaOportunidadesISM = new int[] { oportunidad.Id };
                this.CerrarOportunidadesOD(listaOportunidadesISM, "System Duplicado");//FALTA IMPLEMENTAR
            }
            //////////////////////////////////// CASO 2:Valida si hay opotunidades con mayor Probabilidad //////////////////////////////////////////
            var listaProbabilidades = _unitOfWork.OportunidadRepository.ValidarOportunidadesProbabilidad(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaProbabilidades.Count() > 0)//Si hay oportunidades con el mismo alumno y del mismo programa
            {
                if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)//Si alguno con mayor probabilidad que el actual me tengo que cerrar
                {
                    int[] listaOportunidadesProbabilidades = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOD(listaOportunidadesProbabilidades, "Sys Duplicado Prob");//FALTA IMPLEMENTAR
                }
                else if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad == pesoOportunidad)//Si tienen una probabilidad igual que el actual no se hace nada
                {
                    //nada
                }
                else//Significa son de menor probablidad que el actual y deben cerrarse
                {
                    int[] listaOportunidadesProbabilidades = new int[listaProbabilidades.Count()];
                    int contador = 0;
                    foreach (var item in listaProbabilidades)
                    {
                        listaOportunidadesProbabilidades[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOD(listaOportunidadesProbabilidades, "Sys Duplicado Prob");//FALTA IMPLEMENTAR
                }
            }

            var listaCategorias = _unitOfWork.OportunidadRepository.ValidarOportunidadesCategoria(oportunidad.IdAlumno.Value, oportunidad.Id, objetoAlumno.Celular);
            //OM-OD
            var listaCategoriasOM = listaCategorias.Where(w => w.IdProgramaGeneral != idProgramaGeneral).ToList();
            var listaCategoriasOD = listaCategorias.Where(w => w.IdProgramaGeneral == idProgramaGeneral).ToList();
            if (listaCategoriasOM.Count() > 0)
            {
                if (listaCategoriasOM.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                {
                    if (oportunidad.IdPersonalAsignado != 125)
                        enviarNotificacion = false;

                    int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOM(listaOportunidadesCategorias, "UsuarioOM");
                }
                else if (listaCategoriasOD.Count() > 0)
                {
                    if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                    {
                        if (oportunidad.IdPersonalAsignado != 125)
                            enviarNotificacion = false;

                        int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                    }
                    else
                    {
                        //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        Update(MapeoBoDTO(oportunidad));

                        //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                        var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                        if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                        {
                            this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                            oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                        }
                        //Mandamos la lista de oportunidades anteriores a Cerrar OD
                        int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasOD)
                        {
                            listaOportunidadesCategorias[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                    }
                }
            }
            else if (listaCategoriasOD.Count() > 0)
            {
                if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                {
                    if (oportunidad.IdPersonalAsignado != 125)
                        enviarNotificacion = false;

                    int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                }
                else
                {
                    //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                    oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    Update(MapeoBoDTO(oportunidad));

                    //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                    var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                    if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                    {

                        this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                        oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                    }
                    //Mandamos la lista de oportunidades anteriores a Cerrar OD
                    int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasOD)
                    {
                        listaOportunidadesCategorias[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                }
            }
            //////////////////////////////////// CASO 4:Valida si hay opotunidades con mayor Categoria en Fase IP ////////////////////////////////
            var listaCategoriasIPS = _unitOfWork.OportunidadRepository.ValidarOportunidadesCategoriaIPMismoPG(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaCategoriasIPS.Count() > 0)
            {
                if (listaCategoriasIPS.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                {
                    //primero me reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                    oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;
                    enviarNotificacion = false;
                    int[] listaOportunidadesCategoriasIP = new int[] { oportunidad.Id };
                    //segundo cerrarme
                    this.CerrarOportunidadesOD(listaOportunidadesCategoriasIP, "Sys duplicadoIP");//FALTA IMPLEMENTAR
                }
                else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                {
                    //primero me  reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                    oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;

                    //segundo cierro la otras IPS
                    int[] listaOportunidadesIPCerrar = new int[listaCategoriasIPS.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasIPS)
                    {
                        listaOportunidadesIPCerrar[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOD(listaOportunidadesIPCerrar, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                    //tercero me paso a IP
                    //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                    oportunidad.IdFaseOportunidad = 8;//IP
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    Update(MapeoBoDTO(oportunidad));
                }
            }
            //////////////////////////////////// CASO 5:Valida si hay opotunidades con mayor Categoria en Fase IP de Otros Programas///////////////
            var listaCategoriasIPSPGDiferente = _unitOfWork.OportunidadRepository.ValidarOportunidadesCategoriaIPDiferentePG(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaCategoriasIPSPGDiferente.Count() > 0)
            {
                if (listaCategoriasIPSPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                {
                    //primero me reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                    oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;
                    enviarNotificacion = false;
                    int[] listaOportunidadesCategoriasIPPGDiferente = new int[] { oportunidad.Id };
                    //segundo cerrarme
                    this.CerrarOportunidadesOD(listaOportunidadesCategoriasIPPGDiferente, "UsuarioOM");//FALTA IMPLEMENTAR

                }
                else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                {
                    //primero me  reasigno
                    this.ReasignarOportunidades(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                    oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;

                    //segundo cierro la otras IPS
                    int[] listaOportunidadesIPPGDiferenteCerrar = new int[listaCategoriasIPSPGDiferente.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasIPSPGDiferente)
                    {
                        listaOportunidadesIPPGDiferenteCerrar[contador] = item.IdOportunidad;
                        contador++;
                    }
                    this.CerrarOportunidadesOM(listaOportunidadesIPPGDiferenteCerrar, "UsuarioOM");

                    //tercero me paso a IP
                    //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                    oportunidad.IdFaseOportunidad = 8;//IP
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    Update(MapeoBoDTO(oportunidad));
                }
            }
            //////////////////////////////////// CASO 6:Valida si hay opotunidades con mayor Categoria en Fase RN,IT,BNC de Otros Programas///////////////
            var listaCategoriasBNCITRNPGDiferente = _unitOfWork.OportunidadRepository.ValidarOportunidadesCategoriaBNCITRNDiferentePG(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
            if (listaCategoriasBNCITRNPGDiferente.Count() > 0)
            {
                if (listaCategoriasBNCITRNPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria < pesoCategoriaOportunidad)
                {
                    //Cerras las oportunidades que llegan
                    int[] listaOportunidadesBNCITRNPGDiferenteCerrar = new int[listaCategoriasBNCITRNPGDiferente.Count()];
                    int contador = 0;
                    foreach (var item in listaCategoriasBNCITRNPGDiferente)
                    {
                        listaOportunidadesBNCITRNPGDiferenteCerrar[contador] = item.IdOportunidad;
                        contador++;
                    }
                    //Me actualizo a la fase de la oportunidad anterior
                    oportunidad.IdFaseOportunidad = listaCategoriasBNCITRNPGDiferente.FirstOrDefault().IdFaseOportunidad;
                    oportunidad.FechaModificacion = DateTime.Now;
                    oportunidad.UsuarioModificacion = "System";
                    Update(MapeoBoDTO(oportunidad));

                    var oportunidadAnterior = listaCategoriasBNCITRNPGDiferente.Where(w => w.IdPersonalAsignado != 125).FirstOrDefault();
                    if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado)
                    {
                        if (oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                        {
                            this.ReasignarOportunidades(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                            oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                        }
                    }
                    this.CerrarOportunidadesOM(listaOportunidadesBNCITRNPGDiferenteCerrar, "UsuarioOM");//FALTA IMPLEMENTAR
                }
            }

            #region Marcado validacion correcta
            try
            {
                _unitOfWork.OportunidadRepository.ActualizarValidacionOportunidad(oportunidad.Id, true);
            }
            catch (Exception ex)
            {
                _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarValidacionOportunidadODYM", Parametros = $"IdOportunidad={idOportunidad}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "VALIDATE ODYM", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                _unitOfWork.Commit();
            }
            #endregion
        }
        /// Tipo Función: interna
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// <summary>
        /// Reasigna las oportunidades enviadas como referencia
        /// </summary>
        /// <param name="Oportunidad">Referencia a objeto de clase OportunidadBO</param>
        /// <param name="IdNuevoAsesor">Id del nuevo asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="EnviaCorreo">Flag para determinar si se enviara un correo o no</param>
        /// <returns>Bool</returns>
        private bool ReasignarOportunidades(ref OportunidadBoDTO oportunidad, int IdNuevoAsesor, bool EnviaCorreo)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var nuevoLog = new OportunidadLog()
                    {
                        Id = 0,
                        IdPersonalAsignado = oportunidad.IdPersonalAsignado,
                        IdAsesorAnt = oportunidad.IdPersonalAsignado,
                        IdContacto = oportunidad.IdAlumno,
                        IdFaseOportunidad = oportunidad.IdFaseOportunidad,
                        IdFaseOportunidadAnt = oportunidad.IdFaseOportunidad,
                        IdOportunidad = oportunidad.Id,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdCentroCostoAnt = oportunidad.IdCentroCosto,
                        IdOrigen = oportunidad.IdOrigen,
                        IdTipoDato = oportunidad.IdTipoDato,
                        FechaLog = DateTime.Now,
                        FechaFinLog = DateTime.Now,
                        FechaCambioFase = DateTime.Now,
                        FechaCambioFaseAnt = DateTime.Now,
                        FechaCambioFaseIs = null,
                        CambioFase = true,
                        CambioFaseIs = false,
                        Comentario = oportunidad.UltimoComentario,
                        IdConjuntoAnuncio = oportunidad.IdConjuntoAnuncio,
                        FechaRegistroCampania = oportunidad.FechaRegistroCampania,
                        CicloRn2 = 1,
                        CambioFaseAsesor = 1,
                        FechaCambioAsesor = DateTime.Now,
                        FechaCambioAsesorAnt = DateTime.Now,
                        IdCategoriaOrigen = oportunidad.IdCategoriaOrigen,
                        IdSubCategoriaDato = oportunidad.IdSubCategoriaDato,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    if (IdNuevoAsesor != 0 && IdNuevoAsesor != -1 && (oportunidad.IdPersonalAsignado != IdNuevoAsesor))
                    {

                        oportunidad.OportunidadLogAntigua = null;
                        oportunidad.OportunidadLogNueva = null;
                        oportunidad.OportunidadLogNueva = nuevoLog;
                        ActualizarOportunidadVentaCruzada(ref oportunidad, IdNuevoAsesor, "UsuarioReasignacion", EnviaCorreo, true);
                    }
                    scope.Complete();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// Tipo Función: interna
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// <summary>
        /// Reasigna las oportunidades enviadas como referencia
        /// </summary>
        /// <param name="Oportunidad">Referencia a objeto de clase OportunidadBO</param>
        /// <param name="IdNuevoAsesor">Id del nuevo asesor (PK de la tabla gp.T_Personal)</param>
        /// <param name="EnviaCorreo">Flag para determinar si se enviara un correo o no</param>
        /// <returns>Bool</returns>
        private bool ReasignarOportunidadesRef(ref OportunidadBoDTO oportunidad, int IdNuevoAsesor, bool EnviaCorreo)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var nuevoLog = new OportunidadLog()
                    {
                        Id = 0,
                        IdPersonalAsignado = oportunidad.IdPersonalAsignado,
                        IdAsesorAnt = oportunidad.IdPersonalAsignado,
                        IdContacto = oportunidad.IdAlumno,
                        IdFaseOportunidad = oportunidad.IdFaseOportunidad,
                        IdFaseOportunidadAnt = oportunidad.IdFaseOportunidad,
                        IdOportunidad = oportunidad.Id,
                        IdCentroCosto = oportunidad.IdCentroCosto,
                        IdCentroCostoAnt = oportunidad.IdCentroCosto,
                        IdOrigen = oportunidad.IdOrigen,
                        IdTipoDato = oportunidad.IdTipoDato,
                        FechaLog = DateTime.Now,
                        FechaFinLog = DateTime.Now,
                        FechaCambioFase = DateTime.Now,
                        FechaCambioFaseAnt = DateTime.Now,
                        FechaCambioFaseIs = null,
                        CambioFase = true,
                        CambioFaseIs = false,
                        Comentario = oportunidad.UltimoComentario,
                        IdConjuntoAnuncio = oportunidad.IdConjuntoAnuncio,
                        FechaRegistroCampania = oportunidad.FechaRegistroCampania,
                        CicloRn2 = 1,
                        CambioFaseAsesor = 1,
                        FechaCambioAsesor = DateTime.Now,
                        FechaCambioAsesorAnt = DateTime.Now,
                        IdCategoriaOrigen = oportunidad.IdCategoriaOrigen,
                        IdSubCategoriaDato = oportunidad.IdSubCategoriaDato,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    if (IdNuevoAsesor != 0 && IdNuevoAsesor != -1 && (oportunidad.IdPersonalAsignado != IdNuevoAsesor))
                    {
                        oportunidad.OportunidadLogAntigua = null;
                        oportunidad.OportunidadLogNueva = null;
                        oportunidad.OportunidadLogNueva = nuevoLog;
                        ActualizarOportunidadVentaCruzada(ref oportunidad, IdNuevoAsesor, "UsuarioReasignacion", EnviaCorreo, true);
                    }
                    scope.Complete();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// Tipo Función: interna
        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra las oportunidades enviadas a OD
        /// </summary>
        /// <param name="Oportunidades">Lista de enteros con las opoortunidades (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        /// <returns>Bool</returns>
        private bool CerrarOportunidadesOD(int[] Oportunidades, string Usuario)
        {
            var oportunidadRemarketingAgendaService = new OportunidadRemarketingAgendaService(_unitOfWork);
            foreach (int idOportunidad in Oportunidades)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        this._oportunidadBo = new OportunidadBoDTO();
                        this._oportunidadBo = _mapper.Map<OportunidadBoDTO>(ObtenerPorId(idOportunidad));
                        this._oportunidadBo.Usuario = Usuario;
                        if (this._oportunidadBo == null)
                            throw new Exception("No existe oportunidad!");

                        try
                        {
                            oportunidadRemarketingAgendaService.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                        }
                        catch (Exception ex)
                        {
                        }

                        //Finalizar Actividad
                        ActividadDetalle actividadDetalle = new ActividadDetalle();
                        actividadDetalle.Id = _oportunidadBo.IdActividadDetalleUltima.Value;
                        actividadDetalle.Comentario = "Cerrado OD";
                        actividadDetalle.IdOcurrencia = 232; //Cerrado Fase OD
                        actividadDetalle.IdOcurrenciaActividad = null;
                        actividadDetalle.IdAlumno = this._oportunidadBo.IdAlumno;
                        actividadDetalle.IdOportunidad = this._oportunidadBo.Id;
                        actividadDetalle.IdCentralLlamada = 0;
                        actividadDetalle.IdActividadCabecera = this._oportunidadBo.IdActividadCabeceraUltima;
                        this._oportunidadBo.ActividadAntigua = actividadDetalle;
                        this._oportunidadBo.ActividadNueva = new ActividadDetalle();

                        OportunidadDTO oportunidad = new OportunidadDTO()
                        {
                            Id = this._oportunidadBo.Id,
                            IdCentroCosto = this._oportunidadBo.IdCentroCosto.Value,
                            IdPersonalAsignado = this._oportunidadBo.IdPersonalAsignado,
                            IdTipoDato = this._oportunidadBo.IdTipoDato,
                            IdFaseOportunidad = this._oportunidadBo.IdFaseOportunidad,
                            IdOrigen = this._oportunidadBo.IdOrigen.Value,
                            IdAlumno = this._oportunidadBo.IdAlumno,
                            UltimoComentario = this._oportunidadBo.UltimoComentario,
                            IdActividadDetalleUltima = this._oportunidadBo.IdActividadDetalleUltima,
                            IdActividadCabeceraUltima = this._oportunidadBo.IdActividadCabeceraUltima,
                            IdEstadoActividadDetalleUltimoEstado = this._oportunidadBo.IdEstadoActividadDetalleUltimoEstado,
                            UltimaFechaProgramada = (this._oportunidadBo.UltimaFechaProgramada.ToString()),
                            IdEstadoOportunidad = this._oportunidadBo.IdEstadoOportunidad,
                            IdEstadoOcurrenciaUltimo = this._oportunidadBo.IdEstadoOcurrenciaUltimo,
                            IdFaseOportunidadMaxima = this._oportunidadBo.IdFaseOportunidadMaxima,
                            IdFaseOportunidadInicial = this._oportunidadBo.IdFaseOportunidadInicial,
                            IdCategoriaOrigen = this._oportunidadBo.IdCategoriaOrigen,
                            IdConjuntoAnuncio = this._oportunidadBo.IdConjuntoAnuncio,
                            IdCampaniaScoring = this._oportunidadBo.IdCampaniaScoring,
                            IdFaseOportunidadIp = this._oportunidadBo.IdFaseOportunidadIp,
                            IdFaseOportunidadIc = this._oportunidadBo.IdFaseOportunidadIc,
                            FechaEnvioFaseOportunidadPf = this._oportunidadBo.FechaEnvioFaseOportunidadPf,
                            FechaPagoFaseOportunidadPf = this._oportunidadBo.FechaPagoFaseOportunidadPf,
                            FechaPagoFaseOportunidadIc = this._oportunidadBo.FechaPagoFaseOportunidadIc,
                            FechaRegistroCampania = this._oportunidadBo.FechaRegistroCampania,
                            IdFaseOportunidadPortal = this._oportunidadBo.IdFaseOportunidadPortal,
                            IdFaseOportunidadPf = this._oportunidadBo.IdFaseOportunidadPf,
                            CodigoPagoIc = this._oportunidadBo.CodigoPagoIc,
                            FlagVentaCruzada = this._oportunidadBo.IdTiempoCapacitacion,
                            IdTiempoCapacitacion = this._oportunidadBo.IdTiempoCapacitacion,
                            IdTiempoCapacitacionValidacion = this._oportunidadBo.IdTiempoCapacitacionValidacion,
                            IdSubCategoriaDato = this._oportunidadBo.IdSubCategoriaDato,
                            IdInteraccionFormulario = this._oportunidadBo.IdInteraccionFormulario,
                            UrlOrigen = this._oportunidadBo.UrlOrigen,
                            FechaPaso2 = this._oportunidadBo.FechaPaso2,
                            Paso2 = this._oportunidadBo.Paso2,
                            CodMailing = this._oportunidadBo.CodMailing,
                            IdPagina = this._oportunidadBo.IdPagina
                        };
                        FinalizarActividad(false, oportunidad);
                        Update(MapeoBoDTO(this._oportunidadBo));
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
        /// Tipo Función: interna
        /// Autor: Carlos Crispin R.
        /// Fecha: 07/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Cierra las oportunidades enviadas a OM
        /// </summary>
        /// <param name="Oportunidades">Lista de enteros con las opoortunidades (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="Usuario">Usuario que realiza la modificacion</param>
        /// <returns>Bool</returns>
        private bool CerrarOportunidadesOM(int[] Oportunidades, string Usuario)
        {
            var oportunidadRemarketingAgendaService = new OportunidadRemarketingAgendaService(_unitOfWork);
            foreach (int idOportunidad in Oportunidades)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        this._oportunidadBo = new OportunidadBoDTO();
                        this._oportunidadBo = _mapper.Map<OportunidadBoDTO>(ObtenerPorId(idOportunidad));
                        this._oportunidadBo.Usuario = Usuario;
                        if (this._oportunidadBo == null)
                            throw new Exception("No existe oportunidad!");

                        try
                        {
                            oportunidadRemarketingAgendaService.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                        }
                        catch (Exception ex)
                        {
                        }

                        //Finalizar Actividad
                        ActividadDetalle actividadDetalle = new ActividadDetalle();
                        actividadDetalle.Id = this._oportunidadBo.IdActividadDetalleUltima.Value;
                        actividadDetalle.Comentario = "Cerrado Fase OM";
                        actividadDetalle.IdOcurrencia = 233; //Cerrado Fase OM
                        actividadDetalle.IdOcurrenciaActividad = null;
                        actividadDetalle.IdAlumno = this._oportunidadBo.IdAlumno;
                        actividadDetalle.IdOportunidad = this._oportunidadBo.Id;
                        actividadDetalle.IdCentralLlamada = 0;
                        actividadDetalle.IdActividadCabecera = this._oportunidadBo.IdActividadCabeceraUltima;
                        this._oportunidadBo.ActividadAntigua = actividadDetalle;
                        this._oportunidadBo.ActividadNueva = new ActividadDetalle();


                        OportunidadDTO oportunidad = new OportunidadDTO()
                        {
                            Id = this._oportunidadBo.Id,
                            IdCentroCosto = this._oportunidadBo.IdCentroCosto.Value,
                            IdPersonalAsignado = this._oportunidadBo.IdPersonalAsignado,
                            IdTipoDato = this._oportunidadBo.IdTipoDato,
                            IdFaseOportunidad = this._oportunidadBo.IdFaseOportunidad,
                            IdOrigen = this._oportunidadBo.IdOrigen.Value,
                            IdAlumno = this._oportunidadBo.IdAlumno,
                            UltimoComentario = this._oportunidadBo.UltimoComentario,
                            IdActividadDetalleUltima = this._oportunidadBo.IdActividadDetalleUltima,
                            IdActividadCabeceraUltima = this._oportunidadBo.IdActividadCabeceraUltima,
                            IdEstadoActividadDetalleUltimoEstado = this._oportunidadBo.IdEstadoActividadDetalleUltimoEstado,
                            UltimaFechaProgramada = (this._oportunidadBo.UltimaFechaProgramada.ToString()),
                            IdEstadoOportunidad = this._oportunidadBo.IdEstadoOportunidad,
                            IdEstadoOcurrenciaUltimo = this._oportunidadBo.IdEstadoOcurrenciaUltimo,
                            IdFaseOportunidadMaxima = this._oportunidadBo.IdFaseOportunidadMaxima,
                            IdFaseOportunidadInicial = this._oportunidadBo.IdFaseOportunidadInicial,
                            IdCategoriaOrigen = this._oportunidadBo.IdCategoriaOrigen,
                            IdConjuntoAnuncio = this._oportunidadBo.IdConjuntoAnuncio,
                            IdCampaniaScoring = this._oportunidadBo.IdCampaniaScoring,
                            IdFaseOportunidadIp = this._oportunidadBo.IdFaseOportunidadIp,
                            IdFaseOportunidadIc = this._oportunidadBo.IdFaseOportunidadIc,
                            FechaEnvioFaseOportunidadPf = this._oportunidadBo.FechaEnvioFaseOportunidadPf,
                            FechaPagoFaseOportunidadPf = this._oportunidadBo.FechaPagoFaseOportunidadPf,
                            FechaPagoFaseOportunidadIc = this._oportunidadBo.FechaPagoFaseOportunidadIc,
                            FechaRegistroCampania = this._oportunidadBo.FechaRegistroCampania,
                            IdFaseOportunidadPortal = this._oportunidadBo.IdFaseOportunidadPortal,
                            IdFaseOportunidadPf = this._oportunidadBo.IdFaseOportunidadPf,
                            CodigoPagoIc = this._oportunidadBo.CodigoPagoIc,
                            FlagVentaCruzada = this._oportunidadBo.IdTiempoCapacitacion,
                            IdTiempoCapacitacion = this._oportunidadBo.IdTiempoCapacitacion,
                            IdTiempoCapacitacionValidacion = this._oportunidadBo.IdTiempoCapacitacionValidacion,
                            IdSubCategoriaDato = this._oportunidadBo.IdSubCategoriaDato,
                            IdInteraccionFormulario = this._oportunidadBo.IdInteraccionFormulario,
                            UrlOrigen = this._oportunidadBo.UrlOrigen,
                            FechaPaso2 = this._oportunidadBo.FechaPaso2,
                            Paso2 = this._oportunidadBo.Paso2,
                            CodMailing = this._oportunidadBo.CodMailing,
                            IdPagina = this._oportunidadBo.IdPagina
                        };
                        FinalizarActividad(false, oportunidad);
                        Update(MapeoBoDTO(this._oportunidadBo));
                        scope.Complete();
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idPersonal"></param>
        /// <returns>int</returns>
        public int ObtenerCentroCostoPorCelularAlumno(string numero, int idPersonal)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerCentroCostoPorCelularAlumno(numero, idPersonal);
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
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idPersonal"></param>
        /// <returns> OportunidadBandejaCorreoDTO </returns>
        public OportunidadBandejaCorreoDTO ObtenerOportunidadPorAsesorYAlumno(int idAlumno, int idPersonal, string numero)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerOportunidadPorAsesorYAlumno(idAlumno, idPersonal, numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paginador"></param>
        /// /// <param name="filtroGrilla"></param>
        /// <param name="operadorComparacion"></param>
        /// <returns></returns>

        public ResultadoAsignacionManualFiltroTotalDTO ObtenerPorFiltroPaginaManual(AsignacionManualOportunidadFiltroDTO obj, FiltroKendoGridDTO filter, List<OperadoresComparacionDTO> operadorComparacion)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerPorFiltroPaginaManual(obj, filter, operadorComparacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 04/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="mantenerestadooportunidad"> Confirmación para mantener el estado de oportunidad </param>
        /// <param name="datosOportunidad"> Datos de Oportunidad </param>
        /// <returns> Vacio </returns>
        public void FinalizarActividades(bool mantenerestadooportunidadNuevaEntidad, string Usuario)
        {

            //, OportunidadDatosDTO datosOportunidad, ActividadDetalle actividadAntigua, OportunidadCompetidor oportunidadCompetidor
            string flagError = "";
            try
            {
                var actividadDetalleService = new ActividadDetalleService(_unitOfWork);
                var preCalculadaCambioFaseService = new PreCalculadaCambioFaseService(_unitOfWork);
                var repActividadDetalle = _unitOfWork.ActividadDetalleRepository;
                var repPreCalculadaCambioFase = _unitOfWork.PreCalculadaCambioFaseRepository;
                //Oportunidad Oportunidad = new Oportunidad(parametroFinalizarActividadDTO.ActividadAntigua.IdOportunidad.Value, parametroFinalizarActividadDTO.Usuario, _integraDBContext);
                //oportunidadNuevaEntidadEntidadDTO = _unitOfWork.OportunidadRepository.ObtenerPorId(actividadAntigua.IdOportunidad.Value);
                var actividadAntigua = this._asignacionManual.ActividadAntigua;
                var oportunidadAntigua = this._asignacionManual.OportunidadAntigua;
                int idFaseOportunidadActual = this._asignacionManual.OportunidadAntigua.IdFaseOportunidad;
                flagError = "ObtenerUltimoOportunidadLog";
                var oportunidadLogAntiguaTemp = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog2(actividadAntigua.IdOportunidad.Value);
                var oportunidadLogAntigua = _mapper.Map<OportunidadLog>(_unitOfWork.OportunidadLogRepository.FirstById(oportunidadLogAntiguaTemp.Id));
                var fechaFinLLamada = DateTime.Now;
                if (this._asignacionManual.ActividadAntigua.IdOcurrencia == 0)
                    throw new ArgumentException("Debe seleccionar una ocurrencia");

                this._asignacionManual.ActividadAntigua.DuracionReal = 13;
                this._asignacionManual.ActividadAntigua.IdCentralLlamada = 3;
                flagError = "ObtenerOcurrenciaPorActividad";
                Ocurrencia ocurrencia = _unitOfWork.OcurrenciaRepository.ObtenerOcurrenciaPorActividad(actividadAntigua.IdOcurrencia.Value);
                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = 2;  // ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                }
                this._asignacionManual.ActividadAntigua.IdEstadoActividadDetalle = 2;  // ValorEstatico.IdEstadoActividadDetalleEjecutado;
                //var validadActividadDetalle = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(actividadAntigua.Id);
                // Actualizar Actividad Detalle
                if (!_unitOfWork.ActividadDetalleRepository.Exist(actividadAntigua.Id)) throw new Exception("La oportunidad no tiene actividad detalle");

                //var actividadNueva = new ActividadDetalle();
                ActividadDetalle actividadNueva = _mapper.Map<ActividadDetalle>(repActividadDetalle.FirstById(actividadAntigua.Id));
                //var actividadDetalleDto = actividadDetalleService.ObtenerPorId(this.asignacionManual.ActividadAntigua.Id);
                //actividadNueva = actividadDetalleService.MapeoEntidadDesdeDTO(actividadDetalleDto);
                actividadNueva.FechaReal = DateTime.Now;
                actividadNueva.DuracionReal = actividadAntigua.DuracionReal;
                actividadNueva.IdEstadoActividadDetalle = actividadAntigua.IdEstadoActividadDetalle;
                actividadNueva.Comentario = actividadAntigua.Comentario;
                actividadNueva.IdOcurrencia = actividadAntigua.IdOcurrencia;
                actividadNueva.IdOcurrenciaActividad = actividadAntigua.IdOcurrenciaActividad;
                actividadNueva.IdCentralLlamada = actividadAntigua.IdCentralLlamada;
                actividadNueva.FechaModificacion = DateTime.Now;
                actividadNueva.UsuarioModificacion = Usuario;
                actividadNueva.IdClasificacionPersona = actividadAntigua.IdClasificacionPersona;
                actividadNueva.FechaProgramada = actividadAntigua.FechaProgramada;

                if (ocurrencia.IdFaseOportunidad != 31) // ValorEstatico.IdFaseOportunidadNulo)
                {
                    this._asignacionManual.OportunidadNueva.IdFaseOportunidad = ocurrencia.IdFaseOportunidad;
                }
                this._asignacionManual.OportunidadNueva.UltimoComentario = actividadAntigua.Comentario;
                this._asignacionManual.OportunidadNueva.UltimaFechaProgramada = actividadAntigua.FechaProgramada == null ? DateTime.Now : actividadAntigua.FechaProgramada.Value;
                this._asignacionManual.OportunidadNueva.IdEstadoActividadDetalleUltimoEstado = 2;    // ValorEstatico.IdEstadoActividadDetalleEjecutado;
                this._asignacionManual.OportunidadNueva.IdActividadDetalleUltima = actividadAntigua.Id;

                if (oportunidadAntigua.IdEstadoOportunidad != 0 && oportunidadAntigua.IdEstadoOportunidad == 7 && mantenerestadooportunidadNuevaEntidad)    //ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada 
                {
                    this._asignacionManual.OportunidadNueva.IdEstadoOportunidad = 7;  // ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
                else
                {
                    this._asignacionManual.OportunidadNueva.IdEstadoOportunidad = 1;  // ValorEstatico.IdEstadoOportunidadEjecutada;
                }
                this._asignacionManual.OportunidadNueva.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                if (this._asignacionManual.OportunidadNueva.IdFaseOportunidad != 0 && oportunidadAntigua.IdFaseOportunidad != this._asignacionManual.OportunidadNueva.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    this._asignacionManual.OportunidadNueva.IdFaseOportunidadMaxima = _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria(oportunidadAntigua.IdFaseOportunidad, this._asignacionManual.OportunidadNueva.IdFaseOportunidad);
                }
                this._asignacionManual.OportunidadNueva.FechaModificacion = DateTime.Now;
                this._asignacionManual.OportunidadNueva.UsuarioModificacion = Usuario;

                var oportunidadNueva = this._asignacionManual.OportunidadNueva;

                //Guardar Log
                OportunidadLog oportunidadLogNueva = new OportunidadLog();
                oportunidadLogNueva.IdClasificacionPersona = oportunidadLogAntigua.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = oportunidadLogAntigua.IdPersonalAreaTrabajo;
                oportunidadLogNueva.IdCentroCosto = oportunidadNueva.IdCentroCosto;
                oportunidadLogNueva.IdPersonalAsignado = oportunidadNueva.IdPersonalAsignado;
                oportunidadLogNueva.IdTipoDato = oportunidadNueva.IdTipoDato;
                oportunidadLogNueva.IdFaseOportunidadAnt = idFaseOportunidadActual;
                oportunidadLogNueva.IdFaseOportunidad = oportunidadNueva.IdFaseOportunidad;
                oportunidadLogNueva.IdOrigen = oportunidadNueva.IdOrigen;
                oportunidadLogNueva.IdContacto = oportunidadNueva.IdAlumno;
                oportunidadLogNueva.FechaFinLog = oportunidadLogAntigua.FechaLog;
                oportunidadLogNueva.IdCentroCostoAnt = oportunidadLogAntigua.IdCentroCosto;
                oportunidadLogNueva.IdAsesorAnt = oportunidadLogAntigua.IdPersonalAsignado;
                oportunidadLogNueva.FechaLog = DateTime.Now;
                oportunidadLogNueva.IdActividadDetalle = actividadAntigua.Id;
                oportunidadLogNueva.IdOcurrencia = actividadAntigua.IdOcurrencia;
                oportunidadLogNueva.IdOcurrenciaActividad = actividadAntigua.IdOcurrenciaActividad;
                oportunidadLogNueva.Comentario = oportunidadNueva.UltimoComentario;
                oportunidadLogNueva.IdOportunidad = oportunidadNueva.Id;
                oportunidadLogNueva.IdCategoriaOrigen = oportunidadNueva.IdCategoriaOrigen;
                oportunidadLogNueva.IdSubCategoriaDato = oportunidadNueva.IdSubCategoriaDato;
                oportunidadLogNueva.FechaRegistroCampania = oportunidadNueva.FechaRegistroCampania;
                oportunidadLogNueva.IdFaseOportunidadIc = oportunidadAntigua.IdFaseOportunidadIc;
                oportunidadLogNueva.IdFaseOportunidadIp = oportunidadAntigua.IdFaseOportunidadIp;
                oportunidadLogNueva.IdFaseOportunidadPf = oportunidadAntigua.IdFaseOportunidadPf;
                oportunidadLogNueva.FechaEnvioFaseOportunidadPf = oportunidadAntigua.FechaEnvioFaseOportunidadPf;
                oportunidadLogNueva.FechaPagoFaseOportunidadIc = oportunidadAntigua.FechaPagoFaseOportunidadIc;
                oportunidadLogNueva.FechaPagoFaseOportunidadPf = oportunidadAntigua.FechaPagoFaseOportunidadPf;
                oportunidadLogNueva.FasesActivas = oportunidadLogAntigua.FasesActivas;
                oportunidadLogNueva.CodigoPagoIc = oportunidadAntigua.CodigoPagoIc;
                oportunidadLogNueva.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = oportunidadNueva.IdPersonalAreaTrabajo;
                oportunidadLogNueva.VentaCruzadaMarketing = _asignacionManual.VentaCruzadaMarketing;


                if (oportunidadLogNueva.IdFaseOportunidadAnt != oportunidadLogNueva.IdFaseOportunidad)
                {
                    oportunidadLogNueva.CambioFase = true;
                    oportunidadLogNueva.FechaCambioFase = oportunidadLogNueva.FechaLog;
                    oportunidadLogNueva.FechaCambioFaseAnt = oportunidadLogAntigua.FechaCambioFase;
                    oportunidadLogNueva.CambioFaseAsesor = 1;
                    oportunidadLogNueva.FechaCambioAsesor = oportunidadLogNueva.FechaLog;
                    oportunidadLogNueva.FechaCambioAsesorAnt = oportunidadLogAntigua.FechaCambioAsesor;
                    if (oportunidadLogNueva.IdFaseOportunidad != 0 && oportunidadLogNueva.IdFaseOportunidad == 5)   //ValorEstatico.IdFaseOportunidadIS)
                    {
                        oportunidadLogNueva.FechaCambioFaseIs = oportunidadLogNueva.FechaLog;
                        oportunidadLogNueva.CambioFaseIs = true;
                    }
                    else
                    {
                        oportunidadLogNueva.FechaCambioFaseIs = oportunidadLogNueva.FechaCambioFaseIs;
                        oportunidadLogNueva.CambioFaseIs = false;
                    }
                    // if (oportunidadLogNueva.IdFaseOportunidad != 0 && oportunidadLogNueva.IdFaseOportunidadAnt != 0 && oportunidadLogNueva.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2 && oportunidadLogNueva.IdFaseOportunidadAnt == ValorEstatico.IdFaseOportunidadRN2)
                    if (oportunidadLogNueva.IdFaseOportunidad != 0 && oportunidadLogNueva.IdFaseOportunidadAnt != 0 && oportunidadLogNueva.IdFaseOportunidad == 10 && oportunidadLogNueva.IdFaseOportunidadAnt == 10)
                    {
                        oportunidadLogNueva.CicloRn2 = oportunidadLogAntigua.CicloRn2 + 1;
                    }
                    else
                    {
                        oportunidadLogNueva.CicloRn2 = oportunidadLogAntigua.CicloRn2;
                    }
                }
                else
                {
                    oportunidadLogNueva.CambioFase = false;
                    oportunidadLogNueva.FechaCambioFase = oportunidadLogAntigua.FechaCambioFase;
                    oportunidadLogNueva.FechaCambioFaseAnt = oportunidadLogAntigua.FechaCambioFase;//ultima 1***
                    oportunidadLogNueva.FechaCambioFaseIs = oportunidadLogAntigua.FechaCambioFaseIs;
                    oportunidadLogNueva.CambioFaseIs = false;
                    oportunidadLogNueva.CambioFaseAsesor = 0;
                    oportunidadLogNueva.FechaCambioAsesor = oportunidadLogAntigua.FechaCambioAsesor;
                    oportunidadLogNueva.FechaCambioAsesorAnt = oportunidadLogAntigua.FechaCambioAsesor;
                    oportunidadLogNueva.CicloRn2 = oportunidadLogAntigua.CicloRn2;
                }
                oportunidadLogNueva.FechaCreacion = DateTime.Now;
                oportunidadLogNueva.FechaModificacion = DateTime.Now;
                oportunidadLogNueva.UsuarioCreacion = Usuario;
                oportunidadLogNueva.UsuarioModificacion = Usuario;
                oportunidadLogNueva.Estado = true;

                if (oportunidadLogNueva.IdFaseOportunidadAnt != oportunidadLogNueva.IdFaseOportunidad)
                {
                    PreCalculadaCambioFase preCalculadaCambioFase = new PreCalculadaCambioFase();
                    preCalculadaCambioFase.IdPersonal = oportunidadLogNueva.IdPersonalAsignado;
                    preCalculadaCambioFase.Fecha = DateTime.Now;
                    preCalculadaCambioFase.IdCategoriaOrigen = oportunidadLogNueva.IdCategoriaOrigen;
                    preCalculadaCambioFase.IdCentroCosto = oportunidadLogNueva.IdCentroCosto;
                    preCalculadaCambioFase.IdFaseOportunidadDestino = oportunidadLogNueva.IdFaseOportunidad;
                    preCalculadaCambioFase.IdFaseOportunidadOrigen = oportunidadLogNueva.IdFaseOportunidadAnt;
                    preCalculadaCambioFase.IdOrigen = oportunidadNueva.IdOrigen;
                    preCalculadaCambioFase.IdTipoDato = oportunidadNueva.IdTipoDato;
                    preCalculadaCambioFase.FechaCreacion = DateTime.Now;
                    preCalculadaCambioFase.FechaModificacion = DateTime.Now;
                    preCalculadaCambioFase.UsuarioCreacion = Usuario;
                    preCalculadaCambioFase.UsuarioModificacion = Usuario;
                    preCalculadaCambioFase.Estado = true;
                    this._asignacionManual.PreCalculadaCambioFase = preCalculadaCambioFase;
                    repPreCalculadaCambioFase.Add(preCalculadaCambioFase);
                }
                if (this._asignacionManual.OportunidadCompetidor != null && this._asignacionManual.OportunidadCompetidor.Id != 0)
                {
                    flagError = "EliminarOportunidadCompetidorDetalle";
                    //servicioCalidadProcesamiento.EliminarOportunidadCompetidorDetalle(OportunidadCompetidor.Id, usuario);
                    this._asignacionManual.OportunidadCompetidor.FechaModificacion = DateTime.Now;
                    this._asignacionManual.OportunidadCompetidor.UsuarioModificacion = Usuario;
                }

                this._asignacionManual.ActividadNueva = actividadNueva;
                this._asignacionManual.OportunidadLogNueva = oportunidadLogNueva;
                this._asignacionManual.ActividadAntigua = null;
                this._asignacionManual.OportunidadLogAntigua = null;
                //return MapeoEntidadDesdeDTO(oportunidadNuevaEntidadEntidadDTO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + '-' + flagError);
            }
        }


        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 30/04/2021
        /// Versión: 1.0
        /// <summary>
        /// Programar Actividad
        /// </summary> 
        /// <returns> Vacio </returns>
        public void ProgramaActividad(bool CheckSegProg = false)
        {
            try
            {
                var _repOcurrencia = _unitOfWork.OcurrenciaRepository;
                var actividadNueva = this._asignacionManual.ActividadNueva;
                var oportunidadNueva = this._asignacionManual.OportunidadNueva;

                var ocurrencia = _repOcurrencia.ObtenerOcurrenciaPorActividad(actividadNueva.IdOcurrencia.Value);
                if (actividadNueva.IdOportunidad == 0)
                {
                    throw new ArgumentException("debe tener una oportunidad");
                }

                if (actividadNueva.IdOcurrencia == 0 || actividadNueva.IdOcurrencia == null)
                {
                    this._asignacionManual.ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(oportunidadNueva.IdFaseOportunidad, oportunidadNueva.IdTipoDato, oportunidadNueva.IdPersonalAreaTrabajo, oportunidadNueva.IdActividadCabeceraUltima);
                    //guardar el logger
                }
                else
                {
                    if (ocurrencia != null)
                    {
                        if (_repOcurrencia.ValidarEstadoOcurrencia(ocurrencia.Id))
                        {
                            ocurrencia.IdEstadoOcurrencia = 2;//ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                        }
                        if (ocurrencia.IdActividadCabecera == null)
                        {
                            if (!(actividadNueva.IdActividadCabecera == null))
                            {
                                this._asignacionManual.ActividadNueva.IdActividadCabecera = actividadNueva.IdActividadCabecera;
                            }
                            else
                            {
                                this._asignacionManual.ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(oportunidadNueva.IdFaseOportunidad, oportunidadNueva.IdTipoDato, oportunidadNueva.IdPersonalAreaTrabajo, oportunidadNueva.IdActividadCabeceraUltima);
                            }
                        }
                        else
                        {
                            if (oportunidadNueva.IdFaseOportunidad == 22 && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                                                                                                  //if (this.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                            {
                                this._asignacionManual.ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(oportunidadNueva.IdFaseOportunidad, oportunidadNueva.IdTipoDato, oportunidadNueva.IdPersonalAreaTrabajo, oportunidadNueva.IdActividadCabeceraUltima);
                            }
                            else
                            {
                                this._asignacionManual.ActividadNueva.IdActividadCabecera = ocurrencia.IdActividadCabecera;
                            }
                        }
                    }
                }
                ActividadDetalle actividadNuevaProgramarActividad = new ActividadDetalle();
                actividadNuevaProgramarActividad.IdOportunidad = actividadNueva.IdOportunidad;
                actividadNuevaProgramarActividad.IdAlumno = actividadNueva.IdAlumno;
                actividadNuevaProgramarActividad.Actor = "A";
                actividadNuevaProgramarActividad.FechaProgramada = oportunidadNueva.UltimaFechaProgramada.HasValue ? oportunidadNueva.UltimaFechaProgramada.Value : default(DateTime);
                actividadNuevaProgramarActividad.IdEstadoActividadDetalle = 1;  // EstadoActividadDetalleBO.EstadoActividadDetalleNoEjecutado;

                actividadNuevaProgramarActividad.FechaCreacion = DateTime.Now;
                actividadNuevaProgramarActividad.FechaModificacion = DateTime.Now;
                actividadNuevaProgramarActividad.UsuarioCreacion = oportunidadNueva.UsuarioModificacion;
                actividadNuevaProgramarActividad.UsuarioModificacion = oportunidadNueva.UsuarioModificacion;
                actividadNuevaProgramarActividad.Estado = true;
                actividadNuevaProgramarActividad.IdActividadCabecera = actividadNueva.IdActividadCabecera;
                actividadNuevaProgramarActividad.IdOcurrencia = actividadNueva.IdOcurrencia;
                actividadNuevaProgramarActividad.IdOcurrenciaActividad = actividadNueva.IdOcurrenciaActividad;
                actividadNuevaProgramarActividad.IdClasificacionPersona = oportunidadNueva.IdClasificacionPersona;

                this._asignacionManual.ActividadNuevaProgramarActividad = actividadNuevaProgramarActividad;
                //oportunidad.ActividadDetalles.Add(ActividadNuevaProgramarActividad);
                //Actualiza Oportunidad

                this._asignacionManual.OportunidadNueva.IdActividadDetalleUltima = actividadNueva.Id;
                this._asignacionManual.OportunidadNueva.IdActividadCabeceraUltima = actividadNueva.IdActividadCabecera.Value;
                this._asignacionManual.OportunidadNueva.IdEstadoActividadDetalleUltimoEstado = actividadNueva.IdEstadoActividadDetalle;
                this._asignacionManual.OportunidadNueva.UltimaFechaProgramada = oportunidadNueva.UltimaFechaProgramada.HasValue ? oportunidadNueva.UltimaFechaProgramada.Value : default(DateTime);
                this._asignacionManual.OportunidadNueva.UltimoComentario = actividadNueva.Comentario;

                if (actividadNueva.IdOcurrencia == 35) //OCURRENCIA_ASIGNACION_MANUAL
                {
                    this._asignacionManual.OportunidadNueva.IdEstadoOportunidad = (CheckSegProg == true) ? 8 : 3;
                }
                else
                {
                    this._asignacionManual.OportunidadNueva.IdEstadoOportunidad = this._asignacionManual.OportunidadNueva.UltimaFechaProgramada.HasValue ? 6 : 2;
                }

                var grupoPrelanzamiento = _repOcurrencia.ValidarGrupoPreLanzamiento(oportunidadNueva.IdCategoriaOrigen.Value);

                if (oportunidadNueva.IdEstadoOportunidad == 2 && ocurrencia.IdEstadoOcurrencia == 2 && grupoPrelanzamiento == 1 && oportunidadNueva.IdFaseOportunidad == 2)
                {
                    this._asignacionManual.OportunidadNueva.IdEstadoOportunidad = 2; //ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                if (oportunidadNueva.IdEstadoOportunidad != 0 && oportunidadNueva.IdEstadoOportunidad.Equals(7) && false) //&& oportunidad.IdEstadoOportunidad.Equals(ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada) && false)
                {
                    this._asignacionManual.OportunidadNueva.IdEstadoOportunidad = 7;//ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AsignacionOportunidadManual MapeoAsignacionOportunidadManualDesdeDto(OportunidadDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<AsignacionOportunidadManual>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidad de operaciones mediante los siguientes parametros
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> OportunidadOperacionesFiltroDTO </returns>
        public OportunidadOperacionesFiltroDTO ObtenerOportunidadOperacionesPorParametros(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerOportunidadOperacionesPorIdMatricula(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Genera oportunidad de operaciones con parámetros
        /// </summary>
        /// <param name="idOportunidad"> Id de oportunidad </param>
        /// <param name="usuario"> Usuario </param>
        /// <param name="idCentroCosto"> Id de centro de costo </param>
        /// <param name="idActividadCabecera"> Id de Actividad de Cabecera </param>
        /// <param name="idPersonal"> Id de Personal </param>
        /// <param name="idMatriculaCabecera"> Id de Matrícula de Cabecera </param>
        /// <returns> oportunidadBO a partir de un string serializado : OportunidadBO </returns>
        public OportunidadDTO GenerarOportunidadOperacionesConParametros(int idOportunidad, string usuario, int idCentroCosto, int idActividadCabecera, int idPersonal, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.GenerarOportunidadOperacionesConParametros(idOportunidad, usuario, idCentroCosto, idActividadCabecera, idPersonal, idMatriculaCabecera);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta la oportunidad generada en la tabla ope.T_OportunidadClasificacionOperaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        public void InsertarOportunidadClasificacionOperaciones(int idOportunidad)
        {
            try
            {
                _unitOfWork.OportunidadClasificacionOperacionesRepository.CalcularPorIdOportunidad(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina la oportunidad de operaciones en v3 y v4 fisicamente
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public bool EliminarOportunidadFisicaOperacionesV3V4(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.EliminarOportunidadFisicaOperacionesV3V4(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/11/2022
        /// <summary>
        /// Actualiza el alumno y crea una oportunidad
        /// </summary>
        /// <param name="oportunidad">Referencia a objeto de clase OportunidadBO</param>
        /// <param name="flagVentaCruzada">Flag para determinar si se considerara como venta cruzada</param>
        /// <param name="tipoPersona">Objeto de clase enum TipoPersona</param>
        /// <param name="idTipoInteraccion">Id tipo de interaccion</param>
        public void CrearOportunidadActualizarPersona(ref OportunidadBoDTO oportunidad, bool flagVentaCruzada, TipoPersona TipoPersona)
        {
            IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
            var personaService = new PersonaService(_unitOfWork);
            if (TipoPersona == TipoPersona.Alumno)
            {
                if (alumnoService.ExisteContacto(oportunidad.Alumno.Email1, oportunidad.Alumno.Email2))
                {
                    alumnoService.Alumno = oportunidad.Alumno;
                    alumnoService.ValidarEstadoContactoWhatsAppTemporal();
                    var empresaAlumno = oportunidad.Alumno.IdEmpresa;
                    alumnoService.Alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                    _unitOfWork.DetachAll();
                    _unitOfWork.AlumnoRepository.Update(alumnoService.Alumno);
                    _unitOfWork.Commit();

                    if (oportunidad.IdClasificacionPersona == null)
                    {
                        var creacionCorrecta = personaService.InsertarPersona(oportunidad.Alumno.Id, TipoPersona.Alumno, oportunidad.UsuarioCreacion);//funcion("Alumno",alumnoNuevo.Id);
                                                                                                                                                      //Si boto error en al funcion 
                        if (creacionCorrecta == null)
                        {
                            var nombreTablaV3 = "talumnos";
                            var nombreTablaV4 = "mkt.T_Alumno";
                            var resultado = _unitOfWork.AlumnoRepository.EliminarFisicaAlumno(nombreTablaV3, nombreTablaV4, oportunidad.Alumno.Id, null, 0);
                            if (resultado == true)
                            {
                                throw new Exception("Se elimino el alumno");
                            }
                            else
                            {
                                throw new Exception("No se elimino alumno");
                            }
                        }
                        oportunidad.IdClasificacionPersona = creacionCorrecta;
                    }
                    if (!oportunidad.IdPersonalAreaTrabajo.HasValue)
                    {
                        oportunidad.IdPersonalAreaTrabajo = 8;
                    }
                }
                else
                {
                    throw new Exception("Alumno no Existe!");
                }
            }
            else if (TipoPersona == TipoPersona.Docente)
            {
                if (_unitOfWork.ExpositorRepository.ExisteExpositorPorEmail(oportunidad.Expositor.Email1))
                {
                    _unitOfWork.ExpositorRepository.Update(oportunidad.Expositor);
                    _unitOfWork.Commit();
                    if (oportunidad.IdClasificacionPersona == null)
                    {
                        var idClasificacionPersona = personaService.InsertarPersona(oportunidad.Expositor.Id, TipoPersona.Docente, oportunidad.UsuarioCreacion);

                        if (idClasificacionPersona == null)
                        {
                            var nombreTablaV3 = "tPLA_Expositor";
                            var nombreTablaV4 = "pla.T_Expositor";
                            var resultado = alumnoService.EliminarFisicaAlumno(nombreTablaV4, nombreTablaV3, oportunidad.Expositor.Id, null, 0);
                            if (resultado == true)
                            {
                                throw new Exception("Se elimino el expositor");
                            }
                            else
                            {
                                throw new Exception("No se elimino expositor");
                            }
                        }
                        oportunidad.IdClasificacionPersona = idClasificacionPersona;
                    }
                    //personal area trabajo - operaciones
                    if (!oportunidad.IdPersonalAreaTrabajo.HasValue)
                    {
                        oportunidad.IdPersonalAreaTrabajo = 3;
                    }
                }
                else
                {
                    throw new Exception("Expositor no Existe!");
                }
            }
            CrearOportunidad(ref oportunidad, flagVentaCruzada, TipoPersona);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Carga una lista de oportunidades por id alumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idClasificacionPersona"></param>
        /// <returns></returns>
        public List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidades(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerHistorialOportunidades(idAlumno, idClasificacionPersona);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Carga un historial de oportunidades por idAlumno
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idClasificacionPersona"></param>
        /// <returns></returns>
        public List<OportunidadHistorialDTO> CargarOportunidadHistorial(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.CargarOportunidadHistorial(idAlumno, idClasificacionPersona);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idTipoSeguimientoAlumnoCategoria"></param>
        /// <returns></returns>
        public List<ObtenerSeguimientoAlumnoComentarioDTO> ObtenerComentariosOperaciones(int idOportunidad, int idTipoSeguimientoAlumnoCategoria)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerComentariosOperaciones(idOportunidad, idTipoSeguimientoAlumnoCategoria);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo pago y academico
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idTipoSeguimientoAlumnoCategoria"></param>
        /// <returns></returns>
        public List<ObtenerSeguimientoPagosAlumnoComentarioDTO> ObtenerComentariosOperacionesPagosAcademicos(int idOportunidad)
        {

            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerComentariosOperacionesPagosAcademicos(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene oportunidades filtrado por numero de alumno
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<ValidarOportunidadWhatsAppDTO> ValidarOportunidadesWhatsApp(string numero, int idPGeneral)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ValidarOportunidadesWhatsApp(numero, idPGeneral);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Fecha: 04/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida las oportunidades del portal web
        /// </summary>
        /// <returns>Response 200, caso contrario response 400 con el mensaje de error</returns>
        public object ValidarOportunidadesPortalWeb()
        {
            try
            {
                var _repAsignacionAutomaticaTemp = _unitOfWork.AsignacionAutomaticaTempRepository;

                // Obtenemos la configuracion de la asignacion automatica
                var _asignacionAutomaticaConfiguracionRep = _unitOfWork.AsignacionAutomaticaConfiguracionRepository;
                var configuraciones = _asignacionAutomaticaConfiguracionRep.ObtenerConfiguracionAsignacionAutomatica();
                var _repAsignacionAutomatica = _unitOfWork.AsignacionAutomaticaRepository;
                var serAsignacionAutomatica = new AsignacionAutomaticaService(_unitOfWork);
                var AsignacionAutomaticaTemp = new AsignacionAutomaticaTempService(_unitOfWork);


                // Se comentan las lineas ya que su uso esta comentado tambien
                //var inclusion = configuraciones.Where(o => o.Inclusivo == true).ToList();
                //var exclusion = configuraciones.Where(o => o.Inclusivo == false).ToList();

                var listaAsignacionAutomaticaTemp = _repAsignacionAutomaticaTemp.GetBy(w => w.Procesado == false).ToList();

                foreach (var idAsignacionAutomaticaTemp in listaAsignacionAutomaticaTemp.Select(w => w.Id))
                {
                    // Declaro el Objeto de AsignacionAutomatica que se va ha insertar

                    AsignacionAutomatica AsignacionAutomatica = new AsignacionAutomatica();

                    // Obtenemos los Paises
                    var listaPaises = new Dictionary<int, string>();
                    var PaisRep = _unitOfWork.PaisRepository;
                    int idElSalvador = 503;
                    string ElSalvadorIniciales = "SAL";
                    var paises = PaisRep.GetBy(x => x.Estado == true).ToList();

                    foreach (var pais in paises)
                    {
                        // El Salvador
                        if (pais.CodigoPais == idElSalvador)
                            listaPaises.Add(pais.CodigoPais, ElSalvadorIniciales);



                        else
                            listaPaises.Add(pais.CodigoPais, pais.NombrePais.Substring(0, 3).ToUpper());
                    }

                    // Obtenemos los origenes
                    var listaOrigenes = new Dictionary<string, OrigenesCategoriaOrigenDTO>();
                    var _repOrigen = _unitOfWork.OrigenRepository;
                    var origenes = _repOrigen.ObtenerOrigenesCategoriasOrigen();

                    foreach (var origen in origenes)
                    {
                        if (!listaOrigenes.ContainsKey(origen.Nombre.Trim().ToUpper()))
                            listaOrigenes.Add(origen.Nombre.Trim().ToUpper(), new OrigenesCategoriaOrigenDTO { Id = origen.Id, NombreCategoria = origen.NombreCategoria });

                    }

                    try
                    {
                        AsignacionAutomaticaTemp.ValidarRegistroFormularioAsignacionAutomaticaTemp(idAsignacionAutomaticaTemp, listaPaises, listaOrigenes);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }

                    //if (AsignacionAutomatica.AplicarConfiguracion(inclusion, exclusion))
                    if (true)
                    {
                        var objAsignacionAutomaticaTemp = _repAsignacionAutomaticaTemp.FirstById(idAsignacionAutomaticaTemp);
                        using (TransactionScope scope = new TransactionScope())
                        {
                            try
                            {
                                // Actualizamos AsignacionAutomaticamTemp con True en Procesado
                                objAsignacionAutomaticaTemp.Procesado = true;
                                _repAsignacionAutomaticaTemp.Update(objAsignacionAutomaticaTemp);
                                _unitOfWork.Commit();
                                // Insertamos en la lista de Registros para la validacion
                                AsignacionAutomatica.Id = 0;
                                AsignacionAutomatica.Validado = false;
                                AsignacionAutomatica.Corregido = false;
                                AsignacionAutomatica.IdAsignacionAutomaticaOrigen = AsignacionAutomaticaOrigen.PortalWeb;
                                AsignacionAutomatica.IdAsignacionAutomaticaTemp = objAsignacionAutomaticaTemp.Id;
                                AsignacionAutomatica.FechaCreacion = DateTime.Now;
                                AsignacionAutomatica.FechaModificacion = DateTime.Now;


                                AsignacionAutomatica.UsuarioCreacion = "System";
                                AsignacionAutomatica.UsuarioModificacion = "System";
                                AsignacionAutomatica.Estado = true;


                                AsignacionAutomatica.IdCategoriaOrigen = objAsignacionAutomaticaTemp.IdCategoriaDato == null && AsignacionAutomatica.IdCategoriaDato == 18 ? 18 : objAsignacionAutomaticaTemp.IdCategoriaDato;
                                serAsignacionAutomatica.Add(AsignacionAutomatica);
                                _unitOfWork.Commit();

                                scope.Complete();
                            }
                            catch (Exception e)
                            {
                                throw new Exception(e.Message);
                            }
                        }

                        //try
                        //{
                        //    string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAsignacionAutomaticaTemp?IdAsignacionAutomaticaTemp=" + idAsignacionAutomaticaTemp;
                        //    using (WebClient wc = new WebClient())
                        //    {
                        //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        //        wc.DownloadString(URI);
                        //    }
                        //}
                        //catch (Exception)
                        //{
                        //}

                        //// Asignacion automatica
                        //try
                        //{
                        //    string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarAsignacionAutomatica?IdAsignacionAutomatica=" + AsignacionAutomatica.Id;
                        //    using (WebClient wc = new WebClient())
                        //    {
                        //        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        //        wc.DownloadString(URI);
                        //    }
                        //}
                        //catch (Exception)
                        //{
                        //}
                    }
                }
                return (true);
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }

        /// Tipo Función: GET
        /// Autor: Margiory Ramirez 
        /// Fecha: 23/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Crear la oportunidad segun el IdAsignacionAutomatica enviado
        /// </summary>
        /// <returns>Response 200</returns>

        public string CrearOportunidadesPortalWeb(int idAsignacion)
        {
            try
            {
                List<TAsignacionAutomatica> asignacionAutomatica;

                if (idAsignacion == 0)
                {
                    asignacionAutomatica = _unitOfWork.AsignacionAutomaticaRepository.GetBy(x => x.Validado == false && x.Corregido == false && x.FechaCreacion > DateTime.Now.AddDays(-1)).OrderByDescending(x => x.FechaCreacion).Where(x => x.IdCategoriaOrigen != null && x.IdOrigen != null).ToList();
                }
                else
                {
                    var rest = _unitOfWork.AsignacionAutomaticaRepository.FirstById(idAsignacion);
                    asignacionAutomatica = new List<TAsignacionAutomatica>()
                    {
                        rest
                    };
                }
                if (asignacionAutomatica.Count() == 0)
                {
                    return "Sin registros que procesar";
                }
                foreach (var item in asignacionAutomatica)
                {
                    try
                    {
                        if (item.IdFaseOportunidadPortal != null)
                        {
                            if (_unitOfWork.AsignacionAutomaticaRepository.Exist(x => x.IdFaseOportunidadPortal == item.IdFaseOportunidadPortal && x.Validado == true))
                            {
                                var existente = _unitOfWork.AsignacionAutomaticaRepository.FirstBy(x => x.IdFaseOportunidadPortal == item.IdFaseOportunidadPortal && x.Validado == true);
                                if (existente != null && item.Id != existente.Id)
                                {
                                    item.Validado = true;
                                    item.Corregido = false;
                                    item.Estado = false;
                                    item.FechaModificacion = DateTime.Now;
                                    item.UsuarioModificacion = "asignacion_duplicado_v5";
                                    try
                                    {
                                        _unitOfWork.AsignacionAutomaticaRepository.Update(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        _unitOfWork.Rollback();
                                        throw new BadRequestException($"#OPS-CPOsW-003@Error en guardar LogRepository: {ex.Message}");
                                    }
                                    try
                                    {
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        _unitOfWork.Rollback();
                                        throw new BadRequestException($"#OPS-CPOsW-002@Error en guardar LogRepository: {ex.Message}");
                                    }
                                }
                                continue;
                            }
                        }
                        var rpta = CrearOportunidadPortalWeb(item.Id);
                        var IdOportunidad = _unitOfWork.AsignacionAutomaticaRepository.FirstById(item.Id).IdOportunidad;

                        if (IdOportunidad != null)
                        {

                            int nroIntentos = 0;
                            bool flagValidado = false;

                            while (!flagValidado && nroIntentos < 10)
                            {
                                try
                                {
                                    var respuesta = ValidarCasosOportunidad(IdOportunidad ?? 0, item.Id, true);

                                    flagValidado = true;
                                }
                                catch (Exception ex)
                                {
                                    nroIntentos++;

                                    Thread.Sleep(3000);
                                }
                            }

                            if (nroIntentos == 10)
                            {
                                _unitOfWork.LogRepository.Add(new Log { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadesPortalWeb", Parametros = $"IdOportunidad={IdOportunidad}&IdAsignacionAutomatica={item.Id}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = "IntegraV5", UsuarioModificacion = "IntegraV5", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                try
                                {
                                    _unitOfWork.Commit();
                                }
                                catch (Exception ex)
                                {
                                    _unitOfWork.Rollback();
                                    throw new BadRequestException($"#OPS-CPOsW-001@Error en guardar LogRepository: {ex.Message}");
                                }
                            }
                        }
                    }
                    catch
                    {
                        _unitOfWork.Rollback();
                        continue;
                    }
                }
                return "Se proceso correctamente";
            }
            catch (Exception e)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Margiory Ramirez
        /// Versión: 1.0
        /// <summary>
        /// Crear la oportunidad segun el IdAsignacionAutomatica enviado
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>
        public int CrearOportunidadPortalWeb(int idAsignacionAutomatica)
        {
            try
            {
                IAsignacionAutomaticaService asignacionAutomaticaService = new AsignacionAutomaticaService(_unitOfWork);
                var objAsignacionAutomatica = _mapper.Map<AsignacionAutomatica>(_unitOfWork.AsignacionAutomaticaRepository.FirstById(idAsignacionAutomatica));
                if (objAsignacionAutomatica != null && objAsignacionAutomatica.Validado == false && objAsignacionAutomatica.Corregido == false)
                {
                    var hoy = DateTime.Now;
                    var cadena = hoy.DayOfWeek;
                    Dictionary<string, string> dias = new Dictionary<string, string>() {
                        { "Monday", "Lunes" },
                        { "Tuesday", "Martes" },
                        { "Wednesday", "Miercoles" },
                        { "Thursday", "Jueves" },
                        { "Friday", "Viernes" },
                        { "Saturday", "Sabado" },
                        { "Sunday", "Domingo" }
                    };
                    var horario = hoy.TimeOfDay;
                    var dia = dias[cadena.ToString()];
                    var diaDto = _unitOfWork.BloqueHorarioProcesaOportunidadRepository.ObtenerConfiguracion(dia);
                    var horaInicioM = diaDto.HoraInicioM;
                    var horaInicioT = diaDto.HoraInicioT;
                    var horaFinM = diaDto.HoraFinM;
                    var horaFinT = diaDto.HoraFinT;

                    int idTipoCategoriaOrigen = _unitOfWork.TipoCategoriaOrigenRepository.ObtenerTipoCategoriaOrigenID(objAsignacionAutomatica.IdCategoriaOrigen.GetValueOrDefault());

                    if ((diaDto != null && (diaDto.TurnoM && ((horario >= horaInicioM && horario <= horaFinM) || diaDto.TurnoT && (horario >= horaInicioT && horario <= horaFinT)))) || (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)) //16=tipocategoriaorigen->chat1,38=tipocategoriaorigen->chat2
                    {
                        try
                        {
                            asignacionAutomaticaService.AsignacionAutomatica = objAsignacionAutomatica;
                            var listaErrores = asignacionAutomaticaService.Validar();
                            if (!listaErrores.Any())
                            {
                                _oportunidadBo = new OportunidadBoDTO();
                                using (TransactionScope scope = new TransactionScope())
                                {

                                    if (asignacionAutomaticaService.AsignacionAutomatica.IdAlumno == null)
                                        _alumno = new Alumno();
                                    else
                                    {
                                        TAlumno TAlumno = _unitOfWork.AlumnoRepository.FirstById(asignacionAutomaticaService.AsignacionAutomatica.IdAlumno.Value);
                                        _alumno = _mapper.Map<Alumno>(TAlumno);
                                    }
                                    string usuario = "SYSTEMV5";
                                    bool esVentaCruzada = true;
                                    GenerarOportunidad(asignacionAutomaticaService.AsignacionAutomatica, esVentaCruzada, usuario, idTipoCategoriaOrigen, asignacionAutomaticaService.IdClasificacionPersona);
                                    _oportunidadBo.Estado = true;
                                    _oportunidadBo.FechaCreacion = DateTime.Now;
                                    _oportunidadBo.FechaModificacion = DateTime.Now;
                                    _oportunidadBo.UsuarioCreacion = usuario;
                                    _oportunidadBo.UsuarioModificacion = usuario;

                                    _oportunidadBo.Alumno = _alumno;
                                    if (_alumno.Id == 0)
                                    {
                                        CrearOportunidadCrearPersona(ref _oportunidadBo, esVentaCruzada, TipoPersona.Alumno);// Se agrego el flag-venta-cruzada     comentado
                                    }
                                    else
                                    {
                                        CrearOportunidadActualizarPersona(ref _oportunidadBo, esVentaCruzada, TipoPersona.Alumno); // Se agrego el flag-venta-cruzada  cometaando
                                    }
                                    asignacionAutomaticaService.AsignacionAutomatica.Validado = true;
                                    asignacionAutomaticaService.AsignacionAutomatica.Corregido = true;
                                    asignacionAutomaticaService.AsignacionAutomatica.IdOportunidad = _oportunidadBo.Id;
                                    asignacionAutomaticaService.AsignacionAutomatica.IdAlumno = _oportunidadBo.IdAlumno;
                                    asignacionAutomaticaService.AsignacionAutomatica.FechaModificacion = DateTime.Now;
                                    asignacionAutomaticaService.AsignacionAutomatica.UsuarioModificacion = usuario;

                                    IAgendaService agendaService = new AgendaService(_unitOfWork);
                                    // 827 Correo Informacion del Curso Completo
                                    agendaService.EnviarCorreoOportunidadAutomatico(_oportunidadBo.Id, 1967, "Automatico1967");

                                    try
                                    {
                                        _unitOfWork.DetachAll();
                                        _unitOfWork.AsignacionAutomaticaRepository.Update(asignacionAutomaticaService.AsignacionAutomatica);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new BadRequestException($"#OS-COPW-001@Error en actualizar Asignacion Automatica: {ex.Message}");
                                    }
                                    try
                                    {
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new BadRequestException($"#OS-COPW-002@Error en guardar Asignacion Automatica: {ex.Message}");
                                    }
                                    scope.Complete();
                                }
                                try
                                {
                                    var nuevaProbabilidad = _unitOfWork.OportunidadRepository.ObtenerProbabilidadModeloPredictivo(_oportunidadBo.Id);
                                }
                                catch (Exception e)
                                {
                                    _unitOfWork.LogRepository.Add(new Log { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CalcularModeloPredictivo", Parametros = $"{_oportunidadBo.Id}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = "IntegraV5", UsuarioModificacion = "IntegraV5", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                                    _unitOfWork.Commit();
                                }

                                return (_oportunidadBo.Id);
                            }
                            else
                            {

                                foreach (var error in listaErrores)
                                {
                                    error.Estado = true;
                                    error.FechaCreacion = DateTime.Now;
                                    error.FechaModificacion = DateTime.Now;
                                    error.UsuarioCreacion = "system_v5";
                                    error.UsuarioModificacion = "system_v5";
                                    try
                                    {
                                        _unitOfWork.AsignacionAutomaticaErrorRepository.Add(error);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new BadRequestException($"#OS-COPW-011@Error en actualizar Asignacion Automatica: {ex.Message}");
                                    }
                                    try
                                    {
                                        _unitOfWork.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new BadRequestException($"#OS-COPW-010@Error en guardar Asignacion Automatica: {ex.Message}");
                                    }
                                }
                                asignacionAutomaticaService.AsignacionAutomatica.Validado = true;
                                asignacionAutomaticaService.AsignacionAutomatica.Corregido = false;
                                asignacionAutomaticaService.AsignacionAutomatica.FechaModificacion = DateTime.Now;
                                asignacionAutomaticaService.AsignacionAutomatica.UsuarioModificacion = "SYSTEM_V5";
                                try
                                {
                                    _unitOfWork.AsignacionAutomaticaRepository.Update(asignacionAutomaticaService.AsignacionAutomatica);
                                }
                                catch (Exception ex)
                                {
                                    throw new BadRequestException($"#OS-COPW-009@Error en actualizar Asignacion Automatica: {ex.Message}");
                                }
                                try
                                {
                                    _unitOfWork.Commit();
                                }
                                catch (Exception ex)
                                {
                                    throw new BadRequestException($"#OS-COPW-008@Error en guardar Asignacion Automatica: {ex.Message}");
                                }
                                throw new BadRequestException("#OPS-COPW-007@Errores al Validar La Oportunidad");
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Se elimino el alumno") || ex.Message.Contains("No se creo el persona clasificacion"))
                            {
                                asignacionAutomaticaService.AsignacionAutomatica.Validado = false;
                                asignacionAutomaticaService.AsignacionAutomatica.Corregido = false;
                                asignacionAutomaticaService.AsignacionAutomatica.FechaModificacion = DateTime.Now;
                                asignacionAutomaticaService.AsignacionAutomatica.UsuarioModificacion = "SYSTEM_V5";
                                try
                                {
                                    _unitOfWork.AsignacionAutomaticaRepository.Update(asignacionAutomaticaService.AsignacionAutomatica);
                                }
                                catch (Exception ex2)
                                {
                                    throw new BadRequestException($"#OS-COPW-006@Error en actualizar Asignacion Automatica: {ex2.Message}");
                                }
                                try
                                {
                                    _unitOfWork.Commit();
                                }
                                catch (Exception ex2)
                                {
                                    throw new BadRequestException($"#OS-COPW-005@Error en guardar Asignacion Automatica: {ex2.Message}");
                                }
                            }
                            if (ex.Message.Equals("Se Actualizo contacto pero NO se creo la OPORTUNIDAD porque tiene Un BNC del tipo lanzamiento"))
                            {
                                asignacionAutomaticaService.AsignacionAutomatica.Validado = true;
                                asignacionAutomaticaService.AsignacionAutomatica.Corregido = false;
                                asignacionAutomaticaService.AsignacionAutomatica.Estado = false;
                                asignacionAutomaticaService.AsignacionAutomatica.FechaModificacion = DateTime.Now;
                                asignacionAutomaticaService.AsignacionAutomatica.UsuarioModificacion = "SYSTEM_V5";
                                try
                                {
                                    _unitOfWork.AsignacionAutomaticaRepository.Update(asignacionAutomaticaService.AsignacionAutomatica);
                                }
                                catch (Exception ex2)
                                {
                                    throw new BadRequestException($"#OS-COPW-004@Error en actualizar Asignacion Automatica: {ex2.Message}");
                                }
                                try
                                {
                                    _unitOfWork.Commit();
                                }
                                catch (Exception ex2)
                                {
                                    throw new BadRequestException($"#OS-COPW-003@Error en guardar Asignacion Automatica: {ex2.Message}");
                                }
                            }
                            throw;
                        }
                    }
                    else
                    {
                        throw new BadRequestException("#OPS-COPW-002@No se encuentra en horario para crear Oportunidades");
                    }
                }
                else
                {
                    throw new BadRequestException("#OPS-COPW-001@No se encuentra valor en Asignacion Automatica o ya fue Validado ");
                }

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }




        /// Margiory
        /// <summary>
        /// Genera oportunidad desde el registro de asignacion automatica
        /// </summary>
        /// <param name="objAsignacionAutomatica">Objeto de clase objAsignacionAutomatica</param>
        /// <param name="ventaCruzada">Flag para determinar si viene de venta cruzada</param>
        /// <param name="usuario">Usuario que realiza la creacion de la oportunidad</param>
        /// <param name="idTipoCategoriaOrigen">Id del tipo de categoria origen (PK de la tabla mkt.T_TipoCategoriaOrigen)</param>
        public void GenerarOportunidad(AsignacionAutomatica objAsignacionAutomatica, bool ventaCruzada, string usuario, int idTipoCategoriaOrigen, int idClasificacionPersona)
        {

            this._oportunidad = new Oportunidad();
            const int idAsesorAsignacionAutomatica = 125;
            var tipoDatoLanzamiento = ValorEstatico.IdTipoDatoLanzamiento;

            if (objAsignacionAutomatica.IdAlumno != 0 && _alumno.Asociado == true) //si el alumno esta en finanzas no actualizamos todos sus campos
            {
                // Reemplazamos los nuevos campos del registro
                _alumno.IdIndustria = objAsignacionAutomatica.IdIndustria.GetValueOrDefault() != 0 ? objAsignacionAutomatica.IdIndustria : _alumno.IdIndustria;
                _alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion.GetValueOrDefault() != 0 ? objAsignacionAutomatica.IdAreaFormacion : _alumno.IdAformacion;
                _alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo.GetValueOrDefault() != 0 ? objAsignacionAutomatica.IdAreaTrabajo : _alumno.IdAtrabajo;
                _alumno.IdCargo = objAsignacionAutomatica.IdCargo.GetValueOrDefault() != 0 ? objAsignacionAutomatica.IdCargo : _alumno.IdCargo;

                _alumno.Celular = objAsignacionAutomatica.Celular;
                _alumno.IdCodigoRegionCiudad = (objAsignacionAutomatica.IdCiudad == -1 || objAsignacionAutomatica.IdCiudad == 0) ? _alumno.IdCodigoRegionCiudad : objAsignacionAutomatica.IdCiudad;
                _alumno.IdCodigoPais = (objAsignacionAutomatica.IdPais == -1 || objAsignacionAutomatica.IdPais == 0) ? _alumno.IdCodigoPais : objAsignacionAutomatica.IdPais;
                _alumno.Email2 = objAsignacionAutomatica.Email;
                _alumno.UsuarioModificacion = "SYSTEMV5";
                _alumno.FechaModificacion = DateTime.Now;
            }
            else if (objAsignacionAutomatica.IdAlumno != 0 && objAsignacionAutomatica.IdAlumno != null)
            {
                // Reemplazamos los nuevos campos del registro
                _alumno.IdIndustria = objAsignacionAutomatica.IdIndustria != 0 && objAsignacionAutomatica.IdIndustria != null ? objAsignacionAutomatica.IdIndustria : _alumno.IdIndustria;
                _alumno.Nombre1 = objAsignacionAutomatica.Nombre1;
                _alumno.Nombre2 = objAsignacionAutomatica.Nombre2;
                _alumno.ApellidoMaterno = objAsignacionAutomatica.ApellidoMaterno;
                _alumno.ApellidoPaterno = objAsignacionAutomatica.ApellidoPaterno;
                _alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion != 0 && objAsignacionAutomatica.IdAreaFormacion != null ? objAsignacionAutomatica.IdAreaFormacion : _alumno.IdAformacion;
                _alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo != 0 && objAsignacionAutomatica.IdAreaTrabajo != null ? objAsignacionAutomatica.IdAreaTrabajo : _alumno.IdAtrabajo;
                _alumno.IdCargo = objAsignacionAutomatica.IdCargo != 0 && objAsignacionAutomatica.IdCargo != null ? objAsignacionAutomatica.IdCargo : _alumno.IdCargo;
                _alumno.Celular = objAsignacionAutomatica.Celular;
                _alumno.IdCodigoRegionCiudad = objAsignacionAutomatica.IdCiudad;
                _alumno.IdCodigoPais = objAsignacionAutomatica.IdPais;
                _alumno.Telefono = objAsignacionAutomatica.Telefono;
                _alumno.Email1 = objAsignacionAutomatica.Email;
                _alumno.Email2 = objAsignacionAutomatica.Email;
                _alumno.Estado = true;
                _alumno.UsuarioModificacion = "SYSTEMV5";
                _alumno.FechaModificacion = DateTime.Now;
            }
            else
            {
                //Reemplazamos los nuevos campos del registro
                _alumno.IdIndustria = objAsignacionAutomatica.IdIndustria;
                _alumno.Nombre1 = objAsignacionAutomatica.Nombre1;
                _alumno.Nombre2 = objAsignacionAutomatica.Nombre2;
                _alumno.ApellidoMaterno = objAsignacionAutomatica.ApellidoMaterno;
                _alumno.ApellidoPaterno = objAsignacionAutomatica.ApellidoPaterno;
                _alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion;
                _alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo;
                _alumno.IdCargo = objAsignacionAutomatica.IdCargo;
                _alumno.Celular = objAsignacionAutomatica.Celular;
                _alumno.IdCodigoRegionCiudad = objAsignacionAutomatica.IdCiudad;
                _alumno.IdCodigoPais = objAsignacionAutomatica.IdPais;
                _alumno.Telefono = objAsignacionAutomatica.Telefono;
                _alumno.Email1 = objAsignacionAutomatica.Email;
                _alumno.Email2 = objAsignacionAutomatica.Email;
                _alumno.Estado = true;
                _alumno.UsuarioCreacion = "GENOPOV5";
                _alumno.UsuarioModificacion = "SYSTEMV5";
                _alumno.FechaModificacion = DateTime.Now;
                _alumno.FechaCreacion = DateTime.Now;
            }
            //Generamos la Oportunidad
            //Si el tipo de dato no es LANZAMIENTO, lo asignamos al asesor de Asignacion Automatica

            _oportunidadBo.IdPersonalAsignado = idAsesorAsignacionAutomatica;
            _oportunidadBo.IdFaseOportunidad = objAsignacionAutomatica.IdFaseOportunidad == null ? 0 : objAsignacionAutomatica.IdFaseOportunidad.Value;
            _oportunidadBo.IdCentroCosto = objAsignacionAutomatica.IdCentroCosto == null ? 0 : objAsignacionAutomatica.IdCentroCosto.Value;
            _oportunidadBo.IdOrigen = objAsignacionAutomatica.IdOrigen == null ? 0 : objAsignacionAutomatica.IdOrigen.Value;
            _oportunidadBo.IdTipoDato = objAsignacionAutomatica.IdTipoDato == null ? 0 : objAsignacionAutomatica.IdTipoDato.Value;
            _oportunidadBo.IdConjuntoAnuncio = objAsignacionAutomatica.IdConjuntoAnuncio == null ? 0 : objAsignacionAutomatica.IdConjuntoAnuncio.Value;

            _oportunidadBo.IdAnuncioFacebook = objAsignacionAutomatica.IdAnuncioFacebook;

            _oportunidadBo.IdTiempoCapacitacion = objAsignacionAutomatica.IdTiempoCapacitacion == null ? 0 : objAsignacionAutomatica.IdTiempoCapacitacion.Value;
            _oportunidadBo.IdPagina = objAsignacionAutomatica.IdPagina == null ? 0 : objAsignacionAutomatica.IdPagina.Value;
            _oportunidadBo.FechaRegistroCampania = objAsignacionAutomatica.FechaRegistroCampania.Value;
            _oportunidadBo.IdFaseOportunidadPortal = objAsignacionAutomatica.IdFaseOportunidadPortal == null ? Guid.Empty : objAsignacionAutomatica.IdFaseOportunidadPortal;
            _oportunidadBo.IdCampaniaScoring = objAsignacionAutomatica.IdCampaniaScoring == null ? 0 : objAsignacionAutomatica.IdCampaniaScoring.Value;
            _oportunidadBo.UltimaFechaProgramada = objAsignacionAutomatica.FechaProgramada;
            _oportunidadBo.UltimoComentario = "Sin Comentario";
            _oportunidadBo.IdAlumno = _alumno.Id == 0 ? _oportunidadBo.IdAlumno : _alumno.Id;
            _oportunidadBo.IdCategoriaOrigen = objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value;
            _oportunidadBo.IdInteraccionFormulario = objAsignacionAutomatica.IdInteraccionFormulario == null ? 0 : objAsignacionAutomatica.IdInteraccionFormulario.Value;
            _oportunidadBo.UrlOrigen = objAsignacionAutomatica.UrlOrigen;
            _oportunidadBo.IdSubCategoriaDato = objAsignacionAutomatica.IdSubCategoriaDato == null ? 0 : objAsignacionAutomatica.IdSubCategoriaDato.Value;
            _oportunidadBo.IdClasificacionPersona = idClasificacionPersona;
            _oportunidadBo.IdPersonalAreaTrabajo = 8;


            if (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)//16=>TipoCategoriaOrigen = Chat , 38=>TipoCategoriaOrigen = Chat2
            {
                //obtenemos el idasesor asignado a ese programa por su cc segun chat
                var asignadoChat = _unitOfWork.OportunidadRepository.ObtenerIdPersonalAsignadoChat(_oportunidadBo.IdAlumno.Value, _oportunidadBo.IdCentroCosto.Value);
                if (asignadoChat == null || asignadoChat.IdAsesor == 0)
                {
                    //nada
                }
                else
                {
                    _oportunidadBo.IdPersonalAsignado = asignadoChat.IdAsesor;
                }
            }
        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin R.
        /// Fecha: 04/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Valida los casos en los que puede convertirse en OD u OM la oportunidad
        /// </summary>
        /// <param name="IdOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="IdAsignacionAutomatica">Id de la asignacion automatica (PK de la tabla 6613)</param>
        /// <param name="FlagPortalWeb">Flag para determinar si el dato viene del portal web</param>
        /// <returns>Json</returns>

        public object ValidarCasosOportunidad(int IdOportunidad, int IdAsignacionAutomatica, bool FlagPortalWeb)
        {
            try
            {
                var _repOportunidad = _unitOfWork.OportunidadRepository;
                var _repProgramaGeneralPuntoCorte = _unitOfWork.ProgramaGeneralPuntoCorteRepository;
                var _repPespecifico = _unitOfWork.PEspecificoRepository;
                var _repAsignacionAutomatica = _unitOfWork.AsignacionAutomaticaRepository;
                var _repCategoriaOrigen = _unitOfWork.CategoriaOrigenRepository;
                var _repTipoCategoriaOrigen = _unitOfWork.TipoCategoriaOrigenRepository;
                var _repConfiguracionDatoRemarketing = _unitOfWork.ConfiguracionDatoRemarketingRepository;
                var _repBloqueHorarioProcesaOportunidad = _unitOfWork.BloqueHorarioProcesaOportunidadRepository;
                var _repModeloPredictivoProbabilidad = _unitOfWork.ModeloPredictivoProbabilidadRepository;
                var _repProbabilidadRegistroPw = _unitOfWork.ProbabilidadRegistroPwRepository;
                var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
                var _repOportunidadRemarketingAgenda = _unitOfWork.OportunidadRemarketingAgendaRepository;
                var _repAlumno = _unitOfWork.AlumnoRepository;
                // Valores Generales
                bool validacionCorrecta = true;
                var oportunidad = _mapper.Map<OportunidadBoDTO>(_repOportunidad.ObtenerPorId(IdOportunidad));
                var asesorAsociado = new ResultadosDTO();
                int probabilidadActual = 0;
                int idTabRedireccion = 0;
                var diaDto = new BloqueHorarioProcesaOportunidad();

                // Validacion punto corte por programa
                int idPGeneral = _repPespecifico.FirstBy(w => w.IdCentroCosto == oportunidad.IdCentroCosto.Value).IdProgramaGeneral == null ? 0 : _repPespecifico.FirstBy(w => w.IdCentroCosto == oportunidad.IdCentroCosto.Value).IdProgramaGeneral.Value;
                var idCodigoPais = _repAlumno.FirstById(oportunidad.IdAlumno.Value).IdCodigoPais;
                var programaGeneralPuntoCorte = _repProgramaGeneralPuntoCorte.FirstBy(w => w.IdProgramaGeneral == idPGeneral && w.IdPais == idCodigoPais);
                if (programaGeneralPuntoCorte == null)
                {
                    programaGeneralPuntoCorte = _repProgramaGeneralPuntoCorte.FirstBy(w => w.IdProgramaGeneral == idPGeneral && w.IdPais == null);
                }
                // Si no tiene probabilidad el programa de la oportunidad
                if (programaGeneralPuntoCorte == null)
                {
                    #region Marcado validacion correcta
                    try
                    {
                        _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                    }
                    catch (Exception)
                    {
                    }
                    #endregion

                    return (true);
                }

                ////////////////////////////////////////////////////////////// SI VIENE DESDE PORTAL WEB /////////////////////////////////////////////////////////////////////////
                if (FlagPortalWeb)
                {
                    var objAsignacionAutomatica = _repAsignacionAutomatica.FirstById(IdAsignacionAutomatica);

                    int idTipoCategoriaOrigen = _repTipoCategoriaOrigen.ObtenerTipoCategoriaOrigenID(objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value);
                    //var objCategoria = _repCategoriaOrigenRep.FirstById(objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value);

                    Dictionary<string, string> dias = new Dictionary<string, string>() {
                        { "Monday", "Lunes" },
                        { "Tuesday", "Martes" },
                        { "Wednesday", "Miercoles" },
                        { "Thursday", "Jueves" },
                        { "Friday", "Viernes" },
                        { "Saturday", "Sabado" },
                        { "Sunday", "Domingo" }
                    };
                    var hoy = DateTime.Now;
                    var cadena = hoy.DayOfWeek;
                    var dia = dias[cadena.ToString()];
                    diaDto = _repBloqueHorarioProcesaOportunidad.ObtenerConfiguracion(dia);

                    //probabilidadActual = _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual == null ? 0 : _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual.Value;

                    //Obtencion del valor 0-4 segun al probabilidad

                    var probabilidaNueva = _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad) == null ? 0 : _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad).Probabilidad;

                    if (probabilidaNueva < programaGeneralPuntoCorte.PuntoCorteAlta)
                    {
                        probabilidadActual = 2;//MEDIA
                    }
                    else if (probabilidaNueva >= programaGeneralPuntoCorte.PuntoCorteAlta && probabilidaNueva < programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                    {
                        probabilidadActual = 3;//ALTA
                    }
                    else if (probabilidaNueva >= programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                    {
                        probabilidadActual = 4;//MUY ALTA
                    }
                    else
                    {
                        probabilidadActual = 1;//SIN PROBABILIDAD
                    }

                    asesorAsociado.IdAsesor = oportunidad.IdPersonalAsignado.Value;

                    int probabilidad = probabilidadActual;

                    // Verificar coincidencia configuracion remarketing
                    try
                    {
                        if (objAsignacionAutomatica.IdTipoDato.HasValue && objAsignacionAutomatica.IdSubCategoriaDato.HasValue)
                            idTabRedireccion = _repConfiguracionDatoRemarketing.ObtenerTabRedireccionRemarketing(objAsignacionAutomatica.IdTipoDato.Value, objAsignacionAutomatica.IdSubCategoriaDato.Value, probabilidad);
                    }
                    catch (Exception ex)
                    {
                        idTabRedireccion = 0;
                    }

                    // Si el contacto ya existe
                    if (objAsignacionAutomatica.IdAlumno > 0)
                    {
                        asesorAsociado.IdAsesor = asesorAsociado.IdAsesor == 125 || asesorAsociado.IdAsesor == 0 ? this.ObtenerAsesorParaCentroCosto(objAsignacionAutomatica.IdCentroCosto.Value, objAsignacionAutomatica.IdSubCategoriaDato.Value, objAsignacionAutomatica.IdPais.Value, probabilidad, idTabRedireccion) : asesorAsociado.IdAsesor;
                    }
                    else
                    {
                        asesorAsociado.IdAsesor = this.ObtenerAsesorParaCentroCosto(objAsignacionAutomatica.IdCentroCosto.Value, objAsignacionAutomatica.IdSubCategoriaDato.Value, objAsignacionAutomatica.IdPais.Value, probabilidad, idTabRedireccion);
                    }

                    if (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)//16=>TipoCategoriaOrigen = Chat1,38=>TipoCategoriaOrigen = Chat2
                    {
                        asesorAsociado.IdAsesor = oportunidad.IdPersonalAsignado.Value;
                    }
                    else
                    {
                        // Aqui si se asigna el asesor
                        oportunidad.IdPersonalAsignado = asesorAsociado.IdAsesor == 0 ? oportunidad.IdPersonalAsignado : asesorAsociado.IdAsesor;
                    }
                }
                ////////////////////////////////////////////////////////////// FIN SI VIENE DESDE EL PORTAL WEB /////////////////////////////////////////////////////////////////////////

                bool enviarNotificacion = true; // para que no se envie notificacion a la agenda si cerramos la oportunidad en OM, OD
                                                // Valores Precalculados

                int idCentroCosto = oportunidad.IdCentroCosto.Value;
                int idCategoriaOrigen = oportunidad.IdCategoriaOrigen.Value;

                int idProgramaGeneral = _repPespecifico.FirstBy(w => w.IdCentroCosto == idCentroCosto).IdProgramaGeneral == null ? 0 : _repPespecifico.FirstBy(w => w.IdCentroCosto == idCentroCosto).IdProgramaGeneral.Value;

                //Obtencion del valor 0-4 segun al probabilidad
                int idProbRegisPW;
                var probabilidaNuevaComparacion = _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad) == null ? 0 : _repModeloPredictivoProbabilidad.FirstBy(w => w.IdOportunidad == IdOportunidad).Probabilidad;
                if (probabilidaNuevaComparacion < programaGeneralPuntoCorte.PuntoCorteAlta)
                {
                    idProbRegisPW = 2;// Media
                }
                else if (probabilidaNuevaComparacion >= programaGeneralPuntoCorte.PuntoCorteAlta && probabilidaNuevaComparacion < programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                {
                    idProbRegisPW = 3;// Alta
                }
                else if (probabilidaNuevaComparacion >= programaGeneralPuntoCorte.PuntoCorteMuyAlta)
                {
                    idProbRegisPW = 4;// Muy alta
                }
                else
                {
                    idProbRegisPW = 1;// Sin probabilidad
                }

                //int idProbRegisPW = _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual == null ? 0 : _repModeloDataMining.FirstBy(w => w.IdOportunidad == IdOportunidad).IdProbabilidadRegistroPwActual.Value;
                string pesoDescripcion = _repProbabilidadRegistroPw.FirstBy(w => w.Id == idProbRegisPW).Nombre;


                //Peso de la Probabilidad de la Oportunidad
                int pesoOportunidad = pesoDescripcion == "Muy Alta" ? 2 : ((pesoDescripcion == "Alta" || pesoDescripcion == "Media") ? 1 : 0);
                //Peso de la Categoria de la Oportunidad
                int pesoCategoriaOportunidad = _repCategoriaOrigen.FirstBy(w => w.Id == idCategoriaOrigen).Meta;

                var objetoAlumno = _repAlumno.FirstById(oportunidad.IdAlumno.Value);
                /*Caracter especial para evitar registros coincidentes con celular vacio*/
                objetoAlumno.Celular = string.IsNullOrEmpty(objetoAlumno.Celular) ? "-|!x!|-" : objetoAlumno.Celular.Trim();
                ///////////////////////////////////// CASO 1:Valida si hay oportunidades en IS o M //////////////////////////////////////////////////////
                var listaISM = _repOportunidad.ValidarOportunidadesISM(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaISM.Count() > 0)//Si hay un IS o M me tengo que cerrar
                {
                    int[] listaOportunidadesISM = new int[] { oportunidad.Id };
                    this.CerrarOportunidadesOD(listaOportunidadesISM, "System Duplicado");//FALTA IMPLEMENTAR

                    #region Marcado validacion correcta
                    try
                    {
                        _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                    }
                    catch (Exception)
                    {
                    }
                    #endregion

                    return true;
                }
                //////////////////////////////////// CASO 2:Valida si hay opotunidades con mayor Probabilidad //////////////////////////////////////////
                var listaProbabilidades = _repOportunidad.ValidarOportunidadesProbabilidad(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaProbabilidades.Count() > 0)//Si hay oportunidades con el mismo alumno y del mismo programa
                {
                    if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)// Si alguno con mayor probabilidad que el actual me tengo que cerrar
                    {
                        if (listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault() != null)//mayor  probabilidad del mismo programa
                        {
                            if (listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)
                            {
                                int[] listaOportunidadesProbabilidadesOD = new int[] { oportunidad.Id };
                                this.CerrarOportunidadesOD(listaOportunidadesProbabilidadesOD, "Sys Duplicado Prob");//FALTA IMPLEMENTAR

                                #region Marcado validacion correcta
                                try
                                {
                                    _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                                }
                                catch (Exception)
                                {
                                }
                                #endregion

                                return true;
                            }

                        }
                        else if (listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault() != null)//mayor  probabilidad de diferente programa
                        {
                            if (listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral).OrderByDescending(z => z.PesoProbabilidad).FirstOrDefault().PesoProbabilidad > pesoOportunidad)
                            {
                                int[] listaOportunidadesProbabilidadesOM = new int[] { oportunidad.Id };
                                this.CerrarOportunidadesOM(listaOportunidadesProbabilidadesOM, "Sys Duplicado Prob");

                                #region Marcado validacion correcta
                                try
                                {
                                    _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                                }
                                catch (Exception)
                                {
                                }
                                #endregion

                                return true;
                            }
                        }
                    }
                    else if (listaProbabilidades.OrderByDescending(w => w.PesoProbabilidad).FirstOrDefault().PesoProbabilidad == pesoOportunidad)//Si tienen una probabilidad igual que el actual no se hace nada
                    {
                        //nada
                    }
                    else//Significa son de menor probablidad que el actual y deben cerrarse
                    {


                        int[] listaOportunidadesProbabilidadesOD = new int[listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral).ToList().Count()];
                        int[] listaOportunidadesProbabilidadesOM = new int[listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral).ToList().Count()];
                        int contador1 = 0;

                        ///listaProbabilidades: itera en la lista de oportunidades con mayor probabilidad que la actual. 
                        foreach (var item in listaProbabilidades.Where(w => w.IdProgramaGeneral == idProgramaGeneral))
                        {
                            listaOportunidadesProbabilidadesOD[contador1] = item.IdOportunidad;
                            contador1++;


                        }
                        int contador2 = 0;
                        foreach (var item in listaProbabilidades.Where(w => w.IdProgramaGeneral != idProgramaGeneral))
                        {
                            listaOportunidadesProbabilidadesOM[contador2] = item.IdOportunidad;
                            contador2++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesProbabilidadesOD, "Sys Duplicado Prob");//FALTA IMPLEMENTAR
                        this.CerrarOportunidadesOM(listaOportunidadesProbabilidadesOM, "Sys Duplicado Prob");//FALTA IMPLEMENTAR

                    }
                }

                //////////////////////////////////Antes de pasar a los demas casos validamos el id del asesor si es  125:Asignacion Automatica////////
                if (FlagPortalWeb)
                {
                    if (oportunidad.IdPersonalAsignado != 125)
                    {
                        //actualizamos el asesor en la oportunidad
                        oportunidad.IdPersonalAsignado = asesorAsociado.IdAsesor;
                        oportunidad.FechaModificacion = DateTime.Now;
                        var oportunidad2 = _mapper.Map<Oportunidad>(oportunidad);
                        _repOportunidad.Update(oportunidad2);

                        #region Envio whatsapp personal asignado

                        var parametros = "";
                        try
                        {
                            EnvioWhatsappAsignacionDTO datos = new EnvioWhatsappAsignacionDTO();
                            var Serializer = new JavaScriptSerializer();
                            var UrlBase = "https://integrav5-servicios-respaldo.bsginstitute.com/api/WhatsAppMensajes/EnvioWhatsAsignacion";
                            //var UrlBase = "https://localhost:44395/api/WhatsAppMensajes/EnvioWhatsAsignacion";

                            datos.idOportunidad = oportunidad.Id;
                            datos.idPais = (int)idCodigoPais;
                            datos.idPersonal = asesorAsociado.IdAsesor;
                            datos.IdCategoriaOrigen = (int)oportunidad.IdCategoriaOrigen;
                            var serializedResult = Serializer.Serialize(datos);
                            parametros = serializedResult;

                            var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                            http.Accept = "application/json";
                            http.ContentType = "application/json";
                            http.Method = "POST";

                            string parsedContent = serializedResult;
                            ASCIIEncoding encoding = new ASCIIEncoding();
                            Byte[] bytes = encoding.GetBytes(parsedContent);

                            Stream newStream = http.GetRequestStream();
                            newStream.Write(bytes, 0, bytes.Length);
                            newStream.Close();

                            var response = http.GetResponse();

                        }
                        catch (Exception e)
                        {
                            _unitOfWork.LogRepository.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "EnvioWhstAsesos", Parametros = $"{parametros}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                        }



                        #endregion
                        //modificacion el log inicial (para actualizar el asesor del primer log )
                        string usuario = "SystemRealTime";
                        var ultimoLog = _repOportunidadLog.GetBy(w => w.IdOportunidad == IdOportunidad).OrderByDescending(w => w.FechaLog).FirstOrDefault();
                        ultimoLog.IdPersonalAsignado = oportunidad.IdPersonalAsignado;
                        ultimoLog.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                        ultimoLog.IdFaseOportunidadAnt = oportunidad.IdFaseOportunidad;
                        ultimoLog.IdCentroCosto = oportunidad.IdCentroCosto;
                        ultimoLog.IdOrigen = oportunidad.IdOrigen;
                        ultimoLog.IdTipoDato = oportunidad.IdTipoDato;
                        ultimoLog.UsuarioModificacion = usuario;
                        ultimoLog.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
                        _repOportunidadLog.Update(ultimoLog);


                        this.ActualizarAsignacionOportunidad(oportunidad.Id, oportunidad.IdPersonalAsignado.Value, oportunidad.IdCentroCosto.Value, oportunidad.IdAlumno.Value, usuario);
                    }
                }
                //////////////////////////////////// CASO 3:Valida si hay opotunidades con mayor Categoria //////////////////////////////////////////
                var listaCategorias = _repOportunidad.ValidarOportunidadesCategoria(oportunidad.IdAlumno.Value, oportunidad.Id, objetoAlumno.Celular);
                //OM-OD
                var listaCategoriasOM = listaCategorias.Where(w => w.IdProgramaGeneral != idProgramaGeneral).ToList();
                var listaCategoriasOD = listaCategorias.Where(w => w.IdProgramaGeneral == idProgramaGeneral).ToList();
                if (listaCategoriasOM.Count() > 0)
                {
                    if (listaCategoriasOM.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                    {
                        if (oportunidad.IdPersonalAsignado != 125)
                            enviarNotificacion = false;

                        int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                        this.CerrarOportunidadesOM(listaOportunidadesCategorias, "UsuarioOM");

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception)
                        {
                        }
                        #endregion

                        return true;
                    }
                    else if (listaCategoriasOD.Count() > 0)
                    {
                        if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                        {
                            if (oportunidad.IdPersonalAsignado != 125)
                                enviarNotificacion = false;

                            int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                            this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR

                            #region Marcado validacion correcta
                            try
                            {
                                _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                            }
                            catch (Exception ex)
                            {
                            }
                            #endregion

                            return true;
                        }
                        else
                        {
                            //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                            oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                            oportunidad.FechaModificacion = DateTime.Now;
                            oportunidad.UsuarioModificacion = "System";
                            var oportunidad2 = _mapper.Map<Oportunidad>(oportunidad);
                            _repOportunidad.Update(oportunidad2);

                            //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                            var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                            if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                            {
                                this.ReasignarOportunidadesRef(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                                oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                            }
                            //Mandamos la lista de oportunidades anteriores a Cerrar OD
                            int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                            int contador = 0;
                            foreach (var item in listaCategoriasOD)
                            {
                                listaOportunidadesCategorias[contador] = item.IdOportunidad;
                                contador++;
                            }
                            this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                        }
                    }
                }
                else if (listaCategoriasOD.Count() > 0)
                {
                    if (listaCategoriasOD.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)
                    {
                        if (oportunidad.IdPersonalAsignado != 125)
                            enviarNotificacion = false;

                        int[] listaOportunidadesCategorias = new int[] { oportunidad.Id };
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return true;
                    }
                    else
                    {
                        //Se actualiza la fase de la oportunidad actual con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = listaCategoriasOD.FirstOrDefault().IdFaseOportunidad;
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";

                        var oportunidad2 = _mapper.Map<Oportunidad>(oportunidad);
                        _repOportunidad.Update(oportunidad2);

                        //Se reasigna la oportunidad actual  al asesor de la oportunidad anterior en caso las anteriores no sean BNC solo (IT,RN)
                        var oportunidadAnterior = listaCategoriasOD.Where(s => s.IdPersonalAsignado != 125).FirstOrDefault();
                        if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != 0 && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado && oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                        {

                            this.ReasignarOportunidadesRef(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                            oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                        }
                        //Mandamos la lista de oportunidades anteriores a Cerrar OD
                        int[] listaOportunidadesCategorias = new int[listaCategoriasOD.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasOD)
                        {
                            listaOportunidadesCategorias[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesCategorias, "System Duplicado");//FALTA IMPLEMENTAR
                    }

                }
                //////////////////////////////////// CASO 4:Valida si hay opotunidades con mayor Categoria en Fase IP ////////////////////////////////
                var listaCategoriasIPS = _repOportunidad.ValidarOportunidadesCategoriaIPMismoPG(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaCategoriasIPS.Count() > 0)
                {
                    if (listaCategoriasIPS.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                    {
                        //primero me reasigno
                        this.ReasignarOportunidadesRef(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                        oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;
                        enviarNotificacion = false;
                        int[] listaOportunidadesCategoriasIP = new int[] { oportunidad.Id };
                        //segundo cerrarme
                        this.CerrarOportunidadesOD(listaOportunidadesCategoriasIP, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return true;
                    }
                    else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                    {
                        //primero me  reasigno
                        this.ReasignarOportunidadesRef(ref oportunidad, listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                        oportunidad.IdPersonalAsignado = listaCategoriasIPS.FirstOrDefault().IdPersonalAsignado;

                        //segundo cierro la otras IPS
                        int[] listaOportunidadesIPCerrar = new int[listaCategoriasIPS.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasIPS)
                        {
                            listaOportunidadesIPCerrar[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOD(listaOportunidadesIPCerrar, "Sys duplicadoIP");//FALTA IMPLEMENTAR

                        //tercero me paso a IP
                        //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = 8;//IP
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        var oportunidad2 = _mapper.Map<Oportunidad>(oportunidad);
                        _repOportunidad.Update(oportunidad2);

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return true;
                    }
                }
                //////////////////////////////////// CASO 5:Valida si hay opotunidades con mayor Categoria en Fase IP de Otros Programas///////////////
                var listaCategoriasIPSPGDiferente = _repOportunidad.ValidarOportunidadesCategoriaIPDiferentePG(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaCategoriasIPSPGDiferente.Count() > 0)
                {
                    if (listaCategoriasIPSPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria >= pesoCategoriaOportunidad)//Si hay alguno con mayor o igual peso de categoria que el actual
                    {
                        //primero me reasigno
                        this.ReasignarOportunidadesRef(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, false);//NO ENVIO CORREO PORQUE SE VA HA CERRAR
                        oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;
                        enviarNotificacion = false;
                        int[] listaOportunidadesCategoriasIPPGDiferente = new int[] { oportunidad.Id };
                        //segundo cerrarme
                        this.CerrarOportunidadesOD(listaOportunidadesCategoriasIPPGDiferente, "UsuarioOM");//FALTA IMPLEMENTAR

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return true;
                    }
                    else//No hay uno con mayor peso de categoria que el actual por ende me reasigno,cierro las demas IPS y luego me paso a IP
                    {
                        //primero me  reasigno
                        this.ReasignarOportunidadesRef(ref oportunidad, listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado, true);//SI ENVIO CORREO
                        oportunidad.IdPersonalAsignado = listaCategoriasIPSPGDiferente.FirstOrDefault().IdPersonalAsignado;

                        //segundo cierro la otras IPS
                        int[] listaOportunidadesIPPGDiferenteCerrar = new int[listaCategoriasIPSPGDiferente.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasIPSPGDiferente)
                        {
                            listaOportunidadesIPPGDiferenteCerrar[contador] = item.IdOportunidad;
                            contador++;
                        }
                        this.CerrarOportunidadesOM(listaOportunidadesIPPGDiferenteCerrar, "UsuarioOM");

                        //tercero me paso a IP
                        //actualizamos la fase de la nueva oportunidad creada con la fase de las anteriores
                        oportunidad.IdFaseOportunidad = 8;//IP
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        var oportunidad2 = _mapper.Map<Oportunidad>(oportunidad);
                        _repOportunidad.Update(oportunidad2);

                        #region Marcado validacion correcta
                        try
                        {
                            _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        return true;
                    }
                }
                //////////////////////////////////// CASO 6:Valida si hay opotunidades con mayor Categoria en Fase RN,IT,BNC de Otros Programas///////////////
                var listaCategoriasBNCITRNPGDiferente = _repOportunidad.ValidarOportunidadesCategoriaBNCITRNDiferentePG(oportunidad.IdAlumno.Value, idProgramaGeneral, oportunidad.Id, objetoAlumno.Celular);
                if (listaCategoriasBNCITRNPGDiferente.Count() > 0)
                {
                    if (listaCategoriasBNCITRNPGDiferente.OrderByDescending(w => w.PesoCategoria).FirstOrDefault().PesoCategoria < pesoCategoriaOportunidad)
                    {
                        //Cerras las oportunidades que llegan
                        int[] listaOportunidadesBNCITRNPGDiferenteCerrar = new int[listaCategoriasBNCITRNPGDiferente.Count()];
                        int contador = 0;
                        foreach (var item in listaCategoriasBNCITRNPGDiferente)
                        {
                            listaOportunidadesBNCITRNPGDiferenteCerrar[contador] = item.IdOportunidad;
                            contador++;
                        }
                        //Me actualizo a la fase de la oportunidad anterior
                        oportunidad.IdFaseOportunidad = listaCategoriasBNCITRNPGDiferente.FirstOrDefault().IdFaseOportunidad;
                        oportunidad.FechaModificacion = DateTime.Now;
                        oportunidad.UsuarioModificacion = "System";
                        var oportunidad2 = _mapper.Map<Oportunidad>(oportunidad);
                        _repOportunidad.Update(oportunidad2);

                        var oportunidadAnterior = listaCategoriasBNCITRNPGDiferente.Where(w => w.IdPersonalAsignado != 125).FirstOrDefault();
                        if (oportunidadAnterior != null && oportunidadAnterior.IdPersonalAsignado != oportunidad.IdPersonalAsignado)
                        {
                            if (oportunidadAnterior.IdFaseOportunidad != 2)//(2:BNC)
                            {
                                this.ReasignarOportunidadesRef(ref oportunidad, oportunidadAnterior.IdPersonalAsignado, false);
                                oportunidad.IdPersonalAsignado = oportunidadAnterior.IdPersonalAsignado;
                            }
                        }
                        this.CerrarOportunidadesOM(listaOportunidadesBNCITRNPGDiferenteCerrar, "UsuarioOM");//FALTA IMPLEMENTAR
                    }
                }

                //////////////////////////////////////////////////////////// EXTRAS///////////////////////////////////////////////////////////////////////////
                #region Configuracion Dato Remarketing
                try
                {
                    // Insertar tabla mkt.T_OportunidadRemarketingAgenda
                    if (idTabRedireccion > 0 && oportunidad.IdFaseOportunidad != 32// ValorEstatico.IdFaseOportunidadOD
                        && oportunidad.IdFaseOportunidad != 33) //ValorEstatico.IdFaseOportunidadOM)
                    {
                        _repOportunidadRemarketingAgenda.EliminarRedireccionRemarketingAnterior(oportunidad.Id);

                        _repOportunidadRemarketingAgenda.Add(new OportunidadRemarketingAgenda
                        {
                            IdOportunidad = oportunidad.Id,
                            IdAgendaTab = idTabRedireccion,
                            AplicaRedireccion = true,
                            Estado = true,
                            UsuarioCreacion = "UsuarioRemarketing",
                            UsuarioModificacion = "UsuarioRemarketing",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                    }
                }
                catch (Exception ex)

                {
                    var _repLog = _unitOfWork.LogRepository;
                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ConfiguracionDatoRemarketing", Parametros = $"{IdOportunidad}/{IdAsignacionAutomatica}/{FlagPortalWeb}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "REMARKETING DATA", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                    validacionCorrecta = false;
                }
                #endregion

                #region Marcado validacion correcta
                try
                {
                    _repOportunidad.ActualizarValidacionOportunidad(oportunidad.Id, validacionCorrecta);
                }
                catch (Exception ex)
                {

                    var _repLog = _unitOfWork.LogRepository;
                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ActualizarValidacionOportunidad", Parametros = $"{IdOportunidad}/{IdAsignacionAutomatica}/{FlagPortalWeb}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "FINISH VALIDATION", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                }
                #endregion

                #region Envio correo, Whatsapp, SMS
                if (FlagPortalWeb)
                {
                    if (enviarNotificacion != false && oportunidad.IdPersonalAsignado != 125) //ValorEstatico.IdPersonalAsignacionAutomatica
                    {
                        // Mailing
                        try
                        {
                            // Flag opcional
                            bool enviarIdPersonalPorDefecto = false;

                            string uri = $"https://integrav5-servicios.bsginstitute.com/api/MailingEnvioAutomatico/EnvioCorreoOportunidadPlantilla/{oportunidad.Id}/{827   // ValorEstatico.IdPlantillaInformacionCursoVentas

                                }/{enviarIdPersonalPorDefecto}";

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(uri);
                            }
                        }
                        catch (Exception)
                        {
                        }

                        // SMS
                        try
                        {
                            string uriSms = string.Empty;

                            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                            {
                                if (DateTime.Now.Hour == 18)
                                    uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1456//ValorEstatico.IdRecordatorioSms01Maniana
                                                                                                                                                                                      }" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                                else if (DateTime.Now.Hour > 18)
                                    uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                                else
                                    uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                            }

                            using (WebClient wc = new WebClient())
                            {
                                wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                wc.DownloadString(uriSms);
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (diaDto.ProbabilidadOportunidad == null)
                        diaDto.ProbabilidadOportunidad = "Muy Alta";

                    if (probabilidadActual != 0 && enviarNotificacion == true)
                    {
                        string probabilidaActualDesc = probabilidadActual == 4 ? "Muy Alta" : (probabilidadActual == 3 ? "Alta" : (probabilidadActual == 2 ? "Media" : "Sin Probabilidad"));

                        var probabilidades = diaDto.ProbabilidadOportunidad.Split(",");
                        if (probabilidades.Any(w => w == probabilidaActualDesc))
                        {
                            // LLama al Socket
                            AgendaSocket.getInstance().NuevaActividadParaEjecutar(IdOportunidad, oportunidad.IdPersonalAsignado.Value);
                        }
                    }
                }
                #endregion

                return true;
            }
            catch (Exception e)
            {
                if (!e.Message.Contains("Timeout expired"))
                {
                    var _repLog = _unitOfWork.LogRepository;
                    _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "ValidarCasosOportunidad", Parametros = $"IdOportunidad:{IdOportunidad}/IdAsignacionAutomatica:{IdAsignacionAutomatica}/FlagPortalWeb:{FlagPortalWeb}", Mensaje = $"{e.Message}-{(e.InnerException != null ? e.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{e}", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = "IntegraV5", UsuarioModificacion = "IntegraV5", FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                }

                return (e.Message);
            }
        }



        /// Tipo Función: Interna
        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 03/02/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el asesor configurado para el centro de costo enviado
        /// </summary>
        /// <param name="IdCentroCosto">Id del centro de costo (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="IdPais">Id del pais (PK de la tabla conf.T_Pais)</param>
        /// <param name="IdSubCategoriaDato">Id de la subcategoria dato(PK de la tabla mkt.T_SubCategoriaDato)</param>
        /// <param name="ProbabilidadActual">Probabilidad actual del dato</param>
        /// <returns>Entero con el asesor que corresponde</returns>
        private int ObtenerAsesorParaCentroCosto(int IdCentroCosto, int IdSubCategoriaDato, int IdPais, int ProbabilidadActual, int IdAgendaTab = 0)
        {
            var _repOportunidad = _unitOfWork.OportunidadRepository;
            var oportunidadService = new OportunidadService(_unitOfWork);
            int prioridad = 1;
            int idAsesor = 0;


            ProbabilidadActual = IdAgendaTab > 0 ? 4 /*4: Muy Alta*/: ProbabilidadActual;

            if (ProbabilidadActual < 4) // 4: Muy Alta
            {
                return 0;
            }

            while (prioridad < 4)

            {

                var posibilidades = _repOportunidad.ObtenerAsesorParaCentroCosto(IdCentroCosto, IdSubCategoriaDato, IdPais, ProbabilidadActual, prioridad);

                if (posibilidades.Count == 0)
                {
                    prioridad++;
                }
                else
                {
                    int minimocc = posibilidades.Min(s => s.AsignadosTotales);
                    var posibles = posibilidades.Where(s => s.AsignadosTotales == minimocc).ToList();
                    if (posibles.Count == 1)
                    {
                        idAsesor = posibles[0].IdAsesor;
                    }
                    else
                    {
                        minimocc = posibles.Min(s => s.AsignadosTotalesBNC);
                        posibles = posibles.Where(s => s.AsignadosTotalesBNC == minimocc).ToList();
                        idAsesor = posibles[0].IdAsesor;
                    }
                    prioridad = 4;
                }
            }

            return idAsesor;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="textoaBuscar"></param>
        /// <param name="tipo"></param>
        /// <returns> OportunidadTabAgendaDTO </returns>
        public OportunidadTabAgendaDTO ObtenerClasificacionTabAgenda(int idPersonal, string textoaBuscar, int tipo)
        {
            try
            {
                ;
                return _unitOfWork.OportunidadRepository.ObtenerClasificacionTabAgenda(idPersonal, textoaBuscar, tipo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 03/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <param name="idPersonal"></param>
        /// <param name="textoaBuscar"></param>
        /// <param name="tipo"></param>
        /// <returns> OportunidadTabAgendaDTO </returns>
        public OportunidadTabAgendaFichaDTO ObtenerClasificacionTabAgendaFicha(int idPersonal, int idMatriculaCabecera, int tipo)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerClasificacionTabAgendaFicha(idPersonal, idMatriculaCabecera, tipo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DesactivarRedireccionRemarketingAnterior(int IdOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnterior(IdOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Genera oportunidad de operaciones
        /// </summary>
        /// <returns> Vacio </returns>
        public void GenerarOportunidadOperaciones()
        {
            _oportunidadBo.Alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(_oportunidadBo.IdAlumno.Value);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idPersonal por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public bool TienePersonalOperaciones(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.TienePersonalOperaciones(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el idPersonal por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public IdDTO ObtenerIdPersonalOperaciones(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerIdPersonalOperaciones(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        /// Autor: Margiory Ramirez 
        /// Fecha: 06/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de Oportunidad asociados a un Alumno
        /// </summary>
        /// <param name="paginador">Id del Alumno</param>
        /// <returns> OportunidadBandejaCorreoDTO </returns>
        public object ObtenerPorFiltroRevertirFase(RevertirFaseFiltroDTO obj, PaginadorDTO paginador, List<GridFilterDTO> filter)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerPorFiltroRevertirFase(obj, paginador, filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 06/12/2022
        /// Versión: 1.0
        /// Autor Modificacion: Gilmer Qm
        /// Fecha Modificacion: 2024-12-06
        /// Descripcion Modidicacion: Se valida que actualice solo la utlima actividad detalle de la oportunidad
        /// Versión: 2.0
        /// <summary>
        /// Revertir Oportunidad  Asignado    
        /// </summary>
        /// <returns>OK-BADREQUEST</returns>
        public object RevertirOportunidad(RevertirFaseOportunidadFiltroDTO Obj)
        {
            try
            {
                var oportunidad = _unitOfWork.OportunidadRepository.FirstById(Obj.IdOportunidad);
                var oportunidadLog = _unitOfWork.OportunidadLogRepository.RevertirFaseOportunidad(Obj.IdOportunidad, Obj.FechaProgramada, Obj.Usuario);
                if (oportunidadLog == null)
                {
                    throw new Exception("Oportunidad no tiene un cambio de fase");
                }
                var actividadDetalle = _unitOfWork.ActividadDetalleRepository.GetBy(x => x.IdOportunidad == Obj.IdOportunidad && x.Id == oportunidad.IdActividadDetalleUltima);
                foreach (var item in actividadDetalle)
                {
                    item.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                    item.UsuarioModificacion = Obj.Usuario;
                    item.FechaModificacion = DateTime.Now;
                    item.TLlamadaActividads = null;
                }
                _unitOfWork.ActividadDetalleRepository.Update(actividadDetalle);
                _unitOfWork.Commit();

                int idEstadoOportunidad = 0;

                if (Obj.FechaProgramada != null)
                {
                    idEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    idEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }
                int actividadCabecera = ObtenerIdActividadCabecera(oportunidadLog);

                ActividadDetalle actDetalle = new ActividadDetalle()
                {
                    IdActividadCabecera = actividadCabecera,
                    FechaProgramada = Obj.FechaProgramada,
                    IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleNoEjecutado,
                    Comentario = "Se ha  revertido el ultimo cambio de fase",
                    IdAlumno = oportunidadLog.IdContacto,
                    Actor = "A",
                    IdOportunidad = oportunidadLog.IdOportunidad == null ? 0 : oportunidadLog.IdOportunidad.Value,
                    IdClasificacionPersona = oportunidadLog.IdClasificacionPersona,
                    Estado = true,
                    UsuarioCreacion = Obj.Usuario,
                    UsuarioModificacion = Obj.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var idActividadDetalleP = _unitOfWork.ActividadDetalleRepository.Add(actDetalle);
                _unitOfWork.Commit();

                oportunidad.IdActividadDetalleUltima = idActividadDetalleP.Id;
                oportunidad.IdActividadCabeceraUltima = actividadCabecera;
                oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleNoEjecutado;
                oportunidad.UltimoComentario = "Se ha revertido el ultimo cambio de fase";
                oportunidad.UltimaFechaProgramada = Obj.FechaProgramada;
                oportunidad.IdEstadoOportunidad = idEstadoOportunidad;
                oportunidad.IdPersonalAsignado = oportunidadLog.IdPersonalAsignado == null ? 0 : oportunidadLog.IdPersonalAsignado.Value;
                oportunidad.IdCentroCosto = oportunidadLog.IdCentroCosto == null ? 0 : oportunidadLog.IdCentroCosto.Value;
                oportunidad.IdFaseOportunidad = oportunidadLog.IdFaseOportunidad == null ? 0 : oportunidadLog.IdFaseOportunidad.Value;
                oportunidad.UsuarioModificacion = Obj.Usuario;
                oportunidad.FechaModificacion = DateTime.Now;
                var oportunidad1 = _mapper.Map<Oportunidad>(oportunidad);
                oportunidad1 = this.Update(oportunidad1);
                _unitOfWork.Commit();
                return oportunidad1;
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }

        private int ObtenerIdActividadCabecera(OportunidadLogRevertirDTO oportunidadLog)
        {
            int actividadCabecera = 0;
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC
                && oportunidadLog.IdTipoDato == ValorEstatico.IdTipoDatoHistorico)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNC
                && oportunidadLog.IdTipoDato == ValorEstatico.IdTipoDatoLanzamiento)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaContactoInicial;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIT)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRevisionInfo;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIP)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaCierre;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRegistroPW;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIC)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirmacionPago;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadIS)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirEnvioDoc;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadRN2A)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN;
            }
            if (oportunidadLog.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadBNCRN2)
            {
                actividadCabecera = ValorEstatico.IdActividadCabeceraLlamadaSeguimientoRN2;
            }

            return actividadCabecera;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por la ActividadDetalle
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns> DatosOportunidadDocumentosCompuestoDTO </returns>
        public DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorActividades(int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerDatosCompuestosPorActividades(idActividadDetalle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Oportunidades de Operaciones
        /// </summary>
        /// <param name="paginador">paginador</param>
        /// <param name="filtro"> Filtro Modulo </param>
        /// <param name="filterGrid"> filtros de grilla </param>        
        /// <returns> ResultadoFiltroAsignacionOportunidadDTO </returns>
        public ResultadoFiltroAsignacionOportunidadDTO ObtenerPorFiltroPaginaManualOperaciones(PaginadorDTO paginador, AsignacionManualOportunidadOperacionesFiltroDTO filtro, GridFiltersDTO filterGrid, List<string> listaCodigoMatricula)
        {
            try
            {
                var _alumnoService = new AlumnoService(_unitOfWork);
                ResultadoFiltroAsignacionOportunidadDTO resultado = new ResultadoFiltroAsignacionOportunidadDTO();
                resultado = _unitOfWork.OportunidadRepository.ObtenerPorFiltroPaginaManualOperaciones(paginador, filtro, filterGrid, listaCodigoMatricula);


                foreach (var item in resultado.Lista)
                {
                    if (!string.IsNullOrWhiteSpace(item.Email))
                        item.Email = _alumnoService.EncriptarCorreoHash(item.Email);

                    //if (!string.IsNullOrWhiteSpace(item.Celular))
                    //    item.Celular = _alumnoService.EncriptarNumeroHash(Regex.Replace(item.Celular, @"[^\d]", ""));
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Finaliza Actividad de Asignación Manual de Operaciones   
        /// </summary>
        /// <param name="mantenerestadooportunidad"> Bool de confirmación para mantener la fase de oportunidad </param>
        /// <param name="datosOportunidad"> Información de oportunidad </param>
        /// <returns></returns>        
        public void FinalizarActividadAsignacionManualOperaciones(bool mantenerestadooportunidad, OportunidadDTO datosOportunidad)
        {
            string flagError = "";
            try
            {

                //OportunidadLog OportunidadLogAntigua = new OportunidadLog();
                //OportunidadLogService repOportunidadLog = new OportunidadLogService(_unitOfWork);
                //ActividadDetalleService repActividadDetalle = new ActividadDetalleService(_unitOfWork);

                flagError = "ObtenerUltimoOportunidadLog";
                _oportunidadBo.OportunidadLogAntigua = _unitOfWork.OportunidadLogRepository.ObtenerOportunidadLogPorIdOportunidad(_oportunidadBo.ActividadAntigua.IdOportunidad.Value);

                var fechaFinLLamada = DateTime.Now;

                flagError = "ObtenerOcurrenciaPorActividad";

                _oportunidadBo.ActividadNueva = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(_oportunidadBo.ActividadAntigua.Id);
                _oportunidadBo.ActividadNueva.FechaReal = DateTime.Now;
                _oportunidadBo.ActividadNueva.DuracionReal = _oportunidadBo.ActividadAntigua.DuracionReal;
                _oportunidadBo.ActividadNueva.IdEstadoActividadDetalle = _oportunidadBo.ActividadAntigua.IdEstadoActividadDetalle;
                _oportunidadBo.ActividadNueva.Comentario = _oportunidadBo.ActividadAntigua.Comentario;
                _oportunidadBo.ActividadNueva.IdOcurrencia = _oportunidadBo.ActividadAntigua.IdOcurrencia;
                _oportunidadBo.ActividadNueva.IdOcurrenciaActividad = _oportunidadBo.ActividadAntigua.IdOcurrenciaActividad;
                _oportunidadBo.ActividadNueva.IdCentralLlamada = _oportunidadBo.ActividadAntigua.IdCentralLlamada;
                _oportunidadBo.ActividadNueva.FechaModificacion = DateTime.Now;
                _oportunidadBo.ActividadNueva.UsuarioModificacion = _oportunidadBo.Usuario;
                _oportunidadBo.ActividadNueva.IdClasificacionPersona = _oportunidadBo.ActividadAntigua.IdClasificacionPersona;
                _oportunidadBo.UltimoComentario = _oportunidadBo.ActividadAntigua.Comentario;
                _oportunidadBo.IdActividadDetalleUltima = _oportunidadBo.ActividadAntigua.Id;

                _oportunidadBo.FechaModificacion = DateTime.Now;
                _oportunidadBo.UsuarioModificacion = _oportunidadBo.Usuario;

                //Guardar Log
                _oportunidadBo.OportunidadLogNueva = new OportunidadLog();
                if (_oportunidadBo.OportunidadLogAntigua == null)
                {
                    _oportunidadBo.OportunidadLogNueva.IdClasificacionPersona = _oportunidadBo.IdClasificacionPersona;
                    _oportunidadBo.OportunidadLogNueva.IdPersonalAreaTrabajo = _oportunidadBo.IdPersonalAreaTrabajo;
                    _oportunidadBo.OportunidadLogNueva.FechaFinLog = DateTime.Now;
                    _oportunidadBo.OportunidadLogNueva.IdCentroCostoAnt = _oportunidadBo.IdCentroCosto;
                    _oportunidadBo.OportunidadLogNueva.IdAsesorAnt = _oportunidadBo.IdPersonalAsignado;
                }
                else
                {
                    _oportunidadBo.OportunidadLogNueva.IdClasificacionPersona = _oportunidadBo.OportunidadLogAntigua.IdClasificacionPersona;
                    _oportunidadBo.OportunidadLogNueva.IdPersonalAreaTrabajo = _oportunidadBo.OportunidadLogAntigua.IdPersonalAreaTrabajo;
                    _oportunidadBo.OportunidadLogNueva.FechaFinLog = _oportunidadBo.OportunidadLogAntigua.FechaLog;
                    _oportunidadBo.OportunidadLogNueva.IdCentroCostoAnt = _oportunidadBo.OportunidadLogAntigua.IdCentroCosto;
                    _oportunidadBo.OportunidadLogNueva.IdAsesorAnt = _oportunidadBo.OportunidadLogAntigua.IdPersonalAsignado;

                }

                _oportunidadBo.OportunidadLogNueva.IdCentroCosto = _oportunidadBo.IdCentroCosto;
                _oportunidadBo.OportunidadLogNueva.IdPersonalAsignado = _oportunidadBo.IdPersonalAsignado;
                _oportunidadBo.OportunidadLogNueva.IdTipoDato = _oportunidadBo.IdTipoDato;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadAnt = datosOportunidad.IdFaseOportunidad;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidad = _oportunidadBo.IdFaseOportunidad;
                _oportunidadBo.OportunidadLogNueva.IdOrigen = _oportunidadBo.IdOrigen;
                _oportunidadBo.OportunidadLogNueva.IdContacto = _oportunidadBo.IdAlumno;
                _oportunidadBo.OportunidadLogNueva.FechaLog = DateTime.Now;
                _oportunidadBo.OportunidadLogNueva.IdActividadDetalle = _oportunidadBo.ActividadAntigua.Id;
                _oportunidadBo.OportunidadLogNueva.IdOcurrencia = _oportunidadBo.ActividadAntigua.IdOcurrencia;
                _oportunidadBo.OportunidadLogNueva.IdOcurrenciaActividad = _oportunidadBo.ActividadAntigua.IdOcurrenciaActividad;
                _oportunidadBo.OportunidadLogNueva.Comentario = _oportunidadBo.UltimoComentario;
                _oportunidadBo.OportunidadLogNueva.IdOportunidad = _oportunidadBo.Id;
                _oportunidadBo.OportunidadLogNueva.IdCategoriaOrigen = _oportunidadBo.IdCategoriaOrigen;
                _oportunidadBo.OportunidadLogNueva.IdSubCategoriaDato = _oportunidadBo.IdSubCategoriaDato;
                _oportunidadBo.OportunidadLogNueva.FechaRegistroCampania = _oportunidadBo.FechaRegistroCampania;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadIc = datosOportunidad.IdFaseOportunidadIc;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadIp = datosOportunidad.IdFaseOportunidadIp;
                _oportunidadBo.OportunidadLogNueva.IdFaseOportunidadPf = datosOportunidad.IdFaseOportunidadPf;
                _oportunidadBo.OportunidadLogNueva.FechaEnvioFaseOportunidadPf = datosOportunidad.FechaEnvioFaseOportunidadPf;
                _oportunidadBo.OportunidadLogNueva.FechaPagoFaseOportunidadIc = datosOportunidad.FechaPagoFaseOportunidadIc;
                _oportunidadBo.OportunidadLogNueva.FechaPagoFaseOportunidadPf = datosOportunidad.FechaPagoFaseOportunidadPf;
                _oportunidadBo.OportunidadLogNueva.FasesActivas = datosOportunidad.FasesActivas;
                _oportunidadBo.OportunidadLogNueva.CodigoPagoIc = datosOportunidad.CodigoPagoIc;
                _oportunidadBo.OportunidadLogNueva.IdClasificacionPersona = _oportunidadBo.IdClasificacionPersona;
                _oportunidadBo.OportunidadLogNueva.IdPersonalAreaTrabajo = _oportunidadBo.IdPersonalAreaTrabajo;
                _oportunidadBo.OportunidadLogNueva.CambioFase = false;
                _oportunidadBo.OportunidadLogNueva.CambioFaseIs = false;
                _oportunidadBo.OportunidadLogNueva.CambioFaseAsesor = 0;
                _oportunidadBo.OportunidadLogNueva.FechaCreacion = DateTime.Now;
                _oportunidadBo.OportunidadLogNueva.FechaModificacion = DateTime.Now;
                _oportunidadBo.OportunidadLogNueva.UsuarioCreacion = _oportunidadBo.Usuario;
                _oportunidadBo.OportunidadLogNueva.UsuarioModificacion = _oportunidadBo.Usuario;
                _oportunidadBo.OportunidadLogNueva.Estado = true;

                _oportunidadBo.ActividadAntigua = null;
                _oportunidadBo.OportunidadLogAntigua = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + flagError);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Email de Personal y alumno por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: EmailPersonalAlumnoDTO </returns>
        public EmailPersonalAlumnoDTO ObtenerEmailPorOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerEmailPorOportunidad(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de oportunidades para reporte
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> List<OportunidadVentaCruzadaDTO> </returns>
        public List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidadesReporte(int idAlumno)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerHistorialOportunidadesReporte(idAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Carga una lista de problemas cliente por idOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<OportunidadProblemaClienteDTO> </returns>
        public List<OportunidadProblemaClienteDTO> ObtenerOportunidadProblemasCliente(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerOportunidadProblemasCliente(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene informacion complementaria para el Reporte de Seguimiento
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> ReporteSeguimientoOportunidadComplementosDTO </returns>
        public ReporteSeguimientoOportunidadComplementosDTO ObtenerInformacionComplementariaReporteSeguimiento(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerInformacionComplementariaReporteSeguimiento(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 28/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las oportunidades anteriores de un Alumno
        /// </summary>
        /// <param name="idAlumno"> Id del almuno </param> 
        /// <returns> List<OportunidadAnteriorDTO> </returns>
        public List<OportunidadAnteriorDTO> ObtenerOportunidadesAnterioresAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerOportunidadesAnterioresAlumno(idAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 21/12/2022
        /// Version: 1.0
        /// <summary>
        /// Programa Actividades de Asignación Manual de operaciones    
        /// </summary>
        /// <param></param>
        /// <returns></returns>        
        public void ProgramaActividadAsignacionManualOperaciones(int idActividadCabecera)
        {
            try
            {
                //ActividadDetalle ActividadNuevaProgramarActividad;
                if (_oportunidadBo.ActividadNueva.IdOportunidad == 0)
                {
                    throw new ArgumentException("debe tener una oportunidad");
                }
                _oportunidadBo.ActividadNueva.IdActividadCabecera = idActividadCabecera;
                _oportunidadBo.ActividadNuevaProgramarActividad = new ActividadDetalle();
                _oportunidadBo.ActividadNuevaProgramarActividad.IdOportunidad = _oportunidadBo.ActividadNueva.IdOportunidad;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdAlumno = _oportunidadBo.ActividadNueva.IdAlumno;
                _oportunidadBo.ActividadNuevaProgramarActividad.Actor = "A";
                _oportunidadBo.ActividadNuevaProgramarActividad.FechaProgramada = _oportunidadBo.UltimaFechaProgramada.HasValue ? _oportunidadBo.UltimaFechaProgramada.Value : default(DateTime);
                _oportunidadBo.ActividadNuevaProgramarActividad.IdEstadoActividadDetalle = EstadoActividadDetalle.NoEjecutado;
                _oportunidadBo.ActividadNuevaProgramarActividad.FechaCreacion = DateTime.Now;
                _oportunidadBo.ActividadNuevaProgramarActividad.FechaModificacion = DateTime.Now;
                _oportunidadBo.ActividadNuevaProgramarActividad.UsuarioCreacion = _oportunidadBo.UsuarioModificacion;
                _oportunidadBo.ActividadNuevaProgramarActividad.UsuarioModificacion = _oportunidadBo.UsuarioModificacion;
                _oportunidadBo.ActividadNuevaProgramarActividad.Estado = true;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdActividadCabecera = _oportunidadBo.ActividadNueva.IdActividadCabecera;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdOcurrencia = _oportunidadBo.ActividadNueva.IdOcurrencia;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdOcurrenciaActividad = _oportunidadBo.ActividadNueva.IdOcurrenciaActividad;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdClasificacionPersona = _oportunidadBo.ActividadNueva.IdClasificacionPersona;

                _oportunidadBo.IdActividadDetalleUltima = _oportunidadBo.ActividadNueva.Id;
                _oportunidadBo.IdActividadCabeceraUltima = _oportunidadBo.ActividadNueva.IdActividadCabecera.Value;
                _oportunidadBo.IdEstadoActividadDetalleUltimoEstado = _oportunidadBo.ActividadNueva.IdEstadoActividadDetalle;
                _oportunidadBo.UltimaFechaProgramada = _oportunidadBo.UltimaFechaProgramada.HasValue ? _oportunidadBo.UltimaFechaProgramada.Value : default(DateTime);
                _oportunidadBo.UltimoComentario = _oportunidadBo.ActividadNueva.Comentario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los dias de reprogramacion manual
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: DatosOportunidadReprogramacionManualOperacionesDTO </returns>
        public DatosOportunidadReprogramacionManualOperacionesDTO ObtenerCasosReprogramacionManualOperacionesAlterno(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerCasosReprogramacionManualOperacionesAlterno(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos necesarios de la oportunidad para calcualr la fecha de programacion Automatica
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DTO: DatosOportunidadReprogramacionAutomaticaDTO </returns>
        public DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramcionAutomatica(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerDatosParaReprogramcionAutomatica(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO </returns>
        public DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO ObtenerCalculoReprogramaciones(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerCalculoReprogramaciones(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO </returns>
        public DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO ObtenerSubEstadoAlumno(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerSubEstadoAlumno(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ActividadAgendaDTO? ObtenerDatosOportunidad(int idOportunidad)
        {
            return _unitOfWork.OportunidadRepository.ObtenerDatosOportunidad(idOportunidad);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosAlumnoDTO </returns>
        public DatosAlumnoDTO ObtenerDatosAlumno(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerDatosAlumno(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cantidad de no ejecutadas ultimas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> DatosAlumnoDTO </returns>
        public DatosAlumnoOportunidadDTO ObtenerDatosOportunidadAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerDatosOportunidadAlumno(idAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Programar Actividad Igual a la Version Integra.Servicios.V4
        /// </summary> 
        /// <returns> Vacio </returns>
        public void ProgramaActividadV2(bool CheckSegProg = false)
        {
            try
            {
                var ocurrencia = _unitOfWork.OcurrenciaRepository.ObtenerOcurrenciaPorActividad(_oportunidadBo.ActividadNueva.IdOcurrencia.Value);
                if (_oportunidadBo.ActividadNueva.IdOportunidad == 0)
                {
                    throw new ArgumentException("debe tener una oportunidad");
                }

                if (_oportunidadBo.ActividadNueva.IdOcurrencia == 0 || _oportunidadBo.ActividadNueva.IdOcurrencia == null)
                {
                    _oportunidadBo.ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this._oportunidadBo.IdFaseOportunidad, this._oportunidadBo.IdTipoDato, this._oportunidadBo.IdPersonalAreaTrabajo, this._oportunidadBo.IdActividadCabeceraUltima);
                    //guardar el logger
                }
                else
                {
                    if (ocurrencia != null)
                    {
                        if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                        {
                            ocurrencia.IdEstadoOcurrencia = 2;// ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                        }
                        if (ocurrencia.IdActividadCabecera == null)
                        {
                            if (!(_oportunidadBo.ActividadNueva.IdActividadCabecera == null))
                            {
                                _oportunidadBo.ActividadNueva.IdActividadCabecera = _oportunidadBo.ActividadNueva.IdActividadCabecera;
                            }
                            else
                            {
                                _oportunidadBo.ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this._oportunidadBo.IdFaseOportunidad, this._oportunidadBo.IdTipoDato, this._oportunidadBo.IdPersonalAreaTrabajo, this._oportunidadBo.IdActividadCabeceraUltima);
                            }
                        }
                        else
                        {
                            if (this._oportunidadBo.IdFaseOportunidad == 22 && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                                                                                                     //if (this.IdFaseOportunidad == ValorEstatico.IdFaseOportunidadPF && ocurrencia.Id == 161) //09a99527-c8a6-4a33-8046-108a8ee75e52 , 8dc0b255-89f6-cbdd-abad-08d39a00deed
                            {
                                _oportunidadBo.ActividadNueva.IdActividadCabecera = ActividadCabeceraUtil.ObtenerActividadCabecera(this._oportunidadBo.IdFaseOportunidad, this._oportunidadBo.IdTipoDato, this._oportunidadBo.IdPersonalAreaTrabajo, this._oportunidadBo.IdActividadCabeceraUltima);
                            }
                            else
                            {
                                _oportunidadBo.ActividadNueva.IdActividadCabecera = ocurrencia.IdActividadCabecera;
                            }
                        }
                    }
                }
                _oportunidadBo.ActividadNuevaProgramarActividad = new ActividadDetalle();
                _oportunidadBo.ActividadNuevaProgramarActividad.IdOportunidad = _oportunidadBo.ActividadNueva.IdOportunidad;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdAlumno = _oportunidadBo.ActividadNueva.IdAlumno;
                _oportunidadBo.ActividadNuevaProgramarActividad.Actor = "A";
                _oportunidadBo.ActividadNuevaProgramarActividad.FechaProgramada = this._oportunidadBo.UltimaFechaProgramada.HasValue ? this._oportunidadBo.UltimaFechaProgramada.Value : default(DateTime);
                _oportunidadBo.ActividadNuevaProgramarActividad.IdEstadoActividadDetalle = 1;    //EstadoActividadDetalleBO.EstadoActividadDetalleNoEjecutado;
                _oportunidadBo.ActividadNuevaProgramarActividad.FechaCreacion = DateTime.Now;
                _oportunidadBo.ActividadNuevaProgramarActividad.FechaModificacion = DateTime.Now;
                _oportunidadBo.ActividadNuevaProgramarActividad.UsuarioCreacion = this._oportunidadBo.UsuarioModificacion;
                _oportunidadBo.ActividadNuevaProgramarActividad.UsuarioModificacion = this._oportunidadBo.UsuarioModificacion;
                _oportunidadBo.ActividadNuevaProgramarActividad.Estado = true;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdActividadCabecera = _oportunidadBo.ActividadNueva.IdActividadCabecera;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdOcurrencia = _oportunidadBo.ActividadNueva.IdOcurrencia;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdOcurrenciaActividad = _oportunidadBo.ActividadNueva.IdOcurrenciaActividad;
                _oportunidadBo.ActividadNuevaProgramarActividad.IdClasificacionPersona = this._oportunidadBo.IdClasificacionPersona;
                //Actualiza Oportunidad

                this._oportunidadBo.IdActividadDetalleUltima = _oportunidadBo.ActividadNueva.Id;
                this._oportunidadBo.IdActividadCabeceraUltima = _oportunidadBo.ActividadNueva.IdActividadCabecera.Value;
                this._oportunidadBo.IdEstadoActividadDetalleUltimoEstado = _oportunidadBo.ActividadNueva.IdEstadoActividadDetalle;
                this._oportunidadBo.UltimaFechaProgramada = this._oportunidadBo.UltimaFechaProgramada.HasValue ? this._oportunidadBo.UltimaFechaProgramada.Value : default(DateTime);
                this._oportunidadBo.UltimoComentario = _oportunidadBo.ActividadNueva.Comentario;

                if (_oportunidadBo.ActividadNueva.IdOcurrencia == 35) //OCURRENCIA_ASIGNACION_MANUAL
                {
                    //this.oportunidadBo.IdEstadoOportunidad = (CheckSegProg == true) ? ValorEstatico.IdEstadoOportunidadSegMejProg : EstadoOportunidadBO.estadoReasignada;
                    this._oportunidadBo.IdEstadoOportunidad = (CheckSegProg == true) ? 8 : 3;
                }
                else
                {
                    // this.oportunidadBo.IdEstadoOportunidad = this.oportunidadBo.UltimaFechaProgramada.HasValue ? ValorEstatico.IdEstadoOportunidadProgramada : ValorEstatico.IdEstadoOportunidadNoProgramada;
                    this._oportunidadBo.IdEstadoOportunidad = this._oportunidadBo.UltimaFechaProgramada.HasValue ? 6 : 2;
                }

                var grupoPrelanzamiento = _unitOfWork.OcurrenciaRepository.ValidarGrupoPreLanzamiento(this._oportunidadBo.IdCategoriaOrigen.Value);

                if (this._oportunidadBo.IdEstadoOportunidad == 2 && ocurrencia.IdEstadoOcurrencia == 2 && grupoPrelanzamiento == 1 && this._oportunidadBo.IdFaseOportunidad == 2)
                {
                    this._oportunidadBo.IdEstadoOportunidad = 2;// ValorEstatico.IdEstadoOportunidadNoProgramada;
                }
                // if (this.oportunidadBo.IdEstadoOportunidad != 0 && this.oportunidadBo.IdEstadoOportunidad.Equals(ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada) && false)
                if (this._oportunidadBo.IdEstadoOportunidad != 0 && this._oportunidadBo.IdEstadoOportunidad.Equals(7) && false)
                {
                    this._oportunidadBo.IdEstadoOportunidad = 7;// ValorEstatico.IdEstadoOportunidadReasignacionVentaCruzada;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 10/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_Oportunidad asociados a un Id del Alumno
        /// </summary>
        /// <param name="idAlumno">Id de la Oportunidad</param>
        /// <returns> Entidad: Oportunidad </returns>
        public List<Oportunidad> ObtenerPorIdAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerPorIdAlumno(idAlumno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 25/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de Tiempo Capacitacion que pertecen al registro de T_Oportunidad asociada al Id Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<OportunidadTiempoCapacitacionDTO> </returns>
        public OportunidadTiempoCapacitacionDTO ObtenerTiempoCapacitacionPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerTiempoCapacitacionPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/03/2023
        /// Version: 1.0
        /// <summary>
        /// Captura en una variable string el correo y las oportunidades del alumno que va a ser reasignado.
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> string: listaOportunidades </returns>
        public string CapturaDeOportunidades(int idAlumno)
        {
            try
            {
                var oportunidades = _unitOfWork.OportunidadRepository.ObtenerPorIdAlumno(idAlumno);
                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(idAlumno);
                string listaOportunidades = alumno.Email1 + ": ";
                listaOportunidades += string.Join(";", oportunidades.Select(x => x.Id).ToList());

                return listaOportunidades;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public OportunidadBoDTO GenerarOportunidad(AsignacionAutomatica objAsignacionAutomatica, bool ventaCruzada, string usuario, int idTipoCategoriaOrigen)
        {
            var asesorAsignacionAutomatica = 125;
            var tipoDatoLanzamiento = ValorEstatico.IdTipoDatoLanzamiento;
            Alumno Alumno = new Alumno();
            OportunidadBoDTO oportunidad = new OportunidadBoDTO();
            if (objAsignacionAutomatica.IdAlumno != null && objAsignacionAutomatica.IdAlumno != 0)
            {

                Alumno = _unitOfWork.AlumnoRepository.ObtenerPorId((int)(objAsignacionAutomatica.IdAlumno));
            }
            if (objAsignacionAutomatica.IdAlumno != 0 && Alumno.Asociado == true) //si el alumno esta en finanzas no actualizamos todos sus campos
            {
                // Reemplazamos los nuevos campos del registro
                Alumno.IdIndustria = objAsignacionAutomatica.IdIndustria != 0 && objAsignacionAutomatica.IdIndustria != null ? objAsignacionAutomatica.IdIndustria : Alumno.IdIndustria;
                Alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion != 0 && objAsignacionAutomatica.IdAreaFormacion != null ? objAsignacionAutomatica.IdAreaFormacion : Alumno.IdAformacion;
                Alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo != 0 && objAsignacionAutomatica.IdAreaTrabajo != null ? objAsignacionAutomatica.IdAreaTrabajo : Alumno.IdAtrabajo;
                Alumno.IdCargo = objAsignacionAutomatica.IdCargo != 0 && objAsignacionAutomatica.IdCargo != null ? objAsignacionAutomatica.IdCargo : Alumno.IdCargo;

                Alumno.Celular = objAsignacionAutomatica.Celular;
                Alumno.IdCodigoRegionCiudad = (objAsignacionAutomatica.IdCiudad == -1 || objAsignacionAutomatica.IdCiudad == 0) ? Alumno.IdCodigoRegionCiudad : objAsignacionAutomatica.IdCiudad;
                Alumno.IdCodigoPais = (objAsignacionAutomatica.IdPais == -1 || objAsignacionAutomatica.IdPais == 0) ? Alumno.IdCodigoPais : objAsignacionAutomatica.IdPais;
                Alumno.Email2 = objAsignacionAutomatica.Email;
                Alumno.UsuarioModificacion = "SYSTEM";
                Alumno.FechaModificacion = DateTime.Now;
            }
            else if (objAsignacionAutomatica.IdAlumno != 0 && objAsignacionAutomatica.IdAlumno != null)
            {
                // Reemplazamos los nuevos campos del registro
                Alumno.IdIndustria = objAsignacionAutomatica.IdIndustria != 0 && objAsignacionAutomatica.IdIndustria != null ? objAsignacionAutomatica.IdIndustria : Alumno.IdIndustria;
                Alumno.Nombre1 = objAsignacionAutomatica.Nombre1;
                Alumno.Nombre2 = objAsignacionAutomatica.Nombre2;
                Alumno.ApellidoMaterno = objAsignacionAutomatica.ApellidoMaterno;
                Alumno.ApellidoPaterno = objAsignacionAutomatica.ApellidoPaterno;
                Alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion != 0 && objAsignacionAutomatica.IdAreaFormacion != null ? objAsignacionAutomatica.IdAreaFormacion : Alumno.IdAformacion;
                Alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo != 0 && objAsignacionAutomatica.IdAreaTrabajo != null ? objAsignacionAutomatica.IdAreaTrabajo : Alumno.IdAtrabajo;
                Alumno.IdCargo = objAsignacionAutomatica.IdCargo != 0 && objAsignacionAutomatica.IdCargo != null ? objAsignacionAutomatica.IdCargo : Alumno.IdCargo;
                Alumno.Celular = objAsignacionAutomatica.Celular;
                Alumno.IdCodigoRegionCiudad = objAsignacionAutomatica.IdCiudad;
                Alumno.IdCodigoPais = objAsignacionAutomatica.IdPais;
                Alumno.Telefono = objAsignacionAutomatica.Telefono;
                Alumno.Email1 = objAsignacionAutomatica.Email;
                Alumno.Email2 = objAsignacionAutomatica.Email;
                Alumno.Estado = true;
                Alumno.UsuarioModificacion = "SYSTEM";
                Alumno.FechaModificacion = DateTime.Now;
            }
            else
            {
                //Reemplazamos los nuevos campos del registro
                Alumno.IdIndustria = objAsignacionAutomatica.IdIndustria;
                Alumno.Nombre1 = objAsignacionAutomatica.Nombre1;
                Alumno.Nombre2 = objAsignacionAutomatica.Nombre2;
                Alumno.ApellidoMaterno = objAsignacionAutomatica.ApellidoMaterno;
                Alumno.ApellidoPaterno = objAsignacionAutomatica.ApellidoPaterno;
                Alumno.IdAformacion = objAsignacionAutomatica.IdAreaFormacion;
                Alumno.IdAtrabajo = objAsignacionAutomatica.IdAreaTrabajo;
                Alumno.IdCargo = objAsignacionAutomatica.IdCargo;
                Alumno.Celular = objAsignacionAutomatica.Celular;
                Alumno.IdCodigoRegionCiudad = objAsignacionAutomatica.IdCiudad;
                Alumno.IdCodigoPais = objAsignacionAutomatica.IdPais;
                Alumno.Telefono = objAsignacionAutomatica.Telefono;
                Alumno.Email1 = objAsignacionAutomatica.Email;
                Alumno.Email2 = objAsignacionAutomatica.Email;
                Alumno.Estado = true;
                Alumno.UsuarioCreacion = "GENOPO";
                Alumno.UsuarioModificacion = "SYSTEM";
                Alumno.FechaModificacion = DateTime.Now;
                Alumno.FechaCreacion = DateTime.Now;
            }
            //Generamos la Oportunidad
            //Si el tipo de dato no es LANZAMIENTO, lo asignamos al asesor de Asignacion Automatica
            oportunidad.IdPersonalAsignado = asesorAsignacionAutomatica;


            oportunidad.IdFaseOportunidad = objAsignacionAutomatica.IdFaseOportunidad == null ? 0 : objAsignacionAutomatica.IdFaseOportunidad.Value;
            oportunidad.IdCentroCosto = objAsignacionAutomatica.IdCentroCosto == null ? 0 : objAsignacionAutomatica.IdCentroCosto.Value;
            oportunidad.IdOrigen = objAsignacionAutomatica.IdOrigen == null ? 0 : objAsignacionAutomatica.IdOrigen.Value;
            oportunidad.IdTipoDato = objAsignacionAutomatica.IdTipoDato == null ? 0 : objAsignacionAutomatica.IdTipoDato.Value;
            oportunidad.IdConjuntoAnuncio = objAsignacionAutomatica.IdConjuntoAnuncio == null ? 0 : objAsignacionAutomatica.IdConjuntoAnuncio.Value;

            oportunidad.IdAnuncioFacebook = objAsignacionAutomatica.IdAnuncioFacebook;

            oportunidad.IdTiempoCapacitacion = objAsignacionAutomatica.IdTiempoCapacitacion == null ? 0 : objAsignacionAutomatica.IdTiempoCapacitacion.Value;
            oportunidad.IdPagina = objAsignacionAutomatica.IdPagina == null ? 0 : objAsignacionAutomatica.IdPagina.Value;
            oportunidad.FechaRegistroCampania = objAsignacionAutomatica.FechaRegistroCampania.Value;
            oportunidad.IdFaseOportunidadPortal = objAsignacionAutomatica.IdFaseOportunidadPortal == null ? Guid.Empty : objAsignacionAutomatica.IdFaseOportunidadPortal;
            oportunidad.IdCampaniaScoring = objAsignacionAutomatica.IdCampaniaScoring == null ? 0 : objAsignacionAutomatica.IdCampaniaScoring.Value;
            oportunidad.UltimaFechaProgramada = objAsignacionAutomatica.FechaProgramada;
            oportunidad.UltimoComentario = "Sin Comentario";
            oportunidad.IdAlumno = Alumno.Id == 0 ? oportunidad.IdAlumno : Alumno.Id;
            oportunidad.IdCategoriaOrigen = objAsignacionAutomatica.IdCategoriaOrigen == null ? 0 : objAsignacionAutomatica.IdCategoriaOrigen.Value;
            oportunidad.IdInteraccionFormulario = objAsignacionAutomatica.IdInteraccionFormulario == null ? 0 : objAsignacionAutomatica.IdInteraccionFormulario.Value;
            oportunidad.UrlOrigen = objAsignacionAutomatica.UrlOrigen;
            oportunidad.IdSubCategoriaDato = objAsignacionAutomatica.IdSubCategoriaDato == null ? 0 : objAsignacionAutomatica.IdSubCategoriaDato.Value;
            //oportunidad.IdClasificacionPersona = objAsignacionAutomatica.idClasificacionPersona;
            oportunidad.IdPersonalAreaTrabajo = 8;

            oportunidad.Alumno = Alumno;

            if (idTipoCategoriaOrigen == 16 || idTipoCategoriaOrigen == 38)//16=>TipoCategoriaOrigen = Chat , 38=>TipoCategoriaOrigen = Chat2
            {
                //obtenemos el idasesor asignado a ese programa por su cc segun chat
                var asignadoChat = _unitOfWork.OportunidadRepository.ObtenerIdPersonalAsignadoChat(Alumno.Id, oportunidad.IdCentroCosto.Value);
                if (asignadoChat == null || asignadoChat.IdAsesor == 0)
                {
                    //nada
                }
                else
                {
                    oportunidad.IdPersonalAsignado = asignadoChat.IdAsesor;
                }
            }

            return oportunidad;
        }

        public bool ProcesarOportunidadesPortalWeb()
        {
            try
            {
                var asignacionAutomaticaTempService = new AsignacionAutomaticaTempService(_unitOfWork);
                var nuevosRegistros = _unitOfWork.AsignacionAutomaticaTempRepository.ObtenerNuevosRegistros();
                string[] listaAProcesar = new string[nuevosRegistros.Count];
                int contador = 0;
                foreach (var item in nuevosRegistros)
                {
                    AsignacionAutomaticaTemp asignacionAutomaticaTemp = new();

                    Guid auxGuid = new Guid(item.IdFaseOportunidadPortal);
                    var resp = _unitOfWork.AsignacionAutomaticaTempRepository.ObtenerPorIdFaseOportunidadPortal(item.IdFaseOportunidadPortal);
                    if (resp == null || resp.Id != 0)
                    {
                        asignacionAutomaticaTempService.MapearAsignacionAutomaticaTemp(ref asignacionAutomaticaTemp, item);
                        _unitOfWork.AsignacionAutomaticaTempRepository.Add(asignacionAutomaticaTemp);
                        _unitOfWork.Commit();
                    }
                    listaAProcesar[contador] = item.IdFaseOportunidadPortal;
                    contador++;
                }
                asignacionAutomaticaTempService.MarcarComoProcesados(listaAProcesar, 1);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Genera oportunidad desde el registro de asignacion automatica
        /// </summary>
        /// <param name="objAsignacionAutomatica">Objeto de clase objAsignacionAutomatica</param>
        /// <param name="ventaCruzada">Flag para determinar si viene de venta cruzada</param>
        /// <param name="usuario">Usuario que realiza la creacion de la oportunidad</param>
        /// <param name="idTipoCategoriaOrigen">Id del tipo de categoria origen (PK de la tabla mkt.T_TipoCategoriaOrigen)</param>
        public List<OportunidadProgramadaManualDTO> CalcularProgramacionManualConsecutivos()
        {
            var _repOportunidad = _unitOfWork.OportunidadRepository;
            var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;

            //limpio la tabla antes de insertar

            try
            {
                var limpiezaexitosa = _repOportunidad.LimpiarProgramacionManualConsecutivos();
            }
            catch (Exception e)
            { }


            var oportunidadProgramadasManual = _repOportunidad.ObtenerProgramacionManualConsecutivos();

            foreach (var oportunidad in oportunidadProgramadasManual)
            {
                var logs = _repOportunidadLog.ObtenerOportunidadLogsPorIdOportunidad(oportunidad.IdOportunidad);
                int contador = 0;
                int[] manuales = new int[8] { 149, 162, 163, 164, 165, 168, 207, 209 };
                foreach (var log in logs.OrderByDescending(w => w.FechaLog))
                {
                    var existe = Array.IndexOf(manuales, log.IdOcurrenciaAlterno);
                    if (existe != -1)
                    {
                        contador++;
                    }
                    else
                    {
                        break;
                    }
                }
                var resultado = _repOportunidad.InsertarContadorReprogramacionManual(oportunidad.IdOportunidad, contador);
            }
            return oportunidadProgramadasManual;
        }

        /// Author: Carlos Crispin
        /// <summary>
        /// Inserta  EnviosWhatsappDiasSinContacto
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad</param>
        public bool InsertarEnviosWhatsappDiasSinContacto(int idOportunidad)
        {
            var _repOportunidad = _unitOfWork.OportunidadRepository;
            try
            {
                var resultado = _repOportunidad.InsertarEnviosWhatsappDiasSinContacto(idOportunidad);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        /// <summary>
        /// Validar formulario con los datos de asignacionautomaticatemp y el origen del dato
        /// </summary>
        /// <param name="IdAsignacionAutomaticaTemp">Id de la asignacion automatica temporal (PK de la tabla mkt.T_AsignacionAutomatica_Temp)</param>
        /// <param name="OrigenDato">Origen del dato (PK de la tabla mkt.T_Origen)</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>
        public int ValidarFormulario(int IdAsignacionAutomaticaTemp, int OrigenDato)// 1: Scoring, 2: PortalWeb, 3: CargaMasiva
        {
            try
            {

                var _repAsignacionAutomatica = _unitOfWork.AsignacionAutomaticaRepository;
                var _repAsignacionAutomaticaTemp = _unitOfWork.AsignacionAutomaticaTempRepository;
                var _repOrigen = _unitOfWork.OrigenRepository;
                var _repPais = _unitOfWork.PaisRepository;

                var res = new TAsignacionAutomatica();

                //Obtenemos la configuracion de la asignacion automatica
                var _repAsignacionAutomaticaConfiguracion = _unitOfWork.AsignacionAutomaticaConfiguracionRepository;
                var configuraciones = _repAsignacionAutomaticaConfiguracion.ObtenerConfiguracionAsignacionAutomatica();

                var inclusion = configuraciones.Where(o => o.Inclusivo == true).ToList();
                var exclusion = configuraciones.Where(o => o.Inclusivo == false).ToList();

                var AsignacionAutomatica = new AsignacionAutomaticaService(_unitOfWork);
                //Declaro el Objeto de AsignacionAutomatica que se va ha insertar


                //Obtenemos los Paises
                var listaPaises = new Dictionary<int, string>();
                int idElSalvador = 503;
                string ElSalvadorIniciales = "SAL";
                var paises = _repPais.ObtenerPaisCombo();
                foreach (var pais in paises)
                {
                    //EL SALVADOR
                    if (pais.CodigoPais == idElSalvador)
                    {
                        listaPaises.Add(pais.CodigoPais, ElSalvadorIniciales);
                    }
                    else
                    {
                        listaPaises.Add(pais.CodigoPais, pais.NombrePais.Substring(0, 3).ToUpper());
                    }
                }
                //Obtenemos los origenes
                var listaOrigenes = new Dictionary<string, OrigenesCategoriaOrigenDTO>();
                var origenes = _repOrigen.ObtenerOrigenesCategoriasOrigen();
                foreach (var origen in origenes)
                {
                    if (!listaOrigenes.ContainsKey(origen.Nombre.Trim().ToUpper()))
                    {
                        listaOrigenes.Add(origen.Nombre.Trim().ToUpper(), new OrigenesCategoriaOrigenDTO { Id = origen.Id, NombreCategoria = origen.NombreCategoria });
                    }
                }

                var dato = AsignacionAutomatica.ValidarRegistroFormularioAsignacionAutomaticaTemp(IdAsignacionAutomaticaTemp, listaPaises, listaOrigenes);

                if (OrigenDato == 3 ? true : AsignacionAutomatica.AplicarConfiguracion(inclusion, exclusion))
                {
                    var objAsignacionAutomaticaTemp = _repAsignacionAutomaticaTemp.FirstById(IdAsignacionAutomaticaTemp);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            //Actualizamos AsignacionAutomaticamTemp con True en Procesado
                            objAsignacionAutomaticaTemp.Procesado = true;
                            _repAsignacionAutomaticaTemp.Update(objAsignacionAutomaticaTemp);
                            _unitOfWork.Commit();
                            //Insertamos en la lista de Registros para la validacion
                            dato.Id = 0;
                            dato.IdAsignacionAutomaticaTemp = objAsignacionAutomaticaTemp.Id;
                            dato.Validado = false;
                            dato.Corregido = false;



                            dato.IdAsignacionAutomaticaOrigen = OrigenDato == 3 ? AsignacionAutomaticaOrigen.CargaMasiva : AsignacionAutomaticaOrigen.PortalWeb;
                            dato.IdCategoriaOrigen = objAsignacionAutomaticaTemp.IdCategoriaDato == null && dato.IdCategoriaDato == 18 ? 18 : objAsignacionAutomaticaTemp.IdCategoriaDato;
                            res = _repAsignacionAutomatica.Add(dato);
                            _unitOfWork.Commit();


                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                        finally
                        {
                            scope.Complete();
                        }
                    }

                    try
                    {
                        //string URI = "http://localhost:4348/Marketing/InsertarActualizarAsignacionAutomaticaTemp?IdAsignacionAutomaticaTemp=" + IdAsignacionAutomaticaTemp;
                        //using (WebClient wc = new WebClient())
                        //{
                        //    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        //    wc.DownloadString(URI);
                        //}
                    }
                    catch (Exception)
                    {
                    }
                    //return true;
                    return (res.Id);

                }

                // return false;
                return (res.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 07/02/2024
        /// <summary>
        /// Validar formulario con los datos de asignacionautomaticatemp y el origen del dato
        /// </summary>
        /// <param name="IdAsignacionAutomatica">Id de la asignacion automatica  (PK de la tabla mkt.T_AsignacionAutomatica)</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>

        public int CrearOportunidadWebHookFacebook(int IdAsignacionAutomatica)
        {
            try
            {
                var asignacionAutomaticaRepositorio = _unitOfWork.AsignacionAutomaticaRepository;

                var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
                var rpta = CrearOportunidadPortalWeb(IdAsignacionAutomatica);
                var IdOportunidad = asignacionAutomaticaRepositorio.FirstById(IdAsignacionAutomatica).IdOportunidad;

                if (IdOportunidad != null)
                {
                    //_repOportunidadLog = new OportunidadLogRepositorio(_integraDBContext2);

                    int nroIntentos = 0;
                    bool flagValidado = false;

                    while (!flagValidado && nroIntentos < 10)
                    {
                        try
                        {
                            ValidarCasosOportunidad(IdOportunidad ?? 0, IdAsignacionAutomatica, true);

                            flagValidado = true;
                        }
                        catch (Exception ex)
                        {
                            nroIntentos++;

                            Thread.Sleep(3000);
                        }
                    }

                    if (nroIntentos == 10)
                    {
                        var _repLog = _unitOfWork.LogRepository;
                        _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadWebHookFacebook", Parametros = $"IdAsignacionAutomatica={IdAsignacionAutomatica}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                    }

                    return (int)IdOportunidad;
                }
                return (int)IdOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 30/07/2024
        /// <summary>
        /// Procesa la lista de IDs para pendiente de Validacion
        /// </summary>
        /// <param name="IdAsignacionAutomatica">Id de la asignacion automatica  (PK de la tabla mkt.T_AsignacionAutomatica)</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>

        public List<int> CrearOportunidadesWebHookFacebookLista(List<int> idAsignacionAutomaticaList)
        {
            var resultados = new List<int>();

            foreach (var IdAsignacionAutomatica in idAsignacionAutomaticaList)
            {
                try
                {
                    var asignacionAutomaticaRepositorio = _unitOfWork.AsignacionAutomaticaRepository;
                    var _repOportunidadLog = _unitOfWork.OportunidadLogRepository;
                    var rpta = CrearOportunidadPortalWeb(IdAsignacionAutomatica);
                    var IdOportunidad = asignacionAutomaticaRepositorio.FirstById(IdAsignacionAutomatica)?.IdOportunidad;

                    if (IdOportunidad != null)
                    {
                        int nroIntentos = 0;
                        bool flagValidado = false;

                        while (!flagValidado && nroIntentos < 10)
                        {
                            try
                            {
                                ValidarCasosOportunidad(IdOportunidad.Value, IdAsignacionAutomatica, true);
                                flagValidado = true;
                            }
                            catch (Exception ex)
                            {
                                nroIntentos++;
                                Thread.Sleep(3000);
                            }
                        }

                        if (nroIntentos == 10)
                        {
                            var _repLog = _unitOfWork.LogRepository;
                            _repLog.Insert(new TLog
                            {
                                Ip = "-",
                                Usuario = "-",
                                Maquina = "-",
                                Ruta = "CrearOportunidadWebHookFacebook",
                                Parametros = $"IdAsignacionAutomatica={IdAsignacionAutomatica}",
                                Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion",
                                Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion",
                                Tipo = "VALIDATE",
                                IdPadre = 0,
                                UsuarioCreacion = string.Empty,
                                UsuarioModificacion = string.Empty,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            });
                        }

                        resultados.Add(IdOportunidad.Value);
                    }
                }
                catch (Exception ex)
                {
                    var _repLog = _unitOfWork.LogRepository;
                    _repLog.Insert(new TLog
                    {
                        Ip = "-",
                        Usuario = "-",
                        Maquina = "-",
                        Ruta = "CrearOportunidadesWebHookFacebook",
                        Parametros = $"IdAsignacionAutomatica={IdAsignacionAutomatica}",
                        Mensaje = $"Error al procesar el ID {IdAsignacionAutomatica}: {ex.Message}",
                        Excepcion = ex.ToString(),
                        Tipo = "ERROR",
                        IdPadre = 0,
                        UsuarioCreacion = string.Empty,
                        UsuarioModificacion = string.Empty,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    });
                }
            }

            return resultados;
        }



        /// Tipo Función: POST
        /// Autor: Margiory 
        /// Fecha: 07/02/2024
        /// Versión: 1.0
        /// <summary>
        /// Registra en la tabla conf.T_Log
        /// </summary>
        /// <returns>Response 200, caso contrario Response 400 con el mensaje de error</returns>

        public bool RegistrarLogError(RegistroLogDTO RegistroLog)
        {
            try
            {
                var _repLog = _unitOfWork.LogRepository;
                _repLog.Insert(new TLog { Ip = RegistroLog.Ip, Usuario = RegistroLog.Usuario, Maquina = RegistroLog.Maquina, Ruta = RegistroLog.Ruta, Parametros = RegistroLog.Parametros, Mensaje = RegistroLog.Mensaje, Excepcion = RegistroLog.Excepcion, Tipo = RegistroLog.Tipo, IdPadre = RegistroLog.IdPadre, UsuarioCreacion = RegistroLog.UsuarioCreacion, UsuarioModificacion = RegistroLog.UsuarioModificacion, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory 
        /// Fecha: 07/02/2024
        /// <summary>
        /// Procesa el formulario nuevo del portal
        /// </summary>
        /// <param name="IdRegistroPortalWeb">Cadena con el registro del portal web</param>
        /// <param name="IdPagina">Id de la pagina de donde proviene el dato</param>
        /// <returns>Response 200 con el id de la asignacion automatica temporal</returns>

        public int ProcesarFormularioNuevoPortal(string IdRegistroPortalWeb, int IdPagina)
        {
            try
            {

                var AsignacionAutomaticaTemp = new AsignacionAutomaticaTempService(_unitOfWork);
                var resultado = AsignacionAutomaticaTemp.ProcesarRegistroFormularioNuevoPortalWeb(IdRegistroPortalWeb, IdPagina);
                //AsignacionAutomaticaTempRepositorio _asignacionAutomaticaTempRep = new AsignacionAutomaticaTempRepositorio(_integraDBContext);

                _unitOfWork.AsignacionAutomaticaTempRepository.Add(resultado);
                string[] listaAProcesar = new string[1];
                listaAProcesar[0] = IdRegistroPortalWeb;
                //foreach (string Procesado in listaAProcesar)
                //{

                //    try
                //    {
                //        _asignacionAutomaticaTempRep.MarcarComoProcesadoNuevoPortal(Procesado);

                //    }
                //    catch (Exception e)
                //    {
                //        continue;
                //    }
                //}

                if (IdPagina != 1)
                    IdPagina = 1;

                AsignacionAutomaticaTemp.MarcarComoProcesados(listaAProcesar, IdPagina);
                return (resultado.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: POST
        /// Autor: Flavio R.M.F.
        /// Fecha: 26/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Actualiza el alumno y crea la oportunidad de ventas
        /// </summary>
        /// <param name="formulario">Objeto de clase RegistroOportunidadAlumnoDTO</param>
        /// <returns>Response 200 con el objeto de clase OportunidadBO, caso contrario Response 400</returns>
        public bool ActualizarAlumnoCrearOportunidadVentas(RegistroOportunidadAlumnoDTO formulario)
        {
            try
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                var alumno = _unitOfWork.AlumnoRepository.ObtenerPorId(formulario.Alumno.Id);
                if (alumno == null)
                {
                    throw new BadRequestException("El alumno no existe");
                }
                alumno.Nombre1 = formulario.Alumno.Nombre1;
                alumno.Nombre2 = formulario.Alumno.Nombre2;
                alumno.ApellidoPaterno = formulario.Alumno.ApellidoPaterno;
                alumno.ApellidoMaterno = formulario.Alumno.ApellidoMaterno;
                alumno.Direccion = formulario.Alumno.Direccion;
                alumno.Telefono = formulario.Alumno.Telefono;
                alumno.Celular = formulario.Alumno.Celular;
                //alumno.Email1 = Formulario.Alumno.Email1;
                alumno.Email2 = formulario.Alumno.Email2;
                alumno.IdCargo = formulario.Alumno.IdCargo;
                alumno.IdAformacion = formulario.Alumno.IdAFormacion;
                alumno.IdAtrabajo = formulario.Alumno.IdATrabajo;
                alumno.IdIndustria = formulario.Alumno.IdIndustria;
                alumno.IdReferido = formulario.Alumno.IdReferido;
                alumno.IdCodigoPais = formulario.Alumno.IdCodigoPais;
                alumno.IdCiudad = formulario.Alumno.IdCodigoCiudad;
                alumno.HoraContacto = formulario.Alumno.HoraContacto;
                alumno.HoraPeru = formulario.Alumno.HoraPeru;
                var empresaAlumno = formulario.Alumno.IdEmpresa;
                alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                alumno.IdEmpresa = formulario.Alumno.IdEmpresa;
                alumno.FechaModificacion = DateTime.Now;
                alumno.UsuarioModificacion = formulario.Usuario;

                OportunidadBoDTO oportunidad = new OportunidadBoDTO();
                oportunidad.IdCentroCosto = formulario.Oportunidad.IdCentroCosto;
                oportunidad.IdPersonalAsignado = formulario.Oportunidad.IdPersonal_Asignado;
                oportunidad.IdTipoDato = formulario.Oportunidad.IdTipoDato;
                oportunidad.IdFaseOportunidad = formulario.Oportunidad.IdFaseOportunidad;
                oportunidad.IdOrigen = formulario.Oportunidad.IdOrigen;
                oportunidad.UltimoComentario = formulario.Oportunidad.UltimoComentario;
                oportunidad.IdTipoInteraccion = formulario.Oportunidad.fk_id_tipointeraccion;
                oportunidad.Estado = true;
                oportunidad.FechaRegistroCampania = formulario.FechaRegistroCampania ?? DateTime.Now;
                oportunidad.UsuarioCreacion = formulario.Usuario;
                oportunidad.UsuarioModificacion = formulario.Usuario;
                oportunidad.FechaCreacion = DateTime.Now;
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.Alumno = alumno;
                oportunidad.IdAlumno = alumno.Id;

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;

                CrearOportunidadActualizarPersona(ref oportunidad, false, TipoPersona.Alumno);

                IAgendaService agendaService = new AgendaService(_unitOfWork);
                // 827 Correo Informacion del Curso Completo
                agendaService.EnviarCorreoOportunidadAutomatico(oportunidad.Id, 1967, "Automatico1967");

                ///01/02/2021
                ///Calculo nuevo modelo predictivo
                ///Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                }

                try
                {
                    MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                    //solo si no funciona MetodoODyOM
                }

                try
                {
                    EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1456}" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 26/06/2026
        /// Versión: 1.0
        /// <summary>
        /// Se crea la oportunidad y el alumno en ventas
        /// </summary>
        /// <returns>ActionResult con estado 200 con objeto anonimo (Ok en cadena y el objeto de clase OportunidadBO)</returns>
        public bool CrearOportunidadCrearAlumnoVentas(RegistroOportunidadAlumnoDTO formulario)
        {
            try
            {
                var email1 = Regex.Replace(formulario.Alumno.Email1, @"\s", "");
                var email2 = Regex.Replace(formulario.Alumno.Email2, @"\s", "");

                Alumno alumno = new Alumno
                {
                    Nombre1 = formulario.Alumno.Nombre1,
                    Nombre2 = formulario.Alumno.Nombre2,
                    ApellidoPaterno = formulario.Alumno.ApellidoPaterno,
                    ApellidoMaterno = formulario.Alumno.ApellidoMaterno,
                    Direccion = formulario.Alumno.Direccion,
                    Telefono = formulario.Alumno.Telefono,
                    Celular = formulario.Alumno.Celular,
                    Email1 = email1,
                    Email2 = email2,
                    IdCargo = formulario.Alumno.IdCargo,
                    IdAformacion = formulario.Alumno.IdAFormacion,
                    IdAtrabajo = formulario.Alumno.IdATrabajo,
                    IdIndustria = formulario.Alumno.IdIndustria,
                    IdReferido = formulario.Alumno.IdReferido,
                    IdCodigoPais = formulario.Alumno.IdCodigoPais,
                    IdCiudad = formulario.Alumno.IdCodigoCiudad,
                    HoraContacto = formulario.Alumno.HoraContacto,
                    HoraPeru = formulario.Alumno.HoraPeru,
                    IdEmpresa = (formulario.Alumno.IdEmpresa == 0 || formulario.Alumno.IdEmpresa == -1) ? null : formulario.Alumno.IdEmpresa,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = formulario.Usuario,
                    UsuarioModificacion = formulario.Usuario,
                    Comentario = formulario.Alumno.Comentario
                };

                OportunidadBoDTO oportunidad = new OportunidadBoDTO
                {
                    IdCentroCosto = formulario.Oportunidad.IdCentroCosto,
                    IdPersonalAsignado = formulario.Oportunidad.IdPersonal_Asignado,
                    IdTipoDato = formulario.Oportunidad.IdTipoDato,
                    IdFaseOportunidad = formulario.Oportunidad.IdFaseOportunidad,
                    IdOrigen = formulario.Oportunidad.IdOrigen,
                    UltimoComentario = formulario.Oportunidad.UltimoComentario,
                    IdTipoInteraccion = formulario.Oportunidad.fk_id_tipointeraccion,
                    FechaRegistroCampania = formulario.FechaRegistroCampania ?? DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = formulario.Usuario,
                    UsuarioModificacion = formulario.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Alumno = alumno
                };

                if (oportunidad.UltimaFechaProgramada != null)
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                else
                    oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;

                CrearOportunidadCrearPersona(ref oportunidad, false, TipoPersona.Alumno);

                try
                {
                    var nuevaProbabilidad = ObtenerProbabilidadModeloPredictivo(oportunidad.Id);
                }
                catch (Exception e)
                {
                }

                try
                {
                    MetodoODyOM(oportunidad.Id);
                }
                catch (Exception ex)
                {
                }

                // Mailing
                try
                {
                    EnviarCorreoOportunidad(oportunidad.Id);
                }
                catch (Exception)
                {
                }

                // SMS
                try
                {
                    string uriSms = string.Empty;

                    if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 21) // Horario permitido para el envio de Sms
                    {
                        if (DateTime.Now.Hour == 18)
                            uriSms = DateTime.Now.Minute < 30 ? $"https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1457}" : "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{1457}";
                        else if (DateTime.Now.Hour > 18)
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Tarde}";
                        else
                            uriSms = "https://integrav5-servicios.bsginstitute.com/api/Alumno/EnvioSmsOportunidadPlantilla/{oportunidad.Id}/{ValorEstatico.IdRecordatorioSms01Maniana}";
                    }

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(uriSms);
                    }
                }
                catch (Exception)
                {
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object ProcesarOportunidadesMkt(IFormFile file, string usuario)
        {
            if (file == null || file.Length <= 0)
            {
                throw new BadRequestException("No se ha proporcionado un archivo o el archivo está vacío.");
            }

            try
            {
                List<InformacionBaseOportunidad> listaDatos = new List<InformacionBaseOportunidad>();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        InformacionBaseOportunidad datos = new InformacionBaseOportunidad();

                        datos.Nombres = worksheet.Cells[row, 1].Value?.ToString();
                        datos.Apellidos = worksheet.Cells[row, 2].Value?.ToString();
                        datos.Correo = worksheet.Cells[row, 3].Value?.ToString();
                        datos.Celular = worksheet.Cells[row, 4].Value?.ToString();
                        datos.Pais = worksheet.Cells[row, 5].Value?.ToString();
                        datos.Cargo = worksheet.Cells[row, 6].Value?.ToString();
                        datos.AreaFormacion = worksheet.Cells[row, 7].Value?.ToString();
                        datos.AreaTrabajo = worksheet.Cells[row, 8].Value?.ToString();
                        datos.Industria = worksheet.Cells[row, 9].Value?.ToString();
                        datos.CentroCosto = worksheet.Cells[row, 10].Value?.ToString();
                        datos.Origen = worksheet.Cells[row, 11].Value?.ToString();
                        datos.Asesor = worksheet.Cells[row, 12].Value?.ToString();
                        datos.TipoDato = worksheet.Cells[row, 13].Value?.ToString();
                        datos.FaseOportunidad = worksheet.Cells[row, 14].Value?.ToString();

                        listaDatos.Add(datos);
                    }
                }
                var resultado = ProcesarInformacionOportunidades(listaDatos, usuario);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ProcesarOportunidadesMktVersionLinkedIn(IFormFile file, string usuario)
        {
            if (file == null || file.Length <= 0)
            {
                throw new BadRequestException("No se ha proporcionado un archivo o el archivo está vacío.");
            }

            try
            {
                List<InformacionBaseOportunidad> listaDatos = new List<InformacionBaseOportunidad>();

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        InformacionBaseOportunidad datos = new InformacionBaseOportunidad();

                        datos.Nombres = worksheet.Cells[row, 1].Value?.ToString();
                        datos.Apellidos = worksheet.Cells[row, 2].Value?.ToString();
                        datos.Correo = worksheet.Cells[row, 3].Value?.ToString();
                        datos.Celular = worksheet.Cells[row, 4].Value?.ToString();
                        datos.Pais = worksheet.Cells[row, 5].Value?.ToString();
                        datos.Cargo = worksheet.Cells[row, 6].Value?.ToString();
                        datos.AreaFormacion = worksheet.Cells[row, 7].Value?.ToString();
                        datos.AreaTrabajo = worksheet.Cells[row, 8].Value?.ToString();
                        datos.Industria = worksheet.Cells[row, 9].Value?.ToString();
                        datos.CentroCosto = worksheet.Cells[row, 10].Value?.ToString();
                        datos.Origen = worksheet.Cells[row, 11].Value?.ToString();
                        datos.Asesor = worksheet.Cells[row, 12].Value?.ToString();
                        datos.TipoDato = worksheet.Cells[row, 13].Value?.ToString();
                        datos.FaseOportunidad = worksheet.Cells[row, 14].Value?.ToString();


                        var celdaFecha = worksheet.Cells[row, 15].Value;

                        if (celdaFecha is double excelDate)
                        {

                            datos.FechaRegistroCampania = DateTime.FromOADate(excelDate);
                        }
                        else if (celdaFecha is DateTime fechaDirecta)
                        {

                            datos.FechaRegistroCampania = fechaDirecta;
                        }
                        else if (DateTime.TryParse(celdaFecha?.ToString(), out DateTime fechaString))
                        {

                            datos.FechaRegistroCampania = fechaString;
                        }
                        else
                        {

                            datos.FechaRegistroCampania = DateTime.Now;
                        }

                        listaDatos.Add(datos);
                    }
                }
                var resultado = ProcesarInformacionOportunidades(listaDatos, usuario);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<InformacionBaseOportunidad> ProcesarInformacionOportunidades(List<InformacionBaseOportunidad> datos, string usuario)
        {
            try
            {
                var datosCorrectos = new List<InformacionBaseOportunidad>();
                var datosIncorrectos = new List<InformacionBaseOportunidad>();

                var listaCargos = _unitOfWork.CargoRepository.ObtenerCombo();
                var listaAreaFormacion = _unitOfWork.AreaFormacionRepository.ObtenerCombo();
                var listaAreaTrabajo = _unitOfWork.AreaTrabajoRepository.ObtenerCombo();
                var listaIndustria = _unitOfWork.IndustriaRepository.ObtenerCombo();
                var listaPaises = _unitOfWork.PaisRepository.ObtenerCombo();
                datos.ForEach(opo =>
                {
                    try
                    {
                        var idAlumno = 0;
                        //validar alumno
                        var alumno = _unitOfWork.AlumnoRepository.FirstBy(x => !string.IsNullOrEmpty(x.Email1) && x.Email1.ToLower() == opo.Correo.Trim().ToLower());
                        if (alumno == null)
                        {
                            alumno = _unitOfWork.AlumnoRepository.FirstBy(x => !string.IsNullOrEmpty(x.Email2) && x.Email2.ToLower() == opo.Correo.Trim().ToLower());
                            if (alumno != null)
                            {
                                idAlumno = alumno.Id;
                            }
                            else
                            {
                                alumno = new();
                            }
                        }
                        else
                        {
                            idAlumno = alumno.Id;
                        }
                        IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                        var nombres = opo.Nombres.Trim().Split(" ");
                        var nombre1 = string.Empty;
                        var nombre2 = string.Empty;

                        if (nombres.Count() > 0)
                        {
                            if (nombres.Count() == 1)
                            {
                                nombre1 = nombres.ElementAt(0);
                            }
                            else
                            {
                                nombre1 = nombres.ElementAt(0);
                                nombre2 = string.Join(" ", nombres.Skip(1).ToList());
                            }
                        }

                        var apellidos = opo.Apellidos.Trim().Split(" ");
                        var apellidoPaterno = string.Empty;
                        var apellidoMaterno = string.Empty;

                        if (apellidos.Count() > 0)
                        {
                            if (apellidos.Count() == 1)
                            {
                                apellidoPaterno = apellidos.ElementAt(0);
                            }
                            else
                            {
                                apellidoPaterno = apellidos.ElementAt(0);
                                apellidoMaterno = string.Join(" ", apellidos.Skip(1).ToList());
                            }
                        }

                        var cargo = listaCargos.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Cargo).ToLower());
                        var idCargo = 24;
                        if (cargo != null)
                        {
                            idCargo = cargo.Id;
                        }

                        var aFormacion = listaAreaFormacion.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.AreaFormacion).ToLower());
                        var idAFormacion = 153;
                        if (aFormacion != null)
                        {
                            idAFormacion = aFormacion.Id;
                        }

                        var aTrabajo = listaAreaTrabajo.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.AreaTrabajo).ToLower());
                        var idATrabajo = 27;
                        if (aTrabajo != null)
                        {
                            idATrabajo = aTrabajo.Id;
                        }

                        var industria = listaIndustria.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Industria).ToLower());
                        var idIndustria = 24;
                        if (industria != null)
                        {
                            idIndustria = industria.Id;
                        }

                        var pais = listaPaises.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Pais).ToLower());
                        var idPais = 0;
                        if (pais != null)
                        {
                            idPais = pais.Id;
                        }



                        var centroCosto = _unitOfWork.CentroCostoRepository.FirstBy(x => x.Nombre == opo.CentroCosto.Trim());
                        var idCentroCosto = 0;
                        if (centroCosto != null)
                        {
                            idCentroCosto = centroCosto.Id;
                        }
                        else
                        {
                            throw new BadRequestException("No se encontro el centro costo");
                        }

                        var origen = _unitOfWork.OrigenRepository.FirstBy(x => x.Nombre == opo.Origen);
                        var idOrigen = 0;
                        if (origen != null)
                        {
                            idOrigen = origen.Id;
                        }
                        else
                        {
                            throw new BadRequestException("No se encontro el origen");
                        }

                        string celular = new string(opo.Celular.Where(char.IsDigit).ToArray());

                        var dtoOportunidad = new OportunidadFormularioDTO();
                        dtoOportunidad.Id = 0;

                        dtoOportunidad.IdCentroCosto = idCentroCosto;
                        dtoOportunidad.IdFaseOportunidad = ValorEstatico.IdFaseOportunidadBNC;
                        dtoOportunidad.IdOrigen = idOrigen;
                        dtoOportunidad.IdPersonal_Asignado = ValorEstatico.IdPersonalAsignacionAutomatica;
                        dtoOportunidad.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                        dtoOportunidad.UltimoComentario = string.Empty;
                        dtoOportunidad.fk_id_tipointeraccion = 18;

                        if (idAlumno == 0)
                        {
                            dtoOportunidad.IdAlumno = 0;

                            var alumnoDTO = new AlumnoFormularioOportunidadDTO();
                            alumnoDTO.Id = 0;
                            alumnoDTO.Nombre1 = nombre1;
                            alumnoDTO.Nombre2 = nombre2;
                            alumnoDTO.ApellidoPaterno = apellidoPaterno;
                            alumnoDTO.ApellidoMaterno = apellidoMaterno;
                            alumnoDTO.DNI = string.Empty;
                            alumnoDTO.Direccion = string.Empty;
                            alumnoDTO.Telefono = string.Empty;
                            alumnoDTO.Celular = celular;
                            alumnoDTO.Email1 = opo.Correo.Trim();
                            alumnoDTO.Email2 = string.Empty;
                            alumnoDTO.IdCargo = idCargo;
                            alumnoDTO.IdAFormacion = idAFormacion;
                            alumnoDTO.IdATrabajo = idATrabajo;
                            alumnoDTO.IdIndustria = idIndustria;
                            alumnoDTO.IdReferido = null;
                            alumnoDTO.IdCodigoPais = idPais;
                            alumnoDTO.IdCodigoCiudad = null;
                            alumnoDTO.HoraContacto = null;
                            alumnoDTO.HoraPeru = null;
                            alumnoDTO.Telefono2 = string.Empty;
                            alumnoDTO.Celular2 = string.Empty;
                            alumnoDTO.IdEmpresa = null;
                            alumnoDTO.Comentario = string.Empty;

                            var dto = new RegistroOportunidadAlumnoDTO()
                            {
                                Alumno = alumnoDTO,
                                Oportunidad = dtoOportunidad,
                                FechaRegistroCampania = opo.FechaRegistroCampania,
                                Usuario = usuario
                            };
                            CrearOportunidadCrearAlumnoVentas(dto);
                        }
                        else
                        {
                            dtoOportunidad.IdAlumno = alumno.Id;
                            var alumnoDTO = new AlumnoFormularioOportunidadDTO();
                            alumnoDTO.Id = alumno.Id;
                            alumnoDTO.Nombre1 = nombre1;
                            alumnoDTO.Nombre2 = nombre2;
                            alumnoDTO.ApellidoPaterno = apellidoPaterno;
                            alumnoDTO.ApellidoMaterno = apellidoMaterno;
                            alumnoDTO.DNI = alumno.Dni;
                            alumnoDTO.Direccion = alumno.Direccion;
                            alumnoDTO.Telefono = alumno.Telefono;
                            alumnoDTO.Celular = celular;
                            alumnoDTO.Email1 = alumno.Email1;
                            alumnoDTO.Email2 = alumno.Email2;
                            alumnoDTO.IdCargo = idCargo;
                            alumnoDTO.IdAFormacion = idAFormacion;
                            alumnoDTO.IdATrabajo = idATrabajo;
                            alumnoDTO.IdIndustria = idIndustria;
                            alumnoDTO.IdReferido = alumno.IdReferido;
                            alumnoDTO.IdCodigoPais = alumno.IdCodigoPais;
                            alumnoDTO.IdCodigoCiudad = alumno.IdCiudad;
                            alumnoDTO.HoraContacto = alumno.HoraContacto;
                            alumnoDTO.HoraPeru = alumno.HoraPeru;
                            alumnoDTO.Telefono2 = alumno.Telefono2;
                            alumnoDTO.Celular2 = alumno.Celular2;
                            alumnoDTO.IdEmpresa = alumno.IdEmpresa;
                            alumnoDTO.Comentario = alumno.Comentario;

                            var dto = new RegistroOportunidadAlumnoDTO()
                            {
                                Alumno = alumnoDTO,
                                Oportunidad = dtoOportunidad,
                                FechaRegistroCampania = opo.FechaRegistroCampania,
                                Usuario = usuario
                            };
                            ActualizarAlumnoCrearOportunidadVentas(dto);
                        }
                        datosCorrectos.Add(opo);
                    }
                    catch (Exception ex)
                    {
                        datosIncorrectos.Add(opo);
                    }
                });
                return datosIncorrectos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InformacionBaseOportunidad> ProcesarInformacionOportunidadesLinkedIn(List<InformacionBaseOportunidad> oportunidadBaseLinkedIn, string usuario)
        {
            try
            {
                var datosCorrectos = new List<InformacionBaseOportunidad>();
                var datosIncorrectos = new List<InformacionBaseOportunidad>();

                var listaCargos = _unitOfWork.CargoRepository.ObtenerCombo();
                var listaAreaFormacion = _unitOfWork.AreaFormacionRepository.ObtenerCombo();
                var listaAreaTrabajo = _unitOfWork.AreaTrabajoRepository.ObtenerCombo();
                var listaIndustria = _unitOfWork.IndustriaRepository.ObtenerCombo();
                var listaPaises = _unitOfWork.PaisRepository.ObtenerCombo();
                oportunidadBaseLinkedIn.ForEach(opo =>
                {
                    try
                    {
                        var idAlumno = 0;
                        //validar alumno
                        var alumno = _unitOfWork.AlumnoRepository.FirstBy(x => !string.IsNullOrEmpty(x.Email1) && x.Email1.ToLower() == opo.Correo.Trim().ToLower());
                        if (alumno == null)
                        {
                            alumno = _unitOfWork.AlumnoRepository.FirstBy(x => !string.IsNullOrEmpty(x.Email2) && x.Email2.ToLower() == opo.Correo.Trim().ToLower());
                            if (alumno != null)
                            {
                                idAlumno = alumno.Id;
                            }
                            else
                            {
                                alumno = new();
                            }
                        }
                        else
                        {
                            idAlumno = alumno.Id;
                        }
                        IAlumnoService alumnoService = new AlumnoService(_unitOfWork);

                        var nombres = opo.Nombres.Trim().Split(" ");
                        var nombre1 = string.Empty;
                        var nombre2 = string.Empty;

                        if (nombres.Count() > 0)
                        {
                            if (nombres.Count() == 1)
                            {
                                nombre1 = nombres.ElementAt(0);
                            }
                            else
                            {
                                nombre1 = nombres.ElementAt(0);
                                nombre2 = string.Join(" ", nombres.Skip(1).ToList());
                            }
                        }

                        var apellidos = opo.Apellidos.Trim().Split(" ");
                        var apellidoPaterno = string.Empty;
                        var apellidoMaterno = string.Empty;

                        if (apellidos.Count() > 0)
                        {
                            if (apellidos.Count() == 1)
                            {
                                apellidoPaterno = apellidos.ElementAt(0);
                            }
                            else
                            {
                                apellidoPaterno = apellidos.ElementAt(0);
                                apellidoMaterno = string.Join(" ", apellidos.Skip(1).ToList());
                            }
                        }

                        var cargo = listaCargos.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Cargo).ToLower());
                        var idCargo = 24;
                        if (cargo != null)
                        {
                            idCargo = cargo.Id;
                        }

                        var aFormacion = listaAreaFormacion.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.AreaFormacion).ToLower());
                        var idAFormacion = 153;
                        if (aFormacion != null)
                        {
                            idAFormacion = aFormacion.Id;
                        }

                        var aTrabajo = listaAreaTrabajo.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.AreaTrabajo).ToLower());
                        var idATrabajo = 27;
                        if (aTrabajo != null)
                        {
                            idATrabajo = aTrabajo.Id;
                        }

                        var industria = listaIndustria.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Industria).ToLower());
                        var idIndustria = 24;
                        if (industria != null)
                        {
                            idIndustria = industria.Id;
                        }

                        var pais = listaPaises.FirstOrDefault(x => LimpiarCadena(x.Nombre).ToLower() == LimpiarCadena(opo.Pais).ToLower());
                        var idPais = 0;
                        if (pais != null)
                        {
                            idPais = pais.Id;
                        }



                        int? idCentroCosto = _unitOfWork.CentroCostoRepository.ObtenerIdPorNombre(opo.CentroCosto);
                        if (idCentroCosto == null || idCentroCosto == 0)
                            throw new BadRequestException($"Error: No existe el centro costo {opo.CentroCosto}");

                        int idOrigen = _unitOfWork.OrigenRepository.ObtenerIdPorNombre(opo.Origen);
                        if (idOrigen == 0)
                            throw new BadRequestException($"Error: No existe el origen {opo.Origen}");


                        string celular = new string(opo.Celular.Where(char.IsDigit).ToArray());

                        var dtoOportunidad = new OportunidadFormularioDTO();
                        dtoOportunidad.Id = 0;

                        dtoOportunidad.IdCentroCosto = idCentroCosto.Value;
                        dtoOportunidad.IdFaseOportunidad = ValorEstatico.IdFaseOportunidadBNC;
                        dtoOportunidad.IdOrigen = idOrigen;
                        dtoOportunidad.IdPersonal_Asignado = ValorEstatico.IdPersonalAsignacionAutomatica;
                        dtoOportunidad.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                        dtoOportunidad.UltimoComentario = string.Empty;
                        dtoOportunidad.fk_id_tipointeraccion = 18;

                        if (idAlumno == 0)
                        {
                            dtoOportunidad.IdAlumno = 0;

                            var alumnoDTO = new AlumnoFormularioOportunidadDTO();
                            alumnoDTO.Id = 0;
                            alumnoDTO.Nombre1 = nombre1;
                            alumnoDTO.Nombre2 = nombre2;
                            alumnoDTO.ApellidoPaterno = apellidoPaterno;
                            alumnoDTO.ApellidoMaterno = apellidoMaterno;
                            alumnoDTO.DNI = string.Empty;
                            alumnoDTO.Direccion = string.Empty;
                            alumnoDTO.Telefono = string.Empty;
                            alumnoDTO.Celular = celular;
                            alumnoDTO.Email1 = opo.Correo.Trim();
                            alumnoDTO.Email2 = string.Empty;
                            alumnoDTO.IdCargo = idCargo;
                            alumnoDTO.IdAFormacion = idAFormacion;
                            alumnoDTO.IdATrabajo = idATrabajo;
                            alumnoDTO.IdIndustria = idIndustria;
                            alumnoDTO.IdReferido = null;
                            alumnoDTO.IdCodigoPais = idPais;
                            alumnoDTO.IdCodigoCiudad = null;
                            alumnoDTO.HoraContacto = null;
                            alumnoDTO.HoraPeru = null;
                            alumnoDTO.Telefono2 = string.Empty;
                            alumnoDTO.Celular2 = string.Empty;
                            alumnoDTO.IdEmpresa = null;
                            alumnoDTO.Comentario = string.Empty;

                            var dto = new RegistroOportunidadAlumnoDTO()
                            {
                                Alumno = alumnoDTO,
                                Oportunidad = dtoOportunidad,
                                Usuario = usuario
                            };
                            CrearOportunidadCrearAlumnoVentas(dto);
                        }
                        else
                        {
                            dtoOportunidad.IdAlumno = alumno.Id;
                            var alumnoDTO = new AlumnoFormularioOportunidadDTO();
                            alumnoDTO.Id = alumno.Id;
                            alumnoDTO.Nombre1 = nombre1;
                            alumnoDTO.Nombre2 = nombre2;
                            alumnoDTO.ApellidoPaterno = apellidoPaterno;
                            alumnoDTO.ApellidoMaterno = apellidoMaterno;
                            alumnoDTO.DNI = alumno.Dni;
                            alumnoDTO.Direccion = alumno.Direccion;
                            alumnoDTO.Telefono = alumno.Telefono;
                            alumnoDTO.Celular = celular;
                            alumnoDTO.Email1 = alumno.Email1;
                            alumnoDTO.Email2 = alumno.Email2;
                            alumnoDTO.IdCargo = idCargo;
                            alumnoDTO.IdAFormacion = idAFormacion;
                            alumnoDTO.IdATrabajo = idATrabajo;
                            alumnoDTO.IdIndustria = idIndustria;
                            alumnoDTO.IdReferido = alumno.IdReferido;
                            alumnoDTO.IdCodigoPais = alumno.IdCodigoPais;
                            alumnoDTO.IdCodigoCiudad = alumno.IdCiudad;
                            alumnoDTO.HoraContacto = alumno.HoraContacto;
                            alumnoDTO.HoraPeru = alumno.HoraPeru;
                            alumnoDTO.Telefono2 = alumno.Telefono2;
                            alumnoDTO.Celular2 = alumno.Celular2;
                            alumnoDTO.IdEmpresa = alumno.IdEmpresa;
                            alumnoDTO.Comentario = alumno.Comentario;

                            var dto = new RegistroOportunidadAlumnoDTO()
                            {
                                Alumno = alumnoDTO,
                                Oportunidad = dtoOportunidad,
                                FechaRegistroCampania = opo.FechaRegistroCampania,
                                Usuario = usuario
                            };
                            ActualizarAlumnoCrearOportunidadVentas(dto);

                        }
                        var guidLinkedin = new StringDTO()
                        {
                            Valor = opo.GuidLinkedInLead,

                        };
                        _unitOfWork.LinkedInApiRepository.ActualizarOportunidadLead(guidLinkedin);
                        datosCorrectos.Add(opo);
                    }
                    catch (Exception ex)
                    {
                        datosIncorrectos.Add(opo);
                    }
                });
                return datosIncorrectos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private string LimpiarCadena(string valor)
        {
            string valorSinTildes = Regex.Replace(valor.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            string cadenaLimpia = Regex.Replace(valorSinTildes, @"\s+", " ");
            return cadenaLimpia.Trim();
        }
        public List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO> ObtenerComentariosOperacionesPagosAcademicos2(int idOportunidad)
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerComentariosOperacionesPagosAcademicos2(idOportunidad);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<ArchivoOportunidadDTO> ObtenerArchivosOportunidad()
        {
            try
            {
                return _unitOfWork.OportunidadRepository.ObtenerArchivosOportunidad();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Crea la oportunidad enviada como objeto y su sincronizacion con V3, ademas de la creacion del alumno o actualizacion incluyendo la persona
        /// </summary>
        /// <param name="Oportunidad">Referencia de Objeto de clase Oportunidad BO</param>
        public void CrearOportunidadMarketing(ref OportunidadBoDTO Oportunidad)
        {
            try
            {
                if (Oportunidad.UltimaFechaProgramada != null)
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadProgramada;
                }
                else
                {
                    Oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadNoProgramada;
                }

                if (Oportunidad.Alumno.Id == 0)
                {
                    CrearOportunidadCrearPersona(ref Oportunidad, false, TipoPersona.Alumno);
                    IAgendaService agendaService = new AgendaService(_unitOfWork);
                    // 827 Correo Informacion del Curso Completo
                    agendaService.EnviarCorreoOportunidadAutomatico(Oportunidad.Id, 1967, "Automatico1967");
                }
                else
                {
                    CrearOportunidadActualizarPersona(ref Oportunidad, false, TipoPersona.Alumno);
                    IAgendaService agendaService = new AgendaService(_unitOfWork);
                    // 827 Correo Informacion del Curso Completo
                    agendaService.EnviarCorreoOportunidadAutomatico(Oportunidad.Id, 1967, "Automatico1967");
                }

                // 01/02/2021
                // Calculo nuevo modelo predictivo
                // Carlos Crispin Riquelme
                try
                {
                    var nuevaProbabilidad = ObtenerProbabilidadModeloPredictivo(Oportunidad.Id);

                }
                catch (Exception e)
                {
                    //throw;
                }

                /////////////////////////////////SE VA HA ELIMINAR YA QUE ESTO SINCRONIZAVA A V3/////////////////////////////////////////////
                //////////Insertar actualizar alumno a v3
                ////////try
                ////////{
                ////////    //string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/SincronizarAlumnoAV3?IdAlumno=" + Oportunidad.Alumno.Id.ToString() + "&EsCrear=true";
                ////////    string URI = "https://integrav4-syncv3.bsginstitute.com/Marketing/InsertarActualizarOportunidadAlumno?IdOportunidad=" + Oportunidad.Id;
                ////////    using (WebClient wc = new WebClient())
                ////////    {
                ////////        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                ////////        wc.DownloadString(URI);
                ////////    }
                ////////}
                ////////catch (Exception e)
                ////////{
                ////////}
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                //return Oportunidad.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}