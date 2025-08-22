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
    /// Repositorio: PgeneralTipoDescuentoRepository
    /// Autor: Gretel Canasa.
    /// Fecha: 10/06/2023
    /// <summary>
    /// Gestión general de T_PgeneralTipoDescuento
    /// </summary>
    public class PgeneralTipoDescuentoRepository : GenericRepository<TPgeneralTipoDescuento>, IPgeneralTipoDescuentoRepository
    {
        private Mapper _mapper;

        public PgeneralTipoDescuentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralTipoDescuento, PgeneralTipoDescuento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralTipoDescuento MapeoEntidad(PgeneralTipoDescuento entidad)
        {
            try
            {
                TPgeneralTipoDescuento modelo = _mapper.Map<TPgeneralTipoDescuento>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralTipoDescuento Add(PgeneralTipoDescuento entidad)
        {
            try
            {
                var PgeneralTipoDescuento = MapeoEntidad(entidad);
                base.Insert(PgeneralTipoDescuento);
                return PgeneralTipoDescuento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralTipoDescuento Update(PgeneralTipoDescuento entidad)
        {
            try
            {
                var PgeneralTipoDescuento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralTipoDescuento.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralTipoDescuento);
                return PgeneralTipoDescuento;
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


        public IEnumerable<TPgeneralTipoDescuento> Add(IEnumerable<PgeneralTipoDescuento> listadoEntidad)
        {
            try
            {
                List<TPgeneralTipoDescuento> listado = new List<TPgeneralTipoDescuento>();
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

        public IEnumerable<TPgeneralTipoDescuento> Update(IEnumerable<PgeneralTipoDescuento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralTipoDescuento> listado = new List<TPgeneralTipoDescuento>();
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
            /// Obtiene todos los registros de T_PgeneralTipoDescuento.
            /// </summary>
            /// <returns> List<PgeneralTipoDescuentoDTO> </returns>
            public PgeneralTipoDescuento ObtenerPorId(int id)
        {
            try
            {
                PgeneralTipoDescuento rpta = new();
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
                   FROM pla.T_PgeneralTipoDescuento WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PgeneralTipoDescuento>(resultado);
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
        /// Obtiene todos los registros de T_PgeneralTipoDescuento.
        /// </summary>
        /// <returns> List<PgeneralTipoDescuentoDTO> </returns>
        public List<PgeneralTipoDescuento> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<PgeneralTipoDescuento> rpta = new();
                var query = @"
SELECT  C.Id,C.Nombre,C.IdPais,CP.Id AS IdCiudad,C.Direccion,C.Telefono,C.Url,NombrePais AS Pais, CP.Nombre AS Ciudad FROM pla.T_PgeneralTipoDescuento AS C INNER JOIN conf.T_Pais AS P ON P.id=C.IdPais INNER JOIN conf.T_Ciudad AS CP ON C.IdCiudad=CP.id WHERE C.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND Id=@id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralTipoDescuento>>(resultado);
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



