using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class EstadoEtapaProcesoSeleccionRepository : GenericRepository<TEstadoEtapaProcesoSeleccion>, IEstadoEtapaProcesoSeleccionRepository
    {
        private Mapper _mapper;
        public EstadoEtapaProcesoSeleccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<EstadoEtapaProcesoSeleccion, EstadoEtapaProcesoSeleccionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<EstadoEtapaProcesoSeleccion, TEstadoEtapaProcesoSeleccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEstadoEtapaProcesoSeleccion MapeoEntidad(EstadoEtapaProcesoSeleccion entidad)
        {
            try
            {
                TEstadoEtapaProcesoSeleccion modelo = _mapper.Map<TEstadoEtapaProcesoSeleccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEstadoEtapaProcesoSeleccion Add(EstadoEtapaProcesoSeleccion entidad)
        {
            try
            {
                var EstadoEtapaProcesoSeleccion = MapeoEntidad(entidad);
                base.Insert(EstadoEtapaProcesoSeleccion);
                return EstadoEtapaProcesoSeleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEstadoEtapaProcesoSeleccion Update(EstadoEtapaProcesoSeleccion entidad)
        {
            try
            {
                var EstadoEtapaProcesoSeleccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoEtapaProcesoSeleccion.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoEtapaProcesoSeleccion);
                return EstadoEtapaProcesoSeleccion;
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
        public IEnumerable<TEstadoEtapaProcesoSeleccion> Add(IEnumerable<EstadoEtapaProcesoSeleccion> listadoEntidad)
        {
            try
            {
                List<TEstadoEtapaProcesoSeleccion> listado = new List<TEstadoEtapaProcesoSeleccion>();
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
        public IEnumerable<TEstadoEtapaProcesoSeleccion> Update(IEnumerable<EstadoEtapaProcesoSeleccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoEtapaProcesoSeleccion> listado = new List<TEstadoEtapaProcesoSeleccion>();
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
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<EstadoEtapaProcesoSeleccionDTO> Obtener()
        {
            try
            {
                List<EstadoEtapaProcesoSeleccionDTO> rpta = new List<EstadoEtapaProcesoSeleccionDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_EstadoEtapaProcesoSeleccion
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoEtapaProcesoSeleccionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria para combo.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM gp.T_EstadoEtapaProcesoSeleccion
                    WHERE Estado = 1 ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria para combo.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM gp.T_EstadoEtapaProcesoSeleccion
                    WHERE Estado = 1 ORDER BY Nombre";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Flavio R. Mamani Fabian
        /// Fecha: 30/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns>EstadoEtapaProcesoSeleccion || null</returns>
        public EstadoEtapaProcesoSeleccion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_EstadoEtapaProcesoSeleccion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<EstadoEtapaProcesoSeleccion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EPS-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
