using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    /// Repositorio: PreguntaTipoRepository
    /// Autor: Gilmer qm.
    /// Fecha: 27/07/2023
    /// <summary>
    /// Gestión general de T_PreguntaTipo
    /// </summary>
    public class PreguntaTipoRepository : GenericRepository<TPreguntaTipo>, IPreguntaTipoRepository
    {
        private Mapper _mapper;
        public PreguntaTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaTipo, PreguntaTipo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor : Gilmer Qm.
        /// Fecha: 27/07/2023
        /// Version: 1.0 
        /// <summary>
        /// Obtiene todos los registros con Estado=1
        /// </summary>
        public IEnumerable<PreguntaTipo> Obtener()
        {
            try
            {
                var query = @"SELECT Id,
                                       Nombre,
                                       IdTipoRespuesta,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM gp.T_PreguntaTipo
                                WHERE Estado = 1;";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]") && res != null)
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaTipo>>(res);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor : Gilmer Qm.
        /// Fecha: 27/07/2023
        /// <summary>
        /// Obtiene tipos de preguntas habilitados
        /// </summary>
        /// <returns>IEnumerable<PreguntaTipoRespuestaDTO></returns>
        public IEnumerable<PreguntaTipoRespuestaDTO> ObtenerPreguntaTipoRespuesta()
        {
            try
            {
                var query = "SELECT Id, Nombre, IdTipoRespuesta, TipoRespuesta FROM gp.V_ObtenerTipoPregunta";
                var dataDB = _dapperRepository.QueryDapper(query, null);
                if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
                    return JsonConvert.DeserializeObject<IEnumerable<PreguntaTipoRespuestaDTO>>(dataDB);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
