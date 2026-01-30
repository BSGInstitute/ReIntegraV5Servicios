using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
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

        public async Task<int> InsertarCabeceraAsync(GestionDocenteActividadCabeceraDTO dto)
        {
            try
            {
                var gestionDocenteActividadCabecera = new GestionDocenteActividadCabecera
                {
                    IdGestionDocenteFlujo = dto.IdGestionDocenteFlujo,
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteEstado = dto.IdGestionDocenteEstado,
                    IdGestionDocenteCategoria = dto.IdGestionDocenteCategoria,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var model = _unitOfWork.GestionDocenteActividadCabeceraRepository.Add(gestionDocenteActividadCabecera);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertarDetalleAsync(int idCabecera, GestionDocenteActividadDetalleDTO dto)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                // 1. Procesar Disparador
                var gestionDocenteDetalleDisparador = new GestionDocenteDetalleDisparador
                {
                    IdGestionDocenteTipoDisparadorFlujo = dto.Disparador.IdGestionDocenteTipoDisparadorFlujo,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var disparadorModel = _unitOfWork.GestionDocenteDetalleDisparadorRepository.Add(gestionDocenteDetalleDisparador);
                await _unitOfWork.CommitAsync();

                // 2. Ocurrencias Previas del Disparador
                if (dto.Disparador.IdsOcurrenciasPrevias != null)
                {
                    foreach (var idOcuPrevia in dto.Disparador.IdsOcurrenciasPrevias)
                    {
                        var gestionDocenteDetalleDisparadorOcurrencia = new GestionDocenteDetalleDisparadorOcurrencia
                        {
                            IdGestionDocenteDetalleDisparador = disparadorModel.Id,
                            IdGestionDocenteOcurrenciaPrevia = idOcuPrevia,
                            Estado = true,
                            UsuarioCreacion = dto.Usuario,
                            UsuarioModificacion = dto.Usuario,
                            FechaCreacion = fechaActual,
                            FechaModificacion = fechaActual
                        };
                        _unitOfWork.GestionDocenteDetalleDisparadorOcurrenciaRepository.Add(gestionDocenteDetalleDisparadorOcurrencia);
                    }
                }

                // 3. Crear Detalle
                var gestionDocenteActividadDetalle = new GestionDocenteActividadDetalle
                {
                    IdGestionDocenteActividadCabecera = idCabecera,
                    IdGestionDocenteTipoActividadDetalle = dto.IdGestionDocenteTipoActividadDetalle,
                    IdPlantillaMediaComunicacion = dto.IdPlantillaMediaComunicacion,
                    IdGestionDocenteDetalleDisparador = disparadorModel.Id,
                    Nombre = dto.Nombre,
                    EstadoActividad = dto.Estado,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                var model = _unitOfWork.GestionDocenteActividadDetalleRepository.Add(gestionDocenteActividadDetalle);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> InsertarOcurrenciaAsync(int idDetalle, GestionDocenteOcurrenciaDTO dto)
        {
            try
            {
                var gestionDocenteOcurrencia = new GestionDocenteOcurrencia
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteOcurrenciaTipo = dto.IdGestionDocenteTipoOcurrencia,
                    IdGestionDocenteActividadDetalle = idDetalle,
                    IdGestionDocenteModoMarcado = dto.IdGestionDocenteModoMarcado,
                    RequiereComentario = dto.RequiereComentario,
                    RequiereFechaHora = dto.RequiereFechaHora,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var model = _unitOfWork.GestionDocenteOcurrenciaRepository.Add(gestionDocenteOcurrencia);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> ProcesarMaestroActividadAsync(MaestroGestionDocenteActividadDTO dto)
        {
            try
            {
                // 1. Insertar Cabecera
                int idCabecera = await InsertarCabeceraAsync(dto.Cabecera);

                // 2. Insertar Detalles
                foreach (var detDto in dto.Detalles)
                {
                    int idDetalle = await InsertarDetalleAsync(idCabecera, detDto);

                    // 3. Insertar Ocurrencias asociadas a este detalle
                    var ocurrenciasAsociadas = dto.Ocurrencias.Where(o => o.IdGestionDocenteActividadDetalle == detDto.Id);
                    foreach (var ocuDto in ocurrenciasAsociadas)
                    {
                        await InsertarOcurrenciaAsync(idDetalle, ocuDto);
                    }
                }

                return idCabecera;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
