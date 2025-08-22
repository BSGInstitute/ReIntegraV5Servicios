using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppConfiguracionApiRepository
    /// Autor: Jorge Gamero.
    /// Fecha: 19/08/2024
    /// <summary>
    /// Gestión general de WhatsAppConfiguracionApi
    /// </summary>
    public class WhatsAppConfiguracionApiRepository : GenericRepository<TWhatsAppConfiguracionApi>, IWhatsAppConfiguracionApiRepository
    {
        private Mapper _mapper;

        public WhatsAppConfiguracionApiRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracionApi, WhatsAppConfiguracionApi>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppConfiguracionApi MapeoEntidad(WhatsAppConfiguracionApi entidad)
        {
            try
            {
                TWhatsAppConfiguracionApi modelo = _mapper.Map<TWhatsAppConfiguracionApi>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionApi Add(WhatsAppConfiguracionApi entidad)
        {
            try
            {
                var WhatsAppConfiguracionApi = MapeoEntidad(entidad);
                base.Insert(WhatsAppConfiguracionApi);
                return WhatsAppConfiguracionApi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionApi Update(WhatsAppConfiguracionApi entidad)
        {
            try
            {
                var mapeo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                mapeo.RowVersion = entidadExistente.RowVersion;

                base.Update(mapeo);
                return mapeo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<WhatsAppConfiguracionApiListaGrillaDTO> ObtenerCredencialesUsuarios()
        {
            try
            {
                List<WhatsAppConfiguracionApiListaGrillaDTO> usuarioswhatsApp = new List<WhatsAppConfiguracionApiListaGrillaDTO>();
                var _query = string.Empty;
                _query = "SELECT IdWhatsAppConfiguracionApi, Numero, VName, IdPais, Pais, Bearer, NumeroIndentificador, VersionApi, FechaExpiracion, NumeroWhatsApp, CuentaIdentificadorWhatsApp, IdPersonal, Personal, IdPersonalAreaTrabajo as IdArea, Area FROM mkt.V_WhatsAppUsuarioComercial";
                var usuariosWhatsAppDB = _dapperRepository.QueryDapper(_query, null);
                usuarioswhatsApp = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionApiListaGrillaDTO>>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 19/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_WhatsAppConfiguracionApi por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> WhatsAppConfiguracionApi </returns>
        public WhatsAppConfiguracionApi ObtenerPorId(int id)
        {
            try
            {
                var rpta = new WhatsAppConfiguracionApi();
                var query = @"SELECT * FROM conf.T_WhatsAppConfiguracionApi WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<WhatsAppConfiguracionApi>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Eliot Arias F
        /// Fecha: 19/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obitiene los campos deT_WhatsAppConfiguracionApi segun el id del personal asignado y el codigo de pais
        /// </summary>
        /// <param name="id"> Id del personal </param>
        /// <returns> WhatsAppConfiguracionApi </returns>
        public WhatsAppConfiguracionApiNumeroIdentificadorDto ObtenerNumeroIdentificadorWhatsAppPorID(int id, int idCodigoPais)
        {
            try
            {
                var rpta = new WhatsAppConfiguracionApiNumeroIdentificadorDto();
                var query = @"SELECT * FROM conf.V_TWhatsAppConfiguracionApi_ObtenerNumeroIdentificador WHERE IdPersonalAsignado = @IdPersonal AND IdPais = @idPais";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersonal = id, idPais = idCodigoPais });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<WhatsAppConfiguracionApiNumeroIdentificadorDto>(resultado);
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