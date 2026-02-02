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

        public async Task<int> InsertarDetalleAsync(GestionDocenteActividadDetalleDTO dto)
        {
            try
            {
                DateTime fechaActual = DateTime.Now;

                // 1. Crear el Disparador Detalle
                var gestionDocenteDisparadorDetalle = new GestionDocenteDisparadorDetalle
                {
                    IdGestionDocenteDisparadorFlujoTipo = dto.IdGestionDocenteDisparadorFlujoTipo,
                    Estado = true,
                    UsuarioCreacion = dto.Usuario,
                    UsuarioModificacion = dto.Usuario,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };
                var disparadorModel = _unitOfWork.GestionDocenteDisparadorDetalleRepository.Add(gestionDocenteDisparadorDetalle);
                await _unitOfWork.CommitAsync();

                // 2. Procesar según el tipo de disparador
                await ProcesarTipoDisparadorAsync(dto, disparadorModel.Id, dto.Usuario, fechaActual);

                // 3. Crear Detalle de Actividad
                var gestionDocenteActividadDetalle = new GestionDocenteActividadDetalle
                {
                    IdGestionDocenteActividadCabecera = dto.IdGestionDocenteActividadCabecera,
                    IdGestionDocenteActividadDetalleTipo = dto.IdGestionDocenteActividadDetalleTipo,
                    IdPlantillaMedioComunicacion = dto.IdPlantillaMedioComunicacion,
                    IdGestionDocenteDisparadorDetalle = disparadorModel.Id,
                    Nombre = dto.Nombre,
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

        /// <summary>
        /// Procesa el tipo de disparador y crea los registros correspondientes según el tipo:
        /// 1 = Primera Actividad (Tiempo Fijo)
        /// 2 = Basado en Ocurrencia Anterior
        /// 3 = Basado en Cronograma
        /// </summary>
        private async Task ProcesarTipoDisparadorAsync(GestionDocenteActividadDetalleDTO dto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            switch (dto.IdGestionDocenteDisparadorFlujoTipo)
            {
                case 1: // Primera Actividad (Tiempo Fijo)
                    await ProcesarDisparadorTiempoFijoAsync(dto, idDisparadorDetalle, usuario, fechaActual);
                    break;

                case 2: // Basado en Ocurrencia Anterior
                    await ProcesarDisparadorOcurrenciaAnteriorAsync(dto, idDisparadorDetalle, usuario, fechaActual);
                    break;

                case 3: // Basado en Cronograma
                    await ProcesarDisparadorCronogramaAsync(dto, idDisparadorDetalle, usuario, fechaActual);
                    break;

                default:
                    throw new ArgumentException($"Tipo de disparador no válido: {dto.IdGestionDocenteDisparadorFlujoTipo}");
            }
        }

        /// <summary>
        /// Tipo 1: Primera Actividad - Tiempo Fijo
        /// Guarda fecha y hora específica en T_GestionDocenteDisparadorReglaTiempoFijo
        /// </summary>
        private async Task ProcesarDisparadorTiempoFijoAsync(GestionDocenteActividadDetalleDTO dto, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (!dto.Fecha.HasValue)
                throw new ArgumentException("La fecha es requerida para el disparador de tipo Primera Actividad (Tiempo Fijo)");

            var fechaFinal = dto.Fecha.Value.Date + (dto.Hora ?? TimeSpan.Zero);

            var reglaFija = new GestionDocenteDisparadorReglaTiempoFijo
            {
                IdGestionDocenteDisparadorReglaTiempo = 1, // Tipo Fijo
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Fecha = fechaFinal,
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
        private async Task ProcesarDisparadorOcurrenciaAnteriorAsync(GestionDocenteDisparadorDetalleDTO disparador, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (!disparador.CantidadTiempo.HasValue || !disparador.IdGestionDocenteUnidadTiempo.HasValue)
                throw new ArgumentException("La cantidad y unidad de tiempo son requeridas para el disparador Basado en Ocurrencia Anterior");

            if (!disparador.IdOcurrenciaActividadAnterior.HasValue)
                throw new ArgumentException("La ocurrencia anterior es requerida para el disparador Basado en Ocurrencia Anterior");

            // Crear regla de tiempo relativo
            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
            {
                IdGestionDocenteDisparadorReglaTiempo = 2, // Tipo Relativo
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Cantidad = disparador.CantidadTiempo.Value,
                IdGestionDocenteUnidadTiempo = disparador.IdGestionDocenteUnidadTiempo.Value,
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
                IdGestionDocenteOcurrenciaPrevia = disparador.IdOcurrenciaActividadAnterior.Value,
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
        private async Task ProcesarDisparadorCronogramaAsync(GestionDocenteDisparadorDetalleDTO disparador, int idDisparadorDetalle, string usuario, DateTime fechaActual)
        {
            if (!disparador.CantidadTiempo.HasValue || !disparador.IdGestionDocenteUnidadTiempo.HasValue)
                throw new ArgumentException("La cantidad y unidad de tiempo son requeridas para el disparador Basado en Cronograma");

            if (!disparador.IdGestionDocenteReferenciaTiempo.HasValue)
                throw new ArgumentException("La referencia de tiempo (antes/después) es requerida para el disparador Basado en Cronograma");

            // Crear regla de tiempo relativo
            var reglaRelativa = new GestionDocenteDisparadorReglaTiempoRelativo
            {
                IdGestionDocenteDisparadorReglaTiempo = 2, // Tipo Relativo
                IdGestionDocenteDisparadorDetalle = idDisparadorDetalle,
                Cantidad = disparador.CantidadTiempo.Value,
                IdGestionDocenteUnidadTiempo = disparador.IdGestionDocenteUnidadTiempo.Value,
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
                IdGestionDocenteReferenciaTiempo = disparador.IdGestionDocenteReferenciaTiempo.Value,
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
                foreach (var detDto in dto.Detalles)
                {
                    detDto.IdGestionDocenteActividadCabecera = idCabecera;
                    int idDetalle = await InsertarDetalleAsync(detDto);

                    // 3. Insertar Ocurrencias asociadas a este detalle
                    var ocurrenciasAsociadas = dto.Ocurrencias.Where(o => o.IdGestionDocenteActividadDetalle == detDto.Id);
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
