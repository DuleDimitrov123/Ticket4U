﻿namespace Shows.Api.Requests.Shows;

public record CreateShowRequest
{
    public string Name { get; set; }

    public string Location { get; set; }

    public int NumberOfplaces { get; set; }

    public string TicketPriceCurrency { get; set; }

    public int TickerPriceAmount { get; set; }

    public Guid PerformerId { get; set; }

    public Guid CategoryId { get; set; }
}
