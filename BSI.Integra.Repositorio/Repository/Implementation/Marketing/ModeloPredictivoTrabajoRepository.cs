using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing
{
    /// Repositorio: ModeloPredictivoTrabajoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ModeloPredictivoTrabajo
    /// </summary>
    public class ModeloPredictivoTrabajoRepository : GenericRepository<TModeloPredictivoTrabajo>, IModeloPredictivoTrabajoRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoTrabajo, ModeloPredictivoTrabajo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloPredictivoTrabajo MapeoEntidad(ModeloPredictivoTrabajo entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoTrabajo perfilAtrabajoCoeficiente = _mapper.Map<TModeloPredictivoTrabajo>(entidad);

                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoTrabajo Add(ModeloPredictivoTrabajo entidad)
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

        public TModeloPredictivoTrabajo Update(ModeloPredictivoTrabajo entidad)
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

        public IEnumerable<TModeloPredictivoTrabajo> Add(IEnumerable<ModeloPredictivoTrabajo> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoTrabajo> listado = new List<TModeloPredictivoTrabajo>();
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

        public IEnumerable<TModeloPredictivoTrabajo> Update(IEnumerable<ModeloPredictivoTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoTrabajo> listado = new List<TModeloPredictivoTrabajo>();
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
        /// Obtiene toda información de T_ProgramaGeneralPerfilTipoDato por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ModeloPredictivoTrabajo </returns>
        public ModeloPredictivoTrabajo? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral,
                        Nombre,
                        valor,
                        validar,
                        IdAreaTrabajo,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoTrabajo
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModeloPredictivoTrabajo>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPTR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ModeloPredictivoTrabajo>() </returns>
        public IEnumerable<ModeloPredictivoTrabajo> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
                        IdPGeneral,
                        Nombre,
                        valor,
                        validar,
                        IdAreaTrabajo,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        mkt.T_ModeloPredictivoTrabajo
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModeloPredictivoTrabajo>>(resultado)!;
                }
                return new List<ModeloPredictivoTrabajo>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPTR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<ModeloPredictivoTrabajoDTO> ObtenerTrabajoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoTrabajoDTO> rpta = new List<ModeloPredictivoTrabajoDTO>();
                var query = "SELECT Id,IdPGeneral,IdAreaTrabajo,Nombre,Valor,Validar FROM mkt.V_TModeloPredictivoTrabajo WHERE Estado = 1 and IdPGeneral = @IdPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ModeloPredictivoTrabajoDTO>>(resultado)!;
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
