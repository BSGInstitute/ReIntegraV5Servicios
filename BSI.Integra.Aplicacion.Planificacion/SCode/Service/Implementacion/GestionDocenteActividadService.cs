using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class GestionDocenteActividadService : IGestionDocenteActividadService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionDocenteActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                // 1. Cabecera
                var cabecera = new TGestionDocenteActividadCabecera
                {
                    IdGestionDocenteFlujo = dto.Cabecera.IdGestionDocenteFlujo,
                    Nombre = dto.Cabecera.Nombre,
                    Descripcion = dto.Cabecera.Descripcion,
                    IdGestionDocenteEstadoGeneral = dto.Cabecera.IdGestionDocenteEstadoGeneral,
                    IdGestionDocenteCategoria = dto.Cabecera.IdGestionDocenteCategoria,
                    Estado = true,
                    UsuarioCreacion = dto.Cabecera.Usuario,
                    UsuarioModificacion = dto.Cabecera.Usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                _unitOfWork.GestionDocenteActividadCabeceraRepository.Insert(cabecera);
                await _unitOfWork.CommitAsync(); // Necesario para el ID de cabecera

                // 2. Detalles y Disparadores
                foreach (var det in dto.Detalles)
                {
                    // 2.1 Procesar Disparador primero para obtener su ID
                    var disparador = new TGestionDocenteDetalleDisparador
                    {
                        IdGestionDocenteTipoDisparadorFlujo = det.Disparador.IdGestionDocenteTipoDisparadorFlujo,
                        Estado = true,
                        UsuarioCreacion = det.Usuario,
                        UsuarioModificacion = det.Usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };
                    _unitOfWork.GestionDocenteDetalleDisparadorRepository.Insert(disparador);
                    await _unitOfWork.CommitAsync();

                    // 2.2 Ocurrencias Previas del Disparador
                    if (det.Disparador.IdsOcurrenciasPrevias != null)
                    {
                        foreach (var idOcuPrevia in det.Disparador.IdsOcurrenciasPrevias)
                        {
                            var ocuPrevia = new TGestionDocenteDetalleDisparadorOcurrencia
                            {
                                IdGestionDocenteDetalleDisparador = disparador.Id,
                                IdGestionDocenteOcurrenciaPrevia = idOcuPrevia,
                                Estado = true,
                                UsuarioCreacion = det.Usuario,
                                UsuarioModificacion = det.Usuario,
                                FechaCreacion = fechaActual,
                                FechaModificacion = fechaActual
                            };
                            _unitOfWork.GestionDocenteDetalleDisparadorOcurrenciaRepository.Insert(ocuPrevia);
                        }
                    }

                    // 2.3 Detalle Actividad
                    var detalle = new TGestionDocenteActividadDetalle
                    {
                        IdGestionDocenteActividadCabecera = cabecera.Id,
                        IdGestionDocenteTipoActividadDetalle = det.IdGestionDocenteTipoActividadDetalle,
                        IdPlantillaMediaComunicacion = det.IdPlantillaMediaComunicacion,
                        IdGestionDocenteDetalleDisparador = disparador.Id,
                        Nombre = det.Nombre,
                        EstadoActividad = det.Estado,
                        Estado = true,
                        UsuarioCreacion = det.Usuario,
                        UsuarioModificacion = det.Usuario,
                        FechaCreacion = fechaActual,
                        FechaModificacion = fechaActual
                    };
                    _unitOfWork.GestionDocenteActividadDetalleRepository.Insert(detalle);
                    await _unitOfWork.CommitAsync();

                    // 3. Ocurrencias (Asociadas a este detalle)
                    var ocurrenciasDetalle = dto.Ocurrencias.Where(o => o.IdGestionDocenteActividadDetalle == 0);
                }

                await _unitOfWork.CommitAsync();

                return cabecera.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
