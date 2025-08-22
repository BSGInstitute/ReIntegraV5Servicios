using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Base.Exceptions;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProgramaGeneralPresentacionArgumentoService : IProgramaGeneralPresentacionArgumentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ProgramaGeneralPresentacionArgumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralPresentacionArgumento, ProgramaGeneralPresentacionArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPresentacionArgumentoDTO, ProgramaGeneralPresentacionArgumento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TProgramaGeneralPresentacionArgumento, ProgramaGeneralPresentacionArgumentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO, ProgramaGeneralPresentacionArgumentoAgendaDTO>(MemberList.None).ReverseMap();



            });
            _mapper = new Mapper(config);
        }


        public IEnumerable<ProgramaGeneralPresentacionArgumentoDTO> Obtener()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ProgramaGeneralPresentacionArgumentoDTO Insertar(CompuestoPresentacionArgumentoModalidadDTO dto, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (_unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.Exist(dto.IdPresentacionArgumento))
                    {
                        var problema = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerPorId(dto.IdPresentacionArgumento)!;
                        problema.IdPgeneral = dto.IdPGeneral;
                        problema.Nombre = dto.NombrePresentacionArgumento;
                        problema.Descripcion = dto.DescripcionPresentacionArgumento;
                        problema.EsVisibleAgenda = dto.EsVisibleAgenda;
                        problema.UsuarioModificacion = usuario;
                        problema.FechaModificacion = DateTime.Now;

                        var listaBorrar = _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository
                            .GetBy(x => x.IdProgramaGeneralPresentacionArgumento == dto.IdPresentacionArgumento).ToList();
                        listaBorrar.RemoveAll(x => dto.PresentacionArgumento.Any(y => y.Id == x.Id));
                        if (listaBorrar != null && listaBorrar.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        var listaBorrar2 = _unitOfWork.ProgramaGeneralPresentacionArgumentoModalidadRepository.GetBy(x => x.IdProgramaGeneralPresentacionArgumento == dto.IdPresentacionArgumento).ToList();
                        listaBorrar2.RemoveAll(x => dto.Modalidades.Any(y => y.Id == x.IdModalidadCurso));
                        if (listaBorrar2 != null && listaBorrar2.Count() > 0)
                        {
                            _unitOfWork.ProgramaGeneralPresentacionArgumentoModalidadRepository.Delete(listaBorrar2.Select(x => x.Id), usuario);
                            _unitOfWork.Commit();
                        }

                        problema.ProgramaGeneralPresentacionArgumentoDetalleSolucion = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucion>();
                        foreach (var subItem in dto.PresentacionArgumento)
                        {
                            ProgramaGeneralPresentacionArgumentoDetalleSolucion detalleSolucion;
                            if (subItem.Id != null && _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository.Exist(subItem.Id.Value))
                            {
                                detalleSolucion = _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository.ObtenerPorId(subItem.Id.Value)!;
                                detalleSolucion.Detalle = subItem.Detalle;
                                detalleSolucion.Solucion = subItem.Solucion;
                                detalleSolucion.IdPgeneral = dto.IdPGeneral;
                                problema.UsuarioModificacion = usuario;
                                problema.FechaModificacion = DateTime.Now;
                            }
                            else
                            {
                                detalleSolucion = new();
                                detalleSolucion.Detalle = subItem.Detalle;
                                detalleSolucion.Solucion = subItem.Solucion;
                                detalleSolucion.IdPgeneral = dto.IdPGeneral;
                                detalleSolucion.UsuarioCreacion = usuario;
                                detalleSolucion.UsuarioModificacion = usuario;
                                detalleSolucion.FechaCreacion = DateTime.Now;
                                detalleSolucion.FechaModificacion = DateTime.Now;
                                detalleSolucion.Estado = true;
                            }
                            problema.ProgramaGeneralPresentacionArgumentoDetalleSolucion.Add(detalleSolucion);
                            _unitOfWork.Commit();
                        }
                        problema.ProgramaGeneralPresentacionArgumentoModalidad = new List<ProgramaGeneralPresentacionArgumentoModalidad>();
                        foreach (var subItem in dto.Modalidades)
                        {
                            ProgramaGeneralPresentacionArgumentoModalidad modalidad;
                            if (!_unitOfWork.ProgramaGeneralPresentacionArgumentoModalidadRepository.Exist(x => x.IdProgramaGeneralPresentacionArgumento == problema.Id && x.IdModalidadCurso == subItem.IdModalidad))
                            {
                                modalidad = new ProgramaGeneralPresentacionArgumentoModalidad()
                                {
                                    Nombre = subItem.Nombre,
                                    IdPgeneral = dto.IdPGeneral,
                                    IdModalidadCurso = subItem.IdModalidad,
                                    UsuarioCreacion = usuario,
                                    UsuarioModificacion = usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now,
                                    Estado = true
                                };
                                problema.ProgramaGeneralPresentacionArgumentoModalidad.Add(modalidad);
                                _unitOfWork.Commit();
                            }
                        }
                        _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.Update(problema);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        var problema = new ProgramaGeneralPresentacionArgumento();
                        problema.IdPgeneral = dto.IdPGeneral;
                        problema.Nombre = dto.NombrePresentacionArgumento;
                        problema.Descripcion = dto.DescripcionPresentacionArgumento;
                        problema.EsVisibleAgenda = dto.EsVisibleAgenda;
                        problema.UsuarioCreacion = usuario;
                        problema.UsuarioModificacion = usuario;
                        problema.FechaCreacion = DateTime.Now;
                        problema.FechaModificacion = DateTime.Now;
                        problema.Estado = true;

                        problema.ProgramaGeneralPresentacionArgumentoDetalleSolucion = dto.PresentacionArgumento
                            .Select(x => new ProgramaGeneralPresentacionArgumentoDetalleSolucion
                            {
                                Detalle = x.Detalle,
                                Solucion = x.Solucion,
                                IdPgeneral = dto.IdPGeneral,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();
                        problema.ProgramaGeneralPresentacionArgumentoModalidad = dto.Modalidades
                            .Select(x => new ProgramaGeneralPresentacionArgumentoModalidad()
                            {
                                Nombre = x.Nombre,
                                IdPgeneral = dto.IdPGeneral,
                                IdModalidadCurso = x.IdModalidad,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            }).ToList();
                        var res = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.Add(problema);
                        _unitOfWork.Commit();
                        dto.IdPresentacionArgumento = res.Id;
                        _unitOfWork.Commit();
                    }
                    scope.Complete();
                }
                var resultado = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerPorId(dto.IdPresentacionArgumento);
                return _mapper.Map<ProgramaGeneralPresentacionArgumentoDTO>(resultado);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="certificadoPartnerComplementoDTO"> parametros de la nueva Plantilla_PW y sus detalles </param>

        public ProgramaGeneralPresentacionArgumentoDTO Actualizar(ProgramaGeneralPresentacionArgumentoDTO dto, string usuario)
        {
            try
            {
                ProgramaGeneralPresentacionArgumento? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {

                            entidad.Nombre = dto.Nombre;
                            entidad.IdPgeneral = dto.IdPgeneral;
                            entidad.EsVisibleAgenda = dto.EsVisibleAgenda;
                            entidad.Estado = dto.Estado;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.Update(entidad);
                            _unitOfWork.Commit();


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

        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 26/09/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var servicioPresentacionArgumento = new ProgramaGeneralPresentacionArgumentoService(_unitOfWork);
                var servicioModalidad = new ProgramaGeneralPresentacionArgumentoModalidadService(_unitOfWork);
                var servicioDetalleSolucion = new ProgramaGeneralPresentacionArgumentoDetalleSolucionService(_unitOfWork);

                var respuestaProblema = _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.Delete(id, usuario);

                var idModalidades = _unitOfWork.ProgramaGeneralPresentacionArgumentoModalidadRepository.ObtenerModalidadPorIdPresentacionArgumento(id).Select(m => m.Id).ToList();
                var respuestaModalidad = _unitOfWork.ProgramaGeneralPresentacionArgumentoModalidadRepository.Delete(idModalidades, usuario);

                var idArgumentos = _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository
                    .ObtenerPresentacionArgumentoDetalleSolucionPorIdPresentacionArgumento(id).Select(d => d.Id).ToList();
                var respuestaArgumento = _unitOfWork.ProgramaGeneralPresentacionArgumentoDetalleSolucionRepository.Delete(idArgumentos, usuario);

                _unitOfWork.Commit();
                if (respuestaProblema && respuestaModalidad && respuestaArgumento)
                {
                    return true;
                }
                return false;
          
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 01-10-2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Presentacion Argumento de Programa General asociados a una Oportunidad.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ProgramaGeneralPresentacionArgumentoAgendaDTO> </returns>
        public List<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                var argumento = ObtenerProgramaGeneralArgumentoParaAgendaPorIdOportunidad(idOportunidad);
                var argumentoDetalle = _mapper.Map<List<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO>>(argumento);

                var presetnacionArgumentoDetalleSolucionService = new ProgramaGeneralPresentacionArgumentoDetalleSolucionService(_unitOfWork);
                argumentoDetalle.ForEach(
                    p => p.Argumentos = presetnacionArgumentoDetalleSolucionService.ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucionParaAgenda(p.IdPresentacionArgumento, idOportunidad).ToList()
                );
                return argumentoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<ProgramaGeneralPresentacionArgumentoAgendaDTO> ObtenerProgramaGeneralArgumentoParaAgendaPorIdOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPresentacionArgumentoRepository.ObtenerProgramaGeneralPresentacionArgumentoParaAgendaPorIdOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
