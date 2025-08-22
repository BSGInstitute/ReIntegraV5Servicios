using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: ModeloPredictivoIndustriaRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ModeloPredictivoIndustria
    /// </summary>
    public class ModeloPredictivoIndustriaRepository : GenericRepository<TModeloPredictivoIndustrium>, IModeloPredictivoIndustriaRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoIndustriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoIndustrium, ModeloPredictivoIndustria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloPredictivoIndustrium MapeoEntidad(ModeloPredictivoIndustria entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoIndustrium modeloPredictivoIndustria = _mapper.Map<TModeloPredictivoIndustrium>(entidad);

                return modeloPredictivoIndustria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoIndustrium Add(ModeloPredictivoIndustria entidad)
        {
            try
            {
                var modeloPredictivoIndustria = MapeoEntidad(entidad);
                Insert(modeloPredictivoIndustria);
                return modeloPredictivoIndustria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoIndustrium Update(ModeloPredictivoIndustria entidad)
        {
            try
            {
                var modeloPredictivoIndustria = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                modeloPredictivoIndustria.RowVersion = entidadExistente.RowVersion;

                Update(modeloPredictivoIndustria);
                return modeloPredictivoIndustria;
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

        public IEnumerable<TModeloPredictivoIndustrium> Add(IEnumerable<ModeloPredictivoIndustria> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoIndustrium> listado = new List<TModeloPredictivoIndustrium>();
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

        public IEnumerable<TModeloPredictivoIndustrium> Update(IEnumerable<ModeloPredictivoIndustria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoIndustrium> listado = new List<TModeloPredictivoIndustrium>();
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
        /// Obtiene toda información de T_ModeloPredictivoIndustria por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ModeloPredictivoIndustria </returns>
        public ModeloPredictivoIndustria? ObtenerPorId(int id)
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
                        IdIndustria,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoIndustria
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModeloPredictivoIndustria>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPIR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ModeloPredictivoIndustria>() </returns>
        public IEnumerable<ModeloPredictivoIndustria> ObtenerPorIdPGeneral(int idPGeneral)
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
                        IdIndustria,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoIndustria
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModeloPredictivoIndustria>>(resultado)!;
                }
                return new List<ModeloPredictivoIndustria>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPIR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<ModeloPredictivoIndustriaDTO> ObtenerIndustriaPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoIndustriaDTO> rpta = new List<ModeloPredictivoIndustriaDTO>();
                var query = @"SELECT Id,IdPGeneral,IdIndustria,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoIndustria WHERE Estado = 1 AND IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ModeloPredictivoIndustriaDTO>>(resultado)!;
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
