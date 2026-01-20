using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: IntegraAspNetUserRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 08/06/2022
    /// <summary>
    /// Gestión general de T_IntegraAspNetUsers
    /// </summary>
    public class IntegraAspNetUserRepository : GenericRepository<TIntegraAspNetUser>, IIntegraAspNetUserRepository
    {
        private Mapper _mapper;

        public IntegraAspNetUserRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIntegraAspNetUser, IntegraAspNetUser>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TIntegraAspNetUser MapeoEntidad(IntegraAspNetUser entidad)
        {
            try
            {
                //crea la entidad padre
                TIntegraAspNetUser modelo = _mapper.Map<TIntegraAspNetUser>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TIntegraAspNetUser Add(IntegraAspNetUser entidad)
        {
            try
            {
                var IntegraAspNetUser = MapeoEntidad(entidad);
                base.Insert(IntegraAspNetUser);
                return IntegraAspNetUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TIntegraAspNetUser Update(IntegraAspNetUser entidad)
        {
            try
            {
                var IntegraAspNetUser = MapeoEntidad(entidad);

                base.Update(IntegraAspNetUser);
                return IntegraAspNetUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TIntegraAspNetUser> Add(IEnumerable<IntegraAspNetUser> listadoEntidad)
        {
            try
            {
                List<TIntegraAspNetUser> listado = new List<TIntegraAspNetUser>();
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

        public IEnumerable<TIntegraAspNetUser> Update(IEnumerable<IntegraAspNetUser> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TIntegraAspNetUser> listado = new List<TIntegraAspNetUser>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombres de Usuario dado su NombreUsuario 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns>PersonalAreaTrabajoComboDTO</returns>
        public ComboDTO ObtenerIdentidadUsusario(string usuario)
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = "[conf].[SP_ObtenerIdNombresPersonalPorUsername]";
                var resultado = _dapperRepository.QuerySPDapper(query, new { UsuarioParam = usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                    if (rpta.Count > 1)
                        throw new Exception("Error: Existe mas de un usuario que coincide con el parametro dado");
                    else return rpta[0];
                }
                else
                {
                    throw new Exception("Error: Ningun usuario coincide con el parametro dado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el Id y Nombres de Usuario dado su NombreUsuario 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <returns></returns>
        public DatoPersonalDTO ObtenerIdentidadUsusarioV2(string usuario)
        {
            try
            {
                List<DatoPersonalDTO> rpta = new List<DatoPersonalDTO>();
                var _query = "conf.SP_ObtenerIdNombresPersonalPorUsername";
                var resultado = _dapperRepository.QuerySPDapper(_query, new { Usuario = usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DatoPersonalDTO>>(resultado)!;
                    if (rpta.Count() > 1)
                        throw new Exception("Error: Existe mas de un usuario que coincide con el parametro dado");
                    else return rpta[0];
                }
                else
                {
                    throw new Exception("Error: Ningun usuario coincide con el parametro dado");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id del personal por el usuario
        /// </summary>
        /// <param name="usuario"> usuario del personal</param>
        /// <returns>IntegraAspNetUser</returns>
        public IntegraAspNetUser ObtenerIdPersonalPorUsuario(string usuario)
        {
            try
            {
                IntegraAspNetUser rpta = new IntegraAspNetUser();
                var query = @"SELECT IU.PerId FROM conf.T_Integra_AspNetUsers AS IU 
                            WHERE IU.UserName = @usuario AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { usuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IntegraAspNetUser>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de accesos de ips configurados por ipPublica
        /// </summary>
        /// <returns> AccesoIpConfiguracionDTO </returns>
        public AccesoIpConfiguracionDTO? ObtenerAccesoPorIp(string ipPublica)
        {
            try
            {
                var query = @"SELECT Id, Nombre, IpPublica, Descripcion, FechaExpira FROM conf.T_AccesoIpConfiguracion WHERE Estado = 1 ";
                //var query = @"SELECT TOP 1 Id, Nombre, @ipPublica as IpPublica, Descripcion, FechaExpira 
                //     FROM conf.T_AccesoIpConfiguracion 
                //     WHERE Estado = 1 
                //     ORDER BY Id";


                var resultado = _dapperRepository.FirstOrDefault(query, new { ipPublica });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<AccesoIpConfiguracionDTO>(resultado)!;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Version: 1.0
        /// <summary>
        /// Validar relogin por usuario
        /// </summary>
        /// <returns> AccesoIpConfiguracionDTO </returns>
        public StringDTO ValidarReLogin(string usuario)
        {
            var rpta = new StringDTO()
            {
                Valor = "0"
            };
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("conf.SP_ValidarReLogin", new { usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception)
            {
                return rpta;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/12/2023
        /// Version: 1.0
        /// <summary>
        /// actualiza relogin por usuario
        /// </summary>
        /// <returns> AccesoIpConfiguracionDTO </returns>
        public StringDTO ActualizarReLogin(string usuario)
        {
            var rpta = new StringDTO()
            {
                Valor = "0"
            };
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("conf.SP_ActualizarReLogin", new { usuario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception)
            {
                return rpta;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Integra_AspNetUsers por Usuario
        /// </summary>
        /// <returns> List<IntegraAspNetUserDTO> </returns>
        public IEnumerable<IntegraAspNetUserDTO> ObtenerPorUsuario(string usuarioNombre)
        {
            try
            {
                List<IntegraAspNetUserDTO> respuesta = new List<IntegraAspNetUserDTO>();

                var query = @"SELECT Id, UsClave, PerId, RolId, AreaTrabajo, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed,
                            TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName FROM conf.T_Integra_AspNetUsers WHERE Estado = 1 AND 
                            UserName = @UsuarioNombre ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { UsuarioNombre = usuarioNombre });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<IntegraAspNetUserDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener información por usuario para visualización de módulos agrupados
        /// </summary>
        /// <param name="usuario">Usuario Personal</param>
        /// <returns>Lista de registros por Usuario</returns>
        /// <returns> lista de objetos DTO: List<ModuloCreacionAgrupadoDTO> </returns>
        public List<ModuloAgrupacionDTO> ObtenerDatosParaModuloAgrupado(string usuario)
        {
            try
            {
                var modulos = new List<ModuloAgrupacionDTO>();
                var query = @"SELECT 
                                IdModulo,NombreModulo,IdGrupo,NombreGrupo,URL,IdModuloSistemaTipo, NombreModuloSistemaTipo, Etiqueta, Icono 
                            FROM 
                                gp.V_ObtenerDataModuloDinamicoAgrupadoV5 
                            WHERE 
                                NombreUsuario=@usuario AND Estado=1 ORDER BY IdGrupo, IdModuloSistemaTipo, OrdenMenuPrincipal, NombreModulo, Etiqueta,IdModulo";
                var asignarModuloDB = _dapperRepository.QueryDapper(query, new { usuario });
                if (!string.IsNullOrEmpty(asignarModuloDB) && !asignarModuloDB.Contains("[]"))
                {
                    modulos = JsonConvert.DeserializeObject<List<ModuloAgrupacionDTO>>(asignarModuloDB)!;
                }
                return modulos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 19/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_Integra_AspNetUsers por PerId.
        /// </summary>
        /// <param name="perId"></param>
        /// <returns> IntegraAspNetUser </returns>
        public IntegraAspNetUser ObtenerPorIdPersonal(int perId)
        {
            try
            {
                IntegraAspNetUser resultado = new IntegraAspNetUser();
                var query = @"
                            SELECT 
                                Id, UsClave, PerId, RolId, AreaTrabajo, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, 
                                LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion 
                            FROM 
                                conf.T_Integra_AspNetUsers 
                            WHERE 
                                Estado = 1 AND PerId = @PerId";
                var respuesta = _dapperRepository.FirstOrDefault(query, new { PerId = perId });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<IntegraAspNetUser>(respuesta)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Indica si existe un usuario por userName
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public bool ExistePorNombreUsuario(string nombreUsuario)
        {
            try
            {
                return this.Exist(x => x.UserName == nombreUsuario);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Margiory Ramirez
        ///Fecha: 03/01/2023
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Email Usuario</returns>
        public string ObtenerEmailPorNombreUsuario(string nombreUsuario)
        {
            try
            {
                string email = "";
                var query = @"SELECT Email FROM conf.T_Integra_AspNetUsers WHERE 
                            UserName = @UsuarioNombre";
                var respuesta = _dapperRepository.FirstOrDefault(query, new { UsuarioNombre = nombreUsuario });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    var resultado = JsonConvert.DeserializeObject<dynamic>(respuesta)!;
                    email = resultado.Email;
                }

                return email;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Margiory Ramirez
        ///Fecha: 27/02/2025
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Email Usuario</returns>
        public string ObtenerEmailFiltro(string nombreUsuario)
        {
            try
            {
                return this.FirstBy(x => x.UserName == nombreUsuario).Email;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Entidad: IntegraAspNetUser </returns>
        public IntegraAspNetUser ObtenerPorNombreUsuario(string nombreUsuario)
        {
            try
            {
                IntegraAspNetUser integraAspNetUser = new IntegraAspNetUser();
                var query = @"
                            SELECT 
                                Id, UsClave, PerId, RolId, AreaTrabajo, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, 
                                LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion 
                            FROM 
                                conf.T_Integra_AspNetUsers 
                            WHERE 
                                UserName = @Username AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Username = nombreUsuario });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    integraAspNetUser = JsonConvert.DeserializeObject<IntegraAspNetUser>(resultado)!;
                }
                return integraAspNetUser;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Margiory Ramirez
        ///Fecha: 03/01/2023
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Email Usuario</returns>
        public bool InsertarIntegraAspNetUser(UserIntegraAspNetDTO dto, string creador)
        {
            try
            {
                var query = "conf.SP_InsertarIntegraAspNetUser";
                var respuesta = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    dto.Usuario,
                    dto.Password,
                    dto.PasswordHash,
                    dto.PerId,
                    dto.RolId,
                    dto.AreaAbrev,
                    dto.Email,
                    creador
                });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("null")) return true;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        ///Repositorio: PersonalRepositorio
        ///Autor: Margiory Ramirez
        ///Fecha: 03/01/2023
        /// <summary>
        /// Obtener email por nombre usuario
        /// </summary>
        /// <param name="nombreUsuario"> Nombre Usuario </param>
        /// <returns> Email Usuario</returns>
        public bool ActualizarIntegraAspNetUser(UserIntegraAspNetDTO dto, string modificador)
        {
            try
            {
                var query = "conf.SP_ActualizarIntegraAspNetUser";
                var respuesta = _dapperRepository.QuerySPFirstOrDefault(query, new
                {
                    dto.Guid,
                    dto.Usuario,
                    dto.Password,
                    dto.PasswordHash,
                    dto.RolId,
                    dto.AreaAbrev,
                    dto.Email,
                    modificador
                });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("null")) return true;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IntegraAspNetUser ObtenerPorId(int perId)
        {
            try
            {
                IntegraAspNetUser respuesta = new IntegraAspNetUser();

                var query = @"SELECT Id,
                               UsClave,
                               PerId,
                               RolId,
                               AreaTrabajo,
                               Email,
                               EmailConfirmed,
                               PasswordHash,
                               SecurityStamp,
                               PhoneNumber,
                               PhoneNumberConfirmed,
                               TwoFactorEnabled,
                               LockoutEndDateUtc,
                               LockoutEnabled,
                               AccessFailedCount,
                               UserName,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion FROM conf.T_Integra_AspNetUsers WHERE Estado = 1 AND 
                                    PerId = @perId ORDER BY Id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { perId = perId });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<IntegraAspNetUser>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
