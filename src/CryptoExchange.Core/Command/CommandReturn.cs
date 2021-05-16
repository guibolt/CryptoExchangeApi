namespace CryptoExchange.Core.Command
{
    public class CommandReturn
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Errors { get; private set; }
        public object Data { get; private set; }

        public CommandReturn(bool success)
        {
            Success = success;
        }

        public CommandReturn(bool success, string mensagem) : this(success)
        {
            Message = mensagem;
        }

        public CommandReturn(bool sucesso, object erros, string mensagem) : this(sucesso)
        {
            Errors = erros;
            Message = mensagem;
        }

        public CommandReturn(bool sucesso, string mensagem, object objetoRetorno) : this(sucesso, mensagem)
        {
            Data = objetoRetorno;
        }
    }
}
