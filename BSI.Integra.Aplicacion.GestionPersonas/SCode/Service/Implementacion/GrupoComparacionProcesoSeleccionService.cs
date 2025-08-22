using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.ViewEngines;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class GrupoComparacionProcesoSeleccionService : IGrupoComparacionProcesoSeleccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public GrupoComparacionProcesoSeleccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<GrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGrupoComparacionProcesoSeleccion, GrupoComparacionProcesoSeleccionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene todos los registros de GrupoComparacionProcesoSeleccion
        /// </summary>
        /// <returns> Lista GrupoComparacionProcesoSeleccionDTO </returns>
        public IEnumerable<GrupoComparacionProcesoSeleccionDTO> Obtener()
        {
            var detalle = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.ObtenerDetalle();
            var resultado = detalle.GroupBy(s => new
            {
                s.Id,
                s.Nombre
            }).Select(x => new GrupoComparacionProcesoSeleccionDTO
            {
                Id = x.Key.Id,
                Nombre = x.Key.Nombre,
                IdsPostulante = x.Where(s => s.IdPostulante != 0).Select(s => s.IdPostulante).Distinct().ToList(),
                IdsPuestoTrabajo = x.Where(s => s.IdPuestoTrabajo != 0).Select(s => s.IdPuestoTrabajo).Distinct().ToList(),
                IdsSedeTrabajo = x.Where(s => s.IdSedeTrabajo != 0).Select(s => s.IdSedeTrabajo).Distinct().ToList(),
            }).ToList();
            return resultado;
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo GrupoComparacionProcesoSeleccion
        /// </summary>
        /// <param name="dto">GrupoComparacionProcesoSeleccionDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>GrupoComparacionProcesoSeleccionDTO</returns>
        public GrupoComparacionProcesoSeleccionDTO Insertar(GrupoComparacionProcesoSeleccionDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        GrupoComparacionProcesoSeleccion entidad = new()
                        {
                            Nombre = dto.Nombre,
                            IdPuestoTrabajo = 1,
                            IdSedeTrabajo = 1,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                        };
                        var respuesta = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.Add(entidad);
                        _unitOfWork.Commit();
                        dto.Id = respuesta.Id;

                        if (dto.IdsPostulante != null && dto.IdsPostulante.Count() > 0)
                        {
                            var items = dto.IdsPostulante.Select(s => new PostulanteComparacion
                            {
                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                IdPostulante = s,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = usuario,
                                FechaModificacion = DateTime.Now,
                            }).ToList();
                            _unitOfWork.PostulanteComparacionRepository.Add(items);
                            _unitOfWork.Commit();
                        }
                        if (dto.IdsPuestoTrabajo != null && dto.IdsPuestoTrabajo.Count() > 0)
                        {
                            var items = dto.IdsPuestoTrabajo.Select(s => new PuestoTrabajoGrupoComparacion
                            {
                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                IdPuestoTrabajo = s,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = usuario,
                                FechaModificacion = DateTime.Now,
                            }).ToList();
                            _unitOfWork.PuestoTrabajoGrupoComparacionRepository.Add(items);
                            _unitOfWork.Commit();
                        }
                        if (dto.IdsPuestoTrabajo != null && dto.IdsPuestoTrabajo.Count() > 0)
                        {
                            var items = dto.IdsPuestoTrabajo.Select(s => new PuestoTrabajoGrupoComparacion
                            {
                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                IdPuestoTrabajo = s,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = usuario,
                                FechaModificacion = DateTime.Now,
                            }).ToList();
                            _unitOfWork.PuestoTrabajoGrupoComparacionRepository.Add(items);
                            _unitOfWork.Commit();
                        }
                        if (dto.IdsSedeTrabajo != null && dto.IdsSedeTrabajo.Count() > 0)
                        {
                            var items = dto.IdsSedeTrabajo.Select(s => new SedeTrabajoGrupoComparacion
                            {
                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                IdSedeTrabajo = s,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                FechaCreacion = DateTime.Now,
                                UsuarioModificacion = usuario,
                                FechaModificacion = DateTime.Now,
                            }).ToList();
                            _unitOfWork.SedeTrabajoGrupoComparacionRepository.Add(items);
                            _unitOfWork.Commit();
                        }
                        scope.Complete();
                    }
                    return dto;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="dto"> GrupoComparacionProcesoSeleccionDTO</param>
        /// <param name="usuario">Usuario Modificacion</param>
        public GrupoComparacionProcesoSeleccionDTO Actualizar(GrupoComparacionProcesoSeleccionDTO dto, string usuario)
        {
            try
            {
                GrupoComparacionProcesoSeleccion? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                entidad.Nombre = dto.Nombre;
                                entidad.IdPuestoTrabajo = 1;
                                entidad.IdSedeTrabajo = 1;
                                entidad.Estado = true;
                                entidad.UsuarioModificacion = usuario;
                                entidad.FechaModificacion = DateTime.Now;
                                var respuesta = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.Update(entidad);
                                _unitOfWork.Commit();

                                //Eliminar Postulantes
                                var postulantes = _unitOfWork.PostulanteComparacionRepository.GetBy(x => x.IdGrupoComparacionProcesoSeleccion == dto.Id).ToList();
                                if (dto.IdsPostulante != null && postulantes != null)
                                {
                                    postulantes.RemoveAll(s => dto.IdsPostulante.Any(x => x == s.IdPostulante));
                                }
                                if (postulantes != null && postulantes.Count() > 0)
                                {
                                    _unitOfWork.PostulanteComparacionRepository.Delete(postulantes.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }

                                if (dto.IdsPostulante != null && dto.IdsPostulante.Count() > 0)
                                {
                                    dto.IdsPostulante.ForEach(s =>
                                    {
                                        if (!_unitOfWork.PostulanteComparacionRepository.Exist(x => x.IdGrupoComparacionProcesoSeleccion == dto.Id
                                            && x.IdPostulante == s))
                                        {
                                            var entidad = new PostulanteComparacion
                                            {
                                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                                IdPostulante = s,
                                                Estado = true,
                                                UsuarioCreacion = usuario,
                                                FechaCreacion = DateTime.Now,
                                                UsuarioModificacion = usuario,
                                                FechaModificacion = DateTime.Now,
                                            };
                                            _unitOfWork.PostulanteComparacionRepository.Add(entidad);
                                            _unitOfWork.Commit();
                                        }
                                    });
                                }
                                //Eliminar PuestoTrabajo
                                var puestoTrabajos = _unitOfWork.PuestoTrabajoGrupoComparacionRepository.GetBy(x => x.IdGrupoComparacionProcesoSeleccion == dto.Id).ToList();
                                if (dto.IdsPuestoTrabajo != null && puestoTrabajos != null)
                                {
                                    puestoTrabajos.RemoveAll(s => dto.IdsPuestoTrabajo.Any(x => x == s.IdPuestoTrabajo));
                                }
                                if (puestoTrabajos != null && puestoTrabajos.Count() > 0)
                                {
                                    _unitOfWork.PuestoTrabajoGrupoComparacionRepository.Delete(puestoTrabajos.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                                if (dto.IdsPuestoTrabajo != null && dto.IdsPuestoTrabajo.Count() > 0)
                                {
                                    dto.IdsPuestoTrabajo.ForEach(s =>
                                    {
                                        if (!_unitOfWork.PuestoTrabajoGrupoComparacionRepository.Exist(x => x.IdGrupoComparacionProcesoSeleccion == dto.Id
                                            && x.IdPuestoTrabajo == s))
                                        {
                                            var entidad = new PuestoTrabajoGrupoComparacion
                                            {
                                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                                IdPuestoTrabajo = s,
                                                Estado = true,
                                                UsuarioCreacion = usuario,
                                                FechaCreacion = DateTime.Now,
                                                UsuarioModificacion = usuario,
                                                FechaModificacion = DateTime.Now,
                                            };
                                            _unitOfWork.PuestoTrabajoGrupoComparacionRepository.Add(entidad);
                                            _unitOfWork.Commit();
                                        }
                                    });
                                }
                                //Eliminar SedeTrabajo
                                var sedeTrabajos = _unitOfWork.SedeTrabajoGrupoComparacionRepository.GetBy(x => x.IdGrupoComparacionProcesoSeleccion == dto.Id).ToList();
                                if (dto.IdsSedeTrabajo != null && sedeTrabajos != null)
                                {
                                    sedeTrabajos.RemoveAll(s => dto.IdsSedeTrabajo.Any(x => x == s.IdSedeTrabajo));
                                }
                                if (sedeTrabajos != null && sedeTrabajos.Count() > 0)
                                {
                                    _unitOfWork.SedeTrabajoGrupoComparacionRepository.Delete(sedeTrabajos.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }
                                if (dto.IdsSedeTrabajo != null && dto.IdsSedeTrabajo.Count() > 0)
                                {
                                    dto.IdsSedeTrabajo.ForEach(s =>
                                    {
                                        if (!_unitOfWork.SedeTrabajoGrupoComparacionRepository.Exist(x => x.IdGrupoComparacionProcesoSeleccion == dto.Id
                                            && x.IdSedeTrabajo == s))
                                        {
                                            var entidad = new SedeTrabajoGrupoComparacion
                                            {
                                                IdGrupoComparacionProcesoSeleccion = dto.Id,
                                                IdSedeTrabajo = s,
                                                Estado = true,
                                                UsuarioCreacion = usuario,
                                                FechaCreacion = DateTime.Now,
                                                UsuarioModificacion = usuario,
                                                FechaModificacion = DateTime.Now,
                                            };
                                            _unitOfWork.SedeTrabajoGrupoComparacionRepository.Add(entidad);
                                            _unitOfWork.Commit();
                                        }
                                    });
                                }
                                scope.Complete();
                            }
                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.GrupoComparacionProcesoSeleccionRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <summary>
        /// Obtiene los combos para el modulos de GrupoComparacionProcesoSeleccion
        /// </summary>
        /// <returns> Lista de sedes y puestos de trabajo </returns>
        public (IEnumerable<ComboDTO> puestosTrabajo, IEnumerable<ComboDTO> sedesTrabajo) ObtenerCombosModulo()
        {
            var puestosTrabajo = new List<ComboDTO>();
            var sedesTrabajo = _unitOfWork.SedeTrabajoRepository.ObtenerCombo();
            return (puestosTrabajo, sedesTrabajo);
        }
    }
}
