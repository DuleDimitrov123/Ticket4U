﻿using Shared.Domain.Events;
using Shows.Application.Contracts.Outbox;

namespace Shows.IntegrationTests.Overrides;

public class TestShowPublisher : IShowPublisher
{
    public Task PublishCreatedShow(CreatedShowEvent createdShowEvent)
    {
        Console.WriteLine($"PUBLISHING CreatedShowEvent with Name: {createdShowEvent.Name}");

        return Task.CompletedTask;
    }
}
