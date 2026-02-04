using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: DocumentoPwRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 14/06/2023
    /// <summary>
    /// Gestión general de T_DocumentoPw
    /// </summary>
    public class DocumentoPwRepository : GenericRepository<TDocumentoPw>, IDocumentoPwRepository
    {
        private Mapper _mapper;
        public DocumentoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocumentoPw, DocumentoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralDocumentoPw, PGeneralDocumentoPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TDocumentoSeccionPw, DocumentoSeccionPw>(MemberList.None).ReverseMap();
                cfg.CreateMap<TBandejaPendientePw, BandejaPendientePw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TDocumentoPw MapeoEntidad(DocumentoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TDocumentoPw modelo = _mapper.Map<TDocumentoPw>(entidad);

                if (entidad.PGeneralDocumentoPws != null && entidad.PGeneralDocumentoPws.Count > 0)
                {
                    modelo.TPgeneralDocumentoPws = _mapper.Map<List<TPgeneralDocumentoPw>>(entidad.PGeneralDocumentoPws);
                }
                if (entidad.DocumentoSeccionPws != null && entidad.DocumentoSeccionPws.Count > 0)
                {
                    modelo.TDocumentoSeccionPws = _mapper.Map<List<TDocumentoSeccionPw>>(entidad.DocumentoSeccionPws);
                }
                if (entidad.BandejaPendientePws != null && entidad.BandejaPendientePws.Count > 0)
                {
                    modelo.TBandejaPendientePws = _mapper.Map<List<TBandejaPendientePw>>(entidad.BandejaPendientePws);
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoPw Add(DocumentoPw entidad)
        {
            try
            {
                var DocumentoPw = MapeoEntidad(entidad);
                base.Insert(DocumentoPw);
                return DocumentoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocumentoPw Update(DocumentoPw entidad)
        {
            try
            {
                var DocumentoPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                DocumentoPw.RowVersion = entidadExistente.RowVersion;

                base.Update(DocumentoPw);
                return DocumentoPw;
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


        public IEnumerable<TDocumentoPw> Add(IEnumerable<DocumentoPw> listadoEntidad)
        {
            try
            {
                List<TDocumentoPw> listado = new List<TDocumentoPw>();
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

        public IEnumerable<TDocumentoPw> Update(IEnumerable<DocumentoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDocumentoPw> listado = new List<TDocumentoPw>();
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
        /// Autor Modificacion: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// <summary>
        /// Obtiene todos los documentos asociados para un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns>  List<PlantillaDocumentoAsociadoDTO> </returns>
        public List<PlantillaDocumentoAsociadoDTO> ObtenerDocumentosAsociados(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT IdDocumentos,
                                   Nombre,
                                   IdPlantillaPW,
                                   EstadoFlujo,
                                   Asignado,
                                   IdPGeneralDocumento
                            FROM pla.V_TPGeneralDocumento
                            WHERE EstadoDocumentos = 1
                                  AND EstadoPgeneral = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PlantillaDocumentoAsociadoDTO>>(respuestaDapper);
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor Modificacion: Gilmer Qm.
        /// Fecha: 14/06/2023
        /// <summary>
        /// Obtiene todos los documentos NO asociados para un programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public List<PlantillaDocumentoNoAsociadoDTO> ObtenerDocumentosNoAsociados(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT IdDocumentos,
                                   Nombre,
                                   IdPlantillaPW,
                                   EstadoFlujo,
                                   Asignado
                            FROM pla.V_TPlantillasPaisDocumentos
                            WHERE EstadoMontos = 1
                                  AND EstadoDocumentos = 1
                                  AND Asignado = 0
                                  AND IdPrograma = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PlantillaDocumentoNoAsociadoDTO>>(respuestaDapper);
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 12/11/2024
        /// <summary>
        /// Regulariza el valor de Introduccion en Cabezera de los NombrePrerrequsito
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void RegularizaIntroduccionPrerrequisito(int IdDocumentoPw, string usuario)
        {
            try
            {
                var query = "mkt.SP_DocumentoSeccionPW_RegularizarPrerrequisitos";
                var parametros = new
                {
                    IdDocumentoPw = IdDocumentoPw,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarOportunidadLead() {ex.Message}", ex);
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 27/11/2024
        /// <summary>
        /// Regulariza el valor de Introduccion en Cabezera de los NombrePrerrequsito
        /// </summary>
        /// <param name="entidad , usuario"></param>
        /// <returns></returns>
        public void InsertarVersionesBeneficiosDocumentosPw(DocumentoPwVersionesDTO entidad, string usuario)
        {
            try
            {
                var query = "mkt.SP_DocumentoPwVersion_Insertar";
                var parametros = new
                {
                    IdVersionPrograma = entidad.IdVersionPrograma,
                    Introduccion = entidad.Introduccion,
                    IdDocumentoPw = entidad.IdDocumentoPw,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarVersionesBeneficiosDocumentosPw() {ex.Message}", ex);
            }
        }
        public void ActualizarIntroduccionBeneficioDocumentoPw(string introduccion, int idDocumentoPw, int version, string usuario)
        {
            try
            {
                var query = "mkt.SP_DocumentoPwVersion_Actualizar";
                var parametros = new
                {
                    Introduccion = introduccion,
                    IdDocumentoPw = idDocumentoPw,
                    IdVersionPrograma = version,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarIntroduccionBeneficioDocumentoPw() {ex.Message}", ex);
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 27/11/2024
        /// <summary>
        /// Indica si tienen Registros
        /// </summary>
        /// <param name="entidad , usuario"></param>
        /// <returns></returns>
        public IntDTO ValidarSiTieneRegistro(int IdDocumentoPw)
        {
            try
            {
                IntDTO respuesta = new IntDTO();
                var query = @"
                    SELECT
	                  Count(*) as Valor
                    FROM pla.T_DocumentoPwVersion
                    WHERE Estado = 1 and IdDocumentoPw=@IdDocumentoPw";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdDocumentoPw });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<IntDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ValidarSiTieneRegistro() {ex.Message}", ex);
            }
        }
        public IEnumerable<DocumentoPwVersionesDTO> ObtenerIntroduccionVersionDocumento(int IdDocumentoPw)
        {
            try
            {
                List<DocumentoPwVersionesDTO> respuesta = new List<DocumentoPwVersionesDTO>();
                var query = @"
                    SELECT
	                  Id,Introduccion,IdDocumentoPw,IdVersionPrograma
                    FROM pla.T_DocumentoPwVersion
                    WHERE Estado = 1 and IdDocumentoPw=@IdDocumentoPw";
                var resultado = _dapperRepository.QueryDapper(query, new { IdDocumentoPw });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<DocumentoPwVersionesDTO>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerIntroduccionVersionDocumento() {ex.Message}", ex);
            }
        }




        public StringDTO ValidarSiTieneIntroduccionLaVersion(int IdDocumentoPw, int version)
        {
            try
            {
                StringDTO respuesta = new StringDTO();
                var query = @"
                    SELECT
	                  Introduccion as Valor
                    FROM pla.T_DocumentoPwVersion
                    WHERE Estado = 1 and IdDocumentoPw=@IdDocumentoPw and idVersionPrograma = @version";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdDocumentoPw, version });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ValidarSiTieneRegistro() {ex.Message}", ex);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_CategoriaOrigen por el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad: DocumentoPw </returns>
        public DocumentoPw? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   IdPlantillaPW,
                                   EstadoFlujo,
                                   Asignado,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_Documento_PW
                            WHERE Id = @Id
                            AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<DocumentoPw>(resultado)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Documento_PW.
        /// </summary>
        /// <returns> List<DocumentoPwDTO> </returns>
        public IEnumerable<DocumentoPwDTO> Obtener()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Nombre,
                        IdPlantillaPW,
                        EstadoFlujo,
                        Asignado
                    FROM pla.T_Documento_PW
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<DocumentoPwDTO>>(resultado)!;
                }
                return new List<DocumentoPwDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#DPwR-O-001@Error en Obtener() {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene los cursos hijos de un programa General para EstructuraCurricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        //public List<CursoHijoDuracionPdusDTO> ObtenerProgramasEstrucuraCurricularPdus(int idPGeneral)
        //{

        //    List<CursoHijoDuracionPdusDTO> cursos = new List<CursoHijoDuracionPdusDTO>();
        //    if (cursos.Count <= 0)
        //    {
        //        var _query = string.Empty;
        //        _query = "SELECT Nombre,Duracion,CodigoPartner,CantidadPdus FROM pla.V_PW_EstructuraCurricularProgramaCongeladaPdusPGeneral WHERE IdPGeneral = @idPGeneral ORDER BY Orden";
        //        var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
        //        if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
        //        {
        //            cursos = JsonConvert.DeserializeObject<List<CursoHijoDuracionPdusDTO>>(respuestaDapper);
        //        }
        //    }
        //    return cursos;
        //}


        public List<CursoHijoDuracionPdusDTO> ObtenerProgramasEstrucuraCurricularPdus(int idPGeneral)
        {


            try
            {
                List<CursoHijoDuracionPdusDTO> rpta = new List<CursoHijoDuracionPdusDTO>();

                var query = "pla.SP_EstructuraCurricularProgramaCongeladaPdusPGeneral";
                var parametros = new
                {
                    IdPGeneral = idPGeneral
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CursoHijoDuracionPdusDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerReporteLeadsByFecha() {ex.Message}", ex);
            }

        }

        public List<EstructuraCursoDTO> ObtenerEstructuraCurso(int idPGeneral)
        {


            try
            {
                List<EstructuraCursoDTO> rpta = new List<EstructuraCursoDTO>();

                var query = "pla.SP_ObtenerDocumentoEstructuraCurricularUnico";
                var parametros = new
                {
                    IdPGeneral = idPGeneral
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstructuraCursoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerReporteLeadsByFecha() {ex.Message}", ex);
            }

        }


        public IEnumerable<ModalidadPortalDTO> ObtenerModalidadPortal()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Nombre,
                        Propiedad
                    FROM  pla.T_ModalidadPortal
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModalidadPortalDTO>>(resultado)!;
                }
                return new List<ModalidadPortalDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#DPwR-O-001@Error en Obtener() {ex.Message}", ex);
            }
        }

        public IEnumerable<ComboDTO> ObtenerModoFechaInicio()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Nombre
                    FROM  pla.T_DocumentoPWFechaInicioModo
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#DPwR-O-001@Error en Obtener() {ex.Message}", ex);
            }
        }
        public IEnumerable<ComboDTO> ObtenerNotasTipo()
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
                        Nombre
                    FROM  pla.T_DocumentoPWNotaTipo
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#DPwR-O-001@Error en Obtener() {ex.Message}", ex);
            }
        }


        public void InsertarDocumentoPwModalidad(SeccionModalidadHorarioDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                var spIntro = "pla.SP_TDocumentoPWModalidadIntroduccion_Insertar";
                var parametrosIntro = new
                {
                    Introduccion = dto.Introduccion,
                    Usuario = usuario
                };

                var rIntro = _dapperRepository.QuerySPDapper(spIntro, parametrosIntro);
                if (string.IsNullOrEmpty(rIntro) || rIntro.Contains("[]"))
                    throw new Exception("No se pudo obtener IdDocumentoPWModalidadIntroduccion.");

                var tIntro = Newtonsoft.Json.Linq.JToken.Parse(rIntro);
                var idIntroduccion =
                    tIntro.Type == Newtonsoft.Json.Linq.JTokenType.Array
                        ? (int)(tIntro.First?["Id"] ?? tIntro.First?["id"] ?? 0)
                        : (int)(tIntro["Id"] ?? tIntro["id"] ?? 0);

                if (idIntroduccion <= 0)
                    throw new Exception("No se pudo obtener IdDocumentoPWModalidadIntroduccion.");

                foreach (var m in dto.Modalidades ?? new List<ModalidadHorarioDTO>())
                {
                    var spModalidad = "pla.SP_TDocumentoPWModalidad_Insertar";
                    var parametrosModalidad = new
                    {

                        IdModalidadPortal = m.IdModalidad ?? 0,
                        SubTitulo = m.SubTitulo,
                        Descripcion = m.Descripcion,
                        Usuario = usuario
                    };

                    var rModalidad = _dapperRepository.QuerySPDapper(spModalidad, parametrosModalidad);
                    if (string.IsNullOrEmpty(rModalidad) || rModalidad.Contains("[]"))
                        throw new Exception("No se pudo obtener IdDocumentoPWModalidad.");

                    var tModalidad = Newtonsoft.Json.Linq.JToken.Parse(rModalidad);
                    var idDocumentoPwModalidad =
                        tModalidad.Type == Newtonsoft.Json.Linq.JTokenType.Array
                            ? (int)(tModalidad.First?["Id"] ?? tModalidad.First?["id"] ?? 0)
                            : (int)(tModalidad["Id"] ?? tModalidad["id"] ?? 0);

                    if (idDocumentoPwModalidad <= 0)
                        throw new Exception("No se pudo obtener IdDocumentoPWModalidad.");

                    var detalles = (m.Detalles ?? new List<ModalidadHorarioDetalleDTO>())
                        .OrderBy(x => x.Orden)
                        .ToList();

                    foreach (var d in detalles)
                    {
                        var tipo = (d.Tipo ?? "").Trim().ToUpperInvariant();

                        var spDetalle = "pla.SP_TDocumentoPWModalidadDetalle_Insertar";
                        var parametrosDetalle = new
                        {
                            IdDocumentoPWModalidad = idDocumentoPwModalidad,
                            Orden = d.Orden,
                            Tipo = tipo,
                            IdPais = tipo == "HORA" ? d.IdPais : (int?)null,
                            Beneficio = tipo == "BENEFICIO" ? d.Beneficio : null,
                            Usuario = usuario
                        };

                        _dapperRepository.QuerySPDapper(spDetalle, parametrosDetalle);
                    }

                    var spConfig = "pla.SP_TDocumentoPWModalidadConfiguracion_Insertar";
                    var parametrosConfig = new
                    {
                        IdDocumentoPw = idDocumentoPw,
                        IdDocumentoPWModalidadIntroduccion = idIntroduccion,
                        IdDocumentoPWModalidad = idDocumentoPwModalidad,
                        Usuario = usuario
                    };

                    _dapperRepository.QuerySPDapper(spConfig, parametrosConfig);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarDocumentoPwModalidad() {ex.Message}", ex);
            }
        }

        public IEnumerable<DocumentoPWModalidadRowVM> ObtenerDocumentoPWModalidadRows(int idDocumentoPW)
        {
            try
            {
                List<DocumentoPWModalidadRowVM> rpta = new List<DocumentoPWModalidadRowVM>();
                var query = "pla.SP_DocumentoPwModalidadPorIdDocumentoPW";
                var parametros = new
                {
                    @IdDocumento_PW = idDocumentoPW
               
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoPWModalidadRowVM>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWModalidadRows() {ex.Message}", ex);
            }
        }

        public void InsertarDocumentoPwDuracion(SeccionDuracionDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                var spDuracion = "pla.SP_TDocumentoPWDuracion_Insertar";
                var pDuracion = new
                {
                    Titulo = dto.Titulo,
                    Introduccion = dto.Introduccion,
                    PieDePagina = dto.PieDePagina,
                    Usuario = usuario
                };

                var rDuracion = _dapperRepository.QuerySPDapper(spDuracion, pDuracion);

                int idDocumentoPwDuracion = 0;
                if (!string.IsNullOrEmpty(rDuracion) && !rDuracion.Contains("[]"))
                {
                    var tmp = JsonConvert.DeserializeObject<List<dynamic>>(rDuracion);
                    if (tmp != null && tmp.Count > 0)
                    {
                        idDocumentoPwDuracion = Convert.ToInt32(tmp[0].Id);
                    }
                }

                if (idDocumentoPwDuracion <= 0)
                    throw new Exception("No se pudo obtener IdDocumentoPWDuracion.");

                var detalles = dto.Detalles ?? new List<DuracionDetalleDTO>();
                foreach (var d in detalles)
                {
                    var spDetalle = "pla.SP_TDocumentoPWDuracionDetalle_Insertar";
                    var pDetalle = new
                    {
                        IdDocumentoPWDuracion = idDocumentoPwDuracion,
                        IdVersionPrograma = d.IdVersionPrograma,
                        DetalleMes = d.Meses,
                        DetalleHora = d.Horas,
                        Usuario = usuario
                    };

                    _dapperRepository.QuerySPDapper(spDetalle, pDetalle);
                }

                var spConfig = "pla.SP_TDocumentoPWDuracionConfiguracion_Insertar";
                var pConfig = new
                {
                    IdDocumento_PW = idDocumentoPw,
                    IdDocumentoPWDuracion = idDocumentoPwDuracion,
                    Usuario = usuario
                };

                _dapperRepository.QuerySPDapper(spConfig, pConfig);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarDocumentoPwDuracion() {ex.Message}", ex);
            }
        }

        public IEnumerable<DocumentoPWDuracionRowVM> ObtenerDocumentoPWDuracionRows(int idDocumentoPW)
        {
            try
            {
                List<DocumentoPWDuracionRowVM> rpta = new List<DocumentoPWDuracionRowVM>();

                var query = @"
            SELECT
                c.IdDocumento_PW AS IdDocumentoPW,
                d.Id AS IdDocumentoPWDuracion,
                d.Titulo,
                d.Introduccion,
                d.PieDePagina,
                dd.Id AS IdDocumentoPWDuracionDetalle,
                dd.IdVersionPrograma,
                dd.DetalleMes,
                dd.DetalleHora
            FROM pla.T_DocumentoPWDuracionConfiguracion c
            INNER JOIN pla.T_DocumentoPWDuracion d
                ON d.Id = c.IdDocumentoPWDuracion AND d.Estado = 1
            LEFT JOIN pla.T_DocumentoPWDuracionDetalle dd
                ON dd.IdDocumentoPWDuracion = d.Id AND dd.Estado = 1
            WHERE c.IdDocumento_PW = @IdDocumentoPW
              AND c.Estado = 1
            ORDER BY dd.IdVersionPrograma ASC;";

                var resultado = _dapperRepository.QueryDapper(query, new { IdDocumentoPW = idDocumentoPW });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocumentoPWDuracionRowVM>>(resultado);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWDuracionRows() {ex.Message}", ex);
            }
        }

        public void InsertarDocumentoPwFechaInicio(SeccionFechaInicioDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                int ObtenerIdDesdeJson(string json)
                {
                    if (string.IsNullOrEmpty(json) || json.Contains("[]")) return 0;
                    var tmp = JsonConvert.DeserializeObject<List<dynamic>>(json);
                    if (tmp == null || tmp.Count == 0) return 0;
                    return Convert.ToInt32(tmp[0].Id);
                }

                var spCab = "pla.SP_TDocumentoPWFechaInicioCabecera_Insertar";
                var pCab = new
                {
                    Titulo = dto.Titulo,
                    SubTitulo = dto.SubTitulo,
                    MostrarWeb = dto.MostrarEnLaWeb,
                    Usuario = usuario
                };

                var rCab = _dapperRepository.QuerySPDapper(spCab, pCab);
                var idCabecera = ObtenerIdDesdeJson(rCab);

                if (idCabecera <= 0)
                    throw new Exception("No se pudo obtener IdDocumentoPWFechaInicioCabecera.");

                foreach (var p in dto.Paises ?? new List<FechaInicioPaisDTO>())
                {
                    if (p.IdPais == null || p.IdPais <= 0) continue;

                    var spPais = "pla.SP_TDocumentoPWFechaInicio_Insertar";
                    var pPais = new
                    {
                        IdPais = p.IdPais,
                        Usuario = usuario
                    };

                    var rPais = _dapperRepository.QuerySPDapper(spPais, pPais);
                    var idFechaInicio = ObtenerIdDesdeJson(rPais);

                    if (idFechaInicio <= 0)
                        throw new Exception("No se pudo obtener IdDocumentoPWFechaInicio.");

                    var detalles = p.Detalles ?? new List<FechaInicioDetalleDTO>();
                    foreach (var d in detalles)
                    {
                        if (d.IdModo == null || d.IdModo <= 0) continue;

                        var spDet = "pla.SP_TDocumentoPWFechaInicioDetalle_Insertar";
                        var pDet = new
                        {
                            IdDocumentoPWFechaInicio = idFechaInicio,
                            IdDocumentoPWFechaInicioModo = d.IdModo,
                            Fecha = d.Fecha,
                            Horario = d.Horario,
                            Usuario = usuario
                        };

                        _dapperRepository.QuerySPDapper(spDet, pDet);
                    }

                    var spCfg = "pla.SP_TDocumentoPWFechaInicioConfiguracion_Insertar";
                    var pCfg = new
                    {
                        IdDocumentoPWFechaInicioCabecera = idCabecera,
                        IdDocumentoPWFechaInicio = idFechaInicio,
                        IdDocumento_PW = idDocumentoPw,
                        Usuario = usuario
                    };

                    _dapperRepository.QuerySPDapper(spCfg, pCfg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarDocumentoPwFechaInicio() {ex.Message}", ex);
            }
        }

        public List<DocumentoPWFechaInicioRowDTO> ObtenerDocumentoPWFechaInicioRows(int idDocumentoPw)
        {
            try
            {
                List<DocumentoPWFechaInicioRowDTO> rpta = new List<DocumentoPWFechaInicioRowDTO>();

                var query = @"
            SELECT
                cab.MostrarWeb as MostrarEnLaWeb,
                cab.Titulo,
                cab.SubTitulo,
                fi.Id AS IdDocumentoPWFechaInicio,
                fi.IdPais,
                det.Id AS IdDetalle,
                det.IdDocumentoPWFechaInicioModo AS IdModo,
                det.Fecha,
                det.Horario
            FROM pla.T_DocumentoPWFechaInicioConfiguracion cfg
            INNER JOIN pla.T_DocumentoPWFechaInicioCabecera cab
                ON cab.Id = cfg.IdDocumentoPWFechaInicioCabecera
            INNER JOIN pla.T_DocumentoPWFechaInicio fi
                ON fi.Id = cfg.IdDocumentoPWFechaInicio
            LEFT JOIN pla.T_DocumentoPWFechaInicioDetalle det
                ON det.IdDocumentoPWFechaInicio = fi.Id
            WHERE cfg.IdDocumento_PW = @IdDocumentoPw
              AND cfg.Estado = 1
              AND cab.Estado = 1
              AND fi.Estado = 1
              AND (det.Id IS NULL OR det.Estado = 1)
            ORDER BY fi.Id, det.Id;";

                var parametros = new
                {
                    IdDocumentoPw = idDocumentoPw
                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DocumentoPWFechaInicioRowDTO>>(resultado);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWFechaInicioRows() {ex.Message}", ex);
            }
        }

        public void InsertarDocumentoPwNotas(SeccionNotasDTO dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                foreach (var n in (dto.Notas ?? new List<NotaDTOV2>()))
                {
                    var vacia =
                        (n.IdNotaTipo == null || n.IdNotaTipo <= 0) &&
                        (n.IdPGeneral == null || n.IdPGeneral <= 0) &&
                        string.IsNullOrWhiteSpace(n.Descripcion) &&
                        (n.Detalles == null || n.Detalles.Count == 0);

                    if (vacia) continue;

                    if (n.IdNotaTipo == null || n.IdNotaTipo <= 0)
                        throw new Exception("Tipo de nota requerido.");

                    var spNota = "pla.SP_TDocumentoPWNota_Insertar";
                    var pNota = new
                    {
                        IdDocumentoPWNotaTipo = n.IdNotaTipo.Value,
                        IdPGeneral = n.IdPGeneral,
                        Descripcion = n.Descripcion,
                        Usuario = usuario
                    };

                    var rNota = _dapperRepository.QuerySPDapper(spNota, pNota);

                    if (string.IsNullOrWhiteSpace(rNota) || rNota.Contains("[]"))
                        throw new Exception("No se pudo obtener IdDocumentoPWNota (insert nota).");

                    var arrNota = JArray.Parse(rNota);
                    var idDocumentoPwNota = arrNota.Count > 0 ? (int)(arrNota[0]["Id"] ?? 0) : 0;

                    if (idDocumentoPwNota <= 0)
                        throw new Exception($"IdDocumentoPWNota inválido (insert nota). Respuesta SP: {rNota}");

                    var spCfg = "pla.SP_TDocumentoPWNotaConfiguracion_Insertar";
                    var pCfg = new
                    {
                        IdDocumento_PW = idDocumentoPw,
                        IdDocumentoPWNota = idDocumentoPwNota,
                        MostrarWeb = dto.MostrarEnLaWeb,
                        Usuario = usuario
                    };

                    _dapperRepository.QuerySPDapper(spCfg, pCfg);

                    var detalles = (n.Detalles ?? new List<NotaDetalleDTO>())
                        .OrderBy(x => x.Orden)
                        .ToList();

                    foreach (var d in detalles)
                    {
                        var spDet = "pla.SP_TDocumentoPWNotaDetalle_Insertar";
                        var pDet = new
                        {
                            IdDocumentoPWNota = idDocumentoPwNota,
                            Orden = d.Orden,
                            InformacionExtra = d.InformacionExtra,
                            IdPais = d.IdPais,
                            Usuario = usuario
                        };

                        _dapperRepository.QuerySPDapper(spDet, pDet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarDocumentoPwNotas() {ex.Message}", ex);
            }
        }


        public List<DocumentoPWNotasRowDTO> ObtenerDocumentoPWNotasRows(int idDocumentoPW)
        {
            try
            {
                var query = @"
            SELECT
                c.IdDocumento_PW              AS IdDocumentoPw,
                c.MostrarWeb                  AS MostrarWeb,
                n.Id                          AS IdDocumentoPWNota,
                n.IdDocumentoPWNotaTipo       AS IdDocumentoPWNotaTipo,
                n.IdPGeneral                  AS IdPGeneral,
                n.Descripcion                 AS Descripcion,
                d.Id                          AS IdDocumentoPWNotaDetalle,
                d.Orden                       AS Orden,
                d.InformacionExtra            AS InformacionExtra,
                d.IdPais                      AS IdPais
            FROM pla.T_DocumentoPWNotaConfiguracion c
            INNER JOIN pla.T_DocumentoPWNota n
                ON n.Id = c.IdDocumentoPWNota
               AND n.Estado = 1
            LEFT JOIN pla.T_DocumentoPWNotaDetalle d
                ON d.IdDocumentoPWNota = n.Id
               AND d.Estado = 1
            WHERE c.IdDocumento_PW = @IdDocumentoPW
              AND c.Estado = 1
            ORDER BY n.Id, d.Orden;
        ";

                var parametros = new { IdDocumentoPW = idDocumentoPW };

                var json = _dapperRepository.QueryDapper(query, parametros);

                if (string.IsNullOrEmpty(json) || json.Contains("[]"))
                    return new List<DocumentoPWNotasRowDTO>();

                return JsonConvert.DeserializeObject<List<DocumentoPWNotasRowDTO>>(json) ?? new List<DocumentoPWNotasRowDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWNotasRows() {ex.Message}", ex);
            }
        }

        public string ObtenerDocumentoPWModalidadRowsSP(int idDocumentoPw)
        {
            var sp = "pla.SP_DocumentoPwModalidadPorIdDocumentoPW";
            var parametros = new { IdDocumento_PW = idDocumentoPw };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidadIntroduccion_Insertar(string? introduccion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidadIntroduccion_Insertar";
            var parametros = new { Introduccion = introduccion, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidadIntroduccion_Actualizar(int id, string? introduccion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidadIntroduccion_Insertar";
            var parametros = new { Id = id, Introduccion = introduccion, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidad_Insertar(int idModalidadPortal, string? subTitulo, string? descripcion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidad_Insertar";
            var parametros = new
            {
                IdModalidadPortal = idModalidadPortal,
                SubTitulo = subTitulo,
                Descripcion = descripcion,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidad_Actualizar(int id, int idModalidadPortal, string? subTitulo, string? descripcion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidad_Actualizar";
            var parametros = new
            {
                Id = id,
                IdModalidadPortal = idModalidadPortal,
                SubTitulo = subTitulo,
                Descripcion = descripcion,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidadDetalle_Insertar(int idDocumentoPWModalidad, int orden, string? tipo,string? beneficio, int? idPais, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidadDetalle_Insertar";
            var parametros = new
            {
                IdDocumentoPWModalidad = idDocumentoPWModalidad,
                Orden = orden,
                Tipo = tipo,
                IdPais = idPais,
                Beneficio = beneficio,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidadDetalle_Actualizar(int id, int idDocumentoPWModalidad, int orden, string? tipo, string? beneficio, int? idPais, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidadDetalle_Actualizar";
            var parametros = new
            {
                Id = id,
                IdDocumentoPWModalidad = idDocumentoPWModalidad,
                Orden = orden,
                Tipo = (tipo ?? "").Trim().ToUpper(),
                Beneficio = beneficio,
                IdPais = idPais,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_DocumentoPWModalidadConfiguracion_RegistrarCambios(int idDocumentoPw, int idIntroduccion, int idDocumentoPWModalidad, string usuario)
        {
            var sp = "pla.SP_DocumentoPWModalidadConfiguracion_RegistrarCambios";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWModalidadIntroduccion = idIntroduccion,
                IdDocumentoPWModalidad = idDocumentoPWModalidad,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidad_Desactivar(int idDocumentoPWModalidad, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidad_Desactivar";
            var parametros = new { Id = idDocumentoPWModalidad, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidadDetalle_Desactivar(int idDocumentoPWModalidadDetalle, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidadDetalle_Desactivar";
            var parametros = new { Id = idDocumentoPWModalidadDetalle, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWModalidadConfiguracion_DesactivarPorModalidad(int idDocumentoPw, int idDocumentoPWModalidad, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWModalidadConfiguracion_DesactivarPorModalidad";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWModalidad = idDocumentoPWModalidad,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string ObtenerDocumentoPWDuracionRowsSP(int idDocumentoPw)
        {
            var sp = "pla.SP_TDocumentoPWDuracionObtener";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWDuracion_Actualizar(int id, string? titulo, string? introduccion, string? pieDePagina, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWDuracion_Actualizar";
            var parametros = new
            {
                Id = id,
                Titulo = titulo,
                Introduccion = introduccion,
                PieDePagina = pieDePagina,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWDuracionDetalle_Actualizar(int id, int idDocumentoPWDuracion, int idVersionPrograma, string? detalleMes, string? detalleHora, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWDuracionDetalle_Actualizar";
            var parametros = new
            {
                Id = id,
                IdDocumentoPWDuracion = idDocumentoPWDuracion,
                IdVersionPrograma = idVersionPrograma,
                DetalleMes = detalleMes,
                DetalleHora = detalleHora,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWDuracionDetalle_Desactivar(int id, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWDuracionDetalle_Desactivar";
            var parametros = new
            {
                Id = id,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_DocumentoPWDuracionConfiguracion_RegistrarCambios(int idDocumentoPw, int idDocumentoPWDuracion, string usuario)
        {
            var sp = "pla.SP_DocumentoPWDuracionConfiguracion_RegistrarCambios";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWDuracion = idDocumentoPWDuracion,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWDuracion_Insertar(string? titulo,string? introduccion, string? pieDePagina,  string usuario)
        {
            var sp = "pla.SP_TDocumentoPWDuracion_Insertar";
            var parametros = new
            {
                Titulo = titulo,
                Introduccion = introduccion,
                PieDePagina = pieDePagina,
                Usuario = usuario
            };

            return _dapperRepository.QuerySPDapper(sp, parametros);
        }
        public string SP_TDocumentoPWDuracionDetalle_Insertar(int idDocumentoPWDuracion,int idVersionPrograma,string? detalleMes, string? detalleHora, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWDuracionDetalle_Insertar";
            var parametros = new
            {
                IdDocumentoPWDuracion = idDocumentoPWDuracion,
                IdVersionPrograma = idVersionPrograma,
                DetalleMes = detalleMes,
                DetalleHora = detalleHora,
                Usuario = usuario
            };

            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWDuracionConfiguracion_Insertar(int idDocumentoPw,int idDocumentoPWDuracion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWDuracionConfiguracion_Insertar";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWDuracion = idDocumentoPWDuracion,
                Usuario = usuario
            };

            return _dapperRepository.QuerySPDapper(sp, parametros);
        }
        public string SP_TDocumentoPWFechaInicio_ObtenerRows(int idDocumentoPw)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioObtener";
            var parametros = new { IdDocumento_PW = idDocumentoPw };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioCabecera_Insertar(string? titulo, string? subTitulo, bool mostrarEnLaWeb, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioCabecera_Insertar";
            var parametros = new
            {
                Titulo = titulo,
                SubTitulo = subTitulo,
                MostrarWeb = mostrarEnLaWeb,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioCabecera_Actualizar(int id, string? titulo, string? subTitulo, bool mostrarEnLaWeb, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioCabecera_Actualizar";
            var parametros = new
            {
                Id = id,
                Titulo = titulo,
                SubTitulo = subTitulo,
                MostrarWeb = mostrarEnLaWeb,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicio_Insertar(int? idPais, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicio_Insertar";
            var parametros = new { IdPais = idPais, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicio_Actualizar(int id, int? idPais, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicio_Actualizar";
            var parametros = new { Id = id, IdPais = idPais, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioDetalle_Insertar(int idDocumentoPWFechaInicio, int? idModo, DateTime? fecha, string? horario, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioDetalle_Insertar";
            var parametros = new
            {
                IdDocumentoPWFechaInicio = idDocumentoPWFechaInicio,
                IdDocumentoPWFechaInicioModo = idModo,
                Fecha = fecha,
                Horario = horario,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioDetalle_Actualizar(int id, int idDocumentoPWFechaInicio, int? idModo, DateTime? fecha, string? horario, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioDetalle_Actualizar";
            var parametros = new
            {
                Id = id,
                IdDocumentoPWFechaInicio = idDocumentoPWFechaInicio,
                IdDocumentoPWFechaInicioModo = idModo,
                Fecha = fecha,
                Horario = horario,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_DocumentoPWFechaInicioConfiguracion_RegistrarCambios(int idDocumentoPWFechaInicioCabecera, int idDocumentoPWFechaInicio, int idDocumentoPw, string usuario)
        {
            var sp = "pla.SP_DocumentoPWFechaInicioConfiguracion_RegistrarCambios";
            var parametros = new
            {
                IdDocumentoPWFechaInicioCabecera = idDocumentoPWFechaInicioCabecera,
                IdDocumentoPWFechaInicio = idDocumentoPWFechaInicio,
                IdDocumento_PW = idDocumentoPw,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioDetalle_Desactivar(int id, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioDetalle_Desactivar";
            var parametros = new { Id = id, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioDetalle_DesactivarPorFechaInicio(int idDocumentoPWFechaInicio, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioDetalle_DesactivarPorFechaInicio";
            var parametros = new { IdDocumentoPWFechaInicio = idDocumentoPWFechaInicio, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicio_Desactivar(int idDocumentoPWFechaInicio, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicio_Desactivar";
            var parametros = new { Id = idDocumentoPWFechaInicio, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWFechaInicioConfiguracion_DesactivarPorFechaInicio(int idDocumentoPw, int idDocumentoPWFechaInicio, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWFechaInicioConfiguracion_DesactivarPorFechaInicio";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWFechaInicio = idDocumentoPWFechaInicio,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNota_ObtenerRows(int idDocumentoPw)
        {
            var sp = "pla.SP_TDocumentoPWNotaObtener";
            var parametros = new { IdDocumento_PW = idDocumentoPw };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNota_Insertar(int idDocumentoPWNotaTipo, int? idPGeneral, string? descripcion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNota_Insertar";
            var parametros = new
            {
                IdDocumentoPWNotaTipo = idDocumentoPWNotaTipo,
                IdPGeneral = idPGeneral,
                Descripcion = descripcion,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNota_Actualizar(int id, int idDocumentoPWNotaTipo, int? idPGeneral, string? descripcion, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNota_Actualizar";
            var parametros = new
            {
                Id = id,
                IdDocumentoPWNotaTipo = idDocumentoPWNotaTipo,
                IdPGeneral = idPGeneral,
                Descripcion = descripcion,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNota_Desactivar(int id, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNota_Desactivar";
            var parametros = new { Id = id, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNotaDetalle_Insertar(int idDocumentoPWNota, int orden, string? informacionExtra, int? idPais, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNotaDetalle_Insertar";
            var parametros = new
            {
                IdDocumentoPWNota = idDocumentoPWNota,
                Orden = orden,
                InformacionExtra = informacionExtra,
                IdPais = idPais,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNotaDetalle_Actualizar(int id, int idDocumentoPWNota, int orden, string? informacionExtra, int? idPais, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNotaDetalle_Actualizar";
            var parametros = new
            {
                Id = id,
                IdDocumentoPWNota = idDocumentoPWNota,
                Orden = orden,
                InformacionExtra = informacionExtra,
                IdPais = idPais,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNotaDetalle_Desactivar(int id, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNotaDetalle_Desactivar";
            var parametros = new { Id = id, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNotaDetalle_DesactivarPorNota(int idDocumentoPWNota, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNotaDetalle_DesactivarPorNota";
            var parametros = new { IdDocumentoPWNota = idDocumentoPWNota, Usuario = usuario };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_DocumentoPWNotaConfiguracion_RegistrarCambios(int idDocumentoPw, int idDocumentoPWNota, bool mostrarWeb, string usuario)
        {
            var sp = "pla.SP_DocumentoPWNotaConfiguracion_RegistrarCambios";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWNota = idDocumentoPWNota,
                MostrarWeb = mostrarWeb,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }

        public string SP_TDocumentoPWNotaConfiguracion_DesactivarPorNota(int idDocumentoPw, int idDocumentoPWNota, string usuario)
        {
            var sp = "pla.SP_TDocumentoPWNotaConfiguracion_DesactivarPorNota";
            var parametros = new
            {
                IdDocumento_PW = idDocumentoPw,
                IdDocumentoPWNota = idDocumentoPWNota,
                Usuario = usuario
            };
            return _dapperRepository.QuerySPDapper(sp, parametros);
        }
    }
}
