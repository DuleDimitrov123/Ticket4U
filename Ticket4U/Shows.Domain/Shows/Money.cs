using Shows.Domain.Common;

namespace Shows.Domain.Shows;

/// <summary>
/// Money
/// </summary>
/// <param name="Currency">Currency of the money</param>
/// <param name="Amount">Amount of the money</param>
public record Money : ValueObject
{
    public string Currency { get; private set; }

    public decimal Amount { get; private set; }

    private Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public static Money Create(string currency, decimal amount)
    {
        if (string.IsNullOrEmpty(currency))
        {
            //TODO: throw domain exception
            throw new ArgumentNullException(nameof(currency));
        }

        if (currency.Length != 3)
        {
            //TODO: throw domain exception
            throw new Exception("Lenght of currency string should be 3");
        }

        if (amount < 0)
        {
            //TODO: throw domain exception
            throw new Exception("Amount of money cannot be negative");
        }

        return new Money(currency, amount);
    }
}
