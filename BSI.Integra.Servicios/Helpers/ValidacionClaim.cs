using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Helpers
{
    public class ValidacionClaim
    {
        public static RegistroTokenDTO ValidarClaimFechaExpiracion(ClaimsIdentity Registro)
        {
            bool _respuestaCorrecta = false;
            string _respuestaDescripcion = string.Empty;
            RegistroTokenDTO _registro = new RegistroTokenDTO();
            var _registroClaim = Registro.Claims.Where(x => x.Type == "Expira").FirstOrDefault();
            if (_registroClaim == null)
            {
                _respuestaCorrecta = false;
                _respuestaDescripcion = "Usuario invalido.";
            }
            else
            {
                if (string.IsNullOrEmpty(_registroClaim.Value))
                {
                    _respuestaCorrecta = false;
                    _respuestaDescripcion = "El codigo de sesión no es el correcto o ha sido alterado.";
                }
                else
                {
                    if (DateTime.UtcNow <= DateTime.UtcNow.AddDays(15))
                    {
                        _respuestaCorrecta = true;
                        _respuestaDescripcion = "";
                    }
                    else
                    {
                        _respuestaCorrecta = false;
                        _respuestaDescripcion = "La sesión ha expirado, vuelve a inicar sesión.";
                    }
                }
            }
            if (_respuestaCorrecta)
            {
                _registro.RegistroClaimToken = new RegistroClaimTokenDTO();
                _registro.RegistroClaimToken.IdPersonal = Convert.ToInt32(Registro.Claims.Where(x => x.Type == "IdPersonal").Select(s => s.Value).First());
                _registro.RegistroClaimToken.IdRol = Convert.ToInt32(Registro.Claims.Where(x => x.Type == "IdRol").Select(s => s.Value).First());
                _registro.RegistroClaimToken.AreaTrabajo = Registro.Claims.Where(x => x.Type == "AreaTrabajo").Select(s => s.Value).First();
                _registro.RegistroClaimToken.UserName = Registro.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First();
                _registro.RegistroClaimToken.UserAsp = Registro.Claims.Where(x => x.Type == "UserAsp").Select(s => s.Value).First();
                _registro.RegistroClaimToken.Expira = Registro.Claims.Where(x => x.Type == "Expira").Select(s => s.Value).First();
                _registro.RegistroClaimToken.TipoPersonal = Registro.Claims.Where(x => x.Type == "TipoPersonal").Select(s => s.Value).First();
            }
            else
            {
                _registro.RegistroClaimToken = null;
            }
            _registro.TokenValida = _respuestaCorrecta;
            _registro.DescripcionGeneral = _respuestaDescripcion;

            return _registro;
        }
        public static RegistroClaimTokenDTO ObtenerRegistroClaimToken(ClaimsIdentity claimsIdentity)
        {
            var _respuestaCorrecta = ValidarClaimFechaExpiracion(claimsIdentity);
            if (_respuestaCorrecta.TokenValida)
            {
                return _respuestaCorrecta.RegistroClaimToken;
            }
            else
                throw new UnauthorizedAccessRequestException(_respuestaCorrecta.DescripcionGeneral);
        }
    }
}
