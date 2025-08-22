using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;
using OfficeOpenXml.ConditionalFormatting;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PEspecificoMatriculaAlumnoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 12/11/2022
    /// <summary>
    /// Gestión general de T_PEspecificoMatriculaAlumno
    /// </summary>
    public class PEspecificoMatriculaAlumnoRepository : GenericRepository<TPespecificoMatriculaAlumno>, IPEspecificoMatriculaAlumnoRepository
    {
        private Mapper _mapper;

        public PEspecificoMatriculaAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoMatriculaAlumno, PEspecificoMatriculaAlumno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// <summary>
        /// Obtiene los PEspecificos asociados a una matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerTodoFiltroAutoComplete(int idMatriculaCabecera)
        {
            try
            {
                var pEspecificoMatriculaAlumnoAgendaDTOs = new List<PEspecificoMatriculaAlumnoAgendaDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "SELECT Id, IdPEspecifico, Nombre, Tipo, TipoMatricula FROM ope.V_ObtenerPEspecifico_MatriculaAlumno WHERE Estado = 1 AND IdMatriculaCabecera = @IdMatriculaCabecera ";
                var lista = _dapperRepository.QueryDapper(_queryAlumnoFiltro, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    pEspecificoMatriculaAlumnoAgendaDTOs = JsonConvert.DeserializeObject<List<PEspecificoMatriculaAlumnoAgendaDTO>>(lista);
                }
                return pEspecificoMatriculaAlumnoAgendaDTOs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// <summary>
        /// Obtiene los PEspecificos asociados a una matricula
        /// </summary>
        /// <param name="idPEspecifico"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<PespecificoPadrePespecificoHijoDTO> ListaPespecificoPadrePespecificoHijo(int idPEspecifico)
        {
            try
            {
                List<PespecificoPadrePespecificoHijoDTO> listaPespecificoPadrePesepcificoHijo = new List<PespecificoPadrePespecificoHijoDTO>();
                var _query = "select Id, PEspecificoPadreId, PEspecificoHijoId from pla.T_PEspecificoPadrePEspecificoHijo where PEspecificoPadreId = @idPEspecifico and Estado = 1";
                var lista = _dapperRepository.QueryDapper(_query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    listaPespecificoPadrePesepcificoHijo = JsonConvert.DeserializeObject<List<PespecificoPadrePespecificoHijoDTO>>(lista);
                }
                return listaPespecificoPadrePesepcificoHijo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// <summary>
        /// Obtiene los PEspecificos asociados a una matricula
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public int? IdUsuarioMoodle(int idAlumno)
        {
            try
            {
                IntDTO IdUsuarioMoodle = new IntDTO();
                var _query = "select Valor from [ope].[V_Talumno_moodle_IdUsuarioMoodle] where id_alumno = @idAlumno";
                var id = _dapperRepository.FirstOrDefault(_query, new { idAlumno });
                if (!string.IsNullOrEmpty(id) && !id.Contains("null"))
                {
                    IdUsuarioMoodle = JsonConvert.DeserializeObject<IntDTO>(id);
                }
                return IdUsuarioMoodle.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 12/11/2022
        /// <summary>
        /// Obtiene los PEspecificos asociados a una matricula
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public int? IdCursoMoodle(int idEspecifico)
        {
            try
            {
                IntDTO IdCursoMoodle = new IntDTO();
                var _query = "select Valor from [pla].[V_PEspecificoIdCursoMoodle] where Id = @idEspecifico and Estado = 1";
                var id = _dapperRepository.FirstOrDefault(_query, new { idEspecifico });
                if (!string.IsNullOrEmpty(id) && !id.Contains("null"))
                {
                    IdCursoMoodle = JsonConvert.DeserializeObject<IntDTO>(id);
                }
                //IdCursoMoodle = JsonConvert.DeserializeObject<IntNullDTO>(id);
                return IdCursoMoodle.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PEspecificoCriterioDetalleEntregableDelAlumno> ObtenerCriterioDetalleEntregablesAlumno(int idCriterioEvaluacion, int idPEspecifico, int idMatriculaCabecera)
        {
            try
            {
                var idCriteriosPermitidos = new List<int>() { 4, 19,20,21 }; //Tareas, Cuestionarios, Cuestionarios Test y Actividades Adicionales son los unicos criterios que tienen data adicional de entregables para obtener
                if(idCriteriosPermitidos.Exists(x => x == idCriterioEvaluacion) == false)
                    throw new Exception("El criterio solicitado no cuenta con informacion adicional para sus entregables. Solo los criterios con Id: 4, 19, 20 o 21 son aceptados");

                var list = new List<PEspecificoCriterioDetalleEntregableDelAlumno>();
                var listaIdSesiones = new List<int>();
                var listaIdEntregables = new List<int>();

                //Obtenemos los Ids de las sesiones del curso.
                var query = "SELECT Id FROM pla.T_PEspecificoSesion WHERE IdPEspecifico = @idPEspecifico AND Estado = 1";
                var objects = _dapperRepository.QueryDapper(query, new { idPEspecifico });

                if (!string.IsNullOrEmpty(objects) && !objects.Contains("null"))
                {
                    var temp = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(objects);
                    listaIdSesiones = temp.Select(obj => obj["Id"]).ToList();
                }

                var queryPorCriterio = new Dictionary<int, string>
                {
                    //Tareas
                    { 4, "SELECT Id, Titulo, FechaEntrega, FechaEntregaSecundaria, CalificacionMaxima, CalificacionMaximaSecundaria FROM pw.T_PW_PEspecificoSesionTarea WHERE Estado = 1 AND Publicado = 1" },
                    //Cuestionario
                    { 19, "SELECT Id, Titulo, FechaEntrega, FechaEntregaSecundaria, CalificacionMaxima, CalificacionMaximaSecundaria FROM pw.T_PW_PEspecificoSesionCuestionario WHERE Estado = 1 AND IdCriterioEvaluacion = 19 AND Publicado = 1" },
                    //Cuestionario Test
                    { 20, "SELECT Id, Titulo, FechaEntrega, FechaEntregaSecundaria, CalificacionMaxima, CalificacionMaximaSecundaria FROM pw.T_PW_PEspecificoSesionCuestionario WHERE Estado = 1 AND IdCriterioEvaluacion = 20 AND Publicado = 1" },
                    //Actividades Adicionales
                    { 21, "SELECT Id, Titulo, FechaEntrega, FechaEntregaSecundaria, CalificacionMaxima, CalificacionMaximaSecundaria FROM pw.T_PW_PEspecificoSesionActividad WHERE Estado = 1 AND Publicado = 1" }
                };

                query = queryPorCriterio[idCriterioEvaluacion] + $" AND IdPEspecificoSesion IN ({string.Join(",",listaIdSesiones)}) ";
                objects = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(objects) && !objects.Contains("null"))
                {
                    var temp = JsonConvert.DeserializeObject<List<dynamic>>(objects);
                    
                    foreach(var entregable in temp){

                        listaIdEntregables.Add((int) entregable.Id);

                        var t = new PEspecificoCriterioDetalleEntregableDelAlumno();
                        t.IdMatriculaCabecera = idMatriculaCabecera;
                        t.IdPEspecifico = idPEspecifico;
                        t.IdCriterioEvaluacion = idCriterioEvaluacion;
                        t.IdSesionEntregable = entregable.Id;
                        t.Titulo = entregable.Titulo;
                        t.FechaProgramada =  entregable.FechaEntrega;
                        t.FechaProgramadaSecundaria = entregable.FechaEntregaSecundaria;
                        t.PuntajeMaximo = entregable.CalificacionMaxima;
                        t.PuntajeMaximoSecundario = entregable.CalificacionMaximaSecundaria;

                        list.Add(t);
                    }
                }

                if (listaIdEntregables.Count == 0)
                    return list;

                queryPorCriterio = new Dictionary<int, string>
                {
                    //Tareas
                    { 4, "SELECT IdPwPEspecificoSesionTarea as IdSesionEntregable, Nota, FechaCreacion FROM pw.T_PW_PEspecificoSesionTareaAlumno WHERE Estado = 1 AND IdPwPEspecificoSesionTarea IN" },
                    //Cuestionario
                    { 19, "SELECT IdPwPEspecificoSesionCuestionario as IdSesionEntregable, Nota, FechaCreacion FROM pw.T_PW_PEspecificoSesionCuestionarioAlumno WHERE Estado = 1 AND IdPwPEspecificoSesionCuestionario IN" },
                    //Cuestionario Test
                    { 20, "SELECT IdPwPEspecificoSesionCuestionario as IdSesionEntregable, Nota, FechaCreacion FROM pw.T_PW_PEspecificoSesionCuestionarioAlumno WHERE Estado = 1 AND IdPwPEspecificoSesionCuestionario IN" },
                    //Actividades Adicionales
                    { 21, "SELECT IdPwPEspecificoSesionActividad as IdSesionEntregable, Nota, FechaCreacion FROM pw.T_PW_PEspecificoSesionActividadAlumno WHERE Estado = 1 AND IdPwPEspecificoSesionActividad IN" }
                };

                query = queryPorCriterio[idCriterioEvaluacion] + $" ({string.Join(",", listaIdEntregables)}) AND IdMatriculaCabecera = {idMatriculaCabecera}";
                objects = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(objects) && !objects.Contains("null"))
                {
                    var temp = JsonConvert.DeserializeObject<List<dynamic>>(objects);

                    foreach (var entregableDetalle in temp)
                    {
                        var t = list.Find(x => x.IdSesionEntregable == (int)entregableDetalle.IdSesionEntregable);
                        if (t != null)
                        {
                            t.Calificacion = entregableDetalle.Nota;
                            t.FechaEntregada = entregableDetalle.FechaEntrega;
                        }
                    }

                    //Check entregado a tiempo
                    foreach (var detalle in list)
                    {
                        if (detalle.FechaEntregada != null)
                        {
                            detalle.EstadoEntrega = "Completado";
                        }
                        else
                        {
                            var t1flag = detalle.FechaProgramada != null;
                            var t2flag = detalle.FechaProgramadaSecundaria != null;

                            if ((t1flag && detalle.FechaProgramada >= DateTime.Now) || (t2flag && detalle.FechaProgramadaSecundaria >= DateTime.Now))
                                detalle.EstadoEntrega = "Pendiente";
                            else
                                detalle.EstadoEntrega = "No presentado";

                        }
                    }
                   

                }

                return list;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: ,Jashin Salazar
        /// Fecha: 13/09/2021
        /// Version: 2.0
        /// <summary>
        /// Verifica que el curso exista en la nueva aula virtual
        /// </summary>
        /// <param name="idPEspecifico">Id Matricula Moodle</param> 
        /// <returns>true / false</returns>
        public bool ExisteNuevaAulaVirtual(int idPEspecifico)
        {
            try
            {
                var query = "SELECT Id FROM [pla].[V_TPEspecificoNuevoAulaVirtual_DataBasica] WHERE Id = @idPEspecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPEspecifico });

                return !string.IsNullOrEmpty(resultado) && !resultado.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PEspecificoMatriculaAlumnoAgendaDTO> InsertarPEspecificoMatriculaAlumnoRepositorio(PEspecificoMatriculaAlumnoDTO pEspecificoMatriculaAlumnoDTO)
        {
            var data = new List<PEspecificoMatriculaAlumnoAgendaDTO>();
            return (data);
            //RespuestaWebDTO cronograma = new RespuestaWebDTO();
            //MoodleCronogramaEvaluacionBO objetoCongelarCronograma = new MoodleCronogramaEvaluacionBO();
            //MdlUser moodleUser = new MdlUser();
        }
        public void ActualizacionTipoMatriculaPEspecifico(int IdPEspecifico, int IdMatriculaCabecera)
        {
            try
            {
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "update ope.T_PEspecificoMatriculaAlumno set IdPEspecificoTipoMatricula = 4 where IdMatriculaCabecera=@IdMatriculaCabecera and IdPEspecifico = @IdPEspecifico ";
                var lista = _dapperRepository.QueryDapper(_queryAlumnoFiltro, new { IdPEspecifico, IdMatriculaCabecera });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private TPespecificoMatriculaAlumno MapeoEntidad(PEspecificoMatriculaAlumno objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPespecificoMatriculaAlumno entidad = new TPespecificoMatriculaAlumno();
                entidad = _mapper.Map<TPespecificoMatriculaAlumno>(objetoBO);
                //entidad = Mapper.Map<PEspecificoMatriculaAlumno, TPespecificoMatriculaAlumno>(objetoBO,
                //    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private void AsignacionId(TPespecificoMatriculaAlumno entidad, PEspecificoMatriculaAlumno objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TPespecificoMatriculaAlumno Add(PEspecificoMatriculaAlumno objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPespecificoMatriculaAlumno entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 09/02/2023
        /// <summary>
        /// Obtiene los PEspecificos,centros de costo y programas asociados a una matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns> List<PEspecificoMatriculaAlumnoAgendaDTO> </returns>
        public List<DatosCursoMatriculaDTO> ObtenerDatosCursosPorMatricula(int idMatriculaCabecera)
        {
            try
            {
                var pEspecificoMatriculaAlumnoAgendaDTOs = new List<DatosCursoMatriculaDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = @"SELECT idMatriculaCabecera,
                                              idPEspecifico,
                                              nombrePEspecifico,
                                              idPGeneral,
                                              nombrePGeneral,
                                              idCentroCosto,
                                              nombreCentroCosto FROM ope.V_ObtenerDatosCursosPorMatricula
                                              where idMatriculaCabecera=@idMatriculaCabecera  ";
                var lista = _dapperRepository.QueryDapper(_queryAlumnoFiltro, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    pEspecificoMatriculaAlumnoAgendaDTOs = JsonConvert.DeserializeObject<List<DatosCursoMatriculaDTO>>(lista);
                }
                return pEspecificoMatriculaAlumnoAgendaDTOs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
