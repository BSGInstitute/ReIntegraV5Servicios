using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TarifarioRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_Tarifario
    /// </summary>
    public class TarifarioRepository : GenericRepository<TTarifario>, ITarifarioRepository
    {
        private Mapper _mapper;

        public TarifarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTarifario, Tarifario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza mediante la gestión del Cronograma Pago Tarifario por medio del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> bool </returns>
        public bool ActualizarGestionadoCronogramaPagoTarifario(int id)
        {
            try
            {
                var actualizargestionado = _dapperRepository.QuerySPDapper("[fin].[SP_ActualizarGestionadoCronogramaPagoTarifario]", new { id });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try

            {
                List<FiltroDTO> rpta = new List<FiltroDTO>();
                var query = @" SELECT id, nombre FROM mkt.T_Tarifario WHERE estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FiltroDTO>>(resultado);
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
