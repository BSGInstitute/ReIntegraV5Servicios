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
    public class FormulaTipoDescuentoRepository : GenericRepository<TFormulaTipoDescuento>, IFormulaTipoDescuentoRepository
    {

        private Mapper _mapper;

        public FormulaTipoDescuentoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormulaTipoDescuento, FormulaTipoDescuento>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene todos los registros sin los campos de auditoría.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FormulaTipoDescuentoDTO> ObtenerTodoGrid()
        {
            try
            {
                IEnumerable<FormulaTipoDescuentoDTO> rpta = new List<FormulaTipoDescuentoDTO>();
                var query = @"SELECT Id,Nombre
                                      
                                FROM pla.T_FormulaTipoDescuento
                                WHERE Estado = 1
                                ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<FormulaTipoDescuentoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
