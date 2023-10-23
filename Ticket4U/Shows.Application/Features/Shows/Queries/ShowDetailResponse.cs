namespace Shows.Application.Features.Shows.Queries;

public class ShowDetailResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public string Status { get; set; }

    public int NumberOfplaces { get; set; }

    public string TicketPriceCurrency { get; set; }

    public decimal TickerPriceAmount { get; set; }

    public Guid PerformerId { get; set; }

    public Guid CategoryId { get; set; }

    public IList<ShowMessageResponse> ShowMessages { get; set; }
}
