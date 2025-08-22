using BSI.Integra.Repositorio.UnitOfWork;
using System.Net.Mail;

namespace BSI.Integra.Aplicacion.Transversal.Validador
{
    public class Validador
    {
        private static readonly Dictionary<string, string> PhoneStatusCodesNegativos = new Dictionary<string, string>
        {
            { "PE01", "Teléfono inválido: El código de área/número de teléfono no existe en nuestra base de datos o contiene caracteres no numéricos." },
            { "PE02", "Teléfono en blanco: El número de teléfono está vacío." },
            { "PE03", "Teléfono incorrecto: El número de teléfono tiene demasiados o muy pocos dígitos." },
            { "PE04", "Coincidencia múltiple: Hay dos o más posibles códigos de área disponibles como corrección y la distancia es demasiado cercana para elegir uno sobre el otro." },
            { "PE05", "Prefijo incorrecto: El prefijo telefónico o los primeros 7 dígitos no existen en nuestra base de datos." },
            { "PE11", "Teléfono desconectado: El número de teléfono ha sido desconectado." }
        };
        public static void ValidarEmail(string Email)
        {
            //var pattern = ConfiguracionReglas.getConfiguracion("Validar Email", "pattern") ?? "";
            //var nullable = Convert.ToBoolean(ConfiguracionReglas.getConfiguracion("Validar Email", "nullable"));
            //if (Email.Equals("") && nullable) return;
            //bool isEmail = Regex.IsMatch(Email, @pattern, RegexOptions.IgnoreCase);
            if (!IsEmailValid(Email))
            {
                var Exception = new ValidatorException("Email Inválido");
                throw Exception;
            }
        }

        public static bool IsEmailValid(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static void ValidarLongitudCelular(int? idPais, string celular, IUnitOfWork _unitOfWork)
        {
            if (!_unitOfWork.CiudadRepository.LongitudCelularPorPaisCorrecta(idPais, celular))
            {
                var Exception = new ValidatorException("Longitud celular Inválido");
                throw Exception;
            }
        }
    }
}
        
    
