using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DocumentoSeccionPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoSeccionPw
    /// </summary>
    public class DocumentoSeccionPwRepository : GenericRepository<TDocumentoSeccionPw>, IDocumentoSeccionPwRepository
    {
        private Mapper _mapper;

        public DocumentoSeccionPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoSeccionPw, DocumentoSeccionPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TDocumentoSeccionPw MapeoEntidad(DocumentoSeccionPw entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoSeccionPw modelo = _mapper.Map<TDocumentoSeccionPw>(entidad);

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

        public TDocumentoSeccionPw Add(DocumentoSeccionPw entidad)
        {
            try
            {
                var DocumentoSeccionPw = MapeoEntidad(entidad);
                base.Insert(DocumentoSeccionPw);
                return DocumentoSeccionPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoSeccionPw Update(DocumentoSeccionPw entidad)
        {
            try
            {
                var DocumentoSeccionPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoSeccionPw.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoSeccionPw);
                return DocumentoSeccionPw;
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


        public IEnumerable<TDocumentoSeccionPw> Add(IEnumerable<DocumentoSeccionPw> listadoEntidad)
        {
            try
            {
                List<TDocumentoSeccionPw> listado = new List<TDocumentoSeccionPw>();
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

        public IEnumerable<TDocumentoSeccionPw> Update(IEnumerable<DocumentoSeccionPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoSeccionPw> listado = new List<TDocumentoSeccionPw>();
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
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoSeccionPw.
        /// </summary>
        /// <returns> List<DocumentoSeccionPwDTO> </returns>
        public IEnumerable<DocumentoSeccionPwDTO> ObtenerDocumentoSeccionPw()
        {
            try
            {
                List<DocumentoSeccionPwDTO> rpta = new List<DocumentoSeccionPwDTO>();
                var query = @"
                    SELECT
	                    Id,Titulo,Contenido,IdPlantillaPW,Posicion,Tipo,IdDocumentoPW,IdSeccionPW,VisibleWeb,ZonaWeb,OrdenWeb,UsuarioCreacion,
	                    UsuarioModificacion,FechaCreacion,FechaModificacion,IdSeccionTipoDetalle_PW,NumeroFila,Cabecera,PiePagina
                    FROM pla.T_DocumentoSeccion_PW
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoSeccionPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoSeccionPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoSeccionPwComboDTO> </returns>
        public IEnumerable<DocumentoSeccionPwComboDTO> ObtenerCombo()
        {
            try
            {
                List<DocumentoSeccionPwComboDTO> rpta = new List<DocumentoSeccionPwComboDTO>();
                var query = @"
                    SELECT
	                    DSPW.Id,
	                    DPW.Nombre AS DocumentoPw,
	                    DSPW.Titulo
                    FROM pla.T_DocumentoSeccion_PW AS DSPW
                    INNER JOIN pla.T_Documento_PW AS DPW
	                    ON DSPW.IdDocumentoPW = DPW.Id
	                    AND DPW.Estado = 1
                    WHERE DSPW.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoSeccionPwComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene seccion de documento asociado a un Programa General.
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<SeccionDocumentoDTO> ObtenerDocumentoSeccion(int idPgeneral)
        {
            try
            {
                List<SeccionDocumentoDTO> listadoSeccionesDocumento = new List<SeccionDocumentoDTO>();
                var _querySeccion = "SELECT Id, Titulo, Contenido, IdPGeneral, Orden, NumeroFila, NombreTitulo FROM pla.V_ListaSeccionesPorIdPrograma_Silabos WHERE IdPGeneral= @idPgeneral";
                var querySeccion = _dapperRepository.QueryDapper(_querySeccion, new { idPgeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<SeccionDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina asociados a un Programa General
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumento(int idPgeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                    SELECT LSPD.Titulo, LSPD.Contenido, LSPD.IdSeccionTipoDetalle_PW, LSPD.NumeroFila, LSPD.Cabecera,CONCAT(LSPD.PiePagina,LSPDD.Contenido) PiePagina, OrdenWeb
                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento LSPD
                    LEFT JOIN pla.V_ListaSeccionesPorIdPrograma_DocumentoDescripcion LSPDD on LSPD.Titulo=LSPDD.Enlace and LSPD.IdPGeneral=LSPDD.IdPGeneral
                    WHERE LSPD.Titulo != 'Estructura Curricular'
	                    AND LSPD.IdSeccionTipoDetalle_PW != 14
	                    AND LSPD.IdSeccionTipoDetalle_PW != 15
                        AND LSPD.IdSeccionTipoDetalle_PW NOT IN (118,119)
	                    AND LSPD.IdPGeneral = @idPgeneral";
                var querySeccion = _dapperRepository.QueryDapper(_querySeccion, new { idPgeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoSpeech(int idPgeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                    SELECT LSPD.Titulo, LSPD.Contenido, LSPD.IdSeccionTipoDetalle_PW, LSPD.NumeroFila, LSPD.Cabecera,CONCAT(LSPD.PiePagina,LSPDD.Contenido) PiePagina, OrdenWeb
                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento LSPD
                    LEFT JOIN pla.V_ListaSeccionesPorIdPrograma_DocumentoDescripcion LSPDD on LSPD.Titulo=LSPDD.Enlace and LSPD.IdPGeneral=LSPDD.IdPGeneral
                    WHERE LSPD.Titulo != 'Estructura Curricular'
	                    AND LSPD.IdSeccionTipoDetalle_PW != 14
	                    AND LSPD.IdSeccionTipoDetalle_PW != 15
                        AND LSPD.IdSeccionTipoDetalle_PW != 25
	                    AND LSPD.IdPGeneral = @idPgeneral";
                var querySeccion = _dapperRepository.QueryDapper(_querySeccion, new { idPgeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina asociados a un Programa General
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public async Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerSeccionDocumentoAsync(int idPgeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                    SELECT LSPD.Titulo, LSPD.Contenido, LSPD.IdSeccionTipoDetalle_PW, LSPD.NumeroFila, LSPD.Cabecera,CONCAT(LSPD.PiePagina,LSPDD.Contenido) PiePagina, OrdenWeb
                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento LSPD
                    LEFT JOIN pla.V_ListaSeccionesPorIdPrograma_DocumentoDescripcion LSPDD on LSPD.Titulo=LSPDD.Enlace and LSPD.IdPGeneral=LSPDD.IdPGeneral
                    WHERE LSPD.Titulo != 'Estructura Curricular'
	                    AND LSPD.IdSeccionTipoDetalle_PW != 14
	                    AND LSPD.IdSeccionTipoDetalle_PW != 15
	                    AND LSPD.IdPGeneral = @idPgeneral";
                var querySeccion = await _dapperRepository.QueryDapperAsync(_querySeccion, new { idPgeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina de Estructura Curricular asociados a un Programa General
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricular(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                    SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb, IdPGeneral, Nombre AS NombreCurso
                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento
                    WHERE Titulo = 'Estructura Curricular'
	                    AND IdSeccionTipoDetalle_PW != 14
	                    AND IdSeccionTipoDetalle_PW != 15
	                    AND IdPGeneral = @idPGeneral
                    ORDER BY NumeroFila ASC, IdSeccionTipoDetalle_PW ASC";
                var querySeccion = _dapperRepository.QueryDapper(_querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina de Estructura Curricular asociados a un Programa General
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricularPorIdsPGeneral(int idPGeneral, int IdHijo)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                            SELECT LSPD.Titulo, LSPD.Contenido, LSPD.IdSeccionTipoDetalle_PW, LSPD.NumeroFila, LSPD.Cabecera, CONCAT(LSPD.PiePagina,LSPDD.Contenido) PiePagina, LSPD.OrdenWeb, LSPD.IdPGeneral, LSPD.Nombre AS NombreCurso
                            FROM pla.V_ListaSeccionesPorIdPrograma_Documento LSPD
                            LEFT JOIN pla.V_ListaSeccionesPorIdPrograma_DocumentoDescripcion LSPDD on LSPD.Titulo=LSPDD.Enlace and LSPDD.IdPGeneral=@idPGeneral
                            WHERE LSPD.Titulo = 'Estructura Curricular'
                                AND LSPD.IdSeccionTipoDetalle_PW != 14
                                AND LSPD.IdSeccionTipoDetalle_PW != 15
                                AND LSPD.IdPGeneral = @IdHijo
                            ORDER BY LSPD.NumeroFila ASC, LSPD.IdSeccionTipoDetalle_PW ASC";
                var querySeccion = _dapperRepository.QueryDapper(_querySeccion, new { idPGeneral, IdHijo });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina de Estructura Curricular asociados a un Programa General
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public async Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerSeccionDocumentoEstructuraCurricularAsync(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                    SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb, IdPGeneral, Nombre AS NombreCurso
                    FROM pla.V_ListaSeccionesPorIdPrograma_Documento
                    WHERE Titulo = 'Estructura Curricular'
	                    AND IdSeccionTipoDetalle_PW != 14
	                    AND IdSeccionTipoDetalle_PW != 15
	                    AND IdPGeneral = @idPGeneral
                    ORDER BY NumeroFila ASC, IdSeccionTipoDetalle_PW ASC";
                var querySeccion = await _dapperRepository.QueryDapperAsync(_querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina de Estructura Curricular asociados a un Programa General
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public async Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerSeccionDocumentoEstructuraCurriculaPorIdsPGeneralAsync(int idProgramaGeneral, List<int> idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> listadoSeccionesDocumento = new List<RegistroListaSeccionesDocumentoDTO>();
                var _querySeccion = @"
                        SELECT LSPD.Titulo, LSPD.Contenido, LSPD.IdSeccionTipoDetalle_PW, LSPD.NumeroFila, LSPD.Cabecera, CONCAT(LSPD.PiePagina,LSPDD.Contenido) PiePagina, LSPD.OrdenWeb, LSPD.IdPGeneral, LSPD.Nombre AS NombreCurso
                        FROM pla.V_ListaSeccionesPorIdPrograma_Documento LSPD
                        LEFT JOIN pla.V_ListaSeccionesPorIdPrograma_DocumentoDescripcion LSPDD on LSPD.Titulo=LSPDD.Enlace and LSPDD.IdPGeneral=@idProgramaGeneral
                        WHERE LSPD.Titulo = 'Estructura Curricular'
	                        AND LSPD.IdSeccionTipoDetalle_PW != 14
	                        AND LSPD.IdSeccionTipoDetalle_PW != 15
	                        AND LSPD.IdPGeneral IN @idPGeneral
                        ORDER BY LSPD.NumeroFila ASC, LSPD.IdSeccionTipoDetalle_PW ASC";
                var querySeccion = await _dapperRepository.QueryDapperAsync(_querySeccion, new { idPGeneral, idProgramaGeneral });
                if (!string.IsNullOrEmpty(querySeccion) && !querySeccion.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(querySeccion);
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las secciones disponibles por el programa general
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<SeccionDocumentoDTO> ObtenerSecciones(int idPGeneral)
        {
            try
            {
                List<SeccionDocumentoDTO> seccionDocumento = new List<SeccionDocumentoDTO>();
                string querySeccion = @"
                    SELECT Id, Titulo, Contenido, IdPGeneral, OrdenWeb
                    FROM pla.V_ListaSeccionesPorIdPrograma
                    WHERE Estado = 1
	                    AND VisibleWeb = 1
	                    AND IdPGeneral = @idPGeneral
                    ORDER BY OrdenWeb";
                var resultado = _dapperRepository.QueryDapper(querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    seccionDocumento = JsonConvert.DeserializeObject<List<SeccionDocumentoDTO>>(resultado);
                }
                return seccionDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las secciones disponibles por el programa general
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public async Task<List<SeccionDocumentoDTO>> ObtenerSeccionesAsync(int idPGeneral)
        {
            try
            {
                List<SeccionDocumentoDTO> seccionDocumento = new List<SeccionDocumentoDTO>();
                string querySeccion = @"
                    SELECT Id, Titulo, Contenido, IdPGeneral, OrdenWeb
                    FROM pla.V_ListaSeccionesPorIdPrograma
                    WHERE Estado = 1
	                    AND VisibleWeb = 1
	                    AND IdPGeneral = @idPGeneral
                    ORDER BY OrdenWeb";
                var resultado = await _dapperRepository.QueryDapperAsync(querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    seccionDocumento = JsonConvert.DeserializeObject<List<SeccionDocumentoDTO>>(resultado);
                }
                return seccionDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la plantilla inicial para envio de correo masivo por operaciones
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General </param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
                var query = "SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb FROM pla.V_ListaSeccionesPorIdPrograma_Documento WHERE Titulo != 'Estructura Curricular' AND Titulo != 'Beneficios' AND  Titulo != 'Certificacion' AND Titulo != 'Prerrequisitos' AND  IdPGeneral = @IdPGeneral";
                var res = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2Objetivos(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
                var query = "SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb FROM pla.V_ListaSeccionesPorIdPrograma_Documento WHERE  IdPGeneral = @IdPGeneral AND Titulo = 'Objetivos'";
                var res = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la plantilla inicial para envio de correo masivo por operaciones
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General </param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public async Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerDatosComplementariosProgramaGeneralV2Async(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> lista = new List<RegistroListaSeccionesDocumentoDTO>();
                var query = "SELECT Titulo, Contenido, IdSeccionTipoDetalle_PW, NumeroFila, Cabecera, PiePagina, OrdenWeb FROM pla.V_ListaSeccionesPorIdPrograma_Documento WHERE Titulo != 'Estructura Curricular' AND Titulo != 'Beneficios' AND  Titulo != 'Certificacion' AND Titulo != 'Prerrequisitos' AND  IdPGeneral = @IdPGeneral";
                var res = await _dapperRepository.QueryDapperAsync(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos Complementarios asociados a un Programa General.
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> datosComplementarios = new List<RegistroListaSeccionesDocumentoDTO>();
                string querySeccion = @"
                    SELECT Titulo, Contenido,IdPGeneral, OrdenWeb
                    FROM pla.V_DatoProgramaGeneralContenidoPorIdPrograma
                    WHERE IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    datosComplementarios = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(resultado);
                }
                return datosComplementarios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1Speech(int idPGeneral)
        {
            try
            {
                List<RegistroListaSeccionesDocumentoDTO> datosComplementarios = new List<RegistroListaSeccionesDocumentoDTO>();
                string querySeccion = @"
                     SELECT Titulo, Contenido,IdPGeneral, OrdenWeb
                        FROM pla.V_DatoProgramaGeneralContenidoPorIdPrograma
                        WHERE IdPGeneral = @IdPGeneral AND ( Titulo = 'Certificación'  OR Titulo = 'Estructura Curricular' OR Titulo='Certificación' OR  Titulo='Metodología Online De Este programa') ";
                var resultado = _dapperRepository.QueryDapper(querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    datosComplementarios = JsonConvert.DeserializeObject<List<RegistroListaSeccionesDocumentoDTO>>(resultado);
                }
                return datosComplementarios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos Complementarios asociados a un Programa General.
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<ProgramaExpositoresDTO> ObtenerExpositoresPorIdGeneral(int idPGeneral)
        {
            try
            {
                List<ProgramaExpositoresDTO> expositores = new List<ProgramaExpositoresDTO>();
                string querySeccion = @"
                    SELECT Id,PrimerNombre,SegundoNombre,ApellidoPaterno,ApellidoMaterno,NombrePais,HojaVidaResumidaPerfil,IdPGeneral
                    FROM pla.V_ObtenerExpositorPorIdPrograma
                    WHERE IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(querySeccion, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    expositores = JsonConvert.DeserializeObject<List<ProgramaExpositoresDTO>>(resultado);
                }
                return expositores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la estructura curricular para un curso
        /// </summary>
        /// <param name="idmatriculaCabecera">Id de la matricula del alumno)</param>
        /// <returns> Lista de contenido para un programa general: List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<EstructuraCursoDTO> ObtenerEstructuraCurso(int IdMatriculaCabecera)
        {
            try
            {
                List<EstructuraCursoDTO> lista = new List<EstructuraCursoDTO>();
                var query = @"SELECT Capitulo AS Contenido FROM pw.V_PW_EstructuraCurricularCursoCongelada WHERE IdMatriculaCabecera=@IdMatriculaCabecera";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EstructuraCursoDTO>>(resultado)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<EstructuraCursoDTO> ObtenerEstructuraCursoPorIdProgramaGeneral(int IdProgramaGeneral)
        {
            try
            {
                List<EstructuraCursoDTO> lista = new List<EstructuraCursoDTO>();
                var query = @"SELECT Contenido FROM pla.V_EstructuraCurso WHERE IdPGeneral = @IdPGeneral ORDER BY NumeroFila";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = IdProgramaGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<EstructuraCursoDTO>>(resultado)!;
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna las secciones por programa general
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<SeccionDocumentoDTO> ObtenerDocumentoSeccionCompleto(int idPGeneral)
        {
            try
            {
                List<SeccionDocumentoDTO> listadoSeccionesDocumento = new List<SeccionDocumentoDTO>();
                var querySeccion = @"
                                    SELECT 
                                        Id,Titulo,Contenido,IdPGeneral, Orden, NumeroFila, NombreTitulo 
                                    FROM 
                                        pla.V_ListaSeccionesPorIdPrograma_Silabos 
                                    WHERE 
                                        IdPGeneral= @IdPgeneral";
                var query = _dapperRepository.QueryDapper(querySeccion, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listadoSeccionesDocumento = JsonConvert.DeserializeObject<List<SeccionDocumentoDTO>>(query)!;
                }
                return listadoSeccionesDocumento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Qm
        /// Fecha: 21-07-23
        /// Versión: 1
        /// <summary>
        /// Obtiene información de las sesiones de preguntas interactivas para su exportación en excel
        /// </summary>
        /// <returns>List<SesionSubSesionPreguntaInteractivaDTO></returns>
        public async Task<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>> ObtenerSesionesPreguntasInteractivasExportacionExcel()
        {
            try
            {
                var query = "SELECT IdPGeneral, Contenido, NumeroFila FROM [pw].[V_ObtenerListadoSesionesSubSesiones] WHERE IdSeccionTipoDetalle = 13";
                var res = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>>(res);
                return new List<SesionSubSesionPreguntaInteractivaDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 21-07-23
        /// Versión: 1
        /// <summary>
        /// Obtiene información de las subsesiones para el reporte de preguntas interactivas para su exportación en excel
        /// </summary>
        /// <returns>List<SesionSubSesionPreguntaInteractivaDTO></returns>
        public async Task<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>> ObtenerSubSesionesPreguntasInteractivasExportacionExcel()
        {
            try
            {
                var query = "SELECT IdPGeneral, Contenido, NumeroFila FROM [pw].[V_ObtenerListadoSesionesSubSesiones] WHERE IdSeccionTipoDetalle = 14";
                var res = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != "null" && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>>(res);
                return new List<SesionSubSesionPreguntaInteractivaDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 02/08/23
        /// Versión: 1
        /// <summary>
        /// Obtener Filtros por capitulo
        /// </summary>
        /// <returns>Retorna una lista de objetos (EstructuraProgramaCapituloDTO)</returns>
        public IEnumerable<EstructuraProgramaCapituloDTO> ObtenerCapituloPrograma()
        {
            try
            {
                new List<EstructuraProgramaCapituloDTO>();
                var queryfiltrocapitulo = "Select distinct Contenido,NroCapitulo FROM pla.V_ListadoEstructuraProgramaCapitulosV2";
                var subfiltroCapitulo = _dapperRepository.QueryDapper(queryfiltrocapitulo, new { });
                if (!string.IsNullOrEmpty(subfiltroCapitulo) && !subfiltroCapitulo.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<EstructuraProgramaCapituloDTO>>(subfiltroCapitulo);
                return new List<EstructuraProgramaCapituloDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 02/08/23
        /// Versión: 1
        /// <summary>
        /// Obtener Filtros por sesion
        /// </summary>
        /// <returns>Retorna una lista de objetos (PreEstructuraCapituloProgramaBO)</returns>
        public IEnumerable<SesionSubSesionPreguntaInteractivaDTO> ObtenerEstructuraProgramaSesion()
        {
            try
            {
                var queryfiltrosesion = "Select IdPGeneral,Contenido,NumeroFila FROM pla.V_ListadoEstructuraPrograma WHERE NombreTitulo = 'Sesion'";
                var subfiltroSesion = _dapperRepository.QueryDapper(queryfiltrosesion, new { });
                if (!string.IsNullOrEmpty(subfiltroSesion) && !subfiltroSesion.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>>(subfiltroSesion);
                return new List<SesionSubSesionPreguntaInteractivaDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros de DocumentoSeccion filtrado por el Id
        /// </summary>
        /// <returns></returns>
        public List<DocumentoSeccionPwFiltroDTO> ObtenerDocumentoSeccionPorId(int idDocumentoSeccion)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id, Titulo, VisibleWeb, ZonaWeb, OrdenEeb, Contenido, IdPlantillaPW, Posicion, Tipo, IdDocumentoPW, IdSeccionPW, IdSeccionTipoContenido, NombreSeccionTipoContenido, 
                                IdSeccionTipoDetallePw, NombreSubSeccion, IdSubSeccionTipoContenido, ContenidoSubSeccion,NumeroFila,Cabecera,PiePagina 
                            FROM 
                                pla.V_ObtenerDocumentoSeccionPorId 
                            WHERE 
                                IdDocumentoPW = @idDocumentoSeccion and EstadoDocumentoSeccion = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { idDocumentoSeccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<DocumentoSeccionPwFiltroDTO>>(resultado)!;
                }
                return new List<DocumentoSeccionPwFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#DOSPwR-ODSPI-001@Error en ObtenerDocumentoSeccionPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el todo DocumentoSeccionPw por medio del idSeccion, idDocumento y contenido
        /// </summary>
        /// <param name="idSeccionPw"></param>
        /// <param name="idDocumentoPw"></param>
        /// <param name="contenido"></param>
        /// <returns> Entidad - DocumentoSeccionPw </returns>
        public DocumentoSeccionPw? ObtenerIdSeccionIdDocumentoContenido(int idSeccionPw, int idDocumentoPw, string contenido)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id,
                                Titulo,
                                Contenido,
                                IdPlantillaPW,
                                Posicion,
                                Tipo,
                                IdDocumentoPW,
                                IdSeccionPW,
                                VisibleWeb,
                                ZonaWeb,
                                OrdenWeb,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                IdSeccionTipoDetalle_PW AS IdSeccionTipoDetallePw,
                                NumeroFila,
                                Cabecera,
                                PiePagina,
                                TituloComparar 
                            FROM 
                                pla.T_DocumentoSeccion_PW 
                            WHERE 
                                IdSeccionPW = @IdSeccionPW AND IdDocumentoPW = @IdDocumentoPW AND Contenido = @Contenido AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdSeccionPW = idSeccionPw, IdDocumentoPW = idDocumentoPw, Contenido = contenido });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<DocumentoSeccionPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#DOSPwR-OISIDC-002@Error en ObtenerIdSeccionIdDocumentoContenido() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el todo DocumentoSeccionPw por medio del idSeccion y idDocumento
        /// </summary>
        /// <param name="idSeccionPw"></param>
        /// <param name="idDocumentoPw"></param>
        /// <returns> Entidad - DocumentoSeccionPw </returns>
        public DocumentoSeccionPw? ObtenerPorIdSeccionIdDocumento(int idSeccionPw, int idDocumentoPw)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id,
                                Titulo,
                                Contenido,
                                IdPlantillaPW,
                                Posicion,
                                Tipo,
                                IdDocumentoPW,
                                IdSeccionPW,
                                VisibleWeb,
                                ZonaWeb,
                                OrdenWeb,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                IdSeccionTipoDetalle_PW AS IdSeccionTipoDetallePw,
                                NumeroFila,
                                Cabecera,
                                PiePagina,
                                TituloComparar 
                            FROM 
                                pla.T_DocumentoSeccion_PW 
                            WHERE 
                                IdSeccionPW = @IdSeccionPW AND IdDocumentoPW = @IdDocumentoPW AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdSeccionPW = idSeccionPw, IdDocumentoPW = idDocumentoPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<DocumentoSeccionPw>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#DOSPwR-OPISID-003@Error en ObtenerPorIdSeccionIdDocumento() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 02/08/23
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el todo DocumentoSeccionPw por medio del idDocumento
        /// </summary>
        /// <param name="idDocumentoPw"></param>
        /// <returns> Entidad - DocumentoSeccionPw </returns>
        public List<DocumentoSeccionPw> ObtenerPorIdDocumento(int idDocumentoPw)
        {
            try
            {
                var query = @"
                            SELECT 
                                Id,
                                Titulo,
                                Contenido,
                                IdPlantillaPW,
                                Posicion,
                                Tipo,
                                IdDocumentoPW,
                                IdSeccionPW,
                                VisibleWeb,
                                ZonaWeb,
                                OrdenWeb,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion,
                                IdSeccionTipoDetalle_PW AS IdSeccionTipoDetallePw,
                                NumeroFila,
                                Cabecera,
                                PiePagina,
                                TituloComparar 
                            FROM 
                                pla.T_DocumentoSeccion_PW 
                            WHERE 
                                IdDocumentoPW = @IdDocumentoPW AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdDocumentoPW = idDocumentoPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<DocumentoSeccionPw>>(resultado)!;
                }
                return new List<DocumentoSeccionPw>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#DOSPwR-OPID-004@Error en ObtenerPorIdDocumento() {ex.Message}", ex);
            }
        }
    }
}
