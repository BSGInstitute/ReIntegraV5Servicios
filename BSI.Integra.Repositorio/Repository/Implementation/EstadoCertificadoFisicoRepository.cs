using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstadoCertificadoFisicoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_EstadoCertificadoFisico
    /// </summary>
    public class EstadoCertificadoFisicoRepository : GenericRepository<TEstadoCertificadoFisico>, IEstadoCertificadoFisicoRepository
    {
        private Mapper _mapper;

        public EstadoCertificadoFisicoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoCertificadoFisico, EstadoCertificadoFisico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Estado para Filtro
        /// </summary>
        /// <returns></returns>
        public List<EstadoCertificadoFisicoDTO> ObtenerEstadParaFiltro()
        {
            try
            {
                List<EstadoCertificadoFisicoDTO> respuesta = new List<EstadoCertificadoFisicoDTO>();
                string query = "Select Id,Nombre From ope.V_ObtenerEstadoCertificadoFisico Where Estado=1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<EstadoCertificadoFisicoDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
