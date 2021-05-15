namespace CryptoExchange.Core.IntegrationModels
{
    public class MainReturn
    {
        public string Message { get; set; }
        public bool Sucess { get; set; }
        public object Data { get; set; }

        public MainReturn(string message)
        {
            Message = message;
        }

        public MainReturn(string message, object data)
        {
            Message = message;
            Sucess = true;
            Data = data;
        }
    }
}
