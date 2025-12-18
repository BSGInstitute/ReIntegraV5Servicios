using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Implementacion
{
    public class GestionContactoService : IGestionContactoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GestionContactoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor: Jose Vega
        /// Fecha: 18/12/2025
        /// Version: 1.0
        /// <summary>
        /// Orquesta la inserción de la Gestión de Contacto.
        /// </summary>
        public async Task<int> ProcesarInsercionGestionAsync(CrearGestionContactoDTO dto)
        {
            try
            {
                // 1. Configuración de Valores por Defecto
                bool estadoActivo = true;
                bool whatsAppDefault = false;//por definir
                int idActividadInicialDefault = 1;
                int idEstadoActividadPendiente = 1;

                DateTime fechaActual = DateTime.Now;

                // 2. Construcción de la Entidad Cabecera (T_GestionContacto)
                var nuevaGestion = new GestionContacto
                {
                    IdCentroCosto = dto.IdCentroCosto,
                    IdPersonalAsignado = dto.IdPersonal_Asignado,       // El Asesor
                    IdClasificacionPersona = dto.IdClasificacionPersona, // El Docente
                    IdFaseGestionContacto = dto.IdFaseGestionContacto,
                    IdOrigen = dto.IdOrigen,
                    IdEstadoGestionContacto = 1,
                    UltimoComentario = dto.Comentario,
                    EstadoSeguimientoWhatsApp = whatsAppDefault,
                    Estado = estadoActivo,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    UsuarioModificacion = dto.UsuarioCreacion,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                // 3. Inserción de Cabecera (Para obtener el ID)
                _unitOfWork.GestionContactoRepository.AddAsync(nuevaGestion);

                // Guardamos para materializar el ID de la nueva gestión
                await _unitOfWork.CommitAsync();

                // 4. Construcción del Log (T_GestionContactoLog)
                var nuevoLog = new GestionContactoLog
                {
                    IdGestionContacto = nuevaGestion.Id, // FK generada arriba
                    IdCentroCosto = dto.IdCentroCosto,
                    IdPersonalAsignado = dto.IdPersonal_Asignado,
                    IdClasificacionPersona = dto.IdClasificacionPersona,
                    IdFaseGestionContacto = dto.IdFaseGestionContacto,
                    IdOrigen = dto.IdOrigen,
                    IdEstadoGestionContacto = 1, // Mismo estado inicial
                    FechaLog = fechaActual,
                    Comentario = "Creación Inicial: " + dto.Comentario,
                    Estado = estadoActivo,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    UsuarioModificacion = dto.UsuarioCreacion,
                    FechaCreacion = fechaActual,
                    FechaModificacion = fechaActual
                };

                // 5. Construcción de Actividad Inicial (T_ActividadDetalleGestionContacto)
                var nuevaActividad = new ActividadDetalleGestionContacto
                {
                    IdGestionContacto = nuevaGestion.Id, // FK
                    IdActividadCabecera = idActividadInicialDefault,
                    FechaProgramada = fechaActual, // Programada para hoy
                    IdEstadoActividadDetalle = idEstadoActividadPendiente,
                    Comentario = "Gestión iniciada. Validar docente.",
                    Estado = estadoActivo,
                    UsuarioCreacion = dto.UsuarioCreacion,
                    FechaCreacion = fechaActual
                };

                // 6. Inserción de Detalle y Log
                _unitOfWork.GestionContactoLogRepository.AddAsync(nuevoLog);
                _unitOfWork.ActividadDetalleGestionContactoRepository.AddAsync(nuevaActividad);

                // 7. Commit Final
                await _unitOfWork.CommitAsync();

                return nuevaGestion.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
