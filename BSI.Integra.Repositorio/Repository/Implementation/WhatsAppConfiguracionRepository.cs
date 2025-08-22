using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppConfiguracionRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de T_WhatsAppConfiguracion
    /// </summary>
    public class WhatsAppConfiguracionRepository : GenericRepository<TWhatsAppConfiguracion>, IWhatsAppConfiguracionRepository
    {
        private Mapper _mapper;

        public WhatsAppConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracion, WhatsAppConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <returns>Objeto de tipo WhatsAppHostDatosDTO</returns>
        public WhatsAppHostDatosDTO ObtenerCredencialHost(int idPais)
        {
            try
            {
                var HostDatos = new WhatsAppHostDatosDTO();
                var Query = string.Empty;
                Query = "SELECT Id, UrlWhatsApp, IpHost, IdPais FROM mkt.T_WhatsAppConfiguracion WHERE Estado = 1 AND IdPais=@idPais";
                var WhatsAppConfiguracionDB = _dapperRepository.FirstOrDefault(Query, new { idPais });
                HostDatos = JsonConvert.DeserializeObject<WhatsAppHostDatosDTO>(WhatsAppConfiguracionDB);
                return HostDatos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp General 
        /// </summary>
        /// <returns>Objeto de tipo WhatsAppHostDatosDTO</returns>
        public List<WhatsAppHostDatosDTO> ObtenerCredencialHostGeneral()
        {
            try
            {
                var HostDatos = new List<WhatsAppHostDatosDTO>();
                var Query = string.Empty;
                Query = "SELECT Id, UrlWhatsApp, IpHost, IdPais FROM mkt.T_WhatsAppConfiguracion WHERE Estado = 1";
                var WhatsAppConfiguracionDB = _dapperRepository.QueryDapper(Query, null);
                HostDatos = JsonConvert.DeserializeObject<List<WhatsAppHostDatosDTO>>(WhatsAppConfiguracionDB);
                return HostDatos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<HoraDTO> ObtenerConfiguracionDeHorariosDeEnvioParaCombo(int intervalo)
        {
            try
            {
                string query = "pla.SP_ObtenerHoraPorIntervalo";
                var WhatsAppConfiguracionDB = _dapperRepository.QuerySPDapper(query, new
                {
                    intervalo
                });
                if (WhatsAppConfiguracionDB != string.Empty && WhatsAppConfiguracionDB != "[]")
                    return JsonConvert.DeserializeObject<List<HoraDTO>>(WhatsAppConfiguracionDB);
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
