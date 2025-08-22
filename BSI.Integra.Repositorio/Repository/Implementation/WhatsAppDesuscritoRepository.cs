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
    /// Repositorio: WhatsAppDesuscritoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/06/2022
    /// <summary>
    /// Gestión general de T_WhatsAppDesuscrito
    /// </summary>
    public class WhatsAppDesuscritoRepository : GenericRepository<TWhatsAppDesuscrito>, IWhatsAppDesuscritoRepository
    {
        private Mapper _mapper;

        public WhatsAppDesuscritoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppDesuscrito, WhatsAppDesuscrito>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public bool ExistePorNumeroTelefono(string numero)
        {
            try
            {
                var query = @"SELECT Id FROM mkt.T_WhatsAppDesuscrito WHERE Estado = 1 AND NumeroTelefono = @Numero";
                var resultado = _dapperRepository.QueryDapper(query, new { Numero = numero });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
