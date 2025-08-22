using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class FeedbackConfigurarService : IFeedbackConfigurarService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FeedbackConfigurarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TFeedbackConfigurar, FeedbackConfigurar>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TFeedbackConfigurar, FeedbackConfigurarDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<FeedbackConfigurar, FeedbackConfigurarDetalle>(MemberList.None).ReverseMap();
                    cfg.CreateMap<FeedbackConfigurarDTO, FeedbackConfigurarDetalle>(MemberList.None).ReverseMap();

                }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de FeedbackConfigurarFiltro
        /// </summary>
        /// <returns> Lista FeedbackConfigurarFiltroDTO </returns>
        public IEnumerable<FeedbackConfigurarFiltroDTO> Obtener()
        {
            return _unitOfWork.FeedbackConfigurarRepository.Obtener();
        }
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registro de Sexo
        /// </summary>
        /// <returns> Lista ComboDTO </returns>
        public IEnumerable<ComboDTO> ObtenerComboSexo()
        {
            return _unitOfWork.SexoRepository.ObtenerCombo();
        }
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo FeedbackConfigurar
        /// </summary>
        /// <param name="dto"> FeedbackConfigurar/param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>FeedbackConfigurarDTO</returns>
        public FeedbackConfigurarDTO Insertar(FeedbackConfigurarDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FeedbackConfigurar entidad = new FeedbackConfigurar()
                    {
                        Nombre = dto.Nombre,
                        IdFeedbackTipo = dto.IdFeedbackTipo,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                    };
                    if (dto.FeedbackConfigurarDetalles.Count() >= 1 && dto.FeedbackConfigurarDetalles != null)
                    {
                        entidad.FeedbackConfigurarDetalles = new List<FeedbackConfigurarDetalle>();
                        foreach (var item in dto.FeedbackConfigurarDetalles)
                        {
                            FeedbackConfigurarDetalle registro = new FeedbackConfigurarDetalle();
                            registro.IdFeedbackConfigurar = item.IdFeedbackConfigurar;
                            registro.NombreVideo = item.NombreVideo;
                            registro.IdSexo = item.IdSexo;
                            registro.Puntaje = item.Puntaje;
                            registro.Estado = true;
                            registro.UsuarioCreacion = usuario;
                            registro.UsuarioModificacion = usuario;
                            registro.FechaCreacion = DateTime.Now;
                            registro.FechaModificacion = DateTime.Now;
                            entidad.FeedbackConfigurarDetalles.Add(registro);

                        }
                    }
                    var respuesta = _unitOfWork.FeedbackConfigurarRepository.Add(entidad);
                    _unitOfWork.Commit();
                    var resultado = _mapper.Map<FeedbackConfigurarDTO>(respuesta);
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Modifica un FeedbackConfigurar
        /// </summary>
        /// <param name="dto">FeedbackConfigurar</param>
        /// <param name="usuario">Usuario Modificacion</param>
        /// <returns>FeedbackConfigurarDTO</returns>
        public FeedbackConfigurarDTO Actualizar(FeedbackConfigurarDTO dto, string usuario)
        {
            try
            {
                FeedbackConfigurar? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.FeedbackConfigurarRepository.ObtenerPorId(dto.Id.Value);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.Nombre = dto.Nombre;
                            entidad.IdFeedbackTipo = dto.IdFeedbackTipo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;


                            var listaDetalle = _unitOfWork.FeedbackConfigurarDetalleRepository.ObtenerDetallePorIdFeedbackConfigurar(entidad.Id).ToList();
                            if (listaDetalle != null && listaDetalle.Count() > 0)
                            {
                                if (dto.FeedbackConfigurarDetalles != null && dto.FeedbackConfigurarDetalles.Count() > 0)
                                {
                                    listaDetalle.RemoveAll(s => dto.FeedbackConfigurarDetalles.Any(x => x.Id == s.Id));
                                }
                                if (listaDetalle.Count() > 0)
                                {
                                    _unitOfWork.FeedbackConfigurarDetalleRepository.Delete(listaDetalle.Select(x => x.Id), usuario);
                                    _unitOfWork.Commit();
                                }

                            }
                            if (dto.FeedbackConfigurarDetalles != null && dto.FeedbackConfigurarDetalles.Count() > 0)
                            {
                                entidad.FeedbackConfigurarDetalles = new();
                                dto.FeedbackConfigurarDetalles.ForEach(feedbackDetalle =>
                                {
                                    FeedbackConfigurarDetalle feedbackConfigurarDetalle;
                                    if (feedbackDetalle.Id != 0 && _unitOfWork.FeedbackConfigurarDetalleRepository.Exist(feedbackDetalle.Id.Value))
                                    {
                                        feedbackConfigurarDetalle = _unitOfWork.FeedbackConfigurarDetalleRepository.ObtenerPorId(feedbackDetalle.Id.Value)!;


                                        feedbackConfigurarDetalle.Puntaje = feedbackDetalle.Puntaje;
                                        feedbackConfigurarDetalle.NombreVideo = feedbackDetalle.NombreVideo;
                                        feedbackConfigurarDetalle.IdSexo = feedbackDetalle.IdSexo;
                                        feedbackConfigurarDetalle.UsuarioModificacion = usuario;
                                        feedbackConfigurarDetalle.FechaModificacion = DateTime.Now;
                                    }
                                    else
                                    {
                                        feedbackConfigurarDetalle = new FeedbackConfigurarDetalle()
                                        {
                                            IdFeedbackConfigurar = entidad.Id,
                                            Puntaje = feedbackDetalle.Puntaje,
                                            IdSexo = feedbackDetalle.IdSexo,
                                            NombreVideo = feedbackDetalle.NombreVideo,
                                            Estado = true,
                                            UsuarioCreacion = usuario,
                                            UsuarioModificacion = usuario,
                                            FechaCreacion = DateTime.Now,
                                            FechaModificacion = DateTime.Now,
                                        };


                                    }

                                    entidad.FeedbackConfigurarDetalles.Add(feedbackConfigurarDetalle);

                                });
                                var respuesta = _unitOfWork.FeedbackConfigurarRepository.Update(entidad);
                                _unitOfWork.Commit();
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
        /// Autor: Marco Villanueva Torres
        /// Fecha: 29/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina el registro FeedbackConfigurar por id
        /// </summary>
        /// <param name="id">Id FeedbackConfigurar</param>
        /// <returns> true/false </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.FeedbackConfigurarRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.FeedbackConfigurarRepository.Delete(id, usuario);
                    var idFeedbackDetalle = _unitOfWork.FeedbackConfigurarDetalleRepository.ObtenerDetallePorIdFeedbackConfigurar(id).Select(x => x.Id);

                    if (idFeedbackDetalle != null && idFeedbackDetalle.Count() > 0)
                    {
                        _unitOfWork.FeedbackConfigurarDetalleRepository.Delete(idFeedbackDetalle, usuario);
                    }

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


    }

}
