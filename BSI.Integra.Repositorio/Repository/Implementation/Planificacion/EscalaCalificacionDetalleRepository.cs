using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: EscalaCalificacionDetalleRepository
    /// Autor:Gretel Canasa
    /// Fecha: 11/05/2023
    /// <summary>
    /// Gestión general de T_EscalaCalificacionDetalle
    /// </summary>
    public class EscalaCalificacionDetalleRepository : GenericRepository<TEscalaCalificacionDetalle>, IEscalaCalificacionDetalleRepository
    {
        private Mapper _mapper;

        public EscalaCalificacionDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEscalaCalificacionDetalle, EscalaCalificacionDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<EscalaCalificacionDetalle, TEscalaCalificacionDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
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
        /// Autor:Gretel Canasa
        /// Fecha: 11/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EscalaCalificacionDetalle.
        /// </summary>
        /// <returns> List<EscalaCalificacionDetalleDTO> </returns>
        public IEnumerable<EscalaCalificacionDetalle> ObtenerTodo()
        {
            try
            {
                List<EscalaCalificacionDetalle> rpta = new();
                var query = @"
                        SELECT 
                            Id,
                            IdEscalaCalificacion,
		                    Nombre,
                            Valor,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_EscalaCalificacionDetalle
                        WHERE Estado = 1 ORDER BY FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EscalaCalificacionDetalle>>(resultado);
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
        /// Obtiene todos los registros de T_EscalaCalificacionDetalle.
        /// </summary>
        /// <returns> List<EscalaCalificacionDetalleDTO> </returns>
        public EscalaCalificacionDetalle ObtenerPorId(int id)
        {
            try
            {
                EscalaCalificacionDetalle rpta = new();
                var query = @"
                        SELECT 
                            Id,
                            IdEscalaCalificacion,
		                    Nombre,
                            Valor,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_EscalaCalificacionDetalle
                        WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<EscalaCalificacionDetalle>(resultado);
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
        /// Obtiene todos los registros de T_EscalaCalificacionDetalle.
        /// </summary>
        /// <returns> List<EscalaCalificacionDetalleDTO> </returns>
        public IEnumerable<EscalaCalificacionDetalle> ObtenerPorIdEscalaCalificacion(int idEscalaCalificacion)
        {
            try
            {
                List<EscalaCalificacionDetalle> rpta = new();
                var query = @"
                        SELECT 
                            Id,
                            IdEscalaCalificacion,
		                    Nombre,
                            Valor,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_EscalaCalificacionDetalle
                        WHERE Estado = 1 AND IdEscalaCalificacion=@idEscalaCalificacion ORDER BY  FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { idEscalaCalificacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<EscalaCalificacionDetalle>>(resultado);
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
        /// Obtiene todos los registros de T_EscalaCalificacionDetalle.
        /// </summary>
        /// <returns> List<EscalaCalificacionDetalleDTO> </returns>
        public List<EscalaCalificacionDetalle> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<EscalaCalificacionDetalle> rpta = new();
                var query = @"
                        SELECT 
                            Id,
                            IdEscalaCalificacion,
		                    Nombre,
                            Valor,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion
                        FROM pla.T_EscalaCalificacionDetalle
                        WHERE Estado = 1 AND Id IN @id ORDER BY  FechaCreacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EscalaCalificacionDetalle>>(resultado);
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



