namespace bookshop.webapi.Interfaces
{
    public interface IHttpClientService
    {
        Task<bool> GetBooksFromGoogle();
        void MakePayment();
        void VerifyPayment();
    }
}
