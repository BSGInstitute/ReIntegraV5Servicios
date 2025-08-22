
using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ResumenGrabacionOnlineRepository
    /// Autor: Jorge Gamero
    /// Fecha: 10/02/2025
    /// <summary>
    /// Gestión general de T_ResumenGrabacionOnline
    /// </summary>
    public class ResumenGrabacionOnlineRepository : GenericRepository<TResumenGrabacionOnline>, IResumenGrabacionOnlineRepository
    {
        private Mapper _mapper;

        public ResumenGrabacionOnlineRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TResumenGrabacionOnline, ResumenGrabacionOnline>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TResumenGrabacionOnline MapeoEntidad(ResumenGrabacionOnline entidad)
        {
            try
            {
                TResumenGrabacionOnline modelo = _mapper.Map<TResumenGrabacionOnline>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ResumenGrabacionOnline
        /// </summary>
        /// <returns> IEnumerable<ResumenGrabacionOnlineDTO> </returns>
        public IEnumerable<ResumenGrabacionOnlineDTO> ObtenerResumenGrabacionOnline()
        {
            try
            {
                var rpta = new List<ResumenGrabacionOnlineDTO>();
                var query = @"SELECT
                    Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion
                    FROM pla.T_ResumenGrabacionOnline
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<ResumenGrabacionOnlineDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ResumenGrabacionOnline filtrado por Id
        /// </summary>
        /// <returns> ResumenGrabacionOnlineDTO </returns>
        public ResumenGrabacionOnlineDTO ObtenerResumenGrabacionOnlinePorId(int id)
        {
            try
            {
                var rpta = new ResumenGrabacionOnlineDTO();
                var query = @"SELECT
                    Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion
                    FROM pla.T_ResumenGrabacionOnline
                    WHERE Id = @id AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var listaRpta = JsonConvert.DeserializeObject<List<ResumenGrabacionOnlineDTO>>(resultado);
                    rpta = listaRpta.FirstOrDefault();
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 11/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProcesamientoTipoGenerar filtrado por Id
        /// </summary>
        /// <returns> ResumenGrabacionOnlineDTO </returns>
        public ProcesamientoTipoGenerarDTO ObtenerProcesamientoTipoGenerarPorId(int id)
        {
            try
            {
                var rpta = new ProcesamientoTipoGenerarDTO();
                var query = @"SELECT
                    Id, IdProcesamientoSesionOnline, IdResumenGrabacionOnline, RegistroUrl, Realizado, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion
                    FROM ia.T_ProcesamientoTipoGenerar
                    WHERE Id = @id AND Estado = 1 AND Realizado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var listaRpta = JsonConvert.DeserializeObject<List<ProcesamientoTipoGenerarDTO>>(resultado);
                    rpta = listaRpta.FirstOrDefault();
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
