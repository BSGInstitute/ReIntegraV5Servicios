using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: ModeloPredictivoTipoDatoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ModeloPredictivoTipoDato
    /// </summary>
    public class ModeloPredictivoTipoDatoRepository : GenericRepository<TModeloPredictivoTipoDato>, IModeloPredictivoTipoDatoRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoTipoDatoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoTipoDato, ModeloPredictivoTipoDato>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloPredictivoTipoDato MapeoEntidad(ModeloPredictivoTipoDato entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoTipoDato perfilAtrabajoCoeficiente = _mapper.Map<TModeloPredictivoTipoDato>(entidad);

                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoTipoDato Add(ModeloPredictivoTipoDato entidad)
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

        public TModeloPredictivoTipoDato Update(ModeloPredictivoTipoDato entidad)
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

        public IEnumerable<TModeloPredictivoTipoDato> Add(IEnumerable<ModeloPredictivoTipoDato> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoTipoDato> listado = new List<TModeloPredictivoTipoDato>();
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

        public IEnumerable<TModeloPredictivoTipoDato> Update(IEnumerable<ModeloPredictivoTipoDato> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoTipoDato> listado = new List<TModeloPredictivoTipoDato>();
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
        /// Obtiene toda información de T_ModeloPredictivoTipoDato por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ModeloPredictivoTipoDato </returns>
        public ModeloPredictivoTipoDato? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral,
                        IdTipoDato,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoTipoDato
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModeloPredictivoTipoDato>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPTDR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ModeloPredictivoTipoDato>() </returns>
        public IEnumerable<ModeloPredictivoTipoDato> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral,
                        IdTipoDato,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoTipoDato
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModeloPredictivoTipoDato>>(resultado)!;
                }
                return new List<ModeloPredictivoTipoDato>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPTDR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<ModeloPredictivoTipoDatoDTO> ObtenerTipoDatoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoTipoDatoDTO> rpta = new List<ModeloPredictivoTipoDatoDTO>();
                var query = "SELECT Id,IdPGeneral,IdTipoDato FROM mkt.V_TModeloPredictivoTipoDato WHERE Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ModeloPredictivoTipoDatoDTO>>(respuestaDapper)!;
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
