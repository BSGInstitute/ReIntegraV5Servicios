using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CourierDetalleRepository
    /// Autor: Gretel Canasa.
    /// Fecha: 10/06/2023
    /// <summary>
    /// Gestión general de T_Courier
    /// </summary>
    public class CourierDetalleRepository : GenericRepository<TCourierDetalle>, ICourierDetalleRepository
    {
        private Mapper _mapper;

        public CourierDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCourierDetalle, CourierDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCourierDetalle MapeoEntidad(CourierDetalle entidad)
        {
            try
            {
                TCourierDetalle modelo = _mapper.Map<TCourierDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCourierDetalle Add(CourierDetalle entidad)
        {
            try
            {
                var CourierDetalle = MapeoEntidad(entidad);
                base.Insert(CourierDetalle);
                return CourierDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCourierDetalle Update(CourierDetalle entidad)
        {
            try
            {
                var CourierDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CourierDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(CourierDetalle);
                return CourierDetalle;
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
        /// Autor: Gretel Canasa.
        /// Fecha: 10/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Courier.
        /// </summary>
        /// <returns> List<CourierDetalleDTO> </returns>
      
        public List<CourierDetalleDTO> ObtenerPorIdCourier(int idCourier)
        {
            try
            {
                List<CourierDetalleDTO> listaCourier = new List<CourierDetalleDTO>();
                var queryText = string.Empty;
                queryText = @"
SELECT  CD.Id,CD.IdCourier,C.Nombre,CD.IdPais,CP.Id AS IdCiudad,CD.Direccion,CD.Telefono, CD.TiempoEnvio,P.NombrePais AS Pais, CP.Nombre AS Ciudad 
                    FROM pla.T_CourierDetalle AS CD INNER JOIN pla.T_Courier AS C ON C.Id=CD.IdCourier 
                    INNER JOIN conf.T_Pais AS P ON P.id=CD.IdPais 
                    INNER JOIN conf.T_Ciudad AS CP ON CD.IdCiudad=CP.id WHERE CD.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND CD.IdCourier=@idCourier ORDER BY CD.Id DESC";
                var datosCourier = _dapperRepository.QueryDapper(queryText, new {idCourier});

                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<CourierDetalleDTO>>(datosCourier);
                }
                return listaCourier;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
            /// Autor: Gretel Canasa.
            /// Fecha: 10/06/2023
            /// Version: 1.0
            /// <summary>
            /// Obtiene todos los registros de T_Courier.
            /// </summary>
            /// <returns> List<CourierDetalleDTO> </returns>
            public CourierDetalle ObtenerPorId(int id)
        {
            try
            {
                CourierDetalle rpta = new();
                var query = @"
                    SELECT  Direccion,
                        Estado,
                        FechaCreacion,
                        FechaModificacion,
                        Id,
                        IdCiudad,
                        IdCourier,
                        IdMigracion,
                        IdPais,
                        RowVersion,
                        Telefono,
                        TiempoEnvio,
                        UsuarioCreacion,
                        UsuarioModificacion FROM pla.T_CourierDetalle WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<CourierDetalle>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gretel Canasa.
        /// Fecha: 10/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Courier.
        /// </summary>
        /// <returns> List<CourierDTO> </returns>
        public List<CourierDetalle> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<CourierDetalle> rpta = new();
                var query = @"
SELECT  CD.Id,CD.IdCourier,C.Nombre,CD.IdPais,CP.Id AS IdCiudad,CD.Direccion,CD.Telefono, CD.TiempoEnvio,P.NombrePais AS Pais, CP.Nombre AS Ciudad 
                    FROM pla.T_CourierDetalle AS CD INNER JOIN pla.T_Courier AS C ON C.Id=CD.IdCourier 
                    INNER JOIN conf.T_Pais AS P ON P.id=CD.IdPais 
                    INNER JOIN conf.T_Ciudad AS CP ON CD.IdCiudad=CP.id WHERE CD.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND CD.Id=@id ORDER BY CD.Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CourierDetalle>>(resultado);
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



