using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: TipoDescuentoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión general de T_TipoDescuento
    /// </summary>
    public class TipoDescuentoService : ITipoDescuentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoDescuentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDescuento, TipoDescuento>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuento, TipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuento, TipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuentoAsesorCoordinadorPw, TipoDescuentoAsesorCoordinadorPwDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuento, CompuestoTipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TTipoDescuento, CompuestoTipoDescuentoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoDescuento, TipoDescuentoProgramaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PGeneralTipoDescuento, TipoDescuentoProgramaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralTipoDescuento, TipoDescuentoProgramaDTO>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// elimina Un tipo de descuento con su tipo descuentocoordinador
        /// </summary>
        /// <param name="id">Id del Tipo descuento</param>
        /// <returns> TipoDescuento </returns>
        /// 
        public CompuestoTipoDescuentoDTO Insertar(CompuestoTipoDescuentoDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    TipoDescuento tipoDescuento = new()
                    {
                        Id = dto.Id,
                        Codigo = dto.Codigo,
                        Descripcion = dto.Descripcion,
                        Formula = dto.Formula,
                        PorcentajeGeneral = dto.PorcentajeGeneral,
                        PorcentajeMatricula = dto.PorcentajeMatricula,
                        FraccionesMatricula = dto.FraccionesMatricula,
                        PorcentajeCuotas = dto.PorcentajeCuotas,
                        CuotasAdicionales = dto.CuotasAdicionales,
                        IdTipoDescuentoNivelAprobacion = dto.IdTipoDescuentoNivelAprobacion,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true,
                    };
                    var result2 = _unitOfWork.TipoDescuentoRepository.Add(tipoDescuento);
                    _unitOfWork.Commit();

                    //foreach (var item in dto.TipoDescuentoAsesorCoordinadorPw)
                    //{
                    //    TipoDescuentoAsesorCoordinadorPw tipoDescuentoAsesorCoordinadorPw = new();
                    //    tipoDescuentoAsesorCoordinadorPw.Tipo = item;
                    //    tipoDescuentoAsesorCoordinadorPw.IdTipoDescuento = result2.Id;
                    //    tipoDescuentoAsesorCoordinadorPw.UsuarioCreacion = usuario;
                    //    tipoDescuentoAsesorCoordinadorPw.UsuarioModificacion = usuario;
                    //    tipoDescuentoAsesorCoordinadorPw.FechaCreacion = DateTime.Now;
                    //    tipoDescuentoAsesorCoordinadorPw.FechaModificacion = DateTime.Now;
                    //    tipoDescuentoAsesorCoordinadorPw.Estado = true;

                    //    var result = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.Add(tipoDescuentoAsesorCoordinadorPw);
                    //}
                    if (dto.TipoDescuentoAsesorCoordinadorPw != null && dto.TipoDescuentoAsesorCoordinadorPw.Count() > 0)
                    {
                        var detalle = dto.TipoDescuentoAsesorCoordinadorPw.Select(x => new TipoDescuentoAsesorCoordinadorPw
                        {
                            Tipo = x,
                            IdTipoDescuento = result2.Id,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                        _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.Add(detalle);
                        _unitOfWork.Commit();
                    }
                    var resultado = _mapper.Map<CompuestoTipoDescuentoDTO>(tipoDescuento);
                    resultado.TipoDescuentoAsesorCoordinadorPw = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.ObtenerTiposPorIdTipoDescuento(tipoDescuento.Id).ToList();
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// elimina Un tipo de descuento con su tipo descuentocoordinador
        /// </summary>
        /// <param name="id">Id del Tipo descuento</param>
        /// <returns> TipoDescuento </returns>
        public CompuestoTipoDescuentoDTO Actualizar(CompuestoTipoDescuentoDTO dto, string usuario)
        {
            try
            {
                TipoDescuento tipoDescuento = new();
                if (dto != null)
                {
                    tipoDescuento = _unitOfWork.TipoDescuentoRepository.ObtenerPorId(dto.Id);
                    if (tipoDescuento != null && tipoDescuento.Id != 0)
                    {
                        tipoDescuento.Id = dto.Id;
                        tipoDescuento.Codigo = dto.Codigo;
                        tipoDescuento.Descripcion = dto.Descripcion;
                        tipoDescuento.Formula = dto.Formula;
                        tipoDescuento.PorcentajeGeneral = dto.PorcentajeGeneral;
                        tipoDescuento.PorcentajeMatricula = dto.PorcentajeMatricula;
                        tipoDescuento.FraccionesMatricula = dto.FraccionesMatricula;
                        tipoDescuento.PorcentajeCuotas = dto.PorcentajeCuotas;
                        tipoDescuento.CuotasAdicionales = dto.CuotasAdicionales;
                        tipoDescuento.IdTipoDescuentoNivelAprobacion = dto.IdTipoDescuentoNivelAprobacion;
                        tipoDescuento.UsuarioModificacion = usuario;
                        tipoDescuento.FechaModificacion = DateTime.Now;
                        tipoDescuento.Estado = true;
                    }
                    else
                        throw new BadRequestException("Entidad no encontrada");
                    _unitOfWork.TipoDescuentoRepository.Update(tipoDescuento);
                    _unitOfWork.Commit();
                    //List<TTipoDescuentoAsesorCoordinadorPw> tipoDescuentoAsesorCoordinadorPw1 = new();

                    ITipoDescuentoAsesorCoordinadorPwService tipoDescuentoAsesorCoordinadorPwService = new TipoDescuentoAsesorCoordinadorPwService(_unitOfWork);
                    tipoDescuentoAsesorCoordinadorPwService.EliminacionLogicoPorTipoDescuento(dto.Id, usuario, dto.TipoDescuentoAsesorCoordinadorPw);

                    var detalle = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.ObtenerPorIdTipoDescuento(tipoDescuento.Id);
                    //foreach (var item in dto.TipoDescuentoAsesorCoordinadorPw)
                    //{
                    //    TipoDescuentoAsesorCoordinadorPw tipoDescuentoAsesorCoordinadorPw = detalle.FirstOrDefault(x => x.Tipo == item);
                    //    if (tipoDescuentoAsesorCoordinadorPw == null || tipoDescuentoAsesorCoordinadorPw.Id == 0)
                    //    {
                    //        tipoDescuentoAsesorCoordinadorPw = new();
                    //        tipoDescuentoAsesorCoordinadorPw.Tipo = item;
                    //        tipoDescuentoAsesorCoordinadorPw.IdTipoDescuento = tipoDescuento.Id;
                    //        tipoDescuentoAsesorCoordinadorPw.UsuarioCreacion = usuario;
                    //        tipoDescuentoAsesorCoordinadorPw.UsuarioModificacion = usuario;
                    //        tipoDescuentoAsesorCoordinadorPw.FechaCreacion = DateTime.Now;
                    //        tipoDescuentoAsesorCoordinadorPw.FechaModificacion = DateTime.Now;
                    //        tipoDescuentoAsesorCoordinadorPw.Estado = true;
                    //        _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.Add(tipoDescuentoAsesorCoordinadorPw);
                    //        _unitOfWork.Commit();
                    //    }
                    //}
                    if (dto.TipoDescuentoAsesorCoordinadorPw != null && dto.TipoDescuentoAsesorCoordinadorPw.Count() > 0)
                    {
                        var detalleInsertar = dto.TipoDescuentoAsesorCoordinadorPw.Where(x => !detalle.Any(s => s.Tipo == x)).Select(x => new TipoDescuentoAsesorCoordinadorPw
                        {
                            Tipo = x,
                            IdTipoDescuento = tipoDescuento.Id,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        });
                        var rresult = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.Add(detalleInsertar);
                        _unitOfWork.Commit();
                    }

                    var resultado = _mapper.Map<CompuestoTipoDescuentoDTO>(tipoDescuento);
                    resultado.TipoDescuentoAsesorCoordinadorPw = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.ObtenerTiposPorIdTipoDescuento(tipoDescuento.Id).ToList();
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// elimina Un area de trabajo
        /// </summary>
        /// <param name="id">Id del area de trabajo</param>
        /// <returns> AreaTrabajo </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var tipoDecuento = _unitOfWork.TipoDescuentoRepository.ObtenerPorId(id);
                if (tipoDecuento != null && tipoDecuento.Id > 0)
                {
                    var respuesta = _unitOfWork.TipoDescuentoRepository.Delete(id, usuario);
                    var hijos = _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.ObtenerPorIdTipoDescuento(tipoDecuento.Id);
                    _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.Delete(hijos.Select(x => x.Id), usuario);
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
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDescuento por id
        /// </summary>
        /// <returns> IEnumerable<string> </returns>
        public IEnumerable<string> ObtenerTiposPorIdTipoDescuento(int idDescuentoAsesor)
        {

            return _unitOfWork.TipoDescuentoAsesorCoordinadorPwRepository.ObtenerTiposPorIdTipoDescuento(idDescuentoAsesor);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDescuento
        /// </summary>
        /// <returns> List<TipoDescuentoDTO> </returns>
        public IEnumerable<TipoDescuentoDTO> Obtener()
        {
            return _unitOfWork.TipoDescuentoRepository.ObtenerTipoDescuento();
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoDescuento para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoDescuentoComboDTO> </returns>
        public ComboTipoDescuentoDTO ObtenerCombosModulo()
        {
            try
            {
                ComboTipoDescuentoDTO combos = new()
                {
                    FormulaTipoDescuentos = _unitOfWork.FormulaTipoDescuentoRepository.ObtenerTodoGrid(),
                    ProgramasGeneral = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro(),
                    TiposUsuario = _unitOfWork.AgendaTipoUsuarioRepository.ObtenerTipoUsuarioFiltro(),
                };
                return combos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tipos de Descuento asociados a una Oportunidad y un Tipo de Personal.
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <param name="tipoPersonal">Tipo de Personal</param>
        /// <returns> List<TipoDescuentoOportunidadDTO> </returns>
        public IEnumerable<TipoDescuentoOportunidadDTO> ObtenerTipoDescuentoOportunidad(int idOportunidad, string tipoPersonal)
        {
            try
            {
                return _unitOfWork.TipoDescuentoRepository.ObtenerTipoDescuentoOportunidad(idOportunidad, tipoPersonal);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los tipos de descuento con su nivel de aprobación asociado
        /// </summary>
        /// <returns> List<TipoDescuentoConNivelAprobacionDTO> </returns>
        public IEnumerable<TipoDescuentoConNivelAprobacionDTO> ObtenerTipoDescuentoConNivelAprobacion()
        {
            try
            {
                return _unitOfWork.TipoDescuentoRepository.ObtenerTipoDescuentoConNivelAprobacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los niveles de aprobación activos
        /// </summary>
        /// <returns> List<TipoDescuentoNivelAprobacionDTO> </returns>
        public IEnumerable<TipoDescuentoNivelAprobacionDTO> ObtenerNivelesAprobacion()
        {
            try
            {
                return _unitOfWork.TipoDescuentoRepository.ObtenerNivelesAprobacion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Lolo Zaa
        /// Fecha: 12/01/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza el nivel de aprobación de un tipo de descuento
        /// </summary>
        /// <param name="id">Id del tipo de descuento</param>
        /// <param name="idTipoDescuentoNivelAprobacion">Id del nivel de aprobación</param>
        /// <param name="usuario">Usuario que realiza la modificación</param>
        /// <returns> bool </returns>
        public bool ActualizarNivelAprobacion(int id, int? idTipoDescuentoNivelAprobacion, string usuario)
        {
            try
            {
                return _unitOfWork.TipoDescuentoRepository.ActualizarNivelAprobacion(id, idTipoDescuentoNivelAprobacion, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
