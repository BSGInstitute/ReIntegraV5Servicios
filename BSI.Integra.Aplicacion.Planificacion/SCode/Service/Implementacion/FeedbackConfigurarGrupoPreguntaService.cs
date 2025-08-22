using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;


namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: FeedbackConfigurarGrupoPreguntaService
    /// Autor: Klebert Layme.
    /// Fecha: 27/05/2023
    /// <summary>
    /// Gestión general de T_FeedbackConfigurarGrupoPregunta
    /// </summary>
    public class FeedbackConfigurarGrupoPreguntaService : IFeedbackConfigurarGrupoPreguntaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FeedbackConfigurarGrupoPreguntaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TFeedbackConfigurarGrupoPreguntum, FeedbackConfigurarGrupoPregunta>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TFeedbackConfigurarGrupoPreguntum, FeedbackConfigurarGrupoPreguntaDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<FeedbackConfigurarGrupoPregunta, FeedbackConfigurarGrupoPreguntaDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de pgeneral , pespecifico y feedbackconfigurar
        /// </summary>
        /// <returns> listaCombos </returns>
        public FeedbackComboDTO ObtenerCombos()
        {
            try
            {
                FeedbackComboDTO listaCombos = new()
                {
                    ProgramasGenerales = _unitOfWork.PGeneralRepository.ObtenerProgramasFiltro(),
                    ProgramasEspecificos = _unitOfWork.PEspecificoRepository.ObtenerFiltro(),
                    FeedbackConfigurados = _unitOfWork.FeedbackConfigurarRepository.ObtenerTodoFeedbackConfigurarFiltro(),
                };
                return listaCombos;
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
        /// Obtiene en lista , los Programas Seleccionados de FeedbackPreguntaPrograma General y Feedback Pregunta Programa Especifico
        /// </summary>
        /// <param name="idConfiguracion">Id de </param>
        /// <returns> listaCombos </returns>
        public (List<int> ProgramasGenerales, List<int> ProgramasEspecificos) ObtenerListaProgramasSelecionados(int idConfiguracion)
        {
            try
            {
                var pgeneral = _unitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == idConfiguracion).Select(x => x.IdPgeneral).ToList();
                var pespecifico = _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.GetBy(x => x.IdFeedbackConfigurarGrupoPregunta == idConfiguracion).Select(x => x.IdPespecifico).ToList();

                return (pgeneral, pespecifico);
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
        /// Registra un nuevo FeedbackConfiguracionGrupoPregunta
        /// </summary>
        /// <param name="dto">Feedback de Configuracion Grupo Preguntan</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>AreaCapacitacion</returns>
        public FeedbackConfigurarGrupoPreguntaDTO Insertar(RegistroFeedbackConfigurarGrupoPreguntaDTO dto, string usuario)
        {

            try
            {
                if (dto != null)
                {
                    FeedbackConfigurarGrupoPregunta NuevaFeedbackConfigurarGrupoPregunta = new()
                    {
                        IdFeedbackConfigurar = dto.IdFeedbackConfigurar,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    var result = _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.Add(NuevaFeedbackConfigurarGrupoPregunta);
                    _unitOfWork.Commit();
                    if (dto.ConfiguracionFeedbackProgramaGeneral != null && dto.ConfiguracionFeedbackProgramaGeneral.Count() > 0)
                    {
                        var detalle = dto.ConfiguracionFeedbackProgramaGeneral.Select(x => new FeedbackGrupoPreguntaProgramaGeneral
                        {
                            IdFeedbackConfigurarGrupoPregunta = result.Id,
                            IdPgeneral = x,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                        _unitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository.Add(detalle);
                        _unitOfWork.Commit();
                    }
                    if (dto.ConfiguracionProgramaEspecifico != null && dto.ConfiguracionProgramaEspecifico.Count() > 0)
                    {
                        var detalle = dto.ConfiguracionProgramaEspecifico.Select(x => new FeedbackGrupoPreguntaProgramaEspecifico
                        {
                            IdFeedbackConfigurarGrupoPregunta = result.Id,
                            IdPespecifico = x,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                        _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.Add(detalle);
                        _unitOfWork.Commit();
                    }

                    var resultado = _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.ObtenerFeedbackConfigurarPorId(result.Id)!;
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");

            }
            catch (Exception)
            {
                _unitOfWork.Dispose();
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 29/05/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza un nuevo FeedbackConfiguracionGrupoPregunta
        /// </summary>
        /// <param name="dto">Feedback de Configuracion Grupo Preguntan</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>AreaCapacitacion</returns>
        public FeedbackConfigurarGrupoPreguntaDTO Actualizar(RegistroFeedbackConfigurarGrupoPreguntaDTO dto, string usuario)
        {
            try
            {
                if (dto != null)
                {
                    FeedbackConfigurarGrupoPregunta? entidad = _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.ObtenerPorId(dto.Id);
                    if (entidad == null)
                    {
                        throw new BadRequestException("No existe la entidad");
                    }
                    entidad.IdFeedbackConfigurar = dto.IdFeedbackConfigurar;
                    entidad.UsuarioModificacion = usuario;
                    entidad.FechaModificacion = DateTime.Now;
                    _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.Update(entidad);
                    _unitOfWork.Commit();

                    IFeedbackGrupoPreguntaProgramaGeneralService feedbackGrupoPreguntaProgramaGeneralService = new FeedbackGrupoPreguntaProgramaGeneralService(_unitOfWork);
                    feedbackGrupoPreguntaProgramaGeneralService.EliminacionLogicoPorIdGrupoPreguntaPGeneral(dto.Id, usuario, dto.ConfiguracionFeedbackProgramaGeneral);

                    IFeedbackGrupoPreguntaProgramaEspecificoService feedbackGrupoPreguntaProgramaEspecificoService = new FeedbackGrupoPreguntaProgramaEspecificoService(_unitOfWork);
                    feedbackGrupoPreguntaProgramaEspecificoService.EliminacionLogicoPorIdGrupoPreguntaPEspecifico(dto.Id, usuario, dto.ConfiguracionProgramaEspecifico);

                    var pgeneral = _unitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository.ObtenerPorIdFeedbackConfigurar(dto.Id);
                    var pespecifico = _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.ObtenerPorIdFeedbackConfigurar(dto.Id);

                    if (dto.ConfiguracionFeedbackProgramaGeneral != null && dto.ConfiguracionFeedbackProgramaGeneral.Count() > 0)
                    {
                        var detalle = dto.ConfiguracionFeedbackProgramaGeneral.Where(x => !pgeneral.Any(s => s.IdPgeneral == x)).Select(x => new FeedbackGrupoPreguntaProgramaGeneral
                        {
                            IdFeedbackConfigurarGrupoPregunta = entidad.Id,
                            IdPgeneral = x,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                        _unitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository.Add(detalle);
                        _unitOfWork.Commit();
                    }
                    if (dto.ConfiguracionProgramaEspecifico != null && dto.ConfiguracionProgramaEspecifico.Count() > 0)
                    {
                        var detalle = dto.ConfiguracionProgramaEspecifico.Where(y => pespecifico.Any(s => s.IdPespecifico != y)).Select(x => new FeedbackGrupoPreguntaProgramaEspecifico
                        {
                            IdFeedbackConfigurarGrupoPregunta = entidad.Id,
                            IdPespecifico = x,
                            Estado = true,
                            UsuarioCreacion = usuario,
                            UsuarioModificacion = usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        });
                        _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.Add(detalle);
                        _unitOfWork.Commit();
                    }
                    var resultado = _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.ObtenerFeedbackConfigurarPorId(dto.Id)!;
                    return resultado;
                }
                else
                    throw new BadRequestException("Entidad Nula");

            }
            catch
            {
                _unitOfWork.Dispose();
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// elimina Un Feedback Configuracion Grupo
        /// </summary>
        /// <param name="id">Id del Feedback Configuracion Grupo</param>
        /// <returns> AreaTrabajo </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var tipoDecuento = _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.ObtenerPorId(id);
                if (tipoDecuento != null && tipoDecuento.Id > 0)
                {
                    var respuesta = _unitOfWork.FeedbackConfigurarGrupoPreguntaRepository.Delete(id, usuario);
                    var hijos = _unitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository.ObtenerPorIdFeedbackConfigurar(tipoDecuento.Id);
                    _unitOfWork.FeedbackGrupoPreguntaProgramaGeneralRepository.Delete(hijos.Select(x => x.Id), usuario);
                    var hijas = _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.ObtenerPorIdFeedbackConfigurar(tipoDecuento.Id);
                    _unitOfWork.FeedbackGrupoPreguntaProgramaEspecificoRepository.Delete(hijas.Select(x => x.Id), usuario);
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
