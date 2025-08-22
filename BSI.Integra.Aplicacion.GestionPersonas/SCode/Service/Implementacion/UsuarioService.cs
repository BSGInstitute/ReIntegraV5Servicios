using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Web.Helpers;

using System.Security.Cryptography;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: UsuarioService
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/11/2022
    /// <summary>
    /// Gestión general de T_Usuario
    /// </summary>
    public class UsuarioService : IUsuarioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public UsuarioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TUsuario, Usuario>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
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
                return _unitOfWork.UsuarioRepository.ObtenerPorNombreUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 08/11/2023
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
                return _unitOfWork.UsuarioRepository.ObtenerTotalPorUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 08/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> Entidad: usuarioEntidad </returns>
        public bool InsertarUsuario(IntegraUsuarioDTO dto, string usuario)
        {
            try
            {
                var validacion = _unitOfWork.IntegraAspNetUserRepository.ExistePorNombreUsuario(dto.Usuario);
                var datosPersonal = _unitOfWork.PersonalRepository.ObtenerPorId(dto.IdPersonal);
                if (validacion) throw new Exception("El nombre de usuario ya existe");
                var insertarCorreoNuevaContrasenia = _unitOfWork.PersonalRepository.InsertarNuevaContrasena(new PersonalNuevaContraseniaDTO()
                {
                    Usuario = dto.Usuario,
                    NuevaContrasena = dto.Password
                });

                var user = new Usuario()
                {
                    IdPersonal = dto.IdPersonal,
                    NombreUsuario = dto.Usuario,
                    Clave = Encriptar(dto.Password),
                    IdUsuarioRol = dto.IdRol,
                    CodigoAreaTrabajo = datosPersonal.AreaAbrev,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario
                };
                _unitOfWork.UsuarioRepository.Add(user);
                _unitOfWork.Commit();

                var res = _unitOfWork.IntegraAspNetUserRepository.InsertarIntegraAspNetUser(new UserIntegraAspNetDTO()
                {
                    Usuario = dto.Usuario,
                    Password = dto.Password,
                    PasswordHash = Crypto.HashPassword(dto.Password),
                    PerId = dto.IdPersonal,
                    RolId = dto.IdRol,
                    AreaAbrev = datosPersonal.AreaAbrev,
                    Email = dto.Email
                }, usuario);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 08/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> Entidad: usuarioEntidad </returns>
        public bool ActualizarUsuario(IntegraUsuarioDTO dto, string usuario)
        {
            try
            {
                var datosPersonal = _unitOfWork.PersonalRepository.ObtenerPorId(dto.IdPersonal);
                var insertarCorreoNuevaContrasenia = _unitOfWork.PersonalRepository.InsertarNuevaContrasena(new PersonalNuevaContraseniaDTO()
                {
                    Usuario = dto.Usuario,
                    NuevaContrasena = dto.Password
                });

                var user = _unitOfWork.UsuarioRepository.ObtenerPorIdPersonal(dto.IdPersonal);
                user.NombreUsuario = dto.Usuario;
                user.Clave = Encriptar(dto.Password);
                user.IdUsuarioRol = dto.IdRol;
                user.CodigoAreaTrabajo = datosPersonal.AreaAbrev;
                user.FechaModificacion = DateTime.Now;
                user.UsuarioModificacion = usuario;
                _unitOfWork.UsuarioRepository.Update(user);
                _unitOfWork.Commit();

                var res = _unitOfWork.IntegraAspNetUserRepository.ActualizarIntegraAspNetUser(new UserIntegraAspNetDTO()
                {
                    Guid = dto.Guid,
                    Usuario = dto.Usuario,
                    Password = dto.Password,
                    PasswordHash = Crypto.HashPassword(dto.Password),
                    RolId = dto.IdRol,
                    AreaAbrev = datosPersonal.AreaAbrev,
                    Email = dto.Email
                }, usuario);
                return res;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 08/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> Entidad: usuarioEntidad </returns>
        public IEnumerable<GestionUsuarioDTO> ObtenerTodo()
        {
            try
            {
                return _unitOfWork.UsuarioRepository.ObtenerTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 08/11/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del usuario por el NombreUsuario
        /// </summary>
        /// <param name="usuario"> NombreUsuario de la tabla T_usuario </param>
        /// <returns> Entidad: usuarioEntidad </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.UsuarioRepository.ObtenerComboRol();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Encriptar(string us_clave)
        {
            return Encriptar(us_clave, "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
        }

        public string Encriptar(string textoQueEncriptaremos, string passBase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passBase,
              saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged()
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
              initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor,
             CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

    }
}
