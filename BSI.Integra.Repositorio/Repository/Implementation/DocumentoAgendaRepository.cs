using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoAgendaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 03/08/2022
    /// <summary>
    /// Gestión general de T_DocumentoAgenda
    /// </summary>
    public class DocumentoAgendaRepository : GenericRepository<TDocumentoAgendum>, IDocumentoAgendaRepository
    {
        private Mapper _mapper;

        public DocumentoAgendaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoAgendum, DocumentoAgenda>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoAgendum MapeoEntidad(DocumentoAgenda entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoAgendum modelo = _mapper.Map<TDocumentoAgendum>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoAgendum Add(DocumentoAgenda entidad)
        {
            try
            {
                var DocumentoAgenda = MapeoEntidad(entidad);
                base.Insert(DocumentoAgenda);
                return DocumentoAgenda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoAgendum Update(DocumentoAgenda entidad)
        {
            try
            {
                var DocumentoAgenda = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoAgenda.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoAgenda);
                return DocumentoAgenda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TDocumentoAgendum> Add(IEnumerable<DocumentoAgenda> listadoEntidad)
        {
            try
            {
                List<TDocumentoAgendum> listado = new List<TDocumentoAgendum>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TDocumentoAgendum> Update(IEnumerable<DocumentoAgenda> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoAgendum> listado = new List<TDocumentoAgendum>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoAgenda.
        /// </summary>
        /// <returns> List<DocumentoAgendaDTO> </returns>
        public IEnumerable<DocumentoAgendaDTO> ObtenerDocumentoAgenda()
        {
            try
            {
                List<DocumentoAgendaDTO> rpta = new List<DocumentoAgendaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Habilitado,
	                    MensajeDetalle,
	                    Generado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_DocumentoAgenda
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoAgenda para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoAgendaComboDTO> </returns>
        public IEnumerable<DocumentoAgendaComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoAgendaComboDTO> rpta = new List<DocumentoAgendaComboDTO>();
                var query = @"SELECT Id,Nombre FROM com.T_DocumentoAgenda WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoAgendaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_DocumentoAgenda obviando los Campos de Auditoria.
        /// </summary>
        /// <returns> List<DocumentoAgendaSinAuditoriaDTO> </returns>
        public IEnumerable<DocumentoAgendaSinAuditoriaDTO> ObtenerDocumentoAgendaSinAuditoria()
        {
            try
            {
                List<DocumentoAgendaSinAuditoriaDTO> rpta = new List<DocumentoAgendaSinAuditoriaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Habilitado,
	                    MensajeDetalle,
	                    Generado
                    FROM com.T_DocumentoAgenda
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoAgendaSinAuditoriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 03/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Url del Documento de Agenda asociado a un Id y a un Id de Pais
        /// </summary>
        /// <param name="idDocumentoAgenda">Id de Documento Agenda</param>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        /// []TODO Mover a T_DocumentoAgendaPaisUrl
        public StringDTO ObtenerDocumentoAgendaUrlPorPais(int idDocumentoAgenda, int idPais)
        {
            try
            {
                StringDTO url = new StringDTO();
                var query = @"SELECT TOP 1 [Url] AS Valor FROM com.T_DocumentoAgendaPaisUrl WHERE IdDocumentoAgenda = @idDocumentoAgenda AND IdPais = @idPais";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idDocumentoAgenda, idPais });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    url = JsonConvert.DeserializeObject<StringDTO>(resultadoQuery);
                }
                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jeremy Pacheco.
        /// Fecha: 21/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene encuestas de Alumno por Matricula 
        /// </summary>
        /// <param name="idMatricula">Matricula del Alumno</param>
        /// <returns> List<EncuestaAsignadoMatriculaDTO> </returns>
        public List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula)
        {
            try
            {
                var query = @"SELECT 
                                IdPEspecificoSesionEncuestaAlumno,
                                IdEncuestaSesionPrograma,
                                Titulo,
                                FechaEncuesta,
                                IdPEspecificoSesion,
                                Tipo,
                                Descripcion,
                                IdPGeneral,
                                IdPEspecifico,
                                FechaEncuestaRealizada,
                                Estatus,
                                ComentarioAlumno FROM pw.V_PW_EncuestaAsignadasAlumnoSincronico 
                                WHERE IdMatriculaCabecera = @idMatricula AND AsignadoPara=1";
                var resultado = _dapperRepository.QueryDapper(query, new { idMatricula });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var respuesta = JsonConvert.DeserializeObject<List<EncuestaAsignadoMatriculaDTO>>(resultado);
                    return respuesta;
                }

                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas de Encuesta del IdPEspecifico
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <returns> List<PEspecificoSesionEncuestaPreguntaDTO> </returns>
        public List<PEspecificoSesionEncuestaPreguntaDTO> ObtenerPreguntasSesionEncuestaIdPespecifico(int IdPEspecificoSesion)
        {
            try
            {
                List<PEspecificoSesionEncuestaPreguntaDTO> rpta = new List<PEspecificoSesionEncuestaPreguntaDTO>();
                string _query = "   SELECT Id,IdEncuestaSesionPrograma,IdEncuestaOnline,IdPreguntaEncuestaTipo,Pregunta,Descripcion,NombreTipoPregunta,IdPEspecificoSesion, IdCategoria, NombreCategoria,DescripcionActiva,PreguntaObligatoria,PreguntaActiva FROM pw.V_PW_EncuestaOnlineObtenerPregunta WHERE IdPEspecificoSesion=@IdPEspecificoSesion AND PreguntaActiva=1 ";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaPreguntaDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Alternativas de las preguntas de Encuesta por Sesion
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <returns> List<PEspecificoSesionEncuestaPreguntaDTO> </returns>
        public List<PEspecificoSesionEncuestaPreguntaAlternativaDTO> ObtenerPEspecificoSesionEncuestaPreguntaAlternativaPorIdSesion(int IdPEspecificoSesion)
        {
            try
            {
                List<PEspecificoSesionEncuestaPreguntaAlternativaDTO> rpta = new List<PEspecificoSesionEncuestaPreguntaAlternativaDTO>();
                string _query = @"SELECT Id,IdEncuestaSesionPrograma,IdEncuestaOnline,IdPreguntaEncuesta,Respuesta,Orden,Puntaje,IdPEspecificoSesion FROM pw.V_PW_EncuestaOnlineObtenerPreguntaAlternativa
                                    WHERE IdPEspecificoSesion=@IdPEspecificoSesion";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaPreguntaAlternativaDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene encuesta por IdPEspecificoSesion y alumno
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <param name="IdMatriculaCabecera"> Identificador de la matricula</param>
        /// <returns> List<PEspecificoSesionEncuestaAlumnoDTO> </returns>
        public List<PEspecificoSesionEncuestaAlumnoDTO> ObtenerEncuestaAlumnoPorIdPEspecificoSesion(int IdPEspecificoSesion, int IdMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionEncuestaAlumnoDTO> rpta = new List<PEspecificoSesionEncuestaAlumnoDTO>();
                string _query = "SELECT Id,IdPEspecificoSesion,IdEncuestaSesionPrograma,IdMatriculaCabecera,Puntaje,FechaRealizada FROM pw.V_PW_ObtenerEncuestaOnlineAlumno WHERE IdPEspecificoSesion = @IdPEspecificoSesion AND IdMatriculaCabecera = @IdMatriculaCabecera";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion, IdMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaAlumnoDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de las encuesta por IdPEspecifico y matricula
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <param name="IdMatriculaCabecera"> Identificador de la matricula</param>
        /// <returns> List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> </returns>
        public List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> ObtenerPEspecificoSesionEncuestaAlumnoRespuestaPorIdSesion(int IdPEspecificoSesion, int IdMatriculaCabecera)
        {
            try
            {
                List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> rpta = new List<PEspecificoSesionEncuestaAlumnoRespuestaDTO>();
                string _query = " SELECT Id,IdPEspecificoSesion,IdPreguntaEncuesta,IdPEspecificoSesionEncuestaAlumno,IdPreguntaRespuestaEncuesta,Valor,Puntos,IdMatriculaCabecera FROM pw.V_PW_ObtenerEncuestaOnlineAlumnoRespuesta WHERE IdPEspecificoSesion=@IdPEspecificoSesion AND IdMatriculaCabecera=@IdMatriculaCabecera";
                string respuesta = _dapperRepository.QueryDapper(_query, new { IdPEspecificoSesion = IdPEspecificoSesion, IdMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEncuestaAlumnoRespuestaDTO>>(respuesta);
                    return rpta;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Agrega encuesta de alumno 
        /// </summary>
        /// <param name="data">Datos para insertar Encuesta</param>
        /// <returns>retorna true o false </returns>
        public bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data)
        {
            try
            {
                string _queryPrevio = @"SELECT * FROM  pw.T_PW_PEspecificoSesionEncuestaAlumno WHERE IdMatriculaCabecera=@IdMatriculaCabecera and IdEncuestaSesionPrograma=@IdEncuestaSesionPrograma and estado=1";
                string respuesta = _dapperRepository.QueryDapper(_queryPrevio, new { IdMatriculaCabecera = data.IdMatriculaCabecera, IdEncuestaSesionPrograma = data.IdEncuestaSesionPrograma });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null" && respuesta != null)
                {
                }
                else
                {
                    var asesor = "ASESOR";
                    string Json = JsonConvert.SerializeObject(data.Categorias);
                    string _query = "[pw].[SP_PW_AgregarPEspecificoSesionEncuestaAlumno]";
                    string query = _dapperRepository.QuerySPFirstOrDefault(_query, new
                    {
                        IdEncuestaSesionPrograma = data.IdEncuestaSesionPrograma,
                        IdMatriculaCabecera = data.IdMatriculaCabecera,
                        IdPEspecificoSesion = data.IdPEspecificoSesion,
                        IdPGeneral = data.IdPGeneral,
                        IdPEspecifico = data.IdPEspecifico,
                        Json,
                        Usuario = asesor
                    });

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un comentario a una encuesta
        /// </summary>
        /// <param name="Encuesta">Datos para agregar comentario a un Alumno</param>
        /// <returns>Retorna true o false</returns>
        public bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta)
        {
            try
            {

                var asesor = "ASESOR";

                string _queryPrevio = @"pw.SP_PW_RegistrarComentarioEncuestaAlumno";

                string respuesta = _dapperRepository.QuerySPDapper(_queryPrevio, new { 
                    Encuesta.IdPEspecificoSesionEncuestaAlumno,
                    Encuesta.Comentario,Usuario=asesor });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
