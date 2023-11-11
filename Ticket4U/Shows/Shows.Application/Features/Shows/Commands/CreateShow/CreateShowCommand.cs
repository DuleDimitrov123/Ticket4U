using MediatR;
using Shows.Domain.Shows;

namespace Shows.Application.Features.Shows.Commands.CreateShow;

public class CreateShowCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Picture { get; set; }

    public string Location { get; set; }

    public int NumberOfplaces { get; set; }

    public string TicketPriceCurrency { get; set; }

    public int TickerPriceAmount { get; set; }

    public DateTime StartingDateTime { get; set; }

    public Guid PerformerId { get; set; }

    public Guid CategoryId { get; set; }

    public Show ToShow()
    {
        return Show.Create(Name,
            Description,
            Picture,
            Location,
            NumberOfPlaces.Create(NumberOfplaces),
            Money.Create(TicketPriceCurrency, TickerPriceAmount),
            StartingDateTime,
            PerformerId,
            CategoryId);
    }
}
