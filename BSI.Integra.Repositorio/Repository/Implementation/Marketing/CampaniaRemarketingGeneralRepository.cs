using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Dapper;
using Newtonsoft.Json;

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

        public List<object> ObtenerRendimientoListadoCampanias(List<int> ids)
        {
            try
            {
                var mock = new List<object>
                {
                    new { CampaniaId = 1, Rendimiento = 0.85 }
                };
                return mock.Cast<object>().ToList();
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
                var query = "SELECT Id, Nombre FROM [mkt].[T_RemarketingMedioEnvio] WHERE Estado = 1";

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

        public List<ElementoConfiguracionCampania> ObtenerArgumentos()
        {
            try
            {
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "SELECT Id, Nombre FROM [mkt].[T_RemarketingArgumento] WHERE Estado = 1";

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

        public List<ResultadoTextoGeneradoDTO> ObtenerResultadosGeneracionTextoPorCampania(int id)
        {
            try
            {
                //Logica para obtener la ultima generacion de mensaje para un id segmento
                //Mock data temporal
                var resultado = new List<ResultadoTextoGeneradoDTO>
                {
                    new ResultadoTextoGeneradoDTO {
                        Id = 1,
                        IdAlumno = 12873,
                        NombreAlumno = "Alumno 1",
                        Pais = "Peru",
                        ContenidoGenerado = "Buenos dias Alumno 1, te invitamos a revisar nuestro programa"
                    },
                    new ResultadoTextoGeneradoDTO {
                        Id = 2,
                        IdAlumno = 298364,
                        NombreAlumno = "Alumno 2",
                        Pais = "Peru",
                        ContenidoGenerado = "Buenos dias Alumno 2, te invitamos a revisar nuestro programa"
                    },
                    new ResultadoTextoGeneradoDTO {
                        Id = 3,
                        IdAlumno = 9182374,
                        NombreAlumno = "Alumno 3",
                        Pais = "Peru",
                        ContenidoGenerado = "Buenos dias Alumno 3, te invitamos a revisar nuestro programa"
                    },
                    new ResultadoTextoGeneradoDTO {
                        Id = 4,
                        IdAlumno = 928344,
                        NombreAlumno = "Alumno 4",
                        Pais = "Peru",
                        ContenidoGenerado = "Buenos dias Alumno 4, te invitamos a revisar nuestro programa"
                    }
                };

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool InsertarCampaniaRemarketing(EnvioCampaniaRemarketingDTO request)
        {
            try
            {
                var querySP = "[mkt].[SP_InsertarCampaniaRemarketing]";

                var parameters = new DynamicParameters();

                parameters.Add("@NombreCampania", request.Segmento.Nombre);
                parameters.Add("@IdFiltroSegmento", request.Segmento.Id);
                parameters.Add("@IdRemarketingTipoMensaje", request.TipoMensaje);
                parameters.Add("@IdRemarketingLogicaEnvio", request.LogicaEnvio);
                parameters.Add("@RemitenteCorreo", request.RemitenteCorreo);
                parameters.Add("@RemitenteNombre", request.RemitenteNombre);
                parameters.Add("@Asunto", request.Asunto);
                parameters.Add("@EnvioConfigurado", request.EnvioSeleccionado);
                parameters.Add("@FechaEnvioProgramada", request.FechaEnvio);
                parameters.Add("@UsuarioCreacion", request.UsuarioCreacion);
                parameters.Add("@MediosEnvio", string.Join(",", request.MediosEnvio));
                parameters.Add("@Argumentos", string.Join(",", request.Argumentos));

                var result = _dapperRepository.QuerySPFirstOrDefault(querySP, parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DetallesCampaniaDTO VerDetallesCampania(int id)
        {
            try
            {
                var mock = new DetallesCampaniaDTO
                {
                    Programados = 15,
                    Aperturas = 10,
                    Clicks = 5,
                    Rebotados = 2,
                    AlumnosContactados = new List<AlumnoContactadoDTO>
                        {
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 1,
                                EstadoEnvio = "Entregado",
                                NombreAlumno = "Juan Perez",
                                Apertura = true,
                                Click = false
                            },
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 2,
                                EstadoEnvio = "Entregado",
                                NombreAlumno = "Maria Gomez",
                                Apertura = true,
                                Click = true
                            },
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 3,
                                EstadoEnvio = "En Proceso",
                                NombreAlumno = "Daniel Gomez",
                                Apertura = true,
                                Click = false
                            },
                            new AlumnoContactadoDTO
                            {
                                IdAlumno = 4,
                                EstadoEnvio = "Entregado",
                                NombreAlumno = "Juan Gomez",
                                Apertura = false,
                                Click = true
                            }
                        }
                };
                return mock;
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
                                            CG.Asunto, CG.EnvioConfigurado, CG.FechaEnvioProgramada
                                        FROM mkt.T_RemarketingCampaniaGeneral CG
                                        WHERE CG.Id = @Id AND CG.Estado = 1;";
                var jsonResultCampania = _dapperRepository.FirstOrDefault(queryCampania, new { Id = id });

                if (!string.IsNullOrEmpty(jsonResultCampania) && jsonResultCampania != "null")
                    resultado = JsonConvert.DeserializeObject<CampaniaRemarketingIndividualDTO>(jsonResultCampania);

                if (resultado == null)
                    return null;

                // Obtener Medios de envío
                var queryMedios = @"SELECT CAST(IdRemarketingMedioEnvio AS INT) AS Value
                                    FROM mkt.T_RemarketingMedioEnvioCampania
                                    WHERE IdRemarketingCampaniaGeneral = @Id
                                      AND Estado = 1;";
                var jsonResultMedios = _dapperRepository.QueryDapper(queryMedios, new { Id = id });

                if (!string.IsNullOrEmpty(jsonResultMedios) && jsonResultMedios != "null")
                {
                    var listaMedios = JsonConvert.DeserializeObject<List<IntValueDTO>>(jsonResultMedios);
                    resultado.MediosEnvio = listaMedios.Select(x => x.Value).ToList();
                }

                // Obtener Argumentos
                var queryArgumentos = @"SELECT CAST(IdRemarketingArgumento AS INT) AS Value
                                        FROM mkt.T_RemarketingArgumentoCampania
                                        WHERE IdRemarketingCampaniaGeneral = @Id
                                          AND Estado = 1;";
                var jsonResultArgumentos = _dapperRepository.QueryDapper(queryArgumentos, new { Id = id });

                if (!string.IsNullOrEmpty(jsonResultArgumentos) && jsonResultArgumentos != "null")
                {
                    var listaArgumentos = JsonConvert.DeserializeObject<List<IntValueDTO>>(jsonResultArgumentos);
                    resultado.Argumentos = listaArgumentos.Select(x => x.Value).ToList();
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditarCampania()
        {
            try
            {
                return true;
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
                List<ElementoConfiguracionCampania> resultado = new List<ElementoConfiguracionCampania>();
                var query = "UPDATE [mkt].[T_RemarketingCampaniaGeneral] " +
                                "SET Estado = 0, UsuarioModificacion = @Usuario, FechaModificacion = GETDATE()" +
                                "WHERE Id = @Id";

                var jsonResult = _dapperRepository.QueryDapper(query, new { Id = id, Usuario = usuario });

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MensajeGeneradoDTO ObtenerMensajeGeneradoPorId(int id)
        {
            try
            {
                var mensaje = new MensajeGeneradoDTO
                {
                    Id = 1,
                    Contenido = "Hola estimado alumno, te saluda una asesora de BSG Institute, queremos comentarte sobre el curso de BI",
                };

                return mensaje;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
