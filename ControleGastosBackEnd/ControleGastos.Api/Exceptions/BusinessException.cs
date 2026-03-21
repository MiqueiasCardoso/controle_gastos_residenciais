namespace ControleGastos.Api.Exceptions
{
    public class BusinessException : AppException
    {
        public BusinessException(string message) : base(message, 400) { }
    }
}
