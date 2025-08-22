using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PEspecificoPadreFrecuenciaService
    /// Autor: Giancarlo Romero
    /// Fecha: 30/05/2022
    /// <summary>
    /// Gestión general de PEspecificoPadreFrecuenciaController
    /// </summary>
    public class PEspecificoPadreFrecuenciaService : IPEspecificoPadreFrecuenciaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PEspecificoPadreFrecuenciaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoPadreFrecuencium, PespecificoPadreFrecuencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoPadreFrecuenciaDTO, PespecificoPadreFrecuencia>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoPadreFrecuenciaDTO, TPespecificoPadreFrecuencium>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 05/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista con las frecuencias del programa padre
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns> DTO - PEspecificoPadreFrecuenciaDTO </returns>
        public PEspecificoPadreFrecuenciaDTO ObtenerPorIdPespecifico(int idPEspecifico)
        {
            try
            {
                var data = _unitOfWork.PespecificoPadreFrecuenciaRepository.ObtenerPorIdPespecifico(idPEspecifico);
                if (data != null && data.Id != 0)
                {
                    PEspecificoPadreFrecuenciaDTO rpta = new PEspecificoPadreFrecuenciaDTO
                    {
                        Id = data.Id,
                        IdFrecuencia = data.IdFrecuencia,
                        IdPespecifico = data.IdPespecifico,
                        IdTiempoFrecuencia = data.IdTiempoFrecuencia,
                        Nota = data.Nota,
                        Sesiones = _unitOfWork.PEspecificoPadreFrecuenciaSesionRepository.ObtenerTodoPorPEspecificoPadreFrecuencia(data.Id).ToList(),
                    };
                    return rpta;
                }
                else
                    return null;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Inserta una lista con las frecuencias del programa padre
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool Insertar(PEspecificoPadreFrecuenciaDTO dto, string usuario)
        {
            try
            {
                var pEspecificoPadreFrecuenciaNuevo = new PespecificoPadreFrecuencia()
                {
                    IdPespecifico = dto.IdPespecifico,
                    IdFrecuencia = dto.IdFrecuencia,
                    IdTiempoFrecuencia = dto.IdTiempoFrecuencia,
                    Nota = dto.Nota,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                //añade la lista de detalles
                if (dto.Sesiones != null && dto.Sesiones.Count > 0)
                {
                    pEspecificoPadreFrecuenciaNuevo.PespecificoPadreFrecuenciaSesions = dto.Sesiones.Select(s =>
                        new PespecificoPadreFrecuenciaSesion()
                        {
                            Sesion = s.Sesion,
                            IdDiaSemana = s.IdDiaSemana,
                            HoraInicio = s.HoraInicio,
                            HoraFin = s.HoraFin,
                            Duracion = s.Duracion,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        }).ToList();
                }
                _unitOfWork.PespecificoPadreFrecuenciaRepository.Add(pEspecificoPadreFrecuenciaNuevo);
                _unitOfWork.Commit();

                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una lista con las frecuencias del programa padre
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool Actualizar(PEspecificoPadreFrecuenciaDTO dto, string usuario)
        {
            try
            {
                if (!_unitOfWork.PespecificoPadreFrecuenciaRepository.Exist(dto.Id))
                {
                    throw new BadRequestException("No existe pespecifico");
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    var frecuenciaExistente = _unitOfWork.PespecificoPadreFrecuenciaRepository.ObtenerPorId(dto.Id);
                    if (frecuenciaExistente != null && frecuenciaExistente.Id != 0)
                    {
                        frecuenciaExistente.IdPespecifico = dto.IdPespecifico;
                        frecuenciaExistente.IdFrecuencia = dto.IdFrecuencia;
                        frecuenciaExistente.IdTiempoFrecuencia = dto.IdTiempoFrecuencia;
                        frecuenciaExistente.Nota = dto.Nota;
                        frecuenciaExistente.UsuarioModificacion = usuario;
                        frecuenciaExistente.FechaModificacion = DateTime.Now;

                        //añade la lista de detalles
                        if (dto.Sesiones != null && dto.Sesiones.Count > 0)
                        {
                            var inserts = dto.Sesiones.Where(x => x.Id == 0).Select(s => new PespecificoPadreFrecuenciaSesion()
                            {
                                IdPespecificoPadreFrecuencia = dto.Id,
                                Sesion = s.Sesion,
                                IdDiaSemana = s.IdDiaSemana,
                                HoraInicio = s.HoraInicio,
                                HoraFin = s.HoraFin,
                                Duracion = s.Duracion,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }).ToList();
                            var updates = dto.Sesiones.Where(x => x.Delete == false && x.Id > 0);
                            var deletes = dto.Sesiones.Where(x => x.Delete == true && x.Id > 0).Select(x => x.Id);

                            if (inserts.Count() > 0)
                            {
                                _unitOfWork.PEspecificoPadreFrecuenciaSesionRepository.Add(inserts);
                                _unitOfWork.Commit();
                            }
                            if (updates.Count() > 0)
                            {
                                var actualizar = new List<PespecificoPadreFrecuenciaSesion>();
                                foreach (var update in updates)
                                {
                                    var sesionExistente = _unitOfWork.PEspecificoPadreFrecuenciaSesionRepository.ObtenerPorId(update.Id);
                                    if (sesionExistente != null && sesionExistente.Id != 0)
                                    {
                                        sesionExistente.Sesion = update.Sesion;
                                        sesionExistente.IdDiaSemana = update.IdDiaSemana;
                                        sesionExistente.HoraInicio = update.HoraInicio;
                                        sesionExistente.HoraFin = update.HoraFin;
                                        sesionExistente.Duracion = update.Duracion;
                                        sesionExistente.UsuarioModificacion = usuario;
                                        sesionExistente.FechaModificacion = DateTime.Now;
                                        actualizar.Add(sesionExistente);
                                    }
                                }
                                _unitOfWork.PEspecificoPadreFrecuenciaSesionRepository.Update(actualizar);
                                _unitOfWork.Commit();
                            }
                            if (deletes.Count() > 0)
                            {
                                _unitOfWork.PEspecificoPadreFrecuenciaSesionRepository.Delete(deletes, usuario);
                                _unitOfWork.Commit();
                            }
                        }
                        _unitOfWork.PespecificoPadreFrecuenciaRepository.Update(frecuenciaExistente);
                        _unitOfWork.Commit();

                        scope.Complete();
                    }
                    else
                        return false;
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
