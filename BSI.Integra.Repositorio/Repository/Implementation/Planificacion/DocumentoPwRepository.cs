using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Dapper;
using Newtonsoft.Json;
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

                var query = @"
                SELECT
                    mc.IdDocumento_PW,
                    mi.Id AS IdDocumentoPWModalidadIntroduccion,
                    mi.Introduccion,
                    m.Id AS IdDocumentoPWModalidad,
                    m.IdModalidadPortal,
                    m.SubTitulo,
                    m.Descripcion,
                    md.Id AS IdDocumentoPWModalidadDetalle,
                    md.Orden,
                    md.Tipo,
                    md.IdPais,
                    md.Beneficio
                FROM pla.T_DocumentoPWModalidadConfiguracion mc
                INNER JOIN pla.T_DocumentoPWModalidadIntroduccion mi
                    ON mi.Id = mc.IdDocumentoPWModalidadIntroduccion
                INNER JOIN pla.T_DocumentoPWModalidad m
                    ON m.Id = mc.IdDocumentoPWModalidad
                LEFT JOIN pla.T_DocumentoPWModalidadDetalle md
                    ON md.IdDocumentoPWModalidad = m.Id AND md.Estado = 1
                WHERE mc.IdDocumento_PW = @IdDocumentoPW
                  AND mc.Estado = 1
                ORDER BY m.Id ASC, md.Orden ASC;";

                var resultado = _dapperRepository.QueryDapper(query, new { IdDocumentoPW = idDocumentoPW });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DocumentoPWModalidadRowVM>>(resultado);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWModalidadRows() {ex.Message}", ex);
            }
        }

    }
}
