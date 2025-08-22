using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ContactoConfiguracionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/09/2022
    /// version: 1.0
    /// <summary>
    /// Gestión general de la tabla TContactoConfiguracion
    /// </summary>
    public class ContactoConfiguracionRepository : GenericRepository<TContactoConfiguracion>, IContactoConfiguracionRepository
    {
        private Mapper _mapper;

        public ContactoConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContactoConfiguracion, ContactoConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla de acuerdo al IdTipoDato.
        /// </summary>
        /// <param name="idTipoDato"></param>
        /// <returns></returns>
        public ContactoConfiguracionDTO ObtenerConfiguracionContactoPorIdTipoDato(int idTipoDato)
        {
            try
            {
                string query = @"SELECT IdTipoDato, DescripcionTipoDato, IdOrigen, NombreOrigen, 
                                IdFaseOportunidad, CodigoFaseOportunidad 
                                FROM com.V_ObtenerDescripcionContactoConfiguracion 
                                WHERE Estado = 1 AND IdTipoDato = @idTipoDato";
                string queryRespuesta = _dapperRepository.FirstOrDefault(query, new { IdTipoDato = idTipoDato });
                if (queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<ContactoConfiguracionDTO>(queryRespuesta);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
