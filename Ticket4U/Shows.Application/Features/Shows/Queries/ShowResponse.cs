namespace Shows.Application.Features.Shows.Queries;

public class ShowResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    public DateTime StartingDateTime { get; set; }

    public string Status { get; set; }

    public int NumberOfplaces { get; set; }

    public string TicketPriceCurrency { get; set; }

    public decimal TickerPriceAmount { get; set; }

    public Guid PerformerId { get; set; }

    public Guid CategoryId { get; set; }
}
