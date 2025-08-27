using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ReporteEncuestasService
    /// Autor: Jeremy Pacheco
    /// Fecha: 12/11/2024
    /// Version 1.0
    /// <summary>
    /// Gestión general del Reporte Encuesta Inicial, Intermedia y Final Sincrónicas
    /// </summary>
    public class ReporteEncuestasSincronicoService: IReporteEncuestasSincronicoService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteEncuestasSincronicoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// Version 1.0
        /// <summary>
        /// Obtener registros de docentes para combos
        /// </summary>
        /// <param></param>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboDocentes()
        {

            try
            {
                return _unitOfWork.ReporteEncuestasSincronicoRepository.ObtenerComboDocentes();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// Version 1.0
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas iniciales
        /// </summary>
        /// <param name="encuestaOnlineFiltroDTO">Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestaAgrupadoDTO> </returns>
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaInicialSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro)
        {
            try
            {
                List<ReporteEncuestaAgrupadoDTO> ReporteEncuestaInicial = new List<ReporteEncuestaAgrupadoDTO>();
                ReporteEncuestaInicial = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteEncuestaInicialSincronico(Filtro);
                return ReporteEncuestaInicial;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// Version 1.0
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas intermedias
        /// </summary>
        /// <param name="encuestaOnlineFiltroDTO">Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasIntermediaSincronicoDTO> </returns>
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaIntermediaSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro)
        {
            try
            {
                List<ReporteEncuestaAgrupadoDTO> ReporteEncuestaIntermedia = new List<ReporteEncuestaAgrupadoDTO>();
                ReporteEncuestaIntermedia = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteEncuestaIntermediaSincronico(Filtro);
                return ReporteEncuestaIntermedia;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 12/11/2024
        /// Version 1.0
        /// <summary>
        /// Obtiene reporte de encuestas sincrónicas finales
        /// </summary>
        /// <param name="encuestaOnlineFiltroDTO">Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasFinalSincronicoDTO> </returns>
        public List<ReporteEncuestaAgrupadoDTO>? GenerarReporteEncuestaFinalSincronico(ReporteEncuestaFiltroSincronicoPorVersionDTO Filtro)
        {
            try
            {
                List<ReporteEncuestaAgrupadoDTO> ReporteEncuestaFinal = new List<ReporteEncuestaAgrupadoDTO>();
                ReporteEncuestaFinal = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteEncuestaFinalSincronico(Filtro);
                return ReporteEncuestaFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 23/01/2025
        /// Version 1.0
        /// <summary>
        /// Obtiene reporte de encuestas  de Docentes
        /// </summary>
        /// <param name="filtro">Filtros para generar reporte</param>
        /// <returns> List<ReporteEncuestasFinalSincronicoDTO> </returns>
        public List<ReporteEncuestasDocenteDTO>? GenerarReporteEncuestaDocente(ReporteEncuestaFiltroSincronicoDTO filtro)
        {
            try
            {
                List<ReporteEncuestasDocenteDTO> ReporteEncuestaDocente = new List<ReporteEncuestasDocenteDTO>();
                ReporteEncuestaDocente = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteEncuestaDocente(filtro);
                return ReporteEncuestaDocente;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioPorModalidad(filtroTestimonioDTO filtro)
        {
			try
			{
				List<ReportePGeneralTestimonio> ReporteEncuestaFinal = new List<ReportePGeneralTestimonio>();
                if (filtro.modalidad == 1)
                {
					ReporteEncuestaFinal = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteTestimonioSincronico(filtro);
				}
                else
                {
					ReporteEncuestaFinal = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteTestimonioASincronico(filtro);
				}
				return ReporteEncuestaFinal.OrderByDescending(r => r.IdPEspecifico).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

        public List<ComboDTO> ObtenerRespuestaEncuestaCombo(FiltroRespuestaCombo filtro)
        {
            return _unitOfWork.ReporteEncuestasSincronicoRepository.ObtenerRespuestaEncuestaCombo(filtro);
        }
        public bool GuardarTestimonio(TestimonioInsertarDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    // Extraer solo los IDs de la lista de respuestas
                    var idsListaRespuestasCompleto = dto.ListaRespuestas.Select(r => r.Id).ToList();
                    var idsListaRespuestas = dto.IdRespuesta.Select(r => r.Id).ToList();
                    // Encontrar coincidencias entre IdRespuesta y ListaRespuestas
                    var idsEncontrados = idsListaRespuestas.Where(id => idsListaRespuestasCompleto.Contains(id)).ToList();
                    var visible = true;
                    if (dto.VisiblePW == 0)
                    {
                        visible = false;
                    }
                    else
                    {
                        visible = true;
                    }
                    // Identificar los que no están en la lista de respuestas
                    var idsNoEncontrados = idsListaRespuestasCompleto.Except(idsEncontrados).ToList();

                    // Procesar los IDs encontrados
                    foreach (var id in idsEncontrados)
                    {
                        TestimonioEncuestaObtenerDTO valores = _unitOfWork.ReporteEncuestasSincronicoRepository.ObtenerDatosTestimonioSincronicoASinEstado(id, dto.Modalidad);
                        if (valores != null)
                        {
                            if (valores.Testimonio != dto.Testimonio || valores.VisiblePW != visible)
                            {
                                TestimonioEntidadActualizarDTO entidad = new()
                                {
                                    Id = valores.Id,
                                    Testimonio = dto.Testimonio,
                                    VisiblePW = dto.VisiblePW,
                                    Modalidad = dto.Modalidad

                                };
                                _unitOfWork.ReporteEncuestasSincronicoRepository.ActualizarTestimonio(entidad, usuario);
                            }
                            if (valores.Estado == false)
                            {
                                TestimonioEntidadActualizarDTO entidad = new()
                                {
                                    Id = valores.Id,
                                    Testimonio = dto.Testimonio,
                                    VisiblePW = dto.VisiblePW,
                                    Modalidad = dto.Modalidad

                                };
                                _unitOfWork.ReporteEncuestasSincronicoRepository.ActualizarEstadoTestimonioEstado(entidad, usuario);
                            }

                        }
                        if (valores == null && dto.Modalidad != null)
                        {
                            TestimonioEntidadDTO entidad = new()
                            {
                                IdRespuesta = id,
                                Testimonio = dto.Testimonio,
                                VisiblePW = dto.VisiblePW,
                                Modalidad = dto.Modalidad

                            };
                            _unitOfWork.ReporteEncuestasSincronicoRepository.GuardarTestimonio(entidad, usuario);
                        }
                    }
                    // Obtener datos de los IDs que no están en la lista de respuestas
                    foreach (var idNoEncontrado in idsNoEncontrados)
                    {
                        TestimonioEncuestaObtenerDTO valores = _unitOfWork.ReporteEncuestasSincronicoRepository.ObtenerDatosTestimonioSincronicoASinEstado(idNoEncontrado, dto.Modalidad);

                        if(valores != null)
                        {
                            if (valores.Estado != null && valores.Estado == true)
                            {
                                TestimonioEntidadActualizarDTO entidad = new()
                                {
                                    Id = valores.Id,
                                    Testimonio = dto.Testimonio,
                                    VisiblePW = dto.VisiblePW,
                                    Modalidad = dto.Modalidad

                                };
                                _unitOfWork.ReporteEncuestasSincronicoRepository.ActualizarEstadoTestimonioEstado(entidad, usuario);
                            }
                        }
                        
                    }

                    return true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public List<ReportePGeneralTestimonio>? GenerarReporteTestimonioASincronico(filtroTestimonioDTO filtro)
        {
            try
            {
                List<ReportePGeneralTestimonio> ReporteEncuestaFinal = new List<ReportePGeneralTestimonio>();
                ReporteEncuestaFinal = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteTestimonioASincronico(filtro);
                return ReporteEncuestaFinal.OrderByDescending(r => r.IdPEspecifico).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ReportePGeneralValoracion>? GenerarReporteValoracionTotal(filtroValoracionDTO filtro)
        {
            try
            {
                List<ReportePGeneralValoracion> ReporteEncuestaValoracion = new List<ReportePGeneralValoracion>();
                ReporteEncuestaValoracion = _unitOfWork.ReporteEncuestasSincronicoRepository.GenerarReporteValoracionTotal(filtro);
                return ReporteEncuestaValoracion.OrderByDescending(r => r.IdPEspecifico).ToList(); ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ActualizarValoracionVisiblePw(ValoracionesActualizarDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    if (dto.IdRespuestas != null)
                    {
                            foreach (var id in dto.IdRespuestas)
                            {
                                _unitOfWork.ReporteEncuestasSincronicoRepository.ActualizarVisibleValoracionEncuesta(id, dto.Modalidad,dto.VisiblePw, usuario);
                            }
                            return true;
                    }
                    else
                        throw new BadRequestException("Respuestas Enviadas Nulas");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
