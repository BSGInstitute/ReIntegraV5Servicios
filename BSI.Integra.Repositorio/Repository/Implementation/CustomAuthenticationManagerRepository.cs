using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class CustomAuthenticationManagerRepository : GenericRepository<TIntegraAspNetUser>, ICustomAuthenticationManagerRepository
    {
        private Mapper _mapper;

        public CustomAuthenticationManagerRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIntegraAspNetUser, IntegraAspNetUser>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public AspNetUserAutenticateDTO AutenticacionUsuarioPortal(string UserName, string UsClave)
        {
            try
            {
                var _query = string.Empty;
                AspNetUserAutenticateDTO registro = new AspNetUserAutenticateDTO();
                _query = @"
                         SELECT 
                            Id, PerId AS IdPersonal, RolId AS IdRol, AreaTrabajo, UserName, TipoPersonal
                         FROM 
                            gp.V_ObtenerDatosToken
                         WHERE 
                            UserName = @UserName AND UsClave = @UsClave";
                var respuesta = _dapperRepository.FirstOrDefault(_query, new { UserName, UsClave });

                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != "null")
                {
                    registro = JsonConvert.DeserializeObject<AspNetUserAutenticateDTO>(respuesta);
                }
                else
                {
                    registro = null;
                }

                return registro;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
