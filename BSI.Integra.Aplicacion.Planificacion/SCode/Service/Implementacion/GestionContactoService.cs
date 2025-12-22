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

            //// 1. Validar Centro de Costo
            //if (!await _unitOfWork.GestionContactoRepository.ExisteCentroCostoAsync(dto.IdCentroCosto))
            //{
            //    throw new Exception($"El Centro de Costo con Id {dto.IdCentroCosto} no existe.");
            //}

            //// 2. Validar Asesor (Personal)
            //if (!await _unitOfWork.GestionContactoRepository.ExistePersonalAsync(dto.IdPersonal_Asignado))
            //{
            //    throw new Exception($"El Asesor con Id {dto.IdPersonal_Asignado} no existe.");
            //}

            //// 3. Validar Clasificación Persona
            //if (!await _unitOfWork.GestionContactoRepository.ExisteClasificacionPersonaAsync(dto.IdClasificacionPersona))
            //{
            //    throw new Exception($"La Clasificación de Persona con Id {dto.IdClasificacionPersona} no existe.");
            //}

            //// 4. Validar Fase Gestión
            //if (!await _unitOfWork.GestionContactoRepository.ExisteFaseGestionAsync(dto.IdFaseGestionContacto))
            //{
            //    throw new Exception($"La Fase de Gestión con Id {dto.IdFaseGestionContacto} no existe.");
            //}

            //// 5. Validar Origen
            //if (!await _unitOfWork.GestionContactoRepository.ExisteOrigenAsync(dto.IdOrigen))
            //{
            //    throw new Exception($"El Origen con Id {dto.IdOrigen} no existe.");
            //}

            //// Solo llegamos aquí si los IDs existen, así que es seguro consultar duplicidad(Centro Costo)
            //bool existeDuplicado = await _unitOfWork.GestionContactoRepository
            //                            .ExisteGestionActivaAsync(dto.IdClasificacionPersona, dto.IdCentroCosto);

            //if (existeDuplicado)
            //{
            //    throw new Exception("Regla de Negocio: El docente ya tiene una gestión activa para este Centro de Costo.");
            //}

            // Lanzamos las 5 tareas al mismo tiempo a la BD
            var t1 = _unitOfWork.GestionContactoRepository.ExisteCentroCostoAsync(dto.IdCentroCosto);
            var t2 = _unitOfWork.GestionContactoRepository.ExistePersonalAsync(dto.IdPersonal_Asignado);
            var t3 = _unitOfWork.GestionContactoRepository.ExisteClasificacionPersonaAsync(dto.IdClasificacionPersona);
            var t4 = _unitOfWork.GestionContactoRepository.ExisteFaseGestionAsync(dto.IdFaseGestionContacto);
            var t5 = _unitOfWork.GestionContactoRepository.ExisteOrigenAsync(dto.IdOrigen);

            // Esperamos a que todas terminen
            await Task.WhenAll(t1, t2, t3, t4, t5);

            // Verificamos resultados
            if (!await t1) throw new Exception($"El Centro de Costo {dto.IdCentroCosto} no existe.");
            if (!await t2) throw new Exception($"El Asesor {dto.IdPersonal_Asignado} no existe.");
            if (!await t3) throw new Exception($"La Clasificación {dto.IdClasificacionPersona} no existe.");
            if (!await t4) throw new Exception($"La Fase {dto.IdFaseGestionContacto} no existe.");
            if (!await t5) throw new Exception($"El Origen {dto.IdOrigen} no existe.");

            // Luego la regla de negocio...

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
