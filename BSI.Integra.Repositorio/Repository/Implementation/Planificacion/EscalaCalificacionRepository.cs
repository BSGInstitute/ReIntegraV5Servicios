using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: EscalaCalificacionRepository
    /// Autor:Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión general de T_EscalaCalificacion
    /// </summary>
    public class EscalaCalificacionRepository : GenericRepository<TEscalaCalificacion>, IEscalaCalificacionRepository
    {
        private Mapper _mapper;

        public EscalaCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEscalaCalificacion, EscalaCalificacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TEscalaCalificacionDetalle, EscalaCalificacionDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEscalaCalificacion MapeoEntidad(EscalaCalificacion entidad)
        {

            try
            {
                TEscalaCalificacion modelo = _mapper.Map<TEscalaCalificacion>(entidad);
                if (entidad.EscalaCalificacionDetalles != null && entidad.EscalaCalificacionDetalles.Count >= 1)
                    modelo.TEscalaCalificacionDetalles = _mapper.Map<List<TEscalaCalificacionDetalle>>(entidad.EscalaCalificacionDetalles);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEscalaCalificacion Add(EscalaCalificacion entidad)
        {
            try
            {
                var EscalaCalificacion = MapeoEntidad(entidad);
                base.Insert(EscalaCalificacion);
                return EscalaCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEscalaCalificacion Update(EscalaCalificacion entidad)
        {
            try
            {
                var EscalaCalificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EscalaCalificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(EscalaCalificacion);
                return EscalaCalificacion;
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


        public IEnumerable<TEscalaCalificacion> Add(IEnumerable<EscalaCalificacion> listadoEntidad)
        {
            try
            {
                List<TEscalaCalificacion> listado = new List<TEscalaCalificacion>();
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

        public IEnumerable<TEscalaCalificacion> Update(IEnumerable<EscalaCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEscalaCalificacion> listado = new List<TEscalaCalificacion>();
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
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EscalaCalificacion.
        /// </summary>
        /// <returns> List<EscalaCalificacionDTO> </returns>
        public IEnumerable<EscalaCalificacionDTO> ObtenerTodo()
        {
            try
            {
                List<EscalaCalificacionDTO> rpta = new();
                var query = @"
                        SELECT 
                            Id,
		                    Nombre
                        FROM pla.T_EscalaCalificacion
                        WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EscalaCalificacionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EscalaCalificacion.
        /// </summary>
        /// <returns> List<EscalaCalificacionDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"
                        SELECT 
                            Id,
		                    Nombre
                        FROM pla.T_EscalaCalificacion
                        WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EscalaCalificacion.
        /// </summary>
        /// <returns> List<EscalaCalificacionDTO> </returns>
        public EscalaCalificacion ObtenerPorId(int id)
        {
            try
            {
                EscalaCalificacion rpta = new();
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
                        FROM pla.T_EscalaCalificacion
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<EscalaCalificacion>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EscalaCalificacion.
        /// </summary>
        /// <returns> List<EscalaCalificacionDTO> </returns>
        public List<EscalaCalificacion> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<EscalaCalificacion> rpta = new();
                var query = @"
                        SELECT 
                            Id,
		                    Nombre,
		                    Descripcion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_EscalaCalificacion
                        WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EscalaCalificacion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



