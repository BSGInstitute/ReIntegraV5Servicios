using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionBeneficioProgramaGeneralRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionBeneficioProgramaGeneral
    /// </summary>
    public class ConfiguracionBeneficioProgramaGeneralRepository : GenericRepository<TConfiguracionBeneficioProgramaGeneral>, IConfiguracionBeneficioProgramaGeneralRepository
    {
        private Mapper _mapper;

        public ConfiguracionBeneficioProgramaGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionBeneficioProgramaGeneral, ConfiguracionBeneficioProgramaGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionBeneficioProgramaGeneral MapeoEntidad(ConfiguracionBeneficioProgramaGeneral entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionBeneficioProgramaGeneral modelo = _mapper.Map<TConfiguracionBeneficioProgramaGeneral>(entidad);

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

        public TConfiguracionBeneficioProgramaGeneral Add(ConfiguracionBeneficioProgramaGeneral entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneral = MapeoEntidad(entidad);
                base.Insert(ConfiguracionBeneficioProgramaGeneral);
                return ConfiguracionBeneficioProgramaGeneral;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionBeneficioProgramaGeneral Update(ConfiguracionBeneficioProgramaGeneral entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneral = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionBeneficioProgramaGeneral.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionBeneficioProgramaGeneral);
                return ConfiguracionBeneficioProgramaGeneral;
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


        public IEnumerable<TConfiguracionBeneficioProgramaGeneral> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneral> listadoEntidad)
        {
            try
            {
                List<TConfiguracionBeneficioProgramaGeneral> listado = new List<TConfiguracionBeneficioProgramaGeneral>();
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

        public IEnumerable<TConfiguracionBeneficioProgramaGeneral> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionBeneficioProgramaGeneral> listado = new List<TConfiguracionBeneficioProgramaGeneral>();
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

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 26/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<ConfiguracionBeneficioProgramaGeneralAlternoDTO> ObtenerPorIdPgeneralIdBeneficio(int idPGeneral, int idBeneficio)
        {
            try
            {
                List<ConfiguracionBeneficioProgramaGeneralAlternoDTO> rpta = new List<ConfiguracionBeneficioProgramaGeneralAlternoDTO>();
                var query = @"SELECT Id	,
		                IdPGeneral AS IdPgeneral,
		                IdBeneficio,
		                Tipo,
		                Asosiar,
		                Entrega,
		                AvanceAcademico,
		                DeudaPendiente,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                OrdenBeneficio,
		                DatosAdicionales,
		                Requisitos,
		                ProcesoSolicitud,
		                DetallesAdicionales 
	                FROM pla.T_ConfiguracionBeneficioProgramaGeneral 
                    WHERE Estado = 1 AND IdPGeneral=@idPGeneral AND IdBeneficio=@idBeneficio";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral, idBeneficio });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralAlternoDTO>>(resultado)!;
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
        /// Obtiene registros de T_ConfiguracionBeneficioProgramaGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<ConfiguracionBeneficioProgramaGeneralComboDTO> </returns>
        public IEnumerable<ConfiguracionBeneficioProgramaGeneralComboDTO> ObtenerCombo()
        {
            try
            {
                List<ConfiguracionBeneficioProgramaGeneralComboDTO> rpta = new List<ConfiguracionBeneficioProgramaGeneralComboDTO>();
                var query = @"SELECT Id,IdPGeneral,IdBeneficio,Tipo FROM pla.T_ConfiguracionBeneficioProgramaGeneral WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralComboDTO>>(resultado);
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
        /// Obtener Programa General Configuracion Beneficios
        /// </summary>
        /// <param name="IdPgeneral">Id Programa General </param>
        /// <returns> List<PgeneralConfiguracionBeneficioDTO></returns> 
        public List<PgeneralConfiguracionBeneficioDTO> ObtenerPGeneralConfiguracionBeneficios(int idPgeneral)
        {
            try
            {
                List<PgeneralConfiguracionBeneficioDTO> rpta = new List<PgeneralConfiguracionBeneficioDTO>();
                string query = "SELECT Id, IdPGeneral,IdBeneficio,OrdenBeneficio,DatosAdicionales,TipoBeneficio,Descripcion,Entrega,Asosiar,AvanceAcademico,DeudaPendiente FROM pla.V_BeniciosPartnerDocumento WHERE IdPGeneral=@IdPgeneral";

                var pgeneralBeneficios = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPgeneral });
                if (!string.IsNullOrEmpty(pgeneralBeneficios) && !pgeneralBeneficios.Contains("[]") && pgeneralBeneficios != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralConfiguracionBeneficioDTO>>(pgeneralBeneficios)!;
                    foreach (var estados in rpta)
                    {
                        estados.EstadosMatricula = new List<int>();
                        estados.SubEstadosMatricula = new List<int>();
                        estados.Paises = new List<int>();
                        estados.Versiones = new List<int>();
                        string queryEstado = "SELECT IdEstadoMatricula AS Valor FROM pla.T_ConfiguracionBeneficioProgramaGeneralEstadoMatricula WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var queryEstadoDB = _dapperRepository.QueryDapper(queryEstado, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryEstadoDB) && !queryEstadoDB.Contains("[]"))
                        {
                            var resEstado = JsonConvert.DeserializeObject<List<IntDTO>>(queryEstadoDB)!;
                            estados.EstadosMatricula = resEstado.Select(x => x.Valor.GetValueOrDefault()).ToList();
                        }
                        string querySubEstado = "SELECT IdSubEstadoMatricula AS Valor FROM pla.T_ConfiguracionBeneficioProgramaGeneralSubEstado WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var querySubEstadoDB = _dapperRepository.QueryDapper(querySubEstado, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(querySubEstadoDB) && !querySubEstadoDB.Contains("[]"))
                        {
                            var resSubEstado = JsonConvert.DeserializeObject<List<IntDTO>>(querySubEstadoDB)!;
                            estados.SubEstadosMatricula = resSubEstado.Select(x => x.Valor.GetValueOrDefault()).ToList();
                        }
                        string queryPais = "SELECT IdPais AS Valor FROM pla.T_ConfiguracionBeneficioProgramaGeneralPais WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var queryPaisDB = _dapperRepository.QueryDapper(queryPais, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryPaisDB) && !queryPaisDB.Contains("[]"))
                        {
                            var resPaises = JsonConvert.DeserializeObject<List<IntDTO>>(queryPaisDB)!;
                            estados.Paises = resPaises.Select(x => x.Valor.GetValueOrDefault()).ToList();
                        }
                        string queryVersion = "SELECT IdVersionPrograma AS Valor FROM pla.T_ConfiguracionBeneficioProgramaGeneralVersion WHERE Estado = 1 and IdConfiguracionBeneficioPGneral=@IdConfiguracionBeneficioPGneral";
                        var queryVersionDB = _dapperRepository.QueryDapper(queryVersion, new { IdConfiguracionBeneficioPGneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryVersionDB) && !queryVersionDB.Contains("[]"))
                        {
                            var resVersion = JsonConvert.DeserializeObject<List<IntDTO>>(queryVersionDB)!;
                            estados.Versiones = resVersion.Select(x => x.Valor.GetValueOrDefault()).ToList();
                        }
                        string queryDatoAdicional = "SELECT IdBeneficioDatoAdicional AS Valor FROM [pla].[T_ConfiguracionBeneficioProgramaGeneralDatoAdicional]WHERE Estado = 1 and IdConfiguracionBeneficioPGeneral=@IdConfiguracionBeneficioPGeneral";
                        var queryDatoAdicionalDB = _dapperRepository.QueryDapper(queryDatoAdicional, new { IdConfiguracionBeneficioPGeneral = estados.Id });
                        if (!string.IsNullOrEmpty(queryDatoAdicionalDB) && !queryDatoAdicionalDB.Contains("[]"))
                        {
                            var resDatoAdicional = JsonConvert.DeserializeObject<List<IntDTO>>(queryDatoAdicionalDB)!;
                            estados.DatosAdicional = resDatoAdicional.Select(x => x.Valor.GetValueOrDefault()).ToList();
                        }
                    }
                }
                return rpta;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 12/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtener Programa General Configuracion Beneficios
        /// </summary>
        /// <param name="IdPgeneral">Id Programa General </param>
        /// <returns> List<PgeneralConfiguracionBeneficioDTO></returns> 
        public async Task<List<BeneficiosConfiguradosProgramaGeneralDTO>> ObtenerBeneficiosConfiguradosProgramaGeneralAsync(int idPgeneral, int idCodigoPais)
        {
            try
            {
                List<BeneficiosConfiguradosProgramaGeneralDTO> ConfiguracionBeneficio = new List<BeneficiosConfiguradosProgramaGeneralDTO>();
                string pgeneralBeneficios = await _dapperRepository.QuerySPDapperAsync("pla.SP_ObtenerBeneficiosConfiguradosProgramaGeneral", new { IdPGeneral = idPgeneral, IdPais = idCodigoPais });
                if (!string.IsNullOrEmpty(pgeneralBeneficios) && !pgeneralBeneficios.Contains("[]"))
                {
                    ConfiguracionBeneficio = JsonConvert.DeserializeObject<List<BeneficiosConfiguradosProgramaGeneralDTO>>(pgeneralBeneficios)!;
                }
                return ConfiguracionBeneficio;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener beneficios del programa general Tipo 1 version 2 Internacional
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<BeneficioDTO> </returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2Internacional(int idPGeneral)
        {
            try
            {
                List<BeneficioDTO> rpta = new List<BeneficioDTO>();
                var query = @"
                    SELECT Paquete,Titulo,OrdenBeneficio
                    FROM pla.V_BeneficiosProgramaTipo1V2
                    WHERE Id = @idPGeneral
                    GROUP BY OrdenBeneficio,Paquete,Titulo";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1V2Internacionaljson(int idPGeneral)
        {
            try
            {
                List<BeneficioDTOjson> rpta = new List<BeneficioDTOjson>();
                var query = @"
                    SELECT Paquete,Titulo,OrdenBeneficio
                    FROM pla.V_BeneficiosProgramaTipo1V2
                    WHERE Id = @idPGeneral
                    GROUP BY OrdenBeneficio,Paquete,Titulo";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioDTOjson>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener beneficios del programa general Tipo 1 version 2
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <param name="codigoPais">Codigo pais </param>
        /// <returns> List<BeneficioDTO> </returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2(int idPGeneral, int codigoPais)
        {
            try
            {
                List<BeneficioDTO> rpta = new List<BeneficioDTO>();
                var query = @"
                    SELECT Paquete, Titulo, OrdenBeneficio
                    FROM pla.V_BeneficiosProgramaTipo1V2
                    WHERE Id = @idPGeneral And CodigoPais = @codigoPais";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral, codigoPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1V2json(int idPGeneral, int codigoPais)
        {
            try
            {
                List<BeneficioDTOjson> rpta = new List<BeneficioDTOjson>();
                var query = @"
                    SELECT 
                        b.Paquete, 
                        b.Titulo, 
                        b.OrdenBeneficio, 
                        v.Nombre AS Version
                    FROM pla.V_BeneficiosProgramaTipo1V2 b
                    LEFT JOIN pla.T_VersionPrograma v ON b.Paquete = v.Id
                    WHERE b.Id = @idPGeneral 
                      AND b.CodigoPais = @codigoPais
                    ORDER BY b.OrdenBeneficio, b.Paquete, b.Titulo";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral, codigoPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioDTOjson>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los beneficios por programa general y pais tipo 1
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <param name="codigoPais">Codigo del pais </param>
        /// <returns> BeneficioDTO </returns>
        public List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais)
        {
            try
            {
                List<BeneficioDTO> rpta = new List<BeneficioDTO>();
                var query = @"
                    SELECT Paquete, Titulo, OrdenBeneficio
                    FROM pla.V_BeneficiosProgramaTipo1
                    WHERE Id = @idPGeneral
	                    AND CodigoPais = @codigoPais";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral, codigoPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1json(int idPGeneral, int codigoPais)
        {
            try
            {
                List<BeneficioDTOjson> rpta = new List<BeneficioDTOjson>();
                var query = @"
                    SELECT Paquete, Titulo, OrdenBeneficio
                    FROM pla.V_BeneficiosProgramaTipo1
                    WHERE Id = @idPGeneral
	                    AND CodigoPais = @codigoPais";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral, codigoPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<BeneficioDTOjson>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el titulo de un beneficio por programa general tipo 2
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> BeneficioDTO </returns>
        public BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral)
        {
            BeneficioDTO beneficio = new BeneficioDTO();
            var query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = @nombre AND IdProgramaGeneral = @idPGeneral AND EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
            var beneficiosDB = _dapperRepository.FirstOrDefault(query, new { idPGeneral, nombre = "Beneficios" });
            if (!string.IsNullOrEmpty(beneficiosDB) && !beneficiosDB.Equals("null"))
            {
                beneficio = JsonConvert.DeserializeObject<BeneficioDTO>(beneficiosDB);
            }
            return beneficio;
        }

        public BeneficioDTOjson ObtenerBeneficiosPGeneralTipo2json(int idPGeneral)
        {
            BeneficioDTOjson beneficio = new BeneficioDTOjson();
            var query = "SELECT Titulo FROM pla.V_BeneficiosProgramaTipo2 WHERE TituloDocumentoSeccion = @nombre AND IdProgramaGeneral = @idPGeneral AND EstadoDocumentoSeccion = 1 AND EstadoProgramaGeneralDocumento = 1 AND EstadoDocumento = 1 AND EstadoProgramaGeneral = 1";
            var beneficiosDB = _dapperRepository.FirstOrDefault(query, new { idPGeneral, nombre = "Beneficios" });
            if (!string.IsNullOrEmpty(beneficiosDB) && !beneficiosDB.Equals("null"))
            {
                beneficio = JsonConvert.DeserializeObject<BeneficioDTOjson>(beneficiosDB);
            }
            return beneficio;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los detalles de Configuracion Beneficio PGeneral
        /// </summary>
        /// <param name="idBeneficio">Id Beneficio</param>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> BeneficioDetalleRequisitoDTO </returns>
        public BeneficioDetalleRequisitoDTO ObtenerBeneficioDetalleRequisitoPorPGeneralYBeneficio(int idBeneficio, int idPGeneral)
        {
            try
            {
                var rpta = new BeneficioDetalleRequisitoDTO();
                var query = @"SELECT Requisitos,ProcesoSolicitud,DetallesAdicionales 
                            FROM pla.T_ConfiguracionBeneficioProgramaGeneral
                            WHERE Estado=1 AND IdPGeneral= " + idPGeneral + " AND IdBeneficio= " + idBeneficio;
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral, idBeneficio });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<BeneficioDetalleRequisitoDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 18/07/2023
        /// Version: 1.0 
        /// <param name="id"> Primary Key </param>
        /// <summary>
        /// Obtiene el registro por el Primary Key.
        /// </summary> 
        /// <returns> ConfiguracionBeneficioProgramaGeneral </returns>
        public ConfiguracionBeneficioProgramaGeneral ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdPGeneral IdPgeneral,
                                       IdBeneficio,
                                       Tipo,
                                       Asosiar,
                                       Entrega,
                                       AvanceAcademico,
                                       DeudaPendiente,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion,
                                       OrdenBeneficio,
                                       DatosAdicionales,
                                       Requisitos,
                                       ProcesoSolicitud,
                                       DetallesAdicionales
                                FROM pla.V_ConfiguracionBeneficioProgramaGeneral
                                WHERE Estado = 1
                                      AND Id = @Id;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<ConfiguracionBeneficioProgramaGeneral>(resultado);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public IEnumerable<DocumentoPwVersionesPGeneralDTO> ObtenerIntroduccionBeneficio(int idPGeneral)
        {
            try
            {
                List<DocumentoPwVersionesPGeneralDTO> respuesta = new List<DocumentoPwVersionesPGeneralDTO>();
                var query = @"
                    SELECT
	                  Introduccion,IdDocumentoPw,IdVersionPrograma,IdPGeneral
                    FROM pla.V_PW_ObtenerIntroduccionBeneficio
                    WHERE  IdPGeneral=@idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<DocumentoPwVersionesPGeneralDTO>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerIntroduccionVersionDocumento() {ex.Message}", ex);
            }
        }


    }
}
