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
    /// Repositorio: TipoEnvioAutomaticoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de T_TipoEnvioAutomatico
    /// </summary>
    public class TipoEnvioAutomaticoRepository : GenericRepository<TTipoEnvioAutomatico>, ITipoEnvioAutomaticoRepository
    {
        private Mapper _mapper;

        public TipoEnvioAutomaticoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoEnvioAutomatico, TipoEnvioAutomatico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Tipos de Envio para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns> Lista </returns>
        public List<FiltroDTO> ObtenerTodoCombo()
        {
            try
            {
                List<FiltroDTO> comboRepuesta = new List<FiltroDTO>();
                var query = @"SELECT 
                                Id, Nombre 
                              FROM 
                                mkt.T_TipoEnvioAutomatico
                              WHERE 
                                Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    comboRepuesta = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado)!;
                }
                return comboRepuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
