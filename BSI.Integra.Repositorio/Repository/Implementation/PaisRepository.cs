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
    /// Repositorio: PaisRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_Pais
    /// </summary>
    public class PaisRepository : GenericRepository<TPai>, IPaisRepository
    {
        private Mapper _mapper;

        public PaisRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPai, Pais>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPai MapeoEntidad(Pais entidad)
        {
            try
            {
                //crea la entidad padre
                TPai modelo = _mapper.Map<TPai>(entidad);

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

        public TPai Add(Pais entidad)
        {
            try
            {
                var Pais = MapeoEntidad(entidad);
                base.Insert(Pais);
                return Pais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPai Update(Pais entidad)
        {
            try
            {
                var Pais = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Pais.RowVersion = entidadExistente.RowVersion;

                base.Update(Pais);
                return Pais;
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


        public IEnumerable<TPai> Add(IEnumerable<Pais> listadoEntidad)
        {
            try
            {
                List<TPai> listado = new List<TPai>();
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

        public IEnumerable<TPai> Update(IEnumerable<Pais> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPai> listado = new List<TPai>();
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
        /// Obtiene todos los registros de T_Pais.
        /// </summary>
        /// <returns> List<PaisDTO> </returns>
        public IEnumerable<PaisDTO> ObtenerPais()
        {
            try
            {
                List<PaisDTO> rpta = new List<PaisDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    CodigoPais,
	                    CodigoISO,
	                    NombrePais,
	                    Moneda,
	                    ZonaHoraria,
	                    EstadoPublicacion,
	                    CodigoGoogleId,
	                    CodigoPaisMoodle,
	                    RutaBandera,
	                    RutaIcono,
	                    EstadoVisualizacion
                    FROM conf.T_Pais
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaisDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Pais.
        /// </summary>
        /// <returns> List<PaisDTO> </returns>
        public async Task<IEnumerable<PaisDTO>> ObtenerPaisAsync()
        {
            try
            {
                List<PaisDTO> rpta = new List<PaisDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    CodigoPais,
	                    CodigoISO,
	                    NombrePais,
	                    Moneda,
	                    ZonaHoraria,
	                    EstadoPublicacion,
	                    CodigoGoogleId,
	                    CodigoPaisMoodle,
	                    RutaBandera,
	                    RutaIcono,
	                    EstadoVisualizacion
                    FROM conf.T_Pais
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaisDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Pais por id
        /// </summary>
        /// <param name="id">Id del Pais</param>
        /// <returns> Pais </returns>
        public Pais? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    CodigoPais,
	                    CodigoISO,
	                    NombrePais,
	                    Moneda,
	                    ZonaHoraria,
	                    EstadoPublicacion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    CodigoGoogleId,
	                    CodigoPaisMoodle,
	                    RutaBandera,
	                    RutaIcono,
	                    EstadoVisualizacion
                    FROM conf.T_Pais
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Pais>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Pais para mostrarse en combo.
        /// </summary>
        /// <returns> List<PaisComboDTO> </returns>
        public IEnumerable<PaisComboDTO> ObtenerPaisCombo()
        {
            try
            {
                List<PaisComboDTO> rpta = new List<PaisComboDTO>();
                var query = @"SELECT Id, CodigoPais, NombrePais FROM conf.T_Pais WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaisComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos basicos de los paises junto con su Zona Horaria.
        /// </summary>
        /// <returns> List<PaisZonaHorariaDTO> </returns>
        public IEnumerable<PaisZonaHorariaDTO> ObtenerPaisZonaHoraria()
        {
            try
            {
                List<PaisZonaHorariaDTO> paises = new List<PaisZonaHorariaDTO>();
                var query = @"SELECT Id, CodigoPais, NombrePais, ZonaHoraria FROM conf.T_Pais WHERE Estado = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    paises = JsonConvert.DeserializeObject<List<PaisZonaHorariaDTO>>(resultadoQuery)!;
                }
                return paises;
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
        /// Obtiene el Nombre de un Pais asociado a un Id.
        /// </summary>
        /// <param name="idPais">Id del Pais</param>
        /// <returns> ValorStringDTO </returns>
        public StringDTO ObtenerNombrePaisPorId(int idPais)
        {
            try
            {
                StringDTO nombre = new StringDTO();
                var query = @"SELECT NombrePais AS Valor FROM conf.T_Pais WHERE CodigoPais = @idPais AND Estado = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idPais });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    nombre = JsonConvert.DeserializeObject<StringDTO>(resultadoQuery)!;
                }
                return nombre;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<PaisMonedaComboDTO> ObtenerComboConMoneda()
        {
            try
            {
                List<PaisMonedaComboDTO> items = new List<PaisMonedaComboDTO>();
                string queryText = string.Empty;
                queryText = "SELECT Id, Nombre, Moneda FROM pla.V_TPais_Filtro WHERE Estado=1";
                var query = _dapperRepository.QueryDapper(queryText, null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<PaisMonedaComboDTO>>(query)!;
                }
                return items;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        public async Task<IEnumerable<PaisMonedaComboDTO>> ObtenerComboConMonedaAsync()
        {
            try
            {
                string queryText = string.Empty;
                queryText = "SELECT Id,Nombre,Moneda FROM pla.V_TPais_Filtro WHERE Estado=1";
                var query = await _dapperRepository.QueryDapperAsync(queryText, null);
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PaisMonedaComboDTO>>(query);
                }
                return null;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }
        /// Autor:Margiory Ramirez Neyra.
        /// Fecha: 03/11/2022
        /// <summary>
        /// Obtiene todos los paises con codigo pais diferente de 0 y estado = 1
        /// </summary>
        /// <returns>Lista de objetos de  Pais  Todo Filtro</returns>
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboConZonaHoraria()
        {
            try
            {
                List<PaisZonaHorariaComboDTO> rpta = new List<PaisZonaHorariaComboDTO>();
                var query = @"SELECT Codigo, Nombre, ZonaHoraria FROM com.V_TPais_ObtenerPaisComboBox WHERE Estado = 1";
                var paisesDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(paisesDB) && !paisesDB.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PaisZonaHorariaComboDTO>>(paisesDB)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor:Junior Llerena
        /// Fecha: 16/07/2025
        /// <summary>
        /// Obtiene todos los paises con estad 1 y estado de visualizacion 1
        /// </summary>
        /// <returns>Lista de objetos de tipo <PaisZonaHorariaComboDTO></returns>
        public IEnumerable<PaisZonaHorariaComboDTO> ObtenerComboZonaHorarioActivo()
        {
            try
            {
                List<PaisZonaHorariaComboDTO> response = new List<PaisZonaHorariaComboDTO>();
                var query = @"SELECT CodigoPais AS Codigo, NombrePais AS Nombre, ZonaHoraria AS ZonaHoraria FROM conf.T_Pais WHERE EstadoVisualizacion = 1 AND
Estado = 1";
                var paisesDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(paisesDB) && !paisesDB.Contains("[]"))
                {
                    response = JsonConvert.DeserializeObject<List<PaisZonaHorariaComboDTO>>(paisesDB)!;
                }
                return response;
            }
            catch (Exception ex)
            {   
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT CodigoPais AS Id, NombrePais AS Nombre FROM conf.T_Pais WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public async Task<List<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "SELECT CodigoPais AS Id, NombrePais AS Nombre FROM conf.T_Pais WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public IEnumerable<ComboDTO> ObtenerListaPais()
        {
            try
            {
                var query = @"SELECT CodigoPais AS Id,  NombrePais AS Nombre FROM conf.T_Pais WHERE Estado = 1 AND Id > 0;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/11/2022
        /// <summary>
        /// Obtiene lista de paises para lista desplagable
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerListaPaisAsync()
        {
            try
            {
                // Se agrega el pais internacional
                // Se quita la condicion "AND Id > 0"
                var query = @"SELECT CodigoPais AS Id, NombrePais AS Nombre FROM conf.T_Pais WHERE Estado = 1;";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los codigo pais con estado 1
        /// </summary>
        /// <returns>Lista de objetos de clase PaisCodigoDTO</returns>
        public List<int> ObtenerTodoCodigoPais()
        {
            try
            {
                var query = "SELECT Codigo AS Valor FROM com.V_TPais_ObtenerPaisComboBox WHERE Estado = 1 GROUP BY Codigo";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var res = JsonConvert.DeserializeObject<List<IntDTO>>(resultado)!;
                    return res.Select(x => x.Valor!.Value).ToList();
                }
                return new List<int>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// <summary>
        /// Obtiene paises asociados al IdPlantillaPais
        /// </summary>
        /// <param name="idPlantilla"></param>
        /// <returns> IEnumerable<PlantillaPaisFiltroDTO> </returns>
        public IEnumerable<PlantillaPaisFiltroDTO> ObtenerPaisesPorIdPlantillaPw(int idPlantillaPw)
        {
            try
            {
                var _query = string.Empty;
                _query = @"SELECT Id,NombrePais,CodigoISO FROM pla.V_ObtenerPaisesPorIdPlantilla WHERE Estado = 1 and IdPlantilla = @idPlantilla";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPlantilla = idPlantillaPw });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaPaisFiltroDTO>>(respuestaDapper);

                return new List<PlantillaPaisFiltroDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
