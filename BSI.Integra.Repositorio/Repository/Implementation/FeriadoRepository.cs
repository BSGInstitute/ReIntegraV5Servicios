using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FeriadoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Feriado
    /// </summary>
    public class FeriadoRepository : GenericRepository<TFeriado>, IFeriadoRepository
    {
        private Mapper _mapper;

        public FeriadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFeriado, Feriado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFeriado MapeoEntidad(Feriado entidad)
        {
            try
            {
                return _mapper.Map<TFeriado>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFeriado Add(Feriado entidad)
        {
            try
            {
                var Feriado = MapeoEntidad(entidad);
                base.Insert(Feriado);
                return Feriado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFeriado Update(Feriado entidad)
        {
            try
            {
                var Feriado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Feriado.RowVersion = entidadExistente.RowVersion;

                base.Update(Feriado);
                return Feriado;
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
        public IEnumerable<TFeriado> Add(IEnumerable<Feriado> listadoEntidad)
        {
            try
            {
                List<TFeriado> listado = new List<TFeriado>();
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
        public IEnumerable<TFeriado> Update(IEnumerable<Feriado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFeriado> listado = new List<TFeriado>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Feriado.
        /// </summary>
        /// <returns> List<FeriadoDTO> </returns>
        public IEnumerable<FeriadoDTO> Obtener()
        {
            try
            {
                var query = @"
                    SELECT Id, Tipo, Dia, Motivo, Frecuencia, IdTroncalCiudad
                    FROM pla.V_TFeriado_Obtener";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<FeriadoDTO>>(resultado)!;
                }
                return new List<FeriadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-O-001@Error en Obtener: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene T_Feriado por id
        /// </summary>
        /// <returns> Feriado </returns>
        public Feriado? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                        SELECT Id, Tipo, Dia, Motivo, Frecuencia, IdTroncalCiudad, Estado, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, RowVersion, IdMigracion
                        FROM pla.V_TFeriado_Obtener
                        WHERE Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<Feriado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Feriado por ids.
        /// </summary>
        /// <returns> Lista Feriado </returns>
        public IEnumerable<Feriado> ObtenerPorIds(IEnumerable<int> ids)
        {
            try
            {
                var query = @"
                        SELECT Id, Tipo, Dia, Motivo, Frecuencia, IdTroncalCiudad, Estado, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, RowVersion, IdMigracion
                        FROM pla.V_TFeriado_Obtener
                        WHERE Id IN @ids";
                var resultado = _dapperRepository.QueryDapper(query, new { ids });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Feriado>>(resultado)!;
                }
                return new List<Feriado>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPIs-001@Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Feriado por tipo.
        /// </summary>
        /// <returns> Lista Feriado </returns>
        public IEnumerable<Feriado> ObtenerPorTipo(int tipo)
        {
            try
            {
                var query = @"
                        SELECT Id, Tipo, Dia, Motivo, Frecuencia, IdTroncalCiudad, Estado, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, RowVersion, IdMigracion
                        FROM pla.V_TFeriado_Obtener
                        WHERE Tipo = @tipo";
                var resultado = _dapperRepository.QueryDapper(query, new { tipo });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Feriado>>(resultado)!;
                }
                return new List<Feriado>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPIs-001@Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: aarroyoh
        /// Fecha: 06/05/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene feriados activos cruzados con TroncalCiudad (vista pla.V_FeriadoConPais)
        /// filtrando por uno o varios IdTroncalPais. Por defecto se usa Perú + el país del PE.
        /// </summary>
        /// <returns>Lista FeriadoConPaisDTO</returns>
        public IEnumerable<FeriadoConPaisDTO> ObtenerPorPaises(IEnumerable<int> idsTroncalPais)
        {
            try
            {
                var lista = string.Join(",", idsTroncalPais ?? Enumerable.Empty<int>());
                var resultado = _dapperRepository.QuerySPDapper(
                    "pla.SP_FeriadoObtenerPorPaises",
                    new { IdTroncalPais_Lista = lista });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    // Mapeo manual: el SP devuelve la columna como IdFeriado (estandar BSG)
                    // pero el DTO mantiene Id para no romper el contrato del API hacia el front.
                    var jArray = JArray.Parse(resultado);
                    return jArray.OfType<JObject>().Select(j => new FeriadoConPaisDTO
                    {
                        Id = j.Value<int>("IdFeriado"),
                        Tipo = j.Value<int?>("Tipo"),
                        Dia = j.Value<DateTime>("Dia"),
                        Motivo = j.Value<string>("Motivo") ?? string.Empty,
                        Frecuencia = j.Value<int>("Frecuencia"),
                        IdTroncalCiudad = j.Value<int>("IdTroncalCiudad"),
                        IdTroncalPais = j.Value<int>("IdTroncalPais")
                    }).ToList();
                }
                return new List<FeriadoConPaisDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPP-001@Error en ObtenerPorPaises: {ex.Message}", ex);
            }
        }
        /// Autor: aarroyoh
        /// Fecha: 06/05/2026
        /// Version: 1.0
        /// <summary>
        /// Combo de TroncalCiudad activos (Id, Nombre, IdTroncalPais) para alimentar dropdowns del CRUD de feriados.
        /// El IdTroncalPais se incluye para que el front pueda filtrar ciudades por país sin un round-trip extra.
        /// </summary>
        public IEnumerable<ComboTroncalCiudadDTO> ObtenerComboTroncalCiudad()
        {
            try
            {
                var query = @"
                        SELECT Id, Nombre, IdTroncalPais
                        FROM pla.V_TTroncalCiudad_Obtener";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboTroncalCiudadDTO>>(resultado)!;
                }
                return new List<ComboTroncalCiudadDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OCC-001@Error en ObtenerComboTroncalCiudad: {ex.Message}", ex);
            }
        }
        /// Autor: aarroyoh
        /// Fecha: 06/05/2026
        /// Version: 1.0
        /// <summary>
        /// Combo de paises activos (Id, NombrePais aliasado a Nombre) desde conf.T_Pais.
        /// La columna IdTroncalPais en T_TroncalCiudad apunta a esta tabla, no a una T_TroncalPais.
        /// </summary>
        public IEnumerable<ComboTroncalPaisDTO> ObtenerComboTroncalPais()
        {
            try
            {
                var query = @"
                        SELECT Id, Nombre
                        FROM pla.V_TTroncalPais_Obtener";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboTroncalPaisDTO>>(resultado)!;
                }
                return new List<ComboTroncalPaisDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OCP-001@Error en ObtenerComboTroncalPais: {ex.Message}", ex);
            }
        }
    }
}
