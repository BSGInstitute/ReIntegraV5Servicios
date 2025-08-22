using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ModeloPredictivoCategoriaDatoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_ModeloPredictivoCategoriaDato
    /// </summary>
    public class ModeloPredictivoCategoriaDatoRepository : GenericRepository<TModeloPredictivoCategoriaDato>, IModeloPredictivoCategoriaDatoRepository
    {
        private Mapper _mapper;

        public ModeloPredictivoCategoriaDatoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModeloPredictivoCategoriaDato, ModeloPredictivoCategoriaDato>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TModeloPredictivoCategoriaDato MapeoEntidad(ModeloPredictivoCategoriaDato entidad)
        {
            try
            {
                //crea la entidad padre
                TModeloPredictivoCategoriaDato perfilAtrabajoCoeficiente = _mapper.Map<TModeloPredictivoCategoriaDato>(entidad);

                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoCategoriaDato Add(ModeloPredictivoCategoriaDato entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModeloPredictivoCategoriaDato Update(ModeloPredictivoCategoriaDato entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilAtrabajoCoeficiente.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilAtrabajoCoeficiente);
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

        public IEnumerable<TModeloPredictivoCategoriaDato> Add(IEnumerable<ModeloPredictivoCategoriaDato> listadoEntidad)
        {
            try
            {
                List<TModeloPredictivoCategoriaDato> listado = new List<TModeloPredictivoCategoriaDato>();
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

        public IEnumerable<TModeloPredictivoCategoriaDato> Update(IEnumerable<ModeloPredictivoCategoriaDato> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModeloPredictivoCategoriaDato> listado = new List<TModeloPredictivoCategoriaDato>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda información de T_ModeloPredictivoCategoriaDato por medio del Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Entidad - ModeloPredictivoCategoriaDato </returns>
        public ModeloPredictivoCategoriaDato? ObtenerPorId(int id)
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
                        IdCategoriaOrigen,
                        IdSubCategoriaDato,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ModeloPredictivoCategoriaDato
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ModeloPredictivoCategoriaDato>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPCDR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidad mediante el idPGeneral
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista Entidad - List<ModeloPredictivoCategoriaDato>() </returns>
        public IEnumerable<ModeloPredictivoCategoriaDato> ObtenerPorIdPGeneral(int idPGeneral)
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
                        IdCategoriaOrigen,
                        IdSubCategoriaDato,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion 
                    FROM 
                        pla.T_ModeloPredictivoCategoriaDato
                    WHERE
                        Estado = 1 AND IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ModeloPredictivoCategoriaDato>>(resultado)!;
                }
                return new List<ModeloPredictivoCategoriaDato>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#MPCDR-OPIPG-002@Error en ObtenerPorIdPGeneral() {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 25/07/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<ModeloPredictivoCategoriaDatoDTO> ObtenerCategoriaDatoPorPrograma(int idPGeneral)
        {
            try
            {
                List<ModeloPredictivoCategoriaDatoDTO> resultadoDTO = new List<ModeloPredictivoCategoriaDatoDTO>();
                var _query = string.Empty;
                _query = @"SELECT Id,IdPGeneral,IdCategoriaOrigen,Nombre,Valor,Validar,IdSubCategoriaDato FROM pla.V_T_ModeloPredictivoCategoriaDato WHERE Estado = 1 and IdPGeneral = @IdPGeneral";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    resultadoDTO = JsonConvert.DeserializeObject<List<ModeloPredictivoCategoriaDatoDTO>>(respuestaDapper)!;
                }
                return resultadoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
