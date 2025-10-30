using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    public class ProgramaMotivacionRepository : GenericRepository<TProgramaMotivacion>, IProgramaMotivacionRepository
    {
        private Mapper _mapper;
        public ProgramaMotivacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaMotivacion, ProgramaMotivacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public List<ProgramaMotivacion> ObtenerTodo()
        {
            try
            {
                List<ProgramaMotivacion> rpta = new();
                var query = @"
                   SELECT 
                    Id,
                    Descripcion,
                    Estado,
                    UsuarioCreacion,
                    UsuarioModificacion,
                    FechaCreacion,
                    FechaModificacion
                    FROM pla.T_ProgramaMotivacion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaMotivacion>>(resultado);
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
