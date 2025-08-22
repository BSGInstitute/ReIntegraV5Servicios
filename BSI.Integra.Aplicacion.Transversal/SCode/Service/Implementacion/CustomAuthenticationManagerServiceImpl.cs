using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public class CustomAuthenticationManagerServiceImpl : ICustomAuthenticationManagerService
    {
        private readonly IDictionary<string, string> tokens = new Dictionary<string, string>();
        public IDictionary<string, string> Tokens => tokens;

        private IConfiguration configuration;
        private IUnitOfWork _unitOfWork;

        private readonly string tokenKey;

        public CustomAuthenticationManagerServiceImpl(IConfiguration _configuration, IUnitOfWork unitOfWork)
        {
            configuration = _configuration;
            this.tokenKey = configuration.GetSection("TokenKey").Value; ;
            _unitOfWork = unitOfWork;
        }

        public AspNetUserAutenticateDTO Authenticate(string username, string password)
        {
            AspNetUserAutenticateDTO _registro = new AspNetUserAutenticateDTO();

            try
            {
                var resultadoAutenticacion = _unitOfWork.CustomAuthenticationManagerRepository.AutenticacionUsuarioPortal(username, password);

                if (resultadoAutenticacion == null)
                {

                    _registro.Excepcion = new ExcepcionRegistroDTO();
                    _registro.Excepcion.ExcepcionGenerada = true;
                    _registro.Excepcion.DescripcionGeneral = "Usuario o clave incorrectos, vuelva a ingresar los datos correctamente.";

                    return _registro;
                }
                else
                {
                    resultadoAutenticacion.Excepcion = new ExcepcionRegistroDTO();
                    resultadoAutenticacion.Excepcion.ExcepcionGenerada = false;
                    resultadoAutenticacion.Excepcion.DescripcionGeneral = "";
                }

                var ecKeyTemp = Encoding.ASCII.GetBytes(tokenKey);

                // Note that the ecKey should have 256 / 8 length:
                byte[] ecKey = new byte[256 / 8];
                DateTime fechaExpiracion = DateTime.UtcNow.AddDays(15);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(tokenKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, username),
                        new Claim("IdPersonal", resultadoAutenticacion.IdPersonal.ToString()),
                        new Claim("IdRol", resultadoAutenticacion.IdRol.ToString()),
                        new Claim("AreaTrabajo", resultadoAutenticacion.AreaTrabajo.ToString()),
                        new Claim("UserName", resultadoAutenticacion.UserName),
                        new Claim("UserAsp", resultadoAutenticacion.Id),
                        new Claim("Expira", fechaExpiracion.ToString()),
                        new Claim("TipoPersonal", resultadoAutenticacion.TipoPersonal.ToString())
                    }),
                    Expires = fechaExpiracion,
                    SigningCredentials = new SigningCredentials(
                       new SymmetricSecurityKey(key),
                       SecurityAlgorithms.HmacSha256Signature),
                    EncryptingCredentials = new EncryptingCredentials(

                         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                         SecurityAlgorithms.Aes128KW,
                         SecurityAlgorithms.Aes128CbcHmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                resultadoAutenticacion.Token = tokenHandler.WriteToken(token);

                _registro.Token = resultadoAutenticacion.Token;
                _registro.IdPersonal = resultadoAutenticacion.IdPersonal;
                _registro.IdRol = resultadoAutenticacion.IdRol;
                _registro.AreaTrabajo = resultadoAutenticacion.AreaTrabajo;
                _registro.UserName = resultadoAutenticacion.UserName;
                _registro.Excepcion = resultadoAutenticacion.Excepcion;
                _registro.TipoPersonal = resultadoAutenticacion.TipoPersonal;
            }
            catch (Exception ex)
            {
                _registro.Token = "";
                _registro.Excepcion = new ExcepcionRegistroDTO();
                _registro.Excepcion.ExcepcionGenerada = true;
                _registro.Excepcion.DescripcionGeneral = ex.Message;
            }

            return _registro;
        }
    }
}
