using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

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

    }
}
