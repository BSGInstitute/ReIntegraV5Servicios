using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: TipoRespuestaCalificacionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 27/07/2023
    /// <summary>
    /// Gestión general de T_TipoRespuestaCalificacion
    /// </summary>
    public class TipoRespuestaCalificacionRepository : GenericRepository<TTipoRespuestaCalificacion>, ITipoRespuestaCalificacionRepository
    {
        private Mapper _mapper;

        public TipoRespuestaCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoRespuestaCalificacion, TipoRespuestaCalificacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// <summary>
        /// Obtiene tipo respuesta calificacion para filtro
        /// </summary>
        /// <returns>Lista de objetos de tipo FiltroDTO con los tipos de respuesta</returns>
        public IEnumerable<TipoRespuestaCalificacion> Obtener()
        {
            try
            {
                var query = @"SELECT Id,
                                       Nombre,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM gp.T_TipoRespuestaCalificacion
                                WHERE Estado = 1;";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]")) 
                    return JsonConvert.DeserializeObject<IEnumerable<TipoRespuestaCalificacion>>(res); 
                return new List<TipoRespuestaCalificacion>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
