using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: FlujoPorPespecificoRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_FlujoPorPespecifico
    /// </summary>
    public class FlujoPorPespecificoRepository : GenericRepository<TFlujoPorPespecifico>, IFlujoPorPespecificoRepository
    {
        private Mapper _mapper;

        public FlujoPorPespecificoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFlujoPorPespecifico, FlujoPorPespecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TFlujoPorPespecifico MapeoEntidad(FlujoPorPespecifico entidad)
        {
            try
            {
                TFlujoPorPespecifico modelo = _mapper.Map<TFlujoPorPespecifico>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoPorPespecifico Add(FlujoPorPespecifico entidad)
        {
            try
            {
                var FlujoPorPespecifico = MapeoEntidad(entidad);
                base.Insert(FlujoPorPespecifico);
                return FlujoPorPespecifico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFlujoPorPespecifico Update(FlujoPorPespecifico entidad)
        {
            try
            {
                var FlujoPorPespecifico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FlujoPorPespecifico.RowVersion = entidadExistente.RowVersion;

                base.Update(FlujoPorPespecifico);
                return FlujoPorPespecifico;
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
        public IEnumerable<TFlujoPorPespecifico> Add(IEnumerable<FlujoPorPespecifico> listadoEntidad)
        {
            try
            {
                List<TFlujoPorPespecifico> listado = new List<TFlujoPorPespecifico>();
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
        public IEnumerable<TFlujoPorPespecifico> Update(IEnumerable<FlujoPorPespecifico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFlujoPorPespecifico> listado = new List<TFlujoPorPespecifico>();
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
        /// Obtiene FlujoPorPespecifico por id.
        /// </summary>
        /// <returns>FlujoPorPespecifico</returns>
        public FlujoPorPespecifico? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPEspecifico AS IdPespecifico,
	                    IdFlujoActividad,
	                    IdFlujoOcurrencia,
	                    IdClasificacionPersona,
	                    FechaEjecucion,
	                    FechaSeguimiento,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_FlujoPorPEspecifico
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<FlujoPorPespecifico>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}



