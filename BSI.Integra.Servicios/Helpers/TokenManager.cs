using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.Win32;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Helpers
{
    public interface ITokenManager
    {
        public int IdPersonal { get; }
        public int IdRol { get; }
        public string AreaTrabajo { get; }
        public string UserName { get; }
        public string UserAsp { get; }
        public DateTime Expira { get; }
        public string TipoPersonal { get; }
    }
    public class TokenManager : ITokenManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int? _idPersonal = null;
        private int? _idRol = null;
        private string? _areaTrabajo = null;
        private string? _userName = null;
        private string? _userAsp = null;
        private DateTime? _expira = null;
        private string? _tipoPersonal = null;
        private RegistroClaimTokenDTO? _claimToken = null;
        public TokenManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public RegistroClaimTokenDTO RegistroClaimToken
        {
            get
            {
                return _claimToken ?? GetRegistroClaimToken();
            }
        }

        public int IdPersonal
        {
            get
            {
                return _idPersonal ?? GetIdPersonal();
            }
        }

        public int IdRol
        {
            get
            {
                return _idRol ?? GetIdRol();
            }
        }

        public string AreaTrabajo
        {
            get
            {
                return _areaTrabajo ?? GetAreaTrabajo();
            }
        }

        public string UserName
        {
            get
            {
                return _userName ?? GetUserName();
            }
        }

        public string UserAsp
        {
            get
            {
                return _userAsp ?? GetUserAsp();
            }
        }

        public DateTime Expira
        {
            get
            {
                return _expira ?? GetExpira();
            }
        }

        public string TipoPersonal
        {
            get
            {
                return _tipoPersonal ?? GetTipoPersonal();
            }
        }

        private void LoadRegistroClaimToken()
        {
            var claimsIndentity = _httpContextAccessor.HttpContext!.User.Identity as ClaimsIdentity;
            _claimToken = new RegistroClaimTokenDTO()
            {
                IdPersonal = Convert.ToInt32(claimsIndentity.Claims.Where(x => x.Type == "IdPersonal").Select(s => s.Value).First()),
                IdRol = Convert.ToInt32(claimsIndentity.Claims.Where(x => x.Type == "IdRol").Select(s => s.Value).First()),
                AreaTrabajo = claimsIndentity.Claims.Where(x => x.Type == "AreaTrabajo").Select(s => s.Value).First(),
                UserName = claimsIndentity.Claims.Where(x => x.Type == "UserName").Select(s => s.Value).First(),
                UserAsp = claimsIndentity.Claims.Where(x => x.Type == "UserAsp").Select(s => s.Value).First(),
                Expira = claimsIndentity.Claims.Where(x => x.Type == "Expira").Select(s => s.Value).First(),
                TipoPersonal = claimsIndentity.Claims.Where(x => x.Type == "TipoPersonal").Select(s => s.Value).First()
            };
            //_claimToken.UserName += "_v5";

            _idPersonal = _claimToken.IdPersonal;
            _idRol = _claimToken.IdRol;
            _areaTrabajo = _claimToken.AreaTrabajo;
            _userName = _claimToken.UserName;
            _userAsp = _claimToken.UserAsp;
            //_expira = Convert.ToDateTime(_claimToken.Expira);
            _expira = DateTime.UtcNow.AddDays(15);
            _tipoPersonal = _claimToken.TipoPersonal;
        }
        private RegistroClaimTokenDTO GetRegistroClaimToken()
        {
            LoadRegistroClaimToken();
            return _claimToken!;
        }
        private int GetIdPersonal()
        {
            LoadRegistroClaimToken();
            return _idPersonal!.Value;
        }
        private int GetIdRol()
        {
            LoadRegistroClaimToken();
            return _idRol!.Value;
        }
        private string GetAreaTrabajo()
        {
            LoadRegistroClaimToken();
            return _areaTrabajo!;
        }
        private string GetUserName()
        {
            LoadRegistroClaimToken();
            return _userName!;
        }
        private string GetUserAsp()
        {
            LoadRegistroClaimToken();
            return _userAsp!;
        }
        private DateTime GetExpira()
        {
            LoadRegistroClaimToken();
            return _expira!.Value;
        }
        private string GetTipoPersonal()
        {
            LoadRegistroClaimToken();
            return _tipoPersonal ?? "";
        }
    }
}
