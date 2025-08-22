using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CampaniaMailingDetalleRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de T_CampaniaMailingDetalle
    /// </summary>
    public class CampaniaMailingDetalleRepository : GenericRepository<TCampaniaMailingDetalle>, ICampaniaMailingDetalleRepository
    {
        private Mapper _mapper;

        public CampaniaMailingDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaMailingDetalle, CampaniaMailingDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el id de la campania mailing por el codigo enviado
        /// </summary>
        /// <param name="codMailing">Codigo Mailing</param>
        /// <returns>Objeto de clase ValorIntDTO</returns>
        public ValorIntDTO? ObtenerIdCampaniaMailing(string codMailing)
        {
            try
            {
                var campaniaMailingDetalle = new ValorIntDTO();
                string _query = @"SELECT IdCampaniaMailing as Valor 
                                FROM mkt.V_TCampaniaMailingDetall_IdCampaniaMailing 
                                WHERE Estado = 1 AND CodMailing = @codMailing";
                var queryCampania = _dapperRepository.FirstOrDefault(_query, new { codMailing });
                if (!string.IsNullOrEmpty(queryCampania) && queryCampania != "null")
                    campaniaMailingDetalle = JsonConvert.DeserializeObject<ValorIntDTO>(queryCampania);
                else
                    return null;
                return campaniaMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtener la Campania Mailing por Id
        /// </summary>
        /// <param name="id">Id de la Campania Mailing a buscar (PK de la tabla mkt.T_CampaniaMailing)</param>
        /// <returns>Objeto de clase CampaniaMailingDTO</returns>
        public CampaniaMailingDTO Obtener(int id)
        {
            try
            {
                var campaniaMailing = new CampaniaMailingDTO();
                var query = @"
                            SELECT Id, 
                            Nombre, 
                            PrincipalValor, 
                            PrincipalValorTiempo, 
                            SecundarioValor, 
                            SecundarioValorTiempo, 
                            ActivaValor, 
                            ActivaValorTiempo, 
                            IdCategoriaOrigen, 
                            FechaCreacion, 
                            FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal, 
                            FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal
                    FROM mkt.V_TCampaniaMailing_DatosCampania
                    WHERE Estado = 1 
                    AND  Id = @id ";
                var respuestaDB = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(respuestaDB) && respuestaDB != "null")
                {
                    campaniaMailing = JsonConvert.DeserializeObject<CampaniaMailingDTO>(respuestaDB);
                }
                return campaniaMailing;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
