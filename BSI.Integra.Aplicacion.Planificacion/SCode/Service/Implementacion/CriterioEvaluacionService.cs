using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Mono.Unix;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Twilio.TwiML.Voice;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: CriterioEvaluacionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/05/2023
    /// <summary>
    /// Gestión general de T_CriterioEvaluacion
    /// </summary>
    public class CriterioEvaluacionService : ICriterioEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public CriterioEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacion, CriterioEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacion, CriterioEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ParametroEvaluacion, ParametroEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionTipoPrograma, CriterioEvaluacionTipoProgramaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionTipoPersona, CriterioEvaluacionTipoPersonaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionModalidadCurso, CriterioEvaluacionModalidadCursoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/05/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla CriterioEvaluacion y sus tablas detalles
        /// </summary>
        /// <returns> CriterioEvaluacionDTO </returns>  
        public CriterioEvaluacionDTO InsertarCriterio(CriterioEvaluacionDTO criterioEvaluacionDTO, string usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    CriterioEvaluacion criterioEvaluacion = new CriterioEvaluacion();

                    criterioEvaluacion.Nombre = criterioEvaluacionDTO.Nombre;
                    criterioEvaluacion.IdCriterioEvaluacionCategoria = criterioEvaluacionDTO.IdCriterioEvaluacionCategoria;
                    criterioEvaluacion.IdFormaCalificacionEvaluacion = criterioEvaluacionDTO.IdFormaCalificacionEvaluacion;
                    criterioEvaluacion.IdFormaCalculoEvaluacion = criterioEvaluacionDTO.IdFormaCalculoEvaluacion;
                    criterioEvaluacion.IdFormaCalculoEvaluacionParametro = criterioEvaluacionDTO.IdFormaCalculoEvaluacionParametro;
                    criterioEvaluacion.UsuarioCreacion = usuario;
                    criterioEvaluacion.UsuarioModificacion = usuario;
                    criterioEvaluacion.FechaCreacion = DateTime.Now;
                    criterioEvaluacion.FechaModificacion = DateTime.Now;
                    criterioEvaluacion.Estado = true;
                    criterioEvaluacion.CriterioEvaluacionTipoPrograma = new List<CriterioEvaluacionTipoPrograma>();
                    criterioEvaluacion.CriterioEvaluacionModalidadCurso = new List<CriterioEvaluacionModalidadCurso>();
                    criterioEvaluacion.CriterioEvaluacionTipoPersona = new List<CriterioEvaluacionTipoPersona>();
                    //añade la lista de parametros
                    if (criterioEvaluacionDTO.ParametroEvaluacion != null && criterioEvaluacionDTO.ParametroEvaluacion.Count > 0)
                    {
                        criterioEvaluacion.ParametroEvaluacion = new List<ParametroEvaluacion>();
                        criterioEvaluacion.ParametroEvaluacion.AddRange(criterioEvaluacionDTO.ParametroEvaluacion.Select(s =>
                            new ParametroEvaluacion()
                            {
                                IdEscalaCalificacion = s.IdEscalaCalificacion,
                                Nombre = s.Nombre,
                                Ponderacion = s.Ponderacion,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }));
                    }
                    //Añade la lista CriterioEvaluacionTipoPrograma
                    foreach (var item in criterioEvaluacionDTO.CriterioEvaluacionTipoPrograma)
                    {
                        CriterioEvaluacionTipoPrograma tipoprograma = new CriterioEvaluacionTipoPrograma();
                        tipoprograma.IdTipoPrograma = item.IdTipoPrograma;
                        tipoprograma.UsuarioCreacion = usuario;
                        tipoprograma.UsuarioModificacion = usuario;
                        tipoprograma.FechaCreacion = DateTime.Now;
                        tipoprograma.FechaModificacion = DateTime.Now;
                        tipoprograma.Estado = true;
                        criterioEvaluacion.CriterioEvaluacionTipoPrograma.Add(tipoprograma);
                    }
                    //Añade la lista CriterioEvaluacionModalidadCurso
                    foreach (var modalidad in criterioEvaluacionDTO.CriterioEvaluacionModalidadCurso)
                    {
                        CriterioEvaluacionModalidadCurso modalidadcurso = new CriterioEvaluacionModalidadCurso();
                        modalidadcurso.IdModalidadCurso = modalidad.IdModalidadCurso;
                        modalidadcurso.UsuarioCreacion = usuario;
                        modalidadcurso.UsuarioModificacion = usuario;
                        modalidadcurso.FechaCreacion = DateTime.Now;
                        modalidadcurso.FechaModificacion = DateTime.Now;
                        modalidadcurso.Estado = true;
                        criterioEvaluacion.CriterioEvaluacionModalidadCurso.Add(modalidadcurso);
                    }
                    //Añade la lista CriterioEvaluacionTipoPersona
                    foreach (var tipoPersona in criterioEvaluacionDTO.CriterioEvaluacionTipoPersona)
                    {
                        CriterioEvaluacionTipoPersona tipoPersonaBO = new CriterioEvaluacionTipoPersona();
                        tipoPersonaBO.IdTipoPersona = tipoPersona.IdTipoPersona;
                        tipoPersonaBO.UsuarioCreacion = usuario;
                        tipoPersonaBO.UsuarioModificacion = usuario;
                        tipoPersonaBO.FechaCreacion = DateTime.Now;
                        tipoPersonaBO.FechaModificacion = DateTime.Now;
                        tipoPersonaBO.Estado = true;
                        criterioEvaluacion.CriterioEvaluacionTipoPersona.Add(tipoPersonaBO);
                    }
                    var retorno = _mapper.Map<CriterioEvaluacionDTO>(_unitOfWork.CriterioEvaluacionRepository.Add(criterioEvaluacion));
                    _unitOfWork.Commit();
                    scope.Complete();
                    return (retorno);
                }
            }
            catch (Exception ex) { throw new Exception($"Error en insertar Criterio Evaluacion: {ex.Message}"); }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una actualizacion en la tabla CriterioEvaluacion y sus tablas detalles
        /// </summary>
        /// <param name="criterioEvaluacionDTO"> Objeto Criterio evaluacion y sus detalles </param>
        /// <param name="usuario"> Usuario que modifica al alumno </param> 
        /// <returns> CriterioEvaluacionDTO </returns>  
        public CriterioEvaluacionDTO ActualizarCriterio(CriterioEvaluacionDTO criterioEvaluacionDTO, string usuario)
        {
            try
            {

                ICriterioEvaluacionTipoProgramaService RepTipoPrograma = new CriterioEvaluacionTipoProgramaService(_unitOfWork);
                ICriterioEvaluacionModalidadCursoService RepModalidadCurso = new CriterioEvaluacionModalidadCursoService(_unitOfWork);
                ICriterioEvaluacionTipoPersonaService RepTipoPersona = new CriterioEvaluacionTipoPersonaService(_unitOfWork);
                CriterioEvaluacion criterio = new CriterioEvaluacion();

                RepTipoPrograma.EliminacionLogicaPorCriterioEvaluacion(criterioEvaluacionDTO.Id.Value, usuario, criterioEvaluacionDTO.CriterioEvaluacionTipoPrograma.Select(x => x.IdTipoPrograma).ToList());
                RepModalidadCurso.EliminacionLogicaPorCriterioEvaluacion(criterioEvaluacionDTO.Id.Value, usuario, criterioEvaluacionDTO.CriterioEvaluacionModalidadCurso.Select(x => x.IdModalidadCurso).ToList());
                RepTipoPersona.EliminacionLogicaPorCriterioEvaluacion(criterioEvaluacionDTO.Id.Value, usuario, criterioEvaluacionDTO.CriterioEvaluacionTipoPersona.Select(x => x.IdTipoPersona).ToList());

                if (_unitOfWork.CriterioEvaluacionRepository.Exist(criterioEvaluacionDTO.Id.Value))
                {
                    criterio = _unitOfWork.CriterioEvaluacionRepository.ObtenerPorId(criterioEvaluacionDTO.Id.Value);
                    criterio.Nombre = criterioEvaluacionDTO.Nombre;
                    criterio.IdCriterioEvaluacionCategoria = criterioEvaluacionDTO.IdCriterioEvaluacionCategoria;
                    criterio.IdFormaCalificacionEvaluacion = criterioEvaluacionDTO.IdFormaCalificacionEvaluacion;
                    criterio.IdFormaCalculoEvaluacion = criterioEvaluacionDTO.IdFormaCalculoEvaluacion;
                    criterio.IdFormaCalculoEvaluacionParametro = criterioEvaluacionDTO.IdFormaCalculoEvaluacionParametro;
                    criterio.UsuarioModificacion = usuario;
                    criterio.FechaModificacion = DateTime.Now;


                    var listadoParametroExistente = _unitOfWork.ParametroEvaluacionRepository.ObtenerPorIdCriterioEvaluacion(criterioEvaluacionDTO.Id.Value);

                    List<int> listadoIdParametroExistente = new List<int>();
                    var listadoEliminar = new List<int>();

                    //añade la lista de detalles
                    if (criterioEvaluacionDTO.ParametroEvaluacion != null && criterioEvaluacionDTO.ParametroEvaluacion.Count > 0)
                    {
                        criterio.ParametroEvaluacion = new List<ParametroEvaluacion>();
                        listadoIdParametroExistente = listadoParametroExistente.Select(s => s.Id).ToList();

                        var listadoNuevo = new List<ParametroEvaluacion>();
                        var listadoActualizar = new List<ParametroEvaluacion>();

                        listadoNuevo.AddRange(criterioEvaluacionDTO.ParametroEvaluacion.Where(w => w.Id == null || w.Id == 0).Select(s =>
                            new ParametroEvaluacion()
                            {
                                IdEscalaCalificacion = s.IdEscalaCalificacion,
                                Nombre = s.Nombre,
                                Ponderacion = s.Ponderacion,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            }));
                        foreach (var detalleExistente in listadoParametroExistente.Where(w => criterioEvaluacionDTO.ParametroEvaluacion.Select(s => s.Id).Contains(w.Id)))
                        {
                            var itemActualizado = criterioEvaluacionDTO.ParametroEvaluacion.FirstOrDefault(f => f.Id == detalleExistente.Id);

                            detalleExistente.IdEscalaCalificacion = itemActualizado.IdEscalaCalificacion;
                            detalleExistente.Nombre = itemActualizado.Nombre;
                            detalleExistente.Ponderacion = itemActualizado.Ponderacion;
                            detalleExistente.UsuarioModificacion = usuario;
                            detalleExistente.FechaModificacion = DateTime.Now;

                            listadoActualizar.Add(detalleExistente);
                        }

                        criterio.ParametroEvaluacion.AddRange(listadoNuevo);
                        criterio.ParametroEvaluacion.AddRange(listadoActualizar);

                    }
                    if (listadoIdParametroExistente != null && listadoIdParametroExistente.Count > 0)
                    {
                        listadoEliminar.AddRange(listadoIdParametroExistente.Where(w =>
                            !criterioEvaluacionDTO.ParametroEvaluacion.Select(s => s.Id).Contains(w)));
                    }

                    _unitOfWork.ParametroEvaluacionRepository.Delete(listadoEliminar, usuario);
                }

                criterio.CriterioEvaluacionTipoPrograma = new List<CriterioEvaluacionTipoPrograma>();
                foreach (var tipo in criterioEvaluacionDTO.CriterioEvaluacionTipoPrograma)
                {
                    CriterioEvaluacionTipoPrograma tipoprograma;
                    tipoprograma = _unitOfWork.CriterioEvaluacionTipoProgramaRepository.ObtenerPorIdTipoProgramaYIdCriterioEvaluacion(tipo.IdTipoPrograma, criterio.Id);
                    if (tipoprograma != null)
                    {
                        tipoprograma.IdTipoPrograma = tipo.IdTipoPrograma;
                        tipoprograma.UsuarioModificacion = usuario;
                        tipoprograma.FechaModificacion = DateTime.Now;
                        tipoprograma.Estado = true;
                    }
                    else
                    {
                        tipoprograma = new CriterioEvaluacionTipoPrograma();
                        tipoprograma.IdTipoPrograma = tipo.IdTipoPrograma;
                        tipoprograma.UsuarioCreacion = usuario;
                        tipoprograma.UsuarioModificacion = usuario;
                        tipoprograma.FechaCreacion = DateTime.Now;
                        tipoprograma.FechaModificacion = DateTime.Now;
                        tipoprograma.Estado = true;

                    }
                    criterio.CriterioEvaluacionTipoPrograma.Add(tipoprograma);

                }
                criterio.CriterioEvaluacionModalidadCurso = new List<CriterioEvaluacionModalidadCurso>();
                foreach (var mo in criterioEvaluacionDTO.CriterioEvaluacionModalidadCurso)
                {
                    CriterioEvaluacionModalidadCurso modalidad;
                    modalidad = _unitOfWork.CriterioEvaluacionModalidadCursoRepository.ObtenerPorIdModalidadCursoYIdCriterioEvaluacion(criterio.Id, mo.IdModalidadCurso);
                    if (modalidad != null)
                    {
                        modalidad.IdModalidadCurso = mo.IdModalidadCurso;
                        modalidad.UsuarioModificacion = usuario;
                        modalidad.FechaModificacion = DateTime.Now;
                        modalidad.Estado = true;
                    }
                    else
                    {
                        modalidad = new CriterioEvaluacionModalidadCurso();
                        modalidad.IdModalidadCurso = mo.IdModalidadCurso;
                        modalidad.UsuarioCreacion = usuario;
                        modalidad.UsuarioModificacion = usuario;
                        modalidad.FechaCreacion = DateTime.Now;
                        modalidad.FechaModificacion = DateTime.Now;
                        modalidad.Estado = true;
                    }
                    criterio.CriterioEvaluacionModalidadCurso.Add(modalidad);

                }
                criterio.CriterioEvaluacionTipoPersona = new List<CriterioEvaluacionTipoPersona>();
                foreach (var mo in criterioEvaluacionDTO.CriterioEvaluacionTipoPersona)
                {
                    CriterioEvaluacionTipoPersona tipoPersona;
                    tipoPersona = _unitOfWork.CriterioEvaluacionTipoPersonaRepository.ObtenerPorIdTipoPersonaYIdCriterioEvaluacion(mo.IdTipoPersona, criterio.Id);
                    if (tipoPersona != null)
                    {
                        tipoPersona.IdTipoPersona = mo.IdTipoPersona;
                        tipoPersona.UsuarioModificacion = usuario;
                        tipoPersona.FechaModificacion = DateTime.Now;
                        tipoPersona.Estado = true;
                    }
                    else
                    {
                        tipoPersona = new CriterioEvaluacionTipoPersona();
                        tipoPersona.IdTipoPersona = mo.IdTipoPersona;
                        tipoPersona.UsuarioCreacion = usuario;
                        tipoPersona.UsuarioModificacion = usuario;
                        tipoPersona.FechaCreacion = DateTime.Now;
                        tipoPersona.FechaModificacion = DateTime.Now;
                        tipoPersona.Estado = true;
                    }
                    criterio.CriterioEvaluacionTipoPersona.Add(tipoPersona);

                }
                _unitOfWork.CriterioEvaluacionRepository.Update(criterio);
                _unitOfWork.Commit();

                var resultado = _mapper.Map<CriterioEvaluacionDTO>(criterio);
                return (resultado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en insertar Criterio Evaluacion: {ex.Message}");
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica a la tabla CriterioEvaluacion y sus tablas detalles
        /// </summary>
        /// <returns> bool </returns>  
        public bool EliminarCriterio(int id, string usuario)
        {
            bool resultado = false;
            if (_unitOfWork.CriterioEvaluacionRepository.Exist(id))
            {
                resultado = _unitOfWork.CriterioEvaluacionRepository.Delete(id, usuario);
                /*Eliminamos los registros detalles del Criterio Evaluacion*/
                if (resultado)
                {
                    _unitOfWork.CriterioEvaluacionRepository.EliminarDetalles(id);
                    _unitOfWork.Commit();
                }
            }
            return resultado;
        }
        /// Autor: Gretel Canasa
        /// Fecha: 12/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos
        /// </summary>
        /// <returns> List<ComboDTO> </returns> 
        public async Task<CriterioEvaluacionComboDTO> ObtenerCombosModulo()
        {
            try
            {
                var task_tipoPersona = _unitOfWork.TipoPersonaRepository.ObtenerComboPorIdsAsync(new List<int> { 1, 3 });
                var task_modalidad = _unitOfWork.ModalidadCursoRepository.ObtenerComboAsync();
                var task_criterioEvaluacionCategorium = _unitOfWork.CriterioEvaluacionCategoriumRepository.ObtenerComboAsync();
                var task_escalaEvaluacion = _unitOfWork.EscalaCalificacionRepository.ObtenerComboAsync();
                var task_formaCalculoEvaluacion = _unitOfWork.FormaCalculoEvaluacionRepository.ObtenerComboAsync();
                var task_formaCalificacion = _unitOfWork.FormaCalificacionEvaluacionRepository.ObtenerComboAsync();
                var task_tipoPrograma = _unitOfWork.TipoProgramaRepository.ObtenerComboAsync();

                CriterioEvaluacionComboDTO resultado = new CriterioEvaluacionComboDTO()
                {
                    TipoPersona = await task_tipoPersona,
                    ModalidadCurso = await task_modalidad,
                    CriterioEvaluacionCategorium = await task_criterioEvaluacionCategorium,
                    EscalaCalificacion = await task_escalaEvaluacion,
                    FormaCalculoEvaluacion = await task_formaCalculoEvaluacion,
                    FormaCalificacionEvaluacion = await task_formaCalificacion,
                    TipoPrograma = await task_tipoPrograma,
                };
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombos(): {ex.Message}");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_CriterioEvaluacion
        /// </summary>
        /// <returns> List<CriterioEvaluacionDTO> </returns>
        public List<CriterioEvaluacionDTO> ObtenerCriteriosEvaluacion()
        {
            try
            {
                var resultado = _unitOfWork.CriterioEvaluacionRepository.ObtenerCriteriosEvaluacion();

                foreach (var item in resultado)
                {
                    item.CriterioEvaluacionTipoPrograma = _unitOfWork.CriterioEvaluacionTipoProgramaRepository.ObtenerPorIdCriterioEvaluacion(item.Id.Value);
                    item.CriterioEvaluacionModalidadCurso = _unitOfWork.CriterioEvaluacionModalidadCursoRepository.ObtenerPorIdCriterioEvaluacion(item.Id.Value);
                    item.CriterioEvaluacionTipoPersona = _unitOfWork.CriterioEvaluacionTipoPersonaRepository.ObtenerPorIdCriterioEvaluacion(item.Id.Value);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ListarCriteriosEvaluacion(): {ex.Message}");
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con los detalles de T_CriterioEvaluacion
        /// </summary>
        /// <returns> List<CriterioEvaluacionDTO> </returns>
        public CriterioEvaluacionDTO ObtenerCriterioEvaluacionPorId(int idCriterioEvaluacion)
        {
            try
            {
                var resultado = _unitOfWork.CriterioEvaluacionRepository.ObtenerCriterioEvaluacionPorId(idCriterioEvaluacion);
                resultado.CriterioEvaluacionTipoPrograma = _unitOfWork.CriterioEvaluacionTipoProgramaRepository.ObtenerPorIdCriterioEvaluacion(resultado.Id.Value);
                resultado.CriterioEvaluacionModalidadCurso = _unitOfWork.CriterioEvaluacionModalidadCursoRepository.ObtenerPorIdCriterioEvaluacion(resultado.Id.Value);
                resultado.CriterioEvaluacionTipoPersona = _unitOfWork.CriterioEvaluacionTipoPersonaRepository.ObtenerPorIdCriterioEvaluacion(resultado.Id.Value);
                var parametros = _unitOfWork.ParametroEvaluacionRepository.ObtenerPorIdCriterioEvaluacion(resultado.Id.Value);
                resultado.ParametroEvaluacion = _mapper.Map<List<ParametroEvaluacionDTO>>(parametros);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ListarCriteriosEvaluacion(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos de criterio evaluacion
        /// </summary>
        /// <returns> Lista de ComboDTO </returns> 
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CriterioEvaluacionRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ComboDTO> ObtenerCriterio(int tipoprograma, int modalidadprograma)
        {
            return _unitOfWork.CriterioEvaluacionRepository.ObtenerCriterio(tipoprograma, modalidadprograma);
        }
    }
}
