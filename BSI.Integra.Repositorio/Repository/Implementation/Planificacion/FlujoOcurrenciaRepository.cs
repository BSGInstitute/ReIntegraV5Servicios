using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FlujoOcurrenciaRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_FlujoOcurrencia
    /// </summary>
    public class FlujoOcurrenciaRepository : GenericRepository<TFlujoOcurrencium>, IFlujoOcurrenciaRepository
    {
        private Mapper _mapper;

        public FlujoOcurrenciaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFlujoOcurrencium, FlujoOcurrencia>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFlujoOcurrencium MapeoEntidad(FlujoOcurrencia entidad)
        {
            try
            {
                TFlujoOcurrencium modelo = _mapper.Map<TFlujoOcurrencium>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoOcurrencium Add(FlujoOcurrencia entidad)
        {
            try
            {
                var FlujoOcurrencia = MapeoEntidad(entidad);
                base.Insert(FlujoOcurrencia);
                return FlujoOcurrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoOcurrencium Update(FlujoOcurrencia entidad)
        {
            try
            {
                var FlujoOcurrencia = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FlujoOcurrencia.RowVersion = entidadExistente.RowVersion;

                base.Update(FlujoOcurrencia);
                return FlujoOcurrencia;
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
        public IEnumerable<TFlujoOcurrencium> Add(IEnumerable<FlujoOcurrencia> listadoEntidad)
        {
            try
            {
                List<TFlujoOcurrencium> listado = new List<TFlujoOcurrencium>();
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
        public IEnumerable<TFlujoOcurrencium> Update(IEnumerable<FlujoOcurrencia> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFlujoOcurrencium> listado = new List<TFlujoOcurrencium>();
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
        /// Obtiene FlujoOcurrencia por id.
        /// </summary>
        /// <returns>FlujoOcurrencia</returns>
        public FlujoOcurrencia? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdFlujoActividad,
	                    Orden,
	                    Nombre,
	                    CerrarSeguimiento,
	                    IdFase_Destino AS IdFaseDestino,
	                    IdFlujoActividad_Siguiente AS IdFlujoActividadSiguiente,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_FlujoOcurrencia
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FlujoOcurrencia>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FOcR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene FlujoOcurrencia por id.
        /// </summary>
        /// <returns>FlujoOcurrencia</returns>
        public IEnumerable<FlujoOcurrenciaDetalleDTO>? ObtenerPorIdFlujoActividad(int idFlujoActividad)
        {
            try
            {
                var query = @"
                    SELECT 
	                    T_FlujoOcurrencia.Id, T_FlujoOcurrencia.IdFlujoActividad, T_FlujoOcurrencia.Orden, T_FlujoOcurrencia.Nombre, 
	                    T_FlujoOcurrencia.CerrarSeguimiento, T_FlujoOcurrencia.IdFase_Destino, T_FlujoOcurrencia.IdFlujoActividad_Siguiente,
	                    T_FlujoFase.Nombre AS FaseDestino, T_FlujoActividad.Nombre AS ActividadSiguiente
                    FROM ope.T_FlujoOcurrencia
                    LEFT JOIN ope.T_FlujoFase ON T_FlujoFase.Id = T_FlujoOcurrencia.IdFase_Destino
                    LEFT JOIN ope.T_FlujoActividad ON T_FlujoActividad.Id = T_FlujoOcurrencia.IdFlujoActividad_Siguiente
                    WHERE T_FlujoOcurrencia.IdFlujoActividad = @idFlujoActividad";
                var resultado = _dapperRepository.QueryDapper(query, new { idFlujoActividad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<FlujoOcurrenciaDetalleDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FOcR-OPI-001@Error en ObtenerPorIdFlujoActividad(), {ex.Message}");
            }
        }
    }
}



