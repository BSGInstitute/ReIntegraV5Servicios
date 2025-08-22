using System.Net.Mail;

namespace BSI.Integra.Aplicacion.Base.Classes
{
    public static class BaseHelper
    {

        /// <summary>
        /// Valida si un correo es valido
        /// </summary>
        /// <param name="email">Direccion email</param>        
        /// <returns> Bool </returns>
        public static bool EsCorreoValido(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
