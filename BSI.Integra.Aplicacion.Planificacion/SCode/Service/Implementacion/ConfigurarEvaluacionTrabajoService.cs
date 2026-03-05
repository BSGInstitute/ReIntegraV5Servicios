using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ConfigurarEvaluacionTrabajoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de TconfigurarEvaluacionTrabajo
    /// </summary>
    public class ConfigurarEvaluacionTrabajoService : IConfigurarEvaluacionTrabajoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfigurarEvaluacionTrabajoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    //cfg.CreateMap<TConfigurarEvaluacionTrabajo, ConfigurarEvaluacionTrabajo>(MemberList.None).ReverseMap();  
                }
              );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica con detalles
        /// </summary>
        /// <param name="configurarEvaluacionTrabajoDTO"> Nuevo Objeto ConfigurarEvaluacionTrabajo y detalles </param>
        /// <returns> bool </returns>
        public bool Insertar(ConfigurarEvaluacionTrabajoDTO configurarEvaluacionTrabajoDTO, string usuario)
        {
            try
            {
                ConfigurarEvaluacionTrabajo configurarEvaluacion = new ConfigurarEvaluacionTrabajo
                {
                    IdTipoEvaluacionTrabajo = configurarEvaluacionTrabajoDTO.IdTipoEvaluacionTrabajo,
                    Nombre = configurarEvaluacionTrabajoDTO.Nombre,
                    Descripcion = configurarEvaluacionTrabajoDTO.Descripcion,
                    IdDocumentoPw = configurarEvaluacionTrabajoDTO.IdDocumentoPw,
                    ArchivoNombre = configurarEvaluacionTrabajoDTO.ArchivoNombre,
                    ArchivoCarpeta = configurarEvaluacionTrabajoDTO.ArchivoCarpeta,
                    IdPgeneral = configurarEvaluacionTrabajoDTO.IdPgeneral,
                    IdSeccion = configurarEvaluacionTrabajoDTO.IdSeccion,
                    Fila = configurarEvaluacionTrabajoDTO.Fila,
                    DescripcionPregunta = configurarEvaluacionTrabajoDTO.DescripcionPregunta,
                    OrdenCapitulo = configurarEvaluacionTrabajoDTO.OrdenCapitulo,
                    HabilitarInstrucciones = configurarEvaluacionTrabajoDTO.HabilitarInstrucciones,
                    HabilitarArchivo = configurarEvaluacionTrabajoDTO.HabilitarArchivo,
                    HabilitarPreguntas = configurarEvaluacionTrabajoDTO.HabilitarPreguntas,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                configurarEvaluacion.PreguntaEvaluacionTrabajos = new List<PreguntaEvaluacionTrabajo>();
                foreach (var item in configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos)
                {
                    PreguntaEvaluacionTrabajo preguntaEvaluacionTrabajo = new PreguntaEvaluacionTrabajo
                    {
                        IdPregunta = item.IdPregunta,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    configurarEvaluacion.PreguntaEvaluacionTrabajos.Add(preguntaEvaluacionTrabajo);
                }

                var resultado = _unitOfWork.ConfigurarEvaluacionTrabajoRepository.Add(configurarEvaluacion);
                _unitOfWork.Commit();
                //return true;
                if (configurarEvaluacionTrabajoDTO.criterioTareas != null && configurarEvaluacionTrabajoDTO.criterioTareas.Count > 0) {
                    foreach (var criterio in configurarEvaluacionTrabajoDTO.criterioTareas) {
                        _unitOfWork.ConfigurarEvaluacionTrabajoRepository.InsertarCriterioConfiguracion(resultado.Id, criterio.idCriterioTarea, usuario);
                    }
                }
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica con detalles
        /// </summary>
        /// <param name="configurarEvaluacionTrabajoDTO"> Nuevo Objeto ConfigurarEvaluacionTrabajo y detalles </param>
        /// <returns> bool </returns>
        public bool Actualizar(ConfigurarEvaluacionTrabajoDTO configurarEvaluacionTrabajoDTO, string usuario)
        {
            var configurarEvaluacionTrabajo = _unitOfWork.ConfigurarEvaluacionTrabajoRepository.ObtenerPorId(configurarEvaluacionTrabajoDTO.Id.Value);
            if (configurarEvaluacionTrabajo != null && configurarEvaluacionTrabajo.Id > 0)
            {
                configurarEvaluacionTrabajo.IdTipoEvaluacionTrabajo = configurarEvaluacionTrabajoDTO.IdTipoEvaluacionTrabajo;
                configurarEvaluacionTrabajo.Nombre = configurarEvaluacionTrabajoDTO.Nombre;
                configurarEvaluacionTrabajo.Descripcion = configurarEvaluacionTrabajoDTO.Descripcion;
                if (configurarEvaluacionTrabajoDTO.HabilitarInstrucciones.Value)
                    configurarEvaluacionTrabajo.IdDocumentoPw = configurarEvaluacionTrabajoDTO.IdDocumentoPw;
                else
                    configurarEvaluacionTrabajo.IdDocumentoPw = null;

                configurarEvaluacionTrabajo.ArchivoNombre = configurarEvaluacionTrabajoDTO.ArchivoNombre;
                configurarEvaluacionTrabajo.ArchivoCarpeta = configurarEvaluacionTrabajoDTO.ArchivoCarpeta;
                configurarEvaluacionTrabajo.DescripcionPregunta = configurarEvaluacionTrabajoDTO.DescripcionPregunta;
                configurarEvaluacionTrabajo.OrdenCapitulo = configurarEvaluacionTrabajoDTO.OrdenCapitulo;
                configurarEvaluacionTrabajo.HabilitarInstrucciones = configurarEvaluacionTrabajoDTO.HabilitarInstrucciones;
                configurarEvaluacionTrabajo.HabilitarArchivo = configurarEvaluacionTrabajoDTO.HabilitarArchivo;
                configurarEvaluacionTrabajo.HabilitarPreguntas = configurarEvaluacionTrabajoDTO.HabilitarPreguntas;
                configurarEvaluacionTrabajo.Estado = true;
                configurarEvaluacionTrabajo.FechaModificacion = DateTime.Now;
                configurarEvaluacionTrabajo.UsuarioModificacion = usuario;

                configurarEvaluacionTrabajo.PreguntaEvaluacionTrabajos = new List<PreguntaEvaluacionTrabajo>();

                if (configurarEvaluacionTrabajo.HabilitarPreguntas.Value)
                {
                    if (configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos != null && configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos.Count > 0)
                    {
                        var preguntaEvaluacionTrabajos = _unitOfWork.PreguntaEvaluacionTrabajoRepository.ObtenerPorIdConfigurarEvaluacionTrabajo(configurarEvaluacionTrabajo.Id).ToList();
                        preguntaEvaluacionTrabajos.RemoveAll(x => configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos.Any(y => y.Id == x.Id));
                        _unitOfWork.PreguntaEvaluacionTrabajoRepository.Delete(preguntaEvaluacionTrabajos.Select(x => x.Id), usuario);
                    }

                    List<PreguntaEvaluacionTrabajo> _listaRegistros = new List<PreguntaEvaluacionTrabajo>();
                    foreach (var item in configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos)
                    {
                        if (item.Id == 0 || item.Id == null)
                        {
                            PreguntaEvaluacionTrabajo NuevaPregunta = new PreguntaEvaluacionTrabajo
                            {
                                IdPregunta = item.IdPregunta,
                                Estado = true,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            _listaRegistros.Add(NuevaPregunta);
                        }
                    }
                    configurarEvaluacionTrabajo.PreguntaEvaluacionTrabajos = _listaRegistros;
                }
                else
                {
                    if (configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos != null && configurarEvaluacionTrabajoDTO.PreguntaEvaluacionTrabajos.Count > 0)
                    {
                        var preguntaEvaluacionTrabajos = _unitOfWork.PreguntaEvaluacionTrabajoRepository.ObtenerPorIdConfigurarEvaluacionTrabajo(configurarEvaluacionTrabajo.Id).Select(x => x.Id);
                        _unitOfWork.PreguntaEvaluacionTrabajoRepository.Delete(preguntaEvaluacionTrabajos, usuario);
                    }
                }
                _unitOfWork.ConfigurarEvaluacionTrabajoRepository.Update(configurarEvaluacionTrabajo);
                _unitOfWork.Commit();

                _unitOfWork.ConfigurarEvaluacionTrabajoRepository.EliminarCriteriosPorConfiguracion(configurarEvaluacionTrabajo.Id, usuario);

                if (configurarEvaluacionTrabajoDTO.criterioTareas != null && configurarEvaluacionTrabajoDTO.criterioTareas.Count > 0) {
                    foreach (var criterio in configurarEvaluacionTrabajoDTO.criterioTareas)
                    {
                        _unitOfWork.ConfigurarEvaluacionTrabajoRepository.InsertarCriterioConfiguracion(configurarEvaluacionTrabajo.Id, criterio.idCriterioTarea, usuario);
                    }
                }
                

                return true;
            }
            else return false;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica con detalles
        /// </summary>
        /// <param name="id"> (PK) Primary Key </param>
        /// <param name="usuario"> usuario integra </param>
        /// <returns> bool </returns>
        public bool Eliminar(int id, string usuario)
        {
            bool respuesta = false;
            var configurarEvaluacionTrabajo = _unitOfWork.ConfigurarEvaluacionTrabajoRepository.ObtenerPorId(id);
            using (TransactionScope scope = new TransactionScope())
            {
                respuesta = _unitOfWork.ConfigurarEvaluacionTrabajoRepository.Delete(configurarEvaluacionTrabajo.Id, usuario);
                _unitOfWork.Commit();
                scope.Complete();
            }
            return (respuesta);
        }

        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// <summary>
        /// Obtiene pregunta evaluacion por configuracion
        /// </summary>
        /// <param name="idConfigurarEvaluacionTrabajo"></param>
        /// <returns>idConfigurarEvaluacionTrabajo, IdConfigurarEvaluacionTrabajo</returns>
        public List<PreguntaEvaluacionTrabajoDTO> ObtenerPorConfiguracion(int idConfigurarEvaluacionTrabajo)
        {
            return _unitOfWork.PreguntaEvaluacionTrabajoRepository.ObtenerPorIdConfigurarEvaluacionTrabajo(idConfigurarEvaluacionTrabajo)
                .Select(x => new PreguntaEvaluacionTrabajoDTO()
                {
                    IdConfigurarEvaluacionTrabajo = x.IdConfigurarEvaluacionTrabajo,
                    IdPregunta = x.IdPregunta
                }).ToList();
        }
    }
}
