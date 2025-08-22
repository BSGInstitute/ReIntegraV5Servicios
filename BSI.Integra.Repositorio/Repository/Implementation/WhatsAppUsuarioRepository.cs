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
    /// Repositorio: WhatsAppUsuarioRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de WhatsAppUsuario
    /// </summary>
    public class WhatsAppUsuarioRepository : GenericRepository<TWhatsAppUsuario>, IWhatsAppUsuarioRepository
    {
        private Mapper _mapper;

        public WhatsAppUsuarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppUsuario, WhatsAppUsuario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TWhatsAppUsuario MapeoEntidad(WhatsAppUsuario entidad)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppUsuario modelo = _mapper.Map<TWhatsAppUsuario>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppUsuario Add(WhatsAppUsuario entidad)
        {
            try
            {
                var WhatsAppUsuario = MapeoEntidad(entidad);
                base.Insert(WhatsAppUsuario);
                return WhatsAppUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppUsuario Update(WhatsAppUsuario entidad)
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
        /// Autor: Gretel Canasa
        /// Fecha: 05/06/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lo lista del personal

        /// <returns>WhatsAppPersonalDTO</returns>
        public List<WhatsAppPersonalDTO> ObtenerListaPersonal()
        {
            try
            {
                List<WhatsAppPersonalDTO> listaPersonal = new List<WhatsAppPersonalDTO>();
                var _query = string.Empty;
                _query = "SELECT PE.Id, CONCAT(PE.Apellidos, ' ', PE.Nombres) AS Nombres, PE.Rol, US.UserName " +
                         "FROM gp.T_Personal AS PE " +
                         "INNER JOIN conf.T_Integra_AspNetUsers AS US ON PE.Id=US.PerId " +
                         "WHERE PE.Estado=1 AND PE.Activo=1";
                var usuariosPersonal = _dapperRepository.QueryDapper(_query, null);
                listaPersonal = JsonConvert.DeserializeObject<List<WhatsAppPersonalDTO>>(usuariosPersonal);
                return listaPersonal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<WhatsAppUsuarioListaGrillaDTO> ObtenerCredencialesUsuario()
        {
            try
            {
                List<WhatsAppUsuarioListaGrillaDTO> usuarioswhatsApp = new List<WhatsAppUsuarioListaGrillaDTO>();
                var _query = string.Empty;
                //_query = "SELECT WU.Id,WU.IdPersonal,WU.RolUser,WU.UserUsername,WU.UserPassword,CONCAT(PE.Nombres,' ',pe.ApellidoPaterno,' ',PE.ApellidoMaterno) AS Nombres " +
                //         "FROM mkt.T_WhatsAppUsuario AS WU " +
                //         "INNER JOIN gp.T_Personal AS PE ON WU.IdPersonal=PE.Id " +
                //         "WHERE WU.Estado=1";
                _query = "SELECT * FROM mkt.V_WhatsAppUsuario";
                var usuariosWhatsAppDB = _dapperRepository.QueryDapper(_query, null);
                usuarioswhatsApp = JsonConvert.DeserializeObject<List<WhatsAppUsuarioListaGrillaDTO>>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppUsuarioDTO ObtenerCredencialUsuarioPorId(int idUsuario)
        {
            try
            {
                WhatsAppUsuarioDTO usuarioswhatsApp = new WhatsAppUsuarioDTO();
                var _query = string.Empty;
                //_query = "SELECT WU.Id,WU.IdPersonal,WU.RolUser,WU.UserUsername,WU.UserPassword,CONCAT(PE.Nombres,' ',pe.ApellidoPaterno,' ',PE.ApellidoMaterno) AS Nombres " +
                //         "FROM mkt.T_WhatsAppUsuario AS WU " +
                //         "INNER JOIN gp.T_Personal AS PE ON WU.IdPersonal=PE.Id " +
                //         "WHERE WU.Estado=1 AND WU.Id=@idUsuario";
                _query = "SELECT * FROM mkt.V_WhatsAppUsuario WHERE Id=@idUsuario";
                var usuariosWhatsAppDB = _dapperRepository.FirstOrDefault(_query, new { idUsuario = idUsuario });
                usuarioswhatsApp = JsonConvert.DeserializeObject<WhatsAppUsuarioDTO>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppUsuarioDTO UsuarioWhatsAppValido(int idPersonal)
        {
            try
            {
                WhatsAppUsuarioDTO usuarioswhatsApp = new WhatsAppUsuarioDTO();
                var query = string.Empty;
                //query = "SELECT WU.Id,WU.IdPersonal,WU.RolUser,WU.UserUsername,WU.UserPassword,CONCAT(PE.Nombres,' ',pe.ApellidoPaterno,' ',PE.ApellidoMaterno) AS Nombres " +
                //         "FROM mkt.T_WhatsAppUsuario AS WU " +
                //         "INNER JOIN gp.T_Personal AS PE ON WU.IdPersonal=PE.Id " +
                //         "WHERE WU.Estado=1 AND WU.IdPersonal=@idPersonal";
                query = "SELECT * FROM mkt.V_WhatsAppUsuario WHERE IdPersonal=@idPersonal";
                var usuariosWhatsAppDB = _dapperRepository.FirstOrDefault(query, new { idPersonal = idPersonal });
                usuarioswhatsApp = JsonConvert.DeserializeObject<WhatsAppUsuarioDTO>(usuariosWhatsAppDB);
                return usuarioswhatsApp;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 17/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_WhatsAppUsuario por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> WhatsAppUsuario </returns>
        public WhatsAppUsuario ObtenerPorId(int id)
        {
            try
            {
                var rpta = new WhatsAppUsuario();
                var query = @"SELECT * FROM mkt.T_WhatsAppUsuario WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<WhatsAppUsuario>(resultado);
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