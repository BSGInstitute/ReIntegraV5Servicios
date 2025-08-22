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
    /// Repositorio: FaseOportunidadRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_FaseOportunidad
    /// </summary>
    public class FaseOportunidadRepository : GenericRepository<TFaseOportunidad>, IFaseOportunidadRepository
    {
        private Mapper _mapper;

        public FaseOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFaseOportunidad, FaseOportunidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFaseOportunidad MapeoEntidad(FaseOportunidad entidad)
        {
            try
            {
                //crea la entidad padre
                TFaseOportunidad modelo = _mapper.Map<TFaseOportunidad>(entidad);

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

        public TFaseOportunidad Add(FaseOportunidad entidad)
        {
            try
            {
                var FaseOportunidad = MapeoEntidad(entidad);
                base.Insert(FaseOportunidad);
                return FaseOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFaseOportunidad Update(FaseOportunidad entidad)
        {
            try
            {
                var FaseOportunidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FaseOportunidad.RowVersion = entidadExistente.RowVersion;

                base.Update(FaseOportunidad);
                return FaseOportunidad;
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


        public IEnumerable<TFaseOportunidad> Add(IEnumerable<FaseOportunidad> listadoEntidad)
        {
            try
            {
                List<TFaseOportunidad> listado = new List<TFaseOportunidad>();
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

        public IEnumerable<TFaseOportunidad> Update(IEnumerable<FaseOportunidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFaseOportunidad> listado = new List<TFaseOportunidad>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FaseOportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<FaseOportunidadComboDTO> ObtenerCombo()
        {
            try
            {
                List<FaseOportunidadComboDTO> respuesta = new List<FaseOportunidadComboDTO>();

                var query = "SELECT Id, Nombre, Codigo FROM pla.T_FaseOportunidad WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<FaseOportunidadComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FaseOportunidad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<FaseOportunidadComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<FaseOportunidadComboDTO> respuesta = new List<FaseOportunidadComboDTO>();
                var query = "SELECT Id, Nombre, Codigo FROM pla.T_FaseOportunidad WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<FaseOportunidadComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 09/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FaseOportunidad
        /// </summary>
        /// <returns> List<FaseOportunidadDTO> </returns>
        public IEnumerable<FaseOportunidadDTO> ObtenerFaseOportunidad()
        {
            try
            {
                List<FaseOportunidadDTO> rpta = new List<FaseOportunidadDTO>();
                var query = @"SELECT Id, Codigo, Nombre, NroMinutos, IdActividad, MaxNumDias, MinNumDias, TasaConversionEsperada, Meta, Final, ReporteMeta, EnSeguimiento, EsCierre, Estado, FechaCreacion, FechaModificacion,
                            UsuarioCreacion, UsuarioModificacion, Descripcion, VisibleEnReporte
                            FROM pla.T_FaseOportunidad
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FaseOportunidadDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si la Oportunidad se encuentra en Fase de Cierre
        /// </summary>
        /// <param name="idFase">Id de la Fase Oportunidad</param>
        /// <returns> bool </returns>
        public bool ValidarFaseCierreOportunidad(int idFase)
        {
            try
            {
                List<ValorIntDTO> fases = new List<ValorIntDTO>();
                var query = @"
                    SELECT Id AS Valor
                    FROM pla.T_FaseOportunidad
                    WHERE Codigo in ('D','RN4','NI','IS','RN3','RN2-B','RN2-A')
	                    AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    fases = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                }
                return fases.Any(f => f.Valor == idFase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Valida si la Oportunidad se encuentra en Fase de Cierre
        /// </summary>
        /// <param name="idFase">Id de la Fase Oportunidad</param>
        /// <returns> bool </returns>
        public async Task<bool> ValidarFaseCierreOportunidadAsync(int idFase)
        {
            try
            {
                List<ValorIntDTO> fases = new List<ValorIntDTO>();
                var query = @"
                    SELECT Id AS Valor
                    FROM pla.T_FaseOportunidad
                    WHERE Codigo in ('D','RN4','NI','IS','RN3','RN2-B','RN2-A')
	                    AND Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    fases = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                }
                return fases.Any(f => f.Valor == idFase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna si la Fase como parametro es una Fase IS.
        /// </summary>
        /// <param name="idFase">Id de la Fase Oportunidad</param>
        /// <returns> bool </returns>
        public bool ValidarFaseIS(int idFase)
        {
            try
            {
                Dictionary<string, string> fase = new Dictionary<string, string>();
                var query = @"SELECT Codigo FROM pla.T_FaseOportunidad WHERE Estado = 1 AND Id = @idFase";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idFase });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    fase = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultado);
                }
                return fase.Any(f => f.Value == "IS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna si la Fase como parametro es una Fase IS.
        /// </summary>
        /// <param name="idFase">Id de la Fase Oportunidad</param>
        /// <returns> bool </returns>
        public async Task<bool> ValidarFaseISAsync(int idFase)
        {
            try
            {
                Dictionary<string, string> fase = new Dictionary<string, string>();
                var query = @"SELECT Codigo FROM pla.T_FaseOportunidad WHERE Estado = 1 AND Id = @idFase";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idFase });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    fase = JsonConvert.DeserializeObject<Dictionary<string, string>>(resultado);
                }
                return fase.Any(f => f.Value == "IS");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna el Id de la Fase Maxima segun dos fases entregadas.
        /// </summary>
        /// <param name="faseUno"> Fase Uno </param>
        /// <param name="faseDos"> Fase Dos </param>
        /// <returns> Retorna el Id de la Fase Maxima segun dos fases entregadas : int en caso de no entregar id's aptops se retorna 2  </returns>
        public int ObternerFaseMaximaHistoria(int FaseUno, int FaseDos)
        {
            try
            {
                ValorIntDTO fase = new ValorIntDTO();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("pla.SP_ObterFaseMaximaOportunidad", new { FaseUno, FaseDos });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    fase = JsonConvert.DeserializeObject<ValorIntDTO>(resultado)!;
                    if (fase.Valor == null)
                        return 2;
                    return fase.Valor.Value;
                }
                else
                    return 2;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna el Id de la Fase Maxima segun dos fases entregadas.
        /// </summary>
        /// <param name="faseUno"> Fase Uno </param>
        /// <param name="faseDos"> Fase Dos </param>
        /// <returns> Retorna el Id de la Fase Maxima segun dos fases entregadas : int en caso de no entregar id's aptops se retorna 2  </returns>
        public async Task<int> ObternerFaseMaximaHistoriaAsync(int FaseUno, int FaseDos)
        {
            try
            {
                ValorIntDTO fase = new ValorIntDTO();

                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync("pla.SP_ObterFaseMaximaOportunidad", new { FaseUno, FaseDos });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    fase = JsonConvert.DeserializeObject<ValorIntDTO>(resultado)!;
                    if (fase.Valor == null)
                        return 2;
                    return fase.Valor.Value;
                }
                else
                    return 2;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Margiory Ramiresz Neyra.
        /// Fecha: 02/11/2022
        /// <summary>
        /// Obtiene Lista de Fases de oportunidad para filtros de fromularios
        /// </summary>
        /// <returns>Lista de objetos de clase FaseOportunidadComboDTO</returns>
        public List<FaseOportunidadComboDTO> ObtenerFaseOportunidadTodoFiltro()
        {
            try
            {
                List<FaseOportunidadComboDTO> fasesOportunidad = new List<FaseOportunidadComboDTO>();
                var query = "SELECT Id, Nombre, Codigo FROM pla.V_TFaseOportunidad_ParaFiltro WHERE estado = 1";
                var fasesOportunidadDB = _dapperRepository.QueryDapper(query, new { });
                fasesOportunidad = JsonConvert.DeserializeObject<List<FaseOportunidadComboDTO>>(fasesOportunidadDB);
                return fasesOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fase oportunidad mediante Id interaccionChat
        /// </summary>
        /// <param name="idInteraccionChat"></param>
        /// <returns> FaseOportunidadInteraccionDTO </returns>
        public FaseOportunidadInteraccionDTO ObtenerFaseOportunidadPorInteraccionId(int idInteraccionChat)
        {
            try
            {
                string query = @"
                                SELECT 
                                    Id, IdFaseOportunidadPortal 
                                FROM 
                                    com.V_ObtenerFaseOportunidadPorInteraccionChatId 
                                WHERE 
                                    Id = @IdInteraccionChat";
                var faseOportunidad = _dapperRepository.FirstOrDefault(query, new { IdInteraccionChat = idInteraccionChat });
                return JsonConvert.DeserializeObject<FaseOportunidadInteraccionDTO>(faseOportunidad)!;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene datos de la Oportunidad mediante IdFaseOportunidad
		/// </summary>
		/// <param name="idFaseOportunidadPortal"></param>
		/// <returns> OportunidadDatosChatDTO </returns>
		public OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortal(string idFaseOportunidadPortal)
        {
            try
            {
                string query = @"
                                SELECT 
                                    IdOportunidad, IdContacto, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno, Direccion, Telefono, Celular, Email1, Email2, 
                                    IdCodigoPais, IdCiudad, IdCargo, IdAFormacion, IdATrabajo, IdIndustria, IdCentroCosto, IdTipoDato, IdFaseOportunidad, IdOrigen, IdEmpresa 
                                FROM 
                                    com.V_ObtenerDatosPorIdFaseOportunidad 
                                WHERE 
                                    Estado = 1 and IdFaseOportunidadPortal = @IdFaseOportunidadPortal";
                var faseOportunidadChat = _dapperRepository.FirstOrDefault(query, new { IdFaseOportunidadPortal = idFaseOportunidadPortal });
                return JsonConvert.DeserializeObject<OportunidadDatosChatDTO>(faseOportunidadChat)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene datos de la oportunidad mediante IdFaseOportunidad
		/// </summary>
		/// <param name="idFaseOportunidadPortal"></param>
		/// <returns> OportunidadDatosChatDTO </returns>
		public OportunidadDatosChatDTO ObtenerOportunidadDatosChatPorIdFaseOportunidadPortalAA(string idFaseOportunidadPortal)
        {
            try
            {
                string query = @"
                                SELECT 
                                    IdOportunidad, Nombre1, Nombre2, ApellidoPaterno, NombreCentroCosto, ApellidoMaterno, Direccion, Telefono, Celular, Email, Email2, Error,
                                    IdCodigoPais,IdCiudad,IdCargo,IdAreaFormacion,IdAreaTrabajo,IdIndustria, IdCentroCosto,IdTipoDato,IdFaseOportunidad,IdOrigen,IdEmpresa
                                FROM 
                                    com.V_ObtenerDatosPorIdFaseOportunidad_AA 
                                WHERE 
                                    Estado = 1 and IdFaseOportunidadPortal = @idFaseOportunidadPortal";
                var faseOportunidadChatAA = _dapperRepository.FirstOrDefault(query, new { idFaseOportunidadPortal });
                return JsonConvert.DeserializeObject<OportunidadDatosChatDTO>(faseOportunidadChatAA)!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FaseOportunidadComboDTO> ObtenerComboFiltroSegmento()
        {
            try
            {
                List<FaseOportunidadComboDTO> respuesta = new List<FaseOportunidadComboDTO>();

                var query = "\tSELECT Id,  Codigo as Nombre FROM pla.T_FaseOportunidad WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<FaseOportunidadComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
