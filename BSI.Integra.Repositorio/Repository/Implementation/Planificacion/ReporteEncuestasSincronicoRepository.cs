using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ReporteEncuestasSincronicoRepository: IReporteEncuestasSincronicoRepository
    {
        /// Repositorio: ReporteEncuestasSincronicoRepository
        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// <summary>
        /// Gestión general del Reporte Encuesta Inicial, Intermedia y Final Sincrónicas
        /// </summary>
        private IDapperRepository _dapperRepository;

        public ReporteEncuestasSincronicoRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// Repositorio : ReporteEncuestasSincronicoRepository
        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// <summary>
        /// /// Obtener registros de docentes para combos
        /// </summary>
        /// <param></param>
        /// <returns>Lista de objetos del tipo ReporteEncuestasDTO</returns>
        public IEnumerable<ComboDTO> ObtenerComboDocentes()
        {
            try
            {
                var query = @"
                                SELECT
                                    Id,Nombre
                                FROM 
                                    fin.V_Obtener_ProveedorParaHonorario
                                ORDER BY Nombre
                            ";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Repositorio : ReporteEncuestasSincronicoRepository
        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas iniciales
        /// </summary>
        /// <param name="Filtro">Datos traidos desde la interfaz para el filtro en el sp</param>
        /// <returns>List<ReporteEncuestasInicialSincronicoDTO></returns>
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaInicialSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro)
        {
            try
            {
                string? IdPrograma = null, IdDocente = null, IdCurso = null;
                if (Filtro.IdsProgramasGenerales != null && Filtro.IdsProgramasGenerales.Count() > 0)
                    IdPrograma = string.Join(",", Filtro.IdsProgramasGenerales);
                if (Filtro.IdsProgramasEspecificos != null && Filtro.IdsProgramasEspecificos.Count() > 0)
                    IdCurso = string.Join(",", Filtro.IdsProgramasEspecificos);
                if (Filtro.IdsDocentes != null && Filtro.IdsDocentes.Count() > 0)
                    IdDocente = string.Join(",", Filtro.IdsDocentes);
                DateTime FechaInicio = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                var resultado = _dapperRepository.QuerySPDapper("pw.SP_PW_ReporteEncuestaInicialSincronicaPorVersion", new
                {
                    FechaHoraInicio_Inicio = FechaInicio,
                    FechaHoraInicio_Fin = FechaFin,
                    IdPGeneral = IdPrograma,
                    IdPEspecifico = IdCurso,
                    IdProveedor = IdDocente,
                    Filtro.Version
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    var registros = JsonConvert.DeserializeObject<List<ReporteEncuestasSincronicoPorVersionDTO>>(resultado)!
                                    .Select((r, index) => { r.OrdenPregunta = index; return r; })
                                    .ToList();

                    var agrupado = registros
                        .GroupBy(r => r.IdPEspecificoSesionEncuestaAlumno)
                        .OrderBy(g => g.First().OrdenPregunta)
                        .Select(grupoAlumno => new ReporteEncuestaAgrupadoDTO
                        {
                            IdPEspecificoSesionEncuestaAlumno = grupoAlumno.Key,
                            CentroCostoPrograma = grupoAlumno.First().CentroCostoPrograma,
                            ProgramaGeneral = grupoAlumno.First().ProgramaGeneral,
                            ProgramaEspecifico = grupoAlumno.First().ProgramaEspecifico,
                            CentroCostoCurso = grupoAlumno.First().CentroCostoCurso,
                            CursoGeneral = grupoAlumno.First().CursoGeneral,
                            CursoEspecifico = grupoAlumno.First().CursoEspecifico,
                            Docente = grupoAlumno.First().Docente,
                            FechaRealizada = grupoAlumno.First().FechaRealizada,
                            FechaIngreso = grupoAlumno.First().FechaIngreso,
                            CodigoMatricula = grupoAlumno.First().CodigoMatricula,
                            NombreAlumno = grupoAlumno.First().NombreAlumno,
                            Correo = grupoAlumno.First().Correo,
                            AsesoraAcademica = grupoAlumno.First().AsesoraAcademica,
                            ComentarioAlumno = grupoAlumno.First().ComentarioAlumno,
                            RegistroPreguntas = grupoAlumno
                            .GroupBy(p => new { p.IdPreguntaEncuesta })
                            .Select(grupoPregunta => new PreguntaAgrupadaDTO
                            {
                                IdPreguntaEncuesta = grupoPregunta.Key.IdPreguntaEncuesta,
                                Pregunta = grupoPregunta.First().Pregunta,
                                Respuestas = grupoPregunta
                                .Select(r => new RespuestaAgrupadaDTO
                                {
                                    Valor = r.Valor,
                                    IdPreguntaRespuestaEncuesta = r.IdPreguntaRespuestaEncuesta
                                })
                                .OrderBy(r => grupoPregunta
                                    .FirstOrDefault(x => x.IdPreguntaRespuestaEncuesta == r.IdPreguntaRespuestaEncuesta)?.IdPEspecificoSesionEncuestaAlumnoRespuesta
                                )
                                .ToList()
                            }).ToList()
                        }).ToList();

                    return agrupado;
                }

                return new List<ReporteEncuestaAgrupadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Repositorio : ReporteEncuestasSincronicoRepository
        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas intermedias
        /// </summary>
        /// <param name="Filtro">Datos traidos desde la interfaz para el filtro en el sp</param>
        /// <returns>List<ReporteEncuestasIntermediaSincronicoDTO></returns>
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaIntermediaSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro)
        {
            try
            {
                string? IdPrograma = null, IdDocente = null, IdCurso = null;
                if (Filtro.IdsProgramasGenerales != null && Filtro.IdsProgramasGenerales.Count() > 0)
                    IdPrograma = string.Join(",", Filtro.IdsProgramasGenerales);
                if (Filtro.IdsProgramasEspecificos != null && Filtro.IdsProgramasEspecificos.Count() > 0)
                    IdCurso = string.Join(",", Filtro.IdsProgramasEspecificos);
                if (Filtro.IdsDocentes != null && Filtro.IdsDocentes.Count() > 0)
                    IdDocente = string.Join(",", Filtro.IdsDocentes);
                DateTime FechaInicio = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                var resultado = _dapperRepository.QuerySPDapper("pw.SP_PW_ReporteEncuestaIntermediaSincronicaPorVersion", new
                {
                    FechaHoraInicio_Inicio = FechaInicio,
                    FechaHoraInicio_Fin = FechaFin,
                    IdPGeneral = IdPrograma,
                    IdPEspecifico = IdCurso,
                    IdProveedor = IdDocente,
                    Filtro.Version
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    var registros = JsonConvert.DeserializeObject<List<ReporteEncuestasSincronicoPorVersionDTO>>(resultado)!
                                    .Select((r, index) => { r.OrdenPregunta = index; return r; })
                                    .ToList();


                    var agrupado = registros
                        .GroupBy(r => r.IdPEspecificoSesionEncuestaAlumno)
                        .OrderBy(g => g.First().OrdenPregunta)
                        .Select(grupoAlumno => new ReporteEncuestaAgrupadoDTO
                        {
                            IdPEspecificoSesionEncuestaAlumno = grupoAlumno.Key,
                            CentroCostoPrograma = grupoAlumno.First().CentroCostoPrograma,
                            ProgramaGeneral = grupoAlumno.First().ProgramaGeneral,
                            ProgramaEspecifico = grupoAlumno.First().ProgramaEspecifico,
                            CentroCostoCurso = grupoAlumno.First().CentroCostoCurso,
                            CursoGeneral = grupoAlumno.First().CursoGeneral,
                            CursoEspecifico = grupoAlumno.First().CursoEspecifico,
                            Docente = grupoAlumno.First().Docente,
                            FechaRealizada = grupoAlumno.First().FechaRealizada,
                            FechaIngreso = grupoAlumno.First().FechaIngreso,
                            CodigoMatricula = grupoAlumno.First().CodigoMatricula,
                            NombreAlumno = grupoAlumno.First().NombreAlumno,
                            Correo = grupoAlumno.First().Correo,
                            AsesoraAcademica = grupoAlumno.First().AsesoraAcademica,
                            ComentarioAlumno = grupoAlumno.First().ComentarioAlumno,
                            RegistroPreguntas = grupoAlumno
                            .GroupBy(p => new { p.IdPreguntaEncuesta })
                            .Select(grupoPregunta => new PreguntaAgrupadaDTO
                            {
                                IdPreguntaEncuesta = grupoPregunta.Key.IdPreguntaEncuesta,
                                Pregunta = grupoPregunta.First().Pregunta,
                                Respuestas = grupoPregunta
                                .Select(r => new RespuestaAgrupadaDTO
                                {
                                    Valor = r.Valor,
                                    IdPreguntaRespuestaEncuesta = r.IdPreguntaRespuestaEncuesta
                                })
                                .OrderBy(r => grupoPregunta
                                    .FirstOrDefault(x => x.IdPreguntaRespuestaEncuesta == r.IdPreguntaRespuestaEncuesta)?.IdPEspecificoSesionEncuestaAlumnoRespuesta
                                )
                                .ToList()
                            }).ToList()
                        }).ToList();

                    return agrupado;
                }

                return new List<ReporteEncuestaAgrupadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Repositorio : ReporteEncuestasSincronicoRepository
        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas finales
        /// </summary>
        /// <param name="Filtro">Datos traidos desde la interfaz para el filtro en el sp</param>
        /// <returns>List<ReporteEncuestasFinalSincronicoDTO></returns>
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaFinalSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro)
        {
            try
            {
                string? IdPrograma = null, IdDocente = null, IdCurso = null;
                if (Filtro.IdsProgramasGenerales != null && Filtro.IdsProgramasGenerales.Count() > 0)
                    IdPrograma = string.Join(",", Filtro.IdsProgramasGenerales);
                if (Filtro.IdsProgramasEspecificos != null && Filtro.IdsProgramasEspecificos.Count() > 0)
                    IdCurso = string.Join(",", Filtro.IdsProgramasEspecificos);
                if (Filtro.IdsDocentes != null && Filtro.IdsDocentes.Count() > 0)
                    IdDocente = string.Join(",", Filtro.IdsDocentes);
                DateTime FechaInicio = new DateTime(Filtro.FechaInicio.Year, Filtro.FechaInicio.Month, Filtro.FechaInicio.Day, 0, 0, 0);
                DateTime FechaFin = new DateTime(Filtro.FechaFin.Year, Filtro.FechaFin.Month, Filtro.FechaFin.Day, 23, 59, 59);

                var resultado = _dapperRepository.QuerySPDapper("pw.SP_PW_ReporteEncuestaFinalSincronicaPorVersion", new
                {
                    FechaHoraInicio_Inicio = FechaInicio,
                    FechaHoraInicio_Fin = FechaFin,
                    IdPGeneral = IdPrograma,
                    IdPEspecifico = IdCurso,
                    IdProveedor = IdDocente,
                    Filtro.Version
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    var registros = JsonConvert.DeserializeObject<List<ReporteEncuestasSincronicoPorVersionDTO>>(resultado)!
                                    .Select((r, index) => { r.OrdenPregunta = index; return r; })
                                    .ToList();

                    var agrupado = registros
                        .GroupBy(r => r.IdPEspecificoSesionEncuestaAlumno)
                        .OrderBy(g => g.First().OrdenPregunta)
                        .Select(grupoAlumno => new ReporteEncuestaAgrupadoDTO
                        {
                            IdPEspecificoSesionEncuestaAlumno = grupoAlumno.Key,
                            CentroCostoPrograma = grupoAlumno.First().CentroCostoPrograma,
                            ProgramaGeneral = grupoAlumno.First().ProgramaGeneral,
                            ProgramaEspecifico = grupoAlumno.First().ProgramaEspecifico,
                            CentroCostoCurso = grupoAlumno.First().CentroCostoCurso,
                            CursoGeneral = grupoAlumno.First().CursoGeneral,
                            CursoEspecifico = grupoAlumno.First().CursoEspecifico,
                            Docente = grupoAlumno.First().Docente,
                            FechaRealizada = grupoAlumno.First().FechaRealizada,
                            FechaIngreso = grupoAlumno.First().FechaIngreso,
                            CodigoMatricula = grupoAlumno.First().CodigoMatricula,
                            NombreAlumno = grupoAlumno.First().NombreAlumno,
                            ComentarioAlumno = grupoAlumno.First().ComentarioAlumno,
                            Correo = grupoAlumno.First().Correo,
                            AsesoraAcademica = grupoAlumno.First().AsesoraAcademica,
                            RegistroPreguntas = grupoAlumno
                            .GroupBy(p => new { p.IdPreguntaEncuesta })
                            .Select(grupoPregunta => new PreguntaAgrupadaDTO
                            {
                                IdPreguntaEncuesta = grupoPregunta.Key.IdPreguntaEncuesta,
                                Pregunta = grupoPregunta.First().Pregunta,
                                Respuestas = grupoPregunta
                                .Select(r => new RespuestaAgrupadaDTO
                                {
                                    Valor = r.Valor,
                                    IdPreguntaRespuestaEncuesta = r.IdPreguntaRespuestaEncuesta
                                })
                                .OrderBy(r => grupoPregunta
                                    .FirstOrDefault(x => x.IdPreguntaRespuestaEncuesta == r.IdPreguntaRespuestaEncuesta)?.IdPEspecificoSesionEncuestaAlumnoRespuesta
                                )
                                .ToList()
                            }).ToList()
                        }).ToList();

                    return agrupado;
                }

                return new List<ReporteEncuestaAgrupadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ReporteEncuestasDocenteDTO>? GenerarReporteEncuestaDocente(ReporteEncuestaFiltroSincronicoDTO filtro)
        {
            try
            {
            
                string? idPrograma = null, idDocente = null, idCurso = null;

                if (filtro.IdsProgramasGenerales != null && filtro.IdsProgramasGenerales.Count > 0)
                    idPrograma = string.Join(",", filtro.IdsProgramasGenerales);

                if (filtro.IdsProgramasEspecificos != null && filtro.IdsProgramasEspecificos.Count > 0)
                    idCurso = string.Join(",", filtro.IdsProgramasEspecificos);

                if (filtro.IdsDocentes != null && filtro.IdsDocentes.Count > 0)
                    idDocente = string.Join(",", filtro.IdsDocentes);

          
                DateTime fechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                DateTime fechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);

                List<ReporteEncuestasDocenteDTO> reporteEncuestasDocente = new List<ReporteEncuestasDocenteDTO>();

           
                var resultado = _dapperRepository.QuerySPDapper("[pw].[SP_PW_ReporteEncuestaDocente]", new
                {
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    IdPrograma = idPrograma,
                    IdCurso = idCurso,
                    IdDocente = idDocente
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    return reporteEncuestasDocente = JsonConvert.DeserializeObject<List<ReporteEncuestasDocenteDTO>>(resultado)!;
                }

                return reporteEncuestasDocente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar el reporte de encuestas: {ex.Message}");
            }
        }

		public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioSincronico(filtroTestimonioDTO filtro)
		{
			try
			{
				List<ReportePGeneralTestimonio> reporteTestimonioSincronico = new List<ReportePGeneralTestimonio>();

				var query = "pla.SP_ObtenerTestimonioEncuestaSincronicaV2";
				var parametros = new
				{
					IdPEspecifico = filtro.idPEspecifico
				};
				var resultado = _dapperRepository.QuerySPDapper(query, parametros);

				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
				{
					return reporteTestimonioSincronico = JsonConvert.DeserializeObject<List<ReportePGeneralTestimonio>>(resultado)!;
				}
				return reporteTestimonioSincronico;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioASincronico(filtroTestimonioDTO filtro)
		{
			try
			{
				List<ReportePGeneralTestimonio> reporteTestimonioSincronico = new List<ReportePGeneralTestimonio>();

				var query = "pla.SP_ObtenerTestimonioEncuestaASincronicaV2";
				var parametros = new
				{
					IdPEspecifico = filtro.idPEspecifico
				};
				var resultado = _dapperRepository.QuerySPDapper(query, parametros);

				if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
				{
					return reporteTestimonioSincronico = JsonConvert.DeserializeObject<List<ReportePGeneralTestimonio>>(resultado)!;
				}
				return reporteTestimonioSincronico;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


        public List<ComboDTO> ObtenerRespuestaEncuestaCombo(FiltroRespuestaCombo filtro)
        {
            try
            {
                List<ComboDTO> rptaFinal = new List<ComboDTO>();
                try
                {
                    List<ComboDTO> Versiones = new List<ComboDTO>();

                    var query = "pla.SP_ObtenerListaRespuestaEncuestaV2";
                    var parametros = new
                    {
                        IdRespuestas = filtro.IdRespuestasAsociada,
                        Modal = filtro.Version,
                    };
                    var resultado = _dapperRepository.QuerySPDapper(query, parametros);

                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
                    {
                        return Versiones = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                    }
                    return Versiones;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TestimonioEncuestaObtenerDTO ObtenerDatosTestimonioSincronicoASinEstado(int id, int modalidad)
        {
            try
            {
                TestimonioEncuestaObtenerDTO resultado = new TestimonioEncuestaObtenerDTO();
                var _query = "";
                if (modalidad == 1)
                {
                    _query = "SELECT Id, Testimonio,IdPEspecificoSesionEncuestaAlumnoRespuesta,VisiblePW ,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion FROM pla.T_TestimonioEncuestaSincronico WHERE IdPEspecificoSesionEncuestaAlumnoRespuesta =@id";
                }
                else
                {
                    _query = "SELECT Id, Testimonio,IdExamenRealizadoRespuestaAulaVirtual,VisiblePW ,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion FROM pla.T_TestimonioEncuestaASincronico WHERE IdExamenRealizadoRespuestaAulaVirtual =@id ";
                }

                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { id });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return resultado = JsonConvert.DeserializeObject<TestimonioEncuestaObtenerDTO>(respuestaDapper);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#GT-PLA-001@Error en ObtenerDatosTestimonio() {ex.Message}", ex);
            }
        }
        public void ActualizarTestimonio(TestimonioEntidadActualizarDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_TestimonioEncuestaAsincronicoSincronico_ActualizarTestimonio";
                var parametros = new
                {
                    Id = dto.Id,
                    Testimonio = dto.Testimonio,
                    Modalidad = dto.Modalidad,
                    VisiblePW = dto.VisiblePW,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#GT-PLA-001@Error ActualizarTestimonio() {ex.Message}", ex);
            }
        }
        public void ActualizarEstadoTestimonioEstado(TestimonioEntidadActualizarDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_TestimonioEncuestaAsincronaSincronica_ActualizarEstado";
                var parametros = new
                {
                    Id = dto.Id,
                    Testimonio = dto.Testimonio,
                    Modalidad = dto.Modalidad,
                    VisiblePW = dto.VisiblePW,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#GT-PLA-001@Error en ActualizarEstadoTestimonio() {ex.Message}", ex);
            }
        }

        public void GuardarTestimonio(TestimonioEntidadDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_TestimonioEncuestaSincronicoAsincronico_Insertar";
                var parametros = new
                {
                    Testimonio = dto.Testimonio,
                    IdRespuesta = dto.IdRespuesta,
                    VisiblePW = dto.VisiblePW,
                    Usuario = usuario,
                    Modalidad = dto.Modalidad
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#GT-PLA-001@Error en GuardarTestimonio() {ex.Message}", ex);
            }
        }

        public List<ReportePGeneralValoracion>? GenerarReporteValoracionTotal(filtroValoracionDTO filtro)
        {
            try
            {
                List<ReportePGeneralValoracion> reporteTestimonioSincronico = new List<ReportePGeneralValoracion>();

                var query = "pla.SP_ObtenerPromedioValoracionEncuestaSincronicaAsincronica";
                var parametros = new
                {
                    IdPEspecifico = filtro.idPEspecifico,
                    IdPGeneral = filtro.idPGeneral

                };
                var resultado = _dapperRepository.QuerySPDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
                {
                    return reporteTestimonioSincronico = JsonConvert.DeserializeObject<List<ReportePGeneralValoracion>>(resultado)!;
                }
                return reporteTestimonioSincronico;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void ActualizarVisibleValoracionEncuesta(int id, int modalidad, string usuario)
        {
            try
            {
                var query = "pla.SP_ValoracionEncuestaAsincronaSincronica_ActualizarVisiblePw";
                var parametros = new
                {
                    Id = id,
                    Modalidad = modalidad,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#GT-PLA-001@Error ActualizarVisibleValoracionEncuesta() {ex.Message}", ex);
            }
        }
    }
}
