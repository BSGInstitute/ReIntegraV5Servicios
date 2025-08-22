using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using PdfSharp.Pdf.Filters;
using System.Collections.Generic;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PreguntaFrecuenteService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PreguntaFrecuente
    /// </summary>
    public class PreguntaFrecuenteService : IPreguntaFrecuenteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PreguntaFrecuenteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaFrecuente, PreguntaFrecuente>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreguntaFrecuenteComboDTO, PreguntaFrecuenteDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreguntaFrecuenteDTO, TPreguntaFrecuente>(MemberList.None).ReverseMap();
                cfg.CreateMap<PreguntaFrecuente, PreguntaFrecuenteDTO>(MemberList.None).ReverseMap();
            }
           );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los datos de T_PreguntaFrecuente y detalles
        /// </summary> 
        /// <returns> List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> </returns>
        public List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> Obtener()
        {
            try
            {
                var preguntaFrecuenteFiltroPaginacionDTOs = _unitOfWork.PreguntaFrecuenteRepository.Obtener();
                List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> respuestaPreguntaFrecuenteFiltroPaginacions = new List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO>();

                var preguntaFrecuenteAreas = _unitOfWork.PreguntaFrecuenteAreaRepository.Obtener();
                var preguntaFrecuenteSubAreas = _unitOfWork.PreguntaFrecuenteSubAreaRepository.Obtener();
                var preguntaFrecuentePGenerals = _unitOfWork.PreguntaFrecuentePGeneralRepository.Obtener();
                var preguntaFrecuenteTipos = _unitOfWork.PreguntaFrecuenteTipoRepository.Obtener();

                foreach (var item in preguntaFrecuenteFiltroPaginacionDTOs)
                {
                    PreguntaFrecuenteFiltroResultadoAgrupadoDTO preguntaFrecuente = new PreguntaFrecuenteFiltroResultadoAgrupadoDTO();


                    preguntaFrecuente.Id = item.Id;
                    preguntaFrecuente.IdSeccion = item.IdSeccionPreguntaFrecuente;
                    preguntaFrecuente.Pregunta = item.Pregunta;
                    preguntaFrecuente.Respuesta = item.Respuesta;

                    preguntaFrecuente.IdsAreas = new List<int?>();
                    preguntaFrecuente.IdsAreas = preguntaFrecuenteAreas.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => (int?)i.IdArea).ToList();

                    if (preguntaFrecuente.IdsAreas.Contains(0))
                    {
                        preguntaFrecuente.IdsAreas.Remove(0);
                    }

                    preguntaFrecuente.IdsSubareas = new List<int?>();
                    preguntaFrecuente.IdsSubareas = preguntaFrecuenteSubAreas.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => (int?)i.IdSubArea).ToList();

                    if (preguntaFrecuente.IdsSubareas.Contains(0))
                    {
                        preguntaFrecuente.IdsSubareas.Remove(0);
                    }

                    preguntaFrecuente.IdsPgenerales = new List<int?>();
                    preguntaFrecuente.IdsPgenerales = preguntaFrecuentePGenerals.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => i.IdPgeneral).ToList();

                    if (preguntaFrecuente.IdsPgenerales.Contains(null))
                    {
                        preguntaFrecuente.IdsPgenerales.Remove(null);
                    }

                    preguntaFrecuente.IdsTipos = new List<int?>();
                    preguntaFrecuente.IdsTipos = preguntaFrecuenteTipos.Where(o => o.IdPreguntaFrecuente.Equals(preguntaFrecuente.Id)).Select(i => (int?)i.IdTipo).ToList();

                    if (preguntaFrecuente.IdsTipos.Contains(3))
                    {
                        preguntaFrecuente.IdsTipos.Remove(3);
                    }

                    respuestaPreguntaFrecuenteFiltroPaginacions.Add(preguntaFrecuente);
                }
                return respuestaPreguntaFrecuenteFiltroPaginacions;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de SeccionPreguntaFrecuente
        /// </summary> 
        /// <returns> PreguntaFrecuenteComboModuloDTO </returns>
        public PreguntaFrecuenteComboModuloDTO ObtenerCombosModulo()
        {
            var preguntaFrecuenteComboModulo = new PreguntaFrecuenteComboModuloDTO();
            preguntaFrecuenteComboModulo.SeccionPreguntaFrecuente = _unitOfWork.SeccionPreguntaFrecuenteRepository.ObtenerCombo().ToList();
            preguntaFrecuenteComboModulo.PGeneralSubArea = _unitOfWork.PGeneralRepository.ObtenerPGeneralSubArea().ToList();
            preguntaFrecuenteComboModulo.SubAreaCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro().ToList();
            preguntaFrecuenteComboModulo.AreaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerFiltro().ToList();
            preguntaFrecuenteComboModulo.ModalidadCurso = _unitOfWork.ModalidadCursoRepository.ObtenerCombo().ToList();
            return preguntaFrecuenteComboModulo;
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// /// <summary>
        /// Obtiene las preguntas frecuentes y sus detalles por filtros
        /// </summary>
        /// Autor Modificacion: Christian Qm.
        /// Fecha Modificacion: 28/06/2023
        /// Version: 1.1
        /// <summary>
        /// Se agrego el agrupado de los campos hijos
        /// </summary>
        /// <param name="filtro"> Filtros </param>
        /// <returns> List<PreguntaFrecuenteFiltroResultadoDTO> </returns>
        public List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> ObtenerPorFiltro(FiltroPreguntaFrecuenteDTO filtro)
        {
            try
            {
                List<PreguntaFrecuenteFiltroResultadoDTO> listaDesagrupada = _unitOfWork.PreguntaFrecuenteRepository.ObtenerPorFiltro(filtro).ToList();
                List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> listaAgrupada = listaDesagrupada.GroupBy(x => new {
                    x.Id,
                    x.IdSeccion,
                    x.Pregunta,
                    x.Respuesta,
                    x.Tipo
                }).Select(y => new PreguntaFrecuenteFiltroResultadoAgrupadoDTO {
                    Id = y.Key.Id,
                    IdSeccion = y.Key.IdSeccion,
                    Pregunta = y.Key.Pregunta,
                    Respuesta = y.Key.Respuesta,
                    Tipo = y.Key.Tipo,
                    IdsAreas = y.GroupBy(w => w.IdArea).Select(z => z.Key).ToList(),
                    IdsSubareas = y.GroupBy(w => w.IdSubArea).Select(z => z.Key).ToList(),
                    IdsPgenerales = y.GroupBy(w => w.IdPGeneral).Select(z => z.Key).ToList(),
                    IdsTipos = y.GroupBy(w => w.IdTipo).Select(z => z.Key).ToList(),
                }).ToList();
                return listaAgrupada.OrderByDescending(x => x.Id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción basica a la tabla y sus detalles
        /// </summary>
        /// <param name="preguntaFrecuenteParametrosDTO"> parametros entrada </param>
        /// <param name="usuario"> Autor de la creacion </param>
        /// <returns> nueva Pregunta Frecuente: PreguntaFrecuenteDTO </returns>
        public List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> Insertar(PreguntaFrecuenteParametrosDTO preguntaFrecuenteParametrosDTO, string usuario)
        {
            try
            {
                PreguntaFrecuente preguntaFrecuente = new PreguntaFrecuente();
                var respuesta = new PreguntaFrecuenteDTO();
                using (TransactionScope scope = new TransactionScope())
                {
                    preguntaFrecuente = _mapper.Map<PreguntaFrecuente>(preguntaFrecuenteParametrosDTO.PreguntaFrecuente);
                    preguntaFrecuente.UsuarioCreacion = usuario;
                    preguntaFrecuente.UsuarioModificacion = usuario;
                    preguntaFrecuente.FechaCreacion = DateTime.Now;
                    preguntaFrecuente.FechaModificacion = DateTime.Now;
                    preguntaFrecuente.Estado = true;

                    preguntaFrecuente.PreguntaFrecuentePgeneral = new List<PreguntaFrecuentePGeneral>();
                    preguntaFrecuente.PreguntaFrecuenteArea = new List<PreguntaFrecuenteArea>();
                    preguntaFrecuente.PreguntaFrecuenteSubArea = new List<PreguntaFrecuenteSubArea>();
                    preguntaFrecuente.PreguntaFrecuenteTipo = new List<PreguntaFrecuenteTipo>();

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuentePGenerals)
                    {
                        PreguntaFrecuentePGeneral preguntaFrecuentePgeneral = new PreguntaFrecuentePGeneral();
                        preguntaFrecuentePgeneral.IdPgeneral = item;
                        preguntaFrecuentePgeneral.UsuarioCreacion = usuario;
                        preguntaFrecuentePgeneral.UsuarioModificacion = usuario;
                        preguntaFrecuentePgeneral.FechaCreacion = DateTime.Now;
                        preguntaFrecuentePgeneral.FechaModificacion = DateTime.Now;
                        preguntaFrecuentePgeneral.Estado = true;

                        preguntaFrecuente.PreguntaFrecuentePgeneral.Add(preguntaFrecuentePgeneral);
                    }

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuenteAreas)
                    {
                        PreguntaFrecuenteArea preguntaFrecuenteArea = new PreguntaFrecuenteArea();
                        preguntaFrecuenteArea.IdArea = item;
                        preguntaFrecuenteArea.UsuarioCreacion = usuario;
                        preguntaFrecuenteArea.UsuarioModificacion = usuario;
                        preguntaFrecuenteArea.FechaCreacion = DateTime.Now;
                        preguntaFrecuenteArea.FechaModificacion = DateTime.Now;
                        preguntaFrecuenteArea.Estado = true;

                        preguntaFrecuente.PreguntaFrecuenteArea.Add(preguntaFrecuenteArea);
                    }

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuenteSubAreas)
                    {
                        PreguntaFrecuenteSubArea preguntaFrecuenteSubArea = new PreguntaFrecuenteSubArea();
                        preguntaFrecuenteSubArea.IdSubArea = item;
                        preguntaFrecuenteSubArea.UsuarioCreacion = usuario;
                        preguntaFrecuenteSubArea.UsuarioModificacion = usuario;
                        preguntaFrecuenteSubArea.FechaCreacion = DateTime.Now;
                        preguntaFrecuenteSubArea.FechaModificacion = DateTime.Now;
                        preguntaFrecuenteSubArea.Estado = true;

                        preguntaFrecuente.PreguntaFrecuenteSubArea.Add(preguntaFrecuenteSubArea);
                    }

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuenteTipos)
                    {
                        PreguntaFrecuenteTipo preguntaFrecuenteTipo = new PreguntaFrecuenteTipo();
                        preguntaFrecuenteTipo.IdTipo = item;
                        preguntaFrecuenteTipo.UsuarioCreacion = usuario;
                        preguntaFrecuenteTipo.UsuarioModificacion = usuario;
                        preguntaFrecuenteTipo.FechaCreacion = DateTime.Now;
                        preguntaFrecuenteTipo.FechaModificacion = DateTime.Now;
                        preguntaFrecuenteTipo.Estado = true;

                        preguntaFrecuente.PreguntaFrecuenteTipo.Add(preguntaFrecuenteTipo);
                    }
                    var nuevaEntidad = _unitOfWork.PreguntaFrecuenteRepository.Add(preguntaFrecuente);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return Obtener();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla y sus detalles
        /// </summary>
        /// <param name="preguntaFrecuenteParametrosDTO"> parametros entrada </param>
        /// <param name="usuario"> Autor de la creacion </param>
        /// <returns> nueva Pregunta Frecuente: PreguntaFrecuenteDTO </returns>
        public List<PreguntaFrecuenteFiltroResultadoAgrupadoDTO> Actualizar(PreguntaFrecuenteParametrosDTO preguntaFrecuenteParametrosDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    /*Obtenemos los registros actuales de los hijos*/
                    var preguntaFrecuentePGenerals = _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPorIdPreguntaFrecuente(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id).ToList();
                    var preguntaFrecuenteAreas = _unitOfWork.PreguntaFrecuenteAreaRepository.ObtenerPorIdPreguntaFrecuente(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id).ToList();
                    var preguntaFrecuenteSubAreas = _unitOfWork.PreguntaFrecuenteSubAreaRepository.ObtenerPorIdPreguntaFrecuente(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id).ToList();
                    var preguntaFrecuenteTipos = _unitOfWork.PreguntaFrecuenteTipoRepository.ObtenerPorIdPreguntaFrecuente(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id).ToList();

                    /*De la lista excluiremos a los hijos que permaneceran relacionado al padre*/
                    preguntaFrecuentePGenerals.RemoveAll(x => preguntaFrecuenteParametrosDTO.PreguntaFrecuentePGenerals.Any(y => y == x.IdPgeneral));
                    preguntaFrecuenteAreas.RemoveAll(x => preguntaFrecuenteParametrosDTO.PreguntaFrecuenteAreas.Any(y => y == x.IdArea));
                    preguntaFrecuenteSubAreas.RemoveAll(x => preguntaFrecuenteParametrosDTO.PreguntaFrecuenteSubAreas.Any(y => y == x.IdSubArea));
                    preguntaFrecuenteTipos.RemoveAll(x => preguntaFrecuenteParametrosDTO.PreguntaFrecuenteTipos.Any(y => y == x.IdTipo));

                    /*Excluimos la lista de hijos*/
                    _unitOfWork.PreguntaFrecuentePGeneralRepository.Delete(preguntaFrecuentePGenerals.Select(x => x.Id), usuario);
                    _unitOfWork.PreguntaFrecuenteAreaRepository.Delete(preguntaFrecuenteAreas.Select(x => x.Id), usuario);
                    _unitOfWork.PreguntaFrecuenteSubAreaRepository.Delete(preguntaFrecuenteSubAreas.Select(x => x.Id), usuario);
                    _unitOfWork.PreguntaFrecuenteTipoRepository.Delete(preguntaFrecuenteTipos.Select(x => x.Id), usuario);
                    _unitOfWork.Commit();

                    /*Actualizamos al padre y los hijos*/
                    var preguntaFrecuente = _unitOfWork.PreguntaFrecuenteRepository.ObtenerPorId(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id);
                    preguntaFrecuente.IdSeccionPreguntaFrecuente = preguntaFrecuenteParametrosDTO.PreguntaFrecuente.IdSeccionPreguntaFrecuente;
                    preguntaFrecuente.Pregunta = preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Pregunta;
                    preguntaFrecuente.Respuesta = preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Respuesta;
                    preguntaFrecuente.Tipo = preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Tipo;
                    preguntaFrecuente.UsuarioModificacion = usuario;
                    preguntaFrecuente.FechaModificacion = DateTime.Now;

                    preguntaFrecuente.PreguntaFrecuentePgeneral = new List<PreguntaFrecuentePGeneral>();
                    preguntaFrecuente.PreguntaFrecuenteArea = new List<PreguntaFrecuenteArea>();
                    preguntaFrecuente.PreguntaFrecuenteSubArea = new List<PreguntaFrecuenteSubArea>();
                    preguntaFrecuente.PreguntaFrecuenteTipo = new List<PreguntaFrecuenteTipo>();

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuentePGenerals)
                    {
                        var preguntaFrecuentePgeneral = _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPorIdPreguntaFrecuenteYIdPGeneral(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id, item);
                        if (preguntaFrecuentePgeneral != null && preguntaFrecuentePgeneral.Id > 0)
                        {
                            preguntaFrecuentePgeneral.IdPgeneral = item;
                            preguntaFrecuentePgeneral.UsuarioModificacion = usuario;
                            preguntaFrecuentePgeneral.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            preguntaFrecuentePgeneral = new PreguntaFrecuentePGeneral();
                            preguntaFrecuentePgeneral.IdPgeneral = item;
                            preguntaFrecuentePgeneral.UsuarioCreacion = usuario;
                            preguntaFrecuentePgeneral.UsuarioModificacion = usuario;
                            preguntaFrecuentePgeneral.FechaCreacion = DateTime.Now;
                            preguntaFrecuentePgeneral.FechaModificacion = DateTime.Now;
                            preguntaFrecuentePgeneral.Estado = true;
                        }
                        preguntaFrecuente.PreguntaFrecuentePgeneral.Add(preguntaFrecuentePgeneral);
                    }

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuenteAreas)
                    {
                        var preguntaFrecuenteArea = _unitOfWork.PreguntaFrecuenteAreaRepository.ObtenerPorIdPreguntaFrecuenteYIdArea(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id, item);
                        if (preguntaFrecuenteArea != null && preguntaFrecuenteArea.Id > 0)
                        {
                            preguntaFrecuenteArea.IdArea = item;
                            preguntaFrecuenteArea.UsuarioModificacion = usuario;
                            preguntaFrecuenteArea.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            preguntaFrecuenteArea = new PreguntaFrecuenteArea();
                            preguntaFrecuenteArea.IdArea = item;
                            preguntaFrecuenteArea.UsuarioCreacion = usuario;
                            preguntaFrecuenteArea.UsuarioModificacion = usuario;
                            preguntaFrecuenteArea.FechaCreacion = DateTime.Now;
                            preguntaFrecuenteArea.FechaModificacion = DateTime.Now;
                            preguntaFrecuenteArea.Estado = true;
                        }
                        preguntaFrecuente.PreguntaFrecuenteArea.Add(preguntaFrecuenteArea);
                    }

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuenteSubAreas)
                    {
                        var preguntaFrecuenteSubArea = _unitOfWork.PreguntaFrecuenteSubAreaRepository.ObtenerPorIdPreguntaFrecuenteYIdSubArea(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id, item);
                        if (preguntaFrecuenteSubArea != null && preguntaFrecuenteSubArea.Id > 0)
                        {
                            preguntaFrecuenteSubArea.IdSubArea = item;
                            preguntaFrecuenteSubArea.UsuarioModificacion = usuario;
                            preguntaFrecuenteSubArea.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            preguntaFrecuenteSubArea = new PreguntaFrecuenteSubArea();
                            preguntaFrecuenteSubArea.IdSubArea = item;
                            preguntaFrecuenteSubArea.UsuarioCreacion = usuario;
                            preguntaFrecuenteSubArea.UsuarioModificacion = usuario;
                            preguntaFrecuenteSubArea.FechaCreacion = DateTime.Now;
                            preguntaFrecuenteSubArea.FechaModificacion = DateTime.Now;
                            preguntaFrecuenteSubArea.Estado = true;
                        }
                        preguntaFrecuente.PreguntaFrecuenteSubArea.Add(preguntaFrecuenteSubArea);
                    }

                    foreach (var item in preguntaFrecuenteParametrosDTO.PreguntaFrecuenteTipos)
                    {
                        var preguntaFrecuenteTipo = _unitOfWork.PreguntaFrecuenteTipoRepository.ObtenerPorIdPreguntaFrecuenteYIdTipo(preguntaFrecuenteParametrosDTO.PreguntaFrecuente.Id, item);
                        if (preguntaFrecuenteTipo != null && preguntaFrecuenteTipo.Id > 0)
                        {
                            preguntaFrecuenteTipo.IdTipo = item;
                            preguntaFrecuenteTipo.UsuarioModificacion = usuario;
                            preguntaFrecuenteTipo.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            preguntaFrecuenteTipo = new PreguntaFrecuenteTipo();
                            preguntaFrecuenteTipo.IdTipo = item;
                            preguntaFrecuenteTipo.UsuarioCreacion = usuario;
                            preguntaFrecuenteTipo.UsuarioModificacion = usuario;
                            preguntaFrecuenteTipo.FechaCreacion = DateTime.Now;
                            preguntaFrecuenteTipo.FechaModificacion = DateTime.Now;
                            preguntaFrecuenteTipo.Estado = true;
                        }
                        preguntaFrecuente.PreguntaFrecuenteTipo.Add(preguntaFrecuenteTipo);
                    }
                    var entidadActualizada = _unitOfWork.PreguntaFrecuenteRepository.Update(preguntaFrecuente);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return Obtener();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 22/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica a la tabla y sus detalles
        /// </summary>
        /// <param name="id"> (PK) de la tabla </param>
        /// <param name="usuario"> Autor de la eliminacion </param>
        /// <returns> bool  </returns>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (_unitOfWork.PreguntaFrecuenteRepository.Exist(id))
                {
                    _unitOfWork.PreguntaFrecuenteRepository.Delete(id, usuario);

                    var preguntaFrecuentePGenerals = _unitOfWork.PreguntaFrecuentePGeneralRepository.ObtenerPorIdPreguntaFrecuente(id);
                    _unitOfWork.PreguntaFrecuentePGeneralRepository.Delete(preguntaFrecuentePGenerals.Select(x => x.Id), usuario);

                    var preguntaFrecuenteAreas = _unitOfWork.PreguntaFrecuenteAreaRepository.ObtenerPorIdPreguntaFrecuente(id);
                    _unitOfWork.PreguntaFrecuenteAreaRepository.Delete(preguntaFrecuenteAreas.Select(x => x.Id), usuario);

                    var preguntaFrecuenteSubAreas = _unitOfWork.PreguntaFrecuenteSubAreaRepository.ObtenerPorIdPreguntaFrecuente(id);
                    _unitOfWork.PreguntaFrecuenteSubAreaRepository.Delete(preguntaFrecuenteSubAreas.Select(x => x.Id), usuario);

                    var preguntaFrecuenteTipos = _unitOfWork.PreguntaFrecuenteTipoRepository.ObtenerPorIdPreguntaFrecuente(id);
                    _unitOfWork.PreguntaFrecuenteTipoRepository.Delete(preguntaFrecuenteTipos.Select(x => x.Id), usuario);

                    _unitOfWork.Commit();
                    return (true);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}











