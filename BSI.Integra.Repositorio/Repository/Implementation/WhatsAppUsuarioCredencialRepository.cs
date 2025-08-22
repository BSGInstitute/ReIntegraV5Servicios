using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppUsuarioCredencialRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de WhatsAppUsuarioCredencial
    /// </summary>
    public class WhatsAppUsuarioCredencialRepository : GenericRepository<TWhatsAppUsuarioCredencial>, IWhatsAppUsuarioCredencialRepository
    {
        private Mapper _mapper;

        public WhatsAppUsuarioCredencialRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppUsuarioCredencial, WhatsAppUsuarioCredencial>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppUsuarioCredencial MapeoEntidad(WhatsAppUsuarioCredencial entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppUsuarioCredencial modelo = _mapper.Map<TWhatsAppUsuarioCredencial>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppUsuarioCredencial Add(WhatsAppUsuarioCredencial entidad)
        {
            try
            {
                var mapeo = MapeoEntidad(entidad);
                base.Insert(mapeo);
                return mapeo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppUsuarioCredencial Update(WhatsAppUsuarioCredencial entidad)
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


        public IEnumerable<TWhatsAppUsuarioCredencial> Add(IEnumerable<WhatsAppUsuarioCredencial> listadoEntidad)
        {
            try
            {
                List<TWhatsAppUsuarioCredencial> listado = new List<TWhatsAppUsuarioCredencial>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TWhatsAppUsuarioCredencial> Update(IEnumerable<WhatsAppUsuarioCredencial> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppUsuarioCredencial> listado = new List<TWhatsAppUsuarioCredencial>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <param name="idPersonal">Id del personal</param>
        /// <param name="idPais">id del pais</param>
        /// <returns>CredencialTokenExpiraDTO</returns>
        public CredencialTokenExpiraDTO ValidarCredencialesUsuario(int idPersonal, int idPais)
        {
            try
            {
                CredencialTokenExpiraDTO tokenCredencial = new CredencialTokenExpiraDTO();
                var query = string.Empty;
                query = @"SELECT		TOP 1
			                            OCWA.IdWhatsAppUsuario,
			                            OCWA.UserAuthToken,
			                            OCWA.ExpiresAfter,IdPersonal,IdPais
                            FROM mkt.V_ObtenerCredencialWhatsApp OCWA
                            WHERE
			                            OCWA.IdPersonal = @idPersonal AND OCWA.IdPais = @idPais 
                            ORDER BY	OCWA.ExpiresAfter DESC;";
                var credencialTokenExpiraDB = _dapperRepository.FirstOrDefault(query, new { idPersonal, idPais });
                tokenCredencial = JsonConvert.DeserializeObject<CredencialTokenExpiraDTO>(credencialTokenExpiraDB);
                return tokenCredencial;
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
        /// Obtiene las credenciales de login de un personal especifico
        /// </summary>
        /// <param name="idPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de tipo de CredencialUsuarioLoginDTO</returns>
        public CredencialUsuarioLoginDTO ObtenerCredencialUsuarioLogin(int idPersonal)
        {
            try
            {
                var tokenCredencial = new CredencialUsuarioLoginDTO();
                var _query = string.Empty;
                _query = @"SELECT Id AS IdWhatsAppUsuario, UserUsername, UserPassword 
                         FROM mkt.T_WhatsAppUsuario 
                         WHERE IdPersonal = @idPersonal AND Estado = 1";
                var CredencialTokenExpiraDB = _dapperRepository.FirstOrDefault(_query, new { idPersonal });
                tokenCredencial = JsonConvert.DeserializeObject<CredencialUsuarioLoginDTO>(CredencialTokenExpiraDB);
                return tokenCredencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene las credenciales de login de un personal especifico
        /// </summary>
        /// <param name="idPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de tipo de CredencialUsuarioLoginDTO</returns>
        public CredencialUsuarioLoginDTO CredencialUsuarioLogin(int idPersonal)
        {
            try
            {
                CredencialUsuarioLoginDTO tokenCredencial = new CredencialUsuarioLoginDTO();
                var _query = string.Empty;
                _query = "SELECT Id AS IdWhatsAppUsuario, UserUsername, UserPassword " +
                         "FROM mkt.T_WhatsAppUsuario " +
                         "WHERE IdPersonal = @idPersonal AND Estado = 1";
                var CredencialTokenExpiraDB = _dapperRepository.FirstOrDefault(_query, new { idPersonal });
                tokenCredencial = JsonConvert.DeserializeObject<CredencialUsuarioLoginDTO>(CredencialTokenExpiraDB);
                return tokenCredencial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Inserta en mkt.T_WhatsAppUsuarioCredencial
        /// </summary>
        /// <param name="filtro">Objeto de tipo TWhatsAppUsuarioCredencial</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsAppUsuarioCredencial(TWhatsAppUsuarioCredencial filtro)
        {
            try
            {
                var resultado = new ValorIntDTO();

                string spQuery = "[mkt].[SP_InsertarWhatsAppUsuarioCredencial]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    filtro.IdWhatsAppUsuario,
                    filtro.IdWhatsAppConfiguracion,
                    filtro.UserAuthToken,
                    filtro.ExpiresAfter,
                    filtro.EsMigracion,
                    filtro.UsuarioCreacion,
                    filtro.UsuarioModificacion
                });

                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
                }
                if (resultado != null)
                    return Convert.ToInt32(resultado.Valor);
                else
                    return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las credenciales de login de un personal especifico
        /// </summary>
        /// <param name="idPersonal">Id del personal (gp.T_Personal)</param>
        /// <returns>Objeto de tipo de CredencialUsuarioLoginDTO</returns>
        public IEnumerable<WhatsAppHostDatosDTO> ObtenerCredencialesHost()
        {
            try
            {
                var tokenCredencial = new List<WhatsAppHostDatosDTO>();
                var _query = @"SELECT Id, UrlWhatsApp, IpHost, IdPais FROM mkt.T_WhatsAppConfiguracion WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    return JsonConvert.DeserializeObject<List<WhatsAppHostDatosDTO>>(res);
                }
                return new List<WhatsAppHostDatosDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
