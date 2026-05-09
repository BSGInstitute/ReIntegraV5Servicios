using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Globalization;
using System.Linq;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: HistorialOportunidadService
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Construye el historial de oportunidades del alumno, transformando la
    /// proyección plana del repositorio en la estructura jerárquica por canal.
    /// </summary>
    public class HistorialOportunidadService : IHistorialOportunidadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HistorialOportunidadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public HistorialOportunidadAlumnoDTO ObtenerHistorialPorIdAlumno(int idAlumno)
        {
            try
            {
                if (idAlumno <= 0)
                    throw new BadRequestException("El IdAlumno es requerido y debe ser mayor a 0.");

                var planos = _unitOfWork.OportunidadRepository.ObtenerHistorialOportunidadesAlumno(idAlumno);

                return new HistorialOportunidadAlumnoDTO
                {
                    IdAlumno = idAlumno,
                    HistorialOportunidades = planos.Select(p => new OportunidadHistorialV2DTO
                    {
                        IdOportunidad = p.IdOportunidad,
                        FechaCreacion = p.FechaCreacion.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                        FaseMaxima = p.FaseMaxima ?? string.Empty,
                        FaseCierre = p.FaseCierre ?? string.Empty,
                        Interacciones = new InteraccionesOportunidadDTO
                        {
                            Llamadas = new LlamadasInteraccionDTO
                            {
                                Ejecutadas = p.LlamadasEjecutadas,
                                NoEjecutadas = p.LlamadasNoEjecutadas,
                                Manual = p.LlamadasManual
                            },
                            Whatsapp = new WhatsappInteraccionDTO
                            {
                                MensajesUsuario = p.WhatsappMensajesUsuario
                            },
                            Correo = new CorreoInteraccionDTO
                            {
                                CorreosUsuario = p.CorreoCorreosUsuario
                            },
                            PortalWeb = new PortalWebInteraccionDTO
                            {
                                MensajesUsuario = p.PortalWebMensajesUsuario
                            }
                        }
                    }).ToList()
                };
            }
            catch (BadRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerHistorialPorIdAlumno: {ex.Message}", ex);
            }
        }
    }
}
