using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FlujoActividadRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_FlujoActividad
    /// </summary>
    public class FlujoActividadRepository : GenericRepository<TFlujoActividad>, IFlujoActividadRepository
    {
        private Mapper _mapper;

        public FlujoActividadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFlujoActividad, FlujoActividad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFlujoActividad MapeoEntidad(FlujoActividad entidad)
        {
            try
            {
                TFlujoActividad modelo = _mapper.Map<TFlujoActividad>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoActividad Add(FlujoActividad entidad)
        {
            try
            {
                var FlujoActividad = MapeoEntidad(entidad);
                base.Insert(FlujoActividad);
                return FlujoActividad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoActividad Update(FlujoActividad entidad)
        {
            try
            {
                var FlujoActividad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FlujoActividad.RowVersion = entidadExistente.RowVersion;

                base.Update(FlujoActividad);
                return FlujoActividad;
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
        public IEnumerable<TFlujoActividad> Add(IEnumerable<FlujoActividad> listadoEntidad)
        {
            try
            {
                List<TFlujoActividad> listado = new List<TFlujoActividad>();
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
        public IEnumerable<TFlujoActividad> Update(IEnumerable<FlujoActividad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFlujoActividad> listado = new List<TFlujoActividad>();
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
        /// Obtiene FlujoActividad por id.
        /// </summary>
        /// <returns>FlujoActividad</returns>
        public FlujoActividad? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdFlujoFase,
	                    Orden,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_FlujoActividad
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FlujoActividad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene FlujoActividad por id.
        /// </summary>
        /// <returns>FlujoActividad</returns>
        public IEnumerable<FlujoActividadDTO>? ObtenerPorIdFlujoFase(int idFlujoFase)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
                        IdFlujoFase,
                        Orden,
                        Nombre
                    FROM ope.T_FlujoActividad
                    WHERE Estado = 1 AND IdFlujoFase = @idFlujoFase";
                var resultado = _dapperRepository.QueryDapper(query, new { idFlujoFase });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<FlujoActividadDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorIdFlujoFase(), {ex.Message}");
            }
        }
    }
}



