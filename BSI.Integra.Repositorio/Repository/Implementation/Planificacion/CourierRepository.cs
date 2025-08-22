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
    /// Repositorio: CourierRepository
    /// Autor: Gretel Canasa.
    /// Fecha: 10/06/2023
    /// <summary>
    /// Gestión general de T_Courier
    /// </summary>
    public class CourierRepository : GenericRepository<TCourier>, ICourierRepository
    {
        private Mapper _mapper;

        public CourierRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCourier, Courier>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCourier MapeoEntidad(Courier entidad)
        {
            try
            {
                TCourier modelo = _mapper.Map<TCourier>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCourier Add(Courier entidad)
        {
            try
            {
                var Courier = MapeoEntidad(entidad);
                base.Insert(Courier);
                return Courier;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCourier Update(Courier entidad)
        {
            try
            {
                var Courier = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Courier.RowVersion = entidadExistente.RowVersion;

                base.Update(Courier);
                return Courier;
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


        public IEnumerable<TCourier> Add(IEnumerable<Courier> listadoEntidad)
        {
            try
            {
                List<TCourier> listado = new List<TCourier>();
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

        public IEnumerable<TCourier> Update(IEnumerable<Courier> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCourier> listado = new List<TCourier>();
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
        /// Autor: Gretel Canasa.
        /// Fecha: 10/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Courier.
        /// </summary>
        /// <returns> List<CourierDTO> </returns>
      
        public List<CourierDTO> ObtenerCourier()
        {
            try
            {
                List<CourierDTO> listaCourier = new List<CourierDTO>();
                var queryText = string.Empty;
                queryText = @"SELECT  C.Id,C.Nombre,C.IdPais,CP.Id AS IdCiudad,C.Direccion,C.Telefono,C.Url,NombrePais AS Pais, CP.Nombre AS Ciudad FROM pla.T_Courier AS C " +
                    "INNER JOIN conf.T_Pais AS P ON P.id=C.IdPais INNER JOIN conf.T_Ciudad AS CP ON C.IdCiudad=CP.id WHERE C.Estado = 1 AND P.Estado=1 AND CP.Estado=1 ORDER BY C.Id DESC";
                var datosCourier = _dapperRepository.QueryDapper(queryText, null);

                if (!string.IsNullOrEmpty(datosCourier) && !datosCourier.Contains("[]") && datosCourier != "null")
                {
                    listaCourier = JsonConvert.DeserializeObject<List<CourierDTO>>(datosCourier);
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
            /// <returns> List<CourierDTO> </returns>
            public Courier ObtenerPorId(int id)
        {
            try
            {
                Courier rpta = new();
                var query = @"
                   SELECT Direccion,
                   Estado,
                   FechaCreacion,
                   FechaModificacion,
                   Id,
                   IdCiudad,
                   IdMigracion,
                   IdPais,
                   Nombre,
                   RowVersion,
                   Telefono,
                   Url,
                   UsuarioCreacion,
                   UsuarioModificacion 
                   FROM pla.T_Courier WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Courier>(resultado);
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
        public List<Courier> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<Courier> rpta = new();
                var query = @"
SELECT  C.Id,C.Nombre,C.IdPais,CP.Id AS IdCiudad,C.Direccion,C.Telefono,C.Url,NombrePais AS Pais, CP.Nombre AS Ciudad FROM pla.T_Courier AS C INNER JOIN conf.T_Pais AS P ON P.id=C.IdPais INNER JOIN conf.T_Ciudad AS CP ON C.IdCiudad=CP.id WHERE C.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND Id=@id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<Courier>>(resultado);
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



