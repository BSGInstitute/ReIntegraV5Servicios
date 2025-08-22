using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProcedenciaVentaCruzadumRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 29/09/2022
    /// <summary>
    /// Gestión general de T_ProcedenciaVentaCruzada
    /// </summary>
    public class ProcedenciaVentaCruzadumRepository : GenericRepository<TProcedenciaVentaCruzadum>, IProcedenciaVentaCruzadumRepository
    {
        private Mapper _mapper;

        public ProcedenciaVentaCruzadumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcedenciaVentaCruzadum, ProcedenciaVentaCruzadum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// inserta un nueva procedencia de venta cruzada
        /// </summary>
        /// <returns> true o false </returns>
        public bool InsertarProcedenciaVentaCruzada(int IdOportunidadActual, int IdOportunidadNueva)
        {
            try
            {
                string _queryInsert = "com.SP_TProcedenciaVentaCruzada_Insertar";
                var queryInsert = _dapperRepository.QuerySPFirstOrDefault(_queryInsert, new { idOportunidadActual = IdOportunidadActual, idoportunidadNuevo = IdOportunidadNueva });
                JsonConvert.DeserializeObject<Dictionary<string, int>>(queryInsert);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
