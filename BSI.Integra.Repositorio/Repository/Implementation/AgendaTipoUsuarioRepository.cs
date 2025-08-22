using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class AgendaTipoUsuarioRepository : GenericRepository<TAgendaTipoUsuario>, IAgendaTipoUsuarioRepository
    {
        private Mapper _mapper;

        public AgendaTipoUsuarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAgendaTipoUsuario, AgendaTipoUsuario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene los tipos de usuarios de la agenda Id y Nombre para Filtro
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AgendaTipoUsuarioDTO> ObtenerTipoUsuarioFiltro()
        {
            try
            {
                IEnumerable<AgendaTipoUsuarioDTO> agendaUsuarios = new List<AgendaTipoUsuarioDTO>();
                string _query = "SELECT Id, Nombre FROM com.T_AgendaTipoUsuario WHERE Estado=1";
                var usuariosAgendaDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(usuariosAgendaDB) && !usuariosAgendaDB.Contains("[]"))
                {
                    agendaUsuarios = JsonConvert.DeserializeObject<IEnumerable<AgendaTipoUsuarioDTO>>(usuariosAgendaDB)!;
                }
                return agendaUsuarios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
