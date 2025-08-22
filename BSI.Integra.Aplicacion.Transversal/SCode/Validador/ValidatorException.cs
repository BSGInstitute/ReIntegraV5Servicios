namespace BSI.Integra.Aplicacion.Transversal.Validador
{
    public class ValidatorException : Exception
    {
        public int IdContactoDuplicado { get; set; }
        public ValidatorException()
        {
        }

        public ValidatorException(string message) : base(message)
        {
        }

        public ValidatorException(string message, Exception e) : base(message, e)
        {
        }
    }
}
