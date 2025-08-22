using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FlujoFaseRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_FlujoFase
    /// </summary>
    public class FlujoFaseRepository : GenericRepository<TFlujoFase>, IFlujoFaseRepository
    {
        private Mapper _mapper;

        public FlujoFaseRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFlujoFase, FlujoFase>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFlujoFase MapeoEntidad(FlujoFase entidad)
        {
            try
            {
                TFlujoFase modelo = _mapper.Map<TFlujoFase>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoFase Add(FlujoFase entidad)
        {
            try
            {
                var FlujoFase = MapeoEntidad(entidad);
                base.Insert(FlujoFase);
                return FlujoFase;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoFase Update(FlujoFase entidad)
        {
            try
            {
                var FlujoFase = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FlujoFase.RowVersion = entidadExistente.RowVersion;

                base.Update(FlujoFase);
                return FlujoFase;
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
        public IEnumerable<TFlujoFase> Add(IEnumerable<FlujoFase> listadoEntidad)
        {
            try
            {
                List<TFlujoFase> listado = new List<TFlujoFase>();
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
        public IEnumerable<TFlujoFase> Update(IEnumerable<FlujoFase> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFlujoFase> listado = new List<TFlujoFase>();
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
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene FlujoFase por id.
        /// </summary>
        /// <returns>FlujoFase</returns>
        public FlujoFase? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdFlujo,
	                    Orden,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_FlujoFase
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FlujoFase>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Christian Qm
        /// Fecha: 5/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene FlujoFase por idFlujo.
        /// </summary>
        /// <returns>FlujoFaseDTO</returns>
        public IEnumerable<FlujoFaseDTO>? ObtenerPorIdFlujo(int idFlujo)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdFlujo,
	                    Orden,
	                    Nombre
                    FROM ope.T_FlujoFase
                    WHERE Estado = 1 AND IdFlujo = @idFlujo";
                var resultado = _dapperRepository.QueryDapper(query, new { idFlujo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<FlujoFaseDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorIdFlujo(), {ex.Message}");
            }
        }
    }
}



