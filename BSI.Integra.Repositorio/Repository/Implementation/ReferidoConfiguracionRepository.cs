using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ReferidoConfiguracionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/09/2022
    /// version: 1.0
    /// <summary>
    /// Gestión general de la tabla ReferidoConfiguracion
    /// </summary>
    public class ReferidoConfiguracionRepository : GenericRepository<TReferidoConfiguracion>, IReferidoConfiguracionRepository
    {
        private Mapper _mapper;

        public ReferidoConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReferidoConfiguracion, ReferidoConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// version: 1.0
        /// <summary>
        /// Obtiene el primer registro de la tabla.
        /// </summary>
        /// <returns>ReferidoConfiguracionDTO</returns>
        public ReferidoConfiguracionDTO ObtenerConfiguracionReferidos()
        {
            try
            {
                string query = @"SELECT IdTipoDato, DescripcionTipoDato, IdOrigen, NombreOrigen, 
                                IdFaseOportunidad, CodigoFaseOportunidad 
                                FROM com.V_ObtenerDescripcionReferidoConfiguracion WHERE Estado = 1";
                string queryRespuesta = _dapperRepository.FirstOrDefault(query, null);
                if (queryRespuesta != "null")
                {
                    return JsonConvert.DeserializeObject<ReferidoConfiguracionDTO>(queryRespuesta);
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
