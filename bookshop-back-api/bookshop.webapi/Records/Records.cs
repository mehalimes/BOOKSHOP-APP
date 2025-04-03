namespace bookshop.webapi.Records
{
    public record PaymentBody (
        string Email,
        string ExpireMonth,
        string ExpireYear,
        string CardNumber,
        string Name,
        string Surname,
        string CardHolderName,
        string CVC,
        string AddressDescription,
        string Country,
        string City
    );
}
