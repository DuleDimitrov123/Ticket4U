using Common;
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
        IList<string> errorMessages = new List<string>();

        if (string.IsNullOrEmpty(currency))
        {
            errorMessages.Add(DefaultErrorMessages.MoneyCurrencyRequired);
        }

        if (currency.Length != 3)
        {
            errorMessages.Add(DefaultErrorMessages.MoneyCurrencyLength);
        }

        if (amount < 0)
        {
            errorMessages.Add(DefaultErrorMessages.MoneyAmountNotNegative);
        }

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Money(currency, amount);
    }
}
