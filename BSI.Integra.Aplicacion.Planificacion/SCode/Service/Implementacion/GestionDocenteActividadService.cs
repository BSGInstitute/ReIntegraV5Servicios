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

        public async Task<int> InsertarDetalleAsync(InsertarActividadDetalleRequestDTO request)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;
                string usuario = request.Detalle.Usuario;

                // 1. Crear el Disparador Detalle
                var gestionDocenteDisparadorDetalle = new GestionDocenteDisparadorDetalle
                {
                    IdGestionDocenteDisparadorFlujoTipo = request.Disparador.IdGestionDocenteDisparadorFlujoTipo,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var disparadorModel = _unitOfWork.GestionDocenteDisparadorDetalleRepository.Add(gestionDocenteDisparadorDetalle);
                await _unitOfWork.CommitAsync();

                // 2. Procesar según el tipo de disparador
                await ProcesarTipoDisparadorAsync(request, disparadorModel.Id, usuario, fechaActual);

                // 3. Crear Detalle de Actividad
                var gestionDocenteActividadDetalle = new GestionDocenteActividadDetalle
                {
                    IdGestionDocenteActividadCabecera = request.Detalle.IdGestionDocenteActividadCabecera,
                    IdGestionDocenteActividadDetalleTipo = request.Detalle.IdGestionDocenteActividadDetalleTipo,
                    IdPlantillaMedioComunicacion = request.Detalle.IdPlantillaMedioComunicacion,
                    IdGestionDocenteDisparadorDetalle = disparadorModel.Id,
                    Nombre = request.Detalle.Nombre,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
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

        /// <summary>
        /// Procesa el tipo de disparador y crea los registros correspondientes según el tipo:
        /// 1 = Primera Actividad (Tiempo Fijo)
        /// 2 = Basado en Ocurrencia Anterior
        /// 3 = Basado en Cronograma
        /// </summary>
        private async Task ProcesarTipoDisparadorAsync(InsertarActividadDetalleRequestDTO request, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            switch (request.Disparador.IdGestionDocenteDisparadorFlujoTipo)
            {
                case 1: // Primera Actividad (Tiempo Fijo)
                    await ProcesarDisparadorTiempoFijoAsync(request.ReglaTiempoFijo, idDisparadorDetalle, usuario, fechaActual);
                    break;

                case 2: // Basado en Ocurrencia Anterior
                    await ProcesarDisparadorOcurrenciaAnteriorAsync(request.ReglaTiempoRelativo, request.OcurrenciaDetalle, idDisparadorDetalle, usuario, fechaActual);
                    break;

                case 3: // Basado en Cronograma
                    await ProcesarDisparadorCronogramaAsync(request.ReglaTiempoRelativo, request.ReferenciaRelativa, idDisparadorDetalle, usuario, fechaActual);
                    break;

                default:
                    throw new ArgumentException($"Tipo de disparador no válido: {request.Disparador.IdGestionDocenteDisparadorFlujoTipo}");
            }
        }

        /// <summary>
        /// Tipo 1: Primera Actividad - Tiempo Fijo
        /// Guarda fecha específica en T_GestionDocenteDisparadorReglaTiempoFijo
        /// </summary>
        private async Task ProcesarDisparadorTiempoFijoAsync(GestionDocenteDisparadorReglaTiempoFijoDTO dto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (dto == null)
                throw new ArgumentException("La regla de tiempo fijo es requerida para el disparador de tipo Primera Actividad");

            var reglaFija = new GestionDocenteDisparadorReglaTiempoFijo
            {
                IdGestionDocenteDisparadorReglaTiempo = dto.IdGestionDocenteDisparadorReglaTiempo,
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Fecha = dto.Fecha,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorReglaTiempoFijoRepository.Add(reglaFija);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Tipo 2: Basado en Ocurrencia Anterior
        /// Guarda cantidad y unidad de tiempo en T_GestionDocenteDisparadorReglaTiempoRelativo
        /// y la ocurrencia anterior en T_GestionDocenteDisparadorOcurrenciaDetalle
        /// </summary>
        private async Task ProcesarDisparadorOcurrenciaAnteriorAsync(GestionDocenteDisparadorReglaTiempoRelativoDTO reglaDto, GestionDocenteDisparadorOcurrenciaDetalleDTO ocurrenciaDto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (reglaDto == null)
                throw new ArgumentException("La regla de tiempo relativo es requerida para el disparador Basado en Ocurrencia Anterior");

            if (ocurrenciaDto == null)
                throw new ArgumentException("La ocurrencia anterior es requerida para el disparador Basado en Ocurrencia Anterior");

            // Crear regla de tiempo relativo
            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
            {
                IdGestionDocenteDisparadorReglaTiempo = reglaDto.IdGestionDocenteDisparadorReglaTiempo,
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Cantidad = reglaDto.Cantidad,
                IdGestionDocenteUnidadTiempo = reglaDto.IdGestionDocenteUnidadTiempo,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);

            // Crear referencia a ocurrencia anterior
            var disparadorOcurrencia = new GestionDocenteDisparadorOcurrenciaDetalle
            {
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                IdGestionDocenteOcurrenciaPrevia = ocurrenciaDto.IdGestionDocenteOcurrenciaPrevia,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorOcurrenciaDetalleRepository.Add(disparadorOcurrencia);
            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Tipo 3: Basado en Cronograma
        /// Guarda cantidad y unidad de tiempo en T_GestionDocenteDisparadorReglaTiempoRelativo
        /// y la referencia de tiempo (antes/después de sesión) en T_GestionDocenteDisparadorReglaTiempoRelativoReferencia
        /// </summary>
        private async Task ProcesarDisparadorCronogramaAsync(GestionDocenteDisparadorReglaTiempoRelativoDTO reglaDto, GestionDocenteDisparadorReglaTiempoRelativoReferenciaDTO referenciaDto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (reglaDto == null)
                throw new ArgumentException("La regla de tiempo relativo es requerida para el disparador Basado en Cronograma");

            if (referenciaDto == null)
                throw new ArgumentException("La referencia de tiempo (antes/después) es requerida para el disparador Basado en Cronograma");

            // Crear regla de tiempo relativo
            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
            {
                IdGestionDocenteDisparadorReglaTiempo = reglaDto.IdGestionDocenteDisparadorReglaTiempo,
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Cantidad = reglaDto.Cantidad,
                IdGestionDocenteUnidadTiempo = reglaDto.IdGestionDocenteUnidadTiempo,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            var reglaRelativaModel = _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoRepository.Add(reglaRelativa);
            await _unitOfWork.CommitAsync();

            // Crear referencia de tiempo (antes/después de sesión)
            var referenciaRelativa = new GestionDocenteDisparadorReglaTiempoRelativoReferencia
            {
                IdGestionDocenteDisparadorReglaTiempoRelativo = reglaRelativaModel.Id,
                IdGestionDocenteReferenciaTiempo = referenciaDto.IdGestionDocenteReferenciaTiempo,
                Estado = true,
                UsuarioCreacion = usuario,
                UsuarioModificacion = usuario,
                FechaCreacion = fechaActual,
                FechaModificacion = fechaActual
            };

            _unitOfWork.GestionDocenteDisparadorReglaTiempoRelativoReferenciaRepository.Add(referenciaRelativa);
            await _unitOfWork.CommitAsync();
        }

        public async Task<int> InsertarOcurrenciaAsync(GestionDocenteOcurrenciaDTO dto)
        {
            try
            {
                var gestionDocenteOcurrencia = new GestionDocenteOcurrencia
                {
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    IdGestionDocenteOcurrenciaTipo = dto.IdGestionDocenteOcurrenciaTipo,
                    IdGestionDocenteActividadDetalle = dto.IdGestionDocenteActividadDetalle,
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
                foreach (var detRequest in dto.Detalles)
                {
                    detRequest.Detalle.IdGestionDocenteActividadCabecera = idCabecera;
                    int idDetalle = await InsertarDetalleAsync(detRequest);

                    // 3. Insertar Ocurrencias asociadas a este detalle
                    var ocurrenciasAsociadas = dto.Ocurrencias.Where(o => o.IdGestionDocenteActividadDetalle == detRequest.Detalle.Id);
                    foreach (var ocuDto in ocurrenciasAsociadas)
                    {
                        ocuDto.IdGestionDocenteActividadDetalle = idDetalle;
                        await InsertarOcurrenciaAsync(ocuDto);
                    }
                }

                return idCabecera;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AsociarActividadAFlujoAsync(GestionDocenteActividadCabeceraFlujoDTO dto)
        {
            try
            {
                var entidad = new GestionDocenteActividadCabeceraFlujo
                {
                    IdGestionDocenteFlujo = dto.IdGestionDocenteFlujo,
                    IdGestionDocenteActividadCabecera = dto.IdGestionDocenteActividadCabecera,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                var model = _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Add(entidad);
                await _unitOfWork.CommitAsync();

                return model.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DesasociarActividadDeFlujoAsync(int id, string usuario)
        {
            try
            {
                _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository.Delete(id, usuario);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GestionDocenteActividadCabeceraFlujoDTO>> ObtenerActividadesPorFlujoAsync(int idFlujo)
        {
            try
            {
                var lista = _unitOfWork.GestionDocenteActividadCabeceraFlujoRepository
                    .GetAll()
                    .Where(x => x.IdGestionDocenteFlujo == idFlujo && x.Estado)
                    .ToList();

                return lista.Select(x => new GestionDocenteActividadCabeceraFlujoDTO
                {
                    Id = x.Id,
                    IdGestionDocenteFlujo = x.IdGestionDocenteFlujo,
                    IdGestionDocenteActividadCabecera = x.IdGestionDocenteActividadCabecera,
                    Estado = x.Estado
                }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
