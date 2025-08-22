using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ReporteEncuestaFinalService
    /// Autor: Jonathan Caipo
    /// Fecha: 03/05/2023
    /// Version 1.0
    /// <summary>
    /// Gestión general del Reporte Encuesta Final
    /// </summary>
    public class ReporteEncuestasService : IReporteEncuestasService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteEncuestasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/05/2023
        /// Version 1.0
        /// <summary>
        /// Genera el reporte de Encuesta Final
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="idExamenEncuesta"></param>
        /// <returns> Lista DTO - List<ReporteEncuestaFinalDTO> </returns>
        public List<AgrupadoEncuestasDTO>? GenerarReporte(ReporteEncuestasFiltroDTO dto, int idTipoEncuesta)
        {
            try
            {
                dto.FechaInicio = new DateTime(dto.FechaInicio.Year, dto.FechaInicio.Month, dto.FechaInicio.Day, 0, 0, 0);
                dto.FechaFin = new DateTime(dto.FechaFin.Year, dto.FechaFin.Month, dto.FechaFin.Day, 23, 59, 59);


                int idExamenEncuesta = _unitOfWork.ReporteEncuestasRepository.ObtenerIdExamenEncuesta(idTipoEncuesta,dto.Version);
                var datos = _unitOfWork.ReporteEncuestasRepository.ObtenerDatosEncuestas(dto, idExamenEncuesta).Distinct().ToList();
                var preguntas = _unitOfWork.ReporteEncuestasRepository.ObtenerPreguntasExamen(idExamenEncuesta);

                var agrupadoPreguntas = preguntas.GroupBy(e => new
                {
                    e.IdExamen,
                    e.IdPregunta,
                    e.EnunciadoPregunta,
                    e.NroOrden,
                    e.NombreTipoRespuesta,
                    e.IdTipoRespuesta,
                    e.IdTipoPregunta,
                    e.TipoPregunta,
                    e.Estado
                }).Select(e => new AgrupadoPreguntasDTO
                {
                    IdExamen = e.Key.IdExamen,
                    IdPregunta = e.Key.IdPregunta,
                    EnunciadoPregunta = e.Key.EnunciadoPregunta,
                    NroOrden = e.Key.NroOrden,
                    NombreTipoRespuesta = e.Key.NombreTipoRespuesta,
                    IdTipoRespuesta = e.Key.IdTipoRespuesta,
                    IdTipoPregunta = e.Key.IdTipoPregunta,
                    TipoPregunta = e.Key.TipoPregunta,
                    Estado = e.Key.Estado,
                    Respuestas = e.Select(f => new RespuestasAgrupadoPreguntasDTO
                    {
                        IdRespuesta = f.IdRespuesta,
                        EnunciadoRespuesta = f.EnunciadoRespuesta,
                        NroOrdenRespuesta = f.NroOrdenRespuesta,
                    }).ToList()
                }).ToList();

                var agrupados = datos.GroupBy(x => new
                {
                    x.IdAlumno,
                    x.Alumno,
                    x.CodigoMatricula,
                    x.IdPGeneralMatricula,
                    x.PGeneralMatricula,
                    x.IdCentroCostoMatricula,
                    x.CentroCostoMatricula,
                    x.IdPGeneralExamen,
                    x.PGeneralExamen,
                    x.IdPEspecificoExamen,
                    x.PEspecificoExamen,
                    x.CentroCostoExamen,
                    x.IdPersonal,
                    x.NombrePersonal,
                    x.Encuesta,
                    x.NombreDocente,
                    x.FechaCreacion

                }).Select(x => new AgrupadoEncuestasDTO
                {
                    IdAlumno = x.Key.IdAlumno,
                    Alumno = x.Key.Alumno,
                    CodigoMatricula = x.Key.CodigoMatricula,
                    IdPEspecificoExamen = x.Key.IdPEspecificoExamen,
                    PEspecificoExamen = x.Key.PEspecificoExamen,
                    IdPGeneralExamen = x.Key.IdPGeneralExamen,
                    PGeneralExamen = x.Key.PGeneralExamen,
                    CentroCostoExamen = x.Key.CentroCostoExamen,
                    IdCentroCostoMatricula = x.Key.IdCentroCostoMatricula,
                    CentroCostoMatricula = x.Key.CentroCostoMatricula,
                    IdCoordinador = x.Key.IdPersonal,
                    CoordinadorAcademico = x.Key.NombrePersonal,
                    IdPGeneralMatricula = x.Key.IdPGeneralMatricula,
                    PGeneralMatricula = x.Key.PGeneralMatricula,
                    Encuesta = x.Key.Encuesta,
                    NombreDocente = x.Key.NombreDocente,
                    FechaCreacion = x.Key.FechaCreacion,
                    Preguntas = x.GroupBy(s => new
                    {
                        s.IdPregunta,
                        s.EnunciadoPregunta,
                        s.NroOrden,
                        s.IdTipoPregunta,
                        s.IdTipoRespuesta,
                        s.NombreTipoRespuesta,
                    }).Select(y => new AgrupadoPreguntasEncuestasDTO
                    {
                        IdPregunta = y.Key.IdPregunta.Value,
                        EnunciadoPregunta = y.Key.EnunciadoPregunta,
                        NroOrden = y.Key.NroOrden,
                        IdTipoPregunta = y.Key.IdTipoPregunta,
                        IdTipoRespuesta = y.Key.IdTipoRespuesta,
                        NombreTipoRespuesta = y.Key.NombreTipoRespuesta,
                        Validado = true,
                        Respuestas = y.Select(z => new AgrupadoRespuestasDTO
                        {
                            IdRespuesta = z.IdRespuesta.Value,
                            EnunciadoRespuesta = z.EnunciadoRespuesta,
                            OrdenRespuesta = z.OrdenRespuesta,
                            TextoRespuesta = z.TextoRespuesta,
                            Validado = true
                        }).OrderBy(n => n.OrdenRespuesta).ToList(),
                    }).ToList()
                }).ToList();
                agrupados.ForEach((Action<AgrupadoEncuestasDTO>)(x =>
                {
                    x.Preguntas.ForEach(z =>
                    {
                        var pregunta = agrupadoPreguntas.FirstOrDefault(a => a.IdPregunta == z.IdPregunta)!;
                        var respuestaTemp = new List<AgrupadoRespuestasDTO>();
                        var contadorRespuesta = 0;
                        pregunta.Respuestas.OrderBy(n => n.NroOrdenRespuesta).ToList().ForEach(d =>
                        {
                            contadorRespuesta++;
                            var item = z.Respuestas.FirstOrDefault(l => l.IdRespuesta == d.IdRespuesta);
                            if(item != null)
                            {
                                item.OrdenRespuesta = contadorRespuesta;
                                respuestaTemp.Add(item);
                            }
                            else
                            {
                                respuestaTemp.Add(new AgrupadoRespuestasDTO
                                {
                                    IdRespuesta = d.IdRespuesta,
                                    EnunciadoRespuesta = d.EnunciadoRespuesta,
                                    OrdenRespuesta = contadorRespuesta,
                                    TextoRespuesta = null,
                                    Validado = false
                                });
                            }
                        });
                        z.Respuestas = respuestaTemp;
                    });
                    var buscarRestantes = agrupadoPreguntas.Where(s => !x.Preguntas.Any(y => y.IdPregunta == s.IdPregunta)).ToList();
                    var agrupadoPreguntasRestantes = buscarRestantes.Select(z => new AgrupadoPreguntasEncuestasDTO
                    {
                        IdPregunta = z.IdPregunta,
                        NroOrden = z.NroOrden,
                        EnunciadoPregunta = z.EnunciadoPregunta,
                        IdTipoPregunta = z.IdTipoPregunta,
                        IdTipoRespuesta = z.IdTipoRespuesta,
                        NombreTipoRespuesta = z.NombreTipoRespuesta,
                        Validado = false,
                        Respuestas = z.Respuestas.Select(z => new AgrupadoRespuestasDTO
                        {
                            IdRespuesta = z.IdRespuesta,
                            EnunciadoRespuesta = z.EnunciadoRespuesta,
                            OrdenRespuesta = z.NroOrdenRespuesta,
                            TextoRespuesta = null,
                            Validado = false
                        }).OrderBy(n => n.OrdenRespuesta).ToList()
                    }).ToList();
                    x.Preguntas.AddRange(agrupadoPreguntasRestantes);
                    x.Preguntas = x.Preguntas.OrderBy(n => n.NroOrden).ToList();
                }));
                return agrupados;
            }
            catch
            {
                throw;
            }
        }

        public List<VersionEncuestaDTO> ObtenerVersionEncuesta(int idTipoEncuesta)
        {
            return _unitOfWork.ReporteEncuestasRepository.ObtenerVersionEncuesta(idTipoEncuesta);
        }
    }
}
