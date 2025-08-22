using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: UsuarioRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/11/2022
    /// <summary>
    /// Gestión general de la tabla T_Usuario
    /// </summary>
    public class UsuarioRepository : GenericRepository<TUsuario>, IUsuarioRepository
    {
        private Mapper _mapper;

        public UsuarioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TUsuario, Usuario>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TUsuario MapeoEntidad(Usuario entidad)
        {
            try
            {
                TUsuario modelo = _mapper.Map<TUsuario>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TUsuario Add(Usuario entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                base.Insert(MaterialAccion);
                return MaterialAccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TUsuario Update(Usuario entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialAccion.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialAccion);
                return MaterialAccion;
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> UsuarioDTO </returns>
        public UsuarioDTO ObtenerPorNombreUsuario(string usuario)
        {
            try
            {
                var usuarioDto = new UsuarioDTO();
                var query = string.Empty;
                query = @"SELECT Id,IdPersonal,NombreUsuario,Clave,IdUsuarioRol,CodigoAreaTrabajo
                            FROM conf.T_Usuario
                            WHERE Estado = 1 AND NombreUsuario = @NombreUsuario";
                var resultado = _dapperRepository.FirstOrDefault(query, new { NombreUsuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    usuarioDto = JsonConvert.DeserializeObject<UsuarioDTO>(resultado)!;
                }
                return usuarioDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> Entidad: usuarioEntidad </returns>
        public Usuario ObtenerTotalPorUsuario(string usuario)
        {
            try
            {
                Usuario usuarioEntidad = new Usuario();
                var query = @"
                        SELECT
                            Id, IdPersonal, NombreUsuario, Clave, IdUsuarioRol, CodigoAreaTrabajo, Estado, UsuarioCreacion, 
                            UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion 
                        FROM 
                            conf.T_Usuario
                        WHERE 
                            Estado = 1 AND NombreUsuario = @NombreUsuario";
                var resultado = _dapperRepository.FirstOrDefault(query, new { NombreUsuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    usuarioEntidad = JsonConvert.DeserializeObject<Usuario>(resultado)!;
                }
                return usuarioEntidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> UsuarioDTO </returns>
        public Usuario ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                var usuarioDto = new Usuario();
                var query = string.Empty;
                query = @"SELECT
                            Id, IdPersonal, NombreUsuario, Clave, IdUsuarioRol, CodigoAreaTrabajo, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion   
                        FROM conf.T_Usuario
                        WHERE IdPersonal = @idPersonal";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    usuarioDto = JsonConvert.DeserializeObject<Usuario>(resultado)!;
                }
                return usuarioDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario
        /// </summary>
        /// <returns> Entidad: usuarioEntidad </returns>
        public IEnumerable<GestionUsuarioDTO> ObtenerTodo()
        {
            try
            {
                List<GestionUsuarioDTO> usuarios = new List<GestionUsuarioDTO>();
                string _query = @"
                                SELECT Id, UserName, Email, Nombre, Rol, AreaTrabajo, RolId, PerId, UsClave, IdUsuario, UsuarioCreacion, UsuarioModificacion
                                FROM gp.V_ObtenerDatosGestionUsuarios
                                WHERE Activo = 1 ORDER BY Fechacreacion DESC";
                var res = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    usuarios = JsonConvert.DeserializeObject<List<GestionUsuarioDTO>>(res);
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 27/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario
        /// </summary>
        /// <returns> Entidad: usuarioEntidad </returns>
        public IEnumerable<ComboDTO> ObtenerComboRol()
        {
            try
            {
                List<ComboDTO> roles = new List<ComboDTO>();
                string _query = @"SELECT Id,Nombre FROM conf.T_UsuarioRol WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(res) && res != "[]")
                {
                    roles = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
