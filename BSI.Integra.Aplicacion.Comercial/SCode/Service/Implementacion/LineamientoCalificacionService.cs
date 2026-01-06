using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.EMMA;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;
using TransicionFase = BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial.TransicionFase;


namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion
{
    /// Service: LineamientoCalificacionService
    /// Autor: Joseph Llanque.
    /// Fecha: 03/07/2025
    /// <summary>
    /// Gestión general de TLineamientoCalificacion
    /// </summary>
    public class LineamientoCalificacionService : ILineamientoCalificacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LineamientoCalificacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TLineamientoCalificacion, LineamientoCalificacion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public LineamientoCalificacion Add(LineamientoCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LineamientoCalificacion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LineamientoCalificacion Update(LineamientoCalificacion entidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LineamientoCalificacion>(modelo);
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
                _unitOfWork.LineamientoCalificacionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LineamientoCalificacion> Add(List<LineamientoCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LineamientoCalificacion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LineamientoCalificacion> Update(List<LineamientoCalificacion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LineamientoCalificacionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LineamientoCalificacion>>(modelo);
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
                _unitOfWork.LineamientoCalificacionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> LineamientoCalificacion </returns>
        public LineamientoCalificacion ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<LineamientoCalificacion> ObtenerLineamiento()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerLineamiento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionDTO> EsquemaCalificacionConfigurado()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.EsquemaCalificacionConfigurado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamada()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.HistorialVersionCalificacionLlamada();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamadaPorAreaTrabajo(int IdPersonalAreaTrabajo)
        {
            try
            {
                // Obtener todas las versiones históricas para el área de trabajo
                var versiones = _unitOfWork.LineamientoCalificacionRepository
                    .HistorialVersionCalificacionLlamadaV2(IdPersonalAreaTrabajo)
                    .ToList();

                foreach (var version in versiones)
                {
                    // Para cada versión, obtener la configuración correspondiente
                    var configuracion = _unitOfWork.LineamientoCalificacionRepository
                        .ObtenerDataConfiguracionPorVersion(version.Id, IdPersonalAreaTrabajo);

                    if (configuracion != null && configuracion.Any())
                    {
                        // Construir el objeto estructurado usando MapearDataEstructurada (o tu método preferido)
                        var dataEstructurada = MapearDataEstructurada(configuracion);

                        // Serializar el objeto a JSON
                        version.ConfiguracionJSON = JsonSerializer.Serialize(dataEstructurada);
                    }
                    else
                    {
                        version.ConfiguracionJSON = "{}";
                    }
                }

                return versiones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Jose Vega
        /// Fecha: 20/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de versiones de configuración de calificación para un área específica, 
        /// construyendo y serializando el JSON de configuración detallada para cada versión.
        /// </summary>
        /// <param name="idPersonalAreaTrabajo">Identificador del área de trabajo del personal</param>
        /// <returns> IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> </returns>
        public IEnumerable<ConfiguracionEsquemaCalificacionLlamdaDTO> HistorialVersionCalificacionLlamadaV2(int idPersonalAreaTrabajo)
        {
            try
            {
                var historial = _unitOfWork.LineamientoCalificacionRepository
                                           .HistorialVersionCalificacionLlamadaV2(idPersonalAreaTrabajo)
                                           .ToList();

                foreach (var version in historial)
                {
                    var listaPlana = _unitOfWork.LineamientoCalificacionRepository
                                                .ObtenerDataConfiguracionPorVersion(version.Id, idPersonalAreaTrabajo);
                    var dataEstructurada = MapearDataEstructurada(listaPlana);
                    version.ConfiguracionJSON = JsonSerializer.Serialize(dataEstructurada);
                }

                return historial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 20/11/2025
        /// Version: 1.0
        /// <summary>
        /// Transforma la lista plana y mezclada obtenida del Stored Procedure en un objeto contenedor con listas tipadas.
        /// Realiza la segregación por 'TipoEntidad' y aplica el ordenamiento necesario en memoria.
        /// </summary>
        /// <param name="listaPlana">Colección plana de objetos jerárquicos provenientes de la base de datos.</param>
        /// <returns> Objeto ConfiguracionLineamientoV2DTO con las colecciones separadas y listas para serializar. </returns>
        private ConfiguracionLineamientoV2DTO MapearDataEstructurada(List<EvaluacionLlamadaJerarquicaDTO> listaPlana)
        {
            var data = new ConfiguracionLineamientoV2DTO();

            if (listaPlana != null && listaPlana.Any())
            {
                data.FasesCalificacion = listaPlana
                    .Where(x => x.TipoEntidad == "FASE")
                    .GroupBy(x => x.Id)
                    .Select(g => g.First())
                    .OrderBy(x => x.Orden)
                    .Select(x => new EvaluacionLlamadaFaseDTO
                    {
                        Id = x.Id,
                        NombreFase = x.Nombre,
                        Orden = x.Orden ?? 0,
                        Descripcion = x.Descripcion
                    }).ToList();

                data.CriteriosCalificacion = listaPlana
                    .Where(x => x.TipoEntidad == "CRITERIO")
                    .GroupBy(x => x.Id)
                    .Select(g => g.First())
                    .Select(x => new EvaluacionLlamadaCriterioDTO
                    {
                        Id = x.Id,
                        NombreCriterio = x.Nombre,
                        IdFaseCalificacion = x.IdPadre ?? 0,
                        Orden = x.Orden ?? 0,
                        Descripcion = x.Descripcion
                    }).ToList();

                data.LineamientosCalificacion = listaPlana
                    .Where(x => x.TipoEntidad == "LINEAMIENTO")
                    .GroupBy(x => x.Id)
                    .Select(g => g.First())
                    .Select(x => new EvaluacionLlamadaLineamientoDTO
                    {
                        Id = x.Id,
                        NombreLineamiento = x.Nombre,
                        IdCriterioCalificacionLlamada = x.IdPadre ?? 0,
                        IdCriticidadCalificacion = x.IdCriticidad ?? 0,
                        Orden = x.Orden ?? 0,
                        Descripcion = x.Descripcion
                    }).ToList();

                data.CriticidadCalificacion = listaPlana
                    .Where(x => x.TipoEntidad == "CRITICIDAD")
                    .GroupBy(x => x.Id)
                    .Select(g => g.First())
                    .Select(x => new EvaluacionLlamadaCriticidadDTO
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Descripcion = x.Descripcion
                    }).ToList();

                data.PuntosGeneralesCalificacion = listaPlana
                    .Where(x => x.TipoEntidad == "PUNTOGENERAL")
                    .GroupBy(x => x.Id)
                    .Select(g => g.First())
                    .Select(x => new EvaluacionLlamadaPuntoGeneralDTO
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Orden = x.Orden ?? 0,
                        Descripcion = x.Descripcion
                    }).ToList();
            }

            return data;
        }

        /// Autor: Lolo Arnold Zaa Fernandez
        /// Fecha: 25/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la versión vigente de configuración de calificación para un área específica,
        /// devolviendo los datos de la tabla T_EvaluacionLlamadaConfiguracionVersion con el JSON construido dinámicamente.
        /// El JSON se construye usando BuildConfiguracionLineamientosFromSp a partir del SP SP_EvaluacionLlamadaObtenerConfiguracionVigente.
        /// </summary>
        /// <param name="idPersonalAreaTrabajo">Identificador del área de trabajo del personal</param>
        /// <returns> ConfiguracionEsquemaCalificacionLlamdaDTO con los datos de la versión vigente y el JSON serializado </returns>
        public ConfiguracionEsquemaCalificacionLlamdaDTO ObtenerConfiguracionVigenteV3(int idPersonalAreaTrabajo)
        {
            try
            {
                // Obtener la versión vigente de la tabla
                var versionVigente = _unitOfWork.LineamientoCalificacionRepository
                                           .HistorialVersionCalificacionLlamadaV2(idPersonalAreaTrabajo)
                                           .FirstOrDefault(v => v.EsVigente);

                if (versionVigente == null)
                {
                    return null;
                }

                // Obtener la configuración desde el SP
                var configuracionVigente = _unitOfWork.LineamientoCalificacionRepository
                    .ObtenerConfiguracionVigentePorArea(idPersonalAreaTrabajo);

                if (configuracionVigente != null && configuracionVigente.Any())
                {
                    // Construir el objeto estructurado usando BuildConfiguracionLineamientosFromSp
                    var dataEstructurada = BuildConfiguracionLineamientosFromSp(configuracionVigente);

                    // Serializar el objeto a JSON
                    versionVigente.ConfiguracionJSON = JsonSerializer.Serialize(dataEstructurada);
                }
                else
                {
                    versionVigente.ConfiguracionJSON = "{}";
                }

                return versionVigente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionMasivaActiva()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionMasivaActiva();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionMasivaActiva()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionCalificacionMasivaActiva();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionCalificacionAuto()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionCalificacionAuto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ConfiguracionMasivaTranscripcionCalificacionDTO> ObtenerConfiguracionTranscripcionAuto()
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionTranscripcionAuto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamiento(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamiento(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionLineamientoHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamientoHistorico(IdOportunidad, IdLlamadaWebphoneCruceCentral3Cx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<HistoricoCalificacionDTO> ObtenerNotaCalificacionPuntoGeneralHistorico(int IdOportunidad, int IdLlamadaWebphoneCruceCentral3Cx)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionPuntoGeneralHistorico(IdOportunidad, IdLlamadaWebphoneCruceCentral3Cx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoGeneral(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamientoGeneral(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionAutomaticaLineamiento(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionAutomaticaLineamiento(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool GuardarCalificacionLlamada(CalificacionLlamadaManualDTO calificacionLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.GuardarCalificacionLlamada(calificacionLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Califica una llamada automáticamente.
        /// </summary>
        /// <param name="calificacionLlamada">Objeto de calificación automática.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public bool CalificarLlamadaAutomaticamente(CalificacionLlamadaAutomaticaDTO calificacionLlamada)
        {
            try
            {
                //var listaIdCriterio = calificacionLlamada.Calificaciones
                //    .Select(x => x.IdLineamientoCalificacion)
                //    .Where(id => id != null)
                //    .Distinct()
                //    .ToList();

                return _unitOfWork.LineamientoCalificacionRepository.CalificarLlamadaAutomaticamente(calificacionLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActualizarEstadoCalificacionLlamada(EstadoLlamadaCalificadaDTO estadoLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActualizarEstadoCalificacionLlamada(estadoLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool CongelarConfiguracion(CongelamientoConfiguracionDTO congelamientoConfiguracionDTO)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.CongelarConfiguracion(congelamientoConfiguracionDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracion(CongelamientoConfiguracionActivaDTO activarConfiguracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActivarConfiguracion(activarConfiguracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelAutomatico(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelAutomatico(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelAutomaticoCalificacion(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelAutomaticoCalificacion(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelCalificacionAuto(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelCalificacionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ConfigurarPanelTranscripcionAuto(ConfiguracionTranscripcionDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ConfigurarPanelTranscripcionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracionTranscripcionAuto(ConfiguracionActivoProcesoDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActivarConfiguracionTranscripcionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public bool ActivarConfiguracionCalificacionAuto(ConfiguracionActivoProcesoDTO configuracion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ActivarConfiguracionCalificacionAuto(configuracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public async Task<List<bool>> TranscripcionAuto(int tipoTranscripcion)
        {
            IEnumerable<LlamadaProcesoAutoDTO> items = null;
            switch (tipoTranscripcion)
            {
                case 1:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionTranscripcionAuto();
                    break;
                case 2:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionTranscripcionMasiva();
                    break;
                default:
                    return new List<bool>();
            }
            var resultados = new List<bool>();
            var itemsOrdenados = items
                .GroupBy(x => x.IdOportunidad)
                .SelectMany(g => g
                    .OrderBy(x => x.IdActividadDetalle)
                    .ThenBy(x => x.IdLlamadaWebphoneCruceCentralTresCx)
                )
                .ToList();

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/");

            // Configurar headers como en Postman
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            var semaphore = new SemaphoreSlim(6); // 6 llamadas concurrentes


            var tasks = itemsOrdenados.Select(async item =>
            {
                var historialReprogramaciones = _unitOfWork.LineamientoCalificacionRepository.ObtenerOcurrenciaRegistrada(item.IdOportunidad).Select(oc => new
                {
                    IdLlamada = oc.IdLlamada,
                    EstadoOcurrencia = oc.EstadoOcurrenciaAlterno,
                    ocurrencia = oc.OcurrenciaAlterno,
                    Fecha = oc.FechaReal
                })
                .ToList();


                var datos = _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionCambioFaseOportunidad(item.IdFaseOportunidad_Ant, item.IdFaseOportunidad);

                var transicionesAgrupadas = datos
                    .GroupBy(t => new {
                        t.IdTransicionFaseOportunidad,
                        t.IdFaseOrigen,
                        t.NombreFaseOrigen,
                        t.CodigoFaseOrigen,
                        t.IdFaseDestino,
                        t.NombreFaseDestino,
                        t.CodigoFaseDestino
                    })
                    .Select(g => new TransicionFase
                    {
                        IdTransicionFaseOportunidad = g.Key.IdTransicionFaseOportunidad,
                        FaseOrigen = new Fase
                        {
                            IdFaseOrigen = g.Key.IdFaseOrigen,
                            NombreFaseOrigen = g.Key.NombreFaseOrigen,
                            CodigoFaseOrigen = g.Key.CodigoFaseOrigen
                        },
                        FaseDestino = new Fase
                        {
                            IdFaseDestino = g.Key.IdFaseDestino,
                            NombreFaseDestino = g.Key.NombreFaseDestino,
                            CodigoFaseDestino = g.Key.CodigoFaseDestino
                        },
                        Criterios = g.GroupBy(c => new {
                            c.IdCriterio,
                            c.OrdenCriterio,
                            c.NombreCriterio
                        })
                        .Select(cg => new Criterio
                        {
                            IdCriterio = cg.Key.IdCriterio,
                            OrdenCriterio = cg.Key.OrdenCriterio,
                            NombreCriterio = cg.Key.NombreCriterio,
                            Lineamientos = cg.Select(l => new Lineamiento
                            {
                                IdLineamientoCalificacionFase = l.IdLineamientoCalificacionFase,
                                OrdenLineamiento = l.OrdenLineamiento,
                                NombreLineamientoCalificacionFase = l.NombreLineamientoCalificacionFase,
                                Criticidad = new Criticidad
                                {
                                    IdCriticidadCalificacion = l.IdCriticidadCalificacion,
                                    NombreCriticidad = l.NombreCriticidad
                                }
                            }).OrderBy(l => l.OrdenLineamiento).ToList()
                        }).OrderBy(c => c.OrdenCriterio).ToList()
                    })
                    .OrderBy(t => t.IdTransicionFaseOportunidad)
                    .ToList();



                var payload = new
                {
                    idLlamada = item.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                    idActividadDetalle = item.IdActividadDetalle.ToString(),
                    idPersonal = item.IdPersonal_Asignado,
                    username = "System-Auto",
                    contacto = "Generico",/*Lo recibe para enviar por signal cuando es manual el proceso*/
                    audio_url = item.UrlAudioProcesado,
                    locale = "es-ES",
                    ocurrencia = item.NombreOcurrencia,
                    historialReprogramaciones = historialReprogramaciones,
                    informacionFases = transicionesAgrupadas,
                    faseOrigen = item.IdFaseOportunidad_Ant,
                    faseDestino = item.IdFaseOportunidad
                };

                await semaphore.WaitAsync();
                try
                {
                    var response = await httpClient.PostAsJsonAsync("transcriptions/transcribe", payload);
                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex, $"Error al transcribir llamada {item.IdLlamadaWebphoneCruceCentralTresCx}");
                    return false;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            resultados = (await Task.WhenAll(tasks)).ToList();
            return resultados;
        }

        public async Task<List<bool>> TranscripcionAutoV2(int idPersonalAreaTrabajo)
        {
            IEnumerable<LlamadaProcesoAutoDTO> items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionTranscripcionAutoAtencionCliente(idPersonalAreaTrabajo);

            var resultados = new List<bool>();

            var itemsOrdenados = items
                .GroupBy(x => x.IdOportunidad)
                .SelectMany(g => g.OrderBy(x => x.IdActividadDetalle).ThenBy(x => x.IdLlamadaWebphoneCruceCentralTresCx))
                .ToList();

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/");
            //httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/");
            // Configurar headers como en Postman
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var semaphore = new SemaphoreSlim(6); // 6 llamadas concurrentes

            var tasks = itemsOrdenados.Select(async item =>
            {
                var historialReprogramaciones = _unitOfWork
                    .LineamientoCalificacionRepository.ObtenerOcurrenciaRegistradaV2(item.IdOportunidad, item.IdPersonalAreaTrabajo)
                    .Select(oc => new
                    {
                        IdLlamada = oc.IdLlamada,
                        EstadoOcurrencia = oc.EstadoOcurrencia,
                        ocurrencia = oc.Ocurrencia,
                        Fecha = oc.FechaReal,
                    })
                    .ToList();

                object payload;
                string endpoint;

                // Cuerpo para Ventas => con informacionFases
                if (idPersonalAreaTrabajo == 8)
                {
                    payload = PayloadVentas(item, historialReprogramaciones);
                    endpoint = "transcriptions/transcribe";
                }
                else
                {
                    // Cuerpo para Atencion al Cliente con endpoint específico
                    payload = PayloadAtencionCliente(item, historialReprogramaciones);
                    endpoint = "transcriptions_atc/transcribe_atc";
                }


                await semaphore.WaitAsync();
                try
                {
                    // Serializar payload para debugging
                    var payloadJson = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
                    Console.WriteLine($"[DEBUG] Payload enviado para llamada {item.IdLlamadaWebphoneCruceCentralTresCx}:");
                    Console.WriteLine(payloadJson);

                    var response = await httpClient.PostAsJsonAsync(
                        endpoint,
                        payload
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"[ERROR] Status Code: {response.StatusCode} ({(int)response.StatusCode})");
                        Console.WriteLine($"[ERROR] Detalle del error para llamada {item.IdLlamadaWebphoneCruceCentralTresCx}:");
                        Console.WriteLine(errorContent);
                        Console.WriteLine($"[ERROR] Headers de respuesta:");
                        foreach (var header in response.Headers)
                        {
                            Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                        }
                        return false;
                    }

                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EXCEPTION] Error al transcribir llamada {item.IdLlamadaWebphoneCruceCentralTresCx}: {ex.Message}");
                    Console.WriteLine($"[EXCEPTION] StackTrace: {ex.StackTrace}");
                    return false;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            resultados = (await Task.WhenAll(tasks)).ToList();
            return resultados;
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 02/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Transcribe automáticamente llamadas sin transcripción para Validación de Matrícula.
        /// Función duplicada de TranscripcionAutoV2 pero específica para llamadas de Ventas
        /// del proceso de Validación de Matrícula.
        /// </summary>
        /// <param name="llamadasSinTranscripcion">Lista de llamadas sin transcripción</param>
        /// <returns>Una lista de booleanos indicando el resultado de cada transcripción.</returns>
        public async Task<List<bool>> TranscripcionValidacionMatricula(List<LlamadaProcesoAutoDTO> llamadasSinTranscripcion)
        {
            var resultados = new List<bool>();

            if (llamadasSinTranscripcion == null || !llamadasSinTranscripcion.Any())
            {
                return resultados;
            }

            var itemsOrdenados = llamadasSinTranscripcion
                .GroupBy(x => x.IdOportunidad)
                .SelectMany(g => g.OrderBy(x => x.IdActividadDetalle).ThenBy(x => x.IdLlamadaWebphoneCruceCentralTresCx))
                .ToList();

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/");

            // Configurar headers
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var semaphore = new SemaphoreSlim(6); // 6 llamadas concurrentes

            var tasks = itemsOrdenados.Select(async item =>
            {
                var historialReprogramaciones = _unitOfWork
                    .LineamientoCalificacionRepository.ObtenerOcurrenciaRegistradaV2(item.IdOportunidad, item.IdPersonalAreaTrabajo)
                    .Select(oc => new
                    {
                        IdLlamada = oc.IdLlamada,
                        EstadoOcurrencia = oc.EstadoOcurrencia,
                        ocurrencia = oc.Ocurrencia,
                        Fecha = oc.FechaReal,
                    })
                    .ToList();

                // Solo para Ventas
                object payload = PayloadVentas(item, historialReprogramaciones);
                string endpoint = "transcriptions/transcribe";

                await semaphore.WaitAsync();
                try
                {
                    // Serializar payload para debugging
                    var payloadJson = JsonSerializer.Serialize(payload, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    });
                    Console.WriteLine($"[DEBUG] Payload enviado para llamada {item.IdLlamadaWebphoneCruceCentralTresCx}:");
                    Console.WriteLine(payloadJson);

                    var response = await httpClient.PostAsJsonAsync(
                        endpoint,
                        payload
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"[ERROR] Status Code: {response.StatusCode} ({(int)response.StatusCode})");
                        Console.WriteLine($"[ERROR] Detalle del error para llamada {item.IdLlamadaWebphoneCruceCentralTresCx}:");
                        Console.WriteLine(errorContent);
                        Console.WriteLine($"[ERROR] Headers de respuesta:");
                        foreach (var header in response.Headers)
                        {
                            Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value)}");
                        }
                        return false;
                    }

                    response.EnsureSuccessStatusCode();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[EXCEPTION] Error al transcribir llamada {item.IdLlamadaWebphoneCruceCentralTresCx}: {ex.Message}");
                    Console.WriteLine($"[EXCEPTION] StackTrace: {ex.StackTrace}");
                    return false;
                }
                finally
                {
                    semaphore.Release();
                }
            });

            resultados = (await Task.WhenAll(tasks)).ToList();
            return resultados;
        }

        private object PayloadVentas<T>(
        LlamadaProcesoAutoDTO item,
        List<T> historialReprogramaciones
        )
        {
            var datos = _unitOfWork.LineamientoCalificacionRepository
                .ObtenerConfiguracionCambioFaseOportunidad(
                    item.IdFaseOportunidad_Ant,
                    item.IdFaseOportunidad
                );

            var transicionesAgrupadas = datos
                .GroupBy(t => new
                {
                    t.IdTransicionFaseOportunidad,
                    t.IdFaseOrigen,
                    t.NombreFaseOrigen,
                    t.CodigoFaseOrigen,
                    t.IdFaseDestino,
                    t.NombreFaseDestino,
                    t.CodigoFaseDestino,
                })
                .Select(g => new TransicionFase
                {
                    IdTransicionFaseOportunidad = g.Key.IdTransicionFaseOportunidad,
                    FaseOrigen = new Fase
                    {
                        IdFaseOrigen = g.Key.IdFaseOrigen,
                        NombreFaseOrigen = g.Key.NombreFaseOrigen,
                        CodigoFaseOrigen = g.Key.CodigoFaseOrigen,
                    },
                    FaseDestino = new Fase
                    {
                        IdFaseDestino = g.Key.IdFaseDestino,
                        NombreFaseDestino = g.Key.NombreFaseDestino,
                        CodigoFaseDestino = g.Key.CodigoFaseDestino,
                    },
                    Criterios = g.GroupBy(c => new
                    {
                        c.IdCriterio,
                        c.OrdenCriterio,
                        c.NombreCriterio,
                    })
                        .Select(cg => new Criterio
                        {
                            IdCriterio = cg.Key.IdCriterio,
                            OrdenCriterio = cg.Key.OrdenCriterio,
                            NombreCriterio = cg.Key.NombreCriterio,
                            Lineamientos = cg
                                .Select(l => new Lineamiento
                                {
                                    IdLineamientoCalificacionFase = l.IdLineamientoCalificacionFase,
                                    OrdenLineamiento = l.OrdenLineamiento,
                                    NombreLineamientoCalificacionFase =
                                        l.NombreLineamientoCalificacionFase,
                                    Criticidad = new Criticidad
                                    {
                                        IdCriticidadCalificacion = l.IdCriticidadCalificacion,
                                        NombreCriticidad = l.NombreCriticidad,
                                    },
                                })
                                .OrderBy(l => l.OrdenLineamiento)
                                .ToList(),
                        })
                        .OrderBy(c => c.OrdenCriterio)
                        .ToList(),
                })
                .OrderBy(t => t.IdTransicionFaseOportunidad)
                .ToList();

            var payload = new
            {
                idLlamada = item.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                idActividadDetalle = item.IdActividadDetalle.ToString(),
                idPersonal = item.IdPersonal_Asignado,
                username = "System-Auto",
                contacto = "Generico",
                audio_url = item.UrlAudioProcesado,
                locale = "es-ES",
                ocurrencia = item.NombreOcurrencia,
                historialReprogramaciones = historialReprogramaciones,
                informacionFases = transicionesAgrupadas,
                faseOrigen = item.IdFaseOportunidad_Ant,
                faseDestino = item.IdFaseOportunidad,
            };

            return payload;
        }

        private object PayloadAtencionCliente<T>(
        LlamadaProcesoAutoDTO item,
        List<T> historialReprogramaciones
        )
        {
            var payload = new
            {
                idLlamada = item.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                idActividadDetalle = item.IdActividadDetalle.ToString(),
                idPersonal = item.IdPersonal_Asignado,
                username = "System-Auto",
                contacto = "Generico",
                audio_url = item.UrlAudioProcesado,
                locale = "es-ES",
                ocurrencia = item.NombreOcurrencia,
                historialReprogramaciones = historialReprogramaciones,
                faseOrigen = item.IdFaseOportunidad,
                faseDestino = item.IdFaseOportunidad_Ant,
            };
            return payload;
        }

        public async Task<List<TranscripcionManualDTO>> TranscripcionManual(TranscripcionManualDTO transcripcionManualDTO)
        {

            if (transcripcionManualDTO == null)
            {
                return new List<TranscripcionManualDTO>();
            }

            using var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/");

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            try
            {
                var payload = new
                {
                    idLlamada = transcripcionManualDTO.IdLlamada.ToString(),
                    idActividadDetalle = transcripcionManualDTO.IdActividadDetalle.ToString(),
                    idPersonal = transcripcionManualDTO.IdPersonal,
                    username = transcripcionManualDTO.Username,
                    contacto = transcripcionManualDTO.Contacto,
                    audio_url = transcripcionManualDTO.Audio_Url?.Trim(),
                    locale = transcripcionManualDTO.Locale,
                    ocurrencia = transcripcionManualDTO.Ocurrencia,
                    historialReprogramaciones = transcripcionManualDTO.HistorialReprogramaciones?.Select(h => new {
                        IdLlamada = h.IdLlamada,
                        EstadoOcurrencia = h.EstadoOcurrencia,
                        ocurrencia = h.Ocurrencia,
                        Fecha = h.Fecha
                    }).ToList(),
                    informacionFases = transcripcionManualDTO.InformacionFases?.Select(f => new {
                        IdTransicionFaseOportunidad = f.IdTransicionFaseOportunidad,
                        FaseOrigen = new
                        {
                            IdFaseOrigen = f.FaseOrigen.IdFaseOrigen,
                            NombreFaseOrigen = f.FaseOrigen.NombreFaseOrigen,
                            CodigoFaseOrigen = f.FaseOrigen.CodigoFaseOrigen
                        },
                        FaseDestino = new
                        {
                            IdFaseDestino = f.FaseDestino.IdFaseDestino,
                            NombreFaseDestino = f.FaseDestino.NombreFaseDestino,
                            CodigoFaseDestino = f.FaseDestino.CodigoFaseDestino
                        },
                        Criterios = f.Criterios?.Select(c => new {
                            IdCriterio = c.IdCriterio,
                            OrdenCriterio = c.OrdenCriterio,
                            NombreCriterio = c.NombreCriterio,
                            Lineamientos = c.Lineamientos?.Select(l => new {
                                IdLineamientoCalificacionFase = l.IdLineamientoCalificacionFase,
                                OrdenLineamiento = l.OrdenLineamiento,
                                NombreLineamientoCalificacionFase = l.NombreLineamientoCalificacionFase,
                                Criticidad = new
                                {
                                    IdCriticidadCalificacion = l.Criticidad.IdCriticidadCalificacion,
                                    NombreCriticidad = l.Criticidad.NombreCriticidad
                                }
                            }).OrderBy(l => l.OrdenLineamiento).ToList()
                        }).OrderBy(c => c.OrdenCriterio).ToList()
                    }).OrderBy(f => f.IdTransicionFaseOportunidad).ToList(),
                    faseOrigen = transcripcionManualDTO.FaseOrigen,
                    faseDestino = transcripcionManualDTO.FaseDestino
                };

                var response = await httpClient.PostAsJsonAsync("transcriptions/transcribe", payload);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error en API: {response.StatusCode} - {errorContent}");
                }
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta exitosa: {responseContent}");
                }
                return new List<TranscripcionManualDTO> { transcripcionManualDTO };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en TranscripcionManual: {ex.Message}");
                throw;
            }
        }


        public async Task<List<bool>> CalificacionAuto(int tipoCalificacion)
        {

            var serviceInformacionOportunidad = new OportunidadInformacionService(_unitOfWork);
            var serviceMotivacionesPrograma = new ProgramaGeneralMotivacionService(_unitOfWork);
            var serviceObjeciones = new ProgramaGeneralProblemaService(_unitOfWork);
            var serviceInformacionPrograma = new InformacionProgramaService(_unitOfWork);
            var servicePGeneral = new PGeneralService(_unitOfWork);
            IEnumerable<LlamadaProcesoAutoDTO> items = null;
            switch (tipoCalificacion)
            {
                case 1:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionAuto();
                    break;
                case 2:
                    items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionMasiva();
                    break;
                default:
                    return new List<bool>();
            }


            /*AQui obtengo la version vigente de lineamientos para calificar*/
            var lineamientoVigente = _unitOfWork.LineamientoCalificacionRepository
                .HistorialVersionCalificacionLlamada()
                .FirstOrDefault(x => x.EsVigente);


            if (lineamientoVigente == null || items == null || !items.Any())
                return new List<bool>();

            var lineamientoVigenteProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                lineamientoVigente.ConfiguracionJSON,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;
            var resultados = new List<bool>();
            var itemsAgrupadosPorOportunidad = items.GroupBy(x => x.IdOportunidad)
                                                    .Select(g => new
                                                    {
                                                        IdOportunidad = g.Key,
                                                        Llamadas = g.OrderBy(x => x.IdActividadDetalle).ThenBy(x => x.FechaLlamada).ToList()
                                                    })
                                                    .ToList();

            var lineamientos = BuildLineamientosFormateados(lineamientoVigenteProcesados);

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/")
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            var semaphore = new SemaphoreSlim(10);
            foreach (var oportunidad in itemsAgrupadosPorOportunidad)
            {

                var llamadasParaCalificar = ObtenerSiguienteLlamadaParaCalificar(oportunidad.IdOportunidad);

                if (!llamadasParaCalificar.Any())
                {
                    // No hay llamadas listas para calificar
                    continue;
                }

                // Procesar la llamada lista
                var tasksOportunidad = llamadasParaCalificar.Select(async item =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        var historicos = oportunidad.Llamadas
                            .Where(x => x.IdActividadDetalle < item.IdActividadDetalle)
                            .ToList();

                        bool historicoOk = historicos.All(h => h.EsLlamadaTranscrita == true && h.EsLlamadaCalificada == true);

                        bool actualOk = item.EsLlamadaTranscrita == true && item.EsLlamadaCalificada != true;

                        if (!historicoOk || !actualOk)
                            return false;

                        // Solo considerar el campo PuntoCritico de puntosCriticosAsesor
                        var puntosCriticosAsesor = _unitOfWork.LineamientoCalificacionRepository
                            .ObtenerPuntoCriticoDiario(item.IdPersonal_Asignado, DateTime.Now.Date.AddDays(-1))
                            .Select(x => x.PuntoCritico)
                            .ToList();

                        var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                                                        .ObtenerHistoricoLlamadaCompletoPorIdOportunidad(oportunidad.IdOportunidad);

                        var llamadasHistoricas = todasLasLlamadas
                             .Where(x => x.EsLlamadaTranscrita == true && (x.IdActividadDetalle < item.IdActividadDetalle || (x.IdActividadDetalle == item.IdActividadDetalle
                                                                     && x.FechaLlamada <= item.FechaLlamada)))
                             .OrderByDescending(x => x.IdActividadDetalle)
                             .ThenByDescending(x => x.FechaLlamada)
                             .ToList();

                        var llamadasHistoricasCalificadas = todasLasLlamadas
                            .Where(x => x.IdActividadDetalle <= item.IdActividadDetalle && x.EsLlamadaTranscrita == true && x.EsLlamadaCalificada == true)
                            .OrderByDescending(x => x.IdActividadDetalle)
                            .ThenByDescending(x => x.FechaLlamada)
                            .ToList();

                        var llamadaActual = llamadasHistoricas.First();
                        var llamadasHistoricasParaPayload = llamadasHistoricasCalificadas.ToList();

                        var transcripcionesParaPayload = new List<object>();
                        var transcripcionActual = await ObtenerTranscripcion(llamadaActual.IdLlamadaWebphoneCruceCentralTresCx);
                        if (transcripcionActual != null)
                        {
                            transcripcionesParaPayload.Add(transcripcionActual);
                        }

                        foreach (var llamadaHistorica in llamadasHistoricasParaPayload)
                        {
                            var transcripcionHistorica = await ObtenerTranscripcion(llamadaHistorica.IdLlamadaWebphoneCruceCentralTresCx);
                            if (transcripcionHistorica != null)
                            {
                                if (int.TryParse(transcripcionHistorica.IdLlamada, out int idLlamadaInt))
                                {
                                    var lineamientoVigentePorLLamada = _unitOfWork.LineamientoCalificacionRepository
                .HistorialVersionCalificacionPorLlamada(idLlamadaInt)
                .FirstOrDefault();
                                    var lineamientoVigentePorLLamadaProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                                        lineamientoVigentePorLLamada.ConfiguracionJSON,
                                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                    )!;
                                    var detallesEvaluacionPorLlamada = _unitOfWork.LineamientoCalificacionRepository
                                        .ObtenerDetallesEvaluacionPorLlamada(idLlamadaInt)
                                        .ToList();
                                    var detallesPuntoGeneralesPorLlamada = _unitOfWork.LineamientoCalificacionRepository
                                      .ObtenerDetallesEvaluacionPuntosGeneralesPorLlamada(idLlamadaInt)
                                      .ToList();

                                    var lineamientosEnriquecidos = BuildLineamientosEnriquecidos(
                                        lineamientoVigentePorLLamadaProcesados,
                                        detallesEvaluacionPorLlamada,
                                        detallesPuntoGeneralesPorLlamada);

                                    var payloadHistorico = new
                                    {
                                        idLlamada = transcripcionHistorica.IdLlamada,
                                        transcription = new
                                        {
                                            source = transcripcionHistorica.Transcription.Source,
                                            summary = transcripcionHistorica.Transcription.Summary
                                        },
                                        calificaciones = new { lineamientos = lineamientosEnriquecidos }
                                    };
                                    transcripcionesParaPayload.Add(payloadHistorico);
                                }
                                else
                                {
                                    var payloadHistorico = new
                                    {
                                        idLlamada = transcripcionHistorica.IdLlamada,
                                        transcription = new
                                        {
                                            source = transcripcionHistorica.Transcription.Source,
                                            summary = transcripcionHistorica.Transcription.Summary
                                        }
                                    };
                                    transcripcionesParaPayload.Add(payloadHistorico);
                                }
                            }
                        }
                        /*Se debe limpiar la data , actualmte se obtiene en html*/
                        /*Se obteine data  acerta de la informacion del programa*/
                        CargarInformacionProgramaAutomaticoRespuestaDTO InformacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomatico(item.IdCentroCosto, item.IdCodigoPais, 0, 0);
                        /*Se obteine data  acerta del apartado presentacion programa de la ficha de la agenda*/
                        CargarInformacionProgramaAutomaticoRespuestaDTO PresentacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomaticoSpeech(item.IdCentroCosto, item.IdCodigoPais, 0, 0);
                        /*Se obteine data  acerta de Motivaciones del Programa*/
                        IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> MotivacionPrograma = serviceMotivacionesPrograma.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(item.IdOportunidad);
                        /*Informacion de solicitudes de informacion previas a la actual*/
                        OportunidadInformacionDTO HistoricoSolicitudInformacion = serviceInformacionOportunidad.ObtenerOportunidadInformacion(item.IdAlumno, item.IdClasificacionPersona);
                        /*Se obteine data  acerta de Objetivo programa*/
                        IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> PublicoObjetivoPrograma = servicePGeneral.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(item.IdCentroCosto, item.IdOportunidad);
                        /*Se obteine data  acerta de lso problemas reportados para el cliente*/
                        IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObjecionesCliente = serviceObjeciones.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(item.IdOportunidad);

                        // Crear brochure
                        var brochure = new
                        {
                            InformacionPrograma = InformacionPrograma,
                            PresentacionPrograma = PresentacionPrograma,
                            MotivacionPrograma = MotivacionPrograma,
                            HistoricoSolicitudInformacion = HistoricoSolicitudInformacion,
                            PublicoObjetivoPrograma = PublicoObjetivoPrograma,
                            ObjecionesCliente = ObjecionesCliente
                        };
                        var payload = new
                        {
                            idPersonal = item.IdPersonal_Asignado,
                            contacto = "Generico",
                            userName = "System-auto",
                            idCodigoPais = item.IdCodigoPais.ToString(),
                            transcription = transcripcionesParaPayload,
                            lineamientos = lineamientos,
                            brochure,
                            faseOrigen = item.FaseOportunidad_Ant,
                            faseDestino = item.FaseOportunidad,
                            puntosCriticosAsesor = puntosCriticosAsesor
                        };


                        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                            payload,
                            new System.Text.Json.JsonSerializerOptions
                            {
                                WriteIndented = true,
                                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                                DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                            }
                        ));

                        //Prevalidacion de estado de calificacion
                        var llamadaActualizada = _unitOfWork.LineamientoCalificacionRepository
                            .ObtenerDatosConfiguracionCalificacionPorIdLlamada(item.IdLlamadaWebphoneCruceCentralTresCx);

                        if (llamadaActualizada?.EsLlamadaCalificada == true)
                        {
                            Console.WriteLine($"[SKIP] Llamada {item.IdLlamadaWebphoneCruceCentralTresCx} ya fue calificada por otro proceso");
                            return false;
                        }
                        // VALIDACIÓN: Verificar que la llamada actual esté en la primera posición
                        if (transcripcionesParaPayload.Any())
                        {
                            var primeraTranscripcion = transcripcionesParaPayload.First();
                            var idLlamadaPrimera = primeraTranscripcion.GetType().GetProperty("IdLlamada")?.GetValue(primeraTranscripcion)?.ToString();

                            if (idLlamadaPrimera != item.IdLlamadaWebphoneCruceCentralTresCx.ToString())
                            {
                                Console.WriteLine($"[SKIP] La llamada actual {item.IdLlamadaWebphoneCruceCentralTresCx} no está en la primera posición del array. Primera posición: {idLlamadaPrimera}");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[SKIP] No hay transcripciones disponibles para la llamada {item.IdLlamadaWebphoneCruceCentralTresCx}");
                            return false; // No procesar si no hay transcripciones
                        }

                        //var response = await httpClient.PostAsJsonAsync("grading/queue/batch", payload);
                        var response = await httpClient.PostAsJsonAsync("grading/queue/batch", payload);

                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();
                        var evaluacionData = JsonSerializer.Deserialize<ResultadoEvaluacion>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return true;
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError(ex, $"Error al calificar llamada {item.IdLlamada}");
                        return false;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                var resultadosOportunidad = await Task.WhenAll(tasksOportunidad);
                resultados.AddRange(resultadosOportunidad);
            }
            return resultados.ToList();
        }
        /// Autor: Jose Vega
        /// Autor Modificación: Lolo Zaa
        /// Fecha: 2025-11-17
        /// Fecha Modificacion: 2025-11-21
        /// Version: 2.0
        /// <summary>
        /// Procesa la calificación automática de llamadas.
        /// Refactorizado para aceptar 'idPersonalAreaTrabajo' y segmentar la
        /// lógica de construcción del 'brochure' por área (Ventas=8, Clientes=3).
        /// </summary>
        /// descripcion modificación
        ///   -se adapto la funcion para usar las tablas normalizadas.
        ///   -Se agrego nuevos Sps para el manejo de información
        /// <param name="tipoCalificacion">Tipo de calificación (1=Auto, 2=Masiva).</param>
        /// <param name="idPersonalAreaTrabajo">ID del área de trabajo (8=Ventas, 3=Clientes).</param>
        /// <returns>Una lista de booleanos indicando el resultado de cada calificación.</returns>
        public async Task<List<bool>> CalificacionAutoV2(int idPersonalAreaTrabajo)
        {
            var serviceInformacionPrograma = new InformacionProgramaService(_unitOfWork);
            var solicitudOperacionesService = new SolicitudOperacionesService(_unitOfWork);
            var alumnoService = new AlumnoService(_unitOfWork);

            IEnumerable<LlamadaProcesoAutoDTO> item = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosEvaluacionLLamadaCalificacionAuto(idPersonalAreaTrabajo);

            var configuracionVigente =
                _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionVigentePorArea(
                    idPersonalAreaTrabajo
                );

            if (
                configuracionVigente == null
                || !configuracionVigente.Any()
                || item == null
                || !item.Any()
            )
                return new List<bool>();

            var resultados = new List<bool>();
            var itemsAgrupadosPorOportunidad = item
                .Where(x => x.IdPersonal_Asignado == 6571)
                .GroupBy(x => x.IdOportunidad)
                .Select(g => new
                {
                    IdOportunidad = g.Key,
                    Llamadas = g.OrderBy(x => x.IdActividadDetalle)
                        .ThenBy(x => x.FechaLlamada)
                        .ToList(),
                })
                .ToList();

            var lineamientos = BuildLineamientosFormateadosFromSp(configuracionVigente);

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri(
                    "http://ia-analisis-llamadas-comercial-api.bsginstitute.com/"
                ),
                //URL PRUEBA LOCAL IA
                //BaseAddress = new Uri(
                //    "http://127.0.0.1:8000/"
                //),

            };
            //
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var semaphore = new SemaphoreSlim(10);

            foreach (var oportunidad in itemsAgrupadosPorOportunidad)
            {
                var llamadasParaCalificar = ObtenerSiguienteLlamadaParaCalificarV2(
                    oportunidad.IdOportunidad,
                    idPersonalAreaTrabajo
                );

                if (!llamadasParaCalificar.Any())
                {
                    continue;
                }

                var tasksOportunidad = llamadasParaCalificar.Select(async item =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        var historicos = oportunidad
                            .Llamadas.Where(x => x.IdActividadDetalle < item.IdActividadDetalle)
                            .ToList();

                        bool historicoOk = historicos.All(h =>
                            h.EsLlamadaTranscrita == true && h.EsLlamadaCalificada == true
                        );
                        bool actualOk =
                            item.EsLlamadaTranscrita == true && item.EsLlamadaCalificada != true;

                        if (!historicoOk || !actualOk)
                            return false;

                        var puntosCriticosAsesor = _unitOfWork
                            .LineamientoCalificacionRepository.ObtenerPuntoCriticoDiario(
                                item.IdPersonal_Asignado,
                                DateTime.Now.Date.AddDays(-1)
                            )
                            .Select(x => x.PuntoCritico)
                            .ToList();

                        var todasLasLlamadas =
                            _unitOfWork.LineamientoCalificacionRepository.ObtenerHistoricoLlamadaCompletoPorIdOportunidadV2(
                                oportunidad.IdOportunidad,
                                idPersonalAreaTrabajo
                            );

                        var llamadasHistoricas = todasLasLlamadas
                            .Where(x =>
                                x.EsLlamadaTranscrita == true
                                && (
                                    x.IdActividadDetalle < item.IdActividadDetalle
                                    || (
                                        x.IdActividadDetalle == item.IdActividadDetalle
                                        && x.FechaLlamada <= item.FechaLlamada
                                    )
                                )
                            )
                            .OrderByDescending(x => x.IdActividadDetalle)
                            .ThenByDescending(x => x.FechaLlamada)
                            .ToList();

                        var llamadasHistoricasCalificadas = todasLasLlamadas
                            .Where(x =>
                                x.IdActividadDetalle <= item.IdActividadDetalle
                                && x.EsLlamadaTranscrita == true
                                && x.EsLlamadaCalificada == true
                            )
                            .OrderByDescending(x => x.IdActividadDetalle)
                            .ThenByDescending(x => x.FechaLlamada)
                            .ToList();

                        var llamadaActual = llamadasHistoricas.First();
                        var llamadasHistoricasParaPayload = llamadasHistoricasCalificadas.ToList();

                        var transcripcionesParaPayload = new List<object>();
                        var transcripcionActual = await ObtenerDisplayTranscripcion(
                            llamadaActual.IdLlamadaWebphoneCruceCentralTresCx
                        );
                        if (transcripcionActual != null)
                        {
                            transcripcionesParaPayload.Add(transcripcionActual);
                        }
                        foreach (var llamadaHistorica in llamadasHistoricasParaPayload)
                        {
                            var transcripcionHistorica = await ObtenerDisplayTranscripcion(
                                llamadaHistorica.IdLlamadaWebphoneCruceCentralTresCx
                            );
                            if (transcripcionHistorica != null)
                            {
                                if (
                                    int.TryParse(
                                        transcripcionHistorica.IdLlamada,
                                        out int idLlamadaInt
                                    )
                                )
                                {
                                    var versionHistorica = _unitOfWork
                                        .LineamientoCalificacionRepository.HistorialVersionCalificacionPorLlamadav2(
                                            idLlamadaInt
                                        )
                                        .FirstOrDefault();

                                    if (versionHistorica != null)
                                    {
                                        var configuracionHistorica =
                                            _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionPorVersion(
                                                versionHistorica.IdVersionConfiguracionCalificacionLlamada,
                                                idPersonalAreaTrabajo
                                            );

                                        var detallesEvaluacionPorLlamada = _unitOfWork
                                            .LineamientoCalificacionRepository.ObtenerDetallesEvaluacionPorLlamada(
                                                idLlamadaInt
                                            )
                                            .ToList();

                                        var detallesPuntoGeneralesPorLlamada = _unitOfWork
                                            .LineamientoCalificacionRepository.ObtenerDetallesEvaluacionPuntosGeneralesPorLlamada(
                                                idLlamadaInt
                                            )
                                            .ToList();

                                        object lineamientosEnriquecidos =
                                            BuildLineamientosEnriquecidosFromSp(
                                                configuracionHistorica,
                                                detallesEvaluacionPorLlamada,
                                                detallesPuntoGeneralesPorLlamada
                                            );

                                        // Filtrar lineamientos históricos solo para Atención al Cliente
                                        if (idPersonalAreaTrabajo == 3)
                                        {
                                            var configuracionLineamentoHistorico = this.ObtenerConfiguracionLineamientoATC(item.EstadoMatricula, item.SubEstadoMatricula);
                                            lineamientosEnriquecidos = this.CalcularLineamientosATC(configuracionLineamentoHistorico, lineamientosEnriquecidos);
                                        }

                                        var payloadHistorico = new
                                        {
                                            idLlamada = transcripcionHistorica.IdLlamada,
                                            transcription = new
                                            {
                                                source = transcripcionHistorica
                                                    .Transcription
                                                    .Source,
                                                summary = transcripcionHistorica
                                                    .Transcription
                                                    .Summary,
                                            },
                                            calificaciones = new
                                            {
                                                lineamientos = lineamientosEnriquecidos,
                                            },
                                        };
                                        transcripcionesParaPayload.Add(payloadHistorico);
                                    }
                                    else
                                    {
                                        var payloadHistorico = new
                                        {
                                            idLlamada = transcripcionHistorica.IdLlamada,
                                            transcription = new
                                            {
                                                source = transcripcionHistorica
                                                    .Transcription
                                                    .Source,
                                                summary = transcripcionHistorica
                                                    .Transcription
                                                    .Summary,
                                            },
                                        };
                                        transcripcionesParaPayload.Add(payloadHistorico);
                                    }
                                }
                                else
                                {
                                    var payloadHistorico = new
                                    {
                                        idLlamada = transcripcionHistorica.IdLlamada,
                                        transcription = new
                                        {
                                            source = transcripcionHistorica.Transcription.Source,
                                            summary = transcripcionHistorica.Transcription.Summary,
                                        },
                                    };
                                    transcripcionesParaPayload.Add(payloadHistorico);
                                }
                            }
                        }

                        object brochure;
                        // Inicializar con lineamientos completos (sin filtrar)
                        object lineamientosParaPayload = lineamientos;

                        switch (idPersonalAreaTrabajo)
                        {
                            case 8: //Area Ventas - usa lineamientos completos sin filtrar
                                brochure = BuildBrochureVentas(item, serviceInformacionPrograma);
                                break;
                            case 3: //Area Clientes - filtra lineamientos según estado de matrícula
                                brochure = await BuildBrochureClientesAsync(item, serviceInformacionPrograma, solicitudOperacionesService, alumnoService);
                                var configuracionLineamento = this.ObtenerConfiguracionLineamientoATC(item.EstadoMatricula,item.SubEstadoMatricula);
                                lineamientosParaPayload = this.CalcularLineamientosATC(configuracionLineamento, lineamientos);
                                break;
                            default:
                                Console.WriteLine(
                                    $"[WARN] idPersonalAreaTrabajo '{idPersonalAreaTrabajo}' no reconocido. Brochure estará vacío."
                                );
                                brochure = new { };
                                break;
                        }

                        var payload = new
                        {
                            idPersonal = item.IdPersonal_Asignado,
                            contacto = "Generico",
                            userName = "System-auto",
                            idCodigoPais = item.IdCodigoPais.ToString(),
                            transcription = transcripcionesParaPayload,
                            lineamientos = lineamientosParaPayload,
                            brochure,
                            faseOrigen = item.FaseOportunidad_Ant,
                            faseDestino = item.FaseOportunidad,
                            puntosCriticosAsesor = puntosCriticosAsesor,
                            idPersonalAreaTrabajo = item.IdPersonalAreaTrabajo
                        };

                        Console.WriteLine(
                            System.Text.Json.JsonSerializer.Serialize(
                                payload,
                                new System.Text.Json.JsonSerializerOptions
                                {
                                    WriteIndented = true,
                                    PropertyNamingPolicy = System
                                        .Text
                                        .Json
                                        .JsonNamingPolicy
                                        .CamelCase,
                                    DictionaryKeyPolicy = System
                                        .Text
                                        .Json
                                        .JsonNamingPolicy
                                        .CamelCase,
                                }
                            )
                        );
                        var llamadaActualizada =
                           _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionPorIdLlamadaV2(
                               item.IdLlamadaWebphoneCruceCentralTresCx,
                               idPersonalAreaTrabajo
                           );

                        if (llamadaActualizada?.EsLlamadaCalificada == true)
                        {
                            Console.WriteLine(
                                $"[SKIP] Llamada {item.IdLlamadaWebphoneCruceCentralTresCx} ya fue calificada por otro proceso"
                            );
                            return false;
                        }

                        if (transcripcionesParaPayload.Any())
                        {
                            var primeraTranscripcion = transcripcionesParaPayload.First();
                            var idLlamadaPrimera = primeraTranscripcion
                                .GetType()
                                .GetProperty("IdLlamada")
                                ?.GetValue(primeraTranscripcion)
                                ?.ToString();

                            if (idLlamadaPrimera != item.IdLlamadaWebphoneCruceCentralTresCx.ToString())
                            {
                                Console.WriteLine(
                                    $"[SKIP] La llamada actual {item.IdLlamadaWebphoneCruceCentralTresCx} no está en la primera posición del array. Primera posición: {idLlamadaPrimera}"
                                );
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine(
                                $"[SKIP] No hay transcripciones disponibles para la llamada {item.IdLlamadaWebphoneCruceCentralTresCx}"
                            );
                            return false;
                        }
                        // Determinar endpoint según el área de trabajo
                        string endpoint;
                        switch (idPersonalAreaTrabajo)
                        {
                            case 3:
                                endpoint = "grading/queue/evaluate/atc/batch";
                                break;
                            case 8:
                                endpoint = "grading/queue/batch";
                                break;
                            default:
                                endpoint = "grading/queue/batch";
                                break;
                        }


                        var response = await httpClient.PostAsJsonAsync(endpoint, payload);
                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();
                        var evaluacionData = JsonSerializer.Deserialize<ResultadoEvaluacion>(
                            json,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                        );

                        return true;
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError(ex, $"Error al calificar llamada {item.IdLlamada}");
                        Console.WriteLine(
                            $"[ERROR] Error al calificar llamada {item.IdLlamadaWebphoneCruceCentralTresCx}: {ex.Message}"
                        );
                        return false;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                var resultadosOportunidad = await Task.WhenAll(tasksOportunidad);
                resultados.AddRange(resultadosOportunidad);
            }
            return resultados.ToList();
        }

        /// Tipo Función: GET
        /// Autor: Lolo Zaa
        /// Fecha: 30/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Procesa validación de matrícula para llamadas pendientes.
        /// Función duplicada de CalificacionAutoV2 pero usa SP_ValidacioMatriculaObtenerInformacionPendiente
        /// que está fijo para área de Ventas (IdPersonalAreaTrabajo = 8).
        /// Separa el procesamiento en dos tipos de payload:
        /// - Convenio de Voz: IdOcurrenciaAlterno = 404
        /// - Proceso de Venta: IdOcurrenciaAlterno != 404
        /// </summary>
        /// <returns>Una lista de booleanos indicando el resultado de cada validación.</returns>
        public async Task<List<bool>> ValidacionMatricula()
        {
            // Área de trabajo fija en Ventas (8) según el SP
            int idPersonalAreaTrabajo = 8;
            int idPersonalCalidad = 6;

            // Constante para identificar Convenio de Voz
            const int ID_OCURRENCIA_CONVENIO_VOZ = 404;

            IEnumerable<LlamadaProcesoAutoDTO> item = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosValidacionMatriculaPendiente();

            var configuracionVigente =
                _unitOfWork.LineamientoCalificacionRepository.ObtenerConfiguracionVigentePorArea(
                    idPersonalCalidad
                );

            if (
                configuracionVigente == null
                || !configuracionVigente.Any()
                || item == null
                || !item.Any()
            )
                return new List<bool>();

            var resultados = new List<bool>();
            var itemsAgrupadosPorOportunidad = item
                .GroupBy(x => x.IdOportunidad)
                .Select(g => new
                {
                    IdOportunidad = g.Key,
                    Llamadas = g.OrderBy(x => x.IdActividadDetalle)
                        .ThenBy(x => x.FechaLlamada)
                        .ToList(),
                })
                .ToList();

            var lineamientos = BuildLineamientosFormateadosFromSp(configuracionVigente);

            //using var httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri(
            //        "http://ia-analisis-llamadas-comercial-api.bsginstitute.com/"
            //    ),
            //    //URL PRUEBA LOCAL IA
            //    //BaseAddress = new Uri(
            //    //    "http://127.0.0.1:8000/"
            //    //),

            //};
            ////
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json")
            //);
            //httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            //httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            //httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var semaphore = new SemaphoreSlim(10);

            foreach (var oportunidad in itemsAgrupadosPorOportunidad)
            {
                // Validar que haya llamadas en la oportunidad
                if (oportunidad.Llamadas == null || !oportunidad.Llamadas.Any())
                {
                    continue;
                }

                var tasksOportunidad = oportunidad.Llamadas.Select(async item =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                    

                       
                        // Determinar el tipo de validación según IdOcurrenciaAlterno
                        bool esConvenioVoz = item.IdOcurrenciaAlterno == ID_OCURRENCIA_CONVENIO_VOZ;
                        string tipoValidacion = esConvenioVoz ? "Convenio de Voz" : "Proceso de Venta";

                        Console.WriteLine($"[INFO] Procesando llamada {item.IdLlamadaWebphoneCruceCentralTresCx} - Tipo: {tipoValidacion} (IdOcurrenciaAlterno: {item.IdOcurrenciaAlterno})");

                        var todasLasLlamadas =
                            _unitOfWork.LineamientoCalificacionRepository.ObtenerHistoricoLlamadaCompletoPorIdOportunidadV2(
                                oportunidad.IdOportunidad,
                                idPersonalAreaTrabajo
                            );

                        // Validar que todasLasLlamadas no sea null
                        if (todasLasLlamadas == null || !todasLasLlamadas.Any())
                        {
                            Console.WriteLine($"[SKIP] No hay llamadas disponibles para la oportunidad {oportunidad.IdOportunidad}");
                            return false;
                        }

                        // Separar llamadas por tipo de ocurrencia
                        // Convenio de Voz (404): Llamada actual para calificar
                        var llamadasConvenioVoz = todasLasLlamadas
                            .Where(x => x.IdOcurrenciaAlterno == ID_OCURRENCIA_CONVENIO_VOZ
                                && x.EsLlamadaTranscrita == true)
                            .OrderBy(x => x.IdActividadDetalle)
                            .ThenBy(x => x.FechaLlamada)
                            .ToList();

                        // Obtener la última llamada de Convenio de Voz con mayor duración y fecha más vigente
                        LlamadaProcesoAutoDTO ultimaLlamadaConvenioVoz = null;
                        ConvenioDeVozPlantillaDTO convenioDeVozPlantilla = null;
                        SpeechBienvenidaProcesadoDTO speechProcesado = null;

                        if (llamadasConvenioVoz.Any())
                        {
                            // Obtener la llamada con mayor duración y, en caso de empate, la más reciente
                            ultimaLlamadaConvenioVoz = llamadasConvenioVoz
                                .OrderByDescending(x => x.DuracionContestoCentral ?? 0)
                                .ThenByDescending(x => x.FechaLlamada ?? DateTime.MinValue)
                                .FirstOrDefault();

                            // Obtener la plantilla de Speech Bienvenida para esta llamada
                            if (ultimaLlamadaConvenioVoz != null)
                            {
                                try
                                {
                                    // Obtener plantilla sin procesar (para metadata)
                                    var agendaActividadService = new AgendaActividadService(_unitOfWork);
                                    convenioDeVozPlantilla = agendaActividadService.ObtenerPlantillaConvenioDeVoz(
                                        ultimaLlamadaConvenioVoz.IdFaseOportunidad,
                                        ultimaLlamadaConvenioVoz.IdActividadDetalle
                                    );

                                    Console.WriteLine($"[INFO] Plantilla obtenida para Convenio de Voz - IdPlantilla: {convenioDeVozPlantilla.IdPlantilla}, IdFase: {ultimaLlamadaConvenioVoz.IdFaseOportunidad}");

                                    // Procesar plantilla con reemplazo de etiquetas
                                    var speechBienvenidaService = new SpeechBienvenidaService(_unitOfWork);
                                    speechProcesado = speechBienvenidaService.ObtenerSpeechBienvenidaProcesado(
                                        ultimaLlamadaConvenioVoz.IdActividadDetalle
                                    );

                                    Console.WriteLine($"[INFO] Speech procesado con etiquetas reemplazadas - IdCodigoPais: {speechProcesado.IdCodigoPais}");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[WARNING] No se pudo obtener plantilla para Convenio de Voz: {ex.Message}");
                                    convenioDeVozPlantilla = null;
                                    speechProcesado = null;
                                }
                            }
                        }

                        // Proceso de Venta (!=404): Histórico de llamadas como contexto
                        var llamadasProcesoVenta = todasLasLlamadas
                            .Where(x => x.IdOcurrenciaAlterno != ID_OCURRENCIA_CONVENIO_VOZ
                                && x.EsLlamadaTranscrita == true
                                && x.EsLlamadaCalificada == true)
                            .OrderBy(x => x.IdActividadDetalle)
                            .ThenBy(x => x.FechaLlamada)
                            .ToList();

                        // Detectar llamadas sin transcripción y enviarlas a transcribir
                        var llamadasSinTranscripcion = todasLasLlamadas
                            .Where(x => x.EsLlamadaTranscrita != true)
                            .ToList();

                        if (llamadasSinTranscripcion.Any())
                        {
                            Console.WriteLine($"[INFO] Se encontraron {llamadasSinTranscripcion.Count} llamadas sin transcripción para la oportunidad {oportunidad.IdOportunidad}");
                            Console.WriteLine($"[INFO] Enviando a transcribir...");

                            var resultadosTranscripcion = await TranscripcionValidacionMatricula(llamadasSinTranscripcion);

                            var exitosas = resultadosTranscripcion.Count(x => x == true);
                            var fallidas = resultadosTranscripcion.Count(x => x == false);

                            Console.WriteLine($"[INFO] Transcripciones completadas: {exitosas} exitosas, {fallidas} fallidas");

                            // Si hay transcripciones exitosas, saltar esta iteración para que se procesen en la siguiente ejecución
                            if (exitosas > 0)
                            {
                                Console.WriteLine($"[INFO] Se procesarán en la siguiente ejecución una vez estén transcritas");
                                return false;
                            }
                        }

                        // Construir transcripciones para Convenio de Voz
                        var transcripcionesConvenioVoz = new List<object>();
                        foreach (var llamadaConvenio in llamadasConvenioVoz)
                        {
                            var transcripcionConvenio = await ObtenerDisplayTranscripcion(
                                llamadaConvenio.IdLlamadaWebphoneCruceCentralTresCx
                            );
                            if (transcripcionConvenio != null)
                            {
                                transcripcionesConvenioVoz.Add(transcripcionConvenio);
                            }
                        }

                        // Construir transcripciones para Proceso de Venta (histórico)
                        var transcripcionesProcesoVenta = new List<object>();
                        foreach (var llamadaHistorica in llamadasProcesoVenta)
                        {
                            var transcripcionHistorica = await ObtenerDisplayTranscripcion(
                                llamadaHistorica.IdLlamadaWebphoneCruceCentralTresCx
                            );
                            if (transcripcionHistorica != null)
                            {
                                transcripcionesProcesoVenta.Add(transcripcionHistorica);
                            }
                        }

                        object payload;

                        // TipoPersonal hardcodeado
                        string tipoPersonal = "Coordinador";

                        // Obtener información del programa y cronograma de pago desde servicios
                        object informacionPrograma = null;
                        object cronogramaPago = null;

                        try
                        {
                            // Llamada 1: Obtener información del programa
                            var servicioInformacionPrograma = new InformacionProgramaService(_unitOfWork);
                            var respuestaPrograma = await servicioInformacionPrograma.CargarInformacionProgramaTodoAsync(item.IdCentroCosto, item.IdCodigoPais.ToString());

                            if (respuestaPrograma.Informacion != null)
                            {
                                dynamic info = respuestaPrograma.Informacion;
                                informacionPrograma = new
                                {
                                    objetivos = info.Objetivos,
                                    estructuraCurricular = info.EstructuraCurricular,
                                    certificacion = info.Certificacion,
                                    duracionHorarios = info.DuracionHorarios,
                                    prerrequisitos = info.Prerrequisitos,
                                    beneficios = info.Beneficios
                                };
                            }
                            else
                            {
                                informacionPrograma = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            informacionPrograma = null;
                        }

                        try
                        {
                            // Llamada 2: Obtener cronograma de pago de la oportunidad
                            var servicioCronograma = new MontoPagoCronogramaService(_unitOfWork);
                            var resumenCronograma = servicioCronograma.ObtenerOportunidadCronogramaPago(item.IdOportunidad, tipoPersonal);

                            if (resumenCronograma?.Cronograma != null)
                            {
                                object detalleList = null;
                                if (resumenCronograma.Cronograma.Detalle != null)
                                {
                                    detalleList = resumenCronograma.Cronograma.Detalle.Select(x => new
                                    {
                                        numeroCuota = x.NumeroCuota,
                                        montoCuota = x.MontoCuotaDescuento,
                                        fechaPago = x.FechaPago,
                                        cuotaDescripcion = x.CuotaDescripcion
                                    }).ToList();
                                }

                                cronogramaPago = new
                                {
                                    precio = resumenCronograma.Cronograma.Precio,
                                    precioDescuento = resumenCronograma.Cronograma.PrecioDescuento,
                                    idTipoDescuento = resumenCronograma.Cronograma.IdTipoDescuento,
                                    nombrePlural = resumenCronograma.Cronograma.NombrePlural,
                                    estadoMatricula = resumenCronograma.EstadoMatricula,
                                    detalle = detalleList
                                };
                            }
                            else
                            {
                                cronogramaPago = null;
                            }
                        }
                        catch (Exception ex)
                        {
                            cronogramaPago = null;
                        }

                        // Filtrar lineamientos según tipo de validación
                        // Convenio de Voz - Fase 339
                        var configuracionConvenioVoz = this.ObtenerConfiguracionLineamientoValidacionMatricula(ID_OCURRENCIA_CONVENIO_VOZ);
                        var lineamientosConvenioVoz = this.CalcularLineamientosValidacionMatricula(configuracionConvenioVoz, lineamientos);

                        // Proceso de Venta - Fase 338
                        var configuracionProcesoVenta = this.ObtenerConfiguracionLineamientoValidacionMatricula(null); // null = cualquier valor != 404
                        var lineamientosProcesoVenta = this.CalcularLineamientosValidacionMatricula(configuracionProcesoVenta, lineamientos);

                        // Construir payload con dos cuerpos separados
                        payload = new
                        {
                            idOportunidad = item.IdOportunidad,
                            idPersonal = item.IdPersonal_Asignado,
                            contacto = "Generico",
                            userName = "System-auto",
                            idCodigoPais = item.IdCodigoPais.ToString(),
                            convenioVoz = new
                            {
                                transcriptions = transcripcionesConvenioVoz,
                                lineamientos = lineamientosConvenioVoz,
                                plantilla = convenioDeVozPlantilla != null ? new
                                {
                                    idPlantilla = convenioDeVozPlantilla.IdPlantilla,
                                    idPlantillaClaveValor = convenioDeVozPlantilla.IdPlantillaClaveValor,
                                    clave = convenioDeVozPlantilla.Clave,
                                    // ✅ Usar speech procesado con etiquetas reemplazadas
                                    valor = speechProcesado != null ? speechProcesado.SpeechBienvenida : convenioDeVozPlantilla.Valor,
                                    valorDespedida = speechProcesado?.SpeechDespedida,
                                    idAreaEtiqueta = convenioDeVozPlantilla.IdAreaEtiqueta,
                                    idFaseOportunidad = convenioDeVozPlantilla.IdFaseOportunidad,
                                    idActividadDetalle = convenioDeVozPlantilla.IdActividadDetalle,
                                    idCodigoPais = speechProcesado?.IdCodigoPais
                                } : null
                            },
                            procesoVenta = new
                            {
                                transcriptions = transcripcionesProcesoVenta,
                                lineamientos = lineamientosProcesoVenta
                            },
                            informacionPrograma = informacionPrograma,
                            cronogramaPago = cronogramaPago,
                            faseOrigen = item.FaseOportunidad_Ant,
                            faseDestino = item.FaseOportunidad,
                            idPersonalAreaTrabajo = item.IdPersonalAreaTrabajo,
                            convenioVozPlantilla = convenioDeVozPlantilla
                        };

                        Console.WriteLine(
                            System.Text.Json.JsonSerializer.Serialize(
                                payload,
                                new System.Text.Json.JsonSerializerOptions
                                {
                                    WriteIndented = true,
                                    PropertyNamingPolicy = System
                                        .Text
                                        .Json
                                        .JsonNamingPolicy
                                        .CamelCase,
                                    DictionaryKeyPolicy = System
                                        .Text
                                        .Json
                                        .JsonNamingPolicy
                                        .CamelCase,
                                }
                            )
                        );
                        var jsonPayload = System.Text.Json.JsonSerializer.Serialize(
                            payload,
                            new System.Text.Json.JsonSerializerOptions
                            {
                                WriteIndented = true,
                                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                                DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                            }
                        );
                        var json = JsonSerializer.Serialize(payload);

                        //var llamadaActualizada =
                        //   _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionPorIdLlamadaV2(
                        //       item.IdLlamadaWebphoneCruceCentralTresCx,
                        //       idPersonalAreaTrabajo
                        //   );

                        //if (llamadaActualizada?.EsLlamadaCalificada == true)
                        //{
                        //    Console.WriteLine(
                        //        $"[SKIP] Llamada {item.IdLlamadaWebphoneCruceCentralTresCx} ya fue calificada por otro proceso"
                        //    );
                        //    return false;
                        //}

                        //// Validar que haya al menos una transcripción de Convenio de Voz (llamada a calificar)
                        //if (!transcripcionesConvenioVoz.Any())
                        //{
                        //    Console.WriteLine(
                        //        $"[SKIP] No hay transcripciones de Convenio de Voz disponibles para la llamada {item.IdLlamadaWebphoneCruceCentralTresCx}"
                        //    );
                        //    return false;
                        //}

                        //// Log de información
                        //Console.WriteLine($"[INFO] Transcripciones Convenio de Voz: {transcripcionesConvenioVoz.Count} (Lineamientos Fase 339)");
                        //Console.WriteLine($"[INFO] Transcripciones Proceso de Venta: {transcripcionesProcesoVenta.Count} (Lineamientos Fase 338)");

                        //// Endpoint fijo para Ventas
                        //string endpoint = "grading/queue/batch";

                        //var response = await httpClient.PostAsJsonAsync(endpoint, payload);
                        //response.EnsureSuccessStatusCode();
                        //var json = await response.Content.ReadAsStringAsync();
                        //var evaluacionData = JsonSerializer.Deserialize<ResultadoEvaluacion>(
                        //    json,
                        //    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                        //);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"[ERROR] Error al validar matrícula para llamada {item.IdLlamadaWebphoneCruceCentralTresCx}: {ex.Message}"
                        );
                        return false;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                var resultadosOportunidad = await Task.WhenAll(tasksOportunidad);
                resultados.AddRange(resultadosOportunidad);
            }
            return resultados.ToList();
        }



        public object BuildLineamientosEnriquecidosFromSp(
            IEnumerable<ConfiguracionVigenteJerarquiaDTO> configuracion,
            List<EvaluacionLlamadaDetalleDTO> detallesEvaluacionPorLlamada,
            List<EvaluacionPuntoGeneralDetalleDTO> detallesPuntoGeneralesPorLlamada
        )
        {
            var fasesDict = new Dictionary<string, object>();

            if (configuracion == null || !configuracion.Any())
                return new
                {
                    fases = new Dictionary<string, object>(),
                    puntosgenerales = new List<object>(),
                };

            var fases = configuracion.Where(x => x.TipoEntidad == "FASE").OrderBy(x => x.Orden);
            var criterios = configuracion.Where(x => x.TipoEntidad == "CRITERIO");
            var lineamientos = configuracion.Where(x => x.TipoEntidad == "LINEAMIENTO");

            foreach (var fase in fases)
            {
                var criteriosDict = new Dictionary<string, object>();
                var criteriosDeFase = criterios
                    .Where(c => c.IdPadre == fase.Id)
                    .OrderBy(c => c.Orden);

                foreach (var criterio in criteriosDeFase)
                {
                    var evaluacionCriterio = detallesEvaluacionPorLlamada.FirstOrDefault(e =>
                        e.IdCriterioCalificacionLlamada == criterio.Id
                    );

                    var lineamientosDeCriterio = lineamientos
                        .Where(l => l.IdPadre == criterio.Id)
                        .OrderBy(l => l.Orden)
                        .Select(l => new
                        {
                            id = l.Id,
                            orden = l.Orden,
                            lineamiento = l.Nombre,
                            importancia = l.NombreCriticidad,
                        })
                        .ToList();

                    // Usar clave única: ID + Nombre para evitar colisiones
                    var criterioKey = $"{criterio.Id}_{criterio.Nombre}";
                    criteriosDict[criterioKey] = new
                    {
                        id = criterio.Id,
                        nombre = criterio.Nombre,
                        orden = criterio.Orden,
                        nota = evaluacionCriterio?.Nota,
                        comentario = evaluacionCriterio?.Comentario,
                        brecha = evaluacionCriterio?.Brecha,
                        lineamientos = lineamientosDeCriterio,
                    };
                }

                // Usar clave única: ID + Nombre para evitar colisiones
                var faseKey = $"{fase.Id}_{fase.Nombre}";
                fasesDict[faseKey] = new
                {
                    id = fase.Id,
                    nombre = fase.Nombre,
                    orden = fase.Orden,
                    criterios = criteriosDict,
                };
            }

            var puntosGenerales = configuracion
                .Where(x => x.TipoEntidad == "PUNTOGENERAL")
                .OrderBy(pg => pg.Orden)
                .Select(pg => new
                {
                    id = pg.Id,
                    orden = pg.Orden,
                    nombre = pg.Nombre,
                    descripcion = pg.Descripcion,
                    nota = detallesPuntoGeneralesPorLlamada
                        .FirstOrDefault(d => d.IdCalificacionPuntoGeneral == pg.Id)
                        ?.Nota,
                    comentario = detallesPuntoGeneralesPorLlamada
                        .FirstOrDefault(d => d.IdCalificacionPuntoGeneral == pg.Id)
                        ?.Comentario,
                    brecha = detallesPuntoGeneralesPorLlamada
                        .FirstOrDefault(d => d.IdCalificacionPuntoGeneral == pg.Id)
                        ?.Brecha,
                })
                .Cast<object>()
                .ToList();

            return new { fases = fasesDict, puntosgenerales = puntosGenerales };
        }

        public object BuildLineamientosFormateadosFromSp(
            IEnumerable<ConfiguracionVigenteJerarquiaDTO> configuracion
        )
        {
            var fasesDict = new Dictionary<string, object>();

            var fases = configuracion.Where(x => x.TipoEntidad == "FASE").OrderBy(x => x.Orden);
            var criterios = configuracion.Where(x => x.TipoEntidad == "CRITERIO");
            var lineamientos = configuracion.Where(x => x.TipoEntidad == "LINEAMIENTO");

            foreach (var fase in fases)
            {
                var criteriosDict = new Dictionary<string, object>();
                var criteriosDeFase = criterios
                    .Where(c => c.IdPadre == fase.Id)
                    .OrderBy(c => c.Orden);

                foreach (var criterio in criteriosDeFase)
                {
                    var lineamientosDeCriterio = lineamientos
                        .Where(l => l.IdPadre == criterio.Id)
                        .OrderBy(l => l.Orden)
                        .Select(l => new
                        {
                            id = l.Id,
                            orden = l.Orden,
                            lineamiento = l.Nombre,
                            importancia = l.NombreCriticidad,
                        })
                        .ToList();

                    // Usar clave única: ID + Nombre para evitar colisiones
                    var criterioKey = $"{criterio.Id}_{criterio.Nombre}";
                    criteriosDict[criterioKey] = new
                    {
                        id = criterio.Id,
                        nombre = criterio.Nombre,
                        orden = criterio.Orden,
                        lineamientos = lineamientosDeCriterio,
                    };
                }

                // Usar clave única: ID + Nombre para evitar colisiones
                var faseKey = $"{fase.Id}_{fase.Nombre}";
                fasesDict[faseKey] = new
                {
                    id = fase.Id,
                    nombre = fase.Nombre,
                    orden = fase.Orden,
                    criterios = criteriosDict,
                };
            }

            var puntosGenerales = configuracion
                .Where(x => x.TipoEntidad == "PUNTOGENERAL")
                .OrderBy(pg => pg.Orden)
                .Select(pg => new
                {
                    id = pg.Id,
                    orden = pg.Orden,
                    nombre = pg.Nombre,
                    descripcion = pg.Descripcion,
                })
                .Cast<object>()
                .ToList();

            return new { fases = fasesDict, puntosgenerales = puntosGenerales };
        }

        public ConfiguracionLineamientoDTO BuildConfiguracionLineamientosFromSp(
            IEnumerable<ConfiguracionVigenteJerarquiaDTO> configuracion
        )
        {
            var fases = configuracion
                .Where(x => x.TipoEntidad == "FASE")
                .OrderBy(x => x.Orden)
                .Select(f => new FaseDTO
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Orden = f.Orden ?? 0
                })
                .ToList();

            var criterios = configuracion
                .Where(x => x.TipoEntidad == "CRITERIO")
                .OrderBy(x => x.Orden)
                .Select(c => new CriterioDTO
                {
                    Id = c.Id,
                    IdFaseCalificacion = c.IdPadre ?? 0,
                    NombreCriterio = c.Nombre,
                    Orden = c.Orden ?? 0
                })
                .ToList();

            var lineamientos = configuracion
                .Where(x => x.TipoEntidad == "LINEAMIENTO")
                .OrderBy(x => x.Orden)
                .Select(l => new LineamientoDTO
                {
                    Id = l.Id,
                    IdCriterioCalificacionLlamada = l.IdPadre ?? 0,
                    IdCriticidadCalificacion = l.IdCriticidad ?? 0,
                    NombreLineamiento = l.Nombre,
                    Orden = l.Orden ?? 0
                })
                .ToList();

            var criticidades = configuracion
                .Where(x => x.TipoEntidad == "LINEAMIENTO" && x.IdCriticidad.HasValue)
                .Select(x => new { Id = x.IdCriticidad.Value, Nombre = x.NombreCriticidad })
                .Distinct()
                .Select(cr => new CriticidadDTO
                {
                    Id = cr.Id,
                    Nombre = cr.Nombre
                })
                .ToList();

            var puntosGenerales = configuracion
                .Where(x => x.TipoEntidad == "PUNTOGENERAL")
                .OrderBy(x => x.Orden)
                .Select(pg => new PuntoGeneralDTO
                {
                    Id = pg.Id,
                    Nombre = pg.Nombre,
                    Descripcion = pg.Descripcion ?? string.Empty,
                    Orden = pg.Orden ?? 0
                })
                .ToList();

            return new ConfiguracionLineamientoDTO
            {
                FasesCalificacion = fases,
                CriteriosCalificacion = criterios,
                LineamientosCalificacion = lineamientos,
                CriticidadCalificacion = criticidades,
                PuntosGeneralesCalificacion = puntosGenerales
            };
        }

        private object BuildBrochureVentas(LlamadaProcesoAutoDTO item, InformacionProgramaService serviceInformacionPrograma)
        {
            /** Instanciación de servicios específicos de Ventas */
            var serviceMotivacionesPrograma = new ProgramaGeneralMotivacionService(_unitOfWork);
            var serviceObjeciones = new ProgramaGeneralProblemaService(_unitOfWork);
            var serviceInformacionOportunidad = new OportunidadInformacionService(_unitOfWork);
            var servicePGeneral = new PGeneralService(_unitOfWork);

            /** Se obteine data acerta de la informacion del programa (Común) */
            CargarInformacionProgramaAutomaticoRespuestaDTO InformacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomatico(item.IdCentroCosto, item.IdCodigoPais, 0, 0);

            /** Se obteine data acerta del apartado presentacion programa de la ficha de la agenda (Común) */
            CargarInformacionProgramaAutomaticoRespuestaDTO PresentacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomaticoSpeech(item.IdCentroCosto, item.IdCodigoPais, 0, 0);

            /** Se obteine data acerta de Motivaciones del Programa (Específico Ventas) */
            IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> MotivacionPrograma = serviceMotivacionesPrograma.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(item.IdOportunidad);

            /** Informacion de solicitudes de informacion previas a la actual (Específico Ventas) */
            OportunidadInformacionDTO HistoricoSolicitudInformacion = serviceInformacionOportunidad.ObtenerOportunidadInformacion(item.IdAlumno, item.IdClasificacionPersona);

            /** Se obteine data acerta de Objetivo programa (Específico Ventas) */
            IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> PublicoObjetivoPrograma = servicePGeneral.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(item.IdCentroCosto, item.IdOportunidad);

            /** Se obteine data acerta de lso problemas reportados para el cliente (Específico Ventas) */
            IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObjecionesCliente = serviceObjeciones.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(item.IdOportunidad);

            /** Crear brochure de Ventas */
            var brochure = new
            {
                InformacionPrograma = InformacionPrograma,
                PresentacionPrograma = PresentacionPrograma,
                MotivacionPrograma = MotivacionPrograma,
                HistoricoSolicitudInformacion = HistoricoSolicitudInformacion,
                PublicoObjetivoPrograma = PublicoObjetivoPrograma,
                ObjecionesCliente = ObjecionesCliente
            };

            return brochure;
        }

        private async Task<object> BuildBrochureClientesAsync(LlamadaProcesoAutoDTO item, InformacionProgramaService serviceInformacionPrograma, SolicitudOperacionesService solicitudOperacionesService, AlumnoService alumnoService)
        {
            // Validar que item no sea null
            if (item == null)
            {
                return new { };
            }

            // Inicializar TODAS las variables con listas vacías por defecto
            object InformacionPrograma = new List<object>();
            object InformacionMatricula = new List<object>();
            object PresentacionPrograma = new List<object>();
            List<object> CronogramaFinanzas = new List<object>();
            List<object> InformacionBeneficioSolicitado = new List<object>();
            List<object> BeneficiosPorMatricula = new List<object>();
            List<object> OportunidadMontoComplementarios = new List<object>();
            object DatosCobranzaAlumno = new List<object>();
            List<object> operacionesPendientes = new List<object>();
            List<object> operacionesRealizadas = new List<object>();
            object AvanceAonline = new List<object>();
            object AvanceOnline = new List<object>();
            List<object> HistorialAsesoria = new List<object>();
            List<object> listadoNotas = new List<object>();
            List<object> ActividadesAgenda = new List<object>();

            // Validamos usando las propiedades REALES del DTO (PascalCase)
            if (item.IdMatriculaCabecera > 0)
            {
                try
                {
                    InformacionMatriculaAlumnoDTO InformacionMatriculaAlumno = _unitOfWork.LineamientoCalificacionRepository.ObtenerInformacionMatriculaAlumno(item.IdMatriculaCabecera.Value);
                    if (InformacionMatriculaAlumno != null)
                    {
                        InformacionMatricula = InformacionMatriculaAlumno;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener ActividadesAgenda: {ex.Message}");
                }
            }

            // Solo llamar si los parámetros son válidos
            if (item.IdCentroCosto > 0 && item.IdCodigoPais > 0 && serviceInformacionPrograma != null)
            {
                try
                {
                    var info = serviceInformacionPrograma.CargarInformacionProgramaAutomatico(item.IdCentroCosto, item.IdCodigoPais, 0, 0);
                    if (info != null)
                    {
                        InformacionPrograma = info;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener InformacionPrograma: {ex.Message}");
                }

                try
                {
                    var presentacion = serviceInformacionPrograma.CargarInformacionProgramaAutomaticoSpeech(item.IdCentroCosto, item.IdCodigoPais, 0, 0);
                    if (presentacion != null)
                    {
                        PresentacionPrograma = presentacion;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener PresentacionPrograma: {ex.Message}");
                }
            }

            // Solo llamar si IdOportunidad es válido
            if (item.IdOportunidad > 0 && solicitudOperacionesService != null)
            {
                try
                {
                    var ops = solicitudOperacionesService.ObtenerSolicitudOperaciones(item.IdOportunidad);
                    if (ops != null)
                    {
                        operacionesPendientes = ops.Cast<object>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener operaciones pendientes: {ex.Message}");
                }

                try
                {
                    var opsRealizadas = solicitudOperacionesService.ObtenerSolicitudOperacionesRealizadas(item.IdOportunidad);
                    if (opsRealizadas != null)
                    {
                        operacionesRealizadas = opsRealizadas.Cast<object>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener operaciones realizadas: {ex.Message}");
                }
            }

            // Solo llamar si IdMatriculaCabecera tiene valor válido
            if (item.IdMatriculaCabecera.HasValue && item.IdMatriculaCabecera.Value > 0 && alumnoService != null)
            {
                try
                {
                    var avanceA = alumnoService.obtenerDatosAvanceAonline(item.IdMatriculaCabecera.Value);
                    if (avanceA != null)
                    {
                        AvanceAonline = avanceA;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener AvanceAonline: {ex.Message}");
                }

                try
                {
                    var avanceO = alumnoService.obtenerDatosAvanceOnline(item.IdMatriculaCabecera.Value);
                    if (avanceO != null)
                    {
                        AvanceOnline = avanceO;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener AvanceOnline: {ex.Message}");
                }
            }

            // Solo llamar si IdMatriculaCabecera tiene valor válido para historial asesoria
            if (item.IdMatriculaCabecera.HasValue && item.IdMatriculaCabecera.Value > 0 && solicitudOperacionesService != null)
            {
                try
                {
                    var historial = solicitudOperacionesService.ObtenerHistorialAsesora(item.IdMatriculaCabecera.Value);
                    if (historial != null)
                    {
                        HistorialAsesoria = historial.Cast<object>().ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al obtener HistorialAsesoria: {ex.Message}");
                }
            }

            // Procesar información de matrícula solo si CodigoMatricula tiene valor
            if (!string.IsNullOrEmpty(item.CodigoMatricula))
            {
                try
                {
                    var cronogramaPagoService = new CronogramaPagoDetalleFinalService(_unitOfWork);
                    var matriculaCabeceraService = new MatriculaCabeceraService(_unitOfWork);

                    var matricula = matriculaCabeceraService.ObtenerPorCodigoMatricula(item.CodigoMatricula);

                    if (matricula != null && matricula.Id > 0)
                    {
                        // Obtener información de beneficios solicitados
                        try
                        {
                            var beneficiosSolicitados = matriculaCabeceraService.ObtenerBeneficiosSolicitadosPorMatricula(item.CodigoMatricula);
                            if (beneficiosSolicitados != null)
                            {
                                InformacionBeneficioSolicitado = new List<object> { beneficiosSolicitados };
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Error al obtener beneficios solicitados: {ex.Message}");
                        }

                        // Obtener beneficios por matrícula
                        try
                        {
                            var beneficios = matriculaCabeceraService.ObtenerBeneficiosPorMatricula(item.CodigoMatricula);
                            if (beneficios != null)
                            {
                                BeneficiosPorMatricula = new List<object> { beneficios };
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Error al obtener beneficios por matrícula: {ex.Message}");
                        }

                        // Obtener oportunidad montos complementarios solo si IdOportunidad es válido
                        if (item.IdOportunidad > 0)
                        {
                            try
                            {
                                // Obtener TipoPersonal del personal asignado
                                var personal = _unitOfWork.PersonalRepository.ObtenerPorId(item.IdPersonal_Asignado);
                                string tipoPersonal = personal?.TipoPersonal ?? "Coordinador";

                                var montoPagoCronogramaService = new MontoPagoCronogramaService(_unitOfWork);
                                var montosComplementarios = montoPagoCronogramaService.ObtenerOportunidadMontoComplementarios(
                                    item.IdOportunidad,
                                    tipoPersonal,
                                    matricula.Id
                                );
                                if (montosComplementarios != null)
                                {
                                    OportunidadMontoComplementarios = new List<object> { montosComplementarios };
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"[ERROR] Error al obtener montos complementarios: {ex.Message}");
                            }
                        }

                        // Obtener datos de cobranza del alumno
                        try
                        {
                            var datosCobranza = alumnoService.obtenerDatosCobranzaAlumno(matricula.Id);
                            if (datosCobranza != null)
                            {
                                DatosCobranzaAlumno = datosCobranza;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Error al obtener datos de cobranza: {ex.Message}");
                        }

                        // Obtener cronograma de finanzas
                        try
                        {
                            var versionAprobada = cronogramaPagoService.ObtenerCronograma(matricula.Id)?.FirstOrDefault();

                            if (versionAprobada?.Version != null)
                            {
                                var cronograma = cronogramaPagoService.ObtenerCronogramaFinanzas(
                                    versionAprobada.Version.Value,
                                    matricula.Id
                                );

                                if (cronograma != null)
                                {
                                    var moras = cronogramaPagoService.ObtenerMorasCalculadas(matricula.Id);
                                    moras?.ForEach(m =>
                                    {
                                        var cuota = cronograma.Find(c => c.Id == m.IdCuota);
                                        if (cuota != null)
                                        {
                                            cuota.MoraCalculada = m.MoraCalculada;
                                            cuota.Cuota = m.Cuota;
                                        }
                                    });

                                    CronogramaFinanzas = cronograma.Cast<object>().ToList();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[ERROR] Error al obtener cronograma de finanzas: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Error al procesar información de matrícula: {ex.Message}");
                }
            }

            // Obtener listado de notas desde API solo si TODOS los parámetros necesarios tienen valor
            if (item.IdMatriculaCabecera.HasValue && item.IdMatriculaCabecera.Value > 0 &&
                item.IdPespecifico.HasValue && item.IdPespecifico.Value > 0)
            {
                try
                {
                    int idMatriculaCabecera = item.IdMatriculaCabecera.Value;
                    int idPespecifico = item.IdPespecifico.Value;
                    int grupo = 1;

                    string urlApi = $"https://api-portalweb.bsginstitute.com/api/Nota/ListadoNotaProcesarSincronico?idMatriculaCabecera={idMatriculaCabecera}&idPespecifico={idPespecifico}&grupo={grupo}";

                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await client.GetAsync(urlApi);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();

                            var opciones = new System.Text.Json.JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            var resultado = System.Text.Json.JsonSerializer.Deserialize<List<object>>(jsonString, opciones);

                            if (resultado != null && resultado.Any())
                            {
                                listadoNotas = resultado;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[WARN] La API retornó status: {response.StatusCode} para la URL: {urlApi}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Falló la petición HTTP para obtener notas: {ex.Message}");
                }
            }

            // Retorno del objeto combinado - siempre con listas vacías si no hay datos
            var brochure = new
            {
                InformacionPrograma = InformacionPrograma,
                InformacionMatricula = InformacionMatricula,
                //PresentacionPrograma = PresentacionPrograma,
                CronogramaFinanzas = CronogramaFinanzas,
                //InformacionBeneficioSolicitado = InformacionBeneficioSolicitado,
                BeneficiosPorMatricula = BeneficiosPorMatricula,
                OportunidadMontoComplementarios = OportunidadMontoComplementarios,
                DatosCobranzaAlumno = DatosCobranzaAlumno,
                OperacionesPendientes = operacionesPendientes,
                OperacionesRealizadas = operacionesRealizadas,
                AvanceAonline = AvanceAonline,
                AvanceOnline = AvanceOnline,
                //HistorialAsesoria = HistorialAsesoria,
                ListadoNota = listadoNotas,
                //ActividadesAgenda = ActividadesAgenda
            };

            return brochure;
        }

        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Version: 1.0
        /// <summary>
        /// Cargar información de motivaciones
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>CargarInformacionMotivacionesRespuestaDTO</returns> 
        public async Task<CargarInformacionMotivacionesRespuestaDTO> CargarInformacionMotivacionesAsync(int idPGeneral)
        {
            try
            {
                if (idPGeneral == 0)
                    return new CargarInformacionMotivacionesRespuestaDTO { Error = "IdPGeneral no válido" };

                var tareaResumen = _unitOfWork.DocumentoAgendaRepository.ObtenerResumenProgramaPorIdPGeneralAsync(idPGeneral);
                var tareaMotivaciones = _unitOfWork.LineamientoCalificacionRepository.ObtenerMotivacionesPorIdPGeneralAsync(idPGeneral);

                await Task.WhenAll(tareaResumen, tareaMotivaciones);

                var resumen = await tareaResumen;
                var motivacionesRaw = await tareaMotivaciones;

                string tipo = resumen?.Any() == true && resumen.First().EsProgramaOCurso == "Curso" ? "Curso" : "Programa";

                var motivaciones = new List<MotivacionDTO>();

                if (motivacionesRaw?.Any() == true)
                {
                    foreach (var motivacionRaw in motivacionesRaw)
                    {
                        var contenidoProcesado = string.Empty;

                        if (!string.IsNullOrEmpty(motivacionRaw.ContenidoArgumento))
                        {
                            var texto = System.Net.WebUtility.HtmlDecode(motivacionRaw.ContenidoArgumento);

                            texto = System.Text.RegularExpressions.Regex.Replace(texto, @"<[^>]+>", "");
                            texto = System.Text.RegularExpressions.Regex.Replace(texto, @"\s+", " ");
                            contenidoProcesado = texto.Trim();
                        }
                        else
                        {
                            contenidoProcesado = string.Empty;
                        }

                        var motivacionExistente = motivaciones.FirstOrDefault(m => m.NombreMotivacion == motivacionRaw.NombreMotivacion);

                        if (motivacionExistente != null)
                        {
                            motivacionExistente.Argumentos.Add(contenidoProcesado);
                        }
                        else
                        {
                            motivaciones.Add(new MotivacionDTO
                            {
                                NombreMotivacion = motivacionRaw.NombreMotivacion,
                                Argumentos = new List<string> { contenidoProcesado }
                            });
                        }
                    }
                }

                return new CargarInformacionMotivacionesRespuestaDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = tipo,
                    Motivaciones = motivaciones,
                    Error = null
                };
            }
            catch (Exception ex)
            {
                return new CargarInformacionMotivacionesRespuestaDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "",
                    Motivaciones = new List<MotivacionDTO>(),
                    Error = $"Error al cargar motivaciones: {ex.Message}"
                };
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 14/10/2025
        /// Version: 1.0
        /// <summary>
        /// Cargar información de objeciones de clientes
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>CargarInformacionObjecionesClientesRespuestaDTO</returns> 
        public async Task<CargarInformacionObjecionesClientesRespuestaV2DTO> CargarInformacionObjecionesClientesAsync(int idPGeneral)
        {
            try
            {
                if (idPGeneral == 0)
                    return new CargarInformacionObjecionesClientesRespuestaV2DTO { Error = "IdPGeneral no válido" };

                var tareaResumen = _unitOfWork.DocumentoAgendaRepository.ObtenerResumenProgramaPorIdPGeneralAsync(idPGeneral);
                var tareaObjeciones = _unitOfWork.LineamientoCalificacionRepository.ObtenerObjecionesClientesPorIdPGeneralAsync(idPGeneral);

                await Task.WhenAll(tareaResumen, tareaObjeciones);

                var resumen = await tareaResumen;
                var objecionesRaw = await tareaObjeciones;

                string tipo = resumen?.Any() == true && resumen.First().EsProgramaOCurso == "Curso" ? "Curso" : "Programa";

                var objeciones = new List<ObjecionClienteDTO>();

                if (objecionesRaw?.Any() == true)
                {
                    var orderedObjecionesRaw = objecionesRaw.OrderBy(o => o.Id).ThenBy(o => o.IdDetalleSolucion).Distinct().ToList();
                    var groupedByObjecion = orderedObjecionesRaw.GroupBy(o => new { o.Id, o.NombreObjecion });

                    foreach (var group in groupedByObjecion)
                    {
                        var objecion = new ObjecionClienteDTO
                        {
                            Id = group.Key.Id,
                            NombreObjecion = group.Key.NombreObjecion,
                            DetallesYSoluciones = new List<DetalleSolucionPair>()
                        };

                        foreach (var objecionRaw in group)
                        {
                            var detalleProcesado = string.Empty;
                            var solucionProcesada = string.Empty;

                            if (!string.IsNullOrEmpty(objecionRaw.Detalle))
                            {
                                var textoDetalle = System.Net.WebUtility.HtmlDecode(objecionRaw.Detalle);
                                textoDetalle = System.Text.RegularExpressions.Regex.Replace(textoDetalle, @"<[^>]+>", "");
                                textoDetalle = System.Text.RegularExpressions.Regex.Replace(textoDetalle, @"\s+", " ");
                                detalleProcesado = textoDetalle.Trim();
                            }

                            if (!string.IsNullOrEmpty(objecionRaw.Solucion))
                            {
                                var textoSolucion = System.Net.WebUtility.HtmlDecode(objecionRaw.Solucion);
                                textoSolucion = System.Text.RegularExpressions.Regex.Replace(textoSolucion, @"<[^>]+>", "");
                                textoSolucion = System.Text.RegularExpressions.Regex.Replace(textoSolucion, @"\s+", " ");
                                solucionProcesada = textoSolucion.Trim();
                            }
                            objecion.DetallesYSoluciones.Add(new DetalleSolucionPair
                            {
                                Detalle = detalleProcesado,
                                Solucion = solucionProcesada
                            });
                        }

                        objeciones.Add(objecion);
                    }
                }

                return new CargarInformacionObjecionesClientesRespuestaV2DTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = tipo,
                    Objecciones = objeciones,
                    Error = null
                };
            }
            catch (Exception ex)
            {
                return new CargarInformacionObjecionesClientesRespuestaV2DTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "",
                    Objecciones = new List<ObjecionClienteDTO>(),
                    Error = $"Error al cargar objeciones de clientes: {ex.Message}"
                };
            }
        }

        public async Task<string?> GenerarCuerpoCalificacion(int idLlamada)
        {

            var serviceInformacionOportunidad = new OportunidadInformacionService(_unitOfWork);
            var serviceMotivacionesPrograma = new ProgramaGeneralMotivacionService(_unitOfWork);
            var serviceObjeciones = new ProgramaGeneralProblemaService(_unitOfWork);
            var serviceInformacionPrograma = new InformacionProgramaService(_unitOfWork);
            var servicePGeneral = new PGeneralService(_unitOfWork);
            LlamadaProcesoAutoDTO items = null;

            items = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosConfiguracionCalificacionPorIdLlamada(idLlamada);


            /*AQui obtengo la version vigente de lineamientos para calificar*/
            var lineamientoVigente = _unitOfWork.LineamientoCalificacionRepository
                .HistorialVersionCalificacionLlamada()
                .FirstOrDefault(x => x.EsVigente);


            if (lineamientoVigente == null || items == null)
                return null;

            var lineamientoVigenteProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                lineamientoVigente.ConfiguracionJSON,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;
            var resultados = new List<bool>();

            var lineamientos = BuildLineamientosFormateados(lineamientoVigenteProcesados);
            var llamadasParaCalificar = ObtenerSiguienteLlamadaParaCalificar(items.IdOportunidad);



            try
            {


                // Validar actual: solo transcrita, no calificada
                bool actualOk = items.EsLlamadaTranscrita == true && items.EsLlamadaCalificada != true;

                var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                                                .ObtenerHistoricoLlamadaCompletoPorIdOportunidad(items.IdOportunidad);

                var llamadasHistoricas = todasLasLlamadas
                     .Where(x => x.IdActividadDetalle <= items.IdActividadDetalle)
                     .OrderByDescending(x => x.IdActividadDetalle)
                     .ThenByDescending(x => x.IdLlamadaWebphoneCruceCentralTresCx)
                     .Select(x => x.IdLlamadaWebphoneCruceCentralTresCx)
                     .ToList();

                var transcripcionesHistoricas = new List<TranscripcionCompletaResponseDisplayDTO>();
                foreach (var idLlamadaHistorica in llamadasHistoricas)
                {
                    var transcripcionHistorica = await ObtenerDisplayTranscripcion(idLlamadaHistorica);
                    if (transcripcionHistorica != null)
                        transcripcionesHistoricas.Add(transcripcionHistorica);
                }

                /*Se debe limpiar la data , actualmte se obtiene en html*/
                /*Se obteine data  acerta de la informacion del programa*/
                CargarInformacionProgramaAutomaticoRespuestaDTO InformacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomatico(items.IdCentroCosto, items.IdCodigoPais, 0, 0);
                /*Se obteine data  acerta del apartado presentacion programa de la ficha de la agenda*/
                CargarInformacionProgramaAutomaticoRespuestaDTO PresentacionPrograma = serviceInformacionPrograma.CargarInformacionProgramaAutomaticoSpeech(items.IdCentroCosto, items.IdCodigoPais, 0, 0);
                /*Se obteine data  acerta de Motivaciones del Programa*/
                IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> MotivacionPrograma = serviceMotivacionesPrograma.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(items.IdOportunidad);
                /*Informacion de solicitudes de informacion previas a la actual*/
                OportunidadInformacionDTO HistoricoSolicitudInformacion = serviceInformacionOportunidad.ObtenerOportunidadInformacion(items.IdAlumno, items.IdClasificacionPersona);
                /*Se obteine data  acerta de Objetivo programa*/
                IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> PublicoObjetivoPrograma = servicePGeneral.ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(items.IdCentroCosto, items.IdOportunidad);
                /*Se obteine data  acerta de lso problemas reportados para el cliente*/
                IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObjecionesCliente = serviceObjeciones.ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(items.IdOportunidad);

                // Crear brochure
                var brochure = new
                {
                    InformacionPrograma = InformacionPrograma,
                    PresentacionPrograma = PresentacionPrograma,
                    MotivacionPrograma = MotivacionPrograma,
                    HistoricoSolicitudInformacion = HistoricoSolicitudInformacion,
                    PublicoObjetivoPrograma = PublicoObjetivoPrograma,
                    ObjecionesCliente = ObjecionesCliente
                };
                var payload = new
                {
                    idPersonal = items.IdPersonal_Asignado,
                    contacto = "Generico",
                    userName = "System-auto",
                    idCodigoPais = items.IdCodigoPais.ToString(),
                    transcription = transcripcionesHistoricas,
                    lineamientos,
                    brochure
                };


                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                    payload,
                    new System.Text.Json.JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
                    }
                ));

                var json = JsonSerializer.Serialize(
                                                    payload,
                                                    new JsonSerializerOptions
                                                    {
                                                        WriteIndented = true,
                                                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                                                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                                                    }
                                                );


                return json;
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, $"Error al calificar llamada {item.IdLlamada}");
                return ex.Message;
            }

        }
        /// Autor: Lolo Zaa.
        /// Fecha: 25/09/2025
        /// Version: 1.0
        /// <summary>
        /// Arma el cuerpo de calificacion donde historico tiene notas. 
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public async Task<string?> GenerarCuerpoCalificacionv2(int idLlamada)
        {
            var serviceInformacionOportunidad = new OportunidadInformacionService(_unitOfWork);
            var serviceMotivacionesPrograma = new ProgramaGeneralMotivacionService(_unitOfWork);
            var serviceObjeciones = new ProgramaGeneralProblemaService(_unitOfWork);
            var serviceInformacionPrograma = new InformacionProgramaService(_unitOfWork);
            var servicePGeneral = new PGeneralService(_unitOfWork);

            LlamadaProcesoAutoDTO items = _unitOfWork.LineamientoCalificacionRepository
                .ObtenerDatosConfiguracionCalificacionPorIdLlamada(idLlamada);
            /*Aqui obtengo la version vigente de lineamientos para calificar*/
            var lineamientoVigente = _unitOfWork.LineamientoCalificacionRepository
                .HistorialVersionCalificacionLlamada()
                .FirstOrDefault(x => x.EsVigente);

            if (lineamientoVigente == null || items == null)
                return null;

            var lineamientoVigenteProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                lineamientoVigente.ConfiguracionJSON,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            )!;

            try
            {
                bool actualOk = items.EsLlamadaTranscrita == true;
                if (!actualOk)
                    return null;

                var lineamientos = BuildLineamientosFormateados(lineamientoVigenteProcesados);

                var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                    .ObtenerHistoricoLlamadaCompletoPorIdOportunidad(items.IdOportunidad);

                var llamadasHistoricas = todasLasLlamadas
                    .Where(x => x.IdActividadDetalle <= items.IdActividadDetalle && x.EsLlamadaTranscrita == true)
                    .OrderByDescending(x => x.IdActividadDetalle)
                    .ThenByDescending(x => x.FechaLlamada)
                    .ToList();

                var llamadasHistoricasCalificadas = todasLasLlamadas
                    .Where(x => x.IdActividadDetalle <= items.IdActividadDetalle && x.EsLlamadaTranscrita == true && x.EsLlamadaCalificada == true)
                    .OrderByDescending(x => x.IdActividadDetalle)
                    .ThenByDescending(x => x.FechaLlamada)
                    .ToList();

                var llamadaActual = llamadasHistoricas.First();
                var transcripcionesParaPayload = new List<object>();


                var transcripcionActual = await ObtenerTranscripcion(llamadaActual.IdLlamadaWebphoneCruceCentralTresCx);
                if (transcripcionActual != null)
                {
                    transcripcionesParaPayload.Add(transcripcionActual);
                }

                List<Task<object?>> tareasTranscripciones = llamadasHistoricasCalificadas
                    .Select(llamadaHistorica => Task.Run<object?>(async () =>
                    {
                        var transcripcionHistorica = await ObtenerTranscripcion(llamadaHistorica.IdLlamadaWebphoneCruceCentralTresCx);
                        if (transcripcionHistorica != null)
                        {
                            if (int.TryParse(transcripcionHistorica.IdLlamada, out int idLlamadaInt))
                            {
                                var lineamientoVigentePorLlamada = _unitOfWork.LineamientoCalificacionRepository
                                    .HistorialVersionCalificacionPorLlamada(idLlamadaInt)
                                    .FirstOrDefault();

                                var lineamientoVigentePorLLamadaProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                                    lineamientoVigentePorLlamada.ConfiguracionJSON,
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                                )!;

                                var detallesEvaluacionPorLlamada = _unitOfWork.LineamientoCalificacionRepository
                                    .ObtenerDetallesEvaluacionPorLlamada(idLlamadaInt)
                                    .ToList();

                                var detallesPuntoGeneralesPorLlamada = _unitOfWork.LineamientoCalificacionRepository
                                    .ObtenerDetallesEvaluacionPuntosGeneralesPorLlamada(idLlamadaInt)
                                    .ToList();

                                var lineamientosEnriquecidos = BuildLineamientosEnriquecidos(
                                    lineamientoVigentePorLLamadaProcesados,
                                    detallesEvaluacionPorLlamada,
                                    detallesPuntoGeneralesPorLlamada);

                                return new
                                {
                                    idLlamada = transcripcionHistorica.IdLlamada,
                                    transcription = new
                                    {
                                        source = transcripcionHistorica.Transcription.Source,
                                        summary = transcripcionHistorica.Transcription.Summary
                                    },
                                    calificaciones = new { lineamientos = lineamientosEnriquecidos }
                                } as object;
                            }
                            else
                            {
                                return new
                                {
                                    idLlamada = transcripcionHistorica.IdLlamada,
                                    transcription = new
                                    {
                                        source = transcripcionHistorica.Transcription.Source,
                                        summary = transcripcionHistorica.Transcription.Summary
                                    }
                                } as object;
                            }
                        }
                        return null;
                    })).ToList();

                // 2. Esperar todas las tareas y filtrar los resultados no nulos
                var resultadosTranscripciones = await Task.WhenAll(tareasTranscripciones);
                transcripcionesParaPayload.AddRange(resultadosTranscripciones.Where(x => x != null));

                /*Se obtiene data acerca de la información del programa*/
                CargarInformacionProgramaAutomaticoRespuestaDTO InformacionPrograma = serviceInformacionPrograma
                    .CargarInformacionProgramaAutomatico(items.IdCentroCosto, items.IdCodigoPais, 0, 0);

                /*Se obtiene data acerca del apartado presentación programa de la ficha de la agenda*/
                CargarInformacionProgramaAutomaticoRespuestaDTO PresentacionPrograma = serviceInformacionPrograma
                    .CargarInformacionProgramaAutomaticoSpeech(items.IdCentroCosto, items.IdCodigoPais, 0, 0);

                /*Se obtiene data acerca de Motivaciones del Programa*/
                IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> MotivacionPrograma =
                    serviceMotivacionesPrograma.ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(items.IdOportunidad);

                /*Información de solicitudes de información previas a la actual*/
                OportunidadInformacionDTO HistoricoSolicitudInformacion = serviceInformacionOportunidad
                    .ObtenerOportunidadInformacion(items.IdAlumno, items.IdClasificacionPersona);

                /*Se obtiene data acerca de Objetivo programa*/
                IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> PublicoObjetivoPrograma = servicePGeneral
                    .ObtenerPublicoObjetivoProgramaParaAgendaNuevaV3(items.IdCentroCosto, items.IdOportunidad);

                /*Se obtiene data acerca de los problemas reportados para el cliente*/
                IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObjecionesCliente = serviceObjeciones
                    .ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(items.IdOportunidad);

                // Crear brochure
                var brochure = new
                {
                    InformacionPrograma = InformacionPrograma,
                    PresentacionPrograma = PresentacionPrograma,
                    MotivacionPrograma = MotivacionPrograma,
                    HistoricoSolicitudInformacion = HistoricoSolicitudInformacion,
                    PublicoObjetivoPrograma = PublicoObjetivoPrograma,
                    ObjecionesCliente = ObjecionesCliente
                };

                var payload = new
                {
                    idPersonal = items.IdPersonal_Asignado,
                    contacto = "Generico",
                    userName = "System-auto",
                    idCodigoPais = items.IdCodigoPais.ToString(),
                    transcription = transcripcionesParaPayload,
                    lineamientos = lineamientos,
                    brochure,
                    faseOrigen = items.FaseOportunidad_Ant,
                    faseDestino = items.FaseOportunidad


                };

                var json = JsonSerializer.Serialize(
                    payload,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
                    }
                );

                return json;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// Autor: Lolo Zaa.
        /// Fecha: 25/09/2025
        /// Version: 1.0
        /// <summary>
        /// Recibe el payload de calificación individual y lo envía al endpoint externo. 
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>

        public async Task<string> CalificacionIndividual(CalificacionIndividualRequestDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("El request no puede ser nulo");

            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/")
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.44.0");
            httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");

            try
            {
                var response = await httpClient.PostAsJsonAsync("grading/queue/batch", dto);

                var content = await response.Content.ReadAsStringAsync();

                return $"Status: {response.StatusCode}\nRespuesta: {content}";
            }
            catch (Exception ex)
            {
                return $"Excepción en CalificacionIndividual: {ex.Message}";
            }
        }

        private List<LlamadaProcesoAutoDTO> ObtenerSiguienteLlamadaParaCalificar(int idOportunidad)
        {
            // Obtener TODAS las llamadas de la oportunidad (transcritas y no transcritas)
            var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                .ObtenerHistoricoLlamadaCompletoPorIdOportunidad(idOportunidad);

            if (!todasLasLlamadas.Any())
                return new List<LlamadaProcesoAutoDTO>();

            // Ordenar por fechaLlamada
            var llamadasOrdenadas = todasLasLlamadas
                .OrderBy(x => x.IdActividadDetalle) // Primero por ID (más antiguo)
                .ThenBy(x => x.FechaLlamada).ToList();        // Luego por fecha (más antigua)

            // Buscar la primera llamada que esté transcrita pero no calificada
            var primeraLlamadaParaCalificar = llamadasOrdenadas
                .FirstOrDefault(x => x.EsLlamadaTranscrita == true && x.EsLlamadaCalificada != true);

            if (primeraLlamadaParaCalificar == null)
            {
                return new List<LlamadaProcesoAutoDTO>();
            }

            // Encontrar el índice de la primera llamada para calificar
            var indicePrimeraParaCalificar = llamadasOrdenadas.FindIndex(x => x.IdLlamadaWebphoneCruceCentralTresCx == primeraLlamadaParaCalificar.IdLlamadaWebphoneCruceCentralTresCx);

            // Verificar que todas las llamadas anteriores estén transcritas y calificadas
            for (int i = 0; i < indicePrimeraParaCalificar; i++)
            {
                var llamadaAnterior = llamadasOrdenadas[i];
                if (llamadaAnterior.EsLlamadaTranscrita != true || llamadaAnterior.EsLlamadaCalificada != true)
                {
                    return new List<LlamadaProcesoAutoDTO>();
                }
            }

            return new List<LlamadaProcesoAutoDTO> { primeraLlamadaParaCalificar };
        }
        private List<LlamadaProcesoAutoDTO> ObtenerSiguienteLlamadaParaCalificarV2(int idOportunidad, int idPersonalAreaTrabajo)
        {

            var todasLasLlamadas = _unitOfWork.LineamientoCalificacionRepository
                .ObtenerHistoricoLlamadaCompletoPorIdOportunidadV2(idOportunidad, idPersonalAreaTrabajo);

            if (!todasLasLlamadas.Any())
                return new List<LlamadaProcesoAutoDTO>();

            // Ordenar por fechaLlamada
            var llamadasOrdenadas = todasLasLlamadas
                .OrderBy(x => x.IdActividadDetalle) // Primero por ID (más antiguo)
                .ThenBy(x => x.FechaLlamada).ToList(); // Luego por fecha (más antigua)

            // Buscar la primera llamada que esté transcrita pero no calificada
            var primeraLlamadaParaCalificar = llamadasOrdenadas
                .FirstOrDefault(x => x.EsLlamadaTranscrita == true && x.EsLlamadaCalificada != true);

            if (primeraLlamadaParaCalificar == null)
            {
                return new List<LlamadaProcesoAutoDTO>();
            }

            // Encontrar el índice de la primera llamada para calificar
            var indicePrimeraParaCalificar = llamadasOrdenadas.FindIndex(x => x.IdLlamadaWebphoneCruceCentralTresCx == primeraLlamadaParaCalificar.IdLlamadaWebphoneCruceCentralTresCx);

            // Verificar que todas las llamadas anteriores estén transcritas y calificadas
            for (int i = 0; i < indicePrimeraParaCalificar; i++)
            {
                var llamadaAnterior = llamadasOrdenadas[i];
                if (llamadaAnterior.EsLlamadaTranscrita != true || llamadaAnterior.EsLlamadaCalificada != true)
                {
                    return new List<LlamadaProcesoAutoDTO>();
                }
            }

            return new List<LlamadaProcesoAutoDTO> { primeraLlamadaParaCalificar };
        }
        public object BuildLineamientosFormateados(ConfiguracionLineamientoDTO lineamientoVigenteProcesados)
        {
            var fasesDict = new Dictionary<string, object>();

            foreach (var fase in lineamientoVigenteProcesados.FasesCalificacion.OrderBy(f => f.Orden))
            {
                var criteriosDict = new Dictionary<string, object>();

                var criterios = lineamientoVigenteProcesados.CriteriosCalificacion
                    .Where(c => c.IdFaseCalificacion == fase.Id)
                    .OrderBy(c => c.Orden)
                    .ToList();

                foreach (var criterio in criterios)
                {
                    var lineamientos = lineamientoVigenteProcesados.LineamientosCalificacion
                        .Where(l => l.IdCriterioCalificacionLlamada == criterio.Id)
                        .OrderBy(l => l.Orden)
                        .Select(l => new
                        {
                            id = l.Id,
                            orden = l.Orden,
                            lineamiento = l.NombreLineamiento,
                            importancia = GetCriticidadNombre(l.IdCriticidadCalificacion, lineamientoVigenteProcesados)
                        })
                        .ToList();

                    criteriosDict[criterio.NombreCriterio] = new
                    {
                        id = criterio.Id,
                        orden = criterio.Orden,
                        lineamientos
                    };
                }

                fasesDict[fase.Nombre] = new
                {
                    id = fase.Id,
                    orden = fase.Orden,
                    criterios = criteriosDict
                };
            }

            var puntosGenerales = (lineamientoVigenteProcesados.PuntosGeneralesCalificacion != null
                ? lineamientoVigenteProcesados.PuntosGeneralesCalificacion
                    .OrderBy(pg => pg.Orden)
                    .Select(pg => new
                    {
                        id = pg.Id,
                        orden = pg.Orden,
                        nombre = pg.Nombre,
                        descripcion = pg.Descripcion
                    }).Cast<object>().ToList()
                : new List<object>());
            return new
            {
                fases = fasesDict,
                puntosgenerales = puntosGenerales
            };
        }
        public object BuildLineamientosEnriquecidos(
    ConfiguracionLineamientoDTO lineamientoVigentePorLLamadaProcesados,
    List<EvaluacionLlamadaDetalleDTO> detallesEvaluacionPorLlamada,
    List<EvaluacionPuntoGeneralDetalleDTO> detallesPuntoGeneralesPorLlamada)
        {
            var fasesDict = new Dictionary<string, object>();

            // Si no hay evaluaciones, retornar estructura VACÍA
            if (!detallesEvaluacionPorLlamada.Any())
                return new { fases = new Dictionary<string, object>(), puntosgenerales = new List<object>() };

            // Procesar por fases y criterios
            foreach (var fase in lineamientoVigentePorLLamadaProcesados.FasesCalificacion.OrderBy(f => f.Orden))
            {
                var criteriosDict = new Dictionary<string, object>();

                var criterios = lineamientoVigentePorLLamadaProcesados.CriteriosCalificacion
                    .Where(c => c.IdFaseCalificacion == fase.Id)
                    .OrderBy(c => c.Orden)
                    .ToList();

                foreach (var criterio in criterios)
                {
                    // Buscar evaluación para este criterio específico en los detalles ya filtrados
                    var evaluacionCriterio = detallesEvaluacionPorLlamada
                        .FirstOrDefault(e => e.IdCriterioCalificacionLlamada == criterio.Id);

                    var lineamientos = lineamientoVigentePorLLamadaProcesados.LineamientosCalificacion
                        .Where(l => l.IdCriterioCalificacionLlamada == criterio.Id)
                        .OrderBy(l => l.Orden)
                        .Select(l => new
                        {
                            id = l.Id,
                            orden = l.Orden,
                            lineamiento = l.NombreLineamiento,
                            importancia = GetCriticidadNombre(l.IdCriticidadCalificacion, lineamientoVigentePorLLamadaProcesados)
                        })
                        .ToList();

                    // ESTRUCTURA: Base + campos de evaluación enriquecidos
                    criteriosDict[criterio.NombreCriterio] = new
                    {
                        id = criterio.Id,
                        orden = criterio.Orden,
                        nota = evaluacionCriterio?.Nota,
                        comentario = evaluacionCriterio?.Comentario,
                        brecha = evaluacionCriterio?.Brecha,
                        lineamientos
                    };
                }

                fasesDict[fase.Nombre] = new
                {
                    id = fase.Id,
                    orden = fase.Orden,
                    criterios = criteriosDict
                };
            }

            var puntosGenerales = (lineamientoVigentePorLLamadaProcesados.PuntosGeneralesCalificacion != null
                ? lineamientoVigentePorLLamadaProcesados.PuntosGeneralesCalificacion
                    .OrderBy(pg => pg.Orden)
                    .Select(pg => new
                    {
                        id = pg.Id,
                        orden = pg.Orden,
                        nombre = pg.Nombre,
                        descripcion = pg.Descripcion,
                        nota = detallesPuntoGeneralesPorLlamada.FirstOrDefault(d => d.IdCalificacionPuntoGeneral == pg.Id)?.Nota,
                        comentario = detallesPuntoGeneralesPorLlamada.FirstOrDefault(d => d.IdCalificacionPuntoGeneral == pg.Id)?.Comentario,
                        brecha = detallesPuntoGeneralesPorLlamada.FirstOrDefault(d => d.IdCalificacionPuntoGeneral == pg.Id)?.Brecha
                    }).Cast<object>().ToList()
                : new List<object>());

            return new
            {
                fases = fasesDict,
                puntosgenerales = puntosGenerales
            };
        }

        private string GetCriticidadNombre(int criticidadId, ConfiguracionLineamientoDTO dto)
        {
            return dto.CriticidadCalificacion
                .FirstOrDefault(c => c.Id == criticidadId)?.Nombre ?? string.Empty;
        }


        public async Task<CalificacionLlamadaAutomaticaDTO> ProcesarYConstruirPayloadCalificacionAsync(ResultadoEvaluacion evaluacionData, int idLlamada, int idVersionConfiguracion, string usuario, ConfiguracionLineamientoDTO configuracionActiva)
        {
            var calificaciones = new List<DetalleCalificacionMualDTO>();
            var calificacionesPuntosGenerales = new List<DetalleCalificacionPuntoGeneralDTO>();
            var calificacionesPuntosCriticos = new List<DetallePuntosCriticosDTO>();
            var calificacionesFase = new List<DetalleCalificacionFaseDTO>();


            var evaluacion = evaluacionData?.GradedResult;

            if (evaluacion == null)
                return null;

            // Procesar fases y criterios
            foreach (var fase in evaluacion.Fases)
            {
                // Obtener el IdFase por el primer criterio de la fase
                int? idFase = null;
                if (fase.Criterios != null && fase.Criterios.Count > 0)
                {
                    var criterioId = fase.Criterios[0].Id;
                    idFase = configuracionActiva.CriteriosCalificacion
                        .Where(c => c.Id == criterioId)
                        .Select(c => c.IdFaseCalificacion)
                        .FirstOrDefault();
                }

                // Agregar calificación de fase si corresponde
                if (idFase.HasValue && (!string.IsNullOrWhiteSpace(fase.JustificacionGeneral) || !string.IsNullOrWhiteSpace(fase.BrechaGeneral)))
                {
                    calificacionesFase.Add(new DetalleCalificacionFaseDTO
                    {
                        IdFase = idFase.Value,
                        JustificacionGeneral = fase.JustificacionGeneral,
                        BrechaGeneral = fase.BrechaGeneral
                    });
                }


                foreach (var criterio in fase.Criterios)
                {
                    if (criterio.Lineamientos != null && criterio.Lineamientos.Any())
                    {
                        calificaciones.Add(new DetalleCalificacionMualDTO
                        {
                            IdCriterioCalificacionLlamada = criterio.Id,
                            Nota = criterio.Calificacion,
                            Comentario = criterio.Justificacion,
                            Brecha = criterio.Brecha
                        });
                    }
                }
            }

            // Procesar puntos generales
            if (evaluacion.Puntosgenerales != null)
            {
                foreach (var punto in evaluacion.Puntosgenerales)
                {
                    calificacionesPuntosGenerales.Add(new DetalleCalificacionPuntoGeneralDTO
                    {
                        idCalificacionPuntoGeneral = punto.Id,
                        Nota = punto.Calificacion,
                        Comentario = punto.Justificacion
                    });
                }
            }

            // Procesar puntos críticos
            if (evaluacion.Puntoscriticos != null)
            {
                foreach (var kvp in evaluacion.Puntoscriticos)
                {
                    calificacionesPuntosCriticos.Add(new DetallePuntosCriticosDTO
                    {
                        Criterio = kvp?.Nombre,
                        Nota = kvp?.Calificacion,
                        Comentario = kvp?.Feedback
                    });
                }
            }

            // Construcción final del payload
            return new CalificacionLlamadaAutomaticaDTO
            {
                IdLlamada = idLlamada,
                IdVersion = idVersionConfiguracion,
                Calificaciones = calificaciones,
                CalificacionesPuntosGenerales = calificacionesPuntosGenerales,
                CalificacionesPuntosCriticos = calificacionesPuntosCriticos,
                Usuario = usuario
            };
        }


        public async Task<CalificacionLlamadaAutomaticaDTO> ProcesarYConstruirPayloadCalificacionBatchAsync(GradedResult evaluacionData, int idLlamada, int idVersionConfiguracion, string usuario, ConfiguracionLineamientoDTO configuracionActiva)
        {
            var calificaciones = new List<DetalleCalificacionMualDTO>();
            var calificacionesPuntosGenerales = new List<DetalleCalificacionPuntoGeneralDTO>();
            var calificacionesPuntosCriticos = new List<DetallePuntosCriticosDTO>();
            var calificacionesFase = new List<DetalleCalificacionFaseDTO>();



            var evaluacion = evaluacionData;

            if (evaluacion == null)
                return null;

            // Procesar fases y criterios
            foreach (var fase in evaluacion.Fases)
            {
                // Obtener el IdFase por el primer criterio de la fase
                int? idFase = null;
                if (fase.Criterios != null && fase.Criterios.Count > 0)
                {
                    var criterioId = fase.Criterios[0].Id;
                    idFase = configuracionActiva.CriteriosCalificacion
                        .Where(c => c.Id == criterioId)
                        .Select(c => c.IdFaseCalificacion)
                        .FirstOrDefault();
                }

                // Agregar calificación de fase si corresponde
                if (idFase.HasValue && (!string.IsNullOrWhiteSpace(fase.JustificacionGeneral) || !string.IsNullOrWhiteSpace(fase.BrechaGeneral)))
                {
                    calificacionesFase.Add(new DetalleCalificacionFaseDTO
                    {
                        IdFase = idFase.Value,
                        JustificacionGeneral = fase.JustificacionGeneral,
                        BrechaGeneral = fase.BrechaGeneral,
                        PorcentajeAvance = fase.PorcentajeAvance,
                        ComentarioAvance = fase.ComentarioAvance
                    });
                }


                foreach (var criterio in fase.Criterios)
                {
                    if (criterio.Lineamientos != null && criterio.Lineamientos.Any())
                    {
                        calificaciones.Add(new DetalleCalificacionMualDTO
                        {
                            IdCriterioCalificacionLlamada = criterio.Id,
                            Nota = criterio.Calificacion,
                            Comentario = criterio.Justificacion,
                            Brecha = criterio.Brecha
                        });
                    }
                }
            }

            // Procesar puntos generales
            if (evaluacion.Puntosgenerales != null)
            {
                foreach (var punto in evaluacion.Puntosgenerales)
                {
                    calificacionesPuntosGenerales.Add(new DetalleCalificacionPuntoGeneralDTO
                    {
                        idCalificacionPuntoGeneral = punto.Id,
                        Nota = punto.Calificacion,
                        Comentario = punto.Justificacion
                    });
                }
            }

            // Procesar puntos críticos
            if (evaluacion.Puntoscriticos != null)
            {
                foreach (var kvp in evaluacion.Puntoscriticos)
                {
                    calificacionesPuntosCriticos.Add(new DetallePuntosCriticosDTO
                    {
                        Criterio = kvp.Nombre,
                        Nota = kvp.Calificacion,
                        Comentario = kvp.Feedback
                    });
                }
            }
            // Si todas las notas de criterios y puntos generales son -1 y existen todos los puntos generales,
            // actualizar el comentario de cada punto general con el valor de evaluacionData.IterrupcionLlamada
            bool todosCriteriosMenosUno = calificaciones.All(x => x.Nota == -1);
            bool todosPuntosGeneralesMenosUno = calificacionesPuntosGenerales.All(x => x.Nota == -1);
            bool tieneTodosPuntosGenerales = evaluacion.Puntosgenerales != null &&
                calificacionesPuntosGenerales.Count == evaluacion.Puntosgenerales.Count;


            if (todosCriteriosMenosUno && todosPuntosGeneralesMenosUno && tieneTodosPuntosGenerales)
            {
                foreach (var punto in calificacionesPuntosGenerales)
                {
                    punto.Comentario = evaluacionData.InterrupcionLlamada;
                }
                foreach (var criterio in calificaciones)
                {
                    criterio.Comentario = evaluacionData.InterrupcionLlamada;
                }
            }

            // Construcción final del payload
            return new CalificacionLlamadaAutomaticaDTO
            {
                IdLlamada = idLlamada,
                IdVersion = idVersionConfiguracion,
                Calificaciones = calificaciones,
                CalificacionesPuntosGenerales = calificacionesPuntosGenerales,
                CalificacionesPuntosCriticos = calificacionesPuntosCriticos,
                CalificacionesFase = calificacionesFase,
                InterrupcionLlamada = evaluacionData.InterrupcionLlamada,
                Usuario = usuario
            };
        }

        /// Autor:Joseph Llanque
        /// Fecha: 25/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<TranscripcionCompletaResponseDto> </returns>
        public async Task<TranscripcionCompletaResponseDTO> ObtenerTranscripcion(int idLlamada)
        {
            try
            {
                var data = (await _unitOfWork.TranscripcionLlamadaRepository.ObtenerTranscripcion(idLlamada)).ToList();
                if (!data.Any()) return null;

                var transcripcion = data.First();

                var dto = new TranscripcionCompletaResponseDTO
                {
                    IdLlamada = transcripcion.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                    IdActividadDetalle = null,
                    Status = "success",
                    Transcription = new TranscriptionDto
                    {
                        Source = transcripcion.Source,
                        Timestamp = transcripcion.Timestamp,
                        DurationInTicks = transcripcion.DurationInTicks ?? 0,
                        DurationMilliseconds = transcripcion.DurationMilliseconds ?? 0,
                        Duration = transcripcion.Duration,
                        Summary = transcripcion.Summary,
                        Ocurrencia_Consistente = transcripcion.OcurrenciaConsistente.HasValue && transcripcion.OcurrenciaConsistente.Value ? "si" : "no",

                        CombinedRecognizedPhrases = data
                            .Where(x => x.FraseCombinadaId != null)
                            .GroupBy(x => x.FraseCombinadaId)
                            .Select(g => new CombinedRecognizedPhraseDto
                            {
                                Channel = g.First().FC_Channel,
                                Lexical = g.First().FC_Lexical,
                                Itn = g.First().FC_ITN,
                                MaskedITN = g.First().FC_MaskedITN,
                                Display = g.First().FC_Display
                            }).ToList(),

                        RecognizedPhrases = data
                            .Where(x => x.FraseReconocidaId != null)
                            .GroupBy(x => x.FraseReconocidaId)
                            .Select(g => new RecognizedPhraseDto
                            {
                                RecognitionStatus = g.First().RecognitionStatus,
                                Channel = g.First().FR_Channel,
                                Speaker = g.First().Speaker,
                                Offset = g.First().Offset,
                                Duration = g.First().FR_Duration,
                                OffsetInTicks = g.First().OffsetInTicks,
                                DurationInTicks = g.First().DurationInTicksFraseReconocida,
                                DurationMilliseconds = g.First().DurationMillisecondsFraseReconocida,
                                OffsetMilliseconds = g.First().OffsetMilliseconds,
                                NBest = g
                                       .Where(x => x.DetalleFraseReconocidaId != null)
                                       .DistinctBy(x => x.DetalleFraseReconocidaId)
                                       .Select(x => new NBestDto
                                       {
                                           Confidence = x.Confidence,
                                           Lexical = x.DFR_Lexical,
                                           Itn = x.DFR_ITN,
                                           MaskedITN = x.DFR_MaskedITN,
                                           Display = x.DFR_Display,
                                           Sentiment = x.DFR_Sentiment
                                       }).ToList()
                            }).ToList(),

                        Recomendaciones = data
                            .Where(x => x.RecomendacionId != null)
                            .Select(x => x.Recomendacion.ToString())
                            .Distinct()
                            .ToList()
                    }
                };

                return dto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 25/12/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<TranscripcionCompletaResponseDto> </returns>
        public async Task<TranscripcionCompletaResponseDisplayDTO> ObtenerDisplayTranscripcion(int idLlamada)
        {
            try
            {
                var data = (await _unitOfWork.TranscripcionLlamadaRepository.ObtenerTranscripcion(idLlamada)).ToList();
                if (!data.Any()) return null;

                var transcripcion = data.First();

                var dto = new TranscripcionCompletaResponseDisplayDTO
                {
                    IdLlamada = transcripcion.IdLlamadaWebphoneCruceCentralTresCx.ToString(),
                    IdActividadDetalle = null,
                    Status = "success",
                    Transcription = new TranscriptionDisplayDto
                    {
                        Source = transcripcion.Source,
                        Timestamp = transcripcion.Timestamp,
                        DurationInTicks = transcripcion.DurationInTicks ?? 0,
                        DurationMilliseconds = transcripcion.DurationMilliseconds ?? 0,
                        Duration = transcripcion.Duration,
                        Summary = transcripcion.Summary,
                        Ocurrencia_Consistente = transcripcion.OcurrenciaConsistente.HasValue && transcripcion.OcurrenciaConsistente.Value ? "si" : "no",

                        CombinedRecognizedPhrases = data
                            .Where(x => x.FraseCombinadaId != null)
                            .GroupBy(x => x.FraseCombinadaId)
                            .Select(g => new CombinedRecognizedPhraseDisplayDto
                            {
                                Channel = g.First().FC_Channel,
                                Display = g.First().FC_Display
                            }).ToList(),

                        RecognizedPhrases = data
                            .Where(x => x.FraseReconocidaId != null)
                            .GroupBy(x => x.FraseReconocidaId)
                            .Select(g => new RecognizedPhraseDisplayDto
                            {
                                RecognitionStatus = g.First().RecognitionStatus,
                                Channel = g.First().FR_Channel,
                                Speaker = g.First().Speaker,
                                Offset = g.First().Offset,
                                Duration = g.First().FR_Duration,
                                OffsetInTicks = g.First().OffsetInTicks,
                                DurationInTicks = g.First().DurationInTicksFraseReconocida,
                                DurationMilliseconds = g.First().DurationMillisecondsFraseReconocida,
                                OffsetMilliseconds = g.First().OffsetMilliseconds,
                                NBest = g
                                       .Where(x => x.DetalleFraseReconocidaId != null)
                                       .DistinctBy(x => x.DetalleFraseReconocidaId)
                                       .Select(x => new NBestDisplayDto
                                       {
                                           Confidence = x.Confidence,
                                           Display = x.DFR_Display,
                                           Sentiment = x.DFR_Sentiment
                                       }).ToList()
                            }).ToList(),

                        Recomendaciones = data
                            .Where(x => x.RecomendacionId != null)
                            .Select(x => x.Recomendacion.ToString())
                            .Distinct()
                            .ToList()
                    }
                };

                return dto;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteCalificacionResponse ObtenerReporte(ReporteCalificacionRequest req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporte(req);

            var filasOrdenadas = filas
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();

            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio)
                        .ToList();

                    decimal? promedio = notasValidas.Count > 0 ? Math.Round(notasValidas.Average(), 2) : (decimal?)null;

                    var puntosCriticos = g
                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                        .Select(x => x.PuntoCritico!.Trim())
                        .Distinct()
                        .ToList();

                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        PuntosCriticos = puntosCriticos,
                        ComentarioLlamadaNoCalificada = null,
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        CambioFaseConsistente = first.CambioFaseConsistente,
                        ComentarioConsistenciaCambioFase = first.ComentarioConsistenciaCambioFase,
                        InterrupcionLlamada = first.InterrupcionLlamada
                    };
                })
                .ToList();

            return new ReporteCalificacionResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }


        public ReporteCalificacionResponse ObtenerReporteVentas(ReporteCalificacionRequestV2 req)
        {
            var (items, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporteVentas(req);

            var filasOrdenadas = items
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();

            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio)
                        .ToList();

                    decimal? promedio = notasValidas.Count > 0 ? Math.Round(notasValidas.Average(), 2) : (decimal?)null;

                    var puntosCriticos = g
                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                        .Select(x => x.PuntoCritico!.Trim())
                        .Distinct()
                        .ToList();

                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        PuntosCriticos = puntosCriticos,
                        ComentarioLlamadaNoCalificada = null,
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        CambioFaseConsistente = first.CambioFaseConsistente,
                        ComentarioConsistenciaCambioFase = first.ComentarioConsistenciaCambioFase,
                        InterrupcionLlamada = first.InterrupcionLlamada
                    };
                })
                .ToList();

            return new ReporteCalificacionResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }

        public ReporteCalificacionAtencionClienteResponse ObtenerReporteAtencionCliente(ReporteCalificacionRequestV2 req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporteAtencionCliente(req);

            var filasOrdenadas = filas
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();

            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio.HasValue && x.PuntajePromedio.Value >= 0)
                        .Select(x => x.PuntajePromedio.Value)
                        .ToList();

                    decimal? promedio = notasValidas.Count > 0 ? Math.Round(notasValidas.Average(), 2) : (decimal?)null;

                    var puntosCriticos = g
                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                        .Select(x => x.PuntoCritico!.Trim())
                        .Distinct()
                        .ToList();

                    return new LlamadaCalificadaAtencionClienteDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        CodigoMatricula=first.CodigoMatricula,
                        EstadoMatricula=first.EstadoMatricula,
                        SubEstadoMatricula=first.SubEstadoMatricula,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        // Ocurrencias (sin sufijo Alterno)
                        IdOcurrenciaPadre = first.IdOcurrenciaPadre,
                        IdOcurrenciaActividad = first.IdOcurrenciaActividad,
                        IdOcurrencia = first.IdOcurrencia,
                        OcurrenciaPadre = first.OcurrenciaPadre,
                        Ocurrencia = first.Ocurrencia,
                        EstadoOcurrencia = first.EstadoOcurrencia,
                        PuntosCriticos = puntosCriticos,
                        ComentarioLlamadaNoCalificada = null, // el SP no devuelve esto
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        CambioFaseConsistente = first.CambioFaseConsistente,
                        ComentarioConsistenciaCambioFase = first.ComentarioConsistenciaCambioFase,
                        InterrupcionLlamada = first.InterrupcionLlamada
                    };

                })
                .ToList();

            return new ReporteCalificacionAtencionClienteResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }

        public ReporteCalificacionResponse ObtenerReportePorArea(ReporteCalificacionAreaRequest req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReportePorArea(req);

            var filasOrdenadas = filas
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();

            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio)
                        .ToList();

                    decimal? promedio = notasValidas.Count > 0 ? Math.Round(notasValidas.Average(), 2) : (decimal?)null;

                    var puntosCriticos = g
                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                        .Select(x => x.PuntoCritico!.Trim())
                        .Distinct()
                        .ToList();

                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        PuntosCriticos = puntosCriticos,
                        ComentarioLlamadaNoCalificada = null,
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        CambioFaseConsistente = first.CambioFaseConsistente,
                        ComentarioConsistenciaCambioFase = first.ComentarioConsistenciaCambioFase,
                        InterrupcionLlamada = first.InterrupcionLlamada
                    };
                })
                .ToList();

            return new ReporteCalificacionResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }

        public ReporteCalificacionResponse ObtenerReporteFase(ReporteCalificacionRequest req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporteFase(req);
            var filasOrdenadas = filas
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();
            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g
                        .Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio);

                    decimal? promedio = notasValidas.Any()
                        ? Math.Round(notasValidas.Average(), 0, MidpointRounding.AwayFromZero)/*redondeo matematico solicitado 'or gerencia*/
                        : (decimal?)null;

                    var puntosCriticos = g
                                        .Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                                        .Select(x => x.PuntoCritico!.Trim())
                                        .Distinct()
                                        .ToList();

                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        IdVersionConfiguracionCalificacionLlamada = first.IdVersionConfiguracionCalificacionLlamada,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        InterrupcionLlamada = first.InterrupcionLlamada
                    };
                })
                .ToList();

            return new ReporteCalificacionResponse
            {
                TotalRegistros = total,
                Data = agrupado
            };
        }

        public ReporteCalificacionResponse ObtenerReporteFaseVentas(ReporteCalificacionRequestV2 req)
        {
            var (filas, total) = _unitOfWork.LineamientoCalificacionRepository.ObtenerReporteFaseVentas(
                req
            );
            var filasOrdenadas = filas
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();
            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var first = g.First();

                    var notasValidas = g.Where(x => x.PuntajePromedio >= 0)
                        .Select(x => x.PuntajePromedio);

                    decimal? promedio = notasValidas.Any()
                        ? Math.Round(notasValidas.Average(), 0, MidpointRounding.AwayFromZero) /*redondeo matematico solicitado 'or gerencia*/
                        : (decimal?)null;

                    var puntosCriticos = g.Where(x => !string.IsNullOrWhiteSpace(x.PuntoCritico))
                        .Select(x => x.PuntoCritico!.Trim())
                        .Distinct()
                        .ToList();

                    return new LlamadaCalificadaDTO
                    {
                        IdLlamada = g.Key,
                        IdOportunidad = first.IdOportunidad,
                        FechaInicioLlamadaCentral = first.FechaInicioLlamadaCentral,
                        DuracionContestoCentral = first.DuracionContestoCentral,
                        IdAlumno = first.IdAlumno,
                        NombreCliente = first.NombreCliente,
                        IdAsesor = first.IdAsesor,
                        NombreAsesor = first.NombreAsesor,
                        IdCentroCosto = first.IdCentroCosto,
                        NombreCentroCosto = first.NombreCentroCosto,
                        NombreFaseI = first.NombreFaseI,
                        CodigoFaseI = first.CodigoFaseI,
                        NombreFaseD = first.NombreFaseD,
                        CodigoFaseD = first.CodigoFaseD,
                        Promedio = promedio,
                        IdOcurrenciaPadreAlterno = first.IdOcurrenciaPadreAlterno,
                        IdOcurrenciaActividadAlterno = first.IdOcurrenciaActividadAlterno,
                        IdOcurrenciaAlterno = first.IdOcurrenciaAlterno,
                        IdVersionConfiguracionCalificacionLlamada =
                            first.IdVersionConfiguracionCalificacionLlamada,
                        OcurrenciaPadreAlterno = first.OcurrenciaPadreAlterno,
                        OcurrenciaAlterno = first.OcurrenciaAlterno,
                        EstadoOcurrenciaAlterno = first.EstadoOcurrenciaAlterno,
                        OcurrenciaConsistente = first.OcurrenciaConsistente,
                        ComentarioConsistenciaOcurrencia = first.ComentarioConsistenciaOcurrencia,
                        InterrupcionLlamada = first.InterrupcionLlamada,
                    };
                })
                .ToList();

            return new ReporteCalificacionResponse { TotalRegistros = total, Data = agrupado };
        }


        public ReporteCalificacionGlobalResponse ObtenerPromedioGlobal(ReporteCalificacionGlobalRequest request)
        {
            var filas = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosParaPromedioGlobal(request);
            var filasOrdenadas = filas
    .OrderByDescending(f => f.FechaInicioLlamadaCentral)
    .ThenBy(f => f.IdLlamada)
    .ToList();
            // Agrupa todas las llamadas
            var agrupadoTotal = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .ToList();
            var totalLlamadas = agrupadoTotal.Count;
            // Agrupa todas las llamadas (sin filtrar criterios)
            var agrupado = filasOrdenadas
               .GroupBy(f => f.IdLlamada)
               .Select(g =>
               {
                   var notasValidas = g
                       .Where(x => x.PuntajePromedio >= 0)
                       .Select(x => x.PuntajePromedio);

                   decimal? promedio = notasValidas.Any()
                       ? Math.Round(notasValidas.Average(), 0, MidpointRounding.AwayFromZero)
                       : (decimal?)null;

                   return new
                   {
                       IdLlamada = g.Key,
                       Promedio = promedio,
                       TotalCalificaciones = notasValidas.Count()
                   };
               })
               .Where(x => x.Promedio.HasValue)
               .ToList();

            var totalCalificaciones = agrupado.Sum(x => x.TotalCalificaciones);

            // Calcular promedio global excluyendo calificacion
            var promediosValidos = filasOrdenadas
                .Where(x => x.PuntajePromedio >= 0 && x.IdCriterioCalificacion != 36 && x.IdCriterioCalificacion != 37)
                .Select(x => x.PuntajePromedio);

            var promedioGlobal = promediosValidos.Any()
                ? Math.Round(promediosValidos.Average(), 2)
                : 0;

            return new ReporteCalificacionGlobalResponse
            {
                TotalLlamadas = totalLlamadas,
                PromedioGlobal = promedioGlobal,
                TotalCalificaciones = totalCalificaciones,
                FechaCalculo = DateTime.Now
            };
        }
        public ReporteCalificacionGlobalResponse ObtenerPromedioGlobalVentas(ReporteCalificacionGlobalRequestV2 request)
        {
            var filas = _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosParaPromedioGlobalVentas(request);
            var filasOrdenadas = filas
    .OrderByDescending(f => f.FechaInicioLlamadaCentral)
    .ThenBy(f => f.IdLlamada)
    .ToList();
            // Agrupa todas las llamadas
            var agrupadoTotal = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .ToList();
            var totalLlamadas = agrupadoTotal.Count;
            // Agrupa todas las llamadas (sin filtrar criterios)
            var agrupado = filasOrdenadas
               .GroupBy(f => f.IdLlamada)
               .Select(g =>
               {
                   var notasValidas = g
                       .Where(x => x.PuntajePromedio >= 0)
                       .Select(x => x.PuntajePromedio);

                   decimal? promedio = notasValidas.Any()
                       ? Math.Round(notasValidas.Average(), 0, MidpointRounding.AwayFromZero)
                       : (decimal?)null;

                   return new
                   {
                       IdLlamada = g.Key,
                       Promedio = promedio,
                       TotalCalificaciones = notasValidas.Count()
                   };
               })
               .Where(x => x.Promedio.HasValue)
               .ToList();

            var totalCalificaciones = agrupado.Sum(x => x.TotalCalificaciones);

            // Calcular promedio global excluyendo calificacion
            var promediosValidos = filasOrdenadas
                .Where(x => x.PuntajePromedio >= 0 && x.IdCriterioCalificacion != 36 && x.IdCriterioCalificacion != 37 && x.IdCriterioCalificacion != null)
                .Select(x => x.PuntajePromedio);

            var promedioGlobal = promediosValidos.Any()
                ? Math.Round(promediosValidos.Average(), 2)
                : 0;

            return new ReporteCalificacionGlobalResponse
            {
                TotalLlamadas = totalLlamadas,
                PromedioGlobal = promedioGlobal,
                TotalCalificaciones = totalCalificaciones,
                FechaCalculo = DateTime.Now
            };
        }

        public ReporteCalificacionGlobalResponse ObtenerPromedioGlobalAtencionCliente(
            ReporteCalificacionGlobalRequestV2 request
        )
        {
            var filas =
                _unitOfWork.LineamientoCalificacionRepository.ObtenerDatosParaPromedioGlobalAtencionCliente(
                    request
                );

            var filasOrdenadas = filas
                .OrderByDescending(f => f.FechaInicioLlamadaCentral)
                .ThenBy(f => f.IdLlamada)
                .ToList();

            // Agrupa todas las llamadas
            var agrupadoTotal = filasOrdenadas.GroupBy(f => f.IdLlamada).ToList();

            var totalLlamadas = agrupadoTotal.Count;

            // Agrupa todas las llamadas (sin filtrar criterios)
            var agrupado = filasOrdenadas
                .GroupBy(f => f.IdLlamada)
                .Select(g =>
                {
                    var notasValidas = g
                        .Where(x => x.PuntajePromedio.HasValue && x.PuntajePromedio.Value >= 0)
                        .Select(x => x.PuntajePromedio.Value)
                        .ToList();

                    decimal? promedio = notasValidas.Any()
                        ? Math.Round(notasValidas.Average(), 0, MidpointRounding.AwayFromZero)
                        : (decimal?)null;

                    return new
                    {
                        IdLlamada = g.Key,
                        Promedio = promedio,
                        TotalCalificaciones = notasValidas.Count(),
                    };
                })
                .Where(x => x.Promedio.HasValue)
                .ToList();

            var totalCalificaciones = agrupado.Sum(x => x.TotalCalificaciones);

            // Calcular promedio global SIN EXCLUIR ningún criterio (ATC incluye todos)
            var promediosValidos = filasOrdenadas
                        .Where(x => x.PuntajePromedio.HasValue && x.PuntajePromedio.Value >= 0)
                        .Select(x => x.PuntajePromedio.Value);

            var promedioGlobal = promediosValidos.Any()
                ? Math.Round(promediosValidos.Average(), 2)
                : 0;

            return new ReporteCalificacionGlobalResponse
            {
                TotalLlamadas = totalLlamadas,
                PromedioGlobal = promedioGlobal,
                TotalCalificaciones = totalCalificaciones,
                FechaCalculo = DateTime.Now,
            };
        }

        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<CalificacionFaseDTO> ObtenerCalificacionFase(int idLlamada, bool tipoCalificacion)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerCalificacionFase(idLlamada, tipoCalificacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public async Task ProcesarCalificacionBatch(ResultadoEvaluacionBatch payload)
        {

            if (payload == null)
                throw new ArgumentNullException(nameof(payload), "El cuerpo de calificación es nulo.");

            var lineamientoVigente =
                                      ObtenerConfiguracionVigenteV3(
                                          payload.IdPersonalAreaTrabajo
                                      );
            if (lineamientoVigente == null)
                throw new InvalidOperationException("No existe una versión vigente de lineamientos para calificar.");
            if (string.IsNullOrWhiteSpace(lineamientoVigente.ConfiguracionJSON))
                throw new InvalidOperationException("La configuración de lineamientos vigente está vacía o nula.");

            ConfiguracionLineamientoDTO lineamientoVigenteProcesados;
            try
            {
                lineamientoVigenteProcesados = JsonSerializer.Deserialize<ConfiguracionLineamientoDTO>(
                    lineamientoVigente.ConfiguracionJSON,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? throw new InvalidOperationException("No se pudo deserializar la configuración de lineamientos vigente.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al deserializar la configuración de lineamientos vigente.", ex);
            }
            var payloadCalificacion = await ProcesarYConstruirPayloadCalificacionBatchAsync(
                payload.GradedResult,
                payload.IdLlamada,
                lineamientoVigente.Id,
                "system-auto",
                lineamientoVigenteProcesados
            );

            if (payloadCalificacion == null)
                throw new InvalidOperationException("No se pudo construir el payload de calificación automática.");

            var resultado = _unitOfWork.LineamientoCalificacionRepository.CalificarLlamadaAutomaticamente(payloadCalificacion);
            if (!resultado)
                throw new InvalidOperationException("No se pudo guardar la calificación automática en la base de datos.");

        }




        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public IEnumerable<InformacionLlamadaTresCxDTO> ObtenerInformacionLlamada(int idLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerInformacionLlamada(idLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Obtiene calificaciones por fase para una llamada específica
        /// </summary>
        /// <param name="idLlamada">ID de la llamada</param>
        /// <param name="tipoCalificacion">Tipo de calificación (0=Manual, 1=Automática)</param>
        /// <returns>Lista de calificaciones por fase</returns>
        public async Task<InsertRecomendacionResultDTO> ProcesarRecomendacionesBatch(RecomendacionLlamadaDTO payload)
        {

            try
            {
                return await _unitOfWork.LineamientoCalificacionRepository.ProcesarRecomendacionesBatch(payload);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public async Task<bool> ProcesarPuntoCriticoDiario()
        {
            var informacionDelDia = _unitOfWork.LineamientoCalificacionRepository.ObtenerPuntosCriticosPorDia();
            if (informacionDelDia == null || !informacionDelDia.Any())
                return true;

            var asesores = informacionDelDia.GroupBy(f => f.IdPersonal);

            // 3) Configurar HttpClient
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://ia-analisis-llamadas-comercial-api.bsginstitute.com/")
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BSG/PCDiario");
            httpClient.DefaultRequestHeaders.ConnectionClose = false;

            var procesadosOk = 0;
            var totalAsesores = asesores.Count();

            foreach (var asesorGroup in asesores)
            {
                var idPersonal = asesorGroup.Key;

                // 4) Agrupar por llamada del asesor
                var llamadas = asesorGroup
                    .GroupBy(x => x.IdLlamadaWebphoneCruceCentralTresCx)
                    .Select(g =>
                    {
                        var resumen = g.Select(x => x.ResumenLlamada)
                                       .FirstOrDefault(s => !string.IsNullOrWhiteSpace(s));

                        var puntosCriticos = g.Select(x => x.PuntoCritico?.Trim())
                                              .Where(s => !string.IsNullOrWhiteSpace(s))
                                              .Distinct(StringComparer.OrdinalIgnoreCase)
                                              // .Take(3) // si quieres topear por llamada, descomenta
                                              .ToList()!;

                        return new LlamadaPuntoCriticoDTO
                        {
                            idLlamada = g.Key.ToString(),
                            summary = resumen,
                            puntoscriticos = puntosCriticos
                        };
                    })
                    .OrderByDescending(l => l.idLlamada)
                    .ToList();

                if (llamadas.Count == 0)
                {
                    // no hay nada que enviar de este asesor; lo consideramos OK
                    procesadosOk++;
                    continue;
                }

                var fechaGeneracion = asesorGroup.First().FechaReal.Date;

                var payload = new RecomendacionPuntoCriticoLlamadaDTO
                {
                    items = llamadas
                };


                try
                {
                    // Imprimir el JSON generado del payload antes de enviarlo
                    Console.WriteLine(JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }));
                    using var response = await httpClient.PostAsJsonAsync("consolidadocritico/comercial", payload);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var resultadoPuntoCritico = JsonSerializer.Deserialize<ResultadoPuntoCriticoConsolidaddoDTO>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                    // Guardar "congelamiento": JSON crudo tal como vino
                    var resultado = _unitOfWork.LineamientoCalificacionRepository.InsertarCongelamientoPuntoCritico(
                        idPersonal: idPersonal,
                        fechaGeneracion: fechaGeneracion,
                        resultadoPuntoCritico: json
                    );

                    if (resultado) procesadosOk++;
                }
                catch (Exception ex)
                {
                    // log y continuar con el siguiente asesor
                    Console.WriteLine("Error procesando IdPersonal {IdPersonal}", idPersonal);


                }
            }

            // 8) Devuelve OK solo si TODOS los asesores salieron bien
            return procesadosOk == totalAsesores;
        }
        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos el resumen de punto critico diario por asesor
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<PuntoCriticoResumenDiarioDTO> ObtenerPuntoCriticoDiario(PuntoCriticoResumenEntradaDTO payload)
        {
            try
            {
                var idPersonal = payload.IdPersonal;
                var FechaGeneracion = payload.FechaGeneracion;
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerPuntoCriticoDiario(idPersonal, FechaGeneracion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 25/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Guarda calificación manual en tiempo real usando tablas temporales.
        /// Permite calificar llamadas antes de que se registren definitivamente,
        /// usando IdActividadDetalle + NumeroLlamada como identificadores temporales.
        /// Los datos se guardan en T_EvaluacionLlamadaTemporal y T_EvaluacionDetalleManualTemporal.
        /// </summary>
        /// <param name="calificacionTemporal">DTO con datos de calificación temporal</param>
        /// <returns>True si la operación fue exitosa</returns>
        public bool GuardarCalificacionLlamadaTemporal(CalificacionLlamadaManualTemporalDTO calificacionTemporal)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.GuardarCalificacionLlamadaTemporal(calificacionTemporal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 25/01/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las calificaciones temporales de una llamada en tiempo real.
        /// Utiliza IdActividadDetalle + NumeroLlamada para identificar la llamada
        /// antes de que se registre en T_LlamadaWebphoneCruceCentralTresCx.
        /// Consulta las tablas T_EvaluacionLlamadaTemporal y T_EvaluacionDetalleManualTemporal.
        /// </summary>
        /// <param name="idActividadDetalle">ID de la actividad detalle</param>
        /// <param name="numeroLlamada">Número secuencial de la llamada (1, 2, 3, etc.)</param>
        /// <returns>Lista de calificaciones temporales</returns>
        public IEnumerable<CalificacionLlamadaDTO> ObtenerNotaCalificacionLineamientoTemporal(int idActividadDetalle, int numeroLlamada)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerNotaCalificacionLineamientoTemporal(idActividadDetalle, numeroLlamada);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuración de lineamiento para Atención al Cliente basada en el estado y sub-estado de la matrícula.
        /// Lógica temporal para contemplar caso de urgencia. Se debe reformular la lógica de estados.
        /// </summary>
        /// <param name="EstadoMatricula">Estado de la matrícula del alumno</param>
        /// <param name="SubEstadoMatricula">Sub estado de la matrícula del alumno</param>
        /// <returns>Tipo de configuración de lineamiento: "GENERICO", "REPORTADO", "PREREPORTE" o "PAGOATRASADO"</returns>
        private string ObtenerConfiguracionLineamientoATC(string EstadoMatricula, string SubEstadoMatricula)
        {
            try
            {
                var estado = (EstadoMatricula ?? string.Empty).Trim().ToUpperInvariant();
                var subEstado = (SubEstadoMatricula ?? string.Empty).Trim().ToUpperInvariant();

                if (estado == "REGULAR" && subEstado == "PAGO ATRASADO")
                    return "PAGOATRASADO";

                if (estado == "MOROSO" && (
                        subEstado == "PRE REPORTE CR" ||
                        subEstado == "PRE REPORTE CR NO CONTESTA" ||
                        subEstado == "PRE REPORTE CR CONTESTA Y CORTA" ||
                        subEstado == "PRE REPORTE CR NÚMERO DESACTUALIZADO" ||
                        subEstado == "PRE REPORTE CR NUMERO DESACTUALIZADO"
                    ))
                    return "PREREPORTE";

                if (estado == "MOROSO" && (
                        subEstado == "REPORTADO CR" ||
                        subEstado == "REPORTADO CR NO CONTESTA" ||
                        subEstado == "REPORTADO CR CONTESTA Y CORTA" ||
                        subEstado == "REPORTADO CR NÚMERO DESACTUALIZADO" ||
                        subEstado == "REPORTADO CR NUMERO DESACTUALIZADO"
                    ))
                    return "REPORTADO";

                return "GENERICO";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Joseph Llanque.
        /// Fecha: 03/07/2025
        /// Version: 1.0
        /// <summary>
        /// Filtra los lineamientos de evaluación según el tipo de configuración para Atención al Cliente.
        /// Filtra las fases y sus criterios y lineamientos asociados según la configuración:
        /// - GENERICO: fases 318, 319, 320
        /// - REPORTADO: fases 321, 322, 323, 324, 325, 326
        /// - PREREPORTE: fases 327, 328, 329, 330, 331, 332
        /// - PAGOATRASADO: fases 333, 334, 335, 336, 337
        /// </summary>
        /// <param name="configuracionLineamento">Tipo de configuración: "GENERICO", "REPORTADO", "PREREPORTE" o "PAGOATRASADO"</param>
        /// <param name="lineamientos">Objeto con la estructura de lineamientos completos (resultado de BuildLineamientosFormateadosFromSp)</param>
        /// <returns>Objeto de lineamientos filtrado con fases, criterios y lineamientos asociados según la configuración</returns>
        private object CalcularLineamientosATC(string configuracionLineamento, object lineamientos)
        {
            try
            {
                // Definir los IDs de fases según la configuración
                List<int> faseIdsPermitidos;
                switch (configuracionLineamento)
                {
                    case "PAGOATRASADO":
                        faseIdsPermitidos = new List<int> {333,334,335,336,337};
                        break;
                    case "PREREPORTE":
                        faseIdsPermitidos = new List<int> { 327,328,329,330,331,332 };
                        break;
                    case "REPORTADO":
                        faseIdsPermitidos = new List<int> {321,322,323,324,325,326 };
                        break;
                    case "GENERICO":
                    default:
                        faseIdsPermitidos = new List<int> { 318,319,320 };
                        break;
                }

                // Obtener el tipo dinámico del objeto lineamientos
                var lineamientosType = lineamientos.GetType();

                // Obtener la propiedad 'fases' del objeto (Dictionary<string, object>)
                var fasesProp = lineamientosType.GetProperty("fases");
                if (fasesProp == null)
                    return lineamientos; // Si no tiene propiedad fases, devolver sin cambios

                var fasesDict = fasesProp.GetValue(lineamientos) as IDictionary<string, object>;
                if (fasesDict == null)
                    return lineamientos; // Si fases no es un diccionario, devolver sin cambios

                // Filtrar las fases que coincidan con los IDs permitidos
                // Cada fase tiene: { id, orden, criterios: { nombreCriterio: { id, orden, lineamientos: [...] } } }
                var fasesFiltradas = new Dictionary<string, object>();

                foreach (var kvpFase in fasesDict)
                {
                    var nombreFase = kvpFase.Key;
                    var faseObj = kvpFase.Value;
                    var faseType = faseObj.GetType();

                    // Obtener el ID de la fase
                    var idProp = faseType.GetProperty("id");
                    if (idProp != null)
                    {
                        var idValue = idProp.GetValue(faseObj);
                        if (idValue != null && int.TryParse(idValue.ToString(), out int faseId))
                        {
                            // Solo incluir si el ID está en la lista permitida
                            if (faseIdsPermitidos.Contains(faseId))
                            {
                                // Incluir la fase completa con todos sus criterios y lineamientos
                                fasesFiltradas[nombreFase] = faseObj;
                            }
                        }
                    }
                }

                // Obtener la propiedad 'puntosgenerales' del objeto
                var puntosGeneralesProp = lineamientosType.GetProperty("puntosgenerales");
                var puntosGenerales = puntosGeneralesProp?.GetValue(lineamientos);

                // Crear y devolver el nuevo objeto con las fases filtradas
                // Las fases incluyen automáticamente sus criterios y lineamientos asociados
                return new
                {
                    fases = fasesFiltradas,
                    puntosgenerales = puntosGenerales ?? new List<object>()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/01/2025
        /// Version: 1.0
        /// <summary>
        /// Determina el tipo de configuración de lineamiento para Validación de Matrícula.
        /// Se basa en el IdOcurrenciaAlterno para determinar si es Convenio de Voz o Proceso de Venta.
        /// </summary>
        /// <param name="idOcurrenciaAlterno">ID de ocurrencia alterno de la llamada</param>
        /// <returns>Tipo de configuración de lineamiento: "CONVENIOVOZ" o "PROCESOVENTA"</returns>
        private string ObtenerConfiguracionLineamientoValidacionMatricula(int? idOcurrenciaAlterno)
        {
            try
            {
                const int ID_OCURRENCIA_CONVENIO_VOZ = 404;

                if (idOcurrenciaAlterno == ID_OCURRENCIA_CONVENIO_VOZ)
                    return "CONVENIOVOZ";

                return "PROCESOVENTA";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/01/2025
        /// Version: 1.0
        /// <summary>
        /// Filtra los lineamientos de evaluación según el tipo de configuración para Validación de Matrícula.
        /// Filtra las fases y sus criterios y lineamientos asociados según la configuración:
        /// - PROCESOVENTA: fase 338
        /// - CONVENIOVOZ: fase 339
        /// </summary>
        /// <param name="configuracionLineamento">Tipo de configuración: "PROCESOVENTA" o "CONVENIOVOZ"</param>
        /// <param name="lineamientos">Objeto con la estructura de lineamientos completos (resultado de BuildLineamientosFormateadosFromSp)</param>
        /// <returns>Objeto de lineamientos filtrado con fases, criterios y lineamientos asociados según la configuración</returns>
        private object CalcularLineamientosValidacionMatricula(string configuracionLineamento, object lineamientos)
        {
            try
            {
                // Definir los IDs de fases según la configuración
                List<int> faseIdsPermitidos;
                switch (configuracionLineamento)
                {
                    case "CONVENIOVOZ":
                        faseIdsPermitidos = new List<int> { 339 };
                        break;
                    case "PROCESOVENTA":
                    default:
                        faseIdsPermitidos = new List<int> { 338 };
                        break;
                }

                // Obtener el tipo dinámico del objeto lineamientos
                var lineamientosType = lineamientos.GetType();

                // Obtener la propiedad 'fases' del objeto (Dictionary<string, object>)
                var fasesProp = lineamientosType.GetProperty("fases");
                if (fasesProp == null)
                    return lineamientos; // Si no tiene propiedad fases, devolver sin cambios

                var fasesDict = fasesProp.GetValue(lineamientos) as IDictionary<string, object>;
                if (fasesDict == null)
                    return lineamientos; // Si fases no es un diccionario, devolver sin cambios

                // Filtrar las fases que coincidan con los IDs permitidos
                // Cada fase tiene: { id, orden, criterios: { nombreCriterio: { id, orden, lineamientos: [...] } } }
                var fasesFiltradas = new Dictionary<string, object>();

                foreach (var kvpFase in fasesDict)
                {
                    var nombreFase = kvpFase.Key;
                    var faseObj = kvpFase.Value;
                    var faseType = faseObj.GetType();

                    // Obtener el ID de la fase
                    var idProp = faseType.GetProperty("id");
                    if (idProp != null)
                    {
                        var idValue = idProp.GetValue(faseObj);
                        if (idValue != null && int.TryParse(idValue.ToString(), out int faseId))
                        {
                            // Solo incluir si el ID está en la lista permitida
                            if (faseIdsPermitidos.Contains(faseId))
                            {
                                // Incluir la fase completa con todos sus criterios y lineamientos
                                fasesFiltradas[nombreFase] = faseObj;
                            }
                        }
                    }
                }

                // Crear y devolver el nuevo objeto con las fases filtradas (sin puntosgenerales)
                // Las fases incluyen automáticamente sus criterios y lineamientos asociados
                return new
                {
                    fases = fasesFiltradas
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene las observaciones de lineamientos evaluados por IA para una validación de matrícula
        /// </summary>
        /// <param name="idOportunidad">ID de la oportunidad</param>
        /// <param name="tipoValidacionMatricula">Tipo de validación: 1=Proceso Enrollment, 2=Contrato Voz</param>
        /// <returns>Lista de lineamientos con sus observaciones</returns>
        public IEnumerable<ValidacionMatriculaLineamientoDTO> ObtenerValidacionMatriculaLineamiento(int idOportunidad, int tipoValidacionMatricula)
        {
            try
            {
                return _unitOfWork.LineamientoCalificacionRepository.ObtenerValidacionMatriculaLineamiento(idOportunidad, tipoValidacionMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la información de las llamadas asociadas a las validaciones de matrícula de una oportunidad,
        /// agrupadas jerárquicamente: Oportunidad → Validaciones → Llamadas
        /// </summary>
        /// <param name="idOportunidad">ID de la oportunidad</param>
        /// <returns>Objeto agrupado con validaciones y sus llamadas</returns>
        public ValidacionMatriculaInformacionLlamadaResponseDTO ObtenerValidacionMatriculaInformacionLlamada(int idOportunidad)
        {
            try
            {
                // Obtener datos planos del repositorio
                var datosPlanos = _unitOfWork.LineamientoCalificacionRepository.ObtenerValidacionMatriculaInformacionLlamada(idOportunidad);

                if (datosPlanos == null || !datosPlanos.Any())
                {
                    return new ValidacionMatriculaInformacionLlamadaResponseDTO
                    {
                        IdOportunidad = idOportunidad,
                        Validaciones = new List<ValidacionMatriculaConLlamadasDTO>()
                    };
                }

                // Agrupar por IdValidacionMatricula
                var validacionesAgrupadas = datosPlanos
                    .GroupBy(d => new { d.IdValidacionMatricula, d.IdTipoValidacion })
                    .Select(g => new ValidacionMatriculaConLlamadasDTO
                    {
                        IdValidacionMatricula = g.Key.IdValidacionMatricula,
                        IdTipoValidacion = g.Key.IdTipoValidacion,
                        Llamadas = g.Select(llamada => new LlamadaAudioDTO
                        {
                            IdLlamadaWebphoneCruceCentralTresCx = llamada.IdLlamadaWebphoneCruceCentralTresCx,
                            UrlAudio = llamada.UrlAudio,
                            UrlAudio2 = llamada.UrlAudio2,
                            UrlAudioProcesado = llamada.UrlAudioProcesado,
                            Origen = llamada.Origen,
                            DuracionContestoCentral = llamada.DuracionContestoCentral
                        }).ToList()
                    })
                    .ToList();

                // Construir respuesta final
                return new ValidacionMatriculaInformacionLlamadaResponseDTO
                {
                    IdOportunidad = idOportunidad,
                    Validaciones = validacionesAgrupadas
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}








