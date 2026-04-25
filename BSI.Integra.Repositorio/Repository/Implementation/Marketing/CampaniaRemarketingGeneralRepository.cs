using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    public class CampaniaRemarketingGeneralRepository : ICampaniaRemarketingGeneralRepository
    {
        private IDapperRepository _dapperRepository;

        public CampaniaRemarketingGeneralRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<CampaniaRemarketingGeneralDTO> ObtenerListadoCampania()
        {
            try
            {
                List<CampaniaRemarketingGeneralDTO> resultado = new List<CampaniaRemarketingGeneralDTO>();

                var querySP = "[mkt].[SP_ObtenerListadoCampaniaRemarketing]";

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<CampaniaRemarketingGeneralDTO>>(jsonResult);

                return resultado.OrderByDescending(x => x.FechaCreacion).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RendimientoDiarioCampaniaDTO> ObtenerRendimientoCampanias(List<int> ids)
        {
            try
            {
                List<RendimientoDiarioCampaniaDTO> resultado = new List<RendimientoDiarioCampaniaDTO>();
                var querySP = "[mkt].[SP_RemarketingCampaniaGeneralRendimientoObtener]";

                var parameters = new DynamicParameters();
                parameters.Add("@IdRemarketingCampaniaGeneral_Lista", string.Join(",", ids));

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, parameters);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<RendimientoDiarioCampaniaDTO>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ElementoConfiguracionCampania> ObtenerMediosEnvio()
        {
            try
            {
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "SELECT Id, Nombre FROM [pla].[T_MedioComunicacion] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<ElementoConfiguracionCampania>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<ElementoConfiguracionCampania> ObtenerTiposMensaje()
        {
            try
            {
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "SELECT Id, Nombre FROM [mkt].[T_RemarketingTipoMensaje] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<ElementoConfiguracionCampania>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ElementoConfiguracionCampania> ObtenerLogicasEnvio()
        {
            try
            {
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "SELECT Id, Nombre FROM [mkt].[T_RemarketingLogicaEnvio] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<ElementoConfiguracionCampania>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ElementoConfiguracionCampania> ObtenerCategoriaArgumento()
        {
            try
            {
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "SELECT Id, Nombre FROM [mkt].[T_CategoriaArgumentoConfigurado] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<ElementoConfiguracionCampania>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<int> ObtenerPrioridadesUnicas()
        {
            try
            {
                List<int> resultado = new List<int>();
                var querySP = "[mkt].[SP_PrioridadesArgumentoRemarketingObtener]";

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                {
                    // Deserializamos como una lista de objetos dinámicos
                    var objetos = JsonConvert.DeserializeObject<List<dynamic>>(jsonResult);

                    // Extraemos solo el valor de "prioridad" y ordenamos
                    return objetos
                        .Select(x => (int)x.Prioridad)
                        .OrderBy(p => p)
                        .ToList();
                }

                return new List<int>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SegmentoCreadoDTO> ObtenerListadoSegmentosCreados()
        {
            try
            {
                List<SegmentoCreadoDTO> resultado = new List<SegmentoCreadoDTO>();
                var query = "SELECT Id, Nombre, FiltroEjecutado FROM [mkt].[V_TFiltroSegmento_PanelDataBasica] WHERE Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<SegmentoCreadoDTO>>(jsonResult);

                return resultado.OrderByDescending(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertarCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request)
        {
            try
            {
                var querySP = "[mkt].[SP_InsertarCampaniaRemarketing]";

                var parameters = new DynamicParameters();

                parameters.Add("@Nombre", request.Segmento.Nombre);
                parameters.Add("@IdFiltroSegmento", request.Segmento.Id);
                parameters.Add("@IdRemarketingTipoMensaje", request.TipoMensaje.Id);
                parameters.Add("@IdRemarketingLogicaEnvio", request.LogicaEnvio.Id);
                parameters.Add("@RemitenteCorreo", request.RemitenteCorreo);
                parameters.Add("@RemitenteNombre", request.RemitenteNombre);
                parameters.Add("@Asunto", request.Asunto);
                parameters.Add("@EnvioConfigurado", request.EnvioSeleccionado);
                parameters.Add("@FechaEnvioProgramada", request.FechaEnvio);
                parameters.Add("@UsuarioCreacion", request.UsuarioCreacion);
                parameters.Add("@IdMedioComunicacion_Lista", string.Join(",", request.MediosEnvio.Select(x => x.Id)));
                parameters.Add("@IdentificadorLlamadaIA", request.IdentificadorLlamadaIA);
                parameters.Add("@IdCategoriaArgumentoConfigurado", request.CategoriaArgumento?.Id);
                parameters.Add("@Prioridad_Lista", string.Join(",", request.Prioridades));
                parameters.Add("@IdEstadoEnvio", 1);

                var result = _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarCampaniaRemarketing(ConfiguracionCampaniaRemarketingDTO request)
        {
            try
            {
                var querySP = "[mkt].[SP_ActualizarCampaniaRemarketing]";

                var parameters = new DynamicParameters();

                parameters.Add("@IdRemarketingCampaniaGeneral", request.Id);
                parameters.Add("@Nombre", request.Segmento.Nombre);
                parameters.Add("@IdFiltroSegmento", request.Segmento.Id);
                parameters.Add("@IdRemarketingTipoMensaje", request.TipoMensaje.Id);
                parameters.Add("@IdRemarketingLogicaEnvio", request.LogicaEnvio.Id);
                parameters.Add("@RemitenteCorreo", request.RemitenteCorreo);
                parameters.Add("@RemitenteNombre", request.RemitenteNombre);
                parameters.Add("@Asunto", request.Asunto);
                parameters.Add("@EnvioConfigurado", request.EnvioSeleccionado);
                parameters.Add("@FechaEnvioProgramada", request.FechaEnvio);
                parameters.Add("@UsuarioModificacion", request.UsuarioCreacion);
                parameters.Add("@IdMedioComunicacion_Lista", string.Join(",", request.MediosEnvio.Select(x => x.Id)));
                parameters.Add("@IdentificadorLlamadaIA", request.IdentificadorLlamadaIA);
                parameters.Add("@IdCategoriaArgumentoConfigurado", request.CategoriaArgumento?.Id);
                parameters.Add("@Prioridad_Lista", string.Join(",", request.Prioridades));

                var result = _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CampaniaRemarketingIndividualDTO ObtenerCampaniaRemarketingPorId(int id)
        {
            try
            {
                CampaniaRemarketingIndividualDTO resultado = new CampaniaRemarketingIndividualDTO();

                //Obtener Campania
                var queryCampania = @"SELECT CG.Id, CG.Nombre, CG.IdFiltroSegmento, CG.IdRemarketingTipoMensaje AS TipoMensaje,
                                            CG.IdRemarketingLogicaEnvio AS LogicaEnvio, CG.RemitenteCorreo, CG.RemitenteNombre,
                                            CG.Asunto, CG.EnvioConfigurado, CG.FechaEnvioProgramada, CG.IdentificadorLlamadaIA,
                                            CG.IdCategoriaArgumentoConfigurado AS CategoriaArgumento
                                        FROM mkt.T_RemarketingCampaniaGeneral CG
                                        WHERE CG.Id = @Id AND CG.Estado = 1;";
                var jsonResultCampania = _dapperRepository.FirstOrDefault(queryCampania, new { Id = id });

                if (!string.IsNullOrEmpty(jsonResultCampania) && jsonResultCampania != "null")
                    resultado = JsonConvert.DeserializeObject<CampaniaRemarketingIndividualDTO>(jsonResultCampania);

                if (resultado == null)
                    return null;

                // Obtener Medios de envío
                var queryMedios = @"SELECT CAST(IdMedioComunicacion AS INT) AS Value
                                    FROM mkt.T_RemarketingMedioEnvioCampania
                                    WHERE IdRemarketingCampaniaGeneral = @Id
                                      AND Estado = 1;";
                var jsonResultMedios = _dapperRepository.QueryDapper(queryMedios, new { Id = id });

                if (!string.IsNullOrEmpty(jsonResultMedios) && jsonResultMedios != "null")
                {
                    var listaMedios = JsonConvert.DeserializeObject<List<IntValueDTO>>(jsonResultMedios);
                    resultado.MediosEnvio = listaMedios.Select(x => x.Value).ToList();
                }

                // Obtener Prioridades
                var queryPrioridades = @"SELECT CAST(Prioridad AS INT) AS Value
                                        FROM mkt.T_RemarketingCampaniaPrioridad
                                        WHERE IdRemarketingCampaniaGeneral = @Id
                                          AND Estado = 1;";
                var jsonResultPrioridades = _dapperRepository.QueryDapper(queryPrioridades, new { Id = id });

                if (!string.IsNullOrEmpty(jsonResultPrioridades) && jsonResultPrioridades != "null")
                {
                    var listaPrioridades = JsonConvert.DeserializeObject<List<IntValueDTO>>(jsonResultPrioridades);
                    resultado.Prioridades = listaPrioridades.Select(x => x.Value).ToList();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DetallesCampaniaDTO ObtenerDetallesGeneralesEnvio(int idCampaniaRemarketing)
        {
            try
            {
                var querySP = "[mkt].[SP_CampaniaEnvioDetalleObtener]";
                var jsonResult = _dapperRepository.QuerySPDapper(querySP, new { IdRemarketingCampaniaGeneral = idCampaniaRemarketing });

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "[]")
                {
                    return new DetallesCampaniaDTO
                    {
                        TotalMensajes = 0,
                        Enviados = 0,
                        Abiertos = 0,
                        Rebotados = 0,
                        EstadoAlumnos = new List<EstadoAlumnosDTO>()
                    };
                }

                // Deserializamos a la clase completa que incluye ambos tipos de datos
                var lista = JsonConvert.DeserializeObject<List<ResultadoCompletoDTO>>(jsonResult);

                if (lista == null || lista.Count == 0)
                {
                    return new DetallesCampaniaDTO
                    {
                        TotalMensajes = 0,
                        Enviados = 0,
                        Abiertos = 0,
                        Rebotados = 0,
                        EstadoAlumnos = new List<EstadoAlumnosDTO>()
                    };
                }

                // Tomamos los totales de la primera fila (son iguales en todas)
                var primeraFila = lista[0];

                // Mapeamos el detalle de alumnos desde todas las filas
                var estadoAlumnos = lista
                    .Select(x => new EstadoAlumnosDTO
                    {
                        IdAlumno = x.IdAlumno,
                        EstadoEnvio = x.EstadoEnvio,
                        Abierto = x.Abierto,
                        Rebotado = x.Rebotado,
                        RazonRechazo = x.RazonRechazo,
                        FechaEnvio = x.FechaEnvio
                    })
                    .OrderByDescending(x => x.FechaEnvio)
                    .ToList();

                // Construimos el resultado final
                var resultado = new DetallesCampaniaDTO
                {
                    TotalMensajes = primeraFila.TotalMensajes,
                    Enviados = primeraFila.Enviados,
                    Abiertos = primeraFila.Abiertos,
                    Rebotados = primeraFila.Rebotados,
                    EstadoAlumnos = estadoAlumnos
                };

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener detalles de campaña: {ex.Message}", ex);
            }
        }

        public ElementoEstadoEnvio ObtenerEstadoEnvioCampaniaRemarketing(int idCampaniaRemarketing)
        {
            try
            {
                ElementoEstadoEnvio resultado = new ElementoEstadoEnvio();
                var query = "SELECT TOP 1 IdEstadoEnvio FROM [mkt].[T_RemarketingCampaniaGeneral] WHERE Estado = 1 AND Id = @Id";

                var jsonResult = _dapperRepository.FirstOrDefault(query, new { Id = idCampaniaRemarketing });

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<ElementoEstadoEnvio>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EliminarCampania(int id, string usuario)
        {
            try
            {
                var querySP = "[mkt].[SP_CampaniaRemarketingCampaniaEliminar]";

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, new { IdRemarketingCampaniaGeneral = id, UsuarioModificacion = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AlumnoCorreoDTO> ObtenerAlumnosCorreosPorFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<AlumnoCorreoDTO> resultado = new List<AlumnoCorreoDTO>();
                var querySP = "[mkt].[SP_AlumnosObtenerPorFiltroSegmento]";

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, new { IdFiltroSegmento = idFiltroSegmento });

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<AlumnoCorreoDTO>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarEstadoEnvioCampania(int idCampaniaRemarketing, int estadoEnvio, string usuario)
        {
            try
            {
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "UPDATE [mkt].[T_RemarketingCampaniaGeneral] " +
                                "SET IdEstadoEnvio = @IdEstadoEnvio, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()" +
                                "WHERE Id = @Id";

                var jsonResult = _dapperRepository.QueryDapper(query, new { Id = idCampaniaRemarketing, IdEstadoEnvio = estadoEnvio, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 11/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta por bloque los múltiples registros de estado de envío de campaña
        /// </summary>
        public bool InsertarEstadosEnvioCampaniaMasivo(List<RemarketingEstadoCampaniaDTO> estados)
        {
            try
            {
                if (estados == null || !estados.Any())
                    return true;

                var table = ConvertirListaADataTable(estados);

                var parameters = new DynamicParameters();
                parameters.Add("@ListadoEstados", table.AsTableValuedParameter("mkt.RemarketingEstadoEnvioCampaniaType"));

                _dapperRepository.QuerySPDapper("[mkt].[SP_CampaniaRemarketingInsertarEnvioDetalle]", parameters);

                return true;
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 11/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las campanias programadas para ser ejecutadas
        /// </summary>
        public List<CampaniaProgramadaParaEjecutarDTO> ObtenerCampaniasProgramadasParaEjecutar()
        {
            try
            {
                List<CampaniaProgramadaParaEjecutarDTO> resultado = new List<CampaniaProgramadaParaEjecutarDTO>();
                var querySP = "[mkt].[SP_RemarketingCampaniaGeneralObtenerProgramas]";

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, null);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null")
                    resultado = JsonConvert.DeserializeObject<List<CampaniaProgramadaParaEjecutarDTO>>(jsonResult);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertarCampaniaCanvas(CampaniaCanvasDTO request, string usuario)
        {
            try
            {
                var querySP = "[mkt].[SP_RemarketingCampaniaCanvasInsertar]";

                var parameters = new DynamicParameters();
                parameters.Add("@IdRemarketingCampaniaGeneral", request.IdRemarketingCampaniaGeneral);
                parameters.Add("@ContenidoSuperior", request.ContenidoSuperior);
                parameters.Add("@ContenidoInferior", request.ContenidoInferior);
                parameters.Add("@UsuarioCreacion", usuario);

                _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ActualizarCampaniaCanvas(CampaniaCanvasDTO request, string usuario)
        {
            try
            {
                var querySP = "[mkt].[SP_RemarketingCampaniaCanvasActualizar]";

                var parameters = new DynamicParameters();
                parameters.Add("@IdRemarketingCampaniaCanvas", request.Id);
                parameters.Add("@ContenidoSuperior", request.ContenidoSuperior);
                parameters.Add("@ContenidoInferior", request.ContenidoInferior);
                parameters.Add("@UsuarioModificacion", usuario);

                _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CampaniaCanvasDTO ObtenerCampaniaCanvas(int idRemarketingCampaniaGeneral)
        {
            try
            {
                var querySP = "[mkt].[SP_TRemarketingCampaniaCanvas_ObtenerPorCampaniaGeneral]";

                var parameters = new DynamicParameters();
                parameters.Add("@IdRemarketingCampaniaGeneral", idRemarketingCampaniaGeneral);

                var jsonResult = _dapperRepository.QuerySPDapper(querySP, parameters);

                if (!string.IsNullOrEmpty(jsonResult) && jsonResult != "null" && jsonResult != "[]")
                {
                    var lista = JsonConvert.DeserializeObject<List<CampaniaCanvasDTO>>(jsonResult);
                    return lista?.FirstOrDefault();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EliminarCampaniaCanvas(int idRemarketingCampaniaGeneral, string usuario)
        {
            try
            {
                var querySP = "[mkt].[SP_RemarketingCampaniaCanvasEliminar]";

                var parameters = new DynamicParameters();
                parameters.Add("@IdRemarketingCampaniaGeneral", idRemarketingCampaniaGeneral);
                parameters.Add("@UsuarioModificacion", usuario);

                _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable ConvertirListaADataTable(List<RemarketingEstadoCampaniaDTO> estados)
        {
            var table = new DataTable();

            table.Columns.Add("IdRemarketingCampaniaGeneral", typeof(int));
            table.Columns.Add("IdAlumno", typeof(int));
            table.Columns.Add("IdentificadorMensaje", typeof(string));
            table.Columns.Add("Enviado", typeof(bool));
            table.Columns.Add("Entregado", typeof(bool));
            table.Columns.Add("Abierto", typeof(bool));
            table.Columns.Add("Rebotado", typeof(bool));
            table.Columns.Add("RazonRechazo", typeof(string));
            table.Columns.Add("EstadoMandrill", typeof(string));
            table.Columns.Add("UsuarioCreacion", typeof(string));

            foreach (var item in estados)
            {
                table.Rows.Add(
                    item.IdCampaniaRemarketing,
                    item.IdAlumno,
                    item.IdentificadorMensaje ?? (object)DBNull.Value,
                    item.Enviado,
                    item.Entregado,
                    item.Abierto,
                    item.Rebotado,
                    item.RazonRechazo ?? (object)DBNull.Value,
                    item.EstadoMandrill ?? (object)DBNull.Value,
                    item.UsuarioCreacion
                );
            }

            return table;
        }

    }
}
