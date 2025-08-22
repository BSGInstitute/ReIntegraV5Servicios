using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: ModeloPredictivoCargoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ModeloPredictivoCargo
    /// </summary>
    public class ModeloPredictivoCargoRepository : GenericRepository<TModeloPredictivoCargo>, IModeloPredictivoCargoRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoCargoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoCargo, ModeloPredictivoCargo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloPredictivoCargo MapeoEntidad(ModeloPredictivoCargo entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoCargo perfilAtrabajoCoeficiente = _mapper.Map<TModeloPredictivoCargo>(entidad);

                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoCargo Add(ModeloPredictivoCargo entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                Insert(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoCargo Update(ModeloPredictivoCargo entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilAtrabajoCoeficiente.RowVersion = entidadExistente.RowVersion;

                Update(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
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

        public IEnumerable<TModeloPredictivoCargo> Add(IEnumerable<ModeloPredictivoCargo> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoCargo> listado = new List<TModeloPredictivoCargo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TModeloPredictivoCargo> Update(IEnumerable<ModeloPredictivoCargo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoCargo> listado = new List<TModeloPredictivoCargo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_ModeloPredictivoCargo por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ModeloPredictivoCargo </returns>
        public ModeloPredictivoCargo? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral,
                        Nombre,
                        Valor,
                        Validar,
                        IdCargo,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoCargo
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModeloPredictivoCargo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPCR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ModeloPredictivoCargo>() </returns>
        public IEnumerable<ModeloPredictivoCargo> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral,
                        Nombre,
                        Valor,
                        Validar,
                        IdCargo,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoCargo
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModeloPredictivoCargo>>(resultado)!;
                }
                return new List<ModeloPredictivoCargo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPCR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<ModeloPredictivoCargoDTO> ObtenerCargoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoCargoDTO> rpta = new List<ModeloPredictivoCargoDTO>();
                var query = @"SELECT Id,IdPGeneral,IdCargo,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoCargo WHERE Estado = 1 AND IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ModeloPredictivoCargoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
